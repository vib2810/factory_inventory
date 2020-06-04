namespace Factory_Inventory
{
    partial class editColour
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
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
            this.editDyeingRateTexbox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.addDyeingRateTexbox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.addQualityCombobox = new System.Windows.Forms.ComboBox();
            this.editQualityCombobox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // userLabel
            // 
            this.userLabel.AutoSize = true;
            this.userLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userLabel.Location = new System.Drawing.Point(15, 11);
            this.userLabel.Name = "userLabel";
            this.userLabel.Size = new System.Drawing.Size(108, 25);
            this.userLabel.TabIndex = 1;
            this.userLabel.Text = "Edit Colour";
            this.userLabel.Click += new System.EventHandler(this.userLabel_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(236, 0);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(383, 486);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // confirmButton
            // 
            this.confirmButton.Location = new System.Drawing.Point(19, 220);
            this.confirmButton.Name = "confirmButton";
            this.confirmButton.Size = new System.Drawing.Size(75, 34);
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
            this.newPasswordLabel.Location = new System.Drawing.Point(17, 36);
            this.newPasswordLabel.Name = "newPasswordLabel";
            this.newPasswordLabel.Size = new System.Drawing.Size(49, 17);
            this.newPasswordLabel.TabIndex = 6;
            this.newPasswordLabel.Text = "Colour";
            // 
            // newConfirmPasswordLabel
            // 
            this.newConfirmPasswordLabel.AutoSize = true;
            this.newConfirmPasswordLabel.Location = new System.Drawing.Point(16, 297);
            this.newConfirmPasswordLabel.Name = "newConfirmPasswordLabel";
            this.newConfirmPasswordLabel.Size = new System.Drawing.Size(87, 17);
            this.newConfirmPasswordLabel.TabIndex = 7;
            this.newConfirmPasswordLabel.Text = "Enter Colour";
            // 
            // newAccessLevelLabel
            // 
            this.newAccessLevelLabel.AutoSize = true;
            this.newAccessLevelLabel.Location = new System.Drawing.Point(17, 173);
            this.newAccessLevelLabel.Name = "newAccessLevelLabel";
            this.newAccessLevelLabel.Size = new System.Drawing.Size(29, 17);
            this.newAccessLevelLabel.TabIndex = 8;
            this.newAccessLevelLabel.Text = "OR";
            // 
            // newQualityTextbox
            // 
            this.newQualityTextbox.Location = new System.Drawing.Point(19, 317);
            this.newQualityTextbox.Name = "newQualityTextbox";
            this.newQualityTextbox.Size = new System.Drawing.Size(144, 22);
            this.newQualityTextbox.TabIndex = 11;
            // 
            // editedQualityTextbox
            // 
            this.editedQualityTextbox.Location = new System.Drawing.Point(20, 56);
            this.editedQualityTextbox.Name = "editedQualityTextbox";
            this.editedQualityTextbox.ReadOnly = true;
            this.editedQualityTextbox.Size = new System.Drawing.Size(144, 22);
            this.editedQualityTextbox.TabIndex = 12;
            // 
            // deleteUserCheckbox
            // 
            this.deleteUserCheckbox.AutoSize = true;
            this.deleteUserCheckbox.Enabled = false;
            this.deleteUserCheckbox.Location = new System.Drawing.Point(19, 193);
            this.deleteUserCheckbox.Name = "deleteUserCheckbox";
            this.deleteUserCheckbox.Size = new System.Drawing.Size(116, 21);
            this.deleteUserCheckbox.TabIndex = 14;
            this.deleteUserCheckbox.Text = "Delete Colour";
            this.deleteUserCheckbox.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(15, 272);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 25);
            this.label1.TabIndex = 15;
            this.label1.Text = "Add Colour";
            // 
            // addQualityButton
            // 
            this.addQualityButton.Location = new System.Drawing.Point(19, 437);
            this.addQualityButton.Name = "addQualityButton";
            this.addQualityButton.Size = new System.Drawing.Size(75, 35);
            this.addQualityButton.TabIndex = 16;
            this.addQualityButton.Text = "Add";
            this.addQualityButton.UseVisualStyleBackColor = true;
            this.addQualityButton.Click += new System.EventHandler(this.addQualityButton_Click);
            // 
            // editDyeingRateTexbox
            // 
            this.editDyeingRateTexbox.Location = new System.Drawing.Point(19, 101);
            this.editDyeingRateTexbox.Name = "editDyeingRateTexbox";
            this.editDyeingRateTexbox.Size = new System.Drawing.Size(144, 22);
            this.editDyeingRateTexbox.TabIndex = 18;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 17);
            this.label2.TabIndex = 17;
            this.label2.Text = "Dyeing Rate";
            // 
            // addDyeingRateTexbox
            // 
            this.addDyeingRateTexbox.Location = new System.Drawing.Point(19, 362);
            this.addDyeingRateTexbox.Name = "addDyeingRateTexbox";
            this.addDyeingRateTexbox.Size = new System.Drawing.Size(144, 22);
            this.addDyeingRateTexbox.TabIndex = 20;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 342);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 17);
            this.label3.TabIndex = 19;
            this.label3.Text = "Dyeing Rate";
            // 
            // addQualityCombobox
            // 
            this.addQualityCombobox.FormattingEnabled = true;
            this.addQualityCombobox.Location = new System.Drawing.Point(19, 407);
            this.addQualityCombobox.Name = "addQualityCombobox";
            this.addQualityCombobox.Size = new System.Drawing.Size(121, 24);
            this.addQualityCombobox.TabIndex = 21;
            // 
            // editQualityCombobox
            // 
            this.editQualityCombobox.FormattingEnabled = true;
            this.editQualityCombobox.Location = new System.Drawing.Point(20, 146);
            this.editQualityCombobox.Name = "editQualityCombobox";
            this.editQualityCombobox.Size = new System.Drawing.Size(141, 24);
            this.editQualityCombobox.TabIndex = 22;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 126);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 17);
            this.label4.TabIndex = 23;
            this.label4.Text = "Quality";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 387);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 17);
            this.label5.TabIndex = 24;
            this.label5.Text = "Quality";
            // 
            // editColour
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.editQualityCombobox);
            this.Controls.Add(this.addQualityCombobox);
            this.Controls.Add(this.addDyeingRateTexbox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.editDyeingRateTexbox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.addQualityButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.deleteUserCheckbox);
            this.Controls.Add(this.editedQualityTextbox);
            this.Controls.Add(this.newQualityTextbox);
            this.Controls.Add(this.newAccessLevelLabel);
            this.Controls.Add(this.newConfirmPasswordLabel);
            this.Controls.Add(this.newPasswordLabel);
            this.Controls.Add(this.confirmButton);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.userLabel);
            this.Name = "editColour";
            this.Size = new System.Drawing.Size(619, 486);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label userLabel;
        private System.Windows.Forms.DataGridView dataGridView1;
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
        private System.Windows.Forms.TextBox editDyeingRateTexbox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox addDyeingRateTexbox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox addQualityCombobox;
        private System.Windows.Forms.ComboBox editQualityCombobox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}
