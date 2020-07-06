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
using System.Runtime.CompilerServices;
using System.Windows;

namespace Factory_Inventory
{
    public partial class A_1_MarkAttendanceUC : UserControl
    {
        private AttConnect a = new AttConnect();
        public A_1_MarkAttendanceUC()
        {
            InitializeComponent();
            this.loadDatabase();
            this.Visible = false;
            this.Visible = true;
            a.set_dgv_column_sort_state(dataGridView1, DataGridViewColumnSortMode.NotSortable);
            this.dateTimePickerDTP.MaxDate = DateTime.Now;
        }
        public void loadDatabase()
        {
            DataTable d = a.runQuery("select Employees.Employee_ID as Emp_ID,* from (select * from Attendance_Log where Record_Date='" + this.dateTimePickerDTP.Value.Date.ToString("yyyy-MM-dd").Substring(0, 10) + "') as T right outer join Employees on T.Employee_ID= Employees.Employee_ID where Date_Of_Joining <= '" + this.dateTimePickerDTP.Value.Date.ToString("yyyy-MM-dd").Substring(0, 10) + "' and End_Date >= '" + this.dateTimePickerDTP.Value.Date.ToString("yyyy-MM-dd").Substring(0, 10) + "' or End_Date is null");
            dataGridView1.DataSource = d;
            this.dataGridView1.Columns.OfType<DataGridViewColumn>().ToList().ForEach(col => col.Visible = false);
            this.dataGridView1.Columns["Employee_Name"].Visible = true;
            this.dataGridView1.Columns["Employee_Name"].DisplayIndex = 0;
            this.dataGridView1.Columns["Employee_Name"].HeaderText = "Employee Name";
            this.dataGridView1.Columns["Employee_Name"].ReadOnly = true;
            this.dataGridView1.Columns["Attendance"].Visible = true;
            this.dataGridView1.Columns["Attendance"].DisplayIndex = 1;
            this.dataGridView1.Columns["Attendance"].HeaderText = "Attendance";
            this.dataGridView1.Columns["Comments"].Visible = true;
            this.dataGridView1.Columns["Comments"].DisplayIndex = 2;
            this.dataGridView1.Columns["Comments"].HeaderText = "Comments";
            
            a.auto_adjust_dgv(this.dataGridView1);
            this.panel1.BackColor = Color.Green;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView1.Font, System.Drawing.FontStyle.Bold);
        }
        private void AddButton_Click(object sender, EventArgs e)
        {
            //checks
            float att = 0F;
            for (int i=0;i<dataGridView1.Rows.Count;i++)
            { 
                if (a.Cell_Not_NullOrEmpty(dataGridView1, i, 0, "Comments") && !a.Cell_Not_NullOrEmpty(dataGridView1, i, 0, "Attendance"))
                {
                    a.ErrorBox("Cannot enter comments if no attendance is marked for Employee: "+ dataGridView1.Rows[i].Cells["Employee_Name"].Value.ToString());
                    return;
                }
                try
                {
                    att = float.Parse(dataGridView1.Rows[i].Cells["Attendance"].Value.ToString());
                    if(att>1F || att<0F)
                    {
                        a.ErrorBox("Attendance for "+ dataGridView1.Rows[i].Cells["Employee_Name"].Value.ToString()+" should be between 0 and 1");
                        return;
                    }
                }
                catch
                {
                    if(a.Cell_Not_NullOrEmpty(dataGridView1, i, 0, "Attendance"))
                    {
                        a.ErrorBox("Attendance for " + dataGridView1.Rows[i].Cells["Employee_Name"].Value.ToString() + " should be between 0 and 1");
                        return;
                    }
                }
            }
            //delete previous
            a.runQuery("DELETE FROM Attendance_Log WHERE Record_Date = '" + this.dateTimePickerDTP.Value.Date.ToString("yyyy-MM-dd").Substring(0, 10) + "'");
            //add attendance
            for (int i=0;i<dataGridView1.Rows.Count;i++)
            {
                if (a.Cell_Not_NullOrEmpty(dataGridView1, i, 0, "Attendance"))
                {
                    string comment;
                    if (a.Cell_Not_NullOrEmpty(dataGridView1, i, 0, "Comments"))
                    {
                        comment = dataGridView1.Rows[i].Cells["Comments"].Value.ToString();
                    }
                    else
                    {
                        comment = "";
                    }
                    a.runQuery("INSERT INTO Attendance_Log VALUES (" + int.Parse(dataGridView1.Rows[i].Cells["Emp_ID"].Value.ToString()) + ", '" + this.dateTimePickerDTP.Value.Date.ToString("yyyy-MM-dd").Substring(0, 10) + "'," + float.Parse(dataGridView1.Rows[i].Cells["Attendance"].Value.ToString()) + ", '" + comment + "')");
                }
            }
            this.loadDatabase();
        }
        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            if (e.RowIndex != dataGridView1.Rows.Count - 1)
            {
                dataGridView1.Rows[e.RowIndex].Cells[0].Value = e.RowIndex + 1;
            }
        }
        private void A_1_EmployeesUC_Load(object sender, EventArgs e)
        {
        }
        private void dateTimePickerDTP_ValueChanged(object sender, EventArgs e)
        {
            this.loadDatabase();
        }
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            
        }
        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            this.panel1.BackColor = Color.Red;
        }
    }
}
