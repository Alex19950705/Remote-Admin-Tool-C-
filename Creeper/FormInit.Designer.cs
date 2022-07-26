
using System.ComponentModel;
using System.Windows.Forms;

namespace Creeper
{
    partial class FormInit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormInit));
            this.paneltop = new System.Windows.Forms.Panel();
            this.labelCreeper = new System.Windows.Forms.Label();
            this.buttonclose = new System.Windows.Forms.Button();
            this.labeladdress = new System.Windows.Forms.Label();
            this.labalpassword = new System.Windows.Forms.Label();
            this.textBoxaddress = new System.Windows.Forms.TextBox();
            this.textBoxpassword = new System.Windows.Forms.TextBox();
            this.buttonconnect = new System.Windows.Forms.Button();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.label = new System.Windows.Forms.Label();
            this.paneltop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // paneltop
            // 
            this.paneltop.BackColor = System.Drawing.SystemColors.Control;
            this.paneltop.Controls.Add(this.labelCreeper);
            this.paneltop.Controls.Add(this.buttonclose);
            resources.ApplyResources(this.paneltop, "paneltop");
            this.paneltop.Name = "paneltop";
            this.paneltop.MouseDown += new System.Windows.Forms.MouseEventHandler(this.paneltop_MouseDown);
            // 
            // labelCreeper
            // 
            resources.ApplyResources(this.labelCreeper, "labelCreeper");
            this.labelCreeper.BackColor = System.Drawing.SystemColors.Control;
            this.labelCreeper.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(81)))), ((int)(((byte)(81)))), ((int)(((byte)(81)))));
            this.labelCreeper.Name = "labelCreeper";
            this.labelCreeper.MouseDown += new System.Windows.Forms.MouseEventHandler(this.labelCreeper_MouseDown);
            // 
            // buttonclose
            // 
            resources.ApplyResources(this.buttonclose, "buttonclose");
            this.buttonclose.BackColor = System.Drawing.SystemColors.Control;
            this.buttonclose.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(49)))), ((int)(((byte)(57)))));
            this.buttonclose.FlatAppearance.BorderSize = 0;
            this.buttonclose.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ControlLight;
            this.buttonclose.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.buttonclose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(81)))), ((int)(((byte)(81)))), ((int)(((byte)(81)))));
            this.buttonclose.Image = global::Creeper.Properties.Resources.close_dark;
            this.buttonclose.Name = "buttonclose";
            this.buttonclose.UseVisualStyleBackColor = false;
            this.buttonclose.Click += new System.EventHandler(this.buttonclose_Click);
            // 
            // labeladdress
            // 
            resources.ApplyResources(this.labeladdress, "labeladdress");
            this.labeladdress.Name = "labeladdress";
            // 
            // labalpassword
            // 
            resources.ApplyResources(this.labalpassword, "labalpassword");
            this.labalpassword.Name = "labalpassword";
            // 
            // textBoxaddress
            // 
            resources.ApplyResources(this.textBoxaddress, "textBoxaddress");
            this.textBoxaddress.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxaddress.Name = "textBoxaddress";
            // 
            // textBoxpassword
            // 
            resources.ApplyResources(this.textBoxpassword, "textBoxpassword");
            this.textBoxpassword.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxpassword.Name = "textBoxpassword";
            // 
            // buttonconnect
            // 
            resources.ApplyResources(this.buttonconnect, "buttonconnect");
            this.buttonconnect.Name = "buttonconnect";
            this.buttonconnect.UseVisualStyleBackColor = true;
            this.buttonconnect.Click += new System.EventHandler(this.buttonconnect_Click);
            // 
            // pictureBox
            // 
            this.pictureBox.Image = global::Creeper.Properties.Resources.Creeper_dark;
            resources.ApplyResources(this.pictureBox, "pictureBox");
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.TabStop = false;
            // 
            // label
            // 
            resources.ApplyResources(this.label, "label");
            this.label.Name = "label";
            // 
            // FormInit
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.buttonconnect);
            this.Controls.Add(this.textBoxpassword);
            this.Controls.Add(this.textBoxaddress);
            this.Controls.Add(this.labalpassword);
            this.Controls.Add(this.labeladdress);
            this.Controls.Add(this.paneltop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormInit";
            this.Load += new System.EventHandler(this.FormInit_Load);
            this.paneltop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Panel paneltop;
        private Label labelCreeper;
        private Button buttonclose;
        private Label labeladdress;
        private Label labalpassword;
        private TextBox textBoxaddress;
        private TextBox textBoxpassword;
        private Button buttonconnect;
        private PictureBox pictureBox;
        private Label label;
    }
}