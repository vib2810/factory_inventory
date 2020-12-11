namespace Factory_Inventory
{
    partial class setLocalString
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
            this.label1 = new System.Windows.Forms.Label();
            this.serverNameTB = new System.Windows.Forms.TextBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Server Name";
            // 
            // serverNameTB
            // 
            this.serverNameTB.Location = new System.Drawing.Point(130, 18);
            this.serverNameTB.Name = "serverNameTB";
            this.serverNameTB.Size = new System.Drawing.Size(190, 22);
            this.serverNameTB.TabIndex = 3;
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(120, 69);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(105, 35);
            this.saveButton.TabIndex = 6;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // setLocalString
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(350, 125);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.serverNameTB);
            this.Controls.Add(this.label1);
            this.Name = "setLocalString";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Set Local Server Name";
            this.Load += new System.EventHandler(this.setLocalString_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox serverNameTB;
        private System.Windows.Forms.Button saveButton;
    }
}