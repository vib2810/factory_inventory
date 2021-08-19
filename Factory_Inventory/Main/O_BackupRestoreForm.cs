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
using System.Data.SqlClient;
using System.IO;

namespace Factory_Inventory
{
    public partial class M_BackupRestore : Form
    {
        public bool wait = false;
        public string backupname;
        public DbConnect c;
        public M_BackupRestore()
        {
            InitializeComponent();
            c = new DbConnect(); 
            this.backupLoactionTB.Text = @"D:\Backups\";
            if(Global.access==2)
            {
                this.restoreButton.Enabled = false;
                this.browseRestoreButton.Enabled = false;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            ControlPaint.DrawBorder(e.Graphics, ClientRectangle,
                                         Color.Black, 10, ButtonBorderStyle.Inset,
                                         Color.Black, 10, ButtonBorderStyle.Inset,
                                         Color.Black, 10, ButtonBorderStyle.Inset,
                                         Color.Black, 10, ButtonBorderStyle.Inset);
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.restoreLocationTB.Text = "";
        }
        private void backup(string path, string database)
        {
            try
            { 
                Console.WriteLine(Global.getconnectionstring(database));
                Server dbServer = new Server(new ServerConnection(new SqlConnection(Global.getconnectionstring(database + "_" + Global.firmid))));
                Backup dbBackup = new Backup() { Action = BackupActionType.Database, Database = database + "_" + Global.firmid };
                string backup_location = path + @"\" + this.fileNameTB.Text + database + "_" + Global.firmid + "(" + DateTime.Now.ToString().Replace(":", "-").Replace('/', '-') + ")" + ".bak";
                dbBackup.Devices.AddDevice(backup_location, DeviceType.File);
                dbBackup.Initialize = true;
                dbBackup.PercentComplete += DbBackup_PercentComplete;
                dbBackup.Complete += DbBackup_Complete;
                dbBackup.SqlBackupAsync(dbServer);
                this.backupLoactionLabel.Text += "Backup stored as: " + backup_location + "\n";
            }
            catch (Exception ex)
            {
                c.ErrorBox(ex.Message, "Error");
            }
        }
        
        //Button Click
        private void backupButton_Click_1(object sender, EventArgs e)
        {
            this.backupLoactionLabel.Text = "";
            if (this.backupLoactionTB.Text==string.Empty)
            {
                c.ErrorBox("Please select backup loaction", "Error");
                return;
            }
            string path = this.backupLoactionTB.Text + DateTime.Now.Date.ToString().Substring(0,10).Replace(":", "-").Replace('/', '-');
            Console.WriteLine(path);
            progressBar1.Value = 0;
            try
            {
                if (!Directory.Exists(path))
                {
                    DirectoryInfo di = Directory.CreateDirectory(path);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Backup failed\n" + ex.ToString());
                return;
            }
            if (checkedListBox1.CheckedIndices.Contains(0))
            {
                this.backup(path, "FactoryData");
            }
            if (checkedListBox1.CheckedIndices.Contains(1))
            {
                this.backup(path, "FactoryAttendance");
            }
        }
        private void browseButton_Click(object sender, EventArgs e)
        {
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
            string database = "FactoryData_" + Global.firmid;
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = @"D:\",
                Title = "Browse Database Files",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "bak",
                Filter = "bak files ( *"+database+ "*.bak) | *" + database + "*.bak",
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
            string database = "FactoryData_" + Global.firmid;
            try
            {
                Server dbServer = new Server(new ServerConnection(new SqlConnection(Global.getconnectionstring(database))));
                Backup dbBackup = new Backup() { Action = BackupActionType.Database, Database = database };
                string s = this.restoreLocationTB.Text;
                string backup_location = s.Substring(0, s.Length - 4) + "(" + DateTime.Now.ToString().Replace(":", "-").Replace('/', '-') + ")" + "restorebackup.bak";
                Console.WriteLine(backup_location);
                dbBackup.Devices.AddDevice(backup_location, DeviceType.File);
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
                Server dbServer = new Server(new ServerConnection(new SqlConnection(Global.getconnectionstring(database))));
                Database db = dbServer.Databases[database];
                dbServer.KillAllProcesses(db.Name);
                db.DatabaseOptions.UserAccess = DatabaseUserAccess.Multiple;
                db.Alter(TerminationClause.RollbackTransactionsImmediately);
                Restore dbRestore = new Restore() { Database = database, Action = RestoreActionType.Database, ReplaceDatabase = true, NoRecovery = false };
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

        //complete bars
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
        private void DbRestore_Complete(object sender, ServerMessageEventArgs e)
        {
            if (e.Error != null)
            {
                this.restoreStatusLabel.Invoke((MethodInvoker)delegate
                {
                    this.restoreStatusLabel.Text = e.Error.Message;
                });
            }
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
    }
}
