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
    public partial class editQuality : UserControl
    {
        private DbConnect c;
        public string currentUser;
        //private int selectedRowIndex = -1;
        public editQuality()
        {
            InitializeComponent();
            this.c = new DbConnect();
        }

        

        public void loadDatabase()
        {
            DataTable d = c.getQC('q');
            userDataView.DataSource = d;
        }


        private void confirmButton_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Confirm Changes?", "Message", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                int row = userDataView.SelectedRows[0].Index;
                if (deleteUserCheckbox.Checked == true)
                {
                    c.deleteQuality(userDataView.Rows[row].Cells[0].Value.ToString());
                }
                else
                {
                    if(editedQualityTextbox.Text=="" || editHSNNoTextbox.Text=="")
                    {
                        MessageBox.Show("Enter all values", "Error");
                        return;
                    }
                    c.editQuality(editedQualityTextbox.Text, editHSNNoTextbox.Text, userDataView.Rows[row].Cells[0].Value.ToString());
                }
                //this.selectedRowIndex = -1;
                this.editedQualityTextbox.Text = "";
                editHSNNoTextbox.Text = "";
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
                editHSNNoTextbox.Text = userDataView.Rows[e.RowIndex].Cells[1].Value.ToString();
            }
        }

        private void addQualityButton_Click(object sender, EventArgs e)
        {
            if (newQualityTextbox.Text == "" || addHSNNoTextbox.Text == "")
            {
                MessageBox.Show("Enter all values", "Error");
                return;
            }
            c.addQuality(newQualityTextbox.Text, addHSNNoTextbox.Text);
            this.newQualityTextbox.Text = "";
            this.addHSNNoTextbox.Text = "";
            loadDatabase();

        }
    }
}
