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
    public partial class M_V4_printUC : UserControl
    {
        public M_V4_printUC()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            M_V4_printDyeingOutward f = new M_V4_printDyeingOutward();
            f.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            M_V4_printBatchReport f = new M_V4_printBatchReport();
            f.Show();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            M_V4_printCartonSlip f = new M_V4_printCartonSlip();
            f.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            M_V4_printDO f = new M_V4_printDO();
            f.Show();
        }
    }
}
