using System;
using System.Net;
using System.Net.Sockets;
using Client.HandlePacket;

namespace Socks5Client
{
    public class ConnectionFactory : IDisposable
    {
        public Socks5Command Socks5Command { get; private set; }
        public Guid Guid { get; private set; }
        public Socks5Data Socks5Data { get; set; }

        public ConnectionFactory(Socks5Command socks5Command, Guid guid)
        {
            Socks5Command = socks5Command;
            Guid = guid;
            CreateConnection();
        }

        public void CreateConnection()
        {
            if (Socks5Command.SocksCommand == Socks5Command.Constants.Command.Connect)
            {
                var socket = new Socket(Socks5Command.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(new IPEndPoint(Socks5Command.DestinationAddress, Socks5Command.DestinationPort));
                Socks5Data = new Socks5TCPData(Guid, socket);
            }

            if (Socks5Command.SocksCommand == Socks5Command.Constants.Command.UdpAssociate)
            {
                var udpClient = new UdpClient(new IPEndPoint(0, 0));
                Socks5Data = new Socks5UDPData(Guid, udpClient);
            }

            if (Socks5Data != null)
            {
                Socks5Data.Start();

                HandleReverseProxy.Send(new Socks5State
                {
                    Guid = Guid,
                    Socks5Status = Socks5Status.NewConnection,
                    ProtocolType = Socks5Command.ProtocolType
                });
            }
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
                Socks5Command = null;
                Socks5Data?.Dispose();
                Socks5Data = null;
            }
        }
    }
}