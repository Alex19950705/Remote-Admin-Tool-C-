using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Socks5Client
{
    public abstract class Socks5Data : IDisposable
    {
        public abstract void OnDataReceived(byte[] bytes);
        public abstract void Start();

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected abstract void Dispose(bool disposing);
    }

    public class Socks5Command
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
        }

        public byte SocksVersion { get; private set; }
        public Constants.Command SocksCommand { get; private set; }
        public Constants.AddressType AddressType { get; private set; }
        public AddressFamily AddressFamily { get; private set; } = AddressFamily.InterNetwork;
        public string Domain { get; private set; }
        public IPAddress DestinationAddress { get; private set; }
        public ushort DestinationPort { get; private set; }
        public bool DnsSuccess { get; private set; }
        public ProtocolType ProtocolType { get; private set; }

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

                    ProtocolType = SocksCommand == Constants.Command.UdpAssociate ? ProtocolType.Udp : ProtocolType.Tcp;

                    if (reader.ReadByte() != 0)
                        return false;

                    AddressType = (Constants.AddressType) reader.ReadByte();
                    if (!Enum.IsDefined(typeof(Constants.AddressType), AddressType))
                        return false;

                    if (AddressType == Constants.AddressType.IPv4)
                    {
                        if (reader.BaseStream.Length - reader.BaseStream.Position != 6)
                            return false;
                        DestinationAddress = new IPAddress((uint) reader.ReadInt32());
                    }
                    else if (AddressType == Constants.AddressType.IPv6)
                    {
                        if (reader.BaseStream.Length - reader.BaseStream.Position != 18)
                            return false;
                        DestinationAddress = new IPAddress(reader.ReadBytes(16));
                        AddressFamily = AddressFamily.InterNetworkV6;
                    }
                    else if (AddressType == Constants.AddressType.Domain)
                    {
                        var domainLength = reader.ReadByte();
                        if (reader.BaseStream.Length - reader.BaseStream.Position != domainLength + 2)
                            return false;
                        Domain = Encoding.UTF8.GetString(reader.ReadBytes(domainLength));

                        try
                        {
                            var dnsResults = Dns.GetHostEntry(Domain);
                            DestinationAddress = dnsResults.AddressList.FirstOrDefault(x => x.AddressFamily == AddressFamily.InterNetwork);
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
}