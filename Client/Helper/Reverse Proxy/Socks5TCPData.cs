using System;
using System.Linq;
using System.Net.Sockets;
using Client.HandlePacket;

namespace Socks5Client
{
    public class Socks5TCPData : Socks5Data
    {
        public class StateObject
        {
            public Socket workSocket;
            public const int BufferSize = 4096;
            public byte[] buffer = new byte[BufferSize];
        }

        private Socket _socket;
        private Guid _guid;

        public Socks5TCPData(Guid guid, Socket socket)
        {
            _socket = socket;
            _guid = guid;
        }

        private void DataReceivedSocket(IAsyncResult ar)
        {
            var state = (StateObject) ar.AsyncState;
            try
            {
                var bytesRead = state.workSocket.EndReceive(ar);

                if (bytesRead > 0)
                {
                    var b = new byte[bytesRead];
                    Array.Copy(state.buffer, 0, b, 0, bytesRead);

                    HandleReverseProxy.Send(new Socks5State
                    {
                        Guid = _guid,
                        Bytes = b,
                        Socks5Status = Socks5Status.Ok
                    });
                }
                else
                {
                    state.workSocket.Close();
                }

                state.workSocket.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                    DataReceivedSocket, state);
            }
            catch
            {
            }
        }

        public override void OnDataReceived(byte[] bytes)
        {
            try
            {
                _socket.BeginSend(bytes, 0, bytes.Count(), SocketFlags.None, DataSent, _socket);
            }
            catch
            {
            }
        }

        private void DataSent(IAsyncResult res)
        {
            try
            {
                var sent = ((Socket) res.AsyncState).EndSend(res);
                if (sent < 0)
                {
                    _socket.Shutdown(SocketShutdown.Both);
                    _socket.Close();
                }
            }
            catch
            {
            }
        }

        public override void Start()
        {
            try
            {
                var state = new StateObject
                {
                    workSocket = _socket
                };
                _socket.BeginReceive(state.buffer, 0, state.buffer.Length, SocketFlags.None,
                    DataReceivedSocket, state);
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
                _socket?.Close();
                _socket = null;
            }
        }
    }
}