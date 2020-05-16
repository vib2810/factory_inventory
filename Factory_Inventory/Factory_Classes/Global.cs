using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factory_Inventory
{
    static class Global
    {
        private static string _ipaddress = "";

        public static string ipaddress
        {
            get { return _ipaddress; }
            set { _ipaddress = value; }
        }
    }
}
