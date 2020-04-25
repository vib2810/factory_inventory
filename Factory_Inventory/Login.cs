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
        public int access=0;
        DbConnect c;
        public string username;
        private bool close_from_code = false;
        public Login(DbConnect input)
        {
            this.c = input;
            this.FormClosing += new FormClosingEventHandler(Login_FormClosing);
            this.CenterToScreen();
            InitializeComponent();
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.access=this.c.checkLogin(textBox1.Text, textBox2.Text);
            if(this.access==1)
            {
                this.username = textBox1.Text;
                this.close_from_code = true;
                this.Close();
            }
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
