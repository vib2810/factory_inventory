
namespace Factory_Inventory.Factory_Data
{
    partial class M_V3_paymentsForm
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
            this.inputDateDTP = new System.Windows.Forms.DateTimePicker();
            this.paymentDateDTP = new System.Windows.Forms.DateTimePicker();
            this.customerCB = new System.Windows.Forms.ComboBox();
            this.amountTB = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.loadDOButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.deleteButton = new System.Windows.Forms.Button();
            this.narrationTB = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.slNoCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.doNoCol = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.amountReceivedCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.amountPendingCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.totalAmountCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.doPaymentClosedCol = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.commentsCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // inputDateDTP
            // 
            this.inputDateDTP.Enabled = false;
            this.inputDateDTP.Location = new System.Drawing.Point(12, 25);
            this.inputDateDTP.Name = "inputDateDTP";
            this.inputDateDTP.Size = new System.Drawing.Size(138, 20);
            this.inputDateDTP.TabIndex = 0;
            // 
            // paymentDateDTP
            // 
            this.paymentDateDTP.Location = new System.Drawing.Point(12, 80);
            this.paymentDateDTP.Name = "paymentDateDTP";
            this.paymentDateDTP.Size = new System.Drawing.Size(138, 20);
            this.paymentDateDTP.TabIndex = 1;
            // 
            // customerCB
            // 
            this.customerCB.FormattingEnabled = true;
            this.customerCB.Location = new System.Drawing.Point(12, 132);
            this.customerCB.Name = "customerCB";
            this.customerCB.Size = new System.Drawing.Size(138, 21);
            this.customerCB.TabIndex = 2;
            // 
            // amountTB
            // 
            this.amountTB.Location = new System.Drawing.Point(23, 233);
            this.amountTB.Name = "amountTB";
            this.amountTB.ReadOnly = true;
            this.amountTB.Size = new System.Drawing.Size(127, 20);
            this.amountTB.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 217);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Received Amount";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.slNoCol,
            this.doNoCol,
            this.amountReceivedCol,
            this.amountPendingCol,
            this.totalAmountCol,
            this.doPaymentClosedCol,
            this.commentsCol});
            this.dataGridView1.Enabled = false;
            this.dataGridView1.Location = new System.Drawing.Point(184, 25);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(604, 333);
            this.dataGridView1.TabIndex = 5;
            this.dataGridView1.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseDown);
            this.dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
            this.dataGridView1.CurrentCellDirtyStateChanged += new System.EventHandler(this.dataGridView1_CurrentCellDirtyStateChanged_1);
            this.dataGridView1.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridView1_EditingControlShowing);
            this.dataGridView1.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGridView1_RowPostPaint);
            this.dataGridView1.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dataGridView1_RowsAdded);
            this.dataGridView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView1_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Payment Date";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 116);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Customer Name";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Input Date";
            // 
            // loadDOButton
            // 
            this.loadDOButton.Location = new System.Drawing.Point(12, 159);
            this.loadDOButton.Name = "loadDOButton";
            this.loadDOButton.Size = new System.Drawing.Size(138, 29);
            this.loadDOButton.TabIndex = 9;
            this.loadDOButton.Text = "Load Pending DOs";
            this.loadDOButton.UseVisualStyleBackColor = true;
            this.loadDOButton.Click += new System.EventHandler(this.loadDOButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Enabled = false;
            this.saveButton.Location = new System.Drawing.Point(12, 272);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(138, 40);
            this.saveButton.TabIndex = 10;
            this.saveButton.Text = "Save Voucher";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 236);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(13, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "₹";
            // 
            // deleteButton
            // 
            this.deleteButton.Location = new System.Drawing.Point(12, 318);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(138, 40);
            this.deleteButton.TabIndex = 13;
            this.deleteButton.Text = "Delete Voucher";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Visible = false;
            // 
            // narrationTB
            // 
            this.narrationTB.Location = new System.Drawing.Point(65, 377);
            this.narrationTB.Name = "narrationTB";
            this.narrationTB.Size = new System.Drawing.Size(723, 20);
            this.narrationTB.TabIndex = 14;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 380);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(50, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Narration";
            // 
            // contextMenuStrip1
            // 
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
            // slNoCol
            // 
            this.slNoCol.FillWeight = 87.05582F;
            this.slNoCol.HeaderText = "Sl No.";
            this.slNoCol.Name = "slNoCol";
            this.slNoCol.ReadOnly = true;
            // 
            // doNoCol
            // 
            this.doNoCol.FillWeight = 177.665F;
            this.doNoCol.HeaderText = "DO No.";
            this.doNoCol.Name = "doNoCol";
            // 
            // amountReceivedCol
            // 
            this.amountReceivedCol.FillWeight = 87.05582F;
            this.amountReceivedCol.HeaderText = "Amount Received";
            this.amountReceivedCol.Name = "amountReceivedCol";
            // 
            // amountPendingCol
            // 
            this.amountPendingCol.FillWeight = 87.05582F;
            this.amountPendingCol.HeaderText = "Amount Pending";
            this.amountPendingCol.Name = "amountPendingCol";
            this.amountPendingCol.ReadOnly = true;
            // 
            // totalAmountCol
            // 
            this.totalAmountCol.FillWeight = 87.05582F;
            this.totalAmountCol.HeaderText = "Total Amount";
            this.totalAmountCol.Name = "totalAmountCol";
            this.totalAmountCol.ReadOnly = true;
            // 
            // doPaymentClosedCol
            // 
            this.doPaymentClosedCol.FillWeight = 87.05582F;
            this.doPaymentClosedCol.HeaderText = "DO Payment Closed";
            this.doPaymentClosedCol.Name = "doPaymentClosedCol";
            // 
            // commentsCol
            // 
            this.commentsCol.FillWeight = 87.05582F;
            this.commentsCol.HeaderText = "Comments";
            this.commentsCol.Name = "commentsCol";
            // 
            // M_V3_paymentsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 405);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.narrationTB);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.loadDOButton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.amountTB);
            this.Controls.Add(this.customerCB);
            this.Controls.Add(this.paymentDateDTP);
            this.Controls.Add(this.inputDateDTP);
            this.Name = "M_V3_paymentsForm";
            this.Text = "M_V3_paymentsForm";
            this.Load += new System.EventHandler(this.M_V3_paymentsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker inputDateDTP;
        private System.Windows.Forms.DateTimePicker paymentDateDTP;
        private System.Windows.Forms.ComboBox customerCB;
        private System.Windows.Forms.TextBox amountTB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button loadDOButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.TextBox narrationTB;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn slNoCol;
        private System.Windows.Forms.DataGridViewComboBoxColumn doNoCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn amountReceivedCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn amountPendingCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn totalAmountCol;
        private System.Windows.Forms.DataGridViewCheckBoxColumn doPaymentClosedCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn commentsCol;
    }
}