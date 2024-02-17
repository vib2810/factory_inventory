using Factory_Inventory.Factory_Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Factory_Inventory.Main
{
    public partial class CR_S_salesReport : Form
    {
        MainConnect mc;
        public CR_S_salesReport(MainConnect mc)
        {
            InitializeComponent();
            this.mc = mc;
        }

        private void CR_S_salesReport_Load(object sender, EventArgs e)
        {
            this.Text = "Sales Report";
            
            for (int i = 0; i < checkedListBox1.Items.Count; i++) checkedListBox1.SetItemChecked(i, true);

        }
    }
}
