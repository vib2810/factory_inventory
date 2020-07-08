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
    public partial class A_1_editGroup : UserControl
    {
        private AttConnect a;
        public string currentUser;
        public A_1_editGroup()
        {
            InitializeComponent();
            this.a = new AttConnect();
        }

        public void loadDatabase()
        {
            DataTable d = a.getTableData("Group_Names", "*", "");
            dataGridView1.DataSource = d;
            a.hideallDGVcols(dataGridView1);
            dataGridView1.Columns["Group_Name"].Visible = true;
            dataGridView1.Columns["Group_Name"].HeaderText = "Group Name";
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
                    string group_name = dataGridView1.Rows[row].Cells["Group_Name"].Value.ToString();
                    string id = dataGridView1.Rows[row].Cells["Group_ID"].Value.ToString();
                    a.runQuery("Delete from Group_Names where Group_ID=" + id);
                }
                else
                {
                    if (string.IsNullOrEmpty(editedGroupTextbox.Text) == true)
                    {
                        a.ErrorBox("Please enter Group_Name", "Error");
                        return;
                    }
                    string id = dataGridView1.Rows[row].Cells["Group_ID"].Value.ToString();
                    a.runQuery("Update Group_Names set Group_Name='" + editedGroupTextbox.Text + "' where Group_ID=" + id);
                }
                this.editedGroupTextbox.Text = "";
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

            if (string.IsNullOrEmpty(newGroupTextbox.Text) == true)
            {
                a.ErrorBox("Please enter Group Name", "Error");
                return;
            }
            a.runQuery("Insert into Group_Names Values ('" + newGroupTextbox.Text + "')");
            this.newGroupTextbox.Text = "";
            loadDatabase();
        }

        private void userDataView_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count <= 0) return;
            int RowIndex = dataGridView1.SelectedRows[0].Index;
            if (RowIndex >= 0)
            {
                editedGroupTextbox.Text = dataGridView1.Rows[RowIndex].Cells["Group_Name"].Value.ToString();
            }
        }
    }
}