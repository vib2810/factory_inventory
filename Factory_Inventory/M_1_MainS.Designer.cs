using Factory_Inventory.Factory_Classes;

namespace Factory_Inventory
{
    partial class M_1_MainS
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
            this.logOutButton = new System.Windows.Forms.Button();
            this.newUserButton = new System.Windows.Forms.Button();
            this.vouchersButton = new System.Windows.Forms.Button();
            this.inventoryButton = new System.Windows.Forms.Button();
            this.loginLogButton = new System.Windows.Forms.Button();
            this.UsersButton = new System.Windows.Forms.Button();
            this.usernameLabel = new System.Windows.Forms.Label();
            this.backupRestoreButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.m_1_BackupRestoreUC = new Factory_Inventory.M_1_BackupRestoreUC();
            this.loginlogUC = new Factory_Inventory.M_1_loginlogUC();
            this.usersUC = new Factory_Inventory.M_1_usersUC();
            this.inventoryUC = new Factory_Inventory.M_1_inventoryUC();
            this.vouchersUC = new Factory_Inventory.M_1_vouchersUC();
            this.SuspendLayout();
            // 
            // logOutButton
            // 
            this.logOutButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.logOutButton.BackColor = System.Drawing.Color.Red;
            this.logOutButton.Location = new System.Drawing.Point(779, 72);
            this.logOutButton.Name = "logOutButton";
            this.logOutButton.Size = new System.Drawing.Size(111, 38);
            this.logOutButton.TabIndex = 1;
            this.logOutButton.Text = "Log Out";
            this.logOutButton.UseVisualStyleBackColor = false;
            this.logOutButton.Click += new System.EventHandler(this.logOutButton_Click_1);
            // 
            // newUserButton
            // 
            this.newUserButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.newUserButton.BackColor = System.Drawing.Color.DarkGray;
            this.newUserButton.Location = new System.Drawing.Point(779, 433);
            this.newUserButton.Name = "newUserButton";
            this.newUserButton.Size = new System.Drawing.Size(111, 66);
            this.newUserButton.TabIndex = 9;
            this.newUserButton.Text = "Create New User";
            this.newUserButton.UseVisualStyleBackColor = false;
            this.newUserButton.Click += new System.EventHandler(this.newUser_Click);
            // 
            // vouchersButton
            // 
            this.vouchersButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.vouchersButton.BackColor = System.Drawing.Color.DarkGray;
            this.vouchersButton.Location = new System.Drawing.Point(779, 116);
            this.vouchersButton.Name = "vouchersButton";
            this.vouchersButton.Size = new System.Drawing.Size(111, 38);
            this.vouchersButton.TabIndex = 3;
            this.vouchersButton.Text = "&Vouchers";
            this.vouchersButton.UseVisualStyleBackColor = false;
            this.vouchersButton.Click += new System.EventHandler(this.vouchersButton_Click_1);
            // 
            // inventoryButton
            // 
            this.inventoryButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.inventoryButton.BackColor = System.Drawing.Color.DarkGray;
            this.inventoryButton.Location = new System.Drawing.Point(779, 160);
            this.inventoryButton.Name = "inventoryButton";
            this.inventoryButton.Size = new System.Drawing.Size(111, 38);
            this.inventoryButton.TabIndex = 4;
            this.inventoryButton.Text = "&Inventory";
            this.inventoryButton.UseVisualStyleBackColor = false;
            this.inventoryButton.Click += new System.EventHandler(this.inventoryButton_Click_1);
            // 
            // loginLogButton
            // 
            this.loginLogButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.loginLogButton.BackColor = System.Drawing.Color.DarkGray;
            this.loginLogButton.Location = new System.Drawing.Point(779, 256);
            this.loginLogButton.Name = "loginLogButton";
            this.loginLogButton.Size = new System.Drawing.Size(111, 38);
            this.loginLogButton.TabIndex = 6;
            this.loginLogButton.Text = "Login Log";
            this.loginLogButton.UseVisualStyleBackColor = false;
            this.loginLogButton.Click += new System.EventHandler(this.loginLogButton_Click);
            // 
            // UsersButton
            // 
            this.UsersButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.UsersButton.BackColor = System.Drawing.Color.DarkGray;
            this.UsersButton.Location = new System.Drawing.Point(779, 361);
            this.UsersButton.Name = "UsersButton";
            this.UsersButton.Size = new System.Drawing.Size(111, 66);
            this.UsersButton.TabIndex = 8;
            this.UsersButton.Text = "Manage Users";
            this.UsersButton.UseVisualStyleBackColor = false;
            this.UsersButton.Click += new System.EventHandler(this.UsersButton_Click_1);
            // 
            // usernameLabel
            // 
            this.usernameLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.usernameLabel.AutoSize = true;
            this.usernameLabel.Cursor = System.Windows.Forms.Cursors.Default;
            this.usernameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usernameLabel.Location = new System.Drawing.Point(57, 36);
            this.usernameLabel.Name = "usernameLabel";
            this.usernameLabel.Size = new System.Drawing.Size(99, 32);
            this.usernameLabel.TabIndex = 0;
            this.usernameLabel.Text = "label1";
            // 
            // backupRestoreButton
            // 
            this.backupRestoreButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.backupRestoreButton.BackColor = System.Drawing.Color.DarkGray;
            this.backupRestoreButton.Location = new System.Drawing.Point(779, 204);
            this.backupRestoreButton.Name = "backupRestoreButton";
            this.backupRestoreButton.Size = new System.Drawing.Size(111, 46);
            this.backupRestoreButton.TabIndex = 5;
            this.backupRestoreButton.Text = "&Backup and Restore";
            this.backupRestoreButton.UseVisualStyleBackColor = false;
            this.backupRestoreButton.Click += new System.EventHandler(this.backupRestoreButton_Click);
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Gainsboro;
            this.label4.Cursor = System.Windows.Forms.Cursors.Default;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(869, 233);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 17);
            this.label4.TabIndex = 0;
            this.label4.Text = "B";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Gainsboro;
            this.label2.Cursor = System.Windows.Forms.Cursors.Default;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(875, 177);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "I";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Gainsboro;
            this.label1.Cursor = System.Windows.Forms.Cursors.Default;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(869, 133);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "V";
            // 
            // m_1_BackupRestoreUC
            // 
            this.m_1_BackupRestoreUC.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.m_1_BackupRestoreUC.Location = new System.Drawing.Point(63, 72);
            this.m_1_BackupRestoreUC.Name = "m_1_BackupRestoreUC";
            this.m_1_BackupRestoreUC.Size = new System.Drawing.Size(671, 427);
            this.m_1_BackupRestoreUC.TabIndex = 0;
            // 
            // loginlogUC
            // 
            this.loginlogUC.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.loginlogUC.Location = new System.Drawing.Point(69, 71);
            this.loginlogUC.Name = "loginlogUC";
            this.loginlogUC.Size = new System.Drawing.Size(671, 427);
            this.loginlogUC.TabIndex = 0;
            // 
            // usersUC
            // 
            this.usersUC.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.usersUC.Location = new System.Drawing.Point(69, 72);
            this.usersUC.Name = "usersUC";
            this.usersUC.Size = new System.Drawing.Size(671, 427);
            this.usersUC.TabIndex = 0;
            // 
            // inventoryUC
            // 
            this.inventoryUC.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.inventoryUC.Location = new System.Drawing.Point(69, 72);
            this.inventoryUC.Name = "inventoryUC";
            this.inventoryUC.Size = new System.Drawing.Size(671, 427);
            this.inventoryUC.TabIndex = 0;
            // 
            // vouchersUC
            // 
            this.vouchersUC.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.vouchersUC.Location = new System.Drawing.Point(69, 72);
            this.vouchersUC.Name = "vouchersUC";
            this.vouchersUC.Size = new System.Drawing.Size(671, 427);
            this.vouchersUC.TabIndex = 0;
            // 
            // M_1_MainS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(950, 550);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.m_1_BackupRestoreUC);
            this.Controls.Add(this.backupRestoreButton);
            this.Controls.Add(this.loginlogUC);
            this.Controls.Add(this.usersUC);
            this.Controls.Add(this.inventoryUC);
            this.Controls.Add(this.vouchersUC);
            this.Controls.Add(this.usernameLabel);
            this.Controls.Add(this.UsersButton);
            this.Controls.Add(this.loginLogButton);
            this.Controls.Add(this.inventoryButton);
            this.Controls.Add(this.vouchersButton);
            this.Controls.Add(this.newUserButton);
            this.Controls.Add(this.logOutButton);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "M_1_MainS";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Factory Inventory - Home";
            this.Load += new System.EventHandler(this.MainS_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button logOutButton;
        private System.Windows.Forms.Button newUserButton;
        private System.Windows.Forms.Button vouchersButton;
        private System.Windows.Forms.Button inventoryButton;
        private System.Windows.Forms.Button loginLogButton;
        private System.Windows.Forms.Button UsersButton;
        private System.Windows.Forms.Label usernameLabel;
        private M_1_vouchersUC vouchersUC;
        private M_1_inventoryUC inventoryUC;
        private M_1_usersUC usersUC;
        private M_1_loginlogUC loginlogUC;
        private System.Windows.Forms.Button backupRestoreButton;
        private M_1_BackupRestoreUC m_1_BackupRestoreUC;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}