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
    public partial class T_V_master : Form
    {
        private Color select = Color.Orange;
        public T_V_master()
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
            m_V_editQBTUC1.Hide();
            m_V_editQBTUC2.Hide();
            m_V_editQBTUC3.Hide();
            t_V_customerMasterUC1.Hide();
            t_v_coneMasterUC1.Hide();
        }

        private void editCNameButton_Click(object sender, EventArgs e)
        {
            hide_all_UCs();
            m_V_editQBTUC2.Show();
            m_V_editQBTUC2.BringToFront();
            m_V_editQBTUC2.loadDatabase();
            m_V_editQBTUC2.Focus();
            this.decolour_all_buttons();
            this.editCNameButton.BackColor = select;
            this.Text = "Trading - Master - Company";
        }

        private void customerButton_Click(object sender, EventArgs e)
        {
            hide_all_UCs();
            t_V_customerMasterUC1.Show();
            t_V_customerMasterUC1.BringToFront();
            t_V_customerMasterUC1.loadDatabase();
            t_V_customerMasterUC1.Focus();
            this.decolour_all_buttons();
            this.customerButton.BackColor = select;
            this.Text = "Trading - Master - Customer";
        }

        private void colourButton_Click(object sender, EventArgs e)
        {
            hide_all_UCs();
            m_V_editQBTUC3.Show();
            m_V_editQBTUC3.BringToFront();
            m_V_editQBTUC3.loadDatabase();
            m_V_editQBTUC3.Focus();
            this.decolour_all_buttons();
            this.colourButton.BackColor = select;
            this.Text = "Trading - Master - Colour";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            hide_all_UCs();
            t_v_coneMasterUC1.Show();
            t_v_coneMasterUC1.BringToFront();
            t_v_coneMasterUC1.loadDatabase();
            t_v_coneMasterUC1.Focus();
            this.decolour_all_buttons();
            this.conesButton.BackColor = select;
            this.Text = "Trading - Master - Cone";
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
            this.Text = "Trading - Master - Quality Before Job";
        }
    }
}
