using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;
using System.Windows;
using System.Diagnostics;

namespace HMS_Software_V2.General_Purpose
{
    public class Database_Connector
    {
        private string connStr;
        private SqlConnection connect;

        public Database_Connector()
        {
            /*string server = "your-database-url";
            string user = "your-username";
            string database = "your-database";
            string port = "3306";
            string password = "your-password";*/

            connStr = @"Data Source=MITHILA\SQLEXPRESS;Initial Catalog=HMS_V2;Integrated Security=True;Connect Timeout=30;Encrypt=False";
            connect = new SqlConnection(connStr);
        }

        public SqlConnection GetConnection()
        {
            try
            {
                connect.Open();
                Debug.WriteLine("\n\n\n\n\nConnection Opened Successfully");
               /* MessageBox.Show("Connection Opened Successfully");*/
                connect.Close();
            }
            catch (SqlException ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
                MessageBox.Show(ex.Message);
            }
            return connect;
        }
    }
}
