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

namespace Factory_Inventory.Factory_Data
{
    public partial class M_VC_paymentForm : Form
    {
        DbConnect c = new DbConnect();
        string customer;
        Dictionary<Tuple<string,string>, DataRow> do_data = new Dictionary<Tuple<string,string>, DataRow>();
        public M_VC_paymentForm()
        {
            InitializeComponent();
            this.customerCB.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.customerCB.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.customerCB.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            dataGridView1.Rows[0].Cells["payment_date"].Value = DateTime.Today.Date.ToString().Substring(0,10);
            dataGridView1.Rows[0].Cells["sl_no"].Value = 1;

            DataTable d = c.runQuery("SELECT Customers FROM Customers");
            List<string> datasource = new List<string>();
            for (int i = 0; i < d.Rows.Count; i++) datasource.Add(d.Rows[i][0].ToString());
            customerCB.DataSource = datasource;
        }

        //helpers
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
                        sum += float.Parse(dataGridView1.Rows[i].Cells["payment_amount"].Value.ToString());
                }
                return sum;
            }
            catch
            {
                return sum;
            }
        }
        private List<string> split_do_no(string do_no_fiscal_year)
        {
            string[] do_no_fiscal_year_split = do_no_fiscal_year.Split('(');
            List<string> ret = new List<string>();
            ret.Add(do_no_fiscal_year_split[0].Split(' ')[0]);
            ret.Add(do_no_fiscal_year_split[1].Substring(0, 9));
            return ret;
        }

        //buttons
        private void button1_Click(object sender, EventArgs e)
        {
            this.customer = customerCB.SelectedItem.ToString();
            string query = "SELECT Voucher_ID, Sale_Rate, Fiscal_Year, Sale_DO_No, Net_Weight, SUM(Payments.Payment_Amount) Paid_Amount\n";
            query += "FROM Sales_Voucher\n";
            query += "LEFT JOIN Payments\n";
            query += "ON Sales_Voucher.Voucher_ID = Payments.Sales_Voucher_ID\n";
            query += "WHERE Payment_Closed = 0 AND Customer = '" + this.customer + "'\n";
            query += "GROUP BY Voucher_ID, Sale_Rate, Fiscal_Year, Sale_DO_No, Net_Weight";
            DataTable data = c.runQuery(query);
            DataGridViewComboBoxColumn do_no_col = (DataGridViewComboBoxColumn)this.dataGridView1.Columns["do_no"];
            for (int i = 0; i < data.Rows.Count; i++)
            {
                do_no_col.Items.Add(data.Rows[i]["Sale_DO_No"].ToString() + " (" + data.Rows[i]["Fiscal_Year"].ToString() + ")");
                this.do_data[new Tuple<string, string>(data.Rows[i]["Sale_DO_No"].ToString(), data.Rows[i]["Fiscal_Year"].ToString())] = data.Rows[i];
            }

            if (data.Rows.Count == 0) c.WarningBox("No pending DOs found");
            else
            {
                c.SuccessBox("Loaded " + data.Rows.Count.ToString() + " pending DOs");
                customerCB.Enabled = false;
                dataGridView1.ReadOnly = false;
                loadDataButton.Enabled = false;
            }
        }

        //datagridview
        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && dataGridView1.SelectedCells.Cast<DataGridViewCell>().Any(x => x.ColumnIndex == 5))
            {
                try
                {
                    if (dataGridView1.Focused && dataGridView1.CurrentCell.ColumnIndex == 5)
                    {
                        DateTime d;
                        if (string.IsNullOrEmpty(dataGridView1.CurrentCell.Value.ToString()) == false)
                        {
                            d = Convert.ToDateTime(dataGridView1.CurrentCell.Value);
                        }
                        else
                        {
                            d = DateTime.Today;
                        }
                        setDate f = new setDate(d);
                        //f.setMinMax(dtp.MinDate, dtp.MaxDate);
                        f.ShowDialog();
                        dataGridView1.CurrentCell.Value = f.result.Date.ToString().Substring(0, 10);
                        e.Handled = true;
                    }
                }
                catch
                {
                    Console.WriteLine("DTP Exception");
                }

            }
        }
        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            if (e.RowIndex != dataGridView1.Rows.Count - 1)
            {
                dataGridView1.Rows[e.RowIndex].Cells[0].Value = e.RowIndex + 1;
            }
        }
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1 && e.RowIndex >= 0)
            {
                if (!c.Cell_Not_NullOrEmpty(this.dataGridView1, e.RowIndex, e.ColumnIndex))
                {
                    for (int i = 2; i <= 5; i++) dataGridView1.Rows[e.RowIndex].Cells[i].Value = null;
                    dataGridView1.Rows[e.RowIndex].Cells["close_do"].Value = false;
                    this.paymentLabel.Text = CellSum().ToString("F2");
                    return;
                }
                Console.WriteLine("CVC");
                List<string> split_do_no = this.split_do_no(dataGridView1.Rows[e.RowIndex].Cells["do_no"].Value.ToString());
                DataRow do_data_row = this.do_data[new Tuple<string, string>(split_do_no[0], split_do_no[1])];
                dataGridView1.Rows[e.RowIndex].Cells["total_amount"].Value = (float.Parse(do_data_row["Sale_Rate"].ToString())*float.Parse(do_data_row["Net_Weight"].ToString())).ToString("F2");
                dataGridView1.Rows[e.RowIndex].Cells["pending_amount"].Value = (float.Parse(dataGridView1.Rows[e.RowIndex].Cells["total_amount"].Value.ToString()) - float.Parse(do_data_row["Paid_Amount"].ToString())).ToString("F2");
            }
        }
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 5 && e.RowIndex >= 0)
            {
                try
                {
                    if (c.Cell_Not_NullOrEmpty(this.dataGridView1, e.RowIndex, e.ColumnIndex))
                    {
                        float payment_amount = float.Parse(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                        List<string> split_do_no = this.split_do_no(dataGridView1.Rows[e.RowIndex].Cells["do_no"].Value.ToString());
                        DataRow do_data_row = this.do_data[new Tuple<string, string>(split_do_no[0], split_do_no[1])];
                        dataGridView1.Rows[e.RowIndex].Cells["pending_amount"].Value = (float.Parse(dataGridView1.Rows[e.RowIndex].Cells["total_amount"].Value.ToString()) - float.Parse(do_data_row["Paid_Amount"].ToString()) - payment_amount).ToString("F2");
                    }
                }
                catch
                {
                    c.ErrorBox("Please Enter Decimal Payment", "Error");
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "";
                    //dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].Selected = true;
                    //SendKeys.Send("{Left}");
                    return;
                }
                this.paymentLabel.Text = CellSum().ToString("F2");
            }
        }
    }
}
