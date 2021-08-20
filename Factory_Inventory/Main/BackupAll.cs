using Factory_Inventory.Factory_Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Factory_Inventory.Main;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;
using System.IO;

namespace Factory_Inventory.Main
{
    public partial class BackupAll : Form
    {
        MainConnect mc;
        DataTable dt = new DataTable();
        public BackupAll(MainConnect m)
        {
            InitializeComponent();
            mc = m;
            this.dt = mc.runQuery("SELECT * FROM Firms_List");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dataGridView1.Rows.Add(dt.Rows[i]["Firm_ID"].ToString(), false, dt.Rows[i]["Firm_Name"].ToString());
            }
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 9.75F, FontStyle.Bold);
            this.statusLabel.Text = "Backups stored for:\n";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.statusLabel.Text = "Backups stored for:\n";
            O_BackupRestoreForm f = new O_BackupRestoreForm();
            
            //Get count of total valid backup firms
            int count = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dataGridView1.Rows[i].Cells["Select"].Value) == true) count++;
            }

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if(Convert.ToBoolean(dataGridView1.Rows[i].Cells["Select"].Value)==true)
                {
                    string firmID = dataGridView1.Rows[i].Cells["Firm_ID"].Value.ToString();
                    string con_string = Global.getconnectionstring(Global.con_start, "FactoryData_" + firmID);
                    DbConnect c = new DbConnect(con_string);
                    Console.WriteLine(con_string);
                    DataTable d = c.runQuery("SELECT Default_Value FROM Defaults WHERE Default_Name = 'Backup Location'");
                    string path = @d.Rows[0][0].ToString() + @"\FactoryData_" + firmID + @"\";
                    try
                    {
                        if (!Directory.Exists(path))
                        {
                            DirectoryInfo di = Directory.CreateDirectory(path);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Backup failed, Couldnt create directory\n" + ex.ToString());
                        return;
                    }
                    string backup_location = backup_location = path + "FactoryData_" + firmID + ".bak";
                    Tuple<Server, Backup> ret = f.backup(backup_location, "FactoryData", firmID);
                    try
                    {
                        ret.Item2.PercentComplete += DbBackup_PercentComplete;
                        ret.Item2.Complete += DbBackup_Complete;
                        ret.Item2.SqlBackupAsync(ret.Item1);
                        this.statusLabel.Text += "FactoryData_" + firmID + ".bak" + "  ";
                        progressBar1.Value = 100*(i+1)/(count);
                        progressBar1.Update();
                    }
                    catch (Exception ex)
                    {
                        c.ErrorBox("Couldnt Finish Backup\n" + ex.Message);
                    }
                }
            }
        }

        private void DbBackup_PercentComplete(object sender, PercentCompleteEventArgs e)
        {
            progressBar2.Invoke((MethodInvoker)delegate
            {
                progressBar2.Value = e.Percent;
                progressBar2.Update();
            });
            backupStatusLabel.Invoke((MethodInvoker)delegate
            {
                backupStatusLabel.Text = $"{e.Percent}%";
            });
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
