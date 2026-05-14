using Factory_Inventory.Factory_Classes;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Factory_Inventory.Main
{
    public class AutoBackupForm : Form
    {
        private MainConnect mc;
        private Label statusLabel;
        private ProgressBar progressBar;
        private bool isClosingAllowed = false;

        public AutoBackupForm(MainConnect m)
        {
            this.mc = m;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.statusLabel = new Label();
            this.progressBar = new ProgressBar();
            this.SuspendLayout();
            
            // statusLabel
            this.statusLabel.AutoSize = true;
            this.statusLabel.Font = new Font("Tahoma", 9.75F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            this.statusLabel.Location = new Point(20, 20);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new Size(185, 16);
            this.statusLabel.TabIndex = 0;
            this.statusLabel.Text = "Auto Backing Up. Please wait...";
            
            // progressBar
            this.progressBar.Location = new Point(20, 50);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new Size(360, 23);
            this.progressBar.TabIndex = 1;
            
            // AutoBackupForm
            this.ClientSize = new Size(400, 100);
            this.ControlBox = false;
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.statusLabel);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.Name = "AutoBackupForm";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Auto Backup";
            this.Shown += new EventHandler(this.AutoBackupForm_Shown);
            this.FormClosing += new FormClosingEventHandler(this.AutoBackupForm_FormClosing);
            
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private async void AutoBackupForm_Shown(object sender, EventArgs e)
        {
            await Task.Run(() => RunBackup());
            isClosingAllowed = true;
            this.Close();
        }

        private void RunBackup()
        {
            try
            {
                DataTable dt = mc.runQuery("SELECT * FROM Firms_List");
                int totalDatabases = (dt != null ? dt.Rows.Count : 0) + 1; // +1 for Main db
                int currentDb = 0;

                // Backup Main Database
                string mainPath = @"D:\Backups\TwistERP\Main\";
                try
                {
                    if (!Directory.Exists(mainPath)) Directory.CreateDirectory(mainPath);
                    string mainBackupLocation = mainPath + "Main.bak";
                    string mainConString = Global.getconnectionstring(Global.con_start, "Main"); // Ensure "Main" is the correct db name
                    
                    // Actually, mc.con.Database gives the current database name (e.g. FactoryData)
                    string mainDbName = mc.con.Database; 
                    
                    Tuple<Server, Backup> mainRet = CreateBackupObject(mainBackupLocation, mainDbName, mainConString);
                    if (mainRet != null)
                    {
                        mainRet.Item2.SqlBackup(mainRet.Item1);
                    }
                }
                catch (Exception ex)
                {
                    // Ignore main backup error to continue with firms
                    Console.WriteLine("Main backup error: " + ex.Message);
                }

                currentDb++;
                UpdateProgress(currentDb, totalDatabases);

                // Backup Firm Databases
                if (dt != null)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string firmID = dt.Rows[i]["Firm_ID"].ToString();
                        string dbName = "FactoryData_" + firmID;
                        string path = @"D:\Backups\TwistERP\" + dbName + @"\";
                        
                        try
                        {
                            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                            string backupLocation = path + dbName + ".bak";
                            string conString = Global.getconnectionstring(Global.con_start, dbName);
                            
                            Tuple<Server, Backup> ret = CreateBackupObject(backupLocation, dbName, conString);
                            if (ret != null)
                            {
                                ret.Item2.SqlBackup(ret.Item1);
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Firm {firmID} backup error: " + ex.Message);
                        }

                        currentDb++;
                        UpdateProgress(currentDb, totalDatabases);
                    }
                }
                
                // Save setting
                Properties.Settings.Default.LastBackupDate = DateTime.Now.Date.ToString("yyyy-MM-dd");
                Properties.Settings.Default.Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Auto Backup encountered an error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Tuple<Server, Backup> CreateBackupObject(string backupLocation, string databaseName, string connectionString)
        {
            try
            {
                Server dbServer = new Server(new ServerConnection(new SqlConnection(connectionString)));
                Backup dbBackup = new Backup() { Action = BackupActionType.Database, Database = databaseName };
                dbBackup.Devices.AddDevice(backupLocation, DeviceType.File);
                dbBackup.Initialize = true;
                return new Tuple<Server, Backup>(dbServer, dbBackup);
            }
            catch
            {
                return null;
            }
        }

        private void UpdateProgress(int current, int total)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => UpdateProgress(current, total)));
                return;
            }
            if (total > 0)
            {
                this.progressBar.Value = (current * 100) / total;
                this.statusLabel.Text = $"Backed up {current} of {total} databases...";
            }
        }

        private void AutoBackupForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isClosingAllowed)
            {
                e.Cancel = true;
            }
        }
    }
}
