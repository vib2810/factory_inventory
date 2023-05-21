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
                //msg.HWnd == dataGridView1.EditingControl.Handle &&
                dataGridView1.SelectedCells
                    .Cast<DataGridViewCell>()
                    .Any(x => x.ColumnIndex == 2))
            {
                Console.WriteLine("Inside process cmd key tab 2");
                this.edit_cmd_send = true;
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
        Dictionary<string, Tuple<DataRow,float>> do_dict = new Dictionary<string, Tuple<DataRow, float>>();
        private bool edit_cmd_send = false;
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

        private void M_V3_paymentsForm_Load(object sender, EventArgs e)
        {
            this.Text = "Payments Voucher";
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
                dt2 = c.runQuery("SELECT Sales_Voucher_ID, SUM(Payment_Amount) as Total_Payment FROM Payments WHERE Sales_Voucher_ID = " + dt.Rows[i]["Vooucher_ID"].ToString() + " GROUP BY Sales_Voucher_ID");
                if (dt2.Rows.Count > 0) total_payment = float.Parse(dt2.Rows[0]["Total_Payment"].ToString());
                do_dict[dt.Rows[i]["Sale_Do_No"].ToString() + " (" + dt.Rows[i]["Fiscal_Year"].ToString() + ")"] = new Tuple<DataRow, float>(dt.Rows[i], total_payment);
            }


            this.cusotmerCB.Enabled = false;
            this.dataGridView1.Enabled = true;
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
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (dataGridView1.Enabled == false || dataGridView1.ReadOnly == true) return;
            if (e.KeyCode == Keys.Tab &&
            (dataGridView1.SelectedCells.Cast<DataGridViewCell>().Any(x => x.ColumnIndex == 1) || this.edit_cmd_send == true))
            {
                bool edit_cmd_local = this.edit_cmd_send;
                this.edit_cmd_send = false;
                int rowindex_tab = dataGridView1.SelectedCells[0].RowIndex;
                //if (edit_cmd_local == true) rowindex_tab--;
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
                //if (edit_cmd_local == false)
                //{
                //    SendKeys.Send("{tab}");
                //}
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
                }
                SendKeys.Send("{tab}");
                SendKeys.Send("{tab}");
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

        private void dataGridView1_CurrentCellDirtyStateChanged_1(object sender, EventArgs e)
        {
            if (dataGridView1.IsCurrentCellDirty)
            {
                dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == 1)
            {
                string do_;
                float total_amount, total_payment;
                DataRow dr;
                
                do_ = dataGridView1.Rows[e.RowIndex].Cells["doNoCol"].Value.ToString();
                dr = do_dict[do_].Item1;
                total_payment = do_dict[do_].Item2;
                total_amount = float.Parse(dr["Sale_Rate"].ToString()) * float.Parse(dr["Net_Weight"].ToString());

                dataGridView1.Rows[e.RowIndex].Cells["amountPendingCol"].Value = total_amount - total_payment;
                dataGridView1.Rows[e.RowIndex].Cells["totalAmountCol"].Value = total_amount;


            }
        }
    }
}
