using Factory_Inventory.Factory_Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Factory_Inventory
{
    public partial class T_Main : Form
    {
        private const int CP_NOCLOSE_BUTTON = 0x200;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.V)
            {
                this.vouchersButton.PerformClick();
                return false;
            }
            if (keyData == Keys.I)
            {
                this.inventoryButton.PerformClick();
                return false;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        public Button last_clicked;
        public Color select = Color.SteelBlue;

        public T_Main()
        {
            InitializeComponent();
            
            hide_all_UCs();
            this.CenterToScreen();
        }
        private void MainS_Load(object sender, EventArgs e)
        {
        }
        public void decolour_all_buttons()
        {
            var buttons = this.Controls
             .OfType<Button>()
             .Where(x => x.Name.EndsWith("Button"));

            foreach (var button in buttons)
            {
                if(button.Text!="Log Out")
                {
                    button.BackColor = Color.DarkGray;
                }
            }
        }
        private void hide_all_UCs()
        {
            t_vouchersUC1.Hide();
            //inventoryUC.Hide();
            //usersUC.Hide();
            //loginlogUC.Hide();
        }

        private void vouchersButton_Click_1(object sender, EventArgs e)
        {
            hide_all_UCs();
            t_vouchersUC1.Show();
            t_vouchersUC1.BringToFront();
            t_vouchersUC1.Focus();
            this.decolour_all_buttons();
            this.vouchersButton.BackColor = select;
            this.last_clicked = this.vouchersButton;
            this.Text = "Factory Inventory - Home - Vouchers";
        }
        private void inventoryButton_Click_1(object sender, EventArgs e)
        {
            hide_all_UCs();
            //inventoryUC.Show();
            //inventoryUC.BringToFront();
            //inventoryUC.Focus();
            this.decolour_all_buttons();
            this.inventoryButton.BackColor = select;
            this.last_clicked = this.inventoryButton;
            this.Text = "Factory Inventory - Home - Inventory";
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            ControlPaint.DrawBorder(e.Graphics, ClientRectangle,
                                         Color.Black, 10, ButtonBorderStyle.Inset,
                                         Color.Black, 10, ButtonBorderStyle.Inset,
                                         Color.Black, 10, ButtonBorderStyle.Inset,
                                         Color.Black, 10, ButtonBorderStyle.Inset);
        }

    }
}
