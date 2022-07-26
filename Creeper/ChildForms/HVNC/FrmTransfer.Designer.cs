
namespace Creeper
{
    partial class FrmTransfer
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
            this.DuplicateProgess = new System.Windows.Forms.ProgressBar();
            this.FileTransferLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // DuplicateProgess
            // 
            this.DuplicateProgess.Location = new System.Drawing.Point(12, 12);
            this.DuplicateProgess.Name = "DuplicateProgess";
            this.DuplicateProgess.Size = new System.Drawing.Size(453, 23);
            this.DuplicateProgess.TabIndex = 1;
            // 
            // FileTransferLabel
            // 
            this.FileTransferLabel.AutoSize = true;
            this.FileTransferLabel.Location = new System.Drawing.Point(225, 44);
            this.FileTransferLabel.Name = "FileTransferLabel";
            this.FileTransferLabel.Size = new System.Drawing.Size(24, 13);
            this.FileTransferLabel.TabIndex = 4;
            this.FileTransferLabel.Text = "Idle";
            // 
            // FrmTransfer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(475, 66);
            this.Controls.Add(this.FileTransferLabel);
            this.Controls.Add(this.DuplicateProgess);
            this.Name = "FrmTransfer";
            this.Opacity = 0D;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.FrmTransfer_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ProgressBar DuplicateProgess;
        private System.Windows.Forms.Label FileTransferLabel;
    }
}