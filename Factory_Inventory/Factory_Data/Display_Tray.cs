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
    public partial class Display_Tray : Form
    {
        public Display_Tray(DataRow tray)
        {
            InitializeComponent();
            this.Select();
            this.textBox1.Text = tray["Tray_Production_Date"].ToString().Substring(0, 10);
            this.textBox1.SelectedText = null;
            this.textBox2.Text = tray["Spring"].ToString();
            this.textBox3.Text = tray["Number_of_Springs"].ToString();
            this.textBox4.Text = tray["Tray_Tare"].ToString();
            this.textBox5.Text = tray["Gross_Weight"].ToString();
            this.textBox6.Text = tray["Quality"].ToString();
            this.textBox7.Text = tray["Company_Name"].ToString();
            string dyeing_in = tray["Dyeing_In_Date"].ToString();
            if(dyeing_in!="") this.textBox8.Text = dyeing_in.Substring(0,10);
            string dyeing_out = tray["Dyeing_Out_Date"].ToString();
            if (dyeing_out != "") this.textBox9.Text = dyeing_out.Substring(0, 10);
            this.textBox10.Text = tray["Batch_No"].ToString();
            this.textBox11.Text = tray["Tray_ID"].ToString();
            this.textBox12.Text = tray["Net_Weight"].ToString();
            this.textBox13.Text = tray["Dyeing_Company_Name"].ToString();
            this.textBox14.Text = tray["Fiscal_Year"].ToString();
            this.textBox15.Text = tray["Machine_No"].ToString();
            this.textBox16.Text = tray["Quality_Before_Twist"].ToString();
            this.textBox17.Text = tray["Tray_No"].ToString();

            if (tray.Table.Columns.Contains("Tray_State")==true)
            {
                this.textBox18.Text = tray["Tray_State"].ToString();
            }
            else this.textBox18.Text = "Tray Has Been Processed";
            this.textBox19.Text = tray["Batch_Fiscal_Year"].ToString();
        }
    }
}
