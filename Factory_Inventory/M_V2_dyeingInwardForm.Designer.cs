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
            this.dynamicWeightLabel = new System.Windows.Forms.Label();
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
            this.billcheckBoxCK = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBox3CB = new System.Windows.Forms.ComboBox();
            this.billDateDTP = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cartonVoucherBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridView1.Location = new System.Drawing.Point(250, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(832, 581);
            this.dataGridView1.TabIndex = 24;
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
            this.contextMenuStrip1.Size = new System.Drawing.Size(123, 28);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(122, 24);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // dynamicWeightLabel
            // 
            this.dynamicWeightLabel.AutoSize = true;
            this.dynamicWeightLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dynamicWeightLabel.Location = new System.Drawing.Point(27, 552);
            this.dynamicWeightLabel.Name = "dynamicWeightLabel";
            this.dynamicWeightLabel.Size = new System.Drawing.Size(42, 25);
            this.dynamicWeightLabel.TabIndex = 0;
            this.dynamicWeightLabel.Text = "0.0";
            // 
            // totalWeightLabel
            // 
            this.totalWeightLabel.AutoSize = true;
            this.totalWeightLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalWeightLabel.Location = new System.Drawing.Point(27, 527);
            this.totalWeightLabel.Name = "totalWeightLabel";
            this.totalWeightLabel.Size = new System.Drawing.Size(117, 25);
            this.totalWeightLabel.TabIndex = 0;
            this.totalWeightLabel.Text = "Total Rate ";
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(65, 460);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(111, 64);
            this.saveButton.TabIndex = 15;
            this.saveButton.Text = "Save Voucher";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // inwardDateDTP
            // 
            this.inwardDateDTP.Location = new System.Drawing.Point(24, 97);
            this.inwardDateDTP.Name = "inwardDateDTP";
            this.inwardDateDTP.Size = new System.Drawing.Size(200, 22);
            this.inwardDateDTP.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Inward Date";
            // 
            // carton_VoucherTableAdapter
            // 
            this.carton_VoucherTableAdapter.ClearBeforeFill = true;
            // 
            // loadBatchButton
            // 
            this.loadBatchButton.Location = new System.Drawing.Point(21, 291);
            this.loadBatchButton.Name = "loadBatchButton";
            this.loadBatchButton.Size = new System.Drawing.Size(200, 38);
            this.loadBatchButton.TabIndex = 7;
            this.loadBatchButton.Text = "Load Batches";
            this.loadBatchButton.UseVisualStyleBackColor = true;
            this.loadBatchButton.Click += new System.EventHandler(this.loadCartonButton_Click);
            // 
            // inputDate
            // 
            this.inputDate.Enabled = false;
            this.inputDate.Location = new System.Drawing.Point(24, 42);
            this.inputDate.Name = "inputDate";
            this.inputDate.Size = new System.Drawing.Size(200, 22);
            this.inputDate.TabIndex = 0;
            this.inputDate.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 17);
            this.label3.TabIndex = 0;
            this.label3.Text = "Input Date";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(21, 342);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 17);
            this.label7.TabIndex = 0;
            this.label7.Text = "Bill Number";
            // 
            // billNumberTextboxTB
            // 
            this.billNumberTextboxTB.Enabled = false;
            this.billNumberTextboxTB.Location = new System.Drawing.Point(21, 362);
            this.billNumberTextboxTB.Name = "billNumberTextboxTB";
            this.billNumberTextboxTB.Size = new System.Drawing.Size(109, 22);
            this.billNumberTextboxTB.TabIndex = 9;
            // 
            // dynamicEditableLabel
            // 
            this.dynamicEditableLabel.AutoSize = true;
            this.dynamicEditableLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dynamicEditableLabel.Location = new System.Drawing.Point(42, 665);
            this.dynamicEditableLabel.Name = "dynamicEditableLabel";
            this.dynamicEditableLabel.Size = new System.Drawing.Size(0, 25);
            this.dynamicEditableLabel.TabIndex = 43;
            // 
            // dyeingCompanyCB
            // 
            this.dyeingCompanyCB.FormattingEnabled = true;
            this.dyeingCompanyCB.Location = new System.Drawing.Point(24, 252);
            this.dyeingCompanyCB.Name = "dyeingCompanyCB";
            this.dyeingCompanyCB.Size = new System.Drawing.Size(200, 24);
            this.dyeingCompanyCB.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 232);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(115, 17);
            this.label4.TabIndex = 0;
            this.label4.Text = "Dyeing Company";
            // 
            // billcheckBoxCK
            // 
            this.billcheckBoxCK.AutoSize = true;
            this.billcheckBoxCK.Checked = true;
            this.billcheckBoxCK.CheckState = System.Windows.Forms.CheckState.Checked;
            this.billcheckBoxCK.Location = new System.Drawing.Point(157, 362);
            this.billcheckBoxCK.Name = "billcheckBoxCK";
            this.billcheckBoxCK.Size = new System.Drawing.Size(64, 21);
            this.billcheckBoxCK.TabIndex = 11;
            this.billcheckBoxCK.Text = "None";
            this.billcheckBoxCK.UseVisualStyleBackColor = true;
            this.billcheckBoxCK.CheckStateChanged += new System.EventHandler(this.billcheckBox_CheckStateChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(21, 167);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(206, 34);
            this.label6.TabIndex = 0;
            this.label6.Text = "Financial Year of Production of \nInward of Batches";
            // 
            // comboBox3CB
            // 
            this.comboBox3CB.FormattingEnabled = true;
            this.comboBox3CB.Location = new System.Drawing.Point(24, 203);
            this.comboBox3CB.Name = "comboBox3CB";
            this.comboBox3CB.Size = new System.Drawing.Size(200, 24);
            this.comboBox3CB.TabIndex = 3;
            // 
            // billDateDTP
            // 
            this.billDateDTP.Enabled = false;
            this.billDateDTP.Location = new System.Drawing.Point(21, 407);
            this.billDateDTP.Name = "billDateDTP";
            this.billDateDTP.Size = new System.Drawing.Size(200, 22);
            this.billDateDTP.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 387);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "Bill Date";
            // 
            // M_V2_dyeingInwardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1077, 581);
            this.Controls.Add(this.billDateDTP);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.comboBox3CB);
            this.Controls.Add(this.billcheckBoxCK);
            this.Controls.Add(this.dyeingCompanyCB);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dynamicEditableLabel);
            this.Controls.Add(this.billNumberTextboxTB);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.inputDate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.loadBatchButton);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.dynamicWeightLabel);
            this.Controls.Add(this.totalWeightLabel);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.inwardDateDTP);
            this.Controls.Add(this.label1);
            this.Name = "M_V2_dyeingInwardForm";
            this.Text = "M_V2_dyeingInwardForm";
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
        private System.Windows.Forms.Label dynamicWeightLabel;
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
        private System.Windows.Forms.CheckBox billcheckBoxCK;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBox3CB;
        private System.Windows.Forms.DateTimePicker billDateDTP;
        private System.Windows.Forms.Label label2;
    }
}