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
            //Global.ipaddress = "192.168.1.35";
            DbConnect c = new DbConnect();
            //c.temp();
            //M_I1_FromToDate f = new M_I1_FromToDate();
            //1M_V_history f = new M_V_history(8);
            //M_V4_printCartonSlip f = new M_V4_printCartonSlip();
            //M_V2_dyeingInwardForm f = new M_V2_dyeingInwardForm("dyeingInward");
            //Application.Run(f);

            while (true)
            {
                Login f1 = new Login();
                Application.Run(f1);
                c = new DbConnect();
                if (f1.access == 1)
                {
                    c.recordLogin(f1.username);
                    Console.WriteLine(Global.ipaddress);
                    M_1_MainS ms = new M_1_MainS(c, f1.username, "Super User");
                    Application.Run(ms);
                    if (ms.logout == true)
                    {
                        c.recordLogout(f1.username);
                        ms.closeAllForms();
                        continue;
                    }
                }
            }

        }
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();
    }
}
