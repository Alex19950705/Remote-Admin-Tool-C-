using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Runtime.InteropServices;

using Donut.Structs;

namespace Donut
{
    public class Helper
    {
        [StructLayout(LayoutKind.Explicit)]
        public unsafe struct M
        {
            [FieldOffset(0)] public fixed byte b[16];
            [FieldOffset(0)] public fixed UInt32 w[2];
        }

        [StructLayout(LayoutKind.Explicit)]
        public unsafe struct X
        {
            [FieldOffset(0)] public fixed UInt32 w[2];
            [FieldOffset(0)] public fixed UInt64 q[2];
        }

        
        public static int ParseConfig(ref DSConfig config, ref DSFileInfo fi)
        {
            string file = new string(config.file).Replace("\0", "");
            var ret = ParseInputFile(file, ref fi);
            if (ret != Constants.DONUT_ERROR_SUCCESS)
            {
                return ret;
            }
            config.mod_type = fi.type;
            return Constants.DONUT_ERROR_SUCCESS;
        }
        public static int ParseInputFile(string file, ref DSFileInfo fi)
        {
            PeNet.PeFile PE;

            var extension = Path.GetExtension(file);

            fi.type = Constants.DONUT_MODULE_EXE;
            fi.arch = Constants.DONUT_ARCH_ANY;
            PE = new PeNet.PeFile(file);
            if (PE.Is32Bit == true)
            {
                fi.arch = Constants.DONUT_ARCH_X86;
            }
            else
            {
                fi.arch = Constants.DONUT_ARCH_X64;
            }

            fi.type = Constants.DONUT_MODULE_NET_EXE;

            Copy(fi.ver, PE.MetaDataHdr.Version);
            return Constants.DONUT_ERROR_SUCCESS;
        }
        public static void APIImports(ref DSInstance inst)
        {
            UInt64 dll_hash, final;
            inst.api.hash = new UInt64[64];
            Dictionary<string, List<string>> apiimports = new Dictionary<string, List<string>>
            {
                {
                    Constants.KERNEL32_DLL,
                    new List<string> {
                "LoadLibraryA", "GetProcAddress", "GetModuleHandleA", "VirtualAlloc", "VirtualFree",
                "VirtualQuery", "VirtualProtect", "Sleep", "MultiByteToWideChar", "GetUserDefaultLCID"}
                },
                {
                    Constants.OLEAUT32_DLL,
                    new List<string> {
                "SafeArrayCreate", "SafeArrayCreateVector", "SafeArrayPutElement", "SafeArrayDestroy", "SafeArrayGetLBound",
                "SafeArrayGetUBound", "SysAllocString", "SysFreeString", "LoadTypeLib"}
                },
                {
                    Constants.WININET_DLL,
                    new List<string> {
                "InternetCrackUrlA", "InternetOpenA", "InternetConnectA", "InternetSetOptionA", "InternetReadFile",
                "InternetCloseHandle", "HttpOpenRequestA", "HttpSendRequestA", "HttpQueryInfoA"}
                },
                {
                    Constants.MSCOREE_DLL,
                    new List<string> {
                "CorBindToRuntime", "CLRCreateInstance"}
                },
                {
                    Constants.OLE32_DLL,
                    new List<string> {
                "CoInitializeEx", "CoCreateInstance", "CoUninitialize"}
                }
            };

            // Generate hashes
            for (var l = 0; l < apiimports.Count; l++)
            {
                for (var i = 0; i < apiimports.ElementAt(l).Value.Count; i++)
                {
                    dll_hash = Maru(apiimports.ElementAt(l).Key, ref inst);
                    final = Maru(apiimports.ElementAt(l).Value[i], ref inst) ^ dll_hash;
                    inst.api.hash[inst.api_cnt++] = final;
                }
            }

            // Initialize substruct
            inst.d = new DLL[Constants.DONUT_MAX_DLL];
            for (int i = 0; i < inst.d.Length; i++)
            {
                inst.d[i] = new DLL
                {
                    dll_name = new char[32]
                };
            }

            // Assign hashes
            string[] dlls = new string[4] { "ole32.dll", "oleaut32.dll", "wininet.dll", "mscoree.dll" };
            for (int i = 0; i < dlls.Length; i++)
            {
                char[] dllchar = new char[32];
                Copy(dllchar, dlls[i]);
                for (int l = 0; l < dllchar.Length; l++)
                {
                    inst.d[i].dll_name[l] = dllchar[l];
                }
                inst.dll_cnt = dlls.Length;
            }
        }
        // Correlate error value to string
        public static string GetError(int ret)
        {
            string returnval = "";
            switch (ret)
            {
                case Constants.DONUT_ERROR_SUCCESS:
                    returnval = "[*] Success";
                    break;
                case Constants.DONUT_ERROR_FILE_NOT_FOUND:
                    returnval = "[-] File not found";
                    break;
                case Constants.DONUT_ERROR_FILE_EMPTY:
                    returnval = "[-] File is empty";
                    break;
                case Constants.DONUT_ERROR_FILE_ACCESS:
                    returnval = "[-] Cannot open file";
                    break;
                case Constants.DONUT_ERROR_FILE_INVALID:
                    returnval = "[-] File is invalid";
                    break;
                case Constants.DONUT_ERROR_NET_PARAMS:
                    returnval = "[-] File is a .NET DLL. Donut requires a class and method";
                    break;
                case Constants.DONUT_ERROR_NO_MEMORY:
                    returnval = "[-] No memory available";
                    break;
                case Constants.DONUT_ERROR_INVALID_ARCH:
                    returnval = "[-] Invalid architecture specified";
                    break;
                case Constants.DONUT_ERROR_INVALID_URL:
                    returnval = "[-] Invalid URL";
                    break;
                case Constants.DONUT_ERROR_URL_LENGTH:
                    returnval = "[-] Invalid URL length";
                    break;
                case Constants.DONUT_ERROR_INVALID_PARAMETER:
                    returnval = "[-] Invalid parameter";
                    break;
                case Constants.DONUT_ERROR_RANDOM:
                    returnval = "[-] Error generating random values";
                    break;
                case Constants.DONUT_ERROR_DLL_FUNCTION:
                    returnval = "[-] Unable to locate DLL function provided. Names are case Constants.sensitive";
                    break;
                case Constants.DONUT_ERROR_ARCH_MISMATCH:
                    returnval = "[-] Target architecture cannot support selected DLL/EXE file";
                    break;
                case Constants.DONUT_ERROR_DLL_PARAM:
                    returnval = "[-] You've supplied parameters for an unmanaged DLL. Donut also requires a DLL function";
                    break;
                case Constants.DONUT_ERROR_BYPASS_INVALID:
                    returnval = "[-] Invalid bypass option specified";
                    break;
                case Constants.DONUT_ERROR_NORELOC:
                    returnval = "[-] This file has no relocation information required for in-memory execution.";
                    break;
            }
            return returnval;
        }
        public dynamic InitStruct(string type)
        {
            if (type == "DSConfig")
            {
                var config = new DSConfig
                {
                    arch = Constants.DONUT_ARCH_X84,
                    bypass = Constants.DONUT_BYPASS_CONTINUE,
                    inst_type = Constants.DONUT_INSTANCE_PIC,
                    mod_len = 0,
                    inst_len = 0,
                    pic = IntPtr.Zero,
                    pic_len = 0,
                    cls = new char[Constants.DONUT_MAX_NAME],
                    domain = new char[Constants.DONUT_MAX_NAME],
                    method = new char[Constants.DONUT_MAX_NAME],
                    modname = new char[Constants.DONUT_MAX_NAME],
                    file = new char[Constants.DONUT_MAX_NAME],
                    runtime = new char[Constants.DONUT_MAX_NAME],
                    url = new char[Constants.DONUT_MAX_NAME],
                    param = new char[(Constants.DONUT_MAX_PARAM + 1) * Constants.DONUT_MAX_NAME],
                    outfile = new char[Constants.DONUT_MAX_NAME]
                };
                Copy(config.outfile, "payload.bin");

                return config;
            }
            else if (type == "DSModule")
            {
                var mod = new DSModule
                {
                    runtime = new byte[512],
                    cls = new byte[512],
                    method = new byte[512],
                    domain = new byte[512],
                    sig = new char[256]
                };
                mod.p = new P[Constants.DONUT_MAX_PARAM + 1];
                for (int i = 0; i < mod.p.Length; i++)
                {
                    mod.p[i] = new P
                    {
                        param = new byte[Constants.DONUT_MAX_NAME * 2]
                    };
                }

                return mod;
            }
            else if (type == "DSInstance")
            {
                var inst = new DSInstance
                {
                    sig = new char[256],
                    amsiInit = new char[16],
                    amsiScanBuf = new char[16],
                    amsiScanStr = new char[16],
                    clr = new char[8],
                    wldp = new char[16],
                    wldpQuery = new char[32],
                    wldpIsApproved = new char[32],
                    wscript = new char[16],
                    wscript_exe = new char[32],
                };
                inst.amsi = new AMSI();
                inst.amsi.s = new char[8];
                inst.key.ctr = new byte[16];
                inst.key.mk = new byte[16];
                inst.mod_key.ctr = new byte[16];
                inst.mod_key.mk = new byte[16];

                return inst;
            }
            return 0;
        }
        public static unsafe void WriteOutput(ref DSConfig config)
        {
            try
            {
                // Raw bytes to file
                FileStream f = new FileStream(Helper.String(config.outfile), FileMode.Create, FileAccess.Write);
                UnmanagedMemoryStream fs = new UnmanagedMemoryStream((byte*)config.pic, Convert.ToInt32(config.pic_cnt));
                fs.CopyTo(f);
                fs.Close();
                f.Close();
            }
            catch
            {
                Console.WriteLine("Failed to write payload to file");
            }
        }
        public unsafe static UInt64 Maru(string input, ref DSInstance inst)
        {
            byte[] zeros = new byte[Constants.MARU_BLK_LEN];
            for (var i = 0; i < zeros.Length; i++) { zeros[i] = 0x00; }
            var ptr = Marshal.UnsafeAddrOfPinnedArrayElement(zeros, 0);

            byte[] api = Encoding.UTF8.GetBytes(input + '\0');
            UInt64 h = inst.iv;
            int len = 0;
            int ind = 0;
            bool finished = false;
            M m = new M();

            while (finished == false)
            {
                if (api[len] == 0 || len == Constants.MARU_MAX_STR)
                {
                    Buffer.MemoryCopy(ptr.ToPointer(), m.b + ind, Marshal.SizeOf(typeof(M)), Constants.MARU_BLK_LEN - ind);
                    m.b[ind] = 0x80;
                    if (ind >= Constants.MARU_BLK_LEN - 4)
                    {
                        h ^= Maru_Crypt(m, h);
                        Buffer.MemoryCopy(ptr.ToPointer(), m.b, Marshal.SizeOf(typeof(M)), Constants.MARU_BLK_LEN);
                    }
                    m.w[(Constants.MARU_BLK_LEN / 4) - 1] = Convert.ToUInt32((len * 8));
                    ind = Constants.MARU_BLK_LEN;
                    finished = true;
                }
                else
                {
                    m.b[ind] = api[len];
                    ind++; len++;
                }
                if (ind == Constants.MARU_BLK_LEN)
                {
                    h ^= Maru_Crypt(m, h);
                    ind = 0;
                }
            }
            return h;
        }
        public unsafe static UInt64 Maru_Crypt(M m, UInt64 p)
        {
            UInt32[] k = new UInt32[4];
            UInt32 t, i;
            UInt32[] c = new UInt32[4];
            byte[] f = new byte[16];

            for (int z = 0; z < 16; z++)
            {
                f[z] = m.b[z];
            }
            Buffer.BlockCopy(f, 0, c, 0, 16);

            X x = new X();

            x.q[0] = p;
            for (i = 0; i < 4; i++)
            {
                k[i] = c[i];
            }

            for (i = 0; i < 27; i++)
            {
                x.w[0] = ((((x.w[0]) >> (8)) | ((x.w[0]) << (32 - (8)))) + x.w[1]) ^ k[0];
                x.w[1] = (((x.w[1]) >> (29)) | ((x.w[1]) << (32 - (29)))) ^ x.w[0];
                t = k[3];

                k[3] = ((((k[1]) >> (8)) | ((k[1]) << (32 - (8)))) + k[0]) ^ i;
                k[0] = (((k[0]) >> (29)) | ((k[0]) << (32 - (29)))) ^ k[3];

                k[1] = k[2];
                k[2] = t;
            }
            return x.q[0];
        }
        public unsafe static void Encrypt(byte[] mk, byte[] ctr, IntPtr data, UInt64 size)
        {
            int len = Convert.ToInt32(size);
            byte[] outbuff = new byte[16];
            byte[] x = new byte[16];
            byte[] p = new byte[size];
            byte[] c = ctr;
            int i, r;

            for (int f = 0; f < len; f++)
            {
                p[f] = Marshal.ReadByte(data, f);
            }
            int counter = 0;
            while (len > 0)
            {
                for (i = 0; i < Constants.CIPHER_BLK_LEN; i++)
                {
                    x[i] = c[i];
                }

                outbuff = Chaskey(mk, x);

                r = len > Constants.CIPHER_BLK_LEN ? Constants.CIPHER_BLK_LEN : len;

                for (i = 0; i < r; i++)
                {
                    p[i + counter] ^= outbuff[i];
                }
                len -= r;
                counter += 16;

                // This is ugly, fix
                // Just need to increment c[12] as int everytime
                for (i = Constants.CIPHER_BLK_LEN; i > 0;)
                {
                    if (c[15] == 255)
                    {
                        if (c[14] == 255)
                        {
                            c[13] = Convert.ToByte(c[13] + 1);
                            c[14] = Convert.ToByte(0);
                            c[15] = Convert.ToByte(0);
                            break;
                        }
                        else
                        {
                            c[14] = Convert.ToByte(c[14] + 1);
                            c[15] = Convert.ToByte(0);
                            break;
                        }
                    }
                    else
                    {
                        c[15] = Convert.ToByte(c[15] + 1);
                        break;
                    }
                }
            }
            for (int f = 0; f < Convert.ToInt32(size); f++)
            {
                Marshal.WriteByte(data + f, 0, p[f]);
            }
        }
        public static byte[] Chaskey(byte[] mk, byte[] datain)
        {
            uint[] key = new uint[4];
            uint[] data = new uint[4];
            Buffer.BlockCopy(mk, 0, key, 0, 16);
            Buffer.BlockCopy(datain, 0, data, 0, 16);

            for (int i = 0; i < 4; i++)
            {
                data[i] ^= key[i];
            }

            for (int i = 0; i < 16; i++)
            {
                data[0] += data[1];
                data[1] = ((((data[1]) >> 27)) | ((data[1]) << (32 - (27)))) ^ data[0];
                data[2] += data[3];
                data[3] = ((((data[3]) >> 24)) | ((data[3]) << (32 - (24)))) ^ data[2];
                data[2] += data[1];
                data[0] = ((((data[0]) >> 16)) | ((data[0]) << (32 - (16)))) + data[3];
                data[3] = ((((data[3]) >> 19)) | ((data[3]) << (32 - (19)))) ^ data[0];
                data[1] = ((((data[1]) >> 25)) | ((data[1]) << (32 - (25)))) ^ data[2];
                data[2] = ((((data[2]) >> 16)) | ((data[2]) << (32 - (16))));
            }
            for (int i = 0; i < 4; i++)
            {
                data[i] ^= key[i];
            }

            return data.SelectMany(BitConverter.GetBytes).ToArray();
        }
        public static void PUT_BYTE(byte insertbyte, ref DSConfig config)
        {
            IntPtr ptr = config.pic + config.pic_cnt;
            byte[] src = { 0x00, insertbyte };
            Marshal.WriteByte(ptr, src[1]);
            config.pic_cnt++;
        }
        public static void PUT_WORD(byte[] sarr, ref DSConfig config)
        {
            IntPtr ptr = config.pic + config.pic_cnt;
            for (int i = 0; i < 4; i++)
            {
                Marshal.WriteByte(ptr + i, sarr[i]);
                config.pic_cnt++;
            }
        }
        public static void PUT_BYTES(byte[] sarr, int cnt, ref DSConfig config)
        {
            IntPtr ptr = config.pic + config.pic_cnt;
            for (int i = 0; i < cnt; i++)
            {
                Marshal.WriteByte(ptr + i, sarr[i]);
                config.pic_cnt++;
            }
        }
        public static void PUT_INST(IntPtr instptr, int cnt, ref DSConfig config)
        {
            IntPtr ptr = config.pic + config.pic_cnt;
            for (int i = 0; i < cnt; i++)
            {
                Marshal.WriteByte(ptr + i, Marshal.ReadByte(instptr + i));
                config.pic_cnt++;
            }
            Marshal.FreeHGlobal(instptr);
        }
        public static string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "HMN34P67R9TWCXYF";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public static byte[] RandomBytes(int length)
        {
            byte[] rand = new byte[length];
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(rand);
            return rand;
        }
        public static void Unicode(byte[] dest, string buff)
        {
            byte[] str = Encoding.Convert(Encoding.ASCII, Encoding.Unicode, Encoding.ASCII.GetBytes(buff));
            Array.Copy(str, dest, str.Length);
        }

        public static string String(char[] source)
        {
            return new string(source).Replace("\0", "");
        }
        public static void Copy(char[] dest, string source)
        {
            Array.Copy(source.ToCharArray(), 0, dest, 0, source.ToCharArray().Length);
        }
        public static void Copy(byte[] dest, string source)
        {
            byte[] src = Encoding.ASCII.GetBytes(source);
            Array.Copy(src, 0, dest, 0, src.Length);
        }
    }
}
