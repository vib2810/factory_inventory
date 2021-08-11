using Factory_Inventory.Factory_Classes;

namespace Factory_Inventory
{
    partial class T_Main
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
            this.t_vouchersUC1 = new Factory_Inventory.T_vouchersUC();
            this.t_I_InventoryUC1 = new Factory_Inventory.T_I_InventoryUC();
            this.SuspendLayout();
            // 
            // vouchersButton
            // 
            this.vouchersButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.vouchersButton.BackColor = System.Drawing.Color.DarkGray;
            this.vouchersButton.Location = new System.Drawing.Point(574, 88);
            this.vouchersButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.vouchersButton.Name = "vouchersButton";
            this.vouchersButton.Size = new System.Drawing.Size(83, 31);
            this.vouchersButton.TabIndex = 3;
            this.vouchersButton.Text = "&Vouchers";
            this.vouchersButton.UseVisualStyleBackColor = false;
            this.vouchersButton.Click += new System.EventHandler(this.vouchersButton_Click_1);
            // 
            // inventoryButton
            // 
            this.inventoryButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.inventoryButton.BackColor = System.Drawing.Color.DarkGray;
            this.inventoryButton.Location = new System.Drawing.Point(574, 124);
            this.inventoryButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.inventoryButton.Name = "inventoryButton";
            this.inventoryButton.Size = new System.Drawing.Size(83, 31);
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
            this.usernameLabel.Location = new System.Drawing.Point(26, 16);
            this.usernameLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.usernameLabel.Name = "usernameLabel";
            this.usernameLabel.Size = new System.Drawing.Size(91, 26);
            this.usernameLabel.TabIndex = 0;
            this.usernameLabel.Text = "Trading";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Gainsboro;
            this.label2.Cursor = System.Windows.Forms.Cursors.Default;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(646, 137);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(10, 13);
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
            this.label1.Location = new System.Drawing.Point(642, 102);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "V";
            // 
            // t_vouchersUC1
            // 
            this.t_vouchersUC1.Location = new System.Drawing.Point(52, 58);
            this.t_vouchersUC1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.t_vouchersUC1.Name = "t_vouchersUC1";
            this.t_vouchersUC1.Size = new System.Drawing.Size(509, 352);
            this.t_vouchersUC1.TabIndex = 5;
            // 
            // t_I_InventoryUC1
            // 
            this.t_I_InventoryUC1.Location = new System.Drawing.Point(52, 58);
            this.t_I_InventoryUC1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.t_I_InventoryUC1.Name = "t_I_InventoryUC1";
            this.t_I_InventoryUC1.Size = new System.Drawing.Size(509, 352);
            this.t_I_InventoryUC1.TabIndex = 6;
            // 
            // T_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(712, 447);
            this.Controls.Add(this.t_I_InventoryUC1);
            this.Controls.Add(this.t_vouchersUC1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.usernameLabel);
            this.Controls.Add(this.inventoryButton);
            this.Controls.Add(this.vouchersButton);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "T_Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Trading - Home";
            this.Load += new System.EventHandler(this.MainS_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button vouchersButton;
        private System.Windows.Forms.Button inventoryButton;
        private System.Windows.Forms.Label usernameLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private T_vouchersUC t_vouchersUC1;
        private T_I_InventoryUC t_I_InventoryUC1;
    }
}