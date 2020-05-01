using Factory_Inventory.Factory_Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Factory_Inventory
{
    public partial class M_V2_dyeingIssueForm : Form
    {
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Tab &&
                dataGridView1.EditingControl != null &&
                msg.HWnd == dataGridView1.EditingControl.Handle &&
                dataGridView1.SelectedCells
                    .Cast<DataGridViewCell>()
                    .Any(x => x.ColumnIndex == 3))
            {
                this.edit_cmd_send = true;
                SendKeys.Send("{Tab}");
                return false;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private DbConnect c;
        private bool edit_cmd_send = false;
        private bool edit_form = false;
        private List<string> tray_no;
        private string[] tray_no_this, tray_id_this;
        private M_V_history v1_history;
        private int voucherID;
        private int batch_state;
        private string old_fiscal_year;
        public M_V2_dyeingIssueForm()
        {
            InitializeComponent();
            this.c = new DbConnect();
            this.tray_no = new List<string>();
            this.tray_no.Add("");
            this.saveButton.Enabled = false;

            //Create drop-down Quality lists
            var dataSource1 = new List<string>();
            DataTable d1 = c.getQC('q');
            dataSource1.Add("---Select---");

            for (int i = 0; i < d1.Rows.Count; i++)
            {
                dataSource1.Add(d1.Rows[i][0].ToString());
            }
            this.comboBox1.DataSource = dataSource1;
            this.comboBox1.DisplayMember = "Quality";
            this.comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.comboBox1.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;


            //Create drop-down Company lists
            var dataSource2 = new List<string>();
            DataTable d2 = c.getQC('c');
            dataSource2.Add("---Select---");

            for (int i = 0; i < d2.Rows.Count; i++)
            {
                dataSource2.Add(d2.Rows[i][0].ToString());
            }
            this.comboBox2.DataSource = dataSource2;
            this.comboBox2.DisplayMember = "Company_Names";
            this.comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.comboBox2.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.comboBox2.AutoCompleteMode = AutoCompleteMode.SuggestAppend;



            //Create drop-down Dyeing Company lists
            var dataSource3 = new List<string>();
            DataTable d3 = c.getQC('d');
            dataSource3.Add("---Select---");

            for (int i = 0; i < d3.Rows.Count; i++)
            {
                dataSource3.Add(d3.Rows[i][0].ToString());
            }
            this.comboBox3.DataSource = dataSource3;
            this.comboBox3.DisplayMember = "Dyeing_Company_Names";
            this.comboBox3.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.comboBox3.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.comboBox3.AutoCompleteMode = AutoCompleteMode.SuggestAppend;


            //Create drop-down Colour lists
            var dataSource4 = new List<string>();
            DataTable d4 = c.getQC('l');
            dataSource4.Add("---Select---");

            for (int i = 0; i < d4.Rows.Count; i++)
            {
                dataSource4.Add(d4.Rows[i][0].ToString());
            }
            List<string> final_list = dataSource4.Distinct().ToList();
            this.comboBox4.DataSource = final_list;
            this.comboBox4.DisplayMember = "Colours";
            this.comboBox4.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.comboBox4.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.comboBox4.AutoCompleteMode = AutoCompleteMode.SuggestAppend;


            //DatagridView
            dataGridView1.Columns.Add("Sl_No", "Sl_No");
            dataGridView1.Columns[0].ReadOnly = true;
            DataGridViewComboBoxColumn dgvCmb = new DataGridViewComboBoxColumn();
            dgvCmb.DataSource = this.tray_no;

            dgvCmb.HeaderText = "Tray Number";
            dataGridView1.Columns.Insert(1, dgvCmb);
            dataGridView1.Columns[1].Width = 250;
            dataGridView1.Columns.Add("Weight", "Weight");
            dataGridView1.Columns[2].ReadOnly = true;
            dataGridView1.RowCount = 10;

        }

        public M_V2_dyeingIssueForm(DataRow row, bool isEditable, M_V_history v1_history)
        {

            InitializeComponent();
            this.edit_form = true;
            this.v1_history = v1_history;
            this.c = new DbConnect();
            this.tray_no = new List<string>();
            this.tray_no.Add("");
            this.saveButton.Enabled = false;

            //Create drop-down Quality lists
            var dataSource1 = new List<string>();
            DataTable d1 = c.getQC('q');
            dataSource1.Add("---Select---");

            for (int i = 0; i < d1.Rows.Count; i++)
            {
                dataSource1.Add(d1.Rows[i][0].ToString());
            }
            this.comboBox1.DataSource = dataSource1;
            this.comboBox1.DisplayMember = "Quality";
            this.comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.comboBox1.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;


            //Create drop-down Company lists
            var dataSource2 = new List<string>();
            DataTable d2 = c.getQC('c');
            dataSource2.Add("---Select---");
            for (int i = 0; i < d2.Rows.Count; i++)
            {
                dataSource2.Add(d2.Rows[i][0].ToString());
            }
            this.comboBox2.DataSource = dataSource2;
            this.comboBox2.DisplayMember = "Company_Names";
            this.comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.comboBox2.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.comboBox2.AutoCompleteMode = AutoCompleteMode.SuggestAppend;



            //Create drop-down Dyeing Company lists
            var dataSource3 = new List<string>();
            DataTable d3 = c.getQC('d');
            dataSource3.Add("---Select---");

            for (int i = 0; i < d3.Rows.Count; i++)
            {
                dataSource3.Add(d3.Rows[i][0].ToString());
            }
            this.comboBox3.DataSource = dataSource3;
            this.comboBox3.DisplayMember = "Dyeing_Company_Names";
            this.comboBox3.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.comboBox3.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.comboBox3.AutoCompleteMode = AutoCompleteMode.SuggestAppend;


            //Create drop-down Colour lists
            var dataSource4 = new List<string>();
            DataTable d4 = c.getQC('l');
            dataSource4.Add("---Select---");

            for (int i = 0; i < d4.Rows.Count; i++)
            {
                dataSource4.Add(d4.Rows[i][0].ToString());
            }
            List<string> final_list = dataSource4.Distinct().ToList();
            this.comboBox4.DataSource = final_list;
            this.comboBox4.DisplayMember = "Colours";
            this.comboBox4.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.comboBox4.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.comboBox4.AutoCompleteMode = AutoCompleteMode.SuggestAppend;


            //DatagridView
            dataGridView1.Columns.Add("Sl_No", "Sl_No");
            dataGridView1.Columns[0].ReadOnly = true;
            DataGridViewComboBoxColumn dgvCmb = new DataGridViewComboBoxColumn();
            dgvCmb.DataSource = this.tray_no;

            dgvCmb.HeaderText = "Tray Number";
            dataGridView1.Columns.Insert(1, dgvCmb);
            dataGridView1.Columns[1].Width = 250;
            dataGridView1.Columns.Add("Weight", "Weight");
            dataGridView1.Columns[2].ReadOnly = true;
            dataGridView1.RowCount = 10;


            this.batch_state = c.getBatchState(int.Parse(row["Batch_No"].ToString()), row["Batch_Fiscal_Year"].ToString());
            if (batch_state == 2)
            {
                dynamicEditableLabel.Text = "This voucher is not editable as the Batch has gone for dyeing";
                isEditable = false;
            }
            else if (batch_state == 3)
            {
                dynamicEditableLabel.Text = "This voucher is not editable as the Batch has been packed";
                isEditable = false;
            }
            if (isEditable == false)
            {
                this.inputDate.Enabled = false;
                this.issueDate.Enabled = false;
                this.comboBox1.Enabled = false;
                this.comboBox2.Enabled = false;
                this.comboBox3.Enabled = false;
                this.comboBox4.Enabled = false;
                this.loadCartonButton.Enabled = false;
                this.saveButton.Enabled = false;
                this.dataGridView1.ReadOnly = true;
                this.rateTextBox.Enabled = false;
            }
            else
            { 
                //no option to edit company name and quality
                this.comboBox1.Enabled = false;
                this.comboBox2.Enabled = false;
                this.comboBox3.Enabled = true;
                this.comboBox4.Enabled = true;
                this.saveButton.Enabled = true;
                this.loadCartonButton.Enabled = false;
                this.dataGridView1.ReadOnly = false;
                this.issueDate.Enabled = false;
            }
            this.inputDate.Value = Convert.ToDateTime(row["Date_Of_Input"].ToString());
            this.issueDate.Value = Convert.ToDateTime(row["Date_Of_Issue"].ToString());
            this.comboBox1.SelectedIndex = this.comboBox1.FindStringExact(row["Quality"].ToString());
            this.comboBox2.SelectedIndex = this.comboBox2.FindStringExact(row["Company_Name"].ToString());
            this.comboBox3.SelectedIndex = this.comboBox3.FindStringExact(row["Dyeing_Company_Name"].ToString());
            this.comboBox4.SelectedIndex = this.comboBox4.FindStringExact(row["Colour"].ToString());
            this.old_fiscal_year = row["Batch_Fiscal_Year"].ToString();
            batchNumberTextbox.Text = row["Batch_No"].ToString();

            this.voucherID = int.Parse(row["Voucher_ID"].ToString());
            this.tray_no_this = c.csvToArray(row["Tray_No_Arr"].ToString());
            this.tray_id_this = c.csvToArray(row["Tray_ID_Arr"].ToString());
            for(int i=0; i< tray_no_this.Length; i++)
            {
                this.tray_no.Add(tray_no_this[i]);
            }
            this.loadData(row["Quality"].ToString(), row["Company_Name"].ToString());
            dataGridView1.RowCount = tray_no_this.Length + 1;

            for (int i = 0; i < tray_no_this.Length; i++)
            {
                dataGridView1.Rows[i].Cells[1].Value = tray_no_this[i];
            }
        }

        public void disable_form_edit()
        {
            this.inputDate.Enabled = false;
            this.issueDate.Enabled = false;
            this.comboBox1.Enabled = false;
            this.comboBox2.Enabled = false;
            this.comboBox3.Enabled = false;
            this.comboBox4.Enabled = false;
            this.loadCartonButton.Enabled = false;
            this.saveButton.Enabled = false;
            this.dataGridView1.ReadOnly = true;
            this.rateTextBox.Enabled = false;
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
                if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null)
                {
                    dataGridView1.Rows[e.RowIndex].Cells[2].Value = null;
                    dynamicWeightLabel.Text = CellSum().ToString("F3");
                    return;
                }
                string trayno = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                float weight;
                if(edit_form==true)
                {
                    weight = c.getTrayWeight(int.Parse(tray_id_this[e.RowIndex]), this.batch_state);
                }
                else
                {
                    int trayid=c.getTrayID(trayno);
                    weight = c.getTrayWeight(trayid, 1);
                    
                }
                dataGridView1.Rows[e.RowIndex].Cells[2].Value = weight.ToString("F3");
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
                if (dataGridView1.Rows.Count - 1 < rowindex_tab + 1)
                {
                    dataGridView1.Rows.Add();
                }
                if (edit_cmd_local == false)
                {
                    SendKeys.Send("{tab}");
                }
            }
            //if (e.KeyCode == Keys.Enter &&
            //   (dataGridView1.SelectedCells.Cast<DataGridViewCell>().Any(x => x.ColumnIndex == 1) || this.edit_cmd_send == true))
            //{
            //    dataGridView1.BeginEdit(true);
            //    ComboBox c = (ComboBox)dataGridView1.EditingControl;
            //    c.DroppedDown = true;
            //    e.Handled = true;
            //}
            if (e.KeyCode == Keys.Tab &&
               (dataGridView1.SelectedCells.Cast<DataGridViewCell>().Any(x => x.ColumnIndex == 2)))
            {
                int rowindex_tab = dataGridView1.SelectedCells[0].RowIndex;
                if (rowindex_tab < 0)
                {
                    SendKeys.Send("{tab}");
                    return;
                }
                if (dataGridView1.Rows.Count - 1 < rowindex_tab + 1)
                {
                    dataGridView1.Rows.Add();
                }
                SendKeys.Send("{tab}");
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
            //Console.WriteLine("Inside cell begin edit");
            //if(e.RowIndex >=0 && e.ColumnIndex==1)
            //{
            //    Console.WriteLine("Valid cell begin edit");
            //    Console.WriteLine(dataGridView1.Rows[e.RowIndex].Cells[1].Value);
            //    if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            //    {
            //        Console.WriteLine("Inside inside cell begin edit");
            //        this.old_quality = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            //        Console.WriteLine(this.old_quality);
            //    }
            //}
        }
        private void saveButton_Click(object sender, EventArgs e)
        {
            //checks
            if (comboBox1.SelectedIndex == 0)
            {
                MessageBox.Show("Enter Select Quality", "Error");
                return;
            }
            if (comboBox2.SelectedIndex == 0)
            {
                MessageBox.Show("Enter Select Company Name", "Error");
                return;
            }
            if (comboBox3.SelectedIndex == 0)
            {
                MessageBox.Show("Enter Select Dyeing Company Name", "Error");
                return;
            }
            if (comboBox4.SelectedIndex == 0)
            {
                MessageBox.Show("Enter Select Colour", "Error");
                return;
            }
            if(batchNumberTextbox.Text==null || batchNumberTextbox.Text=="")
            {
                MessageBox.Show("Enter Batch Number", "Error");
            }
            try
            {
                int.Parse(batchNumberTextbox.Text);
            }
            catch
            {
                MessageBox.Show("Enter numeric Batch Number only", "Error");
                return;
            }
            if (dataGridView1.Rows[0].Cells[1].Value==null)
            {
                MessageBox.Show("Please enter Tray Numbers", "Error");
                return;
            }
            try
            {
                float.Parse(rateTextBox.Text);
            }
            catch
            {
                MessageBox.Show("Enter numeric rate only", "Error");
                return;
            }
            if (this.inputDate.Value.Date < this.issueDate.Value.Date)
            {
                MessageBox.Show("Issue Date is in the future", "Error");
                return;
            }
            string trayno = "", trayid="";
            int number = 0;

            List<int> temp = new List<int>();
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {

                //ComboBox c = (ComboBox)dataGridView1.EditingControl;
                if (dataGridView1.Rows[i].Cells[1].Value == null || dataGridView1.Rows[i].Cells[1].Value.ToString() == "")
                {
                    continue;
                }
                else
                {
                    trayno += dataGridView1.Rows[i].Cells[1].Value.ToString() + ",";
                    trayid += c.getTrayID(dataGridView1.Rows[i].Cells[1].Value.ToString()).ToString() + ",";
                    number++;

                    //to check for all different tray_nos
                    temp.Add(int.Parse(dataGridView1.Rows[i].Cells[1].Value.ToString()));
                    var distinctBytes = new HashSet<int>(temp);
                    bool allDifferent = distinctBytes.Count == temp.Count;
                    if (allDifferent == false)
                    {
                        MessageBox.Show("Please Enter Distinct Tray Nos at Row: " + (i + 1).ToString(), "Error");
                        return;
                    }

                }
            }

            if (this.edit_form == false)
            {
                bool added= c.addDyeingIssueVoucher(inputDate.Value, issueDate.Value, comboBox1.SelectedItem.ToString(), comboBox2.SelectedItem.ToString(), trayno, number, comboBox4.SelectedItem.ToString(), comboBox3.SelectedItem.ToString(), int.Parse(batchNumberTextbox.Text), trayid, CellSum(), float.Parse(rateTextBox.Text));
                if (added == true) disable_form_edit();
                else return;
            }
            else
            {
                bool edited=c.editDyeingIssueVoucher(this.voucherID, this.old_fiscal_year, inputDate.Value, issueDate.Value, comboBox1.SelectedItem.ToString(), comboBox2.SelectedItem.ToString(), trayno, number, comboBox4.SelectedItem.ToString(), comboBox3.SelectedItem.ToString(), int.Parse(batchNumberTextbox.Text), trayid, CellSum(), float.Parse(rateTextBox.Text), trayid);
                if (edited == true)
                {
                    disable_form_edit();
                    this.v1_history.loadData();
                }
                else return;
            }
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
            if (comboBox1.SelectedIndex == 0)
            {
                MessageBox.Show("Enter Select Quality", "Error");
                return;
            }
            if (comboBox2.SelectedIndex == 0)
            {
                MessageBox.Show("Enter Select Company Name", "Error");
                return;
            }
            if(this.edit_form==false)
            {
                int batch_no = c.getNextBatchNumber("Highest_Batch_No", c.getFinancialYear(issueDate.Value));
                if (batch_no == -1)
                {
                    batchNumberTextbox.Text = "Error";
                }
                else
                {
                    batchNumberTextbox.Text = batch_no.ToString();
                }
            }
            this.loadData(this.comboBox1.SelectedItem.ToString(), this.comboBox2.SelectedItem.ToString());
            this.loadCartonButton.Enabled = false;
            this.comboBox1.Enabled = false;
            this.comboBox2.Enabled = false;
            this.saveButton.Enabled = true;
            this.issueDate.Enabled = false; //because the next batch number is coming from the date
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillRate();
        }

        private void fillRate()
        {
            if (comboBox1.SelectedIndex == 0 || comboBox4.SelectedIndex == 0)
            {
                rateTextBox.Text = "";
                return;
            }
            float f = c.getDyeingRate(comboBox4.SelectedItem.ToString(), comboBox1.SelectedItem.ToString());
            if (f == -1F)
            {
                rateTextBox.Text = "";
                return;
            }
            rateTextBox.Text = (c.getDyeingRate(comboBox4.SelectedItem.ToString(), comboBox1.SelectedItem.ToString())).ToString();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillRate();
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

        private void loadData(string quality, string company)
        {
            DataTable d = c.getTrayStateQualityCompany(1, quality, company);
            Console.WriteLine(d.Rows.Count);
            for(int i=0; i<d.Rows.Count; i++)
            {
                this.tray_no.Add(d.Rows[i][0].ToString());
            }
            if (this.edit_form==false) MessageBox.Show("Loaded Data");
        }
    }
}
