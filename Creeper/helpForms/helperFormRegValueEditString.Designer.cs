﻿
using System.ComponentModel;
using System.Windows.Forms;

namespace Creeper.helpForms
{
    partial class helperFormRegValueEditString
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(helperFormRegValueEditString));
            this.paneltop = new System.Windows.Forms.Panel();
            this.labelCreeper = new System.Windows.Forms.Label();
            this.buttonclose = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.valueNameTxtBox = new System.Windows.Forms.TextBox();
            this.valueDataTxtBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.paneltop.SuspendLayout();
            this.SuspendLayout();
            // 
            // paneltop
            // 
            resources.ApplyResources(this.paneltop, "paneltop");
            this.paneltop.BackColor = System.Drawing.SystemColors.Control;
            this.paneltop.Controls.Add(this.labelCreeper);
            this.paneltop.Controls.Add(this.buttonclose);
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
            // cancelButton
            // 
            resources.ApplyResources(this.cancelButton, "cancelButton");
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // okButton
            // 
            resources.ApplyResources(this.okButton, "okButton");
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Name = "okButton";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // valueNameTxtBox
            // 
            resources.ApplyResources(this.valueNameTxtBox, "valueNameTxtBox");
            this.valueNameTxtBox.Name = "valueNameTxtBox";
            this.valueNameTxtBox.ReadOnly = true;
            // 
            // valueDataTxtBox
            // 
            resources.ApplyResources(this.valueDataTxtBox, "valueDataTxtBox");
            this.valueDataTxtBox.Name = "valueDataTxtBox";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // helperFormRegValueEditString
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.valueNameTxtBox);
            this.Controls.Add(this.valueDataTxtBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.paneltop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "helperFormRegValueEditString";
            this.ShowInTaskbar = false;
            this.paneltop.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Panel paneltop;
        private Label labelCreeper;
        private Button buttonclose;
        private Button cancelButton;
        private Button okButton;
        private Label label2;
        private TextBox valueNameTxtBox;
        private TextBox valueDataTxtBox;
        private Label label1;
    }
}