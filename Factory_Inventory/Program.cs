using Factory_Inventory.Factory_Classes;
using System;
using System.Collections.Generic;
using System.Data;
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
            //float f=float.Parse("1151.000");
            //Console.WriteLine(f);
            //c.getInventoryCarton(new DateTime(2020, 05, 15), new DateTime(2020, 05, 17));
            M_I1_OnDate f = new M_I1_OnDate();
            Application.Run(f);
            while (true)
            {
                Login f1 = new Login();
                Application.Run(f1);
                if (f1.access == 1 || f1.access == 2)
                {
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
        }
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();
    }
}
