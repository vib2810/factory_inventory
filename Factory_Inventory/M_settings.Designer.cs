﻿namespace Factory_Inventory
{
    partial class M_settings
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.displaytabPage = new System.Windows.Forms.TabPage();
            this.m_S_displayUC1 = new Factory_Inventory.M_S_displayUC();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.m_S_defaultUC1 = new Factory_Inventory.M_S_defaultUC();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.m_S_defaultUC2 = new Factory_Inventory.M_S_defaultUC();
            this.tabControl1.SuspendLayout();
            this.displaytabPage.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.displaytabPage);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(589, 366);
            this.tabControl1.TabIndex = 0;
            // 
            // displaytabPage
            // 
            this.displaytabPage.Controls.Add(this.m_S_displayUC1);
            this.displaytabPage.Location = new System.Drawing.Point(4, 22);
            this.displaytabPage.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.displaytabPage.Name = "displaytabPage";
            this.displaytabPage.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.displaytabPage.Size = new System.Drawing.Size(581, 340);
            this.displaytabPage.TabIndex = 1;
            this.displaytabPage.Text = "Display";
            this.displaytabPage.UseVisualStyleBackColor = true;
            // 
            // m_S_displayUC1
            // 
            this.m_S_displayUC1.Location = new System.Drawing.Point(0, 0);
            this.m_S_displayUC1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.m_S_displayUC1.Name = "m_S_displayUC1";
            this.m_S_displayUC1.Size = new System.Drawing.Size(594, 342);
            this.m_S_displayUC1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.m_S_defaultUC1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabPage1.Size = new System.Drawing.Size(581, 340);
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Text = "Print";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // m_S_defaultUC1
            // 
            this.m_S_defaultUC1.Location = new System.Drawing.Point(0, 0);
            this.m_S_defaultUC1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.m_S_defaultUC1.Name = "m_S_defaultUC1";
            this.m_S_defaultUC1.Size = new System.Drawing.Size(594, 342);
            this.m_S_defaultUC1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.m_S_defaultUC2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(581, 340);
            this.tabPage2.TabIndex = 3;
            this.tabPage2.Text = "Default";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // m_S_defaultUC2
            // 
            this.m_S_defaultUC2.Location = new System.Drawing.Point(0, 0);
            this.m_S_defaultUC2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.m_S_defaultUC2.Name = "m_S_defaultUC2";
            this.m_S_defaultUC2.Size = new System.Drawing.Size(594, 342);
            this.m_S_defaultUC2.TabIndex = 0;
            // 
            // M_settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(589, 366);
            this.Controls.Add(this.tabControl1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "M_settings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.M_settings_Load);
            this.tabControl1.ResumeLayout(false);
            this.displaytabPage.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage displaytabPage;
        private M_S_displayUC m_S_displayUC1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private M_S_defaultUC m_S_defaultUC1;
        private M_S_defaultUC m_S_defaultUC2;
    }
}