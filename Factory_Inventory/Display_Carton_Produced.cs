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
    public partial class Display_Carton_Produced : Form
    {
        DbConnect c = new DbConnect();
        int production_voucher_id = -1, sales_voucher_id = -1;
        public Display_Carton_Produced(DataRow Carton)
        {
            InitializeComponent();
            this.Select();
            if (Carton == null) return;
            this.textBox17.Text = Carton["Carton_No"].ToString();
            this.textBox6.Text = Carton["Quality"].ToString();
            //this.textBox7.Text = Carton["Customer_Name"].ToString();
            this.textBox3.Text = Carton["Number_of_Cones"].ToString();
            this.textBox12.Text = Carton["Cone_Weight"].ToString();
            this.textBox15.Text = Carton["Colour"].ToString();
            
            string prod = Carton["Date_Of_Production"].ToString();
            if (prod != "") this.textBox1.Text = prod.Substring(0, 10);
            //string dyeing_in = Carton["Sale_Bill_Date"].ToString();
            //if (dyeing_in != "") this.textBox22.Text = dyeing_in.Substring(0, 10);
            string dyeing_out = Carton["Date_Of_Sale"].ToString();
            if (dyeing_out != "") this.textBox9.Text = dyeing_out.Substring(0, 10);
            this.textBox18.Text = Carton["Carton_State"].ToString();
            //string billdate = Carton["Sale_DO_Date"].ToString();
            //if (billdate != "") this.textBox4.Text = billdate.Substring(0, 10);
            
            this.textBox5.Text = Carton["Gross_Weight"].ToString();
            this.textBox2.Text = Carton["Carton_Weight"].ToString();
            this.textBox10.Text = Carton["Net_Weight"].ToString();
            this.textBox20.Text = Carton["Grade"].ToString();
            this.textBox19.Text = Carton["Printed"].ToString();
            this.textBox16.Text = Carton["Batch_No_Arr"].ToString();
            this.textBox14.Text = Carton["Fiscal_Year"].ToString();
            this.textBox13.Text = Carton["Dyeing_Company_Name"].ToString();
            this.textBox11.Text = Carton["Batch_Fiscal_Year_Arr"].ToString();
            this.textBox24.Text = Carton["Sale_Rate"].ToString();
            this.textBox21.Text = Carton["Sale_DO_No"].ToString();
            //this.textBox23.Text = Carton["Sale_Bill_No"].ToString();
            if (string.IsNullOrEmpty(Carton["Production_Voucher_ID"].ToString()) == false) production_voucher_id = int.Parse(Carton["Production_Voucher_ID"].ToString());
            if (string.IsNullOrEmpty(Carton["Sales_Voucher_ID"].ToString()) == false) sales_voucher_id = int.Parse(Carton["Sales_Voucher_ID"].ToString());
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
            DataTable table = c.getTableRows("Sales_Voucher", "Voucher_ID=" + sales_voucher_id);
            DataRow voucher = table.Rows[0];
            M_VC_cartonSalesForm f = new M_VC_cartonSalesForm(voucher, false, new M_V_history(1), "Carton_Produced");
            f.deleteButton.Visible = false;
            f.StartPosition = FormStartPosition.CenterScreen;
            f.Show();
        }

        private void button1_Click(object sender, EventArgs e)
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
    }
}
