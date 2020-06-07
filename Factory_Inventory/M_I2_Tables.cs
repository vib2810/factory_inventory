using Factory_Inventory.Factory_Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
        public M_I2_Tables(string procname)
        {
            InitializeComponent();
            this.procedurename = procname;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView1.Font, FontStyle.Bold);
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            DataTable dt = c.runProcedure(this.procedurename, "@searchText = '" + this.searchTB.Text + "', @date = 0");
            this.dataGridView1.DataSource = dt;
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

            c.printDGVSort(l, this.dataGridView1, 5);
            c.set_dgv_column_sort_state(this.dataGridView1, DataGridViewColumnSortMode.NotSortable);
            c.auto_adjust_dgv(this.dataGridView1);
            //dataGridView1.Columns["Dyeing_Company_Name"].AutoSizeMode= DataGridViewAutoSizeColumnMode.None;
            dataGridView1.Columns["Dyeing_Company_Name"].Width= 150;
            //dataGridView1.Columns["Quality"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridView1.Columns["Quality"].Width = 125;
            for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
            {
                DataGridViewRow row = dataGridView1.Rows[i];
                if (dt.Rows[i]["Batch_State"].ToString() == "2")
                {
                    row.DefaultCellStyle.SelectionBackColor = Color.Yellow;
                    row.DefaultCellStyle.SelectionForeColor= Color.Blue;
                    row.DefaultCellStyle.BackColor = Color.Yellow;
                }
                if (dt.Rows[i]["Batch_State"].ToString() == "3")
                {
                    row.DefaultCellStyle.SelectionBackColor = Color.LawnGreen;
                    row.DefaultCellStyle.SelectionForeColor= Color.Blue;
                    row.DefaultCellStyle.BackColor = Color.LawnGreen;
                }
                row.Cells[l.Count - 5].Style.SelectionBackColor = Color.Gray;
                row.Cells[l.Count - 5].Style.BackColor = Color.Gray;
                int bold_index = -1;
                string bold = dt.Rows[i]["priority"].ToString();
                if (bold[0] == 'a')
                {
                    bold_index = int.Parse(bold.Substring(1, bold.Length - 1));
                }
                else bold_index = int.Parse(bold);
                
                row.Cells[l[bold_index-1]].Style.Font = new Font("Arial", dataGridView1.Font.Size, FontStyle.Bold);
            }
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.Columns[l.Count - 5].HeaderCell.Style.SelectionBackColor = Color.Gray;
            dataGridView1.Columns[l.Count - 5].HeaderCell.Style.BackColor = Color.Gray;
        }

        private void searchTB_TextChanged(object sender, EventArgs e)
        {
        //    DataTable dt = c.runProcedure(this.procedurename, "@searchText = '" + this.searchTB.Text + "', @date = 0");
        //    this.dataGridView1.DataSource = dt;
        //    List<string> l = new List<string>();
        //    l.Add("Batch_No");
        //    l.Add("Colour");
        //    l.Add("Quality");
        //    l.Add("Net_Weight");
        //    l.Add("Dyeing_Company_Name");
        //    l.Add("Fiscal_Year");
        //    l.Add("Bill_No");
        //    l.Add("Slip_No");
        //    l.Add("Company_Name");
        //    l.Add("Dyeing_Out_Date");
        //    l.Add("Dyeing_In_Date");
        //    l.Add("Start_Date_Of_Production");
        //    l.Add("Date_Of_Production");
        //    l.Add("Bill_Date");

        //    c.printDGVSort(l, this.dataGridView1, 5);
        //    c.set_dgv_column_sort_state(this.dataGridView1, DataGridViewColumnSortMode.NotSortable);
        //    c.auto_adjust_dgv(this.dataGridView1);
        //    //dataGridView1.Columns["Dyeing_Company_Name"].AutoSizeMode= DataGridViewAutoSizeColumnMode.None;
        //    dataGridView1.Columns["Dyeing_Company_Name"].Width = 150;
        //    //dataGridView1.Columns["Quality"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        //    dataGridView1.Columns["Quality"].Width = 125;
        //    for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
        //    {
        //        DataGridViewRow row = dataGridView1.Rows[i];
        //        if (dt.Rows[i]["Batch_State"].ToString() == "2")
        //        {
        //            row.DefaultCellStyle.SelectionBackColor = Color.Yellow;
        //            row.DefaultCellStyle.BackColor = Color.Yellow;
        //        }
        //        if (dt.Rows[i]["Batch_State"].ToString() == "3")
        //        {
        //            row.DefaultCellStyle.SelectionBackColor = Color.LawnGreen;
        //            row.DefaultCellStyle.BackColor = Color.LawnGreen;
        //        }
        //        int bold_index = -1;
        //        string bold = dt.Rows[i]["priority"].ToString();
        //        if (bold[0] == 'a')
        //        {
        //            bold_index = int.Parse(bold.Substring(1, bold.Length - 1));
        //        }
        //        else bold_index = int.Parse(bold);

        //        row.Cells[l[bold_index - 1]].Style.Font = new Font("Arial", dataGridView1.Font.Size, FontStyle.Bold);
        //    }
        }

        private void searchTB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.searchButton.PerformClick();
                e.SuppressKeyPress= true;
            }
        }

        private void dateTimePicker1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.searchByDateButton.PerformClick();
                e.SuppressKeyPress = true;
            }
        }

        private void searchByDateButton_Click(object sender, EventArgs e)
        {
            DataTable dt = c.runProcedure(this.procedurename, "@searchText = '" + this.dateTimePicker1.Value.Date.ToString("yyyy-MM-dd")+ "', @date = 1");
            this.dataGridView1.DataSource = dt;
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

            c.printDGVSort(l, this.dataGridView1, 5);
            c.set_dgv_column_sort_state(this.dataGridView1, DataGridViewColumnSortMode.NotSortable);
            c.auto_adjust_dgv(this.dataGridView1);
            //dataGridView1.Columns["Dyeing_Company_Name"].AutoSizeMode= DataGridViewAutoSizeColumnMode.None;
            dataGridView1.Columns["Dyeing_Company_Name"].Width = 150;
            //dataGridView1.Columns["Quality"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridView1.Columns["Quality"].Width = 125;
            for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
            {
                DataGridViewRow row = dataGridView1.Rows[i];
                if (dt.Rows[i]["Batch_State"].ToString() == "2")
                {
                    row.DefaultCellStyle.SelectionBackColor = Color.Yellow;
                    row.DefaultCellStyle.SelectionForeColor = Color.Blue;
                    row.DefaultCellStyle.BackColor = Color.Yellow;
                }
                if (dt.Rows[i]["Batch_State"].ToString() == "3")
                {
                    row.DefaultCellStyle.SelectionBackColor = Color.LawnGreen;
                    row.DefaultCellStyle.SelectionForeColor = Color.Blue;
                    row.DefaultCellStyle.BackColor = Color.LawnGreen;
                }
                row.Cells[l.Count - 5].Style.SelectionBackColor = Color.Gray;
                row.Cells[l.Count - 5].Style.BackColor = Color.Gray;
                int bold_index = -1;
                string bold = dt.Rows[i]["priority"].ToString();
                if (bold.StartsWith("da"))
                {
                    bold_index = int.Parse(bold.Substring(2, bold.Length - 2));
                }
                else bold_index = int.Parse(bold.Substring(1, bold.Length - 1));

                row.Cells[l[bold_index - 1+9]].Style.Font = new Font("Arial", dataGridView1.Font.Size, FontStyle.Bold);
            }
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.Columns[l.Count - 5].HeaderCell.Style.SelectionBackColor = Color.Gray;
            dataGridView1.Columns[l.Count - 5].HeaderCell.Style.BackColor = Color.Gray;
        }
    }
}
