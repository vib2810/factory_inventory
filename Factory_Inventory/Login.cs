using Factory_Inventory.Factory_Classes;
using Factory_Inventory.Properties;
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
            if(Properties.Settings.Default.localState == true)
            {
                this.checkBox1.Checked = true;
                this.iptextbox.BackColor = Color.SandyBrown;
            }
            else
            {
                this.checkBox1.Checked = false;
            }
            this.c = new DbConnect();
            if (Properties.Settings.Default.LastIP=="-1")
            {
                this.iptextbox.Text = "Local";
            }
            else this.iptextbox.Text = Properties.Settings.Default.LastIP + ": Default";
            this.FormClosing += new FormClosingEventHandler(Login_FormClosing);
            this.CenterToScreen();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool properties_changed = false;
            string[] split = this.iptextbox.Text.Split(':');
            if(this.checkBox1.Checked==true)
            {
                Global.connectionstring = Properties.Settings.Default.LocalConnectionString;
            }
            else
            {
                split[0] = split[0].Replace(" ", "");
                string con = "Data Source=" + split[0] + ", 1433;Initial Catalog=FactoryData;Persist Security Info=True;User ID=sa;Password=Kdvghr2810@;";
                if(split[0] != Properties.Settings.Default.LastIP)
                {
                    Properties.Settings.Default.LastIP = split[0];
                    properties_changed = true;
                    Properties.Settings.Default.Save();
                }
                Global.connectionstring = con;
             }
            if(Properties.Settings.Default.localState != this.checkBox1.Checked)
            {
                Properties.Settings.Default.localState = this.checkBox1.Checked;
                properties_changed = true;
            }
            if (properties_changed == true) Properties.Settings.Default.Save();
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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox1.Checked == false)
            {
                this.iptextbox.ReadOnly = false;
                this.iptextbox.BackColor = Color.White;
            }
            else
            {
                this.iptextbox.ReadOnly = true;
                this.iptextbox.BackColor = Color.SandyBrown;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            setLocalString f = new setLocalString();
            f.ShowDialog();
        }
    }
}
