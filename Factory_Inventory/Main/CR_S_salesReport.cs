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

namespace Factory_Inventory.Main
{
    public partial class CR_S_salesReport : Form
    {
        MainConnect mc;
        DbConnect c = new DbConnect();
        Dictionary<string, int> qualityBeforeJob = new Dictionary<string, int>();
        Dictionary<string, int> customers = new Dictionary<string, int>();
        public CR_S_salesReport(MainConnect mc)
        {
            InitializeComponent();
            this.mc = mc;

            DataTable qualityDT = c.runQuery("SELECT Quality FROM Quality");
            DataTable T_qualityDT = c.runQuery("SELECT Quality_Before_Job_ID, Quality_Before_Job FROM T_M_Quality_Before_Job");
            foreach (DataRow row in qualityDT.Rows) checkedListBox2.Items.Add(row["Quality"].ToString());
            foreach (DataRow row in T_qualityDT.Rows)
            {
                checkedListBox2.Items.Add(row["Quality_Before_Job"].ToString());
                qualityBeforeJob[row["Quality_Before_Job"].ToString()] = int.Parse(row["Quality_Before_Job_ID"].ToString());
            }

            DataTable customerDT = c.runQuery("SELECT Customers FROM Customers");
            DataTable T_customerDT = c.runQuery("SELECT Customer_ID, Customer_Name FROM T_M_Customers");
            foreach (DataRow row in customerDT.Rows) checkedListBox3.Items.Add(row["Customers"].ToString());
            foreach (DataRow row in T_customerDT.Rows)
            {
                checkedListBox3.Items.Add(row["Customer_Name"].ToString());
                customers[row["Customer_Name"].ToString()] = int.Parse(row["Customer_ID"].ToString());
            }


        }

        private void CR_S_salesReport_Load(object sender, EventArgs e)
        {
            this.Text = "Sales Report";
            setAllBoxes(this.checkedListBox1, true);
            setAllBoxes(this.checkedListBox2, true);
            setAllBoxes(this.checkedListBox3, true);
            setAllBoxes(this.checkedListBox4, true);

            dateTimePicker1.MaxDate = DateTime.Today;
            dateTimePicker2.MaxDate = DateTime.Today;

            checkBox1.Checked = true;
            checkBox2.Checked = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Checks
            if (checkedListBox1.CheckedItems.Count == 0)
            {
                c.ErrorBox("Select atleast one Business Line");
                return;
            }
            if (dateTimePicker2.Value.Date < dateTimePicker1.Value.Date)
            {
                c.ErrorBox("To Date should be greater than or equal to from date");
                return;
            }
            if (checkedListBox2.CheckedItems.Count == 0)
            {
                c.ErrorBox("Select atleast one Quality");
                return;
            }
            if (checkedListBox3.CheckedItems.Count == 0)
            {
                c.ErrorBox("Select atleast one Customer");
                return;
            }
            if (checkedListBox4.CheckedItems.Count == 0)
            {
                c.ErrorBox("Select Type of Sale");
                return;
            }

            if (checkedListBox1.GetItemChecked(0))
            {
                string sql = "SELECT Date_Of_Sale as 'Date of Sale', Sale_DO_No as 'DO Number', Quality, Customer, Net_Weight as 'Net Weight', Sale_Rate as 'Rate', Net_Weight* Sale_Rate as 'Amount'\n";
                sql += "FROM Sales_Voucher\n";
                sql += "WHERE Date_Of_Sale >= '" + dateTimePicker1.Value.Date.ToString("yyyy-MM-dd") + "' AND Date_Of_Sale <= '" + dateTimePicker2.Value.Date.ToString("yyyy-MM-dd") + "'\n";
                sql += "AND Quality IN (";
                for (int i = 0; i < checkedListBox2.Items.Count; i++)
                {
                    if (checkedListBox2.GetItemChecked(i)) sql += "'" + checkedListBox2.Items[i].ToString() + "',";
                }
                sql = sql.Substring(0, sql.Length - 1);
                sql += ") \nAND Customer IN (";
                for (int i = 0; i < checkedListBox3.Items.Count; i++)
                {
                    if (checkedListBox3.GetItemChecked(i)) sql += "'" + checkedListBox3.Items[i].ToString() + "',";
                }
                sql = sql.Substring(0, sql.Length - 1);
                sql += ")";

                DataTable manufacturingResults = c.runQuery(sql);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true) setAllBoxes(this.checkedListBox2, true);
            else setAllBoxes(this.checkedListBox2, false);
        }

        private void setAllBoxes(CheckedListBox cbl, bool value)
        {
            for (int i = 0; i < cbl.Items.Count; i++) cbl.SetItemChecked(i, value);
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true) setAllBoxes(this.checkedListBox3, true);
            else setAllBoxes(this.checkedListBox3, false);
        }
    }
}
