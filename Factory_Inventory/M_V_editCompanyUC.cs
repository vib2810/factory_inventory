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
    public partial class editCompany : UserControl
    {
        private DbConnect c;
        public string currentUser;
        public editCompany()
        {
            InitializeComponent();
            this.c = new DbConnect();
        }
        public void loadDatabase()
        {
            DataTable d = c.getQC('c');
            dataGridView1.DataSource = d;
        }
        private void confirmButton_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count <= 0) return;
            DialogResult dialogResult = MessageBox.Show("Confirm Changes?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                int row = dataGridView1.SelectedRows[0].Index;
                if (deleteUserCheckbox.Checked == true)
                {
                    c.deleteQC(dataGridView1.Rows[row].Cells[0].Value.ToString(),'c');
                }
                else
                {
                    c.editQC(editedQualityTextbox.Text, dataGridView1.Rows[row].Cells[0].Value.ToString(), 'c');
                }
                //this.selectedRowIndex = -1;
                this.editedQualityTextbox.Text = "";
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
            }
        }
        private void addQualityButton_Click(object sender, EventArgs e)
        {

            c.addQC(newQualityTextbox.Text, 'c');
            this.newQualityTextbox.Text = "";
            loadDatabase();

        }
        private void userLabel_Click(object sender, EventArgs e)
        {

        }
    }
}
