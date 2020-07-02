using Factory_Inventory.Factory_Classes;
using Microsoft.SqlServer.Management.SqlParser.Diagnostics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Factory_Inventory
{
    public partial class A_1_AddEmployee : Form
    {
        private AttConnect a = new AttConnect();
        private DataTable dt;
        private A_1_EmployeesUC parent;
        private DataRow input_row;
        private bool edit_form=false;
        private int employee_id;
        //Dictionary<int, string> group_id_to_name = new Dictionary<int, string>();
        public A_1_AddEmployee(A_1_EmployeesUC a_1_EmployeesUC)
        {
            this.parent = a_1_EmployeesUC;
            InitializeComponent();
            dataGridView1.Columns["Date"].DefaultCellStyle.Format = "dd/MM/yyyy";
            this.dataGridView1.Rows.Add();
            this.dataGridView1.Rows[0].Cells[0].Value = this.joiningdateDTP.Value.Date;
        }
        public A_1_AddEmployee(DataRow input_row, A_1_EmployeesUC a_1_EmployeesUC)
        {
            this.edit_form = true;
            this.parent = a_1_EmployeesUC;
            InitializeComponent();
            this.addButton.Visible = true;
            this.editButton.Visible = false;
            dataGridView1.Columns["Date"].DefaultCellStyle.Format = "dd/MM/yyyy";
            this.input_row = input_row;
            this.parent = a_1_EmployeesUC;
            this.nameTB.Text = input_row["Employee_Name"].ToString();
            this.employee_id = int.Parse(input_row["Employee_ID"].ToString());
            this.groupCB.FindStringExact(input_row["Group_Name"].ToString());
            this.joiningdateDTP.Value = Convert.ToDateTime(input_row["Date_Of_Joining"].ToString());
            DataTable salaries = a.runQuery("select change_date, salary from Salary inner join Employees on Salary.Employee_ID=Employees.Employee_ID where Salary.Employee_ID="+ input_row["Employee_ID"].ToString()+" order by Change_Date asc");
            for(int i=0; i<salaries.Rows.Count; i++)
            {
                dataGridView1.Rows.Add(salaries.Rows[i][0], salaries.Rows[i][1]);
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            dt = a.getTableData("Group_Names", "*", null);
            List<string> datasource = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //this.group_id_to_name[int.Parse(dt.Rows[i]["Group_ID"].ToString())] = dt.Rows[i]["Group_Name"].ToString();
                datasource.Add(dt.Rows[i]["Group_Name"].ToString());
            }
            this.groupCB.DataSource = datasource;
            this.groupCB.DisplayMember = "Name";
            this.groupCB.DropDownStyle = ComboBoxStyle.DropDownList;
            this.dataGridView1.Columns[1].ReadOnly = false;
        }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if(this.edit_form==false)
            {
                this.dataGridView1.Rows[this.dataGridView1.Rows.Count - 1].Cells[0].Value = this.joiningdateDTP.Value.Date;
            }
        }

        //clicks
        private void Submit_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(nameTB.Text))
            {
                a.ErrorBox("Please enter name");
                return;
            }
            if(this.edit_form == true)
            {
                if (dataGridView1.Rows.Count <= 0)
                {
                    a.ErrorBox("Please enter atleat one entry in salary table");
                    return;
                }
                Console.WriteLine(this.joiningdateDTP.Value.Date.ToString("dd-MM-yyyy").Substring(0, 10));
                Console.WriteLine(this.dataGridView1.Rows[0].Cells[0].Value.ToString());
                if (this.joiningdateDTP.Value.Date.ToString("dd-MM-yyyy").Substring(0, 10) != this.dataGridView1.Rows[0].Cells[0].Value.ToString().Substring(0, 10))
                {
                    a.ErrorBox("Joining date should be equal to the first date salary table");
                    return;
                }
            }
            float salary = 0F;
            for (int i=0;i<dataGridView1.Rows.Count;i++)
            {
                if (this.dataGridView1.Rows[i].Cells[1].Value == null)
                {
                    a.ErrorBox("Please enter the salary in row: "+(i+1).ToString());
                    return;
                }
                try
                {
                    salary = float.Parse(this.dataGridView1.Rows[0].Cells[1].Value.ToString());
                }
                catch
                {
                    a.ErrorBox("Please enter numeric salary in row: " + (i + 1).ToString());
                    return;
                }
            }
            
            if(this.edit_form == false)
            {
                bool added = a.addEmployee(this.nameTB.Text, int.Parse(dt.Rows[this.groupCB.SelectedIndex]["Group_ID"].ToString()), this.joiningdateDTP.Value, salary);
                if(added == true)
                {
                    parent.loadDatabase();
                    this.Close();
                }
            }
            else
            {
                string sql = "begin transaction; begin try; ";
                sql += "UPDATE Employees SET Employee_Name = '"+this.nameTB.Text+ "', Group_ID = "+ int.Parse(dt.Rows[this.groupCB.SelectedIndex]["Group_ID"].ToString())+", Date_Of_Joining = '"+ this.joiningdateDTP.Value.Date.ToString("yyyy-MM-dd").Substring(0, 10)+"' WHERE Employee_ID = "+this.employee_id+"; ";
                sql += "DELETE FROM Salary WHERE Employee_ID = " + this.employee_id + "; ";
                for(int i=0;i<this.dataGridView1.Rows.Count;i++)
                {
                    DateTime d = DateTime.ParseExact(this.dataGridView1.Rows[i].Cells["Date"].Value.ToString().Substring(0, 10), "dd-MM-yyyy", CultureInfo.InvariantCulture);
                    string date = d.ToString("yyyy-MM-dd");
                    sql += "INSERT INTO Salary VALUES (" + this.employee_id + ", '" + date + "', " + float.Parse(this.dataGridView1.Rows[i].Cells["Salary"].Value.ToString()) + "); ";
                }
                sql += "commit transaction; end try begin catch; rollback transaction; end catch";
                a.runQuery(sql);
                this.Close();
                parent.loadDatabase();
            }
        }
        private void addButton_Click(object sender, EventArgs e)
        {
            setSalary f = new setSalary(DateTime.Now);
            f.ShowDialog();
            if(f.values_set==true)
            {
                dataGridView1.Rows.Add(f.result, f.salary);
                this.dataGridView1.Sort(this.dataGridView1.Columns["Date"], ListSortDirection.Ascending);
            }
        }
        private void editButton_Click(object sender, EventArgs e)
        {
            int row_index = -1;
            if (dataGridView1.SelectedRows.Count > 0) row_index=dataGridView1.SelectedRows[0].Index;
            if (dataGridView1.SelectedCells.Count > 0 && row_index < 0) row_index = dataGridView1.SelectedCells[0].RowIndex;
            if (row_index < 0) return;
            DateTime d = Convert.ToDateTime(dataGridView1.SelectedRows[0].Cells["Date"].Value);
            float salary = -1F;
            try
            {
                salary = float.Parse(dataGridView1.SelectedRows[0].Cells["Salary"].Value.ToString());
            }
            catch { };
            setSalary f = new setSalary(d, salary);
            f.ShowDialog();
            if (f.values_set == true)
            {
                dataGridView1.SelectedRows[0].SetValues(f.result, f.salary);
            }
        }
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int count = dataGridView1.SelectedRows.Count;
            for (int i = 0; i < count; i++)
            {
                dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);
            }
        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                dataGridView1.Rows[e.RowIndex].Selected = true;
            }
        }
    }
}
