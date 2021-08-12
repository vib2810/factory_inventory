using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Factory_Inventory.Factory_Classes;

namespace Factory_Inventory
{
    public partial class M_S_printUC : UserControl
    {
        DbConnect c = new DbConnect();
        List<string> col_names = new List<string>(); //column names of dgv1
        Dictionary<int, int> row_status = new Dictionary<int, int>(); //Print_Type_ID, Status(0=no change, 1=edited, 2=deleted)
        DataGridViewComboBoxColumn col;
        public M_S_printUC()
        {
            InitializeComponent();
            col= new DataGridViewComboBoxColumn();
            col.HeaderText = "Print Type ID";
            col.Name = "Default_Value";
            //dataGridView2.Columns.Add(col);
        }
        public void load_data()
        {
            //col.Items.Clear();
            dataGridView2.Rows.Clear();
            //Get and Set defaults
            DataTable dt = c.runQuery("SELECT * FROM Defaults WHERE Default_Type like '%print%' and default_name like '%default print type%'");
            //Add data to dropdown list
            //List<string> temp_dgv_col = new List<string>();
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    temp_dgv_col.Add(dt.Rows[i]["Default_Value"].ToString());
            //}
            //temp_dgv_col = temp_dgv_col.Distinct().ToList();
            //for(int i=0; i<temp_dgv_col.Count; i++)
            //{
            //    col.Items.Add(temp_dgv_col[i]);
            //}
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dataGridView2.Rows.Add(dt.Rows[i]["Default_Type"].ToString(), dt.Rows[i]["Default_Value"].ToString());
            }

            //Set values in dgv1
            DataTable dt2 = c.runQuery("SELECT * FROM Print_Types");
            dataGridView1DGV.Columns.Clear();
            dataGridView1DGV.Rows.Clear();
            row_status.Clear(); //clear row status dictionary
            //Add columns
            for (int i = 0; i < dt2.Columns.Count; i++)
            {
                dataGridView1DGV.Columns.Add(dt2.Columns[i].ColumnName, dt2.Columns[i].ColumnName.Replace('_', ' '));
            }
            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                dataGridView1DGV.Rows.Add(dt2.Rows[i].ItemArray);
                row_status[int.Parse(dt2.Rows[i]["Print_Type_ID"].ToString())] = 0;
            }
            c.auto_adjust_dgv(dataGridView1DGV);
            c.set_dgv_column_sort_state(dataGridView1DGV, DataGridViewColumnSortMode.NotSortable);
            //save column names in a list
            for (int i = 0; i < dataGridView1DGV.Columns.Count; i++)
            {
                col_names.Add(dataGridView1DGV.Columns[i].Name);
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            string sql = "begin transaction; begin try;\n";
            //catch
            //Update DGV
            for (int i = 0; i < dataGridView1DGV.Rows.Count; i++)
            {
                if(dataGridView1DGV.Rows[i].Cells["Print_Type_ID"].Value.ToString() == "-1")
                {
                    sql += "insert into Print_Types values (";
                    for(int j=0; j<dataGridView1DGV.Columns.Count-1; j++)
                    {
                        sql += "'" + dataGridView1DGV.Rows[i].Cells[j].Value.ToString() + "'";
                        if (j < dataGridView1DGV.Columns.Count - 2) sql += ",";
                    }
                    sql += ");\n";
                }
                else
                {
                    int value = -1, key = int.Parse(dataGridView1DGV.Rows[i].Cells["Print_Type_ID"].Value.ToString());
                    bool present = row_status.TryGetValue(key, out value);
                    Console.WriteLine("at " + i.ToString() + " value=" + value.ToString());
                    if (present == true) //add to sql query
                    {
                        if(value==1) //edit
                        {
                            sql += "update Print_Types set ";
                            for(int j=0; j<dataGridView1DGV.Columns.Count-1; j++)
                            {
                                sql += " " + col_names[j] + "= '" + dataGridView1DGV.Rows[i].Cells[j].Value.ToString() + "'";
                                if (j < dataGridView1DGV.Columns.Count - 2) sql += ",";
                            }
                            sql += " where Print_Type_ID=" + dataGridView1DGV.Rows[i].Cells["Print_Type_ID"].Value.ToString() + " ;\n";
                        }
                        else if(value==2)
                        {
                            sql += "delete from Print_Types where Print_Type_ID=" + dataGridView1DGV.Rows[i].Cells["Print_Type_ID"].Value.ToString() + " ;\n";
                        }
                    }
                }
            }

            //Update Defaults
            for(int i=0; i<dataGridView2.Rows.Count; i++)
            {
                sql += "update Defaults set Default_Value='" + dataGridView2.Rows[i].Cells["Default_Value"].Value.ToString() + "' where ";
                sql += "default_type='" + dataGridView2.Rows[i].Cells["Default_Type"].Value.ToString() + "' and default_name like '%default print type%';\n";
            }
            sql += "commit transaction; end try BEGIN CATCH rollback transaction; ";
            sql += "DECLARE @ErrorMessage NVARCHAR(4000); DECLARE @ErrorSeverity INT; DECLARE @ErrorState INT; SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE(); ";
            sql += "RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState); END CATCH; ";
            DataTable d = c.runQuery(sql);
            if(d!=null)
            {
                c.SuccessBox("Print Setting Saved");
                this.load_data();
            }
        }
        private void addButton_Click(object sender, EventArgs e)
        {
            setPrint_Type f = new setPrint_Type(null);
            f.ShowDialog();
            List<string> result = f.result;
            if (result == null) return;
            result.Add("-1");
            dataGridView1DGV.Rows.Add(result.ToArray());
        }
        private void editButton_Click(object sender, EventArgs e)
        {
            if (dataGridView1DGV.SelectedRows.Count < 1) return;
            List<string> input = new List<string>();
            for (int i = 0; i < dataGridView1DGV.Columns.Count-1; i++)
            {
                input.Add(dataGridView1DGV.SelectedRows[0].Cells[i].Value.ToString());
            }
            setPrint_Type f = new setPrint_Type(input);
            f.ShowDialog();
            List<string> result = f.result;
            if (result == null) return;
            for (int i = 0; i < result.Count; i++)
            {
                dataGridView1DGV.SelectedRows[0].Cells[i].Value = result[i];
            }
            int value=-1, key= int.Parse(dataGridView1DGV.SelectedRows[0].Cells["Print_Type_ID"].Value.ToString());
            bool present= row_status.TryGetValue(key, out value);
            if(present==true) //update dictionary only if present initially 
            {
                row_status[key] = 1;
            }
        }
        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (dataGridView1DGV.SelectedRows.Count < 1) return;
            int value = -1, key = int.Parse(dataGridView1DGV.SelectedRows[0].Cells["Print_Type_ID"].Value.ToString());
            bool present = row_status.TryGetValue(key, out value);
            if (present == true) //update dictionary only if present initially 
            {
                row_status[key] = 2;
                Console.WriteLine("Setting " + key.ToString() + "-> 2");
                dataGridView1DGV.Rows[dataGridView1DGV.SelectedRows[0].Index].Visible = false;
            }
            else { dataGridView1DGV.Rows.Remove(dataGridView1DGV.SelectedRows[0]); } //delete completely if not present earlier
        }

        //dgv
        private void dataGridView2_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is DataGridViewComboBoxEditingControl)
            {
                ((ComboBox)e.Control).DropDownStyle = ComboBoxStyle.DropDown;
                ((ComboBox)e.Control).AutoCompleteSource = AutoCompleteSource.ListItems;
                ((ComboBox)e.Control).AutoCompleteMode = AutoCompleteMode.Append;
            }
        }
        private void dataGridView2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && (dataGridView1DGV.SelectedCells.Cast<DataGridViewCell>().Any(x => x.ColumnIndex == 1)))
            {
                Console.WriteLine("here");
                dataGridView1DGV.BeginEdit(true);
                ComboBox c = (ComboBox)dataGridView1DGV.EditingControl;
                if (c != null)
                {
                    if (c != null) c.DroppedDown = true;
                    SendKeys.Send("{down}");
                    SendKeys.Send("{up}");
                }
                e.Handled = true;
            }
        }
    }
}
