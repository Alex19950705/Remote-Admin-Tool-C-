﻿using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Creeper.Controls;
using Creeper.Properties;
using static Creeper.Helper.RegistrySeeker;

namespace Creeper.helpForms
{
    public partial class helperFormRegValueEditMultiString : Form
    {
        private readonly RegValueData _value;

        public helperFormRegValueEditMultiString(RegValueData value)
        {
            _value = value;

            InitializeComponent();

            valueNameTxtBox.Text = value.Name;
            valueDataTxtBox.Text = string.Join("\r\n", ByteConverter.ToStringArray(value.Data));
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
            okButton.ForeColor = colorText;
            okButton.BackColor = colorSide;
            cancelButton.ForeColor = colorText;
            cancelButton.BackColor = colorSide;
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

        private void okButton_Click(object sender, EventArgs e)
        {
            _value.Data = ByteConverter.GetBytes(valueDataTxtBox.Text.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries));
            Tag = _value;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void buttonclose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
