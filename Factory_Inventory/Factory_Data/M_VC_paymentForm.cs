using Factory_Inventory.Factory_Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Factory_Inventory.Factory_Classes.Structures;

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
                    .Any(x => x.ColumnIndex == 3))
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
        private bool edit_form = false;
        public M_VC_paymentForm()
        {
            InitializeComponent();
            this.Name = "Payment Voucher";

            this.totalPaymentTB.Text = "0.00";

            this.customerCB.DisplayMember = "Customers";
            this.customerCB.DropDownStyle = ComboBoxStyle.DropDown;//Create a drop-down list
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
        private void edit_pending_amount(string do_string)
        {
            float sum = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; ++i)
            {
                if (c.Cell_Not_NullOrEmpty(dataGridView1, i, -1, "payment_amount"))
                {
                    if (dataGridView1.Rows[i].Cells["do_no"].Value.ToString() == do_string)
                    {
                        try
                        {
                            sum += float.Parse(dataGridView1.Rows[i].Cells["payment_amount"].Value.ToString());
                        }
                        catch
                        {
                            continue;
                        }
                    }
                }
            }
            List<string> split_do_no = this.split_do_no(do_string);
            DataRow do_data_row = this.do_data[new Tuple<string, string>(split_do_no[0], split_do_no[1])];
            int index = -1;
            for(int i=0;i<dataGridView2.Rows.Count;i++)
            {
                if(dataGridView2.Rows[i].Cells["do_no_2"].Value.ToString() == do_string)
                {
                    index = i;
                    break;
                }
            }
            dataGridView2.Rows[index].Cells["pending_amount"].Value = (float.Parse(dataGridView2.Rows[index].Cells["total_amount"].Value.ToString()) - float.Parse(do_data_row["Paid_Amount"].ToString()) - sum).ToString("F2");
        }
        private void disable_form_edit()
        {
            customerCB.Enabled = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.ReadOnly = true;
            loadDataButton.Enabled = false;
            saveButton.Enabled = false;
            narrationTB.Enabled = false;
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
                string do_no_show = data.Rows[i]["Sale_DO_No"].ToString() + " (" + data.Rows[i]["Fiscal_Year"].ToString() + ")";
                do_no_col.Items.Add(do_no_show);
                do_data[new Tuple<string, string>(data.Rows[i]["Sale_DO_No"].ToString(), data.Rows[i]["Fiscal_Year"].ToString())] = data.Rows[i];
                float total_amount = float.Parse(data.Rows[i]["Sale_Rate"].ToString()) * float.Parse(data.Rows[i]["Net_Weight"].ToString());
                dataGridView2.Rows.Add(do_no_show, total_amount.ToString(), (total_amount - float.Parse(data.Rows[i]["Paid_Amount"].ToString())).ToString(), false);
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
        private void saveButton_Click(object sender, EventArgs e)
        {
            if (c.check_login_val() == false) return;
            //checks
            if (dataGridView1.Rows.Count < 0)
            {
                c.ErrorBox("Please enter payments", "Error");
                return;
            }
            
            //Iterate to check for mistakes in dataGridView1
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                int count = 0;
                if (dataGridView1.Rows[i].Cells["do_no"].Value == null)
                    count++;
                if (dataGridView1.Rows[i].Cells["payment_date"].Value == null)
                    count++;
                if (dataGridView1.Rows[i].Cells["payment_amount"].Value == null)
                    count++;
                Console.WriteLine("Count: " + count);

                if (count != 0 && count != 3)
                {
                    c.ErrorBox("Error at row " + (i + 1).ToString(), "Error");
                    return;
                }
            }

            //Iterate to check for mistakes in dataGridView2
            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                if (float.Parse(dataGridView2.Rows[i].Cells["pending_amount"].Value.ToString()) < 0F)
                {
                    c.ErrorBox("Pending Amount at row " + (i + 1).ToString() + " is negative", "Error");
                    return;
                }
                if (float.Parse(dataGridView2.Rows[i].Cells["pending_amount"].Value.ToString()) > 0F)
                {
                    c.WarningBox("Discount of ₹" + dataGridView2.Rows[i].Cells["pending_amount"].Value.ToString() + " at row " + (i + 1).ToString(), "Error");
                    return;
                }
            }

            if (this.edit_form == false)
            {
                string input_date = inputDateDTP.Value.ToString("yyyy-MM-dd");
                string customer = customerCB.SelectedItem.ToString();
                string narration = narrationTB.Text;

                string sql = "begin transaction; begin try; \n";
                sql += "DECLARE @voucherID int; \nINSERT INTO Payment_Voucher (Date_Of_Input, Customer, Narration) VALUES ('" + input_date + "','" + customer + "', '" + narration + "');\n ";
                sql += "SELECT @voucherID = SCOPE_IDENTITY(); \n";
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    int count = 0;
                    if (dataGridView1.Rows[i].Cells["do_no"].Value == null)
                        count++;
                    if (dataGridView1.Rows[i].Cells["payment_date"].Value == null)
                        count++;
                    if (dataGridView1.Rows[i].Cells["payment_amount"].Value == null)
                        count++;
                    Console.WriteLine("Count: " + count);

                    if (count == 0)
                    {
                        List<string> split_do_no = this.split_do_no(dataGridView1.Rows[i].Cells["do_no"].Value.ToString());
                        DataRow do_data_row = this.do_data[new Tuple<string, string>(split_do_no[0], split_do_no[1])];
                        string sales_voucher_id = do_data_row["Voucher_ID"].ToString();
                        string payment_amount = dataGridView1.Rows[i].Cells["payment_amount"].Value.ToString();
                        DateTime dt = DateTime.ParseExact(dataGridView1.Rows[i].Cells["payment_date"].Value.ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture);
                        string date_of_payment = dt.ToString("yyyy-MM-dd");
                        string comments = "";
                        if (c.Cell_Not_NullOrEmpty(dataGridView1, i, -1, "comments") == true) comments = dataGridView1.Rows[i].Cells["comments"].Value.ToString();

                        sql += "INSERT INTO Payments (Sales_Voucher_ID, Payment_Amount, Payment_Voucher_ID, Date_of_Payment, Comments) ";
                        sql += "VALUES (" + sales_voucher_id + ", " + payment_amount + ", @voucherID, '" + date_of_payment + "', '" + comments + "');\n";
                    }
                }

                for (int i = 0; i < dataGridView2.Rows.Count; i++)
                {
                    //close DOs
                    if (Convert.ToBoolean(dataGridView2.Rows[i].Cells["close_do"].Value) == true)
                    {
                        List<string> split_do_no = this.split_do_no(dataGridView1.Rows[i].Cells["do_no"].Value.ToString());
                        DataRow do_data_row = this.do_data[new Tuple<string, string>(split_do_no[0], split_do_no[1])];
                        string sales_voucher_id = do_data_row["Voucher_ID"].ToString();

                        sql += "UPDATE Sales_Voucher SET Payment_Closed = 1 WHERE Voucher_ID = " + sales_voucher_id + ";\n";
                    }
                }


                //catch
                sql += "commit transaction; end try BEGIN CATCH rollback transaction; \n";
                sql += "DECLARE @ErrorMessage NVARCHAR(4000); DECLARE @ErrorSeverity INT; DECLARE @ErrorState INT; SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE(); \n";
                sql += "RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState); END CATCH; ";
                ErrorTable et = c.runQuerywithError(sql);

                if (et.dt != null)
                {
                    dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.LawnGreen;
                    c.SuccessBox("Voucher Added Successfully");
                    disable_form_edit();
                }
                else
                {
                    Global.ErrorBox("Could not save voucher:\n" + et.e.Message);
                    return;
                }
            }
            //else
            //{
            //    string bill_date = billDateDTP.Value.ToString("yyyy-MM-dd");
            //    string fiscal_year = c.getFinancialYear(billDateDTP.Value);
            //    int type = int.Parse(typeCB.Text);
            //    string sql = "begin transaction; begin try;\n";
            //    sql += "UPDATE T_Carton_Inward_Voucher SET Type = " + type + ", Date_Of_Billing = '" + bill_date + "', Bill_No = '" + billNumberTextboxTB.Text + "', Company_ID = " + company_dict[comboBox2CB.SelectedItem.ToString()] + ", Fiscal_Year = '" + fiscal_year + "', Narration = '" + narrationTB.Text + "' WHERE Voucher_ID = " + this.voucher_id + ";\n";
            //    for (int i = 0; i < dataGridView1.Rows.Count; i++)
            //    {
            //        int count = 0;
            //        //ComboBox c = (ComboBox)dataGridView1.EditingControl;
            //        if (dataGridView1.Rows[i].Cells[1].Value == null || dataGridView1.Rows[i].Cells[1].Value.ToString() == "---Select---")
            //            count++;
            //        if (dataGridView1.Rows[i].Cells[2].Value == null || dataGridView1.Rows[i].Cells[2].Value.ToString() == "---Select---")
            //            count++;
            //        if (dataGridView1.Rows[i].Cells["Carton_No"].Value == null)
            //            count++;
            //        if (dataGridView1.Rows[i].Cells["Grade"].Value == null || dataGridView1.Rows[i].Cells["Grade"].Value.ToString() == "---Select---")
            //            count++;
            //        if (dataGridView1.Rows[i].Cells[5].Value == null)
            //            count++;
            //        Console.WriteLine("Count: " + count);

            //        if (count == 0)
            //        {
            //            string quality = dataGridView1.Rows[i].Cells["Quality"].Value.ToString();
            //            string colour = dataGridView1.Rows[i].Cells["Colour"].Value.ToString();
            //            float weight = float.Parse(dataGridView1.Rows[i].Cells["Weight"].Value.ToString());
            //            string grade = dataGridView1.Rows[i].Cells["Grade"].Value.ToString();
            //            string comments = "";
            //            if (c.Cell_Not_NullOrEmpty(dataGridView1, i, -1, "Comments") == true) comments = dataGridView1.Rows[i].Cells["Comments"].Value.ToString();

            //            if (c.Cell_Not_NullOrEmpty(dataGridView1, i, -1, "Carton_ID") == true)
            //            {
            //                string carton_id = dataGridView1.Rows[i].Cells["Carton_ID"].Value.ToString();
            //                cartons_to_edit[carton_id] = true;
            //                sql += "UPDATE T_Inward_Carton SET Carton_No = '" + dataGridView1.Rows[i].Cells["Carton_No"].Value.ToString() + "', Quality_ID = " + quality_dict[quality] + ", Colour_ID = " + colour_dict[colour] + ", Company_ID = " + company_dict[comboBox2CB.SelectedItem.ToString()] + ", Net_Weight = " + weight + ", Buy_Cost = " + rate[new Tuple<string, string>(quality, colour)] + ", Comments = '" + comments + "', Inward_Type = " + type + ", Grade = '" + grade + "' WHERE Carton_ID = '" + carton_id + "';\n";
            //            }
            //            else
            //            {
            //                sql += "INSERT INTO T_Inward_Carton (Carton_No, Carton_State, Quality_ID, Colour_ID, Net_Weight, Buy_Cost, Fiscal_Year, Inward_Voucher_ID, Comments, Inward_Type, Grade) ";
            //                sql += "VALUES ('" + dataGridView1.Rows[i].Cells["Carton_No"].Value.ToString() + "', 0, " + quality_dict[quality] + ", " + colour_dict[colour] + ", " + weight + ", " + rate[new Tuple<string, string>(quality, colour)] + ", '" + fiscal_year + "', " + this.voucher_id + ", '" + comments + "', " + type + ", '" + grade + "');\n";
            //            }
            //        }
            //    }

            //    //deleting
            //    foreach (KeyValuePair<string, bool> entry in cartons_to_edit)
            //    {
            //        if (entry.Value == false)
            //        {
            //            sql += "DELETE FROM T_Inward_Carton WHERE Carton_ID = '" + entry.Key + "';\n";
            //        }
            //    }

            //    //catch
            //    sql += "commit transaction; end try BEGIN CATCH rollback transaction;\n";
            //    sql += "DECLARE @ErrorMessage NVARCHAR(4000); DECLARE @ErrorSeverity INT; DECLARE @ErrorState INT; SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE();\n";
            //    sql += "RAISERROR (@ErrorMessage, @ErrorSeverity, 10); END CATCH;\n";
            //    //Note the specific error state to check problems with primary key violations
            //    ErrorTable et = c.runQuerywithError(sql);
            //    if (et.dt != null)
            //    {
            //        dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.LawnGreen;
            //        c.SuccessBox("Voucher Added Successfully");
            //        disable_form_edit();
            //    }
            //    else if (et.e.State == 10)
            //    {
            //        //primary key violation
            //        Global.ErrorBox("Repeated Carton Number with the same Quality, Colour, Company Name, Weight and Financial Year entered\n " + et.e.Message);
            //        return;
            //    }
            //    else
            //    {
            //        Global.ErrorBox("Could not save voucher:\n" + et.e.Message);
            //        return;
            //    }
            //}
            dataGridView1.EnableHeadersVisualStyles = false;
        }

        //datagridview
        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (dataGridView1.Enabled == false || dataGridView1.ReadOnly == true)
            {
                return;
            }
            if (e.KeyCode == Keys.Enter && dataGridView1.SelectedCells.Cast<DataGridViewCell>().Any(x => x.ColumnIndex == 2))
            {
                try
                {
                    if (dataGridView1.Focused && dataGridView1.CurrentCell.ColumnIndex == 2)
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
                }
                if (edit_cmd_local == false)
                {
                    SendKeys.Send("{tab}");
                }
            }
            if (e.KeyCode == Keys.Tab &&
               (dataGridView1.SelectedCells.Cast<DataGridViewCell>().Any(x => x.ColumnIndex == 3) || this.edit_cmd_send == true))
            {
                Console.WriteLine("last column");
                bool edit_cmd_local = this.edit_cmd_send;
                this.edit_cmd_send = false;
                int rowindex_tab = dataGridView1.SelectedCells[0].RowIndex;
                if (dataGridView1.Rows.Count - 2 == rowindex_tab)
                {
                    DataGridViewRow row = (DataGridViewRow)dataGridView1.Rows[rowindex_tab].Clone();
                    dataGridView1.Rows.Add(row);
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
                    SendKeys.Send("{Tab}");
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
        }
        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            if (e.RowIndex != dataGridView1.Rows.Count - 1)
            {
                dataGridView1.Rows[e.RowIndex].Cells[0].Value = e.RowIndex + 1;
            }
        }
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3 && e.RowIndex >= 0)
            {
                if (c.Cell_Not_NullOrEmpty(this.dataGridView1, e.RowIndex, e.ColumnIndex))
                {
                    try
                    {
                        float amount = float.Parse(dataGridView1.Rows[e.RowIndex].Cells["payment_amount"].Value.ToString());
                        if(amount <= 0F)
                        {
                            c.ErrorBox("Negative amount entered", "Error");
                            dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "";
                            return;
                        }
                    }
                    catch
                    {
                        c.ErrorBox("Please Enter Decimal Payment", "Error");
                        dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "";
                        return;
                    }
                    edit_pending_amount(dataGridView1.Rows[e.RowIndex].Cells["do_no"].Value.ToString());
                    this.totalPaymentTB.Text = CellSum().ToString("F2");
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
            this.totalPaymentTB.Text = CellSum().ToString("F2");
        }
    }
}
