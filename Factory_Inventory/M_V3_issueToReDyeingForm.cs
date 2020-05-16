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
        private int prev = 0;
        public Label dummyLabel;
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
                    for (int j = i + 1; j < batch_no_arr.Count; j++)
                    {
                        if (dt.Rows[i]["Batch_No"].ToString() == dt.Rows[j]["Batch_No"].ToString())
                        {
                            batch_no_arr[i] = dt.Rows[i]["Batch_No"].ToString() + "  (" + dt.Rows[i]["Fiscal_Year"].ToString() + ")";
                            batch_no_arr[j] = dt.Rows[j]["Batch_No"].ToString() + "  (" + dt.Rows[j]["Fiscal_Year"].ToString() + ")";
                        }
                    }
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

        }

        private void M_V3_issueToReDyeingForm_Load(object sender, EventArgs e)
        {
            
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
            DataRow row = c.getBatchRow_BatchNo(batchno, fiscal_year);
            this.qualityTB.Text = row["Quality"].ToString();
            this.colourTB.Text = row["Colour"].ToString();
            this.batchWeightTB.Text = row["Net_Weight"].ToString();
            this.companyNameTB.Text = row["Company_Name"].ToString();
            this.dyeingCompanyNameTB.Text = row["Dyeing_Company_Name"].ToString();
            this.nonRedyeingBatchNoTB.Text = c.getNextNumber_FiscalYear("Highest_Batch_No", c.getFinancialYear(DateTime.Now));
            this.redyeingBatchNoTB.Text = (int.Parse(this.nonRedyeingBatchNoTB.Text) + 1).ToString();
            this.redyeingColourCB.Enabled = true;
            this.rateTextBoxTB.ReadOnly = false;
            this.addTrayButton.Enabled = true;
            this.editTrayButton.Enabled = true;
            this.saveButton.Enabled = true;

            //Create drop-down Colour list
            var dataSource1 = new List<string>();
            DataTable dt = c.getQC('l');
            dataSource1.Add("---Select---");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dataSource1.Add(dt.Rows[i][0].ToString());
            }
            List<string> final_list = dataSource1.Distinct().ToList();
            this.redyeingColourCB.DataSource = final_list;
            this.redyeingColourCB.DisplayMember = "Colour";
            this.redyeingColourCB.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.redyeingColourCB.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.redyeingColourCB.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

        }

        private void addTrayButton_Click(object sender, EventArgs e)
        {
            
            f = new M_V2_trayInputForm(null, null, null, "135", 18, -1F, -1F, this.qualityTB.Text, this.companyNameTB.Text, null, dataGridView1);
            f.ShowDialog();
            this.all_trays = f.tray_details;
            Console.WriteLine(this.all_trays.Rows.Count);
            //Console.WriteLine("hi");
            //if(this.prev!=f.dummyint)
            //{
            //    Console.WriteLine("add clicked");
            //    this.prev = f.dummyint;
            //    this.addTrayButton.PerformClick();
            //}
            //if (f.closed==true)
            //{
            //    return;
            //}
            //List<string> tray_details = f.tray_details;
        }

        private void redyeingColourCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(this.redyeingColourCB.SelectedIndex!=0)
            {
                float rate = c.getDyeingRate(this.rateTextBoxTB.Text, this.qualityTB.Text);
                if(rate!=0)
                {
                    this.rateTextBoxTB.Text = rate.ToString();
                }
            }
        }
    }
}
