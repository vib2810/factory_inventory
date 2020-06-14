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
    public partial class Display_Carton : Form
    {
        int inward_voucher_id=-1, ts_voucher_id=-1;
        int ts=0;
        DbConnect c = new DbConnect();
        public Display_Carton(DataRow Carton)
        {
            InitializeComponent();
            this.Select();
            this.textBox17.Text = Carton["Carton_No"].ToString();
            this.textBox6.Text = Carton["Quality"].ToString();
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
            string dyeing_out = Carton["Date_Of_Issue"].ToString();
            if (dyeing_out != "") this.textBox9.Text = dyeing_out.Substring(0, 10);
            this.textBox16.Text = Carton["Date_Of_Input"].ToString().Substring(0,10);
            this.textBox14.Text = Carton["Fiscal_Year"].ToString();
            this.textBox15.Text = Carton["Bill_No"].ToString();
            this.textBox2.Text = Carton["Buy_Cost"].ToString();
            this.textBox3.Text = Carton["Sale_Rate"].ToString();
            this.textBox4.Text= Carton["Sale_DO_No"].ToString();
            this.textBox11.Text= Carton["DO_Fiscal_Year"].ToString();
            this.textBox10.Text= Carton["Type_Of_Sale"].ToString();
            inward_voucher_id= int.Parse(Carton["Inward_Voucher_ID"].ToString());
            if(string.IsNullOrEmpty(Carton["TS_Voucher_ID"].ToString())==false) ts_voucher_id =int.Parse(Carton["TS_Voucher_ID"].ToString());
            if(string.IsNullOrEmpty(this.textBox9.Text)==false)
            {
                ts = 1;
            }
            if (string.IsNullOrEmpty(this.textBox8.Text) == false)
            {
                ts = 2;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (ts_voucher_id == -1)
            {
                c.ErrorBox("No Voucher Found/Linked");
                return;
            }
            if(ts==1)
            {
                DataTable table = c.getTableRows("Twist_Voucher", "Voucher_ID=" + ts_voucher_id);
                DataRow voucher = table.Rows[0];
                M_V1_cartonTwistForm f = new M_V1_cartonTwistForm(voucher, false, new M_V_history(1));
                f.deleteButton.Visible = false;
                f.StartPosition = FormStartPosition.CenterScreen;
                f.Show();
            }
            if(ts==2)
            {
                DataTable table = c.getTableRows("Sales_Voucher", "Voucher_ID=" + ts_voucher_id);
                DataRow voucher = table.Rows[0];
                M_VC_cartonSalesForm f = new M_VC_cartonSalesForm(voucher, false, new M_V_history(1), "Carton");
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
            DataTable table = c.getTableRows("Carton_Voucher", "Voucher_ID=" + inward_voucher_id);
            DataRow voucher = table.Rows[0];
            M_V1_cartonInwardForm f = new M_V1_cartonInwardForm(voucher, false, new M_V_history(1));
            f.deleteButton.Visible = false;
            f.StartPosition = FormStartPosition.CenterScreen;
            f.Show();
        }
    }
}
