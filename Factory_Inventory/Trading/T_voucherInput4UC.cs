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
    public partial class T_voucherInput4UC : UserControl
    {
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            Console.WriteLine(keyData.ToString());

            if (keyData == (Keys.D1) || keyData == (Keys.NumPad1))
            {
                this.button6.Focus();
                this.button6.PerformClick();
                return false;
            }
            if (keyData == (Keys.D1 | Keys.Shift))
            {
                this.button5.Focus();
                this.button5.PerformClick();
                return false;
            }

            if (keyData == (Keys.D2) || keyData == (Keys.NumPad2))
            {
                this.button8.Focus();
                this.button8.PerformClick();
                return false;
            }
            if (keyData == (Keys.D2 | Keys.Shift))
            {
                this.button7.Focus();
                this.button7.PerformClick();
                return false;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        DbConnect c = new DbConnect();
        public T_voucherInput4UC()
        {
            InitializeComponent();
        }
        private void button6_Click(object sender, EventArgs e)
        {
            if (c.check_login_val() == false) return;
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(T_V3_cartonSalesForm))
                {
                    form.WindowState = FormWindowState.Normal;
                    form.Activate();
                    return;
                }
            }
            T_V3_cartonSalesForm f = new T_V3_cartonSalesForm();
            Global.background.show_form(f);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (c.isHistoryFormOpen(15) == false)
            {
                M_V_history f = new M_V_history(15);
                Global.background.show_form(f);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (c.check_login_val() == false) return;
            T_V3_addBill f = new T_V3_addBill();
            Global.background.show_form(f);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (c.isHistoryFormOpen(11) == false)
            {
                M_V_history f = new M_V_history(16);
                Global.background.show_form(f);
            }
        }
    }
}
