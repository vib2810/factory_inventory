using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;

namespace Factory_Inventory.Factory_Classes
{
    class moduleExcel
    {
        public void ToCsV(System.Data.DataTable dt, string title, string filename)
        {
            //========Data from textbox==========//        
            string stOutput = "";
            string stTitle = "";
            string sHeaders = "";
            
            stTitle = "\r\n" + title + "\r\n\n";

            for (int j = 0; j < dt.Columns.Count; j++)
                sHeaders = sHeaders.ToString() + Convert.ToString(dt.Columns[j].ColumnName) + "\t";
            stOutput += sHeaders + "\r\n";
            // Export data.  
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string stLine = "";
                for (int j = 0; j < dt.Columns.Count; j++)
                    stLine = stLine.ToString() + Convert.ToString(dt.Rows[i][j]) + "\t";
                stOutput += stLine + "\r\n";
            }
            Encoding utf16 = Encoding.GetEncoding(1254);
            byte[] output = utf16.GetBytes(stOutput);
            FileStream fs = new FileStream(filename, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);

            bw.Write(output, 0, output.Length); //write the encoded file  
            bw.Flush();
            bw.Close();
            fs.Close();
        }
        private Worksheet FindSheet(Workbook workbook, string sheet_name)
        {
            foreach (Worksheet sheet in workbook.Sheets)
            {
                if (sheet.Name == sheet_name) return sheet;
            }
            return null;
        }
        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

    }
}
