using Factory_Inventory.Factory_Classes;
using Factory_Inventory.Main;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Factory_Inventory
{
    public partial class TwistERP : Form
    {
        public bool temp;
        //dynamic lists which keep adding names of all the forms of various types which have opened atleast once
        Dictionary<string, int> form_group = new Dictionary<string, int>();
        MainConnect mc;
        DbConnect c;
        //List<string> attendance_forms = new List<string>();
        //List<string> backup_form = new List<string>();
        //List<string> trading_forms = new List<string>();
        // public M_1_MainS main_form = null;
        public bool logout = false;
        public TwistERP(string user, int access, string firmname, MainConnect mc)
        {
            this.mc = mc;
            c = new DbConnect();
            InitializeComponent();
            Global.access = access;
            this.DoubleBuffered = true;
            string access_type = "";
            if (access == 1) access_type = "Super User";
            else if (access == 2)
            {
                access_type = "User";
                manageUsersToolStripMenuItem.Visible = false;
            }
            this.usertoolStripButton1.Text = "Logged in as " + user + ": " + access_type;
            this.firmtoolStripButton2.Text = firmname;
        }

        //group 0- Factory
        //group 1- Trading 
        //group 2- Attendance
        //group 10- Backup restore
        //group 11- Manage Users
        public void show_form(Form f, int group=0, bool ismain=false)
        {
            f.Scale(new SizeF(Properties.Settings.Default.ScaleX, Properties.Settings.Default.ScaleY));
            f.Size = new Size((int)((float)Properties.Settings.Default.SizeX*f.Size.Width) , (int)((float)Properties.Settings.Default.SizeY*f.Size.Height));
            f.FormBorderStyle = FormBorderStyle.FixedSingle;
            f.AutoScroll = true;

            if (group == 0)
            {
                if (f.Name.ToString().StartsWith("M_I") || f.Name.ToString().StartsWith("T_I"))
                {
                    f.Show();
                }
                else
                {
                    f.Location = new Point(0, 0);
                    f.MaximizeBox = false;
                    f.MdiParent = this;
                }
                f.Show();
                int val;
                bool exists=form_group.TryGetValue(f.Name, out val);
                if(exists==false)
                {
                    form_group[f.Name] = 0;
                }
            }
            if (group==1)
            {
                int val;
                bool exists = form_group.TryGetValue(f.Name, out val);
                if (exists == false)
                {
                    form_group[f.Name] = 1;
                }
                f.MaximizeBox = false;
                f.MdiParent = this;
                f.Show();
            }
            if (group == 2)
            {
                int val;
                bool exists = form_group.TryGetValue(f.Name, out val);
                if (exists == false)
                {
                    form_group[f.Name] = 2;
                }
                f.MaximizeBox = false;
                f.MdiParent = this;
                f.Show();
            }
            if (ismain == true)
            {
                f.FormBorderStyle = FormBorderStyle.None;
                f.Scale(new SizeF(1.3F, 1.3F));
                f.AutoScaleMode = AutoScaleMode.Font;
                f.StartPosition = FormStartPosition.CenterScreen;
            }
            this.LayoutMdi(MdiLayout.Cascade);
        }
        private void TwistERP_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Log Out and Exit?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                this.logout = true;               
            }
            else if (dialogResult == DialogResult.No)
            {
                e.Cancel = true;
            }
        }


        //3 Mains of operation
        private void eRPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //main form is always open
            foreach (Form f in this.MdiChildren)
            {
                int val;
                form_group.TryGetValue(f.Name, out val);
                //show only forms with group no 0
                if (val == 0) f.Visible = true;
                else f.Visible = false;
            }
            this.LayoutMdi(MdiLayout.Cascade);
        }
        private void tradingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //main form is always open
            foreach (Form f in this.MdiChildren)
            {
                int val = -1;
                form_group.TryGetValue(f.Name, out val);
                //show only forms with group no 1
                if (val == 1) f.Visible = true;
                else f.Visible = false;
            }
            this.LayoutMdi(MdiLayout.Cascade);
        }
        private void attendanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mc.ErrorBox("Work under progress");
            return;
            //main form is always open
            foreach (Form f in this.MdiChildren)
            {
                int val;
                form_group.TryGetValue(f.Name, out val);
                //show only forms with group no 2
                if (val == 2) f.Visible = true;
                else f.Visible = false;
            }
            this.LayoutMdi(MdiLayout.Cascade);
        }


        //Options Menu
        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void backupAndRestoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool backup = false;
            O_BackupRestoreForm frm = new O_BackupRestoreForm();
            //check if the form is already open but hidden
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == frm.Name)
                {
                    backup = true;
                    f.Show();
                }
                else
                {
                    f.Visible = false;
                }
            }
            //start a new instance
            if (backup == false)
            {
                frm.MdiParent = Global.background;
                frm.Scale(new SizeF(1.3F, 1.3F));
                frm.AutoScaleMode = AutoScaleMode.Font;
                frm.StartPosition = FormStartPosition.CenterScreen;
                //insert into dictionary with the group no
                int val;
                bool exists = form_group.TryGetValue(frm.Name, out val);
                if (exists == false)
                {
                    form_group[frm.Name] = 10;
                }
                frm.Show();
            }
            this.LayoutMdi(MdiLayout.Cascade);
        }
        private void applicationSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool settings = false;
            O_SettingsForm frm = new O_SettingsForm();
            //frm.MdiParent = Global.background;
            //frm.Scale(new SizeF(1.3F, 1.3F));
            //frm.AutoScaleMode = AutoScaleMode.Font;
            //frm.StartPosition = FormStartPosition.CenterScreen;
            //frm.Show();
            //check if the form is already open but hidden
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == frm.Name)
                {
                    settings = true;
                    f.Show();
                    f.WindowState=FormWindowState.Normal;
                }
                else
                {
                    //f.Visible = false;
                }
            }
            //start a new instance if not already open
            if (settings == false)
            {
                frm.MdiParent = Global.background;
                frm.Scale(new SizeF(1.3F, 1.3F));
                frm.AutoScaleMode = AutoScaleMode.Font;
                frm.StartPosition = FormStartPosition.Manual;
                frm.Location = new Point(0, 0);
                //insert into dictionary with the group no
                int val;
                bool exists = form_group.TryGetValue(frm.Name, out val);
                if (exists == false)
                {
                    form_group[frm.Name] = 11;
                }
                frm.Show();
            }
        }
        private void manageUsersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool users = false;
            O_U_ManageUsersForm frm = new O_U_ManageUsersForm();
            //check if the form is already open but hidden
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == frm.Name)
                {
                    users = true;
                    f.Show();
                }
                else
                {
                    f.Visible = false;
                }
            }
            //start a new instance
            if (users == false)
            {
                frm.MdiParent = Global.background;
                frm.Scale(new SizeF(1.3F, 1.3F));
                frm.AutoScaleMode = AutoScaleMode.Font;
                frm.StartPosition = FormStartPosition.CenterScreen;
                //insert into dictionary with the group no
                int val;
                bool exists = form_group.TryGetValue(frm.Name, out val);
                if (exists == false)
                {
                    form_group[frm.Name] = 12;
                }
                frm.Show();
            }
            this.LayoutMdi(MdiLayout.Cascade);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql = "select Active_User from firms_list where Firm_ID = " + Global.firmid;
            DataTable dt = mc.runQuery(sql);
            if(dt==null)
            {
                mc.ErrorBox("Could not connect to database");
                return;
            }
            if(dt.Rows[0][0].ToString()==Global.accessToken)
            {
                editAccessButton.BackColor = Color.LawnGreen;
                editAccessButton.Text = "Edit Access";
            }
            else if (dt.Rows[0][0].ToString() == "")
            {
                //No one is having edit access, claim it
                sql = "update Firms_List set Active_User = '" + Global.accessToken + "' where firm_id = " + Global.firmid;
                dt = mc.runQuery(sql);
                if (dt == null)
                {
                    mc.ErrorBox("Couldn't connect to database and claim edit access");
                    editAccessButton.BackColor = Color.OrangeRed;
                    editAccessButton.Text = "No Edit Access";
                }
                else
                {
                    editAccessButton.BackColor = Color.LawnGreen;
                    editAccessButton.Text = "Edit Access";
                }
            }
            else
            {
                editAccessButton.BackColor = Color.OrangeRed;
                editAccessButton.Text = "No Edit Access";
            }
            if (this.editAccessButton.Text=="No Edit Access")
            {
                if(Global.access==2)
                {
                    string username = dt.Rows[0][0].ToString().Substring(0, dt.Rows[0][0].ToString().Length - 5);
                    sql = "SELECT AccessLevel FROM Users WHERE Username = '" + username + "'";
                    DataTable d = c.runQuery(sql);
                    if (d.Rows[0][0].ToString() == "1") return;
                }
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to gain access forcefully?\nSomeone may have not logged out.", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.No)
                {
                    return;
                }
                //update token in firms table
                sql = "update Firms_List set Active_User = '" + Global.accessToken + "' where firm_id = " + Global.firmid;
                dt= mc.runQuery(sql);
                if(dt!=null)
                {
                    editAccessButton.BackColor = Color.LawnGreen;
                    editAccessButton.Text = "Edit Access";
                    mc.SuccessBox("Gained Edit Access");
                }
                else
                {
                    mc.ErrorBox("Failed to connect to database");
                    return;
                }
            }
        }

        private void outstandingPaymentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CR_P_outstandingPaymentReport f = new CR_P_outstandingPaymentReport(this.mc);
            f.MdiParent = Global.background;
            f.StartPosition = FormStartPosition.CenterScreen;
            f.Show();
        }

        private void salesReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CR_S_salesReport f = new CR_S_salesReport(this.mc);
            f.MdiParent = Global.background;
            f.StartPosition = FormStartPosition.CenterScreen;
            f.Show();
        }
    }
}
