﻿using Factory_Inventory.Factory_Classes;
using Factory_Inventory.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Drawing;
using System.Collections.Specialized;

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

            // Get the application configuration file.
            System.Configuration.Configuration config =ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            ConfigurationManager.RefreshSection("appSettings");

            // Get the AppSettings section.
            AppSettingsSection appSettingSection =(AppSettingsSection)config.GetSection("appSettings");
            if (appSettingSection.Settings["sql_update"].Value == "1")
            {
                bool updated = c.sql_update_query();
                if(updated==true) c.SuccessBox("Updated SQL");
                Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                //configuration.AppSettings.Settings.Remove("sql_update");
                configuration.AppSettings.Settings["sql_update"].Value = "lala";
                configuration.Save(ConfigurationSaveMode.Full, true);
                ConfigurationManager.RefreshSection("appSettings");
            }
            //M_V4_printCartonSlip f = new M_V4_printCartonSlip();
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
                    ms.MdiParent = Global.background;
                    ms.Scale(new SizeF(1.3F, 1.3F));
                    ms.AutoScaleMode = AutoScaleMode.Font;
                    ms.StartPosition = FormStartPosition.CenterScreen;
                    ms.Show();
                    //Global.background.MdiChildren[0].StartPosition= 
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
