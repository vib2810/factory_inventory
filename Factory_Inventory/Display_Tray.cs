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
            this.textBox17.Text = tray["Tray_No"].ToString();
            this.textBox17.Text = tray["Tray_Production_Date"].ToString();

        }
    }
}
