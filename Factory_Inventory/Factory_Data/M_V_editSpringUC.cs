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
    public partial class editSpring : UserControl
    {
        private DbConnect c;
        public string currentUser;
        public editSpring()
        {
            InitializeComponent();
            this.c = new DbConnect();
        }
        public void loadDatabase()
        {
            DataTable d = c.getQC('s');
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
                    c.deleteQC(dataGridView1.Rows[row].Cells[0].Value.ToString(),'s');
                }
                else
                {
                    if (editedQualityTextbox.Text == null || editedQualityTextbox.Text == "")
                    {
                        c.ErrorBox("Please enter spring number", "Error");
                        return;
                    }
                    try
                    {
                        float.Parse(editSpringWeightTextbox.Text);
                        c.editSpring(editedQualityTextbox.Text, dataGridView1.Rows[row].Cells[0].Value.ToString(), float.Parse(editSpringWeightTextbox.Text), "Spring", "Spring_Weight");
                    }
                    catch
                    {
                        c.ErrorBox("Please enter numeric weight value only", "Error");
                        editSpringWeightTextbox.Text = dataGridView1.Rows[row].Cells[1].Value.ToString();
                        return;
                    }
                }
                //this.selectedRowIndex = -1;
                this.editedQualityTextbox.Text = "";
                this.editSpringWeightTextbox.Text = "";
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

            if (newQualityTextbox.Text == null || newQualityTextbox.Text == "")
            {
                c.ErrorBox("Please enter spring number", "Error");
                return;
            }
            try
            {
                float.Parse(addSpringWeightTextbox.Text);
                c.addSpring(newQualityTextbox.Text, float.Parse(addSpringWeightTextbox.Text), "Spring");
            }
            catch
            {
                c.ErrorBox("Please enter numeric weight value only", "Error");
                addSpringWeightTextbox.Text = "";
                return;
            }
            this.newQualityTextbox.Text = "";
            addSpringWeightTextbox.Text = "";
            loadDatabase();

        }
        private void userLabel_Click(object sender, EventArgs e)
        {

        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void userDataView_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count <= 0) return;
            int RowIndex = dataGridView1.SelectedRows[0].Index;
            if (RowIndex >= 0)
            {
                editedQualityTextbox.Text = dataGridView1.Rows[RowIndex].Cells[0].Value.ToString();
                editSpringWeightTextbox.Text = dataGridView1.Rows[RowIndex].Cells[1].Value.ToString();
            }
        }
    }
}
