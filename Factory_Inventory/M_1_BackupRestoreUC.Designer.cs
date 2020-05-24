namespace Factory_Inventory
{
    partial class M_1_BackupRestoreUC
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.backupStatusLabel = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.backupLoactionTB = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.backupButton = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.browseBackupButton = new System.Windows.Forms.Button();
            this.restoreStatusLabel = new System.Windows.Forms.Label();
            this.progressBar2 = new System.Windows.Forms.ProgressBar();
            this.restoreLocationTB = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.restoreButton = new System.Windows.Forms.Button();
            this.browseRestoreButton = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.backupLoactionLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // backupStatusLabel
            // 
            this.backupStatusLabel.AutoSize = true;
            this.backupStatusLabel.Location = new System.Drawing.Point(118, 159);
            this.backupStatusLabel.Name = "backupStatusLabel";
            this.backupStatusLabel.Size = new System.Drawing.Size(52, 17);
            this.backupStatusLabel.TabIndex = 0;
            this.backupStatusLabel.Text = "Status:";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(175, 74);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(326, 23);
            this.progressBar1.TabIndex = 0;
            // 
            // backupLoactionTB
            // 
            this.backupLoactionTB.Location = new System.Drawing.Point(175, 31);
            this.backupLoactionTB.Name = "backupLoactionTB";
            this.backupLoactionTB.Size = new System.Drawing.Size(246, 22);
            this.backupLoactionTB.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(175, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Backup Location";
            // 
            // backupButton
            // 
            this.backupButton.Location = new System.Drawing.Point(296, 103);
            this.backupButton.Name = "backupButton";
            this.backupButton.Size = new System.Drawing.Size(75, 23);
            this.backupButton.TabIndex = 5;
            this.backupButton.Text = "Backup";
            this.backupButton.UseVisualStyleBackColor = true;
            this.backupButton.Click += new System.EventHandler(this.backupButton_Click_1);
            // 
            // browseBackupButton
            // 
            this.browseBackupButton.Location = new System.Drawing.Point(430, 30);
            this.browseBackupButton.Name = "browseBackupButton";
            this.browseBackupButton.Size = new System.Drawing.Size(71, 23);
            this.browseBackupButton.TabIndex = 3;
            this.browseBackupButton.Text = "Browse";
            this.browseBackupButton.UseVisualStyleBackColor = true;
            this.browseBackupButton.Click += new System.EventHandler(this.browseButton_Click);
            // 
            // restoreStatusLabel
            // 
            this.restoreStatusLabel.AutoSize = true;
            this.restoreStatusLabel.Location = new System.Drawing.Point(115, 376);
            this.restoreStatusLabel.Name = "restoreStatusLabel";
            this.restoreStatusLabel.Size = new System.Drawing.Size(52, 17);
            this.restoreStatusLabel.TabIndex = 0;
            this.restoreStatusLabel.Text = "Status:";
            // 
            // progressBar2
            // 
            this.progressBar2.Location = new System.Drawing.Point(172, 291);
            this.progressBar2.Name = "progressBar2";
            this.progressBar2.Size = new System.Drawing.Size(326, 23);
            this.progressBar2.TabIndex = 0;
            // 
            // restoreLocationTB
            // 
            this.restoreLocationTB.Location = new System.Drawing.Point(172, 248);
            this.restoreLocationTB.Name = "restoreLocationTB";
            this.restoreLocationTB.Size = new System.Drawing.Size(246, 22);
            this.restoreLocationTB.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AllowDrop = true;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(172, 228);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 17);
            this.label3.TabIndex = 0;
            this.label3.Text = "Restore From";
            // 
            // restoreButton
            // 
            this.restoreButton.Location = new System.Drawing.Point(293, 320);
            this.restoreButton.Name = "restoreButton";
            this.restoreButton.Size = new System.Drawing.Size(75, 23);
            this.restoreButton.TabIndex = 11;
            this.restoreButton.Text = "Restore";
            this.restoreButton.UseVisualStyleBackColor = true;
            this.restoreButton.Click += new System.EventHandler(this.restoreButton_Click);
            // 
            // browseRestoreButton
            // 
            this.browseRestoreButton.Location = new System.Drawing.Point(430, 247);
            this.browseRestoreButton.Name = "browseRestoreButton";
            this.browseRestoreButton.Size = new System.Drawing.Size(71, 23);
            this.browseRestoreButton.TabIndex = 9;
            this.browseRestoreButton.Text = "Browse";
            this.browseRestoreButton.UseVisualStyleBackColor = true;
            this.browseRestoreButton.Click += new System.EventHandler(this.browseRestoreButton_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // backupLoactionLabel
            // 
            this.backupLoactionLabel.AutoSize = true;
            this.backupLoactionLabel.Location = new System.Drawing.Point(118, 176);
            this.backupLoactionLabel.Name = "backupLoactionLabel";
            this.backupLoactionLabel.Size = new System.Drawing.Size(0, 17);
            this.backupLoactionLabel.TabIndex = 12;
            // 
            // M_1_BackupRestoreUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.backupLoactionLabel);
            this.Controls.Add(this.browseRestoreButton);
            this.Controls.Add(this.restoreStatusLabel);
            this.Controls.Add(this.progressBar2);
            this.Controls.Add(this.restoreLocationTB);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.restoreButton);
            this.Controls.Add(this.browseBackupButton);
            this.Controls.Add(this.backupStatusLabel);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.backupLoactionTB);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.backupButton);
            this.Name = "M_1_BackupRestoreUC";
            this.Size = new System.Drawing.Size(671, 427);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label backupStatusLabel;
        private System.Windows.Forms.ProgressBar progressBar1;
        public System.Windows.Forms.TextBox backupLoactionTB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button backupButton;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button browseBackupButton;
        private System.Windows.Forms.Label restoreStatusLabel;
        private System.Windows.Forms.ProgressBar progressBar2;
        public System.Windows.Forms.TextBox restoreLocationTB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button restoreButton;
        private System.Windows.Forms.Button browseRestoreButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label backupLoactionLabel;
    }
}
