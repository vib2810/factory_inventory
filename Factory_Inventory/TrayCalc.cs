using Factory_Inventory.Factory_Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Factory_Inventory
{
    public partial class TrayCalc : Form
    {
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F1)
            {
                this.dataGridView1.Focus();
                this.ActiveControl = dataGridView1;
                this.dataGridView1.CurrentCell = dataGridView1[0, 0];
                return false;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        public Structures.Tray_Params tray_Params=new Structures.Tray_Params();
        DbConnect c = new DbConnect();
       
        public TrayCalc(Structures.Tray_Params tray_Params_input)
        {
            InitializeComponent();
            dataGridView1.Rows.Add("Gray");
            dataGridView1.Rows.Add("Redyeing");
            dataGridView1.Rows.Add("Total");
            dataGridView1.Rows[1].DefaultCellStyle.BackColor = Color.Wheat;
            dataGridView1.Rows[1].DefaultCellStyle.SelectionBackColor= Color.Wheat;

            this.tray_Params = tray_Params_input;
            if (tray_Params.net_wt==-1F) //values are not set
            {

            }
            else
            {
                float r = 0F;
                Console.WriteLine("Ratio r=" + r.ToString());
                tray_Params.n_rd = (tray_Params.rd_percentage * tray_Params.net_wt) / 100F;
                tray_Params.g_rd = tray_Params.n_rd + r * tray_Params.tray_tare + tray_Params.s_rd * tray_Params.spring_wt;
                tray_Params.g_g = tray_Params.gross_wt - tray_Params.g_rd;
                tray_Params.s_g = tray_Params.no_of_springs - tray_Params.s_rd;
                tray_Params.n_g = tray_Params.net_wt - tray_Params.n_rd;
                this.load_values();
            }
        }
        private void TrayCalc_Load(object sender, EventArgs e)
        {
            var comboBoxes = this.Controls
              .OfType<ComboBox>()
              .Where(x => x.Name.EndsWith("CB"));

            foreach (var cmbBox in comboBoxes)
            {
                c.comboBoxEvent(cmbBox);
            }

            var textBoxes = this.Controls
                  .OfType<TextBox>()
                  .Where(x => x.Name.EndsWith("TB"));

            foreach (var txtBox in textBoxes)
            {
                c.textBoxEvent(txtBox);
            }

            var dtps = this.Controls
                  .OfType<DateTimePicker>()
                  .Where(x => x.Name.EndsWith("DTP"));

            foreach (var dtp in dtps)
            {
                c.DTPEvent(dtp);
            }

            var buttons = this.Controls
                  .OfType<Button>()
                  .Where(x => x.Name.EndsWith("Button"));

            foreach (var button in buttons)
            {
                Console.WriteLine(button.Name);
                c.buttonEvent(button);
            }

            this.netRedyeingWeightTB.Focus();
            this.ActiveControl = netRedyeingWeightTB;
        }

        //user fns
        public void load_values()
        {
            this.netRedyeingWeightTB.Text = tray_Params.n_rd.ToString("F3");
            this.rdSpringTB.Text = tray_Params.s_rd.ToString();
            this.graySpringTB.Text = tray_Params.s_g.ToString();
            this.trayTareTB.Text = tray_Params.tray_tare.ToString("F3");
            this.grayGrossWtTB.Text = tray_Params.g_g.ToString("F3");
            this.redyeingGrossWtTB.Text = tray_Params.g_rd.ToString("F3");
            dataGridView1.Rows[0].Cells[1].Value = tray_Params.g_g.ToString("F3");
            dataGridView1.Rows[0].Cells[2].Value = tray_Params.s_g.ToString();
            dataGridView1.Rows[0].Cells[3].Value = tray_Params.n_g.ToString("F3");

            dataGridView1.Rows[1].Cells[1].Value = tray_Params.g_rd.ToString("F3");
            dataGridView1.Rows[1].Cells[2].Value = tray_Params.s_rd.ToString();
            dataGridView1.Rows[1].Cells[3].Value = tray_Params.n_rd.ToString("F3");

            dataGridView1.Rows[2].Cells[1].Value = tray_Params.gross_wt.ToString("F3");
            dataGridView1.Rows[2].Cells[2].Value = tray_Params.no_of_springs.ToString();
            dataGridView1.Rows[2].Cells[3].Value = tray_Params.net_wt.ToString("F3");
        }
        
        //callbacks
        private void calcButton_Click(object sender, EventArgs e)
        {
            //checks
            try { float.Parse(this.netRedyeingWeightTB.Text); }
            catch { c.ErrorBox("Please Enter Numeric Net Redyeing Weight"); return; }
            try { int.Parse(this.rdSpringTB.Text); }
            catch { c.ErrorBox("Please Enter Numeric Redyeing Spring Nos"); return; }
            try { int.Parse(this.graySpringTB.Text); }
            catch { c.ErrorBox("Please Enter Numeric Gray Spring Nos"); return; }
            try { float.Parse(this.trayTareTB.Text); }
            catch { c.ErrorBox("Please Enter Numeric Tray Tare"); return; }
            try { float.Parse(this.grayGrossWtTB.Text); }
            catch { c.ErrorBox("Please Enter Numeric Gray Gross Weight"); return; }

            if((float.Parse(this.netRedyeingWeightTB.Text)==0F && float.Parse(this.rdSpringTB.Text) != 0F)||
                float.Parse(this.netRedyeingWeightTB.Text) != 0F && float.Parse(this.rdSpringTB.Text) == 0F)
            {
                //both should be zero together
                c.ErrorBox("Net Redyeing Weight and No of Redyeing springs have to be either zero or non zero together");
                return;
            }
            if(float.Parse(grayGrossWtTB.Text) == 0F || int.Parse(this.graySpringTB.Text) == 0 || float.Parse(trayTareTB.Text) == 0F)
            {
                if (!(float.Parse(grayGrossWtTB.Text) == 0F && int.Parse(this.graySpringTB.Text) == 0 && float.Parse(trayTareTB.Text) == 0F))
                {
                    //both should be zero together
                    c.ErrorBox("Gross Weight, Number of Springs and Tray Tare for can should be either all 0 or all non-zero");
                    return;
                }
            }
            //update direct params
            tray_Params.n_rd= float.Parse(this.netRedyeingWeightTB.Text);
            tray_Params.s_rd = int.Parse(this.rdSpringTB.Text);
            tray_Params.s_g= int.Parse(this.graySpringTB.Text);
            tray_Params.tray_tare = float.Parse(this.trayTareTB.Text);
            tray_Params.g_g = float.Parse(this.grayGrossWtTB.Text);

            //calculate
            tray_Params.no_of_springs = tray_Params.s_g + tray_Params.s_rd;
            float r = 0F;
            Console.WriteLine("Ratio r calc=" + r.ToString());

            tray_Params.g_rd = tray_Params.n_rd + r * tray_Params.tray_tare + (float)tray_Params.s_rd * tray_Params.spring_wt;
            tray_Params.gross_wt = tray_Params.g_rd + tray_Params.g_g;
            tray_Params.net_wt = tray_Params.gross_wt - tray_Params.tray_tare - tray_Params.no_of_springs * tray_Params.spring_wt;
            tray_Params.n_g = tray_Params.net_wt - tray_Params.n_rd;
            tray_Params.rd_percentage = (tray_Params.n_rd * 100F) / (tray_Params.net_wt);
            
            //load values
            this.load_values();
        }
        private void loadButton_Click(object sender, EventArgs e)
        {
            this.calcButton.PerformClick();
            this.Close();
        }
    }
}
