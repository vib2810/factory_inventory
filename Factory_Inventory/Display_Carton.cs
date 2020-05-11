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
        }
    }
}
