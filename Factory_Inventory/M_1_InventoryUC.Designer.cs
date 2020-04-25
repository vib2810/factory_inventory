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
            this.trayProductionButton = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.editCNameQualityButton = new System.Windows.Forms.Button();
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
            this.button1.Size = new System.Drawing.Size(97, 34);
            this.button1.TabIndex = 1;
            this.button1.Text = "On Date";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // trayProductionButton
            // 
            this.trayProductionButton.BackColor = System.Drawing.SystemColors.ControlDark;
            this.trayProductionButton.Location = new System.Drawing.Point(16, 100);
            this.trayProductionButton.Name = "trayProductionButton";
            this.trayProductionButton.Size = new System.Drawing.Size(97, 53);
            this.trayProductionButton.TabIndex = 2;
            this.trayProductionButton.Text = "Tray Production";
            this.trayProductionButton.UseVisualStyleBackColor = false;
            this.trayProductionButton.Click += new System.EventHandler(this.trayProductionButton_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.SystemColors.ControlDark;
            this.button3.Location = new System.Drawing.Point(16, 169);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(97, 53);
            this.button3.TabIndex = 3;
            this.button3.Text = "Carton Production";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.SystemColors.ControlDark;
            this.button4.Location = new System.Drawing.Point(16, 228);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(97, 34);
            this.button4.TabIndex = 4;
            this.button4.Text = "button4";
            this.button4.UseVisualStyleBackColor = false;
            // 
            // editCNameQualityButton
            // 
            this.editCNameQualityButton.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.editCNameQualityButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.editCNameQualityButton.Location = new System.Drawing.Point(16, 342);
            this.editCNameQualityButton.Name = "editCNameQualityButton";
            this.editCNameQualityButton.Size = new System.Drawing.Size(97, 66);
            this.editCNameQualityButton.TabIndex = 7;
            this.editCNameQualityButton.Text = "Add/Edit Drop Downs";
            this.editCNameQualityButton.UseVisualStyleBackColor = false;
            this.editCNameQualityButton.Click += new System.EventHandler(this.editCNameQualityButton_Click);
            // 
            // M_1_inventoryUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.editCNameQualityButton);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.trayProductionButton);
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
        private System.Windows.Forms.Button trayProductionButton;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button editCNameQualityButton;
    }
}
