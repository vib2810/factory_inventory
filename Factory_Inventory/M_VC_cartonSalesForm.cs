using Factory_Inventory.Factory_Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Factory_Inventory
{
    public partial class M_VC_cartonSalesForm : Form
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
        private List<Tuple<string, string>> carton_data;           //List that stores carton numbers from SQL
        private M_V_history v1_history;
        private int voucher_id;
        private string tablename;
        struct fetch_data
        {
            public float net_wt;
            public string colour;
            public fetch_data(float net_wt, string shade)
            {
                this.net_wt = net_wt;
                this.colour = shade;
            }
        }
        Dictionary<Tuple<string, string>, fetch_data> carton_fetch_data = new Dictionary<Tuple<string, string>, fetch_data>();

        //Form Functions
        public M_VC_cartonSalesForm(string form)
        {
            InitializeComponent();
            this.c = new DbConnect();
            this.carton_data = new List<Tuple<string, string>>();
            this.tablename = form;

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

            //Create drop-down quality list
            DataTable d1 = c.getQC('q');
            List<string> input_qualities = new List<string>();
            input_qualities.Add("---Select---");
            for (int i = 0; i < d1.Rows.Count; i++)
            {
                if (this.tablename == "Carton")
                {
                    input_qualities.Add(d1.Rows[i]["Quality_Before_Twist"].ToString());
                }
                else if (this.tablename == "Carton_Produced")
                {
                    input_qualities.Add(d1.Rows[i]["Quality"].ToString());
                }
            }
            List<string> final_list = input_qualities.Distinct().ToList();
            this.comboBox1CB.DataSource = final_list;
            this.comboBox1CB.DisplayMember = "Quality";
            this.comboBox1CB.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.comboBox1CB.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.comboBox1CB.AutoCompleteMode = AutoCompleteMode.SuggestAppend;


            //Create drop-down Company list
            var dataSource2 = new List<string>();
            if (this.tablename == "Carton_Produced")
            {
                dataSource2.Add("Self");
            }
            else if(this.tablename=="Carton")
            {
                DataTable d2 = c.getQC('c');
                dataSource2.Add("---Select---");
                for (int i = 0; i < d2.Rows.Count; i++)
                {
                    dataSource2.Add(d2.Rows[i][0].ToString());
                }
            }
            this.comboBox2CB.DataSource = dataSource2;
            this.comboBox2CB.DisplayMember = "Company_Names";
            this.comboBox2CB.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.comboBox2CB.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.comboBox2CB.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            if (this.tablename == "Carton_Produced")
            {
                this.comboBox2CB.SelectedIndex = this.comboBox2CB.FindStringExact("Self");
                this.comboBox2CB.Enabled = false;
                this.comboBox2CB.TabIndex = 0;
                this.comboBox2CB.TabStop = false;
            }

            //Create drop-down Customers list
            var dataSource3 = new List<string>();
            DataTable d3 = c.getQC('C');
            dataSource3.Add("---Select---");

            for (int i = 0; i < d3.Rows.Count; i++)
            {
                dataSource3.Add(d3.Rows[i][0].ToString());
            }
            this.comboBox3CB.DataSource = dataSource3;
            this.comboBox3CB.DisplayMember = "Customers";
            this.comboBox3CB.DropDownStyle = ComboBoxStyle.DropDown;//Create a drop-down list
            this.comboBox3CB.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.comboBox3CB.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            
            //Create drop-down Colour lists
            var dataSource5 = new List<string>();
            if (tablename == "Carton")
            {
                this.shadeCB.Items.Add("Gray");
                this.shadeCB.SelectedIndex = this.shadeCB.FindStringExact("Gray");
                this.shadeCB.Enabled = false;
            }
            else
            {
                DataTable d5 = c.getQC('l');
                dataSource5.Add("---Select---");
                for (int i = 0; i < d5.Rows.Count; i++)
                {
                    dataSource5.Add(d5.Rows[i][0].ToString());
                }
                List<string> final_list1 = dataSource5.Distinct().ToList();
                this.shadeCB.DataSource = final_list1;
                this.shadeCB.DisplayMember = "Colours";
                this.shadeCB.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
                this.shadeCB.AutoCompleteSource = AutoCompleteSource.ListItems;
                this.shadeCB.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            }


            #endregion //combobox

            //DatagridView
            dataGridView1.Columns.Add("Sl_No", "Sl_No");
            dataGridView1.Columns[0].ReadOnly = true;
            DataGridViewComboBoxColumn dgvCmb = new DataGridViewComboBoxColumn();
            for (int i = 0; i < this.carton_data.Count; i++) dgvCmb.Items.Add(this.carton_data[i].Item1 + " (" + this.carton_data[i].Item2 + ")");
            //dgvCmb.DataSource = this.carton_data;

            dgvCmb.HeaderText = "Carton Number";
            dataGridView1.Columns.Insert(1, dgvCmb);
            dataGridView1.Columns.Add("Weight", "Weight");
            dataGridView1.Columns[2].ReadOnly = true;
            dataGridView1.Columns.Add("Shade", "Shade");
            dataGridView1.Columns[3].ReadOnly = true;
            dataGridView1.RowCount = 10;

            c.set_dgv_column_sort_state(this.dataGridView1, DataGridViewColumnSortMode.NotSortable);
        }
        public M_VC_cartonSalesForm(DataRow row, bool isEditable, M_V_history v1_history, string form)
        {
            InitializeComponent();
            this.edit_form = true;
            this.v1_history = v1_history;
            this.c = new DbConnect();
            this.carton_data = new List<Tuple<string, string>>();
            this.tablename = form;

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
            DataTable d1 = c.getQC('q');
            List<string> input_qualities = new List<string>();
            input_qualities.Add("---Select---");
            for (int i = 0; i < d1.Rows.Count; i++)
            {
                if (this.tablename == "Carton")
                {
                    input_qualities.Add(d1.Rows[i]["Quality_Before_Twist"].ToString());
                }
                else if (this.tablename == "Carton_Produced")
                {
                    input_qualities.Add(d1.Rows[i]["Quality"].ToString());
                }
            }
            List<string> norep_quality_list = input_qualities.Distinct().ToList();
            this.comboBox1CB.DataSource = norep_quality_list;
            this.comboBox1CB.DisplayMember = "Quality";
            this.comboBox1CB.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.comboBox1CB.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.comboBox1CB.AutoCompleteMode = AutoCompleteMode.SuggestAppend;


            //Create drop-down Company list
            var dataSource2 = new List<string>();
            if (this.tablename == "Carton_Produced")
            {
                dataSource2.Add("Self");
            }
            else if (this.tablename == "Carton")
            {
                DataTable d2 = c.getQC('c');
                dataSource2.Add("---Select---");
                for (int i = 0; i < d2.Rows.Count; i++)
                {
                    dataSource2.Add(d2.Rows[i][0].ToString());
                }
            }
            this.comboBox2CB.DataSource = dataSource2;
            this.comboBox2CB.DisplayMember = "Company_Names";
            this.comboBox2CB.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.comboBox2CB.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.comboBox2CB.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            //Create drop-down Customers list
            var dataSource3 = new List<string>();
            DataTable d3 = c.getQC('C');
            dataSource3.Add("---Select---");

            for (int i = 0; i < d3.Rows.Count; i++)
            {
                dataSource3.Add(d3.Rows[i][0].ToString());
            }
            this.comboBox3CB.DataSource = dataSource3;
            this.comboBox3CB.DisplayMember = "Customers";
            this.comboBox3CB.DropDownStyle = ComboBoxStyle.DropDown;//Create a drop-down list
            this.comboBox3CB.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.comboBox3CB.AutoCompleteMode = AutoCompleteMode.Append;

            var dataSource5 = new List<string>();
            if (tablename == "Carton")
            {
                this.shadeCB.Items.Add("Gray");
                this.shadeCB.SelectedIndex = this.shadeCB.FindStringExact("Gray");
                this.shadeCB.Enabled = false;
            }
            else
            {
                DataTable d5 = c.getQC('l');
                dataSource5.Add("---Select---");
                for (int i = 0; i < d5.Rows.Count; i++)
                {
                    dataSource5.Add(d5.Rows[i][0].ToString());
                }
                List<string> final_list1 = dataSource5.Distinct().ToList();
                this.shadeCB.DataSource = final_list1;
                this.shadeCB.DisplayMember = "Colours";
                this.shadeCB.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
                this.shadeCB.AutoCompleteSource = AutoCompleteSource.ListItems;
                this.shadeCB.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            }
            #endregion //dropdown

            //DatagridView
            dataGridView1.Columns.Add("Sl_No", "Sl_No");
            dataGridView1.Columns[0].ReadOnly = true;

            DataGridViewComboBoxColumn dgvCmb = new DataGridViewComboBoxColumn();
            dgvCmb.HeaderText = "Carton Number";
            for (int i = 0; i < this.carton_data.Count; i++) dgvCmb.Items.Add(this.carton_data[i].Item1 + " (" + this.carton_data[i].Item2 + ")");
            //dgvCmb.DataSource = this.carton_data;
            dataGridView1.Columns.Insert(1, dgvCmb);
            dataGridView1.Columns.Add("Weight", "Weight");
            dataGridView1.Columns.Add("Shade", "Shade");
            dataGridView1.Columns[3].ReadOnly = true;
            dataGridView1.Columns[2].ReadOnly = true;

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
            if (this.comboBox1CB.FindStringExact(row["Quality"].ToString()) == -1)
            {
                norep_quality_list.Add(row["Quality"].ToString());
                this.comboBox1CB.DataSource = null;
                this.comboBox1CB.DataSource = norep_quality_list;
            }
            this.comboBox1CB.SelectedIndex = this.comboBox1CB.FindStringExact(row["Quality"].ToString());
            if (this.comboBox2CB.FindStringExact(row["Company_Name"].ToString()) == -1)
            {
                dataSource2.Add(row["Company_Name"].ToString());
                this.comboBox2CB.DataSource = null;
                this.comboBox2CB.DataSource = dataSource2;

            }
            this.comboBox2CB.SelectedIndex = this.comboBox2CB.FindStringExact(row["Company_Name"].ToString());
            this.voucher_id = int.Parse(row["Voucher_ID"].ToString());
            if (this.comboBox3CB.FindStringExact(row["Customer"].ToString()) == -1)
            {
                dataSource3.Add(row["Customer"].ToString());
                this.comboBox3CB.DataSource = null;
                this.comboBox3CB.DataSource = dataSource3;

            }
            this.comboBox3CB.SelectedIndex = this.comboBox3CB.FindStringExact(row["Customer"].ToString());
            this.saleDONoTB.Text = row["Sale_DO_No"].ToString();
            this.rateTextboxTB.Text = row["Sale_Rate"].ToString();

            
            DataTable d = new DataTable();
            if (this.tablename == "Carton")
            {
                d = c.getTableData(this.tablename, "Carton_No, Net_Weight, Fiscal_Year", "TS_Voucher_ID = "+ row["Voucher_ID"].ToString()+" AND Date_Of_Sale IS NOT NULL");
            }
            else d = c.getTableData(this.tablename, "Carton_No, Net_Weight, Colour, Fiscal_Year", "Sales_Voucher_ID = " + row["Voucher_ID"].ToString() + "");
            
            dataGridView1.RowCount = d.Rows.Count + 1;

            for (int i=0; i<d.Rows.Count; i++)
            {
                this.carton_data.Add(new Tuple<string, string>(d.Rows[i]["Carton_No"].ToString(), d.Rows[i]["Fiscal_Year"].ToString()));
                dgvCmb.Items.Add(d.Rows[i]["Carton_No"].ToString()+" ("+ d.Rows[i]["Fiscal_Year"].ToString()+")");
                string colour = "Gray";
                if (this.tablename != "Carton") colour = d.Rows[i]["Colour"].ToString();
                this.carton_fetch_data[new Tuple<string, string>(d.Rows[i]["Carton_No"].ToString(), d.Rows[i]["Fiscal_Year"].ToString())] = new fetch_data(float.Parse(d.Rows[i]["Net_Weight"].ToString()), colour);
                dataGridView1.Rows[i].Cells[1].Value = d.Rows[i]["Carton_No"].ToString() + " (" + d.Rows[i]["Fiscal_Year"].ToString() + ")";
            }
            this.loadData(row["Quality"].ToString(), row["Company_Name"].ToString());

            c.set_dgv_column_sort_state(this.dataGridView1, DataGridViewColumnSortMode.NotSortable);

            if(row["Sale_Bill_No"].ToString()!="")
            {
                this.label10.Text = "This DO cannot be edited/deleted as its bill as already been made. Delete/Edit its bill first";
                this.deleteButton.Enabled = false;
                this.disable_form_edit();
            }
        }
        private void M_V1_cartonSalesForm_Load(object sender, EventArgs e)
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

            this.saleDateDTP.Focus();
            if (Global.access == 2)
            {
                this.deleteButton.Visible = false;
            }
            dataGridView1.Columns[1].Width = 200;
        }

        //Own Functions
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
        private void loadData(string quality, string company)
        {
            DataTable d = new DataTable();
            if(this.tablename=="Carton")
            {
                d = c.getTableData(this.tablename, "Carton_No, Net_Weight, Fiscal_Year", "Carton_State = 1 AND Quality = '" + quality + "' AND Company_Name = '" + company + "'");
            }
            else d= c.getTableData(this.tablename, "Carton_No, Net_Weight, Colour, Fiscal_Year", "Carton_State = 1 AND Quality = '" + quality + "' AND Company_Name = '" + company + "'");
            DataGridViewComboBoxColumn dgvCmb = (DataGridViewComboBoxColumn)dataGridView1.Columns[1];
            for (int i = 0; i < d.Rows.Count; i++)
            {
                string cartonno = d.Rows[i]["Carton_No"].ToString();
                string carton_fiscal_year = d.Rows[i]["Fiscal_Year"].ToString();
                string to_show = cartonno + " (" + carton_fiscal_year + ")";
                dgvCmb.Items.Add(to_show);
                this.carton_data.Add(new Tuple<string, string>(cartonno, carton_fiscal_year));
                string colour = "Gray";
                if (this.tablename != "Carton") colour = d.Rows[i]["Colour"].ToString();
                this.carton_fetch_data[new Tuple<string, string>(cartonno, carton_fiscal_year)] = new fetch_data(float.Parse(d.Rows[i]["Net_Weight"].ToString()), colour);
            }
            int j = 0;
            foreach(Tuple<string, string> item in carton_fetch_data.Keys)
            {
                Console.WriteLine(item.Item1 + "," + item.Item2 + carton_fetch_data[item].colour+" "+carton_fetch_data[item].net_wt);
                j++;
            }
            Console.WriteLine(j);
            
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
                this.saleDONoTB.Text = c.getNextNumber_FiscalYear("Highest_0_DO_No", c.getFinancialYear(this.saleDateDTP.Value));
            }
            else if (this.typeCB.SelectedIndex == 2)
            {
                this.saleDONoTB.Text = c.getNextNumber_FiscalYear("Highest_1_DO_No", c.getFinancialYear(this.saleDateDTP.Value));
            }
            else
            {
                this.saleDONoTB.Text = "";
            }
        }

        //Clicks
        private void saveButton_Click(object sender, EventArgs e)
        {
            //checks
            if (typeCB.Text == "---Select---")
            {
                c.ErrorBox("Enter type of sale", "Error");
                return;
            }
            if (comboBox1CB.Text == "---Select---")
            {
                c.ErrorBox("Enter Select Quality", "Error");
                return;
            }
            if (comboBox2CB.Text == "---Select---")
            {
                c.ErrorBox("Select Company Name "+comboBox2CB.Text, "Error");
                return;
            }
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
            List<Tuple<string, string> > cartonno = new List<Tuple<string, string>>();
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
                    string[] carton = c.repeated_batch_csv(dataGridView1.Rows[i].Cells[1].Value.ToString());
                    cartonno.Add(new Tuple<string, string>(carton[0], carton[1]));
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
                bool added = c.addSalesVoucher(inputDate.Value, saleDateDTP.Value, typeCB.Text, comboBox1CB.SelectedItem.ToString(), comboBox2CB.SelectedItem.ToString(), comboBox3CB.SelectedItem.ToString(), cartonno, float.Parse(rateTextboxTB.Text), this.saleDONoTB.Text, this.tablename, float.Parse(this.totalWeightTB.Text));
                if (added == false)
                {
                    return;
                }
                else
                {
                    dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.LawnGreen;
                    this.disable_form_edit();
                }
            }
            else
            {
                bool edited = c.editSalesVoucher(this.voucher_id, saleDateDTP.Value, typeCB.Text, comboBox1CB.SelectedItem.ToString(), comboBox2CB.SelectedItem.ToString(), comboBox3CB.SelectedItem.ToString(), cartonno, float.Parse(rateTextboxTB.Text), this.saleDONoTB.Text, this.tablename, float.Parse(this.totalWeightTB.Text));
                if (edited == false)
                {
                    return;
                }
                else
                {
                    dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.LawnGreen;
                    this.disable_form_edit();
                    this.v1_history.loadData();
                }
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
            this.loadData(this.comboBox1CB.SelectedItem.ToString(), this.comboBox2CB.SelectedItem.ToString());
            if(this.carton_data.Count-1==0)
            {
                c.WarningBox("No Cartons Loaded");
                return;
            }
            else
            {
                if (this.edit_form == false)
                {
                    c.SuccessBox("Loaded " + (this.carton_data.Count-1).ToString() + " Cartons");
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
            DialogResult dialogResult = MessageBox.Show("Confirm Delete", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                bool deleted = c.deleteSalesVoucher(this.voucher_id);
                if (deleted == true)
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
                
                List<Tuple<string, string>> temp=new List<Tuple<string, string>>();
                foreach (string item in cb.Items)
                {
                    string[] carton_data = c.repeated_batch_csv(item);
                    temp.Add(new Tuple<string, string>(carton_data[0], carton_data[1]));
                }
                foreach(Tuple<string, string> item in temp)
                {
                    fetch_data value = new fetch_data(-1F, "");
                    this.carton_fetch_data.TryGetValue(item, out value);
                    if (value.colour!=this.shadeCB.Text)
                    {
                        cb.Items.Remove(item.Item1 + " (" + item.Item2 + ")");
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
                Console.WriteLine(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                string[] carton_data_changed = c.repeated_batch_csv(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                Console.WriteLine("setting " + carton_data_changed[0] + " " + carton_data_changed[1]);
                fetch_data data = new fetch_data(-1F, "");
                this.carton_fetch_data.TryGetValue(new Tuple<string, string>(carton_data_changed[0], carton_data_changed[1]), out data);
                dataGridView1.Rows[e.RowIndex].Cells[2].Value = data.net_wt.ToString("F3");
                dataGridView1.Rows[e.RowIndex].Cells["Shade"].Value = data.colour;
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
