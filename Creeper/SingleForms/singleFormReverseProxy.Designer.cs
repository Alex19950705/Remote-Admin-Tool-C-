
using System.ComponentModel;
using System.Windows.Forms;

namespace Creeper.singleForms
{
    partial class singleFormReverseProxy
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(singleFormReverseProxy));
            this.paneltop = new System.Windows.Forms.Panel();
            this.labelCreeper = new System.Windows.Forms.Label();
            this.buttonclose = new System.Windows.Forms.Button();
            this.labelWebHostPort = new System.Windows.Forms.Label();
            this.labelUserName = new System.Windows.Forms.Label();
            this.labelPassword = new System.Windows.Forms.Label();
            this.buttonStart = new System.Windows.Forms.Button();
            this.buttonStop = new System.Windows.Forms.Button();
            this.labelSocks5Port = new System.Windows.Forms.Label();
            this.textBoxSocks5Port = new System.Windows.Forms.TextBox();
            this.textBoxWebHostPort = new System.Windows.Forms.TextBox();
            this.textBoxUserName = new System.Windows.Forms.TextBox();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelproxy = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabellink = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelblank = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelcopy = new System.Windows.Forms.ToolStripStatusLabel();
            this.paneltop.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // paneltop
            // 
            this.paneltop.BackColor = System.Drawing.SystemColors.Control;
            this.paneltop.Controls.Add(this.labelCreeper);
            this.paneltop.Controls.Add(this.buttonclose);
            resources.ApplyResources(this.paneltop, "paneltop");
            this.paneltop.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(81)))), ((int)(((byte)(81)))), ((int)(((byte)(81)))));
            this.paneltop.Name = "paneltop";
            this.paneltop.MouseDown += new System.Windows.Forms.MouseEventHandler(this.paneltop_MouseDown);
            // 
            // labelCreeper
            // 
            resources.ApplyResources(this.labelCreeper, "labelCreeper");
            this.labelCreeper.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(81)))), ((int)(((byte)(81)))), ((int)(((byte)(81)))));
            this.labelCreeper.Name = "labelCreeper";
            this.labelCreeper.MouseDown += new System.Windows.Forms.MouseEventHandler(this.labelCreeper_MouseDown);
            // 
            // buttonclose
            // 
            resources.ApplyResources(this.buttonclose, "buttonclose");
            this.buttonclose.BackColor = System.Drawing.SystemColors.Control;
            this.buttonclose.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonShadow;
            this.buttonclose.FlatAppearance.BorderSize = 0;
            this.buttonclose.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ControlLight;
            this.buttonclose.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.buttonclose.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.buttonclose.Image = global::Creeper.Properties.Resources.close_dark;
            this.buttonclose.Name = "buttonclose";
            this.buttonclose.UseVisualStyleBackColor = false;
            this.buttonclose.Click += new System.EventHandler(this.buttonclose_Click);
            // 
            // labelWebHostPort
            // 
            resources.ApplyResources(this.labelWebHostPort, "labelWebHostPort");
            this.labelWebHostPort.Name = "labelWebHostPort";
            // 
            // labelUserName
            // 
            resources.ApplyResources(this.labelUserName, "labelUserName");
            this.labelUserName.Name = "labelUserName";
            // 
            // labelPassword
            // 
            resources.ApplyResources(this.labelPassword, "labelPassword");
            this.labelPassword.Name = "labelPassword";
            // 
            // buttonStart
            // 
            resources.ApplyResources(this.buttonStart, "buttonStart");
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // buttonStop
            // 
            resources.ApplyResources(this.buttonStop, "buttonStop");
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // labelSocks5Port
            // 
            resources.ApplyResources(this.labelSocks5Port, "labelSocks5Port");
            this.labelSocks5Port.Name = "labelSocks5Port";
            // 
            // textBoxSocks5Port
            // 
            this.textBoxSocks5Port.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.textBoxSocks5Port, "textBoxSocks5Port");
            this.textBoxSocks5Port.Name = "textBoxSocks5Port";
            // 
            // textBoxWebHostPort
            // 
            this.textBoxWebHostPort.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.textBoxWebHostPort, "textBoxWebHostPort");
            this.textBoxWebHostPort.Name = "textBoxWebHostPort";
            // 
            // textBoxUserName
            // 
            resources.ApplyResources(this.textBoxUserName, "textBoxUserName");
            this.textBoxUserName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxUserName.Name = "textBoxUserName";
            // 
            // textBoxPassword
            // 
            resources.ApplyResources(this.textBoxPassword, "textBoxPassword");
            this.textBoxPassword.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxPassword.Name = "textBoxPassword";
            // 
            // statusStrip
            // 
            resources.ApplyResources(this.statusStrip, "statusStrip");
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelproxy,
            this.toolStripStatusLabellink,
            this.toolStripStatusLabelblank,
            this.toolStripStatusLabelcopy});
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.SizingGrip = false;
            // 
            // toolStripStatusLabelproxy
            // 
            this.toolStripStatusLabelproxy.Name = "toolStripStatusLabelproxy";
            resources.ApplyResources(this.toolStripStatusLabelproxy, "toolStripStatusLabelproxy");
            // 
            // toolStripStatusLabellink
            // 
            this.toolStripStatusLabellink.Name = "toolStripStatusLabellink";
            resources.ApplyResources(this.toolStripStatusLabellink, "toolStripStatusLabellink");
            // 
            // toolStripStatusLabelblank
            // 
            this.toolStripStatusLabelblank.Name = "toolStripStatusLabelblank";
            resources.ApplyResources(this.toolStripStatusLabelblank, "toolStripStatusLabelblank");
            // 
            // toolStripStatusLabelcopy
            // 
            this.toolStripStatusLabelcopy.Image = global::Creeper.Properties.Resources.copy_dark;
            this.toolStripStatusLabelcopy.Name = "toolStripStatusLabelcopy";
            resources.ApplyResources(this.toolStripStatusLabelcopy, "toolStripStatusLabelcopy");
            this.toolStripStatusLabelcopy.Click += new System.EventHandler(this.toolStripStatusLabelcopy_Click);
            // 
            // singleFormReverseProxy
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.textBoxPassword);
            this.Controls.Add(this.textBoxUserName);
            this.Controls.Add(this.textBoxWebHostPort);
            this.Controls.Add(this.textBoxSocks5Port);
            this.Controls.Add(this.labelSocks5Port);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.labelPassword);
            this.Controls.Add(this.labelUserName);
            this.Controls.Add(this.labelWebHostPort);
            this.Controls.Add(this.paneltop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "singleFormReverseProxy";
            this.paneltop.ResumeLayout(false);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Panel paneltop;
        private Label labelCreeper;
        private Button buttonclose;
        private Label labelWebHostPort;
        private Label labelUserName;
        private Label labelPassword;
        private Button buttonStart;
        private Button buttonStop;
        private Label labelSocks5Port;
        private TextBox textBoxSocks5Port;
        private TextBox textBoxWebHostPort;
        private TextBox textBoxUserName;
        private TextBox textBoxPassword;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel toolStripStatusLabelproxy;
        private ToolStripStatusLabel toolStripStatusLabellink;
        private ToolStripStatusLabel toolStripStatusLabelblank;
        private ToolStripStatusLabel toolStripStatusLabelcopy;
    }
}