namespace Factory_Inventory
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
            this.printtabPage = new System.Windows.Forms.TabPage();
            this.displaytabPage = new System.Windows.Forms.TabPage();
            this.m_S_print1 = new Factory_Inventory.M_S_printUC();
            this.m_S_display1 = new Factory_Inventory.M_S_displayUC();
            this.tabControl1.SuspendLayout();
            this.printtabPage.SuspendLayout();
            this.displaytabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.printtabPage);
            this.tabControl1.Controls.Add(this.displaytabPage);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(800, 450);
            this.tabControl1.TabIndex = 0;
            // 
            // printtabPage
            // 
            this.printtabPage.Controls.Add(this.m_S_print1);
            this.printtabPage.Location = new System.Drawing.Point(4, 25);
            this.printtabPage.Name = "printtabPage";
            this.printtabPage.Padding = new System.Windows.Forms.Padding(3);
            this.printtabPage.Size = new System.Drawing.Size(792, 421);
            this.printtabPage.TabIndex = 0;
            this.printtabPage.Text = "Print";
            this.printtabPage.UseVisualStyleBackColor = true;
            // 
            // displaytabPage
            // 
            this.displaytabPage.Controls.Add(this.m_S_display1);
            this.displaytabPage.Location = new System.Drawing.Point(4, 25);
            this.displaytabPage.Name = "displaytabPage";
            this.displaytabPage.Padding = new System.Windows.Forms.Padding(3);
            this.displaytabPage.Size = new System.Drawing.Size(792, 421);
            this.displaytabPage.TabIndex = 1;
            this.displaytabPage.Text = "Display";
            this.displaytabPage.UseVisualStyleBackColor = true;
            // 
            // m_S_print1
            // 
            this.m_S_print1.Location = new System.Drawing.Point(0, 0);
            this.m_S_print1.Name = "m_S_print1";
            this.m_S_print1.Size = new System.Drawing.Size(792, 421);
            this.m_S_print1.TabIndex = 0;
            // 
            // m_S_display1
            // 
            this.m_S_display1.Location = new System.Drawing.Point(0, 3);
            this.m_S_display1.Name = "m_S_display1";
            this.m_S_display1.Size = new System.Drawing.Size(792, 421);
            this.m_S_display1.TabIndex = 0;
            // 
            // M_settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabControl1);
            this.Name = "M_settings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "M_settings";
            this.Load += new System.EventHandler(this.M_settings_Load);
            this.tabControl1.ResumeLayout(false);
            this.printtabPage.ResumeLayout(false);
            this.displaytabPage.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage printtabPage;
        private System.Windows.Forms.TabPage displaytabPage;
        private M_S_displayUC m_S_display1;
        private M_S_printUC m_S_print1;
    }
}