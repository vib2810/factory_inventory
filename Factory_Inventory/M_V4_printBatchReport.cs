using Factory_Inventory.Factory_Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Factory_Inventory
{
    public partial class M_V4_printBatchReport : Form
    {
        private DbConnect c;
        DataTable dt2 = new DataTable();
        DataTable dt3 = new DataTable();
        DataTable dt4 = new DataTable();

        int topmargin, lrmargin;
        bool print_batch = true;
        string carton_fiscal_year;
        List<int> cartons_to_print = new List<int>();
        List<string> cartons_to_print_fiscal_year = new List<string>();
        int printed_rows = 0, rows_to_print=0;
        int printed_pages=0;
        float net_carton_wt = 0;
        int printed_voucher_id = -1;
        Dictionary<int, int> vouchers = new Dictionary<int, int>(); //to store active vouchers
        
        public M_V4_printBatchReport()
        {
            InitializeComponent();
            this.c = new DbConnect();

            //Load Data
            //Create drop-down lists
            var dataSource = new List<string>();
            DataTable d = c.getQC('f');

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
            if (true)
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

            dataGridView2.DataSource = dt2;
            dataGridView2.DefaultCellStyle.SelectionBackColor = Color.White;
            dataGridView2.DefaultCellStyle.SelectionForeColor = Color.Blue;

            dataGridView3.DataSource = this.dt3;
            dataGridView3.RowHeadersVisible = false;
            this.dt3.Columns.Clear();
            this.dt3.Columns.Add("SlNo");
            this.dt3.Columns.Add("Batch_No");
            this.dt3.Columns.Add("Financal Year");
            this.dt3.Columns.Add("Net Weight");
            dataGridView3.Columns["SlNo"].Width = 60;
            dataGridView3.DefaultCellStyle.SelectionBackColor = Color.White;
            dataGridView3.DefaultCellStyle.SelectionForeColor = Color.Black;
            dataGridView3.Columns["Net Weight"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dataGridView4.DataSource = this.dt4;
            dataGridView4.RowHeadersVisible = false;
            this.dt4.Columns.Clear();
            this.dt4.Columns.Add("SlNo");
            this.dt4.Columns.Add("Carton No.");
            this.dt4.Columns.Add("Cones");
            this.dt4.Columns.Add("Gross Wt");
            this.dt4.Columns.Add("Tare Wt");
            this.dt4.Columns.Add("Net Wt");
            this.dt4.Columns.Add("Grade");
            dataGridView4.Columns["SlNo"].Width = 70;
            dataGridView4.Columns["Grade"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView4.DefaultCellStyle.SelectionBackColor = Color.White;
            dataGridView4.DefaultCellStyle.SelectionForeColor = Color.Black;

            PrinterSettings ps = new PrinterSettings();
            printDocument1.PrinterSettings = ps;
            IEnumerable<PaperSize> paperSizes = ps.PaperSizes.Cast<PaperSize>();
            PaperSize sizeA4 = paperSizes.First<PaperSize>(size => size.Kind == PaperKind.A4); // setting paper size to A4 size
            printDocument1.DefaultPageSettings.PaperSize = sizeA4;
        }

        private void M_V4_printBatchReport_Load(object sender, EventArgs e)
        {
            //fill the vouchers dictionary
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                string voucher = dataGridView1.Rows[i].Cells["Voucher_ID"].Value.ToString();
                int voucher_id = -1;
                if (voucher != "") voucher_id = int.Parse(voucher);
                vouchers[voucher_id] = 0;
            }
            //get prints of distinct voucher ids
            List<int> keys = new List<int>(vouchers.Keys);
            foreach (int key in keys)
            {
                int voucher_id = key;
                int printed = c.getPrint("Carton_Production_Voucher", "Voucher_ID=" + voucher_id);
                if (printed > 0) vouchers[key] = 1;
            }
            //color the dgv
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                string voucher = dataGridView1.Rows[i].Cells["Voucher_ID"].Value.ToString();
                int voucher_id = -1;
                if (voucher != "") voucher_id = int.Parse(voucher);
                int printed = vouchers[voucher_id];
                Console.WriteLine(i + " " + printed);
                if (printed > 0)
                {
                    Console.WriteLine(" setting for" + i);
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Global.printedColor;
                    dataGridView1.Rows[i].DefaultCellStyle.SelectionBackColor = Global.printedColor;
                }
            }
            dataGridView1.Visible = false;
            dataGridView1.Visible = true;
        }
        //user
        private void load_batch()
        {
            int batch_no = -1;
            int voucher_id = -1;
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int index = this.dataGridView1.SelectedRows[0].Index;
                if (index >= this.dataGridView1.Rows.Count)
                {
                    c.ErrorBox("Please select valid voucher", "Error");
                    return;
                }
                DataRow row = (dataGridView1.Rows[index].DataBoundItem as DataRowView).Row;
                batch_no = int.Parse(row["Batch_No"].ToString());
                voucher_id = int.Parse(row["Voucher_ID"].ToString());
            }
            if (dataGridView2.SelectedRows.Count > 0)
            {
                if (dataGridView2.Columns.Count <= 2) return;
                int index = this.dataGridView2.SelectedRows[0].Index;
                if (index >= this.dataGridView2.Rows.Count)
                {
                    c.ErrorBox("Please select valid voucher", "Error");
                    return;
                }
                DataRow row = (dataGridView2.Rows[index].DataBoundItem as DataRowView).Row;
                batch_no = int.Parse(row["Batch_No"].ToString());
                voucher_id = int.Parse(row["Voucher_ID"].ToString());
            }
            if (batch_no == -1 || voucher_id == -1)
            {
                //c.ErrorBox("Invalid Batch, couldnt fetch voucher", "Error");
                return;
            }
            this.printed_voucher_id = voucher_id;
            DataTable voucher = c.getProductionVoucherTable_VoucherID(voucher_id);
            if (voucher.Rows[0]["Date_Of_Production"].ToString() == null || voucher.Rows[0]["Date_Of_Production"].ToString() == "") //the batches are not closed  
            {
                label15.Text = "The Batches arent closed yet";
                this.print_batch = false;
            }
            else
            {
                label15.Text = "";
                this.print_batch = true;
            }

            //dt3
            this.dt3.Clear();
            string[] batch_nos = c.csvToArray(voucher.Rows[0]["Batch_No_Arr"].ToString());
            string[] batch_fiscal_years = c.csvToArray(voucher.Rows[0]["Batch_Fiscal_Year_Arr"].ToString());
            List<string> tray_ids = new List<string>();
            List<float> batch_wts = new List<float>();
            float total_batch_wts = 0F;
            int batch_index = -1;
            for (int i = 0; i < batch_nos.Length; i++)
            {
                float batch_wt = float.Parse(c.getColumnBatchNo("Net_Weight", int.Parse(batch_nos[i]), batch_fiscal_years[i]));
                dt3.Rows.Add(i + 1, batch_nos[i], batch_fiscal_years[i], batch_wt);
                batch_wts.Add(batch_wt);
                total_batch_wts += batch_wt;
                if (batch_no.ToString() == batch_nos[i]) batch_index = i;
                string[] batch_tray_ids = c.csvToArray(c.getColumnBatchNo("Tray_ID_Arr", int.Parse(batch_nos[i]), batch_fiscal_years[i]));
                for (int j = 0; j < batch_tray_ids.Length; j++) tray_ids.Add(batch_tray_ids[j]);
            }
            dataGridView3.DataSource = this.dt3;
            this.dataGridView3.RowsDefaultCellStyle.BackColor = Color.White;
            if (batch_index >= 0)
            {
                DataGridViewRow r = (DataGridViewRow)dataGridView3.Rows[batch_index];
                r.DefaultCellStyle.BackColor = Color.LightGray;
                r.DefaultCellStyle.SelectionBackColor = Color.LightGray;
            }
            if (this.dataGridView3.SelectedRows.Count >= 0) this.dataGridView3.Rows[0].Selected = false;

            //dt4
            this.dt4.Clear();
            string[] carton_nos = c.csvToArray(voucher.Rows[0]["Carton_No_Production_Arr"].ToString());
            this.carton_fiscal_year = voucher.Rows[0]["Carton_Fiscal_Year"].ToString(); //set carton_fiscal_year
            int total_cones = 0;
            for (int i = 0; i < carton_nos.Length; i++)
            {
                DataRow carton = c.getProducedCartonRow(carton_nos[i], carton_fiscal_year);
                //float net_wt = c.getCartonProducedWeight(carton_nos[i], carton_fiscal_year);
                int cones = int.Parse(carton["Number_Of_Cones"].ToString());
                total_cones += cones;
                string tare_wt = (float.Parse(carton["Carton_Weight"].ToString()) + cones * float.Parse(carton["Cone_Weight"].ToString())).ToString("F3");
                dt4.Rows.Add(i + 1, carton_nos[i], carton["Number_Of_Cones"], float.Parse(carton["Gross_Weight"].ToString()).ToString("F3"), tare_wt, float.Parse(carton["Net_Weight"].ToString()).ToString("F3"), carton["Grade"]);
            }
            this.net_carton_wt = float.Parse(voucher.Rows[0]["Net_Carton_Weight"].ToString());
            dt4.Rows.Add("", "Total Cones", total_cones, "", "Net Weight", voucher.Rows[0]["Net_Carton_Weight"], "");

            dataGridView4.DataSource = this.dt4;
            dataGridView4.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);

            if (this.dataGridView4.SelectedRows.Count >= 0) this.dataGridView4.Rows[0].Selected = false;

            //fill the textboxes
            this.dyeingCompanyTB.Text = voucher.Rows[0]["Dyeing_Company_Name"].ToString();
            this.colourTB.Text = voucher.Rows[0]["Colour"].ToString();
            //calculate no of springs
            int no_of_springs = 0;
            for (int i = 0; i < tray_ids.Count; i++)
            {
                no_of_springs += c.getTraySprings(int.Parse(tray_ids[i]));
            }
            this.springTB.Text = no_of_springs.ToString();
            if (this.print_batch == true) this.dopTB.Text = c.getColumnBatchNo("Date_Of_Production", int.Parse(batch_nos[0]), batch_fiscal_years[0]).Substring(0, 10);
            else this.dopTB.Text = "Batches Not Closed";
            this.batchwtTB.Text = total_batch_wts.ToString("F3");
            this.cartonwtTB.Text = float.Parse(voucher.Rows[0]["Net_Carton_Weight"].ToString()).ToString("F3");
            if (this.print_batch == true) this.oilgainTB.Text = float.Parse(voucher.Rows[0]["Oil_Gain"].ToString()).ToString("F2");
            else this.oilgainTB.Text = "Batches Not Closed";
            this.qualityTB.Text = voucher.Rows[0]["Quality"].ToString();
        }
        private void search_batch()
        {
            int batch_no;
            string fiscal_year;
            try
            {
                batch_no = int.Parse(batchnoTextbox.Text);
            }
            catch
            {
                c.ErrorBox("Please Enter Numeric Batch Number", "Error");
                return;
            }
            fiscal_year = fiscalCombobox.Text;
            DataTable result = c.getBatchTable_BatchNoState(batch_no, 3, fiscal_year);
            if (result.Rows.Count == 0)
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
                this.dt2 = result;
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
                this.dataGridView2.Columns["Number_Of_Trays"].Width = 80;

                dataGridView2.Columns["Fiscal_Year"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                int printed = c.getPrint("Carton_Production_Voucher", "Voucher_ID=" + dataGridView2.Rows[0].Cells["Voucher_ID"].Value.ToString());
                if (printed>0)
                {
                    dataGridView2.Rows[0].DefaultCellStyle.BackColor = Global.printedColor;
                    dataGridView2.Rows[0].DefaultCellStyle.SelectionBackColor = Global.printedColor;
                    dataGridView2.Visible = false;
                    dataGridView2.Visible = true;
                }
                
            }

            this.dataGridView2.Rows[0].Selected = false;
            return;
        }
        private void load_color()
        {
            for(int i=0; i<dataGridView1.Rows.Count; i++)
            {
                string voucher = dataGridView1.Rows[i].Cells["Voucher_ID"].Value.ToString();
                int voucher_id = -1;
                if (voucher != "") voucher_id = int.Parse(voucher);
                if(voucher_id==this.printed_voucher_id)
                {
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Global.printedColor;
                    dataGridView1.Rows[i].DefaultCellStyle.SelectionBackColor = Global.printedColor;
                }
            }
            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                string voucher="";
                try { voucher = dataGridView2.Rows[i].Cells["Voucher_ID"].Value.ToString(); } catch { };
                int voucher_id = -1;
                if (voucher != "") voucher_id = int.Parse(voucher);
                if (voucher_id == this.printed_voucher_id)
                {
                    dataGridView2.Rows[i].DefaultCellStyle.BackColor = Global.printedColor;
                    dataGridView2.Rows[i].DefaultCellStyle.SelectionBackColor = Global.printedColor;
                }
            }
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            search_batch();
        }
        private void printButton_Click(object sender, EventArgs e)
        {
            if(this.print_batch==false)
            {
                c.ErrorBox("Batch is not closed yet", "Error");
                return;
            }
            if (dataGridView3.Rows.Count <= 0) return;
            this.printed_rows = 0;
            this.rows_to_print = dataGridView4.Rows.Count;
            this.printed_pages = 0;
            c.setPrint("Carton_Production_Voucher", "Voucher_ID=" + this.printed_voucher_id, 1);
            load_color();
            printPreviewDialog1.ShowDialog();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            load_batch();
        }
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //set margins and initial setup
            this.topmargin = (int)(0.03 * e.PageBounds.Height);
            this.lrmargin = (int)(0.05 * e.PageBounds.Width);
            Graphics g = e.Graphics;
            int page_width = (e.PageBounds.Width - 2 * lrmargin);
            int page_height = (e.PageBounds.Height - 2 * topmargin);
            this.printed_pages++;

            g.DrawRectangle(Pens.Black, lrmargin-5, topmargin-5, page_width+10, page_height+10);

            int write_height = topmargin;
            int basic_size = 8;
            int header_spacing = 3;
            //header
            write(e, (int)(0.90 * page_width) + lrmargin, write_height, (int)(0.10 * page_width), "Page No:" + this.printed_pages, basic_size, 'l', 1, 0);
            write_height += write(e, lrmargin, write_height, page_width, "||Shri||", basic_size, 'c', 0) + header_spacing;
            write_height += write(e, lrmargin, write_height, page_width, "BATCH FINAL REPORT", basic_size + 6, 'c', 0) + header_spacing-2;
            write_height += write(e, lrmargin, write_height, page_width, "KRISHANA SALES AND INDUSRIES", basic_size + 8, 'c', 0) + header_spacing;
            write_height += write(e, lrmargin, write_height, page_width, "550/1, Datta Galli, M. Vadgaon, Belagavi", basic_size + 2, 'c', 1) +header_spacing;
            write_height += write(e, lrmargin, write_height, page_width, "(GSTIN No. 29AIOPM5869K1Z8)", basic_size + 2, 'c', 1);

            string batch_nos = "";
            for (int i = 0; i < dataGridView3.Rows.Count - 1; i++) batch_nos += dataGridView3.Rows[i].Cells[1].Value + ", ";
            batch_nos += dataGridView3.Rows[dataGridView3.Rows.Count-1].Cells[1].Value;

            int gap = 0;
            write(e, (int)(0.00 * page_width)+lrmargin, write_height, (int)(0.15 * page_width), "Batch Number(s):", basic_size, 'l', 1, 0);
            write(e, (int)(0.15 * page_width) + lrmargin, write_height, (int)(0.60 * page_width), batch_nos, basic_size, 'l', 0, 1);
            write(e, (int)(0.75 * page_width) + lrmargin, write_height, (int)(0.08 * page_width), "Quality:", basic_size, 'r', 1, 0);
            write_height += write(e, (int)(0.83 * page_width)+lrmargin, write_height, (int)(0.17 * page_width), qualityTB.Text, basic_size, 'l', 0, 1) + gap;
            
            write(e, (int)(0.00 * page_width) + lrmargin, write_height, (int)(0.15 * page_width), "Dyeing Company:", basic_size, 'l', 1, 0);
            write(e, (int)(0.15 * page_width) + lrmargin, write_height, (int)(0.25 * page_width), dyeingCompanyTB.Text, basic_size, 'l', 0, 1);
            write(e, (int)(0.40 * page_width) + lrmargin, write_height, (int)(0.20 * page_width), "Colour:", basic_size, 'r', 1, 0);
            write(e, (int)(0.60 * page_width) + lrmargin, write_height, (int)(0.15 * page_width), colourTB.Text, basic_size, 'l', 0, 1);
            write(e, (int)(0.75 * page_width) + lrmargin, write_height, (int)(0.125 * page_width), "Springs:", basic_size, 'r', 1, 0);
            write_height+= write(e, (int)(0.875 * page_width) + lrmargin, write_height, (int)(0.125 * page_width), springTB.Text, basic_size, 'l', 0, 1)+gap;

            write(e, (int)(0.00 * page_width) + lrmargin, write_height, (int)(0.15 * page_width), "Date of Production:", basic_size, 'l', 1, 0);
            write(e, (int)(0.15 * page_width) + lrmargin, write_height, (int)(0.10 * page_width), dopTB.Text, basic_size, 'l', 0, 1);
            write(e, (int)(0.25 * page_width) + lrmargin, write_height, (int)(0.18 * page_width), "Combined Batch Wt:", basic_size, 'r', 1, 0);
            write(e, (int)(0.43 * page_width) + lrmargin, write_height, (int)(0.10 * page_width), batchwtTB.Text, basic_size, 'l', 0, 1);
            write(e, (int)(0.53 * page_width) + lrmargin, write_height, (int)(0.15 * page_width), "Weight Gain(kg):", basic_size, 'r', 1, 0);
            write(e, (int)(0.68 * page_width) + lrmargin, write_height, (int)(0.095 * page_width), (this.net_carton_wt- float.Parse(batchwtTB.Text)).ToString("F3") , basic_size, 'l', 0, 1);
            write(e, (int)(0.775 * page_width) + lrmargin, write_height, (int)(0.10 * page_width), "Oil Gain(%)", basic_size, 'r', 1, 0);
            write_height += write(e, (int)(0.875 * page_width) + lrmargin, write_height, (int)(0.125 * page_width), oilgainTB.Text, basic_size, 'l', 0, 1) + gap;
            write_height += 3;

            //rescale widths of datagridview
            int total_width = 0;
            for (int j = 0; j < dataGridView4.Columns.Count; j++) total_width += dataGridView4.Columns[j].Width;

            int new_total = page_width;
            float scale = (float)new_total / total_width;
            int[] column_widths = new int[dataGridView4.Columns.Count];
            int new_total_real = 0;
            for (int j = 0; j < dataGridView4.Columns.Count; j++)
            {
                column_widths[j] = (int)(scale * (float)dataGridView4.Columns[j].Width);
                new_total_real += column_widths[j];
            }
            column_widths[dataGridView4.Columns.Count - 1] += new_total - new_total_real;
            //header dgv
            write_height = drawDGVHeader(lrmargin, write_height, column_widths, e);
            write_height = drawDGVRow(lrmargin, write_height, column_widths, e, 0);

            while (this.rows_to_print - this.printed_rows > 0)
            {
                if (write_height + dataGridView4.Rows[0].Height >= topmargin + page_height - 15) break;
                if(rows_to_print-printed_rows==1) write_height = drawDGVRow(lrmargin, write_height, column_widths, e, this.printed_rows, 1);
                else write_height = drawDGVRow(lrmargin, write_height, column_widths, e, this.printed_rows);
                this.printed_rows++;
            }
            write(e, (int)(0.80 * page_width) + lrmargin, topmargin + page_height - 15, (int)(0.20 * page_width), "Signature", basic_size, 'c', 1, 0) ;

            if (this.rows_to_print - this.printed_rows > 0) e.HasMorePages = true;
            else e.HasMorePages = false;
        }
        private void printDocument1_BeginPrint(object sender, PrintEventArgs e)
        {
            this.printed_pages = 0;
            this.printed_rows = 0;
        }
        private void printDocument1_EndPrint(object sender, PrintEventArgs e)
        {
            this.printed_pages = 0;
            this.printed_rows = 0;
        }
        private void batchnoTextbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                search_batch();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
       
        //dgv
        private void dataGridView1_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count != 0) dataGridView2.SelectedRows[0].Selected = false;
        }
        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                load_batch();
                e.Handled = true;
            }
        }
        private void dataGridView2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                load_batch();
                e.Handled = true;
            }
        }
        private void dataGridView2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 0) dataGridView1.SelectedRows[0].Selected = false;
        }

        //print
        int drawDGVHeader(int x, int write_height, int[] column_widths, System.Drawing.Printing.PrintPageEventArgs e)
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

            int start_height = write_height;
            Console.WriteLine("write height dgv: " + write_height);
            int start_width = x;
            int draw_width = start_width;
            Font newFont = new Font(dataGridView4.Font.FontFamily, dataGridView4.Font.Size, FontStyle.Bold);
            for (int j = 0; j < dataGridView4.Columns.Count; j++)
            {
                str.Alignment = StringAlignment.Center;
                //draws the first cell of column
                g.FillRectangle(Brushes.LightGray, new Rectangle(draw_width, start_height, column_widths[j], dataGridView4.Rows[0].Height));
                g.DrawRectangle(Pens.Black, draw_width, start_height, column_widths[j], dataGridView4.Rows[0].Height);
                g.DrawString(dataGridView4.Columns[j].HeaderText, newFont, Brushes.Black, new RectangleF(draw_width, start_height, column_widths[j], dataGridView4.Rows[0].Height), str);
                if (j == 0) write_height += dataGridView4.Rows[0].Height;
                draw_width += column_widths[j];
            }
            #endregion
            return write_height;
        }


        int drawDGVRow(int x, int write_height, int[] column_widths, System.Drawing.Printing.PrintPageEventArgs e, int row_index, int bold = 0)
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

            int start_height = write_height;
            int width = x;
            Font newFont = new Font(dataGridView4.Font.FontFamily, dataGridView4.Font.Size, FontStyle.Regular);
            if (bold == 1)
            {
                newFont = new Font(dataGridView4.Font.FontFamily, dataGridView4.Font.Size, FontStyle.Bold);
            }
            for (int j = 0; j < dataGridView4.Columns.Count; j++)
            {
                str.Alignment = StringAlignment.Center;
                g.DrawRectangle(Pens.Black, width, start_height, column_widths[j], dataGridView4.Rows[0].Height);
                g.DrawString(dataGridView4.Rows[row_index].Cells[j].Value.ToString(), newFont, Brushes.Black, new RectangleF(width, start_height, column_widths[j], dataGridView4.Rows[0].Height), str);
                if (j == 0) write_height += dataGridView4.Rows[0].Height;
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
