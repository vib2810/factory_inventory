using Factory_Inventory.Factory_Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Factory_Inventory
{
    public partial class T_I_Tables : Form
    {
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.V)
            {
                this.detailsButton.PerformClick();
                return false;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private int obj;    //1 = Inward Carton. 2 = Repacked Carton
        private DbConnect c = new DbConnect();
        private DataTable dt;
        public T_I_Tables(int obj)
        {
            InitializeComponent();
            this.obj = obj;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView1.Font, FontStyle.Bold);
            dataGridView1.RowHeadersVisible = false;
            this.doublebuffer(true);
        }
        private void doublebuffer(bool onoff)
        {
            if (!System.Windows.Forms.SystemInformation.TerminalServerSession)
            {
                Type dgvType = dataGridView1.GetType();
                PropertyInfo pi = dgvType.GetProperty("DoubleBuffered",
                BindingFlags.Instance | BindingFlags.NonPublic);
                pi.SetValue(dataGridView1, onoff, null);
            }
        }

        //searches
        private void search_carton(bool date)
        {

            List<string> l = new List<string>();
            l.Add("Carton_No");
            l.Add("Quality_Before_Job");
            l.Add("Company_Name");
            l.Add("Net_Weight");
            l.Add("Sold_Weight");
            l.Add("Fiscal_Year");
            l.Add("Bill_No");
            l.Add("Buy_Cost");
            l.Add("Inward_Comments");
            l.Add("Grade");
            l.Add("Date_Of_Billing");
            l.Add("Date_Of_Production");
            l.Add("Start_Date_Of_Production");
            l.Add("Carton_State");
            int date_cols = 4, normal_cols = 10;

            c.printDGVSort(l, this.dataGridView1, date_cols);
            c.set_dgv_column_sort_state(this.dataGridView1, DataGridViewColumnSortMode.NotSortable);
            c.auto_adjust_dgv(this.dataGridView1);

            dataGridView1.Columns["Quality_Before_Job"].Width = 70;
            dataGridView1.Columns.Remove(dataGridView1.Columns["Carton_State"]);
            DataGridViewTextBoxColumn newcol = new DataGridViewTextBoxColumn();
            newcol.HeaderText = "Carton State";
            newcol.Name = "Carton State";
            dataGridView1.Columns.Add(newcol);
            dataGridView1.Columns["Carton State"].Width = 80;

            for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
            {
                DataGridViewRow row = dataGridView1.Rows[i];
                if (dt.Rows[i]["Carton_State"].ToString() == "1")
                {
                    if(dt.Rows[i]["Date_Of_Production"].ToString() == "")
                    {
                        row.DefaultCellStyle.SelectionBackColor = Color.Yellow;
                        row.DefaultCellStyle.SelectionForeColor = Color.Blue;
                        row.DefaultCellStyle.BackColor = Color.Yellow;
                        this.dataGridView1.Rows[i].Cells["Carton State"].Value = "Repacking";
                    }
                    else
                    {
                        row.DefaultCellStyle.SelectionBackColor = Color.LightGray;
                        row.DefaultCellStyle.SelectionForeColor = Color.Blue;
                        row.DefaultCellStyle.BackColor = Color.LightGray;
                        this.dataGridView1.Rows[i].Cells["Carton State"].Value = "Repacked";
                    }
                }
                else if (dt.Rows[i]["Carton_State"].ToString() == "2")
                {
                    row.DefaultCellStyle.SelectionBackColor = Color.Orange;
                    row.DefaultCellStyle.SelectionForeColor = Color.Blue;
                    row.DefaultCellStyle.BackColor = Color.Orange;
                    this.dataGridView1.Rows[i].Cells["Carton State"].Value = "Partially Sold";
                }
                else if (dt.Rows[i]["Carton_State"].ToString() == "3")
                {
                    row.DefaultCellStyle.SelectionBackColor = Color.LawnGreen;
                    row.DefaultCellStyle.SelectionForeColor = Color.Blue;
                    row.DefaultCellStyle.BackColor = Color.LawnGreen;
                    this.dataGridView1.Rows[i].Cells["Carton State"].Value = "Sold";
                }
                else if (dt.Rows[i]["Carton_State"].ToString() == "0")
                {
                    this.dataGridView1.Rows[i].Cells["Carton State"].Value = "Received In";
                }

                row.Cells["nullcol"].Style.SelectionBackColor = Color.Gray;
                row.Cells["nullcol"].Style.BackColor = Color.Gray;
                int bold_index = -1;
                string bold = dataGridView1.Rows[i].Cells["priority"].Value.ToString();
                if (date == true)
                {
                    if (bold.StartsWith("da"))
                    {
                        bold_index = int.Parse(bold.Substring(2, bold.Length - 2));
                    }
                    else bold_index = int.Parse(bold.Substring(1, bold.Length - 1));
                    Console.WriteLine(bold_index);
                    row.Cells[l[bold_index - 1 + normal_cols]].Style.Font = new Font("Arial", dataGridView1.Font.Size, FontStyle.Bold);
                }
                else
                {
                    if (bold[0] == 'a')
                    {
                        bold_index = int.Parse(bold.Substring(1, bold.Length - 1));
                    }
                    else bold_index = int.Parse(bold);

                    row.Cells[l[bold_index - 1]].Style.Font = new Font("Arial", dataGridView1.Font.Size, FontStyle.Bold);
                }
            }

            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.Columns["nullcol"].HeaderCell.Style.SelectionBackColor = Color.Gray;
            dataGridView1.Columns["nullcol"].HeaderCell.Style.BackColor = Color.Gray;
            dataGridView1.Columns["nullcol"].Width = 20;

        }
        private void search_cartonrepacked(bool date)
        {

            List<string> l = new List<string>();
            l.Add("Carton_No");
            l.Add("Quality_Before_Job");
            l.Add("Colour");
            l.Add("Net_Weight");
            l.Add("Sold_Weight");
            l.Add("Grade");
            l.Add("Repack_Comments");
            l.Add("Fiscal_Year");
            l.Add("Date_Of_Production");
            l.Add("Start_Date_Of_Production");
            l.Add("Carton_State");
            int date_cols = 3, normal_cols = 8;

            c.printDGVSort(l, this.dataGridView1, date_cols);
            c.set_dgv_column_sort_state(this.dataGridView1, DataGridViewColumnSortMode.NotSortable);
            c.auto_adjust_dgv(this.dataGridView1);
            // Double buffering can make DGV slow in remote desktop
            dataGridView1.Columns["Quality_Before_Job"].Width = 70;
            dataGridView1.Columns.Remove(dataGridView1.Columns["Carton_State"]);
            DataGridViewTextBoxColumn newcol = new DataGridViewTextBoxColumn();
            newcol.HeaderText = "Carton State";
            newcol.Name = "Carton State";
            dataGridView1.Columns.Add(newcol);
            dataGridView1.Columns["Carton State"].Width = 80;

            for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
            {
                DataGridViewRow row = dataGridView1.Rows[i];
                if (dt.Rows[i]["Carton_State"].ToString() == "1")
                {
                    row.DefaultCellStyle.SelectionBackColor = Color.Orange;
                    row.DefaultCellStyle.SelectionForeColor = Color.Blue;
                    row.DefaultCellStyle.BackColor = Color.Orange;
                    this.dataGridView1.Rows[i].Cells["Carton State"].Value = "Partially Sold";
                }
                else if (dt.Rows[i]["Carton_State"].ToString() == "2")
                {
                    row.DefaultCellStyle.SelectionBackColor = Color.LawnGreen;
                    row.DefaultCellStyle.SelectionForeColor = Color.Blue;
                    row.DefaultCellStyle.BackColor = Color.LawnGreen;
                    this.dataGridView1.Rows[i].Cells["Carton State"].Value = "Sold";
                }
                else if (dt.Rows[i]["Carton_State"].ToString() == "0")
                {
                    this.dataGridView1.Rows[i].Cells["Carton State"].Value = "Repacked";
                }

                row.Cells["nullcol"].Style.SelectionBackColor = Color.Gray;
                row.Cells["nullcol"].Style.BackColor = Color.Gray;
                int bold_index = -1;
                string bold = dataGridView1.Rows[i].Cells["priority"].Value.ToString();
                if (date == true)
                {
                    if (bold.StartsWith("da"))
                    {
                        bold_index = int.Parse(bold.Substring(2, bold.Length - 2));
                    }
                    else bold_index = int.Parse(bold.Substring(1, bold.Length - 1));
                    Console.WriteLine(bold_index);
                    row.Cells[l[bold_index - 1 + normal_cols]].Style.Font = new Font("Arial", dataGridView1.Font.Size, FontStyle.Bold);
                }
                else
                {
                    if (bold[0] == 'a')
                    {
                        bold_index = int.Parse(bold.Substring(1, bold.Length - 1));
                    }
                    else bold_index = int.Parse(bold);

                    row.Cells[l[bold_index - 1]].Style.Font = new Font("Arial", dataGridView1.Font.Size, FontStyle.Bold);
                }
            }

            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.Columns["nullcol"].HeaderCell.Style.SelectionBackColor = Color.Gray;
            dataGridView1.Columns["nullcol"].HeaderCell.Style.BackColor = Color.Gray;
            dataGridView1.Columns["nullcol"].Width = 20;
        }


        //buttons
        private void searchButton_Click(object sender, EventArgs e)
        {
            this.dataGridView1.Columns.Clear();
            if(this.obj==1)
            {
                string sql = "DECLARE @searchText nvarchar(50) = '" + this.searchTB.Text + "', @date tinyint = 0\n";
                sql += File.ReadAllText(@"../../Queries/Search_Inward_Carton.sql");
                this.dt = c.runQuery(sql);
            }
            if (this.obj == 2)
            {
                string sql = "DECLARE @searchText nvarchar(50) = '" + this.searchTB.Text + "', @date tinyint = 0\n";
                sql += File.ReadAllText(@"../../Queries/Search_Repacked_Carton.sql");
                this.dt = c.runQuery(sql);
            }
            this.dataGridView1.DataSource = dt;
            
            if(this.obj == 1)
            {
                this.search_carton(false);
            }
            if (this.obj == 2)
            {
                this.search_cartonrepacked(false);
            }
        }
        private void searchByDateButton_Click(object sender, EventArgs e)
        {
            this.dataGridView1.Columns.Clear();
            if (this.obj == 1)
            {
                string sql = "DECLARE @searchText nvarchar(50) = '" + this.dateTimePicker1.Value.Date.ToString("yyyy-MM-dd") + "', @date tinyint = 1\n";
                sql += File.ReadAllText(@"../../Queries/Search_Inward_Carton.sql");
                this.dt = c.runQuery(sql);
            }
            if (this.obj == 2)
            {
                string sql = "DECLARE @searchText nvarchar(50) = '" + this.dateTimePicker1.Value.Date.ToString("yyyy-MM-dd") + "', @date tinyint = 1\n";
                sql += File.ReadAllText(@"../../Queries/Search_Repacked_Carton.sql");
                this.dt = c.runQuery(sql);
            }
            this.dataGridView1.DataSource = dt;

            if (this.obj == 1)
            {
                this.search_carton(true);
            }
            if (this.obj == 2)
            {
                this.search_cartonrepacked(true);
            }

            //if (this.procedurename == "SearchInBatch")
            //{
            //    this.search_batch(true);
            //}
            //else if(this.procedurename == "SearchInCarton")
            //{
            //    this.search_carton(true);
            //}
            //else if (this.procedurename == "SearchInCartonProduced")
            //{
            //    this.search_cartonproduced(true);
            //}
            //else if (this.procedurename == "SearchInTray")
            //{
            //    this.search_tray(true);
            //}
        }

        //key downs
        private void searchTB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.searchButton.PerformClick();
                e.SuppressKeyPress = true;
            }
        }
        private void dateTimePicker1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.searchByDateButton.Focus();
                this.dateTimePicker1.Focus();
                this.searchByDateButton.PerformClick();
                e.SuppressKeyPress = true;
            }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

        }
        private void detailsButton_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count <= 0) return;
            int row_index = dataGridView1.SelectedCells[0].RowIndex;

            if (this.obj == 1)
            {
                Display_Inward_Carton f = new Display_Inward_Carton((dataGridView1.Rows[row_index].DataBoundItem as DataRowView).Row);
                f.Show();
            }
            else if (this.obj == 2)
            {
                Display_Repacked_Carton f = new Display_Repacked_Carton((dataGridView1.Rows[row_index].DataBoundItem as DataRowView).Row);
                f.Show();
            }
        }

    }
}
