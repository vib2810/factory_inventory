﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
//using System.Windows;
using System.Windows.Forms;

namespace Factory_Inventory.Factory_Classes
{
    public class DbConnect
    {
        //Connect to DB
        //static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;
        public SqlConnection con;
        public DateTime loginTime;
        private string useractive;

        public DbConnect()
        {
            //Connection string for Gaurang's Laptop
            this.con = new SqlConnection(@"Data Source=DESKTOP-MOUBPNG\MSSQLSERVER2019;Initial Catalog=FactoryData;Persist Security Info=True;User ID=sa;Password=Kdvghr2810@;"); // making connection   
            
            //Connection string for old Database
            //this.con = new SqlConnection(@"Data Source=DESKTOP-MOUBPNG\MSSQLSERVER2019;Initial Catalog=FactoryInventory;Persist Security Info=True;User ID=sa;Password=Kdvghr2810@;"); // making connection   
            
            //Connection string for Vob's laptop
            //this.con = new SqlConnection(@"Data Source=192.168.1.12, 1433;Initial Catalog=FactoryData;Persist Security Info=True;User ID=sa;Password=Kdvghr2810@;        "); // making connection   
        }

        public void temp()
        {
            for(int i=2019;i<2060;i++)
            {
                string fy = i.ToString() + "-" + (i + 1).ToString();
                try
                {
                    con.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    string sql = "INSERT INTO Fiscal_Year_Data VALUES ('" + fy + "', 0 ,0)";
                    adapter.InsertCommand = new SqlCommand(sql, con);
                    adapter.InsertCommand.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    MessageBox.Show("Could not add temp (addUser) \n" + e.Message, "Exception");
                }

                finally
                {
                    con.Close();
                }
            }
        }
        //Utility Functions
        public string[] csvToArray(string str)
        {
            string[] ans = str.Split(',');
            ans = ans.Take(ans.Length - 1).ToArray();
            return ans;
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
                MessageBox.Show("Error (isPresentColour)\n" + e.Message, "Exception");
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
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM "+tablename+" ORDER BY Voucher_ID DESC", con);
                sda.Fill(dt);
            }
            catch
            {
                MessageBox.Show("Could not connect to database (getVoucherHistories)", "Exception");
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
        public int getCount_FinancialYear(string tablename, string financialyear, string quantitycolumn, string quantity)
        {
            int ans = -1;
            try
            {
                con.Open();
                //DateTime start = new DateTime(financialyear[0], 4, 1);
                //DateTime end = new DateTime(financialyear[1], 3, 31);
                //string startdate = start.Date.ToString("yyyy-MM-dd");
                //string enddate = end.Date.ToString("yyyy-MM-dd");
                string sql = "SELECT COUNT(*) FROM " + tablename + " WHERE " + quantitycolumn + "=" + quantity + " AND Fiscal_Year='"+financialyear+"'";
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                sda.Fill(dt);
                ans = int.Parse(dt.Rows[0][0].ToString());
            }
            catch(Exception e)
            {
                MessageBox.Show("Error (getCount_FinancialYear):\n" + e.Message, "Exception");
            }
            finally
            {
                con.Close();
            }
            return ans;
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
                MessageBox.Show("Login record error", "Exception");
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
                    MessageBox.Show("Invalid username or password");
                    ans = 0;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Could not connect to database (checkLogin) \n"+e.Message, "Exception");
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
                MessageBox.Show("Could not add user (addUser) \n" + e.Message, "Exception");
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
                MessageBox.Show("Could not connect to database (getUserData)", "Exception");
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
                MessageBox.Show("User deleted", "Success");
            }
            catch (Exception e)
            {
                MessageBox.Show("Could not delete user (deleteUser)", "Exception");
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
                    MessageBox.Show("Access Level Updated", "Success");
                }
                else
                {
                    sql = "UPDATE Users SET PasswordHash = HASHBYTES('SHA', '" + password + "') , AccessLevel = " + access + "  WHERE Username='" + username + "'";
                    MessageBox.Show("Password/Access Level Updated", "Success");
                }
                adapter.InsertCommand = new SqlCommand(sql, con);
                adapter.InsertCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show("Could not edit user (updateUser)", "Exception");
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
                MessageBox.Show("Could not connect to database (getUserLog)", "Exception");
            }
            finally
            {
                con.Close();

            }
            return dt;
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
                MessageBox.Show("Could not add " + tablename + " (addQC) \n" + e.Message, "Exception");
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
                MessageBox.Show("Could not add Spring (addSpring) \n" + e.Message, "Exception");
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
                MessageBox.Show("Could not add Colour (addColour) \n" + e.Message, "Exception");
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
                MessageBox.Show("Could not delete " + tablename + " (deleteQC) \n" + e.Message, "Exception");
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
                MessageBox.Show("Could not delete colour (deleteColour) \n" + e.Message, "Exception");
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
                MessageBox.Show("Could not edit (editQC) "+ e.Message, "Exception");
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
                MessageBox.Show("Could not edit spring (editSpring) " + e.Message, "Exception");
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
                MessageBox.Show("Could not edit colour (editColour) " + e.Message, "Exception");
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
            DataTable dt = new DataTable(); //this is creating a virtual table  
            try
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM " + tablename + " ", con);
                sda.Fill(dt);
            }
            catch(Exception e)
            {
                MessageBox.Show("Could not connect to database (getQC) " + e.Message, "Exception");
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
                MessageBox.Show("Could not connect to database (getDyeingRate) " + e.Message, "Exception");
            }
            finally
            {
                con.Close();

            }
            return ans;
        }


        //Carton Voucher
        public bool addCartonVoucher(DateTime dtinput, DateTime dtbill, string billNumber, string quality, string quality_arr, string company, string cost, string cartonno, string weights, int number, float netweight)
        {
            string inputDate = dtinput.Date.ToString("MM-dd-yyyy").Substring(0, 10);
            string billDate = dtbill.Date.ToString("MM-dd-yyyy").Substring(0, 10);
            List<string> added_carton = new List<string>();
            string[] carton_no = this.csvToArray(cartonno);
            string financialyear = this.getFinancialYear(dtbill);
            for (int i=0;i<carton_no.Length;i++)
            {
                int count = this.getCount_FinancialYear("Carton", financialyear, "Carton_No", carton_no[i]);
                if(count>0)
                {
                    MessageBox.Show("Carton number " + carton_no[i] + " at row " + (i + 1).ToString() + " already exists in Financial Year " + financialyear, "Error");
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
                    added_carton.Add(carton_no[i]);
                }
                if(flag == true)
                {
                    //Failed to add all cartons
                    //Remove all added cartons
                    for(int i=0; i<added_carton.Count; i++)
                    {
                        removeCarton(added_carton[i], financialyear);
                    }
                    MessageBox.Show("Carton Number: " + carton_no[index] + " at Row: " + (index + 1).ToString() + " was already added to the Database. Could not add voucher", "Error");
                    return false;
                }
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                string sql = "INSERT INTO Carton_Voucher (Date_Of_Input, Date_Of_Billing, Bill_No, Quality, Quality_Arr, Company_Name, Number_of_Cartons, Carton_No_Arr, Carton_Weight_Arr, Net_Weight, Buy_Cost, Fiscal_Year) VALUES ('" + inputDate + "','" + billDate + "', '" + billNumber + "','" + quality + "', '" + quality_arr + "', '" + company + "', " + number + ", '" + cartonno + "', '" + weights + "', " + netweight + " , '" + cost + "', '"+financialyear+"')";
                Console.WriteLine(sql);
                adapter.InsertCommand = new SqlCommand(sql, con);
                adapter.InsertCommand.ExecuteNonQuery();
                MessageBox.Show("Voucher Added Successfully", "Success");
            }
            catch (SqlException e)
            {
                if(e.Number==2627)
                {
                    MessageBox.Show("Bill Number Already Exists", "Exception");
                }
                else
                {
                    MessageBox.Show("Could not add carton voucher (addCartonVoucher) \n" + e.Message, "Exception");
                }
                con.Close();
                for (int i = 0; i < added_carton.Count; i++)
                {
                    removeCarton(added_carton[i], financialyear);
                }
                return false;
            }

            finally
            {
                con.Close();
            }
            return true;

        }
        public bool editCartonVoucher(string oldbillno, DateTime dtinput, DateTime dtbill, string billNumber, string quality, string quality_arr, string company, string cost, string cartonno, string weights, int number, float netweight, Dictionary<string, bool> carton_editable)
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
                SqlDataAdapter sda = new SqlDataAdapter("SELECT Carton_No_Arr, Fiscal_Year FROM Carton_Voucher WHERE Bill_No='" + oldbillno + "'", con);
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
                string financialyear = this.getFinancialYear(dtbill);
                for (int i = 0; i < carton_no.Length; i++)
                {
                    //Check if its a new carton(not present in Hash)
                    bool value=false;
                    bool value2 = old_cartons_hash.TryGetValue(carton_no[i], out value);
                    if(value2 == false && value == false) //Carton not present in the hash, hence its new
                    {
                        //get its count in this financial year
                        int count = this.getCount_FinancialYear("Carton", financialyear, "Carton_No", carton_no[i]);
                        if (count > 0)
                        {
                            MessageBox.Show("Carton number " + carton_no[i] + " at row " + (i + 1).ToString() + " already exists in Financial Year " + financialyear, "Error");
                            return false;
                        }
                    }
                }

                /*<Check if issue date and sale date of cartons in state 2 and 3 is >= Bill Date>*/
                for (int i = 0; i < carton_no.Length; i++)
                {
                    bool value;
                    bool value2 = carton_editable.TryGetValue(carton_no[i], out value);
                    if (value2 == true) //does contain entry, means it is in state 2 and 3
                    {
                        con.Open();
                        SqlDataAdapter sda1 = new SqlDataAdapter("SELECT Date_Of_Issue, Date_Of_Sale FROM Carton WHERE Carton_No='"+carton_no[i]+"' AND Fiscal_Year='"+old_fiscal_year+"'", con);
                        DataTable dt = new DataTable();
                        sda1.Fill(dt);
                        con.Close();
                        if(dt.Rows[0]["Date_Of_Issue"]==null)
                        {
                            DateTime sale = Convert.ToDateTime(dt.Rows[0]["Date_Of_Sale"].ToString());
                            if(dtbill>sale)
                            {
                                MessageBox.Show("Carton number: " + carton_no[i] + " at row " + (i + 1).ToString() + " has Date of Sale (" + sale.Date.ToString("dd-MM-yyyy") + ") earlier than given Date of billing (" + dtbill.Date.ToString("dd-MM-yyyy") + "),", "Error");
                                return false;
                            }
                        }
                        else if (dt.Rows[0]["Date_Of_Sale"] == null)
                        {
                            DateTime issue = Convert.ToDateTime(dt.Rows[0]["Date_Of_Issue"].ToString());
                            if (dtbill > issue)
                            {
                                MessageBox.Show("Carton number: " + carton_no[i] + " at row " + (i + 1).ToString() + " has Date of Issue (" + issue.Date.ToString("dd-MM-yyyy") + ") earlier than given Date of billing (" + dtbill.Date.ToString("dd-MM-yyyy") + ")", "Error");
                                return false;
                            }
                        }

                    }
                }
                Console.WriteLine("selected2");

                //Remove cartons with state 1 in the old voucher
                for (int i = 0; i < old_carton_nos.Length; i++)
                {
                    bool value;
                    bool value2 = carton_editable.TryGetValue(old_carton_nos[i], out value);
                    if (value2 == false) //doesnt contain entry, means it is in state 1
                    {
                        Console.WriteLine("Removing Carton: " + old_carton_nos[i]);
                        this.removeCarton(old_carton_nos[i], old_fiscal_year);
                    }
                }
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
                string sql = "UPDATE Carton_Voucher SET Date_Of_Billing='" + billDate + "', Bill_No='" + billNumber + "', Quality='" + quality + "', Quality_Arr='" + quality_arr + "', Company_Name='" + company + "', Number_of_Cartons= " + number + ", Carton_No_Arr='" + cartonno + "', Carton_Weight_Arr='" + weights + "', Net_Weight=" + netweight + ", Buy_Cost='" + cost + "', Fiscal_Year='" + financialyear+"' WHERE Bill_No='" + oldbillno + "'";
                Console.WriteLine(sql);
                adapter.InsertCommand = new SqlCommand(sql, con);
                adapter.InsertCommand.ExecuteNonQuery();
                MessageBox.Show("Voucher Edited Successfully", "Success");
            }
            catch (Exception e)
            {
                MessageBox.Show("Could not edit carton voucher (editCartonVoucher) \n" + e.Message, "Exception");
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
                MessageBox.Show("Could not update Carton (updateCarton)", "Exception");
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
                string sql = "INSERT INTO Carton (Carton_No, Carton_State, Date_Of_Input, Date_Of_Billing, Bill_No, Quality, Company_Name, Carton_Weight, Buy_Cost, Fiscal_Year) VALUES ('" + carton_no + "', " + state + " , '" + inputDate + "','" + billDate + "', '" + billNumber + "','" + quality + "', '" + company + "', " + carton_weight + " , " + buy_cost + ", '"+fiscal_year+"')";
                Console.WriteLine(sql);
                adapter.InsertCommand = new SqlCommand(sql, con);
                adapter.InsertCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show("Could not add carton (addCarton) \n" + e.Message, "Exception");
                con.Close();
                return false;
            }

            finally
            {
                con.Close();
            }
            return true;
        }
        public void removeCarton(string no, string fiscal_year)
        {
            try
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                string sql = "DELETE FROM Carton WHERE Carton_No='" + no + "' AND Fiscal_Year='"+fiscal_year+"'";
                adapter.InsertCommand = new SqlCommand(sql, con);
                adapter.InsertCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show("Could not delete Carton_No" + no + " (removeCarton) \n" + e.Message, "Exception");
            }

            finally
            {
                con.Close();
            }
        }
        //send carton has 3 overloads.
        //1) to enter update just the carton number and state 
        public void sendCarton(string cartonno, int state, string carton_fiscal_year)
        {
            try
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                string sql = "UPDATE Carton SET Carton_State=" +state+ " WHERE Carton_No= '" + cartonno+ "' AND Fiscal_Year ='"+carton_fiscal_year+"'";
                adapter.InsertCommand = new SqlCommand(sql, con);
                adapter.InsertCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show("Could not edit Carton State (sendCarton1)", "Exception");
            }
            finally
            {
                con.Close();
            }
        }
        //2) other one to enter issue date too while adding twist voucher
        public void sendCarton(string cartonno, int state, string date, string carton_fiscal_year)
        {
            try
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                string sql = "UPDATE Carton SET Carton_State=" + state + " , Date_Of_Issue='" + date + "' WHERE Carton_No='" + cartonno + "' AND Fiscal_Year='"+carton_fiscal_year+"'";
                adapter.InsertCommand = new SqlCommand(sql, con);
                adapter.InsertCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show("Could not edit Carton State (sendCarton) " + e.Message, "Exception");
            }
            finally
            {
                con.Close();
            }
        }
        //3) other one to enter selling date and sell cost too while adding twist voucher
        public void sendCarton(string cartonno, int state, string date, float sell_cost, string carton_fiscal_year)
        {
            try
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                string sql = "UPDATE Carton SET Carton_State=" + state + " , Date_Of_Sale='" + date + "', Sell_Cost="+sell_cost+" WHERE Carton_No='" + cartonno + "' AND Fiscal_Year='" + carton_fiscal_year + "'";
                adapter.InsertCommand = new SqlCommand(sql, con);
                adapter.InsertCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show("Could not edit Carton State (sendCarton) " + e.Message, "Exception");
            }
            finally
            {
                con.Close();
            }
        }
        public DataTable getCartonStateQualityCompany(int state, string quality, string company, string fiscalyear)
        {
            DataTable dt = new DataTable(); //this is creating a virtual table  
            try
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT Carton_No FROM Carton WHERE Carton_State="+state+" AND Quality='"+quality+"' AND Company_Name='" + company + "' AND Fiscal_Year='"+fiscalyear+"'", con);
                sda.Fill(dt);
            }
            catch(Exception e)
            {
                MessageBox.Show("Could not connect to database (getCartonStateQualityCompany) "+e.Message, "Exception");
            }
            finally
            {
                con.Close();
            }
            return dt;
        }
        public DataTable getCartonWeight(string cartonno, string fiscal_year)
        {
            DataTable dt = new DataTable(); //this is creating a virtual table  
            try
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT Carton_Weight FROM Carton WHERE Carton_No='" + cartonno + "' AND Fiscal_Year='"+fiscal_year+"'", con);
                sda.Fill(dt);
            }
            catch (Exception e)
            {
                MessageBox.Show("Could not get weight (getCartonWeight) \n" + e.Message, "Exception");
            }
            finally
            {
                con.Close();
            }
            return dt;
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
                MessageBox.Show("Could not connect to database (getCartonState) " + e.Message, "Exception");
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
                    MessageBox.Show("Carton number: " + carton_no[i] + " at row " + (i + 1).ToString() + " has Date of Billing (" + bill.Date.ToString("dd-MM-yyyy") + " earlier than given Date of Issue (" + dtissue.Date.ToString("dd-MM-yyyy") + "),", "Error");
                    return false;
                }
            }
            try
            {
                for (int i = 0; i < number; i++)
                {
                    this.sendCarton(carton_no[i], 2, issueDate, carton_fiscal_year);
                }
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                string sql = "INSERT INTO Twist_Voucher (Date_Of_Input, Date_Of_Issue, Quality, Company_Name, Carton_No_Arr, Number_of_Cartons, Carton_Fiscal_Year, Fiscal_Year) VALUES ('"+inputDate+"', '" + issueDate + "','" + quality + "', '" + company + "','" + cartonno + "', " + number + ", '"+carton_fiscal_year+"', '"+fiscal_year+"')";
                Console.WriteLine(sql);
                adapter.InsertCommand = new SqlCommand(sql, con);
                adapter.InsertCommand.ExecuteNonQuery();
                MessageBox.Show("Voucher Added Successfully", "Success");
            }
            catch (Exception e)
            {
                MessageBox.Show("Could not add carton voucher (addTwistVoucher) \n" + e.Message, "Exception");
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
                    MessageBox.Show("Carton number: " + carton_no[i] + " at row " + (i + 1).ToString() + " has Date of Billing (" + bill.Date.ToString("dd-MM-yyyy") + ") earlier than given Date of Issue (" + dtissue.Date.ToString("dd-MM-yyyy") + "),", "Error");
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
                    this.sendCarton(old_carton_nos[i],1, carton_fiscal_year);
                }
                //Add all New Cartons

                for (int i = 0; i < carton_no.Length; i++)
                {
                    this.sendCarton(carton_no[i], 2, carton_fiscal_year);
                }
                con.Open();
                string sql = "UPDATE Twist_Voucher SET Date_Of_Issue='" + issueDate + "', Quality='" + quality + "', Company_Name='" + company + "', Number_of_Cartons= " + number + ", Carton_No_Arr='" + cartonno + "', Fiscal_Year = '"+fiscal_year+"' WHERE Voucher_ID='" + voucherID + "'";
                //Console.WriteLine(sql);
                adapter.InsertCommand = new SqlCommand(sql, con);
                adapter.InsertCommand.ExecuteNonQuery();
                MessageBox.Show("Voucher Edited Successfully", "Success");
            }
            catch (Exception e)
            {
                MessageBox.Show("Could not edit twist voucher (editTwistVoucher) \n" + e.Message, "Exception");
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
        public bool addSalesVoucher(DateTime dtinput, DateTime dtissue, string quality, string company, string cartonno, int number, string customer, float sell_cost, string carton_fiscal_year)
        {
            string inputDate = dtinput.Date.ToString("MM-dd-yyyy").Substring(0, 10);
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
                    MessageBox.Show("Carton number: " + carton_no[i] + " at row " + (i + 1).ToString() + " has Date of Billing (" + bill.Date.ToString("dd-MM-yyyy") + " earlier than given Date of Issue (" + dtissue.Date.ToString() + "),", "Error");
                    return false;
                }
            }
            try
            {
                for (int i = 0; i < number; i++)
                {
                    this.sendCarton(carton_no[i], 3, issueDate, sell_cost, carton_fiscal_year);
                }
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                string sql = "INSERT INTO Sales_Voucher (Date_Of_Input,Date_Of_Issue, Quality, Company_Name, Customer, Selling_Price, Carton_No_Arr, Number_of_Cartons, Fiscal_Year, Carton_Fiscal_Year) VALUES ('" + inputDate+"' ,'" + issueDate + "','" + quality + "', '" + company + "', '" + customer + "', " + sell_cost + " , '" + cartonno + "', " + number + ", '"+fiscal_year+"', '"+carton_fiscal_year+"')";
                Console.WriteLine(sql);
                adapter.InsertCommand = new SqlCommand(sql, con);
                adapter.InsertCommand.ExecuteNonQuery();
                MessageBox.Show("Voucher Added Successfully", "Success");
            }
            catch (Exception e)
            {
                MessageBox.Show("Could not add carton voucher (addSalesVoucher) \n" + e.Message, "Exception");
                con.Close();
                return false;
            }

            finally
            {
                con.Close();
            }
            return true;
        }
        public bool editSalesVoucher(int voucherID, DateTime dtissue, string quality, string company, string cartonno, int number, string customer, float sell_cost, string carton_fiscal_year)
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
                    MessageBox.Show("Carton number: " + carton_no[i] + " at row " + (i + 1).ToString() + " has Date of Billing (" + bill.Date.ToString("dd-MM-yyyy") + ") earlier than given Date of Issue (" + dtissue.Date.ToString("dd-MM-yyyy") + "),", "Error");
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
                string[] old_carton_nos = this.csvToArray(old.Rows[0][0].ToString());
                for (int i = 0; i < old_carton_nos.Length; i++)
                {
                    this.sendCarton(old_carton_nos[i], 1, carton_fiscal_year);
                }

                //Add all New Cartons
                for (int i = 0; i < carton_no.Length; i++)
                {
                    this.sendCarton(carton_no[i], 3, carton_fiscal_year);
                }
                con.Open();
                string sql = "UPDATE Sales_Voucher SET Date_Of_Issue='" + issueDate + "', Quality='" + quality + "', Company_Name='" + company + "', Number_of_Cartons= " + number + ", Carton_No_Arr='" + cartonno + "', Customer='"+customer+"', Selling_Price="+sell_cost+", Fiscal_Year='"+fiscal_year+"'  WHERE Voucher_ID='" + voucherID + "'";
                //Console.WriteLine(sql);
                adapter.InsertCommand = new SqlCommand(sql, con);
                adapter.InsertCommand.ExecuteNonQuery();
                MessageBox.Show("Voucher Edited Successfully", "Success");
            }
            catch (Exception e)
            {
                MessageBox.Show("Could not edit sales voucher (editSalesVoucher) \n" + e.Message, "Exception");
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
        public int addTrayActive(string tray_production_date, string tray_no, string spring, int number_of_springs, float tray_tare, float gross_weight, string quality, string company_name, float net_weight, string fiscal_year)
        {
            //Adds to table Tray_Active
            //Returns the unique Tray_ID for the entered tray
            int ans = -1;
            try
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                string sql = "INSERT INTO Tray_Active (Tray_Production_Date, Tray_No, Spring, Number_Of_Springs, Tray_Tare, Gross_Weight, Quality, Company_Name, Tray_State, Net_Weight, Fiscal_Year) VALUES ('" + tray_production_date + "', '" + tray_no + "', '" + spring + "', " + number_of_springs + " , " + tray_tare + ", " + gross_weight + ", '" + quality + "', '" + company_name + "', 1, "+net_weight+", '"+fiscal_year+"')";
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
                MessageBox.Show("Could not add tray voucher (addTrayVoucher) \n" + e.Message, "Exception");
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
                MessageBox.Show("Could not check tray (checkTray) \n" + e.Message, "Exception");
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
                MessageBox.Show("Could not check tray (checkTray) \n" + e.Message, "Exception");
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
                MessageBox.Show("Could not get tray id tray (getTrayID) \n" + e.Message, "Exception");
            }

            finally
            {
                con.Close();
            }
            return ans;
        }
        public bool freeTray(int tray_id, string date)
        {
            try
            {
                //Update Dyeing date in Tray_Active
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                string sql = "UPDATE Tray_Active SET Dyeing_In_Date='" + date + "' WHERE Tray_ID=" + tray_id + "";
                Console.WriteLine(sql);
                adapter.InsertCommand = new SqlCommand(sql, con);
                adapter.InsertCommand.ExecuteNonQuery();

                //Select single row from Tray_Active
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Tray_Active WHERE Tray_ID=" + tray_id + "", con);
                sda.Fill(dt);

                //Remove Tray_State column
                dt.Columns.Remove("Tray_State");

                //Store all rows in variables
                int trayid = int.Parse(dt.Rows[0]["Tray_ID"].ToString());
                //Change date in correct format
                string productiondate = dt.Rows[0]["Tray_Production_Date"].ToString().Substring(0, 10);
                DateTime d = DateTime.ParseExact(productiondate, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                productiondate = d.ToString("MM-dd-yyyy");

                string trayno = dt.Rows[0]["Tray_No"].ToString();
                string spring = dt.Rows[0]["Spring"].ToString();
                int Number_Of_Springs = int.Parse(dt.Rows[0]["Number_Of_Springs"].ToString());
                float Tray_Tare = float.Parse(dt.Rows[0]["Tray_Tare"].ToString());
                float Gross_Weight = float.Parse(dt.Rows[0]["Gross_Weight"].ToString());
                string Quality = dt.Rows[0]["Quality"].ToString();
                string Company_Name = dt.Rows[0]["Company_Name"].ToString();
                string Dyeing_Company_Name = dt.Rows[0]["Dyeing_Company_Name"].ToString();
                //Change date in correct format
                string Dyeing_In_Date = dt.Rows[0]["Dyeing_In_Date"].ToString().Substring(0, 10);
                d = DateTime.ParseExact(Dyeing_In_Date, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                Dyeing_In_Date = d.ToString("MM-dd-yyyy");
                //Change date in correct format
                string Dyeing_Out_Date = dt.Rows[0]["Dyeing_Out_Date"].ToString().Substring(0, 10);
                d = DateTime.ParseExact(Dyeing_Out_Date, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                Dyeing_Out_Date = d.ToString("MM-dd-yyyy");

                int Batch_No = int.Parse(dt.Rows[0]["Batch_No"].ToString());
                float Net_Weight = float.Parse(dt.Rows[0]["Net_Weight"].ToString());
                string fiscal_year = dt.Rows[0]["Fiscal_Year"].ToString();
                //Put that row in Tray_History
                sql = "INSERT INTO Tray_History (Tray_ID, Tray_Production_Date, Tray_No, Spring, Number_Of_Springs, Tray_Tare, Gross_Weight, Quality, Company_Name, Dyeing_Company_Name, Dyeing_In_Date, Dyeing_Out_Date, Batch_No, Net_Weight, Fiscal_Year) VALUES (" + trayid + ", '" + productiondate + "', '" + trayno + "', '" + spring + "', " + Number_Of_Springs + ", " + Tray_Tare + ", " + Gross_Weight + ", '" + Quality + "', '" + Company_Name + "', '" + Dyeing_Company_Name + "', '" + Dyeing_In_Date + "', '" + Dyeing_Out_Date + "', " + Batch_No + ", " + Net_Weight + ", '"+fiscal_year+"')";
                sda.InsertCommand = new SqlCommand(sql, con);
                sda.InsertCommand.ExecuteNonQuery();
                //Remove that row from Tray_Active
                SqlDataAdapter sda2 = new SqlDataAdapter();
                sql = "DELETE FROM Tray_Active WHERE Tray_ID=" + tray_id + "";
                adapter.InsertCommand = new SqlCommand(sql, con);
                adapter.InsertCommand.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                MessageBox.Show("Could not free tray(freeTray) \n" + e.Message, "Exception");
                con.Close();
                return false;
            }
            finally
            {
                con.Close();
            }
            return true;
        }
        public bool unfreeTray(int tray_id)
        {
            try
            {
                con.Open();
                //Select single row from Tray_History
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Tray_History WHERE Tray_ID=" + tray_id + "", con);
                sda.Fill(dt);
                con.Close();

                //Store all rows in variables
                int trayid = int.Parse(dt.Rows[0]["Tray_ID"].ToString());
                //Change date in correct format
                string productiondate = dt.Rows[0]["Tray_Production_Date"].ToString().Substring(0, 10);
                DateTime d = DateTime.ParseExact(productiondate, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                productiondate = d.ToString("MM-dd-yyyy");

                string trayno = dt.Rows[0]["Tray_No"].ToString();
                string spring = dt.Rows[0]["Spring"].ToString();
                int Number_Of_Springs = int.Parse(dt.Rows[0]["Number_Of_Springs"].ToString());
                float Tray_Tare = float.Parse(dt.Rows[0]["Tray_Tare"].ToString());
                float Gross_Weight = float.Parse(dt.Rows[0]["Gross_Weight"].ToString());
                string Quality = dt.Rows[0]["Quality"].ToString();
                string Company_Name = dt.Rows[0]["Company_Name"].ToString();
                string Dyeing_Company_Name = dt.Rows[0]["Dyeing_Company_Name"].ToString();
                //Change date in correct format
                string Dyeing_Out_Date = dt.Rows[0]["Dyeing_Out_Date"].ToString().Substring(0, 10);
                d = DateTime.ParseExact(Dyeing_Out_Date, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                Dyeing_Out_Date = d.ToString("MM-dd-yyyy");
                int Batch_No = int.Parse(dt.Rows[0]["Batch_No"].ToString());
                float Net_Weight = float.Parse(dt.Rows[0]["Net_Weight"].ToString());
                string fiscal_year = dt.Rows[0]["Fiscal_Year"].ToString();
                //setIdentityInsert("Tray_Active", "ON");

                //Put that row in Tray_Active with state 2 (It is in dyeing) and Dyeing_In_Date NULL
                con.Open();
                string sql = "SET IDENTITY_INSERT Tray_Active ON; INSERT INTO Tray_Active (Tray_ID, Tray_Production_Date, Tray_No, Spring, Number_Of_Springs, Tray_Tare, Gross_Weight, Quality, Company_Name, Dyeing_Company_Name, Dyeing_In_Date, Dyeing_Out_Date, Batch_No, Net_Weight, Tray_State, Fiscal_Year) VALUES (" + trayid + ", '" + productiondate + "', '" + trayno + "', '" + spring + "', " + Number_Of_Springs + ", " + Tray_Tare + ", " + Gross_Weight + ", '" + Quality + "', '" + Company_Name + "', '" + Dyeing_Company_Name + "', NULL, '" + Dyeing_Out_Date + "', " + Batch_No + ", " + Net_Weight + ", 2, '"+fiscal_year+"'); SET IDENTITY_INSERT Tray_Active OFF";
                sda.InsertCommand = new SqlCommand(sql, con);
                sda.InsertCommand.ExecuteNonQuery();
                con.Close();

                //setIdentityInsert("Tray_Active", "OFF");

                //Remove that row from Tray_History
                con.Open();
                SqlDataAdapter sda2 = new SqlDataAdapter();
                sql = "DELETE FROM Tray_History WHERE Tray_ID=" + tray_id + "";
                sda2.InsertCommand = new SqlCommand(sql, con);
                sda2.InsertCommand.ExecuteNonQuery();
                con.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show("Could not unfree tray(unfreeTray) \n" + e.Message, "Exception");
                con.Close();
                return false;
            }
            finally
            {
                con.Close();
            }
            return true;
        }
        public float getTrayWeight(int trayid, int state)
        {
            DataTable dt = new DataTable(); //this is creating a virtual table  
            try
            {
                con.Open();
                if(state==1)
                {
                    SqlDataAdapter sda = new SqlDataAdapter("SELECT Net_Weight FROM Tray_Active WHERE Tray_ID='" + trayid + "'", con);
                    sda.Fill(dt);
                }
                else
                {
                    SqlDataAdapter sda = new SqlDataAdapter("SELECT Net_Weight FROM Tray_History WHERE Tray_ID='" + trayid + "'", con);
                    sda.Fill(dt);
                }
                
            }
            catch (Exception e)
            {
                MessageBox.Show("Could not get weight (getTrayWeight) \n" + e.Message, "Exception");
                con.Close();
                return -1F;
            }
            finally
            {
                con.Close();
            }
            return float.Parse(dt.Rows[0][0].ToString());
        }
        public DataTable getTrayStateQualityCompany(int state, string quality, string company)
        {
            DataTable dt = new DataTable(); //this is creating a virtual table  
            try
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT Tray_No, Tray_ID FROM Tray_Active WHERE Tray_State=" + state + " AND Quality='" + quality + "' AND Company_Name='" + company + "'", con);
                sda.Fill(dt);
                Console.WriteLine("Got Data");
            }
            catch (Exception e)
            {
                MessageBox.Show("Could not connect to database (getTrayStateQualityCompany) " + e.Message, "Exception");
            }
            finally
            {
                con.Close();
            }
            return dt;
        }
        public void changeTrayStateDyeingInDateDyeingCompanyNameBatchNo(string tray_no, int state, string date, string company, int batchno)
        {
            try
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                string sql;
                if(date == null && company== null && batchno == 0)
                {
                    sql = "UPDATE Tray_Active SET Tray_State=" + state + ", Dyeing_Out_Date=NULL, Dyeing_Company_Name=NULL, Batch_No=NULL WHERE Tray_No='" + tray_no + "'";
                }
                else sql = "UPDATE Tray_Active SET Tray_State=" + state + ", Dyeing_Out_Date='"+date+"', Dyeing_Company_Name='"+company+"', Batch_No="+batchno+" WHERE Tray_No='" + tray_no + "'";
                //Console.WriteLine(sql);
                adapter.InsertCommand = new SqlCommand(sql, con);
                adapter.InsertCommand.ExecuteNonQuery();
                //MessageBox.Show("Voucher Edited Successfully", "Success");
            }
            catch (Exception e)
            {
                MessageBox.Show("Could not edit tray state (changeTrayState) \n" + e.Message, "Exception");
            }

            finally
            {
                con.Close();
            }
        }

        //Tray voucher
        public bool addTrayVoucher(DateTime dtinput_date, DateTime dttray_production_date, string tray_no, string spring, int number_of_springs, float tray_tare, float gross_weight, string quality, string company_name, float net_weight)
        {
            string input_date = dtinput_date.Date.ToString("MM-dd-yyyy");
            string tray_production_date = dttray_production_date.Date.ToString("MM-dd-yyyy");
            string fiscal_year = this.getFinancialYear(dttray_production_date);
            //check if tray is present in Tray_Active
            int tray_state = getTrayState(tray_no);
            if (tray_state==1 || tray_state== 2)
            {
                MessageBox.Show("Tray " + tray_no.ToString() + " is already in use", "Error");
                return false;
            }
            //insert into Tray_Active and get unique tray_id
            int tray_id = addTrayActive(tray_production_date, tray_no, spring, number_of_springs, tray_tare, gross_weight, quality, company_name, net_weight, fiscal_year);
            //insert into Tray_Voucher with unique tray_id
            try
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                string sql = "INSERT INTO Tray_Voucher (Input_Date, Tray_Production_Date, Tray_No, Spring, Number_Of_Springs, Tray_Tare, Gross_Weight, Quality, Company_Name, Tray_ID, Net_Weight, Fiscal_Year) VALUES ('" + input_date + "','" + tray_production_date + "', '" + tray_no + "', '" + spring + "', " + number_of_springs + " , " + tray_tare+ ", " + gross_weight + ", '"+quality+"', '"+company_name+"', "+tray_id+", "+net_weight+", '"+fiscal_year+"')";
                Console.WriteLine(sql);
                adapter.InsertCommand = new SqlCommand(sql, con);
                adapter.InsertCommand.ExecuteNonQuery();
                MessageBox.Show("Voucher Added Successfully", "Success");
            }
            catch (Exception e)
            {
                MessageBox.Show("Could not add tray voucher (addTrayVoucher) \n" + e.Message, "Exception");
                con.Close();
                return false;
            }
            finally
            {
                con.Close();
            }
            return true;
        }
        public bool editTrayVoucher(int voucher_id, int tray_id, DateTime dtinput_date, DateTime dttray_production_date, string new_tray_no, string old_tray_no, string spring, int number_of_springs, float tray_tare, float gross_weight, string quality, string company_name, float net_weight)
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
                MessageBox.Show("Tray " + new_tray_no.ToString() + " is already in use", "Error");

                return false;
            }
            //insert into Tray_Voucher with unique tray_id
            try
            {

                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                SqlDataAdapter sda = new SqlDataAdapter();
                string sql = "UPDATE Tray_Voucher SET Tray_Production_Date='" + tray_production_date + "', Tray_No='" + new_tray_no+ "', Spring='"+spring+"', Number_Of_Springs="+number_of_springs+", Tray_Tare="+tray_tare+", Gross_Weight="+gross_weight+", Quality='"+quality+"',  Company_Name='" + company_name + "', Net_Weight=" + net_weight + ", Fiscal_Year = '"+fiscal_year+"' WHERE Voucher_ID='" + voucher_id + "'";
                Console.WriteLine(sql);
                adapter.InsertCommand = new SqlCommand(sql, con);
                adapter.InsertCommand.ExecuteNonQuery();

                sql = "UPDATE Tray_Active SET Tray_Production_Date='" + tray_production_date + "', Tray_No='" + new_tray_no + "', Spring='" + spring + "', Number_Of_Springs=" + number_of_springs + ", Tray_Tare=" + tray_tare + ", Gross_Weight=" + gross_weight + ", Quality='" + quality + "',  Company_Name='" + company_name + "', Net_Weight="+net_weight+", Fiscal_Year = '"+fiscal_year+"' WHERE Tray_ID='" + tray_id + "'";
                Console.WriteLine(sql);
                sda.InsertCommand = new SqlCommand(sql, con);
                sda.InsertCommand.ExecuteNonQuery();

                MessageBox.Show("Voucher Edited Successfully", "Success");
            }
            catch (Exception e)
            {
                MessageBox.Show("Could not edit tray voucher (editTrayVoucher) \n" + e.Message, "Exception");
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
                    MessageBox.Show("Tray Number: " + tray_nos[i] + " at row " + (i + 1).ToString() + " has Date of Production (" + prod.Date.ToString("dd-MM-yyyy") + " earlier than given Date of Issue (" + dtissueDate.Date.ToString("dd-MM-yyyy") + "),", "Error");
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
                this.changeTrayStateDyeingInDateDyeingCompanyNameBatchNo(tray_nos[i], 2, issueDate, dyeing_company_name, batchno);
            }

            try
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                string sql = "INSERT INTO Dyeing_Issue_Voucher (Date_Of_Input, Date_Of_Issue, Quality, Company_Name, Colour, Dyeing_Company_Name, Batch_No, Tray_No_Arr, Number_Of_Trays, Dyeing_Rate, Tray_ID_Arr, Fiscal_Year) VALUES ('" + inputDate + "','" + issueDate + "', '" + quality + "', '" + company_name + "', '" + colour + "' , '" + dyeing_company_name + "', " + batchno + ", '" + trayno + "', "+number+", "+rate+", '"+trayid+"', '"+fiscal_year+"')";
                adapter.InsertCommand = new SqlCommand(sql, con);
                adapter.InsertCommand.ExecuteNonQuery();
                MessageBox.Show("Voucher Added Successfully", "Success");
            }
            catch (Exception e)
            {
                MessageBox.Show("Could not add dyeing issue voucher (addTrayVoucher) \n" + e.Message, "Exception");
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
                    MessageBox.Show("Tray Number: " + tray_nos[i] + " at row " + (i + 1).ToString() + " has Date of Production (" + prod.Date.ToString("dd-MM-yyyy") + " earlier than given Date of Issue (" + dtissueDate.Date.ToString("dd-MM-yyyy") + "),", "Error");
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
                this.changeTrayStateDyeingInDateDyeingCompanyNameBatchNo(old_tray_nos[i], 1, null, null, 0);
            }

            //Send all current trays in Tray_Active to state 2, adding batch_no, dyeing_company_name, dyeing_issue_date
            for (int i = 0; i < tray_nos.Length; i++)
            {
                this.changeTrayStateDyeingInDateDyeingCompanyNameBatchNo(tray_nos[i], 2, issueDate, dyeing_company_name, batchno);
            }

            try
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                string sql = "UPDATE Dyeing_Issue_Voucher Set Date_Of_Issue='"+issueDate+"', Quality='"+quality+"', Company_Name='"+company_name+"', Colour='"+colour+"', Dyeing_Company_Name='"+dyeing_company_name+"', Batch_No="+batchno+", Tray_No_Arr='"+trayno+"', Number_Of_Trays=" + number+" , Dyeing_Rate = "+rate+", Tray_ID_Arr='"+tray_id_arr+"', Fiscal_Year = '"+fiscal_year+"' WHERE Voucher_ID="+voucherID;
                Console.WriteLine(sql);
                adapter.InsertCommand = new SqlCommand(sql, con);
                adapter.InsertCommand.ExecuteNonQuery();
                MessageBox.Show("Voucher Updated Successfully", "Success");
            }
            catch (Exception e)
            {
                MessageBox.Show("Could not edit dyeing issue voucher (editDyeingIssueVoucher) \n" + e.Message, "Exception");
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
        public int getNextBatchNumber(string tablename, DateTime dtissue)
        {
            int ans = -1;
            try
            {
                con.Open();
                string financialyear = this.getFinancialYear(dtissue);
                string sql = "SELECT * FROM Fiscal_Year WHERE Fiscal_Year='"+financialyear+"'";
                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                con.Close();
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if(dt.Rows.Count==0)
                {
                    con.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    sql = "INSERT INTO Fiscal_Year VALUES ('" + financialyear + "', 0 ,'0')";
                    adapter.InsertCommand = new SqlCommand(sql, con);
                    adapter.InsertCommand.ExecuteNonQuery();
                    
                    sql = "SELECT * FROM Fiscal_Year WHERE Fiscal_Year='" + financialyear + "'";
                    SqlDataAdapter sda1 = new SqlDataAdapter(sql, con);
                    sda1.Fill(dt);
                    con.Close();
                }
                ans = 1+int.Parse(dt.Rows[0]["Highest_Batch_No"].ToString()); 
            }
            catch(Exception e)
            {
                MessageBox.Show("Cannot get next batch number (getNextBatchNumber)\n" + e.Message, "Exception");
            }
            finally
            {
                con.Close();
            }
            return ans;
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
                MessageBox.Show("Could not Batch (addBatch) \n" + e.Message, "Exception");
                con.Close();
                return false;
            }
            finally
            {
                con.Close();
            }
            return true;
        }
        public int getBatchState(int batch_no)
        {
            int ans = -1;
            DataTable dt = new DataTable(); //this is creating a virtual table  
            try
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT Batch_State FROM Batch WHERE Batch_No="+batch_no+"", con);
                sda.Fill(dt);
                if(dt.Rows.Count!=0)
                {
                    ans=int.Parse(dt.Rows[0][0].ToString());
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Could not connect to database (getBatchState) \n" + e.Message, "Exception");
            }
            finally
            {
                con.Close();

            }
            return ans;
        }
        public void sendBatchStateDateBillNo(int batch_no, int state, string date, int bill_no, string batch_fiscal_year)
        {
            try
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                string sql;
                if(date==null && bill_no==-1)
                {
                    sql = "UPDATE Batch SET Batch_State=" + state + ", Dyeing_In_Date=NULL, Bill_No=NULL WHERE Batch_No= " + batch_no+" AND Fiscal_Year = '"+batch_fiscal_year+"'";
                }
                else
                {
                    sql = "UPDATE Batch SET Batch_State=" + state + ", Dyeing_In_Date='" + date + "', Bill_No="+bill_no+" WHERE Batch_No= " + batch_no+ " AND Fiscal_Year = '" + batch_fiscal_year + "'";
                }
                adapter.InsertCommand = new SqlCommand(sql, con);
                adapter.InsertCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show("Could not edit Batch State (sendBatchStateDate)", "Exception");
            }
            finally
            {
                con.Close();
            }
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
                MessageBox.Show("Could not Batch (addBatch) \n" + e.Message, "Exception");
                con.Close();
                return false;
            }
            finally
            {
                con.Close();
            }
            return true;
        }
        public string[] getBatch_StateDyeingCompanyColourQuality(int state, string dyeing_company, string colour, string quality, string fiscal_year)
        {
            DataTable dt = new DataTable(); //this is creating a virtual table  
            string[] batch_nos = null;
            try
            {
                con.Open();
                if(dyeing_company==null)
                {
                    SqlDataAdapter sda = new SqlDataAdapter("SELECT Batch_No FROM Batch WHERE Batch_State=" + state + " AND Fiscal_Year ='"+fiscal_year+"' ", con);
                    sda.Fill(dt);
                }
                else if(colour!=null && quality != null)
                {
                    SqlDataAdapter sda = new SqlDataAdapter("SELECT Batch_No FROM Batch WHERE Batch_State=" + state + " AND Dyeing_Company_Name='"+dyeing_company+"' AND Colour='"+colour+"' AND Quality='"+quality+ "' AND Fiscal_Year ='" + fiscal_year + "'", con);
                    sda.Fill(dt);
                }
                else
                {
                    SqlDataAdapter sda = new SqlDataAdapter("SELECT Batch_No FROM Batch WHERE Batch_State=" + state + " AND Dyeing_Company_Name='" + dyeing_company + "' AND Fiscal_Year ='" + fiscal_year + "'", con);
                    sda.Fill(dt);
                }
                string temp = "";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    temp += dt.Rows[i][0].ToString() + ",";
                }
                batch_nos = this.csvToArray(temp);
            }
            catch (Exception e)
            {
                MessageBox.Show("Could not connect to database (getBatch_StateDyeingCompanyColourQuality) " + e.Message, "Exception");
            }
            finally
            {
                con.Close();
            }
            return batch_nos;
        }
        public string[] getTrayIDsFromBatch(int batch_no, string batch_fiscal_year)
        {
            DataTable dt = new DataTable();
            string[] tray_ids = null;
            try
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT Tray_ID_Arr FROM Batch WHERE Batch_No=" + batch_no + " AND Fiscal_Year='"+batch_fiscal_year+"'", con);
                sda.Fill(dt);
                if (dt.Rows.Count != 0)
                {
                    tray_ids = csvToArray(dt.Rows[0][0].ToString());
                }

            }
            catch (Exception e)
            {
                MessageBox.Show("Could not get tray ids tray (getTrayIDs) \n" + e.Message, "Exception");
            }

            finally
            {
                con.Close();
            }
            return tray_ids;
        }
        public string[] getBatchesWithBillNoDyeingCompanyName(int bill_no, string dyeing_company, string fiscal_year)
        {
            DataTable dt = new DataTable();
            string[] batch_nos = null;
            try
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT Batch_No FROM Batch WHERE Bill_No=" + bill_no+ " AND Dyeing_Company_Name='"+dyeing_company+"' AND Fiscal_Year ='"+fiscal_year+"' ", con);
                sda.Fill(dt);
                string temp = "";
                for(int i=0; i<dt.Rows.Count; i++)
                {
                    temp += dt.Rows[i][0].ToString() + ",";
                }
                batch_nos = this.csvToArray(temp);

            }
            catch (Exception e)
            {
                MessageBox.Show("Could not get batch nos (getBatchesWithBillNo) \n" + e.Message, "Exception");
            }

            finally
            {
                con.Close();
            }
            return batch_nos;
        }
        public void addBillNo(int batch_no, int bill_no, string batch_fiscal_year)
        {
            try
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                string sql;
                if (bill_no == -1)
                {
                    sql = "UPDATE Batch SET Bill_No=NULL WHERE Batch_No= " + batch_no+" AND Fiscal_Year='"+batch_fiscal_year+"'";
                }
                else
                {
                    sql = "UPDATE Batch SET Bill_No=" + bill_no + " WHERE Batch_No= " + batch_no+ " AND Fiscal_Year='" + batch_fiscal_year + "'";
                }
                Console.WriteLine(sql);
                adapter.InsertCommand = new SqlCommand(sql, con);
                adapter.InsertCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show("Could not add BillNo (addBillNo)", "Exception");
            }
            finally
            {
                con.Close();
            }
        }
        public string getColumnBatchNo(string column, int batch_no)
        {
            string ans = null;
            try
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT "+column+" FROM Batch WHERE Batch_No=" + batch_no + "", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count != 0)
                {
                    ans = dt.Rows[0][0].ToString();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Could not connect to database (getColumnBatchNo) "+column+"\n" + e.Message, "Exception");
            }
            finally
            {
                con.Close();

            }
            return ans;
        }
        public DataRow getRow_BatchNo(int batch_no)
        {
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Batch WHERE Batch_No=" + batch_no + "", con);
                sda.Fill(dt);
            }
            catch (Exception e)
            {
                MessageBox.Show("Could not connect to database (getRow_BatchNo) \n" + e.Message, "Exception");
                con.Close();
                return null;
            }
            finally
            {
                con.Close();

            }
            return (DataRow)dt.Rows[0];
        }


        //Dye In Voucher
        public bool addDyeingInwardVoucher(DateTime dtinput_date, DateTime dtinward_date, string dyeing_company_name, int bill_no, string batch_no_arr, string batch_fiscal_year)
        {
            string input_date = dtinput_date.Date.ToString("MM-dd-yyyy").Substring(0, 10);
            string inward_date = dtinward_date.Date.ToString("MM-dd-yyyy").Substring(0, 10);
            string fiscal_year = this.getFinancialYear(dtinward_date);
            //check if batch issue date of all batches are <= inward date
            string[] batchnos = csvToArray(batch_no_arr);
            for (int i = 0; i < batchnos.Length; i++)
            {
                con.Open();
                SqlDataAdapter sda1 = new SqlDataAdapter("SELECT Dyeing_Out_Date FROM Batch WHERE Batch_No='" + batchnos[i] + "' AND Fiscal_Year='"+batch_fiscal_year+"' ", con);
                DataTable dt = new DataTable();
                sda1.Fill(dt);
                con.Close();
                DateTime outDate = Convert.ToDateTime(dt.Rows[0]["Dyeing_Out_Date"].ToString());
                if (dtinward_date < outDate)
                {
                    MessageBox.Show("Batch Number: " + batchnos[i] + " at row " + (i + 1).ToString() + " has Date of Issue to Dyeing (" + outDate.Date.ToString("dd-MM-yyyy") + ") earlier than given Inward date (" + dtinward_date.Date.ToString("dd-MM-yyyy") + "),", "Error");
                    return false;
                }
            }
            //Set Batch State to 2 for each batch
            for(int i=0;i<batchnos.Length;i++)
            {
                sendBatchStateDateBillNo(int.Parse(batchnos[i]),2, inward_date, bill_no, batch_fiscal_year);
                string[] tray_ids = getTrayIDsFromBatch(int.Parse(batchnos[i]), batch_fiscal_year);
                //for each trayid in batch, set dyeing in date. Delete it from Tray_Active and push to Tray_History
                for(int j=0;j<tray_ids.Length;j++)
                {
                    int tray_id = int.Parse(tray_ids[j]);
                    bool freed= freeTray(tray_id, inward_date);
                }
            }

            try
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                string sql = "INSERT INTO Dyeing_Inward_Voucher (Date_Of_Input, Inward_Date, Dyeing_Company_Name, Bill_No, Batch_No_Arr, Fiscal_Year, Batch_Fiscal_Year) VALUES ('" + input_date + "', '" + inward_date + "', '" + dyeing_company_name + "', " + bill_no + " , '" + batch_no_arr + "', '"+fiscal_year+"', '"+batch_fiscal_year+"')";
                Console.WriteLine(sql);
                adapter.InsertCommand = new SqlCommand(sql, con);
                adapter.InsertCommand.ExecuteNonQuery();
                MessageBox.Show("Voucher Added Successfully", "Success");
            }
            catch (Exception e)
            {
                MessageBox.Show("Could not add Dyeing Inward Voucher (addDyeingInwardVoucher) \n" + e.Message, "Exception");
                con.Close();
                return false;
            }
            finally
            {
                con.Close();
            }
            return true;
        }
        public bool editDyeingInwardVoucher(int voucher_id, DateTime dtinput_date, DateTime dtinward_date, string dyeing_company_name, int bill_no, string batch_no_arr, string batch_fiscal_year)
        {
            string input_date = dtinput_date.Date.ToString("MM-dd-yyyy").Substring(0, 10);
            string inward_date = dtinward_date.Date.ToString("MM-dd-yyyy").Substring(0, 10);
            string fiscal_year = this.getFinancialYear(dtinward_date);
            //check if batch issue date of all batches are <= inward date
            string[] batchnos = csvToArray(batch_no_arr);
            for (int i = 0; i < batchnos.Length; i++)
            {
                con.Open();
                SqlDataAdapter sda1 = new SqlDataAdapter("SELECT Dyeing_Out_Date FROM Batch WHERE Batch_No='" + batchnos[i] + "' AND Fiscal_Year='" + batch_fiscal_year + "' ", con);
                DataTable dt = new DataTable();
                sda1.Fill(dt);
                con.Close();
                DateTime outDate = Convert.ToDateTime(dt.Rows[0]["Dyeing_Out_Date"].ToString());
                if (dtinward_date < outDate)
                {
                    MessageBox.Show("Batch Number: " + batchnos[i] + " at row " + (i + 1).ToString() + " has Date of Issue to Dyeing (" + outDate.Date.ToString("dd-MM-yyyy") + ") earlier than given Inward date (" + dtinward_date.Date.ToString("dd-MM-yyyy") + "),", "Error");
                    return false;
                }
            }
            //get old batch numbers
            //Get all batch_nos which were previously present
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("SELECT Batch_No_Arr FROM Dyeing_Inward_Voucher WHERE Voucher_ID=" + voucher_id + "", con);
            DataTable old = new DataTable();
            sda.Fill(old);
            con.Close();
            string[] old_batch_nos = this.csvToArray(old.Rows[0][0].ToString());
            //send old batch numbers to state 1 and remove Dyeing_In_Date 
            for (int i = 0; i < old_batch_nos.Length; i++)
            {
                sendBatchStateDateBillNo(int.Parse(old_batch_nos[i]), 1, null, -1, batch_fiscal_year);
                string[] tray_ids = getTrayIDsFromBatch(int.Parse(old_batch_nos[i]), batch_fiscal_year);
                //for each trayid in batch, remove dyeing in date. Delete it from Tray_History and push to Tray_Active
                for (int j = 0; j < tray_ids.Length; j++)
                {
                    int tray_id = int.Parse(tray_ids[j]);
                    bool freed = unfreeTray(tray_id);
                }
            }
            //Add all the new batches and respective tray ids            
            //Set Batch State to 2 for each batch
            for (int i = 0; i < batchnos.Length; i++)
            {
                sendBatchStateDateBillNo(int.Parse(batchnos[i]), 2, inward_date, bill_no, batch_fiscal_year);
                string[] tray_ids = getTrayIDsFromBatch(int.Parse(batchnos[i]), batch_fiscal_year);
                //for each trayid in batch, set dyeing in date. Delete it from Tray_Active and push to Tray_History
                for (int j = 0; j < tray_ids.Length; j++)
                {
                    int tray_id = int.Parse(tray_ids[j]);
                    bool freed = freeTray(tray_id, inward_date);
                }
            }

            try
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                string sql = "UPDATE Dyeing_Inward_Voucher SET Inward_Date = '" + inward_date + "' , Dyeing_Company_Name ='" + dyeing_company_name + "',  Bill_No =" + bill_no + " , Batch_No_Arr ='" + batch_no_arr + "', Fiscal_Year='"+fiscal_year+"' WHERE Voucher_ID ="+voucher_id+"";
                adapter.InsertCommand = new SqlCommand(sql, con);
                adapter.InsertCommand.ExecuteNonQuery();
                MessageBox.Show("Voucher Edited Successfully", "Success");
            }
            catch (Exception e)
            {
                MessageBox.Show("Could not edit Dyeing Inward Voucher (editDyeingInwardVoucher) \n" + e.Message, "Exception");
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
        public bool addBillNosVoucher(int sendbill_no, DateTime dtinput_date, string batch_nos, string dyeing_company, string batch_fiscal_year)
        {
            string input_date = dtinput_date.Date.ToString("MM-dd-yyyy").Substring(0, 10);
            //add bill nos to batches
            string[] batches = this.csvToArray(batch_nos);
            for(int i=0; i<batches.Length; i++)
            {
                addBillNo(int.Parse(batches[i]), sendbill_no, batch_fiscal_year);
            }
            string fiscal_year = this.getFinancialYear(dtinput_date);
            //save voucher
            try
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                string sql = "INSERT INTO BillNos_Voucher (Date_Of_Input, Batch_No_Arr, Dyeing_Company_Name, Bill_No, Batch_Fiscal_Year, Fiscal_Year) VALUES ('"+input_date+"','" + batch_nos + "','"+dyeing_company+"',"+sendbill_no+",'"+batch_fiscal_year+"', '"+fiscal_year+"')";
                Console.WriteLine(sql);
                adapter.InsertCommand = new SqlCommand(sql, con);
                adapter.InsertCommand.ExecuteNonQuery();
                MessageBox.Show("Voucher Added Successfully", "Success");
            }
            catch (Exception e)
            {
                MessageBox.Show("Could not add Dyeing Inward Voucher (addDyeingInwardVoucher) \n" + e.Message, "Exception");
                con.Close();
                return false;
            }
            finally
            {
                con.Close();
            }
            return true;
        }
        public bool editBillNosVoucher(int voucher_id, int sendbill_no, DateTime dtinput_date, string batch_nos, string dyeing_company, string batch_fiscal_year)
        {
            string input_date = dtinput_date.Date.ToString("MM-dd-yyyy").Substring(0, 10);
            string fiscal_year = this.getFinancialYear(dtinput_date);

            //Get all batch_nos which were previously present
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("SELECT Batch_No_Arr FROM BillNos_Voucher WHERE Voucher_ID=" + voucher_id + "", con);
            DataTable old = new DataTable();
            sda.Fill(old);
            con.Close();
            string[] old_batch_nos = this.csvToArray(old.Rows[0][0].ToString());

            //send old batch nos to bill no 0
            for (int i = 0; i < old_batch_nos.Length; i++)
            {
                addBillNo(int.Parse(old_batch_nos[i]), 0, batch_fiscal_year);
            }

            //add bill nos to current batches
            string[] batches = this.csvToArray(batch_nos);
            for (int i = 0; i < batches.Length; i++)
            {
                addBillNo(int.Parse(batches[i]), sendbill_no, batch_fiscal_year);
            }

            //update voucher
            try
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                string sql = "UPDATE BillNos_Voucher SET Batch_No_Arr='" + batch_nos + "', Dyeing_Company_Name='" + dyeing_company + "', Bill_No="+sendbill_no+", Fiscal_Year='"+fiscal_year+"' WHERE Voucher_ID="+voucher_id+"";
                Console.WriteLine(sql);
                adapter.InsertCommand = new SqlCommand(sql, con);
                adapter.InsertCommand.ExecuteNonQuery();
                MessageBox.Show("Voucher Updated Successfully", "Success");
            }
            catch (Exception e)
            {
                MessageBox.Show("Could not update Dyeing Inward Voucher (editBillNosVoucher) \n" + e.Message, "Exception");
                con.Close();
                return false;
            }
            finally
            {
                con.Close();
            }
            return true;
        }
    }

    
}

