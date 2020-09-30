using System;

namespace Factory_Inventory
{
    partial class M_S_displayUC
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
            this.label1 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.saveButton = new System.Windows.Forms.Button();
            this.sizeyTB = new System.Windows.Forms.TextBox();
            this.scaleyTB = new System.Windows.Forms.TextBox();
            this.scalexTB = new System.Windows.Forms.TextBox();
            this.sizexTB = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Tai Le", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(38, 25);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Display Settings";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft YaHei", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(86, 116);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(51, 20);
            this.label9.TabIndex = 0;
            this.label9.Text = "Size Y:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft YaHei", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(77, 168);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(60, 20);
            this.label8.TabIndex = 2;
            this.label8.Text = "Scale X:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft YaHei", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(78, 220);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 20);
            this.label7.TabIndex = 3;
            this.label7.Text = "Scale Y:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.SystemColors.Control;
            this.label6.Font = new System.Drawing.Font("Microsoft YaHei", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(85, 64);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 20);
            this.label6.TabIndex = 0;
            this.label6.Text = "Size X:";
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(414, 196);
            this.panel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(136, 43);
            this.panel1.TabIndex = 0;
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(414, 254);
            this.saveButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(136, 35);
            this.saveButton.TabIndex = 5;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // sizeyTB
            // 
            this.sizeyTB.Location = new System.Drawing.Point(136, 118);
            this.sizeyTB.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.sizeyTB.Name = "sizeyTB";
            this.sizeyTB.Size = new System.Drawing.Size(236, 20);
            this.sizeyTB.TabIndex = 2;
            // 
            // scaleyTB
            // 
            this.scaleyTB.Location = new System.Drawing.Point(136, 221);
            this.scaleyTB.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.scaleyTB.Name = "scaleyTB";
            this.scaleyTB.Size = new System.Drawing.Size(236, 20);
            this.scaleyTB.TabIndex = 4;
            // 
            // scalexTB
            // 
            this.scalexTB.Location = new System.Drawing.Point(136, 170);
            this.scalexTB.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.scalexTB.Name = "scalexTB";
            this.scalexTB.Size = new System.Drawing.Size(236, 20);
            this.scalexTB.TabIndex = 3;
            // 
            // sizexTB
            // 
            this.sizexTB.Location = new System.Drawing.Point(136, 67);
            this.sizexTB.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.sizexTB.Name = "sizexTB";
            this.sizexTB.Size = new System.Drawing.Size(236, 20);
            this.sizexTB.TabIndex = 1;
            // 
            // M_S_displayUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.sizeyTB);
            this.Controls.Add(this.scaleyTB);
            this.Controls.Add(this.scalexTB);
            this.Controls.Add(this.sizexTB);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "M_S_displayUC";
            this.Size = new System.Drawing.Size(594, 342);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.TextBox sizeyTB;
        private System.Windows.Forms.TextBox scaleyTB;
        private System.Windows.Forms.TextBox scalexTB;
        private System.Windows.Forms.TextBox sizexTB;
    }
}
