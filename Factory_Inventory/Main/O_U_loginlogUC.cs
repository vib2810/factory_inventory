using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Factory_Inventory.Factory_Classes;

namespace Factory_Inventory
{
    public partial class O_U_loginlogUC : UserControl
    {
        private DbConnect c;
        public O_U_loginlogUC()
        {
            InitializeComponent();
            this.c = new DbConnect();
        }
        public void loadUserData()
        {
            DataTable d = c.getUserLog();
            d.DefaultView.Sort = "LoginTime DESC";
            dataGridView1.DataSource = d;
            dataGridView1.Columns["LoginTime"].Width = 180;
            dataGridView1.Columns["LoginTime"].HeaderText= "Login Time";
            dataGridView1.Columns["LogoutTime"].Width = 180;
            dataGridView1.Columns["LogoutTime"].HeaderText = "Logout Time";
        }

    }
}
