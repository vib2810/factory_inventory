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
    public partial class M_1_vouchersUC : UserControl
    {
        private DbConnect c;
        public M_1_vouchersUC()
        {
            InitializeComponent();
            this.c = new DbConnect();
            hide_all_UCs();
        }
        private void hide_all_UCs()
        {
            voucherInput1UC1.Hide();
            m_V2_voucherInput2UC1.Hide();
            m_V3_voucherInput3UC1.Hide();
            m_V4_printUC1.Hide();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            hide_all_UCs();
            voucherInput1UC1.Show();
            voucherInput1UC1.BringToFront();
        }

        private void editCNameQualityButton_Click(object sender, EventArgs e)
        {
            M_V_AddEditDropDowns f = new M_V_AddEditDropDowns();
            f.Show();
        }

        private void trayProductionButton_Click(object sender, EventArgs e)
        {
            hide_all_UCs();
            m_V2_voucherInput2UC1.Show();
            m_V2_voucherInput2UC1.BringToFront();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            hide_all_UCs();
            m_V3_voucherInput3UC1.Show();
            m_V3_voucherInput3UC1.BringToFront();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            hide_all_UCs();
            m_V4_printUC1.Show();
            m_V4_printUC1.BringToFront();
        }
    }
}
