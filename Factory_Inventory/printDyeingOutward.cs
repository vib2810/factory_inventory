﻿using Factory_Inventory.Factory_Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using FontStyle = System.Drawing.FontStyle;

namespace Factory_Inventory
{
    public partial class M_V4_printDyeingOutward : Form
    {
        private DbConnect c;
        private int topmargin;
        private int lrmargin;
        public M_V4_printDyeingOutward(DataRow row)
        {
            InitializeComponent();
            this.c = new DbConnect();
            this.batchnoTextbox.Text = row["Batch_No"].ToString();
            //this.dateTimePicker1.Value = Convert.ToDateTime(row["Input_Date"].ToString());
            this.outDateTextbox.Text = row["Dyeing_Out_Date"].ToString().Substring(0,10);
            this.customerNameTextbox.Text = row["Dyeing_Company_Name"].ToString();
            this.qualityTextbox.Text = row["Quality"].ToString();
            this.shadeTextbox.Text = row["Colour"].ToString();
            this.netwtTextbox.Text = row["Net_Weight"].ToString();
            string[] tray_ids = c.csvToArray(row["Tray_ID_Arr"].ToString());
            DataTable dt = new DataTable();
            dt.Columns.Add("Sl No");
            dt.Columns.Add("Tray No.");
            dt.Columns.Add("Gross Wt");
            dt.Columns.Add("Tray Wt");
            dt.Columns.Add("MNo");
            dt.Columns.Add("Springs");
            dt.Columns.Add("Spring Wt");
            dt.Columns.Add("Net Wt");
            float net_wt = 0F;
            for (int i=0; i<tray_ids.Length; i++)
            {
                DataTable dtemp= c.getTrayTable_TrayID(int.Parse(tray_ids[i]));
                float spring_wt = c.getSpringWeight(dtemp.Rows[0]["Spring"].ToString())*int.Parse(dtemp.Rows[0]["Number_Of_Springs"].ToString());
                net_wt += float.Parse(dtemp.Rows[0]["Net_Weight"].ToString());
                dt.Rows.Add(i+1, dtemp.Rows[0]["Tray_No"].ToString(), dtemp.Rows[0]["Gross_Weight"].ToString(), dtemp.Rows[0]["Tray_Tare"].ToString(), dtemp.Rows[0]["Machine_No"].ToString(), dtemp.Rows[0]["Number_of_Springs"].ToString(), spring_wt,dtemp.Rows[0]["Net_Weight"].ToString());
            }

            dt.Rows.Add("", "", "", "", "", "", "Net Weight", net_wt);
            dataGridView1.DataSource = dt;
            int weight_width = 100;
            dataGridView1.Columns["Sl No"].Width = 70;
            dataGridView1.Columns["Tray No."].Width = 100;
            dataGridView1.Columns["Gross Wt"].Width = weight_width;
            dataGridView1.Columns["Tray Wt"].Width = weight_width;
            dataGridView1.Columns["MNo"].Width = 50;
            dataGridView1.Columns["Springs"].Width = 50;
            dataGridView1.Columns["Spring Wt"].Width = weight_width;
            dataGridView1.Columns["Net Wt"].Width = weight_width;
            dataGridView1.Columns["Net Wt"].AutoSizeMode= DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.Transparent;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.Black;
        }

        private int write(System.Drawing.Printing.PrintPageEventArgs e, int x, int y, int width, string text, int size, char lr='c', int bold=0, int drawrect=0)
        {
            Graphics g=e.Graphics;
            StringFormat format = new StringFormat();
            format.LineAlignment = StringAlignment.Center;
            if (width==0)
            {
                format.Alignment = StringAlignment.Center;
            }
            else if(lr=='l')
            {
                format.Alignment = StringAlignment.Near;
            }
            else if(lr=='r')
            {
                format.Alignment = StringAlignment.Far;
            }
            else if (lr == 'c')
            {
                format.Alignment = StringAlignment.Center;
            }

            SolidBrush myBrush = new SolidBrush(Color.Black);
            Font newFont;
            if (bold==1)
            {
                newFont = new Font(dataGridView1.Font.FontFamily, size, FontStyle.Bold);
            }
            else
            {
                newFont= new Font(dataGridView1.Font.FontFamily, size, dataGridView1.Font.Style);
            }
            if (width == 0) width = e.PageBounds.Width - 2 * lrmargin;
            if (x == -1) x = lrmargin;
            else x = x + lrmargin;
            g.DrawString(text, newFont, myBrush, new RectangleF(x, topmargin+y, width, newFont.Height+5), format);
            if(drawrect==1)
            {
                g.DrawRectangle(Pens.Black, x, topmargin + y, width, newFont.Height + 5);
            }
            return newFont.Height + 7;
        }
        int write_header(int write_height, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //inputs write_height(not including the margin)
            //returns the next write height(not including the margin)
            int basic_size = 8;
            int header_size_sub = 5;

            write_height += write(e, -1, write_height, 0, "|| Shri ||", basic_size + 2, 'c', 1) - header_size_sub;
            write_height += write(e, -1, write_height, 0, "Krishana Sales and Industries", basic_size + 6, 'c', 1) - header_size_sub - 3;
            write_height += write(e, -1, write_height, 0, "550/1, Datta Galli, M. Vadgaon, Belgavi", basic_size + 2, 'c', 1) - header_size_sub;
            write_height += write(e, -1, write_height, 0, "(GSTIN No. 29AIOPM5869K1Z8)", basic_size + 2, 'c', 1);

            int current_width = e.PageBounds.Width - 2 * lrmargin;
            //first row
            write(e, 0, write_height, (int)(0.15 * current_width), "Invoice Number: ", basic_size, 'l', 1);
            write(e, (int)(0.15 * current_width), write_height, (int)(0.15 * current_width), this.batchnoTextbox.Text, basic_size, 'l', 0, 1);
            write(e, (int)(0.50 * current_width), write_height, (int)(0.25 * current_width), "Invoice Date: ", basic_size, 'r', 1);
            write_height += write(e, (int)(0.75 * current_width), write_height, (int)(0.25 * current_width), this.outDateTextbox.Text, basic_size, 'r', 0, 1);
            //second row
            write(e, 0, write_height, (int)(0.25 * current_width), "Customer Name: ", basic_size, 'l', 1);
            write_height += write(e, (int)(0.25 * current_width), write_height, (int)(0.75 * current_width), this.customerNameTextbox.Text, basic_size, 'l', 0, 1);
            //third row
            write(e, 0, write_height, (int)(0.25 * current_width), "Customer Address: ", basic_size, 'l', 1);
            write_height += write(e, (int)(0.25 * current_width), write_height, (int)(0.75 * current_width), this.customerAddressTextbox.Text, basic_size, 'l', 0, 1);
            //fourth row
            write(e, 0, write_height, (int)(0.20 * current_width), "Customer GSTIN: ", basic_size, 'l', 1);
            write(e, (int)(0.20 * current_width), write_height, (int)(0.45 * current_width), this.customergstin.Text, basic_size, 'l', 0, 1);
            write(e, (int)(0.65 * current_width), write_height, (int)(0.20 * current_width), "HSN Number: ", basic_size, 'r', 1);
            write_height += write(e, (int)(0.85 * current_width), write_height, (int)(0.15 * current_width), this.hsnnumber.Text, basic_size, 'r', 0, 1);
            //fifth row
            write(e, 0, write_height, (int)(0.10 * current_width), "Quality: ", basic_size, 'l', 1);
            write(e, (int)(0.10 * current_width), write_height, (int)(0.23 * current_width), this.qualityTextbox.Text, basic_size, 'l', 0, 1);
            write(e, (int)(0.33 * current_width), write_height, (int)(0.10 * current_width), "Shade: ", basic_size, 'r', 1);
            write(e, (int)(0.43 * current_width), write_height, (int)(0.23 * current_width), this.shadeTextbox.Text, basic_size, 'l', 0, 1);
            write(e, (int)(0.66 * current_width), write_height, (int)(0.20 * current_width), "Net Weight: ", basic_size, 'r', 1);
            write_height += write(e, (int)(0.86 * current_width), write_height, (int)(0.14 * current_width), this.netwtTextbox.Text, basic_size, 'r', 0, 1);
            Console.WriteLine("inside writeheight "+ write_height);
            return write_height;
        }
        int drawDGV(int write_height, System.Drawing.Printing.PrintPageEventArgs e)
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
            for (int j = 0; j < dataGridView1.Columns.Count; j++) total_width += dataGridView1.Columns[j].Width;

            int new_total = e.PageBounds.Width - 2 * lrmargin;
            float scale = (float)new_total / total_width;
            int[] column_widths = new int[dataGridView1.Columns.Count];
            int new_total_real = 0;
            for (int j = 0; j < dataGridView1.Columns.Count; j++)
            {
                column_widths[j] = (int)(scale * (float)dataGridView1.Columns[j].Width);
                new_total_real += column_widths[j];
            }
            column_widths[dataGridView1.Columns.Count - 1] += new_total - new_total_real;
            int start_height = topmargin + write_height;
            Console.WriteLine("write height dgv: " + write_height);
            int start_width = lrmargin;
            int width = start_width;
            Font newFont = new Font(dataGridView1.Font.FontFamily, dataGridView1.Font.Size, FontStyle.Bold);
            for (int j = 0; j < dataGridView1.Columns.Count; j++)
            {
                str.Alignment = StringAlignment.Center;
                //draws the first cell of column
                g.FillRectangle(Brushes.LightGray, new Rectangle(width, start_height, column_widths[j], dataGridView1.Rows[0].Height));
                g.DrawRectangle(Pens.Black, width, start_height, column_widths[j], dataGridView1.Rows[0].Height);
                g.DrawString(dataGridView1.Columns[j].HeaderText, newFont, Brushes.Black, new RectangleF(width, start_height, column_widths[j], dataGridView1.Rows[0].Height), str);
                if(j==0) write_height+= dataGridView1.Rows[0].Height;
                newFont = new Font(dataGridView1.Font.FontFamily, dataGridView1.Font.Size, FontStyle.Regular);
                str.Alignment = StringAlignment.Far;
                int height = start_height;
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if(i==dataGridView1.Rows.Count-1)
                    {
                        newFont = new Font(dataGridView1.Font.FontFamily, dataGridView1.Font.Size, FontStyle.Bold);
                    }
                    height += dataGridView1.Rows[i].Height;
                    if(j==0) write_height+= dataGridView1.Rows[i].Height;
                    g.DrawRectangle(Pens.Black, width, height, column_widths[j], dataGridView1.Rows[0].Height);
                    g.DrawString(dataGridView1.Rows[i].Cells[j].Value.ToString(), newFont, Brushes.Black, new RectangleF(width, height, column_widths[j], dataGridView1.Rows[0].Height), str);
                }
                width += column_widths[j];
            }
            #endregion
            return write_height;
        }
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //Page setup
            PrinterSettings ps = new PrinterSettings();
            printDocument1.PrinterSettings = ps;
            IEnumerable<PaperSize> paperSizes = ps.PaperSizes.Cast<PaperSize>();
            PaperSize sizeA4 = paperSizes.First<PaperSize>(size => size.Kind == PaperKind.A4); // setting paper size to A4 size
            printDocument1.DefaultPageSettings.PaperSize = sizeA4;

            //set margins
            //1100*850
            this.topmargin = (int)(0.03 * e.PageBounds.Height);
            this.lrmargin = (int)(0.05 * e.PageBounds.Width);

            //draw 2 rects
            Graphics g=e.Graphics;
            //g.DrawRectangle(Pens.Blue, lrmargin, topmargin, e.PageBounds.Width - 2 * lrmargin, e.PageBounds.Height-2*topmargin);
            //g.DrawRectangle(Pens.Blue, lrmargin, topmargin, (e.PageBounds.Width - 2 * lrmargin), (e.PageBounds.Height - 2 * topmargin)/2);
            int rect_width = e.PageBounds.Width - 2 * lrmargin + 20;
            int rect_height = ((e.PageBounds.Height - 2 * topmargin) / 2);
            g.DrawRectangle(Pens.Black, lrmargin - 10, topmargin - 5, rect_width, rect_height);
            g.DrawRectangle(Pens.Black, lrmargin - 10, topmargin -5 + rect_height+10, rect_width, rect_height);

            int current_width = e.PageBounds.Width - 2 * lrmargin;
            //Draw all graphics
            int write_height = write_header(-5, e);
            write_height = drawDGV(write_height, e);
            write(e, (int)(0.75 * current_width), rect_height - 5 - 25, (int)(0.25 * current_width), "Signature", 9, 'c', 1);
            write_height = rect_height + 5;
            write_height = write_header(write_height, e);
            write_height = drawDGV(write_height, e);
            write(e, (int)(0.75 * current_width), 2*rect_height - 25, (int)(0.25 * current_width), "Signature", 9, 'c', 1);
            //if (height > e.MarginBounds.Height)
            //{
            //    height = 100;
            //    width = 100;
            //    e.HasMorePages = true;
            //    return;
            //}
            //e.Graphics.DrawImage(bmp,
            //             (e.PageBounds.Width - bmp.Width) / 2,
            //             (e.PageBounds.Height - bmp.Height) / 2,
            //             bmp.Width,
            //             bmp.Height);
        }
        Bitmap bmp;
        private void button1_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.ShowDialog();

            //Graphics g = this.CreateGraphics();
            //bmp = new Bitmap(this.Size.Width, this.Size.Height, g);
            //Graphics mg = Graphics.FromImage(bmp);
            //mg.CopyFromScreen(this.Location.X, this.Location.Y, 0, 0, this.Size);
            //printPreviewDialog1.ShowDialog();

        }
    }
}