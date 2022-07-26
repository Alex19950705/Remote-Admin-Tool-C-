
namespace Creeper
{
    partial class FrmVNC
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmVNC));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.CopyBtn = new System.Windows.Forms.Button();
            this.PasteBtn = new System.Windows.Forms.Button();
            this.ShowStart = new System.Windows.Forms.Button();
            this.StartExplorer = new System.Windows.Forms.Button();
            this.ResizeLabel = new System.Windows.Forms.Label();
            this.QualityLabel = new System.Windows.Forms.Label();
            this.IntervalLabel = new System.Windows.Forms.Label();
            this.VNCBox = new System.Windows.Forms.PictureBox();
            this.IntervalScroll = new System.Windows.Forms.TrackBar();
            this.QualityScroll = new System.Windows.Forms.TrackBar();
            this.ResizeScroll = new System.Windows.Forms.TrackBar();
            this.CloseBtn = new System.Windows.Forms.Button();
            this.RestoreMaxBtn = new System.Windows.Forms.Button();
            this.MinBtn = new System.Windows.Forms.Button();
            this.StartBrowserBtn = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.chkClone = new System.Windows.Forms.CheckBox();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.textBoxEXE = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button6 = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.button8 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.VNCBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IntervalScroll)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.QualityScroll)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ResizeScroll)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // CopyBtn
            // 
            this.CopyBtn.Image = global::Creeper.Properties.Resources.Copy32x32;
            this.CopyBtn.Location = new System.Drawing.Point(6, 78);
            this.CopyBtn.Name = "CopyBtn";
            this.CopyBtn.Size = new System.Drawing.Size(40, 40);
            this.CopyBtn.TabIndex = 0;
            this.CopyBtn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.CopyBtn.UseVisualStyleBackColor = true;
            this.CopyBtn.Click += new System.EventHandler(this.CopyBtn_Click);
            // 
            // PasteBtn
            // 
            this.PasteBtn.Image = global::Creeper.Properties.Resources.Copy32x32;
            this.PasteBtn.Location = new System.Drawing.Point(52, 78);
            this.PasteBtn.Name = "PasteBtn";
            this.PasteBtn.Size = new System.Drawing.Size(40, 40);
            this.PasteBtn.TabIndex = 1;
            this.PasteBtn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.PasteBtn.UseVisualStyleBackColor = true;
            this.PasteBtn.Click += new System.EventHandler(this.PasteBtn_Click);
            // 
            // ShowStart
            // 
            this.ShowStart.Image = global::Creeper.Properties.Resources.Windows32x32;
            this.ShowStart.Location = new System.Drawing.Point(6, 32);
            this.ShowStart.Name = "ShowStart";
            this.ShowStart.Size = new System.Drawing.Size(40, 40);
            this.ShowStart.TabIndex = 2;
            this.ShowStart.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ShowStart.UseVisualStyleBackColor = true;
            this.ShowStart.Click += new System.EventHandler(this.ShowStart_Click);
            // 
            // StartExplorer
            // 
            this.StartExplorer.Image = global::Creeper.Properties.Resources.Explorer32x32.ToBitmap();
            this.StartExplorer.Location = new System.Drawing.Point(52, 32);
            this.StartExplorer.Name = "StartExplorer";
            this.StartExplorer.Size = new System.Drawing.Size(40, 40);
            this.StartExplorer.TabIndex = 3;
            this.StartExplorer.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.StartExplorer.UseVisualStyleBackColor = true;
            this.StartExplorer.Click += new System.EventHandler(this.StartExplorer_Click);
            // 
            // ResizeLabel
            // 
            this.ResizeLabel.AutoSize = true;
            this.ResizeLabel.Location = new System.Drawing.Point(397, 18);
            this.ResizeLabel.Name = "ResizeLabel";
            this.ResizeLabel.Size = new System.Drawing.Size(68, 13);
            this.ResizeLabel.TabIndex = 4;
            this.ResizeLabel.Text = "Resize : 55%";
            // 
            // QualityLabel
            // 
            this.QualityLabel.AutoSize = true;
            this.QualityLabel.Location = new System.Drawing.Point(213, 18);
            this.QualityLabel.Name = "QualityLabel";
            this.QualityLabel.Size = new System.Drawing.Size(68, 13);
            this.QualityLabel.TabIndex = 5;
            this.QualityLabel.Text = "Quality : 50%";
            // 
            // IntervalLabel
            // 
            this.IntervalLabel.AutoSize = true;
            this.IntervalLabel.Location = new System.Drawing.Point(9, 18);
            this.IntervalLabel.Name = "IntervalLabel";
            this.IntervalLabel.Size = new System.Drawing.Size(88, 13);
            this.IntervalLabel.TabIndex = 6;
            this.IntervalLabel.Text = "Interval (ms): 500";
            // 
            // VNCBox
            // 
            this.VNCBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.VNCBox.Location = new System.Drawing.Point(3, 52);
            this.VNCBox.Name = "VNCBox";
            this.VNCBox.Size = new System.Drawing.Size(1004, 524);
            this.VNCBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.VNCBox.TabIndex = 7;
            this.VNCBox.TabStop = false;
            this.VNCBox.Click += new System.EventHandler(this.VNCBox_Click);
            this.VNCBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.VNCBox_MouseDown);
            this.VNCBox.MouseHover += new System.EventHandler(this.VNCBox_MouseHover);
            this.VNCBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.VNCBox_MouseMove);
            this.VNCBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.VNCBox_MouseUp);
            // 
            // IntervalScroll
            // 
            this.IntervalScroll.AutoSize = false;
            this.IntervalScroll.Location = new System.Drawing.Point(103, 11);
            this.IntervalScroll.Maximum = 1000;
            this.IntervalScroll.Minimum = 10;
            this.IntervalScroll.Name = "IntervalScroll";
            this.IntervalScroll.Size = new System.Drawing.Size(104, 26);
            this.IntervalScroll.TabIndex = 8;
            this.IntervalScroll.TickStyle = System.Windows.Forms.TickStyle.None;
            this.IntervalScroll.Value = 500;
            this.IntervalScroll.Scroll += new System.EventHandler(this.IntervalScroll_Scroll);
            // 
            // QualityScroll
            // 
            this.QualityScroll.AutoSize = false;
            this.QualityScroll.Location = new System.Drawing.Point(287, 11);
            this.QualityScroll.Maximum = 100;
            this.QualityScroll.Name = "QualityScroll";
            this.QualityScroll.Size = new System.Drawing.Size(104, 26);
            this.QualityScroll.TabIndex = 9;
            this.QualityScroll.TickStyle = System.Windows.Forms.TickStyle.None;
            this.QualityScroll.Value = 50;
            this.QualityScroll.Scroll += new System.EventHandler(this.QualityScroll_Scroll);
            // 
            // ResizeScroll
            // 
            this.ResizeScroll.AutoSize = false;
            this.ResizeScroll.Location = new System.Drawing.Point(471, 11);
            this.ResizeScroll.Maximum = 100;
            this.ResizeScroll.Minimum = 10;
            this.ResizeScroll.Name = "ResizeScroll";
            this.ResizeScroll.Size = new System.Drawing.Size(104, 26);
            this.ResizeScroll.TabIndex = 10;
            this.ResizeScroll.TickStyle = System.Windows.Forms.TickStyle.None;
            this.ResizeScroll.Value = 55;
            this.ResizeScroll.Scroll += new System.EventHandler(this.ResizeScroll_Scroll);
            // 
            // CloseBtn
            // 
            this.CloseBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CloseBtn.BackgroundImage = global::Creeper.Properties.Resources.close_window;
            this.CloseBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CloseBtn.Location = new System.Drawing.Point(972, 18);
            this.CloseBtn.Name = "CloseBtn";
            this.CloseBtn.Size = new System.Drawing.Size(26, 26);
            this.CloseBtn.TabIndex = 11;
            this.CloseBtn.UseVisualStyleBackColor = true;
            this.CloseBtn.Click += new System.EventHandler(this.CloseBtn_Click);
            // 
            // RestoreMaxBtn
            // 
            this.RestoreMaxBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RestoreMaxBtn.BackgroundImage = global::Creeper.Properties.Resources.maximize_window;
            this.RestoreMaxBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.RestoreMaxBtn.Location = new System.Drawing.Point(947, 18);
            this.RestoreMaxBtn.Name = "RestoreMaxBtn";
            this.RestoreMaxBtn.Size = new System.Drawing.Size(26, 26);
            this.RestoreMaxBtn.TabIndex = 12;
            this.RestoreMaxBtn.UseVisualStyleBackColor = true;
            this.RestoreMaxBtn.Click += new System.EventHandler(this.RestoreMaxBtn_Click);
            // 
            // MinBtn
            // 
            this.MinBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MinBtn.BackgroundImage = global::Creeper.Properties.Resources.minimize_window;
            this.MinBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.MinBtn.Location = new System.Drawing.Point(922, 18);
            this.MinBtn.Name = "MinBtn";
            this.MinBtn.Size = new System.Drawing.Size(26, 26);
            this.MinBtn.TabIndex = 13;
            this.MinBtn.UseVisualStyleBackColor = true;
            this.MinBtn.Click += new System.EventHandler(this.MinBtn_Click);
            // 
            // StartBrowserBtn
            // 
            this.StartBrowserBtn.Image = global::Creeper.Properties.Resources.Chrome32x32.ToBitmap();
            this.StartBrowserBtn.Location = new System.Drawing.Point(6, 19);
            this.StartBrowserBtn.Name = "StartBrowserBtn";
            this.StartBrowserBtn.Size = new System.Drawing.Size(40, 40);
            this.StartBrowserBtn.TabIndex = 14;
            this.StartBrowserBtn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.StartBrowserBtn.UseVisualStyleBackColor = true;
            this.StartBrowserBtn.Click += new System.EventHandler(this.StartBrowserBtn_Click);
            // 
            // button1
            // 
            this.button1.Image = global::Creeper.Properties.Resources.Edge32x32.ToBitmap();
            this.button1.Location = new System.Drawing.Point(6, 65);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(40, 40);
            this.button1.TabIndex = 16;
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Image = global::Creeper.Properties.Resources.Firefox32x32.ToBitmap();
            this.button2.Location = new System.Drawing.Point(52, 65);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(40, 40);
            this.button2.TabIndex = 17;
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Image = global::Creeper.Properties.Resources.Miner;
            this.button3.Location = new System.Drawing.Point(6, 19);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(40, 40);
            this.button3.TabIndex = 18;
            this.button3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ContextMenuStrip = this.contextMenuStrip1;
            this.statusStrip1.Location = new System.Drawing.Point(0, 579);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1133, 22);
            this.statusStrip1.TabIndex = 19;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(165, 26);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Image = global::Creeper.Properties.Resources.server_delete;
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.closeToolStripMenuItem.Text = "Close Connexion";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(44, 17);
            this.toolStripStatusLabel1.Text = "Statut :";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(26, 17);
            this.toolStripStatusLabel2.Text = "Idle";
            // 
            // chkClone
            // 
            this.chkClone.AutoSize = true;
            this.chkClone.Location = new System.Drawing.Point(7, 157);
            this.chkClone.Name = "chkClone";
            this.chkClone.Size = new System.Drawing.Size(85, 17);
            this.chkClone.TabIndex = 20;
            this.chkClone.Text = "Clone Profile";
            this.chkClone.UseVisualStyleBackColor = true;
            // 
            // timer2
            // 
            this.timer2.Enabled = true;
            this.timer2.Interval = 1000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // button4
            // 
            this.button4.Image = global::Creeper.Properties.Resources.brave_browser_logo_icon_153013;
            this.button4.Location = new System.Drawing.Point(52, 19);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(40, 40);
            this.button4.TabIndex = 21;
            this.button4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Enabled = false;
            this.button5.Image = global::Creeper.Properties.Resources.opera_browser_logo_icon_152972;
            this.button5.Location = new System.Drawing.Point(6, 111);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(40, 40);
            this.button5.TabIndex = 22;
            this.button5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // textBoxEXE
            // 
            this.textBoxEXE.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxEXE.ForeColor = System.Drawing.Color.Gray;
            this.textBoxEXE.Location = new System.Drawing.Point(6, 33);
            this.textBoxEXE.Name = "textBoxEXE";
            this.textBoxEXE.Size = new System.Drawing.Size(88, 20);
            this.textBoxEXE.TabIndex = 25;
            this.textBoxEXE.Text = "https://the.earth.li/~sgtatham/putty/latest/w64/putty.exe";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.StartBrowserBtn);
            this.groupBox1.Controls.Add(this.button5);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.button4);
            this.groupBox1.Controls.Add(this.chkClone);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Location = new System.Drawing.Point(7, 195);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(99, 185);
            this.groupBox1.TabIndex = 26;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Browsers";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button3);
            this.groupBox2.Location = new System.Drawing.Point(7, 386);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(99, 66);
            this.groupBox2.TabIndex = 27;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Miner";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.button6);
            this.groupBox3.Controls.Add(this.textBoxEXE);
            this.groupBox3.Location = new System.Drawing.Point(7, 458);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(99, 107);
            this.groupBox3.TabIndex = 28;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Download / Execute";
            // 
            // button6
            // 
            this.button6.Image = global::Creeper.Properties.Resources.icons8_download_from_cloud_32;
            this.button6.Location = new System.Drawing.Point(6, 61);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(40, 40);
            this.button6.TabIndex = 26;
            this.button6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.button8);
            this.groupBox4.Controls.Add(this.button7);
            this.groupBox4.Controls.Add(this.CopyBtn);
            this.groupBox4.Controls.Add(this.ShowStart);
            this.groupBox4.Controls.Add(this.PasteBtn);
            this.groupBox4.Controls.Add(this.StartExplorer);
            this.groupBox4.Location = new System.Drawing.Point(7, 15);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(99, 174);
            this.groupBox4.TabIndex = 29;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Windows Functions";
            // 
            // button8
            // 
            this.button8.Image = global::Creeper.Properties.Resources.sss;
            this.button8.Location = new System.Drawing.Point(52, 124);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(40, 40);
            this.button8.TabIndex = 5;
            this.button8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button7
            // 
            this.button7.Image = global::Creeper.Properties.Resources.Icon5;
            this.button7.Location = new System.Drawing.Point(7, 124);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(40, 40);
            this.button7.TabIndex = 4;
            this.button7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.ContextMenuStrip = this.contextMenuStrip1;
            this.groupBox5.Controls.Add(this.groupBox4);
            this.groupBox5.Controls.Add(this.groupBox1);
            this.groupBox5.Controls.Add(this.groupBox2);
            this.groupBox5.Controls.Add(this.groupBox3);
            this.groupBox5.Location = new System.Drawing.Point(1013, -3);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(117, 571);
            this.groupBox5.TabIndex = 30;
            this.groupBox5.TabStop = false;
            // 
            // groupBox6
            // 
            this.groupBox6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox6.ContextMenuStrip = this.contextMenuStrip1;
            this.groupBox6.Controls.Add(this.IntervalScroll);
            this.groupBox6.Controls.Add(this.ResizeLabel);
            this.groupBox6.Controls.Add(this.QualityLabel);
            this.groupBox6.Controls.Add(this.IntervalLabel);
            this.groupBox6.Controls.Add(this.MinBtn);
            this.groupBox6.Controls.Add(this.QualityScroll);
            this.groupBox6.Controls.Add(this.RestoreMaxBtn);
            this.groupBox6.Controls.Add(this.ResizeScroll);
            this.groupBox6.Controls.Add(this.CloseBtn);
            this.groupBox6.Location = new System.Drawing.Point(3, -3);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(1004, 49);
            this.groupBox6.TabIndex = 31;
            this.groupBox6.TabStop = false;
            // 
            // FrmVNC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1133, 601);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.VNCBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1120, 632);
            this.Name = "FrmVNC";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.VNCForm_FormClosing);
            this.Load += new System.EventHandler(this.VNCForm_Load);
            this.Click += new System.EventHandler(this.VNCForm_Click);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.VNCForm_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.VNCBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IntervalScroll)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.QualityScroll)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ResizeScroll)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button CopyBtn;
        private System.Windows.Forms.Button PasteBtn;
        private System.Windows.Forms.Button ShowStart;
        private System.Windows.Forms.Button StartExplorer;
        private System.Windows.Forms.Label ResizeLabel;
        private System.Windows.Forms.Label QualityLabel;
        private System.Windows.Forms.Label IntervalLabel;
        private System.Windows.Forms.PictureBox VNCBox;
        private System.Windows.Forms.TrackBar IntervalScroll;
        private System.Windows.Forms.TrackBar QualityScroll;
        private System.Windows.Forms.TrackBar ResizeScroll;
        private System.Windows.Forms.Button CloseBtn;
        private System.Windows.Forms.Button RestoreMaxBtn;
        private System.Windows.Forms.Button MinBtn;
        private System.Windows.Forms.Button StartBrowserBtn;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.CheckBox chkClone;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.TextBox textBoxEXE;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
    }
}