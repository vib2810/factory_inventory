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
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.C)
            {
                this.cartonButton.PerformClick(); ;
                return false;
            }
            if (keyData == Keys.T)
            {
                this.trayProductionButton.PerformClick();
                return false;
            }
            if (keyData == Keys.N)
            {
                this.cartonProductionButton.PerformClick();
                return false;
            }
            if (keyData == Keys.P)
            {
                this.printButton.PerformClick();
                return false;
            }
            if (keyData == Keys.O)
            {
                this.openingStockButton.PerformClick();
                return false;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private DbConnect c;
        public M_1_vouchersUC()
        {
            InitializeComponent();
            this.c = new DbConnect();
            hide_all_UCs();
            Console.WriteLine("Access is :" + Global.access);
            if (Global.access == 2)
            {
                this.masterButton.Visible = false;
            }
        }
        public void decolour_all_buttons()
        {
            var buttons = this.Controls
     .OfType<Button>()
     .Where(x => x.Name.EndsWith("Button"));

            foreach (var button in buttons)
            {
                if (button.BackColor != SystemColors.ActiveCaption)
                {
                    button.BackColor = SystemColors.ControlDark;
                }
            }
        }
        private void hide_all_UCs()
        {
            voucherInput1UC1.Hide();
            m_V2_voucherInput2UC1.Hide();
            m_V3_voucherInput3UC1.Hide();
            m_V4_printUC1.Hide();
            m_V5_OpeningUC1.Hide();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            hide_all_UCs();
            voucherInput1UC1.Show();
            voucherInput1UC1.BringToFront();
            voucherInput1UC1.Focus();
            this.decolour_all_buttons();
            this.cartonButton.BackColor = Color.Orange;
        }
        private void editCNameQualityButton_Click(object sender, EventArgs e)
        {
            if (c.check_login_val() == false) return;
            M_V_AddEditDropDowns f = new M_V_AddEditDropDowns();
            Global.background.show_form(f);
        }
        private void trayProductionButton_Click(object sender, EventArgs e)
        {
            hide_all_UCs();
            m_V2_voucherInput2UC1.Show();
            m_V2_voucherInput2UC1.BringToFront();
            m_V2_voucherInput2UC1.Focus();
            this.decolour_all_buttons();
            this.trayProductionButton.BackColor = Color.Orange;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            hide_all_UCs();
            m_V3_voucherInput3UC1.Show();
            m_V3_voucherInput3UC1.BringToFront();
            m_V3_voucherInput3UC1.Focus();
            this.decolour_all_buttons();
            this.cartonProductionButton.BackColor = Color.Orange;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            hide_all_UCs();
            m_V4_printUC1.Show();
            m_V4_printUC1.BringToFront();
            m_V4_printUC1.Focus();
            this.decolour_all_buttons();
            this.printButton.BackColor = Color.Orange;
        }

        private void openingStockButton_Click(object sender, EventArgs e)
        {
            hide_all_UCs();
            m_V5_OpeningUC1.Show();
            m_V5_OpeningUC1.BringToFront();
            m_V5_OpeningUC1.Focus();
            this.decolour_all_buttons();
            this.openingStockButton.BackColor = Color.Orange;
        }
    }
}
