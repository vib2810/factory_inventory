using Factory_Inventory.Factory_Classes;
using Microsoft.SqlServer.Management.SqlParser.Parser;
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
    public partial class M_V4_printDyeingOutward : Form
    {
        private DbConnect c;
        DataTable dt2= new DataTable();
        public M_V4_printDyeingOutward()
        {
            InitializeComponent();
            this.c = new DbConnect();

            //Load Data
            //Create drop-down lists
            var dataSource = new List<string>();
            DataTable d = c.getQC('f');
            dataSource.Add("---Select---");

            for (int i = 0; i < d.Rows.Count; i++)
            {
                dataSource.Add(d.Rows[i][0].ToString());
            }
            this.fiscalCombobox.DataSource = dataSource;
            this.fiscalCombobox.DisplayMember = "Financial Year";
            this.fiscalCombobox.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.fiscalCombobox.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.fiscalCombobox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.fiscalCombobox.SelectedIndex = this.fiscalCombobox.FindStringExact(c.getFinancialYear(DateTime.Now));

            //Datagridviews
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.Transparent;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.Blue;
            dataGridView2.DefaultCellStyle.SelectionBackColor = Color.Transparent;
            dataGridView2.DefaultCellStyle.SelectionForeColor = Color.Blue;


            DataTable dyeing_batches = c.getBatchTable_State(1);
            dataGridView1.DataSource = dyeing_batches;
            if(dyeing_batches.Rows.Count!=0)
            {
                dataGridView1.Columns.OfType<DataGridViewColumn>().ToList().ForEach(col => col.Visible = false);
                this.dataGridView1.Columns["Batch_No"].Visible = true;
                this.dataGridView1.Columns["Colour"].Visible = true;
                this.dataGridView1.Columns["Quality"].Visible = true;
                this.dataGridView1.Columns["Dyeing_Company_Name"].Visible = true;
                this.dataGridView1.Columns["Dyeing_Out_Date"].Visible = true;
                this.dataGridView1.Columns["Net_Weight"].Visible = true;
                this.dataGridView1.Columns["Number_Of_Trays"].Visible = true;
                this.dataGridView1.Columns["Fiscal_Year"].Visible = true;

                this.dataGridView1.Columns["Dyeing_Company_Name"].Width = 150;
                this.dataGridView1.Columns["Dyeing_Out_Date"].Width = 150;

                this.dataGridView1.Columns["Fiscal_Year"].HeaderText = "Financial Year";
                this.dataGridView1.Columns["Number_Of_Trays"].HeaderText = "Number of Trays";
                this.dataGridView1.Columns["Dyeing_Company_Name"].HeaderText = "Dyeing Company Name";
                this.dataGridView1.Columns["Dyeing_Out_Date"].HeaderText = "Dyeing Outward Date";
            }
            dataGridView2.DataSource=dt2;

        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            search_batch();
        }
        private void search_batch()
        {
            int batch_no;
            string fiscal_year;
            try
            {
                batch_no = int.Parse(batchnoTextbox.Text);
            }
            catch
            {
                c.ErrorBox("Please Enter Numeric Batch Number", "Error");
                return;
            }
            fiscal_year = fiscalCombobox.Text;
            DataTable result = c.getBatchTable_BatchNo(batch_no, fiscal_year);
            if (result.Rows.Count == 0)
            {
                this.dt2.Clear();
                this.dt2.Columns.Clear();
                this.dt2.Columns.Add("Not Found");
                this.dt2.Rows.Add("No Batch Found the the following details: Batch Number=" + batch_no + " and Financial Year=" + fiscal_year);
                this.dataGridView2.Columns[0].Width = 800;
                this.dataGridView2.DataSource = dt2;
            }
            else
            {
                this.dt2 = result;
                this.dataGridView2.DataSource = dt2;
                dataGridView2.Columns.OfType<DataGridViewColumn>().ToList().ForEach(col => col.Visible = false);
                this.dataGridView2.Columns["Batch_No"].Visible = true;
                this.dataGridView2.Columns["Batch_No"].HeaderText = "Batch Number";
                this.dataGridView2.Columns["Colour"].Visible = true;
                this.dataGridView2.Columns["Quality"].Visible = true;
                this.dataGridView2.Columns["Dyeing_Company_Name"].Visible = true;
                this.dataGridView2.Columns["Dyeing_Out_Date"].Visible = true;
                this.dataGridView2.Columns["Net_Weight"].Visible = true;
                this.dataGridView2.Columns["Number_Of_Trays"].Visible = true;
                this.dataGridView2.Columns["Fiscal_Year"].Visible = true;

                this.dataGridView2.Columns["Dyeing_Company_Name"].Width = 150;
                this.dataGridView2.Columns["Dyeing_Out_Date"].Width = 150;

                this.dataGridView2.Columns["Fiscal_Year"].HeaderText = "Financial Year";
                this.dataGridView2.Columns["Number_Of_Trays"].HeaderText = "Number of Trays";
                this.dataGridView2.Columns["Dyeing_Company_Name"].HeaderText = "Dyeing Company Name";
                this.dataGridView2.Columns["Dyeing_Out_Date"].HeaderText = "Dyeing Outward Date";
            }
            this.dataGridView2.Rows[0].Selected = false;
            return;
        }

        private void printButton_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int index = this.dataGridView1.SelectedRows[0].Index;
                if (index > this.dataGridView1.Rows.Count - 1)
                {
                    c.ErrorBox("Please select valid voucher", "Error");
                    return;
                }
                DataRow row = (dataGridView1.Rows[index].DataBoundItem as DataRowView).Row;
                printDyeingOutward f = new printDyeingOutward(row);
                f.Show();
            }
            if (dataGridView2.SelectedRows.Count > 0)
            {
                int index = this.dataGridView2.SelectedRows[0].Index;
                if (index > this.dataGridView2.Rows.Count - 1)
                {
                    c.ErrorBox("Please select valid voucher", "Error");
                    return;
                }
                DataRow row = (dataGridView2.Rows[index].DataBoundItem as DataRowView).Row;
                printDyeingOutward f = new printDyeingOutward(row);
                f.Show();
            }


        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count != 0) dataGridView2.SelectedRows[0].Selected = false;
        }

        private void dataGridView2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 0) dataGridView1.SelectedRows[0].Selected = false;
        }

        private void batchnoTextbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) search_batch();
        }
    }
}
