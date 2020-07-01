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
    public partial class A_1_MainS : Form
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
            if (keyData == Keys.E)
            {
                this.employeesButton.PerformClick();
                return false;
            }
            if (keyData == Keys.A)
            {
                this.markAttendanceButton.PerformClick();
                return false;
            }
            if (keyData == Keys.R)
            {
                this.reportsButton.PerformClick();
                return false;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private AttConnect a = new AttConnect();
        public bool logout = false;
        public Button last_clicked;
        public Color select = Color.SteelBlue;

        public A_1_MainS()
        {
            
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(MainS_FormClosing);

            hide_all_UCs();
            this.CenterToScreen();
        }
        public void decolour_all_buttons()
        {
            var buttons = this.Controls
             .OfType<Button>()
             .Where(x => x.Name.EndsWith("Button"));

            foreach (var button in buttons)
            {
                button.BackColor = Color.DarkGray;
            }
        }
        private void hide_all_UCs()
        {
            this.a_1_EmployeesUC1.Hide();
        }
        private void MainS_FormClosing(object sender, FormClosingEventArgs e)
        {
            //e.Cancel = true;
        }
        private void vouchersButton_Click_1(object sender, EventArgs e)
        {
            hide_all_UCs();
            a_1_EmployeesUC1.Show();
            a_1_EmployeesUC1.BringToFront();
            a_1_EmployeesUC1.Focus();
            this.decolour_all_buttons();
            this.employeesButton.BackColor = select;
            this.last_clicked = this.employeesButton;
            this.Text = "Attendance - Home - Employees";
        }
        private void inventoryButton_Click_1(object sender, EventArgs e)
        {
            hide_all_UCs();
            //inventoryUC.Show();
            //inventoryUC.BringToFront();
            //inventoryUC.Focus();
            this.decolour_all_buttons();
            this.markAttendanceButton.BackColor = select;
            this.last_clicked = this.markAttendanceButton;
            this.Text = "Factory Inventory - Home - Inventory";
        }
        private void loginLogButton_Click(object sender, EventArgs e)
        {
            hide_all_UCs();
            //loginlogUC.Show();
            //loginlogUC.BringToFront();
            //loginlogUC.loadUserData();
            //loginlogUC.Focus();
            this.decolour_all_buttons();
            this.reportsButton.BackColor = select;
            this.last_clicked = this.reportsButton;
            this.Text = "Factory Inventory - Home - Login Log";
        }
    }
}
