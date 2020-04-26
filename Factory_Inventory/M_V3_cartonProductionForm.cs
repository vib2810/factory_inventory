using Factory_Inventory.Factory_Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
        private List<string> batch_nos;
        private string[] tray_no_this, tray_id_this;
        private M_V_history v1_history;
        private int voucherID;
        private int batch_state;
        public M_V3_cartonProductionForm()
        {
            InitializeComponent();
            this.c = new DbConnect();
            this.batch_nos = new List<string>();
            this.saveButton.Enabled = false;

            //Create drop-down Colour list
            var dataSource1 = new List<string>();
            DataTable dt = c.getQC('l');
            dataSource1.Add("---Select---");
            for(int i=0;i<dt.Rows.Count;i++)
            {
                dataSource1.Add(dt.Rows[i][0].ToString());
            }
            List<string> final_list = dataSource1.Distinct().ToList();
            this.colourCombobox.DataSource = final_list;
            this.colourCombobox.DisplayMember = "Colour";
            this.colourCombobox.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.colourCombobox.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.colourCombobox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            //Create drop-down Quality list
            var dataSource2 = new List<string>();
            dt = c.getQC('q');
            dataSource2.Add("---Select---");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dataSource2.Add(dt.Rows[i][0].ToString());
            }
            this.qualityCombobox.DataSource = dataSource2;
            this.qualityCombobox.DisplayMember = "Quality";
            this.qualityCombobox.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.qualityCombobox.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.qualityCombobox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            //Create drop-down Dyeing Company list
            var dataSource3 = new List<string>();
            dt = c.getQC('d');
            dataSource3.Add("---Select---"); 
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dataSource3.Add(dt.Rows[i][0].ToString());
            }
            this.dyeingCompanyCombobox.DataSource = dataSource3;
            this.dyeingCompanyCombobox.DisplayMember = "Dyeing_Company_Names";
            this.dyeingCompanyCombobox.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.dyeingCompanyCombobox.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.dyeingCompanyCombobox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;


            //DatagridView 1
            dataGridView1.Columns.Add("Sl_No", "Sl_No");
            dataGridView1.Columns[0].ReadOnly = true;
            //DataGridViewComboBoxColumn dgvCmb = new DataGridViewComboBoxColumn();
            //dgvCmb.DataSource = this.tray_no;
            dataGridView1.Columns.Add("Carton_Number", "Carton Number");
            dataGridView1.Columns[1].Width = 250;
            //dataGridView1.Columns.Add("Weight", "Weight");
            //dataGridView1.Columns[2].ReadOnly = true;
            //dataGridView1.RowCount = 10;

        }

        public M_V3_cartonProductionForm(DataRow row, bool isEditable, M_V_history v1_history)
        {
            
            InitializeComponent();
            this.edit_form = true;
            this.v1_history = v1_history;
            this.c = new DbConnect();
            this.batch_nos = new List<string>();
            this.saveButton.Enabled = false;

            //Create drop-down Colour list
            var dataSource1 = new List<string>();
            DataTable dt = c.getQC('l');
            dataSource1.Add("---Select---");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dataSource1.Add(dt.Rows[i][0].ToString());
            }
            this.colourCombobox.DataSource = dataSource1;
            this.colourCombobox.DisplayMember = "Colour";
            this.colourCombobox.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.colourCombobox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.colourCombobox.AutoCompleteSource = AutoCompleteSource.ListItems;

            //Create drop-down Quality list
            var dataSource2 = new List<string>();
            dt = c.getQC('q');
            dataSource2.Add("---Select---");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dataSource2.Add(dt.Rows[i][0].ToString());
            }
            this.qualityCombobox.DataSource = dataSource2;
            this.qualityCombobox.DisplayMember = "Quality";
            this.qualityCombobox.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.qualityCombobox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.qualityCombobox.AutoCompleteSource = AutoCompleteSource.ListItems;

            //Create drop-down Dyeing Company list
            var dataSource3 = new List<string>();
            dt = c.getQC('d');
            dataSource3.Add("---Select---");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dataSource2.Add(dt.Rows[i][0].ToString());
            }
            this.dyeingCompanyCombobox.DataSource = dataSource3;
            this.dyeingCompanyCombobox.DisplayMember = "Quality";
            this.dyeingCompanyCombobox.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.dyeingCompanyCombobox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.dyeingCompanyCombobox.AutoCompleteSource = AutoCompleteSource.ListItems;




            //DatagridView
            dataGridView1.Columns.Add("Sl_No", "Sl_No");
            dataGridView1.Columns[0].ReadOnly = true;
            DataGridViewComboBoxColumn dgvCmb = new DataGridViewComboBoxColumn();
            //dgvCmb.DataSource = this.tray_no;

            dgvCmb.HeaderText = "Tray Number";
            dataGridView1.Columns.Insert(1, dgvCmb);
            dataGridView1.Columns[1].Width = 250;
            dataGridView1.Columns.Add("Weight", "Weight");
            dataGridView1.Columns[2].ReadOnly = true;
            dataGridView1.RowCount = 10;

            
            this.batch_state = c.getBatchState(int.Parse(row["Batch_No"].ToString()));
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
                this.productionDate.Enabled = false;
                this.loadDataButton.Enabled = false;
                this.saveButton.Enabled = false;
                this.dataGridView1.ReadOnly = true;
            }
            else
            { 
                //no option to edit company name and quality
                this.productionDate.Enabled = false;
                this.saveButton.Enabled = true;
                this.loadDataButton.Enabled = false;
                this.dataGridView1.ReadOnly = false;
            }
            this.inputDate.Value = Convert.ToDateTime(row["Date_Of_Input"].ToString());
            this.productionDate.Value = Convert.ToDateTime(row["Date_Of_Issue"].ToString());

            this.voucherID = int.Parse(row["Voucher_ID"].ToString());
            this.tray_no_this = c.csvToArray(row["Tray_No_Arr"].ToString());
            this.tray_id_this = c.csvToArray(row["Tray_ID_Arr"].ToString());
            for(int i=0; i< tray_no_this.Length; i++)
            {
                //this.tray_no.Add(tray_no_this[i]);
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
            this.productionDate.Enabled = false;
            this.loadDataButton.Enabled = false;
            this.saveButton.Enabled = false;
            this.dataGridView1.ReadOnly = true;
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
                    Console.WriteLine("aaaaaaaaaaaaaaa"+tray_id_this[e.RowIndex]+"aaaaaaaaaaaa");
                    weight = c.getTrayWeight(int.Parse(tray_id_this[e.RowIndex]), this.batch_state);
                }
                else
                {
                    int trayid=c.getTrayID(trayno);
                    weight = c.getTrayWeight(trayid, 1);
                    
                }
                dataGridView1.Rows[e.RowIndex].Cells[2].Value = weight.ToString();
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
        }
        private void saveButton_Click(object sender, EventArgs e)
        {
            //checks
            //if (comboBox1.SelectedIndex == 0)
            {
                MessageBox.Show("Enter Select Quality", "Error");
                return;
            }
           // if(colourTextbox.Text==null || colourTextbox.Text=="")
            {
                //MessageBox.Show("Enter Batch Number", "Error");
            }
            try
            {
               // int.Parse(colourTextbox.Text);
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
                //bool added= c.addDyeingIssueVoucher(inputDate.Value.Date.ToString("MM-dd-yyyy").Substring(0, 10), productionDate.Value.Date.ToString("MM-dd-yyyy").Substring(0, 10), comboBox1.SelectedItem.ToString(), comboBox2.SelectedItem.ToString(), trayno, number, comboBox4.SelectedItem.ToString(), comboBox3.SelectedItem.ToString(), int.Parse(colourTextbox.Text), trayid, CellSum(), float.Parse(rateTextBox.Text));
                //if (added == true) disable_form_edit();
                //else return;
            }
            else
            {
                //bool edited=c.editDyeingIssueVoucher(this.voucherID, inputDate.Value.Date.ToString("MM-dd-yyyy").Substring(0, 10), productionDate.Value.Date.ToString("MM-dd-yyyy").Substring(0, 10), comboBox1.SelectedItem.ToString(), comboBox2.SelectedItem.ToString(), trayno, number, comboBox4.SelectedItem.ToString(), comboBox3.SelectedItem.ToString(), int.Parse(colourTextbox.Text), trayid, CellSum(), float.Parse(rateTextBox.Text), trayid);
                //if (edited == true) disable_form_edit();
                //else return;
                //this.v1_history.loadData();
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
            if (colourCombobox.SelectedIndex == 0)
            {
                MessageBox.Show("Select Colour Number", "Error");
                return;
            }
            if (qualityCombobox.SelectedIndex == 0)
            {
                MessageBox.Show("Select Quality", "Error");
                return;
            }
            if (dyeingCompanyCombobox.SelectedIndex == 0)
            {
                MessageBox.Show("Select Dyeing Company Name", "Error");
                return;
            }
            string[] batch_nos_arr = c.getBatch_StateDyeingCompanyColourQuality(2, dyeingCompanyCombobox.SelectedItem.ToString(), colourCombobox.SelectedItem.ToString(), qualityCombobox.SelectedItem.ToString(), "");
            //this.loadData(this.comboBox1.SelectedItem.ToString(), this.comboBox2.SelectedItem.ToString());
            for(int i=0;i<batch_nos_arr.Length;i++)
            {
                this.batch_nos.Add(batch_nos_arr[i]);
            }
            this.loadDataButton.Enabled = false;
            //this.comboBox1.Enabled = false;
            this.saveButton.Enabled = true;
            //Datagridview 2
            //dataGridView1.Columns.Add("Batch_No", "Batch_No");

            DataGridViewComboBoxColumn dgvCmb1 = new DataGridViewComboBoxColumn();
            dgvCmb1.HeaderText = "Batch Number";
            dgvCmb1.Items.Add("---Select---");
            for (int i = 0; i < this.batch_nos.Count; i++)
            {
                dgvCmb1.Items.Add(this.batch_nos[i]);
            }
            dgvCmb1.Name = "Batch Number";
            dataGridView2.Columns.Insert(0, dgvCmb1);
            //dataGridView2.Columns.Add();

        }

        private void rateTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillRate();
        }

        private void fillRate()
        {
            //if (comboBox1.SelectedIndex == 0 || comboBox4.SelectedIndex == 0)
            //{
            //    rateTextBox.Text = "";
            //    return;
            //}
            //float f = c.getDyeingRate(comboBox4.SelectedItem.ToString(), comboBox1.SelectedItem.ToString());
            //if (f == -1F)
            //{
            //    rateTextBox.Text = "";
            //    return;
            //}
            //rateTextBox.Text = (c.getDyeingRate(comboBox4.SelectedItem.ToString(), comboBox1.SelectedItem.ToString())).ToString();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillRate();
        }

        private void M_V3_cartonProductionForm_Load(object sender, EventArgs e)
        {

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

        private void loadData(string quality, string company)
        {
            DataTable d = c.getTrayStateQualityCompany(1, quality, company);
            Console.WriteLine(d.Rows.Count);
            for(int i=0; i<d.Rows.Count; i++)
            {
               // Console.WriteLine(d.Rows[i][0].ToString());
                //this.tray_no.Add(d.Rows[i][0].ToString());
                //this.tray_id.Add(c.getTrayID(tray_no[i]).ToString());
                //this.tray_id.Add(d.Rows[i][1].ToString());
            }
            if (this.edit_form==false) MessageBox.Show("Loaded Data");
        }
    }
}
