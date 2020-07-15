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
    public partial class A_1_AddEditDropDowns : Form
    {
        private Color select = Color.Orange;
        public A_1_AddEditDropDowns()
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
            a_1_editGroup1.Hide();
        }
        private void editQualityButton_Click(object sender, EventArgs e)
        {
            hide_all_UCs();
            a_1_editGroup1.Show();
            a_1_editGroup1.BringToFront();
            a_1_editGroup1.loadDatabase();
            a_1_editGroup1.Focus();
            this.decolour_all_buttons();
            this.editGroupButton.BackColor = select;
            this.Text = "Add/Edit Fixed Details - Group";
        }
    }
}
