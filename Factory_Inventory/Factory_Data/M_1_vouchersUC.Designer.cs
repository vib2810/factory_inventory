namespace Factory_Inventory
{
    partial class M_1_vouchersUC
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.voucherLabel = new System.Windows.Forms.Label();
            this.cartonButton = new System.Windows.Forms.Button();
            this.trayProductionButton = new System.Windows.Forms.Button();
            this.cartonProductionButton = new System.Windows.Forms.Button();
            this.printButton = new System.Windows.Forms.Button();
            this.masterButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.openingStockButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.voucherInput1UC1 = new Factory_Inventory.M_V1_voucherInput1UC();
            this.m_V2_voucherInput2UC1 = new Factory_Inventory.M_V2_voucherInput2UC();
            this.m_V3_voucherInput3UC1 = new Factory_Inventory.M_V3_voucherInput3UC();
            this.m_V4_printUC1 = new Factory_Inventory.M_V4_printUC();
            this.m_V5_OpeningUC1 = new Factory_Inventory.M_V5_OpeningUC();
            this.SuspendLayout();
            // 
            // voucherLabel
            // 
            this.voucherLabel.AutoSize = true;
            this.voucherLabel.Location = new System.Drawing.Point(13, 18);
            this.voucherLabel.Name = "voucherLabel";
            this.voucherLabel.Size = new System.Drawing.Size(100, 17);
            this.voucherLabel.TabIndex = 0;
            this.voucherLabel.Text = "Voucher Menu";
            // 
            // cartonButton
            // 
            this.cartonButton.BackColor = System.Drawing.SystemColors.ControlDark;
            this.cartonButton.Location = new System.Drawing.Point(16, 40);
            this.cartonButton.Name = "cartonButton";
            this.cartonButton.Size = new System.Drawing.Size(130, 45);
            this.cartonButton.TabIndex = 1;
            this.cartonButton.Text = "Carton";
            this.cartonButton.UseVisualStyleBackColor = false;
            this.cartonButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // trayProductionButton
            // 
            this.trayProductionButton.BackColor = System.Drawing.SystemColors.ControlDark;
            this.trayProductionButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.trayProductionButton.Location = new System.Drawing.Point(16, 90);
            this.trayProductionButton.Name = "trayProductionButton";
            this.trayProductionButton.Size = new System.Drawing.Size(130, 45);
            this.trayProductionButton.TabIndex = 2;
            this.trayProductionButton.Text = "Tray Production";
            this.trayProductionButton.UseVisualStyleBackColor = false;
            this.trayProductionButton.Click += new System.EventHandler(this.trayProductionButton_Click);
            // 
            // cartonProductionButton
            // 
            this.cartonProductionButton.BackColor = System.Drawing.SystemColors.ControlDark;
            this.cartonProductionButton.Location = new System.Drawing.Point(16, 141);
            this.cartonProductionButton.Name = "cartonProductionButton";
            this.cartonProductionButton.Size = new System.Drawing.Size(130, 45);
            this.cartonProductionButton.TabIndex = 3;
            this.cartonProductionButton.Text = "Carton Production";
            this.cartonProductionButton.UseVisualStyleBackColor = false;
            this.cartonProductionButton.Click += new System.EventHandler(this.button3_Click);
            // 
            // printButton
            // 
            this.printButton.BackColor = System.Drawing.SystemColors.ControlDark;
            this.printButton.Location = new System.Drawing.Point(16, 243);
            this.printButton.Name = "printButton";
            this.printButton.Size = new System.Drawing.Size(130, 45);
            this.printButton.TabIndex = 4;
            this.printButton.Text = "Print";
            this.printButton.UseVisualStyleBackColor = false;
            this.printButton.Click += new System.EventHandler(this.button4_Click);
            // 
            // masterButton
            // 
            this.masterButton.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.masterButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.masterButton.Location = new System.Drawing.Point(16, 342);
            this.masterButton.Name = "masterButton";
            this.masterButton.Size = new System.Drawing.Size(130, 66);
            this.masterButton.TabIndex = 7;
            this.masterButton.Text = "Master";
            this.masterButton.UseVisualStyleBackColor = false;
            this.masterButton.Click += new System.EventHandler(this.editCNameQualityButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Gainsboro;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(129, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(15, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "C";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Gainsboro;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(127, 269);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(15, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "P";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Gainsboro;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(129, 171);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(16, 15);
            this.label3.TabIndex = 0;
            this.label3.Text = "N";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Gainsboro;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(129, 120);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(14, 15);
            this.label4.TabIndex = 0;
            this.label4.Text = "T";
            // 
            // openingStockButton
            // 
            this.openingStockButton.BackColor = System.Drawing.SystemColors.ControlDark;
            this.openingStockButton.Location = new System.Drawing.Point(16, 192);
            this.openingStockButton.Name = "openingStockButton";
            this.openingStockButton.Size = new System.Drawing.Size(130, 45);
            this.openingStockButton.TabIndex = 6;
            this.openingStockButton.Text = "Opening Stock";
            this.openingStockButton.UseVisualStyleBackColor = false;
            this.openingStockButton.Click += new System.EventHandler(this.openingStockButton_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Gainsboro;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(126, 218);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(16, 15);
            this.label5.TabIndex = 0;
            this.label5.Text = "O";
            // 
            // voucherInput1UC1
            // 
            this.voucherInput1UC1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(215)))), ((int)(((byte)(205)))));
            this.voucherInput1UC1.Location = new System.Drawing.Point(165, 0);
            this.voucherInput1UC1.Name = "voucherInput1UC1";
            this.voucherInput1UC1.Size = new System.Drawing.Size(514, 432);
            this.voucherInput1UC1.TabIndex = 6;
            // 
            // m_V2_voucherInput2UC1
            // 
            this.m_V2_voucherInput2UC1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(242)))), ((int)(((byte)(204)))));
            this.m_V2_voucherInput2UC1.Location = new System.Drawing.Point(165, 0);
            this.m_V2_voucherInput2UC1.Name = "m_V2_voucherInput2UC1";
            this.m_V2_voucherInput2UC1.Size = new System.Drawing.Size(514, 432);
            this.m_V2_voucherInput2UC1.TabIndex = 8;
            // 
            // m_V3_voucherInput3UC1
            // 
            this.m_V3_voucherInput3UC1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(222)))), ((int)(((byte)(240)))));
            this.m_V3_voucherInput3UC1.ImeMode = System.Windows.Forms.ImeMode.On;
            this.m_V3_voucherInput3UC1.Location = new System.Drawing.Point(165, 1);
            this.m_V3_voucherInput3UC1.Name = "m_V3_voucherInput3UC1";
            this.m_V3_voucherInput3UC1.Size = new System.Drawing.Size(514, 432);
            this.m_V3_voucherInput3UC1.TabIndex = 9;
            // 
            // m_V4_printUC1
            // 
            this.m_V4_printUC1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(234)))), ((int)(((byte)(211)))));
            this.m_V4_printUC1.Location = new System.Drawing.Point(165, 0);
            this.m_V4_printUC1.Name = "m_V4_printUC1";
            this.m_V4_printUC1.Size = new System.Drawing.Size(514, 432);
            this.m_V4_printUC1.TabIndex = 10;
            // 
            // m_V5_OpeningUC1
            // 
            this.m_V5_OpeningUC1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.m_V5_OpeningUC1.Location = new System.Drawing.Point(165, 1);
            this.m_V5_OpeningUC1.Name = "m_V5_OpeningUC1";
            this.m_V5_OpeningUC1.Size = new System.Drawing.Size(514, 432);
            this.m_V5_OpeningUC1.TabIndex = 11;
            // 
            // M_1_vouchersUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.masterButton);
            this.Controls.Add(this.printButton);
            this.Controls.Add(this.cartonProductionButton);
            this.Controls.Add(this.trayProductionButton);
            this.Controls.Add(this.cartonButton);
            this.Controls.Add(this.voucherLabel);
            this.Controls.Add(this.m_V4_printUC1);
            this.Controls.Add(this.m_V3_voucherInput3UC1);
            this.Controls.Add(this.m_V2_voucherInput2UC1);
            this.Controls.Add(this.voucherInput1UC1);
            this.Controls.Add(this.m_V5_OpeningUC1);
            this.Controls.Add(this.openingStockButton);
            this.Name = "M_1_vouchersUC";
            this.Size = new System.Drawing.Size(679, 433);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label voucherLabel;
        private System.Windows.Forms.Button cartonButton;
        private System.Windows.Forms.Button trayProductionButton;
        private System.Windows.Forms.Button cartonProductionButton;
        private System.Windows.Forms.Button printButton;
        private M_V1_voucherInput1UC voucherInput1UC1;
        private System.Windows.Forms.Button masterButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button openingStockButton;
        private System.Windows.Forms.Label label5;
        private M_V2_voucherInput2UC m_V2_voucherInput2UC1;
        private M_V3_voucherInput3UC m_V3_voucherInput3UC1;
        private M_V4_printUC m_V4_printUC1;
        private M_V5_OpeningUC m_V5_OpeningUC1;
    }
}
