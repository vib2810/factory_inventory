using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factory_Inventory.Factory_Classes
{
    class MainConnect : DbConnect
    {
        public MainConnect(string con_start)
        {
            this.con = new SqlConnection(con_start + "Initial Catalog=Main;Persist Security Info=True;User ID=sa;Password=Kdvghr2810@;"); // making connection 
        }


    }
}
