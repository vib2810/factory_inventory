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
    public partial class editCone : UserControl
    {
        private DbConnect c;
        public string currentUser;
        public editCone()
        {
            InitializeComponent();
            this.c = new DbConnect();
        }
        public void loadDatabase()
        {
            DataTable d = c.getQC('n');
            dataGridView1.DataSource = d;
        }
        private void confirmButton_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count <= 0) return;
            int RowIndex = this.dataGridView1.SelectedRows[0].Index;

            DialogResult dialogResult = MessageBox.Show("Confirm Changes?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                int row = dataGridView1.SelectedRows[0].Index;
                if (deleteUserCheckbox.Checked == true)
                {
                    c.deleteQC(dataGridView1.Rows[row].Cells[0].Value.ToString(),'n');
                }
                else
                {
                    try
                    {
                        int.Parse(this.editedQualityTextbox.Text);
                    }
                    catch
                    {
                        c.ErrorBox("Cone weight should be numerical", "Error");
                        return;
                    }
                    c.editQC(editedQualityTextbox.Text, dataGridView1.Rows[row].Cells[0].Value.ToString(), 'n');
                }
                //this.selectedRowIndex = -1;
                this.editedQualityTextbox.Text = "";
                this.deleteUserCheckbox.Checked = false;
                loadDatabase();
                if (RowIndex >= 0 && RowIndex<=dataGridView1.Rows.Count-1)
                {
                    dataGridView1.Rows[RowIndex].Selected = true;
                }
            }
            
        }
        private void addQualityButton_Click(object sender, EventArgs e)
        {
            try
            {
                int.Parse(this.newQualityTextbox.Text);
            }
            catch
            {
                c.ErrorBox("Cone weight should be numerical", "Error");
                return;
            }
            c.addQC(newQualityTextbox.Text, 'n');
            this.newQualityTextbox.Text = "";
            loadDatabase();

        }
        private void userLabel_Click(object sender, EventArgs e)
        {

        }
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count <= 0) return;
            int RowIndex = dataGridView1.SelectedRows[0].Index;
            if (RowIndex >= 0)
            {
                editedQualityTextbox.Text = dataGridView1.Rows[RowIndex].Cells[0].Value.ToString();
            }
        }
    }
}
