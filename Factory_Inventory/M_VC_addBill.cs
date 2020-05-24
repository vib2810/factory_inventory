using Factory_Inventory.Factory_Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Factory_Inventory
{
    public partial class M_VC_addBill : Form
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
        private List<string> do_no;
        private M_V_history v1_history;
        private int voucher_id;
        private string tablename;
        Dictionary<string, bool> batch_editable = new Dictionary<string, bool>();

        //Form functions
        public M_VC_addBill(string form)
        {
            InitializeComponent();
            this.Name = "Add Bill";
            this.tablename = form;
            this.c = new DbConnect();
            this.do_no = new List<string>();
            this.do_no.Add("");

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

            //Create drop-down lists
            var dataSource1 = new List<string>();
            DataTable d = c.getQC('f');

            for (int i = 0; i < d.Rows.Count; i++)
            {
                dataSource1.Add(d.Rows[i][0].ToString());
            }
            this.financialYearCB.DataSource = dataSource1;
            this.financialYearCB.DisplayMember = "Financial Year";
            this.financialYearCB.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.financialYearCB.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.financialYearCB.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            this.financialYearCB.SelectedIndex = this.financialYearCB.FindStringExact(c.getFinancialYear(this.inputDate.Value));

            //Create drop-down Quality lists
            var dataSource3 = new List<string>();
            DataTable d3 = c.getQC('q');
            dataSource3.Add("---Select---");

            for (int i = 0; i < d3.Rows.Count; i++)
            {
                if (this.tablename == "Carton")
                {
                    dataSource3.Add(d3.Rows[i]["Quality_Before_Twist"].ToString());
                }
                else if (this.tablename == "Carton_Produced")
                {
                    dataSource3.Add(d3.Rows[i]["Quality"].ToString());
                }
            }
            this.qualityCB.DataSource = dataSource3;
            this.qualityCB.DisplayMember = "Quality";
            this.qualityCB.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.qualityCB.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.qualityCB.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            //DatagridView
            dataGridView1.Columns.Add("Sl_No", "Sl_No");
            dataGridView1.Columns[0].ReadOnly = true;
            DataGridViewComboBoxColumn dgvCmb = new DataGridViewComboBoxColumn();
            dgvCmb.DataSource = this.do_no;
            dgvCmb.HeaderText = "DO Number";
            dataGridView1.Columns.Insert(1, dgvCmb);
            dataGridView1.Columns.Add("DO Weight", "DO Weight");
            dataGridView1.Columns.Add("DO Amount", "DO Amount");
            dataGridView1.Columns[2].ReadOnly = true;
            dataGridView1.Columns[3].ReadOnly = true;
            dataGridView1.RowCount = 10;

            c.SetGridViewSortState(this.dataGridView1, DataGridViewColumnSortMode.NotSortable);
        }
        public M_VC_addBill(DataRow row, bool isEditable, M_V_history v1_history, string form)
        {
            InitializeComponent();
            //common initializations
            this.Name = "Add Bill";
            this.tablename = form;
            this.edit_form = true;
            this.v1_history = v1_history;
            this.c = new DbConnect();
            this.do_no = new List<string>();
            this.do_no.Add("");

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

            //Create drop-down lists
            var dataSource1 = new List<string>();
            DataTable d = c.getQC('f');

            for (int i = 0; i < d.Rows.Count; i++)
            {
                dataSource1.Add(d.Rows[i][0].ToString());
            }
            this.financialYearCB.DataSource = dataSource1;
            this.financialYearCB.DisplayMember = "Financial Year";
            this.financialYearCB.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.financialYearCB.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.financialYearCB.AutoCompleteMode = AutoCompleteMode.SuggestAppend;


            //Create drop-down Quality lists
            var dataSource3 = new List<string>();
            DataTable d3 = c.getQC('q');
            dataSource3.Add("---Select---");

            for (int i = 0; i < d3.Rows.Count; i++)
            {
                if(this.tablename=="Carton")
                {
                    dataSource3.Add(d3.Rows[i]["Quality_Before_Twist"].ToString());
                }
                else if(this.tablename=="Carton_Produced")
                {
                    dataSource3.Add(d3.Rows[i]["Quality"].ToString());
                }
            }
            this.qualityCB.DataSource = dataSource3;
            this.qualityCB.DisplayMember = "Quality";
            this.qualityCB.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.qualityCB.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.qualityCB.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            //DatagridView make
            dataGridView1.Columns.Add("Sl_No", "Sl_No");
            dataGridView1.Columns[0].ReadOnly = true;
            DataGridViewComboBoxColumn dgvCmb = new DataGridViewComboBoxColumn();
            //List<string> final_list = this.do_no.Distinct().ToList();
            dgvCmb.HeaderText = "DO Number";
            dataGridView1.Columns.Insert(1, dgvCmb);
            dataGridView1.Columns.Add("DO Weight", "DO Weight");
            dataGridView1.Columns.Add("DO Amount", "DO Amount");
            dataGridView1.Columns[2].ReadOnly = true;
            dataGridView1.Columns[3].ReadOnly = true;

            //if only in view mode
            if (isEditable == false)
            {
                this.Text += "(View Only)";
                this.deleteButton.Visible = true;
                this.deleteButton.Enabled = true;
                this.disable_form_edit();
            }
            else
            {
                this.Text += "(Edit)";
                this.typeCB.Enabled = false;
                this.financialYearCB.Enabled = false;
                this.qualityCB.Enabled = false;
                this.loadDOButton.Enabled = false;
                this.saveButton.Enabled = true;
            }

            //Fill in required values
            this.inputDate.Value = Convert.ToDateTime(row["Date_Of_Input"].ToString());
            this.billDateDTP.Value = Convert.ToDateTime(row["Sale_Bill_Date"].ToString());
            this.qualityCB.SelectedIndex = this.qualityCB.FindStringExact(row["Quality"].ToString());
            this.billNumberTextboxTB.Text = row["Sale_Bill_No"].ToString();
            this.voucher_id = int.Parse(row["Voucher_ID"].ToString());
            this.financialYearCB.SelectedIndex = this.financialYearCB.FindStringExact(row["DO_Fiscal_Year"].ToString());
            this.typeCB.SelectedIndex = this.typeCB.FindStringExact(row["Type_Of_Sale"].ToString());
            this.billWeightTB.Text = row["Sale_Bill_Weight"].ToString();
            this.billAmountTB.Text = row["Sale_Bill_Amount"].ToString();
            this.netDOWeightTB.Text = row["Sale_Bill_Weight_Calc"].ToString();
            this.netDOAmountTB.Text = row["Sale_Bill_Amount_Calc"].ToString();


            //Load data in datagridview dropdown
            this.loadData(row["Quality"].ToString(), row["DO_Fiscal_Year"].ToString(), row["Type_Of_Sale"].ToString());
            //Load previous data
            string[] do_nos = c.csvToArray(row["DO_No_Arr"].ToString());
            for(int i=0; i<do_nos.Length; i++)
            {
                this.do_no.Add(do_nos[i]);
            }
            dataGridView1.RowCount = do_nos.Length + 1;
            //Fill data in datagridview
            for (int i = 0; i < do_nos.Length; i++)
            {
                dataGridView1.Rows[i].Cells[1].Value = do_nos[i];
            }
            dgvCmb.DataSource = this.do_no;
            c.SetGridViewSortState(this.dataGridView1, DataGridViewColumnSortMode.NotSortable);
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

            this.billDateDTP.Focus();
            if (Global.access == 2)
            {
                this.deleteButton.Visible = false;
            }
        }

        //Own functions
        public void disable_form_edit()
        {
            this.inputDate.Enabled = false;
            this.qualityCB.Enabled = false;
            this.loadDOButton.Enabled = false;
            this.saveButton.Enabled = false;
            this.dataGridView1.ReadOnly = true;
            this.billNumberTextboxTB.Enabled = false;
            this.billDateDTP.Enabled = false;
            this.billWeightTB.Enabled = false;
            this.billAmountTB.Enabled = false;
            this.deleteToolStripMenuItem.Enabled = false;
            this.typeCB.Enabled = false;
            this.financialYearCB.Enabled = false;
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
        private void loadData(string quality, string do_fiscal_year, string type)
        {

            string[] d = c.getDO_QualityFiscalYearType(quality, do_fiscal_year, type, this.tablename);
            for (int i = 0; i < d.Length; i++)
            {
                this.do_no.Add(d[i]);
            }
        }

        //Clicks
        private void saveButton_Click(object sender, EventArgs e)
        {
            //checks
            if (!c.Cell_Not_NullOrEmpty(this.dataGridView1, 0, 1))
            {
                c.ErrorBox("Please enter DO Numbers", "Error");
                return;
            }
            try
            {
                float.Parse(this.billWeightTB.Text);
            }
            catch
            {
                c.ErrorBox("Enter numeric bill weight", "Error");
                return;
            }
            try
            {
                float.Parse(this.billAmountTB.Text);
            }
            catch
            {
                c.ErrorBox("Enter numeric bill amount", "Error");
                return;
            }
            if (Math.Abs(Double.Parse(billWeightTB.Text) - Double.Parse(netDOWeightTB.Text)) > 1D)
            {
                c.ErrorBox("Bill Weight is does not match total DO weight", "Error");
                return;
            }
            if (typeCB.SelectedItem.ToString() == "1")
            {
                
                if (Math.Abs(float.Parse(this.netDOAmountTB.Text) - float.Parse(this.billAmountTB.Text)) > 5F)
                {
                    c.ErrorBox("Bill Amount and Net DO Amount have a difference greater than ₹5", "Error");
                    return;
                }
            }
            if (typeCB.SelectedItem.ToString() == "0")
            {

                if (float.Parse(this.netDOAmountTB.Text) < float.Parse(this.billAmountTB.Text))
                {
                    c.ErrorBox("Bill Amount is more than total DO Amount", "Error");
                    return;
                }
            }
            string do_nos = "";
            List<string> temp = new List<string>();
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                if (!c.Cell_Not_NullOrEmpty(this.dataGridView1, i, 1))
                {
                    continue;
                }
                else
                {
                    do_nos += dataGridView1.Rows[i].Cells[1].Value.ToString() + ",";
                    //to check for all different do_nos
                    temp.Add(dataGridView1.Rows[i].Cells[1].Value.ToString());
                    var distinctBytes = new HashSet<string>(temp);
                    bool allDifferent = distinctBytes.Count == temp.Count;
                    if (allDifferent == false)
                    {
                        c.ErrorBox("Please Enter Distinct DO Numbers at Row: " + (i + 1).ToString(), "Error");
                        return;
                    }

                }
            }
            if (this.edit_form == true)
            {
                bool editbill = c.editSalesBillNosVoucher(this.voucher_id, inputDate.Value, billDateDTP.Value, do_nos, this.financialYearCB.SelectedItem.ToString(), billNumberTextboxTB.Text, float.Parse(billWeightTB.Text), float.Parse(billAmountTB.Text), float.Parse(netDOWeightTB.Text), float.Parse(netDOAmountTB.Text), this.tablename);
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
                bool addbill = c.addSalesBillNosVoucher(inputDate.Value, billDateDTP.Value, do_nos, qualityCB.SelectedItem.ToString(), this.financialYearCB.SelectedItem.ToString(), int.Parse(typeCB.Text), billNumberTextboxTB.Text, float.Parse(billWeightTB.Text), float.Parse(billAmountTB.Text), float.Parse(netDOWeightTB.Text), float.Parse(netDOAmountTB.Text), this.tablename);
                if (addbill == true)
                {
                    dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.LawnGreen;
                    disable_form_edit();
                }
                return;
            }
            dataGridView1.EnableHeadersVisualStyles = false;
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
            this.netDOWeightTB.Text = CellSum(2).ToString("F3");
            this.netDOAmountTB.Text = CellSum(3).ToString("F2");
        }
        private void loadCartonButton_Click(object sender, EventArgs e)
        {
            if (qualityCB.SelectedIndex == 0)
            {
                c.ErrorBox("Enter Quality", "Error");
                return;
            }
            if (financialYearCB.SelectedIndex < 0)
            {
                c.ErrorBox("Please select Batch Financial Year", "Error");
                return;
            }
            if (typeCB.SelectedIndex == 0)
            {
                c.ErrorBox("Please select type of DOs", "Error");
                return;
            }
            this.loadData(this.qualityCB.SelectedItem.ToString(), financialYearCB.SelectedItem.ToString(), this.typeCB.Text); ;
            if (this.do_no.Count - 1 == 0)
            {
                c.WarningBox("No DOs Loaded");
                return;
            }
            else
            {
                if (this.edit_form == false)
                {
                    c.SuccessBox("Loaded " + (this.do_no.Count - 1).ToString() + " DOs");
                }
            }
            this.saveButton.Enabled = true;
            this.loadDOButton.Enabled = false;
            this.qualityCB.Enabled = false;
            this.saveButton.Enabled = true;
            this.financialYearCB.Enabled = false;
            this.typeCB.Enabled = false;
        }
        private void deleteButton_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Confirm Delete", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                bool deleted = c.deleteSalesBillNosVoucher(this.voucher_id);
                if (deleted == true)
                {
                    c.SuccessBox("Voucher Deleted Successfully");
                    this.saveButton.Enabled = false;
                    this.v1_history.loadData();
                }
                else return;
            }
            else if (dialogResult == DialogResult.No)
            {
                return;
            }
        }

        //DataGridView 1
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
                    dataGridView1.Rows[e.RowIndex].Cells["DO Weight"].Value = null;
                    dataGridView1.Rows[e.RowIndex].Cells["DO Amount"].Value = null;
                    this.netDOWeightTB.Text = CellSum(2).ToString("F3");
                    this.netDOAmountTB.Text = CellSum(3).ToString("F2");
                    return;
                }
                string do_no = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                DataRow row = c.getSalesRow(do_no, this.financialYearCB.SelectedItem.ToString());
                dataGridView1.Rows[e.RowIndex].Cells["DO Weight"].Value = row["Net_Weight"].ToString();
                dataGridView1.Rows[e.RowIndex].Cells["DO Amount"].Value = (float.Parse(row["Sale_Rate"].ToString())*float.Parse(row["Net_Weight"].ToString())).ToString("F2");
                this.netDOWeightTB.Text = CellSum(2).ToString("F3");
                this.netDOAmountTB.Text = CellSum(3).ToString("F2");
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
                //if (dataGridView1.Rows.Count - 2 == rowindex_tab && ((col==6 && this.addBill==false)|| (col == 5 && this.addBill == true)))
                //{
                //    DataGridViewRow row = (DataGridViewRow)dataGridView1.Rows[rowindex_tab].Clone();
                //    dataGridView1.Rows.Add(row);
                //}
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
                //if (col == 5 && this.addBill == true)
                //{
                //    Console.WriteLine("col4");
                //    SendKeys.Send("{tab}");
                //}
                //if (col == 5 && this.addBill == false && this.billcheckBoxCK.Checked==false)
                //{
                //    Console.WriteLine("col5 no slip");
                //    SendKeys.Send("{tab}");
                //}
                //if (col == 6 && this.addBill==false)
                //{
                //    Console.WriteLine("col4");
                //    SendKeys.Send("{tab}");
                //}

            }
            if (e.KeyCode == Keys.Enter &&
               (dataGridView1.SelectedCells.Cast<DataGridViewCell>().Any(x => x.ColumnIndex == 1) || this.edit_cmd_send == true))
            {
                dataGridView1.BeginEdit(true);
                ComboBox c = (ComboBox)dataGridView1.EditingControl;
                c.DroppedDown = true;
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
