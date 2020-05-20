namespace Factory_Inventory
{
    partial class M_1_inventoryUC
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
            this.voucherLabel = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.fromtoButton = new System.Windows.Forms.Button();
            this.tablesButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // voucherLabel
            // 
            this.voucherLabel.AutoSize = true;
            this.voucherLabel.Location = new System.Drawing.Point(13, 18);
            this.voucherLabel.Name = "voucherLabel";
            this.voucherLabel.Size = new System.Drawing.Size(105, 17);
            this.voucherLabel.TabIndex = 0;
            this.voucherLabel.Text = "Inventory Menu";
            this.voucherLabel.Click += new System.EventHandler(this.voucherLabel_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.button1.Location = new System.Drawing.Point(16, 51);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(97, 55);
            this.button1.TabIndex = 1;
            this.button1.Text = "On Date";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // fromtoButton
            // 
            this.fromtoButton.BackColor = System.Drawing.SystemColors.ControlDark;
            this.fromtoButton.Location = new System.Drawing.Point(16, 112);
            this.fromtoButton.Name = "fromtoButton";
            this.fromtoButton.Size = new System.Drawing.Size(97, 53);
            this.fromtoButton.TabIndex = 2;
            this.fromtoButton.Text = "From-To Date";
            this.fromtoButton.UseVisualStyleBackColor = false;
            this.fromtoButton.Click += new System.EventHandler(this.fromtoButton_Click);
            // 
            // tablesButton
            // 
            this.tablesButton.BackColor = System.Drawing.SystemColors.ControlDark;
            this.tablesButton.Location = new System.Drawing.Point(16, 171);
            this.tablesButton.Name = "tablesButton";
            this.tablesButton.Size = new System.Drawing.Size(97, 53);
            this.tablesButton.TabIndex = 4;
            this.tablesButton.Text = "Tables";
            this.tablesButton.UseVisualStyleBackColor = false;
            this.tablesButton.Click += new System.EventHandler(this.tablesButton_Click);
            // 
            // M_1_inventoryUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tablesButton);
            this.Controls.Add(this.fromtoButton);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.voucherLabel);
            this.Name = "M_1_inventoryUC";
            this.Size = new System.Drawing.Size(619, 432);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label voucherLabel;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button fromtoButton;
        private System.Windows.Forms.Button tablesButton;
    }
}
