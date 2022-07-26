﻿
using System.ComponentModel;
using System.Windows.Forms;

namespace Creeper.helpForms
{
    partial class helperFormMessage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(helperFormMessage));
            this.paneltop = new System.Windows.Forms.Panel();
            this.labelCreeper = new System.Windows.Forms.Label();
            this.buttonclose = new System.Windows.Forms.Button();
            this.richTextBox = new System.Windows.Forms.RichTextBox();
            this.paneltop.SuspendLayout();
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
            // richTextBox
            // 
            this.richTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.richTextBox, "richTextBox");
            this.richTextBox.Name = "richTextBox";
            // 
            // helperFormMessage
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.richTextBox);
            this.Controls.Add(this.paneltop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "helperFormMessage";
            this.paneltop.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Panel paneltop;
        private Label labelCreeper;
        private Button buttonclose;
        public RichTextBox richTextBox;
    }
}