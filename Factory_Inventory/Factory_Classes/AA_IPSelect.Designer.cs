namespace Factory_Inventory.Factory_Classes
{
    partial class AA_IPSelect
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
            this.ipTB = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.saveButton = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // ipTB
            // 
            this.ipTB.Location = new System.Drawing.Point(17, 48);
            this.ipTB.Name = "ipTB";
            this.ipTB.Size = new System.Drawing.Size(277, 22);
            this.ipTB.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(185, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Select IP Address";
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(70, 127);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(167, 52);
            this.saveButton.TabIndex = 2;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(87, 76);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(207, 25);
            this.button2.TabIndex = 11;
            this.button2.Text = "Set Local Server Name";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(17, 80);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(64, 21);
            this.checkBox1.TabIndex = 10;
            this.checkBox1.Text = "Local";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // AA_IPSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(313, 202);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ipTB);
            this.Name = "AA_IPSelect";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AA_IPSelect";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox ipTB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}