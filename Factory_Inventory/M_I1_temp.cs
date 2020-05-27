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
    public partial class M_I1_temp : UserControl
    {
        public M_I1_temp()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            M_V1_cartonInwardForm f = new M_V1_cartonInwardForm();
            Global.background.show_form(f);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            M_V_history f = new M_V_history(1);
            Global.background.show_form(f);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            M_V1_cartonTwistForm f = new M_V1_cartonTwistForm();
            Global.background.show_form(f);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            M_V_history f = new M_V_history(2);
            Global.background.show_form(f);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            M_VC_cartonSalesForm f = new M_VC_cartonSalesForm("Carton");
            Global.background.show_form(f);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            M_V_history f = new M_V_history(3);
            Global.background.show_form(f);
        }
    }
}
