using Factory_Inventory.Factory_Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Factory_Inventory
{
    public partial class Login : Form
    {
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                this.button1.PerformClick();
                return false;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        public int access=0;
        DbConnect c;
        public string username;
        private bool close_from_code = false;
        public Login()
        {
            InitializeComponent();
            Console.WriteLine("before setting: " + this.iptextbox.Text.Replace(" ", ""));
            this.c = new DbConnect();
            this.FormClosing += new FormClosingEventHandler(Login_FormClosing);
            this.CenterToScreen();
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.iptextbox.Text != "Default")
            {
                Console.WriteLine("setting: " + this.iptextbox.Text.Replace(" ", ""));
                Global.ipaddress = this.iptextbox.Text.Replace(" ", "");
            }
            this.c = new DbConnect();
            this.access=this.c.checkLogin(textBox1.Text, textBox2.Text);
            this.username = textBox1.Text;
            this.close_from_code = true;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Signup f2 = new Signup();
            //f2.Show();
        }
        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if(!string.Equals((sender as Button).Name, @"CloseButton"))
            //{
            //    Console.WriteLine("close login");
            //    Environment.Exit(0);
            //}
            //if (e.CloseReason == CloseReason.UserClosing)
            //{
               
            //}
            if(this.close_from_code != true) //not called after login
            {
                this.close_from_code = false;
                Environment.Exit(0);
            }
        }
    }
}
