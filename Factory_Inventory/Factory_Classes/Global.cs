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
    }
}
