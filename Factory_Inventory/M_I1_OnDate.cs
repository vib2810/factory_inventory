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

namespace Factory_Inventory
{
    public partial class M_I1_OnDate : Form
    {
        DbConnect c= new DbConnect();
        public M_I1_OnDate()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable cartons = c.getInventoryCarton(this.dateTimePicker1.Value);
            dataGridView1.DataSource = cartons;
            DataTable twist_stock = c.getTwistStock2(this.dateTimePicker1.Value);
            dataGridView2.DataSource = twist_stock;
            DataTable trays = c.getInventoryTray(this.dateTimePicker1.Value);
            dataGridView3.DataSource = trays;
            DataTable dyeingBatch= c.getInventoryDyeingBatch(this.dateTimePicker1.Value);
            dataGridView4.DataSource = dyeingBatch;
            DataTable ConningBatch= c.getInventoryConningBatch(this.dateTimePicker1.Value);
            dataGridView5.DataSource = ConningBatch;
            DataTable cartonproduced = c.getInventoryCartonProduced(this.dateTimePicker1.Value);
            dataGridView6.DataSource = cartonproduced;
        }
    }
}
