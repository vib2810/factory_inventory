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

    public partial class M_V2_voucherInput2UC : UserControl
    {
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            Console.WriteLine(keyData.ToString());
            if (keyData == (Keys.D1) || keyData == (Keys.NumPad1))
            {
                this.trayVoucherButton.Focus();
                this.trayVoucherButton.PerformClick();
                return false;
            }
            if (keyData == (Keys.D1 | Keys.Shift))
            {
                this.historyButton.Focus();
                this.historyButton.PerformClick();
                return false;
            }

            if (keyData == (Keys.D2) || keyData == (Keys.NumPad2))
            {
                this.button4.Focus();
                this.button4.PerformClick();
                return false;
            }
            if (keyData == (Keys.D2 | Keys.Shift))
            {
                this.button3.Focus();
                this.button3.PerformClick();
                return false;
            }

            if (keyData == (Keys.D3) || keyData == (Keys.NumPad3))
            {
                this.button6.Focus();
                this.button6.PerformClick();
                return false;
            }
            if (keyData == (Keys.D3 | Keys.Shift))
            {
                this.button5.Focus();
                this.button5.PerformClick();
                return false;
            }

            if (keyData == (Keys.D4) || keyData == (Keys.NumPad4))
            {
                this.button1.Focus();
                this.button1.PerformClick();
                return false;
            }
            if (keyData == (Keys.D4 | Keys.Shift))
            {
                this.button2.Focus();
                this.button2.PerformClick();
                return false;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private DbConnect c = new DbConnect();
        public M_V2_voucherInput2UC()
        {
            InitializeComponent();
        }

        private void M_V2_voucherInput2UC_Load(object sender, EventArgs e)
        {

        }

        private void trayVoucherButton_Click_1(object sender, EventArgs e)
        {
            if (c.check_login_val() == false) return;
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(M_V2_trayInputForm))
                {
                    form.WindowState = FormWindowState.Normal;
                    form.Activate();
                    return;
                }
            }
            M_V2_trayInputForm f = new M_V2_trayInputForm();
            Global.background.show_form(f);
        }

        private void historyButton_Click_1(object sender, EventArgs e)
        {
            if (c.isHistoryFormOpen(4) == false)
            {
                M_V_history f = new M_V_history(4);
                Global.background.show_form(f);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (c.check_login_val() == false) return;
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(M_V2_dyeingIssueForm))
                {
                    form.WindowState = FormWindowState.Normal;
                    form.Activate();
                    return;
                }
            }
            M_V2_dyeingIssueForm f = new M_V2_dyeingIssueForm();
            Global.background.show_form(f);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (c.isHistoryFormOpen(5) == false)
            {
                M_V_history f = new M_V_history(5);
                Global.background.show_form(f);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (c.check_login_val() == false) return;
            M_V2_dyeingInwardForm f = new M_V2_dyeingInwardForm("dyeingInward");
            Global.background.show_form(f);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (c.isHistoryFormOpen(6) == false)
            {
                M_V_history f = new M_V_history(6);
                Global.background.show_form(f);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (c.check_login_val() == false) return;
            M_V2_dyeingInwardForm f = new M_V2_dyeingInwardForm("addBill");
            Global.background.show_form(f);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (c.isHistoryFormOpen(7) == false)
            {
                M_V_history f = new M_V_history(7);
                Global.background.show_form(f);
            }
        }
    }
}
