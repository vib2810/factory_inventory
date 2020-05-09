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
            DbConnect c = new DbConnect();
            //c.temp();
            Display_Carton_Produced f = new Display_Carton_Produced(c.getProducedCartonRow("16","2020-2021"));
            //M_V1_cartonInwardForm f = new M_V1_cartonInwardForm();
            //M_V_history f = new M_V_history(8);
            //M_V4_printCartonSlip f = new M_V4_printCartonSlip();
            Application.Run(f);
            while (true)
            {
                Login f1 = new Login(c);
                Application.Run(f1);
                if (f1.access == 1)
                {
                    c.recordLogin(f1.username);
                    Console.WriteLine(f1.username);
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
