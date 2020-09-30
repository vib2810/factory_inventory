namespace Factory_Inventory
{
    partial class printDyeingOutward
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(printDyeingOutward));
            this.label1 = new System.Windows.Forms.Label();
            this.batchnoTextbox = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.outDateTextbox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.customerNameTextbox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.netwtTextbox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.shadeTextbox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.qualityTextbox = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.customerAddressTextbox = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.customergstin = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.hsnnumber = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.rdLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 154);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Invoice Number";
            // 
            // batchnoTextbox
            // 
            this.batchnoTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.batchnoTextbox.Location = new System.Drawing.Point(154, 149);
            this.batchnoTextbox.Name = "batchnoTextbox";
            this.batchnoTextbox.ReadOnly = true;
            this.batchnoTextbox.Size = new System.Drawing.Size(148, 27);
            this.batchnoTextbox.TabIndex = 1;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 273);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(928, 397);
            this.dataGridView1.TabIndex = 2;
            // 
            // outDateTextbox
            // 
            this.outDateTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.outDateTextbox.Location = new System.Drawing.Point(711, 147);
            this.outDateTextbox.Name = "outDateTextbox";
            this.outDateTextbox.ReadOnly = true;
            this.outDateTextbox.Size = new System.Drawing.Size(229, 27);
            this.outDateTextbox.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(603, 154);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Invoice Date";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(283, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(392, 32);
            this.label3.TabIndex = 5;
            this.label3.Text = c.getDefault("Print", "Firm Name");
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(392, 38);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(166, 25);
            this.label4.TabIndex = 6;
            this.label4.Text = "FOR JOB WORK";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(309, 95);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(363, 25);
            this.label5.TabIndex = 7;
            this.label5.Text = c.getDefault("Print", "Address");
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(353, 121);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(251, 20);
            this.label6.TabIndex = 8;
            this.label6.Text = "(GSTIN No. "+ c.getDefault("Print", "GSTIN") + ")";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(439, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(69, 24);
            this.label7.TabIndex = 9;
            this.label7.Text = "|| Shri ||";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // customerNameTextbox
            // 
            this.customerNameTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customerNameTextbox.Location = new System.Drawing.Point(154, 179);
            this.customerNameTextbox.Name = "customerNameTextbox";
            this.customerNameTextbox.ReadOnly = true;
            this.customerNameTextbox.Size = new System.Drawing.Size(287, 27);
            this.customerNameTextbox.TabIndex = 11;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(12, 182);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(131, 20);
            this.label8.TabIndex = 10;
            this.label8.Text = "Customer Name";
            // 
            // netwtTextbox
            // 
            this.netwtTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.netwtTextbox.Location = new System.Drawing.Point(824, 241);
            this.netwtTextbox.Name = "netwtTextbox";
            this.netwtTextbox.ReadOnly = true;
            this.netwtTextbox.Size = new System.Drawing.Size(116, 27);
            this.netwtTextbox.TabIndex = 15;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(726, 244);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(92, 20);
            this.label9.TabIndex = 14;
            this.label9.Text = "Net Weight";
            // 
            // shadeTextbox
            // 
            this.shadeTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.shadeTextbox.Location = new System.Drawing.Point(415, 241);
            this.shadeTextbox.Name = "shadeTextbox";
            this.shadeTextbox.ReadOnly = true;
            this.shadeTextbox.Size = new System.Drawing.Size(266, 27);
            this.shadeTextbox.TabIndex = 13;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(353, 244);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(56, 20);
            this.label10.TabIndex = 12;
            this.label10.Text = "Shade";
            // 
            // qualityTextbox
            // 
            this.qualityTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.qualityTextbox.Location = new System.Drawing.Point(89, 239);
            this.qualityTextbox.Name = "qualityTextbox";
            this.qualityTextbox.ReadOnly = true;
            this.qualityTextbox.Size = new System.Drawing.Size(135, 27);
            this.qualityTextbox.TabIndex = 17;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(12, 244);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(61, 20);
            this.label11.TabIndex = 16;
            this.label11.Text = "Quality";
            // 
            // customerAddressTextbox
            // 
            this.customerAddressTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customerAddressTextbox.Location = new System.Drawing.Point(177, 208);
            this.customerAddressTextbox.Name = "customerAddressTextbox";
            this.customerAddressTextbox.ReadOnly = true;
            this.customerAddressTextbox.Size = new System.Drawing.Size(504, 27);
            this.customerAddressTextbox.TabIndex = 19;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(12, 211);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(149, 20);
            this.label12.TabIndex = 18;
            this.label12.Text = "Customer Address";
            // 
            // customergstin
            // 
            this.customergstin.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customergstin.Location = new System.Drawing.Point(602, 177);
            this.customergstin.Name = "customergstin";
            this.customergstin.ReadOnly = true;
            this.customergstin.Size = new System.Drawing.Size(338, 27);
            this.customergstin.TabIndex = 21;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(461, 182);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(137, 20);
            this.label13.TabIndex = 20;
            this.label13.Text = "Customer GSTIN";
            // 
            // hsnnumber
            // 
            this.hsnnumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hsnnumber.Location = new System.Drawing.Point(824, 208);
            this.hsnnumber.Name = "hsnnumber";
            this.hsnnumber.ReadOnly = true;
            this.hsnnumber.Size = new System.Drawing.Size(116, 27);
            this.hsnnumber.TabIndex = 23;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(729, 211);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(89, 20);
            this.label14.TabIndex = 22;
            this.label14.Text = "HSN Code";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(276, 676);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(401, 33);
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
            // rdLabel
            // 
            this.rdLabel.AutoSize = true;
            this.rdLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdLabel.Location = new System.Drawing.Point(828, 9);
            this.rdLabel.Name = "rdLabel";
            this.rdLabel.Size = new System.Drawing.Size(112, 25);
            this.rdLabel.TabIndex = 0;
            this.rdLabel.Text = "REDYEING";
            this.rdLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.rdLabel.Visible = false;
            // 
            // printDyeingOutward
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(952, 718);
            this.Controls.Add(this.rdLabel);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.hsnnumber);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.customergstin);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.customerAddressTextbox);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.qualityTextbox);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.netwtTextbox);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.shadeTextbox);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.customerNameTextbox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.outDateTextbox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.batchnoTextbox);
            this.Controls.Add(this.label1);
            this.Name = "printDyeingOutward";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Print Dyeing Outward";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox batchnoTextbox;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox outDateTextbox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox customerNameTextbox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox netwtTextbox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox shadeTextbox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox qualityTextbox;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox customerAddressTextbox;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox customergstin;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox hsnnumber;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button button1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private System.Windows.Forms.Label rdLabel;
    }
}