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

namespace HMS_Software_V2.Admin
{
    /// <summary>
    /// Interaction logic for Admin_Patient_Search.xaml
    /// </summary>
    public partial class Admin_Patient_Search : Window
    {
        ObservableCollection<Patient> Patients { get; set; } = new ObservableCollection<Patient>();
        public Admin_Patient_Search()
        {
            InitializeComponent();

            MyShowTable();

            showPatient_DataGrid.ItemsSource = Patients;
        }

        private void MyShowTable()
        {
            using (SqlConnection connection = new Database_Connector().GetConnection())
            {
                try
                {
                    connection.Open();

                    #region Get Patient Details

                    string query = "SELECT Patient_ID, P_FullName, P_NameWithIinitials, P_Age, P_Gender, P_NIC, P_CurrentStatus FROM Patient";
                    SqlCommand command = new SqlCommand(query, connection);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Patients.Add(new Patient
                            {
                                PatientID = reader.GetInt32(reader.GetOrdinal("Patient_ID")),
                                P_FullName = reader.IsDBNull(reader.GetOrdinal("P_FullName")) ? null : reader.GetString(reader.GetOrdinal("P_FullName")),
                                P_NameWithInitials = reader.IsDBNull(reader.GetOrdinal("P_NameWithIinitials")) ? null : reader.GetString(reader.GetOrdinal("P_NameWithIinitials")),
                                P_Age = reader.IsDBNull(reader.GetOrdinal("P_Age")) ? null : reader.GetString(reader.GetOrdinal("P_Age")),
                                P_Gender = reader.IsDBNull(reader.GetOrdinal("P_Gender")) ? null : reader.GetString(reader.GetOrdinal("P_Gender")),
                                P_NIC = reader.IsDBNull(reader.GetOrdinal("P_NIC")) ? null : reader.GetString(reader.GetOrdinal("P_NIC")),
                                P_Status = reader.IsDBNull(reader.GetOrdinal("P_CurrentStatus")) ? null : reader.GetString(reader.GetOrdinal("P_CurrentStatus"))
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

        class Patient
        {
            public int PatientID { get; set; }
            public string? P_FullName { get; set; }
            public string? P_NameWithInitials { get; set; }
            public string? P_Age { get; set; }
            public string? P_Gender { get; set; }
            public string? P_NIC { get; set; }
            public string? P_Status { get; set; }
        }

        private void SearchBar_tbx_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterRecords(SearchBar_tbx.Text);

        }

        private void FilterRecords(string searchText)
        {
            var selectedColumn = FilterColumn_ComboBox.SelectedItem as ComboBoxItem;
            var selectedContent = selectedColumn?.Content.ToString();

            var filteredDoctors = new ObservableCollection<Patient>();

            if (string.IsNullOrWhiteSpace(searchText))
            {
                filteredDoctors = Patients;
            }
            else
            {
                switch (selectedContent)
                {
                    case "By Name":
                        filteredDoctors = new ObservableCollection<Patient>(
                            Patients.Where(d => d.P_FullName != null && d.P_FullName.Contains(searchText, StringComparison.OrdinalIgnoreCase)));
                        break;
                    case "By ID":
                        filteredDoctors = new ObservableCollection<Patient>(
                            Patients.Where(d => d.PatientID.ToString().Contains(searchText)));
                        break;
                        // Add cases for other columns as needed
                }
            }

            showPatient_DataGrid.ItemsSource = filteredDoctors;
        }

        private void FilterColumn_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SearchBar_tbx.Text = "";
        }
    }
}
