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
        int firm_type;
        DataTable firmdata;
        MainConnect mc;
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
            //null string if the server doesnt exist
            if(f.final_string!="")
            {
                this.con_start = f.final_string;
                this.localButton.Text = f.button_text;
                if (this.firm_type == 0) factoryButton.PerformClick();
                else tradingButton.PerformClick();
            }
        }
        private void factoryButton_Click(object sender, EventArgs e)
        {
            this.fillfirms(0);
            factoryButton.BackColor = Color.Gold;
            tradingButton.BackColor = SystemColors.Control;
        }
        private void tradingButton_Click(object sender, EventArgs e)
        {
            this.fillfirms(1);
            tradingButton.BackColor = Color.Gold;
            factoryButton.BackColor = SystemColors.Control;
        }
        private void enterButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            string firmID = this.firmdata.Rows[dataGridView1.SelectedRows[0].Index]["Firm_ID"].ToString();
            string dbname = "FactoryData";
            if (this.firm_type == 1) dbname = "TradingData";
            Global.defaultconnectionstring = Global.getconnectionstring(this.con_start, dbname+"_" + firmID);
            Login l = new Login();
            l.setfirmtb(this.firmdata.Rows[dataGridView1.SelectedRows[0].Index]["Firm_Name"].ToString());
            l.ShowDialog();
            Console.WriteLine("Hello here");
            if (l.access == 1 || l.access == 2)
            {
                Global.background = new TwistERP();
                Global.background.IsMdiContainer = true;
                if (this.firm_type == 0)
                {
                    //set the corresponding global connection string
                    DbConnect c = new DbConnect();
                    c.recordLogin(l.username);
                    M_1_MainS ms = new M_1_MainS(c, l.username, l.access);
                    Global.background.main_form = ms;
                    ms.MdiParent = Global.background;
                    ms.Scale(new SizeF(1.3F, 1.3F));
                    ms.AutoScaleMode = AutoScaleMode.Font;
                    ms.StartPosition = FormStartPosition.CenterScreen;
                    ms.Show();
                    Global.background.ShowDialog();

                    if (Global.background.logout == true)
                    {
                        c.recordLogout(l.username);
                    }
                }
            }
            this.Show();
        }

        //user
        public void fillfirms(int type)
        {
            mc = new MainConnect(con_start);

            dataGridView1.Rows.Clear();
            string sql = "SELECT * FROM Firms_List WHERE Firm_Type =" + type.ToString();
            DataTable dt = mc.runQuery(sql);
            this.firmdata = dt;
            if (dt == null) return;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dataGridView1.Rows.Add(dt.Rows[i]["Firm_Name"].ToString(), dt.Rows[i]["Active_User"].ToString());
            }
            this.firm_type = type;
        }

    }
}
