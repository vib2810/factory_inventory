using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Factory_Inventory.Factory_Classes;

namespace Factory_Inventory
{
    public partial class M_1_inventoryUC : UserControl
    {
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.O)
            {
                this.onDateButton.PerformClick(); ;
                return false;
            }
            if (keyData == Keys.F)
            {
                this.fromtoButton.PerformClick();
                return false;
            }
            if (keyData == Keys.T)
            {
                this.tablesButton.PerformClick();
                return false;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private DbConnect c;
        public M_1_inventoryUC()
        {
            InitializeComponent();
            this.c = new DbConnect();
            m_I1_Tables1.Hide();
        }
        public void decolour_all_buttons()
        {
            var buttons = this.Controls
             .OfType<Button>()
             .Where(x => x.Name.EndsWith("Button"));

            foreach (var button in buttons)
            {
                button.BackColor = SystemColors.ControlDark;
            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            m_I1_Tables1.Hide();
            M_I1_OnDate f = new M_I1_OnDate();
            Global.background.show_form(f);
            this.decolour_all_buttons();
            this.onDateButton.BackColor = Color.Orange;
        }

        private void tablesButton_Click(object sender, EventArgs e)
        {
            m_I1_Tables1.Show();
            m_I1_Tables1.Focus();
            this.decolour_all_buttons();
            this.tablesButton.BackColor = Color.Orange;
        }

        private void fromtoButton_Click(object sender, EventArgs e)
        {
            m_I1_Tables1.Hide();
            M_I1_FromToDate f = new M_I1_FromToDate();
            Global.background.show_form(f);
            this.decolour_all_buttons();
            this.fromtoButton.BackColor = Color.Orange;
        }
    }
}
