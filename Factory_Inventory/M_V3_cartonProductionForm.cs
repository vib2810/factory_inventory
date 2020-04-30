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
                    .Any(x => x.ColumnIndex == 5))
            {
                this.edit_cmd_send = true;
                SendKeys.Send("{Tab}");
                return false;
            }

            if (keyData == Keys.Enter &&
                dataGridView1.EditingControl != null &&
                msg.HWnd == dataGridView1.EditingControl.Handle &&
                dataGridView1.SelectedCells
                    .Cast<DataGridViewCell>()
                    .Any(x => x.ColumnIndex == 1))
            {
                SendKeys.Send("{Enter}");
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
        DataTable dt;
        DateTimePicker dtp;
        public M_V3_cartonProductionForm()
        {
            InitializeComponent();
            this.c = new DbConnect();
            this.batch_nos = new List<string>();
            this.saveButton.Enabled = false;
            this.dt = new DataTable();

            //Create drop-down Colour list
            var dataSource1 = new List<string>();
            DataTable dt = c.getQC('l');
            dataSource1.Add("---Select---");
            for (int i = 0; i < dt.Rows.Count; i++)
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

            //Create drop-down Cone list
            var dataSource4 = new List<string>();
            dt = c.getQC('n');
            dataSource4.Add("---Select---");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dataSource4.Add(dt.Rows[i][0].ToString());
            }
            this.coneCombobox.DataSource = dataSource4;
            this.coneCombobox.DisplayMember = "Cones";
            this.coneCombobox.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.coneCombobox.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.coneCombobox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.coneCombobox.SelectedIndex = 1; //default selected cone

            //Create drop-down Fiscal Year lists
            var dataSource5 = new List<string>();
            DataTable d5 = c.getQC('f');
            dataSource4.Add("---Select---");

            for (int i = 0; i < d5.Rows.Count; i++)
            {
                dataSource5.Add(d5.Rows[i][0].ToString());
            }
            this.financialYearCombobox.DataSource = dataSource5;
            this.financialYearCombobox.DisplayMember = "Financial Year";
            this.financialYearCombobox.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.financialYearCombobox.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.financialYearCombobox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            this.financialYearCombobox.SelectedIndex = this.financialYearCombobox.FindStringExact(c.getFinancialYear(this.inputDate.Value));

            //DatagridView 1
            dataGridView1.Columns.Add("Sl_No", "Sl No");
            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns.Add("Production_Date", "Production Date");
            dataGridView1.Columns.Add("Carton_Number", "Carton Number");
            dataGridView1.Columns.Add("Gross_Weight", "Gross Weight");
            dataGridView1.Columns.Add("Carton_Weight", "Carton Weight");
            dataGridView1.Columns.Add("Number_Of_Cones", "Number of Cones");
            dataGridView1.Columns.Add("Net_Weight", "Net Weight");
            dataGridView1.Columns[6].ReadOnly = true;
            dataGridView1.RowCount = 10;
            dataGridView1.Enabled = false;


            //Datagridview 2
            dataGridView2.Columns.Add("Sl_No", "Sl No");
            dataGridView2.Columns[0].ReadOnly = true;
            DataGridViewComboBoxColumn dgvCmb1 = new DataGridViewComboBoxColumn();
            dgvCmb1.HeaderText = "Batch Number";
            dgvCmb1.Items.Add("---Select---");
            dgvCmb1.DataSource = this.batch_nos;
            dgvCmb1.Name = "Batch Number";
            dataGridView2.Columns.Insert(1, dgvCmb1);
            dataGridView2.Columns[0].Width = 50;
            dataGridView2.Columns[1].Width = 150;
            dataGridView2.Columns.Add("Weight", "Weight");

            oilGainButton.Enabled = false;
            if(closedCheckbox.Checked==true)
            {
                oilGainButton.Enabled = true;
            }
        }

        public M_V3_cartonProductionForm(DataRow row, bool isEditable, M_V_history v1_history)
        {

            InitializeComponent();
            this.dt = new DataTable();
            this.edit_form = true;
            this.v1_history = v1_history;
            this.saveButton.Enabled = false;
            this.c = new DbConnect();
            this.batch_nos = new List<string>();

            //Create drop-down Colour list
            var dataSource1 = new List<string>();
            DataTable dt = c.getQC('l');
            dataSource1.Add("---Select---");
            for (int i = 0; i < dt.Rows.Count; i++)
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

            //Create drop-down Cone list
            var dataSource4 = new List<string>();
            dt = c.getQC('n');
            dataSource4.Add("---Select---");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dataSource4.Add(dt.Rows[i][0].ToString());
            }
            this.coneCombobox.DataSource = dataSource4;
            this.coneCombobox.DisplayMember = "Cones";
            this.coneCombobox.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.coneCombobox.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.coneCombobox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.coneCombobox.SelectedIndex = 1; //default selected cone

            //Create drop-down Fiscal Year lists
            var dataSource5 = new List<string>();
            DataTable d5 = c.getQC('f');
            dataSource4.Add("---Select---");

            for (int i = 0; i < d5.Rows.Count; i++)
            {
                dataSource5.Add(d5.Rows[i][0].ToString());
            }
            this.financialYearCombobox.DataSource = dataSource5;
            this.financialYearCombobox.DisplayMember = "Financial Year";
            this.financialYearCombobox.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.financialYearCombobox.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.financialYearCombobox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            //DatagridView 1
            dataGridView1.Columns.Add("Sl_No", "Sl No");
            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns.Add("Production_Date", "Production Date");
            dataGridView1.Columns.Add("Carton_Number", "Carton Number");
            dataGridView1.Columns.Add("Gross_Weight", "Gross Weight");
            dataGridView1.Columns.Add("Carton_Weight", "Carton Weight");
            dataGridView1.Columns.Add("Number_Of_Cones", "Number of Cones");
            dataGridView1.Columns.Add("Net_Weight", "Net Weight");
            dataGridView1.Columns[6].ReadOnly = true;

            //Datagridview 2
            dataGridView2.Columns.Add("Sl_No", "Sl No");
            dataGridView2.Columns[0].ReadOnly = true;
            DataGridViewComboBoxColumn dgvCmb1 = new DataGridViewComboBoxColumn();
            dgvCmb1.HeaderText = "Batch Number";
            dgvCmb1.Items.Add("---Select---");
            dgvCmb1.Name = "Batch Number";
            dataGridView2.Columns.Insert(1, dgvCmb1);
            dataGridView2.Columns[0].Width = 50;
            dataGridView2.Columns[1].Width = 150;
            dataGridView2.Columns.Add("Weight", "Weight");

            oilGainButton.Enabled = false;
            if (closedCheckbox.Checked == true)
            {
                oilGainButton.Enabled = true;
            }

            if (isEditable == false)
            {
                this.inputDate.Enabled = false;
                this.loadDataButton.Enabled = false;
                this.saveButton.Enabled = false;
                this.dataGridView1.ReadOnly = true;
            }
            else
            {
                //no option to edit company name and quality
                this.saveButton.Enabled = true;
                this.loadDataButton.Enabled = false;
                this.dataGridView1.ReadOnly = false;
            }
            this.inputDate.Value = Convert.ToDateTime(row["Date_Of_Input"].ToString());
            this.voucherID = int.Parse(row["Voucher_ID"].ToString());
            
            //Adding batch numbers to datagridview 2
            string[] temp_batch_no_arr= c.csvToArray(row["Batch_No_Arr"].ToString());
            string[] batch_fiscal_year_arr = c.csvToArray(row["Batch_Fiscal_year_Arr"].ToString());
            List<string> show_batches=new List<string>();
            for(int i=0;i<temp_batch_no_arr.Length;i++)
            {
                show_batches.Add(temp_batch_no_arr[i]);
            }
            for (int i = 0; i < temp_batch_no_arr.Length; i++)
            {
                for (int j = i + 1; j < temp_batch_no_arr.Length; j++)
                {
                    if (temp_batch_no_arr[i] == temp_batch_no_arr[j])
                    {
                        show_batches[i] = "*" + temp_batch_no_arr[i] + " " + batch_fiscal_year_arr[i];
                        show_batches[j] = "*" + temp_batch_no_arr[j] + " " + batch_fiscal_year_arr[j];
                    }
                }
            }
            dgvCmb1.DataSource = show_batches;
            dataGridView2.RowCount = temp_batch_no_arr.Length+1;
            for (int i=0;i<temp_batch_no_arr.Length;i++)
            {
                DataRow batch_row = c.getBatchRow_BatchNo(int.Parse(temp_batch_no_arr[i]), batch_fiscal_year_arr[i]);
                dataGridView2.Rows[i].Cells[1].Value = show_batches[i];
                dataGridView2.Rows[i].Cells[2].Value = batch_row["Net_Weight"].ToString();
            }
            this.batchnwtTextbox.Text = this.CellSum2(2).ToString("F3");

            this.colourCombobox.SelectedIndex = this.colourCombobox.FindStringExact(row["Colour"].ToString());
            this.qualityCombobox.SelectedIndex = this.qualityCombobox.FindStringExact(row["Quality"].ToString());
            this.dyeingCompanyCombobox.SelectedIndex = this.dyeingCompanyCombobox.FindStringExact(row["Dyeing_Company_Name"].ToString());
            this.financialYearCombobox.SelectedIndex = this.financialYearCombobox.FindStringExact(row["Carton_Fiscal_Year"].ToString());
            this.coneCombobox.SelectedIndex = this.coneCombobox.FindStringExact((float.Parse(row["Cone_Weight"].ToString())*1000F).ToString());
            

            if(row["Voucher_Closed"].ToString()=="0")
            {
                this.closedCheckbox.Checked = false;
            }
            else
            {
                this.closedCheckbox.Checked = true;
                this.oilGainTextbox.Text = row["Oil_Gain"].ToString();
                this.oilGainButton.Enabled = false;
            }

            string[] produced_cartons = c.csvToArray(row["Carton_No_Production_Arr"].ToString());
            dataGridView1.RowCount= produced_cartons.Length + 1;
            for (int i = 0; i < produced_cartons.Length; i++)
            {
                DataRow carton_row = c.getProducedCartonRow(produced_cartons[i], row["Carton_Fiscal_Year"].ToString());
                string correctformat = Convert.ToDateTime(carton_row["Date_Of_Production"].ToString()).Date.ToString().Substring(0,10);
                dataGridView1.Rows[i].Cells[1].Value = correctformat;
                dataGridView1.Rows[i].Cells[2].Value = produced_cartons[i];
                dataGridView1.Rows[i].Cells[3].Value = carton_row["Gross_Weight"].ToString();
                dataGridView1.Rows[i].Cells[4].Value = carton_row["Carton_Weight"].ToString();
                dataGridView1.Rows[i].Cells[5].Value = carton_row["Number_Of_cones"].ToString();
                dataGridView1.Rows[i].Cells[6].Value = carton_row["Net_Weight"].ToString();
            }
            this.cartonweight.Text = CellSum1(6).ToString("F3");
        }

        public void disable_form_edit()
        {
            this.inputDate.Enabled = false;
            this.loadDataButton.Enabled = false;
            this.saveButton.Enabled = false;
            this.dataGridView1.ReadOnly = true;
        }
        private void dataGridView2_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0 && e.ColumnIndex == 1)
            {
                for (int i = 0; i < dataGridView2.Rows.Count - 1; i++)
                {
                    for (int j = i + 1; j < dataGridView2.Rows.Count - 1; j++)
                    {
                        if (dataGridView2.Rows[i].Cells[1].Value.ToString() == dataGridView2.Rows[j].Cells[1].Value.ToString())
                        {
                            MessageBox.Show("Rows " + (i + 1).ToString() + " and " + (j + 1).ToString() + " have same Batch Number", "Error");
                            dataGridView2.Rows[j].Cells[1].Value = "";
                            dataGridView2.Rows[j].Cells[2].Value = "";
                            return;
                        }
                    }
                }
                string batch = dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString();
                int index = -1;
                for (int i = 0; i < this.batch_nos.Count; i++)
                {
                    if (this.batch_nos[i] == batch)
                    {
                        index = i;
                        break;
                    }
                }
                if (index != -1)
                {
                    dataGridView2.Rows[e.RowIndex].Cells[2].Value = dt.Rows[index]["Net_Weight"].ToString();
                }
                batchnwtTextbox.Text = CellSum2(2).ToString("F3");
            }
        }
        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            //called when tab is pressed at last row or tab is pressed while editing last row
            if (e.KeyCode == Keys.Tab &&
                (dataGridView1.SelectedCells.Cast<DataGridViewCell>().Any(x => x.ColumnIndex == 5) || this.edit_cmd_send == true))
            {
                Console.WriteLine("Inside Editing");
                bool edit_cmd_local = this.edit_cmd_send;
                this.edit_cmd_send = false;
                int rowindex_tab = dataGridView1.SelectedCells[0].RowIndex;
                //if (edit_cmd_local == true) rowindex_tab--;

                //if (rowindex_tab < 0)
                //{
                //    SendKeys.Send("{tab}");
                //    SendKeys.Send("{tab}");
                //    return;
                //}
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
                SendKeys.Send("{tab}");
                SendKeys.Send("{tab}");
                if (edit_cmd_local == false)
                {
                    SendKeys.Send("{tab}");
                }
            }
            if (e.KeyCode==Keys.Enter && dataGridView1.SelectedCells.Cast<DataGridViewCell>().Any(x => x.ColumnIndex == 1))
            {
                try
                {
                    if (dataGridView1.Focused && dataGridView1.CurrentCell.ColumnIndex == 1)
                    {
                        int i = dataGridView1.CurrentCell.RowIndex;
                        int j = dataGridView1.CurrentCell.ColumnIndex;
                        dtp.Location = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, false).Location;
                        dtp.Visible = true;
                        if (dataGridView1.CurrentCell.Value != DBNull.Value)
                        {
                            dtp.Value = Convert.ToDateTime(dataGridView1.CurrentCell.Value);
                        }
                        else
                        {
                            dtp.Value = DateTime.Today;
                        }
                        dtp.Focus();
                        e.Handled = true;
                    }
                    else
                    {
                        dtp.Visible = false;
                    }
                }
                catch
                {
                    Console.WriteLine("DTP Exception");
                }

            }
        }
        private void saveButton_Click(object sender, EventArgs e)
        {
            //checks
            if (coneCombobox.SelectedIndex == 0)
            {
                MessageBox.Show("Select Cone Weight", "Error");
                return;
            }
            if (dataGridView1.Rows[0].Cells[1].Value==null)
            {
                MessageBox.Show("Please enter values", "Error");
                return;
            }
            if ((float.Parse(cartonweight.Text) - float.Parse(batchnwtTextbox.Text) < 0F) && closedCheckbox.Checked==true)
            {
                MessageBox.Show("Net Carton Weight should be greater than or equal to Net Batch Weight", "Error");
                return;
            }

            string production_dates = "", carton_nos = "", gross_weights = "", carton_weights = "", number_of_cones = "", net_weights = "";
            int number = 0;

            List<int> temp = new List<int>();
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                int sum = 0;
                for(int j=2;j<=6;j++)
                {
                    if(dataGridView1.Rows[i].Cells[j].Value==null || dataGridView1.Rows[i].Cells[j].Value == "")
                    {
                    }
                    else
                    {
                        sum++;
                    }
                }
                if(sum==0)
                {
                    continue;
                }
                else if(sum!=5)
                {
                    MessageBox.Show("Missing values in " + (i + 1).ToString() + " row", "Error");
                    return;
                }
                ComboBox c = (ComboBox)dataGridView1.EditingControl;
                if (dataGridView1.Rows[i].Cells[1].Value == null || dataGridView1.Rows[i].Cells[1].Value.ToString() == "")
                {
                    continue;
                }
                else
                {
                    production_dates += dataGridView1.Rows[i].Cells[1].Value.ToString() + ",";
                    carton_nos += dataGridView1.Rows[i].Cells[2].Value.ToString() + ",";
                    gross_weights += dataGridView1.Rows[i].Cells[3].Value.ToString() + ",";
                    carton_weights += dataGridView1.Rows[i].Cells[4].Value.ToString() + ",";
                    number_of_cones+= dataGridView1.Rows[i].Cells[5].Value.ToString() + ",";
                    net_weights += dataGridView1.Rows[i].Cells[6].Value.ToString() + ",";

                    number++;

                    //to check for all different tray_nos
                    temp.Add(int.Parse(dataGridView1.Rows[i].Cells[2].Value.ToString()));
                    var distinctBytes = new HashSet<int>(temp);
                    bool allDifferent = distinctBytes.Count == temp.Count;
                    if (allDifferent == false)
                    {
                        MessageBox.Show("Please Enter Carton Tray Nos at Row: " + (i + 1).ToString(), "Error");
                        return;
                    }

                }
            }
            string batch_nos = "";
            for(int i=0;i<dataGridView2.Rows.Count-1; i++)
            {
                batch_nos+= dataGridView2.Rows[i].Cells[1].Value.ToString() + ",";
            }
            int closed;
            if(closedCheckbox.Checked==true)
            {
                closed = 1;
            }
            else
            {
                closed = 0;
            }
            if (this.edit_form == false)
            {
                bool added= c.addCartonProductionVoucher(inputDate.Value, colourCombobox.Text, qualityCombobox.Text, dyeingCompanyCombobox.Text, financialYearCombobox.Text, coneCombobox.Text, production_dates, carton_nos, gross_weights, carton_weights, number_of_cones, net_weights, batch_nos, closed, float.Parse(batchnwtTextbox.Text), float.Parse(cartonweight.Text));
                if (added == true) disable_form_edit();
                else return;
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
            cartonweight.Text = CellSum1(6).ToString("F3");
        }
        private void deleteToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            int count2 = dataGridView2.SelectedRows.Count;
            for (int i = 0; i < count2; i++)
            {
                if (dataGridView2.SelectedRows[0].Index == dataGridView2.Rows.Count - 1)
                {
                    dataGridView2.SelectedRows[0].Selected = false;
                    continue;
                }
                dataGridView2.Rows.RemoveAt(dataGridView2.SelectedRows[0].Index);
            }
            batchnwtTextbox.Text = CellSum2(2).ToString("F3");
        }
        private float CellSum1(int col)
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
                    if (dataGridView1.Rows[i].Cells[col].Value != null)
                        sum += float.Parse(dataGridView1.Rows[i].Cells[col].Value.ToString());
                }
                return sum;
            }
            catch
            {
                return sum;
            }
        }
        private float CellSum2(int col)
        {
            float sum = 0;
            try
            {
                if (dataGridView2.Rows.Count == 0)
                {
                    return sum;
                }
                for (int i = 0; i < dataGridView2.Rows.Count; ++i)
                {
                    if (dataGridView2.Rows[i].Cells[col].Value != null)
                        sum += float.Parse(dataGridView2.Rows[i].Cells[col].Value.ToString());
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
            this.loadData();

            //Set the first date in form
            string current_fiscal_year = c.getFinancialYear(DateTime.Now);
            if(current_fiscal_year==financialYearCombobox.Text)
            {
                dataGridView1.Rows[0].Cells[1].Value = DateTime.Now.Date.ToString().Substring(0, 10);
            }
            else
            {
                string[] years = financialYearCombobox.Text.Split('-');
                DateTime dt = new DateTime(int.Parse(years[1]),31,3);
                this.dataGridView1.Rows[0].Cells[1].Value = dt.Date.ToString().Substring(0, 10);
            }

            //set the first carton number in form
            int next_carton_no = c.getNextBatchNumber("Highest_Carton_Production_No", this.financialYearCombobox.Text);
            dataGridView1.Rows[0].Cells[2].Value = next_carton_no;

            //enable and disable 
            this.loadDataButton.Enabled = false;
            this.colourCombobox.Enabled = false;
            this.qualityCombobox.Enabled = false;
            this.financialYearCombobox.Enabled = false;
            this.dyeingCompanyCombobox.Enabled = false;
            this.saveButton.Enabled = true;
            this.dataGridView1.Enabled = true;
        }
        private void loadData()
        {
            this.dt = c.getBatchFiscalYearWeight_StateDyeingCompanyColourQuality(2, dyeingCompanyCombobox.SelectedItem.ToString(), colourCombobox.SelectedItem.ToString(), qualityCombobox.SelectedItem.ToString());
            List<string> batch_no_arr = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                batch_no_arr.Add(dt.Rows[i]["Batch_No"].ToString());
            }
            for (int i = 0; i < batch_no_arr.Count; i++)
            {
                for (int j = i + 1; j < batch_no_arr.Count; j++)
                {
                    if (dt.Rows[i]["Batch_No"].ToString() == dt.Rows[j]["Batch_No"].ToString())
                    {
                        batch_no_arr[i] = "*" + dt.Rows[i]["Batch_No"].ToString() + " " + dt.Rows[i]["Fiscal_Year"].ToString();
                        batch_no_arr[j] = "*" + dt.Rows[j]["Batch_No"].ToString() + " " + dt.Rows[j]["Fiscal_Year"].ToString();
                    }
                }
            }
            for (int i = 0; i < batch_no_arr.Count; i++)
            {
                this.batch_nos.Add(batch_no_arr[i]);
            }
            if (this.edit_form == false) MessageBox.Show("Loaded Data");
        }
        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            try
            {
                if(dataGridView1.Focused && dataGridView1.CurrentCell.ColumnIndex==1)
                {
                    dtp.Location = dataGridView1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Location;
                    dtp.Visible = true;
                    if(dataGridView1.CurrentCell.Value!=DBNull.Value)
                    {
                        dtp.Value = Convert.ToDateTime(dataGridView1.CurrentCell.Value);
                    }
                    else
                    {
                        dtp.Value = DateTime.Today;
                    }
                }
                else
                {
                    dtp.Visible = false;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("DTP Exception "+ ex.Message);
            }
        }
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView1.Focused && dataGridView1.CurrentCell.ColumnIndex == 1)
                {
                    dataGridView1.CurrentCell.Value = dtp.Value.Date.ToString().Substring(0, 10);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("DTP2 Exception " + ex.Message);
            }

            //Checks for numeric values
            if (e.ColumnIndex == 3 && e.RowIndex >= 0)
            {
                try
                {
                    string a = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                    float.Parse(a);
                }
                catch
                {
                    MessageBox.Show("Please Enter Decimal Gross Weight");
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "";
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].Selected = true;
                    return;
                }
            }
            if (e.ColumnIndex == 4 && e.RowIndex >= 0)
            {
                try
                {
                    string a = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                    float.Parse(a);
                }
                catch
                {
                    MessageBox.Show("Please Enter Decimal Carton Weight");
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "";
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].Selected = true;
                    return;
                }
            }
            if (e.ColumnIndex == 5 && e.RowIndex >= 0)
            {
                try
                {
                    string a = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                    int.Parse(a);
                }
                catch
                {
                    MessageBox.Show("Please Enter Integer Number of Cones");
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "";
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].Selected = true;
                    return;
                }
            }
            //Fill New Wt
            if ((e.ColumnIndex == 5 || e.ColumnIndex == 4 || e.ColumnIndex == 3) && e.RowIndex >= 0)
            {
                calculate_net_wt(e.RowIndex);
            }
        }
        public void calculate_net_wt(int row_index)
        {
            if (dataGridView1.Rows[row_index].Cells[3].Value == null || dataGridView1.Rows[row_index].Cells[4].Value == null || dataGridView1.Rows[row_index].Cells[5].Value == null)
            {
                dataGridView1.Rows[row_index].Cells[6].Value = null;
                cartonweight.Text = CellSum1(6).ToString("F3");
                return;
            }
            if (dataGridView1.Rows[row_index].Cells[3].Value == "" || dataGridView1.Rows[row_index].Cells[4].Value == "" || dataGridView1.Rows[row_index].Cells[5].Value == "")
            {
                dataGridView1.Rows[row_index].Cells[6].Value = null;
                cartonweight.Text = CellSum1(6).ToString("F3");
                return;
            }
            if (coneCombobox.SelectedIndex == 0)
            {
                dataGridView1.Rows[row_index].Cells[6].Value = "Please select Cone Wt";
            }
            float gross_weight = float.Parse(dataGridView1.Rows[row_index].Cells[3].Value.ToString());
            float carton_weight = float.Parse(dataGridView1.Rows[row_index].Cells[4].Value.ToString());
            float cone_weight = int.Parse(dataGridView1.Rows[row_index].Cells[5].Value.ToString()) * float.Parse(coneCombobox.Text) * 0.001F;
            float net_weight = (gross_weight - carton_weight - cone_weight);
            if(net_weight < 0)
            {
                MessageBox.Show("Net Weight ("+net_weight.ToString()+") should be positive only. Please check your entries");
                for(int i=3;i<=6;i++)
                {
                    dataGridView1.Rows[row_index].Cells[i].Value = "";
                }
                cartonweight.Text = CellSum1(6).ToString("F3");
                return;
            }
            dataGridView1.Rows[row_index].Cells[6].Value = (gross_weight - carton_weight - cone_weight).ToString();
            cartonweight.Text = CellSum1(6).ToString("F3");
        }
        private void dtp_ValueChanged(object sender, EventArgs e)
        {
            Console.WriteLine("dtp value chnager");
            if (this.financialYearCombobox.Text != c.getFinancialYear(dtp.Value))
            {
                MessageBox.Show("Carton Production Date has to be of the same financial year as Entered", "Error");
                dtp.Value = DateTime.Today;
                return;
            }
            dataGridView1.CurrentCell.Value = dtp.Value.Date.ToString().Substring(0, 10);
        }
        private void M_V3_cartonProductionForm_Load(object sender, EventArgs e)
        {
            dtp = new DateTimePicker();
            dtp.Format = DateTimePickerFormat.Short;
            dtp.Visible = false;
            dtp.Width = 100;
            dataGridView1.Controls.Add(dtp);

            dtp.ValueChanged += this.dtp_ValueChanged;
        }
        private void coneCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++) calculate_net_wt(i);
        }
        private void dataGridView2_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                dataGridView2.Rows[e.RowIndex].Selected = true;
            }
        }
        private void dataGridView1_RowPostPaint_1(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            if (e.RowIndex != dataGridView1.Rows.Count - 1)
            {
                dataGridView1.Rows[e.RowIndex].Cells[0].Value = e.RowIndex + 1;
            }
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
        private void dataGridView2_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            if (e.RowIndex != dataGridView2.Rows.Count - 1)
            {
                dataGridView2.Rows[e.RowIndex].Cells[0].Value = e.RowIndex + 1;
            }
        }
        private void dataGridView2_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is DataGridViewComboBoxEditingControl)
            {
                ((ComboBox)e.Control).DropDownStyle = ComboBoxStyle.DropDown;
                ((ComboBox)e.Control).AutoCompleteSource = AutoCompleteSource.ListItems;
                ((ComboBox)e.Control).AutoCompleteMode = AutoCompleteMode.Append;
            }
        }

        private void closedCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if(closedCheckbox.Checked==true)
            {
                oilGainButton.Enabled = true;
            }
            else
            {
                oilGainButton.Enabled = false;
                oilGainTextbox.Text = "";
            }
        }

        private void oilGainButton_Click(object sender, EventArgs e)
        {
            float net_weight, batch_weight;
            try
            {
                batch_weight = float.Parse(batchnwtTextbox.Text);
            }
            catch
            {
                MessageBox.Show("Incorrect Net Batch Weight", "Error");
                return;
            }
            try
            {
                net_weight = float.Parse(cartonweight.Text);
            }
            catch
            {
                MessageBox.Show("Incorrect Net Carton Weight", "Error");
                return;
            }
            if(net_weight-batch_weight<0F)
            {
                MessageBox.Show("Net Carton Weight should be greater than or equal to Net Batch Weight", "Error");
                return;
            }
            oilGainTextbox.Text = ((net_weight - batch_weight) / batch_weight * 100F).ToString("F2");
        }

        private void dataGridView2_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dataGridView2.IsCurrentCellDirty)
            {
                dataGridView2.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }
    }
}
