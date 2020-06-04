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
                this.detailsButton.PerformClick();
                return false;
            }
            if (keyData == Keys.S)
            {
                this.backButton.PerformClick();
                return false;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private DbConnect c= new DbConnect();
        private DataTable cartons=new DataTable(), twist_stock= new DataTable(), 
        trays=new DataTable(), dyeingBatch = new DataTable(), ConningBatch= new DataTable(), semiproduced=new DataTable(), cartonproduced=new DataTable();
        private int[] dgvstates = new int[7];
        private DataTable[] to_show_details= new DataTable[7];
        private DataTable[] to_show_summary = new DataTable[7];

        DateTime prev_load_date;
        public M_I1_OnDate()
        {
            InitializeComponent();
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

            this.dataGridViewQuality.Columns.Add("Quality", "Quality");
            this.dataGridViewQuality.Columns[0].Width = 135;
            this.dataGridViewQuality.Columns[0].ReadOnly = true;

            DataGridViewCheckBoxColumn dgvCmb1 = new DataGridViewCheckBoxColumn();
            dgvCmb1.ValueType = typeof(bool);
            dgvCmb1.Name = "Check";
            dgvCmb1.HeaderText = "Check";
            dataGridViewQuality.Columns.Add(dgvCmb1);
            dataGridViewQuality.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            this.dataGridViewQuality.Columns.Add("Quality Before Twist", "Quality Before Twist");
            this.dataGridViewQuality.Columns[2].Visible = false;

            for (int i = 0; i < dataSource.Count; i++)
            {
                this.dataGridViewQuality.Rows.Add(dataSource[i], false, quality_before_twist[i]);
            }

            //Create drop-down Colour list
            var dataSource1 = new List<string>();
            DataTable dt = c.getQC('l');
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dataSource1.Add(dt.Rows[i][0].ToString());
            }
            List<string> final_list = dataSource1.Distinct().ToList();

            this.dataGridViewCompany.Columns.Add("Colour", "Colour");
            this.dataGridViewCompany.Columns[0].Width = 135;
            this.dataGridViewCompany.Columns[0].ReadOnly = true;

            DataGridViewCheckBoxColumn dgvCmb = new DataGridViewCheckBoxColumn();
            dgvCmb.ValueType = typeof(bool);
            dgvCmb.Name = "Check";
            dgvCmb.HeaderText = "Check";
            dataGridViewCompany.Columns.Add(dgvCmb);
            dataGridViewCompany.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            for (int i = 0; i < final_list.Count; i++)
            {
                this.dataGridViewCompany.Rows.Add(final_list[i], false);
            }

            //Create a drop-down list
            var dataSource2 = new List<string>();
            DataTable d2 = c.getQC('c');

            this.dataGridViewCName.Columns.Add("Company Names", "Company Names");
            this.dataGridViewCName.Columns[0].Width = 135;
            this.dataGridViewCName.Columns[0].ReadOnly = true;

            DataGridViewCheckBoxColumn dgvCmb2 = new DataGridViewCheckBoxColumn();
            dgvCmb2.ValueType = typeof(bool);
            dgvCmb2.Name = "Check";
            dgvCmb2.HeaderText = "Check";
            dataGridViewCName.Columns.Add(dgvCmb2);
            dataGridViewCName.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            for (int i = 0; i < d2.Rows.Count; i++)
            {
                this.dataGridViewCName.Rows.Add(d2.Rows[i][0].ToString(), false);
            }

            this.allColourCK.Checked = true;
            this.allQualityCK.Checked = true;
            this.allCompanyCK.Checked = true;

            this.prev_load_date = DateTime.MinValue;

            var dgvs = panel1.Controls
            .OfType<DataGridView>()
            .Where(x => x.Name.StartsWith("dataGridView"));

            foreach (var dgv in dgvs)
            {
                this.dgvEvent(dgv);
            }
        }

        //user fns
        private void set_summary_column_widths(DataGridView d)
        {
            d.Columns[0].Width = 150;
            d.Columns[1].Width = 150;
            d.Columns[2].Width = 100;
            d.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
        private void set_details_column_widths(int dgv_index)
        {
            if (dgv_index == 0 || dgv_index == -1)
            {
                //ci
                this.dataGridView0.Rows[0].Selected = true;
                this.dataGridView0.Columns.OfType<DataGridViewColumn>().ToList().ForEach(col => col.Visible = false);
                this.dataGridView0.Columns["Date_Of_Billing"].Visible = true;
                this.dataGridView0.Columns["Date_Of_Billing"].DisplayIndex = 1;
                this.dataGridView0.Columns["Date_Of_Billing"].HeaderText = "Date of Billing";
                this.dataGridView0.Columns["Carton_No"].Visible = true;
                this.dataGridView0.Columns["Carton_No"].DisplayIndex = 2;
                this.dataGridView0.Columns["Carton_No"].HeaderText = "Carton Number";
                this.dataGridView0.Columns["Quality"].Visible = true;
                this.dataGridView0.Columns["Quality"].DisplayIndex = 6;
                this.dataGridView0.Columns["Quality"].HeaderText = "Quality";
                this.dataGridView0.Columns["Company_Name"].Visible = true;
                this.dataGridView0.Columns["Company_Name"].DisplayIndex = 7;
                this.dataGridView0.Columns["Company_Name"].HeaderText = "Company Name";
                this.dataGridView0.Columns["Bill_No"].Visible = true;
                this.dataGridView0.Columns["Bill_No"].DisplayIndex = 9;
                this.dataGridView0.Columns["Bill_No"].HeaderText = "Bill Number";
                this.dataGridView0.Columns["Net_Weight"].Visible = true;
                this.dataGridView0.Columns["Net_Weight"].DisplayIndex = 11;
                this.dataGridView0.Columns["Net_Weight"].HeaderText = "Net Weight";
                c.auto_adjust_dgv(this.dataGridView0);
            }
            if (dgv_index == 1 || dgv_index == -1)
            {
                //null
            }
            if (dgv_index == 2 || dgv_index == -1)
            {
                //tray
                this.dataGridView2.Columns.OfType<DataGridViewColumn>().ToList().ForEach(col => col.Visible = false);
                this.dataGridView2.Columns["Tray_Production_Date"].Visible = true;
                this.dataGridView2.Columns["Tray_Production_Date"].DisplayIndex = 1;
                this.dataGridView2.Columns["Tray_Production_Date"].HeaderText = "Production Date";
                this.dataGridView2.Columns["Tray_No"].Visible = true;
                this.dataGridView2.Columns["Tray_No"].DisplayIndex = 2;
                this.dataGridView2.Columns["Tray_No"].HeaderText = "Tray Number";
                this.dataGridView2.Columns["Quality"].Visible = true;
                this.dataGridView2.Columns["Quality"].DisplayIndex = 6;
                this.dataGridView2.Columns["Quality"].HeaderText = "Quality";
                this.dataGridView2.Columns["Net_Weight"].Visible = true;
                this.dataGridView2.Columns["Net_Weight"].DisplayIndex = 8;
                this.dataGridView2.Columns["Net_Weight"].HeaderText = "Net Weight";
                this.dataGridView2.Columns["Machine_No"].Visible = true;
                this.dataGridView2.Columns["Machine_No"].DisplayIndex = 10;
                this.dataGridView2.Columns["Machine_No"].HeaderText = "Machine Number";
                c.auto_adjust_dgv(this.dataGridView2);
            }
            if (dgv_index == 3 || dgv_index == -1)
            {
                this.dataGridView3.Columns.OfType<DataGridViewColumn>().ToList().ForEach(col => col.Visible = false);
                this.dataGridView3.Columns["Batch_No"].Visible = true;
                this.dataGridView3.Columns["Batch_No"].DisplayIndex = 1;
                this.dataGridView3.Columns["Batch_No"].HeaderText = "Batch Number";
                this.dataGridView3.Columns["Quality"].Visible = true;
                this.dataGridView3.Columns["Quality"].DisplayIndex = 4;
                this.dataGridView3.Columns["Quality"].HeaderText = "Quality";
                this.dataGridView3.Columns["Colour"].Visible = true;
                this.dataGridView3.Columns["Colour"].DisplayIndex = 6;
                this.dataGridView3.Columns["Colour"].HeaderText = "Colour";
                this.dataGridView3.Columns["Dyeing_Company_Name"].Visible = true;
                this.dataGridView3.Columns["Dyeing_Company_Name"].DisplayIndex = 8;
                this.dataGridView3.Columns["Dyeing_Company_Name"].HeaderText = "Dyeing Company Name";
                this.dataGridView3.Columns["Net_Weight"].Visible = true;
                this.dataGridView3.Columns["Net_Weight"].DisplayIndex = 9;
                this.dataGridView3.Columns["Net_Weight"].HeaderText = "Net Weight";
                c.auto_adjust_dgv(this.dataGridView3);
            }
            if (dgv_index == 4 || dgv_index == -1)
            {
                this.dataGridView4.Columns.OfType<DataGridViewColumn>().ToList().ForEach(col => col.Visible = false);
                this.dataGridView4.Columns["Batch_No"].Visible = true;
                this.dataGridView4.Columns["Batch_No"].DisplayIndex = 1;
                this.dataGridView4.Columns["Batch_No"].HeaderText = "Batch Number";
                this.dataGridView4.Columns["Quality"].Visible = true;
                this.dataGridView4.Columns["Quality"].DisplayIndex = 4;
                this.dataGridView4.Columns["Quality"].HeaderText = "Quality";
                this.dataGridView4.Columns["Colour"].Visible = true;
                this.dataGridView4.Columns["Colour"].DisplayIndex = 6;
                this.dataGridView4.Columns["Colour"].HeaderText = "Colour";
                this.dataGridView4.Columns["Dyeing_Company_Name"].Visible = true;
                this.dataGridView4.Columns["Dyeing_Company_Name"].DisplayIndex = 8;
                this.dataGridView4.Columns["Dyeing_Company_Name"].HeaderText = "Dyeing Company Name";
                this.dataGridView4.Columns["Net_Weight"].Visible = true;
                this.dataGridView4.Columns["Net_Weight"].DisplayIndex = 9;
                this.dataGridView4.Columns["Net_Weight"].HeaderText = "Net Weight";
                c.auto_adjust_dgv(this.dataGridView4);
            }
            if (dgv_index == 5 || dgv_index == -1)
            {
                this.dataGridView5.Columns.OfType<DataGridViewColumn>().ToList().ForEach(col => col.Visible = false);
                this.dataGridView5.Columns["Quality"].Visible = true;
                this.dataGridView5.Columns["Quality"].DisplayIndex = 2;
                this.dataGridView5.Columns["Quality"].HeaderText = "Quality";
                this.dataGridView5.Columns["Colour"].Visible = true;
                this.dataGridView5.Columns["Colour"].DisplayIndex = 3;
                this.dataGridView5.Columns["Colour"].HeaderText = "Colour";
                this.dataGridView5.Columns["Batch_No_Arr"].Visible = true;
                this.dataGridView5.Columns["Batch_No_Arr"].DisplayIndex = 4;
                this.dataGridView5.Columns["Batch_No_Arr"].HeaderText = "Batch Numbers";
                this.dataGridView5.Columns["Net_Batch_Weight"].Visible = true;
                this.dataGridView5.Columns["Net_Batch_Weight"].DisplayIndex = 5;
                this.dataGridView5.Columns["Net_Batch_Weight"].HeaderText = "Net Batch Weight";
                this.dataGridView5.Columns["Carton_No_Production_Arr"].Visible = true;
                this.dataGridView5.Columns["Carton_No_Production_Arr"].DisplayIndex = 6;
                this.dataGridView5.Columns["Carton_No_Production_Arr"].HeaderText = "Carton Numbers";
                this.dataGridView5.Columns["Net_Carton_Weight"].Visible = true;
                this.dataGridView5.Columns["Net_Carton_Weight"].DisplayIndex = 8;
                this.dataGridView5.Columns["Net_Carton_Weight"].HeaderText = "Net Carton Weight";
                c.auto_adjust_dgv(this.dataGridView5);

            }
            if (dgv_index == 6 || dgv_index == -1)
            {
                this.dataGridView6.Columns.OfType<DataGridViewColumn>().ToList().ForEach(col => col.Visible = false);
                this.dataGridView6.Columns["Date_Of_Production"].Visible = true;
                this.dataGridView6.Columns["Date_Of_Production"].DisplayIndex = 1;
                this.dataGridView6.Columns["Date_Of_Production"].HeaderText = "Date of Production";
                this.dataGridView6.Columns["Carton_No"].Visible = true;
                this.dataGridView6.Columns["Carton_No"].DisplayIndex = 2;
                this.dataGridView6.Columns["Carton_No"].HeaderText = "Carton Number";
                this.dataGridView6.Columns["Quality"].Visible = true;
                this.dataGridView6.Columns["Quality"].DisplayIndex = 6;
                this.dataGridView6.Columns["Quality"].HeaderText = "Quality";
                this.dataGridView6.Columns["Batch_No_Arr"].Visible = true;
                this.dataGridView6.Columns["Batch_No_Arr"].DisplayIndex = 7;
                this.dataGridView6.Columns["Batch_No_Arr"].HeaderText = "Batches";
                this.dataGridView6.Columns["Net_Weight"].Visible = true;
                this.dataGridView6.Columns["Net_Weight"].DisplayIndex = 11;
                this.dataGridView6.Columns["Net_Weight"].HeaderText = "Net Weight";
                c.auto_adjust_dgv(this.dataGridView6);
            }
        }
        private DataTable getTwoColSummary(DataTable input, string col1= "Company_Name", string col2= "Quality", int side_cols=1)
        {
            DataTable ans = new DataTable();
            ans.Columns.Add(col1);
            ans.Columns.Add(col2);
            if(side_cols==2)
            {
                ans.Columns.Add("Net Batch Weight");
                ans.Columns.Add("Net Carton Weight");
            }
            else ans.Columns.Add("Net Weight");
        
            //Dictionary<string, bool> companies = new Dictionary<string, bool>();
            DataView dv = input.DefaultView;
            dv.Sort = col1+" desc";
            input = dv.ToTable();
            string prev_col1 = "";
            if (side_cols == 1)
            {
                for (int i = 0; i < input.Rows.Count; i++)
                {
                    string col1_value = input.Rows[i][col1].ToString();
                    string col2_value = input.Rows[i][col2].ToString();
                    float net_weight = float.Parse(input.Rows[i]["Net_Weight"].ToString());
                    if (col1_value == prev_col1)
                    {
                        bool quality_exists = false;
                        for (int j = 0; j < ans.Rows.Count; j++)
                        {
                            string anscompany = ans.Rows[j][col1].ToString();
                            string ansquality = ans.Rows[j][col2].ToString();
                            float ansnet_weight = 0F;
                            if (ans.Rows[j]["Net Weight"].ToString() != "") ansnet_weight = float.Parse(ans.Rows[j]["Net Weight"].ToString());

                            if (anscompany == col1_value && ansquality == col2_value)
                            {
                                ans.Rows[j]["Net Weight"] = ansnet_weight + net_weight;
                                quality_exists = true;
                                break;
                            }
                        }
                        if (quality_exists == false) ans.Rows.Add(col1_value, col2_value, net_weight.ToString("F3"));
                    }
                    else
                    {
                        if (ans.Rows.Count != 0) ans.Rows.Add("", "", "");
                        ans.Rows.Add(col1_value, col2_value, net_weight.ToString("F3"));
                        prev_col1 = col1_value;
                    }
                }
                return ans;
            }
            else if(side_cols==2)
            {
                for (int i = 0; i < input.Rows.Count; i++)
                {
                    string col1_value = input.Rows[i][col1].ToString();
                    string col2_value = input.Rows[i][col2].ToString();
                    float nw1, nw2;
                    nw1 = float.Parse(input.Rows[i]["Net_Batch_Weight"].ToString());
                    nw2 = float.Parse(input.Rows[i]["Net_Carton_Weight"].ToString());
                    if (col1_value == prev_col1)
                    {
                        bool quality_exists = false;
                        for (int j = 0; j < ans.Rows.Count; j++)
                        {
                            string anscompany = ans.Rows[j][col1].ToString();
                            string ansquality = ans.Rows[j][col2].ToString();
                            float ansnw1=0F, ansnw2=0F;
                            if (ans.Rows[j]["Net Batch Weight"].ToString() != "") ansnw1 = float.Parse(ans.Rows[j]["Net Batch Weight"].ToString());
                            if (ans.Rows[j]["Net Carton Weight"].ToString() != "") ansnw2 = float.Parse(ans.Rows[j]["Net Carton Weight"].ToString());
                            if (anscompany == col1_value && ansquality == col2_value)
                            {
                                ans.Rows[j]["Net Batch Weight"] = ansnw1 + nw1;
                                ans.Rows[j]["Net Carton Weight"] = ansnw2 + nw2;
                                quality_exists = true;
                                break;
                            }
                        }
                        if (quality_exists == false) ans.Rows.Add(col1_value, col2_value, nw1.ToString("F3"), nw2.ToString("F3"));
                    }
                    else
                    {
                        if (ans.Rows.Count != 0) ans.Rows.Add("", "", "", "");
                        ans.Rows.Add(col1_value, col2_value, nw1.ToString("F3"), nw2.ToString("F3"));
                        prev_col1 = col1_value;
                    }
                }
            }

            foreach (DataColumn c in ans.Columns)
            {
                c.ColumnName = c.ColumnName.Replace('_', ' ');
            }
            return ans;
        }
        private void deselect_dgvs(object sender, EventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            if (dgv == dataGridView1)
            {
                this.detailsButton.Enabled = false;
                this.backButton.Enabled = false;
            }
            else
            {
                this.detailsButton.Enabled = true;
                this.backButton.Enabled = true;
            }
            if (dataGridView0.SelectedRows.Count != 0 && dataGridView0 != dgv) dataGridView0.SelectedRows[0].Selected = false;
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

        //checkbox
        private void allCompanyCK_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < this.dataGridViewCName.Rows.Count; i++)
            {
                if (this.allCompanyCK.Checked == true)
                {
                    this.dataGridViewCName.Rows[i].Cells[1].Value = true;
                }
                else
                {
                    this.dataGridViewCName.Rows[i].Cells[1].Value = false;
                }
            }
        }
        private void allQualityCK_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < this.dataGridViewQuality.Rows.Count; i++)
            {
                if (this.allQualityCK.Checked == true)
                {
                    this.dataGridViewQuality.Rows[i].Cells[1].Value = true;
                }
                else
                {
                    this.dataGridViewQuality.Rows[i].Cells[1].Value = false;
                }
            }
        }
        private void allColourCK_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < this.dataGridViewCompany.Rows.Count; i++)
            {
                if (this.allColourCK.Checked == true)
                {
                    this.dataGridViewCompany.Rows[i].Cells[1].Value = true;
                }
                else
                {
                    this.dataGridViewCompany.Rows[i].Cells[1].Value = false;
                }
            }
        }

        //buttons
        private void loadButton_Click(object sender, EventArgs e)
        {
            if (prev_load_date != this.dateTimePicker1.Value)
            {
                this.cartons = c.getInventoryCarton(this.dateTimePicker1.Value);
                this.twist_stock = c.getTwistStock2(this.dateTimePicker1.Value);
                this.trays = c.getInventoryTray(this.dateTimePicker1.Value);
                c.printDataTable(trays, "Trays");
                DataTable batch_inv = c.getInventoryBatch(this.dateTimePicker1.Value);
                this.dyeingBatch = batch_inv.Clone(); this.dyeingBatch.Clear();
                this.ConningBatch= batch_inv.Clone(); this.ConningBatch.Clear();
                List<string> semi_batches_vouchers = new List<string>();
                for (int i=0; i<batch_inv.Rows.Count; i++)
                {
                    if(batch_inv.Rows[i]["dyecon"].ToString()=="1")
                    {
                        this.dyeingBatch.Rows.Add(batch_inv.Rows[i].ItemArray);
                    }
                    else
                    {
                        if(batch_inv.Rows[i]["dyecon"].ToString() == "3")
                            this.ConningBatch.Rows.Add(batch_inv.Rows[i].ItemArray);
                        else
                        {
                            Console.WriteLine(batch_inv.Rows[i]["Voucher_ID"].ToString());
                            semi_batches_vouchers.Add(batch_inv.Rows[i]["Voucher_ID"].ToString());
                        }
                    }
                }
                semi_batches_vouchers = semi_batches_vouchers.Distinct().ToList();
                semi_batches_vouchers.Remove("");
                string semi_batches_vouchers_str = "";
                for (int i = 0; i < semi_batches_vouchers.Count; i++)
                {
                    semi_batches_vouchers_str += semi_batches_vouchers[i] + ",";
                    Console.WriteLine("Short "+semi_batches_vouchers[i]);

                }
                if (semi_batches_vouchers_str!="")
                {
                    this.semiproduced = c.getTableData("Carton_Production_Voucher", "*", "Voucher_ID IN (" + c.removecom(semi_batches_vouchers_str) + ")");
                }
                else
                {
                    this.semiproduced = c.getTableData("Carton_Production_Voucher", "*", "Voucher_ID =-1");
                }
                this.cartonproduced = c.getInventoryCartonProduced(this.dateTimePicker1.Value);
            }
            #region
            //Carton datagridview
            DataTable cartons_to_show = this.cartons.Clone();
            for (int i = 0; i < this.cartons.Rows.Count; i++)
            {
                string quality = this.cartons.Rows[i]["Quality"].ToString();
                string company = this.cartons.Rows[i]["Company_Name"].ToString();
                int flag = 0;
                for (int j = 0; j < this.dataGridViewQuality.Rows.Count; j++)
                {
                    if (this.dataGridViewQuality.Rows[j].Cells["Quality Before Twist"].Value.ToString() == quality && Convert.ToBoolean(this.dataGridViewQuality.Rows[j].Cells["Check"].Value) == true)
                    {
                        flag++;
                    }
                }
                for (int j = 0; j < this.dataGridViewCName.Rows.Count; j++)
                {
                    if (this.dataGridViewCName.Rows[j].Cells["Company Names"].Value.ToString() == company && Convert.ToBoolean(this.dataGridViewCName.Rows[j].Cells["Check"].Value) == true)
                    {
                        flag++;
                    }
                }
                if (flag == 2)
                {
                    cartons_to_show.Rows.Add(this.cartons.Rows[i].ItemArray);
                }

            }

            //Twist Datagridview
            DataTable twist_stock_to_show = this.twist_stock.Clone();
            string prev_company = "";
            if(this.twist_stock.Rows.Count>0) prev_company = this.twist_stock.Rows[0]["Company_Name"].ToString();
            
            for (int i = 0; i < this.twist_stock.Rows.Count; i++)
            {
                string quality = this.twist_stock.Rows[i]["Quality"].ToString();
                string company = this.twist_stock.Rows[i]["Company_Name"].ToString();
                if (prev_company != company)
                {
                    twist_stock_to_show.Rows.Add();
                }
                int flag = 0;
                for (int j = 0; j < this.dataGridViewQuality.Rows.Count; j++)
                {
                    if (this.dataGridViewQuality.Rows[j].Cells["Quality Before Twist"].Value.ToString() == quality && Convert.ToBoolean(this.dataGridViewQuality.Rows[j].Cells["Check"].Value) == true)
                    {
                        flag++;
                    }
                }
                for (int j = 0; j < this.dataGridViewCName.Rows.Count; j++)
                {
                    if (this.dataGridViewCName.Rows[j].Cells["Company Names"].Value.ToString() == company && Convert.ToBoolean(this.dataGridViewCName.Rows[j].Cells["Check"].Value) == true)
                    {
                        flag++;
                    }
                }
                if (flag == 2)
                {
                    twist_stock_to_show.Rows.Add(this.twist_stock.Rows[i].ItemArray);
                }
                prev_company = company;
            }

            //Trays datagridview
            DataTable trays_to_show = this.trays.Clone();
            for (int i = 0; i < this.trays.Rows.Count; i++)
            {
                string quality = this.trays.Rows[i]["Quality"].ToString();
                string company = this.trays.Rows[i]["Company_Name"].ToString();
                int flag = 0;
                for (int j = 0; j < this.dataGridViewQuality.Rows.Count; j++)
                {
                    if (this.dataGridViewQuality.Rows[j].Cells["Quality"].Value.ToString() == quality && Convert.ToBoolean(this.dataGridViewQuality.Rows[j].Cells["Check"].Value) == true)
                    {
                        flag++;
                    }
                }
                for (int j = 0; j < this.dataGridViewCName.Rows.Count; j++)
                {
                    if (this.dataGridViewCName.Rows[j].Cells["Company Names"].Value.ToString() == company && Convert.ToBoolean(this.dataGridViewCName.Rows[j].Cells["Check"].Value) == true)
                    {
                        flag++;
                    }
                }
                if (flag == 2)
                {
                    trays_to_show.Rows.Add(this.trays.Rows[i].ItemArray);
                }
            }

            //Dyeing batches Datagridview
            DataTable dyeingBatch_to_show = this.dyeingBatch.Clone();
            for (int i = 0; i < this.dyeingBatch.Rows.Count; i++)
            {
                string quality = this.dyeingBatch.Rows[i]["Quality"].ToString();
                string colour = this.dyeingBatch.Rows[i]["Colour"].ToString();
                string company = this.dyeingBatch.Rows[i]["Company_Name"].ToString();
                int flag = 0;
                for (int j = 0; j < this.dataGridViewQuality.Rows.Count; j++)
                {
                    if (this.dataGridViewQuality.Rows[j].Cells["Quality"].Value.ToString() == quality && Convert.ToBoolean(this.dataGridViewQuality.Rows[j].Cells["Check"].Value) == true)
                    {
                        flag++;
                    }
                }
                for (int j = 0; j < this.dataGridViewCompany.Rows.Count; j++)
                {
                    if (this.dataGridViewCompany.Rows[j].Cells["Colour"].Value.ToString() == colour && Convert.ToBoolean(this.dataGridViewCompany.Rows[j].Cells["Check"].Value) == true)
                    {
                        flag++;
                    }
                }
                for (int j = 0; j < this.dataGridViewCName.Rows.Count; j++)
                {
                    if (this.dataGridViewCName.Rows[j].Cells["Company Names"].Value.ToString() == company && Convert.ToBoolean(this.dataGridViewCName.Rows[j].Cells["Check"].Value) == true)
                    {
                        flag++;
                    }
                }
                if (flag == 3)
                {
                    dyeingBatch_to_show.Rows.Add(this.dyeingBatch.Rows[i].ItemArray);
                }
            }

            //Not conned batches datagridview
            DataTable ConningBatch_to_show = this.ConningBatch.Clone();
            for (int i = 0; i < this.ConningBatch.Rows.Count; i++)
            {
                string quality = this.ConningBatch.Rows[i]["Quality"].ToString();
                string colour = this.ConningBatch.Rows[i]["Colour"].ToString();
                string company = this.ConningBatch.Rows[i]["Company_Name"].ToString();
                int flag = 0;
                for (int j = 0; j < this.dataGridViewQuality.Rows.Count; j++)
                {
                    if (this.dataGridViewQuality.Rows[j].Cells["Quality"].Value.ToString() == quality && Convert.ToBoolean(this.dataGridViewQuality.Rows[j].Cells["Check"].Value) == true)
                    {
                        flag++;
                    }
                }
                for (int j = 0; j < this.dataGridViewCompany.Rows.Count; j++)
                {
                    if (this.dataGridViewCompany.Rows[j].Cells["Colour"].Value.ToString() == colour && Convert.ToBoolean(this.dataGridViewCompany.Rows[j].Cells["Check"].Value) == true)
                    {
                        flag++;
                    }
                }
                for (int j = 0; j < this.dataGridViewCName.Rows.Count; j++)
                {
                    if (this.dataGridViewCName.Rows[j].Cells["Company Names"].Value.ToString() == company && Convert.ToBoolean(this.dataGridViewCName.Rows[j].Cells["Check"].Value) == true)
                    {
                        flag++;
                    }
                }
                if (flag == 3)
                {
                    ConningBatch_to_show.Rows.Add(this.ConningBatch.Rows[i].ItemArray);
                }
            }
            //Semi Produced Datagridview
            DataTable semiproduced_to_show = this.semiproduced.Clone();
            for (int i = 0; i < this.semiproduced.Rows.Count; i++)
            {
                string quality = this.semiproduced.Rows[i]["Quality"].ToString();
                string colour = this.semiproduced.Rows[i]["Colour"].ToString();
                int flag = 0;
                for (int j = 0; j < this.dataGridViewQuality.Rows.Count; j++)
                {
                    if (this.dataGridViewQuality.Rows[j].Cells["Quality"].Value.ToString() == quality && Convert.ToBoolean(this.dataGridViewQuality.Rows[j].Cells["Check"].Value) == true)
                    {
                        flag++;
                    }
                }
                for (int j = 0; j < this.dataGridViewCompany.Rows.Count; j++)
                {
                    if (this.dataGridViewCompany.Rows[j].Cells["Colour"].Value.ToString() == colour && Convert.ToBoolean(this.dataGridViewCompany.Rows[j].Cells["Check"].Value) == true)
                    {
                        flag++;
                    }
                }
                if (flag == 2)
                {
                    semiproduced_to_show.Rows.Add(this.semiproduced.Rows[i].ItemArray);
                }
            }

            //Cartons Produced Datagridview
            DataTable cartonproduced_to_show = this.cartonproduced.Clone();
            for (int i = 0; i < this.cartonproduced.Rows.Count; i++)
            {
                string quality = this.cartonproduced.Rows[i]["Quality"].ToString();
                string colour = this.cartonproduced.Rows[i]["Colour"].ToString();
                int flag = 0;
                for (int j = 0; j < this.dataGridViewQuality.Rows.Count; j++)
                {
                    if (this.dataGridViewQuality.Rows[j].Cells["Quality"].Value.ToString() == quality && Convert.ToBoolean(this.dataGridViewQuality.Rows[j].Cells["Check"].Value) == true)
                    {
                        flag++;
                    }
                }
                for (int j = 0; j < this.dataGridViewCompany.Rows.Count; j++)
                {
                    if (this.dataGridViewCompany.Rows[j].Cells["Colour"].Value.ToString() == colour && Convert.ToBoolean(this.dataGridViewCompany.Rows[j].Cells["Check"].Value) == true)
                    {
                        flag++;
                    }
                }
                if (flag == 2)
                {
                    cartonproduced_to_show.Rows.Add(this.cartonproduced.Rows[i].ItemArray);
                }
            }
            #endregion

            this.to_show_summary[0] = this.getTwoColSummary(cartons_to_show);
            dataGridView0.DataSource = this.to_show_summary[0];
            set_summary_column_widths(dataGridView0);
            this.to_show_details[0] = cartons_to_show;

            this.to_show_summary[1] = twist_stock_to_show;
            dataGridView1.DataSource = twist_stock_to_show;
            set_summary_column_widths(dataGridView1);
            this.to_show_details[1] = twist_stock_to_show;

            this.to_show_summary[2] = this.getTwoColSummary(trays_to_show);
            dataGridView2.DataSource = this.to_show_summary[2];
            set_summary_column_widths(dataGridView2);
            this.to_show_details[2] = trays_to_show;

            this.to_show_summary[3] = this.getTwoColSummary(dyeingBatch_to_show);
            dataGridView3.DataSource = this.to_show_summary[3];
            set_summary_column_widths(dataGridView3);
            this.to_show_details[3] = dyeingBatch_to_show;

            this.to_show_summary[4] = this.getTwoColSummary(ConningBatch_to_show);
            dataGridView4.DataSource = this.to_show_summary[4];
            set_summary_column_widths(dataGridView4);
            this.to_show_details[4] = ConningBatch_to_show;
            
            this.to_show_summary[5] = this.getTwoColSummary(semiproduced_to_show, "Quality", "Colour", 2);
            dataGridView5.DataSource = this.to_show_summary[5];
            set_summary_column_widths(dataGridView5);
            this.to_show_details[5] = semiproduced_to_show;

            this.to_show_summary[6] = this.getTwoColSummary(cartonproduced_to_show, "Quality", "Colour");
            dataGridView6.DataSource = this.to_show_summary[6];
            set_summary_column_widths(dataGridView6);
            this.to_show_details[6] = cartonproduced_to_show;


            //state 0= summary
            //state 1= details
            for (int i = 0; i < this.dgvstates.Length; i++) this.dgvstates[i] = 0;
            
            var dgvs = panel1.Controls
              .OfType<DataGridView>()
              .Where(x => x.Name.StartsWith("dataGridView"));

            foreach (var dgv in dgvs)
            {
                if (dgv.SelectedRows.Count != 0)
                {
                    dgv.SelectedRows[0].Selected = false;
                }
            }
            prev_load_date = this.dateTimePicker1.Value;
        }
        private void detailsButton_Click(object sender, EventArgs e)
        {
            //view details form or details
            if(this.dataGridView0.SelectedRows.Count!=0)
            {
                if (this.dgvstates[0] == 1)
                {
                    Display_Carton dc = new Display_Carton((dataGridView0.Rows[dataGridView0.SelectedRows[0].Index].DataBoundItem as DataRowView).Row);
                    dc.Show();
                }
                else
                {
                    dataGridView0.DataSource = this.to_show_details[0];
                    this.dgvstates[0] = 1;
                    set_details_column_widths(0);
                }
            }
            else if (this.dataGridView2.SelectedRows.Count != 0)
            {
                if (this.dgvstates[2] == 1)
                {
                    Display_Tray dt = new Display_Tray((dataGridView2.Rows[dataGridView2.SelectedRows[0].Index].DataBoundItem as DataRowView).Row);
                    dt.Show();
                }
                else
                {
                    dataGridView2.DataSource = this.to_show_details[2];
                    this.dgvstates[2] = 1;
                    set_details_column_widths(2);
                }
            }
            else if (this.dataGridView3.SelectedRows.Count!= 0)
            {
                if (this.dgvstates[3] == 1)
                {
                    Display_Batch db = new Display_Batch((dataGridView3.Rows[dataGridView3.SelectedRows[0].Index].DataBoundItem as DataRowView).Row);
                    db.Show();
                }
                else
                {
                    dataGridView3.DataSource = this.to_show_details[3];
                    this.dgvstates[3] = 1;
                    set_details_column_widths(3);
                }
            }
            else if (this.dataGridView4.SelectedRows.Count!=0)
            {
                if (this.dgvstates[4] == 1)
                {
                    Display_Batch db = new Display_Batch((dataGridView4.Rows[dataGridView4.SelectedRows[0].Index].DataBoundItem as DataRowView).Row);
                    db.Show();
                }
                else
                {
                    dataGridView4.DataSource = this.to_show_details[4];
                    this.dgvstates[4] = 1;
                    set_details_column_widths(4);
                }
            }
            else if (this.dataGridView5.SelectedRows.Count != 0)
            {
                if (this.dgvstates[5] == 1)
                { 
                    DataRow row = (dataGridView5.Rows[dataGridView5.SelectedRows[0].Index].DataBoundItem as DataRowView).Row;
                    M_V3_cartonProductionForm f= new M_V3_cartonProductionForm(row, false, new M_V_history(1));
                    f.deleteButton.Visible = false;
                    f.Show();
                }
                else
                {
                    dataGridView5.DataSource = this.to_show_details[5];
                    set_details_column_widths(5);
                    this.dgvstates[5] = 1;
                }
            }
            else if (this.dataGridView6.SelectedRows.Count!= 0)
            {
                if (this.dgvstates[6] == 1)
                {
                    Display_Carton_Produced dcp = new Display_Carton_Produced((dataGridView6.Rows[dataGridView6.SelectedRows[0].Index].DataBoundItem as DataRowView).Row);
                    dcp.Show();
                }
                else
                {
                    dataGridView6.DataSource = this.to_show_details[6];
                    set_details_column_widths(6);
                    this.dgvstates[6] = 1;
                }
            }
        }
        private void backButton_Click(object sender, EventArgs e)
        {
            //switch to summary
            if (this.dataGridView0.SelectedRows.Count != 0)
            {
                if (this.dgvstates[0] == 1)
                {
                    dataGridView0.DataSource = this.to_show_summary[0];
                    this.dgvstates[0] = 0;
                    set_summary_column_widths(dataGridView0);
                }
            }
            else if (this.dataGridView2.SelectedRows.Count != 0)
            {
                if (this.dgvstates[2] == 1)
                {
                    dataGridView2.DataSource = this.to_show_summary[2];
                    this.dgvstates[2] = 0;
                    set_summary_column_widths(dataGridView2);
                }
            }
            else if (this.dataGridView3.SelectedRows.Count != 0)
            {
                if (this.dgvstates[3] == 1)
                {
                    dataGridView3.DataSource = this.to_show_summary[3];
                    this.dgvstates[3] = 0;
                    set_summary_column_widths(dataGridView3);
                }
            }
            else if (this.dataGridView4.SelectedRows.Count != 0)
            {
                if (this.dgvstates[4] == 1)
                {
                    dataGridView4.DataSource = this.to_show_summary[4];
                    this.dgvstates[4] = 0;
                    set_summary_column_widths(dataGridView4);
                }
            }
            else if (this.dataGridView5.SelectedRows.Count != 0)
            {
                if (this.dgvstates[5] == 1)
                { 
                    dataGridView5.DataSource = this.to_show_summary[5];
                    set_summary_column_widths(dataGridView5);
                    this.dgvstates[5] = 0;
                }
            }
            else if (this.dataGridView6.SelectedRows.Count != 0)
            {
                if (this.dgvstates[6] == 1)
                {
                    dataGridView6.DataSource = this.to_show_summary[6];
                    set_summary_column_widths(dataGridView6);
                    this.dgvstates[6] = 0;
                }
            }
        }

    }
}
