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
        public M_VC_paymentForm()
        {
            InitializeComponent();
            dataGridView1.Rows[0].Cells[5].Value = DateTime.Today.Date.ToString().Substring(0,10);
            dataGridView1.Rows[0].Cells[0].Value = 1;

            DataTable d = c.runQuery("SELECT Customers FROM Customers");
            List<string> datasource = new List<string>();
            for (int i = 0; i < d.Rows.Count; i++) datasource.Add(d.Rows[i][0].ToString());
            customerCB.DataSource = datasource;
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

        private void button1_Click(object sender, EventArgs e)
        {
            this.customer = customerCB.SelectedItem.ToString();
            DataTable d = c.runQuery("SELECT Voucher_ID, Sale_Rate, Fiscal_Year, Sale_DO_No, Net_Weight FROM Sales_Voucher WHERE Payment_Closed = 0 AND Customer = '" + this.customer + "'");
            DataGridViewComboBoxColumn do_no_col = (DataGridViewComboBoxColumn)this.dataGridView1.Columns["dono"];
            for (int i = 0; i < d.Rows.Count; i++) do_no_col.Items.Add(d.Rows[i]["Sale_DO_No"].ToString());

            if (d.Rows.Count == 0) c.WarningBox("No pending DOs found");
            else
            {
                c.SuccessBox("Loaded " + d.Rows.Count.ToString() + " pending DOs");
                customerCB.Enabled = false;
                dataGridView1.ReadOnly = false;
                loadDataButton.Enabled = false;
            }
        }
    }
}
