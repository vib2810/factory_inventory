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
            this.c = new DbConnect();
            this.iptextbox.Text = "Default (" + Properties.Settings.Default.LastIP+")";
            this.FormClosing += new FormClosingEventHandler(Login_FormClosing);
            this.CenterToScreen();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.iptextbox.Text.StartsWith("Default")!=true)
            {
                Console.WriteLine("setting: " + this.iptextbox.Text.Replace(" ", ""));
                Properties.Settings.Default.LastIP = this.iptextbox.Text.Replace(" ", ""); ;
                Properties.Settings.Default.Save();
            }
            this.c = new DbConnect();
            this.access=this.c.checkLogin(textBox1.Text, textBox2.Text);
            if(access==1 || access==2)
            {
                this.username = textBox1.Text;
                this.close_from_code = true;
                this.Close();
            }
        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(this.close_from_code != true) //not called after login
            {
                this.close_from_code = false;
                Environment.Exit(0);
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {
            var textBoxes = this.Controls
                  .OfType<TextBox>()
                  .Where(x => x.Name.EndsWith("TB"));

            foreach (var txtBox in textBoxes)
            {
                c.textBoxEvent(txtBox);
            }
            
            var buttons = this.Controls
                  .OfType<Button>()
                  .Where(x => x.Name.EndsWith("Button"));

            foreach (var button in buttons)
            {
                c.buttonEvent(button);
            }
        }
    }
}
