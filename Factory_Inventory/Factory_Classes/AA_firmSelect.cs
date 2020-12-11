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
    public partial class AA_firmSelect : Form
    {
        string con_start;
        MainConnect c;
        public AA_firmSelect()
        {
            InitializeComponent();
            if(Properties.Settings.Default.localState == true)
            {
                //previously local was checked
                string localstring = Properties.Settings.Default.LocalConnectionString;
                this.localButton.Text = "Local Server: "+ localstring.Substring(0,localstring.Length - 1);
                con_start = localstring;
            }
            else
            {
                this.localButton.Text = "Remote Server: Data Source = " + Properties.Settings.Default.LastIP;
                con_start = "Data Source = " + Properties.Settings.Default.LastIP + ", 1433;";
            }
        }
        private void AA_firmSelect_Load(object sender, EventArgs e)
        {
            factoryButton.PerformClick();
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

        private void factoryButton_Click(object sender, EventArgs e)
        {
            c = new MainConnect(con_start);

            dataGridView1.Rows.Clear();
            DataTable dt = c.runQuery("SELECT * FROM Firms_List WHERE Firm_Type = 0");
            for(int i=0;i<dt.Rows.Count;i++)
            {
                dataGridView1.Rows.Add(dt.Rows[i]["Firm_Name"].ToString(), dt.Rows[i]["Active_User"].ToString());
            }
            factoryButton.BackColor = Color.Gold;
            tradingButton.BackColor = SystemColors.Control;
        }

        private void tradingButton_Click(object sender, EventArgs e)
        {
            c = new MainConnect(con_start);

            dataGridView1.Rows.Clear();
            DataTable dt = c.runQuery("SELECT * FROM Firms_List WHERE Firm_Type = 1");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dataGridView1.Rows.Add(dt.Rows[i]["Firm_Name"].ToString(), dt.Rows[i]["Active_User"].ToString());
            }
            tradingButton.BackColor = Color.Gold;
            factoryButton.BackColor = SystemColors.Control;
        }
    }
}
