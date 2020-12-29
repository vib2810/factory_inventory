namespace Factory_Inventory
{
    partial class T_V2_repackingForm
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
            this.carton_VoucherTableAdapter = new Factory_Inventory.FactoryInventoryDataSetTableAdapters.Carton_VoucherTableAdapter();
            this.loadDataButton = new System.Windows.Forms.Button();
            this.inputDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.colourComboboxCB = new System.Windows.Forms.ComboBox();
            this.companyComboboxCB = new System.Windows.Forms.ComboBox();
            this.qualityComboboxCB = new System.Windows.Forms.ComboBox();
            this.coneComboboxCB = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.closedCheckboxCK = new System.Windows.Forms.CheckBox();
            this.cartonVoucherBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label8 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.cartonweight = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.financialYearComboboxCB = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.oilGainTextbox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.deleteButton = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.nextcartonnoTB = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.typeCB = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.batchnwtTextbox = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.coneWeightTB = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.contextMenuStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cartonVoucherBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridView1.Location = new System.Drawing.Point(587, 30);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(829, 689);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.TabStop = false;
            this.dataGridView1.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dataGridView1_CellBeginEdit);
            this.dataGridView1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEndEdit);
            this.dataGridView1.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseDown);
            this.dataGridView1.CurrentCellDirtyStateChanged += new System.EventHandler(this.dataGridView1_CurrentCellDirtyStateChanged);
            this.dataGridView1.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridView1_EditingControlShowing);
            this.dataGridView1.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGridView1_RowPostPaint_1);
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
            this.saveButton.Location = new System.Drawing.Point(10, 656);
            this.saveButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(211, 63);
            this.saveButton.TabIndex = 15;
            this.saveButton.Text = "Save Voucher";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // carton_VoucherTableAdapter
            // 
            this.carton_VoucherTableAdapter.ClearBeforeFill = true;
            // 
            // loadDataButton
            // 
            this.loadDataButton.Location = new System.Drawing.Point(10, 338);
            this.loadDataButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.loadDataButton.Name = "loadDataButton";
            this.loadDataButton.Size = new System.Drawing.Size(209, 55);
            this.loadDataButton.TabIndex = 11;
            this.loadDataButton.Text = "Load Data";
            this.loadDataButton.UseVisualStyleBackColor = true;
            this.loadDataButton.Click += new System.EventHandler(this.loadCartonButton_Click);
            // 
            // inputDate
            // 
            this.inputDate.Enabled = false;
            this.inputDate.Location = new System.Drawing.Point(10, 30);
            this.inputDate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.inputDate.Name = "inputDate";
            this.inputDate.Size = new System.Drawing.Size(209, 22);
            this.inputDate.TabIndex = 36;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 17);
            this.label3.TabIndex = 0;
            this.label3.Text = "Input Date";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 54);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 17);
            this.label6.TabIndex = 0;
            this.label6.Text = "Colour";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 17);
            this.label4.TabIndex = 0;
            this.label4.Text = "Quality";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(228, 728);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(103, 20);
            this.label5.TabIndex = 0;
            this.label5.Text = "Total Weight";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 147);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 17);
            this.label7.TabIndex = 0;
            this.label7.Text = "Company";
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToResizeColumns = false;
            this.dataGridView2.AllowUserToResizeRows = false;
            this.dataGridView2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.ContextMenuStrip = this.contextMenuStrip2;
            this.dataGridView2.Location = new System.Drawing.Point(232, 30);
            this.dataGridView2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersWidth = 51;
            this.dataGridView2.RowTemplate.Height = 24;
            this.dataGridView2.Size = new System.Drawing.Size(349, 689);
            this.dataGridView2.TabIndex = 0;
            this.dataGridView2.TabStop = false;
            this.dataGridView2.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellEndEdit);
            this.dataGridView2.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView2_CellMouseDown);
            this.dataGridView2.CurrentCellDirtyStateChanged += new System.EventHandler(this.dataGridView2_CurrentCellDirtyStateChanged);
            this.dataGridView2.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridView2_EditingControlShowing);
            this.dataGridView2.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGridView2_RowPostPaint);
            this.dataGridView2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView2_KeyDown);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem1});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(123, 28);
            // 
            // deleteToolStripMenuItem1
            // 
            this.deleteToolStripMenuItem1.Name = "deleteToolStripMenuItem1";
            this.deleteToolStripMenuItem1.Size = new System.Drawing.Size(122, 24);
            this.deleteToolStripMenuItem1.Text = "Delete";
            this.deleteToolStripMenuItem1.Click += new System.EventHandler(this.deleteToolStripMenuItem1_Click_1);
            // 
            // colourComboboxCB
            // 
            this.colourComboboxCB.FormattingEnabled = true;
            this.colourComboboxCB.Location = new System.Drawing.Point(10, 74);
            this.colourComboboxCB.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.colourComboboxCB.Name = "colourComboboxCB";
            this.colourComboboxCB.Size = new System.Drawing.Size(209, 24);
            this.colourComboboxCB.TabIndex = 1;
            // 
            // companyComboboxCB
            // 
            this.companyComboboxCB.FormattingEnabled = true;
            this.companyComboboxCB.Location = new System.Drawing.Point(10, 166);
            this.companyComboboxCB.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.companyComboboxCB.Name = "companyComboboxCB";
            this.companyComboboxCB.Size = new System.Drawing.Size(209, 24);
            this.companyComboboxCB.TabIndex = 5;
            // 
            // qualityComboboxCB
            // 
            this.qualityComboboxCB.FormattingEnabled = true;
            this.qualityComboboxCB.Location = new System.Drawing.Point(10, 120);
            this.qualityComboboxCB.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.qualityComboboxCB.Name = "qualityComboboxCB";
            this.qualityComboboxCB.Size = new System.Drawing.Size(209, 24);
            this.qualityComboboxCB.TabIndex = 3;
            // 
            // coneComboboxCB
            // 
            this.coneComboboxCB.FormattingEnabled = true;
            this.coneComboboxCB.Location = new System.Drawing.Point(10, 429);
            this.coneComboboxCB.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.coneComboboxCB.Name = "coneComboboxCB";
            this.coneComboboxCB.Size = new System.Drawing.Size(178, 24);
            this.coneComboboxCB.TabIndex = 13;
            this.coneComboboxCB.SelectedIndexChanged += new System.EventHandler(this.coneCombobox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 410);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "Cone";
            // 
            // closedCheckboxCK
            // 
            this.closedCheckboxCK.AutoSize = true;
            this.closedCheckboxCK.Location = new System.Drawing.Point(10, 517);
            this.closedCheckboxCK.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.closedCheckboxCK.Name = "closedCheckboxCK";
            this.closedCheckboxCK.Size = new System.Drawing.Size(130, 21);
            this.closedCheckboxCK.TabIndex = 0;
            this.closedCheckboxCK.TabStop = false;
            this.closedCheckboxCK.Text = "Close Voucher?";
            this.closedCheckboxCK.UseVisualStyleBackColor = true;
            this.closedCheckboxCK.CheckedChanged += new System.EventHandler(this.closedCheckboxCK_CheckedChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(135, 479);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(27, 17);
            this.label8.TabIndex = 0;
            this.label8.Text = "gm";
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(1284, 854);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(26, 20);
            this.label12.TabIndex = 0;
            this.label12.Text = "kg";
            // 
            // cartonweight
            // 
            this.cartonweight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cartonweight.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cartonweight.Location = new System.Drawing.Point(1195, 721);
            this.cartonweight.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cartonweight.Name = "cartonweight";
            this.cartonweight.ReadOnly = true;
            this.cartonweight.Size = new System.Drawing.Size(221, 27);
            this.cartonweight.TabIndex = 0;
            this.cartonweight.TabStop = false;
            this.cartonweight.TextChanged += new System.EventHandler(this.cartonweight_TextChanged);
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(1055, 728);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(134, 20);
            this.label13.TabIndex = 0;
            this.label13.Text = "Total Net Weight";
            // 
            // financialYearComboboxCB
            // 
            this.financialYearComboboxCB.FormattingEnabled = true;
            this.financialYearComboboxCB.Location = new System.Drawing.Point(10, 257);
            this.financialYearComboboxCB.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.financialYearComboboxCB.Name = "financialYearComboboxCB";
            this.financialYearComboboxCB.Size = new System.Drawing.Size(209, 24);
            this.financialYearComboboxCB.TabIndex = 7;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(7, 238);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(212, 17);
            this.label9.TabIndex = 0;
            this.label9.Text = "Repacked Carton Financial Year";
            // 
            // oilGainTextbox
            // 
            this.oilGainTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.oilGainTextbox.Location = new System.Drawing.Point(663, 721);
            this.oilGainTextbox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.oilGainTextbox.Name = "oilGainTextbox";
            this.oilGainTextbox.ReadOnly = true;
            this.oilGainTextbox.Size = new System.Drawing.Size(167, 27);
            this.oilGainTextbox.TabIndex = 0;
            this.oilGainTextbox.TabStop = false;
            this.oilGainTextbox.TextChanged += new System.EventHandler(this.oilGainTextbox_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(587, 728);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Oil Gain";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(836, 728);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(24, 20);
            this.label10.TabIndex = 0;
            this.label10.Text = "%";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(7, 757);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(98, 25);
            this.label14.TabIndex = 0;
            this.label14.Tag = "";
            this.label14.Text = "{label14}";
            // 
            // deleteButton
            // 
            this.deleteButton.Location = new System.Drawing.Point(10, 568);
            this.deleteButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(209, 65);
            this.deleteButton.TabIndex = 0;
            this.deleteButton.TabStop = false;
            this.deleteButton.Text = "Delete Voucher";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Visible = false;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(9, 283);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(104, 17);
            this.label15.TabIndex = 0;
            this.label15.Text = "Next Carton No";
            // 
            // nextcartonnoTB
            // 
            this.nextcartonnoTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nextcartonnoTB.Location = new System.Drawing.Point(12, 302);
            this.nextcartonnoTB.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.nextcartonnoTB.Name = "nextcartonnoTB";
            this.nextcartonnoTB.ReadOnly = true;
            this.nextcartonnoTB.Size = new System.Drawing.Size(117, 22);
            this.nextcartonnoTB.TabIndex = 0;
            this.nextcartonnoTB.TabStop = false;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(13, 772);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(0, 20);
            this.label16.TabIndex = 37;
            // 
            // typeCB
            // 
            this.typeCB.FormattingEnabled = true;
            this.typeCB.Items.AddRange(new object[] {
            "0",
            "1"});
            this.typeCB.Location = new System.Drawing.Point(10, 211);
            this.typeCB.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.typeCB.Name = "typeCB";
            this.typeCB.Size = new System.Drawing.Size(209, 24);
            this.typeCB.TabIndex = 39;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(7, 192);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(40, 17);
            this.label17.TabIndex = 38;
            this.label17.Text = "Type";
            // 
            // batchnwtTextbox
            // 
            this.batchnwtTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.batchnwtTextbox.Location = new System.Drawing.Point(337, 721);
            this.batchnwtTextbox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.batchnwtTextbox.Name = "batchnwtTextbox";
            this.batchnwtTextbox.ReadOnly = true;
            this.batchnwtTextbox.Size = new System.Drawing.Size(244, 27);
            this.batchnwtTextbox.TabIndex = 0;
            this.batchnwtTextbox.TabStop = false;
            this.batchnwtTextbox.TextChanged += new System.EventHandler(this.batchnwtTextbox_TextChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(227, 3);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(237, 25);
            this.label11.TabIndex = 40;
            this.label11.Tag = "";
            this.label11.Text = "Cartons to be repacked";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(586, 3);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(190, 25);
            this.label18.TabIndex = 41;
            this.label18.Tag = "";
            this.label18.Text = "Repacked Cartons";
            // 
            // coneWeightTB
            // 
            this.coneWeightTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.coneWeightTB.Location = new System.Drawing.Point(12, 474);
            this.coneWeightTB.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.coneWeightTB.Name = "coneWeightTB";
            this.coneWeightTB.ReadOnly = true;
            this.coneWeightTB.Size = new System.Drawing.Size(117, 22);
            this.coneWeightTB.TabIndex = 42;
            this.coneWeightTB.TabStop = false;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(9, 455);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(89, 17);
            this.label19.TabIndex = 43;
            this.label19.Text = "Cone Weight";
            // 
            // T_V2_repackingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1417, 821);
            this.Controls.Add(this.coneWeightTB);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.typeCB);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.nextcartonnoTB);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.oilGainTextbox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.financialYearComboboxCB);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.cartonweight);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.closedCheckboxCK);
            this.Controls.Add(this.coneComboboxCB);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.qualityComboboxCB);
            this.Controls.Add(this.companyComboboxCB);
            this.Controls.Add(this.colourComboboxCB);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.batchnwtTextbox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.inputDate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.loadDataButton);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.saveButton);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "T_V2_repackingForm";
            this.Text = "Voucher - Repacking";
            this.Load += new System.EventHandler(this.M_V3_cartonProductionForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.contextMenuStrip2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cartonVoucherBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.BindingSource cartonVoucherBindingSource;
        private FactoryInventoryDataSetTableAdapters.Carton_VoucherTableAdapter carton_VoucherTableAdapter;
        private System.Windows.Forms.Button loadDataButton;
        private System.Windows.Forms.DateTimePicker inputDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.ComboBox colourComboboxCB;
        private System.Windows.Forms.ComboBox companyComboboxCB;
        private System.Windows.Forms.ComboBox qualityComboboxCB;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem1;
        private System.Windows.Forms.ComboBox coneComboboxCB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox closedCheckboxCK;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox cartonweight;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox financialYearComboboxCB;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox oilGainTextbox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label14;
        public System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox nextcartonnoTB;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox typeCB;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox batchnwtTextbox;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox coneWeightTB;
        private System.Windows.Forms.Label label19;
    }
}