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
        //private int selectedRowIndex = -1;
        public editCone()
        {
            InitializeComponent();
            this.c = new DbConnect();
        }

        

        public void loadDatabase()
        {
            DataTable d = c.getQC('n');
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
                    c.deleteQC(userDataView.Rows[row].Cells[0].Value.ToString(),'n');
                }
                else
                {
                    try
                    {
                        int.Parse(this.editedQualityTextbox.Text);
                    }
                    catch
                    {
                        MessageBox.Show("Cone weight should be numerical","Error");
                        return;
                    }
                    c.editQC(editedQualityTextbox.Text, userDataView.Rows[row].Cells[0].Value.ToString(), 'n');
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
            try
            {
                int.Parse(this.newQualityTextbox.Text);
            }
            catch
            {
                MessageBox.Show("Cone weight should be numerical", "Error");
                return;
            }
            c.addQC(newQualityTextbox.Text, 'n');
            this.newQualityTextbox.Text = "";
            loadDatabase();

        }

        private void userLabel_Click(object sender, EventArgs e)
        {

        }
    }
}
