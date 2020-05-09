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

        private void M_I1_OnDate_Load(object sender, EventArgs e)
        {

            //Create drop-down Colour list
            var dataSource = new List<string>();
            DataTable dt1 = c.getQC('q');
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                dataSource.Add(dt1.Rows[i][0].ToString());
            }

            this.dataGridView7.Columns.Add("Quality", "Quality");
            this.dataGridView7.Columns[0].Width = 135;
            this.dataGridView7.Columns[0].ReadOnly= true;

            DataGridViewCheckBoxColumn dgvCmb1 = new DataGridViewCheckBoxColumn();
            dgvCmb1.ValueType = typeof(bool);
            dgvCmb1.Name = "Chk";
            dgvCmb1.HeaderText = "CheckBox";
            dataGridView7.Columns.Add(dgvCmb1);
            dataGridView7.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;


            for (int i = 0; i < dataSource.Count; i++)
            {
                this.dataGridView7.Rows.Add(dataSource[i], false);
            }

            //Create drop-down Colour list
            var dataSource1 = new List<string>();
            DataTable dt = c.getQC('l');
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dataSource1.Add(dt.Rows[i][0].ToString());
            }
            List<string> final_list = dataSource1.Distinct().ToList();
            
            this.dataGridView8.Columns.Add("Colour", "Colour");
            this.dataGridView8.Columns[0].Width = 135;
            this.dataGridView8.Columns[0].ReadOnly = true;

            DataGridViewCheckBoxColumn dgvCmb = new DataGridViewCheckBoxColumn();
            dgvCmb.ValueType = typeof(bool);
            dgvCmb.Name = "Chk";
            dgvCmb.HeaderText = "CheckBox";
            dataGridView8.Columns.Add(dgvCmb);
            dataGridView8.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            for (int i=0;i<final_list.Count;i++)
            {
                this.dataGridView8.Rows.Add(final_list[i], false);
            }
        }

        private void allQualityCK_CheckedChanged(object sender, EventArgs e)
        {
            for(int i=0;i<this.dataGridView7.Rows.Count;i++)
            {
                if(this.allQualityCK.Checked==true)
                {
                    this.dataGridView7.Rows[i].Cells[1].Value = true;
                }
                else
                {
                    this.dataGridView7.Rows[i].Cells[1].Value = false;
                }
            }
        }

        private void allColourCK_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < this.dataGridView8.Rows.Count; i++)
            {
                if (this.allColourCK.Checked == true)
                {
                    this.dataGridView8.Rows[i].Cells[1].Value = true;
                }
                else
                {
                    this.dataGridView8.Rows[i].Cells[1].Value = false;
                }
            }
        }
    }
}
