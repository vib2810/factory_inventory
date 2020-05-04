﻿namespace Factory_Inventory
{
    partial class editCone
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
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.newPasswordLabel = new System.Windows.Forms.Label();
            this.newConfirmPasswordLabel = new System.Windows.Forms.Label();
            this.newAccessLevelLabel = new System.Windows.Forms.Label();
            this.newQualityTextbox = new System.Windows.Forms.TextBox();
            this.editedQualityTextbox = new System.Windows.Forms.TextBox();
            this.deleteUserCheckbox = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.addQualityButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.userDataView)).BeginInit();
            this.SuspendLayout();
            // 
            // userLabel
            // 
            this.userLabel.AutoSize = true;
            this.userLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userLabel.Location = new System.Drawing.Point(53, 20);
            this.userLabel.Name = "userLabel";
            this.userLabel.Size = new System.Drawing.Size(98, 25);
            this.userLabel.TabIndex = 1;
            this.userLabel.Text = "Edit Cone";
            this.userLabel.Click += new System.EventHandler(this.userLabel_Click);
            // 
            // userDataView
            // 
            this.userDataView.AllowUserToAddRows = false;
            this.userDataView.AllowUserToDeleteRows = false;
            this.userDataView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.userDataView.Location = new System.Drawing.Point(236, 0);
            this.userDataView.MultiSelect = false;
            this.userDataView.Name = "userDataView";
            this.userDataView.ReadOnly = true;
            this.userDataView.RowHeadersWidth = 51;
            this.userDataView.RowTemplate.Height = 24;
            this.userDataView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.userDataView.Size = new System.Drawing.Size(383, 393);
            this.userDataView.TabIndex = 2;
            this.userDataView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.userDataView_CellClick);
            this.userDataView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.userDataView_CellContentClick);
            // 
            // confirmButton
            // 
            this.confirmButton.Location = new System.Drawing.Point(19, 137);
            this.confirmButton.Name = "confirmButton";
            this.confirmButton.Size = new System.Drawing.Size(75, 23);
            this.confirmButton.TabIndex = 3;
            this.confirmButton.Text = "Confirm";
            this.confirmButton.UseVisualStyleBackColor = true;
            this.confirmButton.Click += new System.EventHandler(this.confirmButton_Click);
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
            this.newPasswordLabel.Location = new System.Drawing.Point(17, 45);
            this.newPasswordLabel.Name = "newPasswordLabel";
            this.newPasswordLabel.Size = new System.Drawing.Size(140, 17);
            this.newPasswordLabel.TabIndex = 6;
            this.newPasswordLabel.Text = "Cone Weight in gram";
            // 
            // newConfirmPasswordLabel
            // 
            this.newConfirmPasswordLabel.AutoSize = true;
            this.newConfirmPasswordLabel.Location = new System.Drawing.Point(16, 261);
            this.newConfirmPasswordLabel.Name = "newConfirmPasswordLabel";
            this.newConfirmPasswordLabel.Size = new System.Drawing.Size(140, 17);
            this.newConfirmPasswordLabel.TabIndex = 7;
            this.newConfirmPasswordLabel.Text = "Cone Weight in gram";
            // 
            // newAccessLevelLabel
            // 
            this.newAccessLevelLabel.AutoSize = true;
            this.newAccessLevelLabel.Location = new System.Drawing.Point(17, 90);
            this.newAccessLevelLabel.Name = "newAccessLevelLabel";
            this.newAccessLevelLabel.Size = new System.Drawing.Size(29, 17);
            this.newAccessLevelLabel.TabIndex = 8;
            this.newAccessLevelLabel.Text = "OR";
            // 
            // newQualityTextbox
            // 
            this.newQualityTextbox.Location = new System.Drawing.Point(20, 281);
            this.newQualityTextbox.Name = "newQualityTextbox";
            this.newQualityTextbox.Size = new System.Drawing.Size(144, 22);
            this.newQualityTextbox.TabIndex = 11;
            // 
            // editedQualityTextbox
            // 
            this.editedQualityTextbox.Location = new System.Drawing.Point(20, 65);
            this.editedQualityTextbox.Name = "editedQualityTextbox";
            this.editedQualityTextbox.Size = new System.Drawing.Size(144, 22);
            this.editedQualityTextbox.TabIndex = 12;
            // 
            // deleteUserCheckbox
            // 
            this.deleteUserCheckbox.AutoSize = true;
            this.deleteUserCheckbox.Location = new System.Drawing.Point(19, 110);
            this.deleteUserCheckbox.Name = "deleteUserCheckbox";
            this.deleteUserCheckbox.Size = new System.Drawing.Size(108, 21);
            this.deleteUserCheckbox.TabIndex = 14;
            this.deleteUserCheckbox.Text = "Delete Cone";
            this.deleteUserCheckbox.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(53, 227);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 25);
            this.label1.TabIndex = 15;
            this.label1.Text = "Add Cone";
            // 
            // addQualityButton
            // 
            this.addQualityButton.Location = new System.Drawing.Point(20, 309);
            this.addQualityButton.Name = "addQualityButton";
            this.addQualityButton.Size = new System.Drawing.Size(75, 23);
            this.addQualityButton.TabIndex = 16;
            this.addQualityButton.Text = "Add";
            this.addQualityButton.UseVisualStyleBackColor = true;
            this.addQualityButton.Click += new System.EventHandler(this.addQualityButton_Click);
            // 
            // editCone
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.addQualityButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.deleteUserCheckbox);
            this.Controls.Add(this.editedQualityTextbox);
            this.Controls.Add(this.newQualityTextbox);
            this.Controls.Add(this.newAccessLevelLabel);
            this.Controls.Add(this.newConfirmPasswordLabel);
            this.Controls.Add(this.newPasswordLabel);
            this.Controls.Add(this.confirmButton);
            this.Controls.Add(this.userDataView);
            this.Controls.Add(this.userLabel);
            this.Name = "editCone";
            this.Size = new System.Drawing.Size(619, 393);
            ((System.ComponentModel.ISupportInitialize)(this.userDataView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label userLabel;
        private System.Windows.Forms.DataGridView userDataView;
        private System.Windows.Forms.Button confirmButton;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Label newPasswordLabel;
        private System.Windows.Forms.Label newConfirmPasswordLabel;
        private System.Windows.Forms.Label newAccessLevelLabel;
        private System.Windows.Forms.TextBox newQualityTextbox;
        private System.Windows.Forms.TextBox editedQualityTextbox;
        private System.Windows.Forms.CheckBox deleteUserCheckbox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button addQualityButton;
    }
}