namespace Factory_Inventory
{
    partial class M_V2_dyeingIssueForm
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
            this.comboBox2CB = new System.Windows.Forms.ComboBox();
            this.dynamicWeightLabel = new System.Windows.Forms.Label();
            this.totalWeightLabel = new System.Windows.Forms.Label();
            this.saveButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.issueDateDTP = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.carton_VoucherTableAdapter = new Factory_Inventory.FactoryInventoryDataSetTableAdapters.Carton_VoucherTableAdapter();
            this.comboBox1CB = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.loadCartonButton = new System.Windows.Forms.Button();
            this.inputDateDTP = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox3CB = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBox4CB = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.batchNumberTextboxTB = new System.Windows.Forms.TextBox();
            this.dynamicEditableLabel = new System.Windows.Forms.Label();
            this.cartonVoucherBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label8 = new System.Windows.Forms.Label();
            this.rateTextBoxTB = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cartonVoucherBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridView1.Location = new System.Drawing.Point(251, 2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(663, 653);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.TabStop = false;
            this.dataGridView1.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dataGridView1_CellBeginEdit);
            this.dataGridView1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEndEdit);
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
            // comboBox2CB
            // 
            this.comboBox2CB.FormattingEnabled = true;
            this.comboBox2CB.Location = new System.Drawing.Point(24, 205);
            this.comboBox2CB.Name = "comboBox2CB";
            this.comboBox2CB.Size = new System.Drawing.Size(200, 24);
            this.comboBox2CB.TabIndex = 5;
            // 
            // dynamicWeightLabel
            // 
            this.dynamicWeightLabel.AutoSize = true;
            this.dynamicWeightLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dynamicWeightLabel.Location = new System.Drawing.Point(27, 600);
            this.dynamicWeightLabel.Name = "dynamicWeightLabel";
            this.dynamicWeightLabel.Size = new System.Drawing.Size(42, 25);
            this.dynamicWeightLabel.TabIndex = 0;
            this.dynamicWeightLabel.Text = "0.0";
            // 
            // totalWeightLabel
            // 
            this.totalWeightLabel.AutoSize = true;
            this.totalWeightLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalWeightLabel.Location = new System.Drawing.Point(27, 575);
            this.totalWeightLabel.Name = "totalWeightLabel";
            this.totalWeightLabel.Size = new System.Drawing.Size(141, 25);
            this.totalWeightLabel.TabIndex = 0;
            this.totalWeightLabel.Text = "Total Weight ";
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(70, 493);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(111, 64);
            this.saveButton.TabIndex = 17;
            this.saveButton.Text = "Save Voucher";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(21, 185);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 17);
            this.label5.TabIndex = 0;
            this.label5.Text = "Company";
            // 
            // issueDateDTP
            // 
            this.issueDateDTP.Location = new System.Drawing.Point(24, 93);
            this.issueDateDTP.Name = "issueDateDTP";
            this.issueDateDTP.Size = new System.Drawing.Size(200, 22);
            this.issueDateDTP.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Issue Date";
            // 
            // carton_VoucherTableAdapter
            // 
            this.carton_VoucherTableAdapter.ClearBeforeFill = true;
            // 
            // comboBox1CB
            // 
            this.comboBox1CB.FormattingEnabled = true;
            this.comboBox1CB.Location = new System.Drawing.Point(24, 148);
            this.comboBox1CB.Name = "comboBox1CB";
            this.comboBox1CB.Size = new System.Drawing.Size(200, 24);
            this.comboBox1CB.TabIndex = 3;
            this.comboBox1CB.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 128);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "Quality";
            // 
            // loadCartonButton
            // 
            this.loadCartonButton.Location = new System.Drawing.Point(24, 235);
            this.loadCartonButton.Name = "loadCartonButton";
            this.loadCartonButton.Size = new System.Drawing.Size(200, 38);
            this.loadCartonButton.TabIndex = 7;
            this.loadCartonButton.Text = "Load Trays";
            this.loadCartonButton.UseVisualStyleBackColor = true;
            this.loadCartonButton.Click += new System.EventHandler(this.loadCartonButton_Click);
            // 
            // inputDateDTP
            // 
            this.inputDateDTP.Enabled = false;
            this.inputDateDTP.Location = new System.Drawing.Point(24, 42);
            this.inputDateDTP.Name = "inputDateDTP";
            this.inputDateDTP.Size = new System.Drawing.Size(200, 22);
            this.inputDateDTP.TabIndex = 0;
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
            // comboBox3CB
            // 
            this.comboBox3CB.FormattingEnabled = true;
            this.comboBox3CB.Location = new System.Drawing.Point(24, 366);
            this.comboBox3CB.Name = "comboBox3CB";
            this.comboBox3CB.Size = new System.Drawing.Size(200, 24);
            this.comboBox3CB.TabIndex = 13;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 346);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(115, 17);
            this.label4.TabIndex = 0;
            this.label4.Text = "Dyeing Company";
            // 
            // comboBox4CB
            // 
            this.comboBox4CB.FormattingEnabled = true;
            this.comboBox4CB.Location = new System.Drawing.Point(24, 309);
            this.comboBox4CB.Name = "comboBox4CB";
            this.comboBox4CB.Size = new System.Drawing.Size(120, 24);
            this.comboBox4CB.TabIndex = 9;
            this.comboBox4CB.SelectedIndexChanged += new System.EventHandler(this.comboBox4_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(21, 289);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 17);
            this.label6.TabIndex = 0;
            this.label6.Text = "Colour";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(24, 406);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(98, 17);
            this.label7.TabIndex = 0;
            this.label7.Text = "Batch Number";
            // 
            // batchNumberTextboxTB
            // 
            this.batchNumberTextboxTB.Enabled = false;
            this.batchNumberTextboxTB.Location = new System.Drawing.Point(27, 427);
            this.batchNumberTextboxTB.Name = "batchNumberTextboxTB";
            this.batchNumberTextboxTB.Size = new System.Drawing.Size(197, 22);
            this.batchNumberTextboxTB.TabIndex = 0;
            this.batchNumberTextboxTB.TabStop = false;
            // 
            // dynamicEditableLabel
            // 
            this.dynamicEditableLabel.AutoSize = true;
            this.dynamicEditableLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dynamicEditableLabel.Location = new System.Drawing.Point(42, 665);
            this.dynamicEditableLabel.Name = "dynamicEditableLabel";
            this.dynamicEditableLabel.Size = new System.Drawing.Size(0, 25);
            this.dynamicEditableLabel.TabIndex = 0;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(150, 289);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(38, 17);
            this.label8.TabIndex = 0;
            this.label8.Text = "Rate";
            // 
            // rateTextBoxTB
            // 
            this.rateTextBoxTB.Location = new System.Drawing.Point(153, 311);
            this.rateTextBoxTB.Name = "rateTextBoxTB";
            this.rateTextBoxTB.Size = new System.Drawing.Size(71, 22);
            this.rateTextBoxTB.TabIndex = 11;
            this.rateTextBoxTB.TabStop = false;
            // 
            // M_V2_dyeingIssueForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(911, 707);
            this.Controls.Add(this.rateTextBoxTB);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.dynamicEditableLabel);
            this.Controls.Add(this.batchNumberTextboxTB);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.comboBox3CB);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.comboBox4CB);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.inputDateDTP);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.loadCartonButton);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.comboBox2CB);
            this.Controls.Add(this.dynamicWeightLabel);
            this.Controls.Add(this.totalWeightLabel);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.issueDateDTP);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox1CB);
            this.Controls.Add(this.label2);
            this.Name = "M_V2_dyeingIssueForm";
            this.Text = "M_V2_dyeingIssueForm";
            this.Load += new System.EventHandler(this.M_V2_dyeingIssueForm_Load);
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
        private System.Windows.Forms.ComboBox comboBox2CB;
        private System.Windows.Forms.Label dynamicWeightLabel;
        private System.Windows.Forms.Label totalWeightLabel;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker issueDateDTP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.BindingSource cartonVoucherBindingSource;
        private FactoryInventoryDataSetTableAdapters.Carton_VoucherTableAdapter carton_VoucherTableAdapter;
        private System.Windows.Forms.ComboBox comboBox1CB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button loadCartonButton;
        private System.Windows.Forms.DateTimePicker inputDateDTP;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBox3CB;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBox4CB;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox batchNumberTextboxTB;
        private System.Windows.Forms.Label dynamicEditableLabel;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox rateTextBoxTB;
    }
}