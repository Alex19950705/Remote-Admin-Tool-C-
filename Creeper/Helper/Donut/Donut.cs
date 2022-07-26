using System;
using System.Text;
using System.Runtime.InteropServices;
using Donut.Structs;

namespace Donut
{
    public class Donut
    {
        public static void Generate(string client,string payload)
        {
            // Init Config Struct
            DSConfig config = new Helper().InitStruct("DSConfig");

            Helper.Copy(config.outfile, payload);
            Helper.Copy(config.file, client);

            // Start Generation with Config
            int ret = Generator.Donut_Create(ref config);

            // Write Result
            Console.WriteLine($"\nReturn Value:\n\t{Helper.GetError(ret)}\n");
            if (ret != Constants.DONUT_ERROR_SUCCESS)
            {
                Marshal.FreeHGlobal(config.pic);
                Environment.Exit(0);
            }

            // Free PIC shellcode
            Marshal.FreeHGlobal(config.pic);
        }
    }
}
