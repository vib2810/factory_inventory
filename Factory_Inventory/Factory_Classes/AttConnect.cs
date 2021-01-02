using System;
using Factory_Inventory.Properties;
using Microsoft.SqlServer.Management.SqlParser.Parser;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
//using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using KeyEventArgs = System.Windows.Forms.KeyEventArgs;


namespace Factory_Inventory.Factory_Classes
{
    class AttConnect : DbConnect
    {
        public AttConnect()
        {
            string ip_address = Properties.Settings.Default.LastIP;
            //Connection string for Vob's laptop
            this.con = new SqlConnection(Global.getconnectionstring("FactoryAttendance")); // making connection  
        }

        public bool addEmployee(string name, int group_id, DateTime dtjoining_date, float salary)
        {
            string joining_date = dtjoining_date.Date.ToString("yyyy-MM-dd").Substring(0, 10);
            bool procedure = runProcedure_bool("AddEmployee", "@empname = '" + name + "', @groupID = " + group_id + ", @joiningDate = '" + joining_date + "', @salary = " + salary + "");
            return procedure;
        }



        }
    }
