﻿namespace Factory_Inventory
{
    partial class M_V_history
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.viewDetailsButton = new System.Windows.Forms.Button();
            this.editDetailsButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(2, 2);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(990, 551);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // viewDetailsButton
            // 
            this.viewDetailsButton.Location = new System.Drawing.Point(1021, 142);
            this.viewDetailsButton.Name = "viewDetailsButton";
            this.viewDetailsButton.Size = new System.Drawing.Size(106, 34);
            this.viewDetailsButton.TabIndex = 1;
            this.viewDetailsButton.Text = "View Details";
            this.viewDetailsButton.UseVisualStyleBackColor = true;
            this.viewDetailsButton.Click += new System.EventHandler(this.viewDetailsButton_Click);
            // 
            // editDetailsButton
            // 
            this.editDetailsButton.Location = new System.Drawing.Point(1021, 263);
            this.editDetailsButton.Name = "editDetailsButton";
            this.editDetailsButton.Size = new System.Drawing.Size(106, 34);
            this.editDetailsButton.TabIndex = 2;
            this.editDetailsButton.Text = "Edit Details";
            this.editDetailsButton.UseVisualStyleBackColor = true;
            this.editDetailsButton.Click += new System.EventHandler(this.editDetailsButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1008, 426);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(332, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Bill Number 0 means\nno bill number has\nbeen given";
            // 
            // M_V_history
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1149, 555);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.editDetailsButton);
            this.Controls.Add(this.viewDetailsButton);
            this.Controls.Add(this.dataGridView1);
            this.Name = "M_V_history";
            this.Text = "History";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button viewDetailsButton;
        private System.Windows.Forms.Button editDetailsButton;
        private System.Windows.Forms.Label label1;
    }
}