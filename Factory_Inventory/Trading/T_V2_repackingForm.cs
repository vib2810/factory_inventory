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
    public partial class T_V2_repackingForm : Form
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

            if (keyData == Keys.F2)
            {
                Console.WriteLine("dgv1");
                this.dataGridView1.Focus();
                this.ActiveControl = dataGridView1;
                this.dataGridView1.CurrentCell = dataGridView1[2, 0];
                return false;
            }
            if (keyData == Keys.F3)
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
        private List<string> destroyed_carton_nos_display;
        private M_V_history v1_history;
        private int voucher_id;
        private int highest_carton_no;
        private Dictionary<string, int> colour_dict = new Dictionary<string, int>();
        private Dictionary<string, int> quality_dict = new Dictionary<string, int>();
        private Dictionary<string, int> company_dict = new Dictionary<string, int>();
        ComboBox dgv2_cmb = null;
        private Dictionary<string, Tuple<float, int>> cones_dict = new Dictionary<string, Tuple<float, int>>(); //(name, <weight, ID>)
        Dictionary<string, bool> carton_editable = new Dictionary<string, bool>();  //<Carton ID, bool>
        Dictionary<string, DataRow> destroyed_carton_dict = new Dictionary<string, DataRow>();  //<Carton ID, Carton Row>
        Dictionary<string, Tuple<DataRow, bool>> old_destroyed_carton_dict = new Dictionary<string, Tuple<DataRow, bool>>();  //<Carton ID, <Row, present in edited voucher>> Only used in edit mode to store old cartons
        Dictionary<string, Tuple<DataRow, bool>> old_repacked_carton_dict = new Dictionary<string, Tuple<DataRow, bool>>();    //<Carton_No, <Row, present in edited voucher>> Only used in edit mode to store old cartons. Indexed with Carton Number as CartonNo and Fiscal year are PK.
        Dictionary<Tuple<string, string, string>, string> destroyed_carton_id_get = new Dictionary<Tuple<string, string, string>, string>();  //<{Carton No, Net Weight, Fiscal Year}, Carton ID>
        DataTable dt;
        DateTimePicker dtp = new DateTimePicker();
        public T_V2_repackingForm()
        {
            InitializeComponent();
            this.c = new DbConnect();
            this.destroyed_carton_nos_display = new List<string>();
            this.saveButton.Enabled = false;
            this.dt = new DataTable();

            //Create drop-down Colour list
            var dataSource1 = new List<string>();
            DataTable dt = c.runQuery("SELECT * FROM T_M_Colours");
            dataSource1.Add("---Select---");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dataSource1.Add(dt.Rows[i]["Colour"].ToString());
                colour_dict[dt.Rows[i]["Colour"].ToString()] = int.Parse(dt.Rows[i]["Colour_ID"].ToString());
            }
            List<string> final_list = dataSource1.Distinct().ToList();
            this.colourComboboxCB.DataSource = final_list;
            this.colourComboboxCB.DisplayMember = "Colour";
            this.colourComboboxCB.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.colourComboboxCB.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.colourComboboxCB.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            //Create drop-down Quality list
            var dataSource2 = new List<string>();
            dt = c.runQuery("SELECT * FROM T_M_Quality_Before_Job");
            dataSource2.Add("---Select---");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dataSource2.Add(dt.Rows[i]["Quality_Before_Job"].ToString());
                quality_dict[dt.Rows[i]["Quality_Before_Job"].ToString()] = int.Parse(dt.Rows[i]["Quality_Before_Job_ID"].ToString());
            }
            this.qualityComboboxCB.DataSource = dataSource2;
            this.qualityComboboxCB.DisplayMember = "Quality";
            this.qualityComboboxCB.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.qualityComboboxCB.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.qualityComboboxCB.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            //Create drop-down Company list
            var dataSource3 = new List<string>();
            dt = c.runQuery("SELECT * FROM T_M_Company_Names");
            dataSource3.Add("---Select---");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dataSource3.Add(dt.Rows[i]["Company_Name"].ToString());
                company_dict[dt.Rows[i]["Company_Name"].ToString()] = int.Parse(dt.Rows[i]["Company_ID"].ToString());
            }
            this.companyComboboxCB.DataSource = dataSource3;
            this.companyComboboxCB.DisplayMember = "Company_Names";
            this.companyComboboxCB.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.companyComboboxCB.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.companyComboboxCB.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            //Type CB
            this.typeCB.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.typeCB.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.typeCB.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            //Create drop-down Cone list
            var dataSource4 = new List<string>();
            dt = c.runQuery("SELECT * FROM T_M_Cones");
            dataSource4.Add("---Select---");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dataSource4.Add(dt.Rows[i]["Cone_Name"].ToString());
                cones_dict[dt.Rows[i]["Cone_Name"].ToString()] = new Tuple<float, int>(float.Parse(dt.Rows[i]["Cone_Weight"].ToString()), int.Parse(dt.Rows[i]["Cone_ID"].ToString()));
            }
            this.coneComboboxCB.DataSource = dataSource4;
            this.coneComboboxCB.DisplayMember = "Cones";
            this.coneComboboxCB.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.coneComboboxCB.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.coneComboboxCB.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

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

            //DatagridView 1
            dataGridView1.Columns.Add("Sl_No", "Sl No");
            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns.Add("Production_Date", "Production Date");
            dataGridView1.Columns["Production_Date"].ReadOnly = true;
            dataGridView1.Columns.Add("Carton_Number", "Carton Number");
            DataGridViewComboBoxColumn dgvCmb = new DataGridViewComboBoxColumn();
            dgvCmb.HeaderText = "Grade";
            dgvCmb.DataSource = grade;
            dgvCmb.Name = "Grade";
            dataGridView1.Columns.Insert(3, dgvCmb);
            dataGridView1.Columns.Add("Gross_Weight", "Gross Weight");
            dataGridView1.Columns.Add("Carton_Weight", "Carton Weight");
            dataGridView1.Columns.Add("Number_Of_Cones", "Number of Cones");
            dataGridView1.Columns.Add("Net_Weight", "Net Weight");
            dataGridView1.Columns.Add("Comments", "Comments");
            dataGridView1.Columns[7].ReadOnly = true;
            dataGridView1.RowCount = 10;
            dataGridView1.Enabled = false;


            //Datagridview 2
            dataGridView2.Columns.Add("Sl_No", "Sl No");
            dataGridView2.Columns[0].ReadOnly = true;
            DataGridViewComboBoxColumn dgvCmb1 = new DataGridViewComboBoxColumn();
            dgvCmb1.HeaderText = "Carton Number";
            dgvCmb1.DataSource = this.destroyed_carton_nos_display;
            dgvCmb1.Name = "Carton_Number";
            dataGridView2.Columns.Insert(1, dgvCmb1);
            dataGridView2.Columns.Add("Weight", "Weight");
            dataGridView2.Columns[2].ReadOnly = true;
            dataGridView2.RowCount = 10;

            c.auto_adjust_dgv(dataGridView2);
            dataGridView2.Columns[0].Width = 50;
            dataGridView2.Columns[2].Width = 100;
            c.set_dgv_column_sort_state(this.dataGridView1, DataGridViewColumnSortMode.NotSortable);
            c.set_dgv_column_sort_state(this.dataGridView2, DataGridViewColumnSortMode.NotSortable);
        }
        public T_V2_repackingForm(DataRow row, bool isEditable, M_V_history v1_history)
        {

            InitializeComponent();
            this.dt = new DataTable();
            this.edit_form = true;
            this.v1_history = v1_history;
            this.saveButton.Enabled = false;
            this.c = new DbConnect();
            this.destroyed_carton_nos_display = new List<string>();

            //Create drop-down Colour list
            var dataSource1 = new List<string>();
            DataTable dt = c.runQuery("SELECT * FROM T_M_Colours");
            dataSource1.Add("---Select---");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dataSource1.Add(dt.Rows[i]["Colour"].ToString());
                colour_dict[dt.Rows[i]["Colour"].ToString()] = int.Parse(dt.Rows[i]["Colour_ID"].ToString());
            }
            List<string> final_list = dataSource1.Distinct().ToList();
            this.colourComboboxCB.DataSource = final_list;
            this.colourComboboxCB.DisplayMember = "Colour";
            this.colourComboboxCB.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.colourComboboxCB.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.colourComboboxCB.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            //Create drop-down Quality list
            var dataSource2 = new List<string>();
            dt = c.runQuery("SELECT * FROM T_M_Quality_Before_Job");
            dataSource2.Add("---Select---");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dataSource2.Add(dt.Rows[i]["Quality_Before_Job"].ToString());
                quality_dict[dt.Rows[i]["Quality_Before_Job"].ToString()] = int.Parse(dt.Rows[i]["Quality_Before_Job_ID"].ToString());
            }
            this.qualityComboboxCB.DataSource = dataSource2;
            this.qualityComboboxCB.DisplayMember = "Quality";
            this.qualityComboboxCB.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.qualityComboboxCB.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.qualityComboboxCB.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            //Create drop-down Company list
            var dataSource3 = new List<string>();
            dt = c.runQuery("SELECT * FROM T_M_Company_Names");
            dataSource3.Add("---Select---");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dataSource3.Add(dt.Rows[i]["Company_Name"].ToString());
                company_dict[dt.Rows[i]["Company_Name"].ToString()] = int.Parse(dt.Rows[i]["Company_ID"].ToString());
            }
            this.companyComboboxCB.DataSource = dataSource3;
            this.companyComboboxCB.DisplayMember = "Company_Names";
            this.companyComboboxCB.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.companyComboboxCB.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.companyComboboxCB.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            //Type CB
            this.typeCB.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.typeCB.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.typeCB.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            //Create drop-down Cone list
            var dataSource4 = new List<string>();
            dt = c.runQuery("SELECT * FROM T_M_Cones");
            dataSource4.Add("---Select---");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dataSource4.Add(dt.Rows[i]["Cone_Name"].ToString());
                cones_dict[dt.Rows[i]["Cone_Name"].ToString()] = new Tuple<float, int>(float.Parse(dt.Rows[i]["Cone_Weight"].ToString()), int.Parse(dt.Rows[i]["Cone_ID"].ToString()));
            }
            this.coneComboboxCB.DataSource = dataSource4;
            this.coneComboboxCB.DisplayMember = "Cones";
            this.coneComboboxCB.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.coneComboboxCB.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.coneComboboxCB.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

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


            List<string> grade = new List<string>();
            grade.Add("1st");
            grade.Add("PQ");
            grade.Add("CLQ");

            //DatagridView 1
            dataGridView1.Columns.Add("Sl_No", "Sl No");
            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns.Add("Production_Date", "Production Date");
            dataGridView1.Columns["Production_Date"].ReadOnly = true;
            dataGridView1.Columns.Add("Carton_Number", "Carton Number");
            DataGridViewComboBoxColumn dgvCmb = new DataGridViewComboBoxColumn();
            dgvCmb.HeaderText = "Grade";
            dgvCmb.DataSource = grade;
            dgvCmb.Name = "Grade";
            dataGridView1.Columns.Insert(3, dgvCmb);
            dataGridView1.Columns.Add("Gross_Weight", "Gross Weight");
            dataGridView1.Columns.Add("Carton_Weight", "Carton Weight");
            dataGridView1.Columns.Add("Number_Of_Cones", "Number of Cones");
            dataGridView1.Columns.Add("Net_Weight", "Net Weight");
            dataGridView1.Columns.Add("Comments", "Comments");
            dataGridView1.Columns[7].ReadOnly = true;
            dataGridView1.Enabled = false;

            //Datagridview 2
            dataGridView2.Columns.Add("Sl_No", "Sl No");
            dataGridView2.Columns[0].ReadOnly = true;
            DataGridViewComboBoxColumn dgvCmb1 = new DataGridViewComboBoxColumn();
            dgvCmb1.HeaderText = "Carton Number";
            dgvCmb1.DataSource = this.destroyed_carton_nos_display;
            dgvCmb1.Name = "Carton_Number";
            dataGridView2.Columns.Insert(1, dgvCmb1);
            dataGridView2.Columns.Add("Weight", "Weight");
            dataGridView2.Columns[2].ReadOnly = true;

            this.colourComboboxCB.Enabled = false;
            this.qualityComboboxCB.Enabled = false;
            this.companyComboboxCB.Enabled = false;
            this.financialYearComboboxCB.Enabled = false;
            this.typeCB.Enabled = false;
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
            this.qualityComboboxCB.SelectedIndex = this.qualityComboboxCB.FindStringExact(row["Quality_Before_Job"].ToString());
            this.companyComboboxCB.SelectedIndex = this.companyComboboxCB.FindStringExact(row["Company_Name"].ToString());
            this.financialYearComboboxCB.SelectedIndex = this.financialYearComboboxCB.FindStringExact(row["Carton_Fiscal_Year"].ToString());
            this.coneComboboxCB.SelectedIndex = this.coneComboboxCB.FindStringExact(row["Cone_Name"].ToString());
            this.typeCB.SelectedIndex = this.typeCB.FindStringExact(row["Inward_Cartons_Type"].ToString());


            if (row["Voucher_Closed"].ToString() == "0")
            {
                this.closedCheckboxCK.Checked = false;
            }
            else
            {
                this.closedCheckboxCK.Checked = true;
                this.closedCheckboxCK.Enabled = false;
                this.oilGainTextbox.Text = row["Oil_Gain"].ToString();
            }

            string sql = "SELECT temp2.*, T_M_Company_Names.Company_Name\n";
            sql += "FROM\n";
            sql += "    (SELECT temp1.*, T_M_Colours.Colour\n";
            sql += "    FROM\n";
            sql += "        (SELECT T_Repacked_Cartons.*, T_M_Quality_Before_Job.Quality_Before_Job\n";
            sql += "        FROM T_Repacked_Cartons\n";
            sql += "        LEFT OUTER JOIN T_M_Quality_Before_Job\n";
            sql += "        ON T_Repacked_Cartons.Quality_ID = T_M_Quality_Before_Job.Quality_Before_Job_ID) as temp1\n";
            sql += "    LEFT OUTER JOIN T_M_Colours\n";
            sql += "    ON temp1.Colour_ID = T_M_Colours.Colour_ID) as temp2\n";
            sql += "LEFT OUTER JOIN T_M_Company_Names\n";
            sql += "ON temp2.Company_ID = T_M_Company_Names.Company_ID\n";
            sql += "WHERE temp2.Repacking_Voucher_ID = " + this.voucher_id + "\n";
            sql += "ORDER BY temp2.ID ASC\n";
            DataTable repacking_carton_data = c.runQuery(sql);
            dataGridView1.RowCount = repacking_carton_data.Rows.Count + 1;
            
            for (int i = 0; i < repacking_carton_data.Rows.Count; i++)
            {
                old_repacked_carton_dict.Add(repacking_carton_data.Rows[i]["Carton_No"].ToString(), new Tuple<DataRow, bool>(repacking_carton_data.Rows[i], false));
            }
            bool flag = false;
            for (int i = 0; i < repacking_carton_data.Rows.Count; i++)
            {
                string repacking_carton_id = repacking_carton_data.Rows[i]["Carton_ID"].ToString();
                string repacking_carton_no = repacking_carton_data.Rows[i]["Carton_No"].ToString();
                DataRow carton_row = old_repacked_carton_dict[repacking_carton_no].Item1;
                if (carton_row == null)
                {
                    continue;
                }
                string correctformat = Convert.ToDateTime(carton_row["Date_Of_Production"].ToString()).Date.ToString().Substring(0, 10);
                dataGridView1.Rows[i].Cells[1].Value = correctformat;
                dataGridView1.Rows[i].Cells[2].Value = carton_row["Carton_No"].ToString();
                dataGridView1.Rows[i].Cells[3].Value = carton_row["Grade"].ToString();
                dataGridView1.Rows[i].Cells[4].Value = carton_row["Gross_Weight"].ToString();
                dataGridView1.Rows[i].Cells[5].Value = carton_row["Carton_Weight"].ToString();
                dataGridView1.Rows[i].Cells[6].Value = carton_row["Number_Of_Cones"].ToString();
                dataGridView1.Rows[i].Cells[7].Value = carton_row["Net_Weight"].ToString();
                dataGridView1.Rows[i].Cells[8].Value = carton_row["Repack_Comments"].ToString();

                //Sold carton will be coloured green
                if (carton_row["Carton_State"].ToString() == "1")
                {
                    flag = true;
                    this.carton_editable[repacking_carton_id] = false;
                    DataGridViewRow r = (DataGridViewRow)dataGridView1.Rows[i];
                    dataGridView1.Rows[i].ReadOnly = true;
                    r.DefaultCellStyle.BackColor = Color.LightGreen;
                    r.DefaultCellStyle.SelectionBackColor = Color.LightGreen;
                    this.deleteButton.Enabled = false;
                    this.label16.Text = "This voucher cannot be deleted as some cartons have been sold\nPartially Sold: Yellow    Fully Sold: Light Green";
                }
                else if (carton_row["Carton_State"].ToString() == "2")
                {
                    flag = true;
                    this.carton_editable[repacking_carton_id] = false;
                    DataGridViewRow r = (DataGridViewRow)dataGridView1.Rows[i];
                    dataGridView1.Rows[i].ReadOnly = true;
                    r.DefaultCellStyle.BackColor = Color.Yellow;
                    r.DefaultCellStyle.SelectionBackColor = Color.DarkGoldenrod;
                    this.deleteButton.Enabled = false;
                    this.label16.Text = "This voucher cannot be deleted as some cartons have been sold\nPartially Sold: Yellow    Fully Sold: Light Green";
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
            this.cartonweight.Text = CellSum1(7).ToString("F3");

            //Adding Destroyed Cartons to datagridview 2
            sql = "";
            sql = "SELECT* FROM\n";
            sql += "    (SELECT T_Inward_Carton.*\n";
            sql += "    FROM T_Inward_Carton\n";
            sql += "    LEFT OUTER JOIN T_Carton_Inward_Voucher\n";
            sql += "    ON T_Inward_Carton.Inward_Voucher_ID = T_Carton_Inward_Voucher.Voucher_ID) as temp\n";
            sql += "WHERE temp.Quality_ID = " + quality_dict[qualityComboboxCB.SelectedItem.ToString()] + " AND temp.Colour_ID = " + colour_dict[colourComboboxCB.SelectedItem.ToString()] + " AND temp.Company_ID = " + company_dict[companyComboboxCB.SelectedItem.ToString()] + " AND temp.Inward_Type = " + typeCB.SelectedItem.ToString() + " AND Carton_State = 1 AND temp.Repacking_Voucher_ID = " + this.voucher_id + " ORDER BY Repacking_Display_Order ASC\n";
            DataTable destroyed_cartons = c.runQuery(sql);
            for (int i = 0; i < destroyed_cartons.Rows.Count; i++)
            {
                this.old_destroyed_carton_dict.Add(destroyed_cartons.Rows[i]["Carton_ID"].ToString(), new Tuple<DataRow, bool>(destroyed_cartons.Rows[i], false));
                this.destroyed_carton_dict.Add(destroyed_cartons.Rows[i]["Carton_ID"].ToString(), destroyed_cartons.Rows[i]);
                this.destroyed_carton_nos_display.Add(destroyed_cartons.Rows[i]["Carton_No"].ToString() + "  (" + destroyed_cartons.Rows[i]["Net_Weight"].ToString() + ", " + destroyed_cartons.Rows[i]["Fiscal_Year"].ToString() + ")");
                Tuple<string, string, string> temp = new Tuple<string, string, string>(destroyed_cartons.Rows[i]["Carton_No"].ToString(), destroyed_cartons.Rows[i]["Net_Weight"].ToString(), destroyed_cartons.Rows[i]["Fiscal_Year"].ToString());
                this.destroyed_carton_id_get.Add(temp, destroyed_cartons.Rows[i]["Carton_ID"].ToString());
            }

            string today_fiscal_year = c.getFinancialYear(DateTime.Now);
            List<int> minmax_years = c.getFinancialYearArr(this.financialYearComboboxCB.Text);
            this.loadData(today_fiscal_year, minmax_years);

            dgvCmb1.DataSource = this.destroyed_carton_nos_display;
            dataGridView2.RowCount = destroyed_cartons.Rows.Count + 1;
            for (int i = 0; i < destroyed_cartons.Rows.Count; i++)
            {
                string destroyed_carton_id = destroyed_cartons.Rows[i]["Carton_ID"].ToString();
                DataRow destroyed_carton_row = destroyed_carton_dict[destroyed_carton_id];
                dataGridView2.Rows[i].Cells[1].Value = this.destroyed_carton_nos_display[i];
                dataGridView2.Rows[i].Cells[2].Value = destroyed_carton_row["Net_Weight"].ToString();
            }
            this.inwardcartonnwtTextbox.Text = this.CellSum2(2).ToString("F3");

            //highest carton number;
            this.highest_carton_no = int.Parse(c.getNextNumber_FiscalYear("Highest_Repacking_Carton_No", this.financialYearComboboxCB.Text));
            Console.WriteLine("Constructor: " + this.highest_carton_no.ToString());
            if (isEditable == true) this.dataGridView1.Rows.Add("", "", this.highest_carton_no);
            this.nextcartonnoTB.Text = this.highest_carton_no.ToString();
            
            c.auto_adjust_dgv(dataGridView2);
            dataGridView2.Columns[0].Width = 50;
            dataGridView2.Columns[2].Width = 100;
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
            Console.WriteLine("The value is " + dtp.Value.Date.ToString().Substring(0, 10));
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
            this.companyComboboxCB.Enabled = false;
            this.colourComboboxCB.Enabled = false;
            this.financialYearComboboxCB.Enabled = false;
            this.coneComboboxCB.Enabled = false;
            this.closedCheckboxCK.Enabled = false;
            this.narrationTB.ReadOnly = true;
        }
        private bool loadData(string today_fiscal_year, List<int> minmax_years)
        {
            string sql = "SELECT * FROM\n";
            sql += "    (SELECT T_Inward_Carton.* \n";
            sql += "    FROM T_Inward_Carton\n";
            sql += "    LEFT OUTER JOIN T_Carton_Inward_Voucher\n";
            sql += "    ON T_Inward_Carton.Inward_Voucher_ID = T_Carton_Inward_Voucher.Voucher_ID) as temp \n";
            sql += "WHERE temp.Quality_ID = " + quality_dict[qualityComboboxCB.SelectedItem.ToString()] + " AND temp.Colour_ID = " + colour_dict[colourComboboxCB.SelectedItem.ToString()] + " \n";
            sql += "AND temp.Company_ID = " + company_dict[companyComboboxCB.SelectedItem.ToString()] + " AND temp.Inward_Type = " + typeCB.SelectedItem.ToString() + " AND Carton_State=0\n";
            this.dt = c.runQuery(sql);
            if (dt == null) return false;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                this.destroyed_carton_dict.Add(dt.Rows[i]["Carton_ID"].ToString(), dt.Rows[i]);
                this.destroyed_carton_nos_display.Add(dt.Rows[i]["Carton_No"].ToString() + "  (" + dt.Rows[i]["Net_Weight"].ToString() + ", " + dt.Rows[i]["Fiscal_Year"].ToString() + ")");
                Tuple<string, string, string> temp = new Tuple<string, string, string>(dt.Rows[i]["Carton_No"].ToString(), dt.Rows[i]["Net_Weight"].ToString(), dt.Rows[i]["Fiscal_Year"].ToString());
                this.destroyed_carton_id_get.Add(temp, dt.Rows[i]["Carton_ID"].ToString());
            }
            if (dt.Rows.Count <= 0 && this.edit_form == false)
            {
                c.WarningBox("No Cartons Found");
                return false;
            }
            if (this.edit_form == false)
            {
                c.SuccessBox("Loaded " + dt.Rows.Count.ToString() + " Cartons");
            }

            this.dtp.MinDate = new DateTime(minmax_years[0], 04, 01);
            if (today_fiscal_year == this.financialYearComboboxCB.Text)
            {
                this.dtp.MaxDate = this.inputDate.Value;
                if (this.edit_form == true)
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
            float net_weight, batch_weight;
            Console.WriteLine(inwardcartonnwtTextbox.Text);
            try
            {
                batch_weight = float.Parse(inwardcartonnwtTextbox.Text);
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
            if (net_weight - batch_weight < 0F)
            {
                oilGainTextbox.Text = "Net Carton Wt < Net Batch Wt";
                return;
            }
            oilGainTextbox.Text = ((net_weight - batch_weight) / batch_weight * 100F).ToString("F2");
        }
        public void calculate_net_wt(int row_index)
        {
            //null values handle
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

            //no cone selected
            if (coneComboboxCB.SelectedIndex == 0)
            {
                dataGridView1.Rows[row_index].Cells[7].Value = "Please select Cone Wt";
                return;
            }
            float net_weight = 0F;
            try
            {
                float gross_weight = float.Parse(dataGridView1.Rows[row_index].Cells["Gross_Weight"].Value.ToString());
                float carton_weight = float.Parse(dataGridView1.Rows[row_index].Cells["Carton_Weight"].Value.ToString());
                float total_cone_weight = int.Parse(dataGridView1.Rows[row_index].Cells["Number_Of_Cones"].Value.ToString()) * float.Parse(coneWeightTB.Text) * 0.001F;
                net_weight = (gross_weight - carton_weight - total_cone_weight);
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
            this.inwardcartonnwtTextbox.Text = CellSum2(2).ToString("F3");
            //checks
            if (coneComboboxCB.SelectedIndex == 0)
            {
                c.ErrorBox("Select Cone Weight", "Error");
                return;
            }
            if (dataGridView1.Rows[0].Cells[1].Value == null)
            {
                c.ErrorBox("Please enter Repacking Cartons", "Error");
                return;
            }
            try
            {
                float.Parse(cartonweight.Text);
                float.Parse(inwardcartonnwtTextbox.Text);
            }
            catch
            {
                c.ErrorBox("Please enter carton numbers");
                return;
            }
            if ((float.Parse(cartonweight.Text) - float.Parse(inwardcartonnwtTextbox.Text) < 0F) && closedCheckboxCK.Checked == true)
            {
                c.ErrorBox("Net Repacked Carton Weight should be greater than or equal to Net Cartons to be repacked Weight", "Error");
                return;
            }
            if (coneComboboxCB.Text != c.getDefault("Default", "Cone"))
            {
                DialogResult dialogResult = MessageBox.Show("You have selected " + coneComboboxCB.Text + "g as cone weight!", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Cancel)
                {
                    return;
                }

            }

            int number = 0;

            List<string> temp = new List<string>();
            bool empty = true;
            for (int i = 0; i < dataGridView2.Rows.Count - 1; i++)
            {
                if (c.Cell_Not_NullOrEmpty(this.dataGridView2, i, 1))
                {
                    empty = false;
                    break;
                }
            }
            if (empty)
            {
                c.ErrorBox("Enter atleast one Carton to be Repacked");
                return;
            }

            //check for data integrity
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                int sum = 0;
                for (int j = 1; j <= 7; j++)
                {
                    if (dataGridView1.Rows[i].Cells[j].Value == null)
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
                if (sum == 0)
                {
                    //all values missing: continue
                    continue;
                }
                else if (sum != 7)
                {
                    //few values missing
                    c.ErrorBox("Missing values in " + (i + 1).ToString() + " row", "Error");
                    return;
                }
                //ComboBox cbox = (ComboBox)dataGridView1.EditingControl;
                if (dataGridView1.Rows[i].Cells[1].Value == null || dataGridView1.Rows[i].Cells[1].Value.ToString() == "")
                {
                    continue;
                }
                else
                {
                    number++;

                    //to check for all different Carton
                    temp.Add(dataGridView1.Rows[i].Cells[2].Value.ToString());
                    var distinctBytes = new HashSet<string>(temp);
                    bool allDifferent = distinctBytes.Count == temp.Count;
                    if (allDifferent == false)
                    {
                        c.ErrorBox("Repacked Carton Nos repeated at Row: " + (i + 1).ToString(), "Error");
                        return;
                    }
                }
            }

            int closed;
            if (closedCheckboxCK.Checked == true)
            {
                closed = 1;
            }
            else
            {
                closed = 0;
            }
            if (this.edit_form == false)
            {
                string fiscal_year = c.getFinancialYear(inputDate.Value);

                //Store carton number and respective fiscal years in batches list and get min carton inward date
                DateTime min_billing_date = DateTime.MaxValue;
                string Date_Of_Billing = "";
                int inward_index = -1;
                for (int i = 0; i < dataGridView2.Rows.Count; i++)
                {
                    if (c.Cell_Not_NullOrEmpty(dataGridView2, i, 1) == false) continue;
                    Console.WriteLine("Carton Inward: "+ dataGridView2.Rows[i].Cells[1].Value.ToString());
                    string carton_id = destroyed_carton_id_get[Global.getCartonNo_Weight_FiscalYear(dataGridView2.Rows[i].Cells[1].Value.ToString())];
                    string sql_temp = "SELECT T_Carton_Inward_Voucher.Date_Of_Billing FROM T_Inward_Carton\n";
                    sql_temp += "JOIN T_Carton_Inward_Voucher ON T_Carton_Inward_Voucher.Voucher_ID = T_Inward_Carton.Inward_Voucher_ID WHERE T_Inward_Carton.Carton_ID = '" + carton_id + "'";
                    Date_Of_Billing = c.runQuery(sql_temp).Rows[0]["Date_Of_Billing"].ToString().Substring(0, 10);
                    Date_Of_Billing = Date_Of_Billing.Replace('/', '-');
                    Console.WriteLine(Date_Of_Billing);
                    DateTime date_of_billing = DateTime.ParseExact(Date_Of_Billing, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                    if (date_of_billing < min_billing_date)
                    {
                        min_billing_date = date_of_billing;
                        inward_index = i;
                    }
                }

                //Check if future entry is not being done and get highest carton number
                int max_carton_no = int.MinValue;
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (c.Cell_Not_NullOrEmpty(dataGridView1, i, -1, "Carton_Number") == false) continue;
                    Console.WriteLine("Production Date: " + dataGridView1.Rows[i].Cells["Production_Date"].Value.ToString().Replace('/', '-'));
                    DateTime prod = DateTime.ParseExact(dataGridView1.Rows[i].Cells["Production_Date"].Value.ToString().Replace('/','-'), "dd-MM-yyyy", CultureInfo.InvariantCulture);
                    if (inputDate.Value < prod)
                    {
                        c.ErrorBox("Carton Number: " + dataGridView1.Rows[i].Cells[2].Value.ToString() + " at row " + (i + 1).ToString() + " has Date of Production (" + prod.Date.ToString("dd-MM-yyyy") + ") in the future", "Error");
                        return;
                    }
                    int carton_no;
                    if (int.TryParse(dataGridView1.Rows[i].Cells[2].Value.ToString(), out carton_no))
                    {
                        if (carton_no > max_carton_no)
                        {
                            max_carton_no = carton_no;
                        }
                    }
                }

                //Get Max and min carton production dates
                DateTime max_prod_date = DateTime.ParseExact(dataGridView1.Rows[0].Cells["Production_Date"].Value.ToString().Replace('/','-'), "dd-MM-yyyy", CultureInfo.InvariantCulture);
                DateTime min_prod_date = DateTime.ParseExact(dataGridView1.Rows[0].Cells["Production_Date"].Value.ToString().Replace('/', '-'), "dd-MM-yyyy", CultureInfo.InvariantCulture);
                int min_index = 0;
                for (int i = 1; i < dataGridView1.Rows.Count; i++)
                {
                    if (c.Cell_Not_NullOrEmpty(dataGridView1, i, -1, "Carton_Number") == false) continue;
                    DateTime dttemp = DateTime.ParseExact(dataGridView1.Rows[i].Cells["Production_Date"].Value.ToString().Replace('/', '-'), "dd-MM-yyyy", CultureInfo.InvariantCulture); ;
                    if (dttemp > max_prod_date)
                    {
                        max_prod_date = dttemp;
                    }
                    if (dttemp < min_prod_date)
                    {
                        min_prod_date = dttemp;
                        min_index = i;
                    }
                }

                //Check if min billing date <= min production date
                if (min_billing_date > min_prod_date)
                {
                    c.ErrorBox("Carton Number " + dataGridView1.Rows[min_index].Cells[2].Value.ToString() + " at row " + (min_index + 1).ToString() + " has Date of Production (" + min_prod_date.Date.ToString("dd-MM-yyyy").Substring(0, 10) + ") less than Date of Billing (" + Date_Of_Billing + ") of Inward Carton " + dataGridView2.Rows[inward_index].Cells[1].Value.ToString() + ")");
                    return;
                }
                
                string min_date = min_prod_date.Date.ToString("yyyy-MM-dd").Substring(0, 10);
                string sql = "begin transaction; begin try; DECLARE @voucherID int;\n";
                if (closed == 1)
                {
                    float oil_gain = (float.Parse(cartonweight.Text) - float.Parse(inwardcartonnwtTextbox.Text)) / float.Parse(inwardcartonnwtTextbox.Text) * 100F;
                    string max_date = max_prod_date.Date.ToString("yyyy-MM-dd").Substring(0, 10);
                    sql += "INSERT INTO T_Repacking_Voucher (Date_Of_Input, Colour_ID, Quality_ID, Company_ID, Inward_Cartons_Type, Voucher_Closed, Oil_Gain, Carton_Fiscal_Year, Cone_ID, Date_Of_Production, Start_Date_Of_Production, Narration, Fiscal_Year) VALUES ('" + inputDate.Value.ToString("MM-dd-yyyy") + "'," + colour_dict[colourComboboxCB.SelectedItem.ToString()] + ", " + quality_dict[qualityComboboxCB.SelectedItem.ToString()] + ", " + company_dict[companyComboboxCB.SelectedItem.ToString()] + ", " + typeCB.SelectedItem.ToString() + " , " + closed + ", " + oil_gain + ", '" + fiscal_year + "', " + cones_dict[coneComboboxCB.SelectedItem.ToString()].Item2 + ", '" + max_date + "', '" + min_date + "', '" + narrationTB.Text + "', '" + c.getFinancialYear(inputDate.Value) + "'); SELECT @voucherID = SCOPE_IDENTITY();\n";
                }
                else
                {
                    sql += "INSERT INTO T_Repacking_Voucher (Date_Of_Input, Colour_ID, Quality_ID, Company_ID, Inward_Cartons_Type, Voucher_Closed, Carton_Fiscal_Year, Cone_ID, Start_Date_Of_Production, Narration, Fiscal_Year) VALUES ('" + inputDate.Value.ToString("MM-dd-yyyy") + "'," + colour_dict[colourComboboxCB.SelectedItem.ToString()] + ", " + quality_dict[qualityComboboxCB.SelectedItem.ToString()] + ", " + company_dict[companyComboboxCB.SelectedItem.ToString()] + ", " + typeCB.SelectedItem.ToString() + " , " + closed + ", '" + fiscal_year + "', " + cones_dict[coneComboboxCB.SelectedItem.ToString()].Item2 + ", '" + min_date + "', '" + narrationTB.Text + "', '" + c.getFinancialYear(inputDate.Value) + "'); SELECT @voucherID = SCOPE_IDENTITY();\n";
                }

                //Enter into carton produced table
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (c.Cell_Not_NullOrEmpty(dataGridView1, i, -1, "Carton_Number") == false) continue;
                    DateTime dttemp = DateTime.ParseExact(dataGridView1.Rows[i].Cells["Production_Date"].Value.ToString().Replace('/','-'), "dd-MM-yyyy", CultureInfo.InvariantCulture);
                    string correct_format_date = dttemp.Date.ToString("yyyy-MM-dd").Substring(0, 10);
                    string carton_no = dataGridView1.Rows[i].Cells["Carton_Number"].Value.ToString();
                    float cartonWeight = float.Parse(dataGridView1.Rows[i].Cells["Carton_Weight"].Value.ToString());
                    int numberOfCones = int.Parse(dataGridView1.Rows[i].Cells["Number_Of_Cones"].Value.ToString());
                    float grossWeight = float.Parse(dataGridView1.Rows[i].Cells["Gross_Weight"].Value.ToString());
                    float netWeight = float.Parse(dataGridView1.Rows[i].Cells["Net_Weight"].Value.ToString());
                    string grade = dataGridView1.Rows[i].Cells["Grade"].Value.ToString();
                    string comments = "";
                    if(c.Cell_Not_NullOrEmpty(dataGridView1, i, -1, "Comments")) comments = dataGridView1.Rows[i].Cells["Comments"].Value.ToString();
                    sql += "INSERT INTO T_Repacked_Cartons (Carton_No, Carton_State, Date_Of_Production, Quality_ID, Colour_ID, Company_ID, Carton_Weight, Number_Of_Cones, Gross_Weight, Net_Weight, Fiscal_Year, Grade, Repacking_Voucher_ID, Inward_Type, Repack_Comments) VALUES ('" + carton_no + "' ," + 0 + ", '" + correct_format_date + "', " + quality_dict[qualityComboboxCB.SelectedItem.ToString()] + ", " + colour_dict[colourComboboxCB.SelectedItem.ToString()] + ", " + company_dict[companyComboboxCB.SelectedItem.ToString()] + ", " + cartonWeight + ", " + numberOfCones + ", " + grossWeight + ", " + netWeight + ", '" + financialYearComboboxCB.SelectedItem.ToString() + "', '" + grade + "', @voucherID, " + typeCB.SelectedItem.ToString() + ", '" + comments + "'); \n";
                }

                //Send Inward Cartons State and Repacking Voucher ID and Display Order
                for (int i = 0; i < dataGridView2.Rows.Count; i++)
                {
                    if (c.Cell_Not_NullOrEmpty(dataGridView2, i, 1) == false) continue;
                    Tuple<string, string, string> temp_tup = Global.getCartonNo_Weight_FiscalYear(dataGridView2.Rows[i].Cells[1].Value.ToString());
                    sql += "UPDATE T_Inward_Carton SET Carton_State = 1, Repacking_Voucher_ID = @voucherID, Repacking_Display_Order = " + (i + 1).ToString() + " WHERE Carton_ID = '" + destroyed_carton_id_get[temp_tup] + "'; \n";
                }

                //Update Fiscal Year Table
                if (max_carton_no != int.MinValue)  //Check if highest carton number is a number
                {
                    sql += "DECLARE @highest_carton_no varchar(20); SELECT @highest_carton_no = Highest_Repacking_Carton_No FROM Fiscal_Year WHERE Fiscal_Year='" + financialYearComboboxCB.SelectedItem.ToString() + "'; \n";
                    sql += "IF @highest_carton_no < " + max_carton_no + "\n";
                    sql += "UPDATE Fiscal_Year SET Highest_Repacking_Carton_No =" + max_carton_no + " WHERE Fiscal_Year='" + financialYearComboboxCB.SelectedItem.ToString() + "'; \n";
                }
                //catch
                sql += "commit transaction; end try BEGIN CATCH rollback transaction; \n";
                sql += "DECLARE @ErrorMessage NVARCHAR(4000); DECLARE @ErrorSeverity INT; DECLARE @ErrorState INT; SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE(); \n";
                sql += "RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState); END CATCH; \n";
                DataTable add = c.runQuery(sql);   

                if (add != null)
                {
                    dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.LawnGreen;
                    c.SuccessBox("Voucher Added Successfully");
                    disable_form_edit();
                }
                else return;
            }
            else
            {
                string fiscal_year = c.getFinancialYear(inputDate.Value);

                //get min carton inward (billing) date
                DateTime min_billing_date = DateTime.MaxValue;
                string Date_Of_Billing = "";
                int inward_index = -1;
                for (int i = 0; i < dataGridView2.Rows.Count; i++)
                {
                    if (c.Cell_Not_NullOrEmpty(dataGridView2, i, 1) == false) continue;
                    Console.WriteLine("Carton Inward: " + dataGridView2.Rows[i].Cells[1].Value.ToString());
                    string carton_id = destroyed_carton_id_get[Global.getCartonNo_Weight_FiscalYear(dataGridView2.Rows[i].Cells[1].Value.ToString())];
                    string sql_temp = "SELECT T_Carton_Inward_Voucher.Date_Of_Billing FROM T_Inward_Carton\n";
                    sql_temp += "JOIN T_Carton_Inward_Voucher ON T_Carton_Inward_Voucher.Voucher_ID = T_Inward_Carton.Inward_Voucher_ID WHERE T_Inward_Carton.Carton_ID = '" + carton_id + "'";
                    Date_Of_Billing = c.runQuery(sql_temp).Rows[0]["Date_Of_Billing"].ToString().Substring(0, 10);
                    Date_Of_Billing = Date_Of_Billing.Replace('/', '-');
                    Console.WriteLine(Date_Of_Billing);
                    DateTime date_of_billing = DateTime.ParseExact(Date_Of_Billing, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                    if (date_of_billing < min_billing_date)
                    {
                        min_billing_date = date_of_billing;
                        inward_index = i;
                    }
                }

                //Check if future entry is not being done and get highest carton number
                int max_carton_no = int.MinValue;
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (c.Cell_Not_NullOrEmpty(dataGridView1, i, -1, "Carton_Number") == false) continue;
                    Console.WriteLine("Production Date: " + dataGridView1.Rows[i].Cells["Production_Date"].Value.ToString().Replace('/', '-'));
                    DateTime prod = DateTime.ParseExact(dataGridView1.Rows[i].Cells["Production_Date"].Value.ToString().Replace('/', '-'), "dd-MM-yyyy", CultureInfo.InvariantCulture);
                    if (inputDate.Value > prod)
                    {
                        c.ErrorBox("Carton Number: " + dataGridView1.Rows[i].Cells[2].Value.ToString() + " at row " + (i + 1).ToString() + " has Date of Production (" + prod.Date.ToString("dd-MM-yyyy") + " in the future", "Error");
                        return;
                    }
                    int carton_no;
                    if (int.TryParse(dataGridView1.Rows[i].Cells[2].Value.ToString(), out carton_no))
                    {
                        if (carton_no > max_carton_no)
                        {
                            max_carton_no = carton_no;
                        }
                    }
                }

                //Get Max and min carton production dates
                DateTime max_prod_date = DateTime.ParseExact(dataGridView1.Rows[0].Cells["Production_Date"].Value.ToString().Replace('/', '-'), "dd-MM-yyyy", CultureInfo.InvariantCulture);
                DateTime min_prod_date = DateTime.ParseExact(dataGridView1.Rows[0].Cells["Production_Date"].Value.ToString().Replace('/', '-'), "dd-MM-yyyy", CultureInfo.InvariantCulture);
                int min_index = 0;
                for (int i = 1; i < dataGridView1.Rows.Count; i++)
                {
                    if (c.Cell_Not_NullOrEmpty(dataGridView1, i, -1, "Carton_Number") == false) continue;
                    DateTime dttemp = DateTime.ParseExact(dataGridView1.Rows[i].Cells["Production_Date"].Value.ToString().Replace('/', '-'), "dd-MM-yyyy", CultureInfo.InvariantCulture); ;
                    if (dttemp > max_prod_date)
                    {
                        max_prod_date = dttemp;
                    }
                    if (dttemp < min_prod_date)
                    {
                        min_prod_date = dttemp;
                        min_index = i;
                    }
                }

                //Check if min billing date <= min production date
                if (min_billing_date > min_prod_date)
                {
                    c.ErrorBox("Carton Number " + dataGridView1.Rows[min_index].Cells[2].Value.ToString() + " at row " + (min_index + 1).ToString() + " has Date of Production (" + min_prod_date.Date.ToString("dd-MM-yyyy").Substring(0, 10) + ") less than Date of Billing (" + Date_Of_Billing + ") of Inward Carton " + dataGridView2.Rows[inward_index].Cells[1].Value.ToString() + ")");
                    return;
                }

                string start_date_of_prod = min_prod_date.Date.ToString("yyyy-MM-dd").Substring(0, 10);

                //We will make 3 lists for repacked cartons: 1) Old cartons which are there after editing too. 2) Old cartons which are deleted in edited voucher. 3) New cartons 
                List<Tuple<string, int>> repacked_list1 = new List<Tuple<string, int>>(), repacked_list3 = new List<Tuple<string, int>>();    //<carton number, row index>
                List<string> repacked_list2 = new List<string>();    //Stores carton no
                for(int i=0;i<dataGridView1.Rows.Count;i++)
                {
                    if (c.Cell_Not_NullOrEmpty(dataGridView1, i, -1, "Carton_Number") == false) continue;
                    string carton_no = dataGridView1.Rows[i].Cells["Carton_Number"].Value.ToString();
                    if (old_repacked_carton_dict.ContainsKey(carton_no))
                    {
                        repacked_list1.Add(new Tuple<string, int>(carton_no, i));
                        DataRow temp_row = old_repacked_carton_dict[carton_no].Item1;
                        Tuple<DataRow, bool> temp_tup = new Tuple<DataRow, bool>(temp_row, true);
                        old_repacked_carton_dict[carton_no] = temp_tup;
                    }
                    else
                    {
                        repacked_list3.Add(new Tuple<string, int>(carton_no, i));
                    }
                }
                foreach (KeyValuePair <string, Tuple<DataRow, bool>> kvp in old_repacked_carton_dict)
                {
                    if(kvp.Value.Item2==false)
                    {
                        repacked_list2.Add(kvp.Key);
                    }
                }

                string sql = "--********************Edit T_Repacking_Voucher**************************\n";
                sql += "begin transaction; begin try; \n";
                string carton_fiscal_year = financialYearComboboxCB.SelectedItem.ToString();
                //Remove cartons with which were deleted in edited voucher (repacked_list2)
                for (int i=0;i<repacked_list2.Count;i++)
                {
                    sql += "DELETE FROM T_Repacked_Cartons WHERE Carton_No = '" + repacked_list2[i] + "' AND Fiscal_Year = '" + carton_fiscal_year + "'; \n";
                }

                //Add all New Cartons with state 0 (repacked_list3)
                for(int j=0;j<repacked_list3.Count;j++)
                {
                    int i = repacked_list3[j].Item2;
                    DateTime dttemp = DateTime.ParseExact(dataGridView1.Rows[i].Cells["Production_Date"].Value.ToString().Replace('/', '-'), "dd-MM-yyyy", CultureInfo.InvariantCulture);
                    string correct_format_date = dttemp.Date.ToString("yyyy-MM-dd").Substring(0, 10);
                    string carton_no = dataGridView1.Rows[i].Cells["Carton_Number"].Value.ToString();
                    float cartonWeight = float.Parse(dataGridView1.Rows[i].Cells["Carton_Weight"].Value.ToString());
                    int numberOfCones = int.Parse(dataGridView1.Rows[i].Cells["Number_Of_Cones"].Value.ToString());
                    float grossWeight = float.Parse(dataGridView1.Rows[i].Cells["Gross_Weight"].Value.ToString());
                    float netWeight = float.Parse(dataGridView1.Rows[i].Cells["Net_Weight"].Value.ToString());
                    string grade = dataGridView1.Rows[i].Cells["Grade"].Value.ToString();
                    string comments = "";
                    if (c.Cell_Not_NullOrEmpty(dataGridView1, i, -1, "Comments")) comments = dataGridView1.Rows[i].Cells["Comments"].Value.ToString();
                    sql += "INSERT INTO T_Repacked_Cartons (Carton_No, Carton_State, Date_Of_Production, Quality_ID, Colour_ID, Company_ID, Carton_Weight, Number_Of_Cones, Gross_Weight, Net_Weight, Fiscal_Year, Grade, Repacking_Voucher_ID, Inward_Type, Repack_Comments) VALUES ('" + carton_no + "' ," + 0 + ", '" + correct_format_date + "', " + quality_dict[qualityComboboxCB.SelectedItem.ToString()] + ", " + colour_dict[colourComboboxCB.SelectedItem.ToString()] + ", " + company_dict[companyComboboxCB.SelectedItem.ToString()] + ", " + cartonWeight + ", " + numberOfCones + ", " + grossWeight + ", " + netWeight + ", '" + financialYearComboboxCB.SelectedItem.ToString() + "', '" + grade + "', " + this.voucher_id + ", " + typeCB.SelectedItem.ToString() + ", '" + comments + "'); \n";
                }

                //Update old cartons which are were there in old voucher as well
                {
                    for (int j = 0; j < repacked_list1.Count; j++)
                    {
                        int i = repacked_list1[j].Item2;
                        DateTime dttemp = DateTime.ParseExact(dataGridView1.Rows[i].Cells["Production_Date"].Value.ToString().Replace('/', '-'), "dd-MM-yyyy", CultureInfo.InvariantCulture);
                        string correct_format_date = dttemp.Date.ToString("yyyy-MM-dd").Substring(0, 10);
                        string carton_no = dataGridView1.Rows[i].Cells["Carton_Number"].Value.ToString();
                        float cartonWeight = float.Parse(dataGridView1.Rows[i].Cells["Carton_Weight"].Value.ToString());
                        int numberOfCones = int.Parse(dataGridView1.Rows[i].Cells["Number_Of_Cones"].Value.ToString());
                        float grossWeight = float.Parse(dataGridView1.Rows[i].Cells["Gross_Weight"].Value.ToString());
                        float netWeight = float.Parse(dataGridView1.Rows[i].Cells["Net_Weight"].Value.ToString());
                        string grade = dataGridView1.Rows[i].Cells["Grade"].Value.ToString();
                        string comments = "";
                        if (c.Cell_Not_NullOrEmpty(dataGridView1, i, -1, "Comments")) comments = dataGridView1.Rows[i].Cells["Comments"].Value.ToString();
                        sql += "UPDATE T_Repacked_Cartons SET Carton_No = '" + carton_no + "', Date_Of_Production = '" + correct_format_date + "', Carton_Weight = " + cartonWeight + ", Number_Of_Cones = " + numberOfCones + ", Gross_Weight = " + grossWeight + ", Net_Weight = " + netWeight + ", Grade = '" + grade + "', Repack_Comments = '" + comments + "' WHERE Carton_No = '" + repacked_list1[j].Item1 + "' AND Fiscal_Year = '" + carton_fiscal_year + "'; \n";
                    }
                }

                //We will make 2 lists for destroyed cartons: 1) Cartons which are there after editing too. 2) Old cartons which are deleted in edited voucher
                List<Tuple<string, int>> destroyed_list1 = new List<Tuple<string, int>>();    //<carton id, row index (used for display order>
                List<string> destroyed_list2 = new List<string>();    //Stores carton id
                for (int i = 0; i < dataGridView2.Rows.Count; i++)
                {
                    if (c.Cell_Not_NullOrEmpty(dataGridView2, i, -1, "Carton_Number") == false) continue;
                    string carton_no_display = dataGridView2.Rows[i].Cells["Carton_Number"].Value.ToString();
                    Tuple<string, string, string> temp_3_tup = Global.getCartonNo_Weight_FiscalYear(carton_no_display);
                    string carton_id = destroyed_carton_id_get[temp_3_tup];
                    if (old_destroyed_carton_dict.ContainsKey(carton_id))
                    {
                        destroyed_list1.Add(new Tuple<string, int>(carton_id, i));
                        DataRow temp_row = old_destroyed_carton_dict[carton_id].Item1;
                        Tuple<DataRow, bool> temp_tup = new Tuple<DataRow, bool>(temp_row, true);
                        old_destroyed_carton_dict[carton_id] = temp_tup;
                    }
                    else
                    {
                        destroyed_list1.Add(new Tuple<string, int>(carton_id, i));
                    }
                }
                foreach (KeyValuePair<string, Tuple<DataRow, bool>> kvp in old_destroyed_carton_dict)
                {
                    if (kvp.Value.Item2 == false)
                    {
                        destroyed_list2.Add(kvp.Key);
                    }
                }

                //Have to add the delete, add and edit destroyed cartons
                //Remove destroyed cartons which were deleted in edited voucher (destroyed_list2)
                for (int i = 0; i < destroyed_list2.Count; i++)
                {
                    sql += "UPDATE T_Inward_Carton SET Carton_State = 0, Repacking_Voucher_ID = NULL, Repacking_Display_Order = NULL  WHERE Carton_ID = '" + destroyed_list2[i] + "'; \n";
                }

                //Send cartons which are still present Carton_State=1, Repacking Voucher ID and Repacking Display Order
                for (int i = 0; i < destroyed_list1.Count; i++)
                {
                    sql += "UPDATE T_Inward_Carton SET Carton_State = 1, Repacking_Voucher_ID = " + this.voucher_id + ", Repacking_Display_Order = " + destroyed_list1[i].Item2 + " WHERE Carton_ID = '" + destroyed_list1[i].Item1 + "'; \n";
                }

                if (closed == 1)
                {
                    float oil_gain = (float.Parse(cartonweight.Text) - float.Parse(inwardcartonnwtTextbox.Text)) / float.Parse(inwardcartonnwtTextbox.Text) * 100F;
                    string max_date = max_prod_date.Date.ToString("yyyy-MM-dd").Substring(0, 10);
                    sql += "UPDATE T_Repacking_Voucher SET Oil_Gain=" + oil_gain + ", Voucher_Closed=" + closed + ", Cone_ID = " + cones_dict[coneComboboxCB.SelectedItem.ToString()].Item2 + ", Date_Of_Production='" + max_date + "', Start_Date_Of_Production= '" + start_date_of_prod + "', Narration = '" + narrationTB.Text + "' WHERE Voucher_ID = " + this.voucher_id + "; \n";
                }
                else
                {
                    sql += "UPDATE T_Repacking_Voucher SET Cone_ID=" + cones_dict[coneComboboxCB.SelectedItem.ToString()].Item2 + ", Start_Date_Of_Production = '" + start_date_of_prod + "', Narration = '" + narrationTB.Text + "' WHERE Voucher_ID = " + this.voucher_id + "; \n";
                }


                //Update Fiscal Year Table
                if (max_carton_no != int.MinValue)  //Check if highest carton number is a number
                {
                    sql += "DECLARE @highest_carton_no varchar(20); SELECT @highest_carton_no = Highest_Repacking_Carton_No FROM Fiscal_Year WHERE Fiscal_Year='" + financialYearComboboxCB.SelectedItem.ToString() + "'; \n";
                    sql += "IF @highest_carton_no < " + max_carton_no + "\n";
                    sql += "UPDATE Fiscal_Year SET Highest_Repacking_Carton_No =" + max_carton_no + " WHERE Fiscal_Year='" + financialYearComboboxCB.SelectedItem.ToString() + "'; \n";
                }

                //catch
                sql += "commit transaction; end try BEGIN CATCH rollback transaction; \n";
                sql += "DECLARE @ErrorMessage NVARCHAR(4000); DECLARE @ErrorSeverity INT; DECLARE @ErrorState INT; SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE(); \n";
                sql += "RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState); END CATCH; \n";
                
                DataTable edit = c.runQuery(sql);
                if (edit != null)
                {
                    dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.LawnGreen;
                    c.SuccessBox("Voucher Edited Successfully");
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
                    if (dataGridView1.Rows[rowindex].Cells[2].Value == null)
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
            inwardcartonnwtTextbox.Text = CellSum2(2).ToString("F3");
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
            if (companyComboboxCB.SelectedIndex == 0)
            {
                c.ErrorBox("Select Dyeing Company Name", "Error");
                return;
            }
            if (typeCB.SelectedIndex < 0)
            {
                c.ErrorBox("Select Inward Cartons Type", "Error");
                return;
            }
            string today_fiscal_year = c.getFinancialYear(this.inputDate.Value);
            List<int> today_fiscal_year_arr = c.getFinancialYearArr(today_fiscal_year);
            List<int> minmax_years = c.getFinancialYearArr(this.financialYearComboboxCB.Text);
            if (today_fiscal_year_arr[1] < minmax_years[1])
            {
                c.ErrorBox("You cannot select Carton Financial Year in the future");
                return;
            }
            bool loaded = this.loadData(today_fiscal_year, minmax_years);
            if (loaded == false && this.edit_form == false) return;
            //Set the first date in form
            string current_fiscal_year = c.getFinancialYear(DateTime.Now);
            if (current_fiscal_year == financialYearComboboxCB.Text)
            {
                dataGridView1.Rows[0].Cells[1].Value = DateTime.Now.Date.ToString().Substring(0, 10);
            }
            else
            {
                string[] years = financialYearComboboxCB.Text.Split('-');
                DateTime dt = new DateTime(int.Parse(years[1]), 3, 31);
                this.dataGridView1.Rows[0].Cells[1].Value = dt.Date.ToString("dd-MM-yyyy").Substring(0, 10);
            }

            //set the first carton number in form
            int next_carton_no = int.Parse(c.getNextNumber_FiscalYear("Highest_Repacking_Carton_No", this.financialYearComboboxCB.Text));
            dataGridView1.Rows[0].Cells[2].Value = next_carton_no;
            this.nextcartonnoTB.Text = next_carton_no.ToString();

            //enable and disable 
            this.loadDataButton.Enabled = false;
            this.colourComboboxCB.Enabled = false;
            this.qualityComboboxCB.Enabled = false;
            this.financialYearComboboxCB.Enabled = false;
            this.companyComboboxCB.Enabled = false;
            this.typeCB.Enabled = false;
            this.saveButton.Enabled = true;
            this.dataGridView1.Enabled = true;
        }
        private void coneCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.coneWeightTB.Text = cones_dict[coneComboboxCB.Text].Item1.ToString("F3");
            }
            catch { }
            for (int i = 0; i<dataGridView1.Rows.Count - 1; i++)
            {
                calculate_net_wt(i);
            }
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
                //Set deleted = 1 in T_Repacking_Voucher
                string sql = "--*********************DELETE T_REPACKING_VOUCHER***********************;\n";
                sql += "UPDATE T_Repacking_Voucher SET Deleted = 1 WHERE Voucher_ID=" + this.voucher_id + ";\n";

                //Remove repacked cartons 
                sql += "DELETE FROM T_Repacked_Cartons WHERE Repacking_Voucher_ID = " + this.voucher_id + ";\n";

                //Remove destroyed inward cartons
                sql += "DELETE FROM T_Inward_Carton WHERE Repacking_Voucher_ID = " + this.voucher_id + ";\n";

                DataTable del = c.runQuery(sql);
                if (del != null)
                {
                    c.SuccessBox("Voucher Deleted Successfully");
                    dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Red;
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
                    //fill in with the last integer value
                    int fill_val = highest_carton_no;
                    for(int i=rowindex_tab; i>=0; i--)
                    {
                        try
                        {
                            int.Parse(dataGridView1.Rows[i].Cells[2].Value.ToString());
                            fill_val = int.Parse(dataGridView1.Rows[i].Cells[2].Value.ToString()) + 1;
                            break;
                        }
                        catch { };
                    }
                    dataGridView1.Rows[rowindex_tab + 1].Cells[2].Value = fill_val.ToString();
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
                    //fill in with the last integer value
                    int fill_val = highest_carton_no;
                    for (int i = rowindex_tab; i >= 0; i--)
                    {
                        try
                        {
                            int.Parse(dataGridView1.Rows[i].Cells[2].Value.ToString());
                            fill_val = int.Parse(dataGridView1.Rows[i].Cells[2].Value.ToString()) + 1;
                            break;
                        }
                        catch { };
                    }
                    dataGridView1.Rows[rowindex_tab + 1].Cells[2].Value = fill_val.ToString();
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
            if (e.KeyCode == Keys.Tab &&
                (dataGridView2.SelectedCells.Cast<DataGridViewCell>().Any(x => x.ColumnIndex == 1) || this.edit_cmd_send == true))
            {
                bool edit_cmd_local = this.edit_cmd_send;
                this.edit_cmd_send = false;
                int rowindex_tab = dataGridView2.SelectedCells[0].RowIndex;
                if (edit_cmd_local == true) rowindex_tab--;

                if (rowindex_tab < 0)
                {
                    SendKeys.Send("{tab}");
                    return;
                }
                if (dataGridView2.Rows.Count - 2 == rowindex_tab)
                {
                    DataGridViewRow row = (DataGridViewRow)dataGridView2.Rows[rowindex_tab].Clone();
                    dataGridView2.Rows.Add(row);
                }
                if (dataGridView2.Rows.Count - 1 < rowindex_tab + 1)
                {
                    dataGridView2.Rows.Add();
                }
                if (edit_cmd_local == false)
                {
                    SendKeys.Send("{tab}");
                }
            }
            if (e.KeyCode == Keys.Tab &&
               (dataGridView2.SelectedCells.Cast<DataGridViewCell>().Any(x => x.ColumnIndex == 2)))
            {
                int rowindex_tab = dataGridView2.SelectedCells[0].RowIndex;
                if (rowindex_tab < 0)
                {
                    SendKeys.Send("{tab}");
                    return;
                }
                if (dataGridView2.Rows.Count - 2 == rowindex_tab)
                {
                    DataGridViewRow row = (DataGridViewRow)dataGridView2.Rows[rowindex_tab].Clone();
                    dataGridView2.Rows.Add(row);
                }
                if (dataGridView2.Rows.Count - 1 < rowindex_tab + 1)
                {
                    dataGridView2.Rows.Add();
                }
                SendKeys.Send("{tab}");
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
                ((ComboBox)e.Control).AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                ((ComboBox)e.Control).FormattingEnabled = false;
            }

            dgv2_cmb = e.Control as ComboBox;
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
                            c.ErrorBox("Rows " + (i + 1).ToString() + " and " + (j + 1).ToString() + " have same Carton Number", "Error");
                            dataGridView2.Rows[j].Cells[1].Value = "";
                            dataGridView2.Rows[j].Cells[2].Value = "";
                            return;
                        }
                    }
                }
                if (!c.Cell_Not_NullOrEmpty(this.dataGridView2, e.RowIndex, 1))
                {
                    return;
                }

                string data = dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString();
                Tuple<string, string, string> values = Global.getCartonNo_Weight_FiscalYear(data);
                Console.WriteLine("----" + values.Item1 + "----");
                Console.WriteLine("----" + values.Item2 + "----");
                Console.WriteLine("----" + values.Item3 + "----");
                string carton_id = destroyed_carton_id_get[values];
                //Console.WriteLine(data.HiddenValue);
                dataGridView2.Rows[e.RowIndex].Cells[2].Value = destroyed_carton_dict[carton_id]["Net_Weight"].ToString();
                inwardcartonnwtTextbox.Text = CellSum2(2).ToString("F3");

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

        private void dataGridView2_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void companyComboboxCB_KeyPress(object sender, KeyPressEventArgs e)
        {
        }
    }
}
