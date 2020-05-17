using Factory_Inventory.Factory_Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
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
        bool edit_form, redyeing=false;
        string old_tray_no;
        public int dummyint = 0;
        public bool closed = false;
        public DataTable tray_details = new DataTable();
        DataTable d1;  //stores quality table
        public DataGridView issuesource = new DataGridView();
        public M_V2_trayInputForm()
        {
            InitializeComponent();
            c = new DbConnect();
            //Create drop-down quality list
            var dataSource1 = new List<string>();
            this.d1 = c.getQC('q');
            dataSource1.Add("---Select---");
            for (int i = 0; i < d1.Rows.Count; i++)
            {
                dataSource1.Add(d1.Rows[i][0].ToString());
            }
            this.qualityCB.DataSource = dataSource1;
            this.qualityCB.DisplayMember = "Quality";
            this.qualityCB.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.qualityCB.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.qualityCB.AutoCompleteMode = AutoCompleteMode.SuggestAppend;


            //Create drop-down Company list
            var dataSource2 = new List<string>();
            DataTable d2 = c.getQC('c');

            for (int i = 0; i < d2.Rows.Count; i++)
            {
                dataSource2.Add(d2.Rows[i][0].ToString());
            }
            this.companyNameCB.DataSource = dataSource2;
            this.companyNameCB.DisplayMember = "Company_Names";
            this.companyNameCB.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.companyNameCB.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.companyNameCB.AutoCompleteMode = AutoCompleteMode.SuggestAppend;


            //Create drop-down Spring list
            spring_table = new DataTable();
            var dataSource3 = new List<string>();
            this.spring_table = c.getQC('s');

            for (int i = 0; i < spring_table.Rows.Count; i++)
            {
                dataSource3.Add(spring_table.Rows[i][0].ToString());
            }
            this.springCB.DataSource = dataSource3;
            this.springCB.DisplayMember = "Spring";
            this.springCB.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.springCB.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.springCB.AutoCompleteMode = AutoCompleteMode.SuggestAppend;


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
            this.machineNoCB.DataSource = dataSource4;
            this.machineNoCB.DisplayMember = "Machine_Number";
            this.machineNoCB.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.machineNoCB.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.machineNoCB.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            this.numberOfSpringsTB.Text = "18";
            this.springCB.SelectedIndex = 0;
        }
        public M_V2_trayInputForm(DataRow row, bool isEditable, M_V_history v1_history)
        {
            InitializeComponent();
            this.v1_history = v1_history;
            this.c = new DbConnect();
            this.edit_form = isEditable;

            //Create drop-down quality list
            var dataSource1 = new List<string>();
            this.d1 = c.getQC('q');
            dataSource1.Add("---Select---");
            for (int i = 0; i < d1.Rows.Count; i++)
            {
                dataSource1.Add(d1.Rows[i][0].ToString());
            }
            this.qualityCB.DataSource = dataSource1;
            this.qualityCB.DisplayMember = "Quality";
            this.qualityCB.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.qualityCB.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.qualityCB.AutoCompleteMode = AutoCompleteMode.SuggestAppend;


            //Create drop-down Company list
            var dataSource2 = new List<string>();
            DataTable d2 = c.getQC('c');

            for (int i = 0; i < d2.Rows.Count; i++)
            {
                dataSource2.Add(d2.Rows[i][0].ToString());
            }
            this.companyNameCB.DataSource = dataSource2;
            this.companyNameCB.DisplayMember = "Company_Names";
            this.companyNameCB.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.companyNameCB.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.companyNameCB.AutoCompleteMode = AutoCompleteMode.SuggestAppend;


            //Create drop-down Spring list
            spring_table = new DataTable();
            var dataSource3 = new List<string>();
            this.spring_table = c.getQC('s');

            for (int i = 0; i < spring_table.Rows.Count; i++)
            {
                dataSource3.Add(spring_table.Rows[i][0].ToString());
            }
            this.springCB.DataSource = dataSource3;
            this.springCB.DisplayMember = "Spring";
            this.springCB.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.springCB.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.springCB.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

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
            this.machineNoCB.DataSource = dataSource4;
            this.machineNoCB.DisplayMember = "Machine_Number";
            this.machineNoCB.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.machineNoCB.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.machineNoCB.AutoCompleteMode = AutoCompleteMode.SuggestAppend;


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
                this.dateTimePickerDTP.Enabled = false;
                this.qualityCB.Enabled = false;
                this.companyNameCB.Enabled = false;
                this.springCB.Enabled = false;
                this.addButton.Enabled = false;
                this.trayNumberTB.Enabled = false;
                this.numberOfSpringsTB.Enabled = false;
                this.traytareTB.Enabled = false;
                this.grossWeightTB.Enabled = false;
                this.machineNoCB.Enabled = false;
            }
            else
            {
                this.dateTimePickerDTP.Enabled = true;
                this.qualityCB.Enabled = true;
                this.companyNameCB.Enabled = true;
                this.springCB.Enabled = true;
                this.addButton.Enabled = true;
                this.trayNumberTB.Enabled = true;
                this.numberOfSpringsTB.Enabled = true;
                this.traytareTB.Enabled = true;
                this.grossWeightTB.Enabled = true;
                this.machineNoCB.Enabled = true;
            }
            this.dateTimePicker1.Value = Convert.ToDateTime(row["Input_Date"].ToString());
            this.dateTimePickerDTP.Value = Convert.ToDateTime(row["Tray_Production_Date"].ToString());
            this.trayNumberTB.Text = row["Tray_No"].ToString();
            this.springCB.SelectedIndex = this.springCB.FindStringExact(row["Spring"].ToString());
            this.qualityCB.SelectedIndex = this.qualityCB.FindStringExact(row["Quality"].ToString());
            this.companyNameCB.SelectedIndex = this.companyNameCB.FindStringExact(row["Company_Name"].ToString());
            this.machineNoCB.SelectedIndex = this.machineNoCB.FindStringExact(row["Machine_No"].ToString());
            this.Voucher_ID = int.Parse(row["Voucher_ID"].ToString());
            this.numberOfSpringsTB.Text = row["Number_Of_Springs"].ToString();
            this.traytareTB.Text = row["Tray_Tare"].ToString();
            this.grossWeightTB.Text = row["Gross_Weight"].ToString();
            this.qualityBeforeTwistTB.Text = row["Quality_Before_Twist"].ToString();
            this.old_tray_no = row["Tray_No"].ToString();


        }
        public M_V2_trayInputForm(string input_date, string production_date, string tray_no, string spring, int no_of_springs, float tray_tare, float gross_weight, string quality, string company_name, string machine_no, DataGridView d)
        {
            InitializeComponent();
            this.issuesource = d;
            c = new DbConnect();
            //Create drop-down quality list
            var dataSource1 = new List<string>();
            this.d1 = c.getQC('q');
            dataSource1.Add("---Select---");
            for (int i = 0; i < d1.Rows.Count; i++)
            {
                dataSource1.Add(d1.Rows[i][0].ToString());
            }
            this.qualityCB.DataSource = dataSource1;
            this.qualityCB.DisplayMember = "Quality";
            this.qualityCB.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.qualityCB.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.qualityCB.AutoCompleteMode = AutoCompleteMode.SuggestAppend;


            //Create drop-down Company list
            var dataSource2 = new List<string>();
            DataTable d2 = c.getQC('c');

            for (int i = 0; i < d2.Rows.Count; i++)
            {
                dataSource2.Add(d2.Rows[i][0].ToString());
            }
            this.companyNameCB.DataSource = dataSource2;
            this.companyNameCB.DisplayMember = "Company_Names";
            this.companyNameCB.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.companyNameCB.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.companyNameCB.AutoCompleteMode = AutoCompleteMode.SuggestAppend;


            //Create drop-down Spring list
            spring_table = new DataTable();
            var dataSource3 = new List<string>();
            this.spring_table = c.getQC('s');

            for (int i = 0; i < spring_table.Rows.Count; i++)
            {
                dataSource3.Add(spring_table.Rows[i][0].ToString());
            }
            this.springCB.DataSource = dataSource3;
            this.springCB.DisplayMember = "Spring";
            this.springCB.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.springCB.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.springCB.AutoCompleteMode = AutoCompleteMode.SuggestAppend;


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
            this.machineNoCB.DataSource = dataSource4;
            this.machineNoCB.DisplayMember = "Machine_Number";
            this.machineNoCB.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.machineNoCB.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.machineNoCB.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            this.tray_details.Columns.Add("Date_Of_Input");
            this.tray_details.Columns.Add("Date_Of_Production");
            this.tray_details.Columns.Add("Tray_No");
            this.tray_details.Columns.Add("Spring");
            this.tray_details.Columns.Add("No_Of_Springs");
            this.tray_details.Columns.Add("Tray_Tare");
            this.tray_details.Columns.Add("Gross_Weight");
            this.tray_details.Columns.Add("Quality");
            this.tray_details.Columns.Add("Company_Name");
            this.tray_details.Columns.Add("Machine_No");

            if (input_date!=null) this.dateTimePicker1.Value = this.dateTimePicker1.Value = Convert.ToDateTime(input_date);
            if (production_date != null) this.dateTimePickerDTP.Value = this.dateTimePickerDTP.Value = Convert.ToDateTime(production_date);
            if (tray_no != null) this.trayNumberTB.Text = tray_no;
            if (spring != null) this.springCB.SelectedIndex = this.springCB.FindStringExact(spring);
            if (no_of_springs != -1) this.numberOfSpringsTB.Text = no_of_springs.ToString();
            if (tray_tare != -1F) this.traytareTB.Text = tray_tare.ToString();
            if (gross_weight != -1F) this.grossWeightTB.Text = gross_weight.ToString();
            if (quality != null) this.qualityCB.SelectedIndex= this.qualityCB.FindStringExact(quality);
            if (company_name != null) this.companyNameCB.SelectedIndex = this.companyNameCB.FindStringExact(company_name);
            if (machine_no != null) this.machineNoCB.SelectedIndex = this.machineNoCB.FindStringExact(machine_no);

            this.addButton.Text = "Add Tray";
            this.redyeing = true;
            if(d.Rows.Count!=0) this.tray_details = (DataTable)d.DataSource;
            this.StartPosition= FormStartPosition.Manual;
        }
        private void addButton_Click(object sender, EventArgs e)
        {
            //checks

            try
            {
                int.Parse(trayNumberTB.Text);
            }
            catch
            {
                c.ErrorBox("Please enter numeric tray number", "Error");
                return;
            }
            try
            {
                int.Parse(numberOfSpringsTB.Text);
            }
            catch
            {
                c.ErrorBox("Please enter correct number of springs", "Error");
                return;
            }
            try
            {
                float.Parse(traytareTB.Text);
            }
            catch
            {
                c.ErrorBox("Please enter correct Tray Tare", "Error");
                return;
            }
            try
            {
                float.Parse(grossWeightTB.Text);
            }
            catch
            {
                c.ErrorBox("Please enter correct gross weight", "Error");
                return;
            }
            if (this.dateTimePicker1.Value.Date < this.dateTimePickerDTP.Value.Date)
            {
                c.ErrorBox("Issue Date is in the future", "Error");
                return;
            }
            if(this.machineNoCB.SelectedIndex==0)
            {
                c.ErrorBox("Enter Machine Number", "Error");
                return;
            }
            if(this.redyeing==true)
            {
                DataRow row = tray_details.NewRow();
                row.Table.Columns.Add("Sl No");
                row["Sl No"] = this.issuesource.Rows.Count+1;
                row["Date_Of_Input"] = this.dateTimePicker1.Value.Date.ToString().Substring(0, 10);
                row["Date_Of_Production"] = this.dateTimePickerDTP.Value.Date.ToString().Substring(0, 10);
                row["Tray_No"] = (this.trayNumberTB.Text);
                row["Spring"] = (this.springCB.Text);
                row["No_Of_Springs"] = (this.numberOfSpringsTB.Text);
                row["Tray_Tare"] = (this.traytareTB.Text);
                row["Gross_Weight"] = (this.grossWeightTB.Text);
                row["Quality"] = (this.qualityCB.Text);
                row["Company_Name"] = (this.companyNameCB.Text);
                row["Machine_No"] = (this.machineNoCB.Text);
                row.Table.Columns.Add("Net_Weight");
                row["Net_Weight"] = dynamicLabelChange();

                this.tray_details.Rows.Add(row);
                this.trayNumberTB.Text = "";
                this.grossWeightTB.Text = "";
                this.traytareTB.Text = "";
                this.trayNumberTB.Focus();
                dummyint++;
                this.issuesource.DataSource = this.tray_details;
                this.issuesource.Columns.OfType<DataGridViewColumn>().ToList().ForEach(col => col.Visible = false);
                this.issuesource.Columns["Sl No"].Visible = true;
                this.issuesource.Columns["Sl No"].DisplayIndex=0;
                this.issuesource.Columns["Sl No"].Width= 80;
                this.issuesource.Columns["Tray_No"].Visible = true;
                this.issuesource.Columns["Tray_No"].HeaderText= "Tray No";
                this.issuesource.Columns["Tray_No"].DisplayIndex = 2;
                this.issuesource.Columns["Quality"].Visible = true;
                this.issuesource.Columns["Quality"].DisplayIndex = 4;
                this.issuesource.Columns["Quality"].Width= 150;
                this.issuesource.Columns["Net_Weight"].Visible = true;
                this.issuesource.Columns["Net_Weight"].DisplayIndex = 6;
            }
            else
            {
                if (this.edit_form == false)
                {
                    bool added = c.addTrayVoucher(dateTimePicker1.Value, dateTimePickerDTP.Value, trayNumberTB.Text, springCB.Text, int.Parse(numberOfSpringsTB.Text), float.Parse(traytareTB.Text), float.Parse(grossWeightTB.Text), qualityCB.Text, companyNameCB.Text, dynamicLabelChange(), machineNoCB.Text, qualityBeforeTwistTB.Text);
                    if (added == true)
                    {
                        this.trayNumberTB.Text = "";
                        this.grossWeightTB.Text = "";
                        this.traytareTB.Text = "";
                        this.trayNumberTB.Focus();
                    }
                    else return;
                }
                else
                {
                    bool edited = false;
                    edited = c.editTrayVoucher(this.Voucher_ID, this.tray_id, dateTimePicker1.Value, dateTimePickerDTP.Value, trayNumberTB.Text, this.old_tray_no, springCB.SelectedItem.ToString(), int.Parse(numberOfSpringsTB.Text), float.Parse(traytareTB.Text), float.Parse(grossWeightTB.Text), qualityCB.SelectedItem.ToString(), companyNameCB.SelectedItem.ToString(), dynamicLabelChange(), machineNoCB.Text, this.qualityBeforeTwistTB.Text);
                    if (edited == true)
                    {
                        disable_form_edit();
                        this.v1_history.loadData();
                    }
                    else return;
                }
            }
            
        }
        public void disable_form_edit()
        {
            this.dateTimePickerDTP.Enabled = false;
            this.qualityCB.Enabled = false;
            this.companyNameCB.Enabled = false;
            this.springCB.Enabled = false;
            this.addButton.Enabled = false;
            this.trayNumberTB.Enabled = false;
            this.numberOfSpringsTB.Enabled = false;
            this.traytareTB.Enabled = false;
            this.grossWeightTB.Enabled = false;
            this.machineNoCB.Enabled = false;
        }
        private float dynamicLabelChange()
        {
            float gross_weight, tray_tare;
            int number_of_springs;
            float spring_weight = 0F;
            try
            {
                gross_weight = float.Parse(grossWeightTB.Text);
            }
            catch
            {
                dynamicWeightLabel.Text = "Enter numeric gross weight";
                return 0F;
            }

            try
            {
                tray_tare = float.Parse(traytareTB.Text);
            }
            catch
            {
                dynamicWeightLabel.Text = "Enter numeric tray tare";
                return 0F;
            }

            try
            {
                number_of_springs = int.Parse(numberOfSpringsTB.Text);
            }
            catch
            {
                dynamicWeightLabel.Text = "Enter numeric number of springs";
                return 0F;
            }
            for (int i = 0; i < spring_table.Rows.Count; i++)
            {
                if (this.springCB.Text == this.spring_table.Rows[i][0].ToString())
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
            this.springWeightTB.Text = (c.getSpringWeight(this.springCB.Text) * 1000F).ToString(); ;
        }
        private void M_V2_trayInputForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                Console.WriteLine("Up");
                this.SelectNextControl((Control)sender, false, true, true, true);
            }
            else if (e.KeyCode == Keys.Down)
            {
                Console.WriteLine("Down");
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }
        private void M_V2_trayInputForm_Load(object sender, EventArgs e)
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

            this.dateTimePickerDTP.Focus();
        }
        private void M_V2_trayInputForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.closed = true;
        }
        private void qualityCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(this.qualityCB.SelectedIndex>0)
            {
                this.qualityBeforeTwistTB.Text = this.d1.Rows[this.qualityCB.SelectedIndex - 1]["Quality_Before_Twist"].ToString();
            }
            else
            {
                this.qualityBeforeTwistTB.Text = "";
            }

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
