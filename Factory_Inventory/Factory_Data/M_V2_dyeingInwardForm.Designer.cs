 namespace Factory_Inventory
{
    partial class M_V2_dyeingInwardForm
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
            this.components = new System.ComponentModel.Container();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dynamicRateLabel = new System.Windows.Forms.Label();
            this.totalWeightLabel = new System.Windows.Forms.Label();
            this.saveButton = new System.Windows.Forms.Button();
            this.inwardDateDTP = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.cartonVoucherBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.carton_VoucherTableAdapter = new Factory_Inventory.FactoryInventoryDataSetTableAdapters.Carton_VoucherTableAdapter();
            this.loadBatchButton = new System.Windows.Forms.Button();
            this.inputDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.billNumberTextboxTB = new System.Windows.Forms.TextBox();
            this.dynamicEditableLabel = new System.Windows.Forms.Label();
            this.dyeingCompanyCB = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBox3CB = new System.Windows.Forms.ComboBox();
            this.billDateDTP = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.deleteButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.dynamicWeightLabel = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cartonVoucherBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridView1.Location = new System.Drawing.Point(188, 0);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(624, 437);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.TabStop = false;
            this.dataGridView1.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseDown);
            this.dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
            this.dataGridView1.CurrentCellDirtyStateChanged += new System.EventHandler(this.dataGridView1_CurrentCellDirtyStateChanged);
            this.dataGridView1.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridView1_EditingControlShowing);
            this.dataGridView1.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGridView1_RowPostPaint_1);
            this.dataGridView1.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dataGridView1_RowPrePaint);
            this.dataGridView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView1_KeyDown);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(108, 26);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // dynamicRateLabel
            // 
            this.dynamicRateLabel.AutoSize = true;
            this.dynamicRateLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dynamicRateLabel.Location = new System.Drawing.Point(112, 312);
            this.dynamicRateLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.dynamicRateLabel.Name = "dynamicRateLabel";
            this.dynamicRateLabel.Size = new System.Drawing.Size(28, 16);
            this.dynamicRateLabel.TabIndex = 0;
            this.dynamicRateLabel.Text = "0.0";
            // 
            // totalWeightLabel
            // 
            this.totalWeightLabel.AutoSize = true;
            this.totalWeightLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalWeightLabel.Location = new System.Drawing.Point(15, 312);
            this.totalWeightLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.totalWeightLabel.Name = "totalWeightLabel";
            this.totalWeightLabel.Size = new System.Drawing.Size(85, 16);
            this.totalWeightLabel.TabIndex = 0;
            this.totalWeightLabel.Text = "Total Rate ";
            // 
            // saveButton
            // 
            this.saveButton.Enabled = false;
            this.saveButton.Location = new System.Drawing.Point(49, 426);
            this.saveButton.Margin = new System.Windows.Forms.Padding(2);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(83, 42);
            this.saveButton.TabIndex = 15;
            this.saveButton.Text = "Save Voucher";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // inwardDateDTP
            // 
            this.inwardDateDTP.Location = new System.Drawing.Point(18, 79);
            this.inwardDateDTP.Margin = new System.Windows.Forms.Padding(2);
            this.inwardDateDTP.Name = "inwardDateDTP";
            this.inwardDateDTP.Size = new System.Drawing.Size(151, 20);
            this.inwardDateDTP.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 63);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Inward Date";
            // 
            // carton_VoucherTableAdapter
            // 
            this.carton_VoucherTableAdapter.ClearBeforeFill = true;
            // 
            // loadBatchButton
            // 
            this.loadBatchButton.Location = new System.Drawing.Point(16, 273);
            this.loadBatchButton.Margin = new System.Windows.Forms.Padding(2);
            this.loadBatchButton.Name = "loadBatchButton";
            this.loadBatchButton.Size = new System.Drawing.Size(150, 31);
            this.loadBatchButton.TabIndex = 7;
            this.loadBatchButton.Text = "Load Batches";
            this.loadBatchButton.UseVisualStyleBackColor = true;
            this.loadBatchButton.Click += new System.EventHandler(this.loadCartonButton_Click);
            // 
            // inputDate
            // 
            this.inputDate.Enabled = false;
            this.inputDate.Location = new System.Drawing.Point(18, 34);
            this.inputDate.Margin = new System.Windows.Forms.Padding(2);
            this.inputDate.Name = "inputDate";
            this.inputDate.Size = new System.Drawing.Size(151, 20);
            this.inputDate.TabIndex = 0;
            this.inputDate.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 18);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Input Date";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(472, 310);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(60, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Bill Number";
            this.label7.Visible = false;
            // 
            // billNumberTextboxTB
            // 
            this.billNumberTextboxTB.Location = new System.Drawing.Point(475, 370);
            this.billNumberTextboxTB.Margin = new System.Windows.Forms.Padding(2);
            this.billNumberTextboxTB.Name = "billNumberTextboxTB";
            this.billNumberTextboxTB.ReadOnly = true;
            this.billNumberTextboxTB.Size = new System.Drawing.Size(83, 20);
            this.billNumberTextboxTB.TabIndex = 0;
            this.billNumberTextboxTB.TabStop = false;
            this.billNumberTextboxTB.Visible = false;
            // 
            // dynamicEditableLabel
            // 
            this.dynamicEditableLabel.AutoSize = true;
            this.dynamicEditableLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dynamicEditableLabel.Location = new System.Drawing.Point(32, 540);
            this.dynamicEditableLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.dynamicEditableLabel.Name = "dynamicEditableLabel";
            this.dynamicEditableLabel.Size = new System.Drawing.Size(0, 20);
            this.dynamicEditableLabel.TabIndex = 43;
            // 
            // dyeingCompanyCB
            // 
            this.dyeingCompanyCB.FormattingEnabled = true;
            this.dyeingCompanyCB.Location = new System.Drawing.Point(18, 241);
            this.dyeingCompanyCB.Margin = new System.Windows.Forms.Padding(2);
            this.dyeingCompanyCB.Name = "dyeingCompanyCB";
            this.dyeingCompanyCB.Size = new System.Drawing.Size(151, 21);
            this.dyeingCompanyCB.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 225);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Dyeing Company";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 172);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(155, 26);
            this.label6.TabIndex = 0;
            this.label6.Text = "Financial Year of Production of \nInward of Batches";
            // 
            // comboBox3CB
            // 
            this.comboBox3CB.FormattingEnabled = true;
            this.comboBox3CB.Location = new System.Drawing.Point(18, 202);
            this.comboBox3CB.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox3CB.Name = "comboBox3CB";
            this.comboBox3CB.Size = new System.Drawing.Size(151, 21);
            this.comboBox3CB.TabIndex = 4;
            // 
            // billDateDTP
            // 
            this.billDateDTP.Enabled = false;
            this.billDateDTP.Location = new System.Drawing.Point(475, 347);
            this.billDateDTP.Margin = new System.Windows.Forms.Padding(2);
            this.billDateDTP.Name = "billDateDTP";
            this.billDateDTP.Size = new System.Drawing.Size(151, 20);
            this.billDateDTP.TabIndex = 0;
            this.billDateDTP.TabStop = false;
            this.billDateDTP.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(472, 331);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Bill Date";
            this.label2.Visible = false;
            // 
            // deleteButton
            // 
            this.deleteButton.Location = new System.Drawing.Point(49, 375);
            this.deleteButton.Margin = new System.Windows.Forms.Padding(2);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(83, 38);
            this.deleteButton.TabIndex = 44;
            this.deleteButton.TabStop = false;
            this.deleteButton.Text = "Delete Voucher";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Visible = false;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(150, 468);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(0, 17);
            this.label5.TabIndex = 0;
            // 
            // dynamicWeightLabel
            // 
            this.dynamicWeightLabel.AutoSize = true;
            this.dynamicWeightLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dynamicWeightLabel.Location = new System.Drawing.Point(112, 335);
            this.dynamicWeightLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.dynamicWeightLabel.Name = "dynamicWeightLabel";
            this.dynamicWeightLabel.Size = new System.Drawing.Size(28, 16);
            this.dynamicWeightLabel.TabIndex = 45;
            this.dynamicWeightLabel.Text = "0.0";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(15, 335);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(100, 16);
            this.label9.TabIndex = 46;
            this.label9.Text = "Total Weight ";
            // 
            // M_V2_dyeingInwardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(808, 490);
            this.Controls.Add(this.dynamicWeightLabel);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.billDateDTP);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.comboBox3CB);
            this.Controls.Add(this.dyeingCompanyCB);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dynamicEditableLabel);
            this.Controls.Add(this.billNumberTextboxTB);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.inputDate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.loadBatchButton);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.dynamicRateLabel);
            this.Controls.Add(this.totalWeightLabel);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.inwardDateDTP);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "M_V2_dyeingInwardForm";
            this.Text = "Voucher - Dyeing Inward";
            this.Load += new System.EventHandler(this.M_V2_dyeingInwardForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cartonVoucherBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.Label dynamicRateLabel;
        private System.Windows.Forms.Label totalWeightLabel;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.DateTimePicker inwardDateDTP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.BindingSource cartonVoucherBindingSource;
        private FactoryInventoryDataSetTableAdapters.Carton_VoucherTableAdapter carton_VoucherTableAdapter;
        private System.Windows.Forms.Button loadBatchButton;
        private System.Windows.Forms.DateTimePicker inputDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox billNumberTextboxTB;
        private System.Windows.Forms.Label dynamicEditableLabel;
        private System.Windows.Forms.ComboBox dyeingCompanyCB;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBox3CB;
        private System.Windows.Forms.DateTimePicker billDateDTP;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label dynamicWeightLabel;
        private System.Windows.Forms.Label label9;
    }
}