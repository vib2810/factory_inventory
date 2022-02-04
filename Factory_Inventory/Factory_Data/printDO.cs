﻿using CoolPrintPreview;
using Factory_Inventory.Factory_Classes;
using Factory_Inventory.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using FontStyle = System.Drawing.FontStyle;

namespace Factory_Inventory
{
    public partial class printDO : Form
    {
        private DbConnect c;
        private int topmargin;
        private int lrmargin;
        private string where; //voucher ID=<> is saved
        private string font_name;
        int type = -1;
        M_V4_printDO parent;
        T_V5_printDO parent1;
        Dictionary<string, string> firmDetails = new Dictionary<string, string>();

        public printDO(DataRow row, M_V4_printDO f, T_V5_printDO f1, string fontname= "generic sans serif")
        {
            InitializeComponent();
            this.font_name = fontname;
            this.c = new DbConnect();
            if (f != null) this.parent = f;
            else this.parent1 = f1;
            if (row.Table.Columns.Count < 3) return;
            if (row["Type_Of_Sale"].ToString() == "0")
            {
                label3.Visible = false;
                label5.Visible = false;
                label6.Visible = false;
                this.label7.Location = new System.Drawing.Point(268, 48);
            }
            // Load default firm details from defaults
            DataTable dt10 = c.runQuery("SELECT * FROM Defaults WHERE Default_Type= 'Print' and default_name = 'Default Print Type'");
            string default_print_type_id = dt10.Rows[0]["Default_Value"].ToString();

            DataTable dt_printTypes = c.runQuery("select * from Print_Types where Print_Type_ID=" + default_print_type_id);
            if (dt_printTypes == null)
            {
                c.WarningBox("(Print:Default Print Type) is set as " + default_print_type_id + ", not found in Default Print_Type_IDs Table, " +
                    "defaulting to first entry in Print_Types table");
                dt_printTypes = c.runQuery("select * from Print_Types");
            }
            for (int j = 0; j < dt_printTypes.Columns.Count - 1; j++)
            {
                firmDetails[dt_printTypes.Columns[j].ColumnName] = dt_printTypes.Rows[0][j].ToString();
            }

            this.type = int.Parse(row["Type_Of_Sale"].ToString());
            this.donoTextbox.Text = row["Sale_DO_No"].ToString();
            this.saleDateTextbox.Text = row["Date_Of_Sale"].ToString().Substring(0, 10);
            this.customerNameTextbox.Text = row["Customer"].ToString();
            this.qualityTextbox.Text = row["Quality"].ToString();
            this.label3.Text = firmDetails["Firm_Name"];
            this.label5.Text = firmDetails["Address"];
            float sale_rate = float.Parse(row["Sale_Rate"].ToString());
            float net_weight = float.Parse(row["Net_Weight"].ToString());

            //this.amountTextbox.Text = (sale_rate * net_weight).ToString("F2");
            //this.netwtTextbox.Text = net_weight.ToString("F3");
            this.rateTB.Text = sale_rate.ToString("F2");

            //string[] carton_nos = c.csvToArray(row["Carton_No_Arr"].ToString());
            //string cartonfisc = row["Carton_Fiscal_Year"].ToString();
            string table = "";
            try
            {
                table = row["Tablename"].ToString();
            }
            catch{ }
            DataTable dt = new DataTable();
            dt.Columns.Add("Sl No");
            dt.Columns.Add("Carton No.");
            dt.Columns.Add("Shade");
            dt.Columns.Add("Net Wt.");
            dt.Columns.Add("Rate");
            float net_wt = 0F, net_rate=0F;
            DataTable cartons = new DataTable();
            if (table == "Carton")
            {
                cartons = c.getTableData("Carton", "*", "TS_Voucher_ID=" + row["Voucher_ID"].ToString() + " AND Date_Of_Sale IS Not Null");
            }
            else if(table == "Carton_Produced")
            {
                cartons = c.getTableData("Carton_Produced", "*", "Sales_Voucher_ID=" + row["Voucher_ID"].ToString());
            }
            else
            {
                //THESE CARTONS ARE FROM T_Inward_Cartons TABLE AS WELL. INCLUDE THEM AS WELL
                string sql = "SELECT temp1.*, T_Carton_Sales.Sales_Voucher_ID, T_Carton_Sales.Sold_Weight as 'Net_Weight'\n";
                sql += "FROM\n";
                sql += "(SELECT T_Repacked_Cartons.Carton_ID, T_Repacked_Cartons.Carton_No, T_M_Colours.Colour\n";
                sql += "FROM T_Repacked_Cartons\n";
                sql += "LEFT OUTER JOIN T_M_Colours\n";
                sql += "ON T_Repacked_Cartons.Colour_ID = T_M_Colours.Colour_ID) as temp1\n";
                sql += "LEFT OUTER JOIN T_Carton_Sales\n";
                sql += "ON T_Carton_Sales.Carton_ID = temp1.Carton_ID\n";
                sql += "WHERE Sales_Voucher_ID = " + row["Voucher_ID"].ToString() + "\n";
                sql += "UNION ALL\n";
                sql += "SELECT temp2.*, T_Carton_Sales.Sales_Voucher_ID, T_Carton_Sales.Sold_Weight as 'Net_Weight'\n";
                sql += "FROM\n";
                sql += "(SELECT T_Inward_Carton.Carton_ID, T_Inward_Carton.Carton_No, T_M_Colours.Colour\n";
                sql += "FROM T_Inward_Carton\n";
                sql += "LEFT OUTER JOIN T_M_Colours\n";
                sql += "ON T_Inward_Carton.Colour_ID = T_M_Colours.Colour_ID) as temp2\n";
                sql += "LEFT OUTER JOIN T_Carton_Sales\n";
                sql += "ON T_Carton_Sales.Carton_ID = temp2.Carton_ID\n";
                sql += "WHERE Sales_Voucher_ID = " + row["Voucher_ID"].ToString() + ";\n";
                cartons = c.runQuery(sql);
            }
            for (int i = 0; i < cartons.Rows.Count; i++)
            {
                DataRow dtemp = cartons.Rows[i];
                string colour = "Gray";
                if (table != "Carton")
                { 
                    colour = dtemp["Colour"].ToString();
                }
                net_wt += float.Parse(dtemp["Net_Weight"].ToString());
                float rate = (float.Parse(dtemp["Net_Weight"].ToString()) * float.Parse(row["Sale_Rate"].ToString()));
                net_rate += rate;
                dt.Rows.Add(i + 1, dtemp["Carton_No"].ToString(), colour, dtemp["Net_Weight"].ToString(), rate.ToString("F2"));
            }

            dt.Rows.Add("", "", "Net Weight", net_wt.ToString("F3"), net_rate.ToString("F2"));
            dataGridView1.DataSource = dt;
            dataGridView1.Columns["Sl No"].Width = 60;
            c.auto_adjust_dgv(dataGridView1);
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.White;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.Black;
            dataGridView1.Columns.Cast<DataGridViewColumn>().ToList().ForEach(t => t.SortMode = DataGridViewColumnSortMode.NotSortable);
            dataGridView1.Columns["Sl NO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            c.set_dgv_column_sort_state(dataGridView1, DataGridViewColumnSortMode.NotSortable);
            this.where = "Voucher_ID=" + int.Parse(row["Voucher_ID"].ToString());

            this.label3.Text = firmDetails["Firm_Name"];
            this.label5.Text = firmDetails["Address"];
            this.label6.Text = "(GSTIN No. " + firmDetails["GSTIN"] + ")";
            PrinterSettings ps = new PrinterSettings();
            printDocument1.PrinterSettings = ps;
            IEnumerable<PaperSize> paperSizes = ps.PaperSizes.Cast<PaperSize>();
            PaperSize sizeA4 = paperSizes.First<PaperSize>(size => size.Kind == PaperKind.A4); // setting paper size to A4 size
            printDocument1.DefaultPageSettings.PaperSize = sizeA4;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (this.parent != null)
            {
                c.setPrint("Sales_Voucher", this.where, 1);
                parent.load_color();
            }
            else
            {
                c.setPrint("T_Sales_Voucher", this.where, 1);
                parent1.load_color();
            }
            using (var dlg = new CoolPrintPreviewDialog())
            {
                dlg.Document = this.printDocument1;
                dlg.WindowState = FormWindowState.Maximized;
                dlg.ShowDialog(this);
            }
            //printPreviewDialog1.ShowDialog();
        }
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //set margins
            //1100*850
            this.topmargin = (int)(0.03 * e.PageBounds.Height);
            this.lrmargin = (int)(0.05 * e.PageBounds.Width);

            //draw 2 rects
            Graphics g = e.Graphics;

            int slip_width = (e.PageBounds.Width - 2 * lrmargin) / 2;
            int slip_height = (e.PageBounds.Height - 2 * topmargin) / 2;
            write_slip(e, lrmargin-5, topmargin-5, slip_width, slip_height);
            write_slip(e, lrmargin + 5 + slip_width, topmargin-5  , slip_width, slip_height);
        }
        
        //print
        int write_slip(System.Drawing.Printing.PrintPageEventArgs e, int x, int write_height, int width, int height)
        {
            int y = write_height;
            int basic_size = 8;
            int gap = 1;
            //Draw rect
            e.Graphics.DrawRectangle(Pens.Black, x, write_height, width, height);
            //header
            int sub_header = 6;
            if (this.type==1)
            {
                write_height += 3;
                write_height += write(e, x, write_height, width, "||Shri||", basic_size, 'c', 0) - sub_header;
                write_height += write(e, x, write_height, width, firmDetails["Firm_Name"], basic_size + 2, 'c', 0) - sub_header-2;
                write_height += write(e, x, write_height, width, firmDetails["Address"], basic_size + 1, 'c', 0) - sub_header;
                write_height += write(e, x, write_height, width, "(GSTIN No. "+ firmDetails["GSTIN"] + ")", basic_size + 1, 'c', 0) - sub_header+2;
            }
            else
            {
                write_height += 15;
                write_height += write(e, x, write_height, width, "||Shri||", basic_size+3, 'c', 0);
            }

            //main
            x = x + 5;
            width = width - 10;
            List<string> a = new List<string>() { "DO No.", donoTextbox.Text, "Date of Sale", saleDateTextbox.Text};
            List<double> r = new List<double>() { 0.15, 0.25, 0.30, 0.30};
            write_height = write_row(e, x, write_height, width, basic_size, a, r) + gap;
            
            write(e, x + (int)(0.00 * width), write_height, (int)(0.30 * width), "Customer Name", basic_size, 'l', 1, 0);
            write_height+=write(e, x + (int)(0.30 * width), write_height, (int)(0.70 * width), customerNameTextbox.Text, basic_size, 'l', 1, 1)+gap;

            a = new List<string>() { "Quality", qualityTextbox.Text, "Rate", rateTB.Text};
            r = new List<double>() { 0.20, 0.40, 0.15, 0.25};
            write_height =write_row(e, x, write_height, width, basic_size+1, a, r)+gap;
            write_height= drawDGV(e, x, width, write_height, dataGridView1);
            write(e, x + (int)(0.80 * width), y+height-25, (int)(0.20 * width), "Signature", basic_size, 'c', 1, 0);
            //write(e, x + (int)(0.10 * width), write_height, (int)(0.20 * width), this.rateTB.Text, basic_size, 'l', 0, 1);
            //write(e, x + (int)(0.30 * width), write_height, (int)(0.125 * width), "Amount", basic_size, 'l', 1, 0);
            //write(e, x + (int)(0.425 * width), write_height, (int)(0.20 * width), this.amountTextbox.Text, basic_size, 'l', 0, 1);
            //write(e, x + (int)(0.625 * width), write_height, (int)(0.175 * width), "Net Weight", basic_size, 'l', 1, 0);
            //write_height += write(e, x + (int)(0.80 * width), write_height, (int)(0.20 * width), this.netwtTextbox.Text, basic_size, 'l', 0, 1) + gap;
            return 0;
        }
        int write_row(System.Drawing.Printing.PrintPageEventArgs e, int x, int write_height, int width, int basic_size, List<string> data, List<double> ratios)
        {
            write(e, x + (int)(0.00 * width), write_height, (int)(ratios[0]* width), data[0], basic_size, 'l', 1, 0);
            write(e, x + (int)(ratios[0]* width), write_height, (int)(ratios[1] * width), data[1], basic_size, 'l', 0, 1);
            write(e, x + (int)((ratios[0] + ratios[1]) * width), write_height, (int)(ratios[2]* width), data[2], basic_size, 'r', 1, 0);
            write_height += write(e, x + (int)((ratios[0] + ratios[1] + ratios[2]) * width), write_height, (int)(ratios[3]* width), data[3], basic_size, 'l', 0, 1);
            return write_height;
        }
        int drawDGV(System.Drawing.Printing.PrintPageEventArgs e, int x, int grid_width, int write_height, DataGridView d)
        {
            //inputs write_height(not including the margin)
            //returns the next write height(not including the margin)
            Graphics g = e.Graphics;
            //Print Datagridview
            #region
            StringFormat str = new StringFormat();
            str.Alignment = StringAlignment.Near;
            str.LineAlignment = StringAlignment.Center;
            str.Trimming = StringTrimming.EllipsisCharacter;
            Pen p = new Pen(Color.Black, 2.5f);

            //rescale widths of datagridview
            int total_width = 0;
            for (int j = 0; j < d.Columns.Count; j++) total_width += d.Columns[j].Width;

            int new_total = grid_width;
            float scale = (float)new_total / total_width;
            int[] column_widths = new int[d.Columns.Count];
            int new_total_real = 0;
            for (int j = 0; j < d.Columns.Count; j++)
            {
                column_widths[j] = (int)(scale * (float)d.Columns[j].Width);
                new_total_real += column_widths[j];
            }
            column_widths[d.Columns.Count - 1] += new_total - new_total_real;
            int start_height =  write_height;
            int width = x;
            Font newFont = new Font(d.Font.FontFamily, d.Font.Size, FontStyle.Bold);
            for (int j = 0; j < d.Columns.Count; j++)
            {
                str.Alignment = StringAlignment.Center;
                //draws the first cell of column
                g.FillRectangle(Brushes.LightGray, new Rectangle(width, start_height, column_widths[j], d.Rows[0].Height));
                g.DrawRectangle(Pens.Black, width, start_height, column_widths[j], d.Rows[0].Height);
                g.DrawString(d.Columns[j].HeaderText, newFont, Brushes.Black, new RectangleF(width, start_height, column_widths[j], d.Rows[0].Height), str);
                if (j == 0) write_height += d.Rows[0].Height;
                newFont = new Font(d.Font.FontFamily, d.Font.Size, FontStyle.Regular);
                str.Alignment = StringAlignment.Far;
                int height = start_height;
                for (int i = 0; i < d.Rows.Count; i++)
                {
                    if (i == d.Rows.Count - 1)
                    {
                        newFont = new Font(d.Font.FontFamily, d.Font.Size, FontStyle.Bold);
                    }
                    height += d.Rows[i].Height;
                    if (j == 0) write_height += d.Rows[i].Height;
                    g.DrawRectangle(Pens.Black, width, height, column_widths[j], d.Rows[0].Height);
                    g.DrawString(d.Rows[i].Cells[j].Value.ToString(), newFont, Brushes.Black, new RectangleF(width, height, column_widths[j], d.Rows[0].Height), str);
                }
                width += column_widths[j];
            }
            #endregion
            return write_height;
        }
        private int write(System.Drawing.Printing.PrintPageEventArgs e, int x, int y, int width, string text, int size, char lr = 'c', int bold = 0, int drawrect = 0)
        {
            Graphics g = e.Graphics;
            StringFormat format = new StringFormat();
            format.LineAlignment = StringAlignment.Center;
            if (width == 0)
            {
                format.Alignment = StringAlignment.Center;
            }
            else if (lr == 'l')
            {
                format.Alignment = StringAlignment.Near;
            }
            else if (lr == 'r')
            {
                format.Alignment = StringAlignment.Far;
            }
            else if (lr == 'c')
            {
                format.Alignment = StringAlignment.Center;
            }

            SolidBrush myBrush = new SolidBrush(Color.Black);
            Font newFont;
            if (bold == 1)
            {
                newFont = new Font(this.font_name, size, FontStyle.Bold);
            }
            else
            {
                newFont = new Font(this.font_name, size, FontStyle.Regular);
            }
            if (width == 0) width = e.PageBounds.Width - 2 * lrmargin;
            g.DrawString(text, newFont, myBrush, new RectangleF(x, y, width, newFont.Height + 5), format);
            if (drawrect == 1)
            {
                g.DrawRectangle(Pens.Black, x, y, width, newFont.Height + 5);
            }
            return newFont.Height + 7;
        }
    }
}
