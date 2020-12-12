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
        List<string> attendance_forms = new List<string>();
        List<string> backup_form = new List<string>();
        public M_1_MainS main_form = null;
        public bool logout = false;
        public TwistERP(string user, int access, string firmname)
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            string access_type = "";
            if (access == 1) access_type = "Super User";
            else if (access == 2) access_type = "User";
            this.usertoolStripButton1.Text = "Logged in as " + user + ": " + access_type;
            this.firmtoolStripButton2.Text = firmname;
        }

        //group 0- ERP
        //group 1- Attendance
        public void show_form(Form f, int group=0)
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
            }
            if (group==1)
            {
                if(!this.attendance_forms.Contains(f.Name)) this.attendance_forms.Add(f.Name);
                f.MaximizeBox = false;
                f.MdiParent = this;
                f.Show();
            }
            this.LayoutMdi(MdiLayout.Cascade);
        }
        private void eRPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form f in this.MdiChildren)
            {
                if (this.attendance_forms.Contains(f.Name) || this.backup_form.Contains(f.Name))
                {
                    f.Visible = false;
                }
                else f.Visible = true;
            }
            this.LayoutMdi(MdiLayout.Cascade);
        }
        private void tradingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            A_1_MainS frm = new A_1_MainS();
            bool a_1_mains = false;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == frm.Name)
                {
                    a_1_mains = true;
                }
                if (this.attendance_forms.Contains(f.Name))
                {
                    f.Visible = true;
                }
                else
                {
                    f.Visible = false;
                }
            }
            if (a_1_mains == false)
            {
                frm.MdiParent = Global.background;
                frm.Scale(new SizeF(1.3F, 1.3F));
                frm.AutoScaleMode = AutoScaleMode.Font;
                frm.StartPosition = FormStartPosition.CenterScreen;
                if (!this.attendance_forms.Contains(frm.Name)) this.attendance_forms.Add(frm.Name);
                frm.Show();
            }
            this.LayoutMdi(MdiLayout.Cascade);
        }
        private void attendanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            A_1_MainS frm = new A_1_MainS();
            bool a_1_mains = false;
            foreach (Form f in this.MdiChildren)
            {
                if(f.Name == frm.Name)
                {
                    a_1_mains = true;
                }
                if(this.attendance_forms.Contains(f.Name))
                {
                    f.Visible = true;
                }
                else
                {
                    f.Visible = false;
                }
            }
            if(a_1_mains == false)
            {
                frm.MdiParent = Global.background;
                frm.Scale(new SizeF(1.3F, 1.3F));
                frm.AutoScaleMode = AutoScaleMode.Font;
                frm.StartPosition = FormStartPosition.CenterScreen;
                if (!this.attendance_forms.Contains(frm.Name)) this.attendance_forms.Add(frm.Name);
                frm.Show();
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
        private void backupRestoreStripMenuItem_Click(object sender, EventArgs e)
        {
            bool backup = false;
            M_BackupRestore frm = new M_BackupRestore();
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
            if (backup == false)
            {
                frm.MdiParent = Global.background;
                frm.Scale(new SizeF(1.3F, 1.3F));
                frm.AutoScaleMode = AutoScaleMode.Font;
                frm.StartPosition = FormStartPosition.CenterScreen;
                if (!this.backup_form.Contains(frm.Name)) this.backup_form.Add(frm.Name);
                frm.Show();
            }
            this.LayoutMdi(MdiLayout.Cascade);
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            M_settings f = new M_settings();
            Global.background.show_form(f);
        }

    }
}
