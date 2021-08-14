using Factory_Inventory.Factory_Classes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
//using System.Windows.Controls;
using System.Windows.Forms;
using static Factory_Inventory.Factory_Classes.Structures;

//Delete Content Menu Strip in DGV2. Redundant ifs in DGV1 Key Down. Save Button
namespace Factory_Inventory
{
    public partial class T_V1_cartonInwardForm : Form
    {
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Tab &&
                dataGridView1.EditingControl != null &&
                msg.HWnd == dataGridView1.EditingControl.Handle &&
                dataGridView1.SelectedCells
                    .Cast<DataGridViewCell>()
                    .Any(x => x.ColumnIndex == 5))
            {
                this.edit_cmd_send = true;
                SendKeys.Send("{Tab}");
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
                this.billDateDTP.Focus();
                this.ActiveControl = billDateDTP;
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
        bool billDateChanged = false;
        private bool edit_cmd_send = false;
        private bool edit_form = false;
        private string oldbillno;
        private int voucher_id = -1;
        Dictionary<string, bool> carton_editable = new Dictionary<string, bool>();  
        Dictionary<string, bool> cartons_to_edit = new Dictionary<string, bool>();   //Stores Carton IDs in edit mode. Value is false means the Carton_ID has to be deleted in Inward_Cartons table
        Dictionary<string, int> company_dict = new Dictionary<string, int>();
        Dictionary<string, int> quality_dict = new Dictionary<string, int>();
        Dictionary<string, int> colour_dict = new Dictionary<string, int>();
        private M_V_history v1_history;
        bool inputerror1 = false; //to stop movement of tab in non numeric weight etc, not done yet
        public T_V1_cartonInwardForm()
        {
            this.c = new DbConnect();
            InitializeComponent();
            dataGridView1.RowCount = 10;
            dataGridView1.ColumnCount = 4;
            dataGridView1.Columns[0].HeaderText = "Sl No";
            dataGridView1.Columns[1].HeaderText = "Carton No";
            dataGridView1.Columns[2].HeaderText = "Weight";
            dataGridView1.Columns[3].HeaderText = "Comments";
            dataGridView1.Columns[0].Name = "Sl_No";
            dataGridView1.Columns[1].Name = "Carton_No";
            dataGridView1.Columns[2].Name = "Weight";
            dataGridView1.Columns[3].Name = "Comments";
            dataGridView1.Columns[0].ReadOnly = true;
            this.billDateChanged = false;
            this.label7.Text = "";

            //Quality Combobox columns
            DataTable d1 = c.runQuery("SELECT * FROM T_M_Quality_Before_Job");
            List<string> input_qualities = new List<string>();
            for (int i = 0; i < d1.Rows.Count; i++)
            {
                input_qualities.Add(d1.Rows[i]["Quality_Before_Job"].ToString());
                quality_dict[d1.Rows[i]["Quality_Before_Job"].ToString()] = int.Parse(d1.Rows[i]["Quality_Before_Job_ID"].ToString());
            }
            List<string> final_list1 = input_qualities.Distinct().ToList();
            DataGridViewComboBoxColumn dgvCmb1 = new DataGridViewComboBoxColumn();
            dgvCmb1.HeaderText = "Quality";
            for (int i = 0; i < final_list1.Count; i++)
            {
                dgvCmb1.Items.Add(final_list1[i]);
            }
            dgvCmb1.Name = "Quality";
            dataGridView1.Columns.Insert(1, dgvCmb1);

            //Colour Combobox columns
            DataTable d2 = c.runQuery("SELECT * FROM T_M_Colours");
            List<string> input_colours= new List<string>();
            for (int i = 0; i < d2.Rows.Count; i++)
            {
                input_colours.Add(d2.Rows[i]["Colour"].ToString());
                colour_dict[d2.Rows[i]["Colour"].ToString()] = int.Parse(d2.Rows[i]["Colour_ID"].ToString());
            }
            List<string> final_list2 = input_colours.Distinct().ToList();
            DataGridViewComboBoxColumn dgvCmb3 = new DataGridViewComboBoxColumn();
            dgvCmb3.HeaderText = "Colour";
            for (int i = 0; i < final_list2.Count; i++)
            {
                dgvCmb3.Items.Add(final_list2[i]);
            }
            dgvCmb3.Name = "Colour";
            dataGridView1.Columns.Insert(2, dgvCmb3);

            //Grade Combobox Column
            DataGridViewComboBoxColumn dgvCmb5 = new DataGridViewComboBoxColumn();
            dgvCmb5.HeaderText = "Grade";
            dgvCmb5.Items.Add("1st");
            dgvCmb5.Items.Add("PQ");
            dgvCmb5.Items.Add("CLQ");
            dgvCmb5.Name = "Grade";
            dataGridView1.Columns.Insert(3, dgvCmb5);

            //Company CB
            var dataSource2 = new List<string>();
            DataTable d3 = c.runQuery("SELECT * FROM T_M_Company_Names");
            dataSource2.Add("---Select---");
            for (int i = 0; i < d3.Rows.Count; i++)
            {
                dataSource2.Add(d3.Rows[i]["Company_Name"].ToString());
                company_dict[d3.Rows[i]["Company_Name"].ToString()] = int.Parse(d3.Rows[i]["Company_ID"].ToString());
            }
            this.comboBox2CB.DataSource = dataSource2;
            this.comboBox2CB.DisplayMember = "Company_Name";
            this.comboBox2CB.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.comboBox2CB.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.comboBox2CB.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            //Type CB
            this.typeCB.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.typeCB.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.typeCB.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            c.set_dgv_column_sort_state(this.dataGridView1, DataGridViewColumnSortMode.NotSortable);
            c.set_dgv_column_sort_state(this.dataGridView2, DataGridViewColumnSortMode.NotSortable);
            inputDate.Enabled = false;
            this.edit_form = false;
        }
        public T_V1_cartonInwardForm(DataRow row, bool isEditable, M_V_history v1_history)
        {
            InitializeComponent();
            this.edit_form = true;
            this.v1_history = v1_history;
            this.c = new DbConnect();
            this.billDateChanged = false;

            //Initialize datagridview1
            dataGridView1.RowCount = 10;
            dataGridView1.ColumnCount = 5;
            dataGridView1.Columns[0].HeaderText = "Sl No";
            dataGridView1.Columns[1].HeaderText = "Carton No";
            dataGridView1.Columns[2].HeaderText = "Weight";
            dataGridView1.Columns[3].HeaderText = "Comments";
            dataGridView1.Columns[4].HeaderText = "Carton_ID";  //Hidden column for Carton ID
            dataGridView1.Columns[0].Name = "Sl_No";
            dataGridView1.Columns[1].Name = "Carton_No";
            dataGridView1.Columns[2].Name = "Weight";
            dataGridView1.Columns[3].Name = "Comments";
            dataGridView1.Columns[4].Name = "Carton_ID";
            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[4].Visible = false;

            //Quality Combobox columns
            DataTable d1 = c.runQuery("SELECT * FROM T_M_Quality_Before_Job");
            List<string> input_qualities = new List<string>();
            for (int i = 0; i < d1.Rows.Count; i++)
            {
                input_qualities.Add(d1.Rows[i]["Quality_Before_Job"].ToString());
                quality_dict[d1.Rows[i]["Quality_Before_Job"].ToString()] = int.Parse(d1.Rows[i]["Quality_Before_Job_ID"].ToString());
            }
            List<string> final_list1 = input_qualities.Distinct().ToList();
            DataGridViewComboBoxColumn dgvCmb1 = new DataGridViewComboBoxColumn();
            dgvCmb1.HeaderText = "Quality";
            for (int i = 0; i < final_list1.Count; i++)
            {
                dgvCmb1.Items.Add(final_list1[i]);
            }
            dgvCmb1.Name = "Quality";
            dataGridView1.Columns.Insert(1, dgvCmb1);

            //Colour Combobox columns
            DataTable d2 = c.runQuery("SELECT * FROM T_M_Colours");
            List<string> input_colours = new List<string>();
            for (int i = 0; i < d2.Rows.Count; i++)
            {
                input_colours.Add(d2.Rows[i]["Colour"].ToString());
                colour_dict[d2.Rows[i]["Colour"].ToString()] = int.Parse(d2.Rows[i]["Colour_ID"].ToString());
            }
            List<string> final_list2 = input_colours.Distinct().ToList();
            DataGridViewComboBoxColumn dgvCmb3 = new DataGridViewComboBoxColumn();
            dgvCmb3.HeaderText = "Colour";
            for (int i = 0; i < final_list2.Count; i++)
            {
                dgvCmb3.Items.Add(final_list2[i]);
            }
            dgvCmb3.Name = "Colour";
            dataGridView1.Columns.Insert(2, dgvCmb3);

            //Grade Combobox Column
            DataGridViewComboBoxColumn dgvCmb5 = new DataGridViewComboBoxColumn();
            dgvCmb5.HeaderText = "Grade";
            dgvCmb5.Items.Add("1st");
            dgvCmb5.Items.Add("PQ");
            dgvCmb5.Items.Add("CLQ");
            dgvCmb5.Name = "Grade";
            dataGridView1.Columns.Insert(3, dgvCmb5);

            var dataSource2 = new List<string>();
            var dataSource3 = new List<string>();
            if (isEditable == false)
            {
                this.Text += " (View Only)";
                this.deleteButton.Visible = true;
                dataSource2.Add(row["Company_Name"].ToString());
                this.comboBox2CB.DataSource = dataSource2;
                this.comboBox2CB.DisplayMember = "Company_Names";
                dataSource3.Add(row["Inward_Type"].ToString());
                this.typeCB.DataSource = dataSource3;
                this.typeCB.DisplayMember = "Type";
                this.disable_form_edit();   
            }
            else
            {
                this.Text += " (Edit)";
                this.inputDate.Enabled = false;
                this.billDateDTP.Enabled = true;
                this.billNumberTextboxTB.Enabled = true;
                this.comboBox2CB.Enabled = true;
                this.typeCB.Enabled = true;
                this.saveButton.Enabled = true;
                this.narrationTB.Enabled = true;

                //Company CB
                var dataSource4 = new List<string>();
                DataTable d3 = c.runQuery("SELECT * FROM T_M_Company_Names");
                dataSource4.Add("---Select---");
                for (int i = 0; i < d3.Rows.Count; i++)
                {
                    dataSource4.Add(d3.Rows[i]["Company_Name"].ToString());
                    company_dict[d3.Rows[i]["Company_Name"].ToString()] = int.Parse(d3.Rows[i]["Company_ID"].ToString());
                }
                this.comboBox2CB.DataSource = dataSource4;
                if (this.comboBox2CB.FindStringExact(row["Company_Name"].ToString()) == -1)
                {
                    dataSource2.Add(row["Company_Name"].ToString());
                    this.comboBox2CB.DataSource = null;
                    this.comboBox2CB.DataSource = dataSource2;

                }
                this.comboBox2CB.DisplayMember = "Company_Names";
                this.comboBox2CB.SelectedIndex = this.comboBox2CB.FindStringExact(row["Company_Name"].ToString());

                //Type CB
                this.typeCB.SelectedIndex = this.typeCB.FindStringExact(row["Inward_Type"].ToString());
            }

            //Company CB
            this.comboBox2CB.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.comboBox2CB.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.comboBox2CB.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            //Type CB
            this.typeCB.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.typeCB.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.typeCB.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            this.voucher_id = int.Parse(row["Voucher_ID"].ToString());
            this.inputDate.Value = Convert.ToDateTime(row["Date_Of_Input"].ToString());
            this.billDateDTP.Value = Convert.ToDateTime(row["Date_Of_Billing"].ToString());
            this.billNumberTextboxTB.Text = row["Bill_No"].ToString();
            this.oldbillno = billNumberTextboxTB.Text;

            string sql = "SELECT temp.*, T_M_Colours.Colour\n";
            sql += "FROM\n";
            sql += "    (SELECT T_Inward_Carton.*, T_M_Quality_Before_Job.Quality_Before_Job\n";
            sql += "    FROM T_Inward_Carton\n";
            sql += "    LEFT OUTER JOIN T_M_Quality_Before_Job\n";
            sql += "    ON T_Inward_Carton.Quality_ID = T_M_Quality_Before_Job.Quality_Before_Job_ID) as temp\n";
            sql += "LEFT OUTER JOIN T_M_Colours\n";
            sql += "ON temp.Colour_ID = T_M_Colours.Colour_ID\n";
            sql += "WHERE temp.Inward_Voucher_ID = " + this.voucher_id.ToString() + "\n";
            sql += "ORDER BY temp.ID ASC\n";
            DataTable d4 = c.runQuery(sql);
            c.printDataTable(d4);
            dataGridView1.RowCount = d4.Rows.Count + 1;

            bool flag = false;
            this.label7.Text = "";
            string carton_financial_year = row["Fiscal_Year"].ToString();
            Dictionary<Tuple<string, string>, string> qc_d = new Dictionary<Tuple<string, string>, string>(); ;
            //Load Data into datagridview1
            for (int i=0;i< d4.Rows.Count; i++)
            {
                //Set Values
                dataGridView1.Rows[i].Cells["Quality"].Value = d4.Rows[i]["Quality_Before_Job"].ToString();
                dataGridView1.Rows[i].Cells["Colour"].Value = d4.Rows[i]["Colour"].ToString();
                dataGridView1.Rows[i].Cells["Grade"].Value = d4.Rows[i]["Grade"].ToString();
                dataGridView1.Rows[i].Cells["Carton_No"].Value = d4.Rows[i]["Carton_No"].ToString();
                dataGridView1.Rows[i].Cells["Weight"].Value = d4.Rows[i]["Net_Weight"].ToString();
                dataGridView1.Rows[i].Cells["Comments"].Value = d4.Rows[i]["Comments"].ToString();
                dataGridView1.Rows[i].Cells["Carton_ID"].Value = d4.Rows[i]["Carton_ID"].ToString();
                cartons_to_edit[d4.Rows[i]["Carton_ID"].ToString()] = false;

                //Fill rates in qc_d for quality and colour
                Tuple<string, string> temp = new Tuple<string, string>(d4.Rows[i]["Quality_Before_Job"].ToString(), d4.Rows[i]["Colour"].ToString());
                qc_d[temp] = d4.Rows[i]["Buy_Cost"].ToString();

                //Check if this carton is issued
                int carton_state = -1;
                try
                {
                    carton_state = int.Parse(d4.Rows[i]["Carton_State"].ToString());
                }
                catch(Exception e)
                {
                    c.ErrorBox("Critical Error, Carton Not found: " + d4.Rows[i]["Carton_No"].ToString() + "\n" + e.Message, "Error");
                }
                if(carton_state>0)
                {
                    flag = true;
                    this.carton_editable[d4.Rows[i]["Carton_ID"].ToString()] = false;
                    DataGridViewRow r = dataGridView1.Rows[i];
                    dataGridView1.Rows[i].ReadOnly = true;
                    if (carton_state == 1)
                    {
                        //Repacking
                        r.DefaultCellStyle.BackColor = Color.LightGray;
                        r.DefaultCellStyle.SelectionBackColor = Color.Gray;
                    }
                    else if (carton_state == 2)
                    {
                        //Sold
                        r.DefaultCellStyle.BackColor = Color.LightGreen;
                        r.DefaultCellStyle.SelectionBackColor = Color.Green;
                    }
                    else if (carton_state == 3)
                    {
                        //Sent to Job
                        r.DefaultCellStyle.BackColor = Color.Yellow;
                        r.DefaultCellStyle.SelectionBackColor = Color.DarkGoldenrod;
                    }
                }
            }
            if (flag)
            {
                comboBox2CB.Enabled = false;
                this.deleteButton.Enabled = false;
                this.label7.Text = "This carton cannot be edited as some cartons have been sold or sent for repacking/job.\nRepacking: Light Gray    Sold: Light Green    Job: Yellow";
            }
            else if(isEditable==false)
            {
                //enable delete if none of the cartons are in states 2 or 3
                this.deleteButton.Enabled = true;
            }

            //Load Data into DataGridView2
            generate_dgv2(qc_d);
            Console.WriteLine(qc_d.Keys.Count);
            update_weights_rates();

            c.set_dgv_column_sort_state(this.dataGridView1, DataGridViewColumnSortMode.NotSortable);
            c.set_dgv_column_sort_state(this.dataGridView2, DataGridViewColumnSortMode.NotSortable);

            List<int> years = c.getFinancialYearArr(row["Fiscal_Year"].ToString());
            this.billDateDTP.MinDate = new DateTime(years[0], 04, 01);
            this.billDateDTP.MaxDate = new DateTime(years[1], 03, 31);
        }
        private void M_V1_cartonInwardForm_Load(object sender, EventArgs e)
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

            this.billDateDTP.Focus();
            if (Global.access == 2)
            {
                this.deleteButton.Visible = false;
            }
            //Double Buffer DGV
            if (!System.Windows.Forms.SystemInformation.TerminalServerSession)
            {
                Type dgvType = dataGridView1.GetType();
                PropertyInfo pi = dgvType.GetProperty("DoubleBuffered",
                BindingFlags.Instance | BindingFlags.NonPublic);
                pi.SetValue(dataGridView1, true, null);
            }
        }
        
        //self fns
        void generate_dgv2(Dictionary<Tuple<string, string>, string> qc_d_param = null)
        {
            //< <quality, colour>, Price/kg>. Dictionary is used for refilling previously filled rates
            Dictionary<Tuple<string, string>, string> qc_d;
            if (qc_d_param == null) qc_d = new Dictionary<Tuple<string, string>, string>();
            else qc_d = new Dictionary<Tuple<string, string>, string>(qc_d_param); 
            
            for (int i=0;i<dataGridView2.Rows.Count;i++)
            {
                Tuple<string, string> temp = new Tuple<string, string>(dataGridView2.Rows[i].Cells["Quality"].Value.ToString(), dataGridView2.Rows[i].Cells["Colour"].Value.ToString());
                string cost = "";
                if (c.Cell_Not_NullOrEmpty(dataGridView2, i, -1, "Cost") == true) cost = dataGridView2.Rows[i].Cells["Cost"].Value.ToString();
                qc_d[temp] = cost;
            }
            dataGridView2.Rows.Clear();
            HashSet<Tuple<string, string>> qc_hs = new HashSet<Tuple<string, string>>();  
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                int count = 2;

                //ComboBox c = (ComboBox)dataGridView1.EditingControl;
                if (dataGridView1.Rows[i].Cells[1].Value == null || dataGridView1.Rows[i].Cells[1].Value.ToString() == "---Select---")
                    count--;
                if (dataGridView1.Rows[i].Cells[2].Value == null || dataGridView1.Rows[i].Cells[2].Value.ToString() == "---Select---")
                    count--;
                Console.WriteLine("Count: " + count);
                if (count == 2)
                {
                    Tuple<string, string> temp = new Tuple<string, string>(dataGridView1.Rows[i].Cells["Quality"].Value.ToString(), dataGridView1.Rows[i].Cells["Colour"].Value.ToString());
                    if (!qc_hs.Contains(temp))
                    {
                        qc_hs.Add(temp);
                        string cost = "";
                        if (qc_d.ContainsKey(temp)) cost = qc_d[temp].ToString();
                        dataGridView2.Rows.Add(temp.Item1, temp.Item2, cost);
                    }
                }
            }
        }
        void update_weights_rates()
        {
            //Updates total and individual weights and amounts
            dynamicWeightTB.Text = CellSum("","").ToString("F3");
            float net_rate = 0F;
            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                float sum = 0F;
                if (c.Cell_Not_NullOrEmpty(dataGridView2, i, 0) == true && c.Cell_Not_NullOrEmpty(dataGridView2, i, 1) == true)   sum = CellSum(dataGridView2.Rows[i].Cells[0].Value.ToString(), dataGridView2.Rows[i].Cells[1].Value.ToString());
                if (sum != 0F)
                {
                    dataGridView2.Rows[i].Cells[3].Value = sum.ToString("F3");
                    if (c.Cell_Not_NullOrEmpty(dataGridView2, i, 2) == true)
                    {
                        dataGridView2.Rows[i].Cells[4].Value = sum * float.Parse(dataGridView2.Rows[i].Cells[2].Value.ToString());
                        net_rate += float.Parse(dataGridView2.Rows[i].Cells[4].Value.ToString());
                    }
                    else dataGridView2.Rows[i].Cells[4].Value = null;
                }
            }
            label4.Text = net_rate.ToString();
        }
        private void disable_form_edit()
        {
            this.lockCartonsCK.Enabled = false;
            typeCB.Enabled = false;
            this.deleteToolStripMenuItem.Enabled = false;
            this.inputDate.Enabled = false;
            this.billDateDTP.Enabled = false;
            this.billNumberTextboxTB.ReadOnly = true;
            this.comboBox2CB.Enabled = false;
            this.saveButton.Enabled = false;
            this.dataGridView1.ReadOnly = true;
            this.dataGridView2.ReadOnly = true;
            this.narrationTB.ReadOnly = true;
        }
        private float CellSum(string quality, string colour)
        {
            //Counts sum of weights of same quality and colour
            //If no quality is specified, then returns sum of all
            float sum = 0F;
            if (quality == "")
            {
                try
                {
                    if (dataGridView1.Rows.Count == 0)
                    {
                        return sum;
                    }
                    for (int i = 0; i < dataGridView1.Rows.Count; ++i)
                    {
                        if (dataGridView1.Rows[i].Cells[5].Value != null)
                            sum += float.Parse(dataGridView1.Rows[i].Cells[5].Value.ToString());
                    }
                    return sum;
                }
                catch
                {
                    return sum;
                }
            }
            else
            {
                try
                {
                    if (dataGridView1.Rows.Count == 0)
                    {
                        return sum;
                    }
                    for (int i = 0; i < dataGridView1.Rows.Count; ++i)
                    {
                        if (c.Cell_Not_NullOrEmpty(dataGridView1, i, 1) == false || c.Cell_Not_NullOrEmpty(dataGridView1, i, 2) == false) continue;
                        if (dataGridView1.Rows[i].Cells[5].Value != null && dataGridView1.Rows[i].Cells[1].Value.ToString() == quality && dataGridView1.Rows[i].Cells[2].Value.ToString() == colour)
                            sum += float.Parse(dataGridView1.Rows[i].Cells[5].Value.ToString());
                    }
                    return sum;
                }
                catch
                {
                    return sum;
                }
            }
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
                generate_dgv2();
                update_weights_rates();
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
                    if(dataGridView1.Rows[rowindex].Cells["Carton_ID"].Value == null)
                    {
                        dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);
                        continue;
                    }
                    string carton_id = dataGridView1.Rows[rowindex].Cells["Carton_ID"].Value.ToString();
                    bool value = true;
                    bool value2 = this.carton_editable.TryGetValue(carton_id, out value);
                    if (value2 == true && value == false)
                    {
                        c.ErrorBox("Cannot delete entry at row: " + (rowindex + 1).ToString(), "Error");
                        dataGridView1.Rows[rowindex].Selected = false;
                        continue;
                    }
                    dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);
                }
                generate_dgv2();
                update_weights_rates();
            }
        }
        private void saveButton_Click(object sender, EventArgs e)
        {
            if (c.check_login_val() == false) return;
            //checks
            if (billNumberTextboxTB.Text == "")
            {
                c.ErrorBox("Enter Bill Number", "Error");
                return;
            }
            if (comboBox2CB.SelectedIndex == 0)
            {
                c.ErrorBox("Select Company Name", "Error");
                return;
            }
            if (typeCB.SelectedIndex < 0)
            {
                c.ErrorBox("Select Type", "Error");
                return;
            }
            if (dataGridView1.Rows.Count < 0)
            {
                c.ErrorBox("Please enter Carton Numbers and Weights", "Error");
                return;
            }
            if (dataGridView2.Rows.Count < 0)
            {
                c.ErrorBox("Please enter atleast one value in 'Cost per kg' table", "Error");
                return;
            }
            if (DateTime.Now.Date<this.billDateDTP.Value.Date)
            {
                c.ErrorBox("Bill Date is in the future", "Error");
                return;
            }

            Console.WriteLine(dataGridView2.Rows.Count);

            //Check if all rates are entered in DGV2
            Dictionary<Tuple<string, string>, float> rate = new Dictionary<Tuple<string, string>, float>();
            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                if (c.Cell_Not_NullOrEmpty(dataGridView2, i, -1, "Cost") == false)
                {
                    c.ErrorBox("Please enter Price/kg in 'Cost per kg' table at row " + (i + 1).ToString());
                    return;
                }
                rate[new Tuple<string, string>(dataGridView2.Rows[i].Cells["Quality"].Value.ToString(), dataGridView2.Rows[i].Cells["Colour"].Value.ToString())] = float.Parse(dataGridView2.Rows[i].Cells["Cost"].Value.ToString());
            }

            //Iterate to check for mistakes in dataGridView1
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                int count = 0;

                //ComboBox c = (ComboBox)dataGridView1.EditingControl;
                if (dataGridView1.Rows[i].Cells[1].Value == null || dataGridView1.Rows[i].Cells[1].Value.ToString() == "---Select---")
                    count++;
                if (dataGridView1.Rows[i].Cells[2].Value == null || dataGridView1.Rows[i].Cells[2].Value.ToString() == "---Select---")
                    count++;
                if (dataGridView1.Rows[i].Cells["Carton_No"].Value == null)
                    count++;
                if (dataGridView1.Rows[i].Cells["Grade"].Value == null || dataGridView1.Rows[i].Cells["Grade"].Value.ToString() == "---Select---")
                    count++;
                if (dataGridView1.Rows[i].Cells[5].Value == null)
                    count++;
                Console.WriteLine("Count: " + count);

                if (count != 0 && count !=5)
                {
                    c.ErrorBox("Error at row " + (i + 1).ToString(), "Error");
                    return;
                }
            }
            
            if (this.edit_form == false)
            {
                string input_date = inputDate.Value.ToString("yyyy-MM-dd");
                string bill_date = billDateDTP.Value.ToString("yyyy-MM-dd");
                string fiscal_year = c.getFinancialYear(billDateDTP.Value);
                int type = int.Parse(typeCB.Text);
                string sql = "begin transaction; begin try; ";
                sql += "DECLARE @voucherID int; INSERT INTO T_Carton_Inward_Voucher (Date_Of_Input, Type, Date_Of_Billing, Bill_No, Company_ID, Fiscal_Year, Narration) VALUES ('" + input_date + "'," + type + ", '" + bill_date + "', '" + billNumberTextboxTB.Text + "', " + company_dict[comboBox2CB.SelectedItem.ToString()] + ", '" + fiscal_year + "','" + narrationTB.Text + "'); ";
                sql += "SELECT @voucherID = SCOPE_IDENTITY(); ";
                for(int i=0;i<dataGridView1.Rows.Count;i++)
                {
                    int count = 0;

                    //ComboBox c = (ComboBox)dataGridView1.EditingControl;
                    if (dataGridView1.Rows[i].Cells[1].Value == null || dataGridView1.Rows[i].Cells[1].Value.ToString() == "---Select---")
                        count++;
                    if (dataGridView1.Rows[i].Cells[2].Value == null || dataGridView1.Rows[i].Cells[2].Value.ToString() == "---Select---")
                        count++;
                    if (dataGridView1.Rows[i].Cells["Carton_No"].Value == null)
                        count++;
                    if (dataGridView1.Rows[i].Cells["Grade"].Value == null || dataGridView1.Rows[i].Cells["Grade"].Value.ToString() == "---Select---")
                        count++;
                    if (dataGridView1.Rows[i].Cells[5].Value == null)
                        count++;
                    Console.WriteLine("Count: " + count);

                    if (count == 0)
                    {
                        string quality = dataGridView1.Rows[i].Cells["Quality"].Value.ToString();
                        string colour = dataGridView1.Rows[i].Cells["Colour"].Value.ToString();
                        string grade = dataGridView1.Rows[i].Cells["Grade"].Value.ToString();
                        string comments = "";
                        if (c.Cell_Not_NullOrEmpty(dataGridView1, i, -1, "Comments") == true) comments = dataGridView1.Rows[i].Cells["Comments"].Value.ToString();
                        float weight = float.Parse(dataGridView1.Rows[i].Cells["Weight"].Value.ToString());
                        
                        sql += "INSERT INTO T_Inward_Carton (Carton_No, Carton_State, Quality_ID, Colour_ID, Company_ID, Net_Weight, Buy_Cost, Fiscal_Year, Inward_Voucher_ID, Comments, Inward_Type, Grade) ";
                        sql += "VALUES ('" + dataGridView1.Rows[i].Cells["Carton_No"].Value.ToString() + "', 0, " + quality_dict[quality] + ", " + colour_dict[colour] + ", " + company_dict[comboBox2CB.SelectedItem.ToString()] + ", " + weight + ", " + rate[new Tuple<string, string>(quality, colour)] + ", '" + fiscal_year + "', @voucherID, '" + comments + "', " + type + ", '" + grade + "'); ";
                    }
                    
                }
                //catch
                sql += "commit transaction; end try BEGIN CATCH rollback transaction; ";
                sql += "DECLARE @ErrorMessage NVARCHAR(4000); DECLARE @ErrorSeverity INT; DECLARE @ErrorState INT; SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE(); ";
                sql += "RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState); END CATCH; ";
                ErrorTable et = c.runQuerywithError(sql);

                if (et.dt != null)
                {
                    dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.LawnGreen;
                    c.SuccessBox("Voucher Added Successfully");
                    disable_form_edit();
                }
                else if(et.e.State== 10)
                {
                    //primary key violation
                    Global.ErrorBox("Repeated Carton Number with the same Quality, Colour, Company Name, Weight and Financial Year entered\n" + et.e.Message);
                    return;
                }
                else
                {
                    Global.ErrorBox("Could not save voucher:\n" + et.e.Message);
                    return;
                }
            }
            else
            {
                string bill_date = billDateDTP.Value.ToString("yyyy-MM-dd");
                string fiscal_year = c.getFinancialYear(billDateDTP.Value);
                int type = int.Parse(typeCB.Text);
                string sql = "begin transaction; begin try;\n";
                sql += "UPDATE T_Carton_Inward_Voucher SET Type = " + type + ", Date_Of_Billing = '" + bill_date + "', Bill_No = '" + billNumberTextboxTB.Text + "', Company_ID = " + company_dict[comboBox2CB.SelectedItem.ToString()] + ", Fiscal_Year = '" + fiscal_year + "', Narration = '" + narrationTB.Text + "' WHERE Voucher_ID = " + this.voucher_id + ";\n";
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    int count = 0;
                    //ComboBox c = (ComboBox)dataGridView1.EditingControl;
                    if (dataGridView1.Rows[i].Cells[1].Value == null || dataGridView1.Rows[i].Cells[1].Value.ToString() == "---Select---")
                        count++;
                    if (dataGridView1.Rows[i].Cells[2].Value == null || dataGridView1.Rows[i].Cells[2].Value.ToString() == "---Select---")
                        count++;
                    if (dataGridView1.Rows[i].Cells["Carton_No"].Value == null)
                        count++;
                    if (dataGridView1.Rows[i].Cells["Grade"].Value == null || dataGridView1.Rows[i].Cells["Grade"].Value.ToString() == "---Select---")
                        count++;
                    if (dataGridView1.Rows[i].Cells[5].Value == null)
                        count++;
                    Console.WriteLine("Count: " + count);

                    if (count == 0)
                    {
                        string quality = dataGridView1.Rows[i].Cells["Quality"].Value.ToString();
                        string colour = dataGridView1.Rows[i].Cells["Colour"].Value.ToString();
                        float weight = float.Parse(dataGridView1.Rows[i].Cells["Weight"].Value.ToString());
                        string grade = dataGridView1.Rows[i].Cells["Grade"].Value.ToString();
                        string comments = "";
                        if (c.Cell_Not_NullOrEmpty(dataGridView1, i, -1, "Comments") == true) comments = dataGridView1.Rows[i].Cells["Comments"].Value.ToString();
                        
                        if (c.Cell_Not_NullOrEmpty(dataGridView1, i, -1, "Carton_ID") == true)
                        {
                            string carton_id = dataGridView1.Rows[i].Cells["Carton_ID"].Value.ToString();
                            cartons_to_edit[carton_id] = true;
                            sql += "UPDATE T_Inward_Carton SET Carton_No = '" + dataGridView1.Rows[i].Cells["Carton_No"].Value.ToString() + "', Quality_ID = " + quality_dict[quality] + ", Colour_ID = " + colour_dict[colour] + ", Company_ID = " + company_dict[comboBox2CB.SelectedItem.ToString()] + ", Net_Weight = " + weight + ", Buy_Cost = " + rate[new Tuple<string, string>(quality, colour)] + ", Comments = '" + comments + "', Inward_Type = " + type + ", Grade = '" + grade + "' WHERE Carton_ID = '" + carton_id + "';\n";
                        }
                        else
                        {
                            sql += "INSERT INTO T_Inward_Carton (Carton_No, Carton_State, Quality_ID, Colour_ID, Net_Weight, Buy_Cost, Fiscal_Year, Inward_Voucher_ID, Comments, Inward_Type, Grade) ";
                            sql += "VALUES ('" + dataGridView1.Rows[i].Cells["Carton_No"].Value.ToString() + "', 0, " + quality_dict[quality] + ", " + colour_dict[colour] + ", " + weight + ", " + rate[new Tuple<string, string>(quality, colour)] + ", '" + fiscal_year + "', " + this.voucher_id + ", '" + comments + "', " + type + ", '" + grade + "');\n";
                        }
                    }
                }

                //deleting
                foreach (KeyValuePair<string, bool> entry in cartons_to_edit)
                {
                    if (entry.Value == false)
                    {
                        sql += "DELETE FROM T_Inward_Carton WHERE Carton_ID = '" + entry.Key + "';\n";
                    }
                }

                //catch
                sql += "commit transaction; end try BEGIN CATCH rollback transaction;\n";
                sql += "DECLARE @ErrorMessage NVARCHAR(4000); DECLARE @ErrorSeverity INT; DECLARE @ErrorState INT; SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE();\n";
                sql += "RAISERROR (@ErrorMessage, @ErrorSeverity, 10); END CATCH;\n";
                //Note the specific error state to check problems with primary key violations
                ErrorTable et = c.runQuerywithError(sql);
                if (et.dt != null)
                {
                    dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.LawnGreen;
                    c.SuccessBox("Voucher Added Successfully");
                    disable_form_edit();
                }
                else if (et.e.State == 10)
                {
                    //primary key violation
                    Global.ErrorBox("Repeated Carton Number with the same Quality, Colour, Company Name, Weight and Financial Year entered\n " + et.e.Message);
                    return;
                }
                else
                {
                    Global.ErrorBox("Could not save voucher:\n"+et.e.Message);
                    return;
                }
            }
            dataGridView1.EnableHeadersVisualStyles = false;
        }
        private void billDate_ValueChanged(object sender, EventArgs e)
        {
            this.billDateChanged = true;
        }
        private void lockCartonsCK_CheckedChanged(object sender, EventArgs e)
        {
            if(this.lockCartonsCK.Checked==true)
            {
                dataGridView1.ReadOnly = true;
                dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Red;
            }
            else
            {
                dataGridView1.ReadOnly = false;
                dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.White;
            }
            dataGridView1.EnableHeadersVisualStyles = false;
        }
        private void dataGridView2_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 2 && e.RowIndex >= 0)
                {
                    if (dataGridView2.Rows[e.RowIndex].Cells[2].Value != null)
                    {
                        float.Parse(dataGridView2.Rows[e.RowIndex].Cells[2].Value.ToString());
                    }
                    update_weights_rates();
                }
            }
            catch (Exception x)
            {
                c.ErrorBox("Please enter numeric Rate value only\n" + x.Message, "Error");
                dataGridView2.Rows[e.RowIndex].Cells[2].Value = null;
                //update label4
                update_weights_rates();
                dataGridView2.CurrentCell = dataGridView2.Rows[e.RowIndex].Cells[2];
            }
        }
        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (c.check_login_val() == false) return;
            DialogResult dialogResult = MessageBox.Show("Confirm Delete", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                string sql="--**********************DELETE T_CARTON INWARD VOUCHER******************************\n";
                //delete all rows from carton voucher
                sql += "DELETE FROM T_Inward_Carton WHERE Inward_Voucher_ID = " + this.voucher_id + ";\n";

                //update deleted column in carton_voucher
                sql += "UPDATE T_Carton_Inward_Voucher SET Deleted=1 WHERE Voucher_ID=" + voucher_id + ";\n";
                
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

        //dgv1
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
           
        }
        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            Console.WriteLine("Inside keydown");
            if (dataGridView1.Enabled == false || dataGridView1.ReadOnly == true)
            {
                return;
            }
            if (e.KeyCode == Keys.Tab &&
                (dataGridView1.SelectedCells.Cast<DataGridViewCell>().Any(x => x.ColumnIndex == 5) || this.edit_cmd_send == true))
            {
                bool edit_cmd_local = this.edit_cmd_send;
                this.edit_cmd_send = false;
                int rowindex_tab = dataGridView1.SelectedCells[0].RowIndex;
                //if (edit_cmd_local == true) rowindex_tab--;
                Console.WriteLine("ROWINDEX: " + rowindex_tab);
                if (rowindex_tab < 0)
                {
                    SendKeys.Send("{tab}");
                    return;
                }
                if (dataGridView1.Rows.Count - 2 == rowindex_tab)
                {
                    DataGridViewRow row = (DataGridViewRow)dataGridView1.Rows[rowindex_tab].Clone();
                    row.DefaultCellStyle.BackColor = dataGridView1.DefaultCellStyle.BackColor;
                    row.DefaultCellStyle.SelectionBackColor = dataGridView1.DefaultCellStyle.SelectionBackColor;
                    dataGridView1.Rows.Add(row);
                    dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.Rows.Count - 2].Cells[1];
                    if (c.Cell_Not_NullOrEmpty(dataGridView1, rowindex_tab, 1) == true && c.Cell_Not_NullOrEmpty(dataGridView1, rowindex_tab + 1, 1) == false)
                    {
                        dataGridView1.Rows[rowindex_tab + 1].Cells[1].Value = dataGridView1.Rows[rowindex_tab].Cells[1].Value;
                    }
                    if (c.Cell_Not_NullOrEmpty(dataGridView1, rowindex_tab, 2) == true && c.Cell_Not_NullOrEmpty(dataGridView1, rowindex_tab + 1, 2) == false)
                    {
                        dataGridView1.Rows[rowindex_tab + 1].Cells[2].Value = dataGridView1.Rows[rowindex_tab].Cells[2].Value;
                    }
                    if (c.Cell_Not_NullOrEmpty(dataGridView1, rowindex_tab, -1,"Carton_No") == true && c.Cell_Not_NullOrEmpty(dataGridView1, rowindex_tab + 1 ,-1, "Carton_No") == false)
                    {
                        try
                        {
                            int carton_no = int.Parse(dataGridView1.Rows[rowindex_tab].Cells["Carton_No"].Value.ToString());
                            dataGridView1.Rows[rowindex_tab + 1].Cells["Carton_No"].Value = (carton_no + 1).ToString();
                        }
                        catch { }
                    }
                    if (c.Cell_Not_NullOrEmpty(dataGridView1, rowindex_tab, -1,"Grade") == true && c.Cell_Not_NullOrEmpty(dataGridView1, rowindex_tab + 1, -1, "Grade") == false)
                    {
                        dataGridView1.Rows[rowindex_tab + 1].Cells["Grade"].Value = dataGridView1.Rows[rowindex_tab].Cells["Grade"].Value;
                    }
                    SendKeys.Send("{tab}");
                    SendKeys.Send("{tab}");
                    return;
                }
                if (c.Cell_Not_NullOrEmpty(dataGridView1, rowindex_tab, 1) == true && c.Cell_Not_NullOrEmpty(dataGridView1, rowindex_tab + 1, 1) == false)
                {
                    dataGridView1.Rows[rowindex_tab + 1].Cells[1].Value = dataGridView1.Rows[rowindex_tab].Cells[1].Value;
                }
                if (c.Cell_Not_NullOrEmpty(dataGridView1, rowindex_tab, 2) == true && c.Cell_Not_NullOrEmpty(dataGridView1, rowindex_tab + 1, 2) == false)
                {
                    dataGridView1.Rows[rowindex_tab + 1].Cells[2].Value = dataGridView1.Rows[rowindex_tab].Cells[2].Value;
                }
                if (c.Cell_Not_NullOrEmpty(dataGridView1, rowindex_tab, -1, "Carton_No") == true && c.Cell_Not_NullOrEmpty(dataGridView1, rowindex_tab + 1 , -1, "Carton_No") == false)
                {
                    try
                    {
                        int carton_no = int.Parse(dataGridView1.Rows[rowindex_tab].Cells["Carton_No"].Value.ToString());
                        dataGridView1.Rows[rowindex_tab + 1].Cells["Carton_No"].Value = (carton_no + 1).ToString();
                    }
                    catch { }
                }
                if (c.Cell_Not_NullOrEmpty(dataGridView1, rowindex_tab, -1, "Grade") == true && c.Cell_Not_NullOrEmpty(dataGridView1, rowindex_tab + 1, -1, "Grade") == false)
                {
                    dataGridView1.Rows[rowindex_tab + 1].Cells["Grade"].Value = dataGridView1.Rows[rowindex_tab].Cells["Grade"].Value;
                }

                if (edit_cmd_local == false)
                {
                    SendKeys.Send("{tab}");
                }
                SendKeys.Send("{tab}");
                SendKeys.Send("{tab}");
                SendKeys.Send("{tab}");
                SendKeys.Send("{tab}");
            }
            if (e.KeyCode == Keys.Enter &&
               (dataGridView1.SelectedCells.Cast<DataGridViewCell>().Any(x => x.ColumnIndex == 1) || dataGridView1.SelectedCells.Cast<DataGridViewCell>().Any(x => x.ColumnIndex == 2) || dataGridView1.SelectedCells.Cast<DataGridViewCell>().Any(x => x.ColumnIndex == 3) || this.edit_cmd_send == true))
            {
                dataGridView1.BeginEdit(true);
                ComboBox c = (ComboBox)dataGridView1.EditingControl;
                if(c!=null) c.DroppedDown = true;
                e.Handled = true;
            }
        }
        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                dataGridView1.Rows[e.RowIndex].Selected = true;
            }
        }
        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            if (e.RowIndex != dataGridView1.Rows.Count - 1)
            {
                dataGridView1.Rows[e.RowIndex].Cells[0].Value = e.RowIndex + 1;
            }
        }
        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            Console.WriteLine("in editingcontrolshowing");
            if (e.Control is DataGridViewComboBoxEditingControl)
            {
                ((ComboBox)e.Control).DropDownStyle = ComboBoxStyle.DropDown;
                ((ComboBox)e.Control).AutoCompleteSource = AutoCompleteSource.ListItems;
                ((ComboBox)e.Control).AutoCompleteMode = AutoCompleteMode.Append;
            }
        }
        private void dataGridView1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dataGridView1.IsCurrentCellDirty)
            {
                dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        //dgv2
        private void dataGridView2_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            Console.WriteLine("in editingcontrolshowing");
            if (e.Control is DataGridViewComboBoxEditingControl)
            {
                ((ComboBox)e.Control).DropDownStyle = ComboBoxStyle.DropDown;
                ((ComboBox)e.Control).AutoCompleteSource = AutoCompleteSource.ListItems;
                ((ComboBox)e.Control).AutoCompleteMode = AutoCompleteMode.Append;
            }
        }
        private void dataGridView2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter &&
               (dataGridView2.SelectedCells.Cast<DataGridViewCell>().Any(x => x.ColumnIndex == 0) || dataGridView2.SelectedCells.Cast<DataGridViewCell>().Any(x => x.ColumnIndex == 1) || this.edit_cmd_send == true))
            {
                dataGridView2.BeginEdit(true);
                ComboBox c = (ComboBox)dataGridView2.EditingControl;
                if (c != null) c.DroppedDown = true;
                e.Handled = true;
            }
        }
        private void dataGridView2_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                dataGridView2.Rows[e.RowIndex].Selected = true;
            }
        }
        private void dataGridView2_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dataGridView2.IsCurrentCellDirty)
            {
                dataGridView2.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            Console.WriteLine("END EDIT");
            if (e.ColumnIndex==1 || e.ColumnIndex==2)
            {
                if(c.Cell_Not_NullOrEmpty(dataGridView1, e.RowIndex, 1) && c.Cell_Not_NullOrEmpty(dataGridView1, e.RowIndex, 2))
                {
                    Console.WriteLine("DGV2 END EDIT");
                    generate_dgv2();
                }
            }
            
            try
            {
                if (e.ColumnIndex == 5 && e.RowIndex >= 0)
                {
                    if (dataGridView1.Rows[e.RowIndex].Cells[5].Value != null)
                    {
                        float.Parse(dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString());
                        if (float.Parse(dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString()) >= 100.00F)
                        {
                            //c.ErrorBox("Weight should be less than 100", "Error");
                            //dataGridView1.Rows[e.RowIndex].Cells[5].Value = null;
                        }
                    }
                }
            }
            catch (Exception x)
            {
                c.ErrorBox("Please enter numeric Weight value only\n" + x.Message, "Error");
                dataGridView1.Rows[e.RowIndex].Cells[5].Value = null;
                this.inputerror1 = true;
            }
            
            if (e.RowIndex >= 0 && (e.ColumnIndex == 1 || e.ColumnIndex == 2 || e.ColumnIndex == 5))
            {
                update_weights_rates();
            }
        }
    }
}
