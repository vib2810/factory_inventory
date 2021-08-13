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
    public partial class M_V5_OpeningUC : UserControl
    {
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            Console.WriteLine(keyData.ToString());
            if (keyData == (Keys.D1) || keyData == (Keys.NumPad1))
            {
                this.button1.Focus();
                this.button1.PerformClick();
                return false;
            }

            if (keyData == (Keys.D1 | Keys.Shift))
            {
                this.button4.Focus();
                this.button4.PerformClick();
                return false;
            }

            if (keyData == (Keys.D2) || keyData == (Keys.NumPad2))
            {
                this.button2.Focus();
                this.button2.PerformClick();
                return false;
            }
           
            if (keyData == (Keys.D3) || keyData == (Keys.NumPad3))
            {
                this.button6.Focus();
                this.button6.PerformClick();
                return false;
            }
            
            if (keyData == (Keys.D4) || keyData == (Keys.NumPad4))
            {
                this.button3.Focus();
                this.button3.PerformClick();
                return false;
            }
            
            return base.ProcessCmdKey(ref msg, keyData);
        }
        DbConnect c = new DbConnect();
        public M_V5_OpeningUC()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (c.check_login_val() == false) return;
            M_V5_cartonProductionOpeningForm f = new M_V5_cartonProductionOpeningForm();
            Global.background.show_form(f);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (c.isHistoryFormOpen(100) == false)
            {
                M_V_history f = new M_V_history(100);
                Global.background.show_form(f);
            }
        }
    }
}
