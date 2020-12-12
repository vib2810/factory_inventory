namespace Factory_Inventory
{
    partial class A_1_AddEditDropDowns
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
            this.editGroupButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.a_1_editGroup1 = new Factory_Inventory.A_1_editGroup();
            this.SuspendLayout();
            // 
            // editGroupButton
            // 
            this.editGroupButton.BackColor = System.Drawing.Color.DarkGray;
            this.editGroupButton.Location = new System.Drawing.Point(819, 37);
            this.editGroupButton.Name = "editGroupButton";
            this.editGroupButton.Size = new System.Drawing.Size(147, 48);
            this.editGroupButton.TabIndex = 1;
            this.editGroupButton.Text = "Group";
            this.editGroupButton.UseVisualStyleBackColor = false;
            this.editGroupButton.Click += new System.EventHandler(this.editQualityButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(814, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 25);
            this.label1.TabIndex = 15;
            this.label1.Text = "Add/Edit:";
            // 
            // a_1_editGroup1
            // 
            this.a_1_editGroup1.Location = new System.Drawing.Point(0, 0);
            this.a_1_editGroup1.Name = "a_1_editGroup1";
            this.a_1_editGroup1.Size = new System.Drawing.Size(800, 700);
            this.a_1_editGroup1.TabIndex = 16;
            // 
            // A_1_AddEditDropDowns
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(978, 703);
            this.Controls.Add(this.a_1_editGroup1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.editGroupButton);
            this.Name = "A_1_AddEditDropDowns";
            this.Text = "Factory Attemdance - Add/Edit Fixed Details";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button editGroupButton;
        private System.Windows.Forms.Label label1;
        private A_1_editGroup a_1_editGroup1;
    }
}