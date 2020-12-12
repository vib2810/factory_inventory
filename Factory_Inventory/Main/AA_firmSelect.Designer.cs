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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AA_firmSelect));
            this.factoryButton = new System.Windows.Forms.Button();
            this.backupAllButton = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Firm_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Current_User = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.localButton = new System.Windows.Forms.Button();
            this.enterButton = new System.Windows.Forms.Button();
            this.tradingButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.createButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // factoryButton
            // 
            this.factoryButton.Location = new System.Drawing.Point(12, 120);
            this.factoryButton.Name = "factoryButton";
            this.factoryButton.Size = new System.Drawing.Size(165, 60);
            this.factoryButton.TabIndex = 0;
            this.factoryButton.Text = "Factory";
            this.factoryButton.UseVisualStyleBackColor = true;
            this.factoryButton.Click += new System.EventHandler(this.factoryButton_Click);
            // 
            // backupAllButton
            // 
            this.backupAllButton.Location = new System.Drawing.Point(15, 433);
            this.backupAllButton.Name = "backupAllButton";
            this.backupAllButton.Size = new System.Drawing.Size(162, 42);
            this.backupAllButton.TabIndex = 2;
            this.backupAllButton.Text = "Backup All";
            this.backupAllButton.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Firm_Name,
            this.Current_User});
            this.dataGridView1.Location = new System.Drawing.Point(196, 120);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(514, 355);
            this.dataGridView1.TabIndex = 3;
            // 
            // Firm_Name
            // 
            this.Firm_Name.HeaderText = "Firm Name";
            this.Firm_Name.MinimumWidth = 6;
            this.Firm_Name.Name = "Firm_Name";
            this.Firm_Name.ReadOnly = true;
            // 
            // Current_User
            // 
            this.Current_User.HeaderText = "Current User";
            this.Current_User.MinimumWidth = 6;
            this.Current_User.Name = "Current_User";
            this.Current_User.ReadOnly = true;
            // 
            // localButton
            // 
            this.localButton.BackColor = System.Drawing.Color.Silver;
            this.localButton.Location = new System.Drawing.Point(243, 37);
            this.localButton.Name = "localButton";
            this.localButton.Size = new System.Drawing.Size(634, 55);
            this.localButton.TabIndex = 4;
            this.localButton.Text = "Server Select";
            this.localButton.UseVisualStyleBackColor = false;
            this.localButton.Click += new System.EventHandler(this.localButton_Click);
            // 
            // enterButton
            // 
            this.enterButton.Location = new System.Drawing.Point(728, 421);
            this.enterButton.Name = "enterButton";
            this.enterButton.Size = new System.Drawing.Size(149, 54);
            this.enterButton.TabIndex = 8;
            this.enterButton.Text = "Enter";
            this.enterButton.UseVisualStyleBackColor = true;
            this.enterButton.Click += new System.EventHandler(this.enterButton_Click);
            // 
            // tradingButton
            // 
            this.tradingButton.Location = new System.Drawing.Point(12, 186);
            this.tradingButton.Name = "tradingButton";
            this.tradingButton.Size = new System.Drawing.Size(165, 60);
            this.tradingButton.TabIndex = 9;
            this.tradingButton.Text = "Trading";
            this.tradingButton.UseVisualStyleBackColor = true;
            this.tradingButton.Click += new System.EventHandler(this.tradingButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.BackColor = System.Drawing.Color.Tomato;
            this.deleteButton.Location = new System.Drawing.Point(728, 186);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(149, 60);
            this.deleteButton.TabIndex = 10;
            this.deleteButton.Text = "Delete Firm";
            this.deleteButton.UseVisualStyleBackColor = false;
            // 
            // createButton
            // 
            this.createButton.BackColor = System.Drawing.Color.GreenYellow;
            this.createButton.Location = new System.Drawing.Point(728, 120);
            this.createButton.Name = "createButton";
            this.createButton.Size = new System.Drawing.Size(149, 60);
            this.createButton.TabIndex = 11;
            this.createButton.Text = "Create New Firm";
            this.createButton.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(239, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 20);
            this.label1.TabIndex = 12;
            this.label1.Text = "Server:";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(207, 80);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 13;
            this.pictureBox1.TabStop = false;
            // 
            // AA_firmSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(897, 491);
            this.Controls.Add(this.pictureBox1);
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
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
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
        private System.Windows.Forms.DataGridViewTextBoxColumn Firm_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn Current_User;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}