namespace Factory_Inventory
{
    partial class printDO
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(printDO));
            this.label1 = new System.Windows.Forms.Label();
            this.donoTextbox = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.saleDateTextbox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.netwtTextbox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.amountTextbox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.qualityTextbox = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.customerNameTextbox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.rateTB = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 128);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "DO Number";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // donoTextbox
            // 
            this.donoTextbox.Location = new System.Drawing.Point(142, 123);
            this.donoTextbox.Name = "donoTextbox";
            this.donoTextbox.ReadOnly = true;
            this.donoTextbox.Size = new System.Drawing.Size(164, 22);
            this.donoTextbox.TabIndex = 1;
            this.donoTextbox.TextChanged += new System.EventHandler(this.donoTextbox_TextChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(28, 222);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(533, 397);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // saleDateTextbox
            // 
            this.saleDateTextbox.Location = new System.Drawing.Point(459, 123);
            this.saleDateTextbox.Name = "saleDateTextbox";
            this.saleDateTextbox.ReadOnly = true;
            this.saleDateTextbox.Size = new System.Drawing.Size(102, 22);
            this.saleDateTextbox.TabIndex = 4;
            this.saleDateTextbox.TextChanged += new System.EventHandler(this.saleDateTextbox_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(367, 126);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Date of Sale";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(111, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(392, 32);
            this.label3.TabIndex = 5;
            this.label3.Text = "Krishana Sales and Industries";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(137, 64);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(352, 25);
            this.label5.TabIndex = 7;
            this.label5.Text = "550/1, Datta Galli, M. Vadgaon, Belgavi";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(163, 90);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(282, 24);
            this.label6.TabIndex = 8;
            this.label6.Text = "(GSTIN No. 29AIOPM5869K1Z8)";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(268, 8);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(69, 24);
            this.label7.TabIndex = 9;
            this.label7.Text = "|| Shri ||";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // netwtTextbox
            // 
            this.netwtTextbox.Location = new System.Drawing.Point(459, 184);
            this.netwtTextbox.Name = "netwtTextbox";
            this.netwtTextbox.ReadOnly = true;
            this.netwtTextbox.Size = new System.Drawing.Size(102, 22);
            this.netwtTextbox.TabIndex = 15;
            this.netwtTextbox.TextChanged += new System.EventHandler(this.netwtTextbox_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(372, 187);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(78, 17);
            this.label9.TabIndex = 14;
            this.label9.Text = "Net Weight";
            this.label9.Click += new System.EventHandler(this.label9_Click);
            // 
            // amountTextbox
            // 
            this.amountTextbox.Location = new System.Drawing.Point(260, 184);
            this.amountTextbox.Name = "amountTextbox";
            this.amountTextbox.ReadOnly = true;
            this.amountTextbox.Size = new System.Drawing.Size(104, 22);
            this.amountTextbox.TabIndex = 13;
            this.amountTextbox.TextChanged += new System.EventHandler(this.amountTextbox_TextChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(198, 187);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(56, 17);
            this.label10.TabIndex = 12;
            this.label10.Text = "Amount";
            this.label10.Click += new System.EventHandler(this.label10_Click);
            // 
            // qualityTextbox
            // 
            this.qualityTextbox.Location = new System.Drawing.Point(459, 151);
            this.qualityTextbox.Name = "qualityTextbox";
            this.qualityTextbox.ReadOnly = true;
            this.qualityTextbox.Size = new System.Drawing.Size(102, 22);
            this.qualityTextbox.TabIndex = 17;
            this.qualityTextbox.TextChanged += new System.EventHandler(this.qualityTextbox_TextChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(403, 154);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(52, 17);
            this.label11.TabIndex = 16;
            this.label11.Text = "Quality";
            this.label11.Click += new System.EventHandler(this.label11_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(191, 625);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(205, 33);
            this.button1.TabIndex = 1;
            this.button1.Text = "Print";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // printPreviewDialog1
            // 
            this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog1.Document = this.printDocument1;
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.Visible = false;
            // 
            // customerNameTextbox
            // 
            this.customerNameTextbox.Location = new System.Drawing.Point(142, 153);
            this.customerNameTextbox.Name = "customerNameTextbox";
            this.customerNameTextbox.ReadOnly = true;
            this.customerNameTextbox.Size = new System.Drawing.Size(222, 22);
            this.customerNameTextbox.TabIndex = 11;
            this.customerNameTextbox.TextChanged += new System.EventHandler(this.customerNameTextbox_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(27, 156);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(109, 17);
            this.label8.TabIndex = 10;
            this.label8.Text = "Customer Name";
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // rateTB
            // 
            this.rateTB.Location = new System.Drawing.Point(73, 184);
            this.rateTB.Name = "rateTB";
            this.rateTB.ReadOnly = true;
            this.rateTB.Size = new System.Drawing.Size(102, 22);
            this.rateTB.TabIndex = 19;
            this.rateTB.TextChanged += new System.EventHandler(this.rateTB_TextChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(27, 187);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(38, 17);
            this.label12.TabIndex = 18;
            this.label12.Text = "Rate";
            this.label12.Click += new System.EventHandler(this.label12_Click);
            // 
            // printDO
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(589, 665);
            this.Controls.Add(this.rateTB);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.qualityTextbox);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.netwtTextbox);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.amountTextbox);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.customerNameTextbox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.saleDateTextbox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.donoTextbox);
            this.Controls.Add(this.label1);
            this.Name = "printDO";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "printDO";
            this.Load += new System.EventHandler(this.printDO_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox donoTextbox;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox saleDateTextbox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox netwtTextbox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox amountTextbox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox qualityTextbox;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button button1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private System.Windows.Forms.TextBox customerNameTextbox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox rateTB;
        private System.Windows.Forms.Label label12;
    }
}