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
    public partial class T_V_coneMasterUC : UserControl
    {
        private DbConnect c;
        public string currentUser;
        public T_V_coneMasterUC()
        {
            InitializeComponent();
            this.c = new DbConnect();
        }
        public void loadDatabase()
        {
            DataTable d = c.runQuery("SELECT * FROM T_M_Cones");
            dataGridView1.DataSource = d;
            dataGridView1.Columns[0].Visible = false;
        }
        private void confirmButton_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count <= 0) return;
            int RowIndex = this.dataGridView1.SelectedRows[0].Index;

            DialogResult dialogResult = MessageBox.Show("Confirm Changes?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                int row = dataGridView1.SelectedRows[0].Index;
                try
                {
                    float.Parse(this.editedQualityTextbox.Text);
                }
                catch
                {
                    c.ErrorBox("Cone weight should be numerical", "Error");
                    return;
                }
                if(string.IsNullOrWhiteSpace(this.editConeNameTB.Text) || string.IsNullOrWhiteSpace(this.editedQualityTextbox.Text))
                {
                    c.ErrorBox("Please enter all the values", "Error");
                    return;
                }
                c.runQuery("UPDATE T_M_Cones SET Cone_Name = '" + editConeNameTB.Text + "', Cone_Weight = " + editedQualityTextbox.Text + "  WHERE Cone_ID = " + dataGridView1.Rows[row].Cells[0].Value.ToString() + "");
                //this.selectedRowIndex = -1;
                this.editedQualityTextbox.Text = "";
                loadDatabase();
                if (RowIndex >= 0 && RowIndex<=dataGridView1.Rows.Count-1)
                {
                    dataGridView1.Rows[RowIndex].Selected = true;
                }
            }
            
        }
        private void addQualityButton_Click(object sender, EventArgs e)
        {
            try
            {
                float.Parse(this.newQualityTextbox.Text);
            }
            catch
            {
                c.ErrorBox("Cone weight should be numerical", "Error");
                return;
            }
            if (string.IsNullOrWhiteSpace(this.editConeNameTB.Text) || string.IsNullOrWhiteSpace(this.editedQualityTextbox.Text))
            {
                c.ErrorBox("Please enter all the values", "Error");
                return;
            }
            c.runQuery("INSERT INTO T_M_Cones VALUES ('" + addConeNameTB.Text + "', " + newQualityTextbox.Text + ")");
            this.newQualityTextbox.Text = "";
            loadDatabase();

        }
        private void userLabel_Click(object sender, EventArgs e)
        {

        }
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count <= 0) return;
            int RowIndex = dataGridView1.SelectedRows[0].Index;
            if (RowIndex >= 0)
            {
                editConeNameTB.Text = dataGridView1.Rows[RowIndex].Cells[1].Value.ToString();
                editedQualityTextbox.Text = dataGridView1.Rows[RowIndex].Cells[2].Value.ToString();
            }
        }

    }
}
