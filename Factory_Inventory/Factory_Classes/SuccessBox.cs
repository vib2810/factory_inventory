using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Factory_Inventory.Factory_Classes
{
    public partial class SuccessBox : Form
    {
        public SuccessBox(string message, string title)
        {
            InitializeComponent();
            if(title!="")
            {
                this.Text = title;
            }
            this.messageLabel.Text = message;
            this.OKButton.Focus();
            this.ShowDialog();
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
