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

namespace Factory_Inventory
{
    public partial class printDyeingOutward : Form
    {
        private DbConnect c;
        public printDyeingOutward(DataRow row)
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
            dt.Columns.Add("Springs");
            dt.Columns.Add("Spring Wt");
            dt.Columns.Add("Net Wt");
            float net_wt = 0F;
            for (int i=0; i<tray_ids.Length; i++)
            {
                DataTable dtemp= c.getTrayTable_TrayID(int.Parse(tray_ids[i]));
                float spring_wt = c.getSpringWeight(dtemp.Rows[0]["Spring"].ToString())*int.Parse(dtemp.Rows[0]["Number_Of_Springs"].ToString());
                net_wt += float.Parse(dtemp.Rows[0]["Net_Weight"].ToString());
                dt.Rows.Add(i+1, dtemp.Rows[0]["Tray_No"].ToString(), dtemp.Rows[0]["Gross_Weight"].ToString(), dtemp.Rows[0]["Tray_Tare"].ToString(), dtemp.Rows[0]["Number_of_Springs"].ToString(), spring_wt, dtemp.Rows[0]["Net_Weight"].ToString());
            }
            dt.Rows.Add("", "", "", "", "", "Net Weight", net_wt);
            dataGridView1.DataSource = dt;
            int weight_width = 100;
            dataGridView1.Columns[0].Width = 70;
            dataGridView1.Columns[1].Width = 100;
            dataGridView1.Columns[2].Width = weight_width;
            dataGridView1.Columns[3].Width = weight_width;
            dataGridView1.Columns[4].Width = 70;
            dataGridView1.Columns[5].Width = weight_width;
            dataGridView1.Columns[6].Width = weight_width;
            dataGridView1.Columns[6].AutoSizeMode= DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.Transparent;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.Black;
        }
        private int topmargin = 100;
        private int lrmargin = 100;
        private int write(System.Drawing.Printing.PrintPageEventArgs e, int x, int y, int width, string text, int size, char lr='p', int bold=0, int drawrect=0)
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
            return newFont.Height + 10;
        }
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Graphics g=e.Graphics;
            this.topmargin =(int) (0.05 * e.PageBounds.Height);
            this.lrmargin = (int) (0.05 * e.PageBounds.Width);
            Console.WriteLine(this.topmargin);
            int write_height = 0; //height from top margin
            write_height+= write(e, -1, write_height, 0, "|| Shri ||", 12);
            write_height += write(e, -1, write_height, 0, "Krishana Sales and Industries", 16);
            write_height += write(e, -1, write_height, 0, "550/1, Datta Galli, M. Vadgaon, Belgavi", 12);
            write_height += write(e, -1, write_height, 0, "(GSTIN No. 29AIOPM5869K1Z8)", 12);

            int current_width = e.PageBounds.Width - 2 * lrmargin; 
            //first row
            write(e, 0,                           write_height, (int)(0.15* current_width), "Invoice Number: ", 10, 'l', 1);
            write(e, (int)(0.15 * current_width), write_height, (int)(0.35 * current_width), this.batchnoTextbox.Text, 10, 'l', 0, 1);
            write(e, (int)(0.50 * current_width), write_height, (int)(0.25 * current_width), "Invoice Date: ", 10, 'r',1);
            write_height += write(e, (int)(0.75 * current_width), write_height, (int)(0.25 *current_width), this.outDateTextbox.Text, 10, 'r', 0, 1);
            //second row
            write(e, 0, write_height, (int)(0.25 * current_width), "Customer Name: " , 10, 'l', 1);
            write_height += write(e, (int)(0.25 * current_width), write_height, (int)(0.75 * current_width), this.customerNameTextbox.Text, 10, 'l', 0, 1);
            //third row
            write(e, 0, write_height, (int)(0.25 * current_width), "Customer Address: ", 10, 'l', 1);
            write_height += write(e, (int)(0.25 * current_width), write_height, (int)(0.75 * current_width), this.customerAddressTextbox.Text, 10, 'l', 0, 1);
            //fourth row
            write(e, 0, write_height, (int)(0.20 * current_width), "Customer GSTIN: ", 10, 'l', 1);
            write(e, (int)(0.20 * current_width), write_height, (int)(0.45 * current_width), this.customergstin.Text, 10, 'l', 0, 1);
            write(e, (int)(0.65 * current_width), write_height, (int)(0.20 * current_width), "HSN Number: ", 10, 'r', 1);
            write_height += write(e, (int)(0.85 * current_width), write_height, (int)(0.15 * current_width), this.hsnnumber.Text, 10, 'r', 0, 1);
            //fifth row
            write(e, 0, write_height, (int)(0.10 * current_width), "Quality: ", 10, 'l', 1);
            write(e, (int)(0.10 * current_width), write_height, (int)(0.23 * current_width), this.qualityTextbox.Text, 10, 'l', 0, 1);
            write(e, (int)(0.33* current_width), write_height, (int)(0.10 * current_width), "Shade: ", 10, 'r', 1);
            write(e, (int)(0.43 * current_width), write_height, (int)(0.23 * current_width), this.shadeTextbox.Text, 10, 'l', 0, 1);
            write(e, (int)(0.66 * current_width), write_height, (int)(0.20 * current_width), "Net Weight: ", 10, 'r', 1);
            write_height += write(e, (int)(0.86 * current_width), write_height, (int)(0.14 * current_width), this.netwtTextbox.Text, 10, 'r', 0, 1);
            
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
            for (int j = 0; j < dataGridView1.Columns.Count; j++) column_widths[j] = (int) (scale*(float)dataGridView1.Columns[j].Width);

            int start_height = topmargin + write_height;
            int start_width = lrmargin;
            int width = start_width;
            for (int j=0; j<dataGridView1.Columns.Count; j++)
            {
                //draws the first cell of column
                g.FillRectangle(Brushes.LightGray, new Rectangle(width, start_height, column_widths[j], dataGridView1.Rows[0].Height));
                g.DrawRectangle(Pens.Black, width, start_height, column_widths[j], dataGridView1.Rows[0].Height);
                g.DrawString(dataGridView1.Columns[j].HeaderText, dataGridView1.Font, Brushes.Black, new RectangleF(width, start_height, column_widths[j], dataGridView1.Rows[0].Height), str);

                int height = start_height;
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    height += dataGridView1.Rows[i].Height;
                    g.DrawRectangle(Pens.Black, width, height, column_widths[j], dataGridView1.Rows[0].Height);
                    g.DrawString(dataGridView1.Rows[i].Cells[j].Value.ToString(), dataGridView1.Font, Brushes.Black, new RectangleF(width, height, column_widths[j], dataGridView1.Rows[0].Height), str);
                }
                width += column_widths[j];
            }
            #endregion

            //if (height > e.MarginBounds.Height)
            //{
            //    height = 100;
            //    width = 100;
            //    e.HasMorePages = true;
            //    return;
            //}
            //e.PageBounds.Width
            //Bitmap bmp = new Bitmap(this.dataGridView1.Width, this.dataGridView1.Height);
            //dataGridView1.DrawToBitmap(bmp, new Rectangle(0, 0, this.dataGridView1.Width, this.dataGridView1.Height));
            //e.Graphics.DrawImage(bm, 0, 0);
            //e.Graphics.DrawImage(bmp,
            //             (e.PageBounds.Width - bmp.Width) / 2,
            //             (e.PageBounds.Height - bmp.Height) / 2,
            //             bmp.Width,
            //             bmp.Height);
            //e.Graphics.DrawImage(bmp, 0, 0);
        }
        Bitmap bmp;
        private void button1_Click(object sender, EventArgs e)
        {
            int height = dataGridView1.Height;
            //dataGridView1.Height = dataGridView1.RowCount * dataGridView1.RowTemplate.Height * 2;
            //bmp = new Bitmap(dataGridView1.Width, dataGridView1.Height);
            //dataGridView1.DrawToBitmap(bmp, new Rectangle(0, 0, dataGridView1.Width, dataGridView1.Height));
            //dataGridView1.Height = height;
            printPreviewDialog1.ShowDialog();
            //DGVPrinter printer;
            //printDocument1.Print();

            //Graphics g = this.CreateGraphics();
            //bmp = new Bitmap(this.Size.Width, this.Size.Height, g);
            //Graphics mg = Graphics.FromImage(bmp);
            //mg.CopyFromScreen(this.Location.X, this.Location.Y, 0, 0, this.Size);
            //printPreviewDialog1.ShowDialog();
            //DGVPrinter printer = new DGVPrinter();
            //printer.Title = "DataGridView Report";
            //printer.SubTitle = "An Easy to Use DataGridView Printing Object";
            //printer.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;

            //printer.PageNumbers = true;

            //printer.PageNumberInHeader = false;

            //printer.PorportionalColumns = true;

            //printer.HeaderCellAlignment = StringAlignment.Near;

            //printer.Footer = "Your Company Name Here";

            //printer.FooterSpacing = 15;



            //printer.PrintDataGridView(dataGridView1);

        }
    }
}
