using HMS_Software_V2.General_Purpose;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
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

namespace HMS_Software_V2.Admin.Admin_UserControls
{
    /// <summary>
    /// Interaction logic for Admin_Reception_Search.xaml
    /// </summary>
    public partial class Admin_Reception_Search : Window
    {
        ObservableCollection<Reception> Receptions { get; set; } = new ObservableCollection<Reception>();
        public Admin_Reception_Search()
        {
            InitializeComponent();

            MyShowTable();

            showReception_DataGrid.ItemsSource = Receptions;
        }

        private void MyShowTable()
        {
            using (SqlConnection connection = new Database_Connector().GetConnection())
            {
                try
                {
                    connection.Open();

                    #region Get Patient Details

                    string query = "SELECT Reception_ID, R_FullName, R_NameWithInitials, R_Age, R_Gender, R_NIC FROM Reception";
                    SqlCommand command = new SqlCommand(query, connection);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Receptions.Add(new Reception
                            {
                                RecptionID = reader.GetInt32(reader.GetOrdinal("Reception_ID")),
                                R_FullName = reader.IsDBNull(reader.GetOrdinal("R_FullName")) ? null : reader.GetString(reader.GetOrdinal("R_FullName")),
                                R_NameWithInitials = reader.IsDBNull(reader.GetOrdinal("R_NameWithInitials")) ? null : reader.GetString(reader.GetOrdinal("R_NameWithInitials")),
                                R_Age = reader.GetInt32(reader.GetOrdinal("R_Age")),
                                R_Gender = reader.IsDBNull(reader.GetOrdinal("R_Gender")) ? null : reader.GetString(reader.GetOrdinal("R_Gender")),
                                R_NIC = reader.IsDBNull(reader.GetOrdinal("R_NIC")) ? null : reader.GetString(reader.GetOrdinal("R_NIC")),
                            });

                        }
                    }

                    #endregion


                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                finally
                {
                    connection.Close();
                }


            }

        }

        class Reception
        {
            public int RecptionID { get; set; }
            public string? R_FullName { get; set; }
            public string? R_NameWithInitials { get; set; }
            public int R_Age { get; set; }
            public string? R_Gender { get; set; }
            public string? R_NIC { get; set; }
            
        }

        private void SearchBar_tbx_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterRecords(SearchBar_tbx.Text);

        }

        private void FilterRecords(string searchText)
        {
            var selectedColumn = FilterColumn_ComboBox.SelectedItem as ComboBoxItem;
            var selectedContent = selectedColumn?.Content.ToString();

            var filteredDoctors = new ObservableCollection<Reception>();

            if (string.IsNullOrWhiteSpace(searchText))
            {
                filteredDoctors = Receptions;
            }
            else
            {
                switch (selectedContent)
                {
                    case "By Name":
                        filteredDoctors = new ObservableCollection<Reception>(
                            Receptions.Where(d => d.R_FullName != null && d.R_FullName.Contains(searchText, StringComparison.OrdinalIgnoreCase)));
                        break;
                    case "By ID":
                        filteredDoctors = new ObservableCollection<Reception>(
                            Receptions.Where(d => d.RecptionID.ToString().Contains(searchText)));
                        break;
                        // Add cases for other columns as needed
                }
            }

            showReception_DataGrid.ItemsSource = filteredDoctors;
        }

        private void FilterColumn_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SearchBar_tbx.Text = "";

        }
    }
}
