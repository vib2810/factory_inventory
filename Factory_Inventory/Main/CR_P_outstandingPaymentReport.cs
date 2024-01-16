﻿using Factory_Inventory.Factory_Classes;
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
    public partial class CR_P_outstandingPaymentReport : Form
    {
        DbConnect c = new DbConnect();
        HashSet<string> twistCustomers = new HashSet<string>();
        Dictionary<string, int> tradingCustomers = new Dictionary<string, int>();
        DataTable dt = new DataTable();
        HashSet<string> customers = new HashSet<string>();
        MainConnect mc;
        struct progressWork
        {
            public string customer;
            public Microsoft.Office.Interop.Excel.Application excelApp;
            public Microsoft.Office.Interop.Excel.Workbook workbook;
            public Microsoft.Office.Interop.Excel.Worksheet worksheet;
            public SaveFileDialog saveFileDialog;
        }
        public CR_P_outstandingPaymentReport(MainConnect mc)
        {
            InitializeComponent();
            this.mc = mc;

            // Read Customer Names from the database and store them in the combobox
            DataTable dt = c.runQuery("SELECT Customers FROM Customers");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                customers.Add(dt.Rows[i]["Customers"].ToString());
                twistCustomers.Add(dt.Rows[i]["Customers"].ToString());
            }

            dt = c.runQuery("SELECT Customer_Name, Customer_ID FROM T_M_Customers");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                customers.Add(dt.Rows[i]["Customer_Name"].ToString());
                tradingCustomers[dt.Rows[i]["Customer_Name"].ToString()] = int.Parse(dt.Rows[i]["Customer_ID"].ToString());
            }

            this.customerCB.DataSource = new BindingSource(customers, null);
            this.customerCB.DisplayMember = "Customers";
            this.customerCB.DropDownStyle = ComboBoxStyle.DropDown;//Create a drop-down list
            this.customerCB.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.customerCB.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            label3.Text = "";
        }

        private void addBorders(Microsoft.Office.Interop.Excel.Range range, List<bool> edge)
        {
            Microsoft.Office.Interop.Excel.Borders borders = range.Borders;

            if (edge[0]) borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            if (edge[1]) borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeRight].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            if (edge[2]) borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeTop].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            if (edge[3]) borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            label3.Text = "Preparing Data ...";
            HashSet<string> selectedCustomer = new HashSet<string>();
            if (checkBox1.Checked == false) selectedCustomer.Add(this.customerCB.Text);
            else selectedCustomer = this.customers;
            string sql = "";
            int count = 0;

            foreach (string customer in selectedCustomer)
            {
                if (twistCustomers.Contains(customer))
                {
                    sql += "(SELECT Sales_Voucher.Date_Of_Sale as 'Date of Sale',\n";
                    sql += "Sales_Voucher.Sale_DO_No as 'DO Number',\n";
                    sql += "Sales_Voucher.Quality as Quality,\n";
                    sql += "Sales_Voucher.Sale_Rate as Rate,\n";
                    sql += "Sales_Voucher.Sale_Rate* Sales_Voucher.Net_Weight as 'Total Amount',\n";
                    sql += "COALESCE(Sales_Voucher.Sale_Rate * Sales_Voucher.Net_Weight - SUM(Payments.Payment_Amount), Sales_Voucher.Sale_Rate * Sales_Voucher.Net_Weight) as 'Pending Amount',\n";
                    sql += "Sales_Voucher.Fiscal_Year\n";
                    sql += "FROM Sales_Voucher\n";
                    sql += "LEFT JOIN Payments ON Sales_Voucher.Voucher_ID = Payments.Sales_Voucher_ID\n";
                    sql += "WHERE DO_Payment_Closed = 0 AND Customer = '" + customer + "'\n";
                    sql += "GROUP BY Date_Of_Sale, Sale_Rate* Net_Weight, Fiscal_Year, Sale_DO_No, Quality, Sale_Rate)\n";
                }
                if (twistCustomers.Contains(customer) && tradingCustomers.ContainsKey(customer)) sql += "UNION\n";
                if (tradingCustomers.ContainsKey(customer))
                {
                    sql += "(SELECT temp.Date_Of_Sale as 'Date of Sale', temp.Sale_DO_No as 'DO Number', T_M_Quality_Before_Job.Quality_Before_Job as Quality, temp.Sale_Rate as Rate, temp.Total_Amount as 'Total Amount', temp.Pending_Amount as 'Pending Amount', temp.Fiscal_Year\n";
                    sql += "FROM\n";
                    sql += "    (SELECT T_Sales_Voucher.Date_Of_Sale,\n";
                    sql += "    T_Sales_Voucher.Sale_DO_No,\n";
                    sql += "    T_Sales_Voucher.Quality_ID,\n";
                    sql += "    T_Sales_Voucher.Sale_Rate,\n";
                    sql += "    T_Sales_Voucher.Sale_Rate * T_Sales_Voucher.Net_Weight as Total_Amount,\n";
                    sql += "    COALESCE(T_Sales_Voucher.Sale_Rate * T_Sales_Voucher.Net_Weight - SUM(T_Payments.Payment_Amount), T_Sales_Voucher.Sale_Rate * T_Sales_Voucher.Net_Weight) as Pending_Amount,\n";
                    sql += "    T_Sales_Voucher.Fiscal_Year\n";
                    sql += "    FROM T_Sales_Voucher\n";
                    sql += "    LEFT JOIN T_Payments ON T_Sales_Voucher.Voucher_ID = T_Payments.Sales_Voucher_ID\n";
                    sql += "    WHERE DO_Payment_Closed = 0 AND Customer_ID = " + tradingCustomers[customer].ToString() + "\n";
                    sql += "    GROUP BY Date_Of_Sale, Sale_Rate * Net_Weight, Fiscal_Year, Sale_DO_No, Quality_ID, Sale_Rate\n";
                    sql += "    ) as temp\n";
                    sql += "LEFT JOIN T_M_Quality_Before_Job ON T_M_Quality_Before_Job.Quality_Before_Job_ID = temp.Quality_ID)\n";
                }

                if (count != selectedCustomer.Count - 1) sql += "UNION\n";
                count++;
            }
            sql += "ORDER BY Date_Of_Sale DESC\n";

            dt = c.runQuery(sql);

            // Export to excel
            for (int i = 0; i < dt.Rows.Count; i++) dt.Rows[i]["Date of Sale"] = DateTime.Parse(dt.Rows[i]["Date of Sale"].ToString()).ToString("dd-MM-yyyy");

            DataTable firm = mc.runQuery("select Firm_Name from firms_list where Firm_ID = " + Global.firmid);
            string reportFor;
            if (checkBox1.Checked == true) reportFor = firm.Rows[0][0].ToString();
            else reportFor = customerCB.Text;

            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel Files|*.xlsx";
            saveFileDialog.FileName = "OutstandingReport_" + reportFor + "_" + DateTime.Now.Date.ToString("dd-MM-yyyy");
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                progressWork progressWorkObj = new progressWork();
                var excelApp = new Microsoft.Office.Interop.Excel.Application();
                var workbook = excelApp.Workbooks.Add();
                var worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets[1];
                progressWorkObj.customer = reportFor;
                progressWorkObj.worksheet = worksheet;
                progressWorkObj.excelApp = excelApp;
                progressWorkObj.workbook = workbook;
                progressWorkObj.saveFileDialog = saveFileDialog;

                backgroundWorker1.RunWorkerAsync(progressWorkObj);
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            string customer = ((progressWork)e.Argument).customer;
            var worksheet = ((progressWork)e.Argument).worksheet;

            // Report Heading
            worksheet.Cells[1, 1] = "Outstanding Report for " + customer;
            worksheet.Cells[1, 1].Font.Bold = true;
            worksheet.Cells[1, 1].Font.Size = 18;

            Microsoft.Office.Interop.Excel.Range range;
            range = worksheet.get_Range("A1", "H1");
            range.Merge(Type.Missing);
            range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            // Column Headers
            worksheet.Cells[2, 1] = "Fiscal Year";
            worksheet.Cells[2, 1].Font.Bold = true;
            for (int i = 0; i < dt.Columns.Count - 1; i++)
            {
                worksheet.Cells[2, i + 2] = dt.Columns[i].ColumnName;
                worksheet.Cells[2, i + 2].Font.Bold = true;
            }
            worksheet.Cells[2, dt.Columns.Count + 1] = "Total Amount for Fiscal Year";
            worksheet.Cells[2, dt.Columns.Count + 1].Font.Bold = true;
            range = worksheet.get_Range("A2", "H2");
            addBorders(range, new List<bool> { true, true, true, true });

            // Filling the rows
            string financialYear = dt.Rows[0]["Fiscal_Year"].ToString();
            worksheet.Cells[3, 1] = financialYear;
            worksheet.Cells[3, 1].Font.Bold = true;

            float financialYearPending = 0F;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["Fiscal_Year"].ToString() != financialYear)
                {
                    financialYear = dt.Rows[i]["Fiscal_Year"].ToString();
                    worksheet.Cells[i + 3, 1] = financialYear;
                    worksheet.Cells[i + 3, 1].Font.Bold = true;

                    worksheet.Cells[i + 2, dt.Columns.Count + 1] = financialYearPending;
                    worksheet.Cells[i + 2, dt.Columns.Count + 1].Font.Bold = true;
                    financialYearPending = 0F;
                    range = worksheet.get_Range("A" + (i + 2).ToString(), "H" + (i + 2).ToString());
                    addBorders(range, new List<bool> { false, false, false, true });
                }
                for (int j = 0; j < dt.Columns.Count - 1; j++)
                {
                    worksheet.Cells[i + 3, j + 2] = dt.Rows[i][j];
                }
                financialYearPending += float.Parse(dt.Rows[i]["Pending Amount"].ToString());
                backgroundWorker1.ReportProgress((int)((float)(i+1) / dt.Rows.Count * 100));
            }
            worksheet.Cells[dt.Rows.Count + 2, dt.Columns.Count + 1] = financialYearPending;
            worksheet.Cells[dt.Rows.Count + 2, dt.Columns.Count + 1].Font.Bold = true;
            range = worksheet.get_Range("A" + (dt.Rows.Count + 2).ToString(), "H" + (dt.Rows.Count + 2).ToString());
            addBorders(range, new List<bool> { false, false, false, true });

            worksheet.Cells[dt.Rows.Count + 3, dt.Columns.Count + 1] = Convert.ToDouble(dt.Compute("Sum([Pending Amount])", ""));
            worksheet.Cells[dt.Rows.Count + 3, dt.Columns.Count + 1].Font.Bold = true;
            range = worksheet.get_Range("H" + (dt.Rows.Count + 3).ToString(), "H" + (dt.Rows.Count + 3).ToString());
            addBorders(range, new List<bool> { true, true, true, true });

            worksheet.Cells[dt.Rows.Count + 3, 1] = "Total Pending Till Date";
            worksheet.Cells[dt.Rows.Count + 3, 1].Font.Bold = true;

            range = worksheet.get_Range("A" + (dt.Rows.Count + 3).ToString(), "G" + (dt.Rows.Count + 3).ToString());
            range.Merge(Type.Missing);
            range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;

            for (char column = 'A'; column <= 'H'; column++)
            {
                string cellName1 = string.Format("{0}{1}", column, 2);
                string cellName2 = string.Format("{0}{1}", column, dt.Rows.Count + 2);
                range = worksheet.get_Range(cellName1, cellName2);
                addBorders(range, new List<bool> { true, true, true, true });
            }
            range = worksheet.get_Range("A2", "H" + (dt.Rows.Count + 3).ToString());
            addBorders(range, new List<bool> { true, true, true, true });

            worksheet.Columns.AutoFit();
            e.Result = e.Argument;
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            label3.Text = "Generating Report ..." + e.ProgressPercentage.ToString() + "%";
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Microsoft.Office.Interop.Excel.Workbook workbook = ((progressWork)e.Result).workbook;
            Microsoft.Office.Interop.Excel.Application excelApp = ((progressWork)e.Result).excelApp;
            SaveFileDialog saveFileDialog = ((progressWork)e.Result).saveFileDialog;
            workbook.SaveAs(saveFileDialog.FileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlOpenXMLWorkbook);
            excelApp.Quit();
            c.SuccessBox("Finished Export");
            progressBar1.Value = 0;
            label3.Text = "";
            button1.Enabled = true;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true) customerCB.Enabled = false;
            else customerCB.Enabled = true;
        }
    }
}