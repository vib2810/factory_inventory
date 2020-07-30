using Factory_Inventory.Factory_Classes;
using Microsoft.SqlServer.Management.SqlParser.Diagnostics;
using Microsoft.SqlServer.Management.SqlParser.MetadataProvider;
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
        private DataTable deleted_sessions = new DataTable();

        public A_1_AddEmployee(A_1_EmployeesUC a_1_EmployeesUC)
        {
            this.parent = a_1_EmployeesUC;
            InitializeComponent();
            //DGV setup
            this.dataGridView1.Rows.Add(DateTime.Now, "");
            this.dataGridView2.Rows.Add("Session Added", DateTime.Now, "");

            //Buttons
            this.addButton.Enabled = false;
            this.startSessionButton.Enabled = false;
            this.endSessionButton.Enabled = false;
            this.deleteSessionButton.Enabled = false;
        }
        public A_1_AddEmployee(DataRow input_row, A_1_EmployeesUC a_1_EmployeesUC)
        {
            this.edit_form = true;
            this.parent = a_1_EmployeesUC;
            
            InitializeComponent();
            
            //buttons
            this.input_row = input_row;
            this.nameTB.Text = input_row["Employee_Name"].ToString();
            this.employee_id = int.Parse(input_row["Employee_ID"].ToString());
            this.groupCB.FindStringExact(input_row["Group_Name"].ToString());
            
            //Fill DGV1 (Salaries)
            DataTable salaries = a.runQuery("select change_date, salary from Salary inner join Employees on Salary.Employee_ID=Employees.Employee_ID where Salary.Employee_ID="+ input_row["Employee_ID"].ToString()+" order by Change_Date asc");
            for(int i=0; i<salaries.Rows.Count; i++)
            {
                dataGridView1.Rows.Add(salaries.Rows[i][0], salaries.Rows[i][1]);
            }

            //Fill DGV2 (Session)
            DataTable session = a.runQuery("select * from Employee_Session where Employee_ID = " + this.employee_id + " order by Begin_Date asc");
            for(int i=0;i<session.Rows.Count;i++)
            {
                dataGridView2.Rows.Add(session.Rows[i]["Session_ID"], session.Rows[i]["Begin_Date"], session.Rows[i]["End_Date"]);
                if(string.IsNullOrEmpty(session.Rows[i]["End_Date"].ToString()))
                {
                    dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.LawnGreen;
                    dataGridView2.Rows[i].DefaultCellStyle.SelectionBackColor = Color.Green;
                    dataGridView2.Rows[i].Cells["End_Date"].Value = "Active Session";
                }
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            dt = a.getTableData("Group_Names", "*", null);
            List<string> datasource = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                datasource.Add(dt.Rows[i]["Group_Name"].ToString());
            }
            this.groupCB.DataSource = datasource;
            this.groupCB.DisplayMember = "Name";
            this.groupCB.DropDownStyle = ComboBoxStyle.DropDownList;
            this.dataGridView1.Columns[1].ReadOnly = false;

            if(this.edit_form == true)
            {
                groupCB.SelectedIndex = groupCB.FindStringExact(this.input_row["Group_Name"].ToString());
            }

            //DGV Setup
            dataGridView1.Columns["Date"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dataGridView2.Columns["Begin_Date"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dataGridView2.Columns["End_Date"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView1.Font, FontStyle.Bold);
            dataGridView2.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView2.Font, FontStyle.Bold);
            a.set_dgv_column_sort_state(dataGridView1, DataGridViewColumnSortMode.NotSortable);
            a.set_dgv_column_sort_state(dataGridView2, DataGridViewColumnSortMode.NotSortable);

            foreach (DataGridViewColumn col in dataGridView2.Columns)
            {
                deleted_sessions.Columns.Add(col.Name);
            }
        }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if(this.edit_form==false)
            {
                //this.dataGridView1.Rows[this.dataGridView1.Rows.Count - 1].Cells[0].Value = this.joiningdateDTP.Value.Date;
            }
        }

        /*-------------Button clicks--------------------*/
        //Salary
        private void addButton_Click(object sender, EventArgs e)
        {
            setSalary f = new setSalary(DateTime.Now);
            f.ShowDialog();
            if (f.values_set == true)
            {
                dataGridView1.Rows.Add(f.result, f.salary);
                this.dataGridView1.Sort(this.dataGridView1.Columns["Date"], ListSortDirection.Ascending);
            }
        }
        private void editButton_Click(object sender, EventArgs e)
        {
            int row_index = -1;
            if (dataGridView1.SelectedRows.Count > 0) row_index = dataGridView1.SelectedRows[0].Index;
            if (dataGridView1.SelectedCells.Count > 0 && row_index < 0) row_index = dataGridView1.SelectedCells[0].RowIndex;
            if (row_index < 0) return;
            DateTime d = Convert.ToDateTime(dataGridView1.Rows[row_index].Cells["Date"].Value.ToString());
            float salary = -1F;
            try
            {
                string temp = dataGridView1.Rows[row_index].Cells["Salary"].Value.ToString();
                salary = float.Parse(temp);
            }
            catch { };
            setSalary f = new setSalary(d, salary);
            f.ShowDialog();
            if (f.values_set == true)
            {
                dataGridView1.Rows[row_index].SetValues(f.result, f.salary);
                this.dataGridView2.Sort(this.dataGridView2.Columns["Begin_Date"], ListSortDirection.Ascending);
                if (this.edit_form == false) dataGridView2.Rows[0].Cells["Begin_Date"].Value = f.result;
            }
        }
        //Sessions
        private void endSessionButton_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedCells.Count <= 0) return;
            int rowindex = dataGridView2.SelectedCells[0].RowIndex;
            if (dataGridView2.Rows[rowindex].Cells["End_Date"].Value.ToString() == "Active Session")
            {
                setDate end = new setDate(DateTime.Now);
                end.set_Message("Enter End Date");
                end.ShowDialog();
                if (end.values_set == false) return;
                Console.WriteLine(Convert.ToDateTime(dataGridView2.Rows[rowindex].Cells["Begin_Date"].Value.ToString()));
                Console.WriteLine(end.result);
                if (Convert.ToDateTime(dataGridView2.Rows[rowindex].Cells["Begin_Date"].Value.ToString()).Date >= end.result.Date)
                {
                    a.ErrorBox("End date should be greater than start date of the session");
                    endSessionButton.PerformClick();
                    return;
                }
                dataGridView2.Rows[rowindex].Cells["End_Date"].Value = end.result;
                dataGridView2.Rows[rowindex].DefaultCellStyle.BackColor = Color.Coral;
                dataGridView2.Rows[rowindex].DefaultCellStyle.SelectionBackColor = Color.Orange;
            }
        }
        private void startSessionButton_Click(object sender, EventArgs e)
        {
            setDate start = new setDate(DateTime.Now);
            start.set_Message("Enter Start Date");
            start.ShowDialog();
            if (start.values_set == false)
            {
                Console.WriteLine("ret");
                return;
            }

            //check if start date is in between any previous start and end dates
            int open_session_index = -1;
            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                DateTime s_i = Convert.ToDateTime(dataGridView2.Rows[i].Cells["Begin_Date"].Value.ToString());
                DateTime e_i;
                try
                {
                    e_i = Convert.ToDateTime(dataGridView2.Rows[i].Cells["End_Date"].Value.ToString());
                }
                catch
                {
                    open_session_index = i;
                    continue;
                }
                if (start.result.Date >= s_i.Date && start.result.Date <= e_i.Date)
                {
                    a.ErrorBox("Start Date is in between Begin and End Dates of Session ID: " + dataGridView2.Rows[i].Cells["Session_ID"].Value.ToString());
                    this.startSessionButton.PerformClick();
                    return;
                }
            }

            Console.WriteLine("Open session index = " + open_session_index);
            if (open_session_index == -1)
            {
                DateTime last_end_date = Convert.ToDateTime(Convert.ToDateTime(dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells["End_Date"].Value.ToString()));
                if (start.result.Date > last_end_date.Date)
                {
                    Console.WriteLine("Case 1a");
                    dataGridView2.Rows.Add("Session Added", start.result, "Active Session");
                    dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.BackColor = Color.LawnGreen;
                    dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.SelectionBackColor = Color.Green;
                    return;
                }
                else
                {
                    while (true)
                    {
                        setDate end = new setDate(DateTime.Now);
                        end.set_Message("Enter End Date (Start Date: " + start.result.ToString().Substring(0, 10) + ")");
                        end.ShowDialog();
                        if (end.values_set == false) return;
                        if (end.result.Date <= start.result.Date)
                        {
                            a.ErrorBox("End Date should greater than start date");
                        }
                        for (int i = 0; i < dataGridView2.Rows.Count; i++)
                        {
                            DateTime s_this = Convert.ToDateTime(dataGridView2.Rows[i].Cells["Begin_Date"].Value.ToString());
                            if (s_this.Date >= start.result.Date && s_this.Date <= end.result.Date)
                            {
                                a.ErrorBox("End date should be between entered start date and start date of next session");
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Case 2c");
                                dataGridView2.Rows.Add("Session Added", start.result, end.result);
                                this.dataGridView2.Sort(this.dataGridView2.Columns["Begin_Date"], ListSortDirection.Ascending);
                                return;
                            }
                        }
                    }
                }
            }
            else
            {
                DateTime last_start_date = Convert.ToDateTime(Convert.ToDateTime(dataGridView2.Rows[open_session_index].Cells["Begin_Date"].Value.ToString()));
                if (start.result.Date == last_start_date.Date)
                {
                    Console.WriteLine("Case 2a");
                    a.ErrorBox("Start Date cannot be equal to the Begin Date of the open session (Session ID: " + dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells["Session_ID"].Value.ToString() + ")");
                    this.startSessionButton.PerformClick();
                    return;
                }
                else if (start.result.Date > last_start_date.Date)
                {
                    Console.WriteLine("Case 2b");
                    a.ErrorBox("Please close the last session before opening a new one after that");
                    this.startSessionButton.PerformClick();
                    return;
                }
                else
                {
                    while (true)
                    {
                        setDate end = new setDate(DateTime.Now);
                        end.set_Message("Enter End Date (Start Date: " + start.result.ToString().Substring(0, 10) + ")");
                        end.ShowDialog();
                        if (end.values_set == false) return;
                        if (end.result.Date <= start.result.Date)
                        {
                            a.ErrorBox("End Date should greater than start date");
                        }
                        for (int i = 0; i < dataGridView2.Rows.Count; i++)
                        {
                            DateTime s_this = Convert.ToDateTime(dataGridView2.Rows[i].Cells["Begin_Date"].Value.ToString());
                            if (s_this.Date >= start.result.Date && s_this.Date <= end.result.Date)
                            {
                                a.ErrorBox("End date should be between entered start date and start date of next session");
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Case 2c");
                                dataGridView2.Rows.Add("Session Added", start.result, end.result);
                                this.dataGridView2.Sort(this.dataGridView2.Columns["Begin_Date"], ListSortDirection.Ascending);
                                return;
                            }
                        }
                    }
                }
            }
        }
        private void deleteSessionButton_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedCells.Count <= 0) return;
            int rowindex = dataGridView2.SelectedCells[0].RowIndex;
            if (dataGridView2.Rows[rowindex].Cells["Session_ID"].Value.ToString() == "Session Added")
            {
                dataGridView2.Rows.RemoveAt(rowindex);
                return;
            }
            DialogResult dialogResult = MessageBox.Show("ALL DATA of this session will be DELETED. Are you sure?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                deleted_sessions.Rows.Add(dataGridView2.Rows[rowindex].Cells[0].Value, dataGridView2.Rows[rowindex].Cells[1].Value, dataGridView2.Rows[rowindex].Cells[2].Value);
                dataGridView2.Rows.RemoveAt(rowindex);
            }
            a.printDataTable(deleted_sessions);
        }

        private void Submit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(nameTB.Text))
            {
                a.ErrorBox("Please enter name");
                return;
            }
            if (groupCB.SelectedIndex < 0)
            {
                a.ErrorBox("Please select group");
                return;
            }
            //salary check
            float salary = 0F;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (this.dataGridView1.Rows[i].Cells[1].Value == null)
                {
                    a.ErrorBox("Please enter the salary in row: " + (i + 1).ToString());
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

            if (this.edit_form == false)
            {
                DateTime dtime = DateTime.ParseExact(this.dataGridView1.Rows[0].Cells["Date"].Value.ToString().Substring(0, 10), "dd-MM-yyyy", CultureInfo.InvariantCulture);
                string date = dtime.ToString("yyyy-MM-dd");
                string sql = "begin transaction; begin try; ";
                sql += "DECLARE @employeeID int; INSERT INTO Employees (Employee_Name, Group_ID) VALUES ('" + nameTB.Text + "'," + int.Parse(dt.Rows[groupCB.SelectedIndex]["Group_ID"].ToString()) + "); ";
                sql += "SELECT @employeeID = SCOPE_IDENTITY(); ";
                sql += "INSERT INTO Salary VALUES (@employeeID, '" + date + "', " + float.Parse(dataGridView1.Rows[0].Cells["Salary"].Value.ToString()) + "); ";
                sql += "INSERT INTO Employee_Session (Employee_ID, Begin_Date) VALUES (@employeeID, '" + date + "' ); SELECT SCOPE_IDENTITY(); ";
                //catch
                sql += "commit transaction; end try BEGIN CATCH rollback transaction; ";
                sql += "DECLARE @ErrorMessage NVARCHAR(4000); DECLARE @ErrorSeverity INT; DECLARE @ErrorState INT; SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE(); ";
                sql += "RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState); END CATCH; ";
                DataTable d = a.runQuery(sql);
                this.dataGridView2.Rows[0].Cells["Session_ID"].Value = d.Rows[0][0].ToString();
                a.SuccessBox("Successfully added");
                this.Close();
                parent.loadDatabase();
            }
            else
            {
                //CREATE TABLE #EmpDetails (id INT, name VARCHAR(25))  
                //INSERT INTO #EmpDetails VALUES (01, 'Lalit'), (02, 'Atharva') 
                //SELECT * FROM #EmpDetails 

                string sql = "begin transaction; begin try; ";
                sql += "CREATE TABLE #ScopeIDs (id INT) ;";  
                sql += "UPDATE Employees SET Employee_Name = '"+this.nameTB.Text+ "', Group_ID = "+ int.Parse(dt.Rows[this.groupCB.SelectedIndex]["Group_ID"].ToString())+ " WHERE Employee_ID = "+this.employee_id+"; ";
                //salary
                sql += "DELETE FROM Salary WHERE Employee_ID = " + this.employee_id + "; ";
                for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
                {
                    DateTime d = DateTime.ParseExact(this.dataGridView1.Rows[i].Cells["Date"].Value.ToString().Substring(0, 10), "dd-MM-yyyy", CultureInfo.InvariantCulture);
                    string date = d.ToString("yyyy-MM-dd");
                    sql += "INSERT INTO Salary VALUES (" + this.employee_id + ", '" + date + "', " + float.Parse(this.dataGridView1.Rows[i].Cells["Salary"].Value.ToString()) + "); ";
                }
                //sessions
                string deleted_sessions_str = "";
                for(int i=0;i<deleted_sessions.Rows.Count;i++)
                {
                    deleted_sessions_str += deleted_sessions.Rows[i]["Session_ID"].ToString() + ",";
                }
                if(deleted_sessions_str!="")
                {
                    sql += "DELETE FROM Employee_Session WHERE Session_ID IN (" + a.removecom(deleted_sessions_str) + "); ";
                    sql += "DELETE FROM Attendance_Log WHERE Session_ID IN (" + a.removecom(deleted_sessions_str) + "); ";
                }
                for(int i=0;i<dataGridView2.Rows.Count;i++)
                {
                    if(dataGridView2.Rows[i].Cells["Session_ID"].Value.ToString() == "Session Added")
                    {
                        if(dataGridView2.Rows[i].Cells["End_Date"].Value.ToString() == "Active Session")
                        {
                            DateTime start = DateTime.ParseExact(this.dataGridView2.Rows[i].Cells["Begin_Date"].Value.ToString().Substring(0, 10), "dd-MM-yyyy", CultureInfo.InvariantCulture);
                            string date = start.ToString("yyyy-MM-dd");
                            sql += "INSERT INTO Employee_Session (Employee_ID, Begin_Date) VALUES (" + this.employee_id + ", '" + date + "'); Insert into #ScopeIDs VALUES (SCOPE_IDENTITY()); ";
                        }
                        else
                        {
                            DateTime start = DateTime.ParseExact(this.dataGridView2.Rows[i].Cells["Begin_Date"].Value.ToString().Substring(0, 10), "dd-MM-yyyy", CultureInfo.InvariantCulture);
                            string date_start = start.ToString("yyyy-MM-dd");
                            DateTime end = DateTime.ParseExact(this.dataGridView2.Rows[i].Cells["End_Date"].Value.ToString().Substring(0, 10), "dd-MM-yyyy", CultureInfo.InvariantCulture);
                            string date_end = end.ToString("yyyy-MM-dd");
                            sql += "INSERT INTO Employee_Session (Employee_ID, Begin_Date, End_Date) VALUES (" + this.employee_id + ", '" + date_start + "', '" + date_end+ "'); Insert into #ScopeIDs VALUES (SCOPE_IDENTITY()); ";
                        }
                    }
                    else
                    {
                        int session_id = int.Parse(dataGridView2.Rows[i].Cells["Session_ID"].Value.ToString());
                        if (dataGridView2.Rows[i].Cells["End_Date"].Value.ToString() == "Active Session")
                        {
                            DateTime start = DateTime.ParseExact(this.dataGridView2.Rows[i].Cells["Begin_Date"].Value.ToString().Substring(0, 10), "dd-MM-yyyy", CultureInfo.InvariantCulture);
                            string date = start.ToString("yyyy-MM-dd");
                            sql += "UPDATE Employee_Session SET Begin_Date = '" + date + "' WHERE Session_ID = "+session_id+"; ";
                        }
                        else
                        {
                            DateTime start = DateTime.ParseExact(this.dataGridView2.Rows[i].Cells["Begin_Date"].Value.ToString().Substring(0, 10), "dd-MM-yyyy", CultureInfo.InvariantCulture);
                            string date_start = start.ToString("yyyy-MM-dd");
                            DateTime end = DateTime.ParseExact(this.dataGridView2.Rows[i].Cells["End_Date"].Value.ToString().Substring(0, 10), "dd-MM-yyyy", CultureInfo.InvariantCulture);
                            string date_end = end.ToString("yyyy-MM-dd");
                            sql += "UPDATE Employee_Session SET Begin_Date = '" + date_start + "', End_Date = '" + date_end + "' WHERE Session_ID = " + session_id + "; ";
                        }
                    }
                }
                sql += "select * from #ScopeIDs; commit transaction; end try BEGIN CATCH rollback transaction; ";
                sql += "DECLARE @ErrorMessage NVARCHAR(4000); DECLARE @ErrorSeverity INT; DECLARE @ErrorState INT; SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE(); ";
                sql += "RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState); END CATCH; ";
                DataTable dtt = a.runQuery(sql);
                a.printDataTable(dtt, "--------------");
                a.SuccessBox("Edited successfully");
                this.Close();
                parent.loadDatabase();
            }
        }
        
        //DGV
        private void dataGridView2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && dataGridView2.SelectedCells.Cast<DataGridViewCell>().Any(x => x.ColumnIndex == 1))
            {
                try
                {
                    if (dataGridView2.Focused)
                    {
                        DateTime d;
                        if (string.IsNullOrEmpty(dataGridView2.CurrentCell.Value.ToString()) == false)
                        {
                            d = Convert.ToDateTime(dataGridView2.CurrentCell.Value);
                        }
                        else
                        {
                            d = DateTime.Today;
                        }
                        setDate f = new setDate(d);
                        f.ShowDialog();
                        dataGridView2.CurrentCell.Value = f.result.Date.ToString().Substring(0, 10);
                        dataGridView1.Rows[0].Cells["Date"].Value = f.result.Date.ToString().Substring(0, 10);
                        e.Handled = true;
                    }
                    else
                    {
                        return;
                    }
                }
                catch
                {
                    return;
                }

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
