using Factory_Inventory.Factory_Classes;

namespace Factory_Inventory
{
    partial class A_1_MainS
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
            this.employeesButton = new System.Windows.Forms.Button();
            this.markAttendanceButton = new System.Windows.Forms.Button();
            this.reportsButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.masterButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.a_1_EmployeesUC1 = new Factory_Inventory.A_1_EmployeesUC();
            this.a_1_MarkAttendanceUC1 = new Factory_Inventory.A_1_MarkAttendanceUC();
            this.SuspendLayout();
            // 
            // employeesButton
            // 
            this.employeesButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.employeesButton.BackColor = System.Drawing.Color.DarkGray;
            this.employeesButton.Location = new System.Drawing.Point(785, 36);
            this.employeesButton.Name = "employeesButton";
            this.employeesButton.Size = new System.Drawing.Size(126, 60);
            this.employeesButton.TabIndex = 3;
            this.employeesButton.Text = "&Employees";
            this.employeesButton.UseVisualStyleBackColor = false;
            this.employeesButton.Click += new System.EventHandler(this.vouchersButton_Click_1);
            // 
            // markAttendanceButton
            // 
            this.markAttendanceButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.markAttendanceButton.BackColor = System.Drawing.Color.DarkGray;
            this.markAttendanceButton.Location = new System.Drawing.Point(785, 102);
            this.markAttendanceButton.Name = "markAttendanceButton";
            this.markAttendanceButton.Size = new System.Drawing.Size(126, 60);
            this.markAttendanceButton.TabIndex = 4;
            this.markAttendanceButton.Text = "Mark &Attendance";
            this.markAttendanceButton.UseVisualStyleBackColor = false;
            this.markAttendanceButton.Click += new System.EventHandler(this.inventoryButton_Click_1);
            // 
            // reportsButton
            // 
            this.reportsButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.reportsButton.BackColor = System.Drawing.Color.DarkGray;
            this.reportsButton.Location = new System.Drawing.Point(785, 166);
            this.reportsButton.Name = "reportsButton";
            this.reportsButton.Size = new System.Drawing.Size(126, 60);
            this.reportsButton.TabIndex = 6;
            this.reportsButton.Text = "&Reports";
            this.reportsButton.UseVisualStyleBackColor = false;
            this.reportsButton.Click += new System.EventHandler(this.loginLogButton_Click);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Gainsboro;
            this.label2.Cursor = System.Windows.Forms.Cursors.Default;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(890, 141);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "A";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Gainsboro;
            this.label1.Cursor = System.Windows.Forms.Cursors.Default;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(890, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "E";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Gainsboro;
            this.label3.Cursor = System.Windows.Forms.Cursors.Default;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(890, 205);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(18, 17);
            this.label3.TabIndex = 7;
            this.label3.Text = "R";
            // 
            // masterButton
            // 
            this.masterButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.masterButton.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.masterButton.Location = new System.Drawing.Point(785, 460);
            this.masterButton.Name = "masterButton";
            this.masterButton.Size = new System.Drawing.Size(126, 60);
            this.masterButton.TabIndex = 8;
            this.masterButton.Text = "&Master";
            this.masterButton.UseVisualStyleBackColor = false;
            this.masterButton.Click += new System.EventHandler(this.masterButton_Click);
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Gainsboro;
            this.label4.Cursor = System.Windows.Forms.Cursors.Default;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(890, 502);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(19, 17);
            this.label4.TabIndex = 9;
            this.label4.Text = "M";
            // 
            // a_1_EmployeesUC1
            // 
            this.a_1_EmployeesUC1.Location = new System.Drawing.Point(60, 36);
            this.a_1_EmployeesUC1.Name = "a_1_EmployeesUC1";
            this.a_1_EmployeesUC1.Size = new System.Drawing.Size(720, 483);
            this.a_1_EmployeesUC1.TabIndex = 0;
            // 
            // a_1_MarkAttendanceUC1
            // 
            this.a_1_MarkAttendanceUC1.Location = new System.Drawing.Point(60, 36);
            this.a_1_MarkAttendanceUC1.Name = "a_1_MarkAttendanceUC1";
            this.a_1_MarkAttendanceUC1.Size = new System.Drawing.Size(720, 483);
            this.a_1_MarkAttendanceUC1.TabIndex = 0;
            // 
            // A_1_MainS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(950, 550);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.masterButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.reportsButton);
            this.Controls.Add(this.markAttendanceButton);
            this.Controls.Add(this.employeesButton);
            this.Controls.Add(this.a_1_MarkAttendanceUC1);
            this.Controls.Add(this.a_1_EmployeesUC1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "A_1_MainS";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Attendance - Home";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button employeesButton;
        private System.Windows.Forms.Button markAttendanceButton;
        private System.Windows.Forms.Button reportsButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button masterButton;
        private System.Windows.Forms.Label label4;
        private A_1_EmployeesUC a_1_EmployeesUC1;
        private A_1_MarkAttendanceUC a_1_MarkAttendanceUC1;
    }
}