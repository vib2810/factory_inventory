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
    public partial class M_V2_voucherInput2UC : UserControl
    {
        public M_V2_voucherInput2UC()
        {
            InitializeComponent();
        }

        private void M_V2_voucherInput2UC_Load(object sender, EventArgs e)
        {

        }

        private void trayVoucherButton_Click_1(object sender, EventArgs e)
        {
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
            f.Show();
        }

        private void historyButton_Click_1(object sender, EventArgs e)
        {
            M_V_history f = new M_V_history(4);
            f.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
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
            f.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            M_V_history f = new M_V_history(5);
            f.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            M_V2_dyeingInwardForm f = new M_V2_dyeingInwardForm("dyeingInward");
            f.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            M_V_history f = new M_V_history(6);
            f.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            M_V2_dyeingInwardForm f = new M_V2_dyeingInwardForm("addBill");
            f.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            M_V_history f = new M_V_history(7);
            f.Show();
        }
    }
}
