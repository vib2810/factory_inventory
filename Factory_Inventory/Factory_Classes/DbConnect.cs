﻿using Microsoft.SqlServer.Management.SqlParser.Parser;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
//using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using KeyEventArgs = System.Windows.Forms.KeyEventArgs;

namespace Factory_Inventory.Factory_Classes
{
    public class DbConnect
    {
        //Connect to DB
        //static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;
        public SqlConnection con;
        public DateTime loginTime;
        private string useractive;
        public struct pair
        {
            public string batch_no, fiscal_year;
        };
        public DbConnect()
        {
            //Connection string for Gaurang's Laptop
            //this.con = new SqlConnection(@"Data Source=DESKTOP-MOUBPNG\MSSQLSERVER2019;Initial Catalog=FactoryData;Persist Security Info=True;User ID=sa;Password=Kdvghr2810@;"); // making connection   

            //Connection string for old Database
            //this.con = new SqlConnection(@"Data Source=DESKTOP-MOUBPNG\MSSQLSERVER2019;Initial Catalog=FactoryInventory;Persist Security Info=True;User ID=sa;Password=Kdvghr2810@;"); // making connection   
            string ip_address = Properties.Settings.Default.LastIP;
            //Connection string for Vob's laptop
            this.con = new SqlConnection(@"Data Source="+ip_address+", 1433;Initial Catalog=FactoryData;Persist Security Info=True;User ID=sa;Password=Kdvghr2810@;        "); // making connection   
        }

        public string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }
        
        //Utility Functions
        public bool isHistoryFormOpen(int vno)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(M_V_history))
                {
                    var frm = (M_V_history)form;
                    if (frm.vno == vno)
                    {
                        form.WindowState = FormWindowState.Normal;
                        form.Activate();
                        return true;
                    }

                }
            }
            return false;
        }
        public string removecom(string input)
        {
            if (input.Length <= 0) return "";
            return input.Substring(0, input.Length - 1);
        }
        public string[] csvToArray(string str)
        {
            string[] ans = str.Split(',');
            ans = ans.Take(ans.Length - 1).ToArray();
            return ans;
        }
        public string[] repeated_batch_csv(string str)
        {
            string[] ans = str.Split('(');
            //ans = ans.Skip(1).ToArray();
            ans[0] = ans[0].Substring(0, ans[0].Length - 2);
            ans[1] = ans[1].Substring(0, ans[1].Length - 1);
            return ans;
        }
        public bool check_if_batch_repeated(string batch)
        {
            if(batch[batch.Length-1] == ')')
            {
                return true;
            }
            return false;
        }
        public bool isPresentInColour(string colour, string quality)
        {
            try
            {
                con.Open();
                DataTable dt2 = new DataTable();
                SqlDataAdapter sda2 = new SqlDataAdapter("SELECT COUNT(*) FROM Colours WHERE Colours='" + colour + "' AND Quality='" + quality + "'", con);
                sda2.Fill(dt2);
                if(int.Parse(dt2.Rows[0][0].ToString())>=1)
                {
                    con.Close();
                    return true;
                }
                else
                {
                    con.Close();
                    return false;
                }
            }
            catch(Exception e)
            {
                this.ErrorBox("Error (isPresentColour)\n" + e.Message, "Exception");
            }
            finally
            {
                con.Close();
            }
            return true;
        }
        public DataTable getVoucherHistories(string tablename)
        {
            DataTable dt = new DataTable(); //this is creating a virtual table  
            try
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT TOP 200 * FROM "+tablename+" ORDER BY Voucher_ID DESC", con);
                sda.Fill(dt);
            }
            catch(Exception e)
            {
                this.ErrorBox("Could not connect to database (getVoucherHistories)\n"+ e.Message, "Exception");
            }
            finally
            {
                con.Close();

            }
            return dt;
        }
        public string getFinancialYear(DateTime d)
        {
            int month = d.Date.Month;
            int year = d.Date.Year;
            List<int> ans = new List<int>();
            if (month >= 4)
            {
                ans.Add(year);
                ans.Add((year + 1));
            }
            else
            {
                ans.Add((year-1));
                ans.Add((year));
            }
            return (ans[0].ToString()+"-"+ans[1].ToString());
        }
        public List<int> getFinancialYearArr(string fiscal_year)
        {
            List<int> ans = new List<int>();
            string[] years = fiscal_year.Split('-');
            ans.Add(int.Parse(years[0]));
            ans.Add(int.Parse(years[1]));
            return ans;
        }
        public DataTable getDataIn_FinancialYear(string tablename, string financialyear, string quantitycolumn, string quantities)
        {
            if (string.IsNullOrEmpty(quantities)) return new DataTable();
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                string sql = "SELECT "+ quantitycolumn+" FROM " + tablename + " WHERE " + quantitycolumn + " IN (" + quantities + ") AND Fiscal_Year='"+financialyear+"'";
                Console.WriteLine(sql);
                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                sda.Fill(dt);
            }
            catch(Exception e)
            {
                this.ErrorBox("Error (getCount_FinancialYear):\n" + e.Message, "Exception");
                con.Close();
                return new DataTable();
            }
            finally
            {
                con.Close();
            }
            return dt;
        }
        public bool Cell_Not_NullOrEmpty(DataGridView dgv, int rowIndex, int columnIndex)
        {
            if(rowIndex>=dgv.Rows.Count || columnIndex>=dgv.Columns.Count || rowIndex<0 || columnIndex<0)
            {
                return false;
            }
            if(dgv.Rows[rowIndex].Cells[columnIndex].Value==null)
            {
                return false;
            }
            if(string.IsNullOrEmpty(dgv.Rows[rowIndex].Cells[columnIndex].Value.ToString()))
            {
                return false;
            }
            return true;
        }
        public void ErrorBox(string message, string title = "Error")
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
        public void SuccessBox(string message, string title="Success")
        {
            SuccessBox s = new SuccessBox(message, title);
        }
        public void WarningBox(string message, string title="Warning")
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        public void SetGridViewSortState(DataGridView dgv, DataGridViewColumnSortMode sortMode)
        {
            foreach (DataGridViewColumn col in dgv.Columns)
                col.SortMode = sortMode;
        }
        public void printDataTable(DataTable d)
        {
            Console.WriteLine("Printing DataTable");
            for(int i=0; i<d.Rows.Count; i++)
            {
                for(int j=0; j<d.Columns.Count; j++)
                {
                    Console.Write(d.Rows[i][j].ToString() + " ");
                }
                Console.Write("\n");
            }
        }
        public void auto_adjust_dgv(DataGridView dgv)
        {
            for (int i = 0; i < dgv.Columns.Count; i++)
            {
                dgv.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        //Arrow Key Events
        public void comboBoxEvent(ComboBox c)
        {
            //c.SelectionChangeCommitted += new System.EventHandler(this.temp);
            c.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.PKeyDownCb);
            c.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDownCb);
        }
        //private void temp(object sender, )
        int i = 0;
        bool droppeddown = false;
        private void KeyDownCb(object sender, KeyEventArgs e)
        {
            ComboBox c = sender as ComboBox;
            if (e.KeyCode == Keys.Enter)
            {
                i++;
                if (c.DroppedDown == false && droppeddown==false)
                {
                    if(c!=null) c.DroppedDown = true;
                    e.Handled = true;
                    return;
                }
                else
                {
                    droppeddown = false;
                }
            }
            if (c.DroppedDown == true && (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down))
            {
                return;
            }
            if(e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                arrowControl(c, sender, e);
                e.Handled = true;
            }
        }
        private void PKeyDownCb(object sender, PreviewKeyDownEventArgs e)
        {
            ComboBox c = sender as ComboBox;
            if (e.KeyCode == Keys.Enter && c.DroppedDown == true)
            {
                droppeddown = true; //actually the combobox is dropped down(in 1st iteration and then closes in the second)
            }
        }
        public void textBoxEvent(TextBox t)
        {
            t.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDownTb);
        }
        private void KeyDownTb(object sender, KeyEventArgs e)
        {
            TextBox t = sender as TextBox;
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                arrowControl(t, sender, e);
                e.Handled = true;
            }
        }
        public void DTPEvent(DateTimePicker dtp)
        {
            dtp.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDownDtp);
        }
        private void KeyDownDtp(object sender, KeyEventArgs e)
        {
            DateTimePicker dtp = sender as DateTimePicker;
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                arrowControl(dtp, sender, e);
                e.Handled = true;
            }
        }
        public void arrowControl(dynamic control, object sender, KeyEventArgs e)
        {
            Form f = control.FindForm();
            dynamic x;
            if (e.KeyCode == Keys.Up)
            {
                Console.WriteLine("Up");
                x = f.GetNextControl((Control)sender, false);
            }
            else if (e.KeyCode == Keys.Down)
            {
                Console.WriteLine("Down");
                x = f.GetNextControl((Control)sender, true);
            }
            else
            {
                Console.WriteLine("Else");
                return;
            }

            if (x == null)
            {
                Console.WriteLine("null");
                return;
            }
            if (x.GetType().Name.ToString() == "Label")
            {
                Console.WriteLine("label");
                return;
            }
            if (x.TabIndex == 0)
            {
                Console.WriteLine("tabindex 0");
                return;
            }
            else if (x.Enabled == false)
            {
                Console.WriteLine("enabled");
                return;
            }
            Console.WriteLine(x.Name);
            Graphics g = x.CreateGraphics();
            g.DrawRectangle(Pens.Black, 0, 0, x.Width, x.Height);
            x.Focus();
            g.Dispose();
        }
        public void buttonEvent(Button b)
        {
            b.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.PKeyDownB);
            b.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDownB);
        }
        private void KeyDownB(object sender, KeyEventArgs e)
        {
            Button b = sender as Button;
            arrowControl(b, sender, e);
            e.Handled = true;
        }
        private void PKeyDownB(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down) e.IsInputKey = true;
        }

        //Login Logout
        public void recordLogin(string user)
        {
            Debug.Assert(useractive == null);
            this.useractive = user;
            loginTime = DateTime.Now;
        }
        public void recordLogout(string user)
        {
            Debug.Assert(useractive == user);
            DateTime logoutTime = DateTime.Now;
            try
            {
                con.Open();
                TimeSpan value = logoutTime.Subtract(this.loginTime);
                //cmd.Parameters.AddWithValue("@value1", this.loginTime);
                //cmd.Parameters.AddWithValue("@value2", logoutTime);
                SqlDataAdapter adapter = new SqlDataAdapter();
                //string sql = "insert Log (Username, LoginTime, LogoutTime) values ('" + this.useractive + "', convert(datetime, '" + this.loginTime.ToString("dd-MM-yy hh:mm:ss tt") + "', 5), convert(datetime, '" + logoutTime.ToString("dd-MM-yy hh:mm:ss tt") + "', 5))";
                string sql = "insert Log (Username, LoginTime, LogoutTime, TimeDuration) values ('" + this.useractive + "', convert(datetime, '" + this.loginTime.ToString("dd-MM-yy hh:mm:ss tt") + "', 5), convert(datetime, '" + logoutTime.ToString("dd-MM-yy hh:mm:ss tt") + "',5), '" + value.ToString(@"hh\:mm\:ss") + ".0000000')";
                adapter.InsertCommand = new SqlCommand(sql, con);
                adapter.InsertCommand.ExecuteNonQuery();
                //cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                this.ErrorBox("Login record error \n"+e.Message, "Exception");
            }
            finally
            {
                con.Close();
            }
            this.useractive = null;
        }
        public int checkLogin(string username, string password)
        {
            //SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-MOUBPNG\MSSQLSERVER2019;Initial Catalog=FactoryInventory;Integrated Security=True;"); // making connection   

            /* in above line the program is selecting the whole data from table and the matching it with the user name and password provided by user. */
            DataTable dt = new DataTable(); //this is creating a virtual table  
            DataTable dt2 = new DataTable(); //this is creating a virtual table  
            int ans = -1;
            try
            {
                con.Open();
                SqlDataAdapter sda2 = new SqlDataAdapter("SELECT COUNT(*) FROM Users WHERE Username='" + username + "' AND PasswordHash=HASHBYTES('SHA', '" + password + "')", con);
                sda2.Fill(dt2);
                if (dt2.Rows[0][0].ToString() == "1")
                {
                    SqlDataAdapter sda = new SqlDataAdapter("SELECT AccessLevel FROM Users WHERE Username='" + username + "' AND PasswordHash=HASHBYTES('SHA', '" + password + "')", con);
                    sda.Fill(dt);
                    //Console.WriteLine(dt.Rows[0][0].ToString());
                    ans = int.Parse(dt.Rows[0][0].ToString());
                }
                else
                {
                    this.ErrorBox("Invalid username or password", "Error");
                    ans = 0;
                }
            }
            catch (Exception e)
            {
                this.ErrorBox("Could not connect to database (checkLogin) \n"+e.Message, "Exception");
            }
            finally
            {
                con.Close();
            }
            return ans;
        }


        //User
        public int addUser(string username, string password, int acc)
        {
            int ans = -1;
            try
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                string sql = "INSERT INTO Users (Username, PasswordHash, AccessLevel) VALUES ('" + username + "',HASHBYTES('SHA', '" + password + "'), " + acc + ")";
                adapter.InsertCommand = new SqlCommand(sql, con);
                adapter.InsertCommand.ExecuteNonQuery();
                ans = 1;
            }
            catch (Exception e)
            {
                ans = 0;
                this.ErrorBox("Could not add user (addUser) \n" + e.Message, "Exception");
            }

            finally
            {
                con.Close();
            }
            return ans;
        }
        public DataTable getUserData()
        {
            DataTable dt = new DataTable(); //this is creating a virtual table  
            try
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT Username, AccessLevel FROM Users", con);
                sda.Fill(dt);
            }
            catch
            {
                this.ErrorBox("Could not connect to database (getUserData)", "Exception");
            }
            finally
            {
                con.Close();

            }
            return dt;
        }
        public void deleteUser(string username)
        {
            try
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                string sql = "DELETE FROM Users WHERE Username='" + username + "'";
                adapter.InsertCommand = new SqlCommand(sql, con);
                adapter.InsertCommand.ExecuteNonQuery();
                this.SuccessBox("User deleted");
            }
            catch (Exception e)
            {
                this.ErrorBox("Could not delete user (deleteUser)\n"+e.Message, "Exception");
            }

            finally
            {
                con.Close();

            }
        }
        public void updateUser(string username, string password, int access)
        {
            try
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                string sql;
                if (password == "")
                {
                    sql = "UPDATE Users AccessLevel = " + access + "  WHERE Username='" + username + "'";
                    this.SuccessBox("Access Level Updated");
                }
                else
                {
                    sql = "UPDATE Users SET PasswordHash = HASHBYTES('SHA', '" + password + "') , AccessLevel = " + access + "  WHERE Username='" + username + "'";
                    this.SuccessBox("Password/Access Level Updated");
                }
                adapter.InsertCommand = new SqlCommand(sql, con);
                adapter.InsertCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                this.ErrorBox("Could not edit user (updateUser)\n"+e.Message, "Exception");
            }

            finally
            {
                con.Close();
            }
        }
        public DataTable getUserLog()
        {
            DataTable dt = new DataTable(); //this is creating a virtual table  
            try
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Log", con);
                sda.Fill(dt);
            }
            catch
            {
                this.ErrorBox("Could not connect to database (getUserLog)", "Exception");
            }
            finally
            {
                con.Close();

            }
            return dt;
        }

        //Print
        public int getPrint(string table_name, string where)
        {
            DataTable dt = new DataTable(); //this is creating a virtual table  
            int ans = -1;
            try
            {
                con.Open();
                string sql = "SELECT Printed FROM " + table_name + " WHERE " + where;
                Console.WriteLine(sql);
                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                sda.Fill(dt);
                if (dt.Rows[0][0].ToString() == null || dt.Rows[0][0].ToString() == "") ans = 0;
                else if (dt.Rows.Count != 0) ans = int.Parse(dt.Rows[0][0].ToString());
            }
            catch (Exception e)
            {
                this.ErrorBox("Could not get Print (getCartonProducedPrint) "+table_name+"\n" + e.Message, "Exception");
            }
            finally
            {
                con.Close();
            }
            return ans;
        }
        public bool setPrint(string table_name, string where, int value)
        {
            try
            {
                con.Open();
                string sql = "UPDATE " + table_name + " SET Printed=" + value + " WHERE " + where;
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.InsertCommand = new SqlCommand(sql, con);
                adapter.InsertCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                this.ErrorBox("Could not setPrint (getCartonProducedPrint) "+table_name+"\n" + e.Message, "Exception");
                con.Close();
                return false;
            }
            finally
            {
                con.Close();
            }
            return true;
        }


        //Quality, Company, Customer, Spring
        public void addQC(string name, char c)
        {
            if (name == "")
                return;
            string tablename = "";
            if (c == 'q')
                tablename = "Quality";
            else if (c == 'c')
                tablename = "Company_Names";
            else if (c == 'C')
                tablename = "Customers";
            else if (c == 'd')      //Dyeing Company
                tablename = "Dyeing_Company_Names";
            else if (c == 'l')      //Colour
                tablename = "Colours";
            else if (c == 'n')      //Cone
                tablename = "Cones";
            try
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                string sql = "INSERT INTO " + tablename + " VALUES ('" + name + "') ";
                adapter.InsertCommand = new SqlCommand(sql, con);
                adapter.InsertCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                this.ErrorBox("Could not add " + tablename + " (addQC) \n" + e.Message, "Exception");
            }

            finally
            {
                con.Close();
            }
        }
        public void addSpring(string name, float weight, string tablename)
        {
            try
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                string sql = "INSERT INTO "+tablename+" VALUES ('" + name + "', "+weight+") ";
                adapter.InsertCommand = new SqlCommand(sql, con);
                adapter.InsertCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                this.ErrorBox("Could not add Spring (addSpring) \n" + e.Message, "Exception");
            }

            finally
            {
                con.Close();
            }
        }
        public void addQuality(string name, string hsn_no, string print_colour, string quality_after_twist)
        {
            try
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                string sql = "INSERT INTO Quality VALUES ('" + quality_after_twist + "', '" + hsn_no + "', '"+print_colour+"', '"+name+"') ";
                adapter.InsertCommand = new SqlCommand(sql, con);
                adapter.InsertCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                this.ErrorBox("Could not add Quality (addQuality) \n" + e.Message, "Exception");
            }

            finally
            {
                con.Close();
            }
        }
        public void addColour(string name, float weight, string quality)
        {
            try
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                string sql = "INSERT INTO Colours VALUES ('" + name + "', '" +quality + "', " + weight + ") ";
                adapter.InsertCommand = new SqlCommand(sql, con);
                adapter.InsertCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                this.ErrorBox("Could not add Colour (addColour) \n" + e.Message, "Exception");
            }

            finally
            {
                con.Close();
            }
        }
        public void deleteQC(string name, char c)
        {
            string tablename = "";
            if (c == 'q')
                tablename = "Quality";
            else if (c == 'c')
                tablename = "Company_Names";
            else if (c == 'C')
                tablename = "Customers";
            else if (c == 's')
                tablename = "Spring";
            else if (c == 'd')      //Dyeing Company
                tablename = "Dyeing_Company_Names";
            else if (c == 'l')      //Colour
                tablename = "Colours";
            else if (c == 'n')      //Cone
                tablename = "Cones";
            try
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                string sql;
                sql = "DELETE FROM " + tablename + " WHERE " + tablename + "='" + name + "'";
                adapter.InsertCommand = new SqlCommand(sql, con);
                adapter.InsertCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                this.ErrorBox("Could not delete " + tablename + " (deleteQC) \n" + e.Message, "Exception");
            }

            finally
            {
                con.Close();

            }
        }
        public void addCustomer(string name, string gst, string address, string tablename)
        {
            try
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                string sql = "INSERT INTO "+tablename+" VALUES ('" + name + "', '" + gst+ "', '"+address+"') ";
                adapter.InsertCommand = new SqlCommand(sql, con);
                adapter.InsertCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                this.ErrorBox("Could not add (addCustomer) \n" + e.Message, "Exception");
            }

            finally
            {
                con.Close();
            }
        }
        public void deleteCustomer(string name, string tablename)
        {
            try
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                string sql;
                sql = "DELETE FROM "+tablename+" WHERE "+tablename+"='" + name + "'";
                adapter.InsertCommand = new SqlCommand(sql, con);
                adapter.InsertCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                this.ErrorBox("Could not delete (deleteCustomer) \n" + e.Message, "Exception");
            }

            finally
            {
                con.Close();

            }
        }
        public void deleteQuality(string quality_after_twist)
        {
            try
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                string sql;
                sql = "DELETE FROM Quality WHERE Quality='" + quality_after_twist + "'";
                adapter.InsertCommand = new SqlCommand(sql, con);
                adapter.InsertCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                this.ErrorBox("Could not delete (deleteQuality) \n" + e.Message, "Exception");
            }

            finally
            {
                con.Close();

            }
        }
        public void editCustomer(string name, string gst, string address, string oldname, string tablename)
        {
            try
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                string sql = "UPDATE "+tablename+" SET "+tablename+" ='"+ name+"', GSTIN='"+gst+"', Customer_Address = '"+address+"' WHERE "+tablename+"= '" + oldname + "'";
                adapter.InsertCommand = new SqlCommand(sql, con);
                adapter.InsertCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                this.ErrorBox("Could not edit (editCustomer) " + e.Message, "Exception");
            }

            finally
            {
                con.Close();
            }
        }
        public void editQuality(string name, string hsn_no, string print_colour, string quality_after_twist, string oldname)
        {
            try
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                string sql = "UPDATE Quality SET Quality='" + quality_after_twist + "', HSN_No='" + hsn_no+ "', Print_Colour='"+print_colour+"', Quality_Before_Twist='"+name+"' WHERE Quality= '" + oldname + "'";
                adapter.InsertCommand = new SqlCommand(sql, con);
                adapter.InsertCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                this.ErrorBox("Could not edit (editCustomer) " + e.Message, "Exception");
            }

            finally
            {
                con.Close();
            }
        }
        public void deleteColour(string colour, string quality)
        { 
            try
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                string sql;
                sql = "DELETE FROM Colours WHERE Colours='" + colour + "' AND Quality='"+quality+"'";
                adapter.InsertCommand = new SqlCommand(sql, con);
                adapter.InsertCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                this.ErrorBox("Could not delete colour (deleteColour) \n" + e.Message, "Exception");
            }

            finally
            {
                con.Close();

            }
        }
        public void editQC(string newname, string oldname, char c)
        {
            if (newname == "")
                return;
            string tablename = "";
            if (c == 'q')
                tablename = "Quality";
            else if (c == 'c')
                tablename = "Company_Names";
            else if (c == 'C')
                tablename = "Customers";
            else if (c == 'd')      //Dyeing Company
                tablename = "Dyeing_Company_Names";
            else if (c == 'l')      //Colour
                tablename = "Colours";
            else if (c == 'n')      //Cone
                tablename = "Cones";
            try
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                string sql = "UPDATE " + tablename + " SET " + tablename + "= '" + newname + "'  WHERE " + tablename + " = '" + oldname + "'";
                adapter.InsertCommand = new SqlCommand(sql, con);
                adapter.InsertCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                this.ErrorBox("Could not edit (editQC) "+ e.Message, "Exception");
            }

            finally
            {
                con.Close();
            }
        }
        public void editSpring(string newname, string oldname, float weight, string tablename, string second_column)
        {
            try
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                string sql = "UPDATE "+tablename+" SET "+tablename+"='" + newname + "', "+second_column+"="+weight+"  WHERE "+tablename+"= '" + oldname + "'";
                adapter.InsertCommand = new SqlCommand(sql, con);
                adapter.InsertCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                this.ErrorBox("Could not edit spring (editSpring) " + e.Message, "Exception");
            }

            finally
            {
                con.Close();
            }
        }
        public void editColour(string newname, string oldname, float dyeing_rate, string quality, string old_quality)
        {
            try
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                string sql = "UPDATE Colours SET Colours='" + newname + "', Dyeing_Rate=" + dyeing_rate + " , Quality='"+quality+"' WHERE Colours= '" + oldname + "' AND Quality='"+old_quality+"'";
                adapter.InsertCommand = new SqlCommand(sql, con);
                adapter.InsertCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                this.ErrorBox("Could not edit colour (editColour) " + e.Message, "Exception");
            }

            finally
            {
                con.Close();
            }
        }
        public DataTable getQC(char c)
        {
            string tablename = "";
            if (c == 'q')       //Quality
                tablename = "Quality";
            else if (c == 'c')      //Compamy
                tablename = "Company_Names";
            else if (c == 'C')      //Customers
                tablename = "Customers";
            else if (c == 's')      //Spring
                tablename = "Spring";
            else if (c == 'd')      //Dyeing Company
                tablename = "Dyeing_Company_Names";
            else if (c == 'l')      //Colour
                tablename = "Colours";
            else if (c == 'f')      //Colour
                tablename = "Fiscal_Year";
            else if (c == 'n')      //Cone
                tablename = "Cones";
            DataTable dt = new DataTable(); //this is creating a virtual table  
            try
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM " + tablename + " ", con);
                sda.Fill(dt);
            }
            catch(Exception e)
            {
                this.ErrorBox("Could not connect to database (getQC) " + e.Message, "Exception");
            }
            finally
            {
                con.Close();

            }
            return dt;
        }
        public float getDyeingRate(string colour, string quality)
        {
           //returns -1F if no rate found
            DataTable dt = new DataTable(); //this is creating a virtual table
            float ans = -1F;
            try
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT Dyeing_Rate FROM Colours WHERE Colours='"+colour+"' AND Quality='"+quality+"'", con);
                sda.Fill(dt);
                if(dt.Rows.Count!=0)
                {
                    ans = float.Parse(dt.Rows[0][0].ToString());
                }
            }
            catch (Exception e)
            {
                this.ErrorBox("Could not connect to database (getDyeingRate) " + e.Message, "Exception");
            }
            finally
            {
                con.Close();

            }
            return ans;
        }
        public float getSpringWeight(string spring)
        {
            //returns -1F if no rate found
            DataTable dt = new DataTable(); //this is creating a virtual table
            float ans = -1F;
            try
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT Spring_Weight FROM Spring WHERE Spring='" + spring+"'", con);
                sda.Fill(dt);
                if (dt.Rows.Count != 0)
                {
                    ans = float.Parse(dt.Rows[0][0].ToString());
                }
            }
            catch (Exception e)
            {
                this.ErrorBox("Could not connect to database (getSpringWeight) " + e.Message, "Exception");
            }
            finally
            {
                con.Close();

            }
            return ans;
        }
        public Color getQualityColour(string quality)
        {
            //returns -1F if no rate found
            DataTable dt = new DataTable(); //this is creating a virtual table
            Color col= Color.White;
            try
            { 
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT Print_Colour FROM Quality WHERE Quality='" + quality+ "'", con);
                sda.Fill(dt);
                if (dt.Rows.Count != 0)
                {
                    if (dt.Rows[0][0].ToString()[0] >= 97 && dt.Rows[0][0].ToString()[0] <= 122)
                    {
                        col = System.Drawing.ColorTranslator.FromHtml("#" + dt.Rows[0][0].ToString());
                    }
                    else
                    {
                        col = System.Drawing.ColorTranslator.FromHtml(dt.Rows[0][0].ToString());
                    }
                }
            }
            catch (Exception e)
            {
                this.ErrorBox("Could not connect to database (getDyeingRate) " + e.Message, "Exception");
                con.Close();
                return Color.White;
            }
            finally
            {
                con.Close();

            }
            return col;
        }
        public string getBeforeQuality(string new_quality)
        {
            string ans="";
            try
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT Quality_Before_Twist FROM Quality WHERE Quality='" + new_quality + "'", con);
                DataTable dt = new DataTable(); //this is creating a virtual table
                sda.Fill(dt);
                if (dt.Rows.Count != 0)
                {
                    ans = dt.Rows[0][0].ToString();
                }
            }
            catch (Exception e)
            {
                this.ErrorBox("Could not connect to database (getBeforeQuality) " + e.Message, "Exception");
                con.Close();
                return ans;
            }
            finally
            {
                con.Close();

            }
            return ans;
        }
        public DataTable getTableRows(string tablename, string where)
        {
            DataTable dt = new DataTable(); //this is creating a virtual table
            try
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM "+tablename+" WHERE "+where, con);
                sda.Fill(dt);
            }
            catch (Exception e)
            {
                this.ErrorBox("Could not connect to database (getTableRow) "+tablename+" where "+where+" \n"+ e.Message, "Exception");
                con.Close();
                return new DataTable();
            }
            finally
            {
                con.Close();
            }
            return dt;
        }
        public DataTable getTableData(string tablename, string cols, string where)
        {
            DataTable dt = new DataTable(); //this is creating a virtual table
            try
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT +"+cols+" FROM " + tablename + " WHERE " + where, con);
                sda.Fill(dt);
            }
            catch (Exception e)
            {
                this.ErrorBox("Could not connect to database (getTableData) " + tablename + " where " + where + " \n" + e.Message, "Exception");
                con.Close();
                return new DataTable();
            }
            finally
            {
                con.Close();

            }
            return dt;
        }

        //Carton Voucher
        public bool addCartonVoucher(DateTime dtinput, DateTime dtbill, string billNumber, string quality, string quality_arr, string company, string cost, string cartonno, string weights, int number, float netweight)
        {
            string inputDate = dtinput.Date.ToString("MM-dd-yyyy").Substring(0, 10);
            string billDate = dtbill.Date.ToString("MM-dd-yyyy").Substring(0, 10);
            string added_carton = "";
            string[] carton_no = this.csvToArray(cartonno);
            string financialyear = this.getFinancialYear(dtbill);
            
            string new_cartons = "";
            for (int i=0;i<carton_no.Length;i++)
            {
                new_cartons += carton_no[i] + ",";
            }
            string[] new_carton_arr = this.csvToArray(new_cartons);
            //get count of all new cartons
            DataTable carton_dup = this.getDataIn_FinancialYear("Carton", financialyear, "Carton_No", removecom(new_cartons));
            Dictionary<string, bool> carton_dup_dict = new Dictionary<string, bool>();
            for (int i = 0; i < carton_dup.Rows.Count; i++) carton_dup_dict[carton_dup.Rows[i]["Carton_No"].ToString()] = true;
            for (int i = 0; i < new_carton_arr.Length; i++)
            {
                bool nouse;
                bool present = carton_dup_dict.TryGetValue(new_carton_arr[i], out nouse);
                if (present==true) //if its present in the duplicate dictionary
                {
                    this.ErrorBox("Carton number " + new_carton_arr[i] + " at row " + (i+ 1).ToString() + " already exists in Financial Year " + financialyear, "Error");
                    return false;
                }
            }

            try
            {
                string[] qualities = this.csvToArray(quality);
                string[] qualities_arr = this.csvToArray(quality_arr);
                string[] carton_weights_arr = this.csvToArray(weights);
                string[] buy_cost = this.csvToArray(cost);

                bool flag = false; //to check errors in adding carton;
                int index = 0;
                for (int i = 0; i < number; i++)
                {
                    bool added = this.addCarton(carton_no[i], inputDate, billDate, billNumber, qualities[int.Parse(qualities_arr[i])], company, float.Parse(carton_weights_arr[i]), float.Parse(buy_cost[int.Parse(qualities_arr[i])]), 1, financialyear);
                    if(added == false)
                    {
                        flag = true; //carton not added successfully
                        index = i;
                        break;
                    }
                    added_carton+=carton_no[i]+",";
                }
                if(flag == true)
                {
                    //Failed to add all cartons
                    //Remove all added cartons
                    removeCarton(removecom(added_carton), financialyear, "Carton");
                    this.ErrorBox("Carton Number: " + carton_no[index] + " at Row: " + (index + 1).ToString() + " was already added to the Database. Could not add voucher", "Error");
                    return false;
                }
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                string sql = "INSERT INTO Carton_Voucher (Date_Of_Input, Date_Of_Billing, Bill_No, Quality, Quality_Arr, Company_Name, Number_of_Cartons, Carton_No_Arr, Carton_Weight_Arr, Net_Weight, Buy_Cost, Fiscal_Year) VALUES ('" + inputDate + "','" + billDate + "', '" + billNumber + "','" + quality + "', '" + quality_arr + "', '" + company + "', " + number + ", '" + cartonno + "', '" + weights + "', " + netweight + " , '" + cost + "', '"+financialyear+"')";
                Console.WriteLine(sql);
                adapter.InsertCommand = new SqlCommand(sql, con);
                adapter.InsertCommand.ExecuteNonQuery();
                this.SuccessBox("Voucher Added Successfully");
            }
            catch (SqlException e)
            {
                if(e.Number==2627)
                {
                    this.ErrorBox("Bill Number Already Exists", "Exception");
                }
                else
                {
                    this.ErrorBox("Could not add carton voucher (addCartonVoucher) \n" + e.Message, "Exception");
                }
                con.Close();
                removeCarton(removecom(added_carton), financialyear, "Carton");
                return false;
            }

            finally
            {
                con.Close();
            }
            return true;

        }
        public bool editCartonVoucher(int voucher_id, DateTime dtinput, DateTime dtbill, string billNumber, string quality, string quality_arr, string company, string cost, string cartonno, string weights, int number, float netweight, Dictionary<string, bool> carton_editable)
        {
            string inputDate = dtinput.Date.ToString("MM-dd-yyyy").Substring(0, 10);
            string billDate = dtbill.Date.ToString("MM-dd-yyyy").Substring(0, 10);
            //Input Date is never updated
            //Dictionary carton_editable contains entries for carton_nos with state =2 or state=3
            Dictionary<string, bool> old_cartons_hash = new Dictionary<string, bool>();
            List<string> added_cartons = new List<string>(); //to store cartons added by the function
            try
            {
                string[] carton_no = this.csvToArray(cartonno);

                //Get all carton_nos which were previously present
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT Carton_No_Arr, Fiscal_Year FROM Carton_Voucher WHERE Voucher_ID=" + voucher_id, con);
                DataTable old = new DataTable();
                sda.Fill(old);
                con.Close();
                string[] old_carton_nos = this.csvToArray(old.Rows[0][0].ToString());
                string old_fiscal_year = old.Rows[0][1].ToString();
                Console.WriteLine("selected1");

                /*<------------------Check for duplicates------------------->*/
                //Insert old cartons into hash 
                Console.WriteLine("-----------Old cartons-----------");
                for (int i = 0; i < old_carton_nos.Length; i++)
                {
                    Console.WriteLine(old_carton_nos[i]);
                    old_cartons_hash[old_carton_nos[i]] = true;
                }

                //Any new carton added should have count=0
                //New carton is a carton in the carton_no list, not present in the old_carton_hash
                string new_cartons = "", new_carton_indexes="";
                string financialyear = this.getFinancialYear(dtbill);
                for (int i = 0; i < carton_no.Length; i++)
                {
                    //Check if its a new carton(not present in Hash)
                    bool value=false;
                    bool value2 = old_cartons_hash.TryGetValue(carton_no[i], out value);
                    if(value2 == false && value == false) //Carton not present in the hash, hence its new
                    {
                        new_cartons += carton_no[i] + ",";
                        new_carton_indexes += i + ",";
                    }
                }
                string[] new_carton_arr = this.csvToArray(new_cartons);
                string[] new_carton_indexes_arr = this.csvToArray(new_carton_indexes);
                //get count of all new cartons
                DataTable carton_dup = this.getDataIn_FinancialYear("Carton", financialyear, "Carton_No", removecom(new_cartons));
                Dictionary<string, bool> carton_dup_dict = new Dictionary<string, bool>();
                for (int i = 0; i < carton_dup.Rows.Count; i++) carton_dup_dict[carton_dup.Rows[i]["Carton_No"].ToString()] = true;
                for (int i = 0; i < new_carton_arr.Length; i++)
                {
                    bool nouse;
                    bool present = carton_dup_dict.TryGetValue(new_carton_arr[i], out nouse);
                    if (present == true) //if its present in the duplicate dictionary
                    {
                        this.ErrorBox("Carton number " + new_carton_arr[i] + " at row " + (i + 1).ToString() + " already exists in Financial Year " + financialyear, "Error");
                        return false;
                    }
                }

                /*<Check if issue date and sale date of cartons in state 2 and 3 is >= Bill Date>*/
                string non_edit_cartons = "", non_edit_carton_index="";
                for (int i = 0; i < carton_no.Length; i++)
                {
                    bool value;
                    bool value2 = carton_editable.TryGetValue(carton_no[i], out value);
                    if (value2 == true) //does contain entry, means it is in state 2 and 3
                    {
                        non_edit_cartons += carton_no[i] + ",";
                    }
                }
                if(removecom(non_edit_cartons)!="")
                {
                    con.Open();
                    SqlDataAdapter sda1 = new SqlDataAdapter("SELECT Carton_No, Date_Of_Issue, Date_Of_Sale FROM Carton WHERE Carton_No IN (" + removecom(non_edit_cartons) + ") AND Fiscal_Year='" + old_fiscal_year + "'", con);
                    DataTable dt = new DataTable();
                    sda1.Fill(dt);
                    con.Close();
                    
                    //populate dictionary
                    Dictionary<string, Tuple<string, int>> dates = new Dictionary<string, Tuple<string, int>>();
                    for (int i=0; i<dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["Date_Of_Issue"].ToString() != "")
                        {
                            dates[dt.Rows[i]["Carton_No"].ToString()] = new Tuple<string, int>(dt.Rows[i]["Date_Of_Issue"].ToString(), 0);
                        }
                        else if (dt.Rows[i]["Date_Of_Sale"].ToString() != "")
                        {
                            dates[dt.Rows[i]["Carton_No"].ToString()] = new Tuple<string, int>(dt.Rows[i]["Date_Of_Sale"].ToString(), 1);
                        }
                    }
                    
                    string[] non_edit_cartons_arr = this.csvToArray(non_edit_cartons);
                    for (int i=0; i<non_edit_cartons_arr.Length; i++)
                    {
                        Tuple<string, int> value = dates[non_edit_cartons_arr[i]];
                        DateTime check= Convert.ToDateTime(value.Item1);
                        int type = value.Item2;
                        if (dtbill > check)
                        {
                            if(type==0) this.ErrorBox("Carton number: " + non_edit_cartons_arr[i] + " at row " + (i + 1).ToString() + " has Date of Issue (" + check.Date.ToString("dd-MM-yyyy") + ") earlier than given Date of billing (" + dtbill.Date.ToString("dd-MM-yyyy") + ")", "Error");
                            else this.ErrorBox("Carton number: " + non_edit_cartons_arr[i] + " at row " + (i + 1).ToString() + " has Date of Sale (" + check.Date.ToString("dd-MM-yyyy") + ") earlier than given Date of billing (" + dtbill.Date.ToString("dd-MM-yyyy") + "),", "Error");
                            return false;
                        }
                    }
                }


                Console.WriteLine("selected2");
                string cartons = "";
                //Remove cartons with state 1 in the old voucher
                for (int i = 0; i < old_carton_nos.Length; i++)
                {
                    bool value;
                    bool value2 = carton_editable.TryGetValue(old_carton_nos[i], out value);
                    if (value2 == false) //doesnt contain entry, means it is in state 1
                    {
                        cartons += old_carton_nos[i] + ",";
                        Console.WriteLine("Removing Carton: " + old_carton_nos[i]);
                    }
                }
                this.removeCarton(removecom(cartons), old_fiscal_year, "Carton");
                
                //Add all New Cartons with state 1
                string[] qualities = this.csvToArray(quality);
                string[] qualities_arr = this.csvToArray(quality_arr);
                string[] carton_weights_arr = this.csvToArray(weights);
                string[] buy_cost = this.csvToArray(cost);
                for (int i = 0; i < carton_no.Length; i++)
                {
                    bool value;
                    bool value2 = carton_editable.TryGetValue(carton_no[i], out value);
                    if (value2 == false)
                    {
                        this.addCarton(carton_no[i], inputDate, billDate, billNumber, qualities[int.Parse(qualities_arr[i])], company, float.Parse(carton_weights_arr[i]), float.Parse(buy_cost[int.Parse(qualities_arr[i])]), 1, financialyear);
                    }
                    else
                    {
                        this.updateCarton(carton_no[i], old_fiscal_year, billDate, billNumber, float.Parse(buy_cost[int.Parse(qualities_arr[i])]), financialyear);
                    }
                }

                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                string sql = "UPDATE Carton_Voucher SET Date_Of_Billing='" + billDate + "', Bill_No='" + billNumber + "', Quality='" + quality + "', Quality_Arr='" + quality_arr + "', Company_Name='" + company + "', Number_of_Cartons= " + number + ", Carton_No_Arr='" + cartonno + "', Carton_Weight_Arr='" + weights + "', Net_Weight=" + netweight + ", Buy_Cost='" + cost + "', Fiscal_Year='" + financialyear+ "' WHERE Voucher_ID=" + voucher_id;
                Console.WriteLine(sql);
                adapter.InsertCommand = new SqlCommand(sql, con);
                adapter.InsertCommand.ExecuteNonQuery();
                this.SuccessBox("Voucher Edited Successfully");
            }
            catch (Exception e)
            {
                this.ErrorBox("Could not edit carton voucher (editCartonVoucher) \n" + e.Message, "Exception");
                con.Close();
                return false;
            }

            finally
            {
                con.Close();
            }
            return true;
        }
        public bool deleteCartonVoucher(int voucher_id)
        {
            try
            {
                //Get cartons in voucher
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT Carton_No_Arr, Fiscal_Year FROM Carton_Voucher WHERE Voucher_ID=" + voucher_id, con);
                DataTable old = new DataTable();
                sda.Fill(old);
                string[] old_carton_nos = this.csvToArray(old.Rows[0][0].ToString());
                string old_fiscal_year = old.Rows[0][1].ToString();

                //delete all rows from carton voucher
                string sql;
                string carton_in= old.Rows[0][0].ToString();
                carton_in = removecom(carton_in);
                SqlDataAdapter adapter = new SqlDataAdapter();
                if (!string.IsNullOrEmpty(carton_in))
                {
                    sql = "DELETE FROM Carton WHERE Carton_No IN (" + carton_in + ") AND Fiscal_Year='" + old_fiscal_year + "'";
                    adapter.InsertCommand = new SqlCommand(sql, con);
                    adapter.InsertCommand.ExecuteNonQuery();
                    Console.WriteLine(sql);
                }

                //update deleted column in carton_voucher
                SqlDataAdapter adapter2 = new SqlDataAdapter();
                sql = "UPDATE Carton_Voucher SET Deleted=1 WHERE Voucher_ID="+voucher_id;
                Console.WriteLine(sql);
                adapter2.InsertCommand = new SqlCommand(sql, con);
                adapter2.InsertCommand.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception e)
            {
                this.ErrorBox("Could not delete carton voucher (deleteCartonVoucher) \n" + e.Message, "Exception");
                con.Close();
                return false;
            }
            finally
            {
                con.Close();
            }
            return true;
        }

        //Carton
        public void updateCarton(string cartonno, string old_fiscal_year, string billDate, string billNumber, float buy_cost, string financialyear)
        {
            try
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                string sql = "UPDATE Carton SET Date_Of_Billing='" + billDate+ "', Bill_No='" + billNumber + "', Buy_Cost="+buy_cost+", Fiscal_Year='"+financialyear+"' WHERE Carton_No= '" + cartonno + "' AND Fiscal_Year='"+old_fiscal_year+"'";
                adapter.InsertCommand = new SqlCommand(sql, con);
                adapter.InsertCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                this.ErrorBox("Could not update Carton (updateCarton)\n" + e.Message, "Exception");
            }
            finally
            {
                con.Close();
            }
        }
        public bool addCarton(string carton_no, string inputDate, string billDate, string billNumber, string quality, string company, float carton_weight, float buy_cost, int state, string fiscal_year)
        {
            //Returns true if carton added successfully, false if not
            try
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                Console.WriteLine("Carton no:" + carton_no);
                string sql = "INSERT INTO Carton (Carton_No, Carton_State, Date_Of_Input, Date_Of_Billing, Bill_No, Quality, Company_Name, Net_Weight, Buy_Cost, Fiscal_Year) VALUES ('" + carton_no + "', " + state + " , '" + inputDate + "','" + billDate + "', '" + billNumber + "','" + quality + "', '" + company + "', " + carton_weight + " , " + buy_cost + ", '"+fiscal_year+"')";
                Console.WriteLine(sql);
                adapter.InsertCommand = new SqlCommand(sql, con);
                adapter.InsertCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                this.ErrorBox("Could not add carton (addCarton) \n" + e.Message, "Exception");
                con.Close();
                return false;
            }

            finally
            {
                con.Close();
            }
            return true;
        }
        public void removeCarton(string carton_nos, string fiscal_year, string tablename)
        {
            if (string.IsNullOrEmpty(carton_nos)) return;
            try
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                string sql = "DELETE FROM "+tablename+" WHERE Carton_No IN (" + carton_nos + ") AND Fiscal_Year='"+fiscal_year+"'";
                adapter.InsertCommand = new SqlCommand(sql, con);
                adapter.InsertCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                this.ErrorBox("Could not delete Carton_No" + carton_nos + " (removeCarton) \n" + e.Message, "Exception");
            }

            finally
            {
                con.Close();
            }
        }
        public void sendCartonTwist(string cartonno, int state, string date_of_issue, string carton_fiscal_year)
        {
            if (string.IsNullOrEmpty(cartonno)) return;
            try
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                string sql;
                if(date_of_issue == null)
                {
                    sql= "UPDATE Carton SET Carton_State=" + state + " , Date_Of_Issue=NULL WHERE Carton_No IN (" + cartonno + ") AND Fiscal_Year='" + carton_fiscal_year + "'";
                }
                else
                {
                    sql = "UPDATE Carton SET Carton_State=" + state + " , Date_Of_Issue='" + date_of_issue + "' WHERE Carton_No='" + cartonno + "' AND Fiscal_Year='" + carton_fiscal_year + "'";
                }
                Console.WriteLine(sql);
                adapter.InsertCommand = new SqlCommand(sql, con);
                adapter.InsertCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                this.ErrorBox("Could not edit Carton State (sendCarton) " + e.Message, "Exception");
            }
            finally
            {
                con.Close();
            }
        }
        public void sendCartonSale(string cartonno, string date_of_sale, float sell_cost, string sale_do_no, string tablename, string type, string carton_fiscal_year, string do_fiscal_year)
        {
            if (string.IsNullOrEmpty(cartonno)) return;
            int state =0;
            if(tablename=="Carton")
            {
                state = 3;
            }
            else if(tablename=="Carton_Produced")
            {
                state = 2;
            }
            try
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                string sql;
                if(date_of_sale==null && sell_cost==-1F && sale_do_no==null)
                {
                    state = 1;
                    sql = "UPDATE "+tablename+" SET Carton_State=" + state + " , Date_Of_Sale=NULL, Sale_Rate=NULL, Sale_DO_No=NULL, Type_Of_Sale=NULL, DO_Fiscal_Year=NULL WHERE Carton_No IN (" + cartonno + ") AND Fiscal_Year='" + carton_fiscal_year + "'";
                }
                else
                {
                    sql = "UPDATE "+tablename+" SET Carton_State=" + state + " , Date_Of_Sale='" + date_of_sale + "', Sale_Rate=" + sell_cost + ", Sale_DO_No = '"+sale_do_no+ "', Type_Of_Sale="+int.Parse(type)+", DO_Fiscal_Year = '"+do_fiscal_year+"' WHERE Carton_No IN (" + cartonno + ") AND Fiscal_Year='" + carton_fiscal_year + "'";
                }
                adapter.InsertCommand = new SqlCommand(sql, con);
                adapter.InsertCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                this.ErrorBox("Could not update carton(sendCartonSale) " + e.Message, "Exception");
            }
            finally
            {
                con.Close();
            }
        }
        public DataTable getCartonTable(string cartonno, string fiscal_year)
        {
            DataTable dt = new DataTable(); //this is creating a virtual table  
            try
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Carton WHERE Carton_No='" + cartonno + "' AND Fiscal_Year='" + fiscal_year + "'", con);
                sda.Fill(dt);
            }
            catch (Exception e)
            {
                this.ErrorBox("Could not get weight (getCartonTable) \n" + e.Message, "Exception");
            }
            finally
            {
                con.Close();
            }
            return dt;
        }
        public DataRow getCartonRow(string cartonno, string fiscal_year)
        {
            DataTable dt = new DataTable(); //this is creating a virtual table  
            try
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Carton WHERE Carton_No='" + cartonno + "' AND Fiscal_Year='" + fiscal_year + "'", con);
                sda.Fill(dt);
            }
            catch (Exception e)
            {
                this.ErrorBox("Could not get weight (getCartonTable) \n" + e.Message, "Exception");
            }
            finally
            {
                con.Close();
            }
            return dt.Rows[0];
        }
        public int getCartonState(string carton_no, string fiscal_year)
        {
            //Returns -1 if carton not found
            int ans = -1;
            try
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT Carton_State FROM Carton WHERE Carton_No='" + carton_no+ "' AND Fiscal_Year='"+fiscal_year+"'", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if(dt.Rows.Count!=0) ans = int.Parse(dt.Rows[0][0].ToString());
            }
            catch (Exception e)
            {
                this.ErrorBox("Could not connect to database (getCartonState) " + e.Message, "Exception");
            }
            finally
            {
                con.Close();
            }
            return ans;
        }


        //Twist Voucher
        public bool addTwistVoucher(DateTime dtinput, DateTime dtissue, string quality, string company, string cartonno, int number, string carton_fiscal_year)
        {
            string inputDate = dtinput.Date.ToString("MM-dd-yyyy").Substring(0, 10);
            string issueDate = dtissue.Date.ToString("MM-dd-yyyy").Substring(0, 10);
            string fiscal_year = this.getFinancialYear(dtissue);
            string[] carton_no = this.csvToArray(cartonno);
            //check if bill dates of all cartons are <= issue date
            for (int i = 0; i < carton_no.Length; i++)
            {
                con.Open();
                SqlDataAdapter sda1 = new SqlDataAdapter("SELECT Date_Of_Billing FROM Carton WHERE Carton_No='" + carton_no[i] + "' AND Fiscal_Year='" + carton_fiscal_year+ "'", con);
                DataTable dt = new DataTable();
                sda1.Fill(dt);
                con.Close();
                DateTime bill = Convert.ToDateTime(dt.Rows[0]["Date_Of_Billing"].ToString());
                if (dtissue < bill)
                {
                    this.ErrorBox("Carton number: " + carton_no[i] + " at row " + (i + 1).ToString() + " has Date of Billing (" + bill.Date.ToString("dd-MM-yyyy") + " later than given Date of Issue (" + dtissue.Date.ToString("dd-MM-yyyy") + "),", "Error");
                    return false;
                }
            }
            try
            {
                for (int i = 0; i < number; i++)
                {
                    this.sendCartonTwist(carton_no[i], 2, issueDate, carton_fiscal_year);
                }
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                string sql = "INSERT INTO Twist_Voucher (Date_Of_Input, Date_Of_Issue, Quality, Company_Name, Carton_No_Arr, Number_of_Cartons, Carton_Fiscal_Year, Fiscal_Year) VALUES ('"+inputDate+"', '" + issueDate + "','" + quality + "', '" + company + "','" + cartonno + "', " + number + ", '"+carton_fiscal_year+"', '"+fiscal_year+"')";
                Console.WriteLine(sql);
                adapter.InsertCommand = new SqlCommand(sql, con);
                adapter.InsertCommand.ExecuteNonQuery();
                this.SuccessBox("Voucher Added Successfully");
            }
            catch (Exception e)
            {
                this.ErrorBox("Could not add carton voucher (addTwistVoucher) \n" + e.Message, "Exception");
                con.Close();
                return false;
            }

            finally
            {
                con.Close();
            }
            return true;
        }
        public bool deleteTwistVoucher(int voucherID)
        {
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                //Send all Previous Cartons to state 1
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT Carton_No_Arr, Carton_Fiscal_Year FROM Twist_Voucher WHERE Voucher_ID=" + voucherID + "", con);
                DataTable old = new DataTable();
                sda.Fill(old);
                con.Close();
                string old_carton_nos = old.Rows[0]["Carton_No_Arr"].ToString();
                string carton_fiscal_year = old.Rows[0]["Carton_Fiscal_Year"].ToString();
                this.sendCartonTwist(removecom(old_carton_nos), 1, null ,carton_fiscal_year);
               
                con.Open();
                string sql = "UPDATE Twist_Voucher SET Deleted=1 WHERE Voucher_ID=" + voucherID;
                //Console.WriteLine(sql);
                adapter.InsertCommand = new SqlCommand(sql, con);
                adapter.InsertCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                this.ErrorBox("Could not Delete twist voucher (deleteTwistVoucher) \n" + e.Message, "Exception");
                con.Close();
                return false;
            }

            finally
            {
                con.Close();
            }
            return true;
        }
        public bool editTwistVoucher(int voucherID, DateTime dtissue, string quality, string company, string cartonno, int number, string carton_fiscal_year)
        {
            string issueDate = dtissue.Date.ToString("MM-dd-yyyy").Substring(0, 10);
            string fiscal_year = this.getFinancialYear(dtissue);
            string[] carton_no = this.csvToArray(cartonno);
            //check if bill dates of all cartons are <= issue date
            for (int i = 0; i < carton_no.Length; i++)
            {
                con.Open();
                SqlDataAdapter sda1 = new SqlDataAdapter("SELECT Date_Of_Billing FROM Carton WHERE Carton_No='" + carton_no[i] + "' AND Fiscal_Year='" + carton_fiscal_year + "'", con);
                DataTable dt = new DataTable();
                sda1.Fill(dt);
                con.Close();
                DateTime bill = Convert.ToDateTime(dt.Rows[0]["Date_Of_Billing"].ToString());
                if (dtissue < bill)
                {
                    this.ErrorBox("Carton number: " + carton_no[i] + " at row " + (i + 1).ToString() + " has Date of Billing (" + bill.Date.ToString("dd-MM-yyyy") + ") later than given Date of Issue (" + dtissue.Date.ToString("dd-MM-yyyy") + "),", "Error");
                    return false;
                }
            }
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                //Delete all Previous Cartons
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT Carton_No_Arr FROM Twist_Voucher WHERE Voucher_ID=" + voucherID + "", con);
                DataTable old = new DataTable();
                sda.Fill(old);
                con.Close();
                string[] old_carton_nos = this.csvToArray(old.Rows[0][0].ToString());
                for (int i = 0; i < old_carton_nos.Length; i++)
                {
                    this.sendCartonTwist(old_carton_nos[i], 1, null, carton_fiscal_year);
                }
                //Add all New Cartons

                for (int i = 0; i < carton_no.Length; i++)
                {
                    this.sendCartonTwist(carton_no[i], 2, issueDate, carton_fiscal_year);
                }
                con.Open();
                string sql = "UPDATE Twist_Voucher SET Date_Of_Issue='" + issueDate + "', Quality='" + quality + "', Company_Name='" + company + "', Number_of_Cartons= " + number + ", Carton_No_Arr='" + cartonno + "', Fiscal_Year = '" + fiscal_year + "' WHERE Voucher_ID='" + voucherID + "'";
                //Console.WriteLine(sql);
                adapter.InsertCommand = new SqlCommand(sql, con);
                adapter.InsertCommand.ExecuteNonQuery();
                this.SuccessBox("Voucher Edited Successfully");
            }
            catch (Exception e)
            {
                this.ErrorBox("Could not edit twist voucher (editTwistVoucher) \n" + e.Message, "Exception");
                con.Close();
                return false;
            }

            finally
            {
                con.Close();
            }
            return true;
        }

        //Sales voucher
        public bool addSalesVoucher(DateTime dtinput, DateTime dtissue, string type, string quality, string company, string cartonno, string customer, float sell_cost, string carton_fiscal_year, string sale_do_no, string tablename, float net_weight)
        {
            string inputDate = dtinput.Date.ToString("MM-dd-yyyy").Substring(0, 10);
            string issueDate = dtissue.Date.ToString("MM-dd-yyyy").Substring(0, 10);
            string fiscal_year = this.getFinancialYear(dtissue);
            string[] carton_no = this.csvToArray(cartonno);
            
            //check if bill dates/Production Dates of all cartons are <= issue date
            DataTable dt = new DataTable();
            if (tablename=="Carton")
            {
                con.Open();
                SqlDataAdapter sda1 = new SqlDataAdapter("SELECT Carton_No, Date_Of_Billing FROM Carton WHERE Carton_No IN (" + removecom(cartonno) + ") AND Fiscal_Year='" + carton_fiscal_year + "'", con);
                sda1.Fill(dt);
                con.Close();
            }
            else
            {
                con.Open();
                SqlDataAdapter sda1 = new SqlDataAdapter("SELECT Carton_No, Date_Of_Production FROM Carton_Produced WHERE Carton_No IN (" + removecom(cartonno) + ") AND Fiscal_Year='" + carton_fiscal_year + "'", con);
                sda1.Fill(dt);
                con.Close();
            }

            //create a dictionary of values and the output values are not in the same order
            Dictionary<string, string> dates = new Dictionary<string, string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string thiscartonno = dt.Rows[i]["Carton_No"].ToString();
                if (tablename == "Carton") dates[thiscartonno] = dt.Rows[i]["Date_Of_Billing"].ToString();
                else dates[thiscartonno] = dt.Rows[i]["Date_Of_Production"].ToString();
            }
            for (int i = 0; i < carton_no.Length; i++)
            {
                DateTime bill = Convert.ToDateTime(dates[carton_no[i]]);
                string billprod = "Billing";
                if (tablename == "Carton_Produced") billprod = "Production";
                if (dtissue < bill)
                {
                    this.ErrorBox("Carton number: " + carton_no[i] + " at row " + (i + 1).ToString() + " has Date of " + billprod + " (" + bill.Date.ToString("dd-MM-yyyy").Substring(0, 10) + ") earlier than given Date of Issue (" + dtissue.Date.ToString("dd-MM-yyyy").Substring(0, 10) + ")", "Error");
                    return false;
                }
            }

            try
            {
                string carton_nos = "";
                for (int i = 0; i < carton_no.Length; i++)
                {
                    carton_nos += carton_no[i] + ",";
                }
                this.sendCartonSale(removecom(carton_nos), issueDate, sell_cost, sale_do_no, tablename, type, carton_fiscal_year, fiscal_year);
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                string sql = "INSERT INTO Sales_Voucher (Date_Of_Input,Date_Of_Sale, Quality, Company_Name, Customer, Sale_Rate, Carton_No_Arr, Fiscal_Year, Carton_Fiscal_Year, Type_Of_Sale, Tablename, Sale_DO_No, Net_Weight, Printed) VALUES ('" + inputDate+"' ,'" + issueDate + "','" + quality + "', '" + company + "', '" + customer + "', " + sell_cost + " , '" + cartonno + "', '"+fiscal_year+"', '"+carton_fiscal_year+"', '"+int.Parse(type)+"', '"+tablename+"', '"+sale_do_no+"', "+net_weight+", 0)";
                Console.WriteLine(sql);
                adapter.InsertCommand = new SqlCommand(sql, con);
                adapter.InsertCommand.ExecuteNonQuery();
                con.Close();

                con.Open();
                //Enter max carton number in Fiscal Year Table
                if(type=="0")
                {
                    sql = "UPDATE Fiscal_Year SET Highest_0_DO_No='" + sale_do_no + "' WHERE Fiscal_Year='" + carton_fiscal_year + "'";
                }
                else if(type=="1")
                {
                    sql = "UPDATE Fiscal_Year SET Highest_1_DO_No='" + sale_do_no + "' WHERE Fiscal_Year='" + carton_fiscal_year + "'";
                }

                adapter.InsertCommand = new SqlCommand(sql, con);
                adapter.InsertCommand.ExecuteNonQuery();
                this.SuccessBox("Voucher Added Successfully");
            }
            catch (Exception e)
            {
                this.ErrorBox("Could not add carton voucher (addSalesVoucher) \n" + e.Message, "Exception");
                con.Close();
                return false;
            }

            finally
            {
                con.Close();
            }
            return true;
        }
        public bool editSalesVoucher(int voucherID, DateTime dtissue, string type, string quality, string company, string cartonno, string customer, float sell_cost, string carton_fiscal_year, string sale_do_no, string tablename, float net_weight)
        {
            string issueDate = dtissue.Date.ToString("MM-dd-yyyy").Substring(0, 10);
            string fiscal_year = this.getFinancialYear(dtissue);
            string[] carton_no = this.csvToArray(cartonno);
            DataTable dt = new DataTable();
            if (tablename == "Carton")
            {
                con.Open();
                SqlDataAdapter sda1 = new SqlDataAdapter("SELECT Carton_No, Date_Of_Billing FROM Carton WHERE Carton_No IN (" + removecom(cartonno) + ") AND Fiscal_Year='" + carton_fiscal_year + "'", con);
                sda1.Fill(dt);
                con.Close();
            }
            else
            {
                con.Open();
                SqlDataAdapter sda1 = new SqlDataAdapter("SELECT Carton_No, Date_Of_Production FROM Carton_Produced WHERE Carton_No IN (" + removecom(cartonno) + ") AND Fiscal_Year='" + carton_fiscal_year + "'", con);
                sda1.Fill(dt);
                con.Close();
            }

            //create a dictionary of values and the output values are not in the same order
            Dictionary<string, string> dates = new Dictionary<string, string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string thiscartonno = dt.Rows[i]["Carton_No"].ToString();
                if (tablename == "Carton") dates[thiscartonno] = dt.Rows[i]["Date_Of_Billing"].ToString();
                else dates[thiscartonno] = dt.Rows[i]["Date_Of_Production"].ToString();
            }
            for (int i = 0; i < carton_no.Length; i++)
            {
                DateTime bill = Convert.ToDateTime(dates[carton_no[i]]);
                string billprod = "Billing";
                if (tablename == "Carton_Produced") billprod = "Production";
                if (dtissue < bill)
                {
                    this.ErrorBox("Carton number: " +carton_no[i] + " at row " + (i + 1).ToString() + " has Date of " + billprod + " (" + bill.Date.ToString("dd-MM-yyyy").Substring(0,10) + ") earlier than given Date of Issue (" + dtissue.Date.ToString("dd-MM-yyyy").Substring(0, 10) + ")", "Error");
                    return false;
                }
            }

            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                //Delete all Previous Cartons
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT Carton_No_Arr FROM Sales_Voucher WHERE Voucher_ID=" + voucherID + "", con);
                DataTable old = new DataTable();
                sda.Fill(old);
                con.Close();
                string old_carton_nos = old.Rows[0][0].ToString();
                this.sendCartonSale(removecom(old_carton_nos), null, -1F, null, tablename, type, carton_fiscal_year, null);

                //Add all New Cartons
                string carton_nos = "";
                for (int i = 0; i < carton_no.Length; i++)
                {
                    carton_nos += carton_no[i] + ",";
                }
                this.sendCartonSale(removecom(carton_nos), issueDate, sell_cost, sale_do_no, tablename, type, carton_fiscal_year, fiscal_year);

                con.Open();
                string sql = "UPDATE Sales_Voucher SET Date_Of_Sale='" + issueDate + "', Quality='" + quality + "', Company_Name='" + company + "', Carton_No_Arr='" + cartonno + "', Customer='"+customer+"', Sale_Rate="+sell_cost+", Fiscal_Year='"+fiscal_year+"', Type_Of_Sale = "+int.Parse(type)+", Net_Weight="+net_weight+"  WHERE Voucher_ID='" + voucherID + "' AND Tablename = '"+tablename+"'";
                //Console.WriteLine(sql);
                adapter.InsertCommand = new SqlCommand(sql, con);
                adapter.InsertCommand.ExecuteNonQuery();
                con.Close();

                //con.Open();
                ////Get higest do number in this financial year
                //dt = new DataTable();
                //if (type=="0")
                //{
                //    SqlDataAdapter sda1 = new SqlDataAdapter("SELECT Highest_0_DO_No FROM Fiscal_Year WHERE Fiscal_Year='" + carton_fiscal_year + "'", con);
                //    sda1.Fill(dt);
                //}
                //else if(type=="1")
                //{
                //    SqlDataAdapter sda1 = new SqlDataAdapter("SELECT Highest_1_DO_No FROM Fiscal_Year WHERE Fiscal_Year='" + carton_fiscal_year + "'", con);
                //    sda1.Fill(dt);
                //}
                //int old_max_do_no = int.Parse(dt.Rows[0][0].ToString().Substring(2, dt.Rows[0][0].ToString().Length - 2));
                //con.Close();

                //Console.WriteLine("selected12");


                //if (old_max_do_no < int.Parse(sale_do_no.Substring(2,sale_do_no.Length-2)))
                //{
                //    con.Open();
                //    //Enter max carton number in Fiscal Year Table
                //    if(type=="0")
                //    {
                //        sql = "UPDATE Fiscal_Year SET Highest_0_DO_No='" + sale_do_no + "' WHERE Fiscal_Year='" + carton_fiscal_year + "'";
                //    }
                //    else if(type=="1")
                //    {
                //        sql = "UPDATE Fiscal_Year SET Highest_1_DO_No='" + sale_do_no + "' WHERE Fiscal_Year='" + carton_fiscal_year + "'";
                //    }
                //    adapter.InsertCommand = new SqlCommand(sql, con);
                //    adapter.InsertCommand.ExecuteNonQuery();
                //}

                this.SuccessBox("Voucher Edited Successfully");
            }
            catch (Exception e)
            {
                this.ErrorBox("Could not edit sales voucher (editSalesVoucher) \n" + e.Message, "Exception");
                con.Close();
                return false;
            }

            finally
            {
                con.Close();
            }
            return true;
        }
        public bool deleteSalesVoucher(int voucherID)
        {
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                //Send all Previous Cartons to state 1
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT Carton_No_Arr, Tablename, Type_Of_Sale, Carton_Fiscal_Year FROM Sales_Voucher WHERE Voucher_ID=" + voucherID + "", con);
                DataTable old = new DataTable();
                sda.Fill(old);
                con.Close();
                string old_carton_nos = old.Rows[0][0].ToString();
                this.sendCartonSale(removecom(old_carton_nos), null, -1F, null, old.Rows[0]["Tablename"].ToString(), old.Rows[0]["Type_Of_Sale"].ToString(), old.Rows[0]["Carton_Fiscal_Year"].ToString(), null);

                con.Open();
                string sql = "UPDATE Sales_Voucher SET Deleted = 1  WHERE Voucher_ID='" + voucherID + "'";
                //Console.WriteLine(sql);
                adapter.InsertCommand = new SqlCommand(sql, con);
                adapter.InsertCommand.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception e)
            {
                this.ErrorBox("Could not edit sales voucher (editSalesVoucher) \n" + e.Message, "Exception");
                con.Close();
                return false;
            }

            finally
            {
                con.Close();
            }
            return true;
        }
        public DataTable getDOTable(string fiscal_year, int type)
        {
            DataTable dt = new DataTable(); //this is creating a virtual table  
            try
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Sales_Voucher WHERE Type_Of_Sale=" + type + " AND Fiscal_Year='" + fiscal_year + "'", con);
                sda.Fill(dt);
            }
            catch (Exception e)
            {
                this.ErrorBox("Could not get weight (getDOTable) \n" + e.Message, "Exception");
            }
            finally
            {
                con.Close();
            }
            return dt;
        }
        public DataTable getDOTable(string fiscal_year, string do_no)
        {
            DataTable dt = new DataTable(); //this is creating a virtual table  
            try
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Sales_Voucher WHERE Sale_DO_No='" + do_no + "' AND Fiscal_Year='" + fiscal_year + "'", con);
                sda.Fill(dt);
            }
            catch (Exception e)
            {
                this.ErrorBox("Could not get weight (getDOTable2) \n" + e.Message, "Exception");
            }
            finally
            {
                con.Close();
            }
            return dt;
        }

        //Add Bill to Sales Voucher
        public string[] getDO_QualityFiscalYearType(string quality, string do_fiscal_year, string type, string tablename)
        {
            //Returns -1 if carton not found
            string[] ans=null;
            try
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT Sale_DO_No FROM Sales_Voucher WHERE Quality='" + quality + "' AND Fiscal_Year='" + do_fiscal_year + "' AND Type_of_Sale='"+type+"' AND Tablename='"+tablename+"' AND Sale_Bill_No IS NULL", con);
                DataTable dt = new DataTable();
                Console.WriteLine("SELECT Sale_DO_No FROM Sales_Voucher WHERE Quality='" + quality + "' AND Fiscal_Year='" + do_fiscal_year + "' AND Type_of_Sale='" + type + "' AND Tablename='" + tablename + "' AND Sale_Bill_No IS NULL");
                sda.Fill(dt);
                string temp = "";
                for (int i=0;i<dt.Rows.Count;i++)
                {
                    temp += dt.Rows[i][0].ToString() + ",";
                }
                ans = this.csvToArray(temp);
            }
            catch (Exception e)
            {
                this.ErrorBox("Could not connect to database (getCartonState) " + e.Message, "Exception");
            }
            finally
            {
                con.Close();
            }
            return ans;
        }
        public DataRow getSalesRow(string do_no, string fiscal_year)
        {
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Sales_Voucher WHERE Sale_DO_No='" + do_no + "' AND Fiscal_Year='" + fiscal_year + "'", con);
                sda.Fill(dt);
            }
            catch (Exception e)
            {
                this.ErrorBox("Could not connect to database (getCartonState) " + e.Message, "Exception");
            }
            finally
            {
                con.Close();
            }
            return (DataRow)dt.Rows[0];
        }
        public bool addSalesBillNosVoucher(DateTime dtinputDate, DateTime dtbillDate, string do_nos, string quality, string do_fiscal_year, int type, string billNumber, float billWeight, float billAmount, float billWeight_calc, float billAmount_calc, string tablename, string customer)
        {
            string input_date = dtinputDate.Date.ToString("MM-dd-yyyy").Substring(0, 10);
            string bill_date = dtbillDate.Date.ToString("MM-dd-yyyy").Substring(0, 10);
            string fiscal_year = this.getFinancialYear(dtinputDate);
            //add bill nos to DOs
            string[] dos = this.csvToArray(do_nos);
            string do_no = "";
            for (int i = 0; i < dos.Length; i++)
            {
                do_no += "'" + dos[i] + "',";
            }
            bool added=addBillNoDate_Sales(removecom(do_no), billNumber, bill_date, do_fiscal_year, tablename);
            if(added==false)
            {
                return false;
            }
            //save voucher
            try
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                string sql;
                if(type==0)
                {
                    sql = "INSERT INTO SalesBillNos_Voucher (Date_Of_Input, Sale_Bill_Date, DO_No_Arr, Quality, DO_Fiscal_Year, Fiscal_Year, Type_Of_Sale, Sale_Bill_No, Sale_Bill_Weight, Sale_Bill_Amount, Sale_Bill_Weight_Calc, Sale_Bill_Amount_Calc, Tablename, Bill_Customer) VALUES ('" + input_date + "','" + bill_date + "','" + do_nos + "','" + quality + "','" + do_fiscal_year + "', '" + fiscal_year + "', " + type + ", '" + billNumber + "', " + billWeight + ", " + billAmount + ", " + billWeight_calc + ", " + billAmount_calc + ", '" + tablename + "', '" + customer + "')";
                }
                else
                {
                    sql = "INSERT INTO SalesBillNos_Voucher (Date_Of_Input, Sale_Bill_Date, DO_No_Arr, Quality, DO_Fiscal_Year, Fiscal_Year, Type_Of_Sale, Sale_Bill_No, Sale_Bill_Weight, Sale_Bill_Amount, Sale_Bill_Weight_Calc, Sale_Bill_Amount_Calc, Tablename) VALUES ('" + input_date + "','" + bill_date + "','" + do_nos + "','" + quality + "','" + do_fiscal_year + "', '" + fiscal_year + "', " + type + ", '" + billNumber + "', " + billWeight + ", " + billAmount + ", " + billWeight_calc + ", " + billAmount_calc + ", '" + tablename + "')";
                }
                Console.WriteLine(sql);
                adapter.InsertCommand = new SqlCommand(sql, con);
                adapter.InsertCommand.ExecuteNonQuery();
                this.SuccessBox("Voucher Added Successfully");
            }
            catch (Exception e)
            {
                this.ErrorBox("Could not add Sales Bill Number Voucher(addSalesBillNosVoucher) \n" + e.Message, "Exception");
                con.Close();
                return false;
            }
            finally
            {
                con.Close();
            }
            return true;
        }
        public bool editSalesBillNosVoucher(int voucherID, DateTime dtinputDate, DateTime dtbillDate, string do_nos, string do_fiscal_year, string billNumber, float billWeight, float billAmount, float billWeight_calc, float billAmount_calc, string tablename, string customer)
        {
            string bill_date = dtbillDate.Date.ToString("MM-dd-yyyy").Substring(0, 10);
            string fiscal_year = this.getFinancialYear(dtinputDate);
            //Get all do_nos which were previously present
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("SELECT DO_No_Arr FROM SalesBillNos_Voucher WHERE Voucher_ID=" + voucherID + "", con);
            DataTable old = new DataTable();
            sda.Fill(old);
            con.Close();
            string[] old_do_nos = this.csvToArray(old.Rows[0][0].ToString());
            string old_dos = "";
            for (int i = 0; i < old_do_nos.Length; i++)
            {
                old_dos += "'" + old_do_nos[i] + "',";
            }
            //send old do nos to bill no NULL
            addBillNoDate_Sales(removecom(old_dos), null, null, do_fiscal_year, tablename);


            //add bill nos to current batches
            string[] dos = this.csvToArray(do_nos);
            string do_no = "";
            for (int i = 0; i < dos.Length; i++)
            {
                do_no += "'" + dos[i] + "',";
            }
            addBillNoDate_Sales(removecom(do_no), billNumber, bill_date, do_fiscal_year, tablename);

            //update voucher
            try
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                string sql;
                if (customer!=null)     //tyoe 0
                {
                    sql = "UPDATE SalesBillNos_Voucher SET Sale_Bill_Date='" + bill_date + "', DO_No_Arr='" + do_nos + "', Fiscal_Year='" + fiscal_year + "', Sale_Bill_No='" + billNumber + "', Sale_Bill_Weight=" + billWeight + ", Sale_Bill_Amount=" + billAmount + ", Sale_Bill_Weight_Calc=" + billWeight_calc + ", Sale_Bill_Amount_Calc=" + billAmount_calc + ", Bill_Customer = '"+customer+"' WHERE Voucher_ID=" + voucherID + "";
                }
                else
                {
                    sql = "UPDATE SalesBillNos_Voucher SET Sale_Bill_Date='" + bill_date + "', DO_No_Arr='" + do_nos + "', Fiscal_Year='" + fiscal_year + "', Sale_Bill_No='" + billNumber + "', Sale_Bill_Weight=" + billWeight + ", Sale_Bill_Amount=" + billAmount + ", Sale_Bill_Weight_Calc=" + billWeight_calc + ", Sale_Bill_Amount_Calc=" + billAmount_calc + ", Bill_Customer = NULL WHERE Voucher_ID=" + voucherID + "";
                }
                Console.WriteLine(sql);
                adapter.InsertCommand = new SqlCommand(sql, con);
                adapter.InsertCommand.ExecuteNonQuery();
                this.SuccessBox("Voucher Updated Successfully");
            }
            catch (Exception e)
            {
                this.ErrorBox("Could not update Dyeing Inward Voucher (editBillNosVoucher) \n" + e.Message, "Exception");
                con.Close();
                return false;
            }
            finally
            {
                con.Close();
            }
            return true;
        }
        public bool addBillNoDate_Sales(string do_nos, string billNumber, string bill_date, string do_fiscal_year, string tablename)
        {
            if (string.IsNullOrEmpty(do_nos)) return false;
            try
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                string sql;
                if (billNumber == null && bill_date==null)
                {
                    sql = "UPDATE Sales_Voucher SET Sale_Bill_No=NULL, Sale_Bill_Date=NULL WHERE Sale_DO_No IN (" + do_nos + ") AND Fiscal_Year='" + do_fiscal_year + "' AND Tablename='"+tablename+"'";
                }
                else
                {
                    sql = "UPDATE Sales_Voucher SET Sale_Bill_No='" + billNumber + "', Sale_Bill_Date='"+bill_date+"' WHERE Sale_DO_No IN (" + do_nos + ") AND Fiscal_Year='" + do_fiscal_year + "' AND Tablename='" + tablename + "'";
                }
                Console.WriteLine(sql);
                adapter.InsertCommand = new SqlCommand(sql, con);
                adapter.InsertCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                this.ErrorBox("Could not add BillNo (addBillNoDate_Sales)\n" + e.Message, "Exception");
                con.Close();
                return false;
            }
            finally
            {
                con.Close();
            }
            return true;
        }
        public bool deleteSalesBillNosVoucher(int voucherID)
        {
            //Get all do_nos which were previously present
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("SELECT DO_No_Arr, DO_Fiscal_Year, Tablename FROM SalesBillNos_Voucher WHERE Voucher_ID=" + voucherID + "", con);
            DataTable old = new DataTable();
            sda.Fill(old);
            con.Close();
            string old_do_nos = old.Rows[0]["DO_No_Arr"].ToString();
            string[] do_nos = this.csvToArray(old_do_nos);
            old_do_nos = "";
            for(int i=0;i<do_nos.Length;i++)
            {
                old_do_nos += "'" + do_nos[i] + "',";
            }
            //send old do nos to bill date and number to NULL
            addBillNoDate_Sales(removecom(old_do_nos), null, null, old.Rows[0]["DO_Fiscal_Year"].ToString(), old.Rows[0]["Tablename"].ToString());

            //update voucher
            try
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                string sql = "UPDATE SalesBillNos_Voucher SET Deleted=1 WHERE Voucher_ID=" + voucherID + "";
                Console.WriteLine(sql);
                adapter.InsertCommand = new SqlCommand(sql, con);
                adapter.InsertCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                this.ErrorBox("Could not delete Sales Bill Number Voucher(deleteSalesBillNosVoucher) \n" + e.Message, "Exception");
                con.Close();
                return false;
            }
            finally
            {
                con.Close();
            }
            return true;
        }

        //Tray
        public int addTrayActive(string tray_production_date, string tray_no, string spring, int number_of_springs, float tray_tare, float gross_weight, string quality, string company_name, float net_weight, string fiscal_year, string machine_no, string quality_before_twist, string grade, int redyeing=-1)
        {
            //Adds to table Tray_Active
            //Returns the unique Tray_ID for the entered tray
            int ans = -1;
            try
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                string sql="";
                if(redyeing==-1)
                {
                    sql = "INSERT INTO Tray_Active (Tray_Production_Date, Tray_No, Spring, Number_Of_Springs, Tray_Tare, Gross_Weight, Quality, Company_Name, Tray_State, Net_Weight, Fiscal_Year, Machine_No, Quality_Before_Twist, Grade) VALUES ('" + tray_production_date + "', '" + tray_no + "', '" + spring + "', " + number_of_springs + " , " + tray_tare + ", " + gross_weight + ", '" + quality + "', '" + company_name + "', 1, " + net_weight + ", '" + fiscal_year + "', '" + machine_no + "', '" + quality_before_twist + "', '"+grade+"')";
                }
                else if(redyeing==1)
                {
                    sql = "INSERT INTO Tray_Active (Tray_Production_Date, Tray_No, Spring, Number_Of_Springs, Tray_Tare, Gross_Weight, Quality, Company_Name, Tray_State, Net_Weight, Fiscal_Year, Machine_No, Quality_Before_Twist, Redyeing) VALUES ('" + tray_production_date + "', '" + tray_no + "', '" + spring + "', " + number_of_springs + " , " + tray_tare + ", " + gross_weight + ", '" + quality + "', '" + company_name + "', 1, " + net_weight + ", '" + fiscal_year + "', '" + machine_no + "', '" + quality_before_twist + "', "+redyeing+")";
                }
                Console.WriteLine(sql);
                adapter.InsertCommand = new SqlCommand(sql, con);
                adapter.InsertCommand.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT Tray_ID FROM Tray_Active WHERE Tray_No='" + tray_no + "'",con);
                sda.Fill(dt);
                if (dt.Rows.Count != 0) ans = int.Parse(dt.Rows[0][0].ToString());
            }
            catch (Exception e)
            {
                this.ErrorBox("Could not add tray voucher (addTrayVoucher) \n" + e.Message, "Exception");
            }
            finally
            {
                con.Close();
            }
            return ans;
        }
        public int getTrayState(string tray_no)
        {
            int ans = -1;
            try
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT Tray_State FROM Tray_Active WHERE Tray_No='" + tray_no + "'", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count != 0) ans = int.Parse(dt.Rows[0][0].ToString());

            }
            catch (Exception e)
            {
                this.ErrorBox("Could not check tray (checkTray) \n" + e.Message, "Exception");
            }

            finally
            {
                con.Close();
            }
            return ans;
        }
        public int getTrayState(int tray_id)
        {
            int ans = -1;
            try
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT Tray_State FROM Tray_Active WHERE Tray_ID=" + tray_id + "", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count != 0) ans = int.Parse(dt.Rows[0][0].ToString());

            }
            catch (Exception e)
            {
                this.ErrorBox("Could not check tray (checkTray) \n" + e.Message, "Exception");
            }

            finally
            {
                con.Close();
            }
            return ans;
        }
        public int getTrayID(string tray_no)
        {
            int ans = -1;
            try
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT Tray_ID FROM Tray_Active WHERE Tray_No='" + tray_no + "'", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count != 0) ans = int.Parse(dt.Rows[0][0].ToString());

            }
            catch (Exception e)
            {
                this.ErrorBox("Could not get tray id tray (getTrayID) \n" + e.Message, "Exception");
            }

            finally
            {
                con.Close();
            }
            return ans;
        }
        public bool freeTray(string tray_ids, string date)
        {
            if (string.IsNullOrEmpty(tray_ids)) return false;
            try
            {
                //Update Dyeing date in Tray_Active
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                string sql = "UPDATE Tray_Active SET Dyeing_In_Date='" + date + "' WHERE Tray_ID IN (" + tray_ids + ")";
                Console.WriteLine(sql);
                adapter.InsertCommand = new SqlCommand(sql, con);
                adapter.InsertCommand.ExecuteNonQuery();

                //Select all rows from Tray_Active
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Tray_Active WHERE Tray_ID IN (" + tray_ids + ")", con);
                sda.Fill(dt);

                //Remove Tray_State column
                dt.Columns.Remove("Tray_State");
                for(int i=0; i<dt.Rows.Count; i++)
                {
                    //Store all rows in variables
                    int trayid = int.Parse(dt.Rows[i]["Tray_ID"].ToString());
                    
                    //Change date in correct format
                    string productiondate = dt.Rows[i]["Tray_Production_Date"].ToString().Substring(0, 10);
                    productiondate = productiondate.Replace('/', '-');
                    Console.WriteLine(productiondate);
                    DateTime d = DateTime.ParseExact(productiondate, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                    productiondate = d.ToString("MM-dd-yyyy");

                    //Change date in correct format
                    string Dyeing_In_Date = dt.Rows[i]["Dyeing_In_Date"].ToString().Substring(0, 10);
                    Dyeing_In_Date = Dyeing_In_Date.Replace('/', '-');
                    Console.WriteLine(Dyeing_In_Date);
                    d = DateTime.ParseExact(Dyeing_In_Date, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                    Dyeing_In_Date = d.ToString("MM-dd-yyyy");

                    //Change date in correct format
                    string Dyeing_Out_Date = dt.Rows[i]["Dyeing_Out_Date"].ToString().Substring(0, 10);
                    Dyeing_Out_Date = Dyeing_Out_Date.Replace('/', '-');
                    d = DateTime.ParseExact(Dyeing_Out_Date, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                    Dyeing_Out_Date = d.ToString("MM-dd-yyyy");

                    string trayno = dt.Rows[i]["Tray_No"].ToString();
                    string spring = dt.Rows[i]["Spring"].ToString();
                    int Number_Of_Springs = int.Parse(dt.Rows[i]["Number_Of_Springs"].ToString());
                    float Tray_Tare = float.Parse(dt.Rows[i]["Tray_Tare"].ToString());
                    float Gross_Weight = float.Parse(dt.Rows[i]["Gross_Weight"].ToString());
                    string Quality = dt.Rows[i]["Quality"].ToString();
                    string Company_Name = dt.Rows[i]["Company_Name"].ToString();
                    string Dyeing_Company_Name = dt.Rows[i]["Dyeing_Company_Name"].ToString();
                    string grade = dt.Rows[i]["Grade"].ToString();



                    int Batch_No = int.Parse(dt.Rows[i]["Batch_No"].ToString());
                    float Net_Weight = float.Parse(dt.Rows[i]["Net_Weight"].ToString());
                    string fiscal_year = dt.Rows[i]["Fiscal_Year"].ToString();
                    string machine_no = dt.Rows[i]["Machine_No"].ToString();
                    string quality_before_twist = dt.Rows[i]["Quality_Before_Twist"].ToString();
                    string batch_fiscal_year = dt.Rows[i]["Batch_Fiscal_Year"].ToString();
                    string redyeing;
                    try
                    {
                        int.Parse(dt.Rows[i]["Redyeing"].ToString());
                        redyeing = dt.Rows[i]["Redyeing"].ToString();
                    }
                    catch
                    {
                        redyeing = null;
                    }
                    //Put that row in Tray_History
                    if(redyeing!=null)
                    {
                        sql = "INSERT INTO Tray_History (Tray_ID, Tray_Production_Date, Tray_No, Spring, Number_Of_Springs, Tray_Tare, Gross_Weight, Quality, Company_Name, Dyeing_Company_Name, Dyeing_In_Date, Dyeing_Out_Date, Batch_No, Net_Weight, Fiscal_Year, Machine_No, Quality_Before_Twist, Batch_Fiscal_Year, Redyeing, Grade) VALUES (" + trayid + ", '" + productiondate + "', '" + trayno + "', '" + spring + "', " + Number_Of_Springs + ", " + Tray_Tare + ", " + Gross_Weight + ", '" + Quality + "', '" + Company_Name + "', '" + Dyeing_Company_Name + "', '" + Dyeing_In_Date + "', '" + Dyeing_Out_Date + "', " + Batch_No + ", " + Net_Weight + ", '" + fiscal_year + "', '" + machine_no + "', '" + quality_before_twist + "', '" + batch_fiscal_year + "', " + int.Parse(redyeing) + ", '"+grade+"')";
                    }
                    else
                    {
                        sql = "INSERT INTO Tray_History (Tray_ID, Tray_Production_Date, Tray_No, Spring, Number_Of_Springs, Tray_Tare, Gross_Weight, Quality, Company_Name, Dyeing_Company_Name, Dyeing_In_Date, Dyeing_Out_Date, Batch_No, Net_Weight, Fiscal_Year, Machine_No, Quality_Before_Twist, Batch_Fiscal_Year, Redyeing, Grade) VALUES (" + trayid + ", '" + productiondate + "', '" + trayno + "', '" + spring + "', " + Number_Of_Springs + ", " + Tray_Tare + ", " + Gross_Weight + ", '" + Quality + "', '" + Company_Name + "', '" + Dyeing_Company_Name + "', '" + Dyeing_In_Date + "', '" + Dyeing_Out_Date + "', " + Batch_No + ", " + Net_Weight + ", '" + fiscal_year + "', '" + machine_no + "', '" + quality_before_twist + "', '" + batch_fiscal_year + "', NULL, '"+grade+"')";
                    }
                    Console.WriteLine(sql);
                    sda.InsertCommand = new SqlCommand(sql, con);
                    sda.InsertCommand.ExecuteNonQuery();
                }
                
                //Remove that row from Tray_Active
                SqlDataAdapter sda2 = new SqlDataAdapter();
                sql = "DELETE FROM Tray_Active WHERE Tray_ID IN (" + tray_ids + ")";
                adapter.InsertCommand = new SqlCommand(sql, con);
                adapter.InsertCommand.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                this.ErrorBox("Could not free tray(freeTray) \n" + e.Message, "Exception");
                con.Close();
                return false;
            }
            finally
            {
                con.Close();
            }
            return true;
        }
        public bool unfreeTray(string tray_ids)
        {
            if (string.IsNullOrEmpty(tray_ids)) return false;
            try
            {
                con.Open();

                //Select and Remove all rows from Tray_History
                DataTable dt = new DataTable();
                string sql= "DELETE FROM Tray_History OUTPUT DELETED.* WHERE Tray_ID IN (" + tray_ids+")";
                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                sda.Fill(dt);

                //add them to tray_active
                for (int i=0; i<dt.Rows.Count; i++)
                {
                    //Store all rows in variables
                    int trayid = int.Parse(dt.Rows[i]["Tray_ID"].ToString());
                    
                    //Change date in correct format
                    string productiondate = dt.Rows[i]["Tray_Production_Date"].ToString().Substring(0, 10);
                    productiondate = productiondate.Replace('/', '-');
                    DateTime d = DateTime.ParseExact(productiondate, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                    productiondate = d.ToString("MM-dd-yyyy");

                    //Change date in correct format
                    string Dyeing_Out_Date = dt.Rows[i]["Dyeing_Out_Date"].ToString().Substring(0, 10);
                    Dyeing_Out_Date = Dyeing_Out_Date.Replace('/', '-');
                    d = DateTime.ParseExact(Dyeing_Out_Date, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                    Dyeing_Out_Date = d.ToString("MM-dd-yyyy");

                    string trayno = dt.Rows[i]["Tray_No"].ToString();
                    string spring = dt.Rows[i]["Spring"].ToString();
                    int Number_Of_Springs = int.Parse(dt.Rows[i]["Number_Of_Springs"].ToString());
                    float Tray_Tare = float.Parse(dt.Rows[i]["Tray_Tare"].ToString());
                    float Gross_Weight = float.Parse(dt.Rows[i]["Gross_Weight"].ToString());
                    string Quality = dt.Rows[i]["Quality"].ToString();
                    string Company_Name = dt.Rows[i]["Company_Name"].ToString();
                    string Dyeing_Company_Name = dt.Rows[i]["Dyeing_Company_Name"].ToString();
                    string grade = dt.Rows[i]["Grade"].ToString();


                    int Batch_No = int.Parse(dt.Rows[i]["Batch_No"].ToString());
                    float Net_Weight = float.Parse(dt.Rows[i]["Net_Weight"].ToString());
                    string fiscal_year = dt.Rows[i]["Fiscal_Year"].ToString();
                    string machine_no = dt.Rows[i]["Machine_No"].ToString();
                    string quality_before_twist = dt.Rows[i]["Quality_Before_Twist"].ToString();
                    string batch_fiscal_year = dt.Rows[i]["Batch_Fiscal_Year"].ToString();
                    string redyeing;
                    try
                    {
                        int.Parse(dt.Rows[i]["Redyeing"].ToString());
                        redyeing = dt.Rows[i]["Redyeing"].ToString();
                    }
                    catch
                    {
                        redyeing = null;
                    }
                    //Put that row in Tray_Active with state 2 (It is in dyeing) and Dyeing_In_Date NULL
                    if(redyeing!=null)
                    {
                        sql = "SET IDENTITY_INSERT Tray_Active ON; INSERT INTO Tray_Active (Tray_ID, Tray_Production_Date, Tray_No, Spring, Number_Of_Springs, Tray_Tare, Gross_Weight, Quality, Company_Name, Dyeing_Company_Name, Dyeing_In_Date, Dyeing_Out_Date, Batch_No, Net_Weight, Tray_State, Fiscal_Year, Machine_No, Quality_Before_Twist, Batch_Fiscal_Year, Redyeing, Grade) VALUES (" + trayid + ", '" + productiondate + "', '" + trayno + "', '" + spring + "', " + Number_Of_Springs + ", " + Tray_Tare + ", " + Gross_Weight + ", '" + Quality + "', '" + Company_Name + "', '" + Dyeing_Company_Name + "', NULL, '" + Dyeing_Out_Date + "', " + Batch_No + ", " + Net_Weight + ", 2, '" + fiscal_year + "', '" + machine_no + "', '" + quality_before_twist + "', '" + batch_fiscal_year + "', " + int.Parse(redyeing) + ", '"+grade+"'); SET IDENTITY_INSERT Tray_Active OFF";
                    }
                    else
                    {
                        sql = "SET IDENTITY_INSERT Tray_Active ON; INSERT INTO Tray_Active (Tray_ID, Tray_Production_Date, Tray_No, Spring, Number_Of_Springs, Tray_Tare, Gross_Weight, Quality, Company_Name, Dyeing_Company_Name, Dyeing_In_Date, Dyeing_Out_Date, Batch_No, Net_Weight, Tray_State, Fiscal_Year, Machine_No, Quality_Before_Twist, Batch_Fiscal_Year, Redyeing, Grade) VALUES (" + trayid + ", '" + productiondate + "', '" + trayno + "', '" + spring + "', " + Number_Of_Springs + ", " + Tray_Tare + ", " + Gross_Weight + ", '" + Quality + "', '" + Company_Name + "', '" + Dyeing_Company_Name + "', NULL, '" + Dyeing_Out_Date + "', " + Batch_No + ", " + Net_Weight + ", 2, '" + fiscal_year + "', '" + machine_no + "', '" + quality_before_twist + "', '" + batch_fiscal_year + "', NULL, '" + grade + "'); SET IDENTITY_INSERT Tray_Active OFF";
                    }
                    sda.InsertCommand = new SqlCommand(sql, con);
                    sda.InsertCommand.ExecuteNonQuery();
                }
                con.Close();
            }
            catch (Exception e)
            {
                this.ErrorBox("Could not unfree tray(unfreeTray) \n" + e.Message, "Exception");
                con.Close();
                return false;
            }
            finally
            {
                con.Close();
            }
            return true;
        }
        public void sendTraytoDyeing(string tray_nos, int state, string dyeing_out_date, string dyeing_company, int batchno, string batch_fiscal_year)
        {
            if (string.IsNullOrEmpty(tray_nos)) return;
            try
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                string sql;
                if(dyeing_out_date == null && dyeing_company== null && batchno == 0)
                {
                    sql = "UPDATE Tray_Active SET Tray_State=" + state + ", Dyeing_Out_Date=NULL, Dyeing_Company_Name=NULL, Batch_No=NULL, Batch_Fiscal_Year=NULL WHERE Tray_No IN (" + tray_nos + ")";
                }
                else sql = "UPDATE Tray_Active SET Tray_State=" + state + ", Dyeing_Out_Date='"+dyeing_out_date+"', Dyeing_Company_Name='"+dyeing_company+"', Batch_No="+batchno+", Batch_Fiscal_Year = '"+batch_fiscal_year+"' WHERE Tray_No='" + tray_nos + "'";
                Console.WriteLine(sql);
                adapter.InsertCommand = new SqlCommand(sql, con);
                adapter.InsertCommand.ExecuteNonQuery();
                //MessageBox.Show("Voucher Edited Successfully", "Success");
            }
            catch (Exception e)
            {
                this.ErrorBox("Could not edit tray state (changeTrayState) \n" + e.Message, "Exception");
            }

            finally
            {
                con.Close();
            }
        }
        public void sendTraytoDyeing_TrayID(string tray_ids, int state, string dyeing_out_date, string dyeing_company, int batchno, string batch_fiscal_year)
        {
            if (string.IsNullOrEmpty(tray_ids)) return;
            try
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                string sql;
                if (dyeing_out_date == null && dyeing_company == null && batchno == 0)
                {
                    sql = "UPDATE Tray_Active SET Tray_State=" + state + ", Dyeing_Out_Date=NULL, Dyeing_Company_Name=NULL, Batch_No=NULL, Batch_Fiscal_Year=NULL WHERE Tray_ID IN (" + tray_ids + ")";
                }
                else sql = "UPDATE Tray_Active SET Tray_State=" + state + ", Dyeing_Out_Date='" + dyeing_out_date + "', Dyeing_Company_Name='" + dyeing_company + "', Batch_No=" + batchno + ", Batch_Fiscal_Year = '" + batch_fiscal_year + "' WHERE Tray_ID IN (" + tray_ids + ")";
                //Console.WriteLine(sql);
                adapter.InsertCommand = new SqlCommand(sql, con);
                adapter.InsertCommand.ExecuteNonQuery();
                //MessageBox.Show("Voucher Edited Successfully", "Success");
            }
            catch (Exception e)
            {
                this.ErrorBox("Could not edit tray state (changeTrayState) \n" + e.Message, "Exception");
            }

            finally
            {
                con.Close();
            }
        }
        public DataTable getTrayTable_TrayID(int tray_id)
        {
            DataTable dt = new DataTable(); //this is creating a virtual table  
            try
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Tray_Active WHERE Tray_ID=" + tray_id, con);
                sda.Fill(dt);
                if(dt.Rows.Count==0)
                {
                    dt.Clear();
                    dt.Columns.Clear();
                    SqlDataAdapter sda2 = new SqlDataAdapter("SELECT * FROM Tray_History WHERE Tray_ID=" + tray_id, con);
                    sda2.Fill(dt);
                }
                //Console.WriteLine("Got Data");
            }
            catch (Exception e)
            {
                this.ErrorBox("Could not connect to database (getTrayTable_TrayID) " + e.Message, "Exception");
            }
            finally
            {
                con.Close();
            }
            return dt;
        }
        public int getTraySprings(int tray_id)
        {
            int ans = -1;
            try
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT Number_of_Springs FROM Tray_History WHERE Tray_ID=" + tray_id + "", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if(dt.Rows.Count==0)
                {
                    SqlDataAdapter sda2 = new SqlDataAdapter("SELECT Number_of_Springs FROM Tray_Active WHERE Tray_ID=" + tray_id + "", con);
                    sda2.Fill(dt);
                }
                if (dt.Rows.Count != 0) ans = int.Parse(dt.Rows[0][0].ToString());
            }
            catch (Exception e)
            {
                this.ErrorBox("Could not get tray springs (getTraySprings) \n" + e.Message, "Exception");
            }
            finally
            {
                con.Close();
            }
            return ans;
        }
        public DataRow getTrayRow_TrayID(int tray_id)
        {
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Tray_Active WHERE Tray_ID=" + tray_id + "", con);
                sda.Fill(dt);
                if(dt.Rows.Count==0)
                {
                    SqlDataAdapter sda1 = new SqlDataAdapter("SELECT * FROM Tray_History WHERE Tray_ID=" + tray_id + "", con);
                    sda1.Fill(dt);
                }
            }
            catch (Exception e)
            {
                this.ErrorBox("Could not connect to database (getTrayRow_TrayID) \n" + e.Message, "Exception");
                con.Close();
                return null;
            }
            finally
            {
                con.Close();

            }
            return (DataRow)dt.Rows[0];
        }
        public DataTable getTrayDataBothTables(string cols, string where)
        {
            DataTable dt = new DataTable(); //this is creating a virtual table
            try
            {
                con.Open();
                string sql = "SELECT " + cols + " FROM Tray_Active WHERE " + where + " UNION SELECT " + cols + " FROM Tray_History WHERE " + where;
                SqlDataAdapter sda = new SqlDataAdapter(sql , con);
                sda.Fill(dt);
            }
            catch (Exception e)
            {
                this.ErrorBox("Could not connect to database (getTrayDataBothTables) WHERE " + where + " \n" + e.Message, "Exception");
                con.Close();
                return new DataTable();
            }
            finally
            {
                con.Close();

            }
            return dt;
        }

        //Tray voucher
        public bool addTrayVoucher(DateTime dtinput_date, DateTime dttray_production_date, string tray_no, string spring, int number_of_springs, float tray_tare, float gross_weight, string quality, string company_name, float net_weight, string machine_no, string quality_before_twist, string grade)
        {
            string input_date = dtinput_date.Date.ToString("MM-dd-yyyy");
            string tray_production_date = dttray_production_date.Date.ToString("MM-dd-yyyy");
            string fiscal_year = this.getFinancialYear(dttray_production_date);
            //check if tray is present in Tray_Active
            int tray_state = getTrayState(tray_no);
            if (tray_state==1 || tray_state== 2)
            {
                this.ErrorBox("Tray " + tray_no.ToString() + " is already in use", "Error");
                return false;
            }
            //insert into Tray_Active and get unique tray_id
            int tray_id = addTrayActive(tray_production_date, tray_no, spring, number_of_springs, tray_tare, gross_weight, quality, company_name, net_weight, fiscal_year, machine_no, quality_before_twist, grade);
            //insert into Tray_Voucher with unique tray_id
            try
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                string sql = "INSERT INTO Tray_Voucher (Input_Date, Tray_Production_Date, Tray_No, Spring, Number_Of_Springs, Tray_Tare, Gross_Weight, Quality, Company_Name, Tray_ID, Net_Weight, Fiscal_Year, Machine_No, Quality_Before_Twist, Grade) VALUES ('" + input_date + "','" + tray_production_date + "', '" + tray_no + "', '" + spring + "', " + number_of_springs + " , " + tray_tare+ ", " + gross_weight + ", '"+quality+"', '"+company_name+"', "+tray_id+", "+net_weight+", '"+fiscal_year+"', '"+machine_no+"', '"+quality_before_twist+"', '"+grade+"')";
                Console.WriteLine(sql);
                adapter.InsertCommand = new SqlCommand(sql, con);
                adapter.InsertCommand.ExecuteNonQuery();
                this.SuccessBox("Voucher Added Successfully");
            }
            catch (Exception e)
            {
                this.ErrorBox("Could not add tray voucher (addTrayVoucher) \n" + e.Message, "Exception");
                con.Close();
                return false;
            }
            finally
            {
                con.Close();
            }
            return true;
        }
        public bool editTrayVoucher(int voucher_id, int tray_id, DateTime dtinput_date, DateTime dttray_production_date, string new_tray_no, string old_tray_no, string spring, int number_of_springs, float tray_tare, float gross_weight, string quality, string company_name, float net_weight, string machine_no, string quality_before_twist, string grade)
        {
            string input_date = dtinput_date.Date.ToString("MM-dd-yyyy");
            string tray_production_date = dttray_production_date.Date.ToString("MM-dd-yyyy");
            string fiscal_year = this.getFinancialYear(dttray_production_date);
            //The data you are getting has already been checked for if the voucher has been processed or has gone for dyeing
            //This means that the unique tray_id is present in the Voucher_Active table with state 1
            //check if tray is present in Tray_Active
            int tray_state = getTrayState(new_tray_no);
            if (tray_state != -1 && old_tray_no != new_tray_no)
            {
                this.ErrorBox("Tray " + new_tray_no.ToString() + " is already in use", "Error");
                return false;
            }
            //insert into Tray_Voucher with unique tray_id
            try
            {

                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                SqlDataAdapter sda = new SqlDataAdapter();
                string sql = "UPDATE Tray_Voucher SET Tray_Production_Date='" + tray_production_date + "', Tray_No='" + new_tray_no+ "', Spring='"+spring+"', Number_Of_Springs="+number_of_springs+", Tray_Tare="+tray_tare+", Gross_Weight="+gross_weight+", Quality='"+quality+"',  Company_Name='" + company_name + "', Net_Weight=" + net_weight + ", Fiscal_Year = '"+fiscal_year+"', Machine_No='"+machine_no+"', Quality_Before_Twist='"+quality_before_twist+"', Grade = '"+grade+"' WHERE Voucher_ID='" + voucher_id + "'";
                Console.WriteLine(sql);
                adapter.InsertCommand = new SqlCommand(sql, con);
                adapter.InsertCommand.ExecuteNonQuery();

                sql = "UPDATE Tray_Active SET Tray_Production_Date='" + tray_production_date + "', Tray_No='" + new_tray_no + "', Spring='" + spring + "', Number_Of_Springs=" + number_of_springs + ", Tray_Tare=" + tray_tare + ", Gross_Weight=" + gross_weight + ", Quality='" + quality + "',  Company_Name='" + company_name + "', Net_Weight="+net_weight+", Fiscal_Year = '"+fiscal_year+"', Machine_No='"+machine_no+"', Quality_Before_Twist='"+quality_before_twist+ "', Grade = '" + grade + "' WHERE Tray_ID='" + tray_id + "'";
                Console.WriteLine(sql);
                sda.InsertCommand = new SqlCommand(sql, con);
                sda.InsertCommand.ExecuteNonQuery();

                this.SuccessBox("Voucher Edited Successfully");
            }
            catch (Exception e)
            {
                this.ErrorBox("Could not edit tray voucher (editTrayVoucher) \n" + e.Message, "Exception");
                con.Close();
                return false;
            }
            finally
            {
                con.Close();
            }
            return true;
        }
        public bool deleteTrayVoucher(int voucher_id)
        {
            //insert into Tray_Voucher with unique tray_id
            try
            {

                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                SqlDataAdapter sda = new SqlDataAdapter();
                string sql = "UPDATE Tray_Voucher SET Deleted=1 WHERE Voucher_ID='" + voucher_id + "'";
                Console.WriteLine(sql);
                adapter.InsertCommand = new SqlCommand(sql, con);
                adapter.InsertCommand.ExecuteNonQuery();

                DataTable dt = new DataTable();
                SqlDataAdapter sda1 = new SqlDataAdapter("SELECT Tray_ID FROM Tray_Voucher WHERE Voucher_ID='" + voucher_id + "'", con);
                sda1.Fill(dt);

                sql = "DELETE FROM Tray_Active WHERE Tray_ID='" + dt.Rows[0]["Tray_ID"].ToString() + "'";
                Console.WriteLine(sql);
                sda.InsertCommand = new SqlCommand(sql, con);
                sda.InsertCommand.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                this.ErrorBox("Could not delete tray voucher (deleteTrayVoucher) \n" + e.Message, "Exception");
                con.Close();
                return false;
            }
            finally
            {
                con.Close();
            }
            return true;
        }


        //Dyenig Issue Voucher
        public bool addDyeingIssueVoucher(DateTime dtinputDate, DateTime dtissueDate, string quality, string company_name, string trayno, int number, string colour, string dyeing_company_name, int batchno, string trayid, float net_wt, float rate)
        {
            string inputDate = dtinputDate.Date.ToString("MM-dd-yyyy").Substring(0, 10);
            string issueDate = dtissueDate.Date.ToString("MM-dd-yyyy").Substring(0, 10);
            string fiscal_year = this.getFinancialYear(dtissueDate);
            //check if tray production dates of all trays are <= issue date
            string[] tray_nos = this.csvToArray(trayno);
            string[] tray_ids = this.csvToArray(trayid);
            for (int i = 0; i < tray_nos.Length; i++)
            {
                con.Open();
                SqlDataAdapter sda1 = new SqlDataAdapter("SELECT Tray_Production_Date FROM Tray_Active WHERE Tray_ID='" + tray_ids[i] + "' ", con);
                DataTable dt = new DataTable();
                sda1.Fill(dt);
                con.Close();
                DateTime prod = Convert.ToDateTime(dt.Rows[0]["Tray_Production_Date"].ToString());
                if (dtissueDate < prod)
                {
                    this.ErrorBox("Tray Number: " + tray_nos[i] + " at row " + (i + 1).ToString() + " has Date of Production (" + prod.Date.ToString("dd-MM-yyyy") + " earlier than given Date of Issue (" + dtissueDate.Date.ToString("dd-MM-yyyy") + "),", "Error");
                    return false;
                }
            }
            //add batch
            bool batch_added = addBatch(batchno, colour, dyeing_company_name, issueDate, trayid, net_wt, quality, company_name, number, rate, fiscal_year);
            if (batch_added == false)
            {
                return false;
            }

            //change Tray_State in Tray_Active
            for(int i=0;i<tray_nos.Length;i++)
            {
                this.sendTraytoDyeing(tray_nos[i], 2, issueDate, dyeing_company_name, batchno, fiscal_year);
            }

            try
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                string sql = "INSERT INTO Dyeing_Issue_Voucher (Date_Of_Input, Date_Of_Issue, Quality, Company_Name, Colour, Dyeing_Company_Name, Batch_No, Tray_No_Arr, Number_Of_Trays, Dyeing_Rate, Tray_ID_Arr, Batch_Fiscal_Year) VALUES ('" + inputDate + "','" + issueDate + "', '" + quality + "', '" + company_name + "', '" + colour + "' , '" + dyeing_company_name + "', " + batchno + ", '" + trayno + "', "+number+", "+rate+", '"+trayid+"', '"+fiscal_year+"')";
                adapter.InsertCommand = new SqlCommand(sql, con);
                adapter.InsertCommand.ExecuteNonQuery();
                this.SuccessBox("Voucher Added Successfully");
            }
            catch (Exception e)
            {
                this.ErrorBox("Could not add dyeing issue voucher (addTrayVoucher) \n" + e.Message, "Exception");
                con.Close();
                return false;
            }
            finally
            {
                con.Close();
            }
            return true;
        }
        public bool editDyeingIssueVoucher(int voucherID, string old_fiscal_year, DateTime dtinputDate, DateTime dtissueDate, string quality, string company_name, string trayno, int number, string colour, string dyeing_company_name, int batchno, string trayid, float net_wt, float rate, string tray_id_arr)
        {
            string inputDate = dtinputDate.Date.ToString("MM-dd-yyyy").Substring(0, 10);
            string issueDate = dtissueDate.Date.ToString("MM-dd-yyyy").Substring(0, 10);
            string fiscal_year = this.getFinancialYear(dtissueDate);
            //check if tray production dates of all trays are <= issue date
            string[] tray_nos = this.csvToArray(trayno);
            string[] tray_ids = this.csvToArray(trayid);
            for (int i = 0; i < tray_nos.Length; i++)
            {
                con.Open();
                SqlDataAdapter sda1 = new SqlDataAdapter("SELECT Tray_Production_Date FROM Tray_Active WHERE Tray_ID='" + tray_ids[i] + "' ", con);
                DataTable dt = new DataTable();
                sda1.Fill(dt);
                con.Close();
                DateTime prod = Convert.ToDateTime(dt.Rows[0]["Tray_Production_Date"].ToString());
                if (dtissueDate < prod)
                {
                    this.ErrorBox("Tray Number: " + tray_nos[i] + " at row " + (i + 1).ToString() + " has Date of Production (" + prod.Date.ToString("dd-MM-yyyy") + " earlier than given Date of Issue (" + dtissueDate.Date.ToString("dd-MM-yyyy") + "),", "Error");
                    return false;
                }
            }

            //update batch
            updateBatch(batchno, colour, dyeing_company_name, issueDate, trayid, net_wt, quality, company_name, number, rate, fiscal_year, old_fiscal_year);

            //Send all previous trays in Tray_Active to state 1, clearing batch_no, dyeing_company_name, dyeing_issue_date
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("SELECT Tray_No_Arr FROM Dyeing_Issue_Voucher WHERE Voucher_ID=" + voucherID + "", con);
            DataTable old = new DataTable();
            sda.Fill(old);
            con.Close();
            string[] old_tray_nos = this.csvToArray(old.Rows[0][0].ToString());
            for (int i = 0; i < old_tray_nos.Length; i++)
            {
                this.sendTraytoDyeing(old_tray_nos[i], 1, null, null, 0, null);
            }

            //Send all current trays in Tray_Active to state 2, adding batch_no, dyeing_company_name, dyeing_issue_date
            for (int i = 0; i < tray_nos.Length; i++)
            {
                this.sendTraytoDyeing(tray_nos[i], 2, issueDate, dyeing_company_name, batchno, fiscal_year);
            }

            try
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                string sql = "UPDATE Dyeing_Issue_Voucher Set Date_Of_Issue='"+issueDate+"', Quality='"+quality+"', Company_Name='"+company_name+"', Colour='"+colour+"', Dyeing_Company_Name='"+dyeing_company_name+"', Batch_No="+batchno+", Tray_No_Arr='"+trayno+"', Number_Of_Trays=" + number+" , Dyeing_Rate = "+rate+", Tray_ID_Arr='"+tray_id_arr+"', Batch_Fiscal_Year = '"+fiscal_year+"' WHERE Voucher_ID="+voucherID;
                Console.WriteLine(sql);
                adapter.InsertCommand = new SqlCommand(sql, con);
                adapter.InsertCommand.ExecuteNonQuery();
                this.SuccessBox("Voucher Updated Successfully");
            }
            catch (Exception e)
            {
                this.ErrorBox("Could not edit dyeing issue voucher (editDyeingIssueVoucher) \n" + e.Message, "Exception");
                con.Close();
                return false;
            }
            finally
            {
                con.Close();
            }
            return true;
        }
        public bool deleteDyeingIssueVoucher(int voucherID)
        {
            try
            {
                //get previous batched, and update deleted column
                con.Open();
                string sql = "UPDATE Dyeing_Issue_Voucher SET Deleted = 1 OUTPUT inserted.Batch_No, inserted.Batch_Fiscal_Year WHERE Voucher_ID =" + voucherID;
                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                DataTable voucher = new DataTable();
                sda.Fill(voucher);
                int batch_no = int.Parse(voucher.Rows[0][0].ToString());
                string batch_fiscal_year = voucher.Rows[0][1].ToString();

                //get batch tray ids and delete batch
                sql = "DELETE from Batch OUTPUT DELETED.Tray_ID_Arr WHERE Batch_No=" + batch_no + " AND Fiscal_Year='" + batch_fiscal_year + "'";
                SqlDataAdapter sda2 = new SqlDataAdapter(sql, con);
                DataTable batch = new DataTable();
                sda2.Fill(batch);
                //adapter.InsertCommand = new SqlCommand(sql, con);
                //adapter.InsertCommand.ExecuteNonQuery();
                con.Close();
                string tray_ids = batch.Rows[0][0].ToString();
                this.sendTraytoDyeing_TrayID(removecom(tray_ids), 1, null, null, 0, null);
                this.SuccessBox("Voucher Deleted Successfully");
            }
            catch (Exception e)
            {
                this.ErrorBox("Could not Delete dyeing issue voucher (deleteDyeingIssueVoucher) \n" + e.Message, "Exception");
                con.Close();
                return false;
            }
            finally
            {
                con.Close();
            }
            return true;
        }
        
        //Batch
        public string getNextNumber_FiscalYear(string columnname,string financialyear)
        {
            int ans = -1;
            string ret="";
            try
            {
                con.Open();
                string sql = "SELECT * FROM Fiscal_Year WHERE Fiscal_Year='"+financialyear+"'";
                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                con.Close();
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if(dt.Rows.Count==0)
                {
                    con.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    sql = "INSERT INTO Fiscal_Year VALUES ('" + financialyear + "', 0 ,'0', 'BB0', 'RR0')";
                    adapter.InsertCommand = new SqlCommand(sql, con);
                    adapter.InsertCommand.ExecuteNonQuery();
                    
                    sql = "SELECT * FROM Fiscal_Year WHERE Fiscal_Year='" + financialyear + "'";
                    SqlDataAdapter sda1 = new SqlDataAdapter(sql, con);
                    sda1.Fill(dt);
                    con.Close();
                }
                try
                {
                    ans = 1 + int.Parse(dt.Rows[0][columnname].ToString());
                    ret = ans.ToString();
                }
                catch
                {
                    string do_no = dt.Rows[0][columnname].ToString();
                    int no = int.Parse(do_no.Substring(2, do_no.Length - 2));
                    no++;
                    ret = do_no.Substring(0, 2) + no.ToString();
                }
                
            }
            catch(Exception e)
            {
                this.ErrorBox("Cannot get next batch number (getNextBatchNumber)\n" + e.Message, "Exception");
            }
            finally
            {
                con.Close();
            }
            return ret;
        }
        public bool addBatch(int batch_no, string colour, string dyeing_company_name, string dyeing_out_date, string tray_id_arr, float net_wt, string quality, string company_name, int number, float rate, string fiscal_year)
        {
            try
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                string sql = "INSERT INTO Batch (Batch_No, Colour, Dyeing_Company_Name, Dyeing_Out_Date, Tray_ID_Arr, Net_Weight, Quality, Company_Name, Number_Of_Trays, Batch_State, Dyeing_Rate, Fiscal_Year) VALUES ("+batch_no+" ,'" + colour+ "', '" + dyeing_company_name + "', '" + dyeing_out_date + "', '" + tray_id_arr + "' , " + net_wt + ", '" + quality + "', '" + company_name + "', " + number + ", '1', "+rate+", '"+fiscal_year+"')";
                Console.WriteLine(sql);
                adapter.InsertCommand = new SqlCommand(sql, con);
                adapter.InsertCommand.ExecuteNonQuery();
                SqlDataAdapter adapter1 = new SqlDataAdapter();
                sql = "UPDATE Fiscal_Year SET Highest_Batch_No=" + batch_no + " WHERE Fiscal_Year='" + fiscal_year + "'";
                adapter1.InsertCommand = new SqlCommand(sql, con);
                adapter1.InsertCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                this.ErrorBox("Could not Batch (addBatch) \n" + e.Message, "Exception");
                con.Close();
                return false;
            }
            finally
            {
                con.Close();
            }
            return true;
        }
        public int getBatchState(int batch_no, string fiscal_year)
        {
            int ans = -1;
            DataTable dt = new DataTable(); //this is creating a virtual table  
            try
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT Batch_State FROM Batch WHERE Batch_No="+batch_no+" AND Fiscal_Year='"+fiscal_year+"'", con);
                sda.Fill(dt);
                if(dt.Rows.Count!=0)
                {
                    ans=int.Parse(dt.Rows[0][0].ToString());
                }
            }
            catch (Exception e)
            {
                this.ErrorBox("Could not connect to database (getBatchState) \n" + e.Message, "Exception");
            }
            finally
            {
                con.Close();

            }
            return ans;
        }
        public DataTable sendBatchStateDateBillNo(int batch_no, int state, string date, string bill_no, string batch_fiscal_year, string bill_date, string slip_no)
        {
            //returns datatable of tray_ids
            DataTable tray_ids = new DataTable();
            try
            {
                con.Open();
                string sql;
                if(date==null && bill_no=="-1" && bill_date==null)
                {
                    //to go back a state
                    sql = "UPDATE Batch SET Batch_State=" + state + ", Dyeing_In_Date=NULL, Bill_No=NULL, Bill_Date=NULL, Slip_No=NULL OUTPUT inserted.Tray_ID_Arr WHERE Batch_No=" + batch_no+" AND Fiscal_Year = '"+batch_fiscal_year+"'";
                }
                else
                {
                    sql = "UPDATE Batch SET Batch_State=" + state + ", Dyeing_In_Date='" + date + "', Bill_No='" + bill_no + "', Slip_No='"+slip_no+ "' OUTPUT inserted.Tray_ID_Arr WHERE Batch_No= " + batch_no + " AND Fiscal_Year = '" + batch_fiscal_year + "'";
                }
                SqlDataAdapter adapter = new SqlDataAdapter(sql,con);
                adapter.Fill(tray_ids);
            }
            catch (Exception e)
            {
                this.ErrorBox("Could not edit Batch State (sendBatchStateDateBillNo) \n"+e.Message, "Exception");
                con.Close();
                return new DataTable();
            }
            finally
            {
                con.Close();
            }
            return tray_ids;
        }
        public bool updateBatch(int batch_no, string colour, string dyeing_company_name, string dyeing_out_date, string tray_id_arr, float net_wt, string quality, string company_name, int number, float rate, string fiscal_year, string old_fiscal_year)
        {
            try
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                string sql = "UPDATE Batch Set Colour='"+colour+"', Dyeing_Company_Name='"+dyeing_company_name+"', Dyeing_Out_Date='"+dyeing_out_date+"', Tray_ID_Arr='"+tray_id_arr+"', Net_Weight="+net_wt+", Quality='"+quality+"', Company_Name='"+company_name+"', Number_Of_Trays="+number+", Batch_State=1, Dyeing_Rate="+rate+", Fiscal_Year = '"+fiscal_year+"' WHERE Batch_No="+batch_no+" AND Fiscal_Year = '"+old_fiscal_year+"'";
                Console.WriteLine(sql);
                adapter.InsertCommand = new SqlCommand(sql, con);
                adapter.InsertCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                this.ErrorBox("Could not Batch (addBatch) \n" + e.Message, "Exception");
                con.Close();
                return false;
            }
            finally
            {
                con.Close();
            }
            return true;
        }
        public DataTable getBatchFiscalYearWeight_StateDyeingCompanyColourQuality(int state, string dyeing_company, string colour, string quality)
        {
            DataTable dt = new DataTable(); //this is creating a virtual table  
            try
            {
                if(dyeing_company==null)
                {
                    con.Open();
                    SqlDataAdapter sda = new SqlDataAdapter("SELECT Batch_No, Fiscal_Year FROM Batch WHERE Batch_State=" + state + "", con);
                    sda.Fill(dt);
                }
                else
                {
                    con.Open();
                    SqlDataAdapter sda = new SqlDataAdapter("SELECT Batch_No, Fiscal_Year, Net_Weight FROM Batch WHERE Batch_State=" + state + " AND Dyeing_Company_Name='" + dyeing_company + "' AND Colour='" + colour + "' AND Quality='" + quality + "' ", con);
                    sda.Fill(dt);
                }
                if(dt.Rows.Count==0)
                {
                    return new DataTable();
                }
                
            }
            catch (Exception e)
            {
                this.ErrorBox("Could not connect to database (getBatchFiscalYear_StateDyeingCompanyColourQuality) " + e.Message, "Exception");
            }
            finally
            {
                con.Close();
            }
            return dt;
        }
        public void addBillNo_Batches(string batch_nos, string bill_no, string bill_date, string batch_fiscal_year)
        {
            if (string.IsNullOrEmpty(batch_nos)) return;
            try
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                string sql;
                if (bill_date == null)
                {
                    sql = "UPDATE Batch SET Bill_No='"+bill_no+"', Bill_Date = NULL WHERE Batch_No IN (" + batch_nos+") AND Fiscal_Year='"+batch_fiscal_year+"'";
                }
                else
                {
                    sql = "UPDATE Batch SET Bill_No='" + bill_no + "', Bill_Date = '"+bill_date+"' WHERE Batch_No IN (" + batch_nos+ ") AND Fiscal_Year='" + batch_fiscal_year + "'";
                }
                Console.WriteLine(sql);
                adapter.InsertCommand = new SqlCommand(sql, con);
                adapter.InsertCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                this.ErrorBox("Could not add BillNo (addBillNo)\n" + e.Message, "Exception");
            }
            finally
            {
                con.Close();
            }
        }
        public string getColumnBatchNo(string column, int batch_no, string fiscal_year)
        {
            string ans = null;
            try
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT "+column+" FROM Batch WHERE Batch_No=" + batch_no + " AND Fiscal_Year = '"+fiscal_year+"'", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count != 0)
                {
                    ans = dt.Rows[0][0].ToString();
                }
            }
            catch (Exception e)
            {
                this.ErrorBox("Could not connect to database (getColumnBatchNo) "+column+"\n" + e.Message, "Exception");
            }
            finally
            {
                con.Close();

            }
            return ans;
        }
        public DataTable getColumnBatchNos(string column, string batch_nos, string fiscal_year)
        {
            if (string.IsNullOrEmpty(batch_nos)) return new DataTable();
            DataTable ans = new DataTable();
            try
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT " + column + " FROM Batch WHERE Batch_No IN (" + batch_nos + ") AND Fiscal_Year = '" + fiscal_year + "'", con);
                sda.Fill(ans);
            }
            catch (Exception e)
            {
                this.ErrorBox("Could not connect to database (getColumnBatchNos) " + column + "\n" + e.Message, "Exception");
                con.Close();
                return new DataTable();
            }
            finally
            {
                con.Close();
            }
            return ans;
        }
        public DataRow getBatchRow_BatchNo(int batch_no, string fiscal_year)
        {
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Batch WHERE Batch_No=" + batch_no + " AND Fiscal_Year =  '"+fiscal_year+"'", con);
                sda.Fill(dt);
            }
            catch (Exception e)
            {
                this.ErrorBox("Could not connect to database (getBatchRow_BatchNo) \n" + e.Message, "Exception");
                con.Close();
                return null;
            }
            finally
            {
                con.Close();

            }
            return (DataRow)dt.Rows[0];
        }
        public DataTable getBatchTable_BatchNo(int batch_no, string fiscal_year)
        {
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Batch WHERE Batch_No=" + batch_no + " AND Fiscal_Year =  '" + fiscal_year + "'", con);
                sda.Fill(dt);
            }
            catch (Exception e)
            {
                this.ErrorBox("Could not connect to database (getBatchTable_BatchNo) \n" + e.Message, "Exception");
                con.Close();
                return new DataTable();
            }
            finally
            {
                con.Close();

            }
            return dt;
        }
        public string getFiscalYear_BatchNoState(string batch_no, int state)
        {
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT Fiscal_Year FROM Batch WHERE Batch_No=" + batch_no + " AND Batch_State =  '" + state + "'", con);
                sda.Fill(dt);
                if(dt.Rows.Count<=0)
                {
                    con.Close();
                    return null;
                }

            }
            catch (Exception e)
            {
                this.ErrorBox("Could not connect to database (getFiscalYear_BatchNoState) \n" + e.Message, "Exception");
                con.Close();
                return null; ;
            }
            finally
            {
                con.Close();
            }
            return dt.Rows[0][0].ToString();
        }
        public DataTable getBatchTable_State(int state)
        {
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Batch WHERE Batch_State=" + state, con);
                sda.Fill(dt);
            }
            catch (Exception e)
            {
                this.ErrorBox("Could not connect to database (getBatchTable_State) \n" + e.Message, "Exception");
                con.Close();
                return new DataTable();
            }
            finally
            {
                con.Close();
            }
            return dt;
        }
        public DataTable getBatchTable_BatchNoState(int batch_no, int state, string fiscal_year)
        {
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Batch WHERE Batch_No=" + batch_no + " AND Fiscal_Year ='" + fiscal_year + "' AND Batch_State=" + state, con);
                sda.Fill(dt);
            }
            catch (Exception e)
            {
                this.ErrorBox("Could not connect to database (getBatchTable_BatchNoState) \n" + e.Message, "Exception");
                con.Close();
                return new DataTable();
            }
            finally
            {
                con.Close();

            }
            return dt;
        }

        //Dye In Voucher
        public bool addDyeingInwardVoucher(DateTime dtinput_date, DateTime dtinward_date, string dyeing_company_name, string bill_no, string batch_no_arr, string batch_fiscal_year, string bill_date, string slip_no_arr)
        {
            string input_date = dtinput_date.Date.ToString("MM-dd-yyyy").Substring(0, 10);
            string inward_date = dtinward_date.Date.ToString("MM-dd-yyyy").Substring(0, 10);
            string fiscal_year = this.getFinancialYear(dtinward_date);
            
            //check if batch issue date of all batches are <= inward date
            string[] batchnos = csvToArray(batch_no_arr);
            string[] slipnos;
            if(slip_no_arr=="")
            {
                slipnos = null;
            }
            else
            {
                slipnos = csvToArray(slip_no_arr);
            }
            con.Open();
            SqlDataAdapter sda1 = new SqlDataAdapter("SELECT Batch_No, Dyeing_Out_Date FROM Batch WHERE Batch_No IN (" + removecom(batch_no_arr) + ") AND Fiscal_Year='" + batch_fiscal_year + "' ", con);
            DataTable dt = new DataTable();
            sda1.Fill(dt);
            con.Close();
            Dictionary<string, string> dyeingout = new Dictionary<string, string>();
            for (int i = 0; i < dt.Rows.Count; i++) dyeingout[dt.Rows[i]["Batch_No"].ToString()] = dt.Rows[i]["Dyeing_Out_Date"].ToString();
            for (int i = 0; i < batchnos.Length; i++)
            {
                DateTime outDate = Convert.ToDateTime(dyeingout[batchnos[i]]);
                if (dtinward_date < outDate)
                {
                    this.ErrorBox("Batch Number: " + batchnos[i] + " at row " + (i + 1).ToString() + " has Date of Issue to Dyeing (" + outDate.Date.ToString("dd-MM-yyyy") + ") earlier than given Inward date (" + dtinward_date.Date.ToString("dd-MM-yyyy") + "),", "Error");
                    return false;
                }
            }

            //Set Batch State to 2 for each batch, and get all tray ids
            string all_tray_ids = "";
            for (int i=0; i<batchnos.Length; i++)
            {
                DataTable tray_id = sendBatchStateDateBillNo(int.Parse(batchnos[i]), 2, inward_date, bill_no, batch_fiscal_year, bill_date, slipnos[i]);
                string i_tray_id = tray_id.Rows[0][0].ToString();
                all_tray_ids += i_tray_id;
            }
            freeTray(removecom(all_tray_ids), inward_date);
            
            //update voucher
            try
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                string sql = "INSERT INTO Dyeing_Inward_Voucher (Date_Of_Input, Inward_Date, Dyeing_Company_Name, Bill_No, Batch_No_Arr, Fiscal_Year, Batch_Fiscal_Year, Slip_No_Arr) VALUES ('" + input_date + "', '" + inward_date + "', '" + dyeing_company_name + "', '" + bill_no + "' , '" + batch_no_arr + "', '" + fiscal_year + "', '" + batch_fiscal_year + "', '"+slip_no_arr+"')";
                Console.WriteLine(sql);
                adapter.InsertCommand = new SqlCommand(sql, con);
                adapter.InsertCommand.ExecuteNonQuery();
                this.SuccessBox("Voucher Added Successfully");
            }
            catch (Exception e)
            {
                this.ErrorBox("Could not add Dyeing Inward Voucher (addDyeingInwardVoucher) \n" + e.Message, "Exception");
                con.Close();
                return false;
            }
            finally
            {
                con.Close();
            }
            return true;
        }
        public bool editDyeingInwardVoucher(int voucher_id, DateTime dtinput_date, DateTime dtinward_date, string dyeing_company_name, string bill_no, string batch_no_arr, string batch_fiscal_year, string bill_date, string slip_no_arr)
        {
            string input_date = dtinput_date.Date.ToString("MM-dd-yyyy").Substring(0, 10);
            string inward_date = dtinward_date.Date.ToString("MM-dd-yyyy").Substring(0, 10);
            string fiscal_year = this.getFinancialYear(dtinward_date);
            
            //check if batch issue date of all batches are <= inward date
            string[] batchnos = csvToArray(batch_no_arr);
            string[] slipnos;
            if (slip_no_arr == "")
            {
                slipnos = null;
            }
            else
            {
                slipnos = csvToArray(slip_no_arr);
            }
            con.Open();
            SqlDataAdapter sda1 = new SqlDataAdapter("SELECT Batch_No, Dyeing_Out_Date FROM Batch WHERE Batch_No IN (" + removecom(batch_no_arr) + ") AND Fiscal_Year='" + batch_fiscal_year + "' ", con);
            DataTable dt = new DataTable();
            sda1.Fill(dt);
            con.Close();
            Dictionary<string, string> dyeingout = new Dictionary<string, string>();
            for (int i = 0; i < dt.Rows.Count; i++) dyeingout[dt.Rows[i]["Batch_No"].ToString()] = dt.Rows[i]["Dyeing_Out_Date"].ToString();
            for (int i = 0; i < batchnos.Length; i++)
            {
                DateTime outDate = Convert.ToDateTime(dyeingout[batchnos[i]]);
                if (dtinward_date < outDate)
                {
                    this.ErrorBox("Batch Number: " + batchnos[i] + " at row " + (i + 1).ToString() + " has Date of Issue to Dyeing (" + outDate.Date.ToString("dd-MM-yyyy") + ") earlier than given Inward date (" + dtinward_date.Date.ToString("dd-MM-yyyy") + "),", "Error");
                    return false;
                }
            }

            //get old batch numbers
            //Get all batch_nos which were previously present
            SqlDataAdapter sda = new SqlDataAdapter("SELECT Batch_No_Arr FROM Dyeing_Inward_Voucher WHERE Voucher_ID=" + voucher_id + "", con);
            DataTable old = new DataTable();
            sda.Fill(old);
            string[] old_batch_nos = this.csvToArray(old.Rows[0][0].ToString());

            //send old batch numbers to state 1 and remove Dyeing_In_Date, get their tray ids 
            string old_batch_nos_str = old.Rows[0][0].ToString();
            DataTable tray_ids = new DataTable();
            string sql = "UPDATE Batch SET Batch_State=1, Dyeing_In_Date=NULL, Bill_No=NULL, Bill_Date=NULL, Slip_No=NULL OUTPUT inserted.Tray_ID_Arr WHERE Batch_No IN (" + removecom(old_batch_nos_str) + ") AND Fiscal_Year = '" + batch_fiscal_year + "'";
            SqlDataAdapter adapter = new SqlDataAdapter(sql, con);
            adapter.Fill(tray_ids);
            con.Close();
            string all_old_tray_ids = "";
            for(int i=0; i<tray_ids.Rows.Count; i++)
            {
                string trayids = tray_ids.Rows[i][0].ToString();
                all_old_tray_ids += trayids;
            }
            unfreeTray(removecom(all_old_tray_ids));
            
            //Add all the new batches and respective tray ids            
            //Set Batch State to 2 for each batch
            string all_tray_ids = "";
            for (int i = 0; i < batchnos.Length; i++)
            {
                DataTable tray_id = sendBatchStateDateBillNo(int.Parse(batchnos[i]), 2, inward_date, bill_no, batch_fiscal_year, bill_date, slipnos[i]);
                string i_tray_id = tray_id.Rows[0][0].ToString();
                all_tray_ids += i_tray_id;
            }
            freeTray(removecom(all_tray_ids), inward_date);

            try
            {
                con.Open();
                SqlDataAdapter adapter2 = new SqlDataAdapter();
                sql = "UPDATE Dyeing_Inward_Voucher SET Inward_Date = '" + inward_date + "' , Dyeing_Company_Name ='" + dyeing_company_name + "',  Bill_No ='" + bill_no + "' , Batch_No_Arr ='" + batch_no_arr + "', Fiscal_Year='" + fiscal_year + "', Bill_Date = NULL, Slip_No_Arr='"+slip_no_arr+"' WHERE Voucher_ID =" + voucher_id + "";
                adapter2.InsertCommand = new SqlCommand(sql, con);
                adapter2.InsertCommand.ExecuteNonQuery();
                this.SuccessBox("Voucher Edited Successfully");
            }
            catch (Exception e)
            {
                this.ErrorBox("Could not edit Dyeing Inward Voucher (editDyeingInwardVoucher) \n" + e.Message, "Exception");
                con.Close();
                return false;
            }
            finally
            {
                con.Close();
            }
            return true;
        }
        public bool deleteDyeingInwardVoucher(int voucher_id)
        {
            try
            {
                //Get batches present in the voucher and set deleted as true
                con.Open();
                string sql = "UPDATE Dyeing_Inward_Voucher SET Deleted = 1 OUTPUT inserted.Batch_No_Arr, inserted.Batch_Fiscal_Year WHERE Voucher_ID =" + voucher_id;
                Console.WriteLine(sql);
                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                DataTable old = new DataTable();
                sda.Fill(old);
                string batch_fiscal_year = old.Rows[0]["Batch_Fiscal_Year"].ToString();

                //send old batch numbers to state 1 and remove Dyeing_In_Date, get their tray ids 
                string old_batch_nos_str = old.Rows[0]["Batch_No_Arr"].ToString();
                DataTable tray_ids = new DataTable();
                sql = "UPDATE Batch SET Batch_State=1, Dyeing_In_Date=NULL, Bill_No=NULL, Bill_Date=NULL, Slip_No=NULL OUTPUT inserted.Tray_ID_Arr WHERE Batch_No IN (" + removecom(old_batch_nos_str) + ") AND Fiscal_Year = '" + batch_fiscal_year + "'";
                Console.WriteLine(sql);
                SqlDataAdapter adapter = new SqlDataAdapter(sql, con);
                adapter.Fill(tray_ids);
                string all_old_tray_ids = "";
                for (int i = 0; i < tray_ids.Rows.Count; i++)
                {
                    string trayids = tray_ids.Rows[i][0].ToString();
                    all_old_tray_ids += trayids;
                }
                con.Close();
                unfreeTray(removecom(all_old_tray_ids));
                this.SuccessBox("Voucher Deleted Successfully");
            }
            catch (Exception e)
            {
                this.ErrorBox("Could not delete Dyeing Inward Voucher (deleteDyeingInwardVoucher) \n" + e.Message, "Exception");
                con.Close();
                return false;
            }
            finally
            {
                con.Close();
            }
            return true;
        }

        //Bill Voucher
        public bool addBillNosVoucher(string sendbill_no, DateTime dtinput_date, string batch_nos, string dyeing_company, string batch_fiscal_year, DateTime dtbill_date, List<Tuple<string, DateTime>> dyeing_in_dates)
        {
            string input_date = dtinput_date.Date.ToString("MM-dd-yyyy").Substring(0, 10);
            string bill_date = dtbill_date.Date.ToString("MM-dd-yyyy").Substring(0, 10);
            //add bill nos to batches
            string batches = batch_nos;

            //check if bill date>= dyeingindates of all new batches
            for (int i = 0; i < dyeing_in_dates.Count; i++)
            {
                if (dtbill_date < dyeing_in_dates[i].Item2)
                {
                    this.ErrorBox("Batch No: " + dyeing_in_dates[i].Item1 + " Has Dyeing Inward Date: " + dyeing_in_dates[i].Item2.Date.ToString("dd-MM-yyyy") + " Later than Given Bill Date: " + dtbill_date.Date.ToString("dd-MM-yyyy"));
                    return false;
                }
            }
            addBillNo_Batches(removecom(batches), sendbill_no, bill_date, batch_fiscal_year);
            
            string fiscal_year = this.getFinancialYear(dtinput_date);
            //save voucher
            try
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                string sql = "INSERT INTO BillNos_Voucher (Date_Of_Input, Batch_No_Arr, Dyeing_Company_Name, Bill_No, Batch_Fiscal_Year, Fiscal_Year, Bill_Date) VALUES ('"+input_date+"','" + batch_nos + "','"+dyeing_company+"','"+sendbill_no+"','"+batch_fiscal_year+"', '"+fiscal_year+"', '"+bill_date+"')";
                Console.WriteLine(sql);
                adapter.InsertCommand = new SqlCommand(sql, con);
                adapter.InsertCommand.ExecuteNonQuery();
                this.SuccessBox("Voucher Added Successfully");
            }
            catch (Exception e)
            {
                this.ErrorBox("Could not add Bill Nos Voucher (addBillNosVoucher) \n" + e.Message, "Exception");
                con.Close();
                return false;
            }
            finally
            {
                con.Close();
            }
            return true;
        }
        public bool editBillNosVoucher(int voucher_id, string sendbill_no, DateTime dtinput_date, string batch_nos, string dyeing_company, string batch_fiscal_year, DateTime dtbill_date, List<Tuple<string, DateTime>> dyeing_in_dates)
        {
            string input_date = dtinput_date.Date.ToString("MM-dd-yyyy").Substring(0, 10);
            string bill_date = dtbill_date.Date.ToString("MM-dd-yyyy").Substring(0, 10);
            string fiscal_year = this.getFinancialYear(dtinput_date);
            //check if bill date>= dyeingindates of all new batches
            for (int i = 0; i < dyeing_in_dates.Count; i++)
            {
                if (dtbill_date < dyeing_in_dates[i].Item2)
                {
                    this.ErrorBox("Batch No: " + dyeing_in_dates[i].Item1 + " Has Dyeing Inward Date: " + dyeing_in_dates[i].Item2.Date.ToString("dd-MM-yyyy") + " Later than Given Bill Date: " + dtbill_date.Date.ToString("dd-MM-yyyy"));
                    return false;
                }
            }

            //Get all batch_nos which were previously present
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("SELECT Batch_No_Arr FROM BillNos_Voucher WHERE Voucher_ID=" + voucher_id + "", con);
            DataTable old = new DataTable();
            sda.Fill(old);
            con.Close();
            string old_batch_nos = old.Rows[0][0].ToString();

            //send old batch nos to bill no 0
            addBillNo_Batches(removecom(old_batch_nos), "0", null, batch_fiscal_year);

            //add bill nos to current batches
            string batches = batch_nos;
            addBillNo_Batches(removecom(batches), sendbill_no, bill_date, batch_fiscal_year);

            //update voucher
            try
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                string sql = "UPDATE BillNos_Voucher SET Batch_No_Arr='" + batch_nos + "', Dyeing_Company_Name='" + dyeing_company + "', Bill_No='"+sendbill_no+"', Fiscal_Year='"+fiscal_year+"', Bill_Date='"+bill_date+"' WHERE Voucher_ID="+voucher_id+"";
                Console.WriteLine(sql);
                adapter.InsertCommand = new SqlCommand(sql, con);
                adapter.InsertCommand.ExecuteNonQuery();
                this.SuccessBox("Voucher Updated Successfully");
            }
            catch (Exception e)
            {
                this.ErrorBox("Could not update Bill Nos Voucher (editBillNosVoucher) \n" + e.Message, "Exception");
                con.Close();
                return false;
            }
            finally
            {
                con.Close();
            }
            return true;
        }
        public bool deleteBillNosVoucher(int voucher_id)
        {
            try
            {
                //Get all batch_nos which were previously present and set deleted
                con.Open();
                string sql = "UPDATE BillNos_Voucher SET Deleted = 1 OUTPUT inserted.Batch_No_Arr, inserted.Batch_Fiscal_Year WHERE Voucher_ID =" + voucher_id;
                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                DataTable old = new DataTable();
                sda.Fill(old);
                con.Close();
                string old_batch_nos = old.Rows[0]["Batch_No_Arr"].ToString();
                string batch_fiscal_year = old.Rows[0]["Batch_Fiscal_Year"].ToString();
                //send old batch nos to bill no 0
                addBillNo_Batches(removecom(old_batch_nos), "0", null, batch_fiscal_year);
                this.SuccessBox("Voucher Deleted Successfully");
            }
            catch (Exception e)
            {
                this.ErrorBox("Could not delete Bill Nos Voucher (deleteBillNosVoucher) \n" + e.Message, "Exception");
                con.Close();
                return false;
            }
            finally
            {
                con.Close();
            }
            return true;
        }

        //Re-Dyeing Voucher
        public bool addRedyeingVoucher(DateTime dtinputDate, DateTime dtissueDate, DataRow old_batch_row, int NRD_batch_no, int RD_batch_no, float NRD_batch_weight, float RD_batch_weight, string RD_fiscal_year, DataTable trays, string RD_colour, float RD_rate)
        {
            string input_date = dtinputDate.Date.ToString("MM-dd-yyyy").Substring(0, 10);
            string issue_date = dtissueDate.Date.ToString("MM-dd-yyyy").Substring(0, 10);
            string tray_ids = "";
            //Add new trays
            for (int i=0;i<trays.Rows.Count;i++)
            {
                DateTime prod_date = Convert.ToDateTime(trays.Rows[i]["Date Of Production"].ToString());
                int tray_id = addTrayActive(prod_date.Date.ToString("MM-dd-yyyy").Substring(0,10), trays.Rows[i]["Tray No"].ToString(), trays.Rows[i]["Spring"].ToString(), int.Parse(trays.Rows[i]["No Of Springs"].ToString()), float.Parse(trays.Rows[i]["Tray Tare"].ToString()), float.Parse(trays.Rows[i]["Gross Weight"].ToString()), trays.Rows[i]["Quality"].ToString(), trays.Rows[i]["Company Name"].ToString(), float.Parse(trays.Rows[i]["Net Weight"].ToString()), this.getFinancialYear(prod_date), trays.Rows[i]["Machine No"].ToString(), trays.Rows[i]["Quality Before Twist"].ToString(), trays.Rows[i]["Grade"].ToString(), 1);
                this.sendTraytoDyeing(trays.Rows[i]["Tray No"].ToString(), 2, issue_date, old_batch_row["Dyeing_Company_Name"].ToString(), RD_batch_no, RD_fiscal_year);
                tray_ids += tray_id.ToString() + ",";
            }

            string redyeing = "";
            //Update old batch
            try
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                redyeing = NRD_batch_no.ToString() + "," + RD_batch_no.ToString() + "," + RD_fiscal_year + ",";
                string sql = "UPDATE Batch SET Batch_State = 4 , Redyeing = '"+redyeing+ "' WHERE Batch_No = '"+old_batch_row["Batch_No"].ToString()+ "' AND Fiscal_Year = '"+old_batch_row["Fiscal_Year"].ToString()+"'";
                Console.WriteLine(sql);
                adapter.InsertCommand = new SqlCommand(sql, con);
                adapter.InsertCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                this.ErrorBox("Could not update old Batch (addRedyeingVoucher) \n" + e.Message, "Exception");
                con.Close();
                return false;
            }
            finally
            {
                con.Close();
            }

            //Add new batches
            int bill_no;
            string bill_date, slip_no;
            if(old_batch_row["Bill_No"].ToString()=="0")
            {
                bill_no = 0;
                bill_date = null;
                slip_no = old_batch_row["Slip_No"].ToString();
            }
            else
            {
                bill_no =int.Parse(old_batch_row["Bill_No"].ToString());
                bill_date = old_batch_row["Bill_Date"].ToString();
                bill_date = bill_date.Replace('/', '-');
                DateTime dt = Convert.ToDateTime(bill_date);
                bill_date = dt.ToString("MM-dd-yyyy");
                slip_no = old_batch_row["Slip_No"].ToString();
            }

            string dyeing_out_date = old_batch_row["Dyeing_Out_Date"].ToString();
            dyeing_out_date = dyeing_out_date.Replace('/', '-');
            DateTime d = Convert.ToDateTime(dyeing_out_date);
            dyeing_out_date = d.ToString("MM-dd-yyyy");
            
            string dyeing_in_date = old_batch_row["Dyeing_In_Date"].ToString();
            dyeing_in_date = dyeing_in_date.Replace('/', '-');
            d = Convert.ToDateTime(dyeing_in_date);
            dyeing_in_date = d.ToString("MM-dd-yyyy");
            redyeing = old_batch_row["Batch_No"].ToString() + "," + old_batch_row["Fiscal_Year"].ToString() + ",";
            bool added1 = addRDBatch(NRD_batch_no, old_batch_row["Colour"].ToString(), old_batch_row["Dyeing_Company_Name"].ToString(), dyeing_out_date, null, NRD_batch_weight, old_batch_row["Quality"].ToString(), old_batch_row["Company_Name"].ToString(), 0, float.Parse(old_batch_row["Dyeing_Rate"].ToString()), this.getFinancialYear(dtissueDate), dyeing_in_date, 2, bill_no, bill_date, slip_no, redyeing);
            bool added2 = addRDBatch(RD_batch_no, RD_colour, old_batch_row["Dyeing_Company_Name"].ToString(), issue_date, tray_ids, RD_batch_weight, old_batch_row["Quality"].ToString(), old_batch_row["Company_Name"].ToString(), trays.Rows.Count, RD_rate, this.getFinancialYear(dtissueDate), null, 1, -1, null, null, redyeing);
            if(added1==false || added2==false)
            {
                return false;
            }
            
            //Add Redyeing Voucher
            try
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                string sql = "INSERT INTO Redyeing_Voucher (Date_Of_Input, Date_Of_Issue, Old_Batch_No, Old_Batch_Fiscal_Year, Non_Redyeing_Batch_No, Redyeing_Batch_No, Redyeing_Batch_Fiscal_Year) VALUES ('"+input_date+"', '"+issue_date+"', "+int.Parse(old_batch_row["Batch_No"].ToString())+", '"+old_batch_row["Fiscal_Year"].ToString()+"', "+NRD_batch_no+", "+RD_batch_no+ ", '" + this.getFinancialYear(dtissueDate) + "')";
                Console.WriteLine(sql);
                adapter.InsertCommand = new SqlCommand(sql, con);
                adapter.InsertCommand.ExecuteNonQuery();
                this.SuccessBox("Voucher Added Successfully");
            }
            catch (Exception e)
            {
                this.ErrorBox("Could not add Redyeing Voucher (addRedyeingVoucher) \n" + e.Message, "Exception");
                con.Close();
                return false;
            }
            finally
            {
                con.Close();
            }
            return true;
        }
        public bool editRedyeingVoucher(DateTime dtissueDate, DataRow old_batch_row, DataTable trays, string colour, float dyeing_rate, float NRD_batch_weight, float RD_batch_weight)
        {
            string tray_ids = "";
            string issue_date = dtissueDate.Date.ToString("MM-dd-yyyy").Substring(0, 10);
            string[] break_batches = this.csvToArray(old_batch_row["Redyeing"].ToString());
            //delete old trays
            try
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                string sql = "DELETE FROM Tray_Active WHERE Redyeing=1 AND Batch_No='"+break_batches[1]+"'";
                Console.WriteLine(sql);
                adapter.InsertCommand = new SqlCommand(sql, con);
                adapter.InsertCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                this.ErrorBox("Could not delete old trays(editRedyeingVoucher) \n" + e.Message, "Exception");
                con.Close();
                return false;
            }
            finally
            {
                con.Close();
            }

            //Add new trays
            for (int i = 0; i < trays.Rows.Count; i++)
            {
                DateTime prod_date = Convert.ToDateTime(trays.Rows[i]["Date Of Production"].ToString());
                int tray_id = addTrayActive(prod_date.Date.ToString("MM-dd-yyyy").Substring(0, 10), trays.Rows[i]["Tray No"].ToString(), trays.Rows[i]["Spring"].ToString(), int.Parse(trays.Rows[i]["No Of Springs"].ToString()), float.Parse(trays.Rows[i]["Tray Tare"].ToString()), float.Parse(trays.Rows[i]["Gross Weight"].ToString()), trays.Rows[i]["Quality"].ToString(), trays.Rows[i]["Company Name"].ToString(), float.Parse(trays.Rows[i]["Net Weight"].ToString()), this.getFinancialYear(prod_date), trays.Rows[i]["Machine No"].ToString(), trays.Rows[i]["Quality Before Twist"].ToString(), trays.Rows[i]["Grade"].ToString(), 1);
                this.sendTraytoDyeing(trays.Rows[i]["Tray No"].ToString(), 2, issue_date, old_batch_row["Dyeing_Company_Name"].ToString(), int.Parse(break_batches[1]), break_batches[2]);
                tray_ids += tray_id.ToString() + ",";
            }

            //Edit redyeing batches
            bool edited1 = editRDBatch(int.Parse(break_batches[0]), break_batches[2], NRD_batch_weight, null, 0, null, -1F);
            bool edited2 = editRDBatch(int.Parse(break_batches[1]), break_batches[2], RD_batch_weight, tray_ids, trays.Rows.Count, colour, dyeing_rate);
            if (edited1 == false || edited2 == false)
            {
                return false;
            }
            this.SuccessBox("Voucher Edited Successfully");
            return true;
        }
        public bool deleteRedyeingVoucher(int voucher_id)
        {
            DataTable dt = new DataTable();
            //Select voucher row and set its deleted = 1
            try
            {
                con.Open();
                string sql = "UPDATE Redyeing_Voucher SET Deleted = 1 OUTPUT inserted.* WHERE Voucher_ID =" + voucher_id;
                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                sda.Fill(dt);
                Console.WriteLine(sql);

            }
            catch (Exception e)
            {
                this.ErrorBox("Could not select redyeing voucher row (deleteRedyeingVoucher)\n" + e.Message, "Exception");
                con.Close();
                return false;
            }
            finally
            {
                con.Close();
            }

            //delete old redyeing trays
            try
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                string sql = "DELETE FROM Tray_Active WHERE Redyeing=1 AND Batch_No='" + dt.Rows[0]["Redyeing_Batch_No"].ToString() + "'";
                Console.WriteLine(sql);
                adapter.InsertCommand = new SqlCommand(sql, con);
                adapter.InsertCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                this.ErrorBox("Could not delete old trays(editRedyeingVoucher) \n" + e.Message, "Exception");
                con.Close();
                return false;
            }
            finally
            {
                con.Close();
            }

            //Delete old batches and set old batch_no state to 2 
            try
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                string sql = "DELETE FROM Batch WHERE Batch_No IN (" + dt.Rows[0]["Redyeing_Batch_No"].ToString()+","+ dt.Rows[0]["Non_Redyeing_Batch_No"].ToString() + ") AND Fiscal_Year = '"+dt.Rows[0]["Redyeing_Batch_Fiscal_Year"].ToString()+"'";
                Console.WriteLine(sql);
                adapter.InsertCommand = new SqlCommand(sql, con);
                adapter.InsertCommand.ExecuteNonQuery();

                sql = "UPDATE Batch SET Batch_State = 2, Redyeing = NULL WHERE Batch_No = '" + dt.Rows[0]["Old_Batch_No"].ToString() + "' AND Fiscal_Year = '" + dt.Rows[0]["Old_Batch_Fiscal_Year"].ToString() + "'";
                Console.WriteLine(sql);
                adapter.InsertCommand = new SqlCommand(sql, con);
                adapter.InsertCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                this.ErrorBox("Could not delete RD batches(editRedyeingVoucher) \n" + e.Message, "Exception");
                con.Close();
                return false;
            }
            finally
            {
                con.Close();
            }
            return true;
        }
        public bool addRDBatch(int batch_no, string colour, string dyeing_company_name, string dyeing_out_date, string tray_id_arr, float batch_weight, string quality, string company_name, int no_of_trays, float dyeing_rate, string fiscal_year, string dyeing_in_date, int state, int bill_no, string bill_date, string slip_no, string redyeing)
        {
            try
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                string sql = "";
                if(tray_id_arr==null)
                {
                    if(bill_no==0)
                    {
                        sql = "INSERT INTO Batch (Batch_No, Colour, Dyeing_Company_Name, Dyeing_Out_Date, Tray_ID_Arr, Net_Weight, Quality, Company_Name, Number_Of_Trays, Dyeing_Rate, Fiscal_Year, Dyeing_In_Date, Batch_State, Bill_No, Bill_Date, Slip_No, Redyeing) VALUES (" + batch_no + " ,'" + colour + "', '" + dyeing_company_name + "', '" + dyeing_out_date + "', NULL , " + batch_weight + ", '" + quality + "', '" + company_name + "', " + no_of_trays + ", " + dyeing_rate + ", '" + fiscal_year + "', '"+dyeing_in_date+"', "+state+", "+bill_no+", NULL, '"+slip_no+"', '"+redyeing+"')";
                    }
                    else
                    {
                        sql = "INSERT INTO Batch (Batch_No, Colour, Dyeing_Company_Name, Dyeing_Out_Date, Tray_ID_Arr, Net_Weight, Quality, Company_Name, Number_Of_Trays, Dyeing_Rate, Fiscal_Year, Dyeing_In_Date, Batch_State, Bill_No, Bill_Date, Slip_No, Redyeing) VALUES (" + batch_no + " ,'" + colour + "', '" + dyeing_company_name + "', '" + dyeing_out_date + "', NULL , " + batch_weight + ", '" + quality + "', '" + company_name + "', " + no_of_trays + ", " + dyeing_rate + ", '" + fiscal_year + "', '" + dyeing_in_date + "', " + state + ", " + bill_no + ", '"+bill_date+"', '"+slip_no+"', '" + redyeing + "')";
                    }
                }
                else
                {
                    sql = "INSERT INTO Batch (Batch_No, Colour, Dyeing_Company_Name, Dyeing_Out_Date, Tray_ID_Arr, Net_Weight, Quality, Company_Name, Number_Of_Trays, Dyeing_Rate, Fiscal_Year, Dyeing_In_Date, Batch_State, Bill_No, Bill_Date, Slip_No, Redyeing) VALUES (" + batch_no + " ,'" + colour + "', '" + dyeing_company_name + "', '" + dyeing_out_date + "', '"+tray_id_arr+"' , " + batch_weight + ", '" + quality + "', '" + company_name + "', " + no_of_trays + ", " + dyeing_rate + ", '" + fiscal_year + "', NULL, " + state + ", NULL, NULL, NULL, '" + redyeing + "')";
                }
                Console.WriteLine(sql);
                adapter.InsertCommand = new SqlCommand(sql, con);
                adapter.InsertCommand.ExecuteNonQuery();
                SqlDataAdapter adapter1 = new SqlDataAdapter();
                sql = "UPDATE Fiscal_Year SET Highest_Batch_No=" + batch_no + " WHERE Fiscal_Year='" + fiscal_year + "'";
                adapter1.InsertCommand = new SqlCommand(sql, con);
                adapter1.InsertCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                this.ErrorBox("Could not RDBatch (addRDBatch) \n" + e.Message, "Exception");
                con.Close();
                return false;
            }
            finally
            {
                con.Close();
            }
            return true;
        }
        public bool editRDBatch(int batch_no, string batch_fiscal_year, float net_weight, string tray_id_arr, int number_of_trays, string colour, float dyeing_rate)
        {
            try
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                string sql = "";
                if (tray_id_arr == null)
                {
                    sql = "UPDATE Batch SET Net_Weight = " + net_weight + " WHERE Batch_No = '" + batch_no + "' AND Fiscal_Year = '" + batch_fiscal_year + "'";
                }
                else
                {
                    sql = "UPDATE Batch SET Net_Weight = " + net_weight + ", Colour = '"+colour+"', Tray_ID_Arr = '"+tray_id_arr+"', Number_Of_Trays = "+number_of_trays+", Dyeing_Rate = "+dyeing_rate+" WHERE Batch_No = '" + batch_no + "' AND Fiscal_Year = '" + batch_fiscal_year + "'";
                }
                Console.WriteLine(sql);
                adapter.InsertCommand = new SqlCommand(sql, con);
                adapter.InsertCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                this.ErrorBox("Could edit RDBatch (editRDBatch) \n" + e.Message, "Exception");
                con.Close();
                return false;
            }
            finally
            {
                con.Close();
            }
            return true;
        }
        
        //Carton Production Voucher
        public bool addCartonProductionVoucher(DateTime dtinputDate, string colour, string quality, string dyeingCompany, string carton_financialYear, string cone_weight, string production_dates, string carton_nos, string gross_weights, string carton_weights, string number_of_cones, string net_weights, string batch_nos, int closed, float net_batch_weight, float carton_net_weight, string grades_arr)
        {
            string fiscal_year = this.getFinancialYear(dtinputDate);
            string inputDate = dtinputDate.Date.ToString("MM-dd-yyyy").Substring(0, 10);
            string[] productionDates = this.csvToArray(production_dates);
            string[] cartonNos = this.csvToArray(carton_nos);
            string[] grossWeights = this.csvToArray(gross_weights);
            string[] cartonWeights = this.csvToArray(carton_weights);
            string[] numberOfCones = this.csvToArray(number_of_cones);
            string[] netWeights = this.csvToArray(net_weights);
            string[] tempBatchNos = this.csvToArray(batch_nos);
            string[] grades = this.csvToArray(grades_arr);
            string batches_to_add = "";
            string batches_fiscal_years = "";

            string new_cartons = "";
            //Check for repeated carton no in carton financial year
            for(int i=0;i<cartonNos.Length;i++)
            {
                new_cartons += cartonNos[i] + ",";
            }
            //check for duplicates of all new cartons
            DataTable carton_dup = this.getDataIn_FinancialYear("Carton_Produced", carton_financialYear, "Carton_No", removecom(new_cartons));
            Dictionary<string, bool> carton_dup_dict = new Dictionary<string, bool>();
            for (int i = 0; i < carton_dup.Rows.Count; i++) carton_dup_dict[carton_dup.Rows[i]["Carton_No"].ToString()] = true;
            for (int i = 0; i < cartonNos.Length; i++)
            {
                bool nouse;
                bool present = carton_dup_dict.TryGetValue(cartonNos[i], out nouse);
                if (present == true) //if its present in the duplicate dictionary
                {
                    this.ErrorBox("Carton number " + cartonNos[i] + " at row " + (i + 1).ToString() + " already exists in Financial Year " + carton_financialYear, "Error");
                    return false;
                }
            }

            //Store batch number and respective fiscal years in batches list
            List<pair> batches = new List<pair>();
            for(int i=0;i<tempBatchNos.Length;i++)
            {
                if(this.check_if_batch_repeated(tempBatchNos[i]))
                {
                    string[] temp = this.repeated_batch_csv(tempBatchNos[i]);
                    pair thisbatch = new pair();
                    thisbatch.batch_no = temp[0];
                    thisbatch.fiscal_year = temp[1];
                    batches.Add(thisbatch);
                    batches_to_add += temp[0] + ",";
                    batches_fiscal_years += temp[1] + ",";
                }
                else
                {
                    pair thisbatch = new pair();
                    thisbatch.batch_no = tempBatchNos[i];
                    thisbatch.fiscal_year = this.getFiscalYear_BatchNoState(tempBatchNos[i], 2);
                    batches.Add(thisbatch);
                    batches_to_add += tempBatchNos[i] + ",";
                    batches_fiscal_years += thisbatch.fiscal_year + ",";
                }
            }

            //Check if future entry is not being done and get highest carton number
            int max_carton_no = int.Parse(cartonNos[0]);
            for (int i = 0; i < productionDates.Length; i++)
            {
                DateTime prod = DateTime.Parse(productionDates[i]);
                if (dtinputDate < prod)
                {
                    this.ErrorBox("Carton Number: " + cartonNos[i] + " at row " + (i + 1).ToString() + " has Date of Production (" + prod.Date.ToString("dd-MM-yyyy") + " after given Date of Input (" + dtinputDate.Date.ToString("dd-MM-yyyy") + "),", "Error");
                    return false;
                }
                if(int.Parse(cartonNos[i])>max_carton_no)
                {
                    max_carton_no = int.Parse(cartonNos[i]);
                }
            }

            //Enter into carton produced table
            for (int i = 0; i < productionDates.Length; i++)
            {
                DateTime temp = DateTime.Parse(productionDates[i]);
                string correct_format_date = temp.Date.ToString("MM-dd-yyyy").Substring(0, 10);
                Console.WriteLine(grades[i]);
                bool batch_added = addProducedCarton(cartonNos[i], 1 , correct_format_date, quality, colour, batches_to_add, dyeingCompany, float.Parse(cartonWeights[i]), int.Parse(numberOfCones[i]), float.Parse(cone_weight)/1000F, float.Parse(grossWeights[i]), float.Parse(netWeights[i]), carton_financialYear, batches_fiscal_years, grades[i]);
                if (batch_added == false)
                {
                    return false;
                }
            }

            //Get Max carton production dates
            DateTime max = DateTime.Parse(productionDates[0]);
            bool addDate = false;
            if (closed==1)
            {
                addDate = true;
                for (int i = 1; i < productionDates.Length; i++)
                {
                    DateTime temp = DateTime.Parse(productionDates[i]);
                    if (temp > max)
                    {
                        max = temp;
                    }
                }
            }

            try
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                string sql;
                if(closed==1)
                {
                    float oil_gain = (carton_net_weight - net_batch_weight) / net_batch_weight * 100F;
                    string max_date = max.Date.ToString("MM-dd-yyyy").Substring(0, 10);
                    sql = "INSERT INTO Carton_Production_Voucher (Date_Of_Input, Colour, Quality, Dyeing_Company_Name, Batch_No_Arr, Carton_No_Production_Arr, Fiscal_Year, Net_Batch_Weight, Net_Carton_Weight, Oil_Gain, Voucher_Closed, Batch_Fiscal_Year_Arr, Carton_Fiscal_Year, Cone_Weight, Date_Of_Production, Grades_Arr) VALUES ('" + inputDate + "','" + colour + "', '" + quality + "', '" + dyeingCompany + "', '" + batches_to_add + "' , '" + carton_nos + "', '" + fiscal_year + "', " + net_batch_weight + ", " + carton_net_weight + ", " + oil_gain + ", " + closed + ", '"+batches_fiscal_years+ "', '" + carton_financialYear + "', "+float.Parse(cone_weight)/1000F+", '"+max_date+"', '"+grades_arr+"')";
                }
                else
                {
                    sql = "INSERT INTO Carton_Production_Voucher (Date_Of_Input, Colour, Quality, Dyeing_Company_Name, Batch_No_Arr, Carton_No_Production_Arr, Fiscal_Year, Net_Batch_Weight, Net_Carton_Weight, Voucher_Closed, Batch_Fiscal_Year_Arr, Carton_Fiscal_Year, Cone_Weight, Grades_Arr) VALUES ('" + inputDate + "','" + colour + "', '" + quality + "', '" + dyeingCompany + "', '" + batches_to_add + "' , '" + carton_nos + "', '" + fiscal_year + "', " + net_batch_weight + ", " + carton_net_weight + ", " + closed + ", '"+batches_fiscal_years+"', '"+carton_financialYear+ "', " + float.Parse(cone_weight)/1000F + ", '"+grades_arr+"')";
                }
                adapter.InsertCommand = new SqlCommand(sql, con);
                adapter.InsertCommand.ExecuteNonQuery();
                con.Close();
                
                con.Open();
                sql = "SELECT MAX(Voucher_ID) FROM Carton_Production_Voucher";
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                sda.Fill(dt);
                con.Close();

                //Send Batches
                for (int i = 0; i < batches.Count; i++)
                {
                    bool flag = this.sendBatch_StateVoucherIDProductionDate(batches[i], 3, int.Parse(dt.Rows[0][0].ToString()), max, addDate);
                    if(!flag)
                    {
                        return false;
                    }    
                }

                con.Open();
                //Enter max carton number in Fiscal Year Table
                sql = "UPDATE Fiscal_Year SET Highest_Carton_Production_No=" + max_carton_no+ " WHERE Fiscal_Year='" + carton_financialYear + "'";
                adapter.InsertCommand = new SqlCommand(sql, con);
                adapter.InsertCommand.ExecuteNonQuery();
                
                this.SuccessBox("Voucher Added Successfully");
            }
            catch (Exception e)
            {
                this.ErrorBox("Could not add carton production voucher (addCartonProductionVoucher) \n" + e.Message, "Exception");
                con.Close();
                return false;
            }
            finally
            {
                con.Close();
            }
            return true;
        }
        public bool sendBatch_StateVoucherIDProductionDate(pair batch, int state, int voucher_id, DateTime max, bool addDate)
        {
            try
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                string sql;
                if(addDate)
                {
                    if(voucher_id==-1)
                    {
                        sql = "UPDATE Batch SET Batch_State=" + state + ", Voucher_ID = NULL, Date_Of_Production=NULL WHERE Batch_No='" + int.Parse(batch.batch_no) + "' AND Fiscal_Year='" + batch.fiscal_year + "'";
                    }
                    else
                    {
                        string max_date= max.Date.ToString("MM-dd-yyyy").Substring(0, 10);
                        sql = "UPDATE Batch SET Batch_State=" + state + ", Voucher_ID = "+voucher_id+", Date_Of_Production='"+max_date+"' WHERE Batch_No='" + int.Parse(batch.batch_no) + "' AND Fiscal_Year='" + batch.fiscal_year + "'";
                    }
                }
                else
                {
                    sql = "UPDATE Batch SET Batch_State=" + state + ", Voucher_ID = " + voucher_id + " WHERE Batch_No='" + int.Parse(batch.batch_no) + "' AND Fiscal_Year='" + batch.fiscal_year + "'";
                }
                Console.WriteLine(sql);
                adapter.InsertCommand = new SqlCommand(sql, con);
                adapter.InsertCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                this.ErrorBox("Could not send Batch (sendBatch_StateVoucherID) \n" + e.Message, "Exception");
                con.Close();
                return false;
            }
            finally
            {
                con.Close();
            }
            return true;
        }
        public DataTable getProductionVoucherTable_VoucherID(int voucherid)
        {
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Carton_Production_Voucher WHERE Voucher_ID=" + voucherid, con);
                sda.Fill(dt);
            }
            catch (Exception e)
            {
                this.ErrorBox("Could not connect to database (getProductionVoucherTable_VoucherID) \n" + e.Message, "Exception");
                con.Close();
                return new DataTable();
            }
            finally
            {
                con.Close();

            }
            return dt;
        }
        public bool editCartonProductionVoucher(int voucherID, string colour, string quality, string dyeing_company_name, string cartonfinancialYear, string cone_weight, string production_dates_arr, string carton_nos_arr, string gross_weights_arr, string carton_weights_arr, string number_of_cones_arr, string net_weights_arr, string batch_nos_arr, int closed, float net_batch_weight, float net_carton_weight, string grades_arr, Dictionary<string,bool> carton_editable)
        {
            //Dictionary carton_editable contains entries for carton_nos with state =2 or state=3
            Dictionary<string, bool> old_cartons_hash = new Dictionary<string, bool>();
            List<string> added_cartons = new List<string>(); //to store cartons added by the function
            string[] carton_no = this.csvToArray(carton_nos_arr);

            //Get all carton_nos, batch_nos and batch_fiscal_years which were previously present
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("SELECT Carton_No_Production_Arr, Batch_No_Arr, Batch_Fiscal_Year_Arr FROM Carton_Production_Voucher WHERE Voucher_ID='" + voucherID + "'", con);
            DataTable old = new DataTable();
            sda.Fill(old);
            con.Close();
            string[] old_carton_nos = this.csvToArray(old.Rows[0][0].ToString());
            string[] old_batch_nos = this.csvToArray(old.Rows[0][1].ToString());
            string[] old_batch_fiscal_years = this.csvToArray(old.Rows[0][2].ToString());
            Console.WriteLine("selected1");

            /*<------------------Check for duplicates------------------->*/
            //Insert old cartons into hash 
            Console.WriteLine("-----------Old cartons-----------");
            for (int i = 0; i < old_carton_nos.Length; i++)
            {
                old_cartons_hash[old_carton_nos[i]] = true;
            }
            //Any new carton added should have count=0
            //New carton is a carton in the carton_no list, not present in the old_carton_hash
            string new_cartons = "", new_carton_indexes = "";
            for (int i = 0; i < carton_no.Length; i++)
            {
                //Check if its a new carton(not present in Hash)
                bool value = false;
                bool value2 = old_cartons_hash.TryGetValue(carton_no[i], out value);
                if (value2 == false && value == false) //Carton not present in the hash, hence its new
                {
                    new_cartons += carton_no[i] + ",";
                    new_carton_indexes += i + ",";
                }
            }
            string[] new_carton_arr = this.csvToArray(new_cartons);

            //check for duplicates of all new cartons
            DataTable carton_dup = this.getDataIn_FinancialYear("Carton_Produced", cartonfinancialYear, "Carton_No", removecom(new_cartons));
            Dictionary<string, bool> carton_dup_dict = new Dictionary<string, bool>();
            for (int i = 0; i < carton_dup.Rows.Count; i++) carton_dup_dict[carton_dup.Rows[i]["Carton_No"].ToString()] = true;
            for (int i = 0; i < new_carton_arr.Length; i++)
            {
                bool nouse;
                bool present = carton_dup_dict.TryGetValue(new_carton_arr[i], out nouse);
                if (present == true) //if its present in the duplicate dictionary
                {
                    this.ErrorBox("Carton number " + new_carton_arr[i] + " at row " + (i + 1).ToString() + " already exists in Financial Year " + cartonfinancialYear, "Error");
                    return false;
                }
            }

            string[] production_dates = this.csvToArray(production_dates_arr);
            /*<Check if sale date of cartons in state 2 is >= production date>*/
            //string state_2_cartons = "", state_2_cartons_index="";
            //for (int i = 0; i < carton_no.Length; i++)
            //{
            //    bool value;
            //    bool value2 = carton_editable.TryGetValue(carton_no[i], out value);
            //    if (value2 == true) //does contain entry, means it is in state 2
            //    {
            //        state_2_cartons += carton_no[i] + ",";
            //        state_2_cartons_index += i + ",";
            //    }
            //}
            //if(state_2_cartons!="")
            //{
            //    con.Open();
            //    SqlDataAdapter sda1 = new SqlDataAdapter("SELECT Carton_No, Date_Of_Sale FROM Carton_Produced WHERE Carton_No IN (" + removecom(state_2_cartons) + ") AND Fiscal_Year='" + cartonfinancialYear + "'", con);
            //    DataTable dt = new DataTable();
            //    sda1.Fill(dt);
            //    con.Close();
            //    Dictionary<string, string> dates = new Dictionary<string, string>();
            //    for (int i = 0; i < dt.Rows.Count; i++) dates[dt.Rows[i]["Carton_No"].ToString()] = dt.Rows[i]["Date_Of_Sale"].ToString();

            //    string[] state_2_cartons_arr = csvToArray(state_2_cartons);
            //    string[] state_2_cartons_index_arr = csvToArray(state_2_cartons_index);
            //    for (int i = 0; i < state_2_cartons_arr.Length; i++)
            //    {
            //        DateTime sale = Convert.ToDateTime(dates[state_2_cartons_arr[i]]);
            //        DateTime production_date = Convert.ToDateTime(production_dates[int.Parse(state_2_cartons_index_arr[i])]);
            //        if (production_date > sale)
            //        {
            //            this.ErrorBox("Carton number: " + state_2_cartons_arr[i] + " at row " + (int.Parse(state_2_cartons_index_arr[i]) + 1).ToString() + " has Date of Sale (" + sale.Date.ToString("dd-MM-yyyy") + ") earlier than given Date of Production (" + production_date.Date.ToString("dd-MM-yyyy") + "),", "Error");
            //            return false;
            //        }
            //    }
            //}

            Console.WriteLine("selected3");
            string cartons = "";
            //Remove cartons with state 1 in the old voucher
            for (int i = 0; i < old_carton_nos.Length; i++)
            {
                bool value;
                bool value2 = carton_editable.TryGetValue(old_carton_nos[i], out value);
                if (value2 == false) //doesnt contain entry, means it is in state 1
                {
                    cartons += old_carton_nos[i] + ",";
                    Console.WriteLine("Removing Carton: " + old_carton_nos[i]);
                }
            }
            this.removeCarton(removecom(cartons), cartonfinancialYear, "Carton_Produced");
            Console.WriteLine("selected4");

            string[] grossWeights = this.csvToArray(gross_weights_arr);
            string[] cartonWeights = this.csvToArray(carton_weights_arr);
            string[] numberOfCones = this.csvToArray(number_of_cones_arr);
            string[] netWeights = this.csvToArray(net_weights_arr);
            string[] tempBatchNos = this.csvToArray(batch_nos_arr);
            string[] grades = this.csvToArray(grades_arr);
            string batches_to_add = "";
            string batches_fiscal_years = "";

            //Store batch number and respective fiscal years in batches list
            List<pair> batches = new List<pair>();
            for (int i = 0; i < tempBatchNos.Length; i++)
            {
                if (this.check_if_batch_repeated(tempBatchNos[i]))
                {
                    string[] temp = this.repeated_batch_csv(tempBatchNos[i]);
                    pair thisbatch = new pair();
                    thisbatch.batch_no = temp[0];
                    thisbatch.fiscal_year = temp[1];
                    batches.Add(thisbatch);
                    batches_to_add += temp[0] + ",";
                    batches_fiscal_years += temp[1] + ",";
                }
                else
                {
                    pair thisbatch = new pair();
                    thisbatch.batch_no = tempBatchNos[i];
                    string batch_finyear = this.getFiscalYear_BatchNoState(tempBatchNos[i], 2);
                    if(batch_finyear == null)
                    {
                        batch_finyear = this.getFiscalYear_BatchNoState(tempBatchNos[i], 3);
                    }
                    thisbatch.fiscal_year = batch_finyear;
                    batches.Add(thisbatch);
                    batches_to_add += tempBatchNos[i] + ",";
                    batches_fiscal_years += thisbatch.fiscal_year + ",";
                }
            }

            Console.WriteLine("selected5");

            //Add all New Cartons with state 1
            for (int i = 0; i < carton_no.Length; i++)
            {
                bool value;
                bool value2 = carton_editable.TryGetValue(carton_no[i], out value);
                if (value2 == false)
                {
                    DateTime temp = DateTime.Parse(production_dates[i]);
                    string correct_format_date = temp.Date.ToString("MM-dd-yyyy").Substring(0, 10);
                    bool batch_added = addProducedCarton(carton_no[i], 1, correct_format_date, quality, colour, batches_to_add, dyeing_company_name, float.Parse(cartonWeights[i]), int.Parse(numberOfCones[i]), float.Parse(cone_weight)/1000F, float.Parse(grossWeights[i]), float.Parse(netWeights[i]), cartonfinancialYear, batches_fiscal_years, grades[i]);
                    if (batch_added == false)
                    {
                        return false;
                    }
                }
            }

            Console.WriteLine("selected6");


            //Get highest carton number
            int max_carton_no = int.Parse(carton_no[0]);
            for (int i = 0; i < carton_no.Length; i++)
            {
                if (int.Parse(carton_no[i]) > max_carton_no)
                {
                    max_carton_no = int.Parse(carton_no[i]);
                }
            }

            Console.WriteLine("selected7");


            //Get Max carton production dates
            DateTime max = DateTime.Parse(production_dates[0]);
            bool addDate = false;
            if (closed == 1)
            {
                addDate = true;
                for (int i = 1; i < production_dates.Length; i++)
                {
                    DateTime temp = DateTime.Parse(production_dates[i]);
                    if (temp > max)
                    {
                        max = temp;
                    }
                }
            }

            Console.WriteLine("selected8");


            //Remove old batches
            for (int i = 0; i < old_batch_nos.Length; i++)
            {
                pair batch;
                batch.batch_no = old_batch_nos[i];
                batch.fiscal_year = old_batch_fiscal_years[i];
                bool flag = this.sendBatch_StateVoucherIDProductionDate(batch, 2, -1, max, true);
                if (!flag)
                {
                    return false;
                }
            }

            Console.WriteLine("selected9");


            //Add new batches
            for (int i=0;i<batches.Count;i++)
            {
                bool flag = this.sendBatch_StateVoucherIDProductionDate(batches[i], 3, voucherID, max, addDate);
                if (!flag)
                {
                    return false;
                }
            }

            Console.WriteLine("selected10");


            try
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                string sql;
                if (closed == 1)
                {
                    float oil_gain = (net_carton_weight - net_batch_weight) / net_batch_weight * 100F;
                    string max_date = max.Date.ToString("MM-dd-yyyy").Substring(0, 10);
                    sql = "UPDATE Carton_Production_Voucher SET Batch_No_Arr = '"+batches_to_add+"', Carton_No_Production_Arr = '"+carton_nos_arr+"', Net_Batch_Weight="+net_batch_weight+", Net_Carton_Weight="+net_carton_weight+", Oil_Gain="+oil_gain+", Voucher_Closed="+closed+", Batch_Fiscal_Year_Arr='"+batches_fiscal_years+"', Cone_Weight="+float.Parse(cone_weight)/1000F+", Date_Of_Production='"+max_date+"', Grades_Arr='"+grades_arr+"' WHERE Voucher_ID = "+voucherID+"";
                }
                else
                {
                    sql = "UPDATE Carton_Production_Voucher SET Batch_No_Arr='"+batches_to_add+"', Carton_No_Production_Arr='"+carton_nos_arr+"', Net_Batch_Weight="+net_batch_weight+", Net_Carton_Weight="+net_carton_weight+", Voucher_Closed="+closed+", Batch_Fiscal_Year_Arr='"+batches_fiscal_years+"', Cone_Weight="+float.Parse(cone_weight)/1000F+", Grades_Arr='"+grades_arr+ "' WHERE Voucher_ID = " + voucherID + "";
                }
                adapter.InsertCommand = new SqlCommand(sql, con);
                adapter.InsertCommand.ExecuteNonQuery();
                con.Close();

                Console.WriteLine("selected11");

                con.Open();
                //Get higest carton number in this financial year
                SqlDataAdapter sda2 = new SqlDataAdapter("SELECT Highest_Carton_Production_No FROM Fiscal_Year WHERE Fiscal_Year='" + cartonfinancialYear + "'", con);
                DataTable dt2 = new DataTable();
                sda2.Fill(dt2);
                int old_max_carton_no = int.Parse(dt2.Rows[0][0].ToString());
                con.Close();

                Console.WriteLine("selected12");


                if (old_max_carton_no<max_carton_no)
                {
                    con.Open();
                    //Enter max carton number in Fiscal Year Table
                    sql = "UPDATE Fiscal_Year SET Highest_Carton_Production_No=" + max_carton_no + " WHERE Fiscal_Year='" + cartonfinancialYear + "'";
                    adapter.InsertCommand = new SqlCommand(sql, con);
                    adapter.InsertCommand.ExecuteNonQuery();
                    con.Close();
                }

                Console.WriteLine("selected13");

                this.SuccessBox("Voucher Edited Successfully");
            }
            catch (Exception e)
            {
                this.ErrorBox("Could not edit carton production voucher (editCartonProductionVoucher) \n" + e.Message, "Exception");
                con.Close();
                return false;
            }
            finally
            {
                con.Close();
            }
            return true;
        }
        public bool deleteCartonProductionVoucher(int voucherID)
        {
            try
            {
                //Get all carton_nos, batch_nos and batch_fiscal_years which were previously present
                con.Open();
                string sql = "UPDATE Carton_Production_Voucher SET Deleted = 1 OUTPUT inserted.Carton_No_Production_Arr, inserted.Batch_No_Arr, inserted.Batch_Fiscal_Year_Arr, inserted.Carton_Fiscal_Year WHERE Voucher_ID=" + voucherID;
                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                DataTable old = new DataTable();
                sda.Fill(old);
                con.Close();
                string old_carton_nos = old.Rows[0]["Carton_No_Production_Arr"].ToString();
                string[] old_batch_nos = this.csvToArray(old.Rows[0]["Batch_No_Arr"].ToString());
                string[] old_batch_fiscal_years = this.csvToArray(old.Rows[0]["Batch_Fiscal_Year_Arr"].ToString());
                string carton_fiscal_year = old.Rows[0]["Carton_Fiscal_Year"].ToString();

                //Remove cartons with state 1 in the old voucher
                this.removeCarton(removecom(old_carton_nos), carton_fiscal_year, "Carton_Produced");

                //Remove old batches
                for (int i = 0; i < old_batch_nos.Length; i++)
                {
                    pair batch;
                    batch.batch_no = old_batch_nos[i];
                    batch.fiscal_year = old_batch_fiscal_years[i];
                    bool flag = this.sendBatch_StateVoucherIDProductionDate(batch, 2, -1, DateTime.Now, true);
                    if (!flag)
                    {
                        return false;
                    }
                }
            }
            catch (Exception e)
            {
                this.ErrorBox("Could not edit carton production voucher (editCartonProductionVoucher) \n" + e.Message, "Exception");
                con.Close();
                return false;
            }
            finally
            {
                con.Close();
            }
            return true;
        }
        
        //Carton Produced
        public bool addProducedCarton(string carton_no, int state, string productionDate, string quality, string colour, string batch_nos, string dyeingCompany,  float cartonWeight, int numberOfCones, float cone_weight, float grossWeight, float netWeight, string carton_financialYear, string batch_fiscal_years, string grade)
        {
            try
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                string sql = "INSERT INTO Carton_Produced (Carton_No, Carton_State, Date_Of_Production, Quality, Colour, Batch_No_Arr, Dyeing_Company_Name, Carton_Weight, Number_Of_Cones, Cone_Weight, Gross_Weight, Net_Weight, Fiscal_Year, Batch_Fiscal_Year_Arr, Grade, Company_Name) VALUES ('" + carton_no + "' ," + state+ ", '" + productionDate + "', '" + quality+ "', '" + colour+ "' , '" + batch_nos+ "', '" + dyeingCompany+ "', " + cartonWeight+ ", " + numberOfCones+ ", "+cone_weight+", " + grossWeight+ ", " + netWeight+ ", '"+carton_financialYear+"', '"+batch_fiscal_years+"', '"+grade+"', 'Self')";
                Console.WriteLine(sql);
                adapter.InsertCommand = new SqlCommand(sql, con);
                adapter.InsertCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                this.ErrorBox("Could not Carton Produced (addCartonProduced) \n" + e.Message, "Exception");
                con.Close();
                return false;
            }
            finally
            {
                con.Close();
            }
            return true;
        }
        public bool isCartonPresent(string carton_no, string fiscal_year)
        {
            bool ans;
            try
            {
                con.Open();
                string sql = "SELECT * FROM Carton_Produced WHERE Carton_No='"+carton_no+"' AND Fiscal_Year='"+fiscal_year+"'";
                Console.WriteLine(sql);
                SqlDataAdapter adapter = new SqlDataAdapter(sql, con);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                if(dt.Rows.Count==0)
                {
                    ans = false;
                }
                else
                {
                    ans = true;
                }
            }
            catch (Exception e)
            {
                this.ErrorBox("Could not check carton(isCartonPresent) \n" + e.Message, "Exception");
                con.Close();
                return false;
            }
            finally
            {
                con.Close();
            }
            return ans;
        }
        public DataRow getProducedCartonRow(string carton_no, string fiscal_year)
        {
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Carton_Produced WHERE Carton_No='" + carton_no + "' AND Fiscal_Year =  '" + fiscal_year + "'", con);
                sda.Fill(dt);
            }
            catch (Exception e)
            {
                this.ErrorBox("Could not connect to database (getProducedCartonRow) \n" + e.Message, "Exception");
                con.Close();
                return null;
            }
            finally
            {
                con.Close();
            }
            if (dt.Rows.Count == 0) return null;
            return (DataRow)dt.Rows[0];
        }
        public float getCartonProducedWeight(string cartonno, string fiscal_year)
        {
            DataTable dt = new DataTable(); //this is creating a virtual table  
            float ans = -1F;
            try
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT Net_Weight FROM Carton_Produced WHERE Carton_No='" + cartonno + "' AND Fiscal_Year='" + fiscal_year + "'", con);
                sda.Fill(dt);
                if (dt.Rows.Count != 0) ans = float.Parse(dt.Rows[0][0].ToString());
            }
            catch (Exception e)
            {
                this.ErrorBox("Could not get weight (getCartonProducedWeight) \n" + e.Message, "Exception");
        }
            finally
            {
                con.Close();
            }
            return ans;
        }
        
        //On Date Inventory
        public DataTable getInventoryCarton(DateTime d)
        {
            DataTable dt = new DataTable(); //this is creating a virtual table  
            try
            {
                con.Open();
                string date = d.Date.ToString("yyyy-MM-dd").Substring(0, 10);
                string sql = "SELECT * FROM Carton WHERE Date_Of_Billing <= '"+date+ "' AND ((Date_Of_Issue IS NULL AND Date_Of_Sale IS Null) OR (Date_Of_Issue IS NULL AND Date_Of_Sale > '" + date + "') OR (Date_Of_Sale IS NULL AND Date_Of_Issue > '" + date + "')) ORDER BY Date_Of_Billing DESC";
                Console.WriteLine(sql);
                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                sda.Fill(dt);
            }
            catch
            {
                MessageBox.Show("Could not connect to database (getInventoryCarton)", "Exception");
                con.Close();
                return new DataTable();
            }
            finally
            {
                con.Close();

            }
            return dt;
        }
        public float getTwistStock(DateTime d)
        {
            float ans = -1F;
            string fiscal_year = this.getFinancialYear(d);
            List<int> fiscal_year_arr = this.getFinancialYearArr(fiscal_year);
            string prev_fiscal_year = (fiscal_year_arr[0] - 1).ToString() + "-" + (fiscal_year_arr[0]).ToString();
            try
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT Is_Set, Twist_Stock FROM Fiscal_Year WHERE Fiscal_Year='" +prev_fiscal_year+"'", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                con.Close();
                if (dt.Rows.Count == 0)
                {
                    //case raw calculate
                    Console.WriteLine("getTwistStock raw");
                    ans = this.getTwistStockFiscalYear(fiscal_year, d);
                }
                else
                {
                    int is_set = int.Parse(dt.Rows[0][0].ToString());
                    float previous_twist_stock=-0F;
                    if (is_set == 0)
                    {
                        Console.WriteLine("getTwistStock calculating for previous year "+prev_fiscal_year);
                        this.calculateTwistStock(prev_fiscal_year);
                        con.Open();
                        SqlDataAdapter sda2 = new SqlDataAdapter("SELECT Is_Set, Twist_Stock FROM Fiscal_Year WHERE Fiscal_Year='" + prev_fiscal_year + "'", con);
                        DataTable dt2 = new DataTable();
                        sda.Fill(dt2);
                        con.Close();
                        if (int.Parse(dt2.Rows[0][0].ToString()) == 0)
                        {
                            ErrorBox("Fatal error in getTwistStock", "Error");
                            con.Close();
                            return ans;
                        }
                        previous_twist_stock = float.Parse(dt2.Rows[0][1].ToString());
                    }
                    else previous_twist_stock = float.Parse(dt.Rows[0][1].ToString());
                    Console.WriteLine("getTwistStock set previous year ka" + prev_fiscal_year+" prev stock="+previous_twist_stock);
                    ans = previous_twist_stock + this.getTwistStockFiscalYear(fiscal_year, d);
                }
            }
            catch(Exception e)
            {
                ErrorBox("Could not connect to database (getTwistStock) \n"+ e.Message , "Exception");
            }
            finally
            {
                con.Close();

            }
            return ans;
        }
        public void calculateTwistStock(string fiscal_year)
        {
            Console.WriteLine("calculateTwistStock for " +fiscal_year);
            List<int> fiscal_year_arr = this.getFinancialYearArr(fiscal_year);
            string prev_fiscal_year = (fiscal_year_arr[0] - 1).ToString() + "-" + (fiscal_year_arr[0]).ToString();
            float twist_stock = 0F;
            try
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT Is_Set, Twist_Stock FROM Fiscal_Year WHERE Fiscal_Year='" + prev_fiscal_year + "'", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                con.Close();
                if (dt.Rows.Count == 0)
                {
                    //this is the last financial_year in the list
                    Console.WriteLine("calculateTwistStock last fiscal year" + fiscal_year);
                    twist_stock = this.getTwistStockFiscalYear(fiscal_year, DateTime.MinValue);
                }
                else
                {
                    int is_set = int.Parse(dt.Rows[0][0].ToString());
                    if (is_set == 1) //previous stock exists
                    {
                        Console.WriteLine("calculateTwistStock Previous stock exists " + fiscal_year);
                        float prev_stock = float.Parse(dt.Rows[0][1].ToString());
                        twist_stock= prev_stock+ this.getTwistStockFiscalYear(fiscal_year, new DateTime());
                    }
                    else if (is_set == 0) //previous stock doesnt exist
                    {
                        Console.WriteLine("calculateTwistStock Previous stock doesnt exist " + fiscal_year);
                        //calculate for previous, then for now
                        con.Close();
                        this.calculateTwistStock(prev_fiscal_year);
                        this.calculateTwistStock(fiscal_year);
                        return;
                    }
                }
                //set this as twist stock
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                string sql = "UPDATE Fiscal_Year SET Twist_Stock=" + twist_stock + ", Is_Set=1 WHERE Fiscal_Year='" + fiscal_year + "'";
                adapter.InsertCommand = new SqlCommand(sql, con);
                adapter.InsertCommand.ExecuteNonQuery();
                con.Close();
            }
            catch(Exception e)
            {
                ErrorBox("Could not connect to database (calculateTwistStock)" +e.Message, "Exception");
            }
            finally
            {
                con.Close();
            }
        }
        public float getTwistStockFiscalYear(string fiscal_year, DateTime d)
        {
            //calculates the twist_stock from start date to d(end of year if d is default)
            List<int> fiscal_year_arr = this.getFinancialYearArr(fiscal_year);
            if(d!=DateTime.MinValue)
            {
                if (this.getFinancialYear(d) != fiscal_year)
                {
                    this.ErrorBox("getTwistStock Break", "Error");
                }
            }

            DateTime start = new DateTime(fiscal_year_arr[0], 4, 1);
            DateTime end = new DateTime(fiscal_year_arr[1], 3, 31);
            if (d != DateTime.MinValue) end = d;
            Console.WriteLine("getTwistStockFiscalYear start=" + start.Date + " End=" + end.Date);
            float net_carton_wt = 0F, net_tray_wt = 0F;
            try
            {
                con.Open();
                //get all cartons gone to twist between this time, and trays produced in this time
                string sql = "SELECT SUM(Carton_Weight) FROM Carton WHERE Date_Of_Issue>='" + start.Date.ToString("yyyy-MM-dd").Substring(0, 10) + "' AND Date_Of_Issue<='" + end.Date.ToString("yyyy-MM-dd").Substring(0, 10) + "'";
                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                Console.WriteLine(sql);

                sql = "SELECT SUM(Net_Weight) FROM Tray_History WHERE Tray_Production_Date>='" + start.Date.ToString("yyyy-MM-dd").Substring(0, 10) + "' AND Tray_Production_Date<='" + end.Date.ToString("yyyy-MM-dd").Substring(0, 10) + "'";
                SqlDataAdapter sdat1 = new SqlDataAdapter(sql, con);
                DataTable dtt1 = new DataTable();
                sdat1.Fill(dtt1);
                Console.WriteLine(sql);

                sql = "SELECT SUM(Net_Weight) FROM Tray_Active WHERE Tray_Production_Date>='" + start.Date.ToString("yyyy-MM-dd").Substring(0, 10) + "' AND Tray_Production_Date<='" + end.Date.ToString("yyyy-MM-dd").Substring(0, 10) + "'";
                SqlDataAdapter sdat2 = new SqlDataAdapter(sql, con);
                DataTable dtt2 = new DataTable();
                sdat2.Fill(dtt2);
                Console.WriteLine(sql);
                Console.WriteLine(dt.Rows[0][0].ToString());
                Console.WriteLine(dtt1.Rows[0][0].ToString());
                Console.WriteLine(dtt2.Rows[0][0].ToString());

                float value;
                if (float.TryParse(dt.Rows[0][0].ToString(), out value)==true ) net_carton_wt += float.Parse(dt.Rows[0][0].ToString());
                if (float.TryParse(dtt1.Rows[0][0].ToString(), out value) == true) net_tray_wt += float.Parse(dtt1.Rows[0][0].ToString());
                if (float.TryParse(dtt2.Rows[0][0].ToString(), out value) == true) net_tray_wt += float.Parse(dtt2.Rows[0][0].ToString());
                Console.WriteLine("Net carton wt=" + net_carton_wt + " net tray wt=" + net_tray_wt);
            }
            catch(Exception e)
            {
                ErrorBox("Could not connect to database (getTwistStockFiscalYear)\n"+e.Message, "Exception");
                con.Close();
                return 0F;
            }
            finally
            {
                con.Close();
            }
            return net_carton_wt - net_tray_wt;
        }
        public DataTable getTwistStock2(DateTime d)
        {
            DataTable dt = new DataTable();
            DataTable dtt1 = new DataTable();
            DataTable dtt2 = new DataTable();
            try
            {
                con.Open();
                //get all cartons gone to twist between this time, and trays produced in this time
                string sql = "SELECT Company_Name, Quality, SUM(Net_Weight) FROM Carton WHERE Date_Of_Issue<='" + d.Date.ToString("yyyy-MM-dd").Substring(0, 10)+"'  GROUP BY QUALITY, Company_Name";
                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                sda.Fill(dt);
                Console.WriteLine(sql);

                sql = "SELECT Company_Name, Quality, SUM(Net_Weight) FROM Tray_History WHERE (Tray_Production_Date<='" + d.Date.ToString("yyyy-MM-dd").Substring(0, 10) + "' AND Redyeing Is NULL) GROUP BY QUALITY, Company_Name";
                SqlDataAdapter sdat1 = new SqlDataAdapter(sql, con);
                sdat1.Fill(dtt1);
                Console.WriteLine(sql);

                sql = "SELECT Company_Name, Quality, SUM(Net_Weight) FROM Tray_Active WHERE (Tray_Production_Date<='" + d.Date.ToString("yyyy-MM-dd").Substring(0, 10) + "' AND Redyeing Is NULL)  GROUP BY QUALITY, Company_Name";
                SqlDataAdapter sdat2 = new SqlDataAdapter(sql, con);
                sdat2.Fill(dtt2);
                Console.WriteLine(sql);
                con.Close();
                this.printDataTable(dt);
                this.printDataTable(dtt1);
                this.printDataTable(dtt2);

                for (int i=0; i<dtt1.Rows.Count; i++)
                {
                    bool found = false;
                    string old_quality = this.getBeforeQuality(dtt1.Rows[i]["Quality"].ToString());
                    for (int j=0; j<dt.Rows.Count; j++)
                    {
                        if(dt.Rows[j]["Quality"].ToString()== old_quality && dt.Rows[j]["Company_Name"].ToString()==dtt1.Rows[i]["Company_Name"].ToString())
                        {
                            //found at the jth row
                            found = true;
                            dt.Rows[j][2] = float.Parse(dt.Rows[j][2].ToString()) - float.Parse(dtt1.Rows[i][2].ToString());
                            break;
                        }
                    }
                    Console.WriteLine("hello2");
                    if(found==false)
                    {
                        dt.Rows.Add(dtt1.Rows[i]["Company_Name"].ToString(), old_quality, (-float.Parse(dtt1.Rows[i][2].ToString())).ToString());
                    }
                }
                Console.WriteLine("Hello3");
                for (int i = 0; i < dtt2.Rows.Count; i++)
                {
                    bool found = false;
                    string old_quality = this.getBeforeQuality(dtt2.Rows[i]["Quality"].ToString());
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        if (dt.Rows[j]["Quality"].ToString() == old_quality && dt.Rows[j]["Company_Name"].ToString() == dtt2.Rows[i]["Company_Name"].ToString())
                        {
                            found = true;
                            dt.Rows[j][2] = float.Parse(dt.Rows[j][2].ToString()) - float.Parse(dtt2.Rows[i][2].ToString());
                            break;
                        }
                    }
                    if (found == false)
                    {
                        dt.Rows.Add(dtt2.Rows[i]["Company_Name"].ToString(), old_quality, (-float.Parse(dtt2.Rows[i][2].ToString())).ToString());
                    }
                }
                this.printDataTable(dt);
            }
            catch (Exception e)
            {
                ErrorBox("Could not connect to database (getTwistStock2)\n" + e.Message, "Exception");
                con.Close();
                DataTable returning= new DataTable();
                returning.Columns.Add("Company_Name");
                returning.Columns.Add("Quality");
                returning.Columns.Add("Net Weight");
                return returning;
            }
            finally
            {
                con.Close();
            }
            DataView dv = dt.DefaultView;
            dv.Sort = "Company_Name desc";
            DataTable sortedDT = dv.ToTable();
            sortedDT.Columns[2].ColumnName = "Net Weight";
            return sortedDT;
        }
        public DataTable getInventoryTray(DateTime d)
        {
            DataTable dt = new DataTable(); //this is creating a virtual table  
            DataTable dt1 = new DataTable(); //this is creating a virtual table  
            try
            {
                con.Open();
                //SqlDataAdapter sda = new SqlDataAdapter("SELECT Tray_No, Tray_Production_Date, Dyeing_Out_Date,  Spring, Number_Of_Springs, Tray_Tare, Gross_Weight, Quality, Company_Name, Net_Weight, Fiscal_Year, Machine_No, Quality_Before_Twist FROM Tray_Active WHERE Tray_Production_Date <='" + d.Date.ToString("yyyy-MM-dd").Substring(0, 10) + "' AND (Dyeing_Out_Date>='" + d.Date.ToString("yyyy-MM-dd").Substring(0, 10) + "' OR Dyeing_Out_Date IS NULL) ORDER BY Tray_Production_Date", con);
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Tray_Active WHERE Tray_Production_Date <='" + d.Date.ToString("yyyy-MM-dd").Substring(0, 10) + "' AND (Dyeing_Out_Date>='" + d.Date.ToString("yyyy-MM-dd").Substring(0, 10) + "' OR Dyeing_Out_Date IS NULL) ORDER BY Tray_Production_Date", con);
                sda.Fill(dt);
                dt1 = dt.Copy();
                dt.Clear();
                //SqlDataAdapter sda2 = new SqlDataAdapter("SELECT Tray_No, Tray_Production_Date, Dyeing_Out_Date,  Spring, Number_Of_Springs, Tray_Tare, Gross_Weight, Quality, Company_Name, Net_Weight, Fiscal_Year, Machine_No, Quality_Before_Twist FROM Tray_History WHERE Tray_Production_Date<='" + d.Date.ToString("yyyy-MM-dd").Substring(0, 10) + "' AND (Dyeing_Out_Date>='" + d.Date.ToString("yyyy-MM-dd").Substring(0, 10) + "' OR Dyeing_Out_Date IS NULL) ORDER BY Tray_Production_Date", con);
                SqlDataAdapter sda2 = new SqlDataAdapter("SELECT * FROM Tray_History WHERE Tray_Production_Date<='" + d.Date.ToString("yyyy-MM-dd").Substring(0, 10) + "' AND (Dyeing_Out_Date>='" + d.Date.ToString("yyyy-MM-dd").Substring(0, 10) + "' OR Dyeing_Out_Date IS NULL) ORDER BY Tray_Production_Date", con);
                sda2.Fill(dt);
            }
            catch(Exception e)
            {
                MessageBox.Show("Could not connect to database (getInventoryTray)\n"+e.Message, "Exception");
            }
            finally
            {
                con.Close();

            }
            DataTable ans = dt1.Copy();
            ans.Merge(dt);
            return ans;
        }
        public DataTable getInventoryDyeingBatch(DateTime d)
        {
            DataTable dt = new DataTable(); //this is creating a virtual table  
            try
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Batch WHERE Dyeing_Out_Date <='" + d.Date.ToString("yyyy-MM-dd").Substring(0, 10) + "' AND (Dyeing_In_Date>'" + d.Date.ToString("yyyy-MM-dd").Substring(0, 10) + "'  OR Dyeing_In_Date IS NULL) ORDER BY Dyeing_Out_Date", con);
                sda.Fill(dt);
            }
            catch (Exception e)
            {
                MessageBox.Show("Could not connect to database (getInventoryDyeingBatch)\n" + e.Message, "Exception");
            }
            finally
            {
                con.Close();

            }
            return dt;
        }
        public DataTable getInventoryConningBatch(DateTime d)
        {
            DataTable dt = new DataTable(); //this is creating a virtual table  
            try
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Batch WHERE Dyeing_In_Date <='" + d.Date.ToString("yyyy-MM-dd").Substring(0, 10) + "' AND (Date_Of_Production>='" + d.Date.ToString("yyyy-MM-dd").Substring(0, 10) + "' OR Date_Of_Production IS NULL) AND Batch_State!=4 ORDER BY Dyeing_In_Date", con);
                sda.Fill(dt);
            }
            catch (Exception e)
            {
                MessageBox.Show("Could not connect to database (getInventoryConningBatch)\n" + e.Message, "Exception");
            }
            finally
            {
                con.Close();

            }
            return dt;
        }
        public DataTable getInventoryCartonProduced(DateTime d)
        {
            DataTable dt = new DataTable(); //this is creating a virtual table  
            try
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Carton_Produced WHERE Date_Of_Production <='" + d.Date.ToString("yyyy-MM-dd").Substring(0, 10) + "' AND (Date_Of_Sale>='" + d.Date.ToString("yyyy-MM-dd").Substring(0, 10) + "' OR Date_Of_Sale IS NULL) ORDER BY Date_Of_Production", con);
                sda.Fill(dt);
            }
            catch (Exception e)
            {
                MessageBox.Show("Could not connect to database (getInventoryCartonProduced)\n" + e.Message, "Exception");
            }
            finally
            {
                con.Close();

            }
            return dt;
        }

        //From To Inventory
        public DataTable getInventoryCarton(DateTime from, DateTime to)
        {
            DataTable dt = new DataTable(); //this is creating a virtual table  
            try
            {
                con.Open();
                string from_date = from.Date.ToString("yyyy-MM-dd").Substring(0, 10);
                string to_date = to.Date.ToString("yyyy-MM-dd").Substring(0, 10);
                string sql =
                    " DECLARE @from varchar(20)" +
                    " DECLARE @to varchar(20)" +
                    " SET @from = '"+from_date+"';" +
                    " SET @to = '"+to_date+"';" +
                    " select *" +
                    " from" +
                    " (" +
                        "select " +
                            " case when(T.Date_Of_Billing < @from AND((T.Date_Of_Issue IS NULL AND T.Date_Of_Sale IS Null) OR(T.Date_Of_Issue IS NULL AND T.Date_Of_Sale >= @from) OR(T.Date_Of_Sale IS NULL AND T.Date_Of_Issue >= @from))) then 1" +
                            " end as Opening," +
                            " case when T.[Date_Of_Billing] between @from and @to then 1" +
                            " end as Transact_Input," +
                            " case " +
                            " when(T.[Date_Of_Issue] between @from and @to) then 1" +
                            " when(T.[Date_Of_Sale] between @from and @to) then 2" +
                            " end as Transact_Output," +
                            " T.*" +
                        " from Carton as T" +
                        " where" +
                            " (T.Date_Of_Billing<@from AND ((T.Date_Of_Issue IS NULL AND T.Date_Of_Sale IS Null) OR(T.Date_Of_Issue IS NULL AND T.Date_Of_Sale >= @from) OR(T.Date_Of_Sale IS NULL AND T.Date_Of_Issue >= @from)))  OR" +
                            " T.[Date_Of_Billing] between @from and @to  OR" +
                            " (T.[Date_Of_Issue] between @from and @to) OR(T.[Date_Of_Sale] between @from and @to)" +
                    " ) as C ;";
                   
                Console.WriteLine(sql);
                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                sda.Fill(dt);
            }
            catch(Exception e)
            {
                this.ErrorBox("Could not connect to database (getInventoryCarton)\n"+e.Message, "Exception");
                con.Close();
                return new DataTable();
            }
            finally
            {
                con.Close();

            }
            this.printDataTable(dt);
            return dt;
        }
    }


}

