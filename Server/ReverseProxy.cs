using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Socks5Server;

namespace Server
{
    public class ReverseProxy
    {
        public string HWID { get; set; }
        private string Identifier = "socks";

        private string Socks5Port { get; set; }
        private string User { get; set; }
        private string Password { get; set; }
        private string WebHostPort { get; set; }

        public ReverseProxy(string HWID, string Socks5Port, string User, string Password, string WebHostPort)
        {
            this.HWID = HWID;
            this.Socks5Port = Socks5Port;
            this.User = User;
            this.Password = Password;
            this.WebHostPort = WebHostPort;
        }

        public void Start()
        {
            Task.Run(() =>
            {
                Socks5 socks5;
                try
                {
                    socks5 = new Socks5(IPAddress.Any, int.Parse(Socks5Port), User, Password);
                    socks5.StartServer(Identifier);
                }
                catch (Exception ex)
                {
                }
            });
            try
            {
                BuildHost("0.0.0.0", WebHostPort).Run();
            }
            catch (Exception ex)
            {
            }
        }

        private static IHost BuildHost(string IPAddress, string WebHostPort)
        {
            return new HostBuilder()
                .ConfigureWebHost(webconfig =>
                {
                    webconfig.UseKestrel(options => { options.AddServerHeader = false; })
                        .UseStartup<Startup>()
                        .UseUrls($"http://{IPAddress}:{WebHostPort}");
                })
                .Build();
        }
    }
}