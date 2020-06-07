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
        public TwistERP()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
        }
        public void show_form(Form f)
        {
            f.FormBorderStyle = FormBorderStyle.FixedSingle;
            if (f.Name.ToString().StartsWith("M_I"))
            {
                f.Show();
            }
            else
            {
                //if(f.StartPosition!= FormStartPosition.CenterScreen)
                //{
                //    f.StartPosition = FormStartPosition.Manual;
                //    f.Location = new System.Drawing.Point((int)(Screen.PrimaryScreen.Bounds.Width/20), Screen.PrimaryScreen.Bounds.Height/20);
                //}
                f.MaximizeBox = false;
                f.MdiParent = this;
            }
            f.Show();
            this.LayoutMdi(MdiLayout.Cascade);
            foreach (var child in this.MdiChildren)
            {
                if(child.Name== "M_1_MainS")
                {
                    child.SendToBack();
                    break;
                }
            }
        }

    }
}
