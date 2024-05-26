using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using HMS_Software_V2.General_Purpose;
using System.Data.SqlClient;
using System.Diagnostics;



namespace HMS_Software_V2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Open the connection
            using (SqlConnection connect = new Database_Connector().GetConnection())
            {
                connect.Open();

                // Execute your SQL query
                string sql = "SELECT * FROM TestTable"; // replace 'your_table' with your actual table name
                SqlCommand cmd = new SqlCommand(sql, connect);
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Debug.WriteLine( " ------------------------------------------------------- "+ rdr[0]);
                }

                rdr.Close();
            }





        }
    }
}