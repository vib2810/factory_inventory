
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
            this.inputDateDTP = new System.Windows.Forms.DateTimePicker();
            this.paymentDateDTP = new System.Windows.Forms.DateTimePicker();
            this.cusotmerCB = new System.Windows.Forms.ComboBox();
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
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
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
            // cusotmerCB
            // 
            this.cusotmerCB.FormattingEnabled = true;
            this.cusotmerCB.Location = new System.Drawing.Point(12, 132);
            this.cusotmerCB.Name = "cusotmerCB";
            this.cusotmerCB.Size = new System.Drawing.Size(138, 21);
            this.cusotmerCB.TabIndex = 2;
            // 
            // amountTB
            // 
            this.amountTB.Location = new System.Drawing.Point(23, 233);
            this.amountTB.Name = "amountTB";
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
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(184, 25);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(604, 333);
            this.dataGridView1.TabIndex = 5;
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
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(12, 272);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(138, 40);
            this.saveButton.TabIndex = 10;
            this.saveButton.Text = "Save Voucher";
            this.saveButton.UseVisualStyleBackColor = true;
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
            this.Controls.Add(this.cusotmerCB);
            this.Controls.Add(this.paymentDateDTP);
            this.Controls.Add(this.inputDateDTP);
            this.Name = "M_V3_paymentsForm";
            this.Text = "M_V3_paymentsForm";
            this.Load += new System.EventHandler(this.M_V3_paymentsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker inputDateDTP;
        private System.Windows.Forms.DateTimePicker paymentDateDTP;
        private System.Windows.Forms.ComboBox cusotmerCB;
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
    }
}