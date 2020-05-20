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
    public partial class M_I1_FromToDate : Form
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
            if (keyData == Keys.S)
            {
                this.button3.PerformClick();
                return false;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private DbConnect c= new DbConnect();
        //private DataTable cartons, twist_stock, trays, dyeingBatch, ConningBatch, cartonproduced;
        private int[] dgvstates = new int[6];
        private DataTable[] to_show_details= new DataTable[6]; //not required
        private DataTable[] to_show_summary = new DataTable[6]; //not required
        private DataTable[] opening_stock = new DataTable[6]; //stores opening stock of all types(RAW)
        private DataTable[] closing_stock = new DataTable[6]; //stores closing stock of all types(RAW)
        private DataTable[,] opening_stock_showing = new DataTable[6,2]; //stores opening stock of selected types(0- summary, 1- details)
        private DataTable[,] closing_stock_showing = new DataTable[6,2]; //stores closing stock of selected types(0- summary, 1- details)

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
                this.button3.Enabled = false;
            }
            else
            {
                this.button2.Enabled = true;
                this.button3.Enabled = true;
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (prev_load_date != this.dateTimePicker1DTP.Value)
            {
                this.opening_stock[0]= c.getInventoryCarton(this.dateTimePicker1DTP.Value);
                this.opening_stock[1] = c.getTwistStock2(this.dateTimePicker1DTP.Value);
                this.opening_stock[2] = c.getInventoryTray(this.dateTimePicker1DTP.Value);
                this.opening_stock[3] = c.getInventoryDyeingBatch(this.dateTimePicker1DTP.Value);
                this.opening_stock[4] = c.getInventoryConningBatch(this.dateTimePicker1DTP.Value);
                this.opening_stock[5] = c.getInventoryCartonProduced(this.dateTimePicker1DTP.Value);
            }
            for(int i=0;i<6;i++)
            {
                this.opening_stock_showing[i, 1] = this.getStockDetails(i);
            }

            for(int i=0;i<6;i++)
            {
                if(i==1 || i==5)
                {
                    this.opening_stock_showing[i, 0] = this.opening_stock_showing[i, 1];
                    continue;
                }
                this.opening_stock_showing[i,0] = this.getCompanyQualitySummary(this.opening_stock_showing[i,1]);
                this.dgvstates[i] = 0;
            }
            int count = 0;
            var dgvs = this.Controls
              .OfType<DataGridView>()
              .Where(x => x.Name.StartsWith("dataGridView"));
            foreach (var dgv in dgvs)
            {
                if (dgv.SelectedRows.Count != 0)
                {
                    dgv.SelectedRows[0].Selected = false;
                }
                count++;
            }

            this.dataGridView1.DataSource = opening_stock_showing[0, 0];
            this.set_summary_column_widths(this.dataGridView1);
            this.dataGridView2.DataSource = opening_stock_showing[1, 0];
            this.set_summary_column_widths(this.dataGridView2);
            this.dataGridView3.DataSource = opening_stock_showing[2, 0];
            this.set_summary_column_widths(this.dataGridView3);
            this.dataGridView4.DataSource = opening_stock_showing[3, 0];
            this.set_summary_column_widths(this.dataGridView4);
            this.dataGridView5.DataSource = opening_stock_showing[4, 0];
            this.set_summary_column_widths(this.dataGridView5);
            this.dataGridView6.DataSource = opening_stock_showing[5, 0];

            prev_load_date = this.dateTimePicker1DTP.Value;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            //view details form or details
            if(this.dataGridView1.SelectedRows.Count!=0)
            {
                if (this.dgvstates[0] == 1)
                {
                    Display_Carton dc = new Display_Carton((dataGridView1.Rows[dataGridView1.CurrentRow.Index].DataBoundItem as DataRowView).Row);
                    dc.Show();
                }
                else
                {
                    dataGridView1.DataSource = this.to_show_details[0];
                    this.dgvstates[0] = 1;
                }
            }
            else if (this.dataGridView3.SelectedRows.Count != 0)
            {
                if (this.dgvstates[2] == 1)
                {
                    Display_Tray dt = new Display_Tray((dataGridView3.Rows[dataGridView3.CurrentRow.Index].DataBoundItem as DataRowView).Row);
                    dt.Show();
                }
                else
                {
                    dataGridView3.DataSource = this.to_show_details[2];
                    this.dgvstates[2] = 1;
                }
            }
            else if (this.dataGridView4.SelectedRows.Count!= 0)
            {
                if (this.dgvstates[2] == 1)
                {
                    Display_Batch db = new Display_Batch((dataGridView4.Rows[dataGridView4.CurrentRow.Index].DataBoundItem as DataRowView).Row);
                    db.Show();
                }
                else
                {
                    dataGridView3.DataSource = this.to_show_details[3];
                    this.dgvstates[3] = 1;
                }
            }
            else if (this.dataGridView5.SelectedRows.Count!=0)
            {
                if (this.dgvstates[4] == 1)
                {
                    Display_Batch db = new Display_Batch((dataGridView5.Rows[dataGridView5.CurrentRow.Index].DataBoundItem as DataRowView).Row);
                    db.Show();
                }
                else
                {
                    dataGridView5.DataSource = this.to_show_details[4];
                    this.dgvstates[4] = 1;
                }
            }
            else if (this.dataGridView6.SelectedRows.Count!= 0)
            {
                if (this.dgvstates[5] == 1)
                {
                    Display_Carton_Produced dcp = new Display_Carton_Produced((dataGridView6.Rows[dataGridView6.CurrentRow.Index].DataBoundItem as DataRowView).Row);
                    dcp.Show();
                }
                else
                {
                    dataGridView6.DataSource = this.to_show_details[5];
                    this.dgvstates[5] = 1;
                }
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            //switch to summary
            if (this.dataGridView1.SelectedRows.Count != 0)
            {
                if (this.dgvstates[0] == 1)
                {
                    dataGridView1.DataSource = this.to_show_summary[0];
                    this.dgvstates[0] = 0;
                    set_summary_column_widths(dataGridView1);
                }
            }
            else if (this.dataGridView3.SelectedRows.Count != 0)
            {
                if (this.dgvstates[2] == 1)
                {
                    dataGridView3.DataSource = this.to_show_summary[2];
                    this.dgvstates[2] = 0;
                    set_summary_column_widths(dataGridView3);
                }
            }
            else if (this.dataGridView4.SelectedRows.Count != 0)
            {
                if (this.dgvstates[2] == 1)
                {
                    dataGridView4.DataSource = this.to_show_summary[3];
                    this.dgvstates[3] = 0;
                    set_summary_column_widths(dataGridView4);
                }
            }
            else if (this.dataGridView5.SelectedRows.Count != 0)
            {
                if (this.dgvstates[4] == 1)
                {
                    dataGridView5.DataSource = this.to_show_summary[4];
                    this.dgvstates[4] = 0;
                    set_summary_column_widths(dataGridView5);
                }
            }
            else if (this.dataGridView6.SelectedRows.Count != 0)
            {
                if (this.dgvstates[5] == 1)
                { 
                    dataGridView6.DataSource = this.to_show_summary[5];
                    set_summary_column_widths(dataGridView6);
                    this.dgvstates[5] = 0;
                }
            }
        }
        public M_I1_FromToDate()
        {
            InitializeComponent();
        }
        private void set_summary_column_widths(DataGridView d)
        {
            c.auto_adjust_dgv(d);
            //d.Columns[0].Width = 150;
            //d.Columns[1].Width = 150;
            //d.Columns[2].Width = 100;
            //d.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
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
        private DataTable getCompanyQualitySummary(DataTable input)
        {
            DataTable ans = new DataTable();
            ans.Columns.Add("Company Name");
            ans.Columns.Add("Quality");
            ans.Columns.Add("Net Weight");


            //Dictionary<string, bool> companies = new Dictionary<string, bool>();
            DataView dv = input.DefaultView;
            dv.Sort = "Company_Name desc";
            input = dv.ToTable();
            string prev_company = "";
            for (int i=0; i<input.Rows.Count; i++)
            {
                string company = input.Rows[i]["Company_Name"].ToString();
                string quality = input.Rows[i]["Quality"].ToString();
                float net_weight= float.Parse(input.Rows[i]["Net_Weight"].ToString());
                if (company==prev_company)
                {
                    bool quality_exists = false;
                    for(int j=0; j<ans.Rows.Count; j++)
                    {
                        string anscompany = ans.Rows[j]["Company Name"].ToString();
                        string ansquality = ans.Rows[j]["Quality"].ToString();
                        Console.WriteLine(ans.Rows[j]["Net Weight"].ToString());
                        float ansnet_weight = 0F;
                        if (ans.Rows[j]["Net Weight"].ToString()!="") ansnet_weight = float.Parse(ans.Rows[j]["Net Weight"].ToString());

                        if (anscompany==company && ansquality==quality)
                        {
                            ans.Rows[j]["Net Weight"] = ansnet_weight + net_weight;
                            quality_exists = true;
                            break;
                        }
                    }
                    if(quality_exists==false) ans.Rows.Add(company, quality, net_weight.ToString("F3"));
                }
                else
                {
                    if(ans.Rows.Count!=0) ans.Rows.Add("", "", "");
                    ans.Rows.Add(company, quality, net_weight.ToString("F3"));
                    prev_company = company;
                }
            }
            return ans;
        }
        private DataTable getStockDetails(int id)
        {
            #region
            DataTable answer = this.opening_stock[id].Clone();
            if(id==0)
            {
                //Carton datagridview
                for (int i = 0; i < this.opening_stock[id].Rows.Count; i++)
                {
                    string quality = this.opening_stock[id].Rows[i]["Quality"].ToString();
                    string company = this.opening_stock[id].Rows[i]["Company_Name"].ToString();
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
                    if (flag == 2)
                    {
                        answer.Rows.Add(this.opening_stock[id].Rows[i].ItemArray);
                    }
                }
                return answer;
            }
            else if(id==1)
            {
                //Twist Datagridview
                string prev_company = this.opening_stock[id].Rows[0]["Company_Name"].ToString();
                for (int i = 0; i < this.opening_stock[id].Rows.Count; i++)
                {
                    string quality = this.opening_stock[id].Rows[i]["Quality"].ToString();
                    string company = this.opening_stock[id].Rows[i]["Company_Name"].ToString();
                    if (prev_company != company)
                    {
                        answer.Rows.Add();
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
                    if (flag == 2)
                    {
                        answer.Rows.Add(this.opening_stock[id].Rows[i].ItemArray);
                    }
                    prev_company = company;
                }
                return answer;
            }
            else if(id==2)
            {
                //Trays datagridview
                for (int i = 0; i < this.opening_stock[id].Rows.Count; i++)
                {
                    string quality = this.opening_stock[id].Rows[i]["Quality"].ToString();
                    string company = this.opening_stock[id].Rows[i]["Company_Name"].ToString();
                    int flag = 0;
                    for (int j = 0; j < this.dataGridView7.Rows.Count; j++)
                    {
                        if (this.dataGridView7.Rows[j].Cells["Quality"].Value.ToString() == quality && Convert.ToBoolean(this.dataGridView7.Rows[j].Cells["Check"].Value) == true)
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
                    if (flag == 2)
                    {
                        answer.Rows.Add(this.opening_stock[id].Rows[i].ItemArray);
                    }
                }
                return answer;
            }
            else if(id==3)
            {
                //Dyeing batches Datagridview
                for (int i = 0; i < this.opening_stock[id].Rows.Count; i++)
                {
                    string quality = this.opening_stock[id].Rows[i]["Quality"].ToString();
                    string colour = this.opening_stock[id].Rows[i]["Colour"].ToString();
                    string company = this.opening_stock[id].Rows[i]["Company_Name"].ToString();
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
                        answer.Rows.Add(this.opening_stock[id].Rows[i].ItemArray);
                    }
                }
                return answer;
            }
            else if(id==4)
            {
                //Not conned batches datagridview
                for (int i = 0; i < this.opening_stock[id].Rows.Count; i++)
                {
                    string quality = this.opening_stock[id].Rows[i]["Quality"].ToString();
                    string colour = this.opening_stock[id].Rows[i]["Colour"].ToString();
                    string company = this.opening_stock[id].Rows[i]["Company_Name"].ToString();
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
                        answer.Rows.Add(this.opening_stock[id].Rows[i].ItemArray);
                    }
                }
                return answer;
            }
            else if(id==5)
            {
                //Cartons Produced Datagridview
                for (int i = 0; i < this.opening_stock[id].Rows.Count; i++)
                {
                    string quality = this.opening_stock[id].Rows[i]["Quality"].ToString();
                    string colour = this.opening_stock[id].Rows[i]["Colour"].ToString();
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
                        answer.Rows.Add(this.opening_stock[id].Rows[i].ItemArray);
                    }
                }
                return answer;
            }
            return null;
            #endregion
        }
    }
}
