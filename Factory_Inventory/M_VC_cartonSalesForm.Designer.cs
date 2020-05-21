namespace Factory_Inventory
{
    partial class M_VC_cartonSalesForm
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
            this.totalWeightLabel = new System.Windows.Forms.Label();
            this.saveButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.saleDateDTP = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.cartonVoucherBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.carton_VoucherTableAdapter = new Factory_Inventory.FactoryInventoryDataSetTableAdapters.Carton_VoucherTableAdapter();
            this.comboBox1CB = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.loadCartonButton = new System.Windows.Forms.Button();
            this.customerLabel = new System.Windows.Forms.Label();
            this.sellingPriceLabel = new System.Windows.Forms.Label();
            this.rateTextboxTB = new System.Windows.Forms.TextBox();
            this.comboBox3CB = new System.Windows.Forms.ComboBox();
            this.inputDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox4CB = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.typeCB = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.saleDONoTB = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.totalWeightTB = new System.Windows.Forms.TextBox();
            this.amountTB = new System.Windows.Forms.TextBox();
            this.Amount = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.deleteButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cartonVoucherBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridView1.Location = new System.Drawing.Point(250, -1);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(663, 745);
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
            this.comboBox2CB.Location = new System.Drawing.Point(24, 277);
            this.comboBox2CB.Name = "comboBox2CB";
            this.comboBox2CB.Size = new System.Drawing.Size(200, 24);
            this.comboBox2CB.TabIndex = 7;
            // 
            // totalWeightLabel
            // 
            this.totalWeightLabel.AutoSize = true;
            this.totalWeightLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalWeightLabel.Location = new System.Drawing.Point(19, 657);
            this.totalWeightLabel.Name = "totalWeightLabel";
            this.totalWeightLabel.Size = new System.Drawing.Size(92, 17);
            this.totalWeightLabel.TabIndex = 0;
            this.totalWeightLabel.Text = "Total Weight ";
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(66, 590);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(111, 54);
            this.saveButton.TabIndex = 15;
            this.saveButton.Text = "Save Voucher";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(21, 256);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 17);
            this.label5.TabIndex = 0;
            this.label5.Text = "Company";
            // 
            // saleDateDTP
            // 
            this.saleDateDTP.Location = new System.Drawing.Point(24, 70);
            this.saleDateDTP.Name = "saleDateDTP";
            this.saleDateDTP.Size = new System.Drawing.Size(200, 22);
            this.saleDateDTP.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Sale Date";
            // 
            // carton_VoucherTableAdapter
            // 
            this.carton_VoucherTableAdapter.ClearBeforeFill = true;
            // 
            // comboBox1CB
            // 
            this.comboBox1CB.FormattingEnabled = true;
            this.comboBox1CB.Location = new System.Drawing.Point(24, 228);
            this.comboBox1CB.Name = "comboBox1CB";
            this.comboBox1CB.Size = new System.Drawing.Size(200, 24);
            this.comboBox1CB.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 208);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "Quality";
            // 
            // loadCartonButton
            // 
            this.loadCartonButton.Location = new System.Drawing.Point(24, 314);
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
            this.customerLabel.Location = new System.Drawing.Point(21, 369);
            this.customerLabel.Name = "customerLabel";
            this.customerLabel.Size = new System.Drawing.Size(68, 17);
            this.customerLabel.TabIndex = 0;
            this.customerLabel.Text = "Customer";
            // 
            // sellingPriceLabel
            // 
            this.sellingPriceLabel.AutoSize = true;
            this.sellingPriceLabel.Location = new System.Drawing.Point(21, 418);
            this.sellingPriceLabel.Name = "sellingPriceLabel";
            this.sellingPriceLabel.Size = new System.Drawing.Size(82, 17);
            this.sellingPriceLabel.TabIndex = 0;
            this.sellingPriceLabel.Text = "Rate per kg";
            // 
            // rateTextboxTB
            // 
            this.rateTextboxTB.Location = new System.Drawing.Point(24, 438);
            this.rateTextboxTB.Name = "rateTextboxTB";
            this.rateTextboxTB.Size = new System.Drawing.Size(200, 22);
            this.rateTextboxTB.TabIndex = 13;
            this.rateTextboxTB.TextChanged += new System.EventHandler(this.rateTextboxTB_TextChanged);
            // 
            // comboBox3CB
            // 
            this.comboBox3CB.FormattingEnabled = true;
            this.comboBox3CB.Location = new System.Drawing.Point(24, 389);
            this.comboBox3CB.Name = "comboBox3CB";
            this.comboBox3CB.Size = new System.Drawing.Size(200, 24);
            this.comboBox3CB.TabIndex = 11;
            // 
            // inputDate
            // 
            this.inputDate.Enabled = false;
            this.inputDate.Location = new System.Drawing.Point(24, 25);
            this.inputDate.Name = "inputDate";
            this.inputDate.Size = new System.Drawing.Size(200, 22);
            this.inputDate.TabIndex = 42;
            this.inputDate.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 17);
            this.label3.TabIndex = 0;
            this.label3.Text = "Input Date";
            // 
            // comboBox4CB
            // 
            this.comboBox4CB.FormattingEnabled = true;
            this.comboBox4CB.Location = new System.Drawing.Point(24, 181);
            this.comboBox4CB.Name = "comboBox4CB";
            this.comboBox4CB.Size = new System.Drawing.Size(200, 24);
            this.comboBox4CB.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 144);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(168, 34);
            this.label4.TabIndex = 0;
            this.label4.Text = "Financial Year of Date of \nInward of Cartons";
            // 
            // typeCB
            // 
            this.typeCB.FormattingEnabled = true;
            this.typeCB.Location = new System.Drawing.Point(24, 115);
            this.typeCB.Name = "typeCB";
            this.typeCB.Size = new System.Drawing.Size(200, 24);
            this.typeCB.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(21, 95);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 17);
            this.label6.TabIndex = 0;
            this.label6.Text = "Type";
            // 
            // saleDONoTB
            // 
            this.saleDONoTB.Enabled = false;
            this.saleDONoTB.Location = new System.Drawing.Point(24, 483);
            this.saleDONoTB.Name = "saleDONoTB";
            this.saleDONoTB.Size = new System.Drawing.Size(200, 22);
            this.saleDONoTB.TabIndex = 0;
            this.saleDONoTB.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(21, 463);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(119, 17);
            this.label7.TabIndex = 0;
            this.label7.Text = "Sale D/O Number";
            // 
            // totalWeightTB
            // 
            this.totalWeightTB.Location = new System.Drawing.Point(22, 677);
            this.totalWeightTB.Name = "totalWeightTB";
            this.totalWeightTB.ReadOnly = true;
            this.totalWeightTB.Size = new System.Drawing.Size(182, 22);
            this.totalWeightTB.TabIndex = 0;
            this.totalWeightTB.TabStop = false;
            this.totalWeightTB.TextChanged += new System.EventHandler(this.totalWeightTB_TextChanged);
            // 
            // amountTB
            // 
            this.amountTB.Location = new System.Drawing.Point(22, 722);
            this.amountTB.Name = "amountTB";
            this.amountTB.ReadOnly = true;
            this.amountTB.Size = new System.Drawing.Size(182, 22);
            this.amountTB.TabIndex = 0;
            this.amountTB.TabStop = false;
            // 
            // Amount
            // 
            this.Amount.AutoSize = true;
            this.Amount.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Amount.Location = new System.Drawing.Point(19, 702);
            this.Amount.Name = "Amount";
            this.Amount.Size = new System.Drawing.Size(56, 17);
            this.Amount.TabIndex = 0;
            this.Amount.Text = "Amount";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(2, 724);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(20, 17);
            this.label8.TabIndex = 43;
            this.label8.Text = " ₹";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(204, 680);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(23, 17);
            this.label9.TabIndex = 0;
            this.label9.Text = "kg";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(22, 767);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(0, 25);
            this.label10.TabIndex = 44;
            // 
            // deleteButton
            // 
            this.deleteButton.Location = new System.Drawing.Point(66, 522);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(111, 53);
            this.deleteButton.TabIndex = 0;
            this.deleteButton.TabStop = false;
            this.deleteButton.Text = "Delete Voucher";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Visible = false;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // M_VC_cartonSalesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(911, 801);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.amountTB);
            this.Controls.Add(this.Amount);
            this.Controls.Add(this.totalWeightTB);
            this.Controls.Add(this.saleDONoTB);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.typeCB);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.comboBox4CB);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.inputDate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBox3CB);
            this.Controls.Add(this.rateTextboxTB);
            this.Controls.Add(this.sellingPriceLabel);
            this.Controls.Add(this.customerLabel);
            this.Controls.Add(this.loadCartonButton);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.comboBox2CB);
            this.Controls.Add(this.totalWeightLabel);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.saleDateDTP);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox1CB);
            this.Controls.Add(this.label2);
            this.Name = "M_VC_cartonSalesForm";
            this.Text = "Voucher - Carton Sale";
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
        private System.Windows.Forms.Label totalWeightLabel;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker saleDateDTP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.BindingSource cartonVoucherBindingSource;
        private FactoryInventoryDataSetTableAdapters.Carton_VoucherTableAdapter carton_VoucherTableAdapter;
        private System.Windows.Forms.ComboBox comboBox1CB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button loadCartonButton;
        private System.Windows.Forms.Label customerLabel;
        private System.Windows.Forms.Label sellingPriceLabel;
        private System.Windows.Forms.TextBox rateTextboxTB;
        private System.Windows.Forms.ComboBox comboBox3CB;
        private System.Windows.Forms.DateTimePicker inputDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBox4CB;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox typeCB;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox saleDONoTB;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox totalWeightTB;
        private System.Windows.Forms.TextBox amountTB;
        private System.Windows.Forms.Label Amount;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button deleteButton;
    }
}