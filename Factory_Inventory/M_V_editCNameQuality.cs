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
    public partial class editCNameQuality : Form
    {
        public editCNameQuality()
        {
            InitializeComponent();
            hide_all_UCs();
        }
        public void hide_all_UCs()
        {
            editQuality1.Hide();
            editCompany1.Hide();
            editCustomer1.Hide();
            editSpring1.Hide();
            editColour1.Hide();
            editDyeingCompany1.Hide();
        }
        private void editQuality1_Load(object sender, EventArgs e)
        {

        }

        private void editQualityButton_Click(object sender, EventArgs e)
        {
            hide_all_UCs();
            editQuality1.Show();
            editQuality1.BringToFront();
            editQuality1.loadDatabase();
        }

        private void editCNameButton_Click(object sender, EventArgs e)
        {
            hide_all_UCs();
            editCompany1.Show();
            editCompany1.BringToFront();
            editCompany1.loadDatabase();
        }

        private void customerButton_Click(object sender, EventArgs e)
        {
            hide_all_UCs();
            editCustomer1.Show();
            editCustomer1.BringToFront();
            editCustomer1.loadDatabase();
        }

        private void springButton_Click(object sender, EventArgs e)
        {
            hide_all_UCs();
            editSpring1.Show();
            editSpring1.BringToFront();
            editSpring1.loadDatabase();
        }

        private void colourButton_Click(object sender, EventArgs e)
        {
            hide_all_UCs();
            editColour1.Show();
            editColour1.BringToFront();
            editColour1.loadDatabase();
        }

        private void dyeingCompanyButton_Click(object sender, EventArgs e)
        {
            hide_all_UCs();
            editDyeingCompany1.Show();
            editDyeingCompany1.BringToFront();
            editDyeingCompany1.loadDatabase();
        }
    }
}
