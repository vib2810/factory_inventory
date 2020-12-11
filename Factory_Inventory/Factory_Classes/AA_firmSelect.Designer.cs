namespace Factory_Inventory.Factory_Classes
{
    partial class AA_firmSelect
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
            this.factoryButton = new System.Windows.Forms.Button();
            this.backupAllButton = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.localButton = new System.Windows.Forms.Button();
            this.enterButton = new System.Windows.Forms.Button();
            this.tradingButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.createButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // factoryButton
            // 
            this.factoryButton.Location = new System.Drawing.Point(24, 65);
            this.factoryButton.Name = "factoryButton";
            this.factoryButton.Size = new System.Drawing.Size(130, 61);
            this.factoryButton.TabIndex = 0;
            this.factoryButton.Text = "Factory";
            this.factoryButton.UseVisualStyleBackColor = true;
            // 
            // backupAllButton
            // 
            this.backupAllButton.Location = new System.Drawing.Point(22, 418);
            this.backupAllButton.Name = "backupAllButton";
            this.backupAllButton.Size = new System.Drawing.Size(130, 65);
            this.backupAllButton.TabIndex = 2;
            this.backupAllButton.Text = "Backup All";
            this.backupAllButton.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(175, 65);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(623, 355);
            this.dataGridView1.TabIndex = 3;
            // 
            // localButton
            // 
            this.localButton.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.localButton.Location = new System.Drawing.Point(89, 12);
            this.localButton.Name = "localButton";
            this.localButton.Size = new System.Drawing.Size(709, 32);
            this.localButton.TabIndex = 4;
            this.localButton.Text = "Server Select";
            this.localButton.UseVisualStyleBackColor = false;
            this.localButton.Click += new System.EventHandler(this.localButton_Click);
            // 
            // enterButton
            // 
            this.enterButton.Location = new System.Drawing.Point(666, 441);
            this.enterButton.Name = "enterButton";
            this.enterButton.Size = new System.Drawing.Size(132, 42);
            this.enterButton.TabIndex = 8;
            this.enterButton.Text = "Enter";
            this.enterButton.UseVisualStyleBackColor = true;
            // 
            // tradingButton
            // 
            this.tradingButton.Location = new System.Drawing.Point(22, 147);
            this.tradingButton.Name = "tradingButton";
            this.tradingButton.Size = new System.Drawing.Size(130, 61);
            this.tradingButton.TabIndex = 9;
            this.tradingButton.Text = "Trading";
            this.tradingButton.UseVisualStyleBackColor = true;
            // 
            // deleteButton
            // 
            this.deleteButton.Location = new System.Drawing.Point(330, 443);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(103, 42);
            this.deleteButton.TabIndex = 10;
            this.deleteButton.Text = "Delete Firm";
            this.deleteButton.UseVisualStyleBackColor = true;
            // 
            // createButton
            // 
            this.createButton.Location = new System.Drawing.Point(175, 441);
            this.createButton.Name = "createButton";
            this.createButton.Size = new System.Drawing.Size(112, 42);
            this.createButton.TabIndex = 11;
            this.createButton.Text = "Create New Firm";
            this.createButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(20, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 20);
            this.label1.TabIndex = 12;
            this.label1.Text = "Server:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // AA_firmSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(833, 497);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.createButton);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.tradingButton);
            this.Controls.Add(this.enterButton);
            this.Controls.Add(this.localButton);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.backupAllButton);
            this.Controls.Add(this.factoryButton);
            this.Name = "AA_firmSelect";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AA_firmSelect";
            this.Load += new System.EventHandler(this.AA_firmSelect_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button factoryButton;
        private System.Windows.Forms.Button backupAllButton;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button localButton;
        private System.Windows.Forms.Button enterButton;
        private System.Windows.Forms.Button tradingButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Button createButton;
        private System.Windows.Forms.Label label1;
    }
}