using Factory_Inventory.Factory_Classes;
using Factory_Inventory.Properties;
using Microsoft.SqlServer.Management.SqlParser.Parser;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Forms;

namespace Factory_Inventory
{
    public partial class M_V3_cartonProductionForm : Form
    {
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Tab &&
                dataGridView1.EditingControl != null &&
                msg.HWnd == dataGridView1.EditingControl.Handle &&
                dataGridView1.SelectedCells
                    .Cast<DataGridViewCell>()
                    .Any(x => x.ColumnIndex == 6))
            {
                this.edit_cmd_send = true;
                SendKeys.Send("{Tab}");
                return false;
            }

            if (keyData == Keys.Enter &&
                dataGridView1.EditingControl != null &&
                msg.HWnd == dataGridView1.EditingControl.Handle &&
                dataGridView1.SelectedCells
                    .Cast<DataGridViewCell>()
                    .Any(x => x.ColumnIndex == 1))
            {
                SendKeys.Send("{Enter}");
                return false;
            }

            if(keyData == Keys.F2)
            {
                Console.WriteLine("dgv1");
                this.dataGridView1.Focus();
                this.ActiveControl = dataGridView1;
                this.dataGridView1.CurrentCell = dataGridView1[2, 0];
                return false;
            }
            if(keyData==Keys.F3)
            {
                Console.WriteLine("cb");
                this.coneComboboxCB.Focus();
                this.ActiveControl = coneComboboxCB;
                return false;
            }
            if (keyData == Keys.F1)
            {
                Console.WriteLine("dgv2");
                this.dataGridView2.Focus();
                this.ActiveControl = dataGridView2;
                this.dataGridView2.CurrentCell = dataGridView2[1, 0];
                return false;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private DbConnect c;
        private bool edit_cmd_send = false;
        private bool edit_form = false;
        private List<string> batch_nos;
        private M_V_history v1_history;
        private int voucher_id;
        private int batch_state;
        private int highest_carton_no;
        private List<string> batch_fiscal_year_list; //Stroes fiscal year of batches during edit only
        private List<string> show_batches; //Stores the batches in fiscal year format only during edit mode
        Dictionary<string, bool> carton_editable = new Dictionary<string, bool>();
        Dictionary<Tuple<string, string>, DataRow> batch_data = new Dictionary<Tuple<string, string>, DataRow>();
        DataTable dt;
        DateTimePicker dtp = new DateTimePicker();
        public M_V3_cartonProductionForm()
        {
            InitializeComponent();
            this.c = new DbConnect();
            this.batch_nos = new List<string>();
            this.saveButton.Enabled = false;
            this.dt = new DataTable();

            //Create drop-down Colour list
            var dataSource1 = new List<string>();
            DataTable dt = c.getQC('l');
            dataSource1.Add("---Select---");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dataSource1.Add(dt.Rows[i][0].ToString());
            }
            List<string> final_list = dataSource1.Distinct().ToList();
            this.colourComboboxCB.DataSource = final_list;
            this.colourComboboxCB.DisplayMember = "Colour";
            this.colourComboboxCB.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.colourComboboxCB.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.colourComboboxCB.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            //Create drop-down Quality list
            var dataSource2 = new List<string>();
            dt = c.getQC('q');
            dataSource2.Add("---Select---");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dataSource2.Add(dt.Rows[i][0].ToString());
            }
            this.qualityComboboxCB.DataSource = dataSource2;
            this.qualityComboboxCB.DisplayMember = "Quality";
            this.qualityComboboxCB.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.qualityComboboxCB.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.qualityComboboxCB.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            //Create drop-down Dyeing Company list
            var dataSource3 = new List<string>();
            dt = c.getQC('d');
            dataSource3.Add("---Select---");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dataSource3.Add(dt.Rows[i][0].ToString());
            }
            this.dyeingCompanyComboboxCB.DataSource = dataSource3;
            this.dyeingCompanyComboboxCB.DisplayMember = "Dyeing_Company_Names";
            this.dyeingCompanyComboboxCB.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.dyeingCompanyComboboxCB.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.dyeingCompanyComboboxCB.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            //Create drop-down Cone list
            var dataSource4 = new List<string>();
            dt = c.getQC('n');
            dataSource4.Add("---Select---");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dataSource4.Add(dt.Rows[i][0].ToString());
                if (c.getDefault("Default", "Cone") == dt.Rows[i][0].ToString()) Console.WriteLine("hi");
            }
            this.coneComboboxCB.DataSource = dataSource4;
            this.coneComboboxCB.DisplayMember = "Cones";
            this.coneComboboxCB.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.coneComboboxCB.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.coneComboboxCB.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            Console.WriteLine("aaaa " + c.getDefault("Default", "Cone"));
            this.coneComboboxCB.SelectedIndex = this.coneComboboxCB.FindStringExact(c.getDefault("Default", "Cone")); //default selected cone

            //Create drop-down Fiscal Year lists
            var dataSource5 = new List<string>();
            DataTable d5 = c.getQC('f');
            for (int i = 0; i < d5.Rows.Count; i++)
            {
                dataSource5.Add(d5.Rows[i][0].ToString());
            }
            this.financialYearComboboxCB.DataSource = dataSource5;
            this.financialYearComboboxCB.DisplayMember = "Financial Year";
            this.financialYearComboboxCB.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.financialYearComboboxCB.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.financialYearComboboxCB.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            this.financialYearComboboxCB.SelectedIndex = this.financialYearComboboxCB.FindStringExact(c.getFinancialYear(this.inputDate.Value));

            List<string> grade = new List<string>();
            grade.Add("1st");
            grade.Add("PQ");
            grade.Add("CLQ");
            grade.Add("Redyeing");
            grade.Add("Waste");
            //DatagridView 1
            dataGridView1.Columns.Add("Sl_No", "Sl No");
            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns.Add("Production_Date", "Production Date");
            dataGridView1.Columns["Production_Date"].ReadOnly = true;
            dataGridView1.Columns.Add("Carton_Number", "Carton Number");
            DataGridViewComboBoxColumn dgvCmb = new DataGridViewComboBoxColumn();
            dgvCmb.HeaderText = "Grade";
            dgvCmb.Items.Add("---Select---");
            dgvCmb.DataSource = grade;
            dgvCmb.Name = "Grade";
            dataGridView1.Columns.Insert(3, dgvCmb);
            dataGridView1.Columns.Add("Gross_Weight", "Gross Weight");
            dataGridView1.Columns.Add("Carton_Weight", "Carton Weight");
            dataGridView1.Columns.Add("Number_Of_Cones", "Number of Cones");
            dataGridView1.Columns.Add("Net_Weight", "Net Weight");
            dataGridView1.Columns[7].ReadOnly = true;
            dataGridView1.RowCount = 10;
            dataGridView1.Enabled = false;
            c.auto_adjust_dgv(dataGridView1);


            //Datagridview 2
            dataGridView2.Columns.Add("Sl_No", "Sl No");
            dataGridView2.Columns[0].ReadOnly = true;
            DataGridViewComboBoxColumn dgvCmb1 = new DataGridViewComboBoxColumn();
            dgvCmb1.HeaderText = "Batch Number";
            dgvCmb1.Items.Add("---Select---");
            dgvCmb1.DataSource = this.batch_nos;
            dgvCmb1.Name = "Batch Number";
            dataGridView2.Columns.Insert(1, dgvCmb1);
            dataGridView2.Columns[0].Width = 50;
            dataGridView2.Columns[1].Width = 150;
            dataGridView2.Columns.Add("Weight", "Weight");
            c.auto_adjust_dgv(dataGridView2);

            c.set_dgv_column_sort_state(this.dataGridView1, DataGridViewColumnSortMode.NotSortable);
            c.set_dgv_column_sort_state(this.dataGridView2, DataGridViewColumnSortMode.NotSortable);
        }
        public M_V3_cartonProductionForm(DataRow row, bool isEditable, M_V_history v1_history)
        {

            InitializeComponent();
            this.dt = new DataTable();
            this.edit_form = true;
            this.v1_history = v1_history;
            this.saveButton.Enabled = false;
            this.c = new DbConnect();
            this.batch_nos = new List<string>();
            this.show_batches = new List<string>();
            this.batch_fiscal_year_list = new List<string>();

            //Create drop-down Colour list
            var dataSource1 = new List<string>();
            DataTable dt = c.getQC('l');
            dataSource1.Add("---Select---");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dataSource1.Add(dt.Rows[i][0].ToString());
            }
            List<string> final_list = dataSource1.Distinct().ToList();
            this.colourComboboxCB.DataSource = final_list;
            this.colourComboboxCB.DisplayMember = "Colour";
            this.colourComboboxCB.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.colourComboboxCB.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.colourComboboxCB.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            //Create drop-down Quality list
            var dataSource2 = new List<string>();
            dt = c.getQC('q');
            dataSource2.Add("---Select---");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dataSource2.Add(dt.Rows[i][0].ToString());
            }
            this.qualityComboboxCB.DataSource = dataSource2;
            this.qualityComboboxCB.DisplayMember = "Quality";
            this.qualityComboboxCB.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.qualityComboboxCB.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.qualityComboboxCB.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            //Create drop-down Dyeing Company list
            var dataSource3 = new List<string>();
            dt = c.getQC('d');
            dataSource3.Add("---Select---");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dataSource3.Add(dt.Rows[i][0].ToString());
            }
            this.dyeingCompanyComboboxCB.DataSource = dataSource3;
            this.dyeingCompanyComboboxCB.DisplayMember = "Dyeing_Company_Names";
            this.dyeingCompanyComboboxCB.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.dyeingCompanyComboboxCB.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.dyeingCompanyComboboxCB.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            //Create drop-down Cone list
            var dataSource4 = new List<string>();
            dt = c.getQC('n');
            dataSource4.Add("---Select---");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dataSource4.Add(dt.Rows[i][0].ToString());
            }
            this.coneComboboxCB.DataSource = dataSource4;
            this.coneComboboxCB.DisplayMember = "Cones";
            this.coneComboboxCB.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.coneComboboxCB.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.coneComboboxCB.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.coneComboboxCB.SelectedIndex = 1; //default selected cone

            //Create drop-down Fiscal Year lists
            var dataSource5 = new List<string>();
            DataTable d5 = c.getQC('f');
            for (int i = 0; i < d5.Rows.Count; i++)
            {
                dataSource5.Add(d5.Rows[i][0].ToString());
            }
            this.financialYearComboboxCB.DataSource = dataSource5;
            this.financialYearComboboxCB.DisplayMember = "Financial Year";
            this.financialYearComboboxCB.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.financialYearComboboxCB.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.financialYearComboboxCB.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            //DatagridView 1
            List<string> grade = new List<string>();
            grade.Add("1st");
            grade.Add("PQ");
            grade.Add("CLQ");
            grade.Add("Redyeing");
            //DatagridView 1
            dataGridView1.Columns.Add("Sl_No", "Sl No");
            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns.Add("Production_Date", "Production Date");
            dataGridView1.Columns["Production_Date"].ReadOnly = true;
            dataGridView1.Columns.Add("Carton_Number", "Carton Number");
            DataGridViewComboBoxColumn dgvCmb = new DataGridViewComboBoxColumn();
            dgvCmb.HeaderText = "Grade";
            dgvCmb.Items.Add("---Select---");
            dgvCmb.DataSource = grade;
            dgvCmb.Name = "Grade";
            dataGridView1.Columns.Insert(3, dgvCmb);
            dataGridView1.Columns.Add("Gross_Weight", "Gross Weight");
            dataGridView1.Columns.Add("Carton_Weight", "Carton Weight");
            dataGridView1.Columns.Add("Number_Of_Cones", "Number of Cones");
            dataGridView1.Columns.Add("Net_Weight", "Net Weight");
            dataGridView1.Columns["Net_Weight"].ReadOnly = true;
            dataGridView1.RowCount = 10;
            c.auto_adjust_dgv(dataGridView1);

            //Datagridview 2
            dataGridView2.Columns.Add("Sl_No", "Sl No");
            dataGridView2.Columns[0].ReadOnly = true;
            DataGridViewComboBoxColumn dgvCmb1 = new DataGridViewComboBoxColumn();
            dgvCmb1.HeaderText = "Batch Number";
            dgvCmb1.Items.Add("---Select---");
            dgvCmb1.Name = "Batch Number";
            dataGridView2.Columns.Insert(1, dgvCmb1);
            dataGridView2.Columns[0].Width = 50;
            dataGridView2.Columns[1].Width = 150;
            dataGridView2.Columns.Add("Weight", "Weight");
            c.auto_adjust_dgv(dataGridView2);

            this.colourComboboxCB.Enabled = false;
            this.qualityComboboxCB.Enabled = false;
            this.dyeingCompanyComboboxCB.Enabled = false;
            this.financialYearComboboxCB.Enabled = false;
            Console.WriteLine(isEditable.ToString());
            if (isEditable == false)
            {
                this.Text += "(View Only)";
                this.deleteButton.Visible = true;
                this.deleteButton.Enabled = true;
                this.disable_form_edit();
            }
            else
            {
                //no option to edit company name and quality
                Console.WriteLine("Else");
                this.Text += "(Edit)";
                this.saveButton.Enabled = true;
                this.loadDataButton.Enabled = false;
                this.dataGridView1.Enabled = true;
                this.dataGridView1.ReadOnly = false;
                this.dataGridView2.ReadOnly = false;
            }
            this.inputDate.Value = Convert.ToDateTime(row["Date_Of_Input"].ToString());
            this.voucher_id = int.Parse(row["Voucher_ID"].ToString());

            this.colourComboboxCB.SelectedIndex = this.colourComboboxCB.FindStringExact(row["Colour"].ToString());
            this.qualityComboboxCB.SelectedIndex = this.qualityComboboxCB.FindStringExact(row["Quality"].ToString());
            this.dyeingCompanyComboboxCB.SelectedIndex = this.dyeingCompanyComboboxCB.FindStringExact(row["Dyeing_Company_Name"].ToString());
            this.financialYearComboboxCB.SelectedIndex = this.financialYearComboboxCB.FindStringExact(row["Carton_Fiscal_Year"].ToString());
            this.coneComboboxCB.SelectedIndex = this.coneComboboxCB.FindStringExact((float.Parse(row["Cone_Weight"].ToString())*1000F).ToString());
            this.wastageTB.Text = row["Wastage"].ToString();
            this.voucher_id = int.Parse(row["Voucher_ID"].ToString());

            if(row["Voucher_Closed"].ToString()=="0")
            {
                this.closedCheckboxCK.Checked = false;
            }
            else
            {
                this.closedCheckboxCK.Checked = true;
                this.closedCheckboxCK.Enabled = false;
                this.oilGainTextbox.Text = row["Oil_Gain"].ToString();
            }

            string[] produced_cartons = c.csvToArray(row["Carton_No_Production_Arr"].ToString());
            Dictionary<string, DataRow> carton_dict = new Dictionary<string, DataRow>();
            DataTable carton_info = c.getTableData("Carton_Produced", "*", "Carton_No IN (" + c.removecom(row["Carton_No_Production_Arr"].ToString()) + ") AND Fiscal_Year = '" + row["Carton_Fiscal_Year"].ToString() + "'");
            for(int i=0;i<carton_info.Rows.Count;i++)
            {
                carton_dict.Add(carton_info.Rows[i]["Carton_No"].ToString(), carton_info.Rows[i]);
            }
            dataGridView1.RowCount = produced_cartons.Length + 1;
            bool flag = false;
            for (int i = 0; i < produced_cartons.Length; i++)
            {
                DataRow carton_row = carton_dict[produced_cartons[i]];
                if (carton_row == null)
                {
                    continue;
                }
                string correctformat = Convert.ToDateTime(carton_row["Date_Of_Production"].ToString()).Date.ToString().Substring(0, 10);
                dataGridView1.Rows[i].Cells[1].Value = correctformat;
                dataGridView1.Rows[i].Cells[2].Value = produced_cartons[i];
                dataGridView1.Rows[i].Cells[3].Value = carton_row["Grade"].ToString();
                dataGridView1.Rows[i].Cells[4].Value = carton_row["Gross_Weight"].ToString();
                dataGridView1.Rows[i].Cells[5].Value = carton_row["Carton_Weight"].ToString();
                dataGridView1.Rows[i].Cells[6].Value = carton_row["Number_Of_cones"].ToString();
                dataGridView1.Rows[i].Cells[7].Value = carton_row["Net_Weight"].ToString();

                //Sold carton will be coloured green
                if (carton_row["Carton_State"].ToString() != "1")
                {
                    flag = true;
                    this.carton_editable[produced_cartons[i]] = false;
                    DataGridViewRow r = (DataGridViewRow)dataGridView1.Rows[i];
                    dataGridView1.Rows[i].ReadOnly = true;
                    r.DefaultCellStyle.BackColor = Color.LightGreen;
                    r.DefaultCellStyle.SelectionBackColor = Color.LightGreen;
                    this.deleteButton.Enabled = false;
                    this.label16.Text = "This voucher cannot be deleted as some cartons have ben sold";
                }
            }
            //if(flag == true)
            //{
            //    //If any one carton in the voucher is sold, batch cannot be edited
            //    this.dataGridView2.ReadOnly = true;
            //    this.deleteToolStripMenuItem1.Enabled = false;
            //    this.label14.Text = "This batches cannot be edited or deleted as some cartons have already been sold. Delete sale DO to edit it";
            //    this.deleteButton.Enabled = false;
            //}
            if(flag==true)
            {
                this.coneComboboxCB.Enabled = false;
            }
            this.cartonweight.Text = CellSum1(7).ToString("F3");
            
            //Adding batch numbers to datagridview 2
            string[] temp_batch_no_arr = c.csvToArray(row["Batch_No_Arr"].ToString());
            string[] batch_fiscal_year_arr = c.csvToArray(row["Batch_Fiscal_year_Arr"].ToString());
            List<string> full_batch_nos = new List<string>();  //stores all batches old and new
            for (int i = 0; i < temp_batch_no_arr.Length; i++)
            {
                this.show_batches.Add(temp_batch_no_arr[i]);
                full_batch_nos.Add(temp_batch_no_arr[i]);
                this.batch_fiscal_year_list.Add(batch_fiscal_year_arr[i]);
            }
            string today_fiscal_year = c.getFinancialYear(DateTime.Now);
            List<int> minmax_years = c.getFinancialYearArr(this.financialYearComboboxCB.Text);
            this.loadData(today_fiscal_year, minmax_years);
            for(int i=0;i<temp_batch_no_arr.Length;i++)
            {
                Tuple<string, string> temp = new Tuple<string, string>(temp_batch_no_arr[i], batch_fiscal_year_arr[i]);
                DataTable batch_row = c.getTableData("Batch", "*", "Batch_No = " + temp_batch_no_arr[i] + " AND Fiscal_Year = '" + batch_fiscal_year_arr[i] + "'");
                this.batch_data.Add(temp, batch_row.Rows[0]);
            }

            for (int i = 0; i < this.batch_nos.Count; i++)
            {
                this.show_batches.Add(this.batch_nos[i]);
                full_batch_nos.Add(this.batch_nos[i]);
                this.batch_fiscal_year_list.Add(this.dt.Rows[i]["Fiscal_Year"].ToString());
            }
            for (int i = 0; i < full_batch_nos.Count; i++)
            {
                this.show_batches[i] = full_batch_nos[i] + "  (" + this.batch_fiscal_year_list[i] + ")";
            }
            dgvCmb1.DataSource = this.show_batches;
            dataGridView2.RowCount = temp_batch_no_arr.Length + 1;
            for (int i = 0; i < temp_batch_no_arr.Length; i++)
            {                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               
                DataRow batch_row = c.getBatchRow_BatchNo(int.Parse(temp_batch_no_arr[i]), batch_fiscal_year_arr[i]);
                dataGridView2.Rows[i].Cells[1].Value = this.show_batches[i];
                dataGridView2.Rows[i].Cells[2].Value = batch_row["Net_Weight"].ToString();
            }
            this.batchnwtTextbox.Text = this.CellSum2(2).ToString("F3");

            //highest carton number;
            this.highest_carton_no = int.Parse(c.getNextNumber_FiscalYear("Highest_Carton_Production_No", this.financialYearComboboxCB.Text));
            Console.WriteLine("Constructor: "+this.highest_carton_no.ToString());
            if(isEditable==true) this.dataGridView1.Rows.Add("", "", this.highest_carton_no);
            this.nextcartonnoTB.Text = this.highest_carton_no.ToString();
            c.set_dgv_column_sort_state(this.dataGridView1, DataGridViewColumnSortMode.NotSortable);
            c.set_dgv_column_sort_state(this.dataGridView2, DataGridViewColumnSortMode.NotSortable);
        }
        private void M_V3_cartonProductionForm_Load(object sender, EventArgs e)
        {
            if (Global.access == 2) this.deleteButton.Visible = false;
            dtp.Format = DateTimePickerFormat.Short;
            dtp.Visible = false;
            dtp.Width = 100;
            dataGridView1.Controls.Add(dtp);
            label14.Text = "";
            label16.Text = "";

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

            this.dtp.ValueChanged += new System.EventHandler(this.dtp_ValueChanged);
            this.dtp.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtp_keydown);
            this.colourComboboxCB.Focus();
            if (Global.access == 2)
            {
                this.deleteButton.Visible = false;
            }
        }

        private void dtp_keydown(object sender, EventArgs e)
        {
            try
            {
                Console.WriteLine(dataGridView1.CurrentCell.RowIndex.ToString() + " llllll " + dataGridView1.CurrentCell.ColumnIndex);
                if (dtp.Focused && dataGridView1.CurrentCell.ColumnIndex == 1)
                {
                    //c.SuccessBox("Changed dtp");
                    Console.WriteLine("SETTING is " + dtp.Value.Date.ToString().Substring(0, 10));

                    dataGridView1.CurrentCell.Value = dtp.Value.Date.ToString().Substring(0, 10);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("DTP2 Exception " + ex.Message);
            }
            Console.WriteLine("The value is " +dtp.Value.Date.ToString().Substring(0, 10));
        }

        //user
        public void disable_form_edit()
        {
            this.inputDate.Enabled = false;
            this.loadDataButton.Enabled = false;
            this.saveButton.Enabled = false;
            this.dataGridView1.ReadOnly = true;
            this.dataGridView2.ReadOnly = true;
            this.deleteToolStripMenuItem.Enabled = false;
            this.deleteToolStripMenuItem1.Enabled = false;
            this.qualityComboboxCB.Enabled = false;
            this.dyeingCompanyComboboxCB.Enabled = false;
            this.colourComboboxCB.Enabled = false;
            this.financialYearComboboxCB.Enabled = false;
            this.coneComboboxCB.Enabled = false;
            this.closedCheckboxCK.Enabled = false;
            this.wastageTB.Enabled = false;
        }
        private bool loadData(string today_fiscal_year, List<int> minmax_years)
        {
            this.dt = c.getTableData("Batch", "*", "Batch_State = 2 AND Colour = '"+colourComboboxCB.SelectedItem.ToString()+"' AND Dyeing_Company_Name = '"+dyeingCompanyComboboxCB.SelectedItem.ToString()+"'AND Quality = '"+qualityComboboxCB.SelectedItem.ToString()+"'");
            for(int i=0;i<dt.Rows.Count;i++)
            {
                Tuple<string, string> temp = new Tuple<string, string>(dt.Rows[i]["Batch_No"].ToString(), dt.Rows[i]["Fiscal_Year"].ToString());
                this.batch_data.Add(temp, dt.Rows[i]);
            }
            List<string> batch_no_arr = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                batch_no_arr.Add(dt.Rows[i]["Batch_No"].ToString());
            }
            if (this.edit_form == false)
            {
                for (int i = 0; i < batch_no_arr.Count; i++)
                {
                    batch_no_arr[i] = dt.Rows[i]["Batch_No"].ToString() + "  (" + dt.Rows[i]["Fiscal_Year"].ToString() + ")";
                }
            }
            for (int i = 0; i < batch_no_arr.Count; i++)
            {
                this.batch_nos.Add(batch_no_arr[i]);
            }
            if(dt.Rows.Count<=0 && this.edit_form == false)
            {
                c.WarningBox("No Batches Found");
                return false;
            }
            if (this.edit_form == false)
            {
                c.SuccessBox("Loaded " + dt.Rows.Count.ToString() + " Batches");
            }

            this.dtp.MinDate = new DateTime(minmax_years[0], 04, 01);
            if (today_fiscal_year == this.financialYearComboboxCB.Text)
            {
                this.dtp.MaxDate = this.inputDate.Value;
                if(this.edit_form == true)
                {
                    this.dtp.MaxDate = DateTime.Now;
                }
            }
            else
            {
                this.dtp.MaxDate = new DateTime(minmax_years[1], 03, 31);
            }
            return true;
        }
        private float CellSum1(int col)
        {
            float sum = 0;
            try
            {
                if (dataGridView1.Rows.Count == 0)
                {
                    return sum;
                }
                for (int i = 0; i < dataGridView1.Rows.Count; ++i)
                {
                    if (dataGridView1.Rows[i].Cells[col].Value != null)
                        sum += float.Parse(dataGridView1.Rows[i].Cells[col].Value.ToString());
                }
                return sum;
            }
            catch
            {
                return sum;
            }
        }
        private float CellSum2(int col)
        {
            float sum = 0;
            try
            {
                if (dataGridView2.Rows.Count == 0)
                {
                    return sum;
                }
                for (int i = 0; i < dataGridView2.Rows.Count; ++i)
                {
                    if (dataGridView2.Rows[i].Cells[col].Value != null)
                        sum += float.Parse(dataGridView2.Rows[i].Cells[col].Value.ToString());
                }
                return sum;
            }
            catch
            {
                return sum;
            }
        }
        private void oilGainButton_Calculate()
        {
            float net_weight, batch_weight, wastage;
            try
            {
                batch_weight = float.Parse(batchnwtTextbox.Text);
            }
            catch
            {
                oilGainTextbox.Text = "Incorrect Net Batch Weight";
                return;
            }
            try
            {
                net_weight = float.Parse(cartonweight.Text);
            }
            catch
            {
                oilGainTextbox.Text = "Incorrect Net Carton Weight";
                return;
            }
            try
            {
                wastage = float.Parse(this.wastageTB.Text);
            }
            catch
            {
                oilGainTextbox.Text = "Incorrect Wastage";
                return;
            }
            if (net_weight - (batch_weight - wastage) < 0F)
            {
                oilGainTextbox.Text = "Net Carton Wt < Net Batch Wt - Wastage";
                return;
            }
            oilGainTextbox.Text = ((net_weight - (batch_weight - wastage)) / (batch_weight - wastage) * 100F).ToString("F2");
        }
        public void calculate_net_wt(int row_index)
        {
            if (dataGridView1.Rows[row_index].Cells[4].Value == null || dataGridView1.Rows[row_index].Cells[5].Value == null || dataGridView1.Rows[row_index].Cells[6].Value == null)
            {
                dataGridView1.Rows[row_index].Cells[7].Value = null;
                cartonweight.Text = CellSum1(7).ToString("F3");
                return;
            }
            if (dataGridView1.Rows[row_index].Cells[4].Value == "" || dataGridView1.Rows[row_index].Cells[5].Value == "" || dataGridView1.Rows[row_index].Cells[6].Value == "")
            {
                dataGridView1.Rows[row_index].Cells[7].Value = null;
                cartonweight.Text = CellSum1(7).ToString("F3");
                return;
            }
            if (coneComboboxCB.SelectedIndex == 0)
            {
                dataGridView1.Rows[row_index].Cells[7].Value = "Please select Cone Wt";
            }
            float net_weight = 0F;
            try
            {
                float gross_weight = float.Parse(dataGridView1.Rows[row_index].Cells[4].Value.ToString());
                float carton_weight = float.Parse(dataGridView1.Rows[row_index].Cells[5].Value.ToString());
                float cone_weight = int.Parse(dataGridView1.Rows[row_index].Cells[6].Value.ToString()) * float.Parse(coneComboboxCB.Text) * 0.001F;
                net_weight = (gross_weight - carton_weight - cone_weight);
            }
            catch
            {

            }

            if (net_weight < 0)
            {
                c.ErrorBox("Net Weight (" + net_weight.ToString() + ") should be positive only. Please check your entries", "Error");
                for (int i = 4; i <= 6; i++)
                {
                    dataGridView1.Rows[row_index].Cells[i].Value = "";
                }
                cartonweight.Text = CellSum1(7).ToString("F3");
                return;
            }
            dataGridView1.Rows[row_index].Cells[7].Value = net_weight.ToString();
            cartonweight.Text = CellSum1(7).ToString("F3");
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (c.check_login_val() == false) return;
            this.cartonweight.Text = CellSum1(7).ToString("F3");
            this.batchnwtTextbox.Text = CellSum2(2).ToString("F3");
            //checks
            if (coneComboboxCB.SelectedIndex == 0)
            {
                c.ErrorBox("Select Cone Weight", "Error");
                return;
            }
            if (dataGridView1.Rows[0].Cells[1].Value==null)
            {
                c.ErrorBox("Please enter values", "Error");
                return;
            }
            try
            {
                float.Parse(cartonweight.Text);
                float.Parse(batchnwtTextbox.Text);
                float.Parse(wastageTB.Text);
            }
            catch
            {
                c.ErrorBox("Please enter carton/batch/wastage numbers");
                return;
            }
            if ((float.Parse(cartonweight.Text) - (float.Parse(batchnwtTextbox.Text) - float.Parse(this.wastageTB.Text)) < 0F) && closedCheckboxCK.Checked==true)
            {
                c.ErrorBox("Net Carton Weight should be greater than or equal to Net Batch Weight", "Error");
                return;
            }
            if(coneComboboxCB.Text!= c.getDefault("Default", "Cone"))
            {
                DialogResult dialogResult = MessageBox.Show("You have selected " + coneComboboxCB.Text + "g as cone weight!", "Warning",  MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if(dialogResult == DialogResult.Cancel)
                {
                    return;
                }
                
            }

            string production_dates = "", carton_nos = "", gross_weights = "", carton_weights = "", number_of_cones = "", net_weights = "", grades="";
            int number = 0;

            List<int> temp = new List<int>();
            string batch_nos = "";
            for (int i = 0; i < dataGridView2.Rows.Count - 1; i++)
            {   
                if(c.Cell_Not_NullOrEmpty(this.dataGridView2, i, 1))
                {
                    batch_nos += dataGridView2.Rows[i].Cells[1].Value.ToString() + ",";
                }
            }
            if(batch_nos=="")
            {
                c.ErrorBox("Please enter batches");
                return;
            }
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                int sum = 0;
                for(int j=1; j<=7;j++)
                {
                    if(dataGridView1.Rows[i].Cells[j].Value==null)
                    {

                    }
                    else if (dataGridView1.Rows[i].Cells[j].Value.ToString() == "") { }
                    else
                    {
                        if (j == 1)
                        {
                            try
                            {
                                Convert.ToDateTime(this.dataGridView1.Rows[i].Cells[1].Value.ToString());
                            }
                            catch
                            {
                                c.ErrorBox("Please enter correct Production Date format in row: " + (i + 1).ToString());
                                return;
                            }
                        }
                        sum++;
                    }
                }
                if(sum==0)
                {
                    continue;
                }
                else if(sum!=7)
                {
                    c.ErrorBox("Missing values in " + (i + 1).ToString() + " row", "Error");
                    return;
                }
                ComboBox cbox = (ComboBox)dataGridView1.EditingControl;
                if (dataGridView1.Rows[i].Cells[1].Value == null || dataGridView1.Rows[i].Cells[1].Value.ToString() == "")
                {
                    continue;
                }
                else
                {
                    production_dates += dataGridView1.Rows[i].Cells[1].Value.ToString() + ",";
                    carton_nos += dataGridView1.Rows[i].Cells[2].Value.ToString() + ",";
                    grades += dataGridView1.Rows[i].Cells[3].Value.ToString() + ",";
                    gross_weights += dataGridView1.Rows[i].Cells[4].Value.ToString() + ",";
                    carton_weights += dataGridView1.Rows[i].Cells[5].Value.ToString() + ",";
                    number_of_cones+= dataGridView1.Rows[i].Cells[6].Value.ToString() + ",";
                    net_weights += dataGridView1.Rows[i].Cells[7].Value.ToString() + ",";

                    number++;

                    //to check for all different tray_nos
                    temp.Add(int.Parse(dataGridView1.Rows[i].Cells[2].Value.ToString()));
                    var distinctBytes = new HashSet<int>(temp);
                    bool allDifferent = distinctBytes.Count == temp.Count;
                    if (allDifferent == false)
                    {
                        c.ErrorBox("Carton Nos repeated at Row: " + (i + 1).ToString(), "Error");
                        return;
                    }
                }
            }
            
            int closed;
            if(closedCheckboxCK.Checked==true)
            {
                closed = 1;
            }
            else
            {
                closed = 0;
            }
            if (this.edit_form == false)
            {
                Console.WriteLine("Batch Nos: " + batch_nos);
                bool added= c.addCartonProductionVoucher(inputDate.Value, colourComboboxCB.Text, qualityComboboxCB.Text, dyeingCompanyComboboxCB.Text, financialYearComboboxCB.Text, coneComboboxCB.Text, production_dates, carton_nos, gross_weights, carton_weights, number_of_cones, net_weights, batch_nos, closed, float.Parse(batchnwtTextbox.Text), float.Parse(cartonweight.Text), float.Parse(this.wastageTB.Text), grades, batch_data);
                if (added == true)
                {
                    dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.LawnGreen;
                    disable_form_edit();
                }   
                else return;
            }
            else
            {
                bool edited=c.editCartonProductionVoucher(this.voucher_id, colourComboboxCB.Text, qualityComboboxCB.Text, dyeingCompanyComboboxCB.Text, financialYearComboboxCB.Text, coneComboboxCB.Text, production_dates, carton_nos, gross_weights, carton_weights, number_of_cones, net_weights, batch_nos, closed, float.Parse(batchnwtTextbox.Text), float.Parse(cartonweight.Text), float.Parse(this.wastageTB.Text), grades, this.carton_editable, batch_data);
                if (edited == true)
                {
                    dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.LawnGreen;
                    disable_form_edit();
                }
                else return;
                this.v1_history.loadData();
            }
            dataGridView1.EnableHeadersVisualStyles = false;
        }
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.edit_form == false)
            {
                int count = dataGridView1.SelectedRows.Count;
                for (int i = 0; i < count; i++)
                {
                    if (dataGridView1.SelectedRows[0].Index == dataGridView1.Rows.Count - 1)
                    {
                        dataGridView1.SelectedRows[0].Selected = false;
                        continue;
                    }
                    dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);
                }
                this.cartonweight.Text = CellSum1(7).ToString("F3");
            }
            else
            {
                int count = dataGridView1.SelectedRows.Count;
                for (int i = 0; i < count; i++)
                {
                    if (dataGridView1.SelectedRows[0].Index == dataGridView1.Rows.Count - 1)
                    {
                        dataGridView1.SelectedRows[0].Selected = false;
                        continue;
                    }
                    int rowindex = dataGridView1.SelectedRows[0].Index;
                    if(dataGridView1.Rows[rowindex].Cells[2].Value==null)
                    {
                        dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);
                        continue;
                    }
                    string carton_no = dataGridView1.Rows[rowindex].Cells[2].Value.ToString();
                    bool value = true;
                    bool value2 = this.carton_editable.TryGetValue(carton_no, out value);
                    if (value2 == true && value == false)
                    {
                        c.ErrorBox("Cannot delete entry at row: " + (rowindex + 1).ToString(), "Error");
                        dataGridView1.Rows[rowindex].Selected = false;
                        continue;
                    }
                    dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);
                }
                this.cartonweight.Text = CellSum1(7).ToString("F3");
            }
            this.dtp.Visible = false;
        }
        private void deleteToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            int count2 = dataGridView2.SelectedRows.Count;
            for (int i = 0; i < count2; i++)
            {
                if (dataGridView2.SelectedRows[0].Index == dataGridView2.Rows.Count - 1)
                {
                    dataGridView2.SelectedRows[0].Selected = false;
                    continue;
                }
                dataGridView2.Rows.RemoveAt(dataGridView2.SelectedRows[0].Index);
            }
            batchnwtTextbox.Text = CellSum2(2).ToString("F3");
        }
        private void loadCartonButton_Click(object sender, EventArgs e)
        {
            if (colourComboboxCB.SelectedIndex == 0)
            {
                c.ErrorBox("Select Colour", "Error");
                return;
            }
            if (qualityComboboxCB.SelectedIndex == 0)
            {
                c.ErrorBox("Select Quality", "Error");
                return;
            }
            if (dyeingCompanyComboboxCB.SelectedIndex == 0)
            {
                c.ErrorBox("Select Dyeing Company Name", "Error");
                return;
            }
            string today_fiscal_year = c.getFinancialYear(this.inputDate.Value);
            List<int> today_fiscal_year_arr = c.getFinancialYearArr(today_fiscal_year);
            List<int> minmax_years = c.getFinancialYearArr(this.financialYearComboboxCB.Text);
            if(today_fiscal_year_arr[1]<minmax_years[1])
            {
                c.ErrorBox("You cannot select Carton Financial Year in the future");
                return;
            }
            bool loaded =this.loadData(today_fiscal_year, minmax_years);
            if (loaded == false && this.edit_form==false) return;
            //Set the first date in form
            string current_fiscal_year = c.getFinancialYear(DateTime.Now);
            if(current_fiscal_year==financialYearComboboxCB.Text)
            {
                dataGridView1.Rows[0].Cells[1].Value = DateTime.Now.Date.ToString().Substring(0, 10);
            }
            else
            {
                string[] years = financialYearComboboxCB.Text.Split('-');
                DateTime dt = new DateTime(int.Parse(years[1]),3,31);
                this.dataGridView1.Rows[0].Cells[1].Value = dt.Date.ToString("dd-MM-yyyy").Substring(0, 10);
            }

            //set the first carton number in form
            int next_carton_no =int.Parse(c.getNextNumber_FiscalYear("Highest_Carton_Production_No", this.financialYearComboboxCB.Text));
            dataGridView1.Rows[0].Cells[2].Value = next_carton_no;
            this.nextcartonnoTB.Text = next_carton_no.ToString();

            //enable and disable 
            this.loadDataButton.Enabled = false;
            this.colourComboboxCB.Enabled = false;
            this.qualityComboboxCB.Enabled = false;
            this.financialYearComboboxCB.Enabled = false;
            this.dyeingCompanyComboboxCB.Enabled = false;
            this.saveButton.Enabled = true;
            this.dataGridView1.Enabled = true;
        }
        private void coneCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++) calculate_net_wt(i);
        }
        private void batchnwtTextbox_TextChanged(object sender, EventArgs e)
        {
            this.oilGainButton_Calculate();
        }
        private void cartonweight_TextChanged(object sender, EventArgs e)
        {
            this.oilGainButton_Calculate();
        }
        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (c.check_login_val() == false) return;
            DialogResult dialogResult = MessageBox.Show("Confirm Delete", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                bool deleted = c.deleteCartonProductionVoucher(this.voucher_id);
                if (deleted == true)
                {
                    c.SuccessBox("Voucher Deleted Successfully");
                    this.deleteButton.Enabled = false;
                    this.v1_history.loadData();
                }
                else return;
            }
            else if (dialogResult == DialogResult.No)
            {
                return;
            }
        }


        //dgv
        private void dataGridView1_RowPostPaint_1(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            if (e.RowIndex != dataGridView1.Rows.Count - 1)
            {
                dataGridView1.Rows[e.RowIndex].Cells[0].Value = e.RowIndex + 1;
            }
        }
        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is DataGridViewComboBoxEditingControl)
            {
                ((ComboBox)e.Control).DropDownStyle = ComboBoxStyle.DropDown;
                ((ComboBox)e.Control).AutoCompleteSource = AutoCompleteSource.ListItems;
                ((ComboBox)e.Control).AutoCompleteMode = AutoCompleteMode.Append;
            }
        }
        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                dataGridView1.Rows[e.RowIndex].Selected = true;
            }
        }
        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (dataGridView1.Enabled == false || dataGridView1.ReadOnly == true) return;
            //called when tab is pressed at last row or tab is pressed while editing last row
            if (e.KeyCode == Keys.Tab &&
                (dataGridView1.SelectedCells.Cast<DataGridViewCell>().Any(x => x.ColumnIndex == 6) || this.edit_cmd_send == true))
            {
                Console.WriteLine("Inside Editing");
                bool edit_cmd_local = this.edit_cmd_send;
                this.edit_cmd_send = false;
                int rowindex_tab = dataGridView1.SelectedCells[0].RowIndex;

                if (dataGridView1.Rows.Count - 2 == rowindex_tab)
                {
                    DataGridViewRow row = (DataGridViewRow)dataGridView1.Rows[rowindex_tab].Clone();
                    row.DefaultCellStyle.BackColor = dataGridView1.DefaultCellStyle.BackColor;
                    row.DefaultCellStyle.SelectionBackColor = dataGridView1.DefaultCellStyle.SelectionBackColor;
                    dataGridView1.Rows.Add(row);
                }
                if (dataGridView1.Rows.Count - 1 == rowindex_tab)
                {
                    Console.WriteLine("This case");
                    return;
                }
                if (c.Cell_Not_NullOrEmpty(this.dataGridView1, rowindex_tab, 1) && !c.Cell_Not_NullOrEmpty(this.dataGridView1, rowindex_tab + 1, 1))
                {
                    dataGridView1.Rows[rowindex_tab + 1].Cells[1].Value = dataGridView1.Rows[rowindex_tab].Cells[1].Value;
                }
                if (c.Cell_Not_NullOrEmpty(this.dataGridView1, rowindex_tab, 2) && !c.Cell_Not_NullOrEmpty(this.dataGridView1, rowindex_tab + 1, 2))
                {
                    dataGridView1.Rows[rowindex_tab + 1].Cells[2].Value = (int.Parse(dataGridView1.Rows[rowindex_tab].Cells[2].Value.ToString()) + 1).ToString();
                }
                if (c.Cell_Not_NullOrEmpty(this.dataGridView1, rowindex_tab, 3) && !c.Cell_Not_NullOrEmpty(this.dataGridView1, rowindex_tab + 1, 3))
                {
                    dataGridView1.Rows[rowindex_tab + 1].Cells[3].Value = dataGridView1.Rows[rowindex_tab].Cells[3].Value;
                }
                //bindingSource.EndEdit();
                dataGridView1.NotifyCurrentCellDirty(true);
                dataGridView1.EndEdit();
                dataGridView1.NotifyCurrentCellDirty(false);
                //SendKeys.Send("{tab}");
                if (edit_cmd_local == false)
                {
                    SendKeys.Send("{tab}");
                }
            }
            if (e.KeyCode == Keys.Tab &&
                (dataGridView1.SelectedCells.Cast<DataGridViewCell>().Any(x => x.ColumnIndex == 7)))
            {
                Console.WriteLine("Inside Editing");
                int rowindex_tab = dataGridView1.SelectedCells[0].RowIndex;
                if (dataGridView1.Rows.Count - 2 == rowindex_tab)
                {
                    DataGridViewRow row = (DataGridViewRow)dataGridView1.Rows[rowindex_tab].Clone();
                    row.DefaultCellStyle.BackColor = dataGridView1.DefaultCellStyle.BackColor;
                    row.DefaultCellStyle.SelectionBackColor = dataGridView1.DefaultCellStyle.SelectionBackColor;
                    dataGridView1.Rows.Add(row);
                }
                if (dataGridView1.Rows.Count - 1 == rowindex_tab)
                {
                    Console.WriteLine("This case");
                    return;
                }
                if (c.Cell_Not_NullOrEmpty(this.dataGridView1, rowindex_tab, 1) && !c.Cell_Not_NullOrEmpty(this.dataGridView1, rowindex_tab + 1, 1))
                {
                    dataGridView1.Rows[rowindex_tab + 1].Cells[1].Value = dataGridView1.Rows[rowindex_tab].Cells[1].Value;
                }
                if (c.Cell_Not_NullOrEmpty(this.dataGridView1, rowindex_tab, 2) && !c.Cell_Not_NullOrEmpty(this.dataGridView1, rowindex_tab + 1, 2))
                {
                    dataGridView1.Rows[rowindex_tab + 1].Cells[2].Value = (int.Parse(dataGridView1.Rows[rowindex_tab].Cells[2].Value.ToString()) + 1).ToString();
                }
                if (c.Cell_Not_NullOrEmpty(this.dataGridView1, rowindex_tab, 3) && !c.Cell_Not_NullOrEmpty(this.dataGridView1, rowindex_tab + 1, 3))
                {
                    dataGridView1.Rows[rowindex_tab + 1].Cells[3].Value = dataGridView1.Rows[rowindex_tab].Cells[3].Value;
                }
                //bindingSource.EndEdit();
                dataGridView1.NotifyCurrentCellDirty(true);
                dataGridView1.EndEdit();
                dataGridView1.NotifyCurrentCellDirty(false);
                SendKeys.Send("{tab}");
                SendKeys.Send("{tab}");
                SendKeys.Send("{tab}");
                SendKeys.Send("{tab}");
            }

            if (e.KeyCode == Keys.Enter && dataGridView1.SelectedCells.Cast<DataGridViewCell>().Any(x => x.ColumnIndex == 1))
            {
                try
                {
                    if (dataGridView1.Focused && dataGridView1.CurrentCell.ColumnIndex == 1)
                    {
                        //int i = dataGridView1.CurrentCell.RowIndex;
                        //int j = dataGridView1.CurrentCell.ColumnIndex;
                        DateTime d;
                        if (string.IsNullOrEmpty(dataGridView1.CurrentCell.Value.ToString()) == false)
                        {
                            d = Convert.ToDateTime(dataGridView1.CurrentCell.Value);
                        }
                        else
                        {
                            d = DateTime.Today;
                        }
                        setDate f = new setDate(d);
                        f.setMinMax(dtp.MinDate, dtp.MaxDate);
                        f.ShowDialog();
                        dataGridView1.CurrentCell.Value = f.result.Date.ToString().Substring(0, 10);
                        e.Handled = true;
                    }
                    else
                    {
                        dtp.Visible = false;
                    }
                }
                catch
                {
                    Console.WriteLine("DTP Exception");
                }

            }
            if (e.KeyCode == Keys.Enter &&
               (dataGridView1.SelectedCells.Cast<DataGridViewCell>().Any(x => x.ColumnIndex == 3) || this.edit_cmd_send == true))
            {
                dataGridView1.BeginEdit(true);
                ComboBox c = (ComboBox)dataGridView1.EditingControl;
                if(c!=null)
                {
                    if(c!=null) c.DroppedDown = true;
                    SendKeys.Send("{down}");
                    SendKeys.Send("{up}");
                }
                e.Handled = true;
            }
        }
        private void dataGridView1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dataGridView1.IsCurrentCellDirty)
            {
                dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }
        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            try
            {
                if (dataGridView1.Focused && dataGridView1.CurrentCell.ColumnIndex == 1)
                {
                    //dtp.Location = dataGridView1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Location;
                    //dtp.Visible = true;
                    DateTime d;
                    if (dataGridView1.CurrentCell.Value != DBNull.Value)
                    {
                        d = Convert.ToDateTime(dataGridView1.CurrentCell.Value);
                    }
                    else
                    {
                        d = DateTime.Today;
                    }
                    setDate f = new setDate(d);
                    f.ShowDialog();
                    dataGridView1.CurrentCell.Value = f.dateTimePicker1.Value.Date.ToString().Substring(0, 10);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("DTP Exception " + ex.Message);
            }
        }
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //try
            //{
            //    if (dataGridView1.Focused && dataGridView1.CurrentCell.ColumnIndex == 1)
            //    {
            //        dataGridView1.CurrentCell.Value = dtp.Value.Date.ToString().Substring(0, 10);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("DTP2 Exception " + ex.Message);
            //}

            //Checks for numeric values
            if (e.ColumnIndex == 4 && e.RowIndex >= 0)
            {
                try
                {
                    if (c.Cell_Not_NullOrEmpty(this.dataGridView1, e.RowIndex, e.ColumnIndex))
                    {
                        float.Parse(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                    }
                }
                catch
                {
                    c.ErrorBox("Please Enter Decimal Gross Weight", "Error");
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "";
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].Selected = true;
                    SendKeys.Send("{Left}");
                    return;
                }
            }
            if (e.ColumnIndex == 5 && e.RowIndex >= 0)
            {
                try
                {

                    if (c.Cell_Not_NullOrEmpty(this.dataGridView1, e.RowIndex, e.ColumnIndex))
                    {
                        float.Parse(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                    }
                }
                catch
                {
                    c.ErrorBox("Please Enter Decimal Carton Weight", "Error");
                    SendKeys.Send("{Left}");
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "";
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].Selected = true;
                    return;
                }
            }
            if (e.ColumnIndex == 6 && e.RowIndex >= 0)
            {
                try
                {
                    if(c.Cell_Not_NullOrEmpty(this.dataGridView1, e.RowIndex, e.ColumnIndex))
                    {
                        int.Parse(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                    }
                }
                catch
                {
                    c.ErrorBox("Please Enter Integer Number of Cones", "Error");
                    SendKeys.Send("{Left}");
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "";
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].Selected = true;
                    return;
                }
            }
            //Fill New Wt
            if ((e.ColumnIndex == 6 || e.ColumnIndex == 5 || e.ColumnIndex == 4) && e.RowIndex >= 0)
            {
                calculate_net_wt(e.RowIndex);
            }
        }
        
        private void dataGridView2_KeyDown(object sender, KeyEventArgs e)
        {
            if (dataGridView2.Enabled == false || dataGridView2.ReadOnly == true)
            {
                return;
            }
            if (e.KeyCode == Keys.Enter &&
            (dataGridView2.SelectedCells.Cast<DataGridViewCell>().Any(x => x.ColumnIndex == 1) || this.edit_cmd_send == true))
            {
                dataGridView2.BeginEdit(true);
                ComboBox c = (ComboBox)dataGridView2.EditingControl;
                if(c!=null) c.DroppedDown = true;
                SendKeys.Send("{down}");
                SendKeys.Send("{up}");
                e.Handled = true;
            }
        }
        private void dataGridView2_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            if (e.RowIndex != dataGridView2.Rows.Count - 1)
            {
                dataGridView2.Rows[e.RowIndex].Cells[0].Value = e.RowIndex + 1;
            }
        }
        private void dataGridView2_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is DataGridViewComboBoxEditingControl)
            {
                ((ComboBox)e.Control).DropDownStyle = ComboBoxStyle.DropDown;
                ((ComboBox)e.Control).AutoCompleteSource = AutoCompleteSource.ListItems;
                ((ComboBox)e.Control).AutoCompleteMode = AutoCompleteMode.Append;
            }
        }
        private void dataGridView2_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dataGridView2.IsCurrentCellDirty)
            {
                dataGridView2.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }
        private void dataGridView2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 1)
            {
                for (int i = 0; i < dataGridView2.Rows.Count - 1; i++)
                {
                    for (int j = i + 1; j < dataGridView2.Rows.Count - 1; j++)
                    {
                        if (dataGridView2.Rows[i].Cells[1].Value == null || dataGridView2.Rows[j].Cells[1].Value == null)
                        {
                            continue;
                        }
                        if (dataGridView2.Rows[i].Cells[1].Value.ToString() == dataGridView2.Rows[j].Cells[1].Value.ToString())
                        {
                            c.ErrorBox("Rows " + (i + 1).ToString() + " and " + (j + 1).ToString() + " have same Batch Number", "Error");
                            dataGridView2.Rows[j].Cells[1].Value = "";
                            dataGridView2.Rows[j].Cells[2].Value = "";
                            return;
                        }
                    }
                }
                if(!c.Cell_Not_NullOrEmpty(this.dataGridView2, e.RowIndex, 1))
                {
                    return;
                }
                if(c.check_if_batch_repeated(dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString()))
                {
                    string[] batch = c.repeated_batch_csv(dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString());
                    Tuple<string, string> temp = new Tuple<string, string>(batch[0], batch[1]);
                    dataGridView2.Rows[e.RowIndex].Cells[2].Value = batch_data[temp]["Net_Weight"].ToString();
                    batchnwtTextbox.Text = CellSum2(2).ToString("F3");
                }
            }
        }
        private void dataGridView2_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                dataGridView2.Rows[e.RowIndex].Selected = true;
            }
        }

        private void oilGainTextbox_TextChanged(object sender, EventArgs e)
        {

        }
        private void closedCheckboxCK_CheckedChanged(object sender, EventArgs e)
        {

        }
        private void dtp_ValueChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    if (dataGridView1.Focused && dataGridView1.CurrentCell.ColumnIndex == 1)
            //    {
            //        c.SuccessBox("Changed dtp");
            //        dataGridView1.CurrentCell.Value = dtp.Value.Date.ToString().Substring(0, 10);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("DTP2 Exception " + ex.Message);
            //}
        }

        private void wastageTB_TextChanged(object sender, EventArgs e)
        {
            float wastage = 0f;
            try
            {
                wastage = float.Parse(this.wastageTB.Text.ToString());
            }
            catch
            {
                c.ErrorBox("Wastage should be a number");
                return;
            }

            this.oilGainButton_Calculate();
        }
    }
}
