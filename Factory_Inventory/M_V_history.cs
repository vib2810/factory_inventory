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
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.V)
            {
                this.viewDetailsButton.PerformClick(); ;
                return false;
            }
            if (keyData == Keys.E)
            {
                this.editDetailsButton.PerformClick();
                return false;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        public M_V_history(int vno)
        {
            InitializeComponent();
            this.c = new DbConnect();
            this.dt = new DataTable();
            this.vno = vno;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView1.Font, FontStyle.Bold);
            this.label1.Visible = false;
            loadData();
        }

        private void viewDetailsButton_Click(object sender, EventArgs e)
        {
            int index = this.dataGridView1.SelectedRows[0].Index;
            Console.WriteLine(index);
            if (index > this.dataGridView1.Rows.Count - 1)
            {
                c.ErrorBox("Please select valid voucher", "Error");
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
                    M_VC_cartonSalesForm f = new M_VC_cartonSalesForm(row, false, this, "Carton");
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
                if (this.vno == 9)
                {
                    M_VC_cartonSalesForm f = new M_VC_cartonSalesForm(row, false, this, "Carton_Produced");
                    f.Show();
                }
                if(this.vno==10)
                {
                    M_VC_addBill f = new M_VC_addBill(row, false, this, "Carton");
                    f.Show();
                }
                if (this.vno == 11)
                {
                    M_VC_addBill f = new M_VC_addBill(row, false, this, "Carton_Produced");
                    f.Show();
                }
                if (this.vno == 12)
                {
                    M_V3_issueToReDyeingForm f = new M_V3_issueToReDyeingForm(row, false, this);
                    f.Show();
                }
            }
        }
        private void editDetailsButton_Click(object sender, EventArgs e)
        {
            int index = this.dataGridView1.SelectedRows[0].Index;
            if(index > this.dataGridView1.Rows.Count-1)
            {
                c.ErrorBox("Please select valid voucher", "Error");
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
                    M_VC_cartonSalesForm f = new M_VC_cartonSalesForm(row, true, this, "Carton");
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
                if (this.vno == 9)
                {
                    M_VC_cartonSalesForm f = new M_VC_cartonSalesForm(row, true, this, "Carton_Produced");
                    f.Show();
                }
                if (this.vno == 10)
                {
                    M_VC_addBill f = new M_VC_addBill(row, true, this, "Carton");
                    f.Show();
                }
                if (this.vno == 11)
                {
                    M_VC_addBill f = new M_VC_addBill(row, true, this, "Carton_Produced");
                    f.Show();
                }
                if (this.vno == 12)
                {
                    M_V3_issueToReDyeingForm f = new M_V3_issueToReDyeingForm(row, true, this);
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
                this.dataGridView1.Columns.OfType<DataGridViewColumn>().ToList().ForEach(col => col.Visible = false);
                this.dataGridView1.Columns["Bill_No"].Visible = true;
                this.dataGridView1.Columns["Bill_No"].DisplayIndex = 0;
                this.dataGridView1.Columns["Bill_No"].HeaderText = "Bill Number";
                this.dataGridView1.Columns["Date_Of_Billing"].Visible = true;
                this.dataGridView1.Columns["Date_Of_Billing"].DisplayIndex = 2;
                this.dataGridView1.Columns["Date_Of_Billing"].HeaderText = "Bill Date";
                this.dataGridView1.Columns["Company_Name"].Visible = true;
                this.dataGridView1.Columns["Company_Name"].DisplayIndex = 4;
                this.dataGridView1.Columns["Company_Name"].HeaderText = "Company Name";
                this.dataGridView1.Columns["Net_Weight"].Visible = true;
                this.dataGridView1.Columns["Net_Weight"].DisplayIndex = 6;
                this.dataGridView1.Columns["Net_Weight"].HeaderText = "Net Weight";
                this.dataGridView1.Columns["Number_of_Cartons"].Visible = true;
                this.dataGridView1.Columns["Number_of_Cartons"].DisplayIndex = 8;
                this.dataGridView1.Columns["Number_of_Cartons"].HeaderText = "Number of Cartons";
                this.dataGridView1.Columns["Fiscal_Year"].Visible = true;
                this.dataGridView1.Columns["Fiscal_Year"].DisplayIndex = 10;
                this.dataGridView1.Columns["Fiscal_Year"].HeaderText = "Financial Year of Carton";
                c.auto_adjust_dgv(this.dataGridView1);


            }
            if (this.vno == 2)
            {
                //this.dt = c.getTwistVoucherHistory();
                this.dt = c.getVoucherHistories("Twist_Voucher");
                this.dataGridView1.ReadOnly = true;
                this.dataGridView1.DataSource = dt;
                this.dataGridView1.Columns.OfType<DataGridViewColumn>().ToList().ForEach(col => col.Visible = false);
                this.dataGridView1.Columns["Quality"].Visible = true;
                this.dataGridView1.Columns["Quality"].DisplayIndex = 0;
                this.dataGridView1.Columns["Quality"].HeaderText = "Quality";
                this.dataGridView1.Columns["Date_Of_Issue"].Visible = true;
                this.dataGridView1.Columns["Date_Of_Issue"].DisplayIndex = 2;
                this.dataGridView1.Columns["Date_Of_Issue"].HeaderText = "Issue Date";
                this.dataGridView1.Columns["Company_Name"].Visible = true;
                this.dataGridView1.Columns["Company_Name"].DisplayIndex = 4;
                this.dataGridView1.Columns["Company_Name"].HeaderText = "Company Name";
                this.dataGridView1.Columns["Number_of_Cartons"].Visible = true;
                this.dataGridView1.Columns["Number_of_Cartons"].DisplayIndex = 6;
                this.dataGridView1.Columns["Number_of_Cartons"].HeaderText = "Number of Cartons";
                this.dataGridView1.Columns["Fiscal_Year"].Visible = true;
                this.dataGridView1.Columns["Fiscal_Year"].DisplayIndex = 8;
                this.dataGridView1.Columns["Fiscal_Year"].HeaderText = "Issue Financial Year";
                c.auto_adjust_dgv(this.dataGridView1);

            }
            if (this.vno == 3)
            {
                //this.dt = c.getSalesVoucherHistory();
                this.dt = c.getVoucherHistories("Sales_Voucher");
                this.dataGridView1.ReadOnly = true;
                this.remove_sales_rows();
                this.dataGridView1.DataSource = dt;
                this.dataGridView1.Columns.OfType<DataGridViewColumn>().ToList().ForEach(col => col.Visible = false);
                this.dataGridView1.Columns["Date_Of_Sale"].Visible = true;
                this.dataGridView1.Columns["Date_Of_Sale"].DisplayIndex = 0;
                this.dataGridView1.Columns["Date_Of_Sale"].HeaderText = "Sale Date";
                this.dataGridView1.Columns["Sale_DO_No"].Visible = true;
                this.dataGridView1.Columns["Sale_DO_No"].DisplayIndex = 2;
                this.dataGridView1.Columns["Sale_DO_No"].HeaderText = "DO Number";
                this.dataGridView1.Columns["Customer"].Visible = true;
                this.dataGridView1.Columns["Customer"].DisplayIndex = 4;
                this.dataGridView1.Columns["Customer"].HeaderText = "Party";
                this.dataGridView1.Columns["Quality"].Visible = true;
                this.dataGridView1.Columns["Quality"].DisplayIndex = 6;
                this.dataGridView1.Columns["Quality"].HeaderText = "Quality";
                this.dataGridView1.Columns["Net_Weight"].Visible = true;
                this.dataGridView1.Columns["Net_Weight"].DisplayIndex = 8;
                this.dataGridView1.Columns["Net_Weight"].HeaderText = "Net Weight";
                this.dataGridView1.Columns["Sale_Rate"].Visible = true;
                this.dataGridView1.Columns["Sale_Rate"].DisplayIndex = 10;
                this.dataGridView1.Columns["Sale_Rate"].HeaderText = "Sale Rate";
                this.dataGridView1.Columns["Sale_Bill_Date"].Visible = true;
                this.dataGridView1.Columns["Sale_Bill_Date"].DisplayIndex = 12;
                this.dataGridView1.Columns["Sale_Bill_Date"].HeaderText = "Bill Date";
                this.dataGridView1.Columns["Sale_Bill_No"].Visible = true;
                this.dataGridView1.Columns["Sale_Bill_No"].DisplayIndex = 14;
                this.dataGridView1.Columns["Sale_Bill_No"].HeaderText = "Bill Number";
                c.auto_adjust_dgv(this.dataGridView1);
            }
            if (this.vno == 4)
            {
                //this.dt = c.getTrayVoucherHistory();
                this.dt = c.getVoucherHistories("Tray_Voucher");
                this.dataGridView1.ReadOnly = true;
                this.dataGridView1.DataSource = dt;
                this.dataGridView1.Columns.OfType<DataGridViewColumn>().ToList().ForEach(col => col.Visible = false);
                this.dataGridView1.Columns["Tray_Production_Date"].Visible = true;
                this.dataGridView1.Columns["Tray_Production_Date"].DisplayIndex = 0;
                this.dataGridView1.Columns["Tray_Production_Date"].HeaderText = "Production Date";
                this.dataGridView1.Columns["Tray_No"].Visible = true;
                this.dataGridView1.Columns["Tray_No"].DisplayIndex = 2;
                this.dataGridView1.Columns["Tray_No"].HeaderText = "Tray Number";
                this.dataGridView1.Columns["Tray_Production_Date"].Visible = true;
                this.dataGridView1.Columns["Tray_Production_Date"].DisplayIndex = 4;
                this.dataGridView1.Columns["Tray_Production_Date"].HeaderText = "Production Date";
                this.dataGridView1.Columns["Quality"].Visible = true;
                this.dataGridView1.Columns["Quality"].DisplayIndex = 6;
                this.dataGridView1.Columns["Quality"].HeaderText = "Quality";
                this.dataGridView1.Columns["Net_Weight"].Visible = true;
                this.dataGridView1.Columns["Net_Weight"].DisplayIndex = 8;
                this.dataGridView1.Columns["Net_Weight"].HeaderText = "Net Weight";
                this.dataGridView1.Columns["Machine_No"].Visible = true;
                this.dataGridView1.Columns["Machine_No"].DisplayIndex = 10;
                this.dataGridView1.Columns["Machine_No"].HeaderText = "Machine Number";
                c.auto_adjust_dgv(this.dataGridView1);
            }
            if (this.vno == 5)
            {
                //this.dt = c.getDyeingIssueVoucherHistory();
                this.dt = c.getVoucherHistories("Dyeing_Issue_Voucher");
                this.dataGridView1.ReadOnly = true;
                this.dataGridView1.DataSource = dt;
                this.dataGridView1.Columns.OfType<DataGridViewColumn>().ToList().ForEach(col => col.Visible = false);
                this.dataGridView1.Columns["Batch_No"].Visible = true;
                this.dataGridView1.Columns["Batch_No"].DisplayIndex = 0;
                this.dataGridView1.Columns["Batch_No"].HeaderText = "Batch Number";
                this.dataGridView1.Columns["Date_Of_Issue"].Visible = true;
                this.dataGridView1.Columns["Date_Of_Issue"].DisplayIndex = 2;
                this.dataGridView1.Columns["Date_Of_Issue"].HeaderText = "Dyeing Issue Date";
                this.dataGridView1.Columns["Quality"].Visible = true;
                this.dataGridView1.Columns["Quality"].DisplayIndex = 4;
                this.dataGridView1.Columns["Quality"].HeaderText = "Quality";
                this.dataGridView1.Columns["Colour"].Visible = true;
                this.dataGridView1.Columns["Colour"].DisplayIndex = 6;
                this.dataGridView1.Columns["Colour"].HeaderText = "Colour";
                this.dataGridView1.Columns["Dyeing_Company_Name"].Visible = true;
                this.dataGridView1.Columns["Dyeing_Company_Name"].DisplayIndex = 8;
                this.dataGridView1.Columns["Dyeing_Company_Name"].HeaderText = "Dyeing Company Name";
                this.dataGridView1.Columns["Dyeing_Rate"].Visible = true;
                this.dataGridView1.Columns["Dyeing_Rate"].DisplayIndex = 10;
                this.dataGridView1.Columns["Dyeing_Rate"].HeaderText = "Rate";
                c.auto_adjust_dgv(this.dataGridView1);

            }
            if (this.vno == 6)
            {
                this.dt = c.getVoucherHistories("Dyeing_Inward_Voucher");
                this.dataGridView1.ReadOnly = true;
                this.dataGridView1.DataSource = dt;
                this.dataGridView1.Columns.OfType<DataGridViewColumn>().ToList().ForEach(col => col.Visible = false);
                this.dataGridView1.Columns["Inward_Date"].Visible = true;
                this.dataGridView1.Columns["Inward_Date"].DisplayIndex = 0;
                this.dataGridView1.Columns["Inward_Date"].HeaderText = "Inward Date";
                this.dataGridView1.Columns["Dyeing_Company_Name"].Visible = true;
                this.dataGridView1.Columns["Dyeing_Company_Name"].DisplayIndex = 2;
                this.dataGridView1.Columns["Dyeing_Company_Name"].HeaderText = "Dyeing Company Name";
                this.dataGridView1.Columns["Bill_No"].Visible = true;
                this.dataGridView1.Columns["Bill_No"].DisplayIndex = 4;
                this.dataGridView1.Columns["Bill_No"].HeaderText = "Bill Number";
                this.dataGridView1.Columns["Bill_Date"].Visible = true;
                this.dataGridView1.Columns["Bill_Date"].DisplayIndex = 6;
                this.dataGridView1.Columns["Bill_Date"].HeaderText = "Bill Date";
                this.label1.Visible = true;
                c.auto_adjust_dgv(this.dataGridView1);

            }
            if (this.vno == 7)
            {
                this.dt = c.getVoucherHistories("BillNos_Voucher");
                this.dataGridView1.ReadOnly = true;
                this.dataGridView1.DataSource = dt;
                this.dataGridView1.Columns.OfType<DataGridViewColumn>().ToList().ForEach(col => col.Visible = false);
                this.dataGridView1.Columns["Dyeing_Company_Name"].Visible = true;
                this.dataGridView1.Columns["Dyeing_Company_Name"].DisplayIndex = 0;
                this.dataGridView1.Columns["Dyeing_Company_Name"].HeaderText = "Dyeing Company Name";
                this.dataGridView1.Columns["Bill_No"].Visible = true;
                this.dataGridView1.Columns["Bill_No"].DisplayIndex = 2;
                this.dataGridView1.Columns["Bill_No"].HeaderText = "Bill Number";
                this.dataGridView1.Columns["Bill_Date"].Visible = true;
                this.dataGridView1.Columns["Bill_Date"].DisplayIndex = 4;
                this.dataGridView1.Columns["Bill_Date"].HeaderText = "Bill Date";
                c.auto_adjust_dgv(this.dataGridView1);
            }
            if (this.vno == 8)
            {
                this.dt = c.getVoucherHistories("Carton_Production_Voucher");
                this.dataGridView1.ReadOnly = true;
                this.dataGridView1.DataSource = dt;
                this.dataGridView1.Columns["Voucher_ID"].Visible = false;
                if (this.vno == 8 && dataGridView1.Rows.Count >= 1)
                {
                    if (dataGridView1.Rows[0].Cells["Voucher_Closed"].Value.ToString() == "1")
                    {
                        this.editDetailsButton.Enabled = false;
                    }
                    else
                    {
                        this.editDetailsButton.Enabled = true;
                    }
                }
                this.dataGridView1.Columns.OfType<DataGridViewColumn>().ToList().ForEach(col => col.Visible = false);
                this.dataGridView1.Columns["Date_Of_Production"].Visible = true;
                this.dataGridView1.Columns["Date_Of_Production"].DisplayIndex = 0;
                this.dataGridView1.Columns["Date_Of_Production"].HeaderText = "Production Date";
                this.dataGridView1.Columns["Quality"].Visible = true;
                this.dataGridView1.Columns["Quality"].DisplayIndex = 2;
                this.dataGridView1.Columns["Quality"].HeaderText = "Quality";
                this.dataGridView1.Columns["Net_Batch_Weight"].Visible = true;
                this.dataGridView1.Columns["Net_Batch_Weight"].DisplayIndex = 4;
                this.dataGridView1.Columns["Net_Batch_Weight"].HeaderText = "Net Batch Weight";
                this.dataGridView1.Columns["Net_Carton_Weight"].Visible = true;
                this.dataGridView1.Columns["Net_Carton_Weight"].DisplayIndex = 6;
                this.dataGridView1.Columns["Net_Carton_Weight"].HeaderText = "Net Carton Weight";
                this.dataGridView1.Columns["Oil_Gain"].Visible = true;
                this.dataGridView1.Columns["Oil_Gain"].DisplayIndex = 8;
                this.dataGridView1.Columns["Oil_Gain"].HeaderText = "Oil Gain";
                this.dataGridView1.Columns["Voucher_Closed"].Visible = true;
                this.dataGridView1.Columns["Voucher_Closed"].DisplayIndex = 10;
                this.dataGridView1.Columns["Voucher_Closed"].HeaderText = "Batches Closed";
                this.label1.Text = "Batches Closed:\n1: Closed\n0: Open";
                this.label1.Visible = true;
                c.auto_adjust_dgv(this.dataGridView1);

            }
            if (this.vno == 9)
            {
                //this.dt = c.getSalesVoucherHistory();
                this.dt = c.getVoucherHistories("Sales_Voucher");
                this.dataGridView1.ReadOnly = true;
                this.remove_sales_rows();
                this.dataGridView1.DataSource = dt;
                this.dataGridView1.Columns.OfType<DataGridViewColumn>().ToList().ForEach(col => col.Visible = false);
                this.dataGridView1.Columns["Date_Of_Sale"].Visible = true;
                this.dataGridView1.Columns["Date_Of_Sale"].DisplayIndex = 0;
                this.dataGridView1.Columns["Date_Of_Sale"].HeaderText = "Sale Date";
                this.dataGridView1.Columns["Sale_DO_No"].Visible = true;
                this.dataGridView1.Columns["Sale_DO_No"].DisplayIndex = 2;
                this.dataGridView1.Columns["Sale_DO_No"].HeaderText = "DO Number";
                this.dataGridView1.Columns["Customer"].Visible = true;
                this.dataGridView1.Columns["Customer"].DisplayIndex = 4;
                this.dataGridView1.Columns["Customer"].HeaderText = "Party";
                this.dataGridView1.Columns["Quality"].Visible = true;
                this.dataGridView1.Columns["Quality"].DisplayIndex = 6;
                this.dataGridView1.Columns["Quality"].HeaderText = "Quality";
                this.dataGridView1.Columns["Net_Weight"].Visible = true;
                this.dataGridView1.Columns["Net_Weight"].DisplayIndex = 8;
                this.dataGridView1.Columns["Net_Weight"].HeaderText = "Net Weight";
                this.dataGridView1.Columns["Sale_Rate"].Visible = true;
                this.dataGridView1.Columns["Sale_Rate"].DisplayIndex = 10;
                this.dataGridView1.Columns["Sale_Rate"].HeaderText = "Sale Rate";
                this.dataGridView1.Columns["Sale_Bill_Date"].Visible = true;
                this.dataGridView1.Columns["Sale_Bill_Date"].DisplayIndex = 12;
                this.dataGridView1.Columns["Sale_Bill_Date"].HeaderText = "Bill Date";
                this.dataGridView1.Columns["Sale_Bill_No"].Visible = true;
                this.dataGridView1.Columns["Sale_Bill_No"].DisplayIndex = 14;
                this.dataGridView1.Columns["Sale_Bill_No"].HeaderText = "Bill Number";
                c.auto_adjust_dgv(this.dataGridView1);
            }
            if (this.vno == 10)
            {
                this.dt = c.getVoucherHistories("SalesBillNos_Voucher");
                this.dataGridView1.ReadOnly = true;
                this.dataGridView1.DataSource = this.remove_sales_rows();
                this.dataGridView1.Columns.OfType<DataGridViewColumn>().ToList().ForEach(col => col.Visible = false);
                this.dataGridView1.Columns["Sale_Bill_Date"].Visible = true;
                this.dataGridView1.Columns["Sale_Bill_Date"].DisplayIndex = 0;
                this.dataGridView1.Columns["Sale_Bill_Date"].HeaderText = "Sale Bill Date";
                this.dataGridView1.Columns["Quality"].Visible = true;
                this.dataGridView1.Columns["Quality"].DisplayIndex = 2;
                this.dataGridView1.Columns["Quality"].HeaderText = "Quality";
                this.dataGridView1.Columns["Sale_Bill_No"].Visible = true;
                this.dataGridView1.Columns["Sale_Bill_No"].DisplayIndex = 4;
                this.dataGridView1.Columns["Sale_Bill_No"].HeaderText = "Sale Bill Number";
                this.dataGridView1.Columns["Sale_Bill_Weight"].Visible = true;
                this.dataGridView1.Columns["Sale_Bill_Weight"].DisplayIndex = 6;
                this.dataGridView1.Columns["Sale_Bill_Weight"].HeaderText = "Bill Weight";
                this.dataGridView1.Columns["Sale_Bill_Amount"].Visible = true;
                this.dataGridView1.Columns["Sale_Bill_Amount"].DisplayIndex = 8;
                this.dataGridView1.Columns["Sale_Bill_Amount"].HeaderText = "Bill Amount";
                c.auto_adjust_dgv(this.dataGridView1);
            }
            if (this.vno == 11)
            {
                this.dt = c.getVoucherHistories("SalesBillNos_Voucher");
                this.dataGridView1.ReadOnly = true;
                this.dataGridView1.DataSource = this.remove_sales_rows();
                this.dataGridView1.Columns.OfType<DataGridViewColumn>().ToList().ForEach(col => col.Visible = false);
                this.dataGridView1.Columns["Sale_Bill_Date"].Visible = true;
                this.dataGridView1.Columns["Sale_Bill_Date"].DisplayIndex = 0;
                this.dataGridView1.Columns["Sale_Bill_Date"].HeaderText = "Sale Bill Date";
                this.dataGridView1.Columns["Quality"].Visible = true;
                this.dataGridView1.Columns["Quality"].DisplayIndex = 2;
                this.dataGridView1.Columns["Quality"].HeaderText = "Quality";
                this.dataGridView1.Columns["Sale_Bill_No"].Visible = true;
                this.dataGridView1.Columns["Sale_Bill_No"].DisplayIndex = 4;
                this.dataGridView1.Columns["Sale_Bill_No"].HeaderText = "Sale Bill Number";
                this.dataGridView1.Columns["Sale_Bill_Weight"].Visible = true;
                this.dataGridView1.Columns["Sale_Bill_Weight"].DisplayIndex = 6;
                this.dataGridView1.Columns["Sale_Bill_Weight"].HeaderText = "Bill Weight";
                this.dataGridView1.Columns["Sale_Bill_Amount"].Visible = true;
                this.dataGridView1.Columns["Sale_Bill_Amount"].DisplayIndex = 8;
                this.dataGridView1.Columns["Sale_Bill_Amount"].HeaderText = "Bill Amount";
                c.auto_adjust_dgv(this.dataGridView1);
            }
            if (this.vno == 12)
            {
                this.dt = c.getVoucherHistories("Redyeing_Voucher");
                this.dataGridView1.ReadOnly = true;
                this.dataGridView1.DataSource = dt;
                this.dataGridView1.Columns.OfType<DataGridViewColumn>().ToList().ForEach(col => col.Visible = false);
                this.dataGridView1.Columns["Voucher_ID"].Visible = false;
                this.dataGridView1.Columns["Date_Of_Input"].Visible= true;
                this.dataGridView1.Columns["Date_Of_Input"].DisplayIndex = 0;
                this.dataGridView1.Columns["Date_Of_Input"].HeaderText = "Date of Input";
                this.dataGridView1.Columns["Date_Of_Issue"].Visible = true;
                this.dataGridView1.Columns["Date_Of_Issue"].DisplayIndex = 1;
                this.dataGridView1.Columns["Date_Of_Issue"].HeaderText = "Date Of Issue";
                this.dataGridView1.Columns["Old_Batch_No"].Visible = true;
                this.dataGridView1.Columns["Old_Batch_No"].DisplayIndex = 2;
                this.dataGridView1.Columns["Old_Batch_No"].HeaderText = "Old Batch No";
                this.dataGridView1.Columns["Old_Batch_Fiscal_Year"].Visible = true;
                this.dataGridView1.Columns["Old_Batch_Fiscal_Year"].DisplayIndex = 3;
                this.dataGridView1.Columns["Old_Batch_Fiscal_Year"].HeaderText = "Old Batch Fiscal Year";
                this.dataGridView1.Columns["Non_Redyeing_Batch_No"].Visible = true;
                this.dataGridView1.Columns["Non_Redyeing_Batch_No"].DisplayIndex = 4;
                this.dataGridView1.Columns["Non_Redyeing_Batch_No"].HeaderText = "Non Redyeing Batch No";
                this.dataGridView1.Columns["Redyeing_Batch_No"].Visible = true;
                this.dataGridView1.Columns["Redyeing_Batch_No"].DisplayIndex = 5;
                this.dataGridView1.Columns["Redyeing_Batch_No"].HeaderText = "Redyeing Batch No";
                this.dataGridView1.Columns["Redyeing_Batch_Fiscal_Year"].Visible = true;
                this.dataGridView1.Columns["Redyeing_Batch_Fiscal_Year"].DisplayIndex = 6;
                this.dataGridView1.Columns["Redyeing_Batch_Fiscal_Year"].HeaderText = "New Batch Fiscal Year";
                c.auto_adjust_dgv(this.dataGridView1);
            }
        }
        private DataTable remove_sales_rows()
        {
            int rows = this.dt.Rows.Count;
            if (rows == 0)
                return null;
            DataTable d = dt.Clone(); ;
            for(int i=0;i<rows;i++)
            {
                if((this.vno==3 || this.vno==10) && this.dt.Rows[i]["Tablename"].ToString()=="Carton_Produced")
                {
                    continue;
                }
                else if ((this.vno == 9 || this.vno==11) && this.dt.Rows[i]["Tablename"].ToString() == "Carton")
                {
                    continue;
                }
                d.Rows.Add(this.dt.Rows[i].ItemArray);
            }
            return d;
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
