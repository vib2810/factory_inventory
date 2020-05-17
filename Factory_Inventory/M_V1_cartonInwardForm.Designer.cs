namespace Factory_Inventory
{
    partial class M_V1_cartonInwardForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
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
            this.dynamicWeightLabel = new System.Windows.Forms.Label();
            this.comboBox2CB = new System.Windows.Forms.ComboBox();
            this.costLabel = new System.Windows.Forms.Label();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.factoryInventoryDataSet = new Factory_Inventory.FactoryInventoryDataSet();
            this.cartonVoucherBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.carton_VoucherTableAdapter = new Factory_Inventory.FactoryInventoryDataSetTableAdapters.Carton_VoucherTableAdapter();
            this.lockCartonsCK = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.Quality = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Weight = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Total_Price = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridView1.Location = new System.Drawing.Point(471, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(695, 668);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.TabStop = false;
            this.dataGridView1.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseDown);
            this.dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
            this.dataGridView1.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGridView1_RowPostPaint);
            this.dataGridView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView1_KeyDown);
            this.dataGridView1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dataGridView1_KeyPress);
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Input Date";
            // 
            // inputDate
            // 
            this.inputDate.Enabled = false;
            this.inputDate.Location = new System.Drawing.Point(12, 39);
            this.inputDate.Name = "inputDate";
            this.inputDate.Size = new System.Drawing.Size(200, 22);
            this.inputDate.TabIndex = 0;
            this.inputDate.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "Bill Date";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 136);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 17);
            this.label3.TabIndex = 0;
            this.label3.Text = "Bill Number";
            // 
            // billDateDTP
            // 
            this.billDateDTP.CustomFormat = " ";
            this.billDateDTP.Location = new System.Drawing.Point(12, 96);
            this.billDateDTP.Name = "billDateDTP";
            this.billDateDTP.Size = new System.Drawing.Size(200, 22);
            this.billDateDTP.TabIndex = 1;
            this.billDateDTP.ValueChanged += new System.EventHandler(this.billDate_ValueChanged);
            // 
            // billNumberTextboxTB
            // 
            this.billNumberTextboxTB.Location = new System.Drawing.Point(12, 156);
            this.billNumberTextboxTB.Name = "billNumberTextboxTB";
            this.billNumberTextboxTB.Size = new System.Drawing.Size(200, 22);
            this.billNumberTextboxTB.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 195);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 17);
            this.label5.TabIndex = 0;
            this.label5.Text = "Company";
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(101, 508);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(111, 64);
            this.saveButton.TabIndex = 9;
            this.saveButton.Text = "Save Voucher";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // totalWeightLabel
            // 
            this.totalWeightLabel.AutoSize = true;
            this.totalWeightLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalWeightLabel.Location = new System.Drawing.Point(43, 585);
            this.totalWeightLabel.Name = "totalWeightLabel";
            this.totalWeightLabel.Size = new System.Drawing.Size(141, 25);
            this.totalWeightLabel.TabIndex = 0;
            this.totalWeightLabel.Text = "Total Weight ";
            // 
            // dynamicWeightLabel
            // 
            this.dynamicWeightLabel.AutoSize = true;
            this.dynamicWeightLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dynamicWeightLabel.Location = new System.Drawing.Point(43, 610);
            this.dynamicWeightLabel.Name = "dynamicWeightLabel";
            this.dynamicWeightLabel.Size = new System.Drawing.Size(42, 25);
            this.dynamicWeightLabel.TabIndex = 0;
            this.dynamicWeightLabel.Text = "0.0";
            // 
            // comboBox2CB
            // 
            this.comboBox2CB.FormattingEnabled = true;
            this.comboBox2CB.Location = new System.Drawing.Point(15, 215);
            this.comboBox2CB.Name = "comboBox2CB";
            this.comboBox2CB.Size = new System.Drawing.Size(197, 24);
            this.comboBox2CB.TabIndex = 5;
            // 
            // costLabel
            // 
            this.costLabel.AutoSize = true;
            this.costLabel.Location = new System.Drawing.Point(12, 258);
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
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Quality,
            this.Cost,
            this.Weight,
            this.Total_Price});
            this.dataGridView2.Location = new System.Drawing.Point(12, 288);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersVisible = false;
            this.dataGridView2.RowHeadersWidth = 51;
            this.dataGridView2.RowTemplate.Height = 24;
            this.dataGridView2.Size = new System.Drawing.Size(438, 210);
            this.dataGridView2.TabIndex = 0;
            this.dataGridView2.TabStop = false;
            this.dataGridView2.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellValueChanged);
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
            this.lockCartonsCK.Location = new System.Drawing.Point(241, 531);
            this.lockCartonsCK.Name = "lockCartonsCK";
            this.lockCartonsCK.Size = new System.Drawing.Size(113, 21);
            this.lockCartonsCK.TabIndex = 10;
            this.lockCartonsCK.Text = "Lock Cartons";
            this.lockCartonsCK.UseVisualStyleBackColor = true;
            this.lockCartonsCK.CheckedChanged += new System.EventHandler(this.lockCartonsCK_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(236, 610);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 25);
            this.label4.TabIndex = 0;
            this.label4.Text = "0.0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(236, 585);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(111, 25);
            this.label6.TabIndex = 0;
            this.label6.Text = "Total Rate";
            // 
            // Quality
            // 
            this.Quality.HeaderText = "Quality";
            this.Quality.MinimumWidth = 6;
            this.Quality.Name = "Quality";
            this.Quality.ReadOnly = true;
            // 
            // Cost
            // 
            this.Cost.HeaderText = "Price/kg";
            this.Cost.MinimumWidth = 6;
            this.Cost.Name = "Cost";
            // 
            // Weight
            // 
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
            // M_V1_cartonInwardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1168, 668);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lockCartonsCK);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.costLabel);
            this.Controls.Add(this.comboBox2CB);
            this.Controls.Add(this.dynamicWeightLabel);
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
            this.Name = "M_V1_cartonInwardForm";
            this.Text = "cartoonInwardForm";
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
        private System.Windows.Forms.Label dynamicWeightLabel;
        private System.Windows.Forms.ComboBox comboBox2CB;
        private System.Windows.Forms.Label costLabel;
        private System.Windows.Forms.DataGridView dataGridView2;
        private FactoryInventoryDataSet factoryInventoryDataSet;
        private System.Windows.Forms.BindingSource cartonVoucherBindingSource;
        private FactoryInventoryDataSetTableAdapters.Carton_VoucherTableAdapter carton_VoucherTableAdapter;
        private System.Windows.Forms.CheckBox lockCartonsCK;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Quality;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cost;
        private System.Windows.Forms.DataGridViewTextBoxColumn Weight;
        private System.Windows.Forms.DataGridViewTextBoxColumn Total_Price;
    }
}