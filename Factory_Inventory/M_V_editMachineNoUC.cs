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
    public partial class M_V_editMachineNoUC : UserControl
    {
        private DbConnect c;
        public string currentUser;
        public M_V_editMachineNoUC()
        {
            InitializeComponent();
            this.c = new DbConnect();
        }

        public void loadDatabase()
        {
            DataTable d = c.getTableData("Machine_No", "*", "");
            dataGridView1.DataSource = d;
            c.hideallDGVcols(dataGridView1);
            dataGridView1.Columns["Machine_No"].Visible = true;
        }
        
        //clicks
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
                    string id = dataGridView1.Rows[row].Cells["Machine_No_ID"].Value.ToString();
                    c.runQuery("Delete from Machine_No where Machine_No_ID="+id);
                }
                else
                {
                    if (string.IsNullOrEmpty(editedQualityTextbox.Text)==true)
                    {
                        c.ErrorBox("Please enter Machine Number", "Error");
                        return;
                    }
                    string id = dataGridView1.Rows[row].Cells["Machine_No_ID"].Value.ToString();
                    c.runQuery("Update Machine_No set Machine_No='"+editedQualityTextbox.Text+"' where Machine_No_ID="+id);
                }
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

            if (string.IsNullOrEmpty(newQualityTextbox.Text)==true)
            {
                c.ErrorBox("Please enter Machine Number", "Error");
                return;
            }
            c.runQuery("Insert into Machine_No Values('" + newQualityTextbox.Text + "')");
            this.newQualityTextbox.Text = "";
            loadDatabase();
        }

        private void userDataView_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count <= 0) return;
            int RowIndex = dataGridView1.SelectedRows[0].Index;
            if (RowIndex >= 0)
            {
                editedQualityTextbox.Text = dataGridView1.Rows[RowIndex].Cells["Machine_No"].Value.ToString();
            }
        }
    }
}
