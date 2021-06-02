namespace Factory_Inventory
{
    partial class setPrint_Type
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
            this.saveButton = new System.Windows.Forms.Button();
            this.firmnameTB = new System.Windows.Forms.TextBox();
            this.label1TB = new System.Windows.Forms.Label();
            this.addressTB = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.gstinTB = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.pnoTB = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.emailTB = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(65, 152);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(200, 50);
            this.saveButton.TabIndex = 12;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // firmnameTB
            // 
            this.firmnameTB.Location = new System.Drawing.Point(121, 12);
            this.firmnameTB.Name = "firmnameTB";
            this.firmnameTB.Size = new System.Drawing.Size(190, 22);
            this.firmnameTB.TabIndex = 14;
            // 
            // label1TB
            // 
            this.label1TB.AutoSize = true;
            this.label1TB.Location = new System.Drawing.Point(15, 15);
            this.label1TB.Name = "label1TB";
            this.label1TB.Size = new System.Drawing.Size(76, 17);
            this.label1TB.TabIndex = 13;
            this.label1TB.Text = "Firm Name";
            // 
            // addressTB
            // 
            this.addressTB.Location = new System.Drawing.Point(121, 40);
            this.addressTB.Name = "addressTB";
            this.addressTB.Size = new System.Drawing.Size(190, 22);
            this.addressTB.TabIndex = 16;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 17);
            this.label2.TabIndex = 15;
            this.label2.Text = "Address";
            // 
            // gstinTB
            // 
            this.gstinTB.Location = new System.Drawing.Point(121, 68);
            this.gstinTB.Name = "gstinTB";
            this.gstinTB.Size = new System.Drawing.Size(190, 22);
            this.gstinTB.TabIndex = 18;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 17);
            this.label3.TabIndex = 17;
            this.label3.Text = "GSTIN";
            // 
            // pnoTB
            // 
            this.pnoTB.Location = new System.Drawing.Point(121, 96);
            this.pnoTB.Name = "pnoTB";
            this.pnoTB.Size = new System.Drawing.Size(190, 22);
            this.pnoTB.TabIndex = 20;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 99);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(103, 17);
            this.label4.TabIndex = 19;
            this.label4.Text = "Phone Number";
            // 
            // emailTB
            // 
            this.emailTB.Location = new System.Drawing.Point(121, 124);
            this.emailTB.Name = "emailTB";
            this.emailTB.Size = new System.Drawing.Size(190, 22);
            this.emailTB.TabIndex = 22;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 127);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 17);
            this.label5.TabIndex = 21;
            this.label5.Text = "Email ID";
            // 
            // setPrint_Type
            // 
            this.AcceptButton = this.saveButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(338, 220);
            this.Controls.Add(this.emailTB);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.pnoTB);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.gstinTB);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.addressTB);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.firmnameTB);
            this.Controls.Add(this.label1TB);
            this.Controls.Add(this.saveButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "setPrint_Type";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Set Print Type";
            this.Load += new System.EventHandler(this.setPrint_Type_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.TextBox firmnameTB;
        private System.Windows.Forms.Label label1TB;
        private System.Windows.Forms.TextBox addressTB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox gstinTB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox pnoTB;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox emailTB;
        private System.Windows.Forms.Label label5;
    }
}