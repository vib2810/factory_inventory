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
            this.loadDatabase();
            a.set_dgv_column_sort_state(dataGridView1, DataGridViewColumnSortMode.NotSortable);
        }

        public void loadDatabase()
        {
            DataTable d = a.runQuery("select * from (select *, ROW_NUMBER() OVER(PARTITION BY Employee_ID ORDER BY Change_Date DESC) as Rank from (select T.*, Salary.Change_Date, Salary.Salary from (SELECT Employees.Employee_ID, Employees.Date_Of_Joining, Employees.Employee_Name, Employees.End_Date, Group_Names.Group_Name FROM Employees INNER JOIN Group_Names ON Employees.Group_ID = Group_Names.Group_ID where Employees.End_Date IS NULL) as T INNER JOIN Salary ON T.Employee_ID = Salary.Employee_ID) as B) as A where A.Rank = 1");
            d.Columns.Add("SLNO", typeof(int)).SetOrdinal(0);
            dataGridView1.DataSource = d;
            this.dataGridView1.Columns.OfType<DataGridViewColumn>().ToList().ForEach(col => col.Visible = false);
            this.dataGridView1.Columns["Employee_Name"].Visible = true;
            this.dataGridView1.Columns["Employee_Name"].DisplayIndex = 0;
            this.dataGridView1.Columns["Employee_Name"].HeaderText = "Employee Name";
            this.dataGridView1.Columns["Group_Name"].Visible = true;
            this.dataGridView1.Columns["Group_Name"].DisplayIndex = 2;
            this.dataGridView1.Columns["Group_Name"].HeaderText = "Group Name";
            this.dataGridView1.Columns["Date_Of_Joining"].Visible = true;
            this.dataGridView1.Columns["Date_Of_Joining"].DisplayIndex = 4;
            this.dataGridView1.Columns["Date_Of_Joining"].HeaderText = "Date of Joining";
            this.dataGridView1.Columns["Salary"].Visible = true;
            this.dataGridView1.Columns["Salary"].DisplayIndex = 6;
            this.dataGridView1.Columns["Salary"].HeaderText = "Salary";
            a.auto_adjust_dgv(dataGridView1);
            a.set_dgv_column_sort_state(dataGridView1, DataGridViewColumnSortMode.NotSortable);
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView1.Font, FontStyle.Bold);
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

        private void editButton_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count <= 0) return;
            if (dataGridView1.SelectedRows[0].Index < 0 || dataGridView1.SelectedRows[0].Index >= dataGridView1.Rows.Count)
            {
                return;
            }
            DataRow input_row = (dataGridView1.SelectedRows[0].DataBoundItem as DataRowView).Row;
            A_1_AddEmployee f = new A_1_AddEmployee(input_row, this);
            f.ShowDialog();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if(dataGridView1.SelectedRows[0].Index < 0 || dataGridView1.SelectedRows[0].Index >= dataGridView1.Rows.Count)
            {
                return;
            }
            A_1_AddEmployee f = new A_1_AddEmployee((dataGridView1.SelectedRows[0].DataBoundItem as DataRowView).Row, this);
            a.runQuery("UPDATE Employees SET End_Date = '" + DateTime.Today.ToString("yyyy-MM-dd").Substring(0, 10) + "' WHERE Employee_ID = " + int.Parse(dataGridView1.SelectedRows[0].Cells["Employee_ID"].Value.ToString()) + "");
            loadDatabase();
        }
    }
}
