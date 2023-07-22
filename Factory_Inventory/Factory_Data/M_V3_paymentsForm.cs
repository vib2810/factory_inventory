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
    public partial class M_V3_paymentsForm : Form
    {

        DbConnect c = new DbConnect();
        Dictionary<string, Tuple<DataRow, float>> do_dict = new Dictionary<string, Tuple<DataRow, float>>();
        HashSet<string> do_set_edit = new HashSet<string>();    //used to see which DOs are added, edited or deleted during edit mode
        List<string> do_to_delete = new List<string>();    //stores which DOs are to be deleted during edit mode
        Dictionary<string, float> amount_received_this_voucher = new Dictionary<string, float>();
        private M_V_history v1_history;
        private bool edit_form = false;               //True if form is being edited
        private int voucher_id;
        private bool populating_dgv_edit_state = false; //Prevents the datagridview to amount pending column to go negative while populating the dgv during view/edit
        public M_V3_paymentsForm()
        {
            InitializeComponent();

            DataTable dt = new DataTable();
            List<string> customers = new List<string>();

            // Read Customer Names from the database and store them in the combobox
            dt = c.runQuery("SELECT Customers FROM Customers");
            for (int i = 0; i < dt.Rows.Count; i++) customers.Add(dt.Rows[i]["Customers"].ToString());
            this.customerCB.DataSource = customers;
            this.customerCB.DisplayMember = "Customers";
            this.customerCB.DropDownStyle = ComboBoxStyle.DropDown;//Create a drop-down list
            this.customerCB.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.customerCB.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            c.set_dgv_column_sort_state(this.dataGridView1, DataGridViewColumnSortMode.NotSortable);
        }

        public M_V3_paymentsForm(DataRow row, bool isEditable, M_V_history v1_history)
        {
            InitializeComponent(); 
            this.edit_form = true;
            this.v1_history = v1_history;

            DataTable dt = new DataTable();
            List<string> customers = new List<string>();

            // Read Customer Names from the database and store them in the combobox
            dt = c.runQuery("SELECT Customers FROM Customers");
            for (int i = 0; i < dt.Rows.Count; i++) customers.Add(dt.Rows[i]["Customers"].ToString());
            this.customerCB.DataSource = customers;
            this.customerCB.DisplayMember = "Customers";
            this.customerCB.DropDownStyle = ComboBoxStyle.DropDown;//Create a drop-down list
            this.customerCB.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.customerCB.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            c.set_dgv_column_sort_state(this.dataGridView1, DataGridViewColumnSortMode.NotSortable);

            if (isEditable == false)
            {
                this.Text += "(View Only)";
                this.saveButton.Enabled = false;
                this.deleteButton.Visible = true;
                this.deleteButton.Enabled = true;
                this.disable_form_edit();
            }
            else
            {
                this.Text += "(Edit)";
                this.paymentDateDTP.Enabled = true;
                this.customerCB.Enabled = false;
                this.saveButton.Enabled = true;
                this.dataGridView1.ReadOnly = false;
                this.dataGridView1.Enabled = true;
                this.loadDOButton.Enabled = false;
            }

            this.paymentDateDTP.Value = Convert.ToDateTime(row["Payment_Date"].ToString());
            this.inputDateDTP.Value = Convert.ToDateTime(row["Input_Date"].ToString());
            this.customerCB.SelectedIndex = this.customerCB.FindStringExact(row["Customers"].ToString());
            this.voucher_id = int.Parse(row["Voucher_ID"].ToString());
            this.narrationTB.Text = row["Narration"].ToString();
            this.loadData();

            string sql = "SELECT Payments.Payment_Amount, Payments.Comments, Payments.Payment_ID, Sales_Voucher.Sale_DO_No, Sales_Voucher.Fiscal_Year, Payments.Display_Order, Sales_Voucher.Sale_Rate*Sales_Voucher.Net_Weight as Total_Amount, Sales_Voucher.DO_Payment_Closed, Sales_Voucher.Voucher_ID FROM Payments\n";
            sql += "JOIN Sales_Voucher ON Sales_Voucher.Voucher_ID = Payments.Sales_Voucher_ID\n";
            sql += "WHERE Payments.Payment_Voucher_ID = " + this.voucher_id + "\n";
            DataTable d1 = c.runQuery(sql);

            dataGridView1.RowCount = d1.Rows.Count + 1;

            DataGridViewComboBoxColumn dgvCmb = (DataGridViewComboBoxColumn)dataGridView1.Columns["doNoCol"];
            this.populating_dgv_edit_state = true;
            for (int i = 0; i < d1.Rows.Count; i++)
            {
                if ((bool)d1.Rows[i]["DO_Payment_Closed"] == true)  //Closed DOs
                {
                    float total_payment = 0F;
                    DataTable d2 = c.runQuery("SELECT Voucher_ID, Sale_DO_No, Sale_Rate, Fiscal_Year, Net_Weight FROM Sales_Voucher WHERE DO_Payment_Closed = 1 AND Customer = '" + this.customerCB.SelectedItem.ToString() + "' AND Sale_DO_No = '" + d1.Rows[i]["Sale_DO_No"].ToString() + "' AND Fiscal_Year = '" + d1.Rows[i]["Fiscal_Year"].ToString() + "'");
                    DataTable d3 = c.runQuery("SELECT Sales_Voucher_ID, SUM(Payment_Amount) as Total_Payment FROM Payments WHERE Sales_Voucher_ID = " + d1.Rows[i]["Voucher_ID"].ToString() + " GROUP BY Sales_Voucher_ID");
                    if (d3.Rows.Count > 0) total_payment = float.Parse(d3.Rows[0]["Total_Payment"].ToString());
                    do_dict[d1.Rows[i]["Sale_DO_No"].ToString() + " (" + d1.Rows[i]["Fiscal_Year"].ToString() + ")"] = new Tuple<DataRow, float>(d2.Rows[0], total_payment);
                    dgvCmb.Items.Add(d1.Rows[i]["Sale_DO_No"].ToString() + " (" + d1.Rows[i]["Fiscal_Year"].ToString() + ")");
                    dataGridView1.Rows[int.Parse(d1.Rows[i]["Display_Order"].ToString())].Cells["doPaymentClosedCol"].Value = true;
                }
                else dataGridView1.Rows[int.Parse(d1.Rows[i]["Display_Order"].ToString())].Cells["doPaymentClosedCol"].Value = false;
                dataGridView1.Rows[int.Parse(d1.Rows[i]["Display_Order"].ToString())].Cells["doNoCol"].Value = d1.Rows[i]["Sale_DO_No"].ToString() + " (" + d1.Rows[i]["Fiscal_Year"].ToString() + ")";
                dataGridView1.Rows[int.Parse(d1.Rows[i]["Display_Order"].ToString())].Cells["amountReceivedCol"].Value = d1.Rows[i]["Payment_Amount"].ToString();
                dataGridView1.Rows[int.Parse(d1.Rows[i]["Display_Order"].ToString())].Cells["commentsCol"].Value = d1.Rows[i]["Comments"].ToString();
                do_set_edit.Add(d1.Rows[i]["Sale_DO_No"].ToString() + " (" + d1.Rows[i]["Fiscal_Year"].ToString() + ")");
                do_to_delete.Add(d1.Rows[i]["Sale_DO_No"].ToString() + " (" + d1.Rows[i]["Fiscal_Year"].ToString() + ")");
                amount_received_this_voucher[d1.Rows[i]["Sale_DO_No"].ToString() + " (" + d1.Rows[i]["Fiscal_Year"].ToString() + ")"] = float.Parse(d1.Rows[i]["Payment_Amount"].ToString());
            }
            this.populating_dgv_edit_state = false;
        }

        private float CellSum()
        {
            float receivedAmountSum = 0F;
            for (int i = 0; i < dataGridView1.Rows.Count-1; i++)
            {
                try
                {
                    if(dataGridView1.Rows[i].Cells["amountReceivedCol"].Value != null) receivedAmountSum += float.Parse(dataGridView1.Rows[i].Cells["amountReceivedCol"].Value.ToString());
                }
                catch { }
            }
            return receivedAmountSum;
        }

        private void M_V3_paymentsForm_Load(object sender, EventArgs e)
        {
            this.Text = "Payments Voucher";
            if(this.edit_form == false) dataGridView1.Rows[0].Cells["doPaymentClosedCol"].Value = false;
        }
        private int loadData()
        {
            DataTable dt = new DataTable();
            DataGridViewComboBoxColumn dgvCmb = (DataGridViewComboBoxColumn)dataGridView1.Columns["doNoCol"];

            dt = c.runQuery("SELECT Voucher_ID, Sale_DO_No, Sale_Rate, Fiscal_Year, Net_Weight FROM Sales_Voucher WHERE DO_Payment_Closed = 0 AND Customer = '" + this.customerCB.SelectedItem.ToString() + "'");

            if (dt.Rows.Count == 0)
            {
                c.WarningBox("No Pending DOs exist for " + this.customerCB.SelectedItem.ToString());
                return 0;
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataTable dt2 = new DataTable();
                float total_payment = 0F;
                dgvCmb.Items.Add(dt.Rows[i]["Sale_Do_No"].ToString() + " (" + dt.Rows[i]["Fiscal_Year"].ToString() + ")");
                dt2 = c.runQuery("SELECT Sales_Voucher_ID, SUM(Payment_Amount) as Total_Payment FROM Payments WHERE Sales_Voucher_ID = " + dt.Rows[i]["Voucher_ID"].ToString() + " GROUP BY Sales_Voucher_ID");
                if (dt2.Rows.Count > 0) total_payment = float.Parse(dt2.Rows[0]["Total_Payment"].ToString());
                do_dict[dt.Rows[i]["Sale_Do_No"].ToString() + " (" + dt.Rows[i]["Fiscal_Year"].ToString() + ")"] = new Tuple<DataRow, float>(dt.Rows[i], total_payment);
            }
            return dt.Rows.Count;
        }

        private void loadDOButton_Click(object sender, EventArgs e)
        {

            int success = loadData();

            this.customerCB.Enabled = false;
            this.dataGridView1.Enabled = true;
            this.saveButton.Enabled = true;
            this.loadDOButton.Enabled = false;
            if(success > 0) c.SuccessBox("Loaded " + success.ToString() + " DOs");
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            if (e.RowIndex != dataGridView1.Rows.Count - 1)
            {
                dataGridView1.Rows[e.RowIndex].Cells["slNoCol"].Value = e.RowIndex + 1;
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
            if (dataGridView1.CurrentCell.ColumnIndex == 2) // Amount Received column
            {
                e.Control.PreviewKeyDown += new PreviewKeyDownEventHandler(Control_PreviewKeyDown);
            }
        }

        private void Control_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                int col = dataGridView1.CurrentCell.ColumnIndex;
                int row = dataGridView1.CurrentCell.RowIndex;

                if (col == 2) // Amount Received column
                {
                    dataGridView1.CurrentCell = dataGridView1.Rows[row + 1].Cells[1]; // DO No column of next row
                    e.IsInputKey = true;
                }
            }
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int col = dataGridView1.CurrentCell.ColumnIndex;
                int row = dataGridView1.CurrentCell.RowIndex;

                if (col == 1) // DO No column
                {
                    DataGridViewComboBoxCell cell = (DataGridViewComboBoxCell)dataGridView1.Rows[row].Cells[col];
                    cell.ReadOnly = false;
                    dataGridView1.BeginEdit(true);
                    ComboBox comboBox = (ComboBox)dataGridView1.EditingControl;
                    comboBox.DroppedDown = true;
                    e.Handled = true;
                }
            }
            else if (e.KeyCode == Keys.Tab)
            {
                int col = dataGridView1.CurrentCell.ColumnIndex;
                int row = dataGridView1.CurrentCell.RowIndex;

                if (col == 2) // Amount Received column
                {
                    dataGridView1.CurrentCell = dataGridView1.Rows[row + 1].Cells[1]; // DO No column of next row
                    e.Handled = true;
                }
            }
        }

        private void dataGridView1_CurrentCellDirtyStateChanged_1(object sender, EventArgs e)
        {
            if (dataGridView1.IsCurrentCellDirty)
            {
                dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1 && e.RowIndex >= 0)
            {
                string do_;
                float total_amount, total_payment;
                DataRow dr;

                do_ = dataGridView1.Rows[e.RowIndex].Cells["doNoCol"].Value.ToString();
                dr = do_dict[do_].Item1;
                total_payment = do_dict[do_].Item2;
                total_amount = float.Parse(dr["Sale_Rate"].ToString()) * float.Parse(dr["Net_Weight"].ToString());

                dataGridView1.Rows[e.RowIndex].Cells["amountReceivedCol"].Value = (total_amount - total_payment).ToString("F2");
                dataGridView1.Rows[e.RowIndex].Cells["amountPendingCol"].Value = (total_amount - total_payment).ToString("F2");
                dataGridView1.Rows[e.RowIndex].Cells["totalAmountCol"].Value = total_amount;
            }
            else if(e.ColumnIndex == 2 && e.RowIndex >= 0)
            {
                try
                {
                    if(!this.populating_dgv_edit_state)
                    {
                        string do_;
                        float total_amount, total_payment, amountReceived = 0F;
                        DataRow dr;

                        do_ = dataGridView1.Rows[e.RowIndex].Cells["doNoCol"].Value.ToString();
                        dr = do_dict[do_].Item1;
                        total_payment = do_dict[do_].Item2;
                        if (amount_received_this_voucher.ContainsKey(dataGridView1.Rows[e.RowIndex].Cells["doNoCol"].Value.ToString())) total_payment -= amount_received_this_voucher[dataGridView1.Rows[e.RowIndex].Cells["doNoCol"].Value.ToString()];
                        total_amount = float.Parse(dr["Sale_Rate"].ToString()) * float.Parse(dr["Net_Weight"].ToString());
                        try
                        {
                            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null)
                            {
                                dataGridView1.Rows[e.RowIndex].Cells["amountPendingCol"].Value = (total_amount - total_payment).ToString("F2");
                                return;
                            }
                            amountReceived = float.Parse(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                        }
                        catch
                        {
                            return;
                        }

                       if (Math.Round(total_amount - total_payment - amountReceived) < 0F)
                        {
                            c.ErrorBox("Amount Pending for DO No. " + dataGridView1.Rows[e.RowIndex].Cells["doNoCol"].Value.ToString() + " at row " + (e.RowIndex + 1).ToString() + " cannot be less than 0 (" + (total_amount - total_payment - amountReceived).ToString() + ")", "Negative Amount Pending Error");
                            dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "";
                            dataGridView1.Rows[e.RowIndex].Cells["amountPendingCol"].Value = (total_amount - total_payment).ToString("F2");
                            return;
                        }

                     dataGridView1.Rows[e.RowIndex].Cells["amountPendingCol"].Value = (total_amount - total_payment - amountReceived).ToString("F2");
                    }
                    amountTB.Text = CellSum().ToString("F2");
                }
                catch
                {
                    c.ErrorBox("The value 'Amount Received' should be a number");
                    string do_;
                    float total_amount, total_payment;
                    DataRow dr;
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "";

                    do_ = dataGridView1.Rows[e.RowIndex].Cells["doNoCol"].Value.ToString();
                    dr = do_dict[do_].Item1;
                    total_payment = do_dict[do_].Item2;
                    total_amount = float.Parse(dr["Sale_Rate"].ToString()) * float.Parse(dr["Net_Weight"].ToString());

                    dataGridView1.Rows[e.RowIndex].Cells["amountPendingCol"].Value = (total_amount - total_payment).ToString("F2");
                    return;
                }
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (c.check_login_val() == false) return;
            if (dataGridView1.Rows[0].Cells[1].Value == null)
            {
                c.ErrorBox("Please enter Payment Details", "Error");
                return;
            }
            if ((inputDateDTP.Value.Date < paymentDateDTP.Value.Date && this.edit_form == false) || paymentDateDTP.Value.Date > DateTime.Now.Date)
            {
                c.ErrorBox("Payment Date is in the future", "Error");
                return;
            }
            List<string> temp = new List<string>();
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                if (!c.Cell_Not_NullOrEmpty(this.dataGridView1, i, -1, "doNoCol"))
                {
                    continue;
                }
                else
                {
                    temp.Add(dataGridView1.Rows[i].Cells["doNoCol"].Value.ToString());

                    var distinctBytes = new HashSet<string>(temp);
                    bool allDifferent = distinctBytes.Count == temp.Count;
                    if (allDifferent == false)
                    {
                        c.ErrorBox("Repeated DO No. at Row: " + (i + 1).ToString(), "Error");
                        return;
                    }

                }
            }

            if(this.edit_form == false)
            {
                string sql = "begin transaction; begin try; DECLARE @voucherID int;\n";
                sql += "INSERT INTO Payments_Voucher (Payment_Date,Input_Date, Customers, Narration) VALUES ('" + inputDateDTP.Value.Date.ToString("MM-dd-yyyy").Substring(0, 10) + "' ,'" + paymentDateDTP.Value.Date.ToString("MM-dd-yyyy").Substring(0, 10) + "', '" + customerCB.SelectedItem.ToString() + "', '" + narrationTB.Text + "'); SELECT @voucherID = SCOPE_IDENTITY();\n";
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    // Check if amount pending is 0 for the DOs that are being closed
                    float amount_pending = float.Parse(dataGridView1.Rows[i].Cells["amountPendingCol"].Value.ToString());
                    if (amount_pending != 0F && (bool)dataGridView1.Rows[i].Cells["doPaymentClosedCol"].Value == true)
                    {
                        c.ErrorBox("Amount Pending for DO No. " + dataGridView1.Rows[i].Cells["doNoCol"].Value.ToString() + " at row " + (i + 1).ToString() + " should be 0", "DO Close Error");
                        return;
                    }
                    string comments = "";
                    if (c.Cell_Not_NullOrEmpty(dataGridView1, i, -1, "commentsCol")) comments = dataGridView1.Rows[i].Cells["commentsCol"].Value.ToString();
                    sql += "INSERT INTO Payments (Payment_Voucher_ID, Sales_Voucher_ID, Payment_Amount, Comments, Display_Order) VALUES (@voucherID, " + do_dict[dataGridView1.Rows[i].Cells["doNoCol"].Value.ToString()].Item1["Voucher_ID"].ToString() + "," + dataGridView1.Rows[i].Cells["amountReceivedCol"].Value.ToString() + ", '" + comments + "', " + i.ToString() + ");\n";
                    // Check and Set DO Closed State
                    if ((bool)dataGridView1.Rows[i].Cells["doPaymentClosedCol"].Value == true) sql += "UPDATE Sales_Voucher SET DO_Payment_Closed = 1 WHERE Voucher_ID = " + do_dict[dataGridView1.Rows[i].Cells["doNoCol"].Value.ToString()].Item1["Voucher_ID"].ToString() + ";\n";
                }
                //catch
                sql += "commit transaction; end try BEGIN CATCH rollback transaction; \n";
                sql += "DECLARE @ErrorMessage NVARCHAR(4000); DECLARE @ErrorSeverity INT; DECLARE @ErrorState INT; SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE(); \n";
                sql += "RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState); END CATCH; \n";
                DataTable add = c.runQuery(sql);

                if (add != null)
                {
                    dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.LawnGreen;
                    c.SuccessBox("Voucher Added Successfully");
                    disable_form_edit();
                }
                else return;
            }
            else
            {
                string sql = "begin transaction; begin try;\n";
                sql += "UPDATE Payments_Voucher SET Payment_Date = '" + paymentDateDTP.Value.Date.ToString("MM-dd-yyyy").Substring(0, 10) + "', Narration = '" + narrationTB.Text + "' WHERE Voucher_ID = " + this.voucher_id.ToString() + ";\n";
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    // Check if amount pending is 0 for the DOs that are being closed
                    float amount_pending = float.Parse(dataGridView1.Rows[i].Cells["amountPendingCol"].Value.ToString());
                    if (dataGridView1.Rows[i].Cells["doPaymentClosedCol"].Value == null) dataGridView1.Rows[i].Cells["doPaymentClosedCol"].Value = false;
                    if (amount_pending != 0F && (bool)dataGridView1.Rows[i].Cells["doPaymentClosedCol"].Value == true)
                    {
                        c.ErrorBox("Amount Pending for DO No. " + dataGridView1.Rows[i].Cells["doNoCol"].Value.ToString() + " at row " + (i + 1).ToString() + " should be 0", "DO Close Error");
                        return;
                    }
                    string comments = "";
                    if (c.Cell_Not_NullOrEmpty(dataGridView1, i, -1, "commentsCol")) comments = dataGridView1.Rows[i].Cells["commentsCol"].Value.ToString();
                    if (do_set_edit.Contains(dataGridView1.Rows[i].Cells["doNoCol"].Value.ToString()))   //Update the DO
                    {
                        sql += "UPDATE Payments SET Payment_Amount = " + dataGridView1.Rows[i].Cells["amountReceivedCol"].Value.ToString() + ", Comments = '" + comments + "', Display_Order = " + i.ToString() + " WHERE Sales_Voucher_ID = " + do_dict[dataGridView1.Rows[i].Cells["doNoCol"].Value.ToString()].Item1["Voucher_ID"].ToString() + " AND Payment_Voucher_ID = " + this.voucher_id.ToString() + "\n";
                        if ((bool)dataGridView1.Rows[i].Cells["doPaymentClosedCol"].Value == true) sql += "UPDATE Sales_Voucher SET DO_Payment_Closed = 1 WHERE Voucher_ID = " + do_dict[dataGridView1.Rows[i].Cells["doNoCol"].Value.ToString()].Item1["Voucher_ID"].ToString() + ";\n";
                        else sql += "UPDATE Sales_Voucher SET DO_Payment_Closed = 0 WHERE Voucher_ID = " + do_dict[dataGridView1.Rows[i].Cells["doNoCol"].Value.ToString()].Item1["Voucher_ID"].ToString() + ";\n";
                        do_to_delete.Remove(dataGridView1.Rows[i].Cells["doNoCol"].Value.ToString());
                    }
                    else    //Add a new DO
                    {
                        sql += "INSERT INTO Payments (Payment_Voucher_ID, Sales_Voucher_ID, Payment_Amount, Comments, Display_Order) VALUES (" + this.voucher_id.ToString() + ", " + do_dict[dataGridView1.Rows[i].Cells["doNoCol"].Value.ToString()].Item1["Voucher_ID"].ToString() + "," + dataGridView1.Rows[i].Cells["amountReceivedCol"].Value.ToString() + ", '" + comments + "', " + i.ToString() + ");\n";
                        // Check and Set DO Closed State
                        if ((bool)dataGridView1.Rows[i].Cells["doPaymentClosedCol"].Value == true) sql += "UPDATE Sales_Voucher SET DO_Payment_Closed = 1 WHERE Voucher_ID = " + do_dict[dataGridView1.Rows[i].Cells["doNoCol"].Value.ToString()].Item1["Voucher_ID"].ToString() + ";\n";
                        do_to_delete.Remove(dataGridView1.Rows[i].Cells["doNoCol"].Value.ToString());
                    }
                }
                for(int i = 0; i < do_to_delete.Count; i++) //Deleted DOs
                {
                    sql += "DELETE FROM Payments WHERE Sales_Voucher_ID = '" + do_dict[do_to_delete[i].ToString()].Item1["Voucher_ID"].ToString() + "' AND Payment_Voucher_ID = " + this.voucher_id.ToString() + "\n";
                    sql += "UPDATE Sales_Voucher SET DO_Payment_Closed = 0 WHERE Voucher_ID = " + do_dict[do_to_delete[i].ToString()].Item1["Voucher_ID"].ToString() + ";\n";
                }
                
                //catch
                sql += "commit transaction; end try BEGIN CATCH rollback transaction; \n";
                sql += "DECLARE @ErrorMessage NVARCHAR(4000); DECLARE @ErrorSeverity INT; DECLARE @ErrorState INT; SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE(); \n";
                sql += "RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState); END CATCH; \n";
                DataTable edited = c.runQuery(sql);
                if (edited != null)
                {
                    c.SuccessBox("Voucher Edited Successfully");
                    disable_form_edit();
                    dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.LawnGreen;
                    this.v1_history.loadData();
                }
                else return;
            }
            
            dataGridView1.EnableHeadersVisualStyles = false;
        }

        private void disable_form_edit()
        {
            this.paymentDateDTP.Enabled = false;
            this.customerCB.Enabled = false;
            this.loadDOButton.Enabled = false;
            this.saveButton.Enabled = false;
            this.deleteButton.Enabled = false;
            this.dataGridView1.Enabled = false;
            this.narrationTB.ReadOnly = true;
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
            this.amountTB.Text = CellSum().ToString("F3");
        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            // Set the default value for the DataGridViewCheckBoxCell
            dataGridView1.Rows[e.RowIndex].Cells["doPaymentClosedCol"].Value = false;
        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                dataGridView1.Rows[e.RowIndex].Selected = true;
                contextMenuStrip1.Show(Cursor.Position);
            }
        }
    }
}
