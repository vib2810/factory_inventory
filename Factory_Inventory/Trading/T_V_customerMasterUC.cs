using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Factory_Inventory.Factory_Classes;

namespace Factory_Inventory
{
    public partial class T_V_customerMasterUC : UserControl
    {
        private DbConnect c;
        public string currentUser;
        public T_V_customerMasterUC()
        {
            InitializeComponent();
            this.c = new DbConnect();
        }
        public void loadDatabase()
        {
            DataTable d = c.runQuery("SELECT * FROM T_M_Customers");
            dataGridView1.DataSource = d;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns["Customer_Name"].HeaderText = "Customer Name";
            dataGridView1.Columns["Customer_Address"].HeaderText = "Customer Address";
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 9.75F, FontStyle.Bold);
        }
        private void confirmButton_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count <= 0) return;
            int RowIndex = this.dataGridView1.SelectedRows[0].Index;

            DialogResult dialogResult = MessageBox.Show("Confirm Changes?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                int row = dataGridView1.SelectedRows[0].Index;
                if(editCustomerNameTB.Text=="" || editGSTINTB.Text=="" || editAddressTB.Text=="")
                {
                    c.ErrorBox("Enter all the values", "Error");
                    return;
                }
                c.runQuery("UPDATE T_M_Customers SET Customer_Name ='" + editCustomerNameTB.Text + "', GSTIN='" + editGSTINTB.Text + "', Customer_Address = '" + editAddressTB.Text + "' WHERE Customer_ID= " + dataGridView1.Rows[row].Cells[0].Value.ToString() + "");
                //this.selectedRowIndex = -1;
                this.editCustomerNameTB.Text = "";
                this.editGSTINTB.Text = "";
                this.editAddressTB.Text = "";
                loadDatabase();
                if (RowIndex >= 0 && RowIndex<=dataGridView1.Rows.Count-1)
                {
                    dataGridView1.Rows[RowIndex].Selected = true;
                }
            }
        }
        private void addQualityButton_Click(object sender, EventArgs e)
        {
            if (addCustomerTB.Text == "" || addGSTINTB.Text == "" || addAddressTB.Text == "")
            {
                c.ErrorBox("Enter all the values", "Error");
                return;
            }
            c.runQuery("INSERT INTO T_M_Customers VALUES ('" + addCustomerTB.Text + "', '" + addGSTINTB.Text + "', '" + addAddressTB.Text + "') ");
            this.addCustomerTB.Text = "";
            this.addGSTINTB.Text = "";
            this.addAddressTB.Text = "";
            loadDatabase();

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count <= 0) return;
            int RowIndex = dataGridView1.SelectedRows[0].Index;
            if (RowIndex >= 0)
            {
                editCustomerNameTB.Text = dataGridView1.Rows[RowIndex].Cells[1].Value.ToString();
                editGSTINTB.Text = dataGridView1.Rows[RowIndex].Cells[2].Value.ToString();
                editAddressTB.Text = dataGridView1.Rows[RowIndex].Cells[3].Value.ToString();
            }
        }
    }
}
