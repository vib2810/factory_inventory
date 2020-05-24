﻿namespace Factory_Inventory
{
    partial class M_1_usersUC
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
            this.components = new System.ComponentModel.Container();
            this.userLabel = new System.Windows.Forms.Label();
            this.userDataView = new System.Windows.Forms.DataGridView();
            this.confirmButton = new System.Windows.Forms.Button();
            this.newUsernameLabel = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.newPasswordLabel = new System.Windows.Forms.Label();
            this.newConfirmPasswordLabel = new System.Windows.Forms.Label();
            this.newAccessLevelLabel = new System.Windows.Forms.Label();
            this.usernameTextbox = new System.Windows.Forms.TextBox();
            this.conformPasswordTextbox = new System.Windows.Forms.TextBox();
            this.passwordTextbox = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.deleteUserCheckbox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.userDataView)).BeginInit();
            this.SuspendLayout();
            // 
            // userLabel
            // 
            this.userLabel.AutoSize = true;
            this.userLabel.Location = new System.Drawing.Point(17, 9);
            this.userLabel.Name = "userLabel";
            this.userLabel.Size = new System.Drawing.Size(104, 17);
            this.userLabel.TabIndex = 0;
            this.userLabel.Text = " Manage Users";
            // 
            // userDataView
            // 
            this.userDataView.AllowUserToAddRows = false;
            this.userDataView.AllowUserToDeleteRows = false;
            this.userDataView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.userDataView.Location = new System.Drawing.Point(153, 0);
            this.userDataView.MultiSelect = false;
            this.userDataView.Name = "userDataView";
            this.userDataView.ReadOnly = true;
            this.userDataView.RowHeadersWidth = 51;
            this.userDataView.RowTemplate.Height = 24;
            this.userDataView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.userDataView.Size = new System.Drawing.Size(466, 393);
            this.userDataView.TabIndex = 0;
            this.userDataView.TabStop = false;
            this.userDataView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.userDataView_CellClick);
            this.userDataView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.userDataView_CellContentClick);
            // 
            // confirmButton
            // 
            this.confirmButton.Location = new System.Drawing.Point(20, 282);
            this.confirmButton.Name = "confirmButton";
            this.confirmButton.Size = new System.Drawing.Size(75, 23);
            this.confirmButton.TabIndex = 9;
            this.confirmButton.Text = "Update";
            this.confirmButton.UseVisualStyleBackColor = true;
            this.confirmButton.Click += new System.EventHandler(this.confirmButton_Click);
            // 
            // newUsernameLabel
            // 
            this.newUsernameLabel.AutoSize = true;
            this.newUsernameLabel.Location = new System.Drawing.Point(17, 47);
            this.newUsernameLabel.Name = "newUsernameLabel";
            this.newUsernameLabel.Size = new System.Drawing.Size(73, 17);
            this.newUsernameLabel.TabIndex = 0;
            this.newUsernameLabel.Text = "Username";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // newPasswordLabel
            // 
            this.newPasswordLabel.AutoSize = true;
            this.newPasswordLabel.Location = new System.Drawing.Point(17, 95);
            this.newPasswordLabel.Name = "newPasswordLabel";
            this.newPasswordLabel.Size = new System.Drawing.Size(100, 17);
            this.newPasswordLabel.TabIndex = 0;
            this.newPasswordLabel.Text = "New Password";
            // 
            // newConfirmPasswordLabel
            // 
            this.newConfirmPasswordLabel.AutoSize = true;
            this.newConfirmPasswordLabel.Location = new System.Drawing.Point(17, 140);
            this.newConfirmPasswordLabel.Name = "newConfirmPasswordLabel";
            this.newConfirmPasswordLabel.Size = new System.Drawing.Size(121, 17);
            this.newConfirmPasswordLabel.TabIndex = 0;
            this.newConfirmPasswordLabel.Text = "Confirm Password";
            // 
            // newAccessLevelLabel
            // 
            this.newAccessLevelLabel.AutoSize = true;
            this.newAccessLevelLabel.Location = new System.Drawing.Point(17, 185);
            this.newAccessLevelLabel.Name = "newAccessLevelLabel";
            this.newAccessLevelLabel.Size = new System.Drawing.Size(91, 17);
            this.newAccessLevelLabel.TabIndex = 0;
            this.newAccessLevelLabel.Text = "Access Level";
            // 
            // usernameTextbox
            // 
            this.usernameTextbox.Location = new System.Drawing.Point(21, 67);
            this.usernameTextbox.Name = "usernameTextbox";
            this.usernameTextbox.ReadOnly = true;
            this.usernameTextbox.Size = new System.Drawing.Size(100, 22);
            this.usernameTextbox.TabIndex = 0;
            this.usernameTextbox.TabStop = false;
            // 
            // conformPasswordTextbox
            // 
            this.conformPasswordTextbox.Location = new System.Drawing.Point(21, 160);
            this.conformPasswordTextbox.Name = "conformPasswordTextbox";
            this.conformPasswordTextbox.PasswordChar = '*';
            this.conformPasswordTextbox.Size = new System.Drawing.Size(100, 22);
            this.conformPasswordTextbox.TabIndex = 3;
            // 
            // passwordTextbox
            // 
            this.passwordTextbox.Location = new System.Drawing.Point(20, 115);
            this.passwordTextbox.Name = "passwordTextbox";
            this.passwordTextbox.PasswordChar = '*';
            this.passwordTextbox.Size = new System.Drawing.Size(100, 22);
            this.passwordTextbox.TabIndex = 1;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(21, 206);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(100, 24);
            this.comboBox1.TabIndex = 5;
            this.comboBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.comboBox1_KeyDown);
            // 
            // deleteUserCheckbox
            // 
            this.deleteUserCheckbox.AutoSize = true;
            this.deleteUserCheckbox.Location = new System.Drawing.Point(20, 246);
            this.deleteUserCheckbox.Name = "deleteUserCheckbox";
            this.deleteUserCheckbox.Size = new System.Drawing.Size(113, 21);
            this.deleteUserCheckbox.TabIndex = 7;
            this.deleteUserCheckbox.Text = "Delete User?";
            this.deleteUserCheckbox.UseVisualStyleBackColor = true;
            // 
            // M_1_usersUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.deleteUserCheckbox);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.passwordTextbox);
            this.Controls.Add(this.conformPasswordTextbox);
            this.Controls.Add(this.usernameTextbox);
            this.Controls.Add(this.newAccessLevelLabel);
            this.Controls.Add(this.newConfirmPasswordLabel);
            this.Controls.Add(this.newPasswordLabel);
            this.Controls.Add(this.newUsernameLabel);
            this.Controls.Add(this.confirmButton);
            this.Controls.Add(this.userDataView);
            this.Controls.Add(this.userLabel);
            this.Name = "M_1_usersUC";
            this.Size = new System.Drawing.Size(619, 393);
            ((System.ComponentModel.ISupportInitialize)(this.userDataView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label userLabel;
        private System.Windows.Forms.DataGridView userDataView;
        private System.Windows.Forms.Button confirmButton;
        private System.Windows.Forms.Label newUsernameLabel;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Label newPasswordLabel;
        private System.Windows.Forms.Label newConfirmPasswordLabel;
        private System.Windows.Forms.Label newAccessLevelLabel;
        public System.Windows.Forms.TextBox usernameTextbox;
        public System.Windows.Forms.TextBox conformPasswordTextbox;
        public System.Windows.Forms.TextBox passwordTextbox;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.CheckBox deleteUserCheckbox;
    }
}
