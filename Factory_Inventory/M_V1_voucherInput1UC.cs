using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace Factory_Inventory
{
    public partial class M_V1_voucherInput1UC : UserControl
    {
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            Console.WriteLine(keyData.ToString());
            if (keyData == (Keys.D1) || keyData == (Keys.NumPad1))
            {
                this.button1.PerformClick();
                return false;
            }
            if (keyData == (Keys.D1 | Keys.Shift))
            {
                this.button2.PerformClick();
                return false;
            }
            
            if (keyData == (Keys.D2) || keyData == (Keys.NumPad2))
            {
                this.button4.PerformClick();
                return false;
            }
            if (keyData == (Keys.D2 | Keys.Shift))
            {
                this.button3.PerformClick();
                return false;
            }
            
            if (keyData == (Keys.D3) || keyData == (Keys.NumPad3))
            {
                this.button6.PerformClick();
                return false;
            }
            if (keyData == (Keys.D3 | Keys.Shift))
            {
                this.button5.PerformClick();
                return false;
            }
            
            if (keyData == (Keys.D4) || keyData == (Keys.NumPad4))
            {
                this.button8.PerformClick();
                return false;
            }
            if (keyData == (Keys.D4 | Keys.Shift))
            {
                this.button7.PerformClick();
                return false;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        public M_V1_voucherInput1UC()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(M_V1_cartonInwardForm))
                {
                    form.WindowState = FormWindowState.Normal;
                    form.Activate();
                    return;
                }
            }
            M_V1_cartonInwardForm f = new M_V1_cartonInwardForm();
            f.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            M_V_history f = new M_V_history(1);
            f.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(M_V1_cartonTwistForm))
                {
                    form.WindowState = FormWindowState.Normal;
                    form.Activate();
                    return;
                }
            }
            M_V1_cartonTwistForm f = new M_V1_cartonTwistForm();
            f.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            M_V_history f = new M_V_history(2);
            f.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(M_VC_cartonSalesForm))
                {
                    form.WindowState = FormWindowState.Normal;
                    form.Activate();
                    return;
                }
            }
            M_VC_cartonSalesForm f = new M_VC_cartonSalesForm("Carton");
            f.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            M_V_history f = new M_V_history(3);
            f.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            M_VC_addBill f = new M_VC_addBill("Carton");
            f.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            M_V_history f = new M_V_history(10);
            f.Show();
        }
    }
}
