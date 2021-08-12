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
    public partial class T_I_TablesUC : UserControl
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

            if (keyData == (Keys.D2) || keyData == (Keys.NumPad2))
            {
                this.button2.Focus();
                this.button2.PerformClick();
                return false;
            }
            
            return base.ProcessCmdKey(ref msg, keyData);
        }
        public T_I_TablesUC()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            T_I_Tables f = new T_I_Tables(1);
            Global.background.show_form(f);
            f.Text = "Tables - Carton";
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            T_I_Tables f = new T_I_Tables(2);
            Global.background.show_form(f);
            f.Text = "Tables - Tray";
        }
    }
}
