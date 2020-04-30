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
    public partial class M_V2_trayInputForm : Form
    {
        DbConnect c;
        DataTable spring_table;
        M_V_history v1_history;
        int Voucher_ID, state, tray_id;
        bool edit_form;
        string old_tray_no;
        public M_V2_trayInputForm()
        {
            InitializeComponent();
            c = new DbConnect();
            //Create drop-down quality list
            var dataSource1 = new List<string>();
            DataTable d1 = c.getQC('q');

            for (int i = 0; i < d1.Rows.Count; i++)
            {
                dataSource1.Add(d1.Rows[i][0].ToString());
            }
            this.comboBox1.DataSource = dataSource1;
            this.comboBox1.DisplayMember = "Quality";
            this.comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.comboBox1.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;


            //Create drop-down Company list
            var dataSource2 = new List<string>();
            DataTable d2 = c.getQC('c');

            for (int i = 0; i < d2.Rows.Count; i++)
            {
                dataSource2.Add(d2.Rows[i][0].ToString());
            }
            this.comboBox2.DataSource = dataSource2;
            this.comboBox2.DisplayMember = "Company_Names";
            this.comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.comboBox2.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.comboBox2.AutoCompleteMode = AutoCompleteMode.SuggestAppend;


            //Create drop-down Spring list
            spring_table = new DataTable();
            var dataSource3 = new List<string>();
            this.spring_table = c.getQC('s');

            for (int i = 0; i < spring_table.Rows.Count; i++)
            {
                dataSource3.Add(spring_table.Rows[i][0].ToString());
            }
            this.comboBox3.DataSource = dataSource3;
            this.comboBox3.DisplayMember = "Spring";
            this.comboBox3.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.comboBox3.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.comboBox3.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            //Create drop-down Spring list
            var dataSource4 = new List<string>();
            dataSource4.Add("---Select---");
            dataSource4.Add("1u");
            dataSource4.Add("2u");
            dataSource4.Add("3u");
            dataSource4.Add("4u");
            dataSource4.Add("1d");
            dataSource4.Add("2d");
            dataSource4.Add("3d");
            dataSource4.Add("4d");
            this.machineNoCombobox.DataSource = dataSource4;
            this.machineNoCombobox.DisplayMember = "Machine_Number";
            this.machineNoCombobox.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.machineNoCombobox.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.machineNoCombobox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;


            this.numberOfSpringsTextbox.Text = "18";
            this.comboBox3.SelectedIndex = 0;
        }
        public M_V2_trayInputForm(DataRow row, bool isEditable, M_V_history v1_history)
        {
            InitializeComponent();
            this.v1_history = v1_history;
            this.c = new DbConnect();
            this.edit_form = isEditable;

            //Create drop-down quality list
            var dataSource1 = new List<string>();
            DataTable d1 = c.getQC('q');

            for (int i = 0; i < d1.Rows.Count; i++)
            {
                dataSource1.Add(d1.Rows[i][0].ToString());
            }
            this.comboBox1.DataSource = dataSource1;
            this.comboBox1.DisplayMember = "Quality";
            this.comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.comboBox1.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;


            //Create drop-down Company list
            var dataSource2 = new List<string>();
            DataTable d2 = c.getQC('c');

            for (int i = 0; i < d2.Rows.Count; i++)
            {
                dataSource2.Add(d2.Rows[i][0].ToString());
            }
            this.comboBox2.DataSource = dataSource2;
            this.comboBox2.DisplayMember = "Company_Names";
            this.comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.comboBox2.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.comboBox2.AutoCompleteMode = AutoCompleteMode.SuggestAppend;


            //Create drop-down Spring list
            spring_table = new DataTable();
            var dataSource3 = new List<string>();
            this.spring_table = c.getQC('s');

            for (int i = 0; i < spring_table.Rows.Count; i++)
            {
                dataSource3.Add(spring_table.Rows[i][0].ToString());
            }
            this.comboBox3.DataSource = dataSource3;
            this.comboBox3.DisplayMember = "Spring";
            this.comboBox3.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.comboBox3.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.comboBox3.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            //Create drop-down Spring list
            var dataSource4 = new List<string>();
            dataSource4.Add("---Select---");
            dataSource4.Add("1u");
            dataSource4.Add("2u");
            dataSource4.Add("3u");
            dataSource4.Add("4u");
            dataSource4.Add("1d");
            dataSource4.Add("2d");
            dataSource4.Add("3d");
            dataSource4.Add("4d");
            this.machineNoCombobox.DataSource = dataSource4;
            this.machineNoCombobox.DisplayMember = "Machine_Number";
            this.machineNoCombobox.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.machineNoCombobox.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.machineNoCombobox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;


            this.tray_id = int.Parse(row["Tray_ID"].ToString());
            this.state = c.getTrayState(this.tray_id);
            if (this.state == -1)
            {
                label1.Text = "Tray Has Been Processed \nTray Number may now be in\nuse in another Voucher";
                isEditable = false;
            }
            else if (this.state == 2)
            {
                label1.Text = "Voucher is not editable \nTray has been sent for dyeing";
                isEditable = false;
            }
            
            if (isEditable == false)
            {
                this.dateTimePicker2.Enabled = false;
                this.comboBox1.Enabled = false;
                this.comboBox2.Enabled = false;
                this.comboBox3.Enabled = false;
                this.addButton.Enabled = false;
                this.trayNumberTextbox.Enabled = false;
                this.numberOfSpringsTextbox.Enabled = false;
                this.traytareTextbox.Enabled = false;
                this.grossWeightTextbox.Enabled = false;
                this.machineNoCombobox.Enabled = false;
            }
            else
            {
                this.dateTimePicker2.Enabled = true;
                this.comboBox1.Enabled = true;
                this.comboBox2.Enabled = true;
                this.comboBox3.Enabled = true;
                this.addButton.Enabled = true;
                this.trayNumberTextbox.Enabled = true;
                this.numberOfSpringsTextbox.Enabled = true;
                this.traytareTextbox.Enabled = true;
                this.grossWeightTextbox.Enabled = true;
                this.machineNoCombobox.Enabled = true;
            }
            this.dateTimePicker1.Value = Convert.ToDateTime(row["Input_Date"].ToString());
            this.dateTimePicker2.Value = Convert.ToDateTime(row["Tray_Production_Date"].ToString());
            this.trayNumberTextbox.Text = row["Tray_No"].ToString();
            this.comboBox3.SelectedIndex = this.comboBox3.FindStringExact(row["Spring"].ToString());
            this.comboBox1.SelectedIndex = this.comboBox1.FindStringExact(row["Quality"].ToString());
            this.comboBox2.SelectedIndex = this.comboBox2.FindStringExact(row["Company_Name"].ToString());
            this.machineNoCombobox.SelectedIndex = this.machineNoCombobox.FindStringExact(row["Machine_No"].ToString());
            this.Voucher_ID = int.Parse(row["Voucher_ID"].ToString());
            this.numberOfSpringsTextbox.Text = row["Number_Of_Springs"].ToString();
            this.traytareTextbox.Text = row["Tray_Tare"].ToString();
            this.grossWeightTextbox.Text = row["Gross_Weight"].ToString();

            this.old_tray_no = row["Tray_No"].ToString();


        }

        private void addButton_Click(object sender, EventArgs e)
        {
            //checks

            try
            {
                int.Parse(trayNumberTextbox.Text);
            }
            catch
            {
                MessageBox.Show("Please enter numeric tray number", "Error");
                return;
            }

            try
            {
                int.Parse(numberOfSpringsTextbox.Text);
            }
            catch
            {
                MessageBox.Show("Please enter correct number of springs", "Error");
                return;
            }

            try
            {
                float.Parse(traytareTextbox.Text);
            }
            catch
            {
                MessageBox.Show("Please enter correct Tray Tare", "Error");
                return;
            }

            try
            {
                float.Parse(grossWeightTextbox.Text);
            }
            catch
            {
                MessageBox.Show("Please enter correct gross weight", "Error");
                return;
            }
            if (this.dateTimePicker1.Value.Date < this.dateTimePicker2.Value.Date)
            {
                MessageBox.Show("Issue Date is in the future", "Error");
                return;
            }
            if(this.machineNoCombobox.SelectedIndex==0)
            {
                MessageBox.Show("Enter Machine Number", "Error");
                return;
            }
            if (this.edit_form == false)
            {
                bool added = c.addTrayVoucher(dateTimePicker1.Value, dateTimePicker2.Value, trayNumberTextbox.Text, comboBox3.Text, int.Parse(numberOfSpringsTextbox.Text), float.Parse(traytareTextbox.Text), float.Parse(grossWeightTextbox.Text), comboBox1.Text, comboBox2.Text, dynamicLabelChange(), machineNoCombobox.Text);
                if (added == true) this.trayNumberTextbox.Text="";
                else return;
            }
            else
            {
                bool edited = false;
                edited = c.editTrayVoucher(this.Voucher_ID, this.tray_id, dateTimePicker1.Value, dateTimePicker2.Value, trayNumberTextbox.Text, this.old_tray_no, comboBox3.SelectedItem.ToString(), int.Parse(numberOfSpringsTextbox.Text), float.Parse(traytareTextbox.Text), float.Parse(grossWeightTextbox.Text), comboBox1.SelectedItem.ToString(), comboBox2.SelectedItem.ToString(), dynamicLabelChange(), machineNoCombobox.Text);
                if (edited == true)
                {
                    disable_form_edit();
                    this.v1_history.loadData();
                }
                else return;
            }
        }
        public void disable_form_edit()
        {
            this.dateTimePicker2.Enabled = false;
            this.comboBox1.Enabled = false;
            this.comboBox2.Enabled = false;
            this.comboBox3.Enabled = false;
            this.addButton.Enabled = false;
            this.trayNumberTextbox.Enabled = false;
            this.numberOfSpringsTextbox.Enabled = false;
            this.traytareTextbox.Enabled = false;
            this.grossWeightTextbox.Enabled = false;
        }

        private float dynamicLabelChange()
        {
            float gross_weight, tray_tare;
            int number_of_springs;
            float spring_weight = 0F;
            try
            {
                gross_weight = float.Parse(grossWeightTextbox.Text);
            }
            catch
            {
                dynamicWeightLabel.Text = "Enter numeric gross weight";
                return 0F;
            }

            try
            {
                tray_tare = float.Parse(traytareTextbox.Text);
            }
            catch
            {
                dynamicWeightLabel.Text = "Enter numeric tray tare";
                return 0F;
            }

            try
            {
                number_of_springs = int.Parse(numberOfSpringsTextbox.Text);
            }
            catch
            {
                dynamicWeightLabel.Text = "Enter numeric number of springs";
                return 0F;
            }
            for (int i = 0; i < spring_table.Rows.Count; i++)
            {
                if (this.comboBox3.Text == this.spring_table.Rows[i][0].ToString())
                {
                    spring_weight = float.Parse(this.spring_table.Rows[i][1].ToString());
                    break;
                }
            }

            dynamicWeightLabel.Text=(gross_weight - tray_tare - number_of_springs * spring_weight).ToString()+" kg";
            return (gross_weight - tray_tare - number_of_springs * spring_weight);
        }

        private void grossWeightTextbox_TextChanged(object sender, EventArgs e)
        {
            dynamicLabelChange();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            dynamicLabelChange();
        }

        private void numberOfSpringsTextbox_TextChanged(object sender, EventArgs e)
        {
            dynamicLabelChange();
        }

        private void traytareTextbox_TextChanged(object sender, EventArgs e)
        {
            dynamicLabelChange();
        }
    }


}
