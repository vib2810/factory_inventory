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
        private int mode;  //1: QBT. 3: T_Company Name. 4: T_Colour 
        public M_V_editQBTUC(int mode)
        {
            InitializeComponent();
            this.c = new DbConnect();
            this.mode = mode;
            if(mode>1)
            {
                deleteUserCheckbox.Visible = false;
                editedQualityTextbox.ReadOnly = false;
            }
            if(mode == 3)
            {
                newPasswordLabel.Text = "Company Name";
                newConfirmPasswordLabel.Text = "Company Name";
                userLabel.Text = "Edit Company\nNames";
                label1.Text = "Add Company\nNames";
            }
            else if(mode==4)
            {
                newPasswordLabel.Text = "Colour";
                newConfirmPasswordLabel.Text = "Colour";
                userLabel.Text = "Edit Colours";
                label1.Text = "Add Colours";
            }
        }

        public void loadDatabase()
        {
            DataTable d = new DataTable();
            if (mode==1) d = c.getTableData("Quality_Before_Twist", "*", "");
            else if(mode==3) d = c.runQuery("SELECT * FROM T_M_Company_Names");
            else if(mode==4) d = c.runQuery("SELECT * FROM T_M_Colours");
            dataGridView1.DataSource = d;
            c.hideallDGVcols(dataGridView1);
            dataGridView1.Columns[1].Name = "Quality_Before_Twist";
            dataGridView1.Columns[0].Name = "Quality_Before_Twist_ID";
            dataGridView1.Columns["Quality_Before_Twist"].Visible = true;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 9.75F, FontStyle.Bold);


            if (mode == 1) dataGridView1.Columns["Quality_Before_Twist"].HeaderText = "Quality Before Twist";
            else if (mode == 3) dataGridView1.Columns["Quality_Before_Twist"].HeaderText = "Company Name";

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
                    if (mode==1)
                    {
                        DataTable d = c.runQuery("Select count(*) from quality where Quality_Before_Twist='" + quality_bt + "'");
                        if (int.Parse(d.Rows[0][0].ToString()) > 0)
                        {
                            c.ErrorBox("Cannot Delete Quality Before Twist as its present in Quality Table");
                            return;
                        }
                        c.runQuery("Delete from Quality_Before_Twist where Quality_Before_Twist_ID=" + id);
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(editedQualityTextbox.Text) == true)
                    {
                        c.ErrorBox("Please enter something", "Error");
                        return;
                    }
                    string id = dataGridView1.Rows[row].Cells["Quality_Before_Twist_ID"].Value.ToString();
                    if (mode==1) c.runQuery("Update Quality_Before_Twist set Quality_Before_Twist='" + editedQualityTextbox.Text + "' where Quality_Before_Twist_ID=" + id);
                    else if (mode == 3) c.runQuery("Update T_M_Company_Names set Company_Name='" + editedQualityTextbox.Text + "' where Company_ID=" + id);
                    else if (mode == 4) c.runQuery("Update T_M_Colours set Colour='" + editedQualityTextbox.Text + "' where Colour_ID=" + id);
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
                c.ErrorBox("Please enter something", "Error");
                return;
            }
            if (mode==1) c.runQuery("Insert into Quality_Before_Twist Values('" + newQualityTextbox.Text + "')");
            else if (mode == 3) c.runQuery("Insert into T_M_Company_Names Values('" + newQualityTextbox.Text + "')");
            else if (mode == 4) c.runQuery("Insert into T_M_Colours Values('" + newQualityTextbox.Text + "')");
            this.newQualityTextbox.Text = "";
            loadDatabase();
        }

        private void userDataView_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count <= 0) return;
            int RowIndex = dataGridView1.SelectedRows[0].Index;
            if (RowIndex >= 0)
            {
                editedQualityTextbox.Text = dataGridView1.Rows[RowIndex].Cells[1].Value.ToString();
            }
        }
    }
}