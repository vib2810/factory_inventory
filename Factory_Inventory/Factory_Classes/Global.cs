using Factory_Inventory.Factory_Classes;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Factory_Inventory
{
    static class Global
    {
        private static TwistERP _form;
        private static int _access = -1;
        private static Color _printedcolor=Color.GreenYellow;
        private static string _connectionstring = "";

        public static TwistERP background
        {
            get { return _form; }
            set { _form = value; }
        }
        public static int access
        {
            get { return _access; }
            set { _access = value; }
        }
        public static Color printedColor
        {
            get { return _printedcolor; }
            set { _printedcolor = value; }
        }
        public static string defaultconnectionstring
        {
            get { return _connectionstring; }
            set { _connectionstring = value; }
        }

        public static string getconnectionstring(string database)
        {
            return _connectionstring.Replace("FactoryData", database);
        }
        public static string getconnectionstring(string con_start, string database)
        {
            return con_start + "Initial Catalog=" + database + ";Persist Security Info=True;User ID=sa;Password=Kdvghr2810@;";
        }

        public static void ErrorBox(string message, string title = "Error")
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
        public static void SuccessBox(string message, string title = "Success")
        {
            SuccessBox s = new SuccessBox(message, title);
        }
        public static void WarningBox(string message, string title = "Warning")
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }


    }
}
