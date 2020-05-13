 namespace Factory_Inventory
{
    partial class M_VC_addBill
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
            this.saveButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cartonVoucherBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.carton_VoucherTableAdapter = new Factory_Inventory.FactoryInventoryDataSetTableAdapters.Carton_VoucherTableAdapter();
            this.loadDOButton = new System.Windows.Forms.Button();
            this.inputDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.billNumberTextboxTB = new System.Windows.Forms.TextBox();
            this.dynamicEditableLabel = new System.Windows.Forms.Label();
            this.qualityCB = new System.Windows.Forms.ComboBox();
            this.Quality = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.financialYearCB = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.billDateDTP = new System.Windows.Forms.DateTimePicker();
            this.Type = new System.Windows.Forms.Label();
            this.typeCB = new System.Windows.Forms.ComboBox();
            this.billWeightTB = new System.Windows.Forms.TextBox();
            this.billAmountTB = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.netDOAmountTB = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.netDOWeightTB = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
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
            this.dataGridView1.Location = new System.Drawing.Point(250, 2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(832, 605);
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
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(65, 450);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(111, 64);
            this.saveButton.TabIndex = 15;
            this.saveButton.Text = "Save Voucher";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Bill Date";
            // 
            // carton_VoucherTableAdapter
            // 
            this.carton_VoucherTableAdapter.ClearBeforeFill = true;
            // 
            // loadDOButton
            // 
            this.loadDOButton.Location = new System.Drawing.Point(21, 268);
            this.loadDOButton.Name = "loadDOButton";
            this.loadDOButton.Size = new System.Drawing.Size(200, 38);
            this.loadDOButton.TabIndex = 7;
            this.loadDOButton.Text = "Load DO Numbers";
            this.loadDOButton.UseVisualStyleBackColor = true;
            this.loadDOButton.Click += new System.EventHandler(this.loadCartonButton_Click);
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
            this.label7.Location = new System.Drawing.Point(21, 313);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 17);
            this.label7.TabIndex = 0;
            this.label7.Text = "Bill Number";
            // 
            // billNumberTextboxTB
            // 
            this.billNumberTextboxTB.Location = new System.Drawing.Point(21, 331);
            this.billNumberTextboxTB.Name = "billNumberTextboxTB";
            this.billNumberTextboxTB.Size = new System.Drawing.Size(178, 22);
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
            // qualityCB
            // 
            this.qualityCB.FormattingEnabled = true;
            this.qualityCB.Location = new System.Drawing.Point(24, 238);
            this.qualityCB.Name = "qualityCB";
            this.qualityCB.Size = new System.Drawing.Size(200, 24);
            this.qualityCB.TabIndex = 5;
            // 
            // Quality
            // 
            this.Quality.AutoSize = true;
            this.Quality.Location = new System.Drawing.Point(21, 218);
            this.Quality.Name = "Quality";
            this.Quality.Size = new System.Drawing.Size(52, 17);
            this.Quality.TabIndex = 0;
            this.Quality.Text = "Quality";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(21, 169);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(146, 17);
            this.label6.TabIndex = 0;
            this.label6.Text = "Financial Year of DOs";
            // 
            // financialYearCB
            // 
            this.financialYearCB.FormattingEnabled = true;
            this.financialYearCB.Location = new System.Drawing.Point(24, 188);
            this.financialYearCB.Name = "financialYearCB";
            this.financialYearCB.Size = new System.Drawing.Size(200, 24);
            this.financialYearCB.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 357);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "Bill Weight";
            // 
            // billDateDTP
            // 
            this.billDateDTP.Location = new System.Drawing.Point(24, 89);
            this.billDateDTP.Name = "billDateDTP";
            this.billDateDTP.Size = new System.Drawing.Size(200, 22);
            this.billDateDTP.TabIndex = 1;
            this.billDateDTP.TabStop = false;
            // 
            // Type
            // 
            this.Type.AutoSize = true;
            this.Type.Location = new System.Drawing.Point(21, 118);
            this.Type.Name = "Type";
            this.Type.Size = new System.Drawing.Size(40, 17);
            this.Type.TabIndex = 0;
            this.Type.Text = "Type";
            // 
            // typeCB
            // 
            this.typeCB.FormattingEnabled = true;
            this.typeCB.Location = new System.Drawing.Point(24, 138);
            this.typeCB.Name = "typeCB";
            this.typeCB.Size = new System.Drawing.Size(200, 24);
            this.typeCB.TabIndex = 3;
            // 
            // billWeightTB
            // 
            this.billWeightTB.Location = new System.Drawing.Point(21, 377);
            this.billWeightTB.Name = "billWeightTB";
            this.billWeightTB.Size = new System.Drawing.Size(178, 22);
            this.billWeightTB.TabIndex = 11;
            // 
            // billAmountTB
            // 
            this.billAmountTB.Location = new System.Drawing.Point(21, 422);
            this.billAmountTB.Name = "billAmountTB";
            this.billAmountTB.Size = new System.Drawing.Size(178, 22);
            this.billAmountTB.TabIndex = 13;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 402);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 17);
            this.label4.TabIndex = 0;
            this.label4.Text = "Bill Amount";
            // 
            // netDOAmountTB
            // 
            this.netDOAmountTB.Enabled = false;
            this.netDOAmountTB.Location = new System.Drawing.Point(20, 583);
            this.netDOAmountTB.Name = "netDOAmountTB";
            this.netDOAmountTB.Size = new System.Drawing.Size(179, 22);
            this.netDOAmountTB.TabIndex = 0;
            this.netDOAmountTB.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 563);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(107, 17);
            this.label5.TabIndex = 0;
            this.label5.Text = "Net DO Amount";
            // 
            // netDOWeightTB
            // 
            this.netDOWeightTB.Enabled = false;
            this.netDOWeightTB.Location = new System.Drawing.Point(20, 538);
            this.netDOWeightTB.Name = "netDOWeightTB";
            this.netDOWeightTB.Size = new System.Drawing.Size(179, 22);
            this.netDOWeightTB.TabIndex = 0;
            this.netDOWeightTB.TabStop = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(20, 518);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(103, 17);
            this.label8.TabIndex = 0;
            this.label8.Text = "Net DO Weight";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(201, 380);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(23, 17);
            this.label9.TabIndex = 0;
            this.label9.Text = "kg";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 586);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(16, 17);
            this.label10.TabIndex = 0;
            this.label10.Text = "₹";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(3, 425);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(16, 17);
            this.label11.TabIndex = 0;
            this.label11.Text = "₹";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(201, 541);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(23, 17);
            this.label12.TabIndex = 0;
            this.label12.Text = "kg";
            // 
            // M_VC_addBill
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1077, 611);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.netDOAmountTB);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.netDOWeightTB);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.billAmountTB);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.billWeightTB);
            this.Controls.Add(this.Type);
            this.Controls.Add(this.typeCB);
            this.Controls.Add(this.billDateDTP);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.financialYearCB);
            this.Controls.Add(this.qualityCB);
            this.Controls.Add(this.Quality);
            this.Controls.Add(this.dynamicEditableLabel);
            this.Controls.Add(this.billNumberTextboxTB);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.inputDate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.loadDOButton);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.label1);
            this.Name = "M_VC_addBill";
            this.Text = "M_VC_addBill";
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
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.BindingSource cartonVoucherBindingSource;
        private FactoryInventoryDataSetTableAdapters.Carton_VoucherTableAdapter carton_VoucherTableAdapter;
        private System.Windows.Forms.Button loadDOButton;
        private System.Windows.Forms.DateTimePicker inputDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox billNumberTextboxTB;
        private System.Windows.Forms.Label dynamicEditableLabel;
        private System.Windows.Forms.ComboBox qualityCB;
        private System.Windows.Forms.Label Quality;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox financialYearCB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker billDateDTP;
        private System.Windows.Forms.Label Type;
        private System.Windows.Forms.ComboBox typeCB;
        private System.Windows.Forms.TextBox billWeightTB;
        private System.Windows.Forms.TextBox billAmountTB;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox netDOAmountTB;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox netDOWeightTB;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
    }
}