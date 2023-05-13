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
                sql = "SELECT Date_of_Issue, Batch_No, Quality, Colour, Dyeing_Company_Name, Net_Weight FROM Dyeing_Issue_Voucher WHERE Date_Of_Issue BETWEEN '" + start_date_string + "' AND '" + end_date_string + "' ORDER BY 'Date_of_Issue' ASC";
                filename = "Issue_" + start_date_string + "_" + end_date_string + ".xls";
            }
            else if (mode == 2)
            {
                sql = "SELECT Dyeing_In_Date, Batch_No, Quality, Colour, Dyeing_Company_Name, Net_Weight FROM Batch WHERE Dyeing_In_Date BETWEEN '" + start_date_string + "' AND '" + end_date_string + "' AND Dyeing_In_Voucher_ID IS NOT NULL AND Redyeing_Voucher_ID IS NULL ORDER BY 'Dyeing_In_Date' ASC";
                filename = "Receive_" + start_date_string + "_" + end_date_string + ".xls";
            }
            else if (mode == 3)
            {
                sql = "SELECT Date_Of_Production, Batch_No, Quality, Colour, Net_Weight FROM Batch WHERE Date_Of_Production BETWEEN '" + start_date_string + "' AND '" + end_date_string + "' AND Production_Voucher_ID IS NOT NULL AND Redyeing_Voucher_ID IS NULL ORDER BY 'Date_Of_Production' ASC";
                filename = "Production_" + start_date_string + "_" + end_date_string + ".xls";
            }

            DataTable dt = c.runQuery(sql);

            string title = " Excel Export";
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Documents (*.xls)|*.xls";
            sfd.FileName = filename;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                excelImp.ToCsV(dt, title, sfd.FileName);
                MessageBox.Show("Finished Export");
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
