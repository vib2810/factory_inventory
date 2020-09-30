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
    public partial class M_S_defaultUC : UserControl
    {
        DbConnect c = new DbConnect();
        Dictionary<int, string> default_map = new Dictionary<int, string>();
        public string default_type = "NULL";
        public M_S_defaultUC()
        {
            InitializeComponent();
            panel1.BackColor = Color.Green;
        }
        public void load_data(string default_type)
        {
            this.default_type = default_type;
            this.label1.Text = this.default_type + " Settings";
            DataTable dt = c.runQuery("SELECT * FROM Defaults WHERE Default_Type = '" + this.default_type + "'");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dataGridView1.Rows.Add(dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString());
            }
        }
        private void allTB_TextChanged(object sender, EventArgs e)
        {
            panel1.BackColor = Color.Red;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            panel1.BackColor = Color.Green;
            for(int i=0;i<dataGridView1.Rows.Count;i++)
            {
                string name = dataGridView1.Rows[i].Cells["CName"].Value.ToString();
                string value = dataGridView1.Rows[i].Cells["Value"].Value.ToString();
                c.runQuery("UPDATE Defaults SET Default_Value = '" + value + "' WHERE Default_Name = '" + name + "' AND Default_Type = '" + this.default_type + "'");
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            panel1.BackColor = Color.Red;
        }
    }
}
