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
    public partial class Display_Inward_Carton : Form
    {
        int inward_voucher_id=-1, sale_voucher_id=-1, repacking_voucher_id = -1;
        int sale=0, repack = 0;
        DbConnect c = new DbConnect();
        public Display_Inward_Carton(DataRow Carton)
        {
            InitializeComponent();
            this.Select();
            this.textBox17.Text = Carton["Carton_No"].ToString();
            this.textBox6.Text = Carton["Quality_Before_Job"].ToString();
            this.textBox7.Text = Carton["Company_Name"].ToString();
            if (Carton.Table.Columns.Contains("Carton_State") == true)
            {
                this.textBox18.Text = Carton["Carton_State"].ToString();
            }
            else this.textBox18.Text = "Carton Has Been Processed";
            this.textBox5.Text = Carton["Net_Weight"].ToString();
            this.textBox1.Text = Carton["Date_Of_Billing"].ToString().Substring(0, 10);
            string dyeing_in = Carton["Date_Of_Sale"].ToString();
            if (dyeing_in != "") this.textBox8.Text = dyeing_in.Substring(0, 10);
            string dyeing_out = Carton["Start_Date_Of_Production"].ToString();
            if (dyeing_out != "") this.textBox9.Text = dyeing_out.Substring(0, 10);
            string dop = Carton["Date_Of_Production"].ToString();
            if (dop != "") this.textBox13.Text = dop.Substring(0, 10);
            this.textBox16.Text = Carton["Date_Of_Input"].ToString().Substring(0,10);
            this.textBox14.Text = Carton["Fiscal_Year"].ToString();
            this.textBox15.Text = Carton["Bill_No"].ToString();
            this.textBox2.Text = Carton["Buy_Cost"].ToString();
            this.textBox3.Text = Carton["Sale_Rate"].ToString();
            this.textBox4.Text= Carton["Sale_DO_No"].ToString();
            this.textBox11.Text= Carton["DO_Fiscal_Year"].ToString();
            this.textBox10.Text= Carton["Type_Of_Sale"].ToString();
            this.textBox12.Text = Carton["Grade"].ToString();
            this.inwardTypeTB.Text = Carton["Inward_Type"].ToString();
            if(string.IsNullOrEmpty(Carton["Inward_Voucher_ID"].ToString()) == false)  inward_voucher_id = int.Parse(Carton["Inward_Voucher_ID"].ToString());
            if (string.IsNullOrEmpty(Carton["Sale_Voucher_ID"].ToString()) == false)
            {
                sale_voucher_id = int.Parse(Carton["Sale_Voucher_ID"].ToString());
                sale = 1;
            }
            if (string.IsNullOrEmpty(Carton["Repacking_Voucher_ID"].ToString()) == false)
            {
                repacking_voucher_id = int.Parse(Carton["Repacking_Voucher_ID"].ToString());
                repack = 1;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (sale == 0 && repack == 0)
            {
                c.ErrorBox("No Voucher Found/Linked");
                return;
            }
            if (repack == 1)
            {
                M_V_history h = new M_V_history(14);
                string sql = h.getTradingQuery("", false, repacking_voucher_id);
                DataTable table = c.runQuery(sql);
                DataRow voucher = table.Rows[0];
                T_V2_repackingForm f = new T_V2_repackingForm(voucher, false, h);
                f.deleteButton.Visible = false;
                f.StartPosition = FormStartPosition.CenterScreen;
                f.Show();
            }
            if (sale == 1)
            {
                M_V_history h = new M_V_history(15);
                string sql = h.getTradingQuery("", false, sale_voucher_id);
                DataTable table = c.runQuery(sql);
                DataRow voucher = table.Rows[0];
                T_V3_cartonSalesForm f = new T_V3_cartonSalesForm(voucher, false, h);
                f.deleteButton.Visible = false;
                f.StartPosition = FormStartPosition.CenterScreen;
                f.Show();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(inward_voucher_id==-1)
            {
                c.ErrorBox("No Voucher Found/Linked");
                return;
            }
            M_V_history h = new M_V_history(13);
            string sql = h.getTradingQuery("", false, inward_voucher_id);
            DataTable table = c.runQuery(sql);
            DataRow voucher = table.Rows[0];
            T_V1_cartonInwardForm f = new T_V1_cartonInwardForm(voucher, false, h);
            f.deleteButton.Visible = false;
            f.StartPosition = FormStartPosition.CenterScreen;
            f.Show();
        }
    }
}
