using Factory_Inventory.Factory_Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Factory_Inventory
{
    public partial class M_I2_Tables : Form
    {
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private string procedurename;
        private DbConnect c = new DbConnect();
        private DataTable dt;
        public M_I2_Tables(string procname)
        {
            InitializeComponent();
            this.procedurename = procname;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView1.Font, FontStyle.Bold);
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
        private void search_batch(bool date)
        {

            List<string> l = new List<string>();
            l.Add("Batch_No");
            l.Add("Colour");
            l.Add("Quality");
            l.Add("Net_Weight");
            l.Add("Dyeing_Company_Name");
            l.Add("Fiscal_Year");
            l.Add("Bill_No");
            l.Add("Slip_No");
            l.Add("Company_Name");
            l.Add("Dyeing_Out_Date");
            l.Add("Dyeing_In_Date");
            l.Add("Start_Date_Of_Production");
            l.Add("Date_Of_Production");
            l.Add("Bill_Date");
            l.Add("Batch_State");
            int date_cols = 6, normal_cols = 9;

            c.printDGVSort(l, this.dataGridView1, date_cols);
            c.set_dgv_column_sort_state(this.dataGridView1, DataGridViewColumnSortMode.NotSortable);
            c.auto_adjust_dgv(this.dataGridView1);
            dataGridView1.Columns["Dyeing_Company_Name"].Width = 150;
            dataGridView1.Columns["Quality"].Width = 150;
            dataGridView1.Columns.Remove(dataGridView1.Columns["Batch_State"]);
            DataGridViewTextBoxColumn newcol = new DataGridViewTextBoxColumn();
            newcol.HeaderText = "Batch State";
            newcol.Name = "Batch State";
            dataGridView1.Columns.Add(newcol);
            dataGridView1.Columns["Batch State"].Width = 150;
            dataGridView1.Columns["Bill_No"].Width = 60;
            dataGridView1.Columns["Slip_No"].Width = 60;


            for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
            {
                DataGridViewRow row = dataGridView1.Rows[i];
                if (dt.Rows[i]["Batch_State"].ToString() == "2")
                {
                    row.DefaultCellStyle.SelectionBackColor = Color.Yellow;
                    row.DefaultCellStyle.SelectionForeColor = Color.Blue;
                    row.DefaultCellStyle.BackColor = Color.Yellow;
                    this.dataGridView1.Rows[i].Cells["Batch State"].Value = "Received from dyeing";
                }
                else if (dt.Rows[i]["Batch_State"].ToString() == "3")
                {
                    row.DefaultCellStyle.SelectionBackColor = Color.LawnGreen;
                    row.DefaultCellStyle.SelectionForeColor = Color.Blue;
                    row.DefaultCellStyle.BackColor = Color.LawnGreen;
                    if (c.Cell_Not_NullOrEmpty(this.dataGridView1, i, 0, "Date_Of_Production"))
                    {
                        this.dataGridView1.Rows[i].Cells["Batch State"].Value = "Produced";
                    }
                    else
                    {
                        this.dataGridView1.Rows[i].Cells["Batch State"].Value = "In Production";
                    }
                }
                else if (dt.Rows[i]["Batch_State"].ToString() == "4")
                {
                    row.DefaultCellStyle.SelectionBackColor = Color.Orange;
                    row.DefaultCellStyle.SelectionForeColor = Color.Blue;
                    row.DefaultCellStyle.BackColor = Color.Orange;
                    this.dataGridView1.Rows[i].Cells["Batch State"].Value = "Redyeing (Destroyed)";
                }
                else if (dt.Rows[i]["Batch_State"].ToString() == "1")
                {
                    this.dataGridView1.Rows[i].Cells["Batch State"].Value = "In Dyeing";
                }

                row.Cells["nullcol"].Style.SelectionBackColor = Color.Gray;
                row.Cells["nullcol"].Style.BackColor = Color.Gray;
                int bold_index = -1;
                string bold = dataGridView1.Rows[i].Cells["priority"].Value.ToString();
                if(date == true)
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
        private void search_tray(bool date)
        {
            List<string> l = new List<string>();
            l.Add("Tray_No");
            l.Add("Quality");
            l.Add("Batch_No");
            l.Add("Batch_Fiscal_Year");
            l.Add("Spring");
            l.Add("Number_of_Springs");
            l.Add("Net_Weight");
            l.Add("Dyeing_Company_Name");
            l.Add("Machine_No");
            l.Add("Grade");
            l.Add("Fiscal_Year");
            l.Add("Tray_Production_Date");
            l.Add("Dyeing_Out_Date");
            l.Add("Dyeing_In_Date");
            l.Add("Tray_State");
            int date_cols = 4, normal_cols = 11;

            c.printDGVSort(l, this.dataGridView1, date_cols);
            c.set_dgv_column_sort_state(this.dataGridView1, DataGridViewColumnSortMode.NotSortable);
            c.auto_adjust_dgv(this.dataGridView1);
            //// Double buffering can make DGV slow in remote desktop
            dataGridView1.Columns["Quality"].Width = 150;
            dataGridView1.Columns["Dyeing_Company_Name"].Width = 150;
            dataGridView1.Columns.Remove(dataGridView1.Columns["Tray_State"]);
            DataGridViewTextBoxColumn newcol = new DataGridViewTextBoxColumn();
            newcol.HeaderText = "Tray State";
            newcol.Name = "Tray State";
            dataGridView1.Columns.Add(newcol);
            dataGridView1.Columns["Tray State"].Width = 200;

            for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
            {
                DataGridViewRow row = dataGridView1.Rows[i];
                if (dt.Rows[i]["Tray_State"].ToString() == "2")
                {
                    row.DefaultCellStyle.SelectionBackColor = Color.Yellow;
                    row.DefaultCellStyle.SelectionForeColor = Color.Blue;
                    row.DefaultCellStyle.BackColor = Color.Yellow;
                    this.dataGridView1.Rows[i].Cells["Tray State"].Value = "In Dyeing";
                }
                else if (dt.Rows[i]["Tray_State"].ToString() == "1")
                {
                    this.dataGridView1.Rows[i].Cells["Tray State"].Value = "Produced";
                }
                else if (dt.Rows[i]["Tray_State"].ToString() == "-1")
                {
                    row.DefaultCellStyle.SelectionBackColor = Color.LawnGreen;
                    row.DefaultCellStyle.SelectionForeColor = Color.Blue;
                    row.DefaultCellStyle.BackColor = Color.LawnGreen;
                    this.dataGridView1.Rows[i].Cells["Tray State"].Value = "Received from Dyeing (free)";
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
        private void search_carton(bool date)
        {

            List<string> l = new List<string>();
            l.Add("Carton_No");
            l.Add("Quality");
            l.Add("Company_Name");
            l.Add("Net_Weight");
            l.Add("Fiscal_Year");
            l.Add("Bill_No");
            l.Add("Sale_DO_No");
            l.Add("DO_Fiscal_Year");
            l.Add("Buy_Cost");
            l.Add("Sale_Rate");
            l.Add("Date_Of_Billing");
            l.Add("Date_Of_Issue");
            l.Add("Date_Of_Sale");
            l.Add("Carton_State");
            int date_cols = 4, normal_cols = 10;

            c.printDGVSort(l, this.dataGridView1, date_cols);
            c.set_dgv_column_sort_state(this.dataGridView1, DataGridViewColumnSortMode.NotSortable);
            c.auto_adjust_dgv(this.dataGridView1);

            dataGridView1.Columns["Quality"].Width = 150;
            dataGridView1.Columns.Remove(dataGridView1.Columns["Carton_State"]);
            DataGridViewTextBoxColumn newcol = new DataGridViewTextBoxColumn();
            newcol.HeaderText = "Carton State";
            newcol.Name = "Carton State";
            dataGridView1.Columns.Add(newcol);
            dataGridView1.Columns["Carton State"].Width = 150;


            for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
            {
                DataGridViewRow row = dataGridView1.Rows[i];
                if (dt.Rows[i]["Carton_State"].ToString() == "2")
                {
                    row.DefaultCellStyle.SelectionBackColor = Color.LightGray;
                    row.DefaultCellStyle.SelectionForeColor = Color.Blue;
                    row.DefaultCellStyle.BackColor = Color.LightGray;
                    this.dataGridView1.Rows[i].Cells["Carton State"].Value = "In Twist";
                }
                else if (dt.Rows[i]["Carton_State"].ToString() == "3")
                {
                    row.DefaultCellStyle.SelectionBackColor = Color.LawnGreen;
                    row.DefaultCellStyle.SelectionForeColor = Color.Blue;
                    row.DefaultCellStyle.BackColor = Color.LawnGreen;
                    this.dataGridView1.Rows[i].Cells["Carton State"].Value = "Sold";
                }
                else if (dt.Rows[i]["Carton_State"].ToString() == "1")
                {
                    this.dataGridView1.Rows[i].Cells["Carton State"].Value = "In Gray Godown";
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
        private void search_cartonproduced(bool date)
        {

            List<string> l = new List<string>();
            l.Add("Carton_No");
            l.Add("Quality");
            l.Add("Colour");
            l.Add("Batch_No_Arr");
            l.Add("Batch_Fiscal_Year_Arr");
            l.Add("Date_Of_Production");
            l.Add("Net_Weight");
            l.Add("Dyeing_Company_Name");
            l.Add("Sale_DO_No");
            l.Add("DO_Fiscal_Year");
            l.Add("Sale_Rate");
            l.Add("Grade");
            l.Add("Fiscal_Year");
            l.Add("Date_Of_Production");
            l.Add("Date_Of_Sale");
            l.Add("Carton_State");
            int date_cols = 3, normal_cols = 13;

            c.printDGVSort(l, this.dataGridView1, date_cols);
            c.set_dgv_column_sort_state(this.dataGridView1, DataGridViewColumnSortMode.NotSortable);
            c.auto_adjust_dgv(this.dataGridView1);
            // Double buffering can make DGV slow in remote desktop
            dataGridView1.Columns["Quality"].Width = 150;
            dataGridView1.Columns["Dyeing_Company_Name"].Width = 150;
            dataGridView1.Columns["Batch_No_Arr"].HeaderText = "Batch Numbers";
            dataGridView1.Columns["Batch_Fiscal_Year_Arr"].HeaderText = "Batch Fiscal Years";
            dataGridView1.Columns.Remove(dataGridView1.Columns["Carton_State"]);
            DataGridViewTextBoxColumn newcol = new DataGridViewTextBoxColumn();
            newcol.HeaderText = "Carton State";
            newcol.Name = "Carton State";
            dataGridView1.Columns.Add(newcol);
            dataGridView1.Columns["Carton State"].Width = 150;

            for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
            {
                dataGridView1.Rows[i].Cells["Batch_No_Arr"].Value = c.removecom(dataGridView1.Rows[i].Cells["Batch_No_Arr"].Value.ToString());
                dataGridView1.Rows[i].Cells["Batch_Fiscal_Year_Arr"].Value = c.removecom(dataGridView1.Rows[i].Cells["Batch_Fiscal_Year_Arr"].Value.ToString());

                DataGridViewRow row = dataGridView1.Rows[i];
                if (dt.Rows[i]["Carton_State"].ToString() == "2")
                {
                    row.DefaultCellStyle.SelectionBackColor = Color.LawnGreen;
                    row.DefaultCellStyle.SelectionForeColor = Color.Blue;
                    row.DefaultCellStyle.BackColor = Color.LawnGreen;
                    this.dataGridView1.Rows[i].Cells["Carton State"].Value = "Sold";
                }
                else if (dt.Rows[i]["Carton_State"].ToString() == "1")
                {
                    this.dataGridView1.Rows[i].Cells["Carton State"].Value = "Produced";
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
            this.dt = c.runProcedure(this.procedurename, "@searchText = '" + this.searchTB.Text + "', @date = 0");
            this.dataGridView1.DataSource = dt;
            
            if (this.procedurename == "SearchInBatch")
            {
                this.search_batch(false);
            }
            else if(this.procedurename == "SearchInCarton")
            {
                this.search_carton(false);
            }
            else if (this.procedurename == "SearchInCartonProduced")
            {
                this.search_cartonproduced(false);
            }
            else if (this.procedurename == "SearchInTray")
            {
                this.search_tray(false);
            }
        }
        private void searchByDateButton_Click(object sender, EventArgs e)
        {
            this.dataGridView1.Columns.Clear();
            this.dt = c.runProcedure(this.procedurename, "@searchText = '" + this.dateTimePicker1.Value.Date.ToString("yyyy-MM-dd")+ "', @date = 1");
            this.dataGridView1.DataSource = dt;
            if (this.procedurename == "SearchInBatch")
            {
                this.search_batch(true);
            }
            else if(this.procedurename == "SearchInCarton")
            {
                this.search_carton(true);
            }
            else if (this.procedurename == "SearchInCartonProduced")
            {
                this.search_cartonproduced(true);
            }
            else if (this.procedurename == "SearchInTray")
            {
                this.search_tray(true);
            }
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
    }
}
