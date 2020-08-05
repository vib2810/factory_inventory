using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Factory_Inventory.Factory_Classes;

namespace Factory_Inventory
{
    public partial class M_S_printUC : UserControl
    {
        DbConnect c = new DbConnect();
        public M_S_printUC()
        {
            InitializeComponent();
            panel1.BackColor = Color.Green;
            firmNameTB.Text = Properties.Settings.Default.FirmName;
            addressTB.Text = Properties.Settings.Default.Address;
            gstinTB.Text = Properties.Settings.Default.GSTIN;
            phoneNoTB.Text = Properties.Settings.Default.PhoneNo;
            emailIDTB.Text = Properties.Settings.Default.EmailID;
            this.firmNameTB.TextChanged += new System.EventHandler(this.allTB_TextChanged);
            this.addressTB.TextChanged += new System.EventHandler(this.allTB_TextChanged);
            this.gstinTB.TextChanged += new System.EventHandler(this.allTB_TextChanged);
            this.phoneNoTB.TextChanged += new System.EventHandler(this.allTB_TextChanged);
            this.emailIDTB.TextChanged += new System.EventHandler(this.allTB_TextChanged);
        }

        private void allTB_TextChanged(object sender, EventArgs e)
        {
            panel1.BackColor = Color.Red;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            panel1.BackColor = Color.Green;
            Properties.Settings.Default.FirmName = firmNameTB.Text;
            Properties.Settings.Default.Address = addressTB.Text;
            Properties.Settings.Default.GSTIN = gstinTB.Text;
            Properties.Settings.Default.PhoneNo = phoneNoTB.Text;
            Properties.Settings.Default.EmailID = emailIDTB.Text;
            Properties.Settings.Default.Save();
        }
    }
}
