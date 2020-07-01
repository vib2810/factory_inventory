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
            loadDatabase();
        }

        public void loadDatabase()
        {
            DataTable d = a.runQuery("SELECT * FROM Employees INNER JOIN Group_Names ON Employees.Group_ID = Group_Names.Group_ID");
            d.Columns.Add("SLNO", typeof(int)).SetOrdinal(0);
            dataGridView1.DataSource = d;
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            A_1_AddEmployee f = new A_1_AddEmployee(this);
            f.ShowDialog();
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].Cells[0].Value == null)
            {
                dataGridView1.Rows[e.RowIndex].Cells[0].Value = e.RowIndex + 1;
            }
            else if(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString()=="")
            {
                dataGridView1.Rows[e.RowIndex].Cells[0].Value = e.RowIndex + 1;
            }
        }
    }
}
