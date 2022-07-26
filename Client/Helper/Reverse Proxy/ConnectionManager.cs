using System;
using System.Collections.Generic;
using System.Threading;
using Client.HandlePacket;

namespace Socks5Client
{
    public static class ConnectionManager
    {
        private static ReaderWriterLockSlim cacheLock = new ReaderWriterLockSlim();
        private static Dictionary<Guid, ConnectionFactory> ConnectionKeys = new Dictionary<Guid, ConnectionFactory>();

        public static ConnectionFactory Read(Guid key)
        {
            cacheLock.EnterReadLock();
            try
            {
                return ConnectionKeys[key];
            }
            finally
            {
                cacheLock.ExitReadLock();
            }
        }

        public static void Add(Guid key, ConnectionFactory value)
        {
            cacheLock.EnterWriteLock();
            try
            {
                ConnectionKeys.Add(key, value);
            }
            finally
            {
                cacheLock.ExitWriteLock();
            }
        }

        public static void Delete(Guid key)
        {
            cacheLock.EnterWriteLock();
            try
            {
                ConnectionKeys.Remove(key);
            }
            finally
            {
                cacheLock.ExitWriteLock();
            }
        }

        public static void CreateConnection(Socks5State Socks5State)
        {
            new Thread(() =>
            {
                try
                {
                    var socks5Command = new Socks5Command();
                    socks5Command.Parse(Socks5State.Bytes);
                    Add(Socks5State.Guid, new ConnectionFactory(socks5Command, Socks5State.Guid));
                }
                catch (Exception ex)
                {
                    EndConnection(Socks5State);
                }
            }).Start();
        }

        public static void EndConnection(Socks5State Socks5State)
        {
            try
            {
                var connectionFactory = Read(Socks5State.Guid);
                ;

                if (connectionFactory != null) connectionFactory.Dispose();

                Delete(Socks5State.Guid);

                HandleReverseProxy.Send(new Socks5State
                {
                    Guid = Socks5State.Guid,
                    Socks5Status = Socks5Status.Error
                });
            }
            catch
            {
            }
        }

        public static void UpdateConnection(Socks5State Socks5State)
        {
            if (Socks5State.Socks5Status == Socks5Status.NewConnection)
                try
                {
                    CreateConnection(Socks5State);
                }
                catch
                {
                }
            else if (Socks5State.Socks5Status == Socks5Status.Ok)
                new Thread(() =>
                {
                    try
                    {
                        var connectionFactory = Read(Socks5State.Guid);

                        if (connectionFactory != null) connectionFactory.Socks5Data.OnDataReceived(Socks5State.Bytes);
                    }
                    catch
                    {
                    }
                }).Start();
            else if (Socks5State.Socks5Status == Socks5Status.Error)
                new Thread(() => { EndConnection(Socks5State); }).Start();
        }
    }
}