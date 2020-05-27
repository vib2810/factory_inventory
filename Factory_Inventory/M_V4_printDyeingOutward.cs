using Factory_Inventory.Factory_Classes;
using Microsoft.SqlServer.Management.SqlParser.Parser;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Factory_Inventory
{
    public partial class M_V4_printDyeingOutward : Form
    {
        private DbConnect c;
        DataTable dt2= new DataTable();
        int dgv1_print_index=-1;
        int dgv2_print_index = -1;

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
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.White;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.Blue;
            dataGridView2.DefaultCellStyle.SelectionBackColor = Color.White;
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
        private void M_V4_printDyeingOutward_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells["Printed"].Value.ToString() == "1")
                {
                    Console.WriteLine("setting print " + i);
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Global.printedColor;
                    dataGridView1.Rows[i].DefaultCellStyle.SelectionBackColor = Global.printedColor;
                }
                else
                {
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.White;
                    dataGridView1.Rows[i].DefaultCellStyle.SelectionBackColor = Color.White;
                }
            }
            this.dataGridView1.Visible = false;
            this.dataGridView1.Visible = true;
        }

        //user
        public void load_color()
        {
            if(this.dgv1_print_index>=0)
            {
                dataGridView1.Rows[dgv1_print_index].DefaultCellStyle.BackColor = Global.printedColor;
                dataGridView1.Rows[dgv1_print_index].DefaultCellStyle.SelectionBackColor = Global.printedColor;
            }
            if (this.dgv2_print_index >= 0)
            {
                dataGridView2.Rows[dgv2_print_index].DefaultCellStyle.BackColor = Global.printedColor;
                dataGridView2.Rows[dgv2_print_index].DefaultCellStyle.SelectionBackColor = Global.printedColor;
            }
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
                if(dataGridView2.Rows[0].Cells["Printed"].Value.ToString()=="1")
                {
                    dataGridView2.Rows[0].DefaultCellStyle.BackColor = Global.printedColor;
                    dataGridView2.Rows[0].DefaultCellStyle.SelectionBackColor = Global.printedColor;
                }
            }
            this.dataGridView2.Rows[0].Selected = false;
            return;
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            search_batch();
        }
        private void printButton_Click(object sender, EventArgs e)
        {
            DataRow row=null;
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int index = this.dataGridView1.SelectedRows[0].Index;
                if (index > this.dataGridView1.Rows.Count - 1)
                {
                    c.ErrorBox("Please select valid voucher", "Error");
                    return;
                }
                row = (dataGridView1.Rows[index].DataBoundItem as DataRowView).Row;
                this.dgv1_print_index = index;
                this.dgv2_print_index = -1;
            }
            else if (dataGridView2.SelectedRows.Count > 0)
            {
                int index = this.dataGridView2.SelectedRows[0].Index;
                if (index > this.dataGridView2.Rows.Count - 1)
                {
                    c.ErrorBox("Please select valid voucher", "Error");
                    return;
                }
                row = (dataGridView2.Rows[index].DataBoundItem as DataRowView).Row;
                this.dgv1_print_index = -1;
                this.dgv2_print_index = index;
            }
            if (row["Tray_ID_Arr"].ToString() == "")
            {
                string[] redyeing_info = c.csvToArray(row["Redyeing"].ToString());
                c.ErrorBox("Cannot Print Dyeing Slip For Batch and Batch is derived from Redyeing Parent Batch: " + redyeing_info[0] + " Fiscal_Year: " + redyeing_info[1]);
                return;
            }
            printDyeingOutward f = new printDyeingOutward(row, this);
            Global.background.show_form(f);
        }
        private void batchnoTextbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) search_batch();
        }

        //dgv
        private void dataGridView1_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count != 0) dataGridView2.SelectedRows[0].Selected = false;
        }
        private void dataGridView2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 0) dataGridView1.SelectedRows[0].Selected = false;
        }
    }
}
