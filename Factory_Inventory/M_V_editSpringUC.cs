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
        //private int selectedRowIndex = -1;
        public editSpring()
        {
            InitializeComponent();
            this.c = new DbConnect();
        }
        public void loadDatabase()
        {
            DataTable d = c.getQC('s');
            userDataView.DataSource = d;
        }
        private void confirmButton_Click(object sender, EventArgs e)
        {
            if (userDataView.SelectedRows.Count <= 0) return;
            DialogResult dialogResult = MessageBox.Show("Confirm Changes?", "Message", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                int row = userDataView.SelectedRows[0].Index;
                if (deleteUserCheckbox.Checked == true)
                {
                    c.deleteQC(userDataView.Rows[row].Cells[0].Value.ToString(),'s');
                }
                else
                {
                    if (editedQualityTextbox.Text == null || editedQualityTextbox.Text == "")
                    {
                        MessageBox.Show("Please enter spring number", "Error");
                        return;
                    }
                    try
                    {
                        float.Parse(editSpringWeightTextbox.Text);
                        c.editSpring(editedQualityTextbox.Text, userDataView.Rows[row].Cells[0].Value.ToString(), float.Parse(editSpringWeightTextbox.Text), "Spring", "Spring_Weight");
                    }
                    catch
                    {
                        MessageBox.Show("Please enter numeric weight value only", "Error");
                        editSpringWeightTextbox.Text = userDataView.Rows[row].Cells[1].Value.ToString();
                        return;
                    }
                }
                //this.selectedRowIndex = -1;
                this.editedQualityTextbox.Text = "";
                this.editSpringWeightTextbox.Text = "";
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
                editedQualityTextbox.Text = userDataView.Rows[e.RowIndex].Cells[0].Value.ToString();
                editSpringWeightTextbox.Text = userDataView.Rows[e.RowIndex].Cells[1].Value.ToString();
            }
            
        }
        private void addQualityButton_Click(object sender, EventArgs e)
        {

            if (newQualityTextbox.Text == null || newQualityTextbox.Text == "")
            {
                MessageBox.Show("Please enter spring number", "Error");
                return;
            }
            try
            {
                float.Parse(addSpringWeightTextbox.Text);
                c.addSpring(newQualityTextbox.Text, float.Parse(addSpringWeightTextbox.Text), "Spring");
            }
            catch
            {
                MessageBox.Show("Please enter numeric weight value only", "Error");
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
    }
}
