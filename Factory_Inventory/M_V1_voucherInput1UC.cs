using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Factory_Inventory
{
    public partial class M_V1_voucherInput1UC : UserControl
    {
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
