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
    public partial class M_V3_voucherInput3UC : UserControl
    {
        public M_V3_voucherInput3UC()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            M_V3_cartonProductionForm f = new M_V3_cartonProductionForm();
            f.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            M_V_history f = new M_V_history(1);
            f.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
        }
    }
}
