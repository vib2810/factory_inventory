using Factory_Inventory.Factory_Classes;
using Microsoft.SqlServer.Management.SqlParser.Parser;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Forms;

namespace Factory_Inventory
{
    public partial class M_V5_cartonProductionOpeningForm : Form
    {
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Tab &&
                dataGridView1.EditingControl != null &&
                msg.HWnd == dataGridView1.EditingControl.Handle &&
                dataGridView1.SelectedCells
                    .Cast<DataGridViewCell>()
                    .Any(x => x.ColumnIndex == 6))
            {
                this.edit_cmd_send = true;
                Console.WriteLine("Sending Tab");
                SendKeys.Send("{Tab}");
                return false;
            }

            if (keyData == Keys.Enter &&
                dataGridView1.EditingControl != null &&
                msg.HWnd == dataGridView1.EditingControl.Handle &&
                dataGridView1.SelectedCells
                    .Cast<DataGridViewCell>()
                    .Any(x => x.ColumnIndex == 1))
            {
                SendKeys.Send("{Enter}");
                return false;
            }

            if (keyData == Keys.F2)
            {
                Console.WriteLine("dgv1");
                this.dataGridView1.Focus();
                this.ActiveControl = dataGridView1;
                this.dataGridView1.CurrentCell = dataGridView1[2, 0];
                return false;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private DbConnect c=new DbConnect();
        private bool edit_cmd_send = false;
        private bool edit_form = false;
        private List<string> batch_nos;
        private M_V_history v1_history;
        private int voucher_id;
        Dictionary<Tuple<string, string>, bool> carton_editable = new Dictionary<Tuple<string, string>, bool>();
        DateTimePicker dtp = new DateTimePicker();
        public M_V5_cartonProductionOpeningForm()
        {
            InitializeComponent();
            this.initialise_comboboxes();
            this.c = new DbConnect();
            this.batch_nos = new List<string>();
            dataGridView1.RowCount = 10;
            c.set_dgv_column_sort_state(this.dataGridView1, DataGridViewColumnSortMode.NotSortable);
        }
        public M_V5_cartonProductionOpeningForm(DataRow row, bool isEditable, M_V_history v1_history)
        {

            InitializeComponent();
            this.edit_form = true;
            this.v1_history = v1_history;
            this.c = new DbConnect();

            this.initialise_comboboxes();
            if (isEditable == false)
            {
                this.Text += "(View Only)";
                this.deleteButton.Visible = true;
                this.deleteButton.Enabled = true;
                this.disable_form_edit();
            }
            else
            {
                //no option to edit company name and quality
                Console.WriteLine("Else");
                this.Text += "(Edit)";
                this.saveButton.Enabled = true;
                this.dataGridView1.Enabled = true;
                this.dataGridView1.ReadOnly = false;
            }
            this.inputDate.Value = Convert.ToDateTime(row["Date_Of_Input"].ToString());
            this.voucher_id = int.Parse(row["Voucher_ID"].ToString());

            Dictionary<Tuple<string, string>, DataRow> carton_dict = new Dictionary<Tuple<string, string>, DataRow>();
            DataTable carton_info = c.getTableData("Carton_Produced", "*", "Production_Voucher_ID = "+this.voucher_id+" AND Batch_No_Arr IS NULL");
            c.printDataTable(carton_info);
            for (int i = 0; i < carton_info.Rows.Count; i++)
            {
                DateTime d = Convert.ToDateTime(carton_info.Rows[i]["Date_Of_Production"].ToString());
                carton_dict.Add(new Tuple<string, string>(carton_info.Rows[i]["Carton_No"].ToString(), c.getFinancialYear(d)), carton_info.Rows[i]);
            }
            dataGridView1.RowCount = carton_info.Rows.Count + 1;
            bool flag = false;
            for (int i = 0; i < carton_info.Rows.Count; i++)
            {
                DateTime d = Convert.ToDateTime(carton_info.Rows[i]["Date_Of_Production"].ToString());
                Tuple<string, string> temp = new Tuple<string, string>(carton_info.Rows[i]["Carton_No"].ToString(), c.getFinancialYear(d));
                DataRow carton_row = carton_dict[temp];
                if (carton_row == null)
                {
                    continue;
                }
                string correctformat = Convert.ToDateTime(carton_row["Date_Of_Production"].ToString()).Date.ToString().Substring(0, 10);
                dataGridView1.Rows[i].Cells["Date_Of_Production"].Value = correctformat;
                dataGridView1.Rows[i].Cells["Carton_No"].Value = carton_row["Carton_No"].ToString();
                dataGridView1.Rows[i].Cells["Grade"].Value = carton_row["Grade"].ToString();
                dataGridView1.Rows[i].Cells["Colour"].Value = carton_row["Colour"].ToString();
                dataGridView1.Rows[i].Cells["Quality"].Value = carton_row["Quality"].ToString();
                dataGridView1.Rows[i].Cells["Net_Weight"].Value = carton_row["Net_Weight"].ToString();

                //Sold carton will be coloured green
                if (carton_row["Carton_State"].ToString() != "1")
                {
                    flag = true;
                    this.carton_editable[temp] = false;
                    DataGridViewRow r = dataGridView1.Rows[i];
                    dataGridView1.Rows[i].ReadOnly = true;
                    r.DefaultCellStyle.BackColor = Color.LightGreen;
                    r.DefaultCellStyle.SelectionBackColor = Color.LightGreen;
                }
            }
            this.cartonweight.Text = CellSum1(6).ToString("F3");
            c.set_dgv_column_sort_state(this.dataGridView1, DataGridViewColumnSortMode.NotSortable);
        }
        private void M_V3_cartonProductionForm_Load(object sender, EventArgs e)
        {
            dtp.Format = DateTimePickerFormat.Short;
            dtp.Visible = false;
            dtp.Width = 100;
            dtp.MaxDate = DateTime.Now;
            dataGridView1.Controls.Add(dtp);

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

            this.dtp.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtp_keydown);
            if (Global.access == 2)
            {
                this.deleteButton.Visible = false;
            }

            this.ActiveControl = dataGridView1;
        }

        private void dtp_keydown(object sender, EventArgs e)
        {
            try
            {
                Console.WriteLine(dataGridView1.CurrentCell.RowIndex.ToString() + " llllll " + dataGridView1.CurrentCell.ColumnIndex);
                if (dtp.Focused && dataGridView1.CurrentCell.ColumnIndex == 1)
                {
                    //c.SuccessBox("Changed dtp");
                    Console.WriteLine("SETTING is " + dtp.Value.Date.ToString().Substring(0, 10));

                    dataGridView1.CurrentCell.Value = dtp.Value.Date.ToString().Substring(0, 10);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("DTP2 Exception " + ex.Message);
            }
            Console.WriteLine("The value is " +dtp.Value.Date.ToString().Substring(0, 10));
        }

        //user
        public void disable_form_edit()
        {
            this.inputDate.Enabled = false;
            this.saveButton.Enabled = false;
            this.dataGridView1.ReadOnly = true;
            this.deleteToolStripMenuItem.Enabled = false;
            this.deleteToolStripMenuItem1.Enabled = false;
        }
        private float CellSum1(int col)
        {
            float sum = 0;
            try
            {
                if (dataGridView1.Rows.Count == 0)
                {
                    return sum;
                }
                for (int i = 0; i < dataGridView1.Rows.Count; ++i)
                {
                    if (dataGridView1.Rows[i].Cells[col].Value != null)
                        sum += float.Parse(dataGridView1.Rows[i].Cells[col].Value.ToString());
                }
                return sum;
            }
            catch
            {
                return sum;
            }
        }
        public void calculate_net_wt(int row_index)
        {
            float net_weight = float.Parse(dataGridView1.Rows[row_index].Cells["Net_Weight"].Value.ToString());
            if (net_weight < 0)
            {
                c.ErrorBox("Net Weight (" + net_weight.ToString() + ") should be positive only. Please check your entries", "Error");
                dataGridView1.Rows[row_index].Cells["Net_Weight"].Value = "";
                cartonweight.Text = CellSum1(6).ToString("F3");
                return;
            }
            cartonweight.Text = CellSum1(6).ToString("F3");
        }
        private void initialise_comboboxes()
        {
            //Create drop-down Colour list
            var dataSource1 = new List<string>();
            DataTable dt = c.getQC('l');
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dataSource1.Add(dt.Rows[i][0].ToString());
            }
            List<string> final_list = dataSource1.Distinct().ToList();

            Colour.HeaderText = "Colour";
            Colour.DataSource = final_list;
            Colour.Name = "Colour";

            //Create drop-down Quality list
            var dataSource2 = new List<string>();
            dt = c.getQC('q');
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dataSource2.Add(dt.Rows[i][0].ToString());
            }
            Quality.HeaderText = "Quality";
            Quality.DataSource = dataSource2;
            Quality.Name = "Quality";

            List<string> grade = new List<string>();
            grade.Add("1st");
            grade.Add("PQ");
            grade.Add("CLQ");
            grade.Add("Redyeing");
            Grade.HeaderText = "Grade";
            Grade.DataSource = grade;
            Grade.Name = "Grade";
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            this.cartonweight.Text = CellSum1(6).ToString("F3");
            //checks
            if (dataGridView1.Rows[0].Cells[1].Value==null)
            {
                c.ErrorBox("Please enter values", "Error");
                return;
            }
            try
            {
                float.Parse(cartonweight.Text);
            }
            catch
            {
                c.ErrorBox("Please enter carton numbers");
                return;
            }
            Dictionary<string, List<string>> carton_data = new Dictionary<string, List<string>>();
            DataTable dt = new DataTable();
            foreach(DataGridViewColumn col in dataGridView1.Columns)
            {
                dt.Columns.Add(col.Name);
            }
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                int sum = 0;
                for(int j=1; j<=6;j++)
                {
                    if(dataGridView1.Rows[i].Cells[j].Value==null)
                    {

                    }
                    else if (dataGridView1.Rows[i].Cells[j].Value.ToString() == "") { }
                    else
                    {
                        if (j == 1)
                        {
                            try
                            {
                                Convert.ToDateTime(this.dataGridView1.Rows[i].Cells[1].Value.ToString());
                            }
                            catch
                            {
                                c.ErrorBox("Please enter correct Production Date format in row: " + (i + 1).ToString());
                                return;
                            }
                        }
                        sum++;
                    }
                }
                if(sum==0)
                {
                    continue;
                }
                else if(sum!=6)
                {
                    c.ErrorBox("Missing values in " + (i + 1).ToString() + " row", "Error");
                    return;
                }
                ComboBox cbox = (ComboBox)dataGridView1.EditingControl;
                if (dataGridView1.Rows[i].Cells[1].Value == null || dataGridView1.Rows[i].Cells[1].Value.ToString() == "")
                {
                    continue;
                }
                else
                {
                    //to check for all different carton
                    List<string> value = new List<string>();
                    string fiscal_year = c.getFinancialYear(Convert.ToDateTime(dataGridView1.Rows[i].Cells["Date_Of_Production"].Value.ToString()));
                    bool present = carton_data.TryGetValue(fiscal_year, out value);
                    string carton_no_i = dataGridView1.Rows[i].Cells["Carton_No"].Value.ToString();
                    if (present == false)
                    {
                        List<string> insert = new List<string>();
                        insert.Add(carton_no_i);
                        carton_data[fiscal_year] = insert;
                    }
                    else
                    {
                        if(value.Contains(carton_no_i) == true)
                        {
                            c.ErrorBox("Carton Nos repeated at Row: " + (i + 1).ToString(), "Error");
                            return;
                        }
                        else
                        {
                            carton_data[fiscal_year].Add(carton_no_i);
                        }
                    }
                    dt.Rows.Add();
                    for(int j=0;j<dataGridView1.Columns.Count;j++)
                    {
                        dt.Rows[dt.Rows.Count - 1][j] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                    }
                }
            }
            c.printDataTable(dt);
            if (this.edit_form == false)
            {
                //check for duplicates of all new cartons
                string sql_check = "SELECT * FROM Carton_Produced WHERE ";
                for(int i=0;i<dt.Rows.Count;i++)
                {
                    DateTime pd_dtp = DateTime.ParseExact(dt.Rows[i]["Date_Of_Production"].ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture);
                    string pd = pd_dtp.ToString("yyyy-MM-dd").Substring(0, 10);
                    sql_check += "(Carton_No = " + dt.Rows[i]["Carton_No"].ToString() + " AND Fiscal_Year = '" + c.getFinancialYear(pd_dtp) + "') OR ";
                }
                sql_check = sql_check.Substring(0, sql_check.Length - 3);
                DataTable d = c.runQuery(sql_check);
                if(d.Rows.Count != 0)
                { 
                    for(int i=0;i<dataGridView1.Rows.Count-1;i++)
                    {
                        try
                        {
                            DateTime pd_dtp = DateTime.ParseExact(dataGridView1.Rows[i].Cells["Date_Of_Production"].Value.ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture);
                            if (dataGridView1.Rows[i].Cells["Carton_No"].Value.ToString() == d.Rows[0]["Carton_No"].ToString() && c.getFinancialYear(pd_dtp) == d.Rows[0]["Fiscal_year"].ToString())
                            {
                                c.ErrorBox("Carton number " + d.Rows[i]["Carton_No"].ToString() + " at row " + (i + 1).ToString() + " already exists in Financial Year " + c.getFinancialYear(pd_dtp), "Error");
                                return;
                            }
                        }
                        catch 
                        {
                            Console.WriteLine("Catch in check for repeated cartonsn while adding");
                        }
                    }
                    
                }
                string sql = "begin transaction; begin try; ";
                sql += "DECLARE @voucherID int; "; 
                sql += "INSERT INTO Opening_Stock (Date_Of_Input, Voucher_Name) VALUES ('" + inputDate.Value.Date.ToString("yyyy-MM-dd").Substring(0, 10) + "','Carton Production'); ";
                sql += "SELECT @voucherID = SCOPE_IDENTITY(); ";
                for(int i=0;i<dt.Rows.Count;i++)
                {
                    DateTime pd_dtp = DateTime.ParseExact(dt.Rows[i]["Date_Of_Production"].ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture);
                    string pd = pd_dtp.ToString("yyyy-MM-dd").Substring(0, 10);
                    sql += "INSERT INTO Carton_Produced (Carton_No, Carton_State, Date_Of_Production, Quality, Colour, Net_Weight, Fiscal_Year, Grade, Company_Name, Production_Voucher_ID) VALUES ('" + dt.Rows[i]["Carton_No"].ToString() + "' ,1, '" + pd + "', '" + dt.Rows[i]["Quality"].ToString() + "', '" + dt.Rows[i]["Colour"].ToString() + "', " + float.Parse(dt.Rows[i]["Net_Weight"].ToString()) + ", '" + c.getFinancialYear(pd_dtp) + "', '" + dt.Rows[i]["Grade"].ToString() + "', 'Self', @voucherID); ";
                }
                //catch
                sql += "commit transaction; end try BEGIN CATCH rollback transaction; ";
                sql += "DECLARE @ErrorMessage NVARCHAR(4000); DECLARE @ErrorSeverity INT; DECLARE @ErrorState INT; SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE(); ";
                sql += "RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState); END CATCH; ";
                DataTable d_result = c.runQuery(sql);
                if (d_result != null)
                {
                    dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.LawnGreen;
                    disable_form_edit();
                }
                else return;
            }
            else
            {
                //check
                //Get all carton_nos and fiscal_years which were previously present
                DataTable old = new DataTable();
                old = c.runQuery("SELECT Carton_No, Fiscal_Year FROM Carton_Produced WHERE Production_Voucher_ID='" + this.voucher_id + "' AND Batch_No_Arr IS NULL");
                Dictionary<Tuple<string, string>, bool> old_cartons_hash = new Dictionary<Tuple<string, string>, bool>();
                //Insert old cartons into hash 
                for (int i=0;i<old.Rows.Count;i++)
                {
                    old_cartons_hash.Add(new Tuple<string, string>(old.Rows[i]["Carton_No"].ToString(), old.Rows[i]["Fiscal_Year"].ToString()), true);
                }

                /*<------------------Check for duplicates------------------->*/
                //Any new carton added should have count=0
                //New carton is a carton in the carton_no list, not present in the old_carton_hash
                List<Tuple<string, string>> new_cartons = new List<Tuple<string, string>>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //Check if its a new carton(not present in Hash)
                    bool value = false;
                    DateTime pd_dtp = DateTime.ParseExact(dt.Rows[i]["Date_Of_Production"].ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture);
                    string fiscal_year_i = c.getFinancialYear(pd_dtp);
                    bool value2 = old_cartons_hash.TryGetValue(new Tuple<string, string>(dt.Rows[i]["Carton_No"].ToString(), fiscal_year_i), out value);
                    if (value2 == false && value == false) //Carton not present in the hash, hence its new
                    {
                        new_cartons.Add(new Tuple<string, string>(dt.Rows[i]["Carton_No"].ToString(), fiscal_year_i));
                    }
                }

                //check for duplicates of all new cartons
                string sql_check = "SELECT * FROM Carton_Produced WHERE ";
                for (int i = 0; i < new_cartons.Count; i++)
                {
                    sql_check += "(Carton_No = " + new_cartons[i].Item1 + " AND Fiscal_Year = '" + new_cartons[i].Item2 + "') OR ";
                }
                sql_check = sql_check.Substring(0, sql_check.Length - 3);
                DataTable d = c.runQuery(sql_check);
                if (d.Rows.Count != 0)
                {
                    for (int i = 0; i < dataGridView1.Rows.Count-1; i++)
                    {
                        try
                        {
                            dataGridView1.Rows[i].Cells["Date_Of_Production"].Value.ToString();
                            DateTime pd_dtp = DateTime.ParseExact(dataGridView1.Rows[i].Cells["Date_Of_Production"].Value.ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture);
                            if (dataGridView1.Rows[i].Cells["Carton_No"].Value.ToString() == d.Rows[0]["Carton_No"].ToString() && c.getFinancialYear(pd_dtp) == d.Rows[0]["Fiscal_year"].ToString())
                            {
                                c.ErrorBox("Carton number " + d.Rows[i]["Carton_No"].ToString() + " at row " + (i + 1).ToString() + " already exists in Financial Year " + c.getFinancialYear(pd_dtp), "Error");
                                return;
                            }
                        }
                        catch
                        {
                            Console.WriteLine("Catch in check for repeated cartonsn while adding");
                        }
                    }

                }
                string sql = "begin transaction; begin try; ";
                sql += "DELETE FROM Carton_Produced WHERE ";
                //Remove cartons with state 1 in the old voucher
                foreach(Tuple<string, string> temp in old_cartons_hash.Keys)
                { 
                    bool value;
                    bool value2 = carton_editable.TryGetValue(temp, out value);
                    if (value2 == false) //doesnt contain entry, means it is in state 1
                    {
                        sql += "(Carton_No = " + temp.Item1 + " AND Fiscal_Year = '" + temp.Item2 + "') OR ";
                        Console.WriteLine("Removing Carton: " + temp.Item1 + " " + temp.Item2);
                    }
                }
                sql = sql.Substring(0, sql.Length - 3);
                sql += "; ";

                //Add all New Cartons with state 1
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    bool value;
                    DateTime pd_dtp = DateTime.ParseExact(dt.Rows[i]["Date_Of_Production"].ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture);
                    bool value2 = carton_editable.TryGetValue(new Tuple<string, string>(dt.Rows[i]["Carton_No"].ToString(), c.getFinancialYear(pd_dtp)), out value);
                    if (value2 == false)
                    {
                        string pd = pd_dtp.ToString("yyyy-MM-dd").Substring(0, 10);
                        sql += "INSERT INTO Carton_Produced (Carton_No, Carton_State, Date_Of_Production, Quality, Colour, Net_Weight, Fiscal_Year, Grade, Company_Name, Production_Voucher_ID) VALUES ('" + dt.Rows[i]["Carton_No"].ToString() + "' ,1, '" + pd + "', '" + dt.Rows[i]["Quality"].ToString() + "', '" + dt.Rows[i]["Colour"].ToString() + "', " + float.Parse(dt.Rows[i]["Net_Weight"].ToString()) + ", '" + c.getFinancialYear(pd_dtp) + "', '" + dt.Rows[i]["Grade"].ToString() + "', 'Self', '" + this.voucher_id + "'); ";
                    }
                }
                //catch
                sql += "commit transaction; end try BEGIN CATCH rollback transaction; ";
                sql += "DECLARE @ErrorMessage NVARCHAR(4000); DECLARE @ErrorSeverity INT; DECLARE @ErrorState INT; SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE(); ";
                sql += "RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState); END CATCH; ";
                DataTable d_result = c.runQuery(sql);
                if (d_result != null)
                {
                    c.SuccessBox("Voucher Edited Successfully");
                    dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.LawnGreen;
                    disable_form_edit();
                }
                else return;
                this.v1_history.loadData();
            }
            dataGridView1.EnableHeadersVisualStyles = false;
        }
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.edit_form == false)
            {
                int count = dataGridView1.SelectedRows.Count;
                for (int i = 0; i < count; i++)
                {
                    if (dataGridView1.SelectedRows[0].Index == dataGridView1.Rows.Count - 1)
                    {
                        dataGridView1.SelectedRows[0].Selected = false;
                        continue;
                    }
                    dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);
                }
                this.cartonweight.Text = CellSum1(7).ToString("F3");
            }
            else
            {
                int count = dataGridView1.SelectedRows.Count;
                for (int i = 0; i < count; i++)
                {
                    if (dataGridView1.SelectedRows[0].Index == dataGridView1.Rows.Count - 1)
                    {
                        dataGridView1.SelectedRows[0].Selected = false;
                        continue;
                    }
                    int rowindex = dataGridView1.SelectedRows[0].Index;
                    if(dataGridView1.Rows[rowindex].Cells[2].Value==null)
                    {
                        dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);
                        continue;
                    }
                    string carton_no = dataGridView1.Rows[rowindex].Cells[2].Value.ToString();
                    DateTime d = DateTime.ParseExact(dataGridView1.Rows[rowindex].Cells["Date_Of_Production"].Value.ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture);
                    bool value = true;
                    bool value2 = this.carton_editable.TryGetValue(new Tuple<string, string>(carton_no, c.getFinancialYear(d)), out value);
                    if (value2 == true && value == false)
                    {
                        c.ErrorBox("Cannot delete entry at row: " + (rowindex + 1).ToString(), "Error");
                        dataGridView1.Rows[rowindex].Selected = false;
                        continue;
                    }
                    dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);
                }
                this.cartonweight.Text = CellSum1(7).ToString("F3");
            }
            this.dtp.Visible = false;
        }
        private void deleteButton_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Confirm Delete", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                bool deleted = c.deleteCartonProductionVoucher(this.voucher_id);
                if (deleted == true)
                {
                    c.SuccessBox("Voucher Deleted Successfully");
                    this.deleteButton.Enabled = false;
                    this.v1_history.loadData();
                }
                else return;
            }
            else if (dialogResult == DialogResult.No)
            {
                return;
            }
        }

        //dgv
        private void dataGridView1_RowPostPaint_1(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            if (e.RowIndex != dataGridView1.Rows.Count - 1)
            {
                dataGridView1.Rows[e.RowIndex].Cells[0].Value = e.RowIndex + 1;
            }
        }
        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is DataGridViewComboBoxEditingControl)
            {
                ((ComboBox)e.Control).DropDownStyle = ComboBoxStyle.DropDown;
                ((ComboBox)e.Control).AutoCompleteSource = AutoCompleteSource.ListItems;
                ((ComboBox)e.Control).AutoCompleteMode = AutoCompleteMode.Append;
            }
        }
        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                dataGridView1.Rows[e.RowIndex].Selected = true;
            }
        }
        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (dataGridView1.Enabled == false || dataGridView1.ReadOnly == true) return;
            //called when tab is pressed at last row or tab is pressed while editing last row
            if (e.KeyCode == Keys.Tab &&
                (dataGridView1.SelectedCells.Cast<DataGridViewCell>().Any(x => x.ColumnIndex == 6) || this.edit_cmd_send == true))
            {
                Console.WriteLine("Inside Editing");
                bool edit_cmd_local = this.edit_cmd_send;
                this.edit_cmd_send = false;
                int rowindex_tab = dataGridView1.SelectedCells[0].RowIndex;
                if (edit_cmd_local == true) rowindex_tab--;
                if (dataGridView1.Rows.Count - 1 == rowindex_tab+1)
                {
                    DataGridViewRow row = (DataGridViewRow)dataGridView1.Rows[rowindex_tab].Clone();
                    row.DefaultCellStyle.BackColor = dataGridView1.DefaultCellStyle.BackColor;
                    row.DefaultCellStyle.SelectionBackColor = dataGridView1.DefaultCellStyle.SelectionBackColor;
                    row.ReadOnly = false;
                    dataGridView1.Rows.Add(row);
                    dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.Rows.Count - 2].Cells[1];
                    if (c.Cell_Not_NullOrEmpty(this.dataGridView1, rowindex_tab, 1) && !c.Cell_Not_NullOrEmpty(this.dataGridView1, rowindex_tab + 1, 1))
                    {
                        dataGridView1.Rows[rowindex_tab + 1].Cells[1].Value = dataGridView1.Rows[rowindex_tab].Cells[1].Value;
                    }
                    if (c.Cell_Not_NullOrEmpty(this.dataGridView1, rowindex_tab, 2) && !c.Cell_Not_NullOrEmpty(this.dataGridView1, rowindex_tab + 1, 2))
                    {
                        dataGridView1.Rows[rowindex_tab + 1].Cells[2].Value = (int.Parse(dataGridView1.Rows[rowindex_tab].Cells[2].Value.ToString()) + 1).ToString();
                    }
                    if (c.Cell_Not_NullOrEmpty(this.dataGridView1, rowindex_tab, 3) && !c.Cell_Not_NullOrEmpty(this.dataGridView1, rowindex_tab + 1, 3))
                    {
                        dataGridView1.Rows[rowindex_tab + 1].Cells[3].Value = dataGridView1.Rows[rowindex_tab].Cells[3].Value;
                    }
                    if (c.Cell_Not_NullOrEmpty(this.dataGridView1, rowindex_tab, 4) && !c.Cell_Not_NullOrEmpty(this.dataGridView1, rowindex_tab + 1, 4))
                    {
                        dataGridView1.Rows[rowindex_tab + 1].Cells[4].Value = dataGridView1.Rows[rowindex_tab].Cells[4].Value;
                    }
                    if (c.Cell_Not_NullOrEmpty(this.dataGridView1, rowindex_tab, 5) && !c.Cell_Not_NullOrEmpty(this.dataGridView1, rowindex_tab + 1, 5))
                    {
                        dataGridView1.Rows[rowindex_tab + 1].Cells[5].Value = dataGridView1.Rows[rowindex_tab].Cells[5].Value;
                    }
                    return;
                }
                if (dataGridView1.Rows.Count - 1 == rowindex_tab)
                {
                    Console.WriteLine("This case");
                    return;
                }

                if (c.Cell_Not_NullOrEmpty(this.dataGridView1, rowindex_tab, 1) && !c.Cell_Not_NullOrEmpty(this.dataGridView1, rowindex_tab + 1, 1))
                {
                    dataGridView1.Rows[rowindex_tab + 1].Cells[1].Value = dataGridView1.Rows[rowindex_tab].Cells[1].Value;
                }
                if (c.Cell_Not_NullOrEmpty(this.dataGridView1, rowindex_tab, 2) && !c.Cell_Not_NullOrEmpty(this.dataGridView1, rowindex_tab + 1, 2))
                {
                    dataGridView1.Rows[rowindex_tab + 1].Cells[2].Value = (int.Parse(dataGridView1.Rows[rowindex_tab].Cells[2].Value.ToString()) + 1).ToString();
                }
                if (c.Cell_Not_NullOrEmpty(this.dataGridView1, rowindex_tab, 3) && !c.Cell_Not_NullOrEmpty(this.dataGridView1, rowindex_tab + 1, 3))
                {
                    dataGridView1.Rows[rowindex_tab + 1].Cells[3].Value = dataGridView1.Rows[rowindex_tab].Cells[3].Value;
                }
                if (c.Cell_Not_NullOrEmpty(this.dataGridView1, rowindex_tab, 4) && !c.Cell_Not_NullOrEmpty(this.dataGridView1, rowindex_tab + 1, 4))
                {
                    dataGridView1.Rows[rowindex_tab + 1].Cells[4].Value = dataGridView1.Rows[rowindex_tab].Cells[4].Value;
                }
                if (c.Cell_Not_NullOrEmpty(this.dataGridView1, rowindex_tab, 5) && !c.Cell_Not_NullOrEmpty(this.dataGridView1, rowindex_tab + 1, 5))
                {
                    dataGridView1.Rows[rowindex_tab + 1].Cells[5].Value = dataGridView1.Rows[rowindex_tab].Cells[5].Value;
                }
                //bindingSource.EndEdit();
                dataGridView1.NotifyCurrentCellDirty(true);
                dataGridView1.EndEdit();
                dataGridView1.NotifyCurrentCellDirty(false);
                SendKeys.Send("{tab}");
                if (edit_cmd_local == false)
                {
                    SendKeys.Send("{tab}");
                }
            }

            if (e.KeyCode == Keys.Enter && dataGridView1.SelectedCells.Cast<DataGridViewCell>().Any(x => x.ColumnIndex == 1))
            {
                try
                {
                    if (dataGridView1.Focused && dataGridView1.CurrentCell.ColumnIndex == 1)
                    {
                        DateTime d;
                        if (!(dataGridView1.CurrentCell.Value == null || dataGridView1.CurrentCell.Value == ""))
                        {
                            d = Convert.ToDateTime(dataGridView1.CurrentCell.Value);
                        }
                        else
                        {
                            d = DateTime.Today;
                        }
                        setDate f = new setDate(d);
                        f.setMinMax(dtp.MinDate, dtp.MaxDate);
                        f.ShowDialog();
                        dataGridView1.CurrentCell.Value = f.result.Date.ToString().Substring(0, 10);
                        e.Handled = true;
                    }
                    else
                    {
                        dtp.Visible = false;
                    }
                }
                catch
                {
                    Console.WriteLine("DTP Exception");
                }

            }
            if (e.KeyCode == Keys.Enter &&
               (dataGridView1.SelectedCells.Cast<DataGridViewCell>().Any(x => x.ColumnIndex == 3) ||
               dataGridView1.SelectedCells.Cast<DataGridViewCell>().Any(x => x.ColumnIndex == 4) ||
               dataGridView1.SelectedCells.Cast<DataGridViewCell>().Any(x => x.ColumnIndex == 5)))

            {
                dataGridView1.BeginEdit(true);
                ComboBox c = (ComboBox)dataGridView1.EditingControl;
                if(c!=null)
                {
                    if(c!=null) c.DroppedDown = true;
                    SendKeys.Send("{down}");
                    SendKeys.Send("{up}");
                }
                e.Handled = true;
            }
        }
        private void dataGridView1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dataGridView1.IsCurrentCellDirty)
            {
                dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //Checks for numeric values
            if (e.ColumnIndex == 6 && e.RowIndex >= 0)
            {
                try
                {

                    if (c.Cell_Not_NullOrEmpty(this.dataGridView1, e.RowIndex, e.ColumnIndex))
                    {
                        float.Parse(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                    }
                }
                catch
                {
                    c.ErrorBox("Please Enter Decimal Net Weight", "Error");
                    SendKeys.Send("{Left}");
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "";
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].Selected = true;
                    return;
                }
            }
            if (e.ColumnIndex == 6 && e.RowIndex >= 0)
            {
                calculate_net_wt(e.RowIndex);
            }
        }
    }
}
