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
    public partial class M_V_history : Form
    {
        DbConnect c;
        DataTable dt;
        private int vno = 0;

        public M_V_history(int vno)
        {
            InitializeComponent();
            this.c = new DbConnect();
            this.dt = new DataTable();
            this.vno = vno;
            loadData();
        }

        private void viewDetailsButton_Click(object sender, EventArgs e)
        {
            int index = this.dataGridView1.SelectedRows[0].Index;
            Console.WriteLine(index);
            if (index > this.dataGridView1.Rows.Count - 1)
            {
                MessageBox.Show("Please select valid voucher", "Error");
            }
            else
            {
                DataRow row = (dataGridView1.Rows[index].DataBoundItem as DataRowView).Row;
                if(this.vno==1)
                {
                    M_V1_cartonInwardForm f = new M_V1_cartonInwardForm(row, false, this);
                    f.Show();
                }
                if (this.vno == 2)
                {
                    M_V1_cartonTwistForm f = new M_V1_cartonTwistForm(row, false, this);
                    f.Show();
                }
                if(this.vno == 3)
                {
                    M_V1_cartonSalesForm f = new M_V1_cartonSalesForm(row, false, this);
                    f.Show();
                }
                if(this.vno==4)
                {
                    M_V2_trayInputForm f = new M_V2_trayInputForm(row, false, this);
                    f.Show();
                }
                if(this.vno==5)
                {
                    M_V2_dyeingIssueForm f = new M_V2_dyeingIssueForm(row, false, this);
                    f.Show();
                }
                if (this.vno == 6)
                {
                    M_V2_dyeingInwardForm f = new M_V2_dyeingInwardForm(row, false, this, "dyeingInward");
                    f.Show();
                }
                if (this.vno == 7)
                {
                    M_V2_dyeingInwardForm f = new M_V2_dyeingInwardForm(row, false, this, "addBill");
                    f.Show();
                }
                if(this.vno == 8)
                {
                    M_V3_cartonProductionForm f = new M_V3_cartonProductionForm(row, false, this);
                    f.Show();
                }
            }
        }
        private void editDetailsButton_Click(object sender, EventArgs e)
        {
            int index = this.dataGridView1.SelectedRows[0].Index;
            if(index > this.dataGridView1.Rows.Count-1)
            {
                MessageBox.Show("Please select valid voucher", "Error");
            }
            else
            {
                DataRow row = (dataGridView1.Rows[index].DataBoundItem as DataRowView).Row;
                if (this.vno == 1)
                {
                    M_V1_cartonInwardForm f = new M_V1_cartonInwardForm(row, true, this);
                    f.Show();
                }
                if (this.vno == 2)
                {
                    M_V1_cartonTwistForm f = new M_V1_cartonTwistForm(row, true, this);
                    f.Show();
                }
                if(this.vno==3)
                {
                    M_V1_cartonSalesForm f = new M_V1_cartonSalesForm(row, true, this);
                    f.Show();
                }
                if (this.vno == 4)
                {
                    M_V2_trayInputForm f = new M_V2_trayInputForm(row, true, this);
                    f.Show();
                }
                if (this.vno == 5)
                {
                    M_V2_dyeingIssueForm f = new M_V2_dyeingIssueForm(row, true, this);
                    f.Show();
                }
                if (this.vno == 6)
                {
                    M_V2_dyeingInwardForm f = new M_V2_dyeingInwardForm(row, true, this, "dyeingInward");
                    f.Show();
                }
                if (this.vno == 7)
                {
                    M_V2_dyeingInwardForm f = new M_V2_dyeingInwardForm(row, true, this, "addBill");
                    f.Show();
                }
                if (this.vno == 8)
                {
                    M_V3_cartonProductionForm f = new M_V3_cartonProductionForm(row, true, this);
                    f.Show();
                }
            }
        }
        
        public void loadData()
        {
            if (this.vno == 1)
            {
                //this.dt = c.getCartonVoucherHistory();
                this.dt = c.getVoucherHistories("Carton_Voucher");
                this.dataGridView1.ReadOnly = true;
                this.dataGridView1.DataSource = dt;
                this.dataGridView1.Columns["Quality"].Visible = false;
                this.dataGridView1.Columns["Quality_Arr"].Visible = false;
                this.dataGridView1.Columns["Carton_No_Arr"].Visible = false;
                this.dataGridView1.Columns["Carton_Weight_Arr"].Visible = false;
                this.dataGridView1.Columns["Buy_Cost"].Visible = false;
            }
            if (this.vno == 2)
            {
                //this.dt = c.getTwistVoucherHistory();
                this.dt = c.getVoucherHistories("Twist_Voucher");
                this.dataGridView1.ReadOnly = true;
                this.dataGridView1.DataSource = dt;
                //this.dataGridView1.Columns["Quality"].Visible = false;
                //this.dataGridView1.Columns["Quality_Arr"].Visible = false;
                this.dataGridView1.Columns["Carton_No_Arr"].Visible = false;
                //this.dataGridView1.Columns["Carton_Weight_Arr"].Visible = false;
                //this.dataGridView1.Columns["Buy_Cost"].Visible = false;
                //this.dataGridView1.Columns["Sell_Cost"].Visible = false;
            }
            if (this.vno == 3)
            {
                //this.dt = c.getSalesVoucherHistory();
                this.dt = c.getVoucherHistories("Sales_Voucher");
                this.dataGridView1.ReadOnly = true;
                this.dataGridView1.DataSource = dt;
                this.dataGridView1.Columns["Carton_No_Arr"].Visible = false;
            }
            if (this.vno == 4)
            {
                //this.dt = c.getTrayVoucherHistory();
                this.dt = c.getVoucherHistories("Tray_Voucher");
                this.dataGridView1.ReadOnly = true;
                this.dataGridView1.DataSource = dt;
                this.dataGridView1.Columns["Voucher_ID"].Visible = false;
            }
            if (this.vno == 5)
            {
                //this.dt = c.getDyeingIssueVoucherHistory();
                this.dt = c.getVoucherHistories("Dyeing_Issue_Voucher");
                this.dataGridView1.ReadOnly = true;
                this.dataGridView1.DataSource = dt;
                this.dataGridView1.Columns["Voucher_ID"].Visible = false;
            }
            if (this.vno == 6)
            {
                this.dt = c.getVoucherHistories("Dyeing_Inward_Voucher");
                this.dataGridView1.ReadOnly = true;
                this.dataGridView1.DataSource = dt;
                this.dataGridView1.Columns["Voucher_ID"].Visible = false;
            }
            if (this.vno == 7)
            {
                this.dt = c.getVoucherHistories("BillNos_Voucher");
                this.dataGridView1.ReadOnly = true;
                this.dataGridView1.DataSource = dt;
                this.dataGridView1.Columns["Voucher_ID"].Visible = false;
            }
            if (this.vno == 8)
            {
                this.dt = c.getVoucherHistories("Carton_Production_Voucher");
                this.dataGridView1.ReadOnly = true;
                this.dataGridView1.DataSource = dt;
                this.dataGridView1.Columns["Voucher_ID"].Visible = false;
                if (this.vno == 8 && dataGridView1.Rows.Count >= 1)
                {
                    if (dataGridView1.Rows[0].Cells[10].Value.ToString() == "1")
                    {
                        this.editDetailsButton.Enabled = false;
                    }
                    else
                    {
                        this.editDetailsButton.Enabled = true;
                    }
                }
            }


        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if(this.vno==8 && dataGridView1.CurrentRow.Index>=0)
            {
                Console.WriteLine(dataGridView1.CurrentRow.Cells[10].Value.ToString());
                if(dataGridView1.CurrentRow.Cells[10].Value.ToString()=="1")
                {
                    this.editDetailsButton.Enabled = false;
                }
                else
                {
                    this.editDetailsButton.Enabled = true;
                }
            }
           
        }
    }
}
