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
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                this.button1.PerformClick();
                return false;
            }
            if(keyData==Keys.V)
            {
                this.button2.PerformClick();
                return false;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private DbConnect c= new DbConnect();
        private DataTable cartons, twist_stock, trays, dyeingBatch, ConningBatch, cartonproduced;
        DateTime prev_load_date;
        private void allCompanyCK_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < this.dataGridView9.Rows.Count; i++)
            {
                if (this.allCompanyCK.Checked == true)
                {
                    this.dataGridView9.Rows[i].Cells[1].Value = true;
                }
                else
                {
                    this.dataGridView9.Rows[i].Cells[1].Value = false;
                }
            }
        }
        private void deselect_dgvs(object sender, EventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            if (dgv == dataGridView2)
            {
                this.button2.Enabled = false;
            }
            else
            {
                this.button2.Enabled = true;
            }
            if (dataGridView1.SelectedRows.Count != 0 && dataGridView1 != dgv) dataGridView1.SelectedRows[0].Selected = false;
            if (dataGridView2.SelectedRows.Count != 0 && dataGridView2 != dgv) dataGridView2.SelectedRows[0].Selected = false;
            if (dataGridView3.SelectedRows.Count != 0 && dataGridView3 != dgv) dataGridView3.SelectedRows[0].Selected = false;
            if (dataGridView4.SelectedRows.Count != 0 && dataGridView4 != dgv) dataGridView4.SelectedRows[0].Selected = false;
            if (dataGridView5.SelectedRows.Count != 0 && dataGridView5 != dgv) dataGridView5.SelectedRows[0].Selected = false;
            if (dataGridView6.SelectedRows.Count != 0 && dataGridView6 != dgv) dataGridView6.SelectedRows[0].Selected = false;
        }
        private void dgvEvent(DataGridView dgv)
        {
            dgv.Click += new EventHandler(this.deselect_dgvs);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if(this.dataGridView1.SelectedRows.Count!=0)
            {
                Display_Carton dc = new Display_Carton((dataGridView1.Rows[dataGridView1.CurrentRow.Index].DataBoundItem as DataRowView).Row);
                dc.Show();
            }
            else if (this.dataGridView3.SelectedRows.Count != 0)
            {
                Display_Tray dt = new Display_Tray((dataGridView3.Rows[dataGridView3.CurrentRow.Index].DataBoundItem as DataRowView).Row);
                dt.Show();
            }
            else if (this.dataGridView4.SelectedRows.Count!= 0)
            {
                Display_Batch db = new Display_Batch((dataGridView4.Rows[dataGridView4.CurrentRow.Index].DataBoundItem as DataRowView).Row);
                db.Show();
            }
            else if (this.dataGridView5.SelectedRows.Count!=0)
            {
                Display_Batch db = new Display_Batch((dataGridView5.Rows[dataGridView5.CurrentRow.Index].DataBoundItem as DataRowView).Row);
                db.Show();
            }
            else if (this.dataGridView6.SelectedRows.Count!= 0)
            {
                Display_Carton_Produced dcp = new Display_Carton_Produced((dataGridView6.Rows[dataGridView6.CurrentRow.Index].DataBoundItem as DataRowView).Row);
                dcp.Show();
            }
        }
        public M_I1_OnDate()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if(prev_load_date!=this.dateTimePicker1.Value)
            {
                this.cartons = c.getInventoryCarton(this.dateTimePicker1.Value);
                this.twist_stock = c.getTwistStock2(this.dateTimePicker1.Value);
                this.trays = c.getInventoryTray(this.dateTimePicker1.Value);
                this.dyeingBatch = c.getInventoryDyeingBatch(this.dateTimePicker1.Value);
                this.ConningBatch = c.getInventoryConningBatch(this.dateTimePicker1.Value);
                this.cartonproduced = c.getInventoryCartonProduced(this.dateTimePicker1.Value);
            }

            //Carton datagridview
            DataTable cartons_to_show = this.cartons.Clone();
            for (int i = 0; i < this.cartons.Rows.Count; i++)
            {
                string quality = this.cartons.Rows[i]["Quality"].ToString();
                string company = this.cartons.Rows[i]["Company_Name"].ToString();
                int flag=0;
                for (int j = 0; j < this.dataGridView7.Rows.Count; j++)
                {
                    if (this.dataGridView7.Rows[j].Cells["Quality Before Twist"].Value.ToString() == quality && Convert.ToBoolean(this.dataGridView7.Rows[j].Cells["Check"].Value) == true)
                    {
                        flag++;
                    }
                }
                for(int j=0;j<this.dataGridView9.Rows.Count;j++)
                {
                    if (this.dataGridView9.Rows[j].Cells["Company Names"].Value.ToString() == company && Convert.ToBoolean(this.dataGridView9.Rows[j].Cells["Check"].Value) == true)
                    {
                        flag++;
                    }
                }
                if(flag==2)
                {
                    cartons_to_show.Rows.Add(this.cartons.Rows[i].ItemArray);
                }
                
            }
            dataGridView1.DataSource = cartons_to_show;

            //Twist Datagridview
            DataTable twist_stock_to_show = this.twist_stock.Clone();
            string prev_company = this.twist_stock.Rows[0]["Company_Name"].ToString();
            for (int i = 0; i < this.twist_stock.Rows.Count; i++)
            {
                string quality = this.twist_stock.Rows[i]["Quality"].ToString();
                string company = this.twist_stock.Rows[i]["Company_Name"].ToString();
                if(prev_company!=company)
                {
                    twist_stock_to_show.Rows.Add();
                    i--;
                    prev_company = company;
                    continue;
                }
                int flag = 0;
                for (int j = 0; j < this.dataGridView7.Rows.Count; j++)
                {
                    if (this.dataGridView7.Rows[j].Cells["Quality Before Twist"].Value.ToString() == quality && Convert.ToBoolean(this.dataGridView7.Rows[j].Cells["Check"].Value) == true)
                    {
                        flag++;
                    }
                }
                for (int j = 0; j < this.dataGridView9.Rows.Count; j++)
                {
                    if (this.dataGridView9.Rows[j].Cells["Company Names"].Value.ToString() == company && Convert.ToBoolean(this.dataGridView9.Rows[j].Cells["Check"].Value) == true)
                    {
                        flag++;
                    }
                }
                if(flag==2)
                {
                    twist_stock_to_show.Rows.Add(this.twist_stock.Rows[i].ItemArray);
                }
                prev_company = company;
            }
            dataGridView2.DataSource = twist_stock_to_show;
            dataGridView2.Columns[0].Width = 135;
            dataGridView2.Columns[1].Width = 135;
            dataGridView7.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            //Trays datagridview
            DataTable trays_to_show = this.trays.Clone();
            for (int i = 0; i < this.trays.Rows.Count; i++)
            {
                string quality = this.trays.Rows[i]["Quality"].ToString();
                string company = this.trays.Rows[i]["Company_Name"].ToString();
                int flag = 0;
                for (int j = 0; j < this.dataGridView7.Rows.Count; j++)
                {
                    if (this.dataGridView7.Rows[j].Cells["Quality"].Value.ToString() == quality && Convert.ToBoolean(this.dataGridView7.Rows[j].Cells["Check"].Value) == true)
                    {
                        flag++;
                    }
                }
                for(int j=0;j<this.dataGridView9.Rows.Count;j++)
                {
                    if (this.dataGridView9.Rows[j].Cells["Company Names"].Value.ToString() == company && Convert.ToBoolean(this.dataGridView9.Rows[j].Cells["Check"].Value) == true)
                    {
                        flag++;
                    }
                }
                if(flag==2)
                {
                    trays_to_show.Rows.Add(this.trays.Rows[i].ItemArray);
                }



            }
            dataGridView3.DataSource = trays_to_show;

            //Dyeing batches Datagridview
            DataTable dyeingBatch_to_show = this.dyeingBatch.Clone();
            for (int i = 0; i < this.dyeingBatch.Rows.Count; i++)
            {
                string quality = this.dyeingBatch.Rows[i]["Quality"].ToString();
                string colour = this.dyeingBatch.Rows[i]["Colour"].ToString();
                string company = this.dyeingBatch.Rows[i]["Company_Name"].ToString();
                int flag = 0;
                for (int j = 0; j < this.dataGridView7.Rows.Count; j++)
                {
                    if (this.dataGridView7.Rows[j].Cells["Quality"].Value.ToString() == quality && Convert.ToBoolean(this.dataGridView7.Rows[j].Cells["Check"].Value) == true)
                    {
                        flag++;
                    }
                }
                for (int j = 0; j < this.dataGridView8.Rows.Count; j++)
                {
                    if (this.dataGridView8.Rows[j].Cells["Colour"].Value.ToString() == colour && Convert.ToBoolean(this.dataGridView8.Rows[j].Cells["Check"].Value) == true)
                    {
                        flag++;
                    }
                }
                for (int j = 0; j < this.dataGridView9.Rows.Count; j++)
                {
                    if (this.dataGridView9.Rows[j].Cells["Company Names"].Value.ToString() == company && Convert.ToBoolean(this.dataGridView9.Rows[j].Cells["Check"].Value) == true)
                    {
                        flag++;
                    }
                }
                if (flag == 3)
                {
                    dyeingBatch_to_show.Rows.Add(this.dyeingBatch.Rows[i].ItemArray);
                }
            }
            dataGridView4.DataSource = dyeingBatch_to_show;

            //Not conned batches datagridview
            DataTable ConningBatch_to_show = this.ConningBatch.Clone();
            for (int i = 0; i < this.ConningBatch.Rows.Count; i++)
            {
                string quality = this.ConningBatch.Rows[i]["Quality"].ToString();
                string colour = this.ConningBatch.Rows[i]["Colour"].ToString();
                string company = this.ConningBatch.Rows[i]["Company_Name"].ToString();
                int flag = 0;
                for (int j = 0; j < this.dataGridView7.Rows.Count; j++)
                {
                    if (this.dataGridView7.Rows[j].Cells["Quality"].Value.ToString() == quality && Convert.ToBoolean(this.dataGridView7.Rows[j].Cells["Check"].Value) == true)
                    {
                        flag++;
                    }
                }
                for (int j = 0; j < this.dataGridView8.Rows.Count; j++)
                {
                    if (this.dataGridView8.Rows[j].Cells["Colour"].Value.ToString() == colour && Convert.ToBoolean(this.dataGridView8.Rows[j].Cells["Check"].Value) == true)
                    {
                        flag++;
                    }
                }
                for (int j = 0; j < this.dataGridView9.Rows.Count; j++)
                {
                    if (this.dataGridView9.Rows[j].Cells["Company Names"].Value.ToString() == company && Convert.ToBoolean(this.dataGridView9.Rows[j].Cells["Check"].Value) == true)
                    {
                        flag++;
                    }
                }
                if (flag == 3)
                {
                    ConningBatch_to_show.Rows.Add(this.ConningBatch.Rows[i].ItemArray);
                }
            }
            dataGridView5.DataSource = ConningBatch_to_show;

            //Cartons Produced Datagridview
            DataTable cartonproduced_to_show = this.cartonproduced.Clone();
            for (int i = 0; i < this.cartonproduced.Rows.Count; i++)
            {
                string quality = this.cartonproduced.Rows[i]["Quality"].ToString();
                string colour = this.cartonproduced.Rows[i]["Colour"].ToString();
                int flag = 0;
                for (int j = 0; j < this.dataGridView7.Rows.Count; j++)
                {
                    if (this.dataGridView7.Rows[j].Cells["Quality"].Value.ToString() == quality && Convert.ToBoolean(this.dataGridView7.Rows[j].Cells["Check"].Value) == true)
                    {
                        flag++;
                    }
                }
                for (int j = 0; j < this.dataGridView8.Rows.Count; j++)
                {
                    if (this.dataGridView8.Rows[j].Cells["Colour"].Value.ToString() == colour && Convert.ToBoolean(this.dataGridView8.Rows[j].Cells["Check"].Value) == true)
                    {
                        flag++;
                    }
                }
                if (flag == 2)
                {
                    cartonproduced_to_show.Rows.Add(this.cartonproduced.Rows[i].ItemArray);
                }
            }
            dataGridView6.DataSource = cartonproduced_to_show;

            var dgvs = this.Controls
      .OfType<DataGridView>()
      .Where(x => x.Name.StartsWith("dataGridView"));

            foreach (var dgv in dgvs)
            {
                dgv.SelectedRows[0].Selected = false;
            }

            prev_load_date = this.dateTimePicker1.Value;
        }
        private void M_I1_OnDate_Load(object sender, EventArgs e)
        {

            //Create drop-down Colour list
            var dataSource = new List<string>();
            var quality_before_twist = new List<string>();
            DataTable dt1 = c.getQC('q');
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                dataSource.Add(dt1.Rows[i][0].ToString());
                quality_before_twist.Add(dt1.Rows[i][3].ToString());
            }

            this.dataGridView7.Columns.Add("Quality", "Quality");
            this.dataGridView7.Columns[0].Width = 135;
            this.dataGridView7.Columns[0].ReadOnly= true;

            DataGridViewCheckBoxColumn dgvCmb1 = new DataGridViewCheckBoxColumn();
            dgvCmb1.ValueType = typeof(bool);
            dgvCmb1.Name = "Check";
            dgvCmb1.HeaderText = "Check";
            dataGridView7.Columns.Add(dgvCmb1);
            dataGridView7.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            this.dataGridView7.Columns.Add("Quality Before Twist", "Quality Before Twist");
            this.dataGridView7.Columns[2].Visible = false;

            for (int i = 0; i < dataSource.Count; i++)
            {
                this.dataGridView7.Rows.Add(dataSource[i], false, quality_before_twist[i]);
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
            dgvCmb.Name = "Check";
            dgvCmb.HeaderText = "Check";
            dataGridView8.Columns.Add(dgvCmb);
            dataGridView8.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            for (int i=0;i<final_list.Count;i++)
            {
                this.dataGridView8.Rows.Add(final_list[i], false);
            }

            //Create a drop-down list
            var dataSource2 = new List<string>();
            DataTable d2 = c.getQC('c');
            
            this.dataGridView9.Columns.Add("Company Names", "Company Names");
            this.dataGridView9.Columns[0].Width = 135;
            this.dataGridView9.Columns[0].ReadOnly = true;

            DataGridViewCheckBoxColumn dgvCmb2 = new DataGridViewCheckBoxColumn();
            dgvCmb2.ValueType = typeof(bool);
            dgvCmb2.Name = "Check";
            dgvCmb2.HeaderText = "Check";
            dataGridView9.Columns.Add(dgvCmb2);
            dataGridView9.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            for (int i = 0; i < d2.Rows.Count; i++)
            {
                this.dataGridView9.Rows.Add(d2.Rows[i][0].ToString(), false);
            }

            this.allColourCK.Checked = true;
            this.allQualityCK.Checked = true;
            this.allCompanyCK.Checked = true;

            this.prev_load_date = DateTime.MinValue;

            var dgvs = this.Controls
      .OfType<DataGridView>()
      .Where(x => x.Name.StartsWith("dataGridView"));

            foreach (var dgv in dgvs)
            {
                this.dgvEvent(dgv);
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
