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
    public partial class M_1_inventoryUC : UserControl
    {
        private DbConnect c;
        public M_1_inventoryUC()
        {
            InitializeComponent();
            this.c = new DbConnect();
            hide_all_UCs();
        }

        private void voucherLabel_Click(object sender, EventArgs e)
        {

        }
        private void hide_all_UCs()
        {
            //M_I1_OnDateUC1. 
            //voucherInput1UC1.Hide();
            //m_V2_voucherInput2UC1.Hide();
            //m_V3_voucherInput3UC1.Hide();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            hide_all_UCs();
            M_I1_OnDate f = new M_I1_OnDate();
            f.Show();
            //voucherInput1UC1.Show();
            //voucherInput1UC1.BringToFront();
        }

        private void tablesButton_Click(object sender, EventArgs e)
        {

        }

        private void fromtoButton_Click(object sender, EventArgs e)
        {
            M_I1_FromToDate f = new M_I1_FromToDate();
            f.Show();
        }
    }
}
