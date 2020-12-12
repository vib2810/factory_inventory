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
    public partial class M_V4_printDO : Form
    {
        private DbConnect c;
        DataTable dt2= new DataTable();
        int dgv1_print_index = -1;
        int dgv2_print_index = -1;
        public M_V4_printDO()
        {
            InitializeComponent();
            this.c = new DbConnect();

            //Load Data
            //Create drop-down lists
            var dataSource = new List<string>();
            var dataSource1 = new List<string>();
            DataTable d = c.getQC('f');
            for (int i = 0; i < d.Rows.Count; i++)
            {
                dataSource.Add(d.Rows[i][0].ToString());
                dataSource1.Add(d.Rows[i][0].ToString());
            }
            this.fiscalCB.DataSource = dataSource;
            this.fiscalCB.DisplayMember = "Financial Year";
            this.fiscalCB.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.fiscalCB.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.fiscalCB.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.fiscalCB.SelectedIndex = this.fiscalCB.FindStringExact(c.getFinancialYear(DateTime.Now));

            this.fiscal1CB.DataSource = dataSource1;
            this.fiscal1CB.DisplayMember = "Financial Year";
            this.fiscal1CB.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.fiscal1CB.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.fiscal1CB.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.fiscal1CB.SelectedIndex = this.fiscal1CB.FindStringExact(c.getFinancialYear(DateTime.Now));

            var dataSource2 = new List<string>();
            dataSource2.Add("0");
            dataSource2.Add("1");
            this.type1CB.DataSource = dataSource2;
            this.type1CB.DisplayMember = "Type";
            this.type1CB.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.type1CB.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.type1CB.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.type1CB.SelectedIndex = this.type1CB.FindStringExact("1");

            //Datagridviews
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.White;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.Blue;
            dataGridView2.DefaultCellStyle.SelectionBackColor = Color.White;
            dataGridView2.DefaultCellStyle.SelectionForeColor = Color.Blue;
            DataTable DO_Nos = c.runQuery("SELECT * FROM Sales_Voucher WHERE Type_Of_Sale = 1 AND Fiscal_Year = '" + c.getFinancialYear(DateTime.Now) + "'");
            dataGridView1.DataSource = DO_Nos;
            
            this.set_columns(dataGridView1);
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells["Printed"].Value.ToString() == "1")
                {
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
        private void M_V4_printDO_Load(object sender, EventArgs e)
        {
            var comboBoxes = this.Controls
                 .OfType<ComboBox>()
                 .Where(x => x.Name.EndsWith("CB"));

            foreach (var cmbBox in comboBoxes)
            {
                c.comboBoxEvent(cmbBox);
            }

            var textBoxes = this.Controls
                  .OfType<TextBox>()
                  .Where(x => x.Name.EndsWith("TB"));

            foreach (var txtBox in textBoxes)
            {
                c.textBoxEvent(txtBox);
            }

            var dtps = this.Controls
                  .OfType<DateTimePicker>()
                  .Where(x => x.Name.EndsWith("DTP"));

            foreach (var dtp in dtps)
            {
                c.DTPEvent(dtp);
            }

            var buttons = this.Controls
                  .OfType<Button>()
                  .Where(x => x.Name.EndsWith("Button"));

            foreach (var button in buttons)
            {
                Console.WriteLine(button.Name);
                c.buttonEvent(button);
            }

            this.type1CB.Focus();
        }
        
        //user
        void set_columns(DataGridView d)
        {
            d.Columns.OfType<DataGridViewColumn>().ToList().ForEach(col => col.Visible = false);
            d.Columns["Sale_DO_No"].Visible = true;
            d.Columns["Sale_DO_No"].DisplayIndex = 0;
            d.Columns["Customer"].Visible = true;
            d.Columns["Customer"].DisplayIndex = 2;
            d.Columns["Quality"].Visible = true;
            d.Columns["Quality"].DisplayIndex = 4;
            //d.Columns["Carton_No_Arr"].Visible = true;
            //d.Columns["Carton_No_Arr"].DisplayIndex = 6;
            d.Columns["Net_Weight"].Visible = true;
            d.Columns["Net_Weight"].DisplayIndex = 8;
            d.Columns["Sale_Rate"].Visible = true;
            d.Columns["Sale_Rate"].DisplayIndex = 10;
            d.Columns["Fiscal_Year"].Visible = true;
            d.Columns["Fiscal_Year"].DisplayIndex = 12;

            //d.Columns["Carton_No_Arr"].Width = 125;
            d.Columns["Customer"].Width = 125;
            d.Columns["Fiscal_Year"].HeaderText = "Financial Year";
            d.Columns["Sale_DO_No"].HeaderText = "DO Number";
            d.Columns["Sale_DO_No"].Width = 80;
            //d.Columns["Carton_No_Arr"].HeaderText = "Carton Nos";
            d.Columns["Net_Weight"].HeaderText = "Net Weight";
            d.Columns["Net_Weight"].Width = 80;
            d.Columns["Sale_Rate"].HeaderText = "Sale Rate";
            c.auto_adjust_dgv(d);
            d.Columns["Fiscal_Year"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
        private void search_DO()
        {
            string fiscal_year = this.fiscal1CB.Text;
            string DO_no = DOnoTB.Text;
            if(string.IsNullOrEmpty(DO_no))
            {
                c.ErrorBox("Please Enter DO Number");
                this.DOnoTB.Focus();
                return;
            }
            DataTable result = c.getDOTable(fiscal_year, DO_no.ToUpper());
            if (result.Rows.Count == 0)
            {
                this.dt2.Clear();
                this.dt2.Columns.Clear();
                this.dt2.Columns.Add("Not Found");
                this.dt2.Rows.Add("No DO Found with the following details: DO Number=" + DO_no + " and Financial Year=" + fiscal_year);
                this.dataGridView2.DataSource = dt2;
                this.dataGridView2.Columns[0].Width = 800;
            }
            else
            {
                this.dt2 = result;
                this.dataGridView2.DataSource = dt2;
                this.set_columns(dataGridView2);
            }
            this.dataGridView2.Rows[0].Selected = false;
            return;
        }
        public void load_color()
        {
            if (this.dgv1_print_index >= 0)
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

        private void searchButton_Click(object sender, EventArgs e)
        {
            search_DO();
        }
        private void printButton_Click(object sender, EventArgs e)
        {
            DataRow row = null;
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
            else if(dataGridView2.SelectedRows.Count > 0)
            {
                if (dataGridView2.Columns.Count <= 2) return;
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
            else
            {
                return;
            }
            printDO f = new printDO(row, this);
            f.Show();
        }
        private void batchnoTextbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) search_DO();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            DataTable dt= c.runQuery("SELECT * FROM Sales_Voucher WHERE Type_Of_Sale = "+ this.type1CB.Text +" AND Fiscal_Year = '" + this.fiscal1CB.Text + "'");
            dataGridView1.DataSource = dt;
            this.set_columns(dataGridView1);
        }


        //dgv
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
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
