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
            DateTime d = new DateTime(2015, 3, 31);
            Console.WriteLine(c.getFinancialYear(d));
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
