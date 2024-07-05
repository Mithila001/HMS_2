using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HMS_Software_V2.General_Purpose
{
    public class ShowCasePurposeONLY_UpdateRecords
    {
        public void UpdateRecords()
        {
            Debug.WriteLine("UpdateRecords");


            using (SQLiteConnection connection = new Database_Connector().GetConnection())
            {
                string todayDate = DateTime.Now.ToString("yyyy-MM-dd");

                try
                {
                    connection.Open();


                    string updateQuery = "UPDATE ClinicEvents SET CE_Date = @TodayDate";

                    SQLiteCommand cmd = new SQLiteCommand(updateQuery, connection);

                    cmd.Parameters.AddWithValue("@TodayDate", todayDate);

                    try
                    {

                        int rowsAffected = cmd.ExecuteNonQuery();

                        Debug.WriteLine($"{rowsAffected} Records Updated");
                    }
                    catch (SQLiteException ex)
                    {
                        MessageBox.Show("Faild to update Table Records to Todays Date: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                    }





                    //string updateQuery2 = "UPDATE ClinicEvents SET CE_Date = @TodayDate";

                    //SQLiteCommand cmd2 = new SQLiteCommand(updateQuery2, connection);

                    //cmd2.Parameters.AddWithValue("@TodayDate", todayDate);

                    //try
                    //{

                    //    int rowsAffected = cmd2.ExecuteNonQuery();

                    //    Debug.WriteLine($"{rowsAffected} Records Updated");
                    //}
                    //catch (SQLiteException ex)
                    //{
                    //    MessageBox.Show("Faild to update Table Records to Todays Date: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                    //}










                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show("Failed to connect to Database: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    connection.Close();
                }
                

            }

        }

    }
}
