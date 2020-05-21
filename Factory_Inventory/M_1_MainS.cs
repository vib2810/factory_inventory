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
    public partial class M_1_MainS : Form
    {
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.V)
            {
                this.vouchersButton.PerformClick(); ;
                return false;
            }
            if (keyData == Keys.I)
            {
                this.inventoryButton.PerformClick();
                return false;
            }
            if (keyData == Keys.B)
            {
                this.backupRestoreButton.PerformClick();
                return false;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        public DbConnect c;
        public bool logout = false;
        public Button last_clicked;
        Color select = Color.SteelBlue;
        //Declare all sub-forms globally so we can close them all
        M_1_Signup f2=null;
        private bool close_from_code = false;
        public M_1_MainS(DbConnect input, string user, int access)
        {
            Global.access = access;
            this.c = input;
            this.FormClosing += new FormClosingEventHandler(MainS_FormClosing);
            InitializeComponent();
            
            string access_type = "";
            if (access == 1) access_type = "Super User";
            else if (access == 2) access_type = "User";
            this.usernameLabel.Text = "Logged in as " + user + ": "+  access_type;
            
            hide_all_UCs();
            usersUC.currentUser = user;
            this.CenterToScreen();
            if(Global.access==2)
            {
                this.UsersButton.Visible= false;
                this.newUserButton.Visible = false;
                this.loginLogButton.Visible = false;
            }
        }
        private void MainS_Load(object sender, EventArgs e)
        {
            this.AutoScaleMode = AutoScaleMode.Dpi;
            this.Scale(new SizeF(1.25F, 1.25F));
            this.CenterToScreen();
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
            vouchersUC.Hide();
            inventoryUC.Hide();
            usersUC.Hide();
            loginlogUC.Hide();
            m_1_BackupRestoreUC.Hide();
        }
        private void close_form()
        {
            this.close_from_code = true;
            this.Close();
        }
        private void newUser_Click(object sender, EventArgs e)
        {
            f2 = new M_1_Signup(c, this);
            f2.Show();
            this.decolour_all_buttons();
            this.newUserButton.BackColor = select;
        }
        public void closeAllForms()
        {
            if(f2!=null) f2.Close();
            this.close_form();
        }
        private void MainS_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(this.close_from_code!=true)
            {
                DialogResult dialogResult = MessageBox.Show("Log Out and Exit?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    this.logout = true;
                    this.close_form();
                }
                else if (dialogResult == DialogResult.No)
                {
                    e.Cancel=true; 
                }
            }
            else
            {
                this.close_from_code = false;
            }

        }
        private void logOutButton_Click_1(object sender, EventArgs e)
        {
            this.logout = true;
            this.Close();
        }
        private void vouchersButton_Click_1(object sender, EventArgs e)
        {
            hide_all_UCs();
            vouchersUC.Show();
            vouchersUC.BringToFront();
            vouchersUC.Focus();
            this.decolour_all_buttons();
            this.vouchersButton.BackColor = select;
            this.last_clicked = this.vouchersButton;
            this.Text = "Factory Inventory - Home - Vouchers";
        }
        private void inventoryButton_Click_1(object sender, EventArgs e)
        {
            hide_all_UCs();
            inventoryUC.Show();
            inventoryUC.BringToFront();
            inventoryUC.Focus();
            this.decolour_all_buttons();
            this.inventoryButton.BackColor = select;
            this.last_clicked = this.inventoryButton;
            this.Text = "Factory Inventory - Home - Inventory";
        }
        private void UsersButton_Click_1(object sender, EventArgs e)
        {
            hide_all_UCs();
            usersUC.Show();
            usersUC.BringToFront();
            usersUC.loadDatabase();
            usersUC.Focus();
            this.decolour_all_buttons();
            this.UsersButton.BackColor = select;
            this.last_clicked = this.UsersButton;
            this.Text = "Factory Inventory - Home - Manage Users";
        }
        private void loginLogButton_Click(object sender, EventArgs e)
        {
            hide_all_UCs();
            loginlogUC.Show();
            loginlogUC.BringToFront();
            loginlogUC.loadUserData();
            loginlogUC.Focus();
            this.decolour_all_buttons();
            this.loginLogButton.BackColor = select;
            this.last_clicked = this.loginLogButton;
            this.Text = "Factory Inventory - Home - Login Log";
        }
        private void backupRestoreButton_Click(object sender, EventArgs e)
        {
            hide_all_UCs();
            m_1_BackupRestoreUC.Show();
            m_1_BackupRestoreUC.BringToFront();
            m_1_BackupRestoreUC.Focus();
            this.decolour_all_buttons();
            this.backupRestoreButton.BackColor = select;
            this.last_clicked = this.backupRestoreButton;
            this.Text = "Factory Inventory - Home - Backup and Restore";
        }
    }
}
