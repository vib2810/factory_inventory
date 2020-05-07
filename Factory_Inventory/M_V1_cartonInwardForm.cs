using Factory_Inventory.Factory_Classes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
//using System.Windows.Controls;
using System.Windows.Forms;

namespace Factory_Inventory
{
    public partial class M_V1_cartonInwardForm : Form
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
        bool billDateChanged = false;
        private bool edit_cmd_send = false;
        private bool edit_form = false;
        private string oldbillno;
        Dictionary<string, bool> carton_editable = new Dictionary<string, bool>();
        private M_V_history v1_history;

        public M_V1_cartonInwardForm()
        {
            this.c = new DbConnect();
            InitializeComponent();
            dataGridView1.RowCount = 10;
            dataGridView1.ColumnCount = 3;
            dataGridView1.Columns[0].HeaderText = "Sl No";
            dataGridView1.Columns[1].HeaderText = "Carton No";
            dataGridView1.Columns[2].HeaderText = "Weight";
            dataGridView1.Columns[0].ReadOnly = true;

            this.billDateChanged = false;

            this.KeyPreview = true;

            DataTable d1 = c.getQC('q');

            for (int i = 0; i < d1.Rows.Count; i++)
            {
                dataGridView2.Rows.Add(d1.Rows[i][3].ToString(),"", "");
            }

            DataGridViewComboBoxColumn dgvCmb = new DataGridViewComboBoxColumn();
            dgvCmb.HeaderText = "Quality";
            dgvCmb.Items.Add("---Select---");
            for (int i = 0; i < d1.Rows.Count; i++)
            {
                dgvCmb.Items.Add(d1.Rows[i][3].ToString());
            }
            dgvCmb.Name = "Quality";
            dataGridView1.Columns.Insert(1, dgvCmb);



            //Create a drop-down list
            var dataSource2 = new List<string>();
            DataTable d2 = c.getQC('c');
            dataSource2.Add("---Select---");
            for (int i = 0; i < d2.Rows.Count; i++)
            {
                dataSource2.Add(d2.Rows[i][0].ToString());
            }
            this.comboBox2CB.DataSource = dataSource2;
            this.comboBox2CB.DisplayMember = "Company_Names";
            this.comboBox2CB.DropDownStyle = ComboBoxStyle.DropDown;//Create a drop-down list
            inputDate.Enabled = false;
            this.edit_form = false;
            this.comboBox2CB.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.comboBox2CB.AutoCompleteSource = AutoCompleteSource.ListItems;

    }
        public M_V1_cartonInwardForm(DataRow row, bool isEditable, M_V_history v1_history)
        {
            InitializeComponent();
            this.edit_form = true;
            this.v1_history = v1_history;
            this.c = new DbConnect();
            this.billDateChanged = false;
            
            //Initialize datagridview1
            dataGridView1.ColumnCount = 3;
            dataGridView1.Columns[0].HeaderText = "Sl No";
            dataGridView1.Columns[1].HeaderText = "Carton No";
            dataGridView1.Columns[2].HeaderText = "Weight";
            dataGridView1.Columns[0].ReadOnly = true;

            //Initialize datagridview2
            DataTable d1 = c.getQC('q');
            string[] quality_array = c.csvToArray(row["Quality_Before_Twist"].ToString());
            for(int i=0;i<quality_array.Length;i++)
            {
                d1.Rows.Add(quality_array[i]);
            }
            List<string> no_rep_d1 = new List<string>();
            for(int i=0;i<d1.Rows.Count;i++)
            {
                no_rep_d1.Add(d1.Rows[i][3].ToString());
            }
            no_rep_d1 = no_rep_d1.Distinct().ToList();
            for (int i = 0; i < no_rep_d1.Count; i++)
            {
                dataGridView2.Rows.Add(no_rep_d1[i], "", "");
            }

            DataGridViewComboBoxColumn dgvCmb = new DataGridViewComboBoxColumn();
            dgvCmb.HeaderText = "Quality";
            dgvCmb.Items.Add("---Select---");
            for (int i = 0; i < no_rep_d1.Count; i++)
            {
                dgvCmb.Items.Add(no_rep_d1[i]);
            }
            dgvCmb.Name = "Quality";
            dataGridView1.Columns.Insert(1, dgvCmb);
            
            var dataSource2 = new List<string>();

            if (isEditable == false)
            {
                this.inputDate.Enabled = false;
                this.billDateDTP.Enabled = false;
                this.billNumberTextboxTB.Enabled = false;
                this.comboBox2CB.Enabled = false;
                this.saveButton.Enabled = false;
                this.dataGridView1.ReadOnly = true;
                this.dataGridView2.ReadOnly = true;
                dataSource2.Add(row["Company_Name"].ToString());
                this.comboBox2CB.DataSource = dataSource2;
                this.comboBox2CB.DisplayMember = "Company_Names";
                this.comboBox2CB.DropDownStyle = ComboBoxStyle.DropDown;//Create a drop-down list
            }

            else
            {
                this.inputDate.Enabled = false;
                this.billDateDTP.Enabled = true;
                this.billNumberTextboxTB.Enabled = true;
                this.comboBox2CB.Enabled = true;
                this.saveButton.Enabled = true;

                //Create a drop-down list
                DataTable d2 = c.getQC('c');
                dataSource2.Add("---Select---");
                //dataSource2.Add(row["Company_Name"].ToString());
                for (int i = 0; i < d2.Rows.Count; i++)
                {
                    dataSource2.Add(d2.Rows[i][0].ToString());
                }
                this.comboBox2CB.DataSource = dataSource2;
                Console.WriteLine(this.comboBox2CB.FindStringExact(row["Company_Name"].ToString()));
                if(this.comboBox2CB.FindStringExact(row["Company_Name"].ToString())==-1)
                {
                    dataSource2.Add(row["Company_Name"].ToString());
                    this.comboBox2CB.DataSource = null;
                    this.comboBox2CB.DataSource = dataSource2;

                }
                this.comboBox2CB.DisplayMember = "Company_Names";
                this.comboBox2CB.DropDownStyle = ComboBoxStyle.DropDown;//Create a drop-down list
                this.comboBox2CB.SelectedIndex = this.comboBox2CB.FindStringExact(row["Company_Name"].ToString());
            }

            this.comboBox2CB.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.comboBox2CB.AutoCompleteSource = AutoCompleteSource.ListItems;

            this.inputDate.Value = Convert.ToDateTime(row["Date_Of_Input"].ToString());
            this.billDateDTP.Value = Convert.ToDateTime(row["Date_Of_Billing"].ToString());
            this.billNumberTextboxTB.Text = row["Bill_No"].ToString();
            this.oldbillno = billNumberTextboxTB.Text;
            string[] quality = c.csvToArray(row["Quality"].ToString());
            string[] quality_arr = c.csvToArray(row["Quality_Arr"].ToString());
            string[] carton_no = c.csvToArray(row["Carton_No_Arr"].ToString());
            string[] carton_weight_arr = c.csvToArray(row["Carton_Weight_Arr"].ToString());
            string[] buy_cost = c.csvToArray(row["Buy_Cost"].ToString());
            dataGridView1.RowCount = carton_no.Length+1;

            //Load Data into DataGridView2
            for (int i=0;i<quality.Length;i++)
            {
                for(int j=0;j<dataGridView2.Rows.Count-1;j++)
                {
                    Console.WriteLine(quality[i]);
                    Console.WriteLine(dataGridView2.Rows[j].Cells[0].Value.ToString());
                    Console.WriteLine("-----------------");
                    if (quality[i]==dataGridView2.Rows[j].Cells[0].Value.ToString())
                    {
                        dataGridView2.Rows[j].Cells[1].Value = buy_cost[i];
                    }
                }
            }
            bool flag = false;
            string carton_financial_year = row["Fiscal_Year"].ToString();
            //Load Data into datagridview1
            for (int i=0;i< carton_no.Length; i++)
            {
                //Set Values
                dataGridView1.Rows[i].Cells[2].Value = carton_no[i];
                dataGridView1.Rows[i].Cells[3].Value = carton_weight_arr[i];
                dataGridView1.Rows[i].Cells[1].Value = quality[int.Parse(quality_arr[i])];

                //Check if this carton is issued
                string this_carton_no = dataGridView1.Rows[i].Cells[2].Value.ToString();
                int carton_state = c.getCartonState(this_carton_no, carton_financial_year);
                if (carton_state == -1)
                {
                    MessageBox.Show("Critical Error, Carton Not found: " + this_carton_no, "Error");
                }
                else if(carton_state!=1)
                {
                    flag = true;
                    this.carton_editable[this_carton_no] =false;
                    DataGridViewRow r = (DataGridViewRow)dataGridView1.Rows[i];
                    dataGridView1.Rows[i].ReadOnly = true;
                    if(carton_state==2)
                    {
                        r.DefaultCellStyle.BackColor = Color.Gray;
                        r.DefaultCellStyle.SelectionBackColor = Color.Gray;

                    }
                    else if(carton_state==3)
                    {
                        r.DefaultCellStyle.BackColor = Color.Green;
                        r.DefaultCellStyle.SelectionBackColor = Color.Green;
                    }
                }
            }
            if(flag)
            {
                comboBox2CB.Enabled = false;
            }
        }
        private void dataGridView1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Back)
            {
                int row = dataGridView1.SelectedCells[0].RowIndex;
                int col = dataGridView1.SelectedCells[0].ColumnIndex;
                dataGridView1.Rows[row].Cells[col].Value = null;
            }
        }
        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            if (e.RowIndex != dataGridView1.Rows.Count - 1)
            {
                dataGridView1.Rows[e.RowIndex].Cells[0].Value = e.RowIndex + 1;
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
                dynamicWeightLabel.Text = CellSum("").ToString("F3");
                for(int i=0;i<dataGridView2.Rows.Count-1;i++)
                {
                    float sum = CellSum(dataGridView2.Rows[i].Cells[0].Value.ToString());
                    if(sum!=0F)
                    {
                        dataGridView2.Rows[i].Cells[2].Value = sum.ToString("F3");
                    }
                }
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
                    string carton_no = dataGridView1.Rows[rowindex].Cells[2].Value.ToString();
                    bool value = true;
                    bool value2 = this.carton_editable.TryGetValue(carton_no, out value);
                    if (value2 == true && value == false)
                    {
                        MessageBox.Show("Cannot delete entry at row: " + (rowindex + 1).ToString(), "Error");
                        dataGridView1.Rows[rowindex].Selected = false;
                        continue;
                    }
                    dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);
                }
                dynamicWeightLabel.Text = CellSum("").ToString("F3");
                for (int i = 0; i < dataGridView2.Rows.Count-1; i++)
                {
                    float sum = CellSum(dataGridView2.Rows[i].Cells[0].Value.ToString());
                    if (sum != 0F)
                    {
                        dataGridView2.Rows[i].Cells[2].Value = sum.ToString("F3");
                    }
                }
            }
        }
        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                dataGridView1.Rows[e.RowIndex].Selected = true;
            }
        }
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 3 && e.RowIndex >= 0)
                {
                    if (dataGridView1.Rows[e.RowIndex].Cells[3].Value != null)
                    {
                        Convert.ToDouble(dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString());
                        if(Convert.ToDouble(dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString())>=100.00)
                        {
                            MessageBox.Show("Weight should be less than 100", "Error");
                            dataGridView1.Rows[e.RowIndex].Cells[3].Value = null;
                        }    
                    }
                    dynamicWeightLabel.Text = CellSum("").ToString("F3");
                    Console.WriteLine(dataGridView2.Rows.Count);
                    for (int i = 0; i < dataGridView2.Rows.Count-1; i++)
                    {
                        float sum = CellSum(dataGridView2.Rows[i].Cells[0].Value.ToString());
                        if (sum != 0F)
                        {
                            dataGridView2.Rows[i].Cells[2].Value = sum.ToString("F3");
                        }
                    }


                }
            }
            catch
            {
                MessageBox.Show("Please enter numeric Weight value only", "Error");
                dataGridView1.Rows[e.RowIndex].Cells[3].Value = null;
            }
            try
            {
                if (e.ColumnIndex == 2 && e.RowIndex >= 0)
                {
                    if (dataGridView1.Rows[e.RowIndex].Cells[2].Value != null)
                    {
                        int.Parse(dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString());
                    }
                }
            }
            catch
            {
                MessageBox.Show("Please enter numeric Carton No only", "Error");
                dataGridView1.Rows[e.RowIndex].Cells[2].Value = null;
            }
        }
        private float CellSum(string quality)
        {
            float sum = 0;
            if(quality == "")
            {
                try
                {
                    if (dataGridView1.Rows.Count == 0)
                    {
                        return sum;
                    }
                    for (int i = 0; i < dataGridView1.Rows.Count; ++i)
                    {
                        if (dataGridView1.Rows[i].Cells[3].Value != null)
                            sum += float.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString());
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
                        if (dataGridView1.Rows[i].Cells[3].Value != null && dataGridView1.Rows[i].Cells[1].Value.ToString()==quality)
                            sum += float.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString());
                    }
                    return sum;
                }
                catch
                {
                    return sum;
                }
            }
        }
        private void saveButton_Click(object sender, EventArgs e)
        {
            //checks
            if (this.billDateChanged == false)
            {
                MessageBox.Show("Enter Bill Date", "Error");
                return;
            }
            if (billNumberTextboxTB.Text == "")
            {
                MessageBox.Show("Enter Bill Number", "Error");
                return;
            }
            if (comboBox2CB.SelectedIndex == 0)
            {
                MessageBox.Show("Enter Select Company Name", "Error");
                return;
            }
            if (dataGridView1.Rows.Count < 0)
            {
                MessageBox.Show("Please enter Carton Numbers and Weights", "Error");
                return;
            }
            if(this.inputDate.Value.Date<this.billDateDTP.Value.Date)
            {
                MessageBox.Show("Bill Date is in the future", "Error");
                return;
            }


            string weights = "", cartonno = "", quality="", quality_arr= "", cost="";
            int number = 0;
            Console.WriteLine(dataGridView2.Rows.Count);
            
            //Fill activated qualities and check validity
            List<bool> activated_qualities= new List<bool>();
            for(int i=0; i<dataGridView2.Rows.Count-1; i++)
            {
                if(dataGridView2.Rows[i].Cells[1].Value == null)
                {
                    activated_qualities.Add(false);
                }
                else if(dataGridView2.Rows[i].Cells[1].Value.ToString() == "")
                {
                    activated_qualities.Add(false);
                }
                else
                {
                    try
                    {
                        Console.WriteLine(dataGridView2.Rows[i].Cells[1].Value.ToString());
                        float.Parse(dataGridView2.Rows[i].Cells[1].Value.ToString());
                        activated_qualities.Add(true);
                    }
                    catch
                    {
                        MessageBox.Show("Enter valid cost for Quality " + dataGridView2.Rows[i].Cells[0].Value, "Error");
                        disable_form_edit();
                        return;
                    }
                }                
            }
            //fill quality and cost
            for(int i=0; i<activated_qualities.Count; i++)
            {
                if(activated_qualities[i]==true)
                {
                    quality += dataGridView2.Rows[i].Cells[0].Value.ToString() + ",";
                    cost += dataGridView2.Rows[i].Cells[1].Value.ToString() + ",";
                }
            }

            //Iterate to check for mistakes in dataGridView1
            List<int> temp = new List<int>();
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                int count = 0;
                
                //ComboBox c = (ComboBox)dataGridView1.EditingControl;
                if (dataGridView1.Rows[i].Cells[1].Value == null || dataGridView1.Rows[i].Cells[1].Value.ToString() == "---Select---")
                    count++;
                if (dataGridView1.Rows[i].Cells[2].Value == null)
                    count++;
                if (dataGridView1.Rows[i].Cells[3].Value == null)
                    count++;
                if (count == 1 || count == 2)
                {
                    MessageBox.Show("Error at row " + (i + 1).ToString(), "Error");
                    return;
                }
                else
                {
                    if (count == 0)
                    {
                        temp.Add(int.Parse(dataGridView1.Rows[i].Cells[2].Value.ToString()));
                        string q = dataGridView1.Rows[i].Cells[1].Value.ToString();
                        int index = -1;
                        int index2 = -1;
                        for (int j = 0; j < dataGridView2.Rows.Count - 1; j++)
                        {
                            if (activated_qualities[j] == true) index2++;
                            if (q == dataGridView2.Rows[j].Cells[0].Value.ToString())
                            {
                                index = j;
                                break;
                            }
                        }
                        if (activated_qualities[index] == false)
                        {
                            MessageBox.Show("Please enter cost for " + dataGridView1.Rows[i].Cells[1].Value.ToString(), "Error");
                            return;
                        }
                        quality_arr += index2 + ",";
                        cartonno += dataGridView1.Rows[i].Cells[2].Value + ",";
                        weights += dataGridView1.Rows[i].Cells[3].Value + ",";
                        number++;
                        var distinctBytes = new HashSet<int>(temp);
                        bool allDifferent = distinctBytes.Count == temp.Count;
                        if (allDifferent == false)
                        {
                            MessageBox.Show("Please Enter Distinct Carton Nos at Row: " + (i + 1).ToString(), "Error");
                            return;
                        }
                    }
                }
            }
            if (this.edit_form==false)
            {
                bool added=c.addCartonVoucher(inputDate.Value, billDateDTP.Value, billNumberTextboxTB.Text, quality, quality_arr, this.comboBox2CB.SelectedItem.ToString(), cost, cartonno, weights, number, CellSum(""));
                if (added == true) disable_form_edit();
                else return;
            }
            else
            {
                bool edited=c.editCartonVoucher(this.oldbillno, inputDate.Value, billDateDTP.Value, billNumberTextboxTB.Text, quality, quality_arr, this.comboBox2CB.SelectedItem.ToString(), cost, cartonno, weights, number, CellSum(""), this.carton_editable);
                if (edited == true)
                {
                    disable_form_edit();
                    this.v1_history.loadData();
                }
                else return;
            }
        }
        private void disable_form_edit()
        {
            this.inputDate.Enabled = false;
            this.billDateDTP.Enabled = false;
            this.billNumberTextboxTB.Enabled = false;
            this.comboBox2CB.Enabled = false;
            this.saveButton.Enabled = false;
            this.dataGridView1.ReadOnly = true;
            this.dataGridView2.ReadOnly = true;
        }
        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab &&
                (dataGridView1.SelectedCells.Cast<DataGridViewCell>().Any(x => x.ColumnIndex == 3) || this.edit_cmd_send == true))
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
                if (dataGridView1.Rows[rowindex_tab].Cells[1].Value != null)
                {
                    dataGridView1.Rows[rowindex_tab + 1].Cells[1].Value = dataGridView1.Rows[rowindex_tab].Cells[1].Value;
                }
                if (dataGridView1.Rows[rowindex_tab].Cells[2].Value != null)
                {
                    dataGridView1.Rows[rowindex_tab + 1].Cells[2].Value = (int.Parse(dataGridView1.Rows[rowindex_tab].Cells[2].Value.ToString()) + 1).ToString();
                }
                if (edit_cmd_local == false)
                {
                    SendKeys.Send("{tab}");
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
        private void billDate_ValueChanged(object sender, EventArgs e)
        {
            this.billDateChanged = true;
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
    }
}
