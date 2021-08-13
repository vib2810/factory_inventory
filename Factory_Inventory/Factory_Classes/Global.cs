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
        private static string _firmid = "";
        private static string _accessToken = "";
        private static string _constart = "";


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
        public static string firmid
        {
            get { return _firmid; }
            set { _firmid = value; }
        }
        public static string accessToken
        {
            get { return _accessToken; }
            set { _accessToken = value; }
        }
        public static string con_start
        {
            get { return _constart; }
            set { _constart = value; }
        }

        public static string getconnectionstring(string database)
        {
            Console.WriteLine("Input: " + database);
            int start = 0, end = 0;
            int count = 0;
            for (int i = 0; i < _connectionstring.Length; i++)
            {
                if (_connectionstring[i] == '=') start = i;
                if (_connectionstring[i] == ';') 
                { 
                    end = i;
                    count++;
                    if(count==2) break; 
                }
            }
            Console.WriteLine("se:" + start.ToString() + " " + end.ToString() + _connectionstring.Substring(start + 1, end - start - 1));
            return _connectionstring.Replace(_connectionstring.Substring(start+1, end-start-1), database);
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
        public static Tuple<string, string, string> getCartonNo_Weight_FiscalYear(string data)
        {
            string[] temp1 = data.Split('(');
            string str1 = temp1[0].Substring(0, temp1[0].Length - 2);
            string[] temp2 = temp1[1].Split(',');
            string str2 = temp2[0].Substring(0, temp2[0].Length);
            string str3 = temp2[1].Substring(1, temp2[1].Length - 2);
            Tuple<string, string, string> ans = new Tuple<string, string, string>(str1, str2, str3);
            return ans;
        }
    } 
}
