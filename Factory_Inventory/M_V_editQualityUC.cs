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
                    c.deleteQC(userDataView.Rows[row].Cells[0].Value.ToString(),'q');
                }
                else
                {
                    c.editQC(editedQualityTextbox.Text, userDataView.Rows[row].Cells[0].Value.ToString(), 'q');
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
                editedQualityTextbox.Text = userDataView.Rows[e.RowIndex].Cells[0].Value.ToString();
            }
        }

        private void addQualityButton_Click(object sender, EventArgs e)
        {

            c.addQC(newQualityTextbox.Text, 'q');
            this.newQualityTextbox.Text = "";
            loadDatabase();

        }
    }
}
