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
using Factory_Inventory.Factory_Classes;

namespace Factory_Inventory
{
    public partial class T_voucherInput1UC : UserControl
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

            return base.ProcessCmdKey(ref msg, keyData);
        }
        public T_voucherInput1UC()
        {
            InitializeComponent();
        }
        private DbConnect c = new DbConnect();
        private void button1_Click(object sender, EventArgs e)
        {
            if (c.check_login_val() == false) return;
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(M_V1_cartonInwardForm))
                {
                    form.WindowState = FormWindowState.Normal;
                    form.Activate();
                    return;
                }
            }
            T_V1_cartonInwardForm f = new T_V1_cartonInwardForm();
            Global.background.show_form(f);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (c.isHistoryFormOpen(13) == false)
            {
                M_V_history f = new M_V_history(13);
                Global.background.show_form(f);
            }
            
        }
    }
}
