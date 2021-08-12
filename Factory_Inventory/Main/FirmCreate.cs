using Factory_Inventory.Factory_Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Factory_Inventory.Main
{
    public partial class FirmCreate : Form
    {
        public bool values_set = false;
        MainConnect mc;
        public FirmCreate(DataTable dt, MainConnect mc)
        {
            this.values_set = false;
            this.mc = mc;
            InitializeComponent();
            List<string> firm_names = new List<string>();
            firm_names.Add("Empty Master");
            for(int i=0;i<dt.Rows.Count;i++)
            {
                firm_names.Add(dt.Rows[i]["Firm_Name"].ToString());
            }
            this.comboBox1.DataSource = firm_names;
            this.comboBox1.DisplayMember = "Firm Names";
            this.comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.comboBox1.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            
            this.comboBox2.SelectedIndex = 0;
        }

        private void Submit_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(firmnameTB.Text))
            {
                mc.ErrorBox("Enter Firm Name");
                return;
            }
            if (string.IsNullOrWhiteSpace(addressTB.Text))
            {
                mc.ErrorBox("Enter Address");
                return;
            }
            if (string.IsNullOrWhiteSpace(gstinTB.Text))
            {
                mc.ErrorBox("Enter GSTIN");
                return;
            }
            if (string.IsNullOrWhiteSpace(pnoTB.Text))
            {
                mc.ErrorBox("Enter Phone Number");
                return;
            }
            if (string.IsNullOrWhiteSpace(emailTB.Text))
            {
                mc.ErrorBox("Enter Email ID");
                return;
            }
            if (string.IsNullOrWhiteSpace(userNameTB.Text))
            {
                mc.ErrorBox("Enter Username");
                return;
            }
            if (string.IsNullOrWhiteSpace(passwordTB.Text))
            {
                mc.ErrorBox("Enter Password");
                return;
            }
            if (string.IsNullOrWhiteSpace(retypePasswordTB.Text))
            {
                mc.ErrorBox("Retype Password");
                return;
            }
            if(passwordTB.Text != retypePasswordTB.Text)
            {
                mc.ErrorBox("Passwords do not match");
                return;
            }
            this.values_set = true;
            this.Close();
        }

        private void FirmCreate_Load(object sender, EventArgs e)
        {
            var comboBoxes = this.Controls
              .OfType<ComboBox>()
              .Where(x => x.Name.EndsWith("CB"));

            foreach (var cmbBox in comboBoxes)
            {
                mc.comboBoxEvent(cmbBox);
            }

            var textBoxes = this.Controls
                  .OfType<TextBox>()
                  .Where(x => x.Name.EndsWith("TB"));

            foreach (var txtBox in textBoxes)
            {
                mc.textBoxEvent(txtBox);
            }

            var dtps = this.Controls
                  .OfType<DateTimePicker>()
                  .Where(x => x.Name.EndsWith("DTP"));

            foreach (var dtp in dtps)
            {
                mc.DTPEvent(dtp);
            }

            var buttons = this.Controls
                  .OfType<Button>()
                  .Where(x => x.Name.EndsWith("Button"));

            foreach (var button in buttons)
            {
                mc.buttonEvent(button);
            }
        }
    }
}
