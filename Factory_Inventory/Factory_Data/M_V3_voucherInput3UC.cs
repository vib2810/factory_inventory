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
using Factory_Inventory.Factory_Data;

namespace Factory_Inventory
{
    public partial class M_V3_voucherInput3UC : UserControl
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
                this.button2.Focus();
                this.button2.PerformClick();
                return false;
            }

            if (keyData == (Keys.D2) || keyData == (Keys.NumPad2))
            {
                this.button6.Focus();
                this.button6.PerformClick();
                return false;
            }
            if (keyData == (Keys.D2 | Keys.Shift))
            {
                this.button5.Focus();
                this.button5.PerformClick();
                return false;
            }

            if (keyData == (Keys.D3) || keyData == (Keys.NumPad3))
            {
                this.button8.Focus();
                this.button8.PerformClick();
                return false;
            }
            if (keyData == (Keys.D3 | Keys.Shift))
            {
                this.button7.Focus();
                this.button7.PerformClick();
                return false;
            }

            if (keyData == (Keys.D4) || keyData == (Keys.NumPad4))
            {
                this.paymentsButton.Focus();
                this.paymentsButton.PerformClick();
                return false;
            }
            if (keyData == (Keys.D4 | Keys.Shift))
            {
                this.paymentsHistoryButton.Focus();
                this.paymentsHistoryButton.PerformClick();
                return false;
            }

            if (keyData == (Keys.D5) || keyData == (Keys.NumPad5))
            {
                this.button4.Focus();
                this.button4.PerformClick();
                return false;
            }
            if (keyData == (Keys.D4 | Keys.Shift))
            {
                this.button3.Focus();
                this.button3.PerformClick();
                return false;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        DbConnect c = new DbConnect();
        public M_V3_voucherInput3UC()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (c.check_login_val() == false) return;
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(M_V3_cartonProductionForm))
                {
                    form.WindowState = FormWindowState.Normal;
                    form.Activate();
                    return;
                }
            }
            M_V3_cartonProductionForm f = new M_V3_cartonProductionForm();
            Global.background.show_form(f);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (c.isHistoryFormOpen(8) == false)
            {
                M_V_history f = new M_V_history(8);
                Global.background.show_form(f);
            }
        }
        private void button6_Click(object sender, EventArgs e)
        {
            if (c.check_login_val() == false) return;
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(M_VC_cartonSalesForm))
                {
                    form.WindowState = FormWindowState.Normal;
                    form.Activate();
                    return;
                }
            }
            M_VC_cartonSalesForm f = new M_VC_cartonSalesForm("Carton_Produced");
            Global.background.show_form(f);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (c.isHistoryFormOpen(9) == false)
            {
                M_V_history f = new M_V_history(9);
                Global.background.show_form(f);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (c.check_login_val() == false) return;
            M_VC_addBill f = new M_VC_addBill("Carton_Produced");
            Global.background.show_form(f);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (c.isHistoryFormOpen(11) == false)
            {
                M_V_history f = new M_V_history(11);
                Global.background.show_form(f);
            }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            if (c.check_login_val() == false) return;
            M_V3_issueToReDyeingForm f = new M_V3_issueToReDyeingForm();
            Global.background.show_form(f);
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            if (c.isHistoryFormOpen(12) == false)
            {
                M_V_history f = new M_V_history(12);
                Global.background.show_form(f);
            }
        }

        private void paymentsButton_Click(object sender, EventArgs e)
        {
            if (c.check_login_val() == false) return;
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(M_V3_paymentsForm))
                {
                    form.WindowState = FormWindowState.Normal;
                    form.Activate();
                    return;
                }
            }
            M_V3_paymentsForm f = new M_V3_paymentsForm();
            Global.background.show_form(f);
        }
    }
}
