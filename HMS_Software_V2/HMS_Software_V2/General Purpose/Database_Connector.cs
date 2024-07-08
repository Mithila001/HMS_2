﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Windows;
using System.Diagnostics;
using System.Data;
using System.IO;

namespace HMS_Software_V2.General_Purpose
{
    #region SQL Server Connection
    //public class Database_Connector
    //{
    //    private string connStr;
    //    private SQLiteConnection connect;


    //    public Database_Connector()
    //    {
    //        /*string server = "your-database-url";
    //        string user = "your-username";
    //        string database = "your-database";
    //        string port = "3306";
    //        string password = "your-password";*/

    //        connStr = @"Data Source=MITHILA\SQLEXPRESS;Initial Catalog=HMS_V2;Integrated Security=True;Connect Timeout=30;Encrypt=False";
    //        connect = new SQLiteConnection(connStr);
    //    }

    //    public SQLiteConnection GetConnection()
    //    {
    //        if (connect.State == ConnectionState.Closed)
    //        {
    //            try
    //            {
    //                connect.Open();
    //                Debug.WriteLine("Connection Opened Successfully");
    //            }
    //            catch (SqlSQLiteException ex)
    //            {
    //                Debug.WriteLine("Error: " + ex.Message);
    //                MessageBox.Show(ex.Message);
    //            }
    //            finally
    //            {
    //                connect.Close();
    //            }
    //        }
    //        return connect;
    //    }

    //    public void CloseConnection()
    //    {
    //        if (connect.State == ConnectionState.Open)
    //        {
    //            connect.Close();
    //            Debug.WriteLine("Connection Closed Successfully");
    //        }
    //    }
    //} 
    #endregion



    public class Database_Connector
    {
        private string? connStr;
        private SQLiteConnection? connect;

        public Database_Connector()
        {


            // Get the base directory (typically points to bin\Debug or bin\Release when running from Visual Studio)
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;




            // --------------------------- Uncomment the following lines to run the application from Visual Studio ---------------------------
            #region Use this if you want to connect to the database From Visual Studio
            /**

                string projectDirectory = Path.GetFullPath(Path.Combine(baseDirectory, @"..\..\..\"));

                Construct the absolute path to the database file
                string databasePath = Path.Combine(projectDirectory, @"HMS_SqlLite_Database1.db"); 

                Check if the database file exists
                if (!File.Exists(databasePath)) 
                {
                    Debug.WriteLine($"Database file not found: {databasePath}");
                    MessageBox.Show($"databasePath: {databasePath}");
                    MessageBox.Show($"Database file not found: {databasePath}");
                    return;
                } 
                Debug.WriteLine($"\n\ndatabasePath: {databasePath}");


                //  Use the first connStr line to run the application from Visual Studio    

                connStr = $@"Data Source={databasePath}; Version=3;";

                **/  
            #endregion




            connStr = $@"Data Source={baseDirectory}HMS_SqlLite_Database1.db; Version=3;"; //Realese vertion Connetion string
        }

        private SQLiteConnection EnsureConnection()
        {
            if (connect == null)
            {
                if (string.IsNullOrEmpty(connStr))
                {
                    MessageBox.Show("Connection string is not initialized.");
                    throw new InvalidOperationException("Connection string has not been initialized.");
                   
                    
                }

                connect = new SQLiteConnection(connStr);
            }

            return connect!;
        }

        public SQLiteConnection GetConnection()
        {
            var connection = EnsureConnection();

            if (connection.State != ConnectionState.Open)
            {
                try
                {
                    connection.Open();
                    Debug.WriteLine("Connection Opened Successfully");
                }
                catch (SQLiteException ex)
                {
                    Debug.WriteLine("Error(Database_Connector): " + ex.Message);
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
            return connection;
        }
    }
}

