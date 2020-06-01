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
            this.DoubleBuffered = true;
            InitializeComponent();
        }
        public void show_form(dynamic f)
        {
            f.MdiParent = this;
            f.Show();
        }
    }
}
