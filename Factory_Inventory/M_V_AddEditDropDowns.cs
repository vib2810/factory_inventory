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
    public partial class M_V_AddEditDropDowns : Form
    {
        private Color select = Color.Orange;
        public M_V_AddEditDropDowns()
        {
            InitializeComponent();
            hide_all_UCs();
        }
        public void decolour_all_buttons()
        {
            var buttons = this.Controls
             .OfType<Button>()
             .Where(x => x.Name.EndsWith("Button"));

            foreach (var button in buttons)
            {
                button.BackColor = Color.DarkGray;
            }
        }
        public void hide_all_UCs()
        {
            editQuality1.Hide();
            editCompany1.Hide();
            editCustomer1.Hide();
            editSpring1.Hide();
            editColour1.Hide();
            editDyeingCompany1.Hide();
            editCone1.Hide();
            m_V_editMachineNoUC1.Hide();
        }
        private void editQualityButton_Click(object sender, EventArgs e)
        {
            hide_all_UCs();
            editQuality1.Show();
            editQuality1.BringToFront();
            editQuality1.loadDatabase();
            editQuality1.Focus();
            this.decolour_all_buttons();
            this.editQualityButton.BackColor = select;
            this.Text = "Add/Edit Fixed Details - Quality";
        }

        private void editCNameButton_Click(object sender, EventArgs e)
        {
            hide_all_UCs();
            editCompany1.Show();
            editCompany1.BringToFront();
            editCompany1.loadDatabase();
            editCompany1.Focus();
            this.decolour_all_buttons();
            this.editCNameButton.BackColor = select;
            this.Text = "Add/Edit Fixed Details - Company";
        }

        private void customerButton_Click(object sender, EventArgs e)
        {
            hide_all_UCs();
            editCustomer1.Show();
            editCustomer1.BringToFront();
            editCustomer1.loadDatabase();
            editCustomer1.Focus();
            this.decolour_all_buttons();
            this.customerButton.BackColor = select;
            this.Text = "Add/Edit Fixed Details - Customer";
        }

        private void springButton_Click(object sender, EventArgs e)
        {
            hide_all_UCs();
            editSpring1.Show();
            editSpring1.BringToFront();
            editSpring1.loadDatabase();
            editSpring1.Focus();
            this.decolour_all_buttons();
            this.springButton.BackColor = select;
            this.Text = "Add/Edit Fixed Details - Spring";
        }

        private void colourButton_Click(object sender, EventArgs e)
        {
            hide_all_UCs();
            editColour1.Show();
            editColour1.BringToFront();
            editColour1.loadDatabase();
            editColour1.Focus();
            this.decolour_all_buttons();
            this.colourButton.BackColor = select;
            this.Text = "Add/Edit Fixed Details - Colour";
        }

        private void dyeingCompanyButton_Click(object sender, EventArgs e)
        {
            hide_all_UCs();
            editDyeingCompany1.Show();
            editDyeingCompany1.BringToFront();
            editDyeingCompany1.loadDatabase();
            editDyeingCompany1.Focus();
            this.decolour_all_buttons();
            this.dyeingCompanyButton.BackColor = select;
            this.Text = "Add/Edit Fixed Details - Dyeing Company";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            hide_all_UCs();
            editCone1.Show();
            editCone1.BringToFront();
            editCone1.loadDatabase();
            editCone1.Focus();
            this.decolour_all_buttons();
            this.conesButton.BackColor = select;
            this.Text = "Add/Edit Fixed Details - Cone";
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            hide_all_UCs();
            m_V_editMachineNoUC1.Show();
            m_V_editMachineNoUC1.BringToFront();
            m_V_editMachineNoUC1.loadDatabase();
            m_V_editMachineNoUC1.Focus();
            this.decolour_all_buttons();
            Button b = sender as Button;
            b.BackColor = select;
            this.Text = "Add/Edit Fixed Details - Machine Number";
        }

        private void qbtButton_Click(object sender, EventArgs e)
        {
            hide_all_UCs();
            m_V_editQBTUC1.Show();
            m_V_editQBTUC1.BringToFront();
            m_V_editQBTUC1.loadDatabase();
            m_V_editQBTUC1.Focus();
            this.decolour_all_buttons();
            Button b = sender as Button;
            b.BackColor = select;
            this.Text = "Add/Edit Fixed Details - Quality Before Twist";
        }
    }
}
