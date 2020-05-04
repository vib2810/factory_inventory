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
        public string edit_colour_code, add_colour_code;
        //private int selectedRowIndex = -1;
        public editQuality()
        {
            InitializeComponent();
            this.c = new DbConnect();
            this.edit_colour_code = "";
            this.add_colour_code = "";
        }

        

        public void loadDatabase()
        {
            DataTable d = c.getQC('q');
            dataGridView1.DataSource = d;
        }

        private void userDataView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex>=0)
            {
                editedQualityTextboxTB.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                editHSNNoTB.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                if(dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString()[0]>=97 && dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString()[0] <= 122)
                {
                    editPickColourTB.BackColor = System.Drawing.ColorTranslator.FromHtml("#"+dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString());
                }
                else
                {
                    editPickColourTB.BackColor = System.Drawing.ColorTranslator.FromHtml(dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString());
                }
                editQualityBeforeTwistTB.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            }
        }

        private void addpickcolourButton_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                this.addPickColourTB.BackColor = this.colorDialog1.Color;
                this.add_colour_code = this.colorDialog1.Color.Name;
            }
        }

        private void confirmButton_Click_1(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Confirm Changes?", "Message", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                int row = dataGridView1.SelectedRows[0].Index;
                if (deleteUserCheckboxCK.Checked == true)
                {
                    c.deleteQuality(dataGridView1.Rows[row].Cells[0].Value.ToString());
                }
                else
                {
                    if (editedQualityTextboxTB.Text == "" || editHSNNoTB.Text == "" || this.edit_colour_code == "" || editQualityBeforeTwistTB.Text == "")
                    {
                        MessageBox.Show("Enter all values", "Error");
                        return;
                    }
                    c.editQuality(editQualityBeforeTwistTB.Text, editHSNNoTB.Text, this.edit_colour_code, editedQualityTextboxTB.Text, dataGridView1.Rows[row].Cells[0].Value.ToString());
                }
                
                //this.selectedRowIndex = -1;
                this.editedQualityTextboxTB.Text = "";
                editHSNNoTB.Text = "";
                editPickColourTB.BackColor = Color.White;
                editQualityBeforeTwistTB.Text = "";
                this.deleteUserCheckboxCK.Checked = false;
                loadDatabase();
            }
            else if (dialogResult == DialogResult.No)
            {
            }
        }

        private void addQualityButton_Click_1(object sender, EventArgs e)
        {
            if (newQualityTextboxTB.Text == "" || addHSNNoTB.Text == "" || this.add_colour_code == "" || addQualityBeforeTwistTB.Text == "")
            {
                MessageBox.Show("Enter all values", "Error");
                return;
            }
            c.addQuality(addQualityBeforeTwistTB.Text, addHSNNoTB.Text, this.add_colour_code, this.newQualityTextboxTB.Text); 
            this.newQualityTextboxTB.Text = "";
            this.addHSNNoTB.Text = "";
            this.addPickColourTB.BackColor = Color.White;
            this.addQualityBeforeTwistTB.Text = "";
            loadDatabase();
        }

        private void editpickcolourButton_Click(object sender, EventArgs e)
        {
            if(colorDialog1.ShowDialog()==DialogResult.OK)
            {
                this.editPickColourTB.BackColor = this.colorDialog1.Color;
                this.edit_colour_code = this.colorDialog1.Color.Name;
            }
        }
    }
}
