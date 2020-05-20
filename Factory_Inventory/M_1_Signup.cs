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
    public partial class M_1_Signup : Form
    {
        public DbConnect c;
        public M_1_Signup(DbConnect input)
        {
            this.c = input;
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            //Create a drop-down list
            var dataSource = new List<string>();
            string s1 = "Super User";
            string s2 = "Normal User";
            string s3 = "";
            dataSource.Add(s3);
            dataSource.Add(s1);
            dataSource.Add(s2);
            //Setup data binding
            this.comboBox1.DataSource = dataSource;
            this.comboBox1.DisplayMember = "Name";

            // make it readonly
            this.comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
        }
       
        
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //int i = this.comboBox1.SelectedIndex;
            //Console.WriteLine(i);

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void Submit_Click(object sender, EventArgs e)
        {
            if(textBox2.Text == textBox3.Text && textBox2.Text!="")
            {
                string username = textBox1.Text;
                string password = textBox2.Text;
                int AccessLevel = this.comboBox1.SelectedIndex;
                if(this.comboBox1.SelectedIndex==0)
                {
                    c.ErrorBox("Select Access Level", "Error");
                    return;
                }
                int ans=c.addUser(username, password, AccessLevel);
                if(ans==1)
                {
                    c.SuccessBox("User added successfully");
                    this.Close();
                }
            }
            else
            {
                c.ErrorBox("Passwords Do Not Match/Password Empty", "Error");
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
