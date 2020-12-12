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
    public partial class M_S_displayUC : UserControl
    {
        DbConnect c = new DbConnect();
        public M_S_displayUC()
        {
            InitializeComponent();
            panel1.BackColor = Color.Green;
            sizexTB.Text = Properties.Settings.Default.SizeX.ToString();
            sizeyTB.Text = Properties.Settings.Default.SizeY.ToString();
            scalexTB.Text = Properties.Settings.Default.ScaleX.ToString();
            scaleyTB.Text = Properties.Settings.Default.ScaleY.ToString();
            this.sizexTB.TextChanged += new System.EventHandler(this.allTB_TextChanged);
            this.sizeyTB.TextChanged += new System.EventHandler(this.allTB_TextChanged);
            this.scalexTB.TextChanged += new System.EventHandler(this.allTB_TextChanged);
            this.scaleyTB.TextChanged += new System.EventHandler(this.allTB_TextChanged);
        }

        private void allTB_TextChanged(object sender, EventArgs e)
        {
            panel1.BackColor = Color.Red;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            bool error1 = false;
            bool error2 = false;
            try
            {
                float.Parse(scalexTB.Text);
                float.Parse(scaleyTB.Text);
            }
            catch
            {
                error1 = true;
            }
            try
            {
                float.Parse(sizexTB.Text);
                float.Parse(sizeyTB.Text);
            }
            catch
            {
                error2 = true;
            }
            if (float.Parse(scalexTB.Text) <= 0F || float.Parse(scaleyTB.Text) <= 0F) error1 = true;
            if (float.Parse(sizexTB.Text) <= 0F || float.Parse(sizeyTB.Text) <= 0F) error2 = true;
            
            if (error1 == true)
            {
                c.ErrorBox("Scale X and Scale Y should be non-zero positive numerical");
                return;
            }
            if(error2 == true)
            {
                c.ErrorBox("Size X and Size Y should be non-zero positve integer only");
                return;
            }
            panel1.BackColor = Color.Green;
            Properties.Settings.Default.SizeX = float.Parse(sizexTB.Text);
            Properties.Settings.Default.SizeY = float.Parse(sizeyTB.Text);
            Properties.Settings.Default.ScaleX = float.Parse(scalexTB.Text);
            Properties.Settings.Default.ScaleY = float.Parse(scaleyTB.Text);
            Properties.Settings.Default.Save();
        }
    }
}
