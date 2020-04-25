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
    public partial class editColour : UserControl
    {
        private DbConnect c;
        public string currentUser;
        //private int selectedRowIndex = -1;
        public editColour()
        {
            InitializeComponent();
            this.c = new DbConnect();

            //Create drop-down lists
            var dataSource1 = new List<string>();
            DataTable d1 = c.getQC('q');
            dataSource1.Add("---Select---");

            for (int i = 0; i < d1.Rows.Count; i++)
            {
                dataSource1.Add(d1.Rows[i][0].ToString());
            }
            this.addQualityCombobox.DataSource = dataSource1;
            this.addQualityCombobox.DisplayMember = "Quality";
            this.addQualityCombobox.DropDownStyle = ComboBoxStyle.DropDown;//Create a drop-down list
            this.addQualityCombobox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.addQualityCombobox.AutoCompleteSource = AutoCompleteSource.ListItems;
            var dataSource2 = new List<string>(dataSource1);
            this.editQualityCombobox.DataSource = dataSource2;
            this.editQualityCombobox.DisplayMember = "Quality";
            this.editQualityCombobox.DropDownStyle = ComboBoxStyle.DropDown;//Create a drop-down list
            this.editQualityCombobox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.editQualityCombobox.AutoCompleteSource = AutoCompleteSource.ListItems;
        }

        

        public void loadDatabase()
        {
            DataTable d = c.getQC('l');
            userDataView.DataSource = d;
            //Create drop-down lists
            var dataSource1 = new List<string>();
            DataTable d1 = c.getQC('q');
            dataSource1.Add("---Select---");

            for (int i = 0; i < d1.Rows.Count; i++)
            {
                dataSource1.Add(d1.Rows[i][0].ToString());
            }
            this.addQualityCombobox.DataSource = dataSource1;
            this.addQualityCombobox.DisplayMember = "Quality";
            this.addQualityCombobox.DropDownStyle = ComboBoxStyle.DropDown;//Create a drop-down list
            this.addQualityCombobox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.addQualityCombobox.AutoCompleteSource = AutoCompleteSource.ListItems;
            var dataSource2 = new List<string>(dataSource1);
            this.editQualityCombobox.DataSource = dataSource2;
            this.editQualityCombobox.DisplayMember = "Quality";
            this.editQualityCombobox.DropDownStyle = ComboBoxStyle.DropDown;//Create a drop-down list
            this.editQualityCombobox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.editQualityCombobox.AutoCompleteSource = AutoCompleteSource.ListItems;
        }


        private void confirmButton_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Confirm Changes?", "Message", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                int row = userDataView.SelectedRows[0].Index;
                if (deleteUserCheckbox.Checked == true)
                {
                    c.deleteColour(userDataView.Rows[row].Cells[0].Value.ToString(), userDataView.Rows[row].Cells[1].Value.ToString());
                }
                else
                {
                    if (editedQualityTextbox.Text == null || editedQualityTextbox.Text == "")
                    {
                        MessageBox.Show("Please enter colour", "Error");
                        return;
                    }
                    if (editQualityCombobox.SelectedIndex==0)
                    {
                        MessageBox.Show("Please select quality", "Error");
                        return;
                    }
                    try
                    {
                        float.Parse(editDyeingRateTexbox.Text);
                        c.editColour(editedQualityTextbox.Text, userDataView.Rows[row].Cells[0].Value.ToString(), float.Parse(editDyeingRateTexbox.Text), editQualityCombobox.SelectedItem.ToString(), userDataView.Rows[row].Cells[1].Value.ToString());
                    }
                    catch
                    {
                        MessageBox.Show("Please enter numeric dyeing rate value only", "Error");
                        editDyeingRateTexbox.Text = userDataView.Rows[row].Cells[2].Value.ToString();
                        return;
                    }
                }
                //this.selectedRowIndex = -1;
                this.deleteUserCheckbox.Checked = false;
                this.editedQualityTextbox.Text = "";
                this.editDyeingRateTexbox.Text = "";
                this.editQualityCombobox.SelectedIndex = 0;

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
            if(e.RowIndex>=0 && e.ColumnIndex>=0)
            {
                editedQualityTextbox.Text = userDataView.Rows[e.RowIndex].Cells[0].Value.ToString();
                editDyeingRateTexbox.Text = userDataView.Rows[e.RowIndex].Cells[2].Value.ToString();
                this.editQualityCombobox.SelectedIndex = this.editQualityCombobox.FindStringExact(userDataView.Rows[e.RowIndex].Cells[1].Value.ToString());
            }
        }

        private void addQualityButton_Click(object sender, EventArgs e)
        {
            if (c.isPresentInColour(newQualityTextbox.Text, addQualityCombobox.SelectedItem.ToString()) == true)
            {
                MessageBox.Show("Entry Already Present", "Error");
                return;
            }
            if (newQualityTextbox.Text == null || newQualityTextbox.Text == "")
            {
                MessageBox.Show("Please enter colour", "Error");
                return;
            }
            if (addQualityCombobox.SelectedIndex == 0)
            {
                MessageBox.Show("Please select quality", "Error");
                return;
            }
            try
            {
                float.Parse(addDyeingRateTexbox.Text);
                c.addColour(newQualityTextbox.Text, float.Parse(addDyeingRateTexbox.Text), addQualityCombobox.SelectedItem.ToString());
            }
            catch
            {
                MessageBox.Show("Enter numeric Dyeing Rate only", "Error");
                addDyeingRateTexbox.Text = "";
                return;
            }
            loadDatabase();

        }

        private void userLabel_Click(object sender, EventArgs e)
        {

        }
    }
}
