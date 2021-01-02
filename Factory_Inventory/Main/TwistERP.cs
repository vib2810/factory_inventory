﻿using System;
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
        //List<string> attendance_forms = new List<string>();
        //List<string> backup_form = new List<string>();
        //List<string> trading_forms = new List<string>();
       // public M_1_MainS main_form = null;
        public bool logout = false;
        public TwistERP(string user, int access, string firmname)
        {
            InitializeComponent();
            Global.access = access;
            this.DoubleBuffered = true;
            string access_type = "";
            if (access == 1) access_type = "Super User";
            else if (access == 2) access_type = "User";
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

            if (group==0)
            {
                if (f.Name.ToString().StartsWith("M_I"))
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
            M_BackupRestore frm = new M_BackupRestore();
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
            M_settings frm = new M_settings();
            //check if the form is already open but hidden
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == frm.Name)
                {
                    settings = true;
                    f.Show();
                }
                else
                {
                    f.Visible = false;
                }
            }
            //start a new instance
            if (settings == false)
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
                    form_group[frm.Name] = 11;
                }
                frm.Show();
            }
            this.LayoutMdi(MdiLayout.Cascade);
        }
    }
}