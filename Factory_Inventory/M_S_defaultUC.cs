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
        public M_S_defaultUC()
        {
            InitializeComponent();
            panel1.BackColor = Color.Green;
            if(string.IsNullOrEmpty(c.con.ConnectionString))
            {
                return; 
            }
            //Create drop-down Cone list
            var dataSource4 = new List<string>();
            DataTable dt = c.getQC('n');
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dataSource4.Add(dt.Rows[i][0].ToString());
            }
            this.coneWeightCB.DataSource = dataSource4;
            this.coneWeightCB.DisplayMember = "Cones";
            this.coneWeightCB.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.coneWeightCB.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.coneWeightCB.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            coneWeightCB.SelectedIndex = coneWeightCB.FindStringExact(Properties.Settings.Default.DefaultCone.ToString());
            this.coneWeightCB.SelectedIndexChanged += new System.EventHandler(this.allTB_TextChanged);
        }

        private void allTB_TextChanged(object sender, EventArgs e)
        {
            panel1.BackColor = Color.Red;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            panel1.BackColor = Color.Green;
            Properties.Settings.Default.DefaultCone = coneWeightCB.Text;
            Properties.Settings.Default.Save();
        }
    }
}
