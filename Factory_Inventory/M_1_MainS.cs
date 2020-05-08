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
        public DbConnect c;
        public bool logout = false;
        //Declare all sub-forms globally so we can close them all
        M_Signup f2=null;
        private bool close_from_code = false;
        public M_1_MainS(DbConnect input, string user, string access)
        {
            Console.WriteLine("Main.S");
            this.c = input;
            this.FormClosing += new FormClosingEventHandler(MainS_FormClosing);
            InitializeComponent();
            this.usernameLabel.Text = "Logged in as " + user + ": "+  access;
            hide_all_UCs();
            usersUC1.currentUser = user;
            this.CenterToScreen();
        }
        private void MainS_Load(object sender, EventArgs e)
        {
            this.AutoScaleMode = AutoScaleMode.Dpi;
            this.Scale(new SizeF(1.25F, 1.25F));
            this.CenterToScreen();
        }
        private void hide_all_UCs()
        {
            vouchersUC1.Hide();
            inventoryUC1.Hide();
            usersUC1.Hide();
            loginlogUC1.Hide();
            m_1_BackupRestoreUC1.Hide();
        }
        private void close_form()
        {
            this.close_from_code = true;
            this.Close();
        }
        private void newUser_Click(object sender, EventArgs e)
        {
            f2 = new M_Signup(c);
            f2.Show();
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
            vouchersUC1.Show();
            vouchersUC1.BringToFront();
        }
        private void inventoryButton_Click_1(object sender, EventArgs e)
        {
            hide_all_UCs();
            inventoryUC1.Show();
            inventoryUC1.BringToFront();
        }
        private void UsersButton_Click_1(object sender, EventArgs e)
        {
            hide_all_UCs();
            usersUC1.Show();
            usersUC1.BringToFront();
            usersUC1.loadDatabase();
        }
        private void loginLogButton_Click(object sender, EventArgs e)
        {
            hide_all_UCs();
            loginlogUC1.Show();
            loginlogUC1.BringToFront();
            loginlogUC1.loadUserData();
        }

        private void backupRestoreButton_Click(object sender, EventArgs e)
        {
            hide_all_UCs();
            m_1_BackupRestoreUC1.Show();
            m_1_BackupRestoreUC1.BringToFront();
        }
    }
}
