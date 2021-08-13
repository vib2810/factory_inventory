namespace Factory_Inventory
{
    partial class TwistERP
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.backupAndRestoreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.applicationSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manageUsersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openWindowsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eRPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tradingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.attendanceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripSeparator();
            this.usertoolStripButton1 = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.firmtoolStripButton2 = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.editAccessButton = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem,
            this.openWindowsToolStripMenuItem,
            this.eRPToolStripMenuItem,
            this.tradingToolStripMenuItem,
            this.attendanceToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.MdiWindowListItem = this.openWindowsToolStripMenuItem;
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(982, 31);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.logoutToolStripMenuItem,
            this.backupAndRestoreToolStripMenuItem,
            this.applicationSettingsToolStripMenuItem,
            this.manageUsersToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(84, 27);
            this.optionsToolStripMenuItem.Text = "&Options";
            // 
            // logoutToolStripMenuItem
            // 
            this.logoutToolStripMenuItem.BackColor = System.Drawing.Color.OrangeRed;
            this.logoutToolStripMenuItem.Name = "logoutToolStripMenuItem";
            this.logoutToolStripMenuItem.Size = new System.Drawing.Size(246, 28);
            this.logoutToolStripMenuItem.Text = "&Logout";
            this.logoutToolStripMenuItem.Click += new System.EventHandler(this.logoutToolStripMenuItem_Click);
            // 
            // backupAndRestoreToolStripMenuItem
            // 
            this.backupAndRestoreToolStripMenuItem.Name = "backupAndRestoreToolStripMenuItem";
            this.backupAndRestoreToolStripMenuItem.Size = new System.Drawing.Size(246, 28);
            this.backupAndRestoreToolStripMenuItem.Text = "&Backup and Restore";
            this.backupAndRestoreToolStripMenuItem.Click += new System.EventHandler(this.backupAndRestoreToolStripMenuItem_Click);
            // 
            // applicationSettingsToolStripMenuItem
            // 
            this.applicationSettingsToolStripMenuItem.Name = "applicationSettingsToolStripMenuItem";
            this.applicationSettingsToolStripMenuItem.Size = new System.Drawing.Size(246, 28);
            this.applicationSettingsToolStripMenuItem.Text = "&Application Settings";
            this.applicationSettingsToolStripMenuItem.Click += new System.EventHandler(this.applicationSettingsToolStripMenuItem_Click);
            // 
            // manageUsersToolStripMenuItem
            // 
            this.manageUsersToolStripMenuItem.Name = "manageUsersToolStripMenuItem";
            this.manageUsersToolStripMenuItem.Size = new System.Drawing.Size(246, 28);
            this.manageUsersToolStripMenuItem.Text = "&Manage Users";
            this.manageUsersToolStripMenuItem.Click += new System.EventHandler(this.manageUsersToolStripMenuItem_Click);
            // 
            // openWindowsToolStripMenuItem
            // 
            this.openWindowsToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.openWindowsToolStripMenuItem.Name = "openWindowsToolStripMenuItem";
            this.openWindowsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.O)));
            this.openWindowsToolStripMenuItem.Size = new System.Drawing.Size(140, 27);
            this.openWindowsToolStripMenuItem.Text = "Open &Windows";
            // 
            // eRPToolStripMenuItem
            // 
            this.eRPToolStripMenuItem.Name = "eRPToolStripMenuItem";
            this.eRPToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.E)));
            this.eRPToolStripMenuItem.Size = new System.Drawing.Size(120, 27);
            this.eRPToolStripMenuItem.Text = "Twist &Factory";
            this.eRPToolStripMenuItem.Click += new System.EventHandler(this.eRPToolStripMenuItem_Click);
            // 
            // tradingToolStripMenuItem
            // 
            this.tradingToolStripMenuItem.Name = "tradingToolStripMenuItem";
            this.tradingToolStripMenuItem.Size = new System.Drawing.Size(81, 27);
            this.tradingToolStripMenuItem.Text = "&Trading";
            this.tradingToolStripMenuItem.Click += new System.EventHandler(this.tradingToolStripMenuItem_Click);
            // 
            // attendanceToolStripMenuItem
            // 
            this.attendanceToolStripMenuItem.Name = "attendanceToolStripMenuItem";
            this.attendanceToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.A)));
            this.attendanceToolStripMenuItem.Size = new System.Drawing.Size(112, 27);
            this.attendanceToolStripMenuItem.Text = "&Attendance";
            this.attendanceToolStripMenuItem.Click += new System.EventHandler(this.attendanceToolStripMenuItem_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton4,
            this.toolStripButton1,
            this.usertoolStripButton1,
            this.toolStripSeparator1,
            this.firmtoolStripButton2,
            this.toolStripButton3,
            this.toolStripLabel1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 719);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(982, 34);
            this.toolStrip1.TabIndex = 5;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(6, 34);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(6, 34);
            // 
            // usertoolStripButton1
            // 
            this.usertoolStripButton1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.usertoolStripButton1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.usertoolStripButton1.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usertoolStripButton1.Name = "usertoolStripButton1";
            this.usertoolStripButton1.ReadOnly = true;
            this.usertoolStripButton1.Size = new System.Drawing.Size(350, 34);
            this.usertoolStripButton1.Text = "Logged in As: ";
            this.usertoolStripButton1.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 34);
            // 
            // firmtoolStripButton2
            // 
            this.firmtoolStripButton2.BackColor = System.Drawing.SystemColors.Info;
            this.firmtoolStripButton2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.firmtoolStripButton2.Name = "firmtoolStripButton2";
            this.firmtoolStripButton2.ReadOnly = true;
            this.firmtoolStripButton2.Size = new System.Drawing.Size(300, 34);
            this.firmtoolStripButton2.Text = "{firmname}";
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(6, 34);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(0, 31);
            // 
            // editAccessButton
            // 
            this.editAccessButton.BackColor = System.Drawing.Color.LawnGreen;
            this.editAccessButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.editAccessButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editAccessButton.Location = new System.Drawing.Point(0, 694);
            this.editAccessButton.Name = "editAccessButton";
            this.editAccessButton.Size = new System.Drawing.Size(982, 25);
            this.editAccessButton.TabIndex = 10;
            this.editAccessButton.Text = "Edit Access";
            this.editAccessButton.UseVisualStyleBackColor = false;
            this.editAccessButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // TwistERP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(982, 753);
            this.Controls.Add(this.editAccessButton);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.IsMdiContainer = true;
            this.Name = "TwistERP";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TwistERP";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TwistERP_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem openWindowsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eRPToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem attendanceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tradingToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripTextBox usertoolStripButton1;
        private System.Windows.Forms.ToolStripSeparator toolStripButton1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripButton4;
        private System.Windows.Forms.ToolStripTextBox firmtoolStripButton2;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logoutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem backupAndRestoreToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem applicationSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manageUsersToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripButton3;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        public System.Windows.Forms.Button editAccessButton;
    }
}