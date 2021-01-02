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
        DataTable firmdata;
        MainConnect mc;
        public AA_firmSelect()
        {
            InitializeComponent();
            if(Properties.Settings.Default.localState == true)
            {
                //previously local was checked
                string localstring = Properties.Settings.Default.LocalConnectionString;
                if (string.IsNullOrEmpty(localstring) != true)
                {
                    this.localButton.Text = "Local Server: " + localstring.Substring(0, localstring.Length - 1);
                    con_start = localstring;
                }
                else
                {
                    this.localButton.Text = "Local Server: Please set the local server address";
                }
            }
            else
            {
                this.localButton.Text = "Remote Server: Data Source = " + Properties.Settings.Default.LastIP;
                con_start = "Data Source = " + Properties.Settings.Default.LastIP + ", 1433;";
            }
        }

        //callbacks
        private void localButton_Click(object sender, EventArgs e)
        {
            AA_IPSelect f = new AA_IPSelect();
            f.ShowDialog();
            //null string if the server doesnt exist
            if(f.final_string!="")
            {
                this.con_start = f.final_string;
                this.localButton.Text = f.button_text;
                fillfirms();
            }
        }
        private void enterButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            string firmID = this.firmdata.Rows[dataGridView1.SelectedRows[0].Index]["Firm_ID"].ToString();
            string dbname = "FactoryData";
            Global.defaultconnectionstring = Global.getconnectionstring(this.con_start, dbname+"_" + firmID);
            Global.firmid = firmID;
            Login l = new Login();
            l.setfirmtb(this.firmdata.Rows[dataGridView1.SelectedRows[0].Index]["Firm_Name"].ToString());
            l.ShowDialog();
            Console.WriteLine("Hello here");
            if (l.access == 1 || l.access == 2)
            {
                //set the corresponding global connection string
                DbConnect c = new DbConnect();
                c.recordLogin(l.username);
                Global.background = new TwistERP(l.username, l.access, this.firmdata.Rows[dataGridView1.SelectedRows[0].Index]["Firm_Name"].ToString());
                Global.background.IsMdiContainer = true;
                
                //open all 3 main forms
                A_1_MainS attendance = new A_1_MainS();
                Global.background.show_form(attendance, 2, true);
                M_1_MainS ms = new M_1_MainS();
                Global.background.show_form(ms, 0, true);
                T_Main trade = new T_Main();
                Global.background.show_form(trade, 1, true);
                //show the background form, runtime stops till the dialouge is closed
                Global.background.ShowDialog();
                
                //logout
                if (Global.background.logout == true)
                {
                    c.recordLogout(l.username);
                }
            }
            this.Show();
        }

        //user
        public void fillfirms()
        {
            mc = new MainConnect(con_start);

            dataGridView1.Rows.Clear();
            string sql = "SELECT * FROM Firms_List";
            DataTable dt = mc.runQuery(sql);
            this.firmdata = dt;
            if (dt == null) return;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dataGridView1.Rows.Add(dt.Rows[i]["Firm_Name"].ToString(), dt.Rows[i]["Active_User"].ToString());
            }
        }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }
        private void AA_firmSelect_Load(object sender, EventArgs e)
        {
            fillfirms();
        }

        private void AA_firmSelect_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                if(dataGridView1.SelectedRows.Count==1)
                {
                    enterButton.PerformClick();
                }
            }
        }
    }
}
