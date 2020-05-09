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
        public Display_Batch(DataRow Batch)
        {
            InitializeComponent();
            this.Select();
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
            string prod = Batch["Date_Of_Production"].ToString();
            if (prod != "") this.textBox1.Text = prod.Substring(0, 10);
            string billdate = Batch["Bill_Date"].ToString();
            if (billdate != "") this.textBox4.Text = billdate.Substring(0, 10);

            this.textBox18.Text = Batch["Batch_State"].ToString();
            this.textBox11.Text = Batch["Voucher_ID"].ToString();
            this.textBox14.Text = Batch["Fiscal_Year"].ToString();
            this.textBox5.Text = Batch["Bill_No"].ToString();
            this.textBox2.Text = Batch["Slip_No"].ToString();
            this.textBox13.Text = Batch["Dyeing_Company_Name"].ToString();
            this.textBox10.Text = Batch["Dyeing_Rate"].ToString();
            this.textBox16.Text = Batch["Tray_ID_Arr"].ToString();
            this.textBox19.Text = Batch["Printed"].ToString();
        }

    }
}
