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
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Tab &&
                dataGridView1.EditingControl != null &&
                //msg.HWnd == dataGridView1.EditingControl.Handle &&
                dataGridView1.SelectedCells
                    .Cast<DataGridViewCell>()
                    .Any(x => x.ColumnIndex == 1))
            {
                Console.WriteLine("Inside process cmd key tab 1");
                this.edit_cmd_send = true;
                SendKeys.Send("{Tab}");
                return false;
            }

            if (keyData == Keys.Tab &&
                dataGridView1.EditingControl != null &&
                msg.HWnd == dataGridView1.EditingControl.Handle &&
                dataGridView1.SelectedCells
                    .Cast<DataGridViewCell>()
                    .Any(x => x.ColumnIndex == 5))
            {
                this.edit_cmd_send = true;
                int rowtab_index = dataGridView1.SelectedCells[0].RowIndex;
                //DataGridViewCell x = dataGridView1.SelectedCells.Cast<DataGridViewCell>().First();
                if (c.Cell_Not_NullOrEmpty(this.dataGridView1, rowtab_index, -1, "payment_date") && !c.Cell_Not_NullOrEmpty(this.dataGridView1, rowtab_index + 1, -1, "payment_date"))
                {
                    dataGridView1.Rows[rowtab_index + 1].Cells["payment_date"].Value = dataGridView1.Rows[rowtab_index].Cells["payment_date"].Value;
                }
                SendKeys.Send("{Tab}");
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
            return base.ProcessCmdKey(ref msg, keyData);
        }
        DbConnect c = new DbConnect();
        string customer;
        Dictionary<Tuple<string,string>, DataRow> do_data = new Dictionary<Tuple<string,string>, DataRow>();
        private bool edit_cmd_send = false;
        public M_VC_paymentForm()
        {
            InitializeComponent();
            this.paymentLabel.Text = "0.00";

            this.customerCB.DisplayMember = "Customers";
            this.customerCB.DropDownStyle = ComboBoxStyle.DropDown;//Create a drop-down list
            this.customerCB.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.customerCB.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            
            dataGridView1.Rows[0].Cells["payment_date"].Value = DateTime.Today.Date.ToString().Substring(0,10);
            dataGridView1.Rows[0].Cells["sl_no"].Value = 1;
            dataGridView1.Rows[0].Cells["close_do"].Value = false;
            
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
                    if (c.Cell_Not_NullOrEmpty(dataGridView1,i,-1, "payment_amount"))
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
                if (string.IsNullOrEmpty(data.Rows[i]["Paid_Amount"].ToString())) data.Rows[i]["Paid_Amount"] = 0F;
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
            if (dataGridView1.Enabled == false || dataGridView1.ReadOnly == true)
            {
                return;
            }
            if (e.KeyCode == Keys.Enter && dataGridView1.SelectedCells.Cast<DataGridViewCell>().Any(x => x.ColumnIndex == 4))
            {
                try
                {
                    if (dataGridView1.Focused && dataGridView1.CurrentCell.ColumnIndex == 4
                        )
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

            if (e.KeyCode == Keys.Tab &&
            (dataGridView1.SelectedCells.Cast<DataGridViewCell>().Any(x => x.ColumnIndex == 1) || this.edit_cmd_send == true))
            {
                bool edit_cmd_local = this.edit_cmd_send;
                this.edit_cmd_send = false;
                int rowindex_tab = dataGridView1.SelectedCells[0].RowIndex;
                Console.WriteLine("row index " + rowindex_tab);

                if (rowindex_tab < 0)
                {
                    SendKeys.Send("{tab}");
                    return;
                }
                if (dataGridView1.Rows.Count - 2 == rowindex_tab)
                {
                    DataGridViewRow row = (DataGridViewRow)dataGridView1.Rows[rowindex_tab].Clone();
                    dataGridView1.Rows.Add(row);
                    dataGridView1.Rows[rowindex_tab + 1].Cells["close_do"].Value = false;
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
                    SendKeys.Send("{tab}");
                    return;
                }
                if (dataGridView1.Rows.Count - 2 == rowindex_tab)
                {
                    DataGridViewRow row = (DataGridViewRow)dataGridView1.Rows[rowindex_tab].Clone();
                    dataGridView1.Rows.Add(row);
                    dataGridView1.Rows[rowindex_tab + 1].Cells["close_do"].Value = false;
                }
                SendKeys.Send("{tab}");
                SendKeys.Send("{tab}");
            }
            if (e.KeyCode == Keys.Tab &&
               (dataGridView1.SelectedCells.Cast<DataGridViewCell>().Any(x => x.ColumnIndex == 5) || this.edit_cmd_send == true))
            {
                Console.WriteLine("last column");
                bool edit_cmd_local = this.edit_cmd_send;
                this.edit_cmd_send = false;
                int rowindex_tab = dataGridView1.SelectedCells[0].RowIndex;
                //if (rowindex_tab < 0)
                //{
                //    SendKeys.Send("{tab}");
                //    SendKeys.Send("{tab}");
                //    return;         
                //}
                if (dataGridView1.Rows.Count - 2 == rowindex_tab)
                {
                    DataGridViewRow row = (DataGridViewRow)dataGridView1.Rows[rowindex_tab].Clone();
                    dataGridView1.Rows.Add(row);
                    dataGridView1.Rows[rowindex_tab + 1].Cells["close_do"].Value = false;
                }
                if (dataGridView1.Rows.Count - 1 == rowindex_tab)
                {
                    Console.WriteLine("This case");
                    return;
                }
                if (c.Cell_Not_NullOrEmpty(this.dataGridView1, rowindex_tab,-1,"payment_date") && !c.Cell_Not_NullOrEmpty(this.dataGridView1, rowindex_tab + 1, -1, "payment_date"))
                {
                    dataGridView1.Rows[rowindex_tab + 1].Cells["payment_date"].Value = dataGridView1.Rows[rowindex_tab].Cells["payment_date"].Value;
                }
                dataGridView1.NotifyCurrentCellDirty(true);
                dataGridView1.EndEdit();
                dataGridView1.NotifyCurrentCellDirty(false);
                if (edit_cmd_local == false)
                {
                    SendKeys.Send("{tab}");
                    SendKeys.Send("{tab}");
                }
            }
            if (e.KeyCode == Keys.Enter &&
               (dataGridView1.SelectedCells.Cast<DataGridViewCell>().Any(x => x.ColumnIndex == 1) || this.edit_cmd_send == true))
            {
                dataGridView1.BeginEdit(true);
                ComboBox c = (ComboBox)dataGridView1.EditingControl;
                if (c != null) c.DroppedDown = true;
                e.Handled = true;
            }
            if (e.KeyCode == Keys.Enter &&
               (dataGridView1.SelectedCells.Cast<DataGridViewCell>().Any(x => x.ColumnIndex == 6)))
            {
                int rowindex_tab = dataGridView1.SelectedCells[0].RowIndex;
                if (dataGridView1.Rows.Count - 1 == rowindex_tab) return;

                if ((bool)dataGridView1.Rows[rowindex_tab].Cells["close_do"].Value == true) dataGridView1.Rows[rowindex_tab].Cells["close_do"].Value = false;
                else dataGridView1.Rows[rowindex_tab].Cells["close_do"].Value = true;

                
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
                Console.WriteLine("--" + split_do_no[0] + "--" + split_do_no[1] + "--");
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
        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                dataGridView1.Rows[e.RowIndex].Selected = true;
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
            this.paymentLabel.Text = CellSum().ToString("F2");
        }
    }
}
