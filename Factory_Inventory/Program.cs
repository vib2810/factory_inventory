using Factory_Inventory.Factory_Classes;
using Factory_Inventory.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
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
            Properties.Settings.Default.LastIP="192.168.1.12";
            Properties.Settings.Default.Save();

            DbConnect c = new DbConnect();
            //float f=float.Parse("1151.000");
            //Console.WriteLine(f);
            //c.getInventoryCarton(new DateTime(2020, 05, 15), new DateTime(2020, 05, 17));
            //M_I1_OnDate f = new M_I1_OnDate();
            //M_VC_cartonSalesForm f = new M_VC_cartonSalesForm("Carton_Produced");
            
            //Global.background.Show();
            //M_2_newMain f = new M_2_newMain();
            //f.MdiParent = Global.background;
            //Global.background.show_form(f);
            //Application.Run(f);
            while (true)
            {
                Login f1 = new Login();
                Application.Run(f1);
                //f1.MdiParent = Global.background;
                //Global.background.show_form(f1);
                if (f1.access == 1 || f1.access == 2)
                {
                    c = new DbConnect();
                    c.recordLogin(f1.username);
                    Console.WriteLine(Properties.Settings.Default.LastIP);
                    M_1_MainS ms = new M_1_MainS(c, f1.username, f1.access);
                    Global.background = new TwistERP();
                    Global.background.IsMdiContainer = true;
                    Global.background.show_form(ms);
                    Application.Run(Global.background);
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
