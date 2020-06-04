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
            f.MdiParent = this;
            f.MaximizeBox= false;
            f.FormBorderStyle = FormBorderStyle.FixedSingle;
            //this.MdiChildren[this.MdiChildren.Length-1].MinimizeBox
            f.Show();
        }

    }
}
