using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factory_Inventory.Factory_Classes
{
    public class MainConnect : DbConnect
    {
        public MainConnect(string con_start)
        {
            this.con = new SqlConnection(Global.getconnectionstring(con_start, "Main")); // making connection 
        }
    }
}
