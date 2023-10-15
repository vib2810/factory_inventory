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

namespace Factory_Inventory.Main
{
    public partial class CR_P_CustomerSelect : Form
    {
        DbConnect c = new DbConnect();
        public CR_P_CustomerSelect()
        {
            InitializeComponent();
            HashSet<string> customers = new HashSet<string>();

            // Read Customer Names from the database and store them in the combobox
            DataTable dt = c.runQuery("SELECT Customers FROM Customers");
            for (int i = 0; i < dt.Rows.Count; i++) customers.Add(dt.Rows[i]["Customers"].ToString());

            dt = c.runQuery("SELECT Customer_Name, Customer_ID FROM T_M_Customers");
            for (int i = 0; i < dt.Rows.Count; i++) customers.Add(dt.Rows[i]["Customer_Name"].ToString());

            this.customerCB.DataSource = new BindingSource(customers, null);
            this.customerCB.DisplayMember = "Customers";
            this.customerCB.DropDownStyle = ComboBoxStyle.DropDown;//Create a drop-down list
            this.customerCB.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.customerCB.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        }
    }
}
