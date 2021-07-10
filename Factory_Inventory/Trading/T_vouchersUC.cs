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
    public partial class T_vouchersUC : UserControl
    {
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.C)
            {
                this.cartonButton.PerformClick(); ;
                return false;
            }
            if (keyData == Keys.R)
            {
                this.repackingButton.PerformClick();
                return false;
            }
            if (keyData == Keys.N)
            {
                this.jobButton.PerformClick();
                return false;
            }
            if (keyData == Keys.S)
            {
                this.salesButton.PerformClick();
                return false;
            }
            if (keyData == Keys.P)
            {
                this.printButton.PerformClick();
                return false;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private DbConnect c;
        public T_vouchersUC()
        {
            InitializeComponent();
            this.c = new DbConnect();
            hide_all_UCs();
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
            t_voucherInput1UC1.Hide();
            t_voucherInput2UC1.Hide();
            t_voucherInput3UC1.Hide();
            t_voucherInput4UC1.Hide();
            t_printUC1.Hide();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            hide_all_UCs();
            t_voucherInput1UC1.Show();
            t_voucherInput1UC1.BringToFront();
            t_voucherInput1UC1.Focus();
            this.decolour_all_buttons();
            this.cartonButton.BackColor = Color.Orange;
        }
        private void editCNameQualityButton_Click(object sender, EventArgs e)
        {
            T_V_master f = new T_V_master();
            Global.background.show_form(f);
        }
        private void trayProductionButton_Click(object sender, EventArgs e)
        {
            hide_all_UCs();
            t_voucherInput2UC1.Show();
            t_voucherInput2UC1.BringToFront();
            t_voucherInput2UC1.Focus();
            this.decolour_all_buttons();
            this.repackingButton.BackColor = Color.Orange;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            hide_all_UCs();
            t_voucherInput3UC1.Show();
            t_voucherInput3UC1.BringToFront();
            t_voucherInput3UC1.Focus();
            this.decolour_all_buttons();
            this.jobButton.BackColor = Color.Orange;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            hide_all_UCs();
            t_voucherInput4UC1.Show();
            t_voucherInput4UC1.BringToFront();
            t_voucherInput4UC1.Focus();
            this.decolour_all_buttons();
            this.salesButton.BackColor = Color.Orange;
        }
        private void openingStockButton_Click(object sender, EventArgs e)
        {
            hide_all_UCs();
            t_printUC1.Show();
            t_printUC1.BringToFront();
            t_printUC1.Focus();
            this.decolour_all_buttons();
            this.printButton.BackColor = Color.Orange;
        }
    }
}
