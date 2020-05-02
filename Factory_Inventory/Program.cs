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
            string s = "123456789  (2019-2020)";
            string[] str = c.repeated_batch_csv(s);
            for(int i=0;i<str.Length;i++)
            {
                Console.WriteLine(str[i]);
            }
            Console.WriteLine(str.Length);
            M_V1_cartonInwardForm f = new M_V1_cartonInwardForm();
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
