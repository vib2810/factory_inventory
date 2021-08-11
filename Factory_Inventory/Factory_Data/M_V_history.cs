using Factory_Inventory.Factory_Classes;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Windows.Media.TextFormatting;

namespace Factory_Inventory
{

    public partial class M_V_history : Form
    {
        DbConnect c;
        DataTable dt;
        public int vno = 0;
        public bool _firstLoaded = true;
        private int prev_selected_row = 0;
        Dictionary<int, string> vno_table_map = new Dictionary<int, string>();

        struct form_data
        {
            public Form form;
            public int type;
            public int voucher_id;
            public form_data(Form f, int type, int voucher_id)
            {
                this.form = f; this.type = type; this.voucher_id = voucher_id;
            }
        }
        List<form_data> child_forms = new List<form_data>();
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.V)
            {
                if (this.ActiveControl == searchTB) return false;
                this.viewDetailsButton.PerformClick(); ;
                return false;
            }
            if (keyData == Keys.E)
            {
                if (this.ActiveControl == searchTB) return false;
                this.editDetailsButton.PerformClick();
                return false;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        public M_V_history(int vno)
        {
            InitializeComponent();
            this.c = new DbConnect();
            this.dt = new DataTable();
            this.vno = vno;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView1.Font, FontStyle.Bold);
            //dataGridView1.DefaultCellStyle.BackColor = Color.LightGreen;
            this.label1.Visible = false;

            vno_table_map[1] = "Carton_Voucher";
            vno_table_map[2] = "Twist_Voucher";
            vno_table_map[3] = "Sales_Voucher";
            vno_table_map[4] = "Tray_Voucher";
            vno_table_map[5] = "Dyeing_Issue_Voucher";
            vno_table_map[6] = "Dyeing_Inward_Voucher";
            vno_table_map[7] = "BillNos_Voucher";
            vno_table_map[8] = "Carton_Production_Voucher";
            vno_table_map[9] = "Sales_Voucher";
            vno_table_map[10] = "SalesBillNos_Voucher";
            vno_table_map[11] = "SalesBillNos_Voucher";
            vno_table_map[12] = "Redyeing_Voucher";
            vno_table_map[13] = "T_Carton_Inward_Voucher";
            vno_table_map[14] = "T_Repacking_Voucher";
            vno_table_map[15] = "T_Sales_Voucher";
            vno_table_map[15] = "T_SalesBillNos_Voucher";

            //Opening
            vno_table_map[100] = "Carton Production";

            loadData();

            dataGridView1.VisibleChanged += DataGridView1_VisibleChanged;
            if (Global.access == 2 && (this.vno != 8 ||  this.vno!=14))
            {
                this.editDetailsButton.Visible = false;
                this.label2.Visible = false;
            }
        }
        private void M_V_history_Load(object sender, EventArgs e)
        {
            //Double Buffer DGV
            if (!System.Windows.Forms.SystemInformation.TerminalServerSession)
            {
                Type dgvType = dataGridView1.GetType();
                PropertyInfo pi = dgvType.GetProperty("DoubleBuffered",
                BindingFlags.Instance | BindingFlags.NonPublic);
                pi.SetValue(dataGridView1, true, null);
            }
            switch (vno)
            {
                case 1:
                    this.Text = "History - Carton Inward";
                    break;
                case 2:
                    this.Text = "History - Carton Twist";
                    break;
                case 3:
                    this.Text = "History - Gray Carton Sale";
                    break;
                case 4:
                    this.Text = "History - Tray Production";
                    break;
                case 5:
                    this.Text = "History - Issue to Dyeing";
                    break;
                case 6:
                    this.Text = "History - Dyeing Inward";
                    break;
                case 7:
                    this.Text = "History - Add Dyeing Bill";
                    break;
                case 8:
                    this.Text = "History - Carton Production";
                    break;
                case 9:
                    this.Text = "History - Colour Carton Sale";
                    break;
                case 10:
                    this.Text = "History - Add Bill to Gray DOs";
                    break;
                case 11:
                    this.Text = "History - Add Bill to Coloured DOs";
                    break;
                case 12:
                    this.Text = "History - Redyeing";
                    break;
                case 13:
                    this.Text = "History - Trading - Carton Inward Voucher";
                    break;
                case 14:
                    this.Text = "History - Trading - Repacking Voucher";
                    break;
                case 15:
                    this.Text = "History - Trading - Carton Sale";
                    break;
                case 16:
                    this.Text = "History - Trading - Add Bill to DOs";
                    break;
                case 100:
                    this.Text = "History - Carton Production Opening";
                    break;
            }
        }
        private void M_V_history_FormClosing(object sender, FormClosingEventArgs e)
        {
            int count = 0;
            for (int i = 0; i < child_forms.Count; i++) if (child_forms[i].form.IsDisposed == false) count++;
            if (count > 0)
            {
                DialogResult dialogResult = MessageBox.Show("Closing this will close all sub forms. Continue?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes)
                {
                    for (int i = 0; i < this.child_forms.Count; i++)
                    {
                        form_data value = this.child_forms[i];
                        value.form.Dispose();
                        value.form.Close();
                    }
                }
                else if (dialogResult == DialogResult.No)
                {
                    e.Cancel = true;
                    return;
                }
            }
        }
        
        //user
        bool check_not_showing(form_data data)
        {
            for (int i = 0; i < this.child_forms.Count; i++)
            {

                form_data value = child_forms[i];
                //Console.WriteLine("value " + value.form.Name + " " + value.type + " " + value.index);
                //Console.WriteLine("data " + data.form.Name + " " + data.type + " " + data.index);

                if (data.form.Name == value.form.Name && data.voucher_id == value.voucher_id && value.form.IsDisposed == false)
                {
                    if (data.type != value.type)
                    {
                        Console.WriteLine("DIfferent types");
                        value.form.Focus();
                        if (data.type == 1) MessageBox.Show("Voucher is already open in View Mode", "Info", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        else MessageBox.Show("Voucher is already open in Edit Mode", "Info", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return false;
                    }
                    else
                    {
                        Console.WriteLine("Same types");
                        value.form.Focus();
                        return false;
                    }
                }
            }
            return true;
        }
        private DataTable remove_sales_rows(DataTable input)
        {
            int rows = input.Rows.Count;
            DataTable d = input.Clone();
            if (rows == 0)
                return d;
            for (int i = 0; i < rows; i++)
            {
                if ((this.vno == 3 || this.vno == 10) && input.Rows[i]["Tablename"].ToString() == "Carton_Produced")
                {
                    continue;
                }
                else if ((this.vno == 9 || this.vno == 11) && input.Rows[i]["Tablename"].ToString() == "Carton")
                {
                    continue;
                }
                d.Rows.Add(input.Rows[i].ItemArray);
            }
            return d;
        }
        private string select_stuff(string distinct, string column, string column_name)
        {
            string sql = "";
            sql += "    ,STUFF((SELECT " + distinct + " ', ' + " + column + "\n";
            sql += "        from #temp t1\n";
            sql += "        where t.[Voucher_ID] = t1.[Voucher_ID]\n";
            sql += "        FOR XML PATH(''), TYPE\n";
            sql += "        ).value('.', 'NVARCHAR(MAX)')\n";
            sql += "    ,1,2,'') " + column_name + "\n";
            return sql;
        }
        private string select_function(string function, string column, string column_name)
        {
            return ",(Select " + function + "(" + column + ") from ((select " + column + " from #temp where [Voucher_ID] = t.[Voucher_ID] )) t1) " + column_name + "\n";
        }

        private string getTradingQuery(string search, bool searching)
        {
            string sql = "";
            if (this.vno == 13)
            {
                sql += "SELECT temp3.*, T_M_Company_Names.Company_Name into #temp\n";
                sql += "FROM\n";
                sql += "    (SELECT temp2.*, T_M_Colours.Colour\n";
                sql += "    FROM\n";
                sql += "        (SELECT temp1.*, T_M_Quality_Before_Job.Quality_Before_Job\n";
                sql += "        FROM\n";
                sql += "            (SELECT T_Carton_Inward_Voucher.*, T_Inward_Carton.Carton_ID, T_Inward_Carton.Carton_No, T_Inward_Carton.Quality_ID, T_Inward_Carton.Colour_ID, T_Inward_Carton.Net_Weight, T_Inward_Carton.Buy_Cost, T_Inward_Carton.Inward_Voucher_ID, T_Inward_Carton.Comments, T_Inward_Carton.Inward_Type, T_Inward_Carton.Grade\n";
                sql += "            FROM T_Carton_Inward_Voucher\n";
                sql += "            FULL OUTER JOIN T_Inward_Carton\n";
                sql += "            ON T_Carton_Inward_Voucher.Voucher_ID = T_Inward_Carton.Inward_Voucher_ID) as temp1\n";
                sql += "        LEFT OUTER JOIN T_M_Quality_Before_Job\n";
                sql += "        ON T_M_Quality_Before_Job.Quality_Before_Job_ID = temp1.Quality_ID) as temp2\n";
                sql += "    LEFT OUTER JOIN T_M_Colours\n";
                sql += "    ON T_M_Colours.Colour_ID = temp2.Colour_ID) as temp3\n";
                sql += "LEFT OUTER JOIN T_M_Company_Names\n";
                sql += "ON T_M_Company_Names.Company_ID = temp3.Company_ID;\n";

                sql += "select distinct t.[Voucher_ID]\n";
                sql += select_stuff("", "t1.Carton_No", "Carton_No_Arr");
                sql += select_stuff("distinct", "t1.Colour", "Colour_Arr");
                sql += select_stuff("distinct", "t1.Quality_Before_Job", "Quality_Before_Job_Arr");
                sql += select_stuff("distinct", "t1.Grade", "Grade_Arr");
                sql += select_stuff("", "CONVERT(VARCHAR, t1.Comments)", "Comments_Arr");
                sql += "    ,t.Bill_No, t.Date_Of_Billing, t.Company_Name, t.Date_Of_Input, t.Deleted, t.Fiscal_Year, t.Inward_Type, CONVERT(VARCHAR, t.Narration) Narration\n";

                sql += select_function("sum", "Net_Weight", "Net_Weight");
                sql += select_function("sum", "Buy_Cost", "Buy_Cost");
                sql += select_function("count", "Voucher_ID", "Number_Of_Cartons");
                sql += "into #tt\n";
                sql += "from #temp t order by Voucher_ID DESC;\n";
                if (!searching) sql += "select * from #tt;\n";
                else sql += search;
                sql += "drop table #temp;\n";
                sql += "drop table #tt;\n";
            }
            else if(this.vno==14)
            {
                sql += "SELECT temp5.*, (T_Inward_Carton.Carton_No)Destroyed_Carton_No, (T_Inward_Carton.Carton_ID)Destroyed_Carton_ID into #temp\n";
                sql += "FROM\n";
                sql += "(SELECT temp4.*, T_M_Cones.Cone_Name, T_M_Cones.Cone_Weight\n";
                sql += "    FROM\n";
                sql += "        (SELECT temp3.*, T_M_Company_Names.Company_Name\n";
                sql += "        FROM\n";
                sql += "            (SELECT temp2.*, T_M_Colours.Colour\n";
                sql += "            FROM\n";
                sql += "                (SELECT temp1.*, T_M_Quality_Before_Job.Quality_Before_Job\n";
                sql += "                FROM\n";
                sql += "                    (SELECT T_Repacking_Voucher.*, T_Repacked_Cartons.Carton_ID, T_Repacked_Cartons.Carton_No, T_Repacked_Cartons.Net_Weight, T_Repacked_Cartons.Repack_Comments, T_Repacked_Cartons.Grade\n";
                sql += "                    FROM T_Repacking_Voucher\n";
                sql += "                    FULL OUTER JOIN T_Repacked_Cartons\n";
                sql += "                    ON T_Repacking_Voucher.Voucher_ID = T_Repacked_Cartons.Repacking_Voucher_ID) as temp1\n";
                sql += "                LEFT OUTER JOIN T_M_Quality_Before_Job\n";
                sql += "                ON T_M_Quality_Before_Job.Quality_Before_Job_ID = temp1.Quality_ID) as temp2\n";
                sql += "            LEFT OUTER JOIN T_M_Colours\n";
                sql += "            ON T_M_Colours.Colour_ID = temp2.Colour_ID) as temp3\n";
                sql += "        LEFT OUTER JOIN T_M_Company_Names\n";
                sql += "        ON T_M_Company_Names.Company_ID = temp3.Company_ID) as temp4\n";
                sql += "    LEFT OUTER JOIN T_M_Cones\n";
                sql += "    ON T_M_Cones.Cone_ID = temp4.Cone_ID) as temp5\n";
                sql += "LEFT OUTER JOIN T_Inward_Carton\n";
                sql += "ON T_Inward_Carton.Repacking_Voucher_ID = temp5.Voucher_ID;\n";

                sql += "select distinct t.[Voucher_ID]\n";
                sql += select_stuff("", "t1.Carton_No", "Carton_No_Arr");
                sql += select_stuff("distinct", "t1.Destroyed_Carton_No", "Destroyed_Carton_No_Arr");
                sql += select_stuff("distinct", "t1.Grade", "Grade_Arr");
                sql += select_stuff("", "CONVERT(VARCHAR, t1.Net_Weight)", "Net_Weight_Arr");
                sql += select_stuff("", "CONVERT(VARCHAR, t1.Repack_Comments)", "Comments_Arr");
                sql += "    ,t.Carton_Fiscal_Year, t.Colour, t.Company_Name, t.Date_Of_Input, t.Date_Of_Production, t.Deleted, t.Oil_Gain, CONVERT(VARCHAR, t.Narration) Narration, t.Quality_Before_Job, t.Start_Date_Of_Production, t.Voucher_Closed, t.Cone_Name, t.Cone_Weight, t.Fiscal_Year, t.Inward_Cartons_Type\n";

                sql += select_function("sum", "Net_Weight", "Total_Weight");
                sql += select_function("count", "Voucher_ID", "Number_Of_Repacked_Cartons");

                sql += "into #tt\n";
                sql += "from #temp t order by Voucher_ID DESC;\n";                
                if (!searching) sql += "select * from #tt;\n";
                else sql += search;
                sql += "drop table #temp;\n";
                sql += "drop table #tt;\n";
            }
            else if(this.vno==15)
            {
                sql += "SELECT temp2.*, T_M_Customers.Customer_Name into #temp\n";
                sql += "FROM\n";
                sql += "    (SELECT temp1.*, T_M_Company_Names.Company_Name\n";
                sql += "    FROM\n";
                sql += "        (SELECT T_Sales_Voucher.Date_Of_Sale, T_Sales_Voucher.Sale_DO_No, T_Sales_Voucher.Date_Of_Input, T_Sales_Voucher.Net_Weight, T_Sales_Voucher.Sale_Rate, T_Sales_Voucher.Sale_Bill_Date, T_Sales_Voucher.Sale_Bill_No, T_Sales_Voucher.Fiscal_Year, T_Sales_Voucher.Company_ID, T_Sales_Voucher.Customer_ID, T_Sales_Voucher.Type_Of_Sale, T_Sales_Voucher.Voucher_ID, T_Sales_Voucher.Narration, T_Sales_Voucher.Deleted, T_M_Quality_Before_Job.Quality_Before_Job\n";
                sql += "        FROM T_Sales_Voucher\n";
                sql += "        LEFT OUTER JOIN T_M_Quality_Before_Job\n";
                sql += "        ON T_Sales_Voucher.Quality_ID = T_M_Quality_Before_Job.Quality_Before_Job_ID) as temp1\n";
                sql += "    LEFT OUTER JOIN T_M_Company_Names\n";
                sql += "    ON T_M_Company_Names.Company_ID = temp1.Company_ID) as temp2\n";
                sql += "LEFT OUTER JOIN T_M_Customers\n";
                sql += "ON T_M_Customers.Customer_ID = temp2.Customer_ID\n";
                sql += "select * into #tt\n";
                sql += "from #temp t ORDER BY Voucher_ID DESC\n";
                if (!searching) sql += "select * from #tt;\n";
                else sql += search;
                sql += "drop table #temp;\n";
                sql += "drop table #tt;\n";
            }
            else if(this.vno==16)
            {
                sql += "SELECT temp2.*, T_Sales_Voucher.Sale_DO_No, T_Sales_Voucher.SalesBillNos_Voucher_ID into #temp\n";
                sql += "FROM\n";
                sql += "    (SELECT temp1.*, T_M_Customers.Customer_Name\n";
                sql += "    FROM\n";
                sql += "        (SELECT T_SalesBillNos_Voucher.Sale_Bill_Date, T_SalesBillNos_Voucher.Date_Of_Input, T_SalesBillNos_Voucher.Sale_Bill_No, T_SalesBillNos_Voucher.Sale_Bill_Weight, T_SalesBillNos_Voucher.Sale_Bill_Amount, T_M_Quality_Before_Job.Quality_Before_Job, T_SalesBillNos_Voucher.Voucher_ID, T_SalesBillNos_Voucher.Narration, T_SalesBillNos_Voucher.Bill_Customer_ID, T_SalesBillNos_Voucher.DO_Fiscal_Year, T_SalesBillNos_Voucher.Type_Of_Sale, T_SalesBillNos_Voucher.Sale_Bill_Weight_Calc, T_SalesBillNos_Voucher.Sale_Bill_Amount_Calc\n";
                sql += "        FROM T_SalesBillNos_Voucher\n";
                sql += "        LEFT OUTER JOIN T_M_Quality_Before_Job\n";
                sql += "        ON T_SalesBillNos_Voucher.Quality_ID = T_M_Quality_Before_Job.Quality_Before_Job_ID) as temp1\n";
                sql += "    LEFT OUTER JOIN T_M_Customers\n";
                sql += "    ON temp1.Bill_Customer_ID = T_M_Customers.Customer_ID) as temp2\n";
                sql += "LEFT OUTER JOIN T_Sales_Voucher\n";
                sql += "ON temp2.Voucher_ID = T_Sales_Voucher.SalesBillNos_Voucher_ID\n";
                sql += "select distinct t.[Voucher_ID]\n";
                sql += select_stuff("", "t1.Sale_DO_No", "DO_No_Arr");
                sql += "    ,t.Date_Of_Input, t.Quality_Before_Job, t.Customer_Name, t.DO_Fiscal_Year, t.Type_Of_Sale, t.Sale_Bill_Amount, t.Sale_Bill_Date, t.Sale_Bill_No, t.Sale_Bill_Weight, CONVERT(VARCHAR, t.Narration) Narration, t.Sale_Bill_Amount_Calc, t.Sale_Bill_Weight_Calc\n";
                sql += "into #tt\n";
                sql += "from #temp t order by Voucher_ID DESC;\n";
                if (!searching) sql += "select * from #tt;\n";
                else sql += search;
                sql += "drop table #temp;\n";
                sql += "drop table #tt;\n";
            }
            return sql;
        }
        private string getSearchString(string searchText, bool date)
        {
            string search_string = "\nSET NOCOUNT ON;\n";
            search_string += "DECLARE @columnName NVARCHAR(100)\n";
            search_string += "DECLARE @sql NVARCHAR(1000) = 'SELECT * FROM #tt WHERE '\n";
            search_string += "DECLARE @searchText nvarchar(50) = '" + searchText + "';\n";
            search_string += "DECLARE @date tinyint = " + Convert.ToInt32(date).ToString() + ";\n";
            search_string += "DECLARE columns CURSOR FOR\n";
            search_string += "SELECT name\n";
            search_string += "FROM tempdb.sys.columns\n";
            search_string += "WHERE  object_id = Object_id('tempdb..#tt')\n";
            search_string += "OPEN columns\n";
            search_string += "FETCH NEXT FROM columns\n";
            search_string += "INTO @columnName\n";
            search_string += "WHILE @@FETCH_STATUS = 0\n";
            search_string += "BEGIN\n";
            search_string += "  if (@columnName not like '%voucher%') and(@columnName not like '%fiscal%') and(@columnName not like '%deleted%')\n";
            search_string += "  begin\n";
            search_string += "      if (@date = 1)\n";
            search_string += "      begin\n";
            search_string += "          if @columnName like '%date%'\n";
            search_string += "          begin SET @sql = @sql + @columnName + ' LIKE ''%' + @searchText + '%'' OR ' end\n";
            search_string += "      end\n";
            search_string += "      else\n";
            search_string += "      begin\n";
            search_string += "          if @columnName not like '%date%'\n";
            search_string += "          begin SET @sql = @sql + @columnName + ' LIKE ''%' + @searchText + '%'' OR ' end\n";
            search_string += "      end\n";
            search_string += "  end\n";
            search_string += "  FETCH NEXT FROM columns\n";
            search_string += "  INTO @columnName\n";
            search_string += "END\n";
            search_string += "CLOSE columns;\n";
            search_string += "DEALLOCATE columns;\n";
            search_string += "SET @sql = LEFT(RTRIM(@sql), LEN(@sql) - 2)\n";
            search_string += "EXEC(@sql)\n";

            return search_string;
        }

        public void loadData()
        {
            //Get dt
            if(this.vno==13)
            {
                string sql = this.getTradingQuery("", false);
                this.dt = c.runQuery(sql);
            }
            else if(this.vno==14)
            {
                string sql = this.getTradingQuery("", false);
                this.dt = c.runQuery(sql);
            }
            else if(this.vno==15)
            {
                string sql = this.getTradingQuery("", false);
                this.dt = c.runQuery(sql);
            }
            else if(this.vno==16)
            {
                string sql = this.getTradingQuery("", false);
                this.dt = c.runQuery(sql);
            }
            else if(this.vno<100)
            {
                this.dt = c.getVoucherHistories(vno_table_map[this.vno]);
            }
            else
            {
                this.dt = c.runQuery("Select * from Opening_Stock where voucher_name='"+vno_table_map[this.vno]+"'");
            }

            //assign datasource
            if (this.vno==3 || this.vno == 9 || this.vno==10 || this.vno==11)
            {
                this.dataGridView1.DataSource = this.remove_sales_rows(this.dt);
            }
            else 
            {
                this.dataGridView1.DataSource = this.dt;
            }

            //set columns
            this.adjust_dgv();
            
            _firstLoaded = true;
            dataGridView1.Visible = false;
            dataGridView1.Visible = true;
            try { this.dataGridView1.Rows[this.prev_selected_row].Selected = true; }
            catch { }
            c.set_dgv_column_sort_state(this.dataGridView1, DataGridViewColumnSortMode.NotSortable);
        }
        void search_in_voucher_table(bool date=false)
        {
            string to_search = "";
            if (date == false) to_search = this.searchTB.Text;
            else to_search = this.dateTimePicker1.Value.Date.ToString("yyyy-MM-dd");

            //if null search
            if(string.IsNullOrEmpty(to_search)==true)
            {
                //this.dt = c.getVoucherHistories(vno_table_map[this.vno]);
                this.loadData();
            }
            else
            {
                if(this.vno<=12 || this.vno==100)
                {
                    string prod = "@tableName = '" + vno_table_map[this.vno] + "', @searchText = '" + to_search + "', @date=0";
                    if (date == true) prod = "@tableName = '" + vno_table_map[this.vno] + "', @searchText = '" + to_search + "', @date=1";
                    this.dt = c.runProcedure("SearchInTable", prod);
                }
                else
                {
                    string search_string = this.getSearchString(to_search, date);
                    string sql = this.getTradingQuery(search_string, true);
                    this.dt = c.runQuery(sql);
                }
                this.dataGridView1.DataSource = this.dt;
                this.adjust_dgv();
                _firstLoaded = true;
                this.dataGridView1.Visible = false;
                this.dataGridView1.Visible = true;
            }
           
        }
        
        void adjust_dgv()
        {
            if (this.vno == 1)
            {
                //this.dt = c.getCartonVoucherHistory();
                this.dataGridView1.ReadOnly = true;
                this.dataGridView1.Columns.OfType<DataGridViewColumn>().ToList().ForEach(col => col.Visible = false);
                this.dataGridView1.Columns["Date_Of_Billing"].Visible = true;
                this.dataGridView1.Columns["Date_Of_Billing"].DisplayIndex = 0;
                this.dataGridView1.Columns["Date_Of_Billing"].HeaderText = "Bill Date";
                this.dataGridView1.Columns["Date_Of_Input"].Visible = true;
                this.dataGridView1.Columns["Date_Of_Input"].DisplayIndex = 1;
                this.dataGridView1.Columns["Date_Of_Input"].HeaderText = "Input Date";
                this.dataGridView1.Columns["Bill_No"].Visible = true;
                this.dataGridView1.Columns["Bill_No"].DisplayIndex = 2;
                this.dataGridView1.Columns["Bill_No"].HeaderText = "Bill Number";
                this.dataGridView1.Columns["Carton_No_Arr"].Visible = true;
                this.dataGridView1.Columns["Carton_No_Arr"].DisplayIndex = 3;
                this.dataGridView1.Columns["Carton_No_Arr"].HeaderText = "Carton Numbers";
                this.dataGridView1.Columns["Company_Name"].Visible = true;
                this.dataGridView1.Columns["Company_Name"].DisplayIndex = 4;
                this.dataGridView1.Columns["Company_Name"].HeaderText = "Company Name";
                this.dataGridView1.Columns["Net_Weight"].Visible = true;
                this.dataGridView1.Columns["Net_Weight"].DisplayIndex = 6;
                this.dataGridView1.Columns["Net_Weight"].HeaderText = "Net Weight";
                this.dataGridView1.Columns["Number_of_Cartons"].Visible = true;
                this.dataGridView1.Columns["Number_of_Cartons"].DisplayIndex = 8;
                this.dataGridView1.Columns["Number_of_Cartons"].HeaderText = "Number of Cartons";
                this.dataGridView1.Columns["Fiscal_Year"].Visible = true;
                this.dataGridView1.Columns["Fiscal_Year"].DisplayIndex = 10;
                this.dataGridView1.Columns["Fiscal_Year"].HeaderText = "Financial Year of Carton";
                c.auto_adjust_dgv(this.dataGridView1);
            }       //Carton Inward
            if (this.vno == 2)
            {
                //this.dt = c.getTwistVoucherHistory();
                this.dataGridView1.ReadOnly = true;
                this.dataGridView1.Columns.OfType<DataGridViewColumn>().ToList().ForEach(col => col.Visible = false);
                this.dataGridView1.Columns["Date_Of_Issue"].Visible = true;
                this.dataGridView1.Columns["Date_Of_Issue"].DisplayIndex = 0;
                this.dataGridView1.Columns["Date_Of_Issue"].HeaderText = "Issue Date";
                this.dataGridView1.Columns["Date_Of_Input"].Visible = true;
                this.dataGridView1.Columns["Date_Of_Input"].DisplayIndex = 1;
                this.dataGridView1.Columns["Date_Of_Input"].HeaderText = "Input Date";
                this.dataGridView1.Columns["Quality"].Visible = true;
                this.dataGridView1.Columns["Quality"].DisplayIndex = 2;
                this.dataGridView1.Columns["Quality"].HeaderText = "Quality";
                this.dataGridView1.Columns["Carton_No_Arr"].Visible = true;
                this.dataGridView1.Columns["Carton_No_Arr"].DisplayIndex = 3;
                this.dataGridView1.Columns["Carton_No_Arr"].HeaderText = "Carton Numbers";
                this.dataGridView1.Columns["Company_Name"].Visible = true;
                this.dataGridView1.Columns["Company_Name"].DisplayIndex = 4;
                this.dataGridView1.Columns["Company_Name"].HeaderText = "Company Name";
                this.dataGridView1.Columns["Number_of_Cartons"].Visible = true;
                this.dataGridView1.Columns["Number_of_Cartons"].DisplayIndex = 6;
                this.dataGridView1.Columns["Number_of_Cartons"].HeaderText = "Number of Cartons";
                this.dataGridView1.Columns["Fiscal_Year"].Visible = true;
                this.dataGridView1.Columns["Fiscal_Year"].DisplayIndex = 8;
                this.dataGridView1.Columns["Fiscal_Year"].HeaderText = "Issue Financial Year";
                c.auto_adjust_dgv(this.dataGridView1);

            }       //Twist
            if (this.vno == 3)
            {
                //this.dt = c.getSalesVoucherHistory();
                this.dataGridView1.ReadOnly = true;
                this.dataGridView1.Columns.OfType<DataGridViewColumn>().ToList().ForEach(col => col.Visible = false);
                this.dataGridView1.Columns["Date_Of_Sale"].Visible = true;
                this.dataGridView1.Columns["Date_Of_Sale"].DisplayIndex = 0;
                this.dataGridView1.Columns["Date_Of_Sale"].HeaderText = "Sale Date";
                this.dataGridView1.Columns["Date_Of_Input"].Visible = true;
                this.dataGridView1.Columns["Date_Of_Input"].DisplayIndex = 1;
                this.dataGridView1.Columns["Date_Of_Input"].HeaderText = "Input Date";
                this.dataGridView1.Columns["Sale_DO_No"].Visible = true;
                this.dataGridView1.Columns["Sale_DO_No"].DisplayIndex = 2;
                this.dataGridView1.Columns["Sale_DO_No"].HeaderText = "DO Number";
                this.dataGridView1.Columns["Customer"].Visible = true;
                this.dataGridView1.Columns["Customer"].DisplayIndex = 4;
                this.dataGridView1.Columns["Customer"].HeaderText = "Party";
                this.dataGridView1.Columns["Quality"].Visible = true;
                this.dataGridView1.Columns["Quality"].DisplayIndex = 6;
                this.dataGridView1.Columns["Quality"].HeaderText = "Quality";
                this.dataGridView1.Columns["Net_Weight"].Visible = true;
                this.dataGridView1.Columns["Net_Weight"].DisplayIndex = 8;
                this.dataGridView1.Columns["Net_Weight"].HeaderText = "Net Weight";
                this.dataGridView1.Columns["Sale_Rate"].Visible = true;
                this.dataGridView1.Columns["Sale_Rate"].DisplayIndex = 10;
                this.dataGridView1.Columns["Sale_Rate"].HeaderText = "Sale Rate";
                this.dataGridView1.Columns["Sale_Bill_Date"].Visible = true;
                this.dataGridView1.Columns["Sale_Bill_Date"].DisplayIndex = 12;
                this.dataGridView1.Columns["Sale_Bill_Date"].HeaderText = "Bill Date";
                this.dataGridView1.Columns["Sale_Bill_No"].Visible = true;
                this.dataGridView1.Columns["Sale_Bill_No"].DisplayIndex = 14;
                this.dataGridView1.Columns["Sale_Bill_No"].HeaderText = "Bill Number";
                c.auto_adjust_dgv(this.dataGridView1);
            }       //Gray Sale
            if (this.vno == 4)
            {
                this.dataGridView1.ReadOnly = true;
                this.dataGridView1.Columns.OfType<DataGridViewColumn>().ToList().ForEach(col => col.Visible = false);
                this.dataGridView1.Columns["Tray_Production_Date"].Visible = true;
                this.dataGridView1.Columns["Tray_Production_Date"].DisplayIndex = 0;
                this.dataGridView1.Columns["Tray_Production_Date"].HeaderText = "Production Date";
                this.dataGridView1.Columns["Input_Date"].Visible = true;
                this.dataGridView1.Columns["Input_Date"].DisplayIndex = 1;
                this.dataGridView1.Columns["Input_Date"].HeaderText = "Input Date";
                this.dataGridView1.Columns["Tray_No"].Visible = true;
                this.dataGridView1.Columns["Tray_No"].DisplayIndex = 2;
                this.dataGridView1.Columns["Tray_No"].HeaderText = "Tray Number";
                this.dataGridView1.Columns["Quality"].Visible = true;
                this.dataGridView1.Columns["Quality"].DisplayIndex = 6;
                this.dataGridView1.Columns["Quality"].HeaderText = "Quality";
                this.dataGridView1.Columns["Net_Weight"].Visible = true;
                this.dataGridView1.Columns["Net_Weight"].DisplayIndex = 8;
                this.dataGridView1.Columns["Net_Weight"].HeaderText = "Net Weight";
                this.dataGridView1.Columns["Machine_No"].Visible = true;
                this.dataGridView1.Columns["Machine_No"].DisplayIndex = 10;
                this.dataGridView1.Columns["Machine_No"].HeaderText = "Machine Number";
                c.auto_adjust_dgv(this.dataGridView1);
            }       //Tray
            if (this.vno == 5)
            {
                this.dataGridView1.ReadOnly = true;
                this.dataGridView1.Columns.OfType<DataGridViewColumn>().ToList().ForEach(col => col.Visible = false);
                this.dataGridView1.Columns["Date_Of_Issue"].Visible = true;
                this.dataGridView1.Columns["Date_Of_Issue"].DisplayIndex = 0;
                this.dataGridView1.Columns["Date_Of_Issue"].HeaderText = "Dyeing Issue Date";
                this.dataGridView1.Columns["Date_Of_Input"].Visible = true;
                this.dataGridView1.Columns["Date_Of_Input"].DisplayIndex = 1;
                this.dataGridView1.Columns["Date_Of_Input"].HeaderText = "Input Date";
                this.dataGridView1.Columns["Batch_No"].Visible = true;
                this.dataGridView1.Columns["Batch_No"].DisplayIndex = 2;
                this.dataGridView1.Columns["Batch_No"].HeaderText = "Batch Number";
                this.dataGridView1.Columns["Tray_No_Arr"].Visible = true;
                this.dataGridView1.Columns["Tray_No_Arr"].DisplayIndex = 3;
                this.dataGridView1.Columns["Tray_No_Arr"].HeaderText = "Tray Numbers";
                this.dataGridView1.Columns["Quality"].Visible = true;
                this.dataGridView1.Columns["Quality"].DisplayIndex = 4;
                this.dataGridView1.Columns["Quality"].HeaderText = "Quality";
                this.dataGridView1.Columns["Colour"].Visible = true;
                this.dataGridView1.Columns["Colour"].DisplayIndex = 6;
                this.dataGridView1.Columns["Colour"].HeaderText = "Colour";
                this.dataGridView1.Columns["Dyeing_Company_Name"].Visible = true;
                this.dataGridView1.Columns["Dyeing_Company_Name"].DisplayIndex = 8;
                this.dataGridView1.Columns["Dyeing_Company_Name"].HeaderText = "Dyeing Company Name";
                this.dataGridView1.Columns["Net_Weight"].Visible = true;
                this.dataGridView1.Columns["Net_Weight"].DisplayIndex = 9;
                this.dataGridView1.Columns["Net_Weight"].HeaderText = "Net Weight";
                c.auto_adjust_dgv(this.dataGridView1);

            }       //Dyeing Issue
            if (this.vno == 6)
            {
                this.dataGridView1.ReadOnly = true;
                this.dataGridView1.Columns.OfType<DataGridViewColumn>().ToList().ForEach(col => col.Visible = false);
                this.dataGridView1.Columns["Inward_Date"].Visible = true;
                this.dataGridView1.Columns["Inward_Date"].DisplayIndex = 0;
                this.dataGridView1.Columns["Inward_Date"].HeaderText = "Inward Date";
                this.dataGridView1.Columns["Date_Of_Input"].Visible = true;
                this.dataGridView1.Columns["Date_Of_Input"].DisplayIndex = 1;
                this.dataGridView1.Columns["Date_Of_Input"].HeaderText = "Input Date";
                this.dataGridView1.Columns["Dyeing_Company_Name"].Visible = true;
                this.dataGridView1.Columns["Dyeing_Company_Name"].DisplayIndex = 2;
                this.dataGridView1.Columns["Dyeing_Company_Name"].HeaderText = "Dyeing Company Name";
                this.dataGridView1.Columns["Batch_No_Arr"].Visible = true;
                this.dataGridView1.Columns["Batch_No_Arr"].DisplayIndex = 3;
                this.dataGridView1.Columns["Batch_No_Arr"].HeaderText = "Batch Numbers";
                this.dataGridView1.Columns["Slip_No_Arr"].Visible = true;
                this.dataGridView1.Columns["Slip_No_Arr"].DisplayIndex = 4;
                this.dataGridView1.Columns["Slip_No_Arr"].HeaderText = "Slip Number";
                this.dataGridView1.Columns["Batch_No_Arr"].Visible = true;
                this.dataGridView1.Columns["Batch_No_Arr"].DisplayIndex = 6;
                this.dataGridView1.Columns["Batch_No_Arr"].HeaderText = "Batch Nos";
                this.dataGridView1.Columns["Batch_Fiscal_Year"].Visible = true;
                this.dataGridView1.Columns["Batch_Fiscal_Year"].DisplayIndex = 8;
                this.dataGridView1.Columns["Batch_Fiscal_Year"].HeaderText = "Batch Fiscal Year";
                this.label1.Visible = true;
                c.auto_adjust_dgv(this.dataGridView1);

            }       //Dyeing Inward
            if (this.vno == 7)
            {
                this.dataGridView1.ReadOnly = true;
                this.dataGridView1.Columns.OfType<DataGridViewColumn>().ToList().ForEach(col => col.Visible = false);
                this.dataGridView1.Columns["Bill_Date"].Visible = true;
                this.dataGridView1.Columns["Bill_Date"].DisplayIndex = 0;
                this.dataGridView1.Columns["Bill_Date"].HeaderText = "Bill Date";
                this.dataGridView1.Columns["Date_Of_Input"].Visible = true;
                this.dataGridView1.Columns["Date_Of_Input"].DisplayIndex = 1;
                this.dataGridView1.Columns["Date_Of_Input"].HeaderText = "Input Date";
                this.dataGridView1.Columns["Dyeing_Company_Name"].Visible = true;
                this.dataGridView1.Columns["Dyeing_Company_Name"].DisplayIndex = 2;
                this.dataGridView1.Columns["Dyeing_Company_Name"].HeaderText = "Dyeing Company Name";
                this.dataGridView1.Columns["Bill_No"].Visible = true;
                this.dataGridView1.Columns["Bill_No"].DisplayIndex = 3;
                this.dataGridView1.Columns["Bill_No"].HeaderText = "Bill Number";
                this.dataGridView1.Columns["Batch_No_Arr"].Visible = true;
                this.dataGridView1.Columns["Batch_No_Arr"].DisplayIndex = 6;
                this.dataGridView1.Columns["Batch_No_Arr"].HeaderText = "Batch Numbers";
                c.auto_adjust_dgv(this.dataGridView1);
            }       //Bill to Dyeing Inward
            if (this.vno == 8)
            {
                this.dataGridView1.ReadOnly = true;
                this.dataGridView1.Columns["Voucher_ID"].Visible = false;
                if (dataGridView1.Rows.Count >= 1 && dataGridView1.SelectedRows.Count > 0)
                {
                    if (dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].Cells["Voucher_Closed"].Value.ToString() == "1" && Global.access == 2)
                    {
                        this.editDetailsButton.Enabled = false;
                    }
                    else
                    {
                        this.editDetailsButton.Enabled = true;
                    }
                }
                this.dataGridView1.Columns.OfType<DataGridViewColumn>().ToList().ForEach(col => col.Visible = false);
                this.dataGridView1.Columns["Start_Date_Of_Production"].Visible = true;
                this.dataGridView1.Columns["Start_Date_Of_Production"].DisplayIndex = 0;
                this.dataGridView1.Columns["Start_Date_Of_Production"].HeaderText = "Start Production Date";
                this.dataGridView1.Columns["Date_Of_Production"].Visible = true;
                this.dataGridView1.Columns["Date_Of_Production"].DisplayIndex = 1;
                this.dataGridView1.Columns["Date_Of_Production"].HeaderText = "End Production Date";
                this.dataGridView1.Columns["Date_Of_Input"].Visible = true;
                this.dataGridView1.Columns["Date_Of_Input"].DisplayIndex = 2;
                this.dataGridView1.Columns["Date_Of_Input"].HeaderText = "Input Date";
                this.dataGridView1.Columns["Quality"].Visible = true;
                this.dataGridView1.Columns["Quality"].DisplayIndex = 3;
                this.dataGridView1.Columns["Quality"].HeaderText = "Quality";
                this.dataGridView1.Columns["Colour"].Visible = true;
                this.dataGridView1.Columns["Colour"].DisplayIndex = 4;
                this.dataGridView1.Columns["Colour"].HeaderText = "Colour";
                this.dataGridView1.Columns["Batch_No_Arr"].Visible = true;
                this.dataGridView1.Columns["Batch_No_Arr"].DisplayIndex = 5;
                this.dataGridView1.Columns["Batch_No_Arr"].HeaderText = "Batch Numbers";
                this.dataGridView1.Columns["Net_Batch_Weight"].Visible = true;
                this.dataGridView1.Columns["Net_Batch_Weight"].DisplayIndex = 6;
                this.dataGridView1.Columns["Net_Batch_Weight"].HeaderText = "Net Batch Weight";
                this.dataGridView1.Columns["Carton_No_Production_Arr"].Visible = true;
                this.dataGridView1.Columns["Carton_No_Production_Arr"].DisplayIndex = 7;
                this.dataGridView1.Columns["Carton_No_Production_Arr"].HeaderText = "Carton Numbers";
                this.dataGridView1.Columns["Net_Carton_Weight"].Visible = true;
                this.dataGridView1.Columns["Net_Carton_Weight"].DisplayIndex = 8;
                this.dataGridView1.Columns["Net_Carton_Weight"].HeaderText = "Net Carton Weight";
                this.dataGridView1.Columns["Oil_Gain"].Visible = true;
                this.dataGridView1.Columns["Oil_Gain"].DisplayIndex = 10;
                this.dataGridView1.Columns["Oil_Gain"].HeaderText = "Oil Gain";
                this.dataGridView1.Columns["Voucher_Closed"].Visible = true;
                this.dataGridView1.Columns["Voucher_Closed"].DisplayIndex = 12;
                this.dataGridView1.Columns["Voucher_Closed"].HeaderText = "Batches Closed";
                this.label1.Text = "Batches Closed:\n1: Closed\n0: Open";
                this.label1.Visible = true;
                c.auto_adjust_dgv(this.dataGridView1);

            }       //Carton Production
            if (this.vno == 9)
            {
                this.dataGridView1.ReadOnly = true;

                this.dataGridView1.Columns.OfType<DataGridViewColumn>().ToList().ForEach(col => col.Visible = false);
                this.dataGridView1.Columns["Date_Of_Sale"].Visible = true;
                this.dataGridView1.Columns["Date_Of_Sale"].DisplayIndex = 0;
                this.dataGridView1.Columns["Date_Of_Sale"].HeaderText = "Sale Date";
                this.dataGridView1.Columns["Date_Of_Input"].Visible = true;
                this.dataGridView1.Columns["Date_Of_Input"].DisplayIndex = 1;
                this.dataGridView1.Columns["Date_Of_Input"].HeaderText = "Input Date";
                this.dataGridView1.Columns["Sale_DO_No"].Visible = true;
                this.dataGridView1.Columns["Sale_DO_No"].DisplayIndex = 2;
                this.dataGridView1.Columns["Sale_DO_No"].HeaderText = "DO Number";
                this.dataGridView1.Columns["Customer"].Visible = true;
                this.dataGridView1.Columns["Customer"].DisplayIndex = 4;
                this.dataGridView1.Columns["Customer"].HeaderText = "Party";
                this.dataGridView1.Columns["Quality"].Visible = true;
                this.dataGridView1.Columns["Quality"].DisplayIndex = 6;
                this.dataGridView1.Columns["Quality"].HeaderText = "Quality";
                this.dataGridView1.Columns["Net_Weight"].Visible = true;
                this.dataGridView1.Columns["Net_Weight"].DisplayIndex = 8;
                this.dataGridView1.Columns["Net_Weight"].HeaderText = "Net Weight";
                this.dataGridView1.Columns["Sale_Rate"].Visible = true;
                this.dataGridView1.Columns["Sale_Rate"].DisplayIndex = 10;
                this.dataGridView1.Columns["Sale_Rate"].HeaderText = "Sale Rate";
                this.dataGridView1.Columns["Sale_Bill_Date"].Visible = true;
                this.dataGridView1.Columns["Sale_Bill_Date"].DisplayIndex = 12;
                this.dataGridView1.Columns["Sale_Bill_Date"].HeaderText = "Bill Date";
                this.dataGridView1.Columns["Sale_Bill_No"].Visible = true;
                this.dataGridView1.Columns["Sale_Bill_No"].DisplayIndex = 14;
                this.dataGridView1.Columns["Sale_Bill_No"].HeaderText = "Bill Number";
                c.auto_adjust_dgv(this.dataGridView1);
            }       //Colour Sale
            if (this.vno == 10)
            {
                this.dataGridView1.ReadOnly = true;
                this.dataGridView1.Columns.OfType<DataGridViewColumn>().ToList().ForEach(col => col.Visible = false);
                this.dataGridView1.Columns["Sale_Bill_Date"].Visible = true;
                this.dataGridView1.Columns["Sale_Bill_Date"].DisplayIndex = 0;
                this.dataGridView1.Columns["Sale_Bill_Date"].HeaderText = "Sale Bill Date";
                this.dataGridView1.Columns["Date_Of_Input"].Visible = true;
                this.dataGridView1.Columns["Date_Of_Input"].DisplayIndex = 1;
                this.dataGridView1.Columns["Date_Of_Input"].HeaderText = "Input Date";
                this.dataGridView1.Columns["DO_No_Arr"].Visible = true;
                this.dataGridView1.Columns["DO_No_Arr"].DisplayIndex = 2;
                this.dataGridView1.Columns["DO_No_Arr"].HeaderText = "DO Numbers";
                this.dataGridView1.Columns["Quality"].Visible = true;
                this.dataGridView1.Columns["Quality"].DisplayIndex = 3;
                this.dataGridView1.Columns["Quality"].HeaderText = "Quality";
                this.dataGridView1.Columns["Sale_Bill_No"].Visible = true;
                this.dataGridView1.Columns["Sale_Bill_No"].DisplayIndex = 4;
                this.dataGridView1.Columns["Sale_Bill_No"].HeaderText = "Sale Bill Number";
                this.dataGridView1.Columns["Sale_Bill_Weight"].Visible = true;
                this.dataGridView1.Columns["Sale_Bill_Weight"].DisplayIndex = 6;
                this.dataGridView1.Columns["Sale_Bill_Weight"].HeaderText = "Bill Weight";
                this.dataGridView1.Columns["Sale_Bill_Amount"].Visible = true;
                this.dataGridView1.Columns["Sale_Bill_Amount"].DisplayIndex = 8;
                this.dataGridView1.Columns["Sale_Bill_Amount"].HeaderText = "Bill Amount";
                c.auto_adjust_dgv(this.dataGridView1);
            }      //Bill to Gray Sale
            if (this.vno == 11)
            {
                this.dataGridView1.ReadOnly = true;
                this.dataGridView1.Columns.OfType<DataGridViewColumn>().ToList().ForEach(col => col.Visible = false);
                this.dataGridView1.Columns["Sale_Bill_Date"].Visible = true;
                this.dataGridView1.Columns["Sale_Bill_Date"].DisplayIndex = 0;
                this.dataGridView1.Columns["Sale_Bill_Date"].HeaderText = "Sale Bill Date";
                this.dataGridView1.Columns["Date_Of_Input"].Visible = true;
                this.dataGridView1.Columns["Date_Of_Input"].DisplayIndex = 1;
                this.dataGridView1.Columns["Date_Of_Input"].HeaderText = "Input Date";
                this.dataGridView1.Columns["DO_No_Arr"].Visible = true;
                this.dataGridView1.Columns["DO_No_Arr"].DisplayIndex = 4;
                this.dataGridView1.Columns["DO_No_Arr"].HeaderText = "DO Numbers";
                this.dataGridView1.Columns["Quality"].Visible = true;
                this.dataGridView1.Columns["Quality"].DisplayIndex = 6;
                this.dataGridView1.Columns["Quality"].HeaderText = "Quality";
                this.dataGridView1.Columns["Sale_Bill_No"].Visible = true;
                this.dataGridView1.Columns["Sale_Bill_No"].DisplayIndex = 8;
                this.dataGridView1.Columns["Sale_Bill_No"].HeaderText = "Sale Bill Number";
                this.dataGridView1.Columns["Sale_Bill_Weight"].Visible = true;
                this.dataGridView1.Columns["Sale_Bill_Weight"].DisplayIndex = 10;
                this.dataGridView1.Columns["Sale_Bill_Weight"].HeaderText = "Bill Weight";
                this.dataGridView1.Columns["Sale_Bill_Amount"].Visible = true;
                this.dataGridView1.Columns["Sale_Bill_Amount"].DisplayIndex = 12;
                this.dataGridView1.Columns["Sale_Bill_Amount"].HeaderText = "Bill Amount";
                c.auto_adjust_dgv(this.dataGridView1);
            }      //Bill to Colour Sale
            if (this.vno == 12)
            {
                this.dataGridView1.ReadOnly = true;
                this.dataGridView1.Columns.OfType<DataGridViewColumn>().ToList().ForEach(col => col.Visible = false);
                this.dataGridView1.Columns["Voucher_ID"].Visible = false;
                this.dataGridView1.Columns["Date_Of_Issue"].Visible = true;
                this.dataGridView1.Columns["Date_Of_Issue"].DisplayIndex = 0;
                this.dataGridView1.Columns["Date_Of_Issue"].HeaderText = "Date Of Issue";
                this.dataGridView1.Columns["Date_Of_Input"].Visible = true;
                this.dataGridView1.Columns["Date_Of_Input"].DisplayIndex = 1;
                this.dataGridView1.Columns["Date_Of_Input"].HeaderText = "Date of Input";
                this.dataGridView1.Columns["Old_Batch_No"].Visible = true;
                this.dataGridView1.Columns["Old_Batch_No"].DisplayIndex = 2;
                this.dataGridView1.Columns["Old_Batch_No"].HeaderText = "Old Batch No";
                this.dataGridView1.Columns["Old_Batch_Fiscal_Year"].Visible = true;
                this.dataGridView1.Columns["Old_Batch_Fiscal_Year"].DisplayIndex = 3;
                this.dataGridView1.Columns["Old_Batch_Fiscal_Year"].HeaderText = "Old Batch Fiscal Year";
                this.dataGridView1.Columns["Non_Redyeing_Batch_No"].Visible = true;
                this.dataGridView1.Columns["Non_Redyeing_Batch_No"].DisplayIndex = 4;
                this.dataGridView1.Columns["Non_Redyeing_Batch_No"].HeaderText = "Non Redyeing Batch No";
                this.dataGridView1.Columns["Redyeing_Batch_No"].Visible = true;
                this.dataGridView1.Columns["Redyeing_Batch_No"].DisplayIndex = 5;
                this.dataGridView1.Columns["Redyeing_Batch_No"].HeaderText = "Redyeing Batch No";
                this.dataGridView1.Columns["Redyeing_Batch_Fiscal_Year"].Visible = true;
                this.dataGridView1.Columns["Redyeing_Batch_Fiscal_Year"].DisplayIndex = 6;
                this.dataGridView1.Columns["Redyeing_Batch_Fiscal_Year"].HeaderText = "New Batch Fiscal Year";
                c.auto_adjust_dgv(this.dataGridView1);
            }      //Redyeing
            if (this.vno == 13)         
            {
                this.dataGridView1.ReadOnly = true;
                this.dataGridView1.Columns.OfType<DataGridViewColumn>().ToList().ForEach(col => col.Visible = false);
                this.dataGridView1.Columns["Date_Of_Billing"].Visible = true;
                this.dataGridView1.Columns["Date_Of_Billing"].DisplayIndex = 0;
                this.dataGridView1.Columns["Date_Of_Billing"].HeaderText = "Bill Date";
                this.dataGridView1.Columns["Date_Of_Input"].Visible = true;
                this.dataGridView1.Columns["Date_Of_Input"].DisplayIndex = 1;
                this.dataGridView1.Columns["Date_Of_Input"].HeaderText = "Input Date";
                this.dataGridView1.Columns["Bill_No"].Visible = true;
                this.dataGridView1.Columns["Bill_No"].DisplayIndex = 2;
                this.dataGridView1.Columns["Bill_No"].HeaderText = "Bill Number";
                this.dataGridView1.Columns["Inward_Type"].Visible = true;
                this.dataGridView1.Columns["Inward_Type"].DisplayIndex = 3;
                this.dataGridView1.Columns["Inward_Type"].HeaderText = "Inward Type";
                this.dataGridView1.Columns["Carton_No_Arr"].Visible = true;
                this.dataGridView1.Columns["Carton_No_Arr"].DisplayIndex = 4;
                this.dataGridView1.Columns["Carton_No_Arr"].HeaderText = "Carton Numbers";
                this.dataGridView1.Columns["Quality_Before_Job_Arr"].Visible = true;
                this.dataGridView1.Columns["Quality_Before_Job_Arr"].DisplayIndex = 5;
                this.dataGridView1.Columns["Quality_Before_Job_Arr"].HeaderText = "Qualities";
                this.dataGridView1.Columns["Colour_Arr"].Visible = true;
                this.dataGridView1.Columns["Colour_Arr"].DisplayIndex = 6;
                this.dataGridView1.Columns["Colour_Arr"].HeaderText = "Colours";
                this.dataGridView1.Columns["Company_Name"].Visible = true;
                this.dataGridView1.Columns["Company_Name"].DisplayIndex = 7;
                this.dataGridView1.Columns["Company_Name"].HeaderText = "Company Name";
                this.dataGridView1.Columns["Net_Weight"].Visible = true;
                this.dataGridView1.Columns["Net_Weight"].DisplayIndex = 8;
                this.dataGridView1.Columns["Net_Weight"].HeaderText = "Net Weight";
                this.dataGridView1.Columns["Buy_Cost"].Visible = true;
                this.dataGridView1.Columns["Buy_Cost"].DisplayIndex = 9;
                this.dataGridView1.Columns["Buy_Cost"].HeaderText = "Buy Cost";
                this.dataGridView1.Columns["Number_of_Cartons"].Visible = true;
                this.dataGridView1.Columns["Number_of_Cartons"].DisplayIndex = 10;
                this.dataGridView1.Columns["Number_of_Cartons"].HeaderText = "Number of Cartons";
                this.dataGridView1.Columns["Narration"].Visible = true;
                this.dataGridView1.Columns["Narration"].DisplayIndex = 11;
                this.dataGridView1.Columns["Narration"].HeaderText = "Narration";
                this.dataGridView1.Columns["Fiscal_Year"].Visible = true;
                this.dataGridView1.Columns["Fiscal_Year"].DisplayIndex = 12;
                this.dataGridView1.Columns["Fiscal_Year"].HeaderText = "Financial Year of Carton";
                c.auto_adjust_dgv(this.dataGridView1);
            }      //Trading Carton Inward
            if (this.vno == 14)
            {
                this.dataGridView1.ReadOnly = true;
                this.dataGridView1.Columns.OfType<DataGridViewColumn>().ToList().ForEach(col => col.Visible = false);
                this.dataGridView1.Columns["Start_Date_Of_Production"].Visible = true;
                this.dataGridView1.Columns["Start_Date_Of_Production"].DisplayIndex = 0;
                this.dataGridView1.Columns["Start_Date_Of_Production"].HeaderText = "Start Date of Production";
                this.dataGridView1.Columns["Date_Of_Production"].Visible = true;
                this.dataGridView1.Columns["Date_Of_Production"].DisplayIndex = 1;
                this.dataGridView1.Columns["Date_Of_Production"].HeaderText = "End Date of Production";
                this.dataGridView1.Columns["Date_Of_Input"].Visible = true;
                this.dataGridView1.Columns["Date_Of_Input"].DisplayIndex = 2;
                this.dataGridView1.Columns["Date_Of_Input"].HeaderText = "Input Date";
                this.dataGridView1.Columns["Destroyed_Carton_No_Arr"].Visible = true;
                this.dataGridView1.Columns["Destroyed_Carton_No_Arr"].DisplayIndex = 3;
                this.dataGridView1.Columns["Destroyed_Carton_No_Arr"].HeaderText = "Destroyed Cartons";
                this.dataGridView1.Columns["Carton_No_Arr"].Visible = true;
                this.dataGridView1.Columns["Carton_No_Arr"].DisplayIndex = 4;
                this.dataGridView1.Columns["Carton_No_Arr"].HeaderText = "Repacked Carton";
                this.dataGridView1.Columns["Quality_Before_Job"].Visible = true;
                this.dataGridView1.Columns["Quality_Before_Job"].DisplayIndex = 5;
                this.dataGridView1.Columns["Quality_Before_Job"].HeaderText = "Quality";
                this.dataGridView1.Columns["Colour"].Visible = true;
                this.dataGridView1.Columns["Colour"].DisplayIndex = 6;
                this.dataGridView1.Columns["Colour"].HeaderText = "Colours";
                this.dataGridView1.Columns["Company_Name"].Visible = true;
                this.dataGridView1.Columns["Company_Name"].DisplayIndex = 7;
                this.dataGridView1.Columns["Company_Name"].HeaderText = "Company Name";
                this.dataGridView1.Columns["Cone_Name"].Visible = true;
                this.dataGridView1.Columns["Cone_Name"].DisplayIndex = 8;
                this.dataGridView1.Columns["Cone_Name"].HeaderText = "Cone";
                this.dataGridView1.Columns["Cone_Weight"].Visible = true;
                this.dataGridView1.Columns["Cone_Weight"].DisplayIndex = 9;
                this.dataGridView1.Columns["Cone_Weight"].HeaderText = "Cone Weight";
                this.dataGridView1.Columns["Inward_Cartons_Type"].Visible = true;
                this.dataGridView1.Columns["Inward_Cartons_Type"].DisplayIndex = 10;
                this.dataGridView1.Columns["Inward_Cartons_Type"].HeaderText = "Destroyed Cartons Inward Type";
                this.dataGridView1.Columns["Oil_Gain"].Visible = true;
                this.dataGridView1.Columns["Oil_Gain"].DisplayIndex = 11;
                this.dataGridView1.Columns["Oil_Gain"].HeaderText = "Oil Gain";
                this.dataGridView1.Columns["Carton_Fiscal_Year"].Visible = true;
                this.dataGridView1.Columns["Carton_Fiscal_Year"].DisplayIndex = 12;
                this.dataGridView1.Columns["Carton_Fiscal_Year"].HeaderText = "Carton Fiscal Year";
                this.dataGridView1.Columns["Total_Weight"].Visible = true;
                this.dataGridView1.Columns["Total_Weight"].DisplayIndex = 13;
                this.dataGridView1.Columns["Total_Weight"].HeaderText = "Total Weight";
                this.dataGridView1.Columns["Net_Weight_Arr"].Visible = true;
                this.dataGridView1.Columns["Net_Weight_Arr"].DisplayIndex = 14;
                this.dataGridView1.Columns["Net_Weight_Arr"].HeaderText = "Repaked Carton Weights";
                this.dataGridView1.Columns["Voucher_Closed"].Visible = true;
                this.dataGridView1.Columns["Voucher_Closed"].DisplayIndex = 15;
                this.dataGridView1.Columns["Voucher_Closed"].HeaderText = "Closed";
                this.dataGridView1.Columns["Grade_Arr"].Visible = true;
                this.dataGridView1.Columns["Grade_Arr"].DisplayIndex = 16;
                this.dataGridView1.Columns["Grade_Arr"].HeaderText = "Grades";
                this.dataGridView1.Columns["Comments_Arr"].Visible = true;
                this.dataGridView1.Columns["Comments_Arr"].DisplayIndex = 17;
                this.dataGridView1.Columns["Comments_Arr"].HeaderText = "Comments";
                this.dataGridView1.Columns["Deleted"].Visible = true;
                this.dataGridView1.Columns["Deleted"].DisplayIndex = 18;
                this.dataGridView1.Columns["Deleted"].HeaderText = "Deleted";
                this.dataGridView1.Columns["Number_Of_Repacked_Cartons"].Visible = true;
                this.dataGridView1.Columns["Number_Of_Repacked_Cartons"].DisplayIndex = 19;
                this.dataGridView1.Columns["Number_Of_Repacked_Cartons"].HeaderText = "Number of Repacked Cartons";
                this.dataGridView1.Columns["Fiscal_Year"].Visible = true;
                this.dataGridView1.Columns["Fiscal_Year"].DisplayIndex = 20;
                this.dataGridView1.Columns["Fiscal_Year"].HeaderText = "Fiscal Year";
                //c.auto_adjust_dgv(this.dataGridView1);
            }      //Trading Repacking
            if (this.vno == 15)
            {
                int i = 0;
                //this.dt = c.getSalesVoucherHistory();
                this.dataGridView1.ReadOnly = true;
                this.dataGridView1.Columns.OfType<DataGridViewColumn>().ToList().ForEach(col => col.Visible = false);
                this.dataGridView1.Columns["Date_Of_Sale"].Visible = true;
                this.dataGridView1.Columns["Date_Of_Sale"].DisplayIndex = i++;
                this.dataGridView1.Columns["Date_Of_Sale"].HeaderText = "Sale Date";
                this.dataGridView1.Columns["Date_Of_Input"].Visible = true;
                this.dataGridView1.Columns["Date_Of_Input"].DisplayIndex = i++;
                this.dataGridView1.Columns["Date_Of_Input"].HeaderText = "Input Date";
                this.dataGridView1.Columns["Sale_DO_No"].Visible = true;
                this.dataGridView1.Columns["Sale_DO_No"].DisplayIndex = i++;
                this.dataGridView1.Columns["Sale_DO_No"].HeaderText = "DO Number";
                this.dataGridView1.Columns["Type_Of_Sale"].Visible = true;
                this.dataGridView1.Columns["Type_Of_Sale"].DisplayIndex = i++;
                this.dataGridView1.Columns["Type_Of_Sale"].HeaderText = "Type of Sale";
                this.dataGridView1.Columns["Customer_Name"].Visible = true;
                this.dataGridView1.Columns["Customer_Name"].DisplayIndex = i++;
                this.dataGridView1.Columns["Customer_Name"].HeaderText = "Party";
                this.dataGridView1.Columns["Quality_Before_Job"].Visible = true;
                this.dataGridView1.Columns["Quality_Before_Job"].DisplayIndex = i++;
                this.dataGridView1.Columns["Quality_Before_Job"].HeaderText = "Quality";
                this.dataGridView1.Columns["Company_Name"].Visible = true;
                this.dataGridView1.Columns["Company_Name"].DisplayIndex = i++;
                this.dataGridView1.Columns["Company_Name"].HeaderText = "Company";
                this.dataGridView1.Columns["Net_Weight"].Visible = true;
                this.dataGridView1.Columns["Net_Weight"].DisplayIndex = i++;
                this.dataGridView1.Columns["Net_Weight"].HeaderText = "Net Weight";
                this.dataGridView1.Columns["Sale_Rate"].Visible = true;
                this.dataGridView1.Columns["Sale_Rate"].DisplayIndex = i++;
                this.dataGridView1.Columns["Sale_Rate"].HeaderText = "Sale Rate";
                this.dataGridView1.Columns["Sale_Bill_Date"].Visible = true;
                this.dataGridView1.Columns["Sale_Bill_Date"].DisplayIndex = i++;
                this.dataGridView1.Columns["Sale_Bill_Date"].HeaderText = "Bill Date";
                this.dataGridView1.Columns["Sale_Bill_No"].Visible = true;
                this.dataGridView1.Columns["Sale_Bill_No"].DisplayIndex = i++;
                this.dataGridView1.Columns["Sale_Bill_No"].HeaderText = "Bill Number";
                c.auto_adjust_dgv(this.dataGridView1);
            }       //Gray Sale
            if (this.vno == 16)
            {
                int i = 0;
                this.dataGridView1.ReadOnly = true;
                this.dataGridView1.Columns.OfType<DataGridViewColumn>().ToList().ForEach(col => col.Visible = false);
                this.dataGridView1.Columns["Sale_Bill_Date"].Visible = true;
                this.dataGridView1.Columns["Sale_Bill_Date"].DisplayIndex = i++;
                this.dataGridView1.Columns["Sale_Bill_Date"].HeaderText = "Sale Bill Date";
                this.dataGridView1.Columns["Type_Of_Sale"].Visible = true;
                this.dataGridView1.Columns["Type_Of_Sale"].DisplayIndex = i++;
                this.dataGridView1.Columns["Type_Of_Sale"].HeaderText = "Sale Type";
                this.dataGridView1.Columns["Date_Of_Input"].Visible = true;
                this.dataGridView1.Columns["Date_Of_Input"].DisplayIndex = i++;
                this.dataGridView1.Columns["Date_Of_Input"].HeaderText = "Input Date";
                this.dataGridView1.Columns["DO_No_Arr"].Visible = true;
                this.dataGridView1.Columns["DO_No_Arr"].DisplayIndex = i++;
                this.dataGridView1.Columns["DO_No_Arr"].HeaderText = "DO Numbers";
                this.dataGridView1.Columns["Quality_Before_Job"].Visible = true;
                this.dataGridView1.Columns["Quality_Before_Job"].DisplayIndex = i++;
                this.dataGridView1.Columns["Quality_Before_Job"].HeaderText = "Quality";
                this.dataGridView1.Columns["Customer_Name"].Visible = true;
                this.dataGridView1.Columns["Customer_Name"].DisplayIndex = i++;
                this.dataGridView1.Columns["Customer_Name"].HeaderText = "Bill Customer Name";
                this.dataGridView1.Columns["Sale_Bill_No"].Visible = true;
                this.dataGridView1.Columns["Sale_Bill_No"].DisplayIndex = i++;
                this.dataGridView1.Columns["Sale_Bill_No"].HeaderText = "Sale Bill Number";
                this.dataGridView1.Columns["Sale_Bill_Weight"].Visible = true;
                this.dataGridView1.Columns["Sale_Bill_Weight"].DisplayIndex = i++;
                this.dataGridView1.Columns["Sale_Bill_Weight"].HeaderText = "Bill Weight";
                this.dataGridView1.Columns["Sale_Bill_Amount"].Visible = true;
                this.dataGridView1.Columns["Sale_Bill_Amount"].DisplayIndex = i++;
                this.dataGridView1.Columns["Sale_Bill_Amount"].HeaderText = "Bill Amount";
                this.dataGridView1.Columns["Narration"].Visible = true;
                this.dataGridView1.Columns["Narration"].DisplayIndex = i++;
                this.dataGridView1.Columns["Narration"].HeaderText = "Narration";
                this.dataGridView1.Columns["DO_Fiscal_Year"].Visible = true;
                this.dataGridView1.Columns["DO_Fiscal_Year"].DisplayIndex = i++;
                this.dataGridView1.Columns["DO_Fiscal_Year"].HeaderText = "DO Fiscal Year";
                c.auto_adjust_dgv(this.dataGridView1);
            }      //Bill to Colour Sale
            if (this.vno == 100)
            {
                this.dataGridView1.ReadOnly = true;
                this.dataGridView1.Columns.OfType<DataGridViewColumn>().ToList().ForEach(col => col.Visible = false);
                this.dataGridView1.Columns["Date_Of_Input"].Visible = true;
                this.dataGridView1.Columns["Date_Of_Input"].DisplayIndex = 0;
                this.dataGridView1.Columns["Date_Of_Input"].HeaderText = "Date of Input";
                c.auto_adjust_dgv(this.dataGridView1);
            }     //Opening

        }
        //Datagridview
        private void DataGridView1_VisibleChanged(object sender, EventArgs e)
        {
            if (_firstLoaded && dataGridView1.Visible)
            {
                _firstLoaded = false;
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    try
                    {
                        string deleted = dataGridView1.Rows[i].Cells["Deleted"].Value.ToString();
                        if (deleted == "1")
                        {
                            Console.WriteLine("Setting for " + i);
                            foreach (DataGridViewCell c in dataGridView1.Rows[i].Cells)
                            {
                                c.Style.BackColor = Color.Red;
                                c.Style.SelectionBackColor = Color.Red;
                            }

                        }
                    }
                    catch (Exception x) { Console.WriteLine("ERROR: " + x.Message); }
                }
            }
        }
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null) return;
            if (dataGridView1.CurrentRow.Index < 0) return;
            DataRow row = (dataGridView1.Rows[dataGridView1.CurrentRow.Index].DataBoundItem as DataRowView).Row;
            try
            {
                string deleted = row["Deleted"].ToString();
                if (deleted == "1")
                {
                    this.viewDetailsButton.Enabled = false;
                    this.editDetailsButton.Enabled = false;
                    return;
                }
                else
                {
                    this.viewDetailsButton.Enabled = true;
                    this.editDetailsButton.Enabled = true;
                }
            }
            catch (Exception x) { Console.WriteLine("ERROR: " + x.Message); }
            if (this.vno == 8 && dataGridView1.CurrentRow.Index >= 0)
            {
                Console.WriteLine(dataGridView1.CurrentRow.Cells[10].Value.ToString());
                if (dataGridView1.CurrentRow.Cells[10].Value.ToString() == "1" && Global.access == 2)
                {
                    this.editDetailsButton.Enabled = false;
                }
                else
                {
                    this.editDetailsButton.Enabled = true;
                }
            }

        }

        //click
        private void viewDetailsButton_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count <= 0) return;
            int index = this.dataGridView1.SelectedRows[0].Index;
            int voucher_id = int.Parse(this.dataGridView1.Rows[index].Cells["Voucher_ID"].Value.ToString());
            Console.WriteLine(voucher_id);
            if (index > this.dataGridView1.Rows.Count - 1)
            {
                c.ErrorBox("Please select valid voucher", "Error");
            }
            else
            {
                DataRow row = (dataGridView1.Rows[index].DataBoundItem as DataRowView).Row;
                if (this.vno == 1)
                {

                    M_V1_cartonInwardForm f = new M_V1_cartonInwardForm(row, false, this);
                    if (this.check_not_showing(new form_data(f, 0, voucher_id)) == true)
                    {
                        Global.background.show_form(f);
                        this.child_forms.Add(new form_data(f, 0, voucher_id));
                    }
                }
                if (this.vno == 2)
                {
                    M_V1_cartonTwistForm f = new M_V1_cartonTwistForm(row, false, this);
                    if (this.check_not_showing(new form_data(f, 0, voucher_id)) == true)
                    {
                        Global.background.show_form(f);
                        this.child_forms.Add(new form_data(f, 0, voucher_id));
                    }
                }
                if (this.vno == 3)
                {
                    M_VC_cartonSalesForm f = new M_VC_cartonSalesForm(row, false, this, "Carton");
                    if (this.check_not_showing(new form_data(f, 0, voucher_id)) == true)
                    {
                        Global.background.show_form(f);
                        this.child_forms.Add(new form_data(f, 0, voucher_id));
                    }
                }
                if (this.vno == 4)
                {
                    M_V2_trayInputForm f = new M_V2_trayInputForm(row, false, this);
                    if (this.check_not_showing(new form_data(f, 0, voucher_id)) == true)
                    {
                        Global.background.show_form(f);
                        this.child_forms.Add(new form_data(f, 0, voucher_id));
                    }
                }
                if (this.vno == 5)
                {
                    M_V2_dyeingIssueForm f = new M_V2_dyeingIssueForm(row, false, this);
                    if (this.check_not_showing(new form_data(f, 0, voucher_id)) == true)
                    {
                        Global.background.show_form(f);
                        this.child_forms.Add(new form_data(f, 0, voucher_id));
                    }
                }
                if (this.vno == 6)
                {
                    M_V2_dyeingInwardForm f = new M_V2_dyeingInwardForm(row, false, this, "dyeingInward");
                    if (this.check_not_showing(new form_data(f, 0, voucher_id)) == true)
                    {
                        Global.background.show_form(f);
                        this.child_forms.Add(new form_data(f, 0, voucher_id));
                    }
                }
                if (this.vno == 7)
                {
                    M_V2_dyeingInwardForm f = new M_V2_dyeingInwardForm(row, false, this, "addBill");
                    if (this.check_not_showing(new form_data(f, 0, voucher_id)) == true)
                    {
                        Global.background.show_form(f);
                        this.child_forms.Add(new form_data(f, 0, voucher_id));
                    }
                }
                if (this.vno == 8)
                {
                    M_V3_cartonProductionForm f = new M_V3_cartonProductionForm(row, false, this);
                    if (this.check_not_showing(new form_data(f, 0, voucher_id)) == true)
                    {
                        Global.background.show_form(f);
                        this.child_forms.Add(new form_data(f, 0, voucher_id));
                    }
                }
                if (this.vno == 9)
                {
                    M_VC_cartonSalesForm f = new M_VC_cartonSalesForm(row, false, this, "Carton_Produced");
                    if (this.check_not_showing(new form_data(f, 0, voucher_id)) == true)
                    {
                        Global.background.show_form(f);
                        this.child_forms.Add(new form_data(f, 0, voucher_id));
                    }
                }
                if (this.vno == 10)
                {
                    M_VC_addBill f = new M_VC_addBill(row, false, this, "Carton");
                    if (this.check_not_showing(new form_data(f, 0, voucher_id)) == true)
                    {
                        Global.background.show_form(f);
                        this.child_forms.Add(new form_data(f, 0, voucher_id));
                    }
                }
                if (this.vno == 11)
                {
                    M_VC_addBill f = new M_VC_addBill(row, false, this, "Carton_Produced");
                    if (this.check_not_showing(new form_data(f, 0, voucher_id)) == true)
                    {
                        Global.background.show_form(f);
                        this.child_forms.Add(new form_data(f, 0, voucher_id));
                    }
                }
                if (this.vno == 12)
                {
                    M_V3_issueToReDyeingForm f = new M_V3_issueToReDyeingForm(row, false, this);
                    if (this.check_not_showing(new form_data(f, 0, voucher_id)) == true)
                    {
                        Global.background.show_form(f);
                        this.child_forms.Add(new form_data(f, 0, voucher_id));
                    }
                }
                if (this.vno == 13)
                {
                    T_V1_cartonInwardForm f = new T_V1_cartonInwardForm(row, false, this);
                    if (this.check_not_showing(new form_data(f, 0, voucher_id)) == true)
                    {
                        Global.background.show_form(f);
                        this.child_forms.Add(new form_data(f, 0, voucher_id));
                    }
                }
                if (this.vno == 14)
                {
                    T_V2_repackingForm f = new T_V2_repackingForm(row, false, this);
                    if (this.check_not_showing(new form_data(f, 0, voucher_id)) == true)
                    {
                        Global.background.show_form(f);
                        this.child_forms.Add(new form_data(f, 0, voucher_id));
                    }
                }
                if (this.vno == 15)
                {
                    T_V3_cartonSalesForm f = new T_V3_cartonSalesForm(row, false, this);
                    if (this.check_not_showing(new form_data(f, 0, voucher_id)) == true)
                    {
                        Global.background.show_form(f);
                        this.child_forms.Add(new form_data(f, 0, voucher_id));
                    }
                }
                if (this.vno == 16)
                {
                    T_V3_addBill f = new T_V3_addBill(row, false, this);
                    if (this.check_not_showing(new form_data(f, 0, voucher_id)) == true)
                    {
                        Global.background.show_form(f);
                        this.child_forms.Add(new form_data(f, 0, voucher_id));
                    }
                }
                if (this.vno == 100)
                {
                    M_V5_cartonProductionOpeningForm f = new M_V5_cartonProductionOpeningForm(row, false, this);
                    if (this.check_not_showing(new form_data(f, 0, voucher_id)) == true)
                    {
                        Global.background.show_form(f);
                        this.child_forms.Add(new form_data(f, 0, voucher_id));
                    }
                }
                this.prev_selected_row = index;
            }
        }
        private void editDetailsButton_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count <= 0)
                return;
            int index = this.dataGridView1.SelectedRows[0].Index;
            int voucher_id = int.Parse(this.dataGridView1.Rows[index].Cells["Voucher_ID"].Value.ToString());
            if (index > this.dataGridView1.Rows.Count - 1)
            {
                c.ErrorBox("Please select valid voucher", "Error");
            }
            else
            {
                DataRow row = (dataGridView1.Rows[index].DataBoundItem as DataRowView).Row;
                if (this.vno == 1)
                {
                    M_V1_cartonInwardForm f = new M_V1_cartonInwardForm(row, true, this);
                    if (this.check_not_showing(new form_data(f, 1, voucher_id)) == true)
                    {
                        Global.background.show_form(f);
                        this.child_forms.Add(new form_data(f, 1, voucher_id));
                    }
                }
                if (this.vno == 2)
                {
                    M_V1_cartonTwistForm f = new M_V1_cartonTwistForm(row, true, this);
                    if (this.check_not_showing(new form_data(f, 1, voucher_id)) == true)
                    {
                        Global.background.show_form(f);
                        this.child_forms.Add(new form_data(f, 1, voucher_id));
                    }
                }
                if (this.vno == 3)
                {
                    M_VC_cartonSalesForm f = new M_VC_cartonSalesForm(row, true, this, "Carton");
                    if (this.check_not_showing(new form_data(f, 1, voucher_id)) == true)
                    {
                        Global.background.show_form(f);
                        this.child_forms.Add(new form_data(f, 1, voucher_id));
                    }
                }
                if (this.vno == 4)
                {
                    M_V2_trayInputForm f = new M_V2_trayInputForm(row, true, this);
                    if (this.check_not_showing(new form_data(f, 1, voucher_id)) == true)
                    {
                        Global.background.show_form(f);
                        this.child_forms.Add(new form_data(f, 1, voucher_id));
                    }
                }
                if (this.vno == 5)
                {
                    M_V2_dyeingIssueForm f = new M_V2_dyeingIssueForm(row, true, this);
                    if (this.check_not_showing(new form_data(f, 1, voucher_id)) == true)
                    {
                        Global.background.show_form(f);
                        this.child_forms.Add(new form_data(f, 1, voucher_id));
                    }
                }
                if (this.vno == 6)
                {
                    M_V2_dyeingInwardForm f = new M_V2_dyeingInwardForm(row, true, this, "dyeingInward");
                    if (this.check_not_showing(new form_data(f, 1, voucher_id)) == true)
                    {
                        Global.background.show_form(f);
                        this.child_forms.Add(new form_data(f, 1, voucher_id));
                    }
                }
                if (this.vno == 7)
                {
                    M_V2_dyeingInwardForm f = new M_V2_dyeingInwardForm(row, true, this, "addBill");
                    if (this.check_not_showing(new form_data(f, 1, voucher_id)) == true)
                    {
                        Global.background.show_form(f);
                        this.child_forms.Add(new form_data(f, 1, voucher_id));
                    }
                }
                if (this.vno == 8)
                {
                    M_V3_cartonProductionForm f = new M_V3_cartonProductionForm(row, true, this);
                    if (this.check_not_showing(new form_data(f, 1, voucher_id)) == true)
                    {
                        Global.background.show_form(f);
                        this.child_forms.Add(new form_data(f, 1, voucher_id));
                    }
                }
                if (this.vno == 9)
                {
                    M_VC_cartonSalesForm f = new M_VC_cartonSalesForm(row, true, this, "Carton_Produced");
                    if (this.check_not_showing(new form_data(f, 1, voucher_id)) == true)
                    {
                        Global.background.show_form(f);
                        this.child_forms.Add(new form_data(f, 1, voucher_id));
                    }
                }
                if (this.vno == 10)
                {
                    M_VC_addBill f = new M_VC_addBill(row, true, this, "Carton");
                    if (this.check_not_showing(new form_data(f, 1, voucher_id)) == true)
                    {
                        Global.background.show_form(f);
                        this.child_forms.Add(new form_data(f, 1, voucher_id));
                    }
                }
                if (this.vno == 11)
                {
                    M_VC_addBill f = new M_VC_addBill(row, true, this, "Carton_Produced");
                    if (this.check_not_showing(new form_data(f, 1, voucher_id)) == true)
                    {
                        Global.background.show_form(f);
                        this.child_forms.Add(new form_data(f, 1, voucher_id));
                    }
                }
                if (this.vno == 12)
                {
                    M_V3_issueToReDyeingForm f = new M_V3_issueToReDyeingForm(row, true, this);
                    if (this.check_not_showing(new form_data(f, 1, voucher_id)) == true)
                    {
                        Global.background.show_form(f);
                        this.child_forms.Add(new form_data(f, 1, voucher_id));
                    }
                }
                if (this.vno == 13)
                {
                    T_V1_cartonInwardForm f = new T_V1_cartonInwardForm(row, true, this);
                    if (this.check_not_showing(new form_data(f, 1, voucher_id)) == true)
                    {
                        Global.background.show_form(f);
                        this.child_forms.Add(new form_data(f, 1, voucher_id));
                    }
                }
                if (this.vno == 14)
                {
                    T_V2_repackingForm f = new T_V2_repackingForm(row, true, this);
                    if (this.check_not_showing(new form_data(f, 1, voucher_id)) == true)
                    {
                        Global.background.show_form(f);
                        this.child_forms.Add(new form_data(f, 1, voucher_id));
                    }
                }
                if (this.vno == 15)
                {
                    T_V3_cartonSalesForm f = new T_V3_cartonSalesForm(row, true, this);
                    if (this.check_not_showing(new form_data(f, 1, voucher_id)) == true)
                    {
                        Global.background.show_form(f);
                        this.child_forms.Add(new form_data(f, 1, voucher_id));
                    }
                }
                if (this.vno == 16)
                {
                    T_V3_addBill f = new T_V3_addBill(row, true, this);
                    if (this.check_not_showing(new form_data(f, 1, voucher_id)) == true)
                    {
                        Global.background.show_form(f);
                        this.child_forms.Add(new form_data(f, 1, voucher_id));
                    }
                }
                if (this.vno == 100)
                {
                    M_V5_cartonProductionOpeningForm f = new M_V5_cartonProductionOpeningForm(row, true, this);
                    if (this.check_not_showing(new form_data(f, 0, voucher_id)) == true)
                    {
                        Global.background.show_form(f);
                        this.child_forms.Add(new form_data(f, 0, voucher_id));
                    }
                }
                this.prev_selected_row = index;
            }
        }

        //search
        private void searchButton_Click(object sender, EventArgs e)
        {
            this.search_in_voucher_table();
        }
        private void searchTB_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Enter) this.search_in_voucher_table();
        }
        private void searchByDateButton_Click(object sender, EventArgs e)
        {
            this.search_in_voucher_table(true);
        }
    }
}
