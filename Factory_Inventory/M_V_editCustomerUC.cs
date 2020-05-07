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
    public partial class editCustomer : UserControl
    {
        private DbConnect c;
        public string currentUser;
        public editCustomer()
        {
            InitializeComponent();
            this.c = new DbConnect();
        }
        public void loadDatabase()
        {
            DataTable d = c.getQC('C');
            dataGridView1.DataSource = d;
        }
        private void confirmButton_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count <= 0) return;

            DialogResult dialogResult = MessageBox.Show("Confirm Changes?", "Message", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                int row = dataGridView1.SelectedRows[0].Index;
                if (deleteUserCheckbox.Checked == true)
                {
                    c.deleteCustomer(dataGridView1.Rows[row].Cells[0].Value.ToString(), "Customers");
                }
                else
                {
                    if(editedQualityTextbox.Text=="" || editGSTINTextbox.Text=="" || editAddressTextbox.Text=="")
                    {
                        MessageBox.Show("Enter all the values", "Error");
                        return;
                    }
                    c.editCustomer(editedQualityTextbox.Text, editGSTINTextbox.Text, editAddressTextbox.Text, dataGridView1.Rows[row].Cells[0].Value.ToString(), "Customers");
                }
                //this.selectedRowIndex = -1;
                this.editedQualityTextbox.Text = "";
                this.editGSTINTextbox.Text = "";
                this.editAddressTextbox.Text = "";
                this.deleteUserCheckbox.Checked = false;
                loadDatabase();
            }
            else if (dialogResult == DialogResult.No)
            {
            }
            
        }
        private void userDataView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void userDataView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex>=0)
            {
                editedQualityTextbox.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                editGSTINTextbox.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                editAddressTextbox.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            }
        }
        private void addQualityButton_Click(object sender, EventArgs e)
        {
            if (newQualityTextbox.Text == "" || addGSTINTextbox.Text == "" || addAddressTextbox.Text == "")
            {
                MessageBox.Show("Enter all the values", "Error");
                return;
            }
            c.addCustomer(newQualityTextbox.Text, addGSTINTextbox.Text, addAddressTextbox.Text, "Customers");
            this.newQualityTextbox.Text = "";
            this.addGSTINTextbox.Text = "";
            this.addAddressTextbox.Text = "";
            loadDatabase();

        }
        private void userLabel_Click(object sender, EventArgs e)
        {

        }
    }
}
