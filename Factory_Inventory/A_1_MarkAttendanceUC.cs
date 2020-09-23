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
            this.Visible = false;
            this.Visible = true;
            a.set_dgv_column_sort_state(dataGridView1, DataGridViewColumnSortMode.NotSortable);
            this.dateTimePickerDTP.MaxDate = DateTime.Now;
            this.label1.Text = "Current date is: " + dateTimePickerDTP.Value.Date.ToString("dd-MM-yyyy").Substring(0, 10).Replace('/', '-');
        }
        public void loadDatabase()
        {
            string dat = dateTimePickerDTP.Value.Date.ToString("yyyy-MM-dd").Substring(0, 10).Replace('/', '-');
            DataTable d = a.runQuery("declare @dat date = '"+dat+"'; select Employees.Employee_Name, Employees.Group_ID, A.*  from Employees inner join (select Employee_Session.Session_ID as Sess_ID, Employee_Session.Employee_ID, T.* from (select * from Attendance_Log where Record_Date=@dat) as T right outer join Employee_Session on T.Session_ID = Employee_Session.Session_ID where Employee_Session.Begin_Date <= @dat and (End_Date >= @dat or End_Date is null)) as A on A.Employee_ID=Employees.Employee_ID");
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

        //Clicks
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
            a.runQuery("DELETE FROM Attendance_Log WHERE Record_Date = '" + this.dateTimePickerDTP.Value.Date.ToString("yyyy-MM-dd").Substring(0, 10).Replace('/', '-') + "'");
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
                    a.runQuery("INSERT INTO Attendance_Log VALUES (" + int.Parse(dataGridView1.Rows[i].Cells["Sess_ID"].Value.ToString()) + ", '" + this.dateTimePickerDTP.Value.Date.ToString("yyyy-MM-dd").Substring(0, 10).Replace('/', '-') + "'," + float.Parse(dataGridView1.Rows[i].Cells["Attendance"].Value.ToString()) + ", '" + comment + "')");
                }
            }
            this.loadDatabase();
        }

        //DTP
        private void dateTimePickerDTP_ValueChanged(object sender, EventArgs e)
        {
            this.loadDatabase();
            this.label1.Text = "Current date is: " + dateTimePickerDTP.Value.Date.ToString("dd-MM-yyyy").Substring(0, 10);
        }
        
        //DGV
        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            this.panel1.BackColor = Color.Red;
        }
    }
}
