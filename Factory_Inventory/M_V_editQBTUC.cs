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
    public partial class M_V_editQBTUC : UserControl
    {
        private DbConnect c;
        public string currentUser;
        public M_V_editQBTUC()
        {
            InitializeComponent();
            this.c = new DbConnect();
        }

        public void loadDatabase()
        {
            DataTable d = c.getTableData("Quality_Before_Twist", "*", "");
            dataGridView1.DataSource = d;
            c.hideallDGVcols(dataGridView1);
            dataGridView1.Columns["Quality_Before_Twist"].Visible = true;
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
                    string quality_bt = dataGridView1.Rows[row].Cells["Quality_Before_Twist"].Value.ToString();
                    string id = dataGridView1.Rows[row].Cells["Quality_Before_Twist_ID"].Value.ToString();
                    DataTable d=c.runQuery("Select count(*) from quality where Quality_Before_Twist='" + quality_bt + "'");
                    if(int.Parse(d.Rows[0][0].ToString())>0)
                    {
                        c.ErrorBox("Cannot Delete Quality Before Twist as its present in Quality Table");
                        return;
                    }
                    c.runQuery("Delete from Quality_Before_Twist where Quality_Before_Twist_ID=" + id);
                }
                else
                {
                    if (string.IsNullOrEmpty(editedQualityTextbox.Text) == true)
                    {
                        c.ErrorBox("Please enter Quality", "Error");
                        return;
                    }
                    string id = dataGridView1.Rows[row].Cells["Quality_Before_Twist_ID"].Value.ToString();
                    c.runQuery("Update Quality_Before_Twist set Quality_Before_Twist='" + editedQualityTextbox.Text + "' where Quality_Before_Twist_ID=" + id);
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

            if (string.IsNullOrEmpty(newQualityTextbox.Text) == true)
            {
                c.ErrorBox("Please enter Quality", "Error");
                return;
            }
            c.runQuery("Insert into Quality_Before_Twist Values('" + newQualityTextbox.Text + "')");
            this.newQualityTextbox.Text = "";
            loadDatabase();
        }

        private void userDataView_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count <= 0) return;
            int RowIndex = dataGridView1.SelectedRows[0].Index;
            if (RowIndex >= 0)
            {
                editedQualityTextbox.Text = dataGridView1.Rows[RowIndex].Cells["Quality_Before_Twist"].Value.ToString();
            }
        }
    }
}