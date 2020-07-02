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
        public TwistERP()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
        }

        //group 0- ERP
        //group 1- Attendance
        public void show_form(Form f, int group=0)
        {
            f.FormBorderStyle = FormBorderStyle.FixedSingle;
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
            foreach (Form f in this.MdiChildren)
            {
                if (this.attendance_forms.Contains(f.Name))
                {
                    f.Visible = false;
                }
            }

            BackupRestore frm = new BackupRestore();
            bool backup = false;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == frm.Name)
                {
                    backup = true;
                }
                if (!this.attendance_forms.Contains(f.Name))
                {
                    f.Visible = false;
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
        }
    }
}
