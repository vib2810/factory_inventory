using Factory_Inventory.Factory_Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Factory_Inventory
{
    public partial class M_V3_issueToReDyeingForm : Form
    {
        private DbConnect c = new DbConnect();
        private DataTable dt, all_trays;
        private List<string> batch_nos = new List<string>();
        private M_V2_trayInputForm f;
        public Label dummyLabel;
        private DataRow old_batch_row;
        public M_V3_issueToReDyeingForm()
        {
            InitializeComponent();
            c = new DbConnect();
            all_trays = new DataTable();
            //Create drop-down Batches lists
            this.dt = c.getBatchFiscalYearWeight_StateDyeingCompanyColourQuality(2, null, null, null);
            List<string> batch_no_arr = new List<string>();
            if(this.dt == null)
            {
                c.ErrorBox("There are no recieved batches", "Error");
                return;
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                batch_no_arr.Add(dt.Rows[i]["Batch_No"].ToString());
            }
            Console.WriteLine("aaaaaaaaaaaaaaaa"+batch_no_arr.Count);
            if(batch_no_arr.Count!=1)
            {
                for (int i = 0; i < batch_no_arr.Count; i++)
                {
                    batch_no_arr[i] = dt.Rows[i]["Batch_No"].ToString() + "  (" + dt.Rows[i]["Fiscal_Year"].ToString() + ")";
                }
            }
            for (int i = 0; i < batch_no_arr.Count; i++)
            {
                this.batch_nos.Add(batch_no_arr[i]);
            }
            batch_no_arr.Insert(0, "---Select---");
            this.batchNoCB.DataSource = batch_no_arr;
            this.batchNoCB.DisplayMember = "Batches";
            this.batchNoCB.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.batchNoCB.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.batchNoCB.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            c.SetGridViewSortState(this.dataGridView1, DataGridViewColumnSortMode.NotSortable);
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.White;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.Blue;
        }
        private void M_V3_issueToReDyeingForm_Load(object sender, EventArgs e)
        {
            this.Location = new System.Drawing.Point(Screen.PrimaryScreen.Bounds.Width/16, Screen.PrimaryScreen.Bounds.Height/8);


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
        private void loadButton_Click(object sender, EventArgs e)
        {
            if(this.batchNoCB.SelectedIndex==0)
            {
                c.ErrorBox("Please Select Batch Number", "Error");
                return;
            }
            int index = this.batchNoCB.SelectedIndex;
            int batchno = int.Parse(this.dt.Rows[index - 1]["Batch_No"].ToString());
            string fiscal_year = this.dt.Rows[index - 1]["Fiscal_Year"].ToString();
            this.old_batch_row = c.getBatchRow_BatchNo(batchno, fiscal_year);
            DateTime inward_date = Convert.ToDateTime(this.old_batch_row["Dyeing_In_Date"].ToString()); 
            if(inward_date > this.issueDateDTP.Value.Date)
            {
                c.ErrorBox("Redyeing issue date should be more than batch dyeing inward date (" + inward_date.Date.ToString("dd-MM-yyyy").Substring(0, 10) + ")");
                return;
            }
            this.qualityTB.Text = this.old_batch_row["Quality"].ToString();
            this.colourTB.Text = this.old_batch_row["Colour"].ToString();
            this.batchWeightTB.Text = this.old_batch_row["Net_Weight"].ToString();
            this.companyNameTB.Text = this.old_batch_row["Company_Name"].ToString();
            this.dyeingCompanyNameTB.Text = this.old_batch_row["Dyeing_Company_Name"].ToString();
            this.nonRedyeingBatchNoTB.Text = c.getNextNumber_FiscalYear("Highest_Batch_No", c.getFinancialYear(DateTime.Now));
            this.redyeingBatchNoTB.Text = (int.Parse(this.nonRedyeingBatchNoTB.Text) + 1).ToString();
            this.redyeingColourCB.Enabled = true;
            this.rateTextBoxTB.ReadOnly = false;
            this.addTrayButton.Enabled = true;
            this.editTrayButton.Enabled = true;
            this.saveButton.Enabled = true;

            //Create drop-down Colour list
            var dataSource1 = new List<string>();
            DataTable d = c.getQC('l');
            dataSource1.Add("---Select---");
            for (int i = 0; i < d.Rows.Count; i++)
            {
                dataSource1.Add(d.Rows[i][0].ToString());
            }
            List<string> final_list = dataSource1.Distinct().ToList();
            this.redyeingColourCB.DataSource = final_list;
            this.redyeingColourCB.DisplayMember = "Colour";
            this.redyeingColourCB.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.redyeingColourCB.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.redyeingColourCB.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            this.batchNoCB.Enabled = false;
            this.issueDateDTP.Enabled = false;
            this.loadButton.Enabled = false;
        }
        private void addTrayButton_Click(object sender, EventArgs e)
        {
            
            f = new M_V2_trayInputForm(null, null, null, "135", 18, -1F, -1F, this.qualityTB.Text, this.companyNameTB.Text, null, -1, this);
            f.Location = new System.Drawing.Point((int)(Screen.PrimaryScreen.Bounds.Width / 1.8), Screen.PrimaryScreen.Bounds.Height/8);
            f.ShowDialog();
            this.all_trays = f.tray_details;
            
        }
        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            if (e.RowIndex != dataGridView1.Rows.Count - 1)
            {
                //dataGridView1.Rows[e.RowIndex].Cells[0].Value = e.RowIndex + 1;
            }
        }
        private void editTrayButton_Click(object sender, EventArgs e)
        {
            if(this.dataGridView1.SelectedRows.Count==0)
            {
                return;
            }
            DataRow row = this.all_trays.Rows[this.dataGridView1.CurrentRow.Index];
            f = new M_V2_trayInputForm(row["Date Of Input"].ToString(), row["Date Of Production"].ToString(), row["Tray No"].ToString(), row["Spring"].ToString(), int.Parse(row["No Of Springs"].ToString()), float.Parse(row["Tray Tare"].ToString()), float.Parse(row["Gross Weight"].ToString()), row["Quality"].ToString(), row["Company Name"].ToString(), row["Machine No"].ToString(), this.dataGridView1.CurrentRow.Index, this);
            f.Location = new System.Drawing.Point((int)(Screen.PrimaryScreen.Bounds.Width / 1.8), Screen.PrimaryScreen.Bounds.Height / 8);
            f.ShowDialog();
            this.all_trays = f.tray_details;
        }
        private void redyeingColourCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(this.redyeingColourCB.SelectedIndex!=0)
            {
                float rate = c.getDyeingRate(this.rateTextBoxTB.Text, this.qualityTB.Text);
                if(rate!=-1F)
                {
                    this.rateTextBoxTB.Text = rate.ToString();
                }
            }
        }
        private void saveButton_Click(object sender, EventArgs e)
        {
            //checks
            if(float.Parse(this.nonRedyeingBatchWeightTB.Text) < 0)
            {
                c.ErrorBox("Weight of batch being sent for redyeing is greater than original batch weight");
                return;
            }
            if(redyeingColourCB.SelectedIndex==0)
            {
                c.ErrorBox("Please select redyeing colour");
                return;
            }
            if(rateTextBoxTB.Text==null || rateTextBoxTB.Text.Replace(" ", "")=="")
            {
                c.ErrorBox("Please enter rate");
                return;
            }
            bool added = c.addRedyeingVoucher(this.inputDateDTP.Value, this.issueDateDTP.Value, this.old_batch_row, int.Parse(this.nonRedyeingBatchNoTB.Text), int.Parse(this.redyeingBatchNoTB.Text), float.Parse(nonRedyeingBatchWeightTB.Text), float.Parse(redyeingBatchWeightTB.Text), c.getFinancialYear(this.issueDateDTP.Value), this.dataGridView1.DataSource as DataTable, this.redyeingColourCB.Text, float.Parse(this.rateTextBoxTB.Text));
            if(added==true)
            {
                this.disable_form_edit();
            }
        }

        private void disable_form_edit()
        {
            this.redyeingColourCB.Enabled = false;
            this.rateTextBoxTB.Enabled = false;
            this.addTrayButton.Enabled = false;
            this.editTrayButton.Enabled = false;
            this.saveButton.Enabled = false;
        }

        public void CellSum()
        {
            float sum = 0;
            try
            {
                if (dataGridView1.Rows.Count == 0)
                {
                    return;
                }
                for (int i = 0; i < dataGridView1.Rows.Count; ++i)
                {
                    if (c.Cell_Not_NullOrEmpty(this.dataGridView1, i, 3) == true)
                        sum += float.Parse(dataGridView1.Rows[i].Cells["Net Weight"].Value.ToString());
                }
                this.redyeingBatchWeightTB.Text = sum.ToString();
                this.nonRedyeingBatchWeightTB.Text = (float.Parse(this.batchWeightTB.Text) - float.Parse(this.redyeingBatchWeightTB.Text)).ToString();
            }
            catch
            {
                return;
            }
        }
    }
}
