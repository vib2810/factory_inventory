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
        int voucher_id, state, tray_id;
        bool edit_form, redyeing=false;
        string old_tray_no;
        public int dummyint = 0;
        public bool closed = false;
        public DataTable tray_details = new DataTable();
        public M_V3_issueToReDyeingForm form; 
        DataTable d1;  //stores quality table
        public DataGridView issuesource = new DataGridView();
        private bool edit_reyeing_tray = false;
        private int edit_redyeing_tray_index;
        private string edit_redyeing_old_tray_no="";
        //Form functions
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
            DataTable dm = c.getTableData("Machine_No", "*", "");
            dataSource4.Add("---Select---");
            for (int i = 0; i < dm.Rows.Count; i++)
            {
                dataSource4.Add(dm.Rows[i]["Machine_No"].ToString());
            }
            this.machineNoCB.DataSource = dataSource4;
            this.machineNoCB.DisplayMember = "Machine_Number";
            this.machineNoCB.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.machineNoCB.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.machineNoCB.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            //Create drop-down Gradeng list
            var dataSource5 = new List<string>();
            dataSource5.Add("---Select---");
            dataSource5.Add("1st");
            dataSource5.Add("PQ");
            dataSource5.Add("CLQ");
            this.gradeCB.DataSource = dataSource5;
            this.gradeCB.DisplayMember = "Grade";
            this.gradeCB.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.gradeCB.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.gradeCB.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            this.numberOfSpringsTB.Text = "18";
            this.springCB.SelectedIndex = 0;
            this.gradeCB.SelectedIndex = 1;
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

            var dataSource4 = new List<string>();
            DataTable dm = c.getTableData("Machine_No", "*", "");
            dataSource4.Add("---Select---");
            for (int i = 0; i < dm.Rows.Count; i++)
            {
                dataSource4.Add(dm.Rows[i]["Machine_No"].ToString());
            }
            this.machineNoCB.DataSource = dataSource4;
            this.machineNoCB.DisplayMember = "Machine_Number";
            this.machineNoCB.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.machineNoCB.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.machineNoCB.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            //Create drop-down Spring list
            var dataSource5 = new List<string>();
            dataSource5.Add("---Select---");
            dataSource5.Add("1st");
            dataSource5.Add("PQ");
            dataSource5.Add("CLQ");
            this.gradeCB.DataSource = dataSource5;
            this.gradeCB.DisplayMember = "Grade";
            this.gradeCB.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.gradeCB.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.gradeCB.AutoCompleteMode = AutoCompleteMode.SuggestAppend;


            this.tray_id = int.Parse(row["Tray_ID"].ToString());
            this.state = c.getTrayState(this.tray_id);
            if (this.state == -1)
            {
                label1.Text = "Tray Has Been Processed \nTray Number may now be in\nuse in another Voucher";
                label1.ForeColor = Color.Red;
                this.deleteButton.Visible = true;
                this.deleteButton.Enabled = false;
                isEditable = false;
            }
            else if (this.state == 2)
            {
                label1.Text = "Voucher is not editable \nTray has been sent for dyeing";
                label1.ForeColor = Color.Red;
                this.deleteButton.Visible = true;
                this.deleteButton.Enabled = false;
                isEditable = false;
            }
            
            if (isEditable == false)
            {
                this.Text += "(View Only)";
                this.deleteButton.Visible = true;
                this.disable_form_edit();
            }
            else
            {
                this.Text += "(Edit)";
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
                this.gradeCB.Enabled = true;
            }
            this.dateTimePicker1.Value = Convert.ToDateTime(row["Input_Date"].ToString());
            this.dateTimePickerDTP.Value = Convert.ToDateTime(row["Tray_Production_Date"].ToString());
            this.trayNumberTB.Text = row["Tray_No"].ToString();
            this.springCB.SelectedIndex = this.springCB.FindStringExact(row["Spring"].ToString());
            this.qualityCB.SelectedIndex = this.qualityCB.FindStringExact(row["Quality"].ToString());
            this.companyNameCB.SelectedIndex = this.companyNameCB.FindStringExact(row["Company_Name"].ToString());
            this.machineNoCB.SelectedIndex = this.machineNoCB.FindStringExact(row["Machine_No"].ToString());
            this.voucher_id = int.Parse(row["Voucher_ID"].ToString());
            this.numberOfSpringsTB.Text = row["Number_Of_Springs"].ToString();
            this.traytareTB.Text = row["Tray_Tare"].ToString();
            this.grossWeightTB.Text = row["Gross_Weight"].ToString();
            this.qualityBeforeTwistTB.Text = row["Quality_Before_Twist"].ToString();
            this.old_tray_no = row["Tray_No"].ToString();
            this.gradeCB.SelectedIndex = this.gradeCB.FindStringExact(row["Grade"].ToString());
        }
        public M_V2_trayInputForm(string production_date, string tray_no, string spring, int no_of_springs, float tray_tare, float gross_weight, string quality, string company_name, string machine_no, string grade, float redyeing, int no_of_springs_rd,  int edit_row_index, M_V3_issueToReDyeingForm f)
        {
            InitializeComponent();
            this.issuesource = f.dataGridView1;
            this.form = f;
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
            DataTable dm = c.getTableData("Machine_No", "*", "");
            dataSource4.Add("---Select---");
            for (int i = 0; i < dm.Rows.Count; i++)
            {
                dataSource4.Add(dm.Rows[i]["Machine_No"].ToString());
            }
            this.machineNoCB.DataSource = dataSource4;
            this.machineNoCB.DisplayMember = "Machine_Number";
            this.machineNoCB.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.machineNoCB.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.machineNoCB.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            //Create drop-down Spring list
            var dataSource5 = new List<string>();
            dataSource5.Add("---Select---");
            dataSource5.Add("1st");
            dataSource5.Add("PQ");
            dataSource5.Add("CLQ");
            this.gradeCB.DataSource = dataSource5;
            this.gradeCB.DisplayMember = "Grade";
            this.gradeCB.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.gradeCB.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.gradeCB.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            this.tray_details.Columns.Add("Sl No");
            this.tray_details.Columns.Add("Date Of Production");
            this.tray_details.Columns.Add("Tray No");
            this.tray_details.Columns.Add("Spring");
            this.tray_details.Columns.Add("No Of Springs");
            this.tray_details.Columns.Add("Tray Tare");
            this.tray_details.Columns.Add("Gross Weight");
            this.tray_details.Columns.Add("Quality");
            this.tray_details.Columns.Add("Company Name");
            this.tray_details.Columns.Add("Machine No");
            this.tray_details.Columns.Add("Net Weight");
            this.tray_details.Columns.Add("Quality Before Twist");
            this.tray_details.Columns.Add("Grade");
            this.tray_details.Columns.Add("Redyeing");
            this.tray_details.Columns.Add("No Of Springs RD");



            if (production_date != null) this.dateTimePickerDTP.Value = this.dateTimePickerDTP.Value = Convert.ToDateTime(production_date);
            if (tray_no != null)
            {
                this.trayNumberTB.Text = tray_no;
                this.edit_redyeing_old_tray_no = tray_no;
            }
            if (spring != null) this.springCB.SelectedIndex = this.springCB.FindStringExact(spring);
            if (no_of_springs != -1) this.numberOfSpringsTB.Text = no_of_springs.ToString();
            if (tray_tare != -1F) this.traytareTB.Text = tray_tare.ToString();
            if (gross_weight != -1F) this.grossWeightTB.Text = gross_weight.ToString();
            if (quality != null) this.qualityCB.SelectedIndex = this.qualityCB.FindStringExact(quality);
            this.qualityCB.Enabled = false;
            if (company_name != null) this.companyNameCB.SelectedIndex = this.companyNameCB.FindStringExact(company_name);
            this.companyNameCB.Enabled = false;
            if (machine_no != null) this.machineNoCB.SelectedIndex = this.machineNoCB.FindStringExact(machine_no);
            if (grade != null) this.gradeCB.SelectedIndex = this.gradeCB.FindStringExact(grade);
            this.gradeCB.Enabled = false;
            if (redyeing != -1F) this.redyeingPerTB.Text = redyeing.ToString();
            if (no_of_springs_rd != -1) this.redyeingSpringsTB.Text = no_of_springs_rd.ToString();

            this.Text = "Add Tray";
            this.addButton.Text = "Add Tray";
            this.redyeing = true;
            if (this.issuesource.Rows.Count != 0) this.tray_details = (DataTable)this.issuesource.DataSource;
            this.StartPosition = FormStartPosition.Manual;

            //set visible and readonly
            this.redyeingPerTB.Visible = true;
            this.redyeingSpringsTB.Visible = true;
            this.add_edit_rd_paramsButton.Visible = true;
            this.label16.Visible = true;
            this.label17.Visible = true;
            this.label18.Visible = true;
            this.grossWeightTB.ReadOnly = true;
            this.numberOfSpringsTB.ReadOnly = true;
            this.traytareTB.ReadOnly = true;

            if (edit_row_index != -1)
            {
                this.Text = "Edit Tray";
                this.edit_reyeing_tray = true;
                this.addButton.Text = "Confirm Edit";
                this.edit_redyeing_tray_index = edit_row_index;
            }
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
            if (Global.access == 2)
            {
                this.deleteButton.Visible = false;
            }
        }
        private void M_V2_trayInputForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.closed = true;
        }
        
        //Own Functions
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
            this.gradeCB.Enabled = false;
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
                return -1F;
            }

            try
            {
                tray_tare = float.Parse(traytareTB.Text);
            }
            catch
            {
                dynamicWeightLabel.Text = "Enter numeric tray tare";
                return -1F;
            }

            try
            {
                number_of_springs = int.Parse(numberOfSpringsTB.Text);
            }
            catch
            {
                dynamicWeightLabel.Text = "Enter numeric number of springs";
                return -1F;
            }
            for (int i = 0; i < spring_table.Rows.Count; i++)
            {
                if (this.springCB.Text == this.spring_table.Rows[i][0].ToString())
                {
                    spring_weight = float.Parse(this.spring_table.Rows[i][1].ToString());
                    break;
                }
            }

            dynamicWeightLabel.Text = (gross_weight - tray_tare - number_of_springs * spring_weight).ToString("F3");
            return (gross_weight - tray_tare - number_of_springs * spring_weight);
        }

        //Clicks
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
                c.ErrorBox("Production Date is in the future", "Error");
                return;
            }
            if (this.qualityCB.SelectedIndex == 0)
            {
                c.ErrorBox("Enter Quality", "Error");
                return;
            }
            if (this.machineNoCB.SelectedIndex==0)
            {
                c.ErrorBox("Enter Machine Number", "Error");
                return;
            }
            if(dynamicLabelChange()<=0F)
            {
                c.ErrorBox("Net Weight cannot be negative or zero", "Error");
                return;
            }
            if(this.gradeCB.SelectedIndex==0)
            {
                c.ErrorBox("Please select Grade");
                return;
            }
            if(this.redyeing==true)
            {
                if(this.dateTimePickerDTP.Value.Date > this.form.issueDateDTP.Value.Date)
                {
                    c.ErrorBox("Tray production date cannot be ahead of Redyeing issue date", "Error");
                    return;
                }
                DataRow row = tray_details.NewRow();
                if(this.edit_reyeing_tray == false)
                {
                    row["Sl No"] = this.issuesource.Rows.Count + 1;
                }
                else
                {
                    row["Sl No"] = this.edit_redyeing_tray_index + 1;
                }
                row["Date Of Production"] = this.dateTimePickerDTP.Value.Date.ToString().Substring(0, 10);
                row["Tray No"] = (this.trayNumberTB.Text);
                row["Spring"] = (this.springCB.Text);
                row["No Of Springs"] = (this.numberOfSpringsTB.Text);
                row["Tray Tare"] = (this.traytareTB.Text);
                row["Gross Weight"] = (this.grossWeightTB.Text);
                row["Quality"] = (this.qualityCB.Text);
                row["Company Name"] = (this.companyNameCB.Text);
                row["Machine No"] = (this.machineNoCB.Text);
                row["Net Weight"] = dynamicLabelChange();
                row["Quality Before Twist"] = this.qualityBeforeTwistTB.Text;
                row["Grade"] = this.gradeCB.Text;
                row["No Of Springs RD"] = this.redyeingSpringsTB.Text;
                row["Redyeing"] = this.redyeingPerTB.Text;
                
                for (int i = 0; i < this.issuesource.Rows.Count; i++)
                {
                    if (this.issuesource.Rows[i].Cells["Tray No"].Value.ToString() == row["Tray No"].ToString() && this.edit_reyeing_tray == false)
                    {
                        c.ErrorBox("Repeated tray number in row " + (i + 1).ToString(), "Error");
                        return;
                    }
                }
                DataTable dttray = c.getTableRows("Tray_Active", "Tray_No='" + row["Tray No"].ToString() + "'");
                if(dttray.Rows.Count!=0)
                {
                    if(this.edit_reyeing_tray==true)
                    {
                        if (dttray.Rows[0]["Tray_No"].ToString() == this.edit_redyeing_old_tray_no)
                        {
                            //allowed
                        }
                        else
                        {
                            c.ErrorBox("Tray number " + row["Tray No"].ToString() + " is already in use", "Error");
                            return;
                        }
                    }
                    else
                    {
                        c.ErrorBox("Tray number " + row["Tray No"].ToString() + " is already in use", "Error");
                        return;
                    }
                }
                if(this.edit_reyeing_tray == true)
                {
                    this.tray_details.Rows[this.edit_redyeing_tray_index].Delete();
                    this.tray_details.Rows.InsertAt(row, this.edit_redyeing_tray_index);
                }
                else
                {
                    this.tray_details.Rows.Add(row);
                    this.trayNumberTB.Text = "";
                    this.grossWeightTB.Text = "";
                    this.traytareTB.Text = "";
                    this.gradeCB.SelectedIndex = 1;
                    this.trayNumberTB.Focus();
                }

                this.issuesource.DataSource = this.tray_details;
                if(this.issuesource.SelectedRows.Count>0)
                {
                    this.issuesource.SelectedRows[0].Selected = false;
                }
                this.issuesource.Columns.OfType<DataGridViewColumn>().ToList().ForEach(col => col.Visible = false);
                this.issuesource.Columns["Sl No"].Visible = true;
                this.issuesource.Columns["Sl No"].DisplayIndex=0;
                this.issuesource.Columns["Tray No"].Visible = true;
                this.issuesource.Columns["Tray No"].HeaderText= "Tray No";
                this.issuesource.Columns["Tray No"].DisplayIndex = 2;
                this.issuesource.Columns["Quality"].Visible = true;
                this.issuesource.Columns["Quality"].DisplayIndex = 4;
                this.issuesource.Columns["Net Weight"].Visible = true;
                this.issuesource.Columns["Net Weight"].DisplayIndex = 6;
                this.issuesource.Columns["No Of Springs RD"].Visible = true;
                this.issuesource.Columns["No Of Springs RD"].DisplayIndex = 8;
                this.issuesource.Columns["No Of Springs RD"].HeaderText = "No of redyeing springs";
                this.issuesource.Columns["No of Springs"].Visible = true;
                this.issuesource.Columns["No of Springs"].DisplayIndex = 10;
                c.auto_adjust_dgv(this.issuesource);
                this.issuesource.Columns["Sl No"].Width= 40;
                this.issuesource.Columns["Quality"].Width= 150;
                this.form.CellSum();
                if (this.edit_reyeing_tray == true)
                {
                    this.Close();
                }
            }
            else
            {
                if (this.edit_form == false)
                {
                    bool added = c.addTrayVoucher(dateTimePicker1.Value, dateTimePickerDTP.Value, trayNumberTB.Text, springCB.Text, int.Parse(numberOfSpringsTB.Text), float.Parse(traytareTB.Text), float.Parse(grossWeightTB.Text), qualityCB.Text, companyNameCB.Text, dynamicLabelChange(), machineNoCB.Text, qualityBeforeTwistTB.Text, gradeCB.Text);
                    if (added == true)
                    {
                        this.trayNumberTB.Text = "";
                        this.grossWeightTB.Text = "";
                        this.traytareTB.Text = "";
                        this.gradeCB.SelectedIndex = 1;
                        this.trayNumberTB.Focus();
                    }
                    else return;
                }
                else
                {
                    bool edited = false;
                    edited = c.editTrayVoucher(this.voucher_id, this.tray_id, dateTimePicker1.Value, dateTimePickerDTP.Value, trayNumberTB.Text, this.old_tray_no, springCB.SelectedItem.ToString(), int.Parse(numberOfSpringsTB.Text), float.Parse(traytareTB.Text), float.Parse(grossWeightTB.Text), qualityCB.SelectedItem.ToString(), companyNameCB.SelectedItem.ToString(), dynamicLabelChange(), machineNoCB.Text, this.qualityBeforeTwistTB.Text, gradeCB.Text);
                    if (edited == true)
                    {
                        disable_form_edit();
                        this.v1_history.loadData();
                    }
                    else return;
                }
            }
            
        }
        private void deleteButton_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Confirm Delete", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                bool deleted = c.deleteTrayVoucher(this.voucher_id);
                if (deleted == true)
                {
                    c.SuccessBox("Voucher Deleted Successfully");
                    this.deleteButton.Enabled = false;
                    v1_history.loadData();
                }
                else return;
            }
            else if (dialogResult == DialogResult.No)
            {
                return;
            }
        }
        private void zeroRDButton_Click(object sender, EventArgs e)
        {
            this.redyeingPerTB.Text = "0";
        }
        private void fullRDButton_Click(object sender, EventArgs e)
        {
            this.redyeingPerTB.Text = this.dynamicLabelChange().ToString();
        }
        private void add_edit_rd_paramsButton_Click(object sender, EventArgs e)
        {
            try
            {
                float.Parse(springWeightTB.Text);
            }
            catch
            {
                c.ErrorBox("Please Select Spring Type");
                return;
            }
            this.springCB.Enabled = false;
            Structures.Tray_Params tray_Params = new Structures.Tray_Params(0);
            if (String.IsNullOrEmpty(this.grossWeightTB.Text) == false) tray_Params.gross_wt = float.Parse(this.grossWeightTB.Text);
            tray_Params.net_wt = this.dynamicLabelChange();
            if (String.IsNullOrEmpty(this.traytareTB.Text) == false) tray_Params.tray_tare = float.Parse(this.traytareTB.Text);
            if (String.IsNullOrEmpty(this.springWeightTB.Text) == false) tray_Params.spring_wt = float.Parse(this.springWeightTB.Text);
            if (String.IsNullOrEmpty(this.numberOfSpringsTB.Text) == false) tray_Params.no_of_springs = int.Parse(this.numberOfSpringsTB.Text);
            if (String.IsNullOrEmpty(this.redyeingPerTB.Text) == false) tray_Params.rd_percentage = float.Parse(this.redyeingPerTB.Text);
            if (String.IsNullOrEmpty(this.redyeingSpringsTB.Text) == false) tray_Params.s_rd = int.Parse(this.redyeingSpringsTB.Text);
            TrayCalc f = new TrayCalc(tray_Params);
            f.ShowDialog();
            tray_Params = f.tray_Params;
            this.grossWeightTB.Text = tray_Params.gross_wt.ToString();
            this.numberOfSpringsTB.Text= tray_Params.no_of_springs.ToString();
            this.traytareTB.Text= tray_Params.tray_tare.ToString();
            this.dynamicLabelChange();
            this.redyeingPerTB.Text = tray_Params.rd_percentage.ToString();
            this.redyeingSpringsTB.Text = tray_Params.s_rd.ToString();
        }

        //Text or Index changed
        private void grossWeightTextbox_TextChanged(object sender, EventArgs e)
        {
            dynamicLabelChange();
        }
        private void springCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            ////if(string.IsNullOrEmpty(this.redyeingPerTB.Text) == false && float.TryParse(this.dynamicWeightLabel.Text, out result) == true)
            //{
            //    float old_net_wt = float.Parse(this.dynamicWeightLabel.Text);
            //    float new_net_wt = dynamicLabelChange();
            //    float nrd = old_net_wt * float.Parse(this.redyeingPerTB.Text);
            //    this.redyeingPerTB.Text = (nrd / new_net_wt).ToString();
            //}
            string spring_wt = "";
            for (int i = 0; i < spring_table.Rows.Count; i++)
            {
                if (spring_table.Rows[i]["Spring"].ToString()==this.springCB.Text)
                {
                    spring_wt = spring_table.Rows[i]["Spring_Weight"].ToString();
                }
            }
            this.springWeightTB.Text = spring_wt;

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
