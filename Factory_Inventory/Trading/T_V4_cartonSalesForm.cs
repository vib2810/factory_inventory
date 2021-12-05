using Factory_Inventory.Factory_Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows;
using System.Windows.Forms;

namespace Factory_Inventory
{
    public partial class T_V3_cartonSalesForm : Form
    {
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Tab &&
                dataGridView1.EditingControl != null &&
                //msg.HWnd == dataGridView1.EditingControl.Handle &&
                dataGridView1.SelectedCells
                    .Cast<DataGridViewCell>()
                    .Any(x => x.ColumnIndex == 1))
            {
                Console.WriteLine("Inside process cmd key tab 1");
                this.edit_cmd_send = true;
                SendKeys.Send("{Tab}");
                return false;
            }
            if (keyData == Keys.F2)
            {
                Console.WriteLine("dgv1");
                this.dataGridView1.Focus();
                this.ActiveControl = dataGridView1;
                this.dataGridView1.CurrentCell = dataGridView1[0, 0];
                return false;
            }
            if (keyData == Keys.F3)
            {
                Console.WriteLine("cb");
                this.comboBox3CB.Focus();
                this.ActiveControl = comboBox3CB;
                return false;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private DbConnect c;
        private bool edit_cmd_send = false;
        private bool edit_form = false;               //True if form is being edited
        private List<Tuple<string, float, string>> carton_data = new List<Tuple<string, float, string>>();         //List that stores carton Tuples<Carton_No, Net_Weight, Fiscal_Year> from SQL of Inward Cartons
        private List<Tuple<string, float, string>> carton_data_repack = new List<Tuple<string, float, string>>();          //List that stores carton Tuples<Carton_No, Net_Weight, Fiscal_Year> from SQL of Repacked Cartons
        
        //Stores previous cartons ONLY IN EDIT MODE
        private Dictionary<Tuple<string, float, string>, bool> carton_data_edit = new Dictionary<Tuple<string, float, string>, bool>();
        private Dictionary<Tuple<string, float, string>, bool> carton_data_repack_edit = new Dictionary<Tuple<string, float, string>, bool>();  

        private M_V_history v1_history;
        private int voucher_id;
        private Dictionary<string, int> quality_dict = new Dictionary<string, int>();
        private Dictionary<string, int> company_dict = new Dictionary<string, int>();
        private Dictionary<string, int> customer_dict = new Dictionary<string, int>();
        private Dictionary<string, int> colour_dict = new Dictionary<string, int>();
        private Dictionary<int, string> colour_dict_reverse = new Dictionary<int, string>();
        
        //Tuple<Carton No, Net Weight, Fiscal Year> -> Tuple<Row, Present>
        //The bool is used only in edit mode to see which catrons were removed from old one
        Dictionary<Tuple<string, float, string>, DataRow> carton_fetch_data = new Dictionary<Tuple<string, float, string>, DataRow>();
        Dictionary<Tuple<string, float, string>, DataRow> carton_fetch_data_repack = new Dictionary<Tuple<string, float, string>, DataRow>();

        //Form Functions
        public T_V3_cartonSalesForm()
        {
            InitializeComponent();
            this.c = new DbConnect();

            #region //combobox
            //Create frop down type list
            List<string> dataSource = new List<string>();
            dataSource.Add("---Select---");
            dataSource.Add("0");
            dataSource.Add("1");
            this.typeCB.DataSource = dataSource;
            this.typeCB.DisplayMember = "Type";
            this.typeCB.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.typeCB.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.typeCB.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            //Create drop-down Quality list
            var dataSource1 = new List<string>();
            DataTable dt = c.runQuery("SELECT * FROM T_M_Quality_Before_Job");
            dataSource1.Add("---Select---");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dataSource1.Add(dt.Rows[i]["Quality_Before_Job"].ToString());
                quality_dict[dt.Rows[i]["Quality_Before_Job"].ToString()] = int.Parse(dt.Rows[i]["Quality_Before_Job_ID"].ToString());
            }
            this.comboBox1CB.DataSource = dataSource1;
            this.comboBox1CB.DisplayMember = "Quality";
            this.comboBox1CB.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.comboBox1CB.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.comboBox1CB.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            //Create drop - down Company list
            var dataSource2 = new List<string>();
            dt = c.runQuery("SELECT * FROM T_M_Company_Names");
            dataSource2.Add("---Select---");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dataSource2.Add(dt.Rows[i]["Company_Name"].ToString());
                company_dict[dt.Rows[i]["Company_Name"].ToString()] = int.Parse(dt.Rows[i]["Company_ID"].ToString());
            }
            this.comboBox2CB.DataSource = dataSource2;
            this.comboBox2CB.DisplayMember = "Company_Names";
            this.comboBox2CB.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.comboBox2CB.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.comboBox2CB.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            //Create drop - down Customers list
            var dataSource3 = new List<string>();
            dt = c.runQuery("SELECT * FROM T_M_Customers");
            dataSource3.Add("---Select---");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dataSource3.Add(dt.Rows[i]["Customer_Name"].ToString());
                customer_dict[dt.Rows[i]["Customer_Name"].ToString()] = int.Parse(dt.Rows[i]["Customer_ID"].ToString());
            }
            this.comboBox3CB.DataSource = dataSource3;
            this.comboBox3CB.DisplayMember = "Customers";
            this.comboBox3CB.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.comboBox3CB.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.comboBox3CB.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            //Create drop-down Colour list
            var dataSource4 = new List<string>();
            dt = c.runQuery("SELECT * FROM T_M_Colours");
            dataSource4.Add("---Select---");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dataSource4.Add(dt.Rows[i]["Colour"].ToString());
                colour_dict[dt.Rows[i]["Colour"].ToString()] = int.Parse(dt.Rows[i]["Colour_ID"].ToString());
                colour_dict_reverse[int.Parse(dt.Rows[i]["Colour_ID"].ToString())] = dt.Rows[i]["Colour"].ToString();
            }
            List<string> final_list = dataSource4.Distinct().ToList();
            this.shadeCB.DataSource = final_list;
            this.shadeCB.DisplayMember = "Colour";
            this.shadeCB.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.shadeCB.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.shadeCB.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            
            #endregion //combobox

            //DatagridView
            dataGridView1.Columns.Add("Sl_No", "Sl. No");
            dataGridView1.Columns[0].ReadOnly = true;
            DataGridViewComboBoxColumn dgvCmb = new DataGridViewComboBoxColumn();
            for (int i = 0; i < this.carton_data.Count; i++) dgvCmb.Items.Add(this.carton_data[i].Item1 + " (" + this.carton_data[i].Item2 + ")");
            //dgvCmb.DataSource = this.carton_data;

            dgvCmb.HeaderText = "Carton Number";
            dataGridView1.Columns.Insert(1, dgvCmb);
            dataGridView1.Columns[1].Width = 250;
            dataGridView1.Columns.Add("Selling Weight", "Selling Weight");
            dataGridView1.Columns.Add("Available Weight", "Available Weight");
            dataGridView1.Columns[3].ReadOnly = true;
            dataGridView1.Columns.Add("Shade", "Shade");
            dataGridView1.Columns[4].ReadOnly = true;
            dataGridView1.Columns.Add("Comments", "Comments");
            dataGridView1.RowCount = 10;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            dataGridView1.RowHeadersWidth = 20;
            c.set_dgv_column_sort_state(this.dataGridView1, DataGridViewColumnSortMode.NotSortable);
        }
        public T_V3_cartonSalesForm(DataRow row, bool isEditable, M_V_history v1_history)
        {
            InitializeComponent();
            this.edit_form = true;
            this.v1_history = v1_history;
            this.c = new DbConnect();

            //Create frop down type list
            List<string> dataSource = new List<string>();
            dataSource.Add("---Select---");
            dataSource.Add("0");
            dataSource.Add("1");
            this.typeCB.DataSource = dataSource;
            this.typeCB.DisplayMember = "Type";
            this.typeCB.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.typeCB.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.typeCB.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            #region //dropdown
            //Create drop-down list for quality
            //Create drop-down Quality list
            var dataSource1 = new List<string>();
            DataTable dt = c.runQuery("SELECT * FROM T_M_Quality_Before_Job");
            dataSource1.Add("---Select---");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dataSource1.Add(dt.Rows[i]["Quality_Before_Job"].ToString());
                quality_dict[dt.Rows[i]["Quality_Before_Job"].ToString()] = int.Parse(dt.Rows[i]["Quality_Before_Job_ID"].ToString());
            }
            this.comboBox1CB.DataSource = dataSource1;
            this.comboBox1CB.DisplayMember = "Quality";
            this.comboBox1CB.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.comboBox1CB.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.comboBox1CB.AutoCompleteMode = AutoCompleteMode.SuggestAppend;


            //Create drop - down Company list
            var dataSource2 = new List<string>();
            dt = c.runQuery("SELECT * FROM T_M_Company_Names");
            dataSource2.Add("---Select---");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dataSource2.Add(dt.Rows[i]["Company_Name"].ToString());
                company_dict[dt.Rows[i]["Company_Name"].ToString()] = int.Parse(dt.Rows[i]["Company_ID"].ToString());
            }
            this.comboBox2CB.DataSource = dataSource2;
            this.comboBox2CB.DisplayMember = "Company_Names";
            this.comboBox2CB.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.comboBox2CB.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.comboBox2CB.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            //Create drop - down Customers list
            var dataSource3 = new List<string>();
            dt = c.runQuery("SELECT * FROM T_M_Customers");
            dataSource3.Add("---Select---");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dataSource3.Add(dt.Rows[i]["Customer_Name"].ToString());
                customer_dict[dt.Rows[i]["Customer_Name"].ToString()] = int.Parse(dt.Rows[i]["Customer_ID"].ToString());
            }
            this.comboBox3CB.DataSource = dataSource3;
            this.comboBox3CB.DisplayMember = "Customers";
            this.comboBox3CB.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.comboBox3CB.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.comboBox3CB.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            //Create drop-down Colour list
            var dataSource4 = new List<string>();
            dt = c.runQuery("SELECT * FROM T_M_Colours");
            dataSource4.Add("---Select---");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dataSource4.Add(dt.Rows[i]["Colour"].ToString());
                colour_dict[dt.Rows[i]["Colour"].ToString()] = int.Parse(dt.Rows[i]["Colour_ID"].ToString());
                colour_dict_reverse[int.Parse(dt.Rows[i]["Colour_ID"].ToString())] = dt.Rows[i]["Colour"].ToString();
            }
            List<string> final_list = dataSource4.Distinct().ToList();
            this.shadeCB.DataSource = final_list;
            this.shadeCB.DisplayMember = "Colour";
            this.shadeCB.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.shadeCB.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.shadeCB.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            #endregion //dropdown

            //DatagridView
            dataGridView1.Columns.Add("Sl. No", "Sl. No");
            dataGridView1.Columns[0].ReadOnly = true;

            DataGridViewComboBoxColumn dgvCmb = new DataGridViewComboBoxColumn();
            dgvCmb.HeaderText = "Carton Number";
            for (int i = 0; i < this.carton_data.Count; i++) dgvCmb.Items.Add(this.carton_data[i].Item1 + " (" + this.carton_data[i].Item2 + ")");
            //dgvCmb.DataSource = this.carton_data;
            dataGridView1.Columns.Insert(1, dgvCmb);
            dataGridView1.Columns.Add("Selling Weight", "Selling Weight");
            dataGridView1.Columns.Add("Available Weight", "Available Weight");
            dataGridView1.Columns.Add("Shade", "Shade");
            dataGridView1.Columns[3].ReadOnly = true;
            dataGridView1.Columns[4].ReadOnly = true;
            dataGridView1.Columns.Add("Comments", "Comments");
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);

            dataGridView1.Columns.Add("Sl_No", "Sl. No");
            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.RowHeadersWidth = 20;

            c.set_dgv_column_sort_state(this.dataGridView1, DataGridViewColumnSortMode.NotSortable);

            if (isEditable == false)
            {
                this.Text += "(View Only)";
                this.saveButton.Enabled = false;
                this.deleteButton.Visible = true;
                this.deleteButton.Enabled = true;
                this.disable_form_edit();
            }
            else
            {
                this.Text += "(Edit)";
                this.saleDateDTP.Enabled = true;
                this.comboBox1CB.Enabled = false;
                this.comboBox2CB.Enabled = false;
                this.saveButton.Enabled = true;
                this.dataGridView1.ReadOnly = false;
                this.saleDONoTB.ReadOnly = true;
                this.loadCartonButton.Enabled = false;
                this.typeCB.Enabled = false;
            }

            this.saleDateDTP.Value = Convert.ToDateTime(row["Date_Of_Sale"].ToString());
            this.typeCB.SelectedIndex = this.typeCB.FindStringExact(row["Type_Of_Sale"].ToString());
            //if (this.comboBox1CB.FindStringExact(row["Quality_Before_Job"].ToString()) == -1)
            //{
            //    norep_quality_list.Add(row["Quality"].ToString());
            //    this.comboBox1CB.DataSource = null;
            //    this.comboBox1CB.DataSource = norep_quality_list;
            //}
            this.comboBox1CB.SelectedIndex = this.comboBox1CB.FindStringExact(row["Quality_Before_Job"].ToString());
            //if (this.comboBox2CB.FindStringExact(row["Company_Name"].ToString()) == -1)
            //{
            //    dataSource2.Add(row["Company_Name"].ToString());
            //    this.comboBox2CB.DataSource = null;
            //    this.comboBox2CB.DataSource = dataSource2;

            //}
            this.comboBox2CB.SelectedIndex = this.comboBox2CB.FindStringExact(row["Company_Name"].ToString());
            this.voucher_id = int.Parse(row["Voucher_ID"].ToString());
            //if (this.comboBox3CB.FindStringExact(row["Customer_Name"].ToString()) == -1)
            //{
            //    dataSource3.Add(row["Customer_Name"].ToString());
            //    this.comboBox3CB.DataSource = null;
            //    this.comboBox3CB.DataSource = dataSource3;

            //}
            this.comboBox3CB.SelectedIndex = this.comboBox3CB.FindStringExact(row["Customer_Name"].ToString());
            this.saleDONoTB.Text = row["Sale_DO_No"].ToString();
            this.rateTextboxTB.Text = row["Sale_Rate"].ToString();
            this.narrationTB.Text = row["Narration"].ToString();


            DataTable d1 = new DataTable();
            string sql = "SELECT temp3.*, T_Carton_Inward_Voucher.Date_Of_Billing\n";
            sql+= "FROM\n";
            sql += "    (SELECT temp2.*, T_M_Colours.Colour\n";
            sql += "    FROM\n";
            sql += "        (SELECT temp1.*, T_Inward_Carton.Carton_No, T_Inward_Carton.Carton_State, T_Inward_Carton.Net_Weight, T_Inward_Carton.Fiscal_Year, T_Inward_Carton.Inward_Voucher_ID, T_Inward_Carton.Colour_ID\n";
            sql += "        FROM\n";
            sql += "            (SELECT T_Carton_Sales.Carton_ID, T_Carton_Sales.Sales_Voucher_ID, T_Carton_Sales.Sale_Comments, T_Carton_Sales.Sale_Display_Order, T_Carton_Sales.Sold_Weight, T_Sales_Voucher.Date_Of_Sale\n";
            sql += "            FROM T_Carton_Sales\n";
            sql += "            LEFT OUTER JOIN T_Sales_Voucher\n";
            sql += "            ON T_Sales_Voucher.Voucher_ID = T_Carton_Sales.Sales_Voucher_ID\n";
            sql += "            WHERE T_Sales_Voucher.Voucher_ID = '" + this.voucher_id + "' AND T_Carton_Sales.Carton_ID like 'IN%') as temp1\n";
            sql += "        LEFT OUTER JOIN T_Inward_Carton\n";
            sql += "        ON T_Inward_Carton.Carton_ID = temp1.Carton_ID) as temp2\n";
            sql += "    LEFT OUTER JOIN T_M_Colours\n";
            sql += "    ON T_M_Colours.Colour_ID = temp2.Colour_ID) as temp3\n";
            sql += "LEFT OUTER JOIN T_Carton_Inward_Voucher\n";
            sql += "ON T_Carton_Inward_Voucher.Voucher_ID = temp3.Inward_Voucher_ID\n";
            sql += "ORDER BY Sale_Display_Order ASC\n;";
            d1 = c.runQuery(sql);

            sql = "SELECT temp3.*, T_Sales_Voucher.Date_Of_Sale\n";
            sql += "FROM\n";
            sql += "    (SELECT temp2.*, T_M_Colours.Colour\n";
            sql += "    FROM\n";
            sql += "        (SELECT temp1.*, T_Repacked_Cartons.Carton_No, T_Repacked_Cartons.Carton_State, T_Repacked_Cartons.Date_Of_Production, T_Repacked_Cartons.Net_Weight, T_Repacked_Cartons.Fiscal_Year, T_Repacked_Cartons.Repacking_Voucher_ID, T_Repacked_Cartons.Colour_ID\n";
            sql += "        FROM\n";
            sql += "            (SELECT T_Carton_Sales.Carton_ID, T_Carton_Sales.Sales_Voucher_ID, T_Carton_Sales.Sale_Comments, T_Carton_Sales.Sale_Display_Order, T_Carton_Sales.Sold_Weight, T_Sales_Voucher.Date_Of_Sale\n";
            sql += "            FROM T_Carton_Sales\n";
            sql += "            LEFT OUTER JOIN T_Sales_Voucher\n";
            sql += "            ON T_Sales_Voucher.Voucher_ID = T_Carton_Sales.Sales_Voucher_ID\n";
            sql += "            WHERE T_Sales_Voucher.Voucher_ID = '" + this.voucher_id + "' AND T_Carton_Sales.Carton_ID like 'RP%') as temp1\n";
            sql += "        LEFT OUTER JOIN T_Repacked_Cartons\n";
            sql += "        ON T_Repacked_Cartons.Carton_ID = temp1.Carton_ID) as temp2\n";
            sql += "    LEFT OUTER JOIN T_M_Colours\n";
            sql += "    ON T_M_Colours.Colour_ID = temp2.Colour_ID) as temp3\n";
            sql += "LEFT OUTER JOIN T_Sales_Voucher\n";
            sql += "ON T_Sales_Voucher.Voucher_ID = temp3.Repacking_Voucher_ID\n";
            sql += "ORDER BY Sale_Display_Order ASC\n;";
            DataTable d2 = c.runQuery(sql);


            dataGridView1.RowCount = d1.Rows.Count + d2.Rows.Count + 1;

            for (int i = 0; i < d1.Rows.Count; i++)
            {
                string carton_no = d1.Rows[i]["Carton_No"].ToString();
                float net_weight = float.Parse(d1.Rows[i]["Net_Weight"].ToString());
                string fiscal_year = d1.Rows[i]["Fiscal_Year"].ToString();
                this.carton_data.Add(new Tuple<string, float, string>(carton_no, net_weight, fiscal_year));
                if(this.edit_form==true) this.carton_data_edit[new Tuple<string, float, string>(carton_no, net_weight, fiscal_year)] = false;
                dgvCmb.Items.Add(carton_no + " ; " + net_weight.ToString() + " ; " + fiscal_year);
                this.carton_fetch_data[new Tuple<string, float, string>(carton_no, net_weight, fiscal_year)] = d1.Rows[i];
                dataGridView1.Rows[int.Parse(d1.Rows[i]["Sale_Display_Order"].ToString())].Cells[1].Value = carton_no + " ; " + net_weight.ToString() + " ; " + fiscal_year;
                dataGridView1.Rows[int.Parse(d1.Rows[i]["Sale_Display_Order"].ToString())].Cells["Comments"].Value = d1.Rows[i]["Sale_Comments"].ToString();
            }
            for (int i = 0; i < d2.Rows.Count; i++)
            {
                string carton_no = d2.Rows[i]["Carton_No"].ToString();
                float net_weight = float.Parse(d2.Rows[i]["Net_Weight"].ToString());
                string fiscal_year = d2.Rows[i]["Fiscal_Year"].ToString();
                this.carton_data_repack.Add(new Tuple<string, float, string>(carton_no, net_weight, fiscal_year));
                if (this.edit_form == true) this.carton_data_repack_edit[new Tuple<string, float, string>(carton_no, net_weight, fiscal_year)] = false;
                dgvCmb.Items.Add(carton_no + " ; " + net_weight.ToString() + " ; " + fiscal_year + " (Repacked)");
                this.carton_fetch_data_repack[new Tuple<string, float, string>(carton_no, net_weight, fiscal_year)] = d2.Rows[i];
                dataGridView1.Rows[int.Parse(d2.Rows[i]["Sale_Display_Order"].ToString())].Cells[1].Value = carton_no + " ; " + net_weight.ToString() + " ; " + fiscal_year + " (Repacked)";
                dataGridView1.Rows[int.Parse(d2.Rows[i]["Sale_Display_Order"].ToString())].Cells["Comments"].Value = d2.Rows[i]["Sale_Comments"].ToString();
            }

            this.loadData();

            c.set_dgv_column_sort_state(this.dataGridView1, DataGridViewColumnSortMode.NotSortable);

            if (row["Sale_Bill_No"].ToString() != "")
            {
                this.label10.Text = "This DO cannot be edited/deleted as its bill as already been made. Delete/Edit its bill first";
                this.deleteButton.Enabled = false;
                this.disable_form_edit();
            }
        }
        private void M_V1_cartonSalesForm_Load(object sender, EventArgs e)
        {
            if (Global.access == 2) this.deleteButton.Visible = false;
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

            this.saleDateDTP.Focus();
            if (Global.access == 2)
            {
                this.deleteButton.Visible = false;
            }
            dataGridView1.Columns[1].Width = 200;
            this.saleDateDTP.MinDate = this.inputDate.Value.Date.AddDays(-2);
            this.saleDateDTP.MaxDate = this.inputDate.Value.Date.AddDays(2);
    }

        //Own Functions
        private List<string> split_catron_data(string text)
        {
            string[] temp = text.Split(';');
            string[] temp1 = temp[2].Split('(');
            List<string> ans = new List<string>();
            ans.Add(temp[0].Trim());
            ans.Add(temp[1].Trim());
            ans.Add(temp1[0].Trim());
            try
            {
                ans.Add(temp1[1]);
            }
            catch { }
            return ans;
        }
        public void disable_form_edit()
        {
            this.saleDateDTP.Enabled = false;
            this.comboBox1CB.Enabled = false;
            this.comboBox2CB.Enabled = false;
            this.comboBox3CB.Enabled = false;
            this.loadCartonButton.Enabled = false;
            this.saveButton.Enabled = false;
            this.rateTextboxTB.ReadOnly= true;
            this.typeCB.Enabled = false;
            this.dataGridView1.ReadOnly = true;
            this.deleteToolStripMenuItem.Enabled = false;
            this.saleDONoTB.ReadOnly= true;
            this.narrationTB.ReadOnly = true;
        }
        private float CellSum()
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
                    if (dataGridView1.Rows[i].Cells[2].Value != null)
                        sum += float.Parse(dataGridView1.Rows[i].Cells[2].Value.ToString());
                }
                return sum;
            }
            catch
            {
                return sum;
            }
        }
        private void loadData()
        {
            DataTable d1 = c.runQuery("SELECT T_Inward_Carton.Carton_ID, T_Inward_Carton.Carton_No, T_Inward_Carton.Net_Weight, T_Inward_Carton.Fiscal_Year, T_Inward_Carton.Colour_ID, T_Carton_Inward_Voucher.Date_Of_Billing FROM T_Inward_Carton INNER JOIN T_Carton_Inward_Voucher ON T_Inward_Carton.Inward_Voucher_ID = T_Carton_Inward_Voucher.Voucher_ID WHERE T_Inward_Carton.Quality_ID = " + quality_dict[comboBox1CB.Text] + " and T_Inward_Carton.Company_ID = " + company_dict[comboBox2CB.Text] + " and (T_Inward_Carton.Carton_State = 0 OR T_Inward_Carton.Carton_State = 2)");
            DataTable d2 = c.runQuery("SELECT Carton_ID, Carton_No, Net_Weight, Fiscal_Year, Colour_ID, Date_Of_Production FROM T_Repacked_Cartons WHERE Quality_ID = " + quality_dict[comboBox1CB.Text] + " and Company_ID = " + company_dict[comboBox2CB.Text] + " and (Carton_State = 0 OR Carton_State = 1)");
            DataGridViewComboBoxColumn dgvCmb = (DataGridViewComboBoxColumn)dataGridView1.Columns[1];
            if (this.edit_form == false) d1.Columns.Add("Sold_Weight");
            for (int i = 0; i < d1.Rows.Count; i++)
            {
                string cartonno = d1.Rows[i]["Carton_No"].ToString();
                string carton_fiscal_year = d1.Rows[i]["Fiscal_Year"].ToString();
                string to_show = cartonno + " ; " + d1.Rows[i]["Net_Weight"].ToString() + " ; " + carton_fiscal_year;
                dgvCmb.Items.Add(to_show);
                //Tuple<Carton No, Colour ID, Net Weight, Fiscal Year>
                Tuple<string, float, string> temp = new Tuple<string, float, string>(cartonno, float.Parse(d1.Rows[i]["Net_Weight"].ToString()), carton_fiscal_year);
                this.carton_data.Add(temp);
                if (this.edit_form == false)
                {
                    DataTable dt = c.runQuery("SELECT SUM(Sold_Weight) FROM T_Carton_Sales WHERE Carton_ID='" + d1.Rows[i]["Carton_ID"].ToString() + "'");
                    d1.Rows[i]["Sold_Weight"] = dt.Rows[0][0].ToString();
                }
                if(carton_fetch_data.ContainsKey(new Tuple<string, float, string>(cartonno, float.Parse(d1.Rows[i]["Net_Weight"].ToString()), carton_fiscal_year)) == false)
                    this.carton_fetch_data[new Tuple<string, float, string>(cartonno, float.Parse(d1.Rows[i]["Net_Weight"].ToString()), carton_fiscal_year)] = d1.Rows[i];
            }
            if (this.edit_form == false) d2.Columns.Add("Sold_Weight");
            for (int i = 0; i < d2.Rows.Count; i++)
            {
                string cartonno = d2.Rows[i]["Carton_No"].ToString();
                string carton_fiscal_year = d2.Rows[i]["Fiscal_Year"].ToString();
                string to_show = cartonno + " ; " + d2.Rows[i]["Net_Weight"].ToString() + " ; " + carton_fiscal_year + " (Repacked)";
                dgvCmb.Items.Add(to_show);
                //Tuple<Carton No, Colour ID, Net Weight, Fiscal Year>
                Tuple<string, float, string> temp = new Tuple<string, float, string>(cartonno, float.Parse(d2.Rows[i]["Net_Weight"].ToString()), carton_fiscal_year);
                this.carton_data_repack.Add(temp);
                if (this.edit_form == false)
                {
                    DataTable dt = c.runQuery("SELECT SUM(Sold_Weight) FROM T_Carton_Sales WHERE Carton_ID='" + d2.Rows[i]["Carton_ID"].ToString() + "'");
                    d2.Rows[i]["Sold_Weight"] = dt.Rows[0][0].ToString();
                }
                if (carton_fetch_data_repack.ContainsKey(new Tuple<string, float, string>(cartonno, float.Parse(d2.Rows[i]["Net_Weight"].ToString()), carton_fiscal_year)) == false)
                    this.carton_fetch_data_repack[new Tuple<string, float, string>(cartonno, float.Parse(d2.Rows[i]["Net_Weight"].ToString()), carton_fiscal_year)] = d2.Rows[i];
            }
            
        }
        private void amountTB_Value()
        {
            float rate, weight;
            try
            {
                rate = float.Parse(this.rateTextboxTB.Text);
            }
            catch
            {
                amountTB.Text = "Enter numeric rate";
                return;
            }
            try
            {
                weight = float.Parse(this.totalWeightTB.Text);
            }
            catch
            {
                amountTB.Text = "";
                return;
            }
            this.amountTB.Text = (rate * weight).ToString("F2");
        }
        private void saleDONoTB_Value()
        {
            if (this.typeCB.SelectedIndex == 1)
            {
            
                this.saleDONoTB.Text = c.getNextNumber_FiscalYear("Highest_Trading_0_DO_No", c.getFinancialYear(this.saleDateDTP.Value));
            }
            else if (this.typeCB.SelectedIndex == 2)
            {
                this.saleDONoTB.Text = c.getNextNumber_FiscalYear("Highest_Trading_1_DO_No", c.getFinancialYear(this.saleDateDTP.Value));
            }
            else
            {
                this.saleDONoTB.Text = "";
            }
        }

        //Clicks
        private void saveButton_Click(object sender, EventArgs e)
        {
            if (c.check_login_val() == false) return;
            //checks
            if (dataGridView1.Rows[0].Cells[1].Value == null)
            {
                c.ErrorBox("Please enter Carton Numbers", "Error");
                return;
            }
            if (comboBox3CB.Text == "---Select---")
            {
                c.ErrorBox("Select Customer Name", "Error");
                return;
            }
            if (rateTextboxTB.Text == null)
            {
                c.ErrorBox("Enter Select selling price", "Error");
                return;
            }
            if (rateTextboxTB.Text == "")
            {
                c.ErrorBox("Enter Select selling price", "Error");
                return;
            }
            try
            {
                float.Parse(rateTextboxTB.Text);
            }
            catch
            {
                c.ErrorBox("Please enter numeric selling price only", "Error");
                return;
            }
            if (float.Parse(rateTextboxTB.Text) < 100F)
            {
                c.ErrorBox("Please enter a valid selling price (more than 100)", "Error");
                return;
            }
            if (inputDate.Value.Date < saleDateDTP.Value.Date)
            {
                c.ErrorBox("Issue Date is in the future", "Error");
                return;
            }
            if (this.comboBox3CB.FindStringExact(this.comboBox3CB.Text) == -1)
            {
                c.ErrorBox("Select valid customer", "Error");
                this.comboBox3CB.SelectedIndex = 0;
                return;
            }
            List<Tuple<Tuple<string, float, string>, int>> cartonno = new List<Tuple<Tuple<string, float, string>, int>>();
            List<Tuple<Tuple<string, float, string>, int>> cartonno_repack = new List<Tuple<Tuple<string, float, string>, int>>();

            //ONLY USED IN EDIT MODE
            List<Tuple<Tuple<string, float, string>, int>> to_update = new List<Tuple<Tuple<string, float, string>, int>>();
            List<Tuple<string, float, string>> to_delete = new List<Tuple<string, float, string>>();

            List<Tuple<Tuple<string, float, string>, int>> to_update_repack = new List<Tuple<Tuple<string, float, string>, int>>();
            List<Tuple<string, float, string>> to_delete_repack = new List<Tuple<string, float, string>>();
            int number = 0;

            List<string> temp = new List<string>();
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                if (!c.Cell_Not_NullOrEmpty(this.dataGridView1, i, 1))
                {
                    continue;
                }
                else
                {
                    float selling_weight = float.Parse(dataGridView1.Rows[i].Cells[2].Value.ToString());
                    if(selling_weight==0F)
                    {
                        c.ErrorBox("Selling Weight in row " + (i + 1) + "should be more than 0kg");
                        return;
                    }
                    float current_weight = float.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString());
                    if(current_weight - selling_weight < 0F)
                    {
                        c.ErrorBox("Selling more weight than available in row " + (i + 1));
                        return;
                    }
                    List<string> carton = this.split_catron_data(dataGridView1.Rows[i].Cells[1].Value.ToString());
                    if (carton.Count == 3)
                    {
                        cartonno.Add(new Tuple<Tuple<string, float, string>, int>(new Tuple<string, float, string>(carton[0], float.Parse(carton[1]), carton[2]), i));
                        if(this.edit_form==true)
                        {
                            to_update.Add(new Tuple<Tuple<string, float, string>, int>(new Tuple<string, float, string>(carton[0], float.Parse(carton[1]), carton[2]), i));
                            if (carton_data_edit.ContainsKey(new Tuple<string, float, string>(carton[0], float.Parse(carton[1]), carton[2])) == true)
                            {
                                carton_data_edit[new Tuple<string, float, string>(carton[0], float.Parse(carton[1]), carton[2])] = true;
                            }
                        }
                    }
                    if (carton.Count == 4)
                    {
                        cartonno_repack.Add(new Tuple<Tuple<string, float, string>, int>(new Tuple<string, float, string>(carton[0], float.Parse(carton[1]), carton[2]), i));
                        if (this.edit_form == true)
                        {
                            to_update_repack.Add(new Tuple<Tuple<string, float, string>, int>(new Tuple<string, float, string>(carton[0], float.Parse(carton[1]), carton[2]), i));
                            if (carton_data_repack_edit.ContainsKey(new Tuple<string, float, string>(carton[0], float.Parse(carton[1]), carton[2])) == true)
                            {
                                carton_data_repack_edit[new Tuple<string, float, string>(carton[0], float.Parse(carton[1]), carton[2])] = true;
                            }
                        }
                    }
                    number++;
                    temp.Add(dataGridView1.Rows[i].Cells[1].Value.ToString());

                    var distinctBytes = new HashSet<string>(temp);
                    bool allDifferent = distinctBytes.Count == temp.Count;
                    if (allDifferent == false)
                    {
                        c.ErrorBox("Please Enter Distinct Carton Nos at Row: " + (i + 1).ToString(), "Error");
                        return;
                    }

                }
            }

            if (this.edit_form == false)
            {

                string inputdate = inputDate.Value.Date.ToString("MM-dd-yyyy").Substring(0, 10);
                string issueDate = saleDateDTP.Value.Date.ToString("MM-dd-yyyy").Substring(0, 10);
                string fiscal_year = c.getFinancialYear(saleDateDTP.Value);

                //check if bill dates/Production Dates of all cartons are <= issue date
                Dictionary<Tuple<string, float, string>, string> dates = new Dictionary<Tuple<string, float, string>, string>();
                Dictionary<Tuple<string, float, string>, string> dates_repack = new Dictionary<Tuple<string, float, string>, string>();
                for (int i = 0; i < cartonno.Count; i++)
                {
                    DataRow dr = carton_fetch_data[cartonno[i].Item1];
                    dates[cartonno[i].Item1] = dr["Date_Of_Billing"].ToString();
                }
                for (int i = 0; i < cartonno_repack.Count; i++)
                {
                    DataRow dr = carton_fetch_data_repack[cartonno_repack[i].Item1];
                    dates_repack[cartonno_repack[i].Item1] = dr["Date_Of_Production"].ToString();
                }
                for (int i = 0; i < cartonno.Count; i++)
                {
                    DateTime bill = Convert.ToDateTime(dates[cartonno[i].Item1]);
                    if (saleDateDTP.Value < bill)
                    {
                        c.ErrorBox("Carton number: " + cartonno[i].Item1.Item1 + " ; " + cartonno[i].Item1.Item2 + " ; " + cartonno[i].Item1.Item3 + " at row " + (i + 1).ToString() + " has Date of Billing (" + bill.Date.ToString("dd-MM-yyyy").Substring(0, 10) + ") earlier than given Date of Issue (" + saleDateDTP.Value.Date.ToString("dd-MM-yyyy").Substring(0, 10) + ")", "Error");
                        return;
                    }
                }
                for (int i = 0; i < cartonno_repack.Count; i++)
                {
                    DateTime prod = Convert.ToDateTime(dates_repack[cartonno_repack[i].Item1]);
                    if (saleDateDTP.Value < prod)
                    {
                        c.ErrorBox("Carton number: " + cartonno_repack[i].Item1.Item1 + " ; " + cartonno_repack[i].Item1.Item2 + " ; " + cartonno_repack[i].Item1.Item3 + " (Repacked) at row " + (i + 1).ToString() + " has Date of Billing (" + prod.Date.ToString("dd-MM-yyyy").Substring(0, 10) + ") earlier than given Date of Issue (" + saleDateDTP.Value.Date.ToString("dd-MM-yyyy").Substring(0, 10) + ")", "Error");
                        return;
                    }
                }

                string sql = "begin transaction; begin try; DECLARE @voucherID int;\n";
                sql += "INSERT INTO T_Sales_Voucher (Date_Of_Input,Date_Of_Sale, Quality_ID, Company_ID, Customer_ID, Sale_Rate, Fiscal_Year, Type_Of_Sale, Sale_DO_No, Net_Weight, Narration) VALUES ('" + inputdate + "' ,'" + issueDate + "','" + quality_dict[comboBox1CB.SelectedItem.ToString()] + "', '" + company_dict[comboBox2CB.SelectedItem.ToString()] + "', '" + customer_dict[comboBox3CB.SelectedItem.ToString()] + "', " + float.Parse(rateTextboxTB.Text).ToString("F3") + " , '" + fiscal_year + "', '" + int.Parse(typeCB.Text) + "', '" + this.saleDONoTB.Text + "', " + float.Parse(this.totalWeightTB.Text).ToString("F3") + ", '" + narrationTB.Text + "'); SELECT @voucherID = SCOPE_IDENTITY();\n";
                for (int i = 0; i < cartonno.Count; i++)
                {
                    string comments = "";
                    if (c.Cell_Not_NullOrEmpty(dataGridView1, cartonno[i].Item2, -1, "Comments")) comments = dataGridView1.Rows[cartonno[i].Item2].Cells["Comments"].Value.ToString();
                    sql += "INSERT INTO T_Carton_Sales VALUES ('" + carton_fetch_data[cartonno[i].Item1]["Carton_ID"].ToString() + "', @voucherID, '" + comments + "', " + cartonno[i].Item2 + "," + dataGridView1.Rows[cartonno[i].Item2].Cells[2].Value.ToString() + " );\n";
                    float selling_weight = float.Parse(dataGridView1.Rows[cartonno[i].Item2].Cells[2].Value.ToString());
                    float current_weight = float.Parse(dataGridView1.Rows[cartonno[i].Item2].Cells[3].Value.ToString());
                    int state = 2;
                    if (current_weight - selling_weight == 0F) state = 3;
                    sql += "UPDATE T_Inward_Carton SET Carton_State = " + state.ToString() + " WHERE Carton_ID = '" + carton_fetch_data[cartonno[i].Item1]["Carton_ID"].ToString() + "';\n";
                }
                for (int i = 0; i < cartonno_repack.Count; i++)
                {
                    string comments = "";
                    if (c.Cell_Not_NullOrEmpty(dataGridView1, cartonno_repack[i].Item2, -1, "Comments")) comments = dataGridView1.Rows[cartonno_repack[i].Item2].Cells["Comments"].Value.ToString();
                    sql += "INSERT INTO T_Carton_Sales VALUES ('" + carton_fetch_data_repack[cartonno_repack[i].Item1]["Carton_ID"].ToString() + "', @voucherID, '" + comments + "', " + cartonno_repack[i].Item2 + "," + dataGridView1.Rows[cartonno_repack[i].Item2].Cells[2].Value.ToString() + " );\n";
                    float selling_weight = float.Parse(dataGridView1.Rows[cartonno_repack[i].Item2].Cells[2].Value.ToString());
                    float current_weight = float.Parse(dataGridView1.Rows[cartonno_repack[i].Item2].Cells[3].Value.ToString());
                    int state = 1;
                    if (current_weight - selling_weight == 0F) state = 2;
                    sql += "UPDATE T_Repacked_Cartons SET Carton_State = " + state.ToString() + " WHERE Carton_ID = '" + carton_fetch_data_repack[cartonno_repack[i].Item1]["Carton_ID"].ToString() + "';\n";
                }

                //Enter DO number in Fiscal Year Table
                if (typeCB.Text == "0")
                {
                    sql += "UPDATE Fiscal_Year SET Highest_Trading_0_DO_No = '" + this.saleDONoTB.Text + "' WHERE Fiscal_Year='" + fiscal_year + "';\n";
                }
                else if (typeCB.Text == "1")
                {
                    sql += "UPDATE Fiscal_Year SET Highest_Trading_1_DO_No = '" + this.saleDONoTB.Text + "' WHERE Fiscal_Year='" + fiscal_year + "';\n";
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
                string issueDate = saleDateDTP.Value.Date.ToString("MM-dd-yyyy").Substring(0, 10);
                string fiscal_year = c.getFinancialYear(saleDateDTP.Value);

                //check if bill dates/Production Dates of all cartons are <= issue date
                Dictionary<Tuple<string, float, string>, string> dates = new Dictionary<Tuple<string, float, string>, string>();
                Dictionary<Tuple<string, float, string>, string> dates_repack = new Dictionary<Tuple<string, float, string>, string>();
                for (int i = 0; i < cartonno.Count; i++)
                {
                    DataRow dr = carton_fetch_data[cartonno[i].Item1];
                    dates[cartonno[i].Item1] = dr["Date_Of_Billing"].ToString();
                }
                for (int i = 0; i < cartonno_repack.Count; i++)
                {
                    DataRow dr = carton_fetch_data_repack[cartonno_repack[i].Item1];
                    dates_repack[cartonno_repack[i].Item1] = dr["Date_Of_Production"].ToString();
                }
                for (int i = 0; i < cartonno.Count; i++)
                {
                    DateTime bill = Convert.ToDateTime(dates[cartonno[i].Item1]);
                    if (saleDateDTP.Value < bill)
                    {
                        c.ErrorBox("Carton number: " + cartonno[i].Item1.Item1 + " ; " + cartonno[i].Item1.Item2 + " ; " + cartonno[i].Item1.Item3 + " at row " + (i + 1).ToString() + " has Date of Billing (" + bill.Date.ToString("dd-MM-yyyy").Substring(0, 10) + ") earlier than given Date of Issue (" + saleDateDTP.Value.Date.ToString("dd-MM-yyyy").Substring(0, 10) + ")", "Error");
                        return;
                    }
                }
                for (int i = 0; i < cartonno_repack.Count; i++)
                {
                    DateTime prod = Convert.ToDateTime(dates_repack[cartonno_repack[i].Item1]);
                    if (saleDateDTP.Value < prod)
                    {
                        c.ErrorBox("Carton number: " + cartonno_repack[i].Item1.Item1 + " ; " + cartonno_repack[i].Item1.Item2 + " ; " + cartonno_repack[i].Item1.Item3 + " (Repacked) at row " + (i + 1).ToString() + " has Date of Billing (" + prod.Date.ToString("dd-MM-yyyy").Substring(0, 10) + ") earlier than given Date of Issue (" + saleDateDTP.Value.Date.ToString("dd-MM-yyyy").Substring(0, 10) + ")", "Error");
                        return;
                    }
                }

                foreach (KeyValuePair<Tuple<string, float, string>, bool> entry in carton_data_edit)
                {
                    if (entry.Value == false)
                    {
                        to_delete.Add(entry.Key);
                    }
                }
                foreach (KeyValuePair<Tuple<string, float, string>, bool> entry in carton_data_repack_edit)
                {
                    if (entry.Value == false)
                    {
                        to_delete_repack.Add(entry.Key);
                    }
                }

                string sql = "begin transaction; begin try;\n";

                //Delete cartons
                for(int i=0;i<to_delete.Count;i++)
                {
                    string carton_id = carton_fetch_data[to_delete[i]]["Carton_ID"].ToString();
                    float sold_weight = float.Parse(carton_fetch_data[to_delete[i]]["Sold_Weight"].ToString());
                    DataTable dt = c.runQuery("SELECT SUM(Sold_Weight) FROM T_Carton_Sales WHERE Carton_ID = '" + carton_id + "'");
                    float sold_weight_all = float.Parse(dt.Rows[0][0].ToString());
                    sql += "DELETE FROM T_Carton_Sales WHERE Carton_ID = '" + carton_id + "' AND Sales_Voucher_ID = '" + this.voucher_id + "';\n";
                    if (sold_weight == sold_weight_all) sql += "UPDATE T_Inward_Carton SET Carton_State = 0 WHERE Carton_ID = '" + carton_id + "';\n";   //Unsold
                    else sql += "UPDATE T_Inward_Carton SET Carton_State = 2 WHERE Carton_ID = '" + carton_id + "';\n"; //Partially Sold
                }
                for (int i = 0; i < to_delete_repack.Count; i++)
                {
                    string carton_id = carton_fetch_data_repack[to_delete_repack[i]]["Carton_ID"].ToString();
                    float sold_weight = float.Parse(carton_fetch_data_repack[to_delete_repack[i]]["Sold_Weight"].ToString());
                    DataTable dt = c.runQuery("SELECT SUM(Sold_Weight) FROM T_Carton_Sales WHERE Carton_ID = '" + carton_id + "'");
                    float sold_weight_all = float.Parse(dt.Rows[0][0].ToString());
                    sql += "DELETE FROM T_Carton_Sales WHERE Carton_ID = '" + carton_id + "' AND Sales_Voucher_ID = '" + this.voucher_id + "';\n";
                    if (sold_weight == sold_weight_all) sql += "UPDATE T_Repacked_Cartons SET Carton_State = 0 WHERE Carton_ID = '" + carton_id + "';\n";   //Unsold
                    else sql += "UPDATE T_Repacked_Cartons SET Carton_State = 1 WHERE Carton_ID = '" + carton_id + "';\n"; //Partially Sold
                }

                //Add all New Cartons
                for (int i = 0; i < to_update.Count; i++)
                {
                    string comments = "";
                    if (c.Cell_Not_NullOrEmpty(dataGridView1, i, -1, "Comments")) comments = dataGridView1.Rows[to_update[i].Item2].Cells["Comments"].Value.ToString();
                    DataTable dt = c.runQuery("SELECT * FROM T_Carton_Sales WHERE Carton_ID = '" + carton_fetch_data[to_update[i].Item1]["Carton_ID"].ToString() + "' AND Sales_Voucher_ID = '" + this.voucher_id + "'");
                    
                    if (dt.Rows.Count == 0) sql += "INSERT INTO T_Carton_Sales VALUES ('" + carton_fetch_data[to_update[i].Item1]["Carton_ID"].ToString() + "', '" + this.voucher_id + "', '" + comments + "', '" + to_update[i].Item2 + "','" + dataGridView1.Rows[to_update[i].Item2].Cells[2].Value.ToString() + "' );\n";
                    else sql += "UPDATE T_Carton_Sales SET Sale_Comments = '" + comments + "', Sale_Display_Order = '" + to_update[i].Item2 + "', Sold_Weight = '" + dataGridView1.Rows[to_update[i].Item2].Cells[2].Value.ToString() + "' WHERE Carton_ID = '" + carton_fetch_data[to_update[i].Item1]["Carton_ID"].ToString() + "' AND Sales_Voucher_ID = '" + this.voucher_id + "';\n";
                    
                    float selling_weight = float.Parse(dataGridView1.Rows[to_update[i].Item2].Cells[2].Value.ToString());
                    float current_weight = float.Parse(dataGridView1.Rows[to_update[i].Item2].Cells[3].Value.ToString());
                    int state = 2;
                    if (current_weight - selling_weight == 0F) state = 3;
                    sql += "UPDATE T_Inward_Carton SET Carton_State = " + state.ToString() + " WHERE Carton_ID = '" + carton_fetch_data[to_update[i].Item1]["Carton_ID"].ToString() + "';\n";
                }
                for (int i = 0; i < to_update_repack.Count; i++)
                {
                    string comments = "";
                    if (c.Cell_Not_NullOrEmpty(dataGridView1, i, -1, "Comments")) comments = dataGridView1.Rows[to_update_repack[i].Item2].Cells["Comments"].Value.ToString();

                    DataTable dt = c.runQuery("SELECT * FROM T_Carton_Sales WHERE Carton_ID = '" + carton_fetch_data_repack[to_update_repack[i].Item1]["Carton_ID"].ToString() + "' AND Sales_Voucher_ID = '" + this.voucher_id + "'");

                    if (dt.Rows.Count == 0) sql += "INSERT INTO T_Carton_Sales VALUES ('" + carton_fetch_data_repack[to_update_repack[i].Item1]["Carton_ID"].ToString() + "', '" + this.voucher_id + "', '" + comments + "', '" + to_update_repack[i].Item2 + "','" + dataGridView1.Rows[to_update_repack[i].Item2].Cells[2].Value.ToString() + "' );\n";
                    else sql += "UPDATE T_Carton_Sales SET Sale_Comments = '" + comments + "', Sale_Display_Order = '" + to_update_repack[i].Item2 + "', Sold_Weight = '" + dataGridView1.Rows[to_update_repack[i].Item2].Cells[2].Value.ToString() + "' WHERE Carton_ID = '" + carton_fetch_data_repack[to_update_repack[i].Item1]["Carton_ID"].ToString() + "' AND Sales_Voucher_ID = '" + this.voucher_id + "';\n";

                    float selling_weight = float.Parse(dataGridView1.Rows[to_update_repack[i].Item2].Cells[2].Value.ToString());
                    float current_weight = float.Parse(dataGridView1.Rows[to_update_repack[i].Item2].Cells[3].Value.ToString());
                    int state = 1;
                    if (current_weight - selling_weight == 0F) state = 2;
                    sql += "UPDATE T_Repacked_Cartons SET Carton_State = " + state.ToString() + " WHERE Carton_ID = '" + carton_fetch_data_repack[to_update_repack[i].Item1]["Carton_ID"].ToString() + "';\n";
                }

                sql += "UPDATE T_Sales_Voucher SET Date_Of_Sale='" + issueDate + "', Sale_Rate=" + float.Parse(rateTextboxTB.Text).ToString() + ", Fiscal_Year='" + fiscal_year + "', Type_Of_Sale = " + int.Parse(typeCB.Text).ToString() + ", Net_Weight=" + float.Parse(this.totalWeightTB.Text).ToString() + ", Narration = '" + narrationTB.Text + "' WHERE Voucher_ID='" + this.voucher_id + "';\n";

                //catch
                sql += "commit transaction; end try BEGIN CATCH rollback transaction; \n";
                sql += "DECLARE @ErrorMessage NVARCHAR(4000); DECLARE @ErrorSeverity INT; DECLARE @ErrorState INT; SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE(); \n";
                sql += "RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState); END CATCH; \n";
                DataTable edited = c.runQuery(sql);
                if (edited != null)
                {
                    c.SuccessBox("Voucher Edited Successfully");
                    disable_form_edit();
                    dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.LawnGreen;
                    this.v1_history.loadData();
                }
                else return;
            }
            dataGridView1.EnableHeadersVisualStyles = false;
        }
        private void loadCartonButton_Click(object sender, EventArgs e)
        {
            if (typeCB.Text=="---Select---")
            {
                c.ErrorBox("Enter type of sale", "Error");
                return;
            }
            if (comboBox1CB.Text=="---Select---")
            {
                c.ErrorBox("Select Quality", "Error");
                return;
            }
            if (comboBox2CB.Text=="---Select---")
            {
                c.ErrorBox("Select Company Name", "Error");
                return;
            }
            this.loadData();
            if(this.carton_data.Count-1==0 && this.carton_data_repack.Count-1==0) 
            {
                c.WarningBox("No Cartons Loaded");
                return;
            }
            else
            {
                if (this.edit_form == false)
                {
                    c.SuccessBox("Loaded " + (this.carton_data.Count + this.carton_data_repack.Count).ToString() + " Cartons");
                }
            }
            this.saveButton.Enabled = true;
            this.loadCartonButton.Enabled = false;
            this.typeCB.Enabled = false;
            this.comboBox1CB.Enabled = false;
            this.comboBox2CB.Enabled = false;
            string fiscal_year = c.getFinancialYear(this.saleDateDTP.Value);
            List<int> years = c.getFinancialYearArr(fiscal_year);
            this.saleDateDTP.MinDate = new DateTime(years[0], 04, 01);
            this.saleDateDTP.MaxDate = new DateTime(years[1], 03, 31);
            //this.saleDateDTP.Enabled = false;
            this.saleDONoTB_Value();
        }
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
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
            this.totalWeightTB.Text = CellSum().ToString("F3");
        }
        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (c.check_login_val() == false) return;
            DialogResult dialogResult = System.Windows.Forms.MessageBox.Show("Confirm Delete", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                
                //Delete all cartons
                string sql = "begin transaction; begin try;\n";
                sql += "UPDATE T_Sales_Voucher SET Deleted = 1  WHERE Voucher_ID='" + this.voucher_id + "';\n";

                List<Tuple<Tuple<string, float, string>, int>> cartonno = new List<Tuple<Tuple<string, float, string>, int>>();
                List<Tuple<Tuple<string, float, string>, int>> cartonno_repack = new List<Tuple<Tuple<string, float, string>, int>>();
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    if (!c.Cell_Not_NullOrEmpty(this.dataGridView1, i, 1)) continue;
                    else
                    {
                        List<string> carton = this.split_catron_data(dataGridView1.Rows[i].Cells[1].Value.ToString());
                        if (carton.Count == 3) cartonno.Add(new Tuple<Tuple<string, float, string>, int>(new Tuple<string, float, string>(carton[0], float.Parse(carton[1]), carton[2]), i));
                        if (carton.Count == 4) cartonno_repack.Add(new Tuple<Tuple<string, float, string>, int>(new Tuple<string, float, string>(carton[0], float.Parse(carton[1]), carton[2]), i));
                    }
                }

                for (int i = 0; i < cartonno.Count; i++)
                {
                    string carton_id = carton_fetch_data[cartonno[i].Item1]["Carton_ID"].ToString();
                    float sold_weight = float.Parse(carton_fetch_data[cartonno[i].Item1]["Sold_Weight"].ToString());
                    DataTable dt = c.runQuery("SELECT SUM(Sold_Weight) FROM T_Carton_Sales WHERE Carton_ID = '" + carton_id + "'");
                    float sold_weight_all = float.Parse(dt.Rows[0][0].ToString());
                    sql += "DELETE FROM T_Carton_Sales WHERE Carton_ID = '" + carton_id + "' AND Sales_Voucher_ID = '" + this.voucher_id + "';\n";
                    if (sold_weight == sold_weight_all) sql += "UPDATE T_Inward_Carton SET Carton_State = 0 WHERE Carton_ID = '" + carton_id + "';\n";   //Unsold
                    else sql += "UPDATE T_Inward_Carton SET Carton_State = 2 WHERE Carton_ID = '" + carton_id + "';\n"; //Partially Sold
                }
                for (int i = 0; i < cartonno_repack.Count; i++)
                {
                    string carton_id = carton_fetch_data_repack[cartonno_repack[i].Item1]["Carton_ID"].ToString();
                    float sold_weight = float.Parse(carton_fetch_data_repack[cartonno_repack[i].Item1]["Sold_Weight"].ToString());
                    DataTable dt = c.runQuery("SELECT SUM(Sold_Weight) FROM T_Carton_Sales WHERE Carton_ID = '" + carton_id + "'");
                    float sold_weight_all = float.Parse(dt.Rows[0][0].ToString());
                    sql += "DELETE FROM T_Carton_Sales WHERE Carton_ID = '" + carton_id + "' AND Sales_Voucher_ID = '" + this.voucher_id + "';\n";
                    if (sold_weight == sold_weight_all) sql += "UPDATE T_Repacked_Cartons SET Carton_State = 0 WHERE Carton_ID = '" + carton_id + "';\n";   //Unsold
                    else sql += "UPDATE T_Repacked_Cartons SET Carton_State = 1 WHERE Carton_ID = '" + carton_id + "';\n"; //Partially Sold
                }

                //catch
                sql += "commit transaction; end try BEGIN CATCH rollback transaction; \n";
                sql += "DECLARE @ErrorMessage NVARCHAR(4000); DECLARE @ErrorSeverity INT; DECLARE @ErrorState INT; SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE(); \n";
                sql += "RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState); END CATCH; \n";
                DataTable deleted = c.runQuery(sql);
                if (deleted != null)
                {
                    c.SuccessBox("Voucher Deleted Successfully");
                    this.deleteButton.Enabled = false;
                    this.v1_history.loadData();
                    dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Red;
                    dataGridView1.EnableHeadersVisualStyles = false;
                }
                else return;
            }
            else if (dialogResult == DialogResult.No)
            {
                return;
            }
        }

        //Text changed
        private void totalWeightTB_TextChanged(object sender, EventArgs e)
        {
            this.amountTB_Value();
        }
        private void rateTextboxTB_TextChanged(object sender, EventArgs e)
        {
            this.amountTB_Value();
        }

        //DataGridView 1
        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is DataGridViewComboBoxEditingControl)
            {
                ((ComboBox)e.Control).DropDownStyle = ComboBoxStyle.DropDown;
                ((ComboBox)e.Control).AutoCompleteSource = AutoCompleteSource.ListItems;
                ((ComboBox)e.Control).AutoCompleteMode = AutoCompleteMode.Append;
                ComboBox cb = (ComboBox)e.Control;
                if (this.shadeCB.Text == "---Select---") return;
                
                List<Tuple<string, float, string>> temp=new List<Tuple<string, float, string>>();
                List<Tuple<string, float, string>> temp1=new List<Tuple<string, float, string>>();
                foreach (string item in cb.Items)
                {
                    List<string> carton_data = this.split_catron_data(item);
                    if(carton_data.Count==3)  temp.Add(new Tuple<string, float, string>(carton_data[0], float.Parse(carton_data[1]), carton_data[2]));
                    if(carton_data.Count==4)  temp1.Add(new Tuple<string, float, string>(carton_data[0], float.Parse(carton_data[1]), carton_data[2]));
                }
                foreach (Tuple<string, float, string> item in temp)
                {
                    DataRow dr;
                    this.carton_fetch_data.TryGetValue(item, out dr);
                    if (int.Parse(dr["Colour_ID"].ToString()) != colour_dict[this.shadeCB.Text])
                    {
                        cb.Items.Remove(item.Item1 + " ; " + item.Item2.ToString("F3") + " ; " + item.Item3);
                    }
                }
                foreach (Tuple<string, float, string> item in temp1)
                {
                    DataRow dr;
                    this.carton_fetch_data_repack.TryGetValue(item, out dr);
                    if (int.Parse(dr["Colour_ID"].ToString()) != colour_dict[this.shadeCB.Text])
                    {
                        cb.Items.Remove(item.Item1 + " ; " + item.Item2.ToString("F3") + " ; " + item.Item3 + " (Repacked)");
                    }
                }
            }
        }
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1 && e.RowIndex >= 0)
            {
                if(!c.Cell_Not_NullOrEmpty(this.dataGridView1, e.RowIndex, e.ColumnIndex))
                {
                    dataGridView1.Rows[e.RowIndex].Cells[2].Value = null;
                    dataGridView1.Rows[e.RowIndex].Cells["Shade"].Value = null;
                    this.totalWeightLabel.Text = CellSum().ToString("F3");
                    return;
                }
                List<string> carton_data_changed = this.split_catron_data(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                DataRow dr;
                if (carton_data_changed.Count==3) this.carton_fetch_data.TryGetValue(new Tuple<string, float, string>(carton_data_changed[0], float.Parse(carton_data_changed[1]), carton_data_changed[2]), out dr);
                else this.carton_fetch_data_repack.TryGetValue(new Tuple<string, float, string>(carton_data_changed[0], float.Parse(carton_data_changed[1]), carton_data_changed[2]), out dr);
                float net_weight = float.Parse(dr["Net_Weight"].ToString());
                float sold_weight;
                try
                {
                    sold_weight = float.Parse(dr["Sold_Weight"].ToString());
                }
                catch
                {
                    sold_weight = 0F;
                }
                float total_sold = 0F;
                if(this.edit_form==true)
                {
                    DataTable dt = c.runQuery("SELECT SUM(Sold_Weight) FROM T_Carton_Sales WHERE Carton_ID = '" + dr["Carton_ID"].ToString() + "'");
                    try
                    {
                        total_sold = float.Parse(dt.Rows[0][0].ToString());
                    }
                    catch { }
                }
                float current_weight = net_weight - sold_weight;
                dataGridView1.Rows[e.RowIndex].Cells[2].Value = current_weight.ToString("F3");
                if (this.edit_form == true)
                {
                    if (sold_weight != 0F) dataGridView1.Rows[e.RowIndex].Cells[2].Value = sold_weight.ToString("F3");
                    else dataGridView1.Rows[e.RowIndex].Cells[2].Value = (net_weight - total_sold + sold_weight).ToString("F3");
                }
                dataGridView1.Rows[e.RowIndex].Cells[3].Value = current_weight.ToString("F3");
                if (this.edit_form == true) dataGridView1.Rows[e.RowIndex].Cells[3].Value = (net_weight - total_sold + sold_weight).ToString("F3");
                dataGridView1.Rows[e.RowIndex].Cells["Shade"].Value = colour_dict_reverse[int.Parse(dr["Colour_ID"].ToString())];
                this.totalWeightTB.Text = CellSum().ToString("F3");
            }
        }
        private void dataGridView1_RowPostPaint_1(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            if (e.RowIndex != dataGridView1.Rows.Count - 1)
            {
                dataGridView1.Rows[e.RowIndex].Cells[0].Value = e.RowIndex + 1;
            }
        }
        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (dataGridView1.Enabled == false || dataGridView1.ReadOnly == true)
            {
                return;
            }
            if (e.KeyCode == Keys.Tab &&
            (dataGridView1.SelectedCells.Cast<DataGridViewCell>().Any(x => x.ColumnIndex == 1) || this.edit_cmd_send == true))
            {
                bool edit_cmd_local = this.edit_cmd_send;
                this.edit_cmd_send = false;
                int rowindex_tab = dataGridView1.SelectedCells[0].RowIndex;
                //if (edit_cmd_local == true) rowindex_tab--;
                Console.WriteLine("row index "+rowindex_tab);

                if (rowindex_tab < 0)
                {
                    SendKeys.Send("{tab}");
                    return;
                }
                if (dataGridView1.Rows.Count - 2 == rowindex_tab)
                {
                    DataGridViewRow row = (DataGridViewRow)dataGridView1.Rows[rowindex_tab].Clone();
                    dataGridView1.Rows.Add(row);
                }
                if (edit_cmd_local == false)
                {
                    SendKeys.Send("{tab}");
                }
            }
            if (e.KeyCode == Keys.Tab &&
               (dataGridView1.SelectedCells.Cast<DataGridViewCell>().Any(x => x.ColumnIndex == 2)))
            {
                int rowindex_tab = dataGridView1.SelectedCells[0].RowIndex;
                if (rowindex_tab < 0)
                {
                    SendKeys.Send("{tab}");
                    SendKeys.Send("{tab}");
                    return;
                }
                if (dataGridView1.Rows.Count - 2 == rowindex_tab)
                {
                    DataGridViewRow row = (DataGridViewRow)dataGridView1.Rows[rowindex_tab].Clone();
                    dataGridView1.Rows.Add(row);
                }
                SendKeys.Send("{tab}");
                SendKeys.Send("{tab}");
            }
            if (e.KeyCode == Keys.Enter &&
               (dataGridView1.SelectedCells.Cast<DataGridViewCell>().Any(x => x.ColumnIndex == 1) || this.edit_cmd_send == true))
            {
                dataGridView1.BeginEdit(true);
                ComboBox c = (ComboBox)dataGridView1.EditingControl;
                if(c!=null) c.DroppedDown = true;
                e.Handled = true;
            }
        }
        private void dataGridView1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex != dataGridView1.Rows.Count - 1)
            {
                dataGridView1.Rows[e.RowIndex].Cells[0].Value = e.RowIndex + 1;
            }
        }
        private void dataGridView1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dataGridView1.IsCurrentCellDirty)
            {
                dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
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
