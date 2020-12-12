namespace Factory_Inventory
{
    partial class O_U_ManageUsersForm
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
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.o_U_usersUC1 = new Factory_Inventory.O_U_usersUC();
            this.o_U_loginlogUC1 = new Factory_Inventory.O_U_loginlogUC();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(26, 24);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(892, 499);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.o_U_usersUC1);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(884, 466);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Add/Edit Users";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.o_U_loginlogUC1);
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(884, 466);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Login Log";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // o_U_usersUC1
            // 
            this.o_U_usersUC1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.o_U_usersUC1.Location = new System.Drawing.Point(3, 3);
            this.o_U_usersUC1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.o_U_usersUC1.Name = "o_U_usersUC1";
            this.o_U_usersUC1.Size = new System.Drawing.Size(878, 460);
            this.o_U_usersUC1.TabIndex = 0;
            // 
            // o_U_loginlogUC1
            // 
            this.o_U_loginlogUC1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.o_U_loginlogUC1.Location = new System.Drawing.Point(3, 3);
            this.o_U_loginlogUC1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.o_U_loginlogUC1.Name = "o_U_loginlogUC1";
            this.o_U_loginlogUC1.Size = new System.Drawing.Size(878, 460);
            this.o_U_loginlogUC1.TabIndex = 0;
            // 
            // O_U_ManageUsersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(950, 550);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "O_U_ManageUsersForm";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private O_U_usersUC o_U_usersUC1;
        private O_U_loginlogUC o_U_loginlogUC1;
    }
}
