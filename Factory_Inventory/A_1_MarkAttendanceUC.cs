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

namespace Factory_Inventory
{
    public partial class A_1_MarkAttendanceUC : UserControl
    {
        private AttConnect a = new AttConnect();
        public A_1_MarkAttendanceUC()
        {
            InitializeComponent();
        }

        public void loadDatabase()
        {
            DataTable d = a.runQuery("select * from (select * from Attendance_Log where Record_Date='"+ this.dateTimePickerDTP.Value.Date.ToString("yyyy-MM-dd").Substring(0, 10) +"') as T right outer join Employees on T.Employee_ID= Employees.Employee_ID");
            dataGridView1.DataSource = d;
            this.dataGridView1.Columns.OfType<DataGridViewColumn>().ToList().ForEach(col => col.Visible = false);
            this.dataGridView1.Columns["Employee_Name"].Visible = true;
            this.dataGridView1.Columns["Employee_Name"].DisplayIndex = 0;
            this.dataGridView1.Columns["Employee_Name"].HeaderText = "Employee Name";
            this.dataGridView1.Columns["Attendance"].Visible = true;
            this.dataGridView1.Columns["Attendance"].DisplayIndex = 2;
            this.dataGridView1.Columns["Attendance"].HeaderText = "Attendance";
            this.dataGridView1.Columns["Comments"].Visible = true;
            this.dataGridView1.Columns["Comments"].DisplayIndex = 4;
            this.dataGridView1.Columns["Comments"].HeaderText = "Comments";
            a.auto_adjust_dgv(this.dataGridView1);
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            a.runQuery("DELETE FROM Attendance_Log WHERE Record_Date = '" + this.dateTimePickerDTP.Value.Date.ToString("yyyy-MM-dd").Substring(0, 10) + "'");
            for(int i=0;i<dataGridView1.Rows.Count;i++)
            {
                if(a.Cell_Not_NullOrEmpty(dataGridView1, i, 0, "Comments") && !a.Cell_Not_NullOrEmpty(dataGridView1, i, 0, "Attendance"))
                {
                    a.ErrorBox("Cannot enter comments if no attendance is marked for Employee: "+ dataGridView1.Rows[i].Cells["Employee_Name"].Value.ToString());
                    return;
                }
                if(a.Cell_Not_NullOrEmpty(dataGridView1, i, 0, "Attendance"))
                {
                    if(a.Cell_Not_NullOrEmpty(dataGridView1, i, 0, "Comments"))
                    {
                        string comment = dataGridView1.Rows[i].Cells["Comments"].Value.ToString();
                        a.runQuery("INSERT INTO Attendance_Log VALUES (" + int.Parse(dataGridView1.Rows[i].Cells["Employee_ID"].Value.ToString()) + ", '" + this.dateTimePickerDTP.Value.Date.ToString("yyyy-MM-dd").Substring(0, 10) + "'," + float.Parse(dataGridView1.Rows[i].Cells["Attendance"].Value.ToString()) + ", '" + comment + "')");
                    }
                    else
                    {
                        a.runQuery("INSERT INTO Attendance_Log (Employee_ID, Record_Date, Attendance) VALUES (" + int.Parse(dataGridView1.Rows[i].Cells["Employee_ID"].Value.ToString()) + ", '" + this.dateTimePickerDTP.Value.Date.ToString("yyyy-MM-dd").Substring(0, 10) + "'," + float.Parse(dataGridView1.Rows[i].Cells["Attendance"].Value.ToString()) + ")");
                    }
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
            loadDatabase();
        }

        private void dateTimePickerDTP_ValueChanged(object sender, EventArgs e)
        {
            this.loadDatabase();
        }
    }
}
