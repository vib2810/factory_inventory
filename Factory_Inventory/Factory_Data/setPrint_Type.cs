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

namespace Factory_Inventory
{
    public partial class setPrint_Type : Form
    {
        DbConnect c = new DbConnect();
        public List<string> result=null;
        public setPrint_Type(List<string> input)
        {
            InitializeComponent();
            if(input!= null)
            {
                this.firmnameTB.Text = input[0];
                this.addressTB.Text = input[1];
                this.gstinTB.Text = input[2];
                this.pnoTB.Text = input[3];
                this.emailTB.Text = input[4];
            }
        }
        private void setPrint_Type_Load(object sender, EventArgs e)
        {
            var comboBoxes = this.Controls
             .OfType<ComboBox>()
             .Where(x => x.Name.EndsWith("CB"));

            foreach (var cmbBox in comboBoxes)
            {
                c.comboBoxEvent(cmbBox);
            }

            var textBoxes = this.Controls
                  .OfType<TextBox>()
                  .Where(x => x.Name.EndsWith("TB"));

            foreach (var txtBox in textBoxes)
            {
                c.textBoxEvent(txtBox);
            }

            var dtps = this.Controls
                  .OfType<DateTimePicker>()
                  .Where(x => x.Name.EndsWith("DTP"));

            foreach (var dtp in dtps)
            {
                c.DTPEvent(dtp);
            }

            var buttons = this.Controls
                  .OfType<Button>()
                  .Where(x => x.Name.EndsWith("Button"));

            foreach (var button in buttons)
            {
                Console.WriteLine(button.Name);
                c.buttonEvent(button);
            }
            this.ActiveControl = this.firmnameTB;
        }
        private void saveButton_Click(object sender, EventArgs e)
        {
            result = new List<string>(5);
            result.Add(this.firmnameTB.Text);
            result.Add(this.addressTB.Text);
            result.Add(this.gstinTB.Text);
            result.Add(this.pnoTB.Text);
            result.Add(this.emailTB.Text);
            this.Close();
        }
    }
}
