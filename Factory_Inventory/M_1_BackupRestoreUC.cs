using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;
using MyCoolCompany.Shuriken;
using Factory_Inventory.Factory_Classes;

namespace Factory_Inventory
{
    public partial class M_1_BackupRestoreUC : UserControl
    {
        public string database="FactoryData";
        public bool wait = false;
        public string backupname;
        public DbConnect c;
        public M_1_BackupRestoreUC()
        {
            InitializeComponent();
            c = new DbConnect(); 
        }

        private void DbBackup_Complete(object sender, ServerMessageEventArgs e)
        {
            if (e.Error != null)
            {
                backupStatusLabel.Invoke((MethodInvoker)delegate
                {
                    backupStatusLabel.Text = e.Error.Message;
                });
            }
        }

        private void DbBackup_PercentComplete(object sender, PercentCompleteEventArgs e)
        {
            progressBar1.Invoke((MethodInvoker)delegate
            {
                progressBar1.Value = e.Percent;
                progressBar1.Update();
            });
            backupStatusLabel.Invoke((MethodInvoker)delegate
            {
                backupStatusLabel.Text = $"{e.Percent}%";
            });
        }

        private void backupButton_Click_1(object sender, EventArgs e)
        {
            if(this.backupLoactionTB.Text==string.Empty)
            {
                c.ErrorBox("Please select backup loaction", "Error");
                return;
            }
            progressBar1.Value = 0;
            try
            {
                Server dbServer = new Server(new ServerConnection("192.168.1.12, 1433", "sa", "Kdvghr2810@"));
                Backup dbBackup = new Backup() { Action = BackupActionType.Database, Database = this.database};
                dbBackup.Devices.AddDevice(this.backupLoactionTB.Text + this.database + "(" + DateTime.Now.ToString().Replace(":", "-") + ")" + ".bak", DeviceType.File);
                dbBackup.Initialize = true;
                dbBackup.PercentComplete += DbBackup_PercentComplete;
                dbBackup.Complete += DbBackup_Complete;
                dbBackup.SqlBackupAsync(dbServer);
                this.backupLoactionLabel.Text += "Backup stored as: "+this.database + "(" + DateTime.Now.ToString().Replace(":", "-") + ")" + ".bak";
            }
            catch (Exception ex)
            {
                c.ErrorBox(ex.Message, "Error");

            }
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            //FolderBrowserDialog folderDlg = new FolderBrowserDialog();
            //folderDlg.ShowNewFolderButton = true; 
            //DialogResult result = folderDlg.ShowDialog();
            //if (result == DialogResult.OK)
            //{
            //    this.backupLoactionTB.Text = folderDlg.SelectedPath+ @"\FactoryData.bak";
            //    Environment.SpecialFolder root = folderDlg.RootFolder;
            //}
            var dialog = new FolderSelectDialog
            {
                InitialDirectory = @"D:\",
                Title = "Select Backup folder"
            };
            if (dialog.Show(Handle))
            {
                this.backupLoactionTB.Text = dialog.FileName + @"\";
            }
        }

        private void browseRestoreButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = @"D:\",
                Title = "Browse Database Files",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "bak",
                Filter = "bak files (*.bak)|*.bak",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.restoreLocationTB.Text = openFileDialog1.FileName;
            }
        }

        private void restoreButton_Click(object sender, EventArgs e)
        {
            if (this.restoreLocationTB.Text == string.Empty)
            {
                c.ErrorBox("Please select restore file", "Error");
                return;
            }
            try
            {
                Server dbServer = new Server(new ServerConnection("192.168.1.12, 1433", "sa", "Kdvghr2810@"));
                Backup dbBackup = new Backup() { Action = BackupActionType.Database, Database = this.database };
                string s = this.restoreLocationTB.Text;
                dbBackup.Devices.AddDevice(s.Substring(0, s.Length - 4) + "(" + DateTime.Now.ToString().Replace(":", "-") + ")" + "restorebackup.bak", DeviceType.File); ;
                dbBackup.Initialize = true;
                dbBackup.PercentComplete += DbRestore_PercentComplete;
                dbBackup.Complete += DbRestore_Complete;
                dbBackup.SqlBackup(dbServer);
            }
            catch (Exception ex)
            {
                c.ErrorBox(ex.Message, "Error");

            }
            progressBar2.Value = 0;
            try
            {

                Server dbServer = new Server(new ServerConnection("192.168.1.12, 1433", "sa", "Kdvghr2810@"));
                Database db = dbServer.Databases[this.database];
                dbServer.KillAllProcesses(db.Name);
                db.DatabaseOptions.UserAccess = DatabaseUserAccess.Multiple;
                db.Alter(TerminationClause.RollbackTransactionsImmediately);
                Restore dbRestore = new Restore() { Database = this.database, Action = RestoreActionType.Database, ReplaceDatabase = true, NoRecovery = false };
                dbRestore.Devices.AddDevice(this.restoreLocationTB.Text, DeviceType.File);
                dbRestore.PercentComplete += DbRestore_PercentComplete;
                dbRestore.Complete += DbRestore_Complete;
                dbRestore.SqlRestoreAsync(dbServer);

            }
            catch (Exception ex)
            {
                c.ErrorBox(ex.Message, "Error");

            }
        }

        private void DbRestore_Complete(object sender, ServerMessageEventArgs e)
        {
            if(e.Error!=null)
            {
                this.restoreStatusLabel.Invoke((MethodInvoker)delegate
                {
                    this.restoreStatusLabel.Text = e.Error.Message;
                });
            }
        }

        private void DbRestore_PercentComplete(object sender, PercentCompleteEventArgs e)
        {
            progressBar2.Invoke((MethodInvoker)delegate
            {
                progressBar2.Value = e.Percent;
                progressBar2.Update();
            });
            restoreStatusLabel.Invoke((MethodInvoker)delegate
            {
                restoreStatusLabel.Text = $"{e.Percent}%";
            });
        }
    }
}
