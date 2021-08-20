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

namespace Factory_Inventory.Factory_Classes
{
    public partial class AA_IPSelect : Form
    {
        public string final_string="";
        public string button_text = "";
        public AA_IPSelect()
        {
            InitializeComponent();
            if (Properties.Settings.Default.localState == true)
            {
                this.checkBox1.Checked = true;
                this.ipTB.BackColor = Color.SandyBrown;
                this.ipTB.ReadOnly = true;
            }
            else
            {
                this.checkBox1.Checked = false;
                this.ipTB.ReadOnly = false;
            }
            if (Properties.Settings.Default.LastIP == "-1")
            {
                this.ipTB.Text = "Local";
            }
            else this.ipTB.Text = Properties.Settings.Default.LastIP + ": Default";
            this.ActiveControl = this.saveButton;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox1.Checked == false)
            {
                this.ipTB.ReadOnly = false;
                this.ipTB.BackColor = Color.White;
            }
            else
            {
                this.ipTB.ReadOnly = true;
                this.ipTB.BackColor = Color.SandyBrown;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            setLocalString f = new setLocalString();
            f.ShowDialog();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            bool properties_changed = false;
            string[] split = this.ipTB.Text.Split(':');
            if (this.checkBox1.Checked == true)
            {
                //take the local string
                this.final_string = Properties.Settings.Default.LocalConnectionString;
                if(string.IsNullOrWhiteSpace(final_string)==true)
                {
                    Global.ErrorBox("Local Connection String not set");
                    return;
                }
                this.button_text = "Local Server: "+ this.final_string.Substring(0, this.final_string.Length-1);
            }
            else
            {
                //construct the string from IP address of server
                split[0] = split[0].Replace(" ", "");
                string con = "Data Source = " + split[0] + ", 1433;";
                //check if the new connection string is same as last ip, if no then update last ip
                if (split[0] != Properties.Settings.Default.LastIP)
                {
                    Properties.Settings.Default.LastIP = split[0];
                    properties_changed = true;
                }
                this.final_string = con;
                this.button_text = "Remote Server: " + " Data Source = " + split[0];
            }
            if (Properties.Settings.Default.localState != this.checkBox1.Checked)
            {
                Properties.Settings.Default.localState = this.checkBox1.Checked;
                properties_changed = true;
            }

            //test if the server exists
            bool result = this.TestForServer(Global.getconnectionstring(this.final_string, "Main"));
            if (result == false)
            {
                Global.ErrorBox("Server Doesnt Exist\nConnection String: "+ this.final_string);
                this.final_string = "";
                return;
            }
            Console.WriteLine("Server exists!!");
            if (properties_changed == true) Properties.Settings.Default.Save();

            this.Close();
        }

        private bool TestForServer(string con)
        {
            try
            {
                SqlConnection temp = new SqlConnection(con + "Connection Timeout=5"); // making connection   
                Console.WriteLine(con);
                temp.Open();
                temp.Close();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }
    }
}
