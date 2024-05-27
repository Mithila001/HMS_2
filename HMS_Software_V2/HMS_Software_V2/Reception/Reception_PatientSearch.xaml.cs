using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using HMS_Software_V2.General_Purpose;

namespace HMS_Software_V2.Reception
{
    /// <summary>
    /// Interaction logic for Reception_PatientSearch.xaml
    /// </summary>
    public partial class Reception_PatientSearch : Window
    {
        public Reception_PatientSearch()
        {
            InitializeComponent();

            ShowTableData();
        }

        private void ShowTableData()
        {
            try
            {
                using (SqlConnection connection = new Database_Connector().GetConnection())
                {
                    connection.Open();
                    DataTable table = new DataTable();

                    string query1 = "SELECT * FROM Patient";

                    using(SqlDataAdapter adapter = new SqlDataAdapter(query1, connection))
                    {
                        adapter.Fill(table);
                    }

                    showPatientsRecord_DataGW.ItemsSource = table.DefaultView;

                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error1: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
           


        }

        private void SearchBar_tbx_TextChanged(object sender, TextChangedEventArgs e)
        {
            Debug.WriteLine($"\n --- TextBox_TextChanged --- \n");
            string searchText = SearchBar_tbx.Text.Trim();

            // Get the default view of the data grid's items source
            DataView dataView = ((DataView)showPatientsRecord_DataGW.ItemsSource);

            if (!string.IsNullOrEmpty(searchText))
            {
                
                dataView.RowFilter = $"[P_NameWithIinitials] LIKE '%{searchText}%'"; 
            }
            else
            {

                dataView.RowFilter = "";
            }

        }
    }
}
