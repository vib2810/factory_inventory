using Factory_Inventory.Main;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
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
            Global.con_start = this.con_start;
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
            if (l.access == 1 || l.access == 2)
            {
                //set the corresponding global connection string
                DbConnect c = new DbConnect();
                c.recordLogin(l.username);
                Global.background = new TwistERP(l.username, l.access, this.firmdata.Rows[dataGridView1.SelectedRows[0].Index]["Firm_Name"].ToString(), mc);
                Global.background.IsMdiContainer = true;

                //Generate access token and store in Global, and set back color in the global form
                Global.accessToken = l.username + '_' + c.RandomString(4);
                //Take edit access if its currently null
                string sql = "DECLARE @active_user AS VARCHAR(100); SET @active_user = (select Active_User from firms_list where Firm_ID = " + firmID + ")";
                sql += "if (@active_user IS NULL or @active_user = '') BEGIN update Firms_List set Active_User = '" + Global.accessToken + "' where firm_id = " + firmID + "; select 'true'; END\n else select 'false';";
                DataTable info = mc.runQuery(sql);
                if (info.Rows[0][0].ToString() == "false")
                {
                    mc.WarningBox("Another User Login is detected. You will be logged in with no ADD/EDIT access");
                    Global.background.editAccessButton.BackColor = Color.OrangeRed;
                    Global.background.editAccessButton.Text = "No Edit Access";
                }
                else
                {
                    Global.background.editAccessButton.BackColor = Color.LawnGreen;
                    Global.background.editAccessButton.Text = "Edit Access";
                }

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
                    sql = "select Active_User from firms_list where Firm_ID = " + Global.firmid;
                    DataTable dt = mc.runQuery(sql);
                    if (dt != null)
                    {
                        if (dt.Rows[0][0].ToString() == Global.accessToken) //if this user has access
                        {
                            sql = "update Firms_List set Active_User = NULL where firm_id = " + Global.firmid;
                            dt = mc.runQuery(sql);
                        }
                    }
                }
            }
            this.fillfirms();
            dataGridView1.Rows[0].Selected = true;
            this.Show();
        }

        //user
        public void fillfirms()
        {
            mc = new MainConnect(con_start);

            dataGridView1.Rows.Clear();
            string sql = "use main; SELECT * FROM Firms_List";
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
                    e.Handled = true;
                    enterButton.PerformClick();
                }
            }
        }

        private void createButton_Click(object sender, EventArgs e)
        {
            //Get data from form
            DataTable dt = mc.runQuery("SELECT Firm_ID, Firm_Name FROM Firms_List");
            FirmCreate f = new FirmCreate(dt, mc);
            f.ShowDialog();
            if (f.values_set == false) return;
            string soursedb = "";
            if (f.comboBox1.SelectedIndex > 0) soursedb = "FactoryData_"+dt.Rows[f.comboBox1.SelectedIndex - 1]["Firm_ID"].ToString();

            //Create Database
            dt = mc.runQuery("SELECT MAX(Firm_ID) FROM Firms_List");
            int firm_id = int.Parse(dt.Rows[0][0].ToString()) + 1;
            string replace = "FactoryData_" + firm_id.ToString();
            dt=mc.runQuery("USE [master]; CREATE DATABASE[" + replace + "]");
            if (dt == null)
            {
                mc.ErrorBox("Error in creating database");
                mc.runQuery("alter database "+replace+" set single_user with rollback immediate; DROP DATABASE " + replace);
                return;
            }
            string script = File.ReadAllText(@"../../Queries/Database_Create_Full.sql").Replace("FactoryData", replace);
            bool res= mc.runQueryGo(script);
            if (res == false)
            {
                mc.ErrorBox("Error in creating database");
                mc.runQuery("alter database "+replace+" set single_user with rollback immediate; DROP DATABASE " + replace);
                return;
            }

            //Make new DbConnectC
            Global.defaultconnectionstring = Global.getconnectionstring(this.con_start, replace);
            Global.firmid = firm_id.ToString();
            DbConnect c = new DbConnect();

            //Fill Users Table
            string username = f.userNameTB.Text;
            string password = f.passwordTB.Text;
            int AccessLevel = 1;
            int ans = c.addUser(username, password, AccessLevel);
            if (ans != 1)
            {
                c.SuccessBox("Could Not Add User");
                mc.runQuery("alter database "+replace+" set single_user with rollback immediate; DROP DATABASE " + replace);
                return;
            }

            //Fill Fiscal Year Table
            string sql = "";
            for(int i=2019;i<2030;i++)
            {
                string fisal_year = i.ToString() + "-" + (i + 1).ToString();
                sql += "INSERT INTO Fiscal_Year VALUES ('" + fisal_year + "', 0, 0, 'BB0', 'RR0', 0, 'BB0', 'RR0')\n";
            }
            dt = c.runQuery(sql);
            if(dt==null)
            {
                c.ErrorBox("Error in creating database (Fiscal Year)");
                mc.runQuery("alter database "+replace+" set single_user with rollback immediate; DROP DATABASE " + replace);
                return;
            }

            //Fill Print Types Table
            DataTable ds = c.runQuery("INSERT INTO Print_Types VALUES ('" + f.firmnameTB.Text + "', '" + f.addressTB.Text + "', '" + f.gstinTB.Text + "', '" + f.pnoTB.Text + "', '" + f.emailTB.Text + "'); SELECT SCOPE_IDENTITY();");
            if (ds == null)
            {
                c.ErrorBox("Error in creating database (Print Types)");
                mc.runQuery("alter database "+replace+" set single_user with rollback immediate; DROP DATABASE " + replace);
                return;
            }

            //Fill Defaults
            sql = "INSERT INTO Defaults VALUES ('Print', 'Default Print Type', " + ds.Rows[0][0].ToString() + ");\n";
            sql += "INSERT INTO Defaults VALUES ('Default', 'Cone', 80);\n";
            sql += "INSERT INTO Defaults VALUES ('Print:Carton_Slip', 'Default Print Type', " + ds.Rows[0][0].ToString() + ");\n";
            dt = c.runQuery(sql);
            if (dt == null)
            {
                c.ErrorBox("Error in creating database (Fill Defaults)");
                mc.runQuery("alter database "+replace+" set single_user with rollback immediate; DROP DATABASE " + replace);
                return;
            }

            //Copy Master Tables
            if (soursedb!="")
            {
                string transfer_query = File.ReadAllText(@"../../Queries/tables_data_transfer.sql");
                sql = "use "+soursedb+"; DECLARE @sql NVARCHAR(1000); DECLARE @column NVARCHAR(1000);\n";
                List<string> master_tables = new List<string>(new string[] { "Colours", "Company_Names", "Cones", "Machine_No", "Quality", "Quality_Before_Twist", "Spring", "T_M_Colours", "T_M_Company_Names", "T_M_Cones", "T_M_Customers", "T_M_Quality_Before_Job" });
                for (int i = 0; i < master_tables.Count; i++)
                {
                    sql += transfer_query.Replace("table_name", master_tables[i]).Replace("source_db",  soursedb).Replace("dest_db", replace) +";\n";
                    // "drop table " + replace + ".dbo." + master_tables[i] + "; select * into " + replace + ".dbo." + master_tables[i] + " from " + soursedb + ".dbo." + master_tables[i] + ";\n";
                }
                dt = mc.runQuery(sql);
                if (dt == null)
                {
                    c.ErrorBox("Error in creating database (Firms List)");
                    mc.runQuery("alter database "+replace+" set single_user with rollback immediate; DROP DATABASE " + replace);
                    return;
                }
            }

            //Enter in Firm_List Table
            dt = mc.runQuery("INSERT INTO Firms_List VALUES ('" + f.firmnameTB.Text + "', NULL, " + firm_id.ToString() + ")");
            if (dt == null)
            {
                c.ErrorBox("Error in creating database (Firms List)");
                mc.runQuery("alter database "+replace+" set single_user with rollback immediate; DROP DATABASE " + replace);
                return;
            }
            //Success
            fillfirms();
            mc.SuccessBox("New Firm Created Successfully!\nMaster imported from " + f.comboBox1.SelectedItem.ToString());
            
        }
    }
}
