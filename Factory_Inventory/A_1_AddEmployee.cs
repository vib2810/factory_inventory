using Factory_Inventory.Factory_Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Factory_Inventory
{
    public partial class A_1_AddEmployee : Form
    {
        private AttConnect a = new AttConnect();
        private bool edit_mode = false;
        private DataTable dt;
        private A_1_EmployeesUC f;
        public A_1_AddEmployee(A_1_EmployeesUC frm)
        {
            this.f = frm;
            InitializeComponent();
            this.dataGridView1.Rows.Add();
            this.dataGridView1.Rows[0].Cells[0].Value = this.joiningdateDTP.Value.Date.ToString().Substring(0, 10);
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            dt = a.getTableData("Group_Names", "*", null);
            List<string> datasource = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                datasource.Add(dt.Rows[i][1].ToString());
            }
            this.groupCB.DataSource = datasource;
            this.groupCB.DisplayMember = "Name";
            this.groupCB.DropDownStyle = ComboBoxStyle.DropDownList;

            this.dataGridView1.Columns[1].ReadOnly = false;
        }

        private void Submit_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(nameTB.Text))
            {
                a.ErrorBox("Please enter name");
                return;
            }
            if(this.dataGridView1.Rows[0].Cells[1].Value == null)
            {
                a.ErrorBox("Please enter the salary");
                return;
            }
            float salary;
            try
            {
                salary = float.Parse(this.dataGridView1.Rows[0].Cells[1].Value.ToString());
            }
            catch
            {
                a.ErrorBox("Please enter numeric salary");
                return;
            }
            if(this.edit_mode == false)
            {
                bool added = a.addEmployee(this.nameTB.Text, int.Parse(dt.Rows[this.groupCB.SelectedIndex][0].ToString()), this.joiningdateDTP.Value, salary);
                if(added == true)
                {
                    f.loadDatabase();
                    this.Close();
                }
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            this.dataGridView1.Rows[this.dataGridView1.Rows.Count - 1].Cells[0].Value = this.joiningdateDTP.Value.Date.ToString().Substring(0, 10);
        }
    }
}
