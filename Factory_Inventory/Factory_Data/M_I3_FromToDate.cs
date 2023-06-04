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

namespace Factory_Inventory.Factory_Data
{
    public partial class M_I3_FromToDate : Form
    {
        DbConnect c = new DbConnect();
        moduleExcel excelImp = new moduleExcel();
        public M_I3_FromToDate()
        {
            InitializeComponent();
            monthDTP.Format = DateTimePickerFormat.Custom;
            monthDTP.CustomFormat = "MM/yyyy";
            monthDTP.ShowUpDown = true;
            this.Text = "Generate Report (From to Date)";
            issueToDyeingRB.Checked = true;
        }

        private void exportToExcel(DateTime start_date, DateTime end_date)
        {
            string start_date_string = start_date.ToString("yyyy-MM-dd");
            string end_date_string = end_date.ToString("yyyy-MM-dd");
            string sql = "";
            int mode = -1;
            string filename = "";

            IEnumerable<RadioButton> buttons = panel1.Controls.OfType<RadioButton>();
            foreach (var Button in buttons)
            {
                if (Button.Checked)
                {
                    if (Button.Name == "issueToDyeingRB") mode = 1;
                    else if (Button.Name == "receivedFromDyeingRB") mode = 2;
                    else if (Button.Name == "catronProducedRB") mode = 3;
                }
            }

            if (mode==1)
            {
                sql = "SELECT Date_of_Issue AS 'Date of Issue', Batch_No AS 'Batch Number', Quality, Colour, Dyeing_Company_Name AS 'Dyeing Company Name', Net_Weight AS 'Net Weight' FROM Dyeing_Issue_Voucher WHERE Date_Of_Issue BETWEEN '" + start_date_string + "' AND '" + end_date_string + "' ORDER BY 'Date of Issue' ASC";
                filename = "Issue_" + start_date_string + "_" + end_date_string + ".xlsx";
            }
            else if (mode == 2)
            {
                sql = "SELECT Dyeing_In_Date AS 'Dyeing Inward Date', Batch_No AS 'Batches', Quality, Colour, Dyeing_Company_Name AS 'Dyeing Company Name', Net_Weight AS 'Net Weight' FROM Batch WHERE Dyeing_In_Date BETWEEN '" + start_date_string + "' AND '" + end_date_string + "' AND Dyeing_In_Voucher_ID IS NOT NULL AND Redyeing_Voucher_ID IS NULL ORDER BY 'Dyeing Inward Date' ASC";
                filename = "Receive_" + start_date_string + "_" + end_date_string + ".xlsx";
            }
            else if (mode == 3)
            {
                sql = "SELECT Date_Of_Production AS 'Date of Production', Batch_No_Arr AS 'Batches', Quality, Colour, Net_Carton_Weight AS 'Net Weight' FROM Carton_Production_Voucher WHERE Date_Of_Production BETWEEN '" + start_date_string + "' AND '" + end_date_string + "' ORDER BY 'Date Of Production' ASC";
                filename = "Production_" + start_date_string + "_" + end_date_string + ".xlsx";
            }

            DataTable dt = c.runQuery(sql);
            if (dt == null) return;
            if(dt.Rows.Count == 0)
            {
                c.WarningBox("No record exists for the given dates");
                return;
            }

            string dateColumnName = dt.Columns[0].ColumnName;
            DataColumn newColumn = new DataColumn(dateColumnName + ".", typeof(string));
            dt.Columns.Add(newColumn);
            newColumn.SetOrdinal(1);

            for (int i = 0; i < dt.Rows.Count; i++) dt.Rows[i][1] = DateTime.Parse(dt.Rows[i][0].ToString()).ToString("dd-MM-yyyy");
            //Convert.ToDateTime(dt.Rows[0][0]).ToString().Remove(Convert.ToDateTime(dt.Rows[0][0]).ToString().Length - 9);

            dt.Columns.Remove(dateColumnName);
            dt.Columns[0].ColumnName = dateColumnName;

            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel Files|*.xlsx";
            saveFileDialog.FileName = filename;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                var excelApp = new Microsoft.Office.Interop.Excel.Application();
                var workbook = excelApp.Workbooks.Add();
                var worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets[1];
                
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    worksheet.Cells[1, i + 1] = dt.Columns[i].ColumnName;
                    worksheet.Cells[1, i + 1].Font.Bold = true;
                }

                int dateColumnIndex = 0;
                // Set the NumberFormat property of the cells in the date column to "text"
                worksheet.Range[worksheet.Cells[2, dateColumnIndex + 1], worksheet.Cells[dt.Rows.Count + 1, dateColumnIndex + 1]].NumberFormat = "@";

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        worksheet.Cells[i + 2, j + 1] = dt.Rows[i][j];
                    }
                }

                worksheet.Columns.AutoFit();

                workbook.SaveAs(saveFileDialog.FileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlOpenXMLWorkbook);
                excelApp.Quit();
                c.SuccessBox("Finished Export");
            }
        }

        private void monthButton_Click(object sender, EventArgs e)
        {
            DateTime start_date = new DateTime(monthDTP.Value.Year, monthDTP.Value.Month, 1);
            DateTime end_date = new DateTime(monthDTP.Value.Year, monthDTP.Value.Month, DateTime.DaysInMonth(monthDTP.Value.Year, monthDTP.Value.Month));

            this.exportToExcel(start_date, end_date);
        }

        private void datesButton_Click(object sender, EventArgs e)
        {
            if(fromDateDTP.Value >= toDateDTP.Value)
            {
                c.ErrorBox("From Date should be before To Date");
                return;
            }

            DateTime start_date = new DateTime(fromDateDTP.Value.Year, fromDateDTP.Value.Month, fromDateDTP.Value.Day);
            DateTime end_date = new DateTime(toDateDTP.Value.Year, toDateDTP.Value.Month, toDateDTP.Value.Day);

            this.exportToExcel(start_date, end_date);
        }
    }
}
