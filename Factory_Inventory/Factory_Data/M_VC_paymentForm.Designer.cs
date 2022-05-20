
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
            this.inputDateDTP = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.slno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dono = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.total_amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pending_amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.payment_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.payment_amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.close = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.saveButton = new System.Windows.Forms.Button();
            this.customerCB = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.loadDataButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.paymentLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
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
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.slno,
            this.dono,
            this.total_amount,
            this.pending_amount,
            this.payment_date,
            this.payment_amount,
            this.close});
            this.dataGridView1.Location = new System.Drawing.Point(154, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(634, 426);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGridView1_RowPostPaint);
            this.dataGridView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView1_KeyDown);
            // 
            // slno
            // 
            this.slno.HeaderText = "Sl. No.";
            this.slno.Name = "slno";
            this.slno.ReadOnly = true;
            this.slno.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.slno.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dono
            // 
            this.dono.HeaderText = "DO No.";
            this.dono.MaxDropDownItems = 100;
            this.dono.Name = "dono";
            this.dono.ReadOnly = true;
            this.dono.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // total_amount
            // 
            this.total_amount.HeaderText = "Total Amount";
            this.total_amount.Name = "total_amount";
            this.total_amount.ReadOnly = true;
            this.total_amount.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // pending_amount
            // 
            this.pending_amount.HeaderText = "Pending Amount";
            this.pending_amount.Name = "pending_amount";
            this.pending_amount.ReadOnly = true;
            this.pending_amount.Resizable = System.Windows.Forms.DataGridViewTriState.False;
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
            // close
            // 
            this.close.HeaderText = "Close DO";
            this.close.Name = "close";
            this.close.ReadOnly = true;
            this.close.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(12, 395);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(136, 43);
            this.saveButton.TabIndex = 3;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
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
            this.label3.Location = new System.Drawing.Point(7, 193);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 16);
            this.label3.TabIndex = 7;
            this.label3.Text = "Total Payment:";
            // 
            // paymentLabel
            // 
            this.paymentLabel.AutoSize = true;
            this.paymentLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.paymentLabel.Location = new System.Drawing.Point(10, 214);
            this.paymentLabel.Name = "paymentLabel";
            this.paymentLabel.Size = new System.Drawing.Size(106, 16);
            this.paymentLabel.TabIndex = 8;
            this.paymentLabel.Text = "paymentLabel";
            // 
            // M_VC_paymentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.paymentLabel);
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
        private System.Windows.Forms.DataGridViewTextBoxColumn slno;
        private System.Windows.Forms.DataGridViewComboBoxColumn dono;
        private System.Windows.Forms.DataGridViewTextBoxColumn total_amount;
        private System.Windows.Forms.DataGridViewTextBoxColumn pending_amount;
        private System.Windows.Forms.DataGridViewTextBoxColumn payment_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn payment_amount;
        private System.Windows.Forms.DataGridViewCheckBoxColumn close;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label paymentLabel;
    }
}