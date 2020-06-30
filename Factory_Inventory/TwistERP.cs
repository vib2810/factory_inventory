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
        int hello = 0;
        List<Form> attendance_forms = new List<Form>();
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
                this.attendance_forms.Add(f);
                f.Location = new Point(0, 0);
                f.MaximizeBox = false;
                f.MdiParent = this;
                f.Show();
            }
            this.LayoutMdi(MdiLayout.Cascade);
        }


        private void attendanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach(Form f in this.MdiChildren)
            {
                f.Visible = false;
            }
            for (int i = 0; i < this.attendance_forms.Count; i++) this.attendance_forms[i].Visible = true;
            this.LayoutMdi(MdiLayout.Cascade);
        }

        private void eRPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form f in this.MdiChildren)
            {
                if (this.attendance_forms.Contains(f))
                {
                    f.Visible = false;
                }
                else f.Visible = true;
            }
            this.LayoutMdi(MdiLayout.Cascade);
        }
    }
}
