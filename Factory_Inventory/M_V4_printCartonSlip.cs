using Factory_Inventory.Factory_Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Factory_Inventory
{
    public partial class M_V4_printCartonSlip : Form
    {
        private DbConnect c;
        DataTable dt2= new DataTable();
        DataTable dt3 = new DataTable();
        DataTable dt4 = new DataTable();
        int topmargin, lrmargin;
        int slip_count = 0, printed_slips=0;
        string carton_fiscal_year;
        List<int> cartons_to_print = new List<int>();
        public M_V4_printCartonSlip()
        {
            InitializeComponent();
            this.c = new DbConnect();

            //Load Data
            //Create drop-down lists
            var dataSource = new List<string>();
            DataTable d = c.getQC('f');
            dataSource.Add("---Select---");

            for (int i = 0; i < d.Rows.Count; i++)
            {
                dataSource.Add(d.Rows[i][0].ToString());
            }
            this.fiscalCombobox.DataSource = dataSource;
            this.fiscalCombobox.DisplayMember = "Financial Year";
            this.fiscalCombobox.DropDownStyle = ComboBoxStyle.DropDownList;//Create a drop-down list
            this.fiscalCombobox.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.fiscalCombobox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.fiscalCombobox.SelectedIndex = this.fiscalCombobox.FindStringExact(c.getFinancialYear(DateTime.Now));

            //Datagridviews

            DataTable dyeing_batches = c.getBatchTable_State(3);
            dataGridView1.DataSource = dyeing_batches;
            if(dyeing_batches.Rows.Count!=0)
            {
                dataGridView1.Columns.OfType<DataGridViewColumn>().ToList().ForEach(col => col.Visible = false);
                this.dataGridView1.Columns["Batch_No"].Visible = true;
                this.dataGridView1.Columns["Colour"].Visible = true;
                this.dataGridView1.Columns["Quality"].Visible = true;
                this.dataGridView1.Columns["Date_Of_Production"].Visible = true;
                this.dataGridView1.Columns["Net_Weight"].Visible = true;
                this.dataGridView1.Columns["Number_Of_Trays"].Visible = true;
                this.dataGridView1.Columns["Fiscal_Year"].Visible = true;

                this.dataGridView1.Columns["Fiscal_Year"].HeaderText = "Financial Year";
                this.dataGridView1.Columns["Number_Of_Trays"].HeaderText = "Number of Trays";
                this.dataGridView1.Columns["Batch_No"].HeaderText = "Batch Number";
                this.dataGridView1.Columns["Date_Of_Production"].HeaderText = "Date of Production";
                
                this.dataGridView1.Columns["Batch_No"].Width = 70;
                this.dataGridView1.Columns["Number_Of_Trays"].Width = 80;
            }
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.White;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.Blue;
            dataGridView1.Columns["Fiscal_Year"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dataGridView2.DataSource=dt2;
            dataGridView2.DefaultCellStyle.SelectionBackColor = Color.White;
            dataGridView2.DefaultCellStyle.SelectionForeColor = Color.Blue;

            dataGridView3.DataSource = this.dt3;
            dataGridView3.RowHeadersVisible = false;
            this.dt3.Columns.Clear();
            this.dt3.Columns.Add("SlNo");
            this.dt3.Columns.Add("Batch_No");
            this.dt3.Columns.Add("Financal Year");
            this.dt3.Columns.Add("Net Weight");
            dataGridView3.DefaultCellStyle.SelectionBackColor = Color.White;
            dataGridView3.DefaultCellStyle.SelectionForeColor = Color.Black;
            dataGridView3.Columns["Net Weight"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dataGridView4.DataSource = this.dt4;
            dataGridView4.RowHeadersVisible = false;
            this.dt4.Columns.Clear();
            this.dt4.Columns.Add("SlNo");
            this.dt4.Columns.Add("Carton No.");
            this.dt4.Columns.Add("Net Weight");
            dataGridView4.Columns["Net Weight"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView4.DefaultCellStyle.SelectionBackColor = Color.White;
            dataGridView4.DefaultCellStyle.SelectionForeColor = Color.Blue;
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            int batch_no;
            string fiscal_year;
            try
            {
                batch_no= int.Parse(batchnoTextbox.Text);
            }
            catch
            {
                MessageBox.Show("Please Enter Numeric Batch Number");
                return;
            }
            fiscal_year = fiscalCombobox.Text;
            DataTable result = c.getBatchTable_BatchNoState(batch_no, 3, fiscal_year);
            if (result.Rows.Count==0)
            {
                this.dt2.Clear();
                this.dt2.Columns.Clear();
                this.dt2.Columns.Add("Not Found");
                this.dt2.Rows.Add("No Conned Batch Found with the following details: Batch Number=" + batch_no + " and Financial Year=" + fiscal_year);
                this.dataGridView2.Columns[0].Width = 800;
                this.dataGridView2.DataSource = dt2;
                dataGridView2.Columns["Not Found"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            }
            else
            {
                this.dt2= result;
                this.dataGridView2.DataSource = dt2;
                dataGridView2.Columns.OfType<DataGridViewColumn>().ToList().ForEach(col => col.Visible = false);
                this.dataGridView2.Columns["Batch_No"].Visible = true;
                this.dataGridView2.Columns["Colour"].Visible = true;
                this.dataGridView2.Columns["Quality"].Visible = true;
                this.dataGridView2.Columns["Date_Of_Production"].Visible = true;
                this.dataGridView2.Columns["Net_Weight"].Visible = true;
                this.dataGridView2.Columns["Number_Of_Trays"].Visible = true;
                this.dataGridView2.Columns["Fiscal_Year"].Visible = true;

                this.dataGridView2.Columns["Fiscal_Year"].HeaderText = "Financial Year";
                this.dataGridView2.Columns["Number_Of_Trays"].HeaderText = "Number of Trays";
                this.dataGridView2.Columns["Batch_No"].HeaderText = "Batch Number";
                this.dataGridView2.Columns["Date_Of_Production"].HeaderText = "Date of Production";

                this.dataGridView2.Columns["Batch_No"].Width = 70;
                this.dataGridView2.Columns["Number_Of_Trays"].Width= 80;

                dataGridView2.Columns["Fiscal_Year"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            this.dataGridView2.Rows[0].Selected = false;
            return;
        }

        private void printButton_Click(object sender, EventArgs e)
        {
            if(dataGridView4.SelectedRows.Count==0)
            {
                MessageBox.Show("Please Select Cartons to Print");
                return;
            }
            this.cartons_to_print.Clear();
            for(int i=0; i<dataGridView4.SelectedRows.Count; i++)
            {
                int carton= int.Parse(dataGridView4.SelectedRows[i].Cells[dataGridView4.Columns["Carton No."].Index].Value.ToString() );
                this.cartons_to_print.Add(carton);
            }
            this.slip_count = dataGridView4.SelectedRows.Count;
            printPreviewDialog1.ShowDialog();
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count != 0) dataGridView2.SelectedRows[0].Selected = false;
        }

        private void dataGridView2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 0) dataGridView1.SelectedRows[0].Selected = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int batch_no=-1;
            int voucher_id=-1;
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int index = this.dataGridView1.SelectedRows[0].Index;
                if (index >= this.dataGridView1.Rows.Count)
                {
                    MessageBox.Show("Please select valid voucher", "Error");
                    return;
                }
                DataRow row = (dataGridView1.Rows[index].DataBoundItem as DataRowView).Row;
                batch_no = int.Parse(row["Batch_No"].ToString());
                voucher_id= int.Parse(row["Voucher_ID"].ToString());
            }
            if (dataGridView2.SelectedRows.Count > 0)
            {
                int index = this.dataGridView2.SelectedRows[0].Index;
                if (index >= this.dataGridView2.Rows.Count - 1)
                {
                    MessageBox.Show("Please select valid voucher", "Error");
                    return;
                }
                DataRow row = (dataGridView2.Rows[index].DataBoundItem as DataRowView).Row;
                batch_no = int.Parse(row["Batch_No"].ToString());
                voucher_id= int.Parse(row["Voucher_ID"].ToString());
            }
            if(batch_no==-1 || voucher_id==-1)
            {
                MessageBox.Show("Invalid Batch, couldnt fetch voucher");
            }
            DataTable voucher = c.getProductionVoucherTable_VoucherID(voucher_id);

            //dt3
            this.dt3.Clear();
            string[] batch_nos = c.csvToArray(voucher.Rows[0]["Batch_No_Arr"].ToString());
            string[] batch_fiscal_years = c.csvToArray(voucher.Rows[0]["Batch_Fiscal_Year_Arr"].ToString());
            for (int i=0; i<batch_nos.Length; i++)
            {
                float batch_wt = float.Parse(c.getColumnBatchNo("Net_Weight", int.Parse(batch_nos[i]), batch_fiscal_years[i]));
                dt3.Rows.Add(i + 1, batch_nos[i], batch_fiscal_years[i], batch_wt);
            }
            dataGridView3.DataSource = this.dt3;
            if(this.dataGridView3.SelectedRows.Count>=0) this.dataGridView3.Rows[0].Selected = false;

            //dt4
            this.dt4.Clear();
            string[] carton_nos= c.csvToArray(voucher.Rows[0]["Carton_No_Production_Arr"].ToString());
            this.carton_fiscal_year = voucher.Rows[0]["Carton_Fiscal_Year"].ToString();
            for (int i = 0; i < carton_nos.Length; i++)
            {
                float net_wt = c.getCartonProducedWeight(carton_nos[i], carton_fiscal_year);
                dt4.Rows.Add(i + 1, carton_nos[i], net_wt);
            }
            dataGridView4.DataSource = this.dt4;
            if (this.dataGridView4.SelectedRows.Count >= 0) this.dataGridView4.Rows[0].Selected = false;
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //Page setup
            PrinterSettings ps = new PrinterSettings();
            printDocument1.PrinterSettings = ps;
            IEnumerable<PaperSize> paperSizes = ps.PaperSizes.Cast<PaperSize>();
            PaperSize sizeA4 = paperSizes.First<PaperSize>(size => size.Kind == PaperKind.A4); // setting paper size to A4 size
            printDocument1.DefaultPageSettings.PaperSize = sizeA4;

            //set margins and initial setup
            this.topmargin = (int)(0.03 * e.PageBounds.Height);
            this.lrmargin = (int)(0.05 * e.PageBounds.Width);
            Graphics g = e.Graphics;
            int slip_width = (e.PageBounds.Width - 2 * lrmargin) / 2;
            int slip_height = (e.PageBounds.Height - 2 * topmargin) / 2;

            int[] w = new int[4]; //stores the x coordinates of the 2 slips
            w[0] = lrmargin - 5;
            w[1] = w[0] + slip_width + 10;
            w[2] = lrmargin - 5;
            w[3] = w[0] + slip_width + 10;
            int running_y = topmargin - 5;
            int local_print_count = 0;

            while (this.slip_count - this.printed_slips > 0)
            {
                write_slip(e, w[local_print_count], running_y, slip_width, slip_height, this.cartons_to_print[this.printed_slips]);
                if (local_print_count == 1) running_y += slip_height + 10;
                if (local_print_count == 3)
                {
                    this.printed_slips++;
                    break;
                }
                local_print_count++;
                this.printed_slips++;
            }
            if (this.slip_count - this.printed_slips > 0) e.HasMorePages = true;
            else e.HasMorePages = false;
        }
       
        int write_slip(System.Drawing.Printing.PrintPageEventArgs e, int x, int write_height, int width, int height, int carton_no)
        {
            int basic_size = 8;
            int header_size_sub = 5;
            //Draw rect
            e.Graphics.DrawRectangle(Pens.Black, x, write_height, width, height);

            //header
            write_height += write(e, x, write_height, width, "Mfg. By: 0831-4205025  ||Shri||  Ph. No.:0230-2424088", basic_size -2, 'c', 0) + 4;
            write_height += write(e, x, write_height, width, carton_no.ToString(), basic_size+10, 'c', 0) + 4;

            //write_height += write(e, -1, write_height, 0, "Krishana Sales and Industries", basic_size + 6, 'c', 1) - header_size_sub - 3;
            //write_height += write(e, -1, write_height, 0, "550/1, Datta Galli, M. Vadgaon, Belgavi", basic_size + 2, 'c', 1) - header_size_sub;
            //write_height += write(e, -1, write_height, 0, "(GSTIN No. 29AIOPM5869K1Z8)", basic_size + 2, 'c', 1);

            //int current_width = e.PageBounds.Width - 2 * lrmargin;
            ////first row
            //write(e, 0, write_height, (int)(0.15 * current_width), "Invoice Number: ", basic_size, 'l', 1);
            //write(e, (int)(0.15 * current_width), write_height, (int)(0.15 * current_width), this.batchnoTextbox.Text, basic_size, 'l', 0, 1);
            //write(e, (int)(0.50 * current_width), write_height, (int)(0.25 * current_width), "Invoice Date: ", basic_size, 'r', 1);
            //write_height += write(e, (int)(0.75 * current_width), write_height, (int)(0.25 * current_width), this.outDateTextbox.Text, basic_size, 'r', 0, 1);
            ////second row
            //write(e, 0, write_height, (int)(0.25 * current_width), "Customer Name: ", basic_size, 'l', 1);
            //write_height += write(e, (int)(0.25 * current_width), write_height, (int)(0.75 * current_width), this.customerNameTextbox.Text, basic_size, 'l', 0, 1);
            ////third row
            //write(e, 0, write_height, (int)(0.25 * current_width), "Customer Address: ", basic_size, 'l', 1);
            //write_height += write(e, (int)(0.25 * current_width), write_height, (int)(0.75 * current_width), this.customerAddressTextbox.Text, basic_size, 'l', 0, 1);
            ////fourth row
            //write(e, 0, write_height, (int)(0.20 * current_width), "Customer GSTIN: ", basic_size, 'l', 1);
            //write(e, (int)(0.20 * current_width), write_height, (int)(0.45 * current_width), this.customergstin.Text, basic_size, 'l', 0, 1);
            //write(e, (int)(0.65 * current_width), write_height, (int)(0.20 * current_width), "HSN Number: ", basic_size, 'r', 1);
            //write_height += write(e, (int)(0.85 * current_width), write_height, (int)(0.15 * current_width), this.hsnnumber.Text, basic_size, 'r', 0, 1);
            ////fifth row
            //write(e, 0, write_height, (int)(0.10 * current_width), "Quality: ", basic_size, 'l', 1);
            //write(e, (int)(0.10 * current_width), write_height, (int)(0.23 * current_width), this.qualityTextbox.Text, basic_size, 'l', 0, 1);
            //write(e, (int)(0.33 * current_width), write_height, (int)(0.10 * current_width), "Shade: ", basic_size, 'r', 1);
            //write(e, (int)(0.43 * current_width), write_height, (int)(0.23 * current_width), this.shadeTextbox.Text, basic_size, 'l', 0, 1);
            //write(e, (int)(0.66 * current_width), write_height, (int)(0.20 * current_width), "Net Weight: ", basic_size, 'r', 1);
            //write_height += write(e, (int)(0.86 * current_width), write_height, (int)(0.14 * current_width), this.netwtTextbox.Text, basic_size, 'r', 0, 1);
            //Console.WriteLine("inside writeheight " + write_height);
            //return write_height;
            return 0;
        }

        private void printDocument1_BeginPrint(object sender, PrintEventArgs e)
        {
            this.printed_slips = 0;
        }

        private void printDocument1_EndPrint(object sender, PrintEventArgs e)
        {
            this.printed_slips = 0;
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
                if (j == 0) write_height += dataGridView1.Rows[0].Height;
                newFont = new Font(dataGridView1.Font.FontFamily, dataGridView1.Font.Size, FontStyle.Regular);
                str.Alignment = StringAlignment.Far;
                int height = start_height;
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (i == dataGridView1.Rows.Count - 1)
                    {
                        newFont = new Font(dataGridView1.Font.FontFamily, dataGridView1.Font.Size, FontStyle.Bold);
                    }
                    height += dataGridView1.Rows[i].Height;
                    if (j == 0) write_height += dataGridView1.Rows[i].Height;
                    g.DrawRectangle(Pens.Black, width, height, column_widths[j], dataGridView1.Rows[0].Height);
                    g.DrawString(dataGridView1.Rows[i].Cells[j].Value.ToString(), newFont, Brushes.Black, new RectangleF(width, height, column_widths[j], dataGridView1.Rows[0].Height), str);
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
                newFont = new Font(dataGridView1.Font.FontFamily, size, FontStyle.Bold);
            }
            else
            {
                newFont = new Font(dataGridView1.Font.FontFamily, size, dataGridView1.Font.Style);
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
