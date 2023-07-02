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
        public M_V3_paymentsForm()
        {
            InitializeComponent();
            DataTable dt = new DataTable();
            List<string> customers = new List<string>();

            // Read Customer Names from the database and store them in the combobox
            dt = c.runQuery("SELECT Customers FROM Customers");
            for (int i = 0; i < dt.Rows.Count; i++) customers.Add(dt.Rows[i]["Customers"].ToString());
            this.cusotmerCB.DataSource = customers;
            this.cusotmerCB.DisplayMember = "Customers";
            this.cusotmerCB.DropDownStyle = ComboBoxStyle.DropDown;//Create a drop-down list
            this.cusotmerCB.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.cusotmerCB.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            c.set_dgv_column_sort_state(this.dataGridView1, DataGridViewColumnSortMode.NotSortable);

        }

        private float CellSum()
        {
            float receivedAmountSum = 0F;
            for (int i = 0; i < dataGridView1.Rows.Count-1; i++)
            {
                try
                {
                    receivedAmountSum += float.Parse(dataGridView1.Rows[i].Cells["amountReceivedCol"].Value.ToString());
                }
                catch { }
            }
            return receivedAmountSum;
        }

        private void M_V3_paymentsForm_Load(object sender, EventArgs e)
        {
            this.Text = "Payments Voucher";
            dataGridView1.Rows[0].Cells["doPaymentClosedCol"].Value = false;
        }

        private void loadDOButton_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            DataGridViewComboBoxColumn dgvCmb = (DataGridViewComboBoxColumn)dataGridView1.Columns["doNoCol"];

            dt = c.runQuery("SELECT Voucher_ID, Sale_DO_No, Sale_Rate, Fiscal_Year, Net_Weight FROM Sales_Voucher WHERE DO_Payment_Closed = 0 AND Customer = '" + this.cusotmerCB.SelectedItem.ToString() + "'");

            if (dt.Rows.Count == 0)
            {
                c.WarningBox("No Pending DOs exist for " + this.cusotmerCB.SelectedItem.ToString());
                return;
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


            this.cusotmerCB.Enabled = false;
            this.dataGridView1.Enabled = true;
            this.saveButton.Enabled = true;
            c.SuccessBox("Loaded " + dt.Rows.Count.ToString() + " DOs");
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

                dataGridView1.Rows[e.RowIndex].Cells["amountPendingCol"].Value = (total_amount - total_payment).ToString("F2");
                dataGridView1.Rows[e.RowIndex].Cells["totalAmountCol"].Value = total_amount;
            }
            else if(e.ColumnIndex == 2 && e.RowIndex >= 0)
            {
                try
                {
                    string do_;
                    float total_amount, amountReceived;
                    DataRow dr;

                    do_ = dataGridView1.Rows[e.RowIndex].Cells["doNoCol"].Value.ToString();
                    dr = do_dict[do_].Item1;
                    total_amount = float.Parse(dr["Sale_Rate"].ToString()) * float.Parse(dr["Net_Weight"].ToString());
                    amountReceived = float.Parse(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());

                    if (total_amount - amountReceived < 0F)
                    {
                        c.ErrorBox("Amount Pending for DO No. " + dataGridView1.Rows[e.RowIndex].Cells["doNoCol"].Value.ToString() + " at row " + (e.RowIndex + 1).ToString() + " cannot be less than 0 (" + (total_amount - amountReceived).ToString() + ")", "Negative Amound Pending Error");
                        return;
                    }

                    dataGridView1.Rows[e.RowIndex].Cells["amountPendingCol"].Value = (total_amount - amountReceived).ToString("F2");

                    amountTB.Text = CellSum().ToString("F2");
                }
                catch
                {
                    c.ErrorBox("The value 'Amount Received' should be a number");
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
            if (inputDateDTP.Value.Date < paymentDateDTP.Value.Date)
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
                        c.ErrorBox("Please Enter DO No. at Row: " + (i + 1).ToString(), "Error");
                        return;
                    }

                }
            }

            string sql = "begin transaction; begin try; DECLARE @voucherID int;\n";
            sql += "INSERT INTO Payments_Voucher (Payment_Date,Input_Date, Customers, Narration) VALUES ('" + inputDateDTP.Value.Date.ToString("MM-dd-yyyy").Substring(0, 10) + "' ,'" + paymentDateDTP.Value.Date.ToString("MM-dd-yyyy").Substring(0, 10) + "', '" + cusotmerCB.SelectedItem.ToString() + "', '" + narrationTB.Text + "'); SELECT @voucherID = SCOPE_IDENTITY();\n";
            for (int i = 0; i < dataGridView1.Rows.Count-1; i++)
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
                sql += "INSERT INTO Payments (Payment_Voucher_ID, Sales_Voucher_ID, Payment_Amount, Comments) VALUES (@voucherID, " + do_dict[dataGridView1.Rows[i].Cells["doNoCol"].Value.ToString()].Item1["Voucher_ID"].ToString() + "," + dataGridView1.Rows[i].Cells["amountReceivedCol"].Value.ToString() + ", '" + comments + "');\n";
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

        private void disable_form_edit()
        {
            this.paymentDateDTP.Enabled = false;
            this.cusotmerCB.Enabled = false;
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
    }
}
