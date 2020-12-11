using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Factory_Inventory.Factory_Classes
{
    public partial class AA_firmSelect : Form
    {
        string con_start;
        public AA_firmSelect()
        {
            InitializeComponent();
            if(Properties.Settings.Default.localState == true)
            {
                //previously local was checked
                string localstring = Properties.Settings.Default.LocalConnectionString;
                this.localButton.Text = "Local Server: "+ localstring.Substring(0,localstring.Length - 1);
            }
            else
            {
                this.localButton.Text = "Remote Server: Data Source = " + Properties.Settings.Default.LastIP;
            }
        }
        private void AA_firmSelect_Load(object sender, EventArgs e)
        {

        }
       
        //callbacks
        private void localButton_Click(object sender, EventArgs e)
        {
            AA_IPSelect f = new AA_IPSelect();
            f.ShowDialog();
            this.con_start = f.final_string;
            this.localButton.Text = f.button_text;
            Console.WriteLine(this.con_start);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
