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
    public partial class Display_Batch : Form
    {
        private DbConnect c = new DbConnect();
        private int issue_voucher_id = -1, inward_voucher_id = -1, bill_voucher_id = -1, production_voucher_id = -1, redyeing_voucher_id = -1;

        public Display_Batch(DataRow Batch)
        {
            InitializeComponent();
            this.Select();
            if (Batch.Table.Columns.Count <= 4)
                return;
            this.textBox17.Text = Batch["Batch_No"].ToString();
            this.textBox6.Text = Batch["Quality"].ToString();
            this.textBox7.Text = Batch["Company_Name"].ToString();
            this.textBox3.Text = Batch["Number_of_Trays"].ToString();
            this.textBox12.Text = Batch["Net_Weight"].ToString();
            this.textBox15.Text = Batch["Colour"].ToString();
            
            string dyeing_in = Batch["Dyeing_In_Date"].ToString();
            if (dyeing_in != "") this.textBox8.Text = dyeing_in.Substring(0, 10);
            string dyeing_out = Batch["Dyeing_Out_Date"].ToString();
            if (dyeing_out != "") this.textBox9.Text = dyeing_out.Substring(0, 10);
            string start_prod = Batch["Start_Date_Of_Production"].ToString();
            if (start_prod != "") this.textBox1.Text = start_prod.Substring(0, 10);
            string prod = Batch["Date_Of_Production"].ToString();
            if (prod != "") this.textBox22.Text = prod.Substring(0, 10);
            string billdate = Batch["Bill_Date"].ToString();
            if (billdate != "") this.textBox4.Text = billdate.Substring(0, 10);

            this.textBox18.Text = Batch["Batch_State"].ToString();
            this.textBox11.Text = Batch["Production_Voucher_ID"].ToString();
            this.textBox14.Text = Batch["Fiscal_Year"].ToString();
            this.textBox5.Text = Batch["Bill_No"].ToString();
            this.textBox2.Text = Batch["Slip_No"].ToString();
            this.textBox13.Text = Batch["Dyeing_Company_Name"].ToString();
            this.textBox10.Text = Batch["Dyeing_Rate"].ToString();
            this.textBox16.Text = c.removecom(Batch["Tray_ID_Arr"].ToString());
            this.textBox19.Text = Batch["Printed"].ToString();
            this.textBox20.Text = Batch["Slip_No"].ToString();
            this.textBox21.Text = Batch["Redyeing"].ToString();
            this.textBox23.Text = Batch["Grade"].ToString();
            if(!string.IsNullOrEmpty(Batch["Tray_ID_Arr"].ToString()))
            {
                DataTable dt = c.getTrayDataBothTables("Tray_No, Net_Weight", "Tray_ID IN (" + c.removecom(Batch["Tray_ID_Arr"].ToString()) + ")");
                this.dataGridView1.DataSource = dt;
                this.dataGridView1.Columns["Tray_No"].DisplayIndex = 0;
                this.dataGridView1.Columns["Tray_No"].HeaderText = "Tray Number";
                this.dataGridView1.Columns["Net_Weight"].DisplayIndex = 1;
                this.dataGridView1.Columns["Net_Weight"].HeaderText = "Net Weight";
            }
            c.auto_adjust_dgv(this.dataGridView1);
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView1.Font, FontStyle.Bold);
            if(!string.IsNullOrEmpty(Batch["Dyeing_Out_Voucher_ID"].ToString())) issue_voucher_id= int.Parse(Batch["Dyeing_Out_Voucher_ID"].ToString());
            if (!string.IsNullOrEmpty(Batch["Dyeing_In_Voucher_ID"].ToString())) inward_voucher_id = int.Parse(Batch["Dyeing_In_Voucher_ID"].ToString());
            if (!string.IsNullOrEmpty(Batch["Bill_Voucher_ID"].ToString())) bill_voucher_id = int.Parse(Batch["Bill_Voucher_ID"].ToString());
            if (!string.IsNullOrEmpty(Batch["Production_Voucher_ID"].ToString())) production_voucher_id = int.Parse(Batch["Production_Voucher_ID"].ToString());
            if (!string.IsNullOrEmpty(Batch["Redyeing_Voucher_ID"].ToString())) redyeing_voucher_id = int.Parse(Batch["Redyeing_Voucher_ID"].ToString());
        }

        private void dyeingIssueVoucherButton_Click(object sender, EventArgs e)
        {
            if (issue_voucher_id == -1)
            {
                c.ErrorBox("No Voucher Found/Linked");
                return;
            }
            DataTable table = c.getTableRows("Dyeing_Issue_Voucher", "Voucher_ID=" + issue_voucher_id);
            DataRow voucher = table.Rows[0];
            M_V2_dyeingIssueForm f = new M_V2_dyeingIssueForm(voucher, false, new M_V_history(1));
            f.deleteButton.Visible = false;
            f.StartPosition = FormStartPosition.CenterScreen;
            f.Show();
        }
        private void cartonProductionButton_Click(object sender, EventArgs e)
        {
            if (production_voucher_id == -1)
            {
                c.ErrorBox("No Voucher Found/Linked");
                return;
            }
            DataTable table = c.getTableRows("Carton_Production_Voucher", "Voucher_ID=" + production_voucher_id);
            DataRow voucher = table.Rows[0];
            M_V3_cartonProductionForm f = new M_V3_cartonProductionForm(voucher, false, new M_V_history(1));
            f.deleteButton.Visible = false;
            f.StartPosition = FormStartPosition.CenterScreen;
            f.Show();
        }
        private void redyeingButton_Click(object sender, EventArgs e)
        {
            if (redyeing_voucher_id == -1)
            {
                c.ErrorBox("No Voucher Found/Linked");
                return;
            }
            DataTable table = c.getTableRows("Redyeing_Voucher", "Voucher_ID=" + redyeing_voucher_id);
            DataRow voucher = table.Rows[0];
            M_V3_issueToReDyeingForm f = new M_V3_issueToReDyeingForm(voucher, false, new M_V_history(1));
            f.deleteButton.Visible = false;
            f.StartPosition = FormStartPosition.CenterScreen;
            f.Show();
        }
        private void billNumberButton_Click(object sender, EventArgs e)
        {
            if (bill_voucher_id == -1)
            {
                c.ErrorBox("No Voucher Found/Linked");
                return;
            }
            DataTable table = c.getTableRows("BillNos_Voucher", "Voucher_ID=" + bill_voucher_id);
            DataRow voucher = table.Rows[0];
            M_V2_dyeingInwardForm f = new M_V2_dyeingInwardForm(voucher, false, new M_V_history(1), "addBill");
            f.deleteButton.Visible = false;
            f.StartPosition = FormStartPosition.CenterScreen;
            f.Show();
        }
        private void dyeingInwardVoucherButton_Click(object sender, EventArgs e)
        {
            if (inward_voucher_id == -1)
            {
                c.ErrorBox("No Voucher Found/Linked");
                return;
            }
            DataTable table = c.getTableRows("Dyeing_Inward_Voucher", "Voucher_ID=" + inward_voucher_id);
            DataRow voucher = table.Rows[0];
            M_V2_dyeingInwardForm f = new M_V2_dyeingInwardForm(voucher, false, new M_V_history(1), "dyeingInward");
            f.deleteButton.Visible = false;
            f.StartPosition = FormStartPosition.CenterScreen;
            f.Show();
        }
    }
}
