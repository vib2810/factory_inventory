namespace Factory_Inventory
{
    partial class A_1_MarkAttendanceUC
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
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.markButton = new System.Windows.Forms.Button();
            this.dateTimePickerDTP = new System.Windows.Forms.DateTimePicker();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // userLabel
            // 
            this.userLabel.AutoSize = true;
            this.userLabel.Location = new System.Drawing.Point(17, 13);
            this.userLabel.Name = "userLabel";
            this.userLabel.Size = new System.Drawing.Size(42, 17);
            this.userLabel.TabIndex = 0;
            this.userLabel.Text = " Date";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(20, 41);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridView1.Size = new System.Drawing.Size(648, 391);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.TabStop = false;
            this.dataGridView1.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dataGridView1_CellBeginEdit);
            this.dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
            this.dataGridView1.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGridView1_RowPostPaint);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // markButton
            // 
            this.markButton.Location = new System.Drawing.Point(559, 443);
            this.markButton.Name = "markButton";
            this.markButton.Size = new System.Drawing.Size(109, 34);
            this.markButton.TabIndex = 3;
            this.markButton.Text = "Mark";
            this.markButton.UseVisualStyleBackColor = true;
            this.markButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // dateTimePickerDTP
            // 
            this.dateTimePickerDTP.Location = new System.Drawing.Point(65, 10);
            this.dateTimePickerDTP.Name = "dateTimePickerDTP";
            this.dateTimePickerDTP.Size = new System.Drawing.Size(200, 22);
            this.dateTimePickerDTP.TabIndex = 1;
            this.dateTimePickerDTP.ValueChanged += new System.EventHandler(this.dateTimePickerDTP_ValueChanged);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(20, 438);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(259, 36);
            this.panel1.TabIndex = 4;
            // 
            // A_1_MarkAttendanceUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dateTimePickerDTP);
            this.Controls.Add(this.markButton);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.userLabel);
            this.Name = "A_1_MarkAttendanceUC";
            this.Size = new System.Drawing.Size(671, 477);
            this.Load += new System.EventHandler(this.A_1_EmployeesUC_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label userLabel;
        public System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Button markButton;
        private System.Windows.Forms.DateTimePicker dateTimePickerDTP;
        private System.Windows.Forms.Panel panel1;
    }
}
