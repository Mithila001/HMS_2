using HMS_Software_V2._DataManage_Classes;
using HMS_Software_V2.General_Purpose;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Linq;
using System.Numerics;
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
    /// Interaction logic for Admin_Doctor_Search.xaml
    /// </summary>
    public partial class Admin_Doctor_Search : Window
    {
        ObservableCollection<Doctor> Doctors { get; set; } = new ObservableCollection<Doctor>();
        public Admin_Doctor_Search()
        {
            InitializeComponent();

            adminName_lbl.Content = SharedData.adminData.AdminName;
            #region Get and Assign Date Time
            int day = DateTime.Now.Day;
            string daySuffix = day switch
            {
                1 or 21 or 31 => "st",
                2 or 22 => "nd",
                3 or 23 => "rd",
                _ => "th"
            };

            todatDate_lbl.Content = $"{day}{daySuffix} {DateTime.Now:MMMM yyyy}";

            todayTime_lbl.Content = DateTime.Now.ToString("hh:mm: tt");
            #endregion


            MyShowTable();

            showDoctors_DataGrid.ItemsSource = Doctors;
        }

        //int Doctor_ID;
        //string? D_FullName;
        //string? D_NameWithInitials;
        //int D_Age;
        //string? D_Gender;

        private void MyShowTable()
        {
            using (SQLiteConnection connection = new Database_Connector().GetConnection())
            {
                try
                {
                    connection.Open();

                    #region Get Total Doctors Count

                    string query = "SELECT Doctor_ID, D_FullName, D_NameWithInitials, D_Age, D_Gender, D_NIC, D_ContactNo, D_Email, D_Position, D_Specialty, D_LicenceNumber FROM Doctor";
                    SQLiteCommand command = new SQLiteCommand(query, connection);

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Doctors.Add(new Doctor
                            {
                                Doctor_ID = reader.GetInt32(reader.GetOrdinal("Doctor_ID")),
                                D_FullName = reader.IsDBNull(reader.GetOrdinal("D_FullName")) ? null : reader.GetString(reader.GetOrdinal("D_FullName")),
                                D_NameWithInitials = reader.IsDBNull(reader.GetOrdinal("D_NameWithInitials")) ? null : reader.GetString(reader.GetOrdinal("D_NameWithInitials")),
                                D_Age = reader.IsDBNull(reader.GetOrdinal("D_Age")) ? 0 : reader.GetInt32(reader.GetOrdinal("D_Age")),
                                D_Gender = reader.IsDBNull(reader.GetOrdinal("D_Gender")) ? null : reader.GetString(reader.GetOrdinal("D_Gender")),
                                D_NIC = reader.IsDBNull(reader.GetOrdinal("D_NIC")) ? null : reader.GetString(reader.GetOrdinal("D_NIC")),
                                D_ContactNo = reader.IsDBNull(reader.GetOrdinal("D_ContactNo")) ? null : reader.GetString(reader.GetOrdinal("D_ContactNo")),
                                D_Email = reader.IsDBNull(reader.GetOrdinal("D_Email")) ? null : reader.GetString(reader.GetOrdinal("D_Email")),
                                D_Position = reader.IsDBNull(reader.GetOrdinal("D_Position")) ? null : reader.GetString(reader.GetOrdinal("D_Position")),
                                D_Specialization = reader.IsDBNull(reader.GetOrdinal("D_Specialty")) ? null : reader.GetString(reader.GetOrdinal("D_Specialty")),
                                D_License = reader.IsDBNull(reader.GetOrdinal("D_LicenceNumber")) ? null : reader.GetString(reader.GetOrdinal("D_LicenceNumber"))

                            });

                        }
                    }

                    #endregion


                }
                catch (SQLiteException ex)
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

        private void SearchBar_tbx_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterRecords(SearchBar_tbx.Text);
        }


        private void FilterRecords(string searchText)
        {
            var selectedColumn = FilterColumn_ComboBox.SelectedItem as ComboBoxItem;
            var selectedContent = selectedColumn?.Content.ToString();

            var filteredDoctors = new ObservableCollection<Doctor>();

            if (string.IsNullOrWhiteSpace(searchText))
            {
                filteredDoctors = Doctors;
            }
            else
            {
                switch (selectedContent)
                {
                    case "By Name":
                        filteredDoctors = new ObservableCollection<Doctor>(
                            Doctors.Where(d => d.D_FullName != null && d.D_FullName.Contains(searchText, StringComparison.OrdinalIgnoreCase)));
                        break;
                    case "By ID":
                        filteredDoctors = new ObservableCollection<Doctor>(
                            Doctors.Where(d => d.Doctor_ID.ToString().Contains(searchText)));
                        break;
                        // Add cases for other columns as needed
                }
            }

            showDoctors_DataGrid.ItemsSource = filteredDoctors;
        }

        private void FilterColumn_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SearchBar_tbx.Text = "";
        }
    }



    class Doctor
    {
        public int Doctor_ID { get; set; }
        public string? D_FullName { get; set; }
        public string? D_NameWithInitials { get; set; }
        public int D_Age { get; set; }
        public string? D_Gender { get; set; }

        public string? D_NIC { get; set; }
        public string? D_ContactNo { get; set; }
        public string? D_Email { get; set; }
        public string? D_Position { get; set; }
        public string? D_Specialization { get; set; }
        public string? D_License { get; set; }
    }
}
