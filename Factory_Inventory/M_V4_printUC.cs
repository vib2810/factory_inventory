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
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            Console.WriteLine(keyData.ToString());
            if (keyData == (Keys.D1) || keyData == (Keys.NumPad1))
            {
                this.button1.PerformClick();
                return false;
            }

            if (keyData == (Keys.D2) || keyData == (Keys.NumPad2))
            {
                this.button2.PerformClick();
                return false;
            }
           
            if (keyData == (Keys.D3) || keyData == (Keys.NumPad3))
            {
                this.button6.PerformClick();
                return false;
            }
            
            if (keyData == (Keys.D4) || keyData == (Keys.NumPad4))
            {
                this.button3.PerformClick();
                return false;
            }
            
            return base.ProcessCmdKey(ref msg, keyData);
        }
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
