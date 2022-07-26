using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Socks5Server
{
    public class Socks5
    {
        private TcpListener TcpListener { get; set; }
        private IPEndPoint LocalEndPoint { get; set; }
        public bool AuthRequired { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public Socks5(IPAddress address, int port, string username = "", string password = "")
        {
            try
            {
                TcpListener = new TcpListener(address, port);
                LocalEndPoint = new IPEndPoint(address, port);

                if (!string.IsNullOrEmpty(username))
                {
                    AuthRequired = true;
                    Username = username;
                    Password = password;
                }
            }
            catch (Exception ex)
            {
            }
        }

        public class Command
        {
            public class Constants
            {
                public enum Command : byte
                {
                    Connect = 0x01,
                    Bind = 0x02,
                    UdpAssociate = 0x03
                }

                public enum AddressType : byte
                {
                    IPv4 = 0x01,
                    Domain = 0x03,
                    IPv6 = 0x04
                }

                public enum AuthMethods : byte
                {
                    NoAuthenticationRequired = 0x00,
                    GSSAPI = 0x01,
                    UsernamePassword = 0x02,
                    NonAcceptableMethod = 0xFF
                }
            }

            public byte SocksVersion { get; private set; }
            public Constants.Command SocksCommand { get; private set; }
            public Constants.AddressType AddressType { get; private set; }
            public string Domain { get; private set; }
            public IPAddress DestinationAddress { get; private set; }
            public ushort DestinationPort { get; private set; }
            public bool DnsSuccess { get; private set; }

            public bool Parse(byte[] response)
            {
                using (var stream = new MemoryStream(response))
                {
                    using (var reader = new BinaryReader(stream))
                    {
                        //        +----+-----+-------+------+----------+----------+
                        //        |VER | CMD |  RSV  | ATYP | DST.ADDR | DST.PORT |
                        //        +----+-----+-------+------+----------+----------+
                        //        | 1  |  1  | X'00' |  1   | Variable |    2     |
                        //        +----+-----+-------+------+----------+----------+
                        //
                        //     Where:
                        //
                        //          o  VER    protocol version: X'05'
                        //          o  CMD
                        //             o  CONNECT X'01'
                        //             o  BIND X'02'
                        //             o  UDP ASSOCIATE X'03'
                        //          o  RSV    RESERVED
                        //          o  ATYP   address type of following address
                        //             o  IP V4 address: X'01'
                        //             o  DOMAINNAME: X'03'
                        //             o  IP V6 address: X'04'
                        //          o  DST.ADDR       desired destination address
                        //          o  DST.PORT desired destination port in network octet
                        //             order

                        if (reader.BaseStream.Length < 10)
                            return false;

                        SocksVersion = reader.ReadByte();

                        SocksCommand = (Constants.Command) reader.ReadByte();
                        if (!Enum.IsDefined(typeof(Constants.Command), SocksCommand))
                            return false;

                        if (reader.ReadByte() != 0)
                            return false;

                        AddressType = (Constants.AddressType) reader.ReadByte();
                        if (!Enum.IsDefined(typeof(Constants.AddressType), AddressType))
                            return false;

                        if (AddressType == Constants.AddressType.IPv4)
                        {
                            if (reader.BaseStream.PeekBytes() != 6)
                                return false;
                            DestinationAddress = new IPAddress((uint) reader.ReadInt32());
                        }
                        else if (AddressType == Constants.AddressType.IPv6)
                        {
                            if (reader.BaseStream.PeekBytes() != 18)
                                return false;
                            DestinationAddress = new IPAddress(reader.ReadBytes(16));
                        }
                        else if (AddressType == Constants.AddressType.Domain)
                        {
                            var domainLength = reader.ReadByte();
                            if (reader.BaseStream.PeekBytes() != domainLength + 2)
                                return false;
                            Domain = Encoding.UTF8.GetString(reader.ReadBytes(domainLength));

                            try
                            {
                                var dnsResults = Dns.GetHostEntry(Domain);
                                DestinationAddress = dnsResults.AddressList
                                    .Where(x => x.AddressFamily == AddressFamily.InterNetwork).FirstOrDefault();
                                DnsSuccess = true;
                            }
                            catch (Exception)
                            {
                            }

                            if (DestinationAddress == null)
                            {
                                DestinationAddress = IPAddress.Loopback;
                                DnsSuccess = false;
                            }
                        }

                        DestinationPort = (ushort) IPAddress.NetworkToHostOrder(reader.ReadInt16());
                        return true;
                    }
                }
            }
        }

        public static class ConnectionManager
        {
            public static ConcurrentDictionary<string, ConnectionFactory> Keys { get; set; } = new();

            public static void CreateConnection(string identifier, TcpClient _client, IPEndPoint _localEndPoint,
                string username, string password)
            {
                Task.Run(() =>
                {
                    try
                    {
                        var guid = Guid.NewGuid();
                        Keys.TryAdd(guid.ToString(),
                            new ConnectionFactory(identifier, _client, _localEndPoint, guid, username, password));
                        Keys.FirstOrDefault(_ => _.Key == guid.ToString()).Value?.ConnectionWorker();
                    }
                    catch (Exception ex)
                    {
                    }
                });
            }

            public static void UpdateConnection(string identifier, Socks5State Socks5State)
            {
                if (Socks5State.Socks5Status == Socks5Status.NewConnection)
                    Task.Run(async () =>
                    {
                        try
                        {
                            await Keys.FirstOrDefault(_ => _.Key == Socks5State.Guid.ToString()).Value
                                ?.ConnectionReady(Socks5State);
                        }
                        catch (Exception ex)
                        {
                        }
                        finally
                        {
                            try
                            {
                                QueueManager.EnqueueElement(identifier, new Socks5State
                                {
                                    Guid = Socks5State.Guid,
                                    Socks5Status = Socks5Status.Error
                                });

                                Keys.FirstOrDefault(_ => _.Key == Socks5State.Guid.ToString()).Value?.Dispose();
                                Keys.TryRemove(Socks5State.Guid.ToString(), out _);
                            }
                            catch (Exception ex)
                            {
                            }
                        }
                    });
                if (Socks5State.Socks5Status == Socks5Status.Ok)
                    Task.Run(() =>
                    {
                        try
                        {
                            Keys.FirstOrDefault(_ => _.Key == Socks5State.Guid.ToString()).Value?.Socks5Data
                                ?.OnDataReceived(Socks5State);
                        }
                        catch
                        {
                        }
                    });
                if (Socks5State.Socks5Status == Socks5Status.Error)
                    Task.Run(() =>
                    {
                        try
                        {
                            Keys.FirstOrDefault(_ => _.Key == Socks5State.Guid.ToString()).Value?.Dispose();
                            Keys.TryRemove(Socks5State.Guid.ToString(), out _);
                        }
                        catch
                        {
                        }
                    });
            }
        }

        public class ConnectionFactory : IDisposable
        {
            public IPEndPoint LocalEndPoint { get; set; }
            public TcpClient Client { get; set; }
            public Guid Guid { get; set; }
            public Socks5Data Socks5Data { get; set; }
            public string Identifier { get; private set; }
            public bool AuthRequired { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }

            public ConnectionFactory(string identifier, TcpClient client, IPEndPoint localEndPoint, Guid guid)
            {
                Identifier = identifier;
                LocalEndPoint = localEndPoint;
                Client = client;
                Guid = guid;
            }

            public ConnectionFactory(string identifier, TcpClient client, IPEndPoint localEndPoint, Guid guid,
                string username, string password)
            {
                Identifier = identifier;
                LocalEndPoint = localEndPoint;
                Client = client;
                Guid = guid;
                Username = username;
                Password = password;

                if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password)) AuthRequired = true;

                TcpClientMonitor();
            }

            public void TcpClientMonitor()
            {
                Task.Run(() =>
                {
                    while (Client?.GetState() == TcpState.Established) Thread.Sleep(100);

                    ConnectionManager.UpdateConnection(Identifier, new Socks5State
                    {
                        Socks5Status = Socks5Status.Error,
                        Guid = Guid
                    });
                });
            }

            public void ConnectionWorker()
            {
                var stream = Client.GetStream();

                var firstRequest = StreamReadToEnd(stream);

                var method = GetConnectionMethod(firstRequest);

                if (method == Command.Constants.AuthMethods.NoAuthenticationRequired ||
                    method == Command.Constants.AuthMethods.GSSAPI)
                {
                    StreamWrite(stream, ReplyAuthentication(Command.Constants.AuthMethods.NoAuthenticationRequired));

                    var secondRecive = StreamReadToEnd(stream);

                    QueueManager.EnqueueElement(Identifier, new Socks5State
                    {
                        Guid = Guid,
                        Bytes = secondRecive,
                        Socks5Status = Socks5Status.NewConnection
                    });
                }
                else if (method == Command.Constants.AuthMethods.UsernamePassword)
                {
                    if (AuthRequired)
                    {
                        StreamWrite(stream, ReplyAuthentication(Command.Constants.AuthMethods.UsernamePassword));

                        var secondRecive = StreamReadToEnd(stream);
                        var usernameLength = secondRecive[1];
                        var username = Encoding.ASCII.GetString(secondRecive, 2, usernameLength);
                        var passwordLength = secondRecive[1 + usernameLength + 1];
                        var password = Encoding.ASCII.GetString(secondRecive, 3 + usernameLength, passwordLength);

                        if (username == Username && password == Password)
                            ReplyAuthentication(stream, 0);
                        else
                            ReplyAuthentication(stream, 255);

                        var thirdRecive = StreamReadToEnd(stream);

                        QueueManager.EnqueueElement(Identifier, new Socks5State
                        {
                            Guid = Guid,
                            Bytes = thirdRecive,
                            Socks5Status = Socks5Status.NewConnection
                        });
                    }
                    else
                    {
                        StreamWrite(stream, ReplyAuthentication(Command.Constants.AuthMethods.NonAcceptableMethod));
                    }
                }
                else
                {
                    StreamWrite(stream, ReplyAuthentication(Command.Constants.AuthMethods.NonAcceptableMethod));
                }
            }

            public async Task ConnectionReady(Socks5State Socks5State)
            {
                if (Socks5State.ProtocolType == ProtocolType.Tcp)
                {
                    StreamWrite(Client.GetStream(), ConfirmationResponse(LocalEndPoint.Port));

                    using (Socks5Data = new Socks5TCPData(Identifier, Guid, Client))
                    {
                        await Socks5Data.Start();
                    }
                }

                if (Socks5State.ProtocolType == ProtocolType.Udp)
                {
                    var udpClient = new UdpClient(new IPEndPoint(LocalEndPoint.Address, 0));

                    StreamWrite(Client.GetStream(),
                        ConfirmationResponse((udpClient.Client.LocalEndPoint as IPEndPoint).Port));

                    using (Socks5Data = new Socks5UDPData(Identifier, Guid, Client, udpClient))
                    {
                        await Socks5Data.Start();
                    }
                }
            }

            private void ReplyAuthentication(NetworkStream stream, byte status)
            {
                stream.Write(new byte[] {1, status}, 0, 2);
            }

            private byte[] StreamReadToEnd(NetworkStream stream)
            {
                var bytes = new byte[1024];
                var count = stream.Read(bytes, 0, bytes.Length);
                var result = new byte[count];
                Array.Copy(bytes, result, count);
                return result;
            }

            private void StreamWrite(NetworkStream stream, byte[] bytes)
            {
                stream.Write(bytes, 0, bytes.Length);
            }

            private Command.Constants.AuthMethods GetConnectionMethod(byte[] firstRequest)
            {
                try
                {
                    if (firstRequest[0] == 5 && firstRequest[1] > 0)
                        return (Command.Constants.AuthMethods) firstRequest[1];
                }
                catch
                {
                    return Command.Constants.AuthMethods.NonAcceptableMethod;
                }

                return Command.Constants.AuthMethods.NonAcceptableMethod;
            }

            private byte[] ReplyAuthentication(Command.Constants.AuthMethods authMethod)
            {
                var responce = new byte[] {0x05, (byte) authMethod};
                return responce;
            }

            private byte[] ConfirmationResponse(int socketPort)
            {
                var port = BitConverter.GetBytes(socketPort);
                var address = LocalEndPoint.Address;
                var ip = address.GetAddressBytes();
                var response = new byte[]
                    {5, 0, 0, 1, ip[0], ip[1], ip[2], ip[3], port[1], port[0]};
                return response;
            }

            private bool disposed;

            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            protected virtual void Dispose(bool disposing)
            {
                if (disposed)
                    return;

                disposed = true;

                if (disposing)
                {
                    Client?.Close();
                    LocalEndPoint = null;
                    Client = null;
                }
            }
        }

        public void StartServer(string identifier)
        {
            try
            {
                TcpListener.Start();
            }
            catch (Exception ex)
            {
            }

            while (true)
                try
                {
                    var client = TcpListener.AcceptTcpClient();

                    ConnectionManager.CreateConnection(identifier, client, LocalEndPoint, Username, Password);
                }
                catch (Exception ex)
                {
                }
        }

        public abstract class Socks5Data : IDisposable
        {
            public Socks5Data(string identifier)
            {
                this.identifier = identifier;
            }

            public abstract void OnDataReceived(Socks5State Socks5State);
            public abstract Task Start();
            public string identifier { get; private set; }

            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            protected abstract void Dispose(bool disposing);
        }

        public class Socks5TCPData : Socks5Data
        {
            public class StateObject
            {
                public Socket workSocket = null;
                public const int BufferSize = 4096;
                public byte[] buffer = new byte[BufferSize];
            }

            private TcpClient _client;
            private Guid _guid;

            public Socks5TCPData(string identifier, Guid guid, TcpClient client) : base(identifier)
            {
                _guid = guid;
                _client = client;
            }

            public override void OnDataReceived(Socks5State Socks5State)
            {
                try
                {
                    _client.Client.Send(Socks5State.Bytes);
                }
                catch
                {
                }
            }

            public async Task ClientRead()
            {
                var buffer = new byte[4096];
                var ns = _client.GetStream();
                while (true)
                {
                    var bytesRead = await ns.ReadAsync(buffer, 0, buffer.Length);
                    if (bytesRead == 0) return;

                    var b = new byte[bytesRead];
                    Array.Copy(buffer, 0, b, 0, bytesRead);
                    QueueManager.EnqueueElement(identifier, new Socks5State
                    {
                        Guid = _guid,
                        Bytes = b,
                        Socks5Status = Socks5Status.Ok
                    });
                }
            }

            public override async Task Start()
            {
                try
                {
                    await ClientRead();
                }
                catch
                {
                }
            }

            private bool disposed;

            protected override void Dispose(bool disposing)
            {
                if (disposed)
                    return;

                disposed = true;

                if (disposing)
                {
                    _client?.Close();
                    _client = null;
                }
            }
        }

        public class Socks5UDPData : Socks5Data
        {
            private TcpClient _client;
            private Guid _guid;
            private UdpClient _udpClient;
            private IPEndPoint _iPEndPoint;

            public Socks5UDPData(string identifier, Guid guid, TcpClient client, UdpClient udpClient) : base(identifier)
            {
                _guid = guid;
                _client = client;
                _udpClient = udpClient;
            }

            public override void OnDataReceived(Socks5State Socks5State)
            {
                Task.Run(async () =>
                {
                    try
                    {
                        await _udpClient.SendAsync(Socks5State.Bytes, Socks5State.Bytes.Count(), _iPEndPoint);
                    }
                    catch
                    {
                    }
                });
            }

            public async Task ClientRead()
            {
                var buffer = new byte[4096];
                var ns = _client.GetStream();
                while (true)
                {
                    var bytesRead = await ns.ReadAsync(buffer, 0, buffer.Length);
                    if (bytesRead == 0) return;
                }
            }

            private void OnUdpDataReceive(IAsyncResult result)
            {
                try
                {
                    var state = (UdpClient) result.AsyncState;
                    var receivedBytes = state.EndReceive(result, ref _iPEndPoint);

                    if (receivedBytes.Count() > 0)
                        QueueManager.EnqueueElement(identifier, new Socks5State
                        {
                            Guid = _guid,
                            Bytes = receivedBytes,
                            Socks5Status = Socks5Status.Ok
                        });

                    _udpClient.BeginReceive(OnUdpDataReceive, _udpClient);
                }
                catch
                {
                }
            }

            public override async Task Start()
            {
                try
                {
                    _udpClient.BeginReceive(OnUdpDataReceive, _udpClient);
                    await ClientRead();
                }
                catch
                {
                }
            }

            private bool disposed;

            protected override void Dispose(bool disposing)
            {
                if (disposed)
                    return;

                disposed = true;

                if (disposing)
                {
                    _client?.Close();
                    _client = null;
                    _udpClient?.Client?.Close();
                    _udpClient = null;
                }
            }
        }
    }
}