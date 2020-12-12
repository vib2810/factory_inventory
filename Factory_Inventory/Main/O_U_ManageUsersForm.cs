using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;
using MyCoolCompany.Shuriken;
using Factory_Inventory.Factory_Classes;
using System.Data.SqlClient;
using System.IO;

namespace Factory_Inventory
{
    public partial class O_U_ManageUsersForm : Form
    {
        public bool wait = false;
        public string backupname;
        public DbConnect c;
        public O_U_ManageUsersForm()
        {
            InitializeComponent();
            c = new DbConnect(); 
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            ControlPaint.DrawBorder(e.Graphics, ClientRectangle,
                                         Color.Black, 10, ButtonBorderStyle.Inset,
                                         Color.Black, 10, ButtonBorderStyle.Inset,
                                         Color.Black, 10, ButtonBorderStyle.Inset,
                                         Color.Black, 10, ButtonBorderStyle.Inset);
        }
        
    }
}
