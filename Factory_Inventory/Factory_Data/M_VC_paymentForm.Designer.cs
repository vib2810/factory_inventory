
namespace Factory_Inventory.Factory_Data
{
    partial class M_VC_paymentForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.inputDateDTP = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.sl_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.do_no = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.payment_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.payment_amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.deleteMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveButton = new System.Windows.Forms.Button();
            this.customerCB = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.loadDataButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.narrationTB = new System.Windows.Forms.TextBox();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.do_no_2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.total_amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pending_amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.close_do = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.label5 = new System.Windows.Forms.Label();
            this.totalPaymentTB = new System.Windows.Forms.TextBox();
            this.comments = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.deleteMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // inputDateDTP
            // 
            this.inputDateDTP.Enabled = false;
            this.inputDateDTP.Location = new System.Drawing.Point(10, 33);
            this.inputDateDTP.Name = "inputDateDTP";
            this.inputDateDTP.Size = new System.Drawing.Size(136, 20);
            this.inputDateDTP.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Input Date";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.sl_no,
            this.do_no,
            this.payment_date,
            this.payment_amount,
            this.comments});
            this.dataGridView1.ContextMenuStrip = this.deleteMenuStrip1;
            this.dataGridView1.Location = new System.Drawing.Point(391, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(640, 443);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEndEdit);
            this.dataGridView1.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseDown);
            this.dataGridView1.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGridView1_RowPostPaint);
            this.dataGridView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView1_KeyDown);
            // 
            // sl_no
            // 
            this.sl_no.HeaderText = "Sl. No.";
            this.sl_no.Name = "sl_no";
            this.sl_no.ReadOnly = true;
            this.sl_no.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.sl_no.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // do_no
            // 
            this.do_no.HeaderText = "DO No.";
            this.do_no.MaxDropDownItems = 100;
            this.do_no.Name = "do_no";
            this.do_no.ReadOnly = true;
            this.do_no.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // payment_date
            // 
            this.payment_date.HeaderText = "Payment Date";
            this.payment_date.Name = "payment_date";
            this.payment_date.ReadOnly = true;
            this.payment_date.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // payment_amount
            // 
            this.payment_amount.HeaderText = "Payment Amount";
            this.payment_amount.Name = "payment_amount";
            this.payment_amount.ReadOnly = true;
            this.payment_amount.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // deleteMenuStrip1
            // 
            this.deleteMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem});
            this.deleteMenuStrip1.Name = "deleteMenuStrip1";
            this.deleteMenuStrip1.Size = new System.Drawing.Size(108, 26);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(13, 412);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(136, 43);
            this.saveButton.TabIndex = 3;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // customerCB
            // 
            this.customerCB.FormattingEnabled = true;
            this.customerCB.Location = new System.Drawing.Point(10, 88);
            this.customerCB.Name = "customerCB";
            this.customerCB.Size = new System.Drawing.Size(136, 21);
            this.customerCB.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Customer";
            // 
            // loadDataButton
            // 
            this.loadDataButton.Location = new System.Drawing.Point(10, 115);
            this.loadDataButton.Name = "loadDataButton";
            this.loadDataButton.Size = new System.Drawing.Size(136, 35);
            this.loadDataButton.TabIndex = 6;
            this.loadDataButton.Text = "Load Data";
            this.loadDataButton.UseVisualStyleBackColor = true;
            this.loadDataButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(58, 373);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 16);
            this.label3.TabIndex = 7;
            this.label3.Text = "Total Payment:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(10, 464);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 16);
            this.label4.TabIndex = 9;
            this.label4.Text = "Narration:";
            // 
            // narrationTB
            // 
            this.narrationTB.Location = new System.Drawing.Point(84, 464);
            this.narrationTB.Name = "narrationTB";
            this.narrationTB.Size = new System.Drawing.Size(947, 20);
            this.narrationTB.TabIndex = 10;
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.AllowUserToOrderColumns = true;
            this.dataGridView2.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.do_no_2,
            this.total_amount,
            this.pending_amount,
            this.close_do});
            this.dataGridView2.Location = new System.Drawing.Point(10, 179);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersVisible = false;
            this.dataGridView2.Size = new System.Drawing.Size(375, 187);
            this.dataGridView2.TabIndex = 11;
            // 
            // do_no_2
            // 
            this.do_no_2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.do_no_2.HeaderText = "DO No.";
            this.do_no_2.Name = "do_no_2";
            this.do_no_2.ReadOnly = true;
            this.do_no_2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.do_no_2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // total_amount
            // 
            this.total_amount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.total_amount.HeaderText = "Total Amount";
            this.total_amount.Name = "total_amount";
            this.total_amount.ReadOnly = true;
            this.total_amount.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.total_amount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // pending_amount
            // 
            this.pending_amount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.pending_amount.HeaderText = "Pending Amount";
            this.pending_amount.Name = "pending_amount";
            this.pending_amount.ReadOnly = true;
            this.pending_amount.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.pending_amount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // close_do
            // 
            this.close_do.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.close_do.HeaderText = "Close DO";
            this.close_do.Name = "close_do";
            this.close_do.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(10, 160);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 16);
            this.label5.TabIndex = 12;
            this.label5.Text = "DO List:";
            // 
            // totalPaymentTB
            // 
            this.totalPaymentTB.Location = new System.Drawing.Point(176, 372);
            this.totalPaymentTB.Name = "totalPaymentTB";
            this.totalPaymentTB.ReadOnly = true;
            this.totalPaymentTB.Size = new System.Drawing.Size(124, 20);
            this.totalPaymentTB.TabIndex = 13;
            // 
            // comments
            // 
            this.comments.HeaderText = "Comments";
            this.comments.Name = "comments";
            this.comments.ReadOnly = true;
            // 
            // M_VC_paymentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1033, 491);
            this.Controls.Add(this.totalPaymentTB);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.narrationTB);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.loadDataButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.customerCB);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.inputDateDTP);
            this.Name = "M_VC_paymentForm";
            this.Text = "M_VC_paymentForm";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.deleteMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker inputDateDTP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.ComboBox customerCB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button loadDataButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox narrationTB;
        private System.Windows.Forms.ContextMenuStrip deleteMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox totalPaymentTB;
        private System.Windows.Forms.DataGridViewTextBoxColumn sl_no;
        private System.Windows.Forms.DataGridViewComboBoxColumn do_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn payment_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn payment_amount;
        private System.Windows.Forms.DataGridViewTextBoxColumn do_no_2;
        private System.Windows.Forms.DataGridViewTextBoxColumn total_amount;
        private System.Windows.Forms.DataGridViewTextBoxColumn pending_amount;
        private System.Windows.Forms.DataGridViewCheckBoxColumn close_do;
        private System.Windows.Forms.DataGridViewTextBoxColumn comments;
    }
}