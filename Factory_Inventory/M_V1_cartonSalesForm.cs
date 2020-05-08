using Factory_Inventory.Factory_Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Factory_Inventory
{
    public partial class M_V1_cartonSalesForm : Form
    {
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Tab &&
                dataGridView1.EditingControl != null &&
                msg.HWnd == dataGridView1.EditingControl.Handle &&
                dataGridView1.SelectedCells
                    .Cast<DataGridViewCell>()
                    .Any(x => x.ColumnIndex == 2))
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
                this.comboBox3CB.Focus();
                this.ActiveControl = comboBox3CB;
                return false;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private DbConnect c;
        private bool edit_cmd_send = false;
        private bool edit_form = false;               //True if form is being edited
        private List<string> carton_data;           //List that stores carton numbers from SQL
        private M_V_history v1_history;
        private int voucherID;
        public M_V1_cartonSalesForm()
        {
            InitializeComponent();
            this.c = new DbConnect();
            this.carton_data = new List<string>();
            this.carton_data.Add("");

            //Create drop-down quality list
            var dataSource1 = new List<string>();
            DataTable d1 = c.getQC('q');
            dataSource1.Add("---Select---");

            for (int i = 0; i < d1.Rows.Count; i++)
            {
                dataSource1.Add(d1.Rows[i][3].ToString());
            }
            this.comboBox1CB.DataSource = dataSource1;
            this.comboBox1CB.DisplayMember = "Quality";
            this.comboBox1CB.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.comboBox1CB.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.comboBox1CB.AutoCompleteMode = AutoCompleteMode.SuggestAppend;


            //Create drop-down Company list
            var dataSource2 = new List<string>();
            DataTable d2 = c.getQC('c');
            dataSource2.Add("---Select---");

            for (int i = 0; i < d2.Rows.Count; i++)
            {
                dataSource2.Add(d2.Rows[i][0].ToString());
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
            this.comboBox3CB.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            //Create drop-down lists
            var dataSource4 = new List<string>();
            DataTable d4 = c.getQC('f');
            dataSource4.Add("---Select---");

            for (int i = 0; i < d4.Rows.Count; i++)
            {
                dataSource4.Add(d4.Rows[i][0].ToString());
            }
            this.comboBox4CB.DataSource = dataSource4;
            this.comboBox4CB.DisplayMember = "Financial Year";
            this.comboBox4CB.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.comboBox4CB.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.comboBox4CB.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            this.comboBox4CB.SelectedIndex = this.comboBox4CB.FindStringExact(c.getFinancialYear(this.issueDateDTP.Value));


            //DatagridView
            dataGridView1.Columns.Add("Sl_No", "Sl_No");
            dataGridView1.Columns[0].ReadOnly = true;
            DataGridViewComboBoxColumn dgvCmb = new DataGridViewComboBoxColumn();
            dgvCmb.DataSource = this.carton_data;

            dgvCmb.HeaderText = "Carton Number";
            dataGridView1.Columns.Insert(1, dgvCmb);
            dataGridView1.Columns[1].Width = 250;
            dataGridView1.Columns.Add("Weight", "Weight");
            dataGridView1.Columns[2].ReadOnly = true;
            dataGridView1.RowCount = 10;

            c.SetGridViewSortState(this.dataGridView1, DataGridViewColumnSortMode.NotSortable);

        }

        public M_V1_cartonSalesForm(DataRow row, bool isEditable, M_V_history v1_history)
        {
            InitializeComponent();
            this.edit_form = true;
            this.v1_history = v1_history;
            this.c = new DbConnect();
            this.carton_data = new List<string>();
            this.carton_data.Add("");

            //Create drop-down list for quality
            var dataSource1 = new List<string>();
            DataTable d1 = c.getQC('q');
            dataSource1.Add("---Select---");

            for (int i = 0; i < d1.Rows.Count; i++)
            {
                dataSource1.Add(d1.Rows[i][3].ToString());
            }
            this.comboBox1CB.DataSource = dataSource1;
            this.comboBox1CB.DisplayMember = "Quality";
            this.comboBox1CB.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.comboBox1CB.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.comboBox1CB.AutoCompleteMode = AutoCompleteMode.SuggestAppend;


            var dataSource2 = new List<string>();
            DataTable d2 = c.getQC('c');
            dataSource2.Add("---Select---");

            for (int i = 0; i < d2.Rows.Count; i++)
            {
                dataSource2.Add(d2.Rows[i][0].ToString());
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

            //Create drop-down lists
            var dataSource4 = new List<string>();
            DataTable d4 = c.getQC('f');
            dataSource4.Add("---Select---");

            for (int i = 0; i < d4.Rows.Count; i++)
            {
                dataSource4.Add(d4.Rows[i][0].ToString());
            }
            this.comboBox4CB.DataSource = dataSource4;
            this.comboBox4CB.DisplayMember = "Financial Year";
            this.comboBox4CB.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.comboBox4CB.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.comboBox4CB.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            this.comboBox4CB.SelectedIndex = this.comboBox4CB.FindStringExact(c.getFinancialYear(this.issueDateDTP.Value));


            //DatagridView
            dataGridView1.Columns.Add("Sl_No", "Sl_No");
            dataGridView1.Columns[0].ReadOnly = true;

            DataGridViewComboBoxColumn dgvCmb = new DataGridViewComboBoxColumn();
            dgvCmb.HeaderText = "Carton Number";
            dgvCmb.DataSource = this.carton_data;
            dataGridView1.Columns.Insert(1, dgvCmb);
            dataGridView1.Columns[1].Width = 250;
            dataGridView1.Columns.Add("Weight", "Weight");
            dataGridView1.Columns[2].ReadOnly = true;

            if (isEditable == false)
            {
                this.issueDateDTP.Enabled = false;
                this.comboBox1CB.Enabled = false;
                this.comboBox2CB.Enabled = false;
                this.comboBox3CB.Enabled = false;
                this.comboBox4CB.Enabled = false;
                this.loadCartonButton.Enabled = false;
                this.saveButton.Enabled = false;
                this.dataGridView1.ReadOnly = true;
                this.sellingPriceTextboxTB.Enabled = false;
            }
            else
            {
                this.issueDateDTP.Enabled = true;
                this.comboBox1CB.Enabled = false;
                this.comboBox2CB.Enabled = false;
                this.comboBox4CB.Enabled = false;
                this.saveButton.Enabled = true;
                this.dataGridView1.ReadOnly = false;
            }

            this.issueDateDTP.Value = Convert.ToDateTime(row["Date_Of_Issue"].ToString());
            if (this.comboBox1CB.FindStringExact(row["Quality"].ToString()) == -1)
            {
                dataSource1.Add(row["Quality"].ToString());
                this.comboBox1CB.DataSource = null;
                this.comboBox1CB.DataSource = dataSource1;

            }
            this.comboBox1CB.SelectedIndex = this.comboBox1CB.FindStringExact(row["Quality"].ToString());
            if (this.comboBox2CB.FindStringExact(row["Company_Name"].ToString()) == -1)
            {
                dataSource2.Add(row["Company_Name"].ToString());
                this.comboBox2CB.DataSource = null;
                this.comboBox2CB.DataSource = dataSource2;

            }
            this.comboBox2CB.SelectedIndex = this.comboBox2CB.FindStringExact(row["Company_Name"].ToString());
            this.voucherID = int.Parse(row["Voucher_ID"].ToString());
            if (this.comboBox3CB.FindStringExact(row["Customer"].ToString()) == -1)
            {
                dataSource3.Add(row["Customer"].ToString());
                this.comboBox3CB.DataSource = null;
                this.comboBox3CB.DataSource = dataSource3;

            }
            this.comboBox3CB.SelectedIndex = this.comboBox3CB.FindStringExact(row["Customer"].ToString());
            this.comboBox4CB.SelectedIndex = this.comboBox4CB.FindStringExact(row["Carton_Fiscal_Year"].ToString());

            this.sellingPriceTextboxTB.Text = row["Selling_Price"].ToString();

            string[] carton_no = c.csvToArray(row["Carton_No_Arr"].ToString());
            for(int i=0; i<carton_no.Length; i++)
            {
                this.carton_data.Add(carton_no[i]);
            }
            this.loadData(row["Quality"].ToString(), row["Company_Name"].ToString(), row["Carton_Fiscal_Year"].ToString());

            dataGridView1.RowCount = carton_no.Length + 1;

            for (int i = 0; i < carton_no.Length; i++)
            {
                dataGridView1.Rows[i].Cells[1].Value = carton_no[i];
            }

            c.SetGridViewSortState(this.dataGridView1, DataGridViewColumnSortMode.NotSortable);
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

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1 && e.RowIndex >= 0)
            {
                if(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null)
                {
                    dataGridView1.Rows[e.RowIndex].Cells[2].Value = null;
                    dynamicWeightLabel.Text = CellSum().ToString("F3");
                    return;
                }
                string cartoon = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                if (this.comboBox4CB.SelectedIndex <= 0) return;
                DataTable dt = c.getCartonWeight(cartoon, this.comboBox4CB.SelectedItem.ToString());
                dataGridView1.Rows[e.RowIndex].Cells[2].Value = dt.Rows[0][0];
                dynamicWeightLabel.Text = CellSum().ToString("F3");
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
            if (e.KeyCode == Keys.Tab &&
                (dataGridView1.SelectedCells.Cast<DataGridViewCell>().Any(x => x.ColumnIndex == 1) || this.edit_cmd_send == true))
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
                if (dataGridView1.Rows.Count - 2 == rowindex_tab)
                {
                    DataGridViewRow row = (DataGridViewRow)dataGridView1.Rows[rowindex_tab].Clone();
                    dataGridView1.Rows.Add(row);
                }
                if (dataGridView1.Rows.Count - 1 < rowindex_tab + 1)
                {
                    dataGridView1.Rows.Add();
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
                    return;
                }
                if (dataGridView1.Rows.Count - 2 == rowindex_tab)
                {
                    DataGridViewRow row = (DataGridViewRow)dataGridView1.Rows[rowindex_tab].Clone();
                    dataGridView1.Rows.Add(row);
                }
                if (dataGridView1.Rows.Count - 1 < rowindex_tab + 1)
                {
                    dataGridView1.Rows.Add();
                }
                SendKeys.Send("{tab}");
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

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        { 
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            //checks

            if (comboBox1CB.SelectedIndex == 0)
            {
                c.ErrorBox("Enter Select Quality", "Error");
                return;
            }
            if (comboBox2CB.SelectedIndex == 0)
            {
                c.ErrorBox("Enter Select Company Name", "Error");
                return;
            }
            if (dataGridView1.Rows[0].Cells[1].Value==null)
            {
                c.ErrorBox("Please enter Carton Numbers", "Error");
                return;
            }
            if(comboBox3CB.SelectedIndex==0)
            {
                c.ErrorBox("Enter Select Customer Name", "Error");
                return;
            }
            if (sellingPriceTextboxTB.Text == null)
            {
                c.ErrorBox("Enter Select selling price", "Error");
                return;
            }
            if (sellingPriceTextboxTB.Text == "")
            {
                c.ErrorBox("Enter Select selling price", "Error");
                return;
            }
            try
            {
                float.Parse(sellingPriceTextboxTB.Text);
            }
            catch
            {
                c.ErrorBox("Please enter numeric selling price only", "Error");
                return;
            }
            if(inputDate.Value.Date<issueDateDTP.Value.Date)
            {
                c.ErrorBox("Issue Date is in the future", "Error");
                return;
            }
            if(this.comboBox3CB.FindStringExact(this.comboBox3CB.Text)==-1)
            {
                c.ErrorBox("Select valid customer", "Error");
                this.comboBox3CB.SelectedIndex = 0;
                return;
            }
            string cartonno = "";
            int number = 0;

            List<int> temp = new List<int>();
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                if (dataGridView1.Rows[i].Cells[1].Value == null || dataGridView1.Rows[i].Cells[1].Value.ToString() == "")
                {
                    continue;
                }
                else
                {
                    cartonno += dataGridView1.Rows[i].Cells[1].Value + ",";
                    number++;
                    temp.Add(int.Parse(dataGridView1.Rows[i].Cells[1].Value.ToString()));

                    var distinctBytes = new HashSet<int>(temp);
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
                bool added = c.addSalesVoucher(inputDate.Value, issueDateDTP.Value, comboBox1CB.SelectedItem.ToString(), comboBox2CB.SelectedItem.ToString(), cartonno, number, comboBox3CB.SelectedItem.ToString(), float.Parse(sellingPriceTextboxTB.Text), comboBox4CB.SelectedItem.ToString());
                if (added == false)
                {
                    return;
                }
                else
                {
                    this.disable_form_edit();
                }
            }
            else
            {
                bool edited = c.editSalesVoucher(this.voucherID, issueDateDTP.Value, comboBox1CB.SelectedItem.ToString(), comboBox2CB.SelectedItem.ToString(), cartonno, number, comboBox3CB.SelectedItem.ToString(), float.Parse(sellingPriceTextboxTB.Text), comboBox4CB.SelectedItem.ToString());
                if (edited == false)
                {
                    return;
                }
                else
                {
                    this.disable_form_edit();
                    this.v1_history.loadData();
                }
            }
        }

        public void disable_form_edit()
        {
            this.issueDateDTP.Enabled = false;
            this.comboBox1CB.Enabled = false;
            this.comboBox2CB.Enabled = false;
            this.comboBox3CB.Enabled = false;
            this.comboBox4CB.Enabled = false;
            this.loadCartonButton.Enabled = false;
            this.saveButton.Enabled = false;
            this.dataGridView1.ReadOnly = true;
            this.sellingPriceTextboxTB.Enabled = false;
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
            dynamicWeightLabel.Text = CellSum().ToString("F3");
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

        private void loadCartonButton_Click(object sender, EventArgs e)
        {
            if (comboBox1CB.SelectedIndex == 0)
            {
                c.ErrorBox("Enter Select Quality", "Error");
                return;
            }
            if (comboBox2CB.SelectedIndex == 0)
            {
                c.ErrorBox("Enter Select Company Name", "Error");
                return;
            }
            this.loadData(this.comboBox1CB.SelectedItem.ToString(), this.comboBox2CB.SelectedItem.ToString(), this.comboBox4CB.SelectedItem.ToString());
            this.loadCartonButton.Enabled = false;
            this.comboBox1CB.Enabled = false;
            this.comboBox2CB.Enabled = false;
            this.comboBox4CB.Enabled = false;
        }

        //Used to get carton numbers given quality, company and state
        private void loadData(string quality, string company, string carton_financial_year)
        {
            DataTable d = c.getCartonStateQualityCompany(1, quality, company, carton_financial_year);          //returns carton numbers
            for(int i=0; i<d.Rows.Count; i++)
            {
                this.carton_data.Add(d.Rows[i][0].ToString());
            }
            if (this.edit_form == false)
            {
                c.SuccessBox("Loaded "+d.Rows.Count.ToString()+" Cartons");
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

            this.issueDateDTP.Focus();
        }
    }
}
