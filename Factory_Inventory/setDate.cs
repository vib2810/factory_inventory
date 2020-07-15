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
    public partial class setDate : Form
    {
        DbConnect c = new DbConnect();
        public DateTime result;
        public bool values_set = false;
        public setDate(DateTime d)
        {
            InitializeComponent();
            if (d != null) dateTimePicker1.Value = d;
            result = this.dateTimePicker1.Value;
        }
        public void setMinMax(DateTime mindate, DateTime maxdate)
        {
            dateTimePicker1.MinDate = mindate;
            dateTimePicker1.MaxDate = maxdate;
        }
        private void setDate_Load(object sender, EventArgs e)
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
            this.ActiveControl = this.dateTimePicker1;
        }
        public void set_Message(string msg)
        {
            this.messageLabel.Visible = true;
            this.messageLabel.Text = msg;
        }
        private void saveButton_Click(object sender, EventArgs e)
        {
            result = this.dateTimePicker1.Value;
            this.values_set = true;
            this.Close();
        }


    }
}
