using Factory_Inventory.Factory_Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Factory_Inventory
{
    public partial class M_V1_cartonTwistForm : Form
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
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private DbConnect c;
        private bool edit_cmd_send = false;
        private bool edit_form = false;
        private List<string> carton_data;
        private M_V_history v1_history;
        private int voucherID;
        public M_V1_cartonTwistForm()
        {
            InitializeComponent();
            this.c = new DbConnect();
            this.carton_data = new List<string>();
            this.carton_data.Add("");

            //Create drop-down lists
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
            //this.comboBox1.
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

            //Create drop-down lists
            var dataSource3 = new List<string>();
            DataTable d3 = c.getQC('f');
            dataSource3.Add("---Select---");

            for (int i = 0; i < d3.Rows.Count; i++)
            {
                dataSource3.Add(d3.Rows[i][0].ToString());
            }
            this.comboBox3.DataSource = dataSource3;
            this.comboBox3.DisplayMember = "Financial Year";
            this.comboBox3.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.comboBox3.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.comboBox3.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            this.comboBox3.SelectedIndex = this.comboBox3.FindStringExact(c.getFinancialYear(this.issueDate.Value));


            //DatagridView
            dataGridView1.Columns.Add("Sl_No", "Sl_No");
            dataGridView1.Columns[0].ReadOnly = true;
            DataGridViewComboBoxColumn dgvCmb = new DataGridViewComboBoxColumn();
            dgvCmb.AutoComplete = false;
            dgvCmb.DataSource = this.carton_data;

            dgvCmb.HeaderText = "Carton Number";
            dataGridView1.Columns.Insert(1, dgvCmb);
            dataGridView1.Columns[1].Width = 250;
            dataGridView1.Columns.Add("Weight", "Weight");
            dataGridView1.Columns[2].ReadOnly = true;
            dataGridView1.RowCount = 10;
        }

        public M_V1_cartonTwistForm(DataRow row, bool isEditable, M_V_history v1_history)
        {
            InitializeComponent();
            this.edit_form = true;
            this.v1_history = v1_history;
            this.c = new DbConnect();
            this.carton_data = new List<string>();
            this.carton_data.Add("");

            //Create drop-down lists
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

            //Create drop-down lists
            var dataSource3 = new List<string>();
            DataTable d3 = c.getQC('f');
            dataSource3.Add("---Select---");

            for (int i = 0; i < d3.Rows.Count; i++)
            {
                dataSource3.Add(d3.Rows[i][0].ToString());
            }
            this.comboBox3.DataSource = dataSource3;
            this.comboBox3.DisplayMember = "Financial Year";
            this.comboBox3.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.comboBox3.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.comboBox3.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            this.comboBox3.SelectedIndex = this.comboBox3.FindStringExact(c.getFinancialYear(this.issueDate.Value));
            

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
                this.issueDate.Enabled = false;
                this.comboBox1.Enabled = false;
                this.comboBox2.Enabled = false;
                this.comboBox3.Enabled = false;
                this.loadCartonButton.Enabled = false;
                this.saveButton.Enabled = false;
                this.dataGridView1.ReadOnly = true;
            }

            else
            {
                this.issueDate.Enabled = true;
                this.comboBox1.Enabled = false;
                this.comboBox2.Enabled = false;
                this.comboBox3.Enabled = false;
                this.saveButton.Enabled = true;
                this.dataGridView1.ReadOnly = false;
                this.loadCartonButton.Enabled = false;
            }

            this.issueDate.Value = Convert.ToDateTime(row["Date_Of_Issue"].ToString());
            if (this.comboBox1.FindStringExact(row["Quality"].ToString()) == -1)
            {
                dataSource1.Add(row["Quality"].ToString());
                this.comboBox1.DataSource = null;
                this.comboBox1.DataSource = dataSource1;

            }
            this.comboBox1.SelectedIndex = this.comboBox1.FindStringExact(row["Quality"].ToString());
            if (this.comboBox2.FindStringExact(row["Company_Name"].ToString()) == -1)
            {
                dataSource2.Add(row["Company_Name"].ToString());
                this.comboBox2.DataSource = null;
                this.comboBox2.DataSource = dataSource2;

            }
            this.comboBox2.SelectedIndex = this.comboBox2.FindStringExact(row["Company_Name"].ToString());
            this.voucherID = int.Parse(row["Voucher_ID"].ToString());
            string[] carton_no = c.csvToArray(row["Carton_No_Arr"].ToString());
            Console.WriteLine("------------------");
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
                bool exists = this.carton_data.Any(s => s.Contains(cartoon));
                if(exists==false)
                {
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "";
                    return;
                }
                if (this.comboBox3.SelectedIndex <= 0) return;
                DataTable dt = c.getCartonWeight(cartoon, this.comboBox3.SelectedItem.ToString());
                if (dt.Rows.Count <= 0) return;
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
                if (dataGridView1.Rows.Count - 1 < rowindex_tab + 1)
                {
                    dataGridView1.Rows.Add();
                }
                SendKeys.Send("{tab}");
            }
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
            if (dataGridView1.Rows[0].Cells[1].Value==null)
            {
                MessageBox.Show("Please enter Carton Numbers", "Error");
                return;
            }
            if (this.inputDate.Value.Date < this.issueDate.Value.Date)
            {
                MessageBox.Show("Issue Date is in the future", "Error");
                return;
            }
            string cartonno = "";
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
                    cartonno += dataGridView1.Rows[i].Cells[1].Value + ",";
                    number++;
                    temp.Add(int.Parse(dataGridView1.Rows[i].Cells[1].Value.ToString()));

                    var distinctBytes = new HashSet<int>(temp);
                    bool allDifferent = distinctBytes.Count == temp.Count;
                    if (allDifferent == false)
                    {
                        MessageBox.Show("Please Enter Distinct Carton Nos at Row: " + (i + 1).ToString(), "Error");
                        return;
                    }

                }
            }

            if (this.edit_form == false)
            {
                bool added = c.addTwistVoucher(inputDate.Value, issueDate.Value, comboBox1.SelectedItem.ToString(), comboBox2.SelectedItem.ToString(), cartonno, number, this.comboBox3.SelectedItem.ToString());
                if(added==false)
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
                bool edited = c.editTwistVoucher(this.voucherID, issueDate.Value, comboBox1.SelectedItem.ToString(), comboBox2.SelectedItem.ToString(), cartonno, number, this.comboBox3.SelectedItem.ToString());
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
            this.issueDate.Enabled = false;
            this.comboBox1.Enabled = false;
            this.comboBox2.Enabled = false;
            this.comboBox3.Enabled = false;
            this.saveButton.Enabled = true;
            this.dataGridView1.ReadOnly = false;
            this.loadCartonButton.Enabled = false;
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int count = dataGridView1.SelectedRows.Count;
            for (int i = 0; i < count; i++)
            {
                if (dataGridView1.SelectedRows[0].Index == dataGridView1.Rows.Count - 1) continue;
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
            this.loadData(this.comboBox1.SelectedItem.ToString(), this.comboBox2.SelectedItem.ToString(), this.comboBox3.SelectedItem.ToString());
            this.loadCartonButton.Enabled = false;
            this.comboBox1.Enabled = false;
            this.comboBox2.Enabled = false;
            this.comboBox3.Enabled = false;
        }

        private void loadData(string quality, string company, string fiscalyear)
        {
            DataTable d = c.getCartonStateQualityCompany(1, quality, company, fiscalyear);
            Console.WriteLine(d.Rows.Count);
            for(int i=0; i<d.Rows.Count; i++)
            {
               // Console.WriteLine(d.Rows[i][0].ToString());
                this.carton_data.Add(d.Rows[i][0].ToString());
            }
            if(this.edit_form==false) MessageBox.Show("Loaded Data");
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
            if (e.Button == MouseButtons.Right && e.RowIndex>=0)
            {
                dataGridView1.Rows[e.RowIndex].Selected = true;
            }
        }
    }
}
