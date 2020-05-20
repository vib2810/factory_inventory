using Factory_Inventory.Factory_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Factory_Inventory
{
    static class Program
    {

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (Environment.OSVersion.Version.Major == 6)
                SetProcessDPIAware();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Global.ipaddress = "192.168.1.12";
            DbConnect c = new DbConnect();
            M_V2_dyeingInwardForm f = new M_V2_dyeingInwardForm("dyeingInward");
            //Application.Run(f);

            while (true)
            {
                Login f1 = new Login();
                Application.Run(f1);
                c = new DbConnect();
                c.recordLogin(f1.username);
                Console.WriteLine(Global.ipaddress);
                M_1_MainS ms = new M_1_MainS(c, f1.username, f1.access);
                Application.Run(ms);
                if (ms.logout == true)
                {
                    c.recordLogout(f1.username);
                    ms.closeAllForms();
                    continue;
                }
            }

        }
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();
    }
}
