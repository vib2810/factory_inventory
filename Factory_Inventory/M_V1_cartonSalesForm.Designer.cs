namespace Factory_Inventory
{
    partial class M_V1_cartonSalesForm
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
            this.cartonVoucherBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.carton_VoucherTableAdapter = new Factory_Inventory.FactoryInventoryDataSetTableAdapters.Carton_VoucherTableAdapter();
            this.comboBox1CB = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.loadCartonButton = new System.Windows.Forms.Button();
            this.customerLabel = new System.Windows.Forms.Label();
            this.sellingPriceLabel = new System.Windows.Forms.Label();
            this.sellingPriceTextboxTB = new System.Windows.Forms.TextBox();
            this.comboBox3CB = new System.Windows.Forms.ComboBox();
            this.inputDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox4CB = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cartonVoucherBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridView1.Location = new System.Drawing.Point(250, -1);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(663, 676);
            this.dataGridView1.TabIndex = 24;
            this.dataGridView1.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dataGridView1_CellBeginEdit);
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
            this.comboBox2CB.Location = new System.Drawing.Point(24, 266);
            this.comboBox2CB.Name = "comboBox2CB";
            this.comboBox2CB.Size = new System.Drawing.Size(200, 24);
            this.comboBox2CB.TabIndex = 7;
            // 
            // dynamicWeightLabel
            // 
            this.dynamicWeightLabel.AutoSize = true;
            this.dynamicWeightLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dynamicWeightLabel.Location = new System.Drawing.Point(19, 642);
            this.dynamicWeightLabel.Name = "dynamicWeightLabel";
            this.dynamicWeightLabel.Size = new System.Drawing.Size(42, 25);
            this.dynamicWeightLabel.TabIndex = 0;
            this.dynamicWeightLabel.Text = "0.0";
            // 
            // totalWeightLabel
            // 
            this.totalWeightLabel.AutoSize = true;
            this.totalWeightLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalWeightLabel.Location = new System.Drawing.Point(19, 621);
            this.totalWeightLabel.Name = "totalWeightLabel";
            this.totalWeightLabel.Size = new System.Drawing.Size(141, 25);
            this.totalWeightLabel.TabIndex = 0;
            this.totalWeightLabel.Text = "Total Weight ";
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(67, 518);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(111, 64);
            this.saveButton.TabIndex = 15;
            this.saveButton.Text = "Save Voucher";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(21, 246);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 17);
            this.label5.TabIndex = 0;
            this.label5.Text = "Company";
            // 
            // issueDateDTP
            // 
            this.issueDateDTP.Location = new System.Drawing.Point(24, 92);
            this.issueDateDTP.Name = "issueDateDTP";
            this.issueDateDTP.Size = new System.Drawing.Size(200, 22);
            this.issueDateDTP.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 72);
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
            this.comboBox1CB.Location = new System.Drawing.Point(24, 214);
            this.comboBox1CB.Name = "comboBox1CB";
            this.comboBox1CB.Size = new System.Drawing.Size(200, 24);
            this.comboBox1CB.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 194);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "Quality";
            // 
            // loadCartonButton
            // 
            this.loadCartonButton.Location = new System.Drawing.Point(24, 309);
            this.loadCartonButton.Name = "loadCartonButton";
            this.loadCartonButton.Size = new System.Drawing.Size(200, 38);
            this.loadCartonButton.TabIndex = 9;
            this.loadCartonButton.Text = "Load Carton Numbers";
            this.loadCartonButton.UseVisualStyleBackColor = true;
            this.loadCartonButton.Click += new System.EventHandler(this.loadCartonButton_Click);
            // 
            // customerLabel
            // 
            this.customerLabel.AutoSize = true;
            this.customerLabel.Location = new System.Drawing.Point(21, 385);
            this.customerLabel.Name = "customerLabel";
            this.customerLabel.Size = new System.Drawing.Size(68, 17);
            this.customerLabel.TabIndex = 0;
            this.customerLabel.Text = "Customer";
            // 
            // sellingPriceLabel
            // 
            this.sellingPriceLabel.AutoSize = true;
            this.sellingPriceLabel.Location = new System.Drawing.Point(21, 455);
            this.sellingPriceLabel.Name = "sellingPriceLabel";
            this.sellingPriceLabel.Size = new System.Drawing.Size(129, 17);
            this.sellingPriceLabel.TabIndex = 0;
            this.sellingPriceLabel.Text = "Selling price per kg";
            // 
            // sellingPriceTextboxTB
            // 
            this.sellingPriceTextboxTB.Location = new System.Drawing.Point(24, 475);
            this.sellingPriceTextboxTB.Name = "sellingPriceTextboxTB";
            this.sellingPriceTextboxTB.Size = new System.Drawing.Size(200, 22);
            this.sellingPriceTextboxTB.TabIndex = 13;
            // 
            // comboBox3CB
            // 
            this.comboBox3CB.FormattingEnabled = true;
            this.comboBox3CB.Location = new System.Drawing.Point(24, 405);
            this.comboBox3CB.Name = "comboBox3CB";
            this.comboBox3CB.Size = new System.Drawing.Size(200, 24);
            this.comboBox3CB.TabIndex = 11;
            // 
            // inputDate
            // 
            this.inputDate.Enabled = false;
            this.inputDate.Location = new System.Drawing.Point(24, 40);
            this.inputDate.Name = "inputDate";
            this.inputDate.Size = new System.Drawing.Size(200, 22);
            this.inputDate.TabIndex = 42;
            this.inputDate.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 17);
            this.label3.TabIndex = 0;
            this.label3.Text = "Input Date";
            // 
            // comboBox4CB
            // 
            this.comboBox4CB.FormattingEnabled = true;
            this.comboBox4CB.Location = new System.Drawing.Point(24, 161);
            this.comboBox4CB.Name = "comboBox4CB";
            this.comboBox4CB.Size = new System.Drawing.Size(200, 24);
            this.comboBox4CB.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 125);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(168, 34);
            this.label4.TabIndex = 0;
            this.label4.Text = "Financial Year of Date of \nInward of Cartons";
            // 
            // M_V1_cartonSalesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(911, 679);
            this.Controls.Add(this.comboBox4CB);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.inputDate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBox3CB);
            this.Controls.Add(this.sellingPriceTextboxTB);
            this.Controls.Add(this.sellingPriceLabel);
            this.Controls.Add(this.customerLabel);
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
            this.Name = "M_V1_cartonSalesForm";
            this.Text = "M_V1_salesForm";
            this.Load += new System.EventHandler(this.M_V1_cartonSalesForm_Load);
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
        private System.Windows.Forms.Label customerLabel;
        private System.Windows.Forms.Label sellingPriceLabel;
        private System.Windows.Forms.TextBox sellingPriceTextboxTB;
        private System.Windows.Forms.ComboBox comboBox3CB;
        private System.Windows.Forms.DateTimePicker inputDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBox4CB;
        private System.Windows.Forms.Label label4;
    }
}