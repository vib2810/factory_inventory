namespace Factory_Inventory
{
    partial class T_V1_cartonInwardForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.inputDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.billDateDTP = new System.Windows.Forms.DateTimePicker();
            this.billNumberTextboxTB = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.saveButton = new System.Windows.Forms.Button();
            this.totalWeightLabel = new System.Windows.Forms.Label();
            this.comboBox2CB = new System.Windows.Forms.ComboBox();
            this.costLabel = new System.Windows.Forms.Label();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.Quality = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Colour = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Weight = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Total_Price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.factoryInventoryDataSet = new Factory_Inventory.FactoryInventoryDataSet();
            this.cartonVoucherBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.carton_VoucherTableAdapter = new Factory_Inventory.FactoryInventoryDataSetTableAdapters.Carton_VoucherTableAdapter();
            this.lockCartonsCK = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.deleteButton = new System.Windows.Forms.Button();
            this.dynamicWeightTB = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.typeCB = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.narrationTB = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.factoryInventoryDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cartonVoucherBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridView1.Location = new System.Drawing.Point(430, 0);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(605, 579);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.TabStop = false;
            this.dataGridView1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEndEdit);
            this.dataGridView1.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseDown);
            this.dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
            this.dataGridView1.CurrentCellDirtyStateChanged += new System.EventHandler(this.dataGridView1_CurrentCellDirtyStateChanged);
            this.dataGridView1.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridView1_EditingControlShowing);
            this.dataGridView1.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGridView1_RowPostPaint);
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 15);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Input Date";
            // 
            // inputDate
            // 
            this.inputDate.Enabled = false;
            this.inputDate.Location = new System.Drawing.Point(9, 32);
            this.inputDate.Margin = new System.Windows.Forms.Padding(2);
            this.inputDate.Name = "inputDate";
            this.inputDate.Size = new System.Drawing.Size(151, 20);
            this.inputDate.TabIndex = 0;
            this.inputDate.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 98);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Bill Date";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 141);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Bill Number";
            // 
            // billDateDTP
            // 
            this.billDateDTP.CustomFormat = " ";
            this.billDateDTP.Location = new System.Drawing.Point(9, 114);
            this.billDateDTP.Margin = new System.Windows.Forms.Padding(2);
            this.billDateDTP.Name = "billDateDTP";
            this.billDateDTP.Size = new System.Drawing.Size(151, 20);
            this.billDateDTP.TabIndex = 1;
            this.billDateDTP.ValueChanged += new System.EventHandler(this.billDate_ValueChanged);
            // 
            // billNumberTextboxTB
            // 
            this.billNumberTextboxTB.Location = new System.Drawing.Point(9, 157);
            this.billNumberTextboxTB.Margin = new System.Windows.Forms.Padding(2);
            this.billNumberTextboxTB.Name = "billNumberTextboxTB";
            this.billNumberTextboxTB.Size = new System.Drawing.Size(151, 20);
            this.billNumberTextboxTB.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 186);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Company";
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(79, 537);
            this.saveButton.Margin = new System.Windows.Forms.Padding(2);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(108, 46);
            this.saveButton.TabIndex = 9;
            this.saveButton.Text = "Save Voucher";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // totalWeightLabel
            // 
            this.totalWeightLabel.AutoSize = true;
            this.totalWeightLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalWeightLabel.Location = new System.Drawing.Point(76, 474);
            this.totalWeightLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.totalWeightLabel.Name = "totalWeightLabel";
            this.totalWeightLabel.Size = new System.Drawing.Size(105, 17);
            this.totalWeightLabel.TabIndex = 0;
            this.totalWeightLabel.Text = "Total Weight ";
            // 
            // comboBox2CB
            // 
            this.comboBox2CB.FormattingEnabled = true;
            this.comboBox2CB.Location = new System.Drawing.Point(9, 202);
            this.comboBox2CB.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox2CB.Name = "comboBox2CB";
            this.comboBox2CB.Size = new System.Drawing.Size(151, 21);
            this.comboBox2CB.TabIndex = 5;
            // 
            // costLabel
            // 
            this.costLabel.AutoSize = true;
            this.costLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.costLabel.Location = new System.Drawing.Point(6, 230);
            this.costLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.costLabel.Name = "costLabel";
            this.costLabel.Size = new System.Drawing.Size(80, 17);
            this.costLabel.TabIndex = 0;
            this.costLabel.Text = "Cost per kg";
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.AllowUserToResizeColumns = false;
            this.dataGridView2.AllowUserToResizeRows = false;
            this.dataGridView2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Quality,
            this.Colour,
            this.Cost,
            this.Weight,
            this.Total_Price});
            this.dataGridView2.Location = new System.Drawing.Point(9, 249);
            this.dataGridView2.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersVisible = false;
            this.dataGridView2.RowHeadersWidth = 51;
            this.dataGridView2.RowTemplate.Height = 24;
            this.dataGridView2.Size = new System.Drawing.Size(406, 215);
            this.dataGridView2.TabIndex = 0;
            this.dataGridView2.TabStop = false;
            this.dataGridView2.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView2_CellMouseDown);
            this.dataGridView2.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellValueChanged);
            this.dataGridView2.CurrentCellDirtyStateChanged += new System.EventHandler(this.dataGridView2_CurrentCellDirtyStateChanged);
            this.dataGridView2.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridView2_EditingControlShowing);
            this.dataGridView2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView2_KeyDown);
            // 
            // Quality
            // 
            this.Quality.HeaderText = "Quality";
            this.Quality.MinimumWidth = 6;
            this.Quality.Name = "Quality";
            this.Quality.ReadOnly = true;
            // 
            // Colour
            // 
            this.Colour.HeaderText = "Colour";
            this.Colour.MinimumWidth = 6;
            this.Colour.Name = "Colour";
            this.Colour.ReadOnly = true;
            // 
            // Cost
            // 
            this.Cost.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Cost.HeaderText = "Price/kg";
            this.Cost.MinimumWidth = 6;
            this.Cost.Name = "Cost";
            // 
            // Weight
            // 
            this.Weight.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Weight.HeaderText = "Weight";
            this.Weight.MinimumWidth = 6;
            this.Weight.Name = "Weight";
            this.Weight.ReadOnly = true;
            // 
            // Total_Price
            // 
            this.Total_Price.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Total_Price.HeaderText = "Total Price";
            this.Total_Price.MinimumWidth = 6;
            this.Total_Price.Name = "Total_Price";
            this.Total_Price.ReadOnly = true;
            // 
            // factoryInventoryDataSet
            // 
            this.factoryInventoryDataSet.DataSetName = "FactoryInventoryDataSet";
            this.factoryInventoryDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // cartonVoucherBindingSource
            // 
            this.cartonVoucherBindingSource.DataMember = "Carton_Voucher";
            this.cartonVoucherBindingSource.DataSource = this.factoryInventoryDataSet;
            // 
            // carton_VoucherTableAdapter
            // 
            this.carton_VoucherTableAdapter.ClearBeforeFill = true;
            // 
            // lockCartonsCK
            // 
            this.lockCartonsCK.AutoSize = true;
            this.lockCartonsCK.Location = new System.Drawing.Point(253, 177);
            this.lockCartonsCK.Margin = new System.Windows.Forms.Padding(2);
            this.lockCartonsCK.Name = "lockCartonsCK";
            this.lockCartonsCK.Size = new System.Drawing.Size(89, 17);
            this.lockCartonsCK.TabIndex = 10;
            this.lockCartonsCK.Text = "Lock Cartons";
            this.lockCartonsCK.UseVisualStyleBackColor = true;
            this.lockCartonsCK.CheckedChanged += new System.EventHandler(this.lockCartonsCK_CheckedChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(76, 496);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(84, 17);
            this.label6.TabIndex = 0;
            this.label6.Text = "Total Rate";
            // 
            // deleteButton
            // 
            this.deleteButton.Location = new System.Drawing.Point(230, 537);
            this.deleteButton.Margin = new System.Windows.Forms.Padding(2);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(108, 46);
            this.deleteButton.TabIndex = 11;
            this.deleteButton.TabStop = false;
            this.deleteButton.Text = "Delete Voucher";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Visible = false;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // dynamicWeightTB
            // 
            this.dynamicWeightTB.Location = new System.Drawing.Point(188, 472);
            this.dynamicWeightTB.Margin = new System.Windows.Forms.Padding(2);
            this.dynamicWeightTB.Name = "dynamicWeightTB";
            this.dynamicWeightTB.ReadOnly = true;
            this.dynamicWeightTB.Size = new System.Drawing.Size(151, 20);
            this.dynamicWeightTB.TabIndex = 0;
            this.dynamicWeightTB.TabStop = false;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(188, 496);
            this.label4.Margin = new System.Windows.Forms.Padding(2);
            this.label4.Name = "label4";
            this.label4.ReadOnly = true;
            this.label4.Size = new System.Drawing.Size(151, 20);
            this.label4.TabIndex = 0;
            this.label4.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(6, 628);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(69, 17);
            this.label7.TabIndex = 12;
            this.label7.Text = "{label 7}";
            // 
            // typeCB
            // 
            this.typeCB.FormattingEnabled = true;
            this.typeCB.Items.AddRange(new object[] {
            "0",
            "1"});
            this.typeCB.Location = new System.Drawing.Point(9, 74);
            this.typeCB.Margin = new System.Windows.Forms.Padding(2);
            this.typeCB.Name = "typeCB";
            this.typeCB.Size = new System.Drawing.Size(151, 21);
            this.typeCB.TabIndex = 14;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(7, 58);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(31, 13);
            this.label8.TabIndex = 13;
            this.label8.Text = "Type";
            // 
            // narrationTB
            // 
            this.narrationTB.Location = new System.Drawing.Point(79, 600);
            this.narrationTB.Margin = new System.Windows.Forms.Padding(2);
            this.narrationTB.Name = "narrationTB";
            this.narrationTB.Size = new System.Drawing.Size(957, 20);
            this.narrationTB.TabIndex = 15;
            this.narrationTB.TabStop = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(3, 601);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(76, 17);
            this.label9.TabIndex = 16;
            this.label9.Text = "Narration";
            // 
            // T_V1_cartonInwardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1028, 649);
            this.Controls.Add(this.narrationTB);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.typeCB);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dynamicWeightTB);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lockCartonsCK);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.costLabel);
            this.Controls.Add(this.comboBox2CB);
            this.Controls.Add(this.totalWeightLabel);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.billNumberTextboxTB);
            this.Controls.Add(this.billDateDTP);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.inputDate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "T_V1_cartonInwardForm";
            this.Text = "Voucher - Carton Inward";
            this.Load += new System.EventHandler(this.M_V1_cartonInwardForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.factoryInventoryDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cartonVoucherBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker inputDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker billDateDTP;
        private System.Windows.Forms.TextBox billNumberTextboxTB;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Label totalWeightLabel;
        private System.Windows.Forms.ComboBox comboBox2CB;
        private System.Windows.Forms.Label costLabel;
        private System.Windows.Forms.DataGridView dataGridView2;
        private FactoryInventoryDataSet factoryInventoryDataSet;
        private System.Windows.Forms.BindingSource cartonVoucherBindingSource;
        private FactoryInventoryDataSetTableAdapters.Carton_VoucherTableAdapter carton_VoucherTableAdapter;
        private System.Windows.Forms.CheckBox lockCartonsCK;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.TextBox dynamicWeightTB;
        private System.Windows.Forms.TextBox label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox typeCB;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox narrationTB;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Quality;
        private System.Windows.Forms.DataGridViewTextBoxColumn Colour;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cost;
        private System.Windows.Forms.DataGridViewTextBoxColumn Weight;
        private System.Windows.Forms.DataGridViewTextBoxColumn Total_Price;
    }
}