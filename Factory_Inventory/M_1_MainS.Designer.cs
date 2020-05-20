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
            this.newUser = new System.Windows.Forms.Button();
            this.vouchersButton = new System.Windows.Forms.Button();
            this.inventoryButton = new System.Windows.Forms.Button();
            this.loginLogButton = new System.Windows.Forms.Button();
            this.UsersButton = new System.Windows.Forms.Button();
            this.usernameLabel = new System.Windows.Forms.Label();
            this.backupRestoreButton = new System.Windows.Forms.Button();
            this.loginlogUC1 = new Factory_Inventory.M_1_loginlogUC();
            this.usersUC1 = new Factory_Inventory.M_1_usersUC();
            this.inventoryUC1 = new Factory_Inventory.M_1_inventoryUC();
            this.vouchersUC1 = new Factory_Inventory.M_1_vouchersUC();
            this.m_1_BackupRestoreUC1 = new Factory_Inventory.M_1_BackupRestoreUC();
            this.SuspendLayout();
            // 
            // logOutButton
            // 
            this.logOutButton.BackColor = System.Drawing.Color.Red;
            this.logOutButton.Location = new System.Drawing.Point(704, 45);
            this.logOutButton.Name = "logOutButton";
            this.logOutButton.Size = new System.Drawing.Size(111, 38);
            this.logOutButton.TabIndex = 1;
            this.logOutButton.Text = "Log Out";
            this.logOutButton.UseVisualStyleBackColor = false;
            this.logOutButton.Click += new System.EventHandler(this.logOutButton_Click_1);
            // 
            // newUser
            // 
            this.newUser.Location = new System.Drawing.Point(704, 406);
            this.newUser.Name = "newUser";
            this.newUser.Size = new System.Drawing.Size(111, 66);
            this.newUser.TabIndex = 8;
            this.newUser.Text = "Create New User";
            this.newUser.UseVisualStyleBackColor = true;
            this.newUser.Click += new System.EventHandler(this.newUser_Click);
            // 
            // vouchersButton
            // 
            this.vouchersButton.Location = new System.Drawing.Point(704, 89);
            this.vouchersButton.Name = "vouchersButton";
            this.vouchersButton.Size = new System.Drawing.Size(111, 38);
            this.vouchersButton.TabIndex = 3;
            this.vouchersButton.Text = "Vouchers";
            this.vouchersButton.UseVisualStyleBackColor = true;
            this.vouchersButton.Click += new System.EventHandler(this.vouchersButton_Click_1);
            // 
            // inventoryButton
            // 
            this.inventoryButton.Location = new System.Drawing.Point(704, 133);
            this.inventoryButton.Name = "inventoryButton";
            this.inventoryButton.Size = new System.Drawing.Size(111, 38);
            this.inventoryButton.TabIndex = 4;
            this.inventoryButton.Text = "Inventory";
            this.inventoryButton.UseVisualStyleBackColor = true;
            this.inventoryButton.Click += new System.EventHandler(this.inventoryButton_Click_1);
            // 
            // loginLogButton
            // 
            this.loginLogButton.Location = new System.Drawing.Point(704, 229);
            this.loginLogButton.Name = "loginLogButton";
            this.loginLogButton.Size = new System.Drawing.Size(111, 38);
            this.loginLogButton.TabIndex = 5;
            this.loginLogButton.Text = "Login Log";
            this.loginLogButton.UseVisualStyleBackColor = true;
            this.loginLogButton.Click += new System.EventHandler(this.loginLogButton_Click);
            // 
            // UsersButton
            // 
            this.UsersButton.Location = new System.Drawing.Point(704, 334);
            this.UsersButton.Name = "UsersButton";
            this.UsersButton.Size = new System.Drawing.Size(111, 66);
            this.UsersButton.TabIndex = 7;
            this.UsersButton.Text = "Manage Users";
            this.UsersButton.UseVisualStyleBackColor = true;
            this.UsersButton.Click += new System.EventHandler(this.UsersButton_Click_1);
            // 
            // usernameLabel
            // 
            this.usernameLabel.AutoSize = true;
            this.usernameLabel.Cursor = System.Windows.Forms.Cursors.Default;
            this.usernameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usernameLabel.Location = new System.Drawing.Point(12, 10);
            this.usernameLabel.Name = "usernameLabel";
            this.usernameLabel.Size = new System.Drawing.Size(99, 32);
            this.usernameLabel.TabIndex = 0;
            this.usernameLabel.Text = "label1";
            // 
            // backupRestoreButton
            // 
            this.backupRestoreButton.Location = new System.Drawing.Point(704, 177);
            this.backupRestoreButton.Name = "backupRestoreButton";
            this.backupRestoreButton.Size = new System.Drawing.Size(111, 46);
            this.backupRestoreButton.TabIndex = 6;
            this.backupRestoreButton.Text = "Backup and Restore";
            this.backupRestoreButton.UseVisualStyleBackColor = true;
            this.backupRestoreButton.Click += new System.EventHandler(this.backupRestoreButton_Click);
            // 
            // loginlogUC1
            // 
            this.loginlogUC1.Location = new System.Drawing.Point(18, 44);
            this.loginlogUC1.Name = "loginlogUC1";
            this.loginlogUC1.Size = new System.Drawing.Size(671, 427);
            this.loginlogUC1.TabIndex = 10;
            // 
            // usersUC1
            // 
            this.usersUC1.Location = new System.Drawing.Point(18, 45);
            this.usersUC1.Name = "usersUC1";
            this.usersUC1.Size = new System.Drawing.Size(671, 427);
            this.usersUC1.TabIndex = 9;
            // 
            // inventoryUC1
            // 
            this.inventoryUC1.Location = new System.Drawing.Point(18, 45);
            this.inventoryUC1.Name = "inventoryUC1";
            this.inventoryUC1.Size = new System.Drawing.Size(671, 427);
            this.inventoryUC1.TabIndex = 8;
            // 
            // vouchersUC1
            // 
            this.vouchersUC1.Location = new System.Drawing.Point(18, 45);
            this.vouchersUC1.Name = "vouchersUC1";
            this.vouchersUC1.Size = new System.Drawing.Size(671, 427);
            this.vouchersUC1.TabIndex = 7;
            // 
            // m_1_BackupRestoreUC1
            // 
            this.m_1_BackupRestoreUC1.Location = new System.Drawing.Point(12, 45);
            this.m_1_BackupRestoreUC1.Name = "m_1_BackupRestoreUC1";
            this.m_1_BackupRestoreUC1.Size = new System.Drawing.Size(671, 427);
            this.m_1_BackupRestoreUC1.TabIndex = 12;
            // 
            // M_1_MainS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(848, 496);
            this.Controls.Add(this.m_1_BackupRestoreUC1);
            this.Controls.Add(this.backupRestoreButton);
            this.Controls.Add(this.loginlogUC1);
            this.Controls.Add(this.usersUC1);
            this.Controls.Add(this.inventoryUC1);
            this.Controls.Add(this.vouchersUC1);
            this.Controls.Add(this.usernameLabel);
            this.Controls.Add(this.UsersButton);
            this.Controls.Add(this.loginLogButton);
            this.Controls.Add(this.inventoryButton);
            this.Controls.Add(this.vouchersButton);
            this.Controls.Add(this.newUser);
            this.Controls.Add(this.logOutButton);
            this.Name = "M_1_MainS";
            this.Text = "Factory Inventory - Home";
            this.Load += new System.EventHandler(this.MainS_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button logOutButton;
        private System.Windows.Forms.Button newUser;
        private System.Windows.Forms.Button vouchersButton;
        private System.Windows.Forms.Button inventoryButton;
        private System.Windows.Forms.Button loginLogButton;
        private System.Windows.Forms.Button UsersButton;
        private System.Windows.Forms.Label usernameLabel;
        private M_1_vouchersUC vouchersUC1;
        private M_1_inventoryUC inventoryUC1;
        private M_1_usersUC usersUC1;
        private M_1_loginlogUC loginlogUC1;
        private System.Windows.Forms.Button backupRestoreButton;
        private M_1_BackupRestoreUC m_1_BackupRestoreUC1;
    }
}