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
    public partial class Display_Repacked_Carton : Form
    {
        DbConnect c = new DbConnect();
        int repacking_voucher_id = -1, sales_voucher_id = -1;
        public Display_Repacked_Carton(DataRow Carton)
        {
            InitializeComponent();
            this.Select();
            if (Carton == null) return;
            this.textBox17.Text = Carton["Carton_No"].ToString();
            this.textBox6.Text = Carton["Quality_Before_job"].ToString();
            this.textBox7.Text = Carton["Customer_Name"].ToString();
            this.textBox3.Text = Carton["Number_of_Cones"].ToString();
            this.textBox12.Text = Carton["Cone_Weight"].ToString();
            this.textBox15.Text = Carton["Colour"].ToString();
            
            string prod = Carton["Date_Of_Production"].ToString();
            if (prod != "") this.textBox8.Text = prod.Substring(0, 10);
            string dyeing_out = Carton["Date_Of_Sale"].ToString();
            if (dyeing_out != "") this.textBox9.Text = dyeing_out.Substring(0, 10);
            string sdop = Carton["Start_Date_Of_Production"].ToString();
            if (sdop != "") this.textBox1.Text = sdop.Substring(0,10);
            this.textBox18.Text = Carton["Carton_State"].ToString();

            this.textBox5.Text = Carton["Gross_Weight"].ToString();
            this.textBox2.Text = Carton["Carton_Weight"].ToString();
            this.textBox10.Text = Carton["Net_Weight"].ToString();
            this.textBox20.Text = Carton["Grade"].ToString();
            this.textBox19.Text = Carton["Printed"].ToString();
            this.textBox14.Text = Carton["Fiscal_Year"].ToString();
            this.textBox24.Text = Carton["Sale_Rate"].ToString();
            this.textBox21.Text = Carton["Sale_DO_No"].ToString();
            //this.textBox23.Text = Carton["Sale_Bill_No"].ToString();
            if (string.IsNullOrEmpty(Carton["Repacking_Voucher_ID"].ToString()) == false) repacking_voucher_id = int.Parse(Carton["Repacking_Voucher_ID"].ToString());
            if (string.IsNullOrEmpty(Carton["Sale_Voucher_ID"].ToString()) == false) sales_voucher_id = int.Parse(Carton["Sale_Voucher_ID"].ToString());
        }
        private void Display_Carton_Produced_Load(object sender, EventArgs e)
        {

        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (sales_voucher_id == -1)
            {
                c.ErrorBox("No Voucher Found/Linked");
                return;
            }
            M_V_history h = new M_V_history(15);
            string sql = h.getTradingQuery("", false, sales_voucher_id);
            DataTable table = c.runQuery(sql);
            DataRow voucher = table.Rows[0];
            T_V3_cartonSalesForm f = new T_V3_cartonSalesForm(voucher, false, h);
            f.deleteButton.Visible = false;
            f.StartPosition = FormStartPosition.CenterScreen;
            f.Show();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (repacking_voucher_id == -1)
            {
                c.ErrorBox("No Voucher Found/Linked");
                return;
            }
            M_V_history h = new M_V_history(14);
            string sql = h.getTradingQuery("", false, repacking_voucher_id);
            DataTable table = c.runQuery(sql);
            DataRow voucher = table.Rows[0];
            T_V2_repackingForm f = new T_V2_repackingForm(voucher, false, h);
            f.deleteButton.Visible = false;
            f.StartPosition = FormStartPosition.CenterScreen;
            f.Show();
        }
    }
}
