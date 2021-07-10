using Factory_Inventory.Factory_Classes;

namespace Factory_Inventory
{
    partial class M_1_MainS
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
            this.vouchersButton = new System.Windows.Forms.Button();
            this.inventoryButton = new System.Windows.Forms.Button();
            this.usernameLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.inventoryUC = new Factory_Inventory.M_1_inventoryUC();
            this.vouchersUC = new Factory_Inventory.M_1_vouchersUC();
            this.SuspendLayout();
            // 
            // vouchersButton
            // 
            this.vouchersButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.vouchersButton.BackColor = System.Drawing.Color.DarkGray;
            this.vouchersButton.Location = new System.Drawing.Point(775, 72);
            this.vouchersButton.Name = "vouchersButton";
            this.vouchersButton.Size = new System.Drawing.Size(111, 58);
            this.vouchersButton.TabIndex = 3;
            this.vouchersButton.Text = "&Vouchers";
            this.vouchersButton.UseVisualStyleBackColor = false;
            this.vouchersButton.Click += new System.EventHandler(this.vouchersButton_Click_1);
            // 
            // inventoryButton
            // 
            this.inventoryButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.inventoryButton.BackColor = System.Drawing.Color.DarkGray;
            this.inventoryButton.Location = new System.Drawing.Point(775, 136);
            this.inventoryButton.Name = "inventoryButton";
            this.inventoryButton.Size = new System.Drawing.Size(111, 58);
            this.inventoryButton.TabIndex = 4;
            this.inventoryButton.Text = "&Inventory";
            this.inventoryButton.UseVisualStyleBackColor = false;
            this.inventoryButton.Click += new System.EventHandler(this.inventoryButton_Click_1);
            // 
            // usernameLabel
            // 
            this.usernameLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.usernameLabel.AutoSize = true;
            this.usernameLabel.Cursor = System.Windows.Forms.Cursors.Default;
            this.usernameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usernameLabel.Location = new System.Drawing.Point(34, 20);
            this.usernameLabel.Name = "usernameLabel";
            this.usernameLabel.Size = new System.Drawing.Size(195, 32);
            this.usernameLabel.TabIndex = 0;
            this.usernameLabel.Text = "Twist Factory";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Gainsboro;
            this.label2.Cursor = System.Windows.Forms.Cursors.Default;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(871, 173);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "I";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Gainsboro;
            this.label1.Cursor = System.Windows.Forms.Cursors.Default;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(869, 113);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "V";
            // 
            // inventoryUC
            // 
            this.inventoryUC.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.inventoryUC.Location = new System.Drawing.Point(69, 72);
            this.inventoryUC.Name = "inventoryUC";
            this.inventoryUC.Size = new System.Drawing.Size(671, 427);
            this.inventoryUC.TabIndex = 0;
            // 
            // vouchersUC
            // 
            this.vouchersUC.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.vouchersUC.Location = new System.Drawing.Point(69, 72);
            this.vouchersUC.Name = "vouchersUC";
            this.vouchersUC.Size = new System.Drawing.Size(671, 427);
            this.vouchersUC.TabIndex = 0;
            this.vouchersUC.Load += new System.EventHandler(this.vouchersUC_Load);
            // 
            // M_1_MainS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(950, 550);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.usernameLabel);
            this.Controls.Add(this.inventoryButton);
            this.Controls.Add(this.vouchersButton);
            this.Controls.Add(this.vouchersUC);
            this.Controls.Add(this.inventoryUC);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "M_1_MainS";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Factory Inventory - Home";
            this.Load += new System.EventHandler(this.MainS_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button vouchersButton;
        private System.Windows.Forms.Button inventoryButton;
        private System.Windows.Forms.Label usernameLabel;
        private M_1_vouchersUC vouchersUC;
        private M_1_inventoryUC inventoryUC;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}