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
    public partial class A_1_EmployeesUC : UserControl
    {
        private AttConnect a = new AttConnect();
        public A_1_EmployeesUC()
        {
            InitializeComponent();
        }

        

        public void loadDatabase()
        {
            DataTable d = new DataTable()/* = a.getEmployeeData()*/;
            d.Columns.Add("SLNO", typeof(int)).SetOrdinal(0);
            dataGridView1.DataSource = d;
        }



        private void confirmButton_Click(object sender, EventArgs e)
        {
                loadDatabase();
        }
    }
}
