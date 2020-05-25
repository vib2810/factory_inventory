using Factory_Inventory.Factory_Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Media.Animation;

namespace Factory_Inventory
{
    public partial class M_V2_dyeingInwardForm : Form
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
                this.saveButton.Focus();
                this.ActiveControl = saveButton;
                return false;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private DbConnect c;
        private bool edit_cmd_send = false;
        private bool edit_form = false;
        private List<string> batch_no;
        private M_V_history v1_history;
        private int voucherID;
        private bool addBill = false;
        Dictionary<string, int> batch_editable = new Dictionary<string, int>();
        public M_V2_dyeingInwardForm(string mode)
        {
            if(mode== "dyeingInward")
            {
                InitializeComponent();
                this.c = new DbConnect();
                this.batch_no = new List<string>();
                this.batch_no.Add("");
                this.saveButton.Enabled = false;

                //Create drop-down Dyeing Company lists
                var dataSource3 = new List<string>();
                DataTable d3 = c.getQC('d');
                dataSource3.Add("---Select---");

                for (int i = 0; i < d3.Rows.Count; i++)
                {
                    dataSource3.Add(d3.Rows[i][0].ToString());
                }
                this.dyeingCompanyCB.DataSource = dataSource3;
                this.dyeingCompanyCB.DisplayMember = "Dyeing_Company_Names";
                this.dyeingCompanyCB.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
                this.dyeingCompanyCB.AutoCompleteSource = AutoCompleteSource.ListItems;
                this.dyeingCompanyCB.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

                //Create drop-down lists
                var dataSource = new List<string>();
                DataTable d = c.getQC('f');
                for (int i = 0; i < d.Rows.Count; i++)
                {
                    dataSource.Add(d.Rows[i][0].ToString());
                }
                this.comboBox3CB.DataSource = dataSource;
                this.comboBox3CB.DisplayMember = "Financial Year";
                this.comboBox3CB.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
                this.comboBox3CB.AutoCompleteSource = AutoCompleteSource.ListItems;
                this.comboBox3CB.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

                this.comboBox3CB.SelectedIndex = this.comboBox3CB.FindStringExact(c.getFinancialYear(this.inwardDateDTP.Value));


                //DatagridView
                dataGridView1.Columns.Add("Sl_No", "Sl_No");
                dataGridView1.Columns[0].ReadOnly = true;
                DataGridViewComboBoxColumn dgvCmb = new DataGridViewComboBoxColumn();
                dgvCmb.DataSource = this.batch_no;
                dgvCmb.HeaderText = "Batch Number";
                dataGridView1.Columns.Insert(1, dgvCmb);
                dataGridView1.Columns.Add("Weight", "Weight");
                dataGridView1.Columns.Add("Colour", "Colour");
                dataGridView1.Columns.Add("Quality", "Quality");
                dataGridView1.Columns.Add("Net Rate", "Net Rate");
                dataGridView1.Columns.Add("Slip Number", "Slip Number");
                dataGridView1.Columns["Weight"].ReadOnly = true;
                dataGridView1.Columns["Colour"].ReadOnly = true;
                dataGridView1.Columns["Quality"].ReadOnly = true;
                dataGridView1.Columns["Net rate"].ReadOnly = true;

                dataGridView1.RowCount = 10;

                c.SetGridViewSortState(this.dataGridView1, DataGridViewColumnSortMode.NotSortable);

            }
            if(mode == "addBill")
            {
                InitializeComponent();
                this.Name = "Add Bill Numbers";
                this.addBill = true;
                this.c = new DbConnect();
                this.batch_no = new List<string>();
                this.batch_no.Add("");

                this.billcheckBoxCK.Enabled = true;
                this.billcheckBoxCK.Checked = false;
                this.billcheckBoxCK.Location = new System.Drawing.Point(139, 94);

                this.billNumberTextboxTB.Visible = true;
                this.billNumberTextboxTB.Enabled = true;
                this.billNumberTextboxTB.ReadOnly = false;
                this.billNumberTextboxTB.TabIndex = 2;
                this.billNumberTextboxTB.TabStop = true;
                this.billNumberTextboxTB.Location = new System.Drawing.Point(24, 94);

                this.billDateDTP.Visible = true;
                this.billDateDTP.TabIndex = 3;
                this.billDateDTP.TabStop = true;
                this.billDateDTP.Location = new System.Drawing.Point(24, 140);
                this.billDateDTP.Enabled = true;

                this.inwardDateDTP.Visible = false;
                this.label1.Visible = false;
                this.label7.Visible = true;
                this.label7.Location = new System.Drawing.Point(24, 74);
                this.label2.Visible = true;
                this.label2.Location = new System.Drawing.Point(24, 120);
                this.saveButton.Enabled = false;

                //Create drop-down Dyeing Company lists
                var dataSource3 = new List<string>();
                DataTable d3 = c.getQC('d');
                dataSource3.Add("---Select---");

                for (int i = 0; i < d3.Rows.Count; i++)
                {
                    dataSource3.Add(d3.Rows[i][0].ToString());
                }
                this.dyeingCompanyCB.DataSource = dataSource3;
                this.dyeingCompanyCB.DisplayMember = "Dyeing_Company_Names";
                this.dyeingCompanyCB.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
                this.dyeingCompanyCB.AutoCompleteSource = AutoCompleteSource.ListItems;
                this.dyeingCompanyCB.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

                //Create drop-down lists
                var dataSource = new List<string>();
                DataTable d = c.getQC('f');
                for (int i = 0; i < d.Rows.Count; i++)
                {
                    dataSource.Add(d.Rows[i][0].ToString());
                }
                this.comboBox3CB.DataSource = dataSource;
                this.comboBox3CB.DisplayMember = "Financial Year";
                this.comboBox3CB.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
                this.comboBox3CB.AutoCompleteSource = AutoCompleteSource.ListItems;
                this.comboBox3CB.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

                this.comboBox3CB.SelectedIndex = this.comboBox3CB.FindStringExact(c.getFinancialYear(this.inwardDateDTP.Value));


                //DatagridView
                dataGridView1.Columns.Add("Sl_No", "Sl_No");
                dataGridView1.Columns[0].ReadOnly = true;
                DataGridViewComboBoxColumn dgvCmb = new DataGridViewComboBoxColumn();
                dgvCmb.DataSource = this.batch_no;

                dgvCmb.HeaderText = "Batch Number";
                dataGridView1.Columns.Insert(1, dgvCmb);
                dataGridView1.Columns.Add("Weight", "Weight");
                dataGridView1.Columns.Add("Colour", "Colour");
                dataGridView1.Columns.Add("Quality", "Quality");
                dataGridView1.Columns.Add("Net Rate", "Net Rate");
                dataGridView1.Columns["Weight"].ReadOnly = true;
                dataGridView1.Columns["Colour"].ReadOnly = true;
                dataGridView1.Columns["Quality"].ReadOnly = true;
                dataGridView1.Columns["Net rate"].ReadOnly = true;

                dataGridView1.RowCount = 10;

                c.SetGridViewSortState(this.dataGridView1, DataGridViewColumnSortMode.NotSortable);
            }
        }
        public M_V2_dyeingInwardForm(DataRow row, bool isEditable, M_V_history v1_history, string mode)
        {
            if (mode == "dyeingInward")
            {
                InitializeComponent();
                this.edit_form = true;
                this.v1_history = v1_history;
                this.c = new DbConnect();
                this.batch_no = new List<string>();
                this.batch_no.Add("");
                this.saveButton.Enabled = false;


                //Create drop-down Dyeing Company lists
                var dataSource3 = new List<string>();
                DataTable d3 = c.getQC('d');
                dataSource3.Add("---Select---");

                for (int i = 0; i < d3.Rows.Count; i++)
                {
                    dataSource3.Add(d3.Rows[i][0].ToString());
                }
                this.dyeingCompanyCB.DataSource = dataSource3;
                this.dyeingCompanyCB.DisplayMember = "Dyeing_Company_Names";
                this.dyeingCompanyCB.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
                this.dyeingCompanyCB.AutoCompleteSource = AutoCompleteSource.ListItems;
                this.dyeingCompanyCB.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

                //Create drop-down lists
                var dataSource = new List<string>();
                DataTable d = c.getQC('f');

                for (int i = 0; i < d.Rows.Count; i++)
                {
                    dataSource.Add(d.Rows[i][0].ToString());
                }
                this.comboBox3CB.DataSource = dataSource;
                this.comboBox3CB.DisplayMember = "Financial Year";
                this.comboBox3CB.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
                this.comboBox3CB.AutoCompleteSource = AutoCompleteSource.ListItems;
                this.comboBox3CB.AutoCompleteMode = AutoCompleteMode.SuggestAppend;




                //DatagridView make
                dataGridView1.Columns.Add("Sl_No", "Sl_No");
                dataGridView1.Columns[0].ReadOnly = true;
                DataGridViewComboBoxColumn dgvCmb = new DataGridViewComboBoxColumn();
                dgvCmb.DataSource = this.batch_no;
                dgvCmb.HeaderText = "Batch Number";
                dataGridView1.Columns.Insert(1, dgvCmb);

                dataGridView1.Columns.Add("Weight", "Weight");
                dataGridView1.Columns.Add("Colour", "Colour");
                dataGridView1.Columns.Add("Quality", "Quality");
                dataGridView1.Columns.Add("Net Rate", "Net Rate");
                dataGridView1.Columns.Add("Slip Number", "Slip Number");
                dataGridView1.Columns["Weight"].ReadOnly = true;
                dataGridView1.Columns["Colour"].ReadOnly = true;
                dataGridView1.Columns["Quality"].ReadOnly = true;
                dataGridView1.Columns["Net rate"].ReadOnly = true;
                dataGridView1.RowCount = 10;

                //if form is only view-only
                if (isEditable == false)
                {
                    this.Text += "(View Only)";
                    disable_form_edit();
                }
                else
                {
                    //no option to edit company name and quality
                    this.Text += "(Edit)";
                    this.inwardDateDTP.Enabled = true;
                    this.dyeingCompanyCB.Enabled = false;
                    this.saveButton.Enabled = true;
                    this.loadBatchButton.Enabled = false;
                    this.dataGridView1.ReadOnly = false;
                    this.comboBox3CB.Enabled = false;
                    if(row["Bill_No"].ToString()=="0")
                    {
                        this.billcheckBoxCK.Checked = true;
                    }
                    else
                    {
                        this.billcheckBoxCK.Checked = false;
                    }
                    this.billcheckBoxCK.Enabled = true ;
                }

                //Fill in required fields
                this.inputDate.Value = Convert.ToDateTime(row["Date_Of_Input"].ToString());
                this.inwardDateDTP.Value = Convert.ToDateTime(row["Inward_Date"].ToString());
                this.dyeingCompanyCB.SelectedIndex = this.dyeingCompanyCB.FindStringExact(row["Dyeing_Company_Name"].ToString());
                this.comboBox3CB.SelectedIndex = this.comboBox3CB.FindStringExact(row["Batch_Fiscal_Year"].ToString());
                if(row["Bill_Date"].ToString() != null && row["Bill_Date"].ToString() != "") this.billDateDTP.Value = Convert.ToDateTime(row["Bill_Date"].ToString());

                if (int.Parse(row["Bill_No"].ToString()) == 0)
                {
                    billcheckBoxCK.Checked = true;
                    billNumberTextboxTB.Enabled = false;
                }
                else
                {
                    billcheckBoxCK.Checked = false;
                    billNumberTextboxTB.Enabled = true;
                    billNumberTextboxTB.Text = row["Bill_No"].ToString();
                }

                this.voucherID = int.Parse(row["Voucher_ID"].ToString());
                string[] batch_nos = c.csvToArray(row["Batch_No_Arr"].ToString());
                for (int i = 0; i < batch_nos.Length; i++)
                {
                    this.batch_no.Add(batch_nos[i]);
                }
                this.loadData(row["Dyeing_Company_Name"].ToString(), row["Batch_Fiscal_Year"].ToString());
                if (isEditable == false) dataGridView1.RowCount = batch_nos.Length;
                else dataGridView1.RowCount = batch_nos.Length + 1;
                string batch_nos_string = row["Batch_No_Arr"].ToString();
                string[] batch_nos_arr = c.csvToArray(row["Batch_No_Arr"].ToString());
                DataTable bill_nos = c.getColumnBatchNos("Batch_No, Bill_No, Batch_State", c.removecom(batch_nos_string), this.comboBox3CB.SelectedItem.ToString());
                Dictionary<string, Tuple<string, int>> billnos = new Dictionary<string, Tuple<string, int>>();
                for(int i=0;i<bill_nos.Rows.Count;i++)
                {
                    billnos[bill_nos.Rows[i]["Batch_No"].ToString()] = new Tuple<string, int>(bill_nos.Rows[i]["Bill_No"].ToString(), int.Parse(bill_nos.Rows[i]["Batch_State"].ToString()));
                }
                bool flag = true;
                for (int i = 0; i < batch_nos_arr.Length; i++)
                {
                    dataGridView1.Rows[i].Cells[1].Value = batch_nos_arr[i];
                    Tuple<string, int> value= billnos[batch_nos_arr[i]];
                    string bill_no = value.Item1;
                    if(bill_no != "0")
                    {
                        flag = false;
                        this.batch_editable[batch_nos[i]] = 0;
                        DataGridViewRow r = (DataGridViewRow)dataGridView1.Rows[i];
                        dataGridView1.Rows[i].ReadOnly = true;
                        r.DefaultCellStyle.BackColor = Color.LightGray;
                        r.DefaultCellStyle.SelectionBackColor= Color.LightGray;
                    }
                    if (value.Item2 == 3)
                    {
                        flag = false;
                        this.batch_editable[batch_nos[i]] = 3;
                        DataGridViewRow r = (DataGridViewRow)dataGridView1.Rows[i];
                        dataGridView1.Rows[i].ReadOnly = true;
                        r.DefaultCellStyle.BackColor = Color.LightGreen;
                        r.DefaultCellStyle.SelectionBackColor = Color.LightGreen;
                    }
                    else if (value.Item2 == 4)
                    {
                        flag = false;
                        this.batch_editable[batch_nos[i]] = 4;
                        DataGridViewRow r = (DataGridViewRow)dataGridView1.Rows[i];
                        dataGridView1.Rows[i].ReadOnly = true;
                        r.DefaultCellStyle.BackColor = Color.Orange;
                        r.DefaultCellStyle.SelectionBackColor = Color.Orange;
                    }
                    //pending
                }
                this.label5.Text = "Grey: Bill Number Added    Green: Batch sent for production    Orange: Batch sent for redyeing";
                if (!flag)
                {
                    this.billcheckBoxCK.Checked = true;
                }
                if (isEditable == false) 
                { 
                    this.deleteButton.Visible = true;
                    this.deleteButton.Enabled = false;
                    if(flag == true) this.deleteButton.Enabled = true; 
                }
                c.SetGridViewSortState(this.dataGridView1, DataGridViewColumnSortMode.NotSortable);
            }
            if (mode == "addBill")
            {
                InitializeComponent();
                //common initializations
                this.Name = "Add Bill Numbers";
                this.addBill = true;
                this.edit_form = true;
                this.v1_history = v1_history;
                this.c = new DbConnect();
                this.batch_no = new List<string>();
                this.batch_no.Add("");

                //graphics placement
                this.billcheckBoxCK.Enabled = true;
                this.billcheckBoxCK.Checked = false;
                this.billcheckBoxCK.Location = new System.Drawing.Point(139, 94);

                this.billNumberTextboxTB.Visible = true;
                this.billNumberTextboxTB.Enabled = true;
                this.billNumberTextboxTB.ReadOnly = false;
                this.billNumberTextboxTB.TabIndex = 2;
                this.billNumberTextboxTB.Location = new System.Drawing.Point(24, 94);

                this.billDateDTP.Visible = true;
                this.billDateDTP.Location = new System.Drawing.Point(24, 140);
                this.billDateDTP.Enabled = true;
                this.billDateDTP.TabIndex = 3;

                this.inwardDateDTP.Visible = false;
                this.label1.Visible = false;
                this.label7.Visible = true;
                this.label7.Location = new System.Drawing.Point(24, 74);
                this.label2.Visible = true;
                this.label2.Location = new System.Drawing.Point(24, 120);
                this.saveButton.Enabled = false;

                //DatagridView make
                dataGridView1.Columns.Add("Sl_No", "Sl_No");
                dataGridView1.Columns[0].ReadOnly = true;
                DataGridViewComboBoxColumn dgvCmb = new DataGridViewComboBoxColumn();
                dgvCmb.DataSource = this.batch_no;
                dgvCmb.HeaderText = "Batch Number";
                dataGridView1.Columns.Insert(1, dgvCmb);

                dataGridView1.Columns.Add("Weight", "Weight");
                dataGridView1.Columns.Add("Colour", "Colour");
                dataGridView1.Columns.Add("Quality", "Quality");
                dataGridView1.Columns.Add("Net Rate", "Net Rate");

                dataGridView1.Columns["Weight"].ReadOnly = true;
                dataGridView1.Columns["Colour"].ReadOnly = true;
                dataGridView1.Columns["Quality"].ReadOnly = true;
                dataGridView1.Columns["Net rate"].ReadOnly = true;

                //Create drop-down Dyeing Company lists
                var dataSource3 = new List<string>();
                DataTable d3 = c.getQC('d');
                dataSource3.Add("---Select---");

                for (int i = 0; i < d3.Rows.Count; i++)
                {
                    dataSource3.Add(d3.Rows[i][0].ToString());
                }
                this.dyeingCompanyCB.DataSource = dataSource3;
                this.dyeingCompanyCB.DisplayMember = "Dyeing_Company_Names";
                this.dyeingCompanyCB.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
                this.dyeingCompanyCB.AutoCompleteSource = AutoCompleteSource.ListItems;
                this.dyeingCompanyCB.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

                //Create drop-down lists
                var dataSource = new List<string>();
                DataTable d = c.getQC('f');
                for (int i = 0; i < d.Rows.Count; i++)
                {
                    dataSource.Add(d.Rows[i][0].ToString());
                }
                this.comboBox3CB.DataSource = dataSource;
                this.comboBox3CB.DisplayMember = "Financial Year";
                this.comboBox3CB.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
                this.comboBox3CB.AutoCompleteSource = AutoCompleteSource.ListItems;
                this.comboBox3CB.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

                //if only in view mode
                if (isEditable == false)
                {
                    this.Text += "(View Only)";
                    disable_form_edit();
                    this.deleteButton.Visible = true;
                    this.deleteButton.Enabled = true;
                }
                else
                {
                    this.Text += "(Edit)";
                    this.loadBatchButton.Enabled = false;
                    this.saveButton.Enabled = true;
                    this.dyeingCompanyCB.Enabled = false;
                    this.comboBox3CB.Enabled = false;
                }

                //Fill in required values
                this.inputDate.Value = Convert.ToDateTime(row["Date_Of_Input"].ToString());
                this.dyeingCompanyCB.SelectedIndex = this.dyeingCompanyCB.FindStringExact(row["Dyeing_Company_Name"].ToString());
                this.billNumberTextboxTB.Text = row["Bill_No"].ToString();
                this.voucherID = int.Parse(row["Voucher_ID"].ToString());
                this.comboBox3CB.SelectedIndex = this.comboBox3CB.FindStringExact(row["Batch_Fiscal_Year"].ToString());


                //Load data in datagridview dropdown
                this.loadData(row["Dyeing_Company_Name"].ToString(), row["Batch_Fiscal_Year"].ToString());
                //Load previous data
                string[] batch_nos = c.csvToArray(row["Batch_No_Arr"].ToString());
                for(int i=0; i<batch_nos.Length; i++)
                {
                    this.batch_no.Add(batch_nos[i]);
                }
                if (isEditable == false) dataGridView1.RowCount = batch_nos.Length;
                else dataGridView1.RowCount = batch_nos.Length + 1;
                //Fill data in datagridview
                for (int i = 0; i < batch_nos.Length; i++)
                {
                    dataGridView1.Rows[i].Cells[1].Value = batch_nos[i];
                }
                c.SetGridViewSortState(this.dataGridView1, DataGridViewColumnSortMode.NotSortable);
            }
            dataGridView1.ClearSelection();
            dataGridView1.CurrentCell = null;
        }
        private void M_V2_dyeingInwardForm_Load(object sender, EventArgs e)
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

            this.inwardDateDTP.Focus();
            if (Global.access == 2)
            {
                this.deleteButton.Visible = false;
            }
        }

        //Own functions
        public void disable_form_edit()
        {
            this.dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ClearSelection();
            this.deleteToolStripMenuItem.Enabled = false;
            this.comboBox3CB.Enabled = false;
            this.inputDate.Enabled = false;
            this.inwardDateDTP.Enabled = false;
            this.dyeingCompanyCB.Enabled = false;
            this.loadBatchButton.Enabled = false;
            this.saveButton.Enabled = false;
            this.dataGridView1.ReadOnly = true;
            this.billNumberTextboxTB.ReadOnly = true;
            this.billDateDTP.Enabled = false;
        }
        private float CellSum(int column)
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
                    if (c.Cell_Not_NullOrEmpty(this.dataGridView1, i, column))
                    {
                        //float dyeing_rate = float.Parse(c.getColumnBatchNo("Dyeing_Rate", int.Parse(this.dataGridView1.Rows[i].Cells[1].Value.ToString()), this.comboBox3CB.Text));
                        //Console.WriteLine(dyeing_rate.ToString());
                        //sum += float.Parse(dataGridView1.Rows[i].Cells[column].Value.ToString())*dyeing_rate;
                        sum += float.Parse(dataGridView1.Rows[i].Cells[column].Value.ToString());
                    }
                }
                return sum;
            }
            catch
            {
                Console.WriteLine("Excep");
                return sum;
            }
        }
        private void loadData(string dyeing_company, string batch_fiscal_year)
        {
            string[] d = null;
            if (this.addBill == true) d = c.getBatchesWithBillNoDyeingCompanyName(0, dyeing_company, batch_fiscal_year);
            else d = c.getBatch_StateDyeingCompanyColourQuality(1, dyeing_company, null, null, batch_fiscal_year);
            for (int i = 0; i < d.Length; i++)
            {
                this.batch_no.Add(d[i]);
            }
        }

        //Clicks
        private void saveButton_Click(object sender, EventArgs e)
        {
            //checks
            if (this.addBill == true)
            {
                try
                {
                    int.Parse(billNumberTextboxTB.Text);
                }
                catch
                {
                    c.ErrorBox("Please Enter Numeric Bill No");
                    return;
                }
            }
            if (dyeingCompanyCB.SelectedIndex == 0)
            {
                c.ErrorBox("Enter Select Dyeing Company Name", "Error");
                return;
            }
            if ((billNumberTextboxTB.Text == null || billNumberTextboxTB.Text == "") && billcheckBoxCK.Checked == false)
            {
                c.ErrorBox("Enter Bill Number", "Error");
                return;
            }
            if (billcheckBoxCK.Checked == true && this.addBill == false)
            {
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    int sum = 0;
                    if (!c.Cell_Not_NullOrEmpty(this.dataGridView1, i, 2))
                    {
                        sum++;
                    }
                    if (!c.Cell_Not_NullOrEmpty(this.dataGridView1, i, 6))
                    {
                        sum++;
                    }
                    if (sum == 1)
                    {
                        c.ErrorBox("Enter Slip Number in row " + (i + 1).ToString(), "Error");
                        return;
                    }

                }
            }
            if (!c.Cell_Not_NullOrEmpty(this.dataGridView1, 0, 1))
            {
                c.ErrorBox("Please enter Batch Numbers", "Error");
                return;
            }
            if (DateTime.Now.Date < this.inwardDateDTP.Value.Date)
            {
                if(addBill==false)
                {
                    c.ErrorBox("Inward Date is in the future", "Error");
                }
                return;
            }
            string batch_nos = "", slip_nos = "";
            int number = 0;

            List<int> temp = new List<int>();
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {

                //ComboBox c = (ComboBox)dataGridView1.EditingControl;
                if (!c.Cell_Not_NullOrEmpty(this.dataGridView1, i, 1))
                {
                    continue;
                }
                else
                {
                    batch_nos += dataGridView1.Rows[i].Cells[1].Value.ToString() + ",";
                    if (this.billcheckBoxCK.Checked == true && this.addBill == false)
                    {
                        slip_nos += dataGridView1.Rows[i].Cells[6].Value.ToString() + ",";
                    }
                    number++;

                    //to check for all different batch_nos
                    temp.Add(int.Parse(dataGridView1.Rows[i].Cells[1].Value.ToString()));
                    var distinctBytes = new HashSet<int>(temp);
                    bool allDifferent = distinctBytes.Count == temp.Count;
                    if (allDifferent == false)
                    {
                        c.ErrorBox("Please Enter Distinct Tray Nos at Row: " + (i + 1).ToString(), "Error");
                        return;
                    }

                }
            }
            string sendbill_no = "-1";
            string send_bill_date = null;
            if (this.billcheckBoxCK.Checked == true) sendbill_no = "0";
            else
            {
                sendbill_no = billNumberTextboxTB.Text.ToString();
                send_bill_date = billDateDTP.Value.Date.ToString("MM-dd-yyyy").Substring(0, 10);
                slip_nos = "";
            }
            if (this.edit_form == true)
            {
                if (this.addBill == true)
                {
                    bool editbill = c.editBillNosVoucher(this.voucherID, sendbill_no, inputDate.Value, batch_nos, dyeingCompanyCB.SelectedItem.ToString(), this.comboBox3CB.SelectedItem.ToString(), this.billDateDTP.Value);
                    if (editbill == true)
                    {
                        dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.LawnGreen;
                        disable_form_edit();
                        this.v1_history.loadData();
                    }
                    return;
                }
                else
                {
                    bool edited = c.editDyeingInwardVoucher(this.voucherID, inputDate.Value, inwardDateDTP.Value, dyeingCompanyCB.SelectedItem.ToString(), sendbill_no, batch_nos, this.comboBox3CB.SelectedItem.ToString(), send_bill_date, slip_nos);
                    if (edited == true)
                    {
                        dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.LawnGreen;
                        disable_form_edit();
                        this.v1_history.loadData();
                    }
                    else return;
                }
            }
            else
            {
                if (this.addBill == true)
                {
                    bool addbill = c.addBillNosVoucher(sendbill_no, inputDate.Value, batch_nos, dyeingCompanyCB.SelectedItem.ToString(), this.comboBox3CB.SelectedItem.ToString(), this.billDateDTP.Value);
                    if (addbill == true)
                    {
                        dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.LawnGreen;
                        disable_form_edit();
                    }
                    return;
                }
                else
                {
                    bool added = c.addDyeingInwardVoucher(inputDate.Value, inwardDateDTP.Value, dyeingCompanyCB.SelectedItem.ToString(), sendbill_no, batch_nos, this.comboBox3CB.SelectedItem.ToString(), send_bill_date, slip_nos);
                    if (added == true)
                    {
                        dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.LawnGreen;
                        disable_form_edit();
                    }
                    else return;
                }
            }
            dataGridView1.EnableHeadersVisualStyles = false;
        }
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.edit_form == true && this.addBill == false)
            {
                int count = dataGridView1.SelectedRows.Count;
                Console.WriteLine("Count= " + count);
                for (int i = 0; i < count; i++)
                {
                    if (dataGridView1.SelectedRows[0].Index == dataGridView1.Rows.Count - 1)
                    {
                        dataGridView1.SelectedRows[0].Selected = false;
                        continue;
                    }
                    int rowindex = dataGridView1.SelectedRows[0].Index;
                    string batch_no = dataGridView1.Rows[rowindex].Cells[1].Value.ToString();
                    int value = -1;
                    bool value2 = this.batch_editable.TryGetValue(batch_no, out value);
                    if (value2 == true && value>=0)
                    {
                        c.ErrorBox("Cannot delete entry at row: " + (rowindex + 1).ToString(), "Error");
                        dataGridView1.Rows[rowindex].Selected = false;
                        continue;
                    }
                    dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);
                    Console.WriteLine("Deleting row: " + rowindex);

                }
                dynamicWeightLabel.Text = CellSum(5).ToString("F3");
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
                    dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);
                }
                dynamicWeightLabel.Text = CellSum(5).ToString("F3");
            }

        }
        private void loadCartonButton_Click(object sender, EventArgs e)
        {
            if (dyeingCompanyCB.SelectedIndex == 0)
            {
                c.ErrorBox("Enter Enter Dyeing Company Name", "Error");
                return;
            }
            if (comboBox3CB.SelectedIndex < 0)
            {
                c.ErrorBox("Please select Batch Financial Year", "Error");
                return;
            }
            this.loadData(this.dyeingCompanyCB.SelectedItem.ToString(), comboBox3CB.SelectedItem.ToString()); ;
            if (this.batch_no.Count - 1 == 0)
            {
                c.WarningBox("No Batches Loaded");
                return;
            }
            else
            {
                if (this.edit_form == false)
                {
                    c.SuccessBox("Loaded " + (this.batch_no.Count - 1).ToString() + " Batches");
                }
            }
            this.saveButton.Enabled = true;
            this.loadBatchButton.Enabled = false;
            this.dyeingCompanyCB.Enabled = false;
            this.saveButton.Enabled = true;
            this.comboBox3CB.Enabled = false;
        }
        private void deleteButton_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Confirm Delete", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                bool deleted;
                if (this.addBill == true)
                {
                    deleted = c.deleteBillNosVoucher(this.voucherID);
                }
                else
                {
                    deleted = c.deleteDyeingInwardVoucher(this.voucherID);
                }
                if (deleted == true)
                {
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

        //Datagridview 1
        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is DataGridViewComboBoxEditingControl)
            {
                ((ComboBox)e.Control).DropDownStyle = ComboBoxStyle.DropDown;
                ((ComboBox)e.Control).AutoCompleteSource = AutoCompleteSource.ListItems;
                ((ComboBox)e.Control).AutoCompleteMode = AutoCompleteMode.Append;
            }
        }
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1 && e.RowIndex >= 0)
            {
                if(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null)
                {
                    dataGridView1.Rows[e.RowIndex].Cells[2].Value = null;
                    dataGridView1.Rows[e.RowIndex].Cells[3].Value = null;
                    dataGridView1.Rows[e.RowIndex].Cells[4].Value = null;
                    dataGridView1.Rows[e.RowIndex].Cells[5].Value = null;
                    if (this.addBill == false) dataGridView1.Rows[e.RowIndex].Cells[6].Value = null;
                    dynamicWeightLabel.Text = CellSum(5).ToString("F2");
                    return;
                }
                int batch_no = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                DataRow row = c.getBatchRow_BatchNo(batch_no, this.comboBox3CB.SelectedItem.ToString());
                dataGridView1.Rows[e.RowIndex].Cells[2].Value = row["Net_Weight"].ToString();
                dataGridView1.Rows[e.RowIndex].Cells[3].Value = row["Colour"].ToString();
                dataGridView1.Rows[e.RowIndex].Cells[4].Value = row["Quality"].ToString();
                dataGridView1.Rows[e.RowIndex].Cells[5].Value = float.Parse(row["Net_Weight"].ToString()) * float.Parse(row["Dyeing_Rate"].ToString());
                if(this.addBill==false)
                {
                    if (!(row["Slip_No"].ToString() == null || row["Slip_No"].ToString() == ""))
                    {
                        dataGridView1.Rows[e.RowIndex].Cells[6].Value = row["Slip_No"].ToString();
                    }
                }
                dynamicWeightLabel.Text = CellSum(5).ToString("F3");

            }
        }
        private void dataGridView1_RowPostPaint_1(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            if (e.RowIndex != dataGridView1.Rows.Count - 1 || dataGridView1.AllowUserToAddRows == false)
            {
                dataGridView1.Rows[e.RowIndex].Cells[0].Value = e.RowIndex + 1;
            }
        }
        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            int col = dataGridView1.SelectedCells[0].ColumnIndex;
            if (e.KeyCode == Keys.Tab &&
               ( (col!=0) || this.edit_cmd_send == true))
            {
                bool edit_cmd_local = this.edit_cmd_send;
                this.edit_cmd_send = false;
                int rowindex_tab = dataGridView1.SelectedCells[0].RowIndex;
                if (edit_cmd_local == true) rowindex_tab--;

                if (rowindex_tab < 0)
                {
                    SendKeys.Send("{tab}");
                    return;
                }
                if (dataGridView1.Rows.Count - 2 == rowindex_tab && ((col==6 && this.addBill==false)|| (col == 5 && this.addBill == true)))
                {
                    DataGridViewRow row = (DataGridViewRow)dataGridView1.Rows[rowindex_tab].Clone();
                    dataGridView1.Rows.Add(row);
                }
                if(col==1)
                {
                    Console.WriteLine("col1 " + edit_cmd_local);
                    SendKeys.Send("{tab}");
                    if (edit_cmd_local == true)
                    {
                        SendKeys.Send("{tab}");
                    }
                }
                if(col==2)
                {
                    Console.WriteLine("col2");
                    SendKeys.Send("{tab}");
                }
                if(col==3)
                {
                    Console.WriteLine("col3");
                    SendKeys.Send("{tab}");
                }
                if (col == 4)
                {
                    Console.WriteLine("col4");
                    SendKeys.Send("{tab}");
                }
                if (col == 5 && this.addBill == true)
                {
                    Console.WriteLine("col4");
                    SendKeys.Send("{tab}");
                }
                if (col == 5 && this.addBill == false && this.billcheckBoxCK.Checked==false)
                {
                    Console.WriteLine("col5 no slip");
                    SendKeys.Send("{tab}");
                }
                if (col == 6 && this.addBill==false)
                {
                    Console.WriteLine("col4");
                    SendKeys.Send("{tab}");
                }

            }
            if (e.KeyCode == Keys.Enter &&
               (dataGridView1.SelectedCells.Cast<DataGridViewCell>().Any(x => x.ColumnIndex == 1) || this.edit_cmd_send == true))
            {
                if (dataGridView1.ReadOnly == true) return;
                dataGridView1.BeginEdit(true);
                ComboBox c = (ComboBox)dataGridView1.EditingControl;
                if (c != null) c.DroppedDown = true;
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
        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                dataGridView1.Rows[e.RowIndex].Selected = true;
            }
        }
        private void dataGridView1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dataGridView1.IsCurrentCellDirty)
            {
                dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }
        
        
    }
}
