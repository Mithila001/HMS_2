using HMS_Software_V2._DataManage_Classes;
using HMS_Software_V2.Admin.Admin_UserControls;
using HMS_Software_V2.General_Purpose;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
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
    /// Interaction logic for Admin_Nurse_Search.xaml
    /// </summary>
    public partial class Admin_Nurse_Search : Window
    {
        ObservableCollection<Nurse> Nurses { get; set; } = new ObservableCollection<Nurse>();

        public Admin_Nurse_Search()
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

            showNurse_DataGrid.ItemsSource = Nurses;
        }

        private void MyShowTable()
        {
            using (SQLiteConnection connection = new Database_Connector().GetConnection())
            {
                try
                {
                    connection.Open();

                    #region Get Total Doctors Count

                    string query = "SELECT * FROM Nurse";
                    SQLiteCommand command = new SQLiteCommand(query, connection);

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Nurses.Add(new Nurse
                            {
                                NurseID = reader.GetInt32(reader.GetOrdinal("Nurce_ID")),
                                N_FullName = reader.IsDBNull(reader.GetOrdinal("N_FullName")) ? null : reader.GetString(reader.GetOrdinal("N_FullName")),
                                N_NameWithInitials = reader.IsDBNull(reader.GetOrdinal("N_NameWithInitials")) ? null : reader.GetString(reader.GetOrdinal("N_NameWithInitials")),
                                N_Age = reader.IsDBNull(reader.GetOrdinal("N_Age")) ? null : reader.GetString(reader.GetOrdinal("N_Age")),
                                N_Gender = reader.IsDBNull(reader.GetOrdinal("N_Gender")) ? null : reader.GetString(reader.GetOrdinal("N_Gender")),
                                N_NIC = reader.IsDBNull(reader.GetOrdinal("N_NIC")) ? null : reader.GetString(reader.GetOrdinal("N_NIC")),
                                N_Email = reader.IsDBNull(reader.GetOrdinal("N_Email")) ? null : reader.GetString(reader.GetOrdinal("N_Email"))
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

        class Nurse
        {
            public int NurseID { get; set; }
            public string? N_FullName { get; set; }
            public string? N_NameWithInitials { get; set; }
            public string? N_Age { get; set; }
            public string? N_Gender { get; set; }
            public string? N_NIC { get; set; }
            public string? N_Email { get; set; }  
        }

        private void SearchBar_tbx_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterRecords(SearchBar_tbx.Text);
        }

        private void FilterRecords(string searchText)
        {
            var selectedColumn = FilterColumn_ComboBox.SelectedItem as ComboBoxItem;
            var selectedContent = selectedColumn?.Content.ToString();

            var filteredDoctors = new ObservableCollection<Nurse>();

            if (string.IsNullOrWhiteSpace(searchText))
            {
                filteredDoctors = Nurses;
            }
            else
            {
                switch (selectedContent)
                {
                    case "By Name":
                        filteredDoctors = new ObservableCollection<Nurse>(
                            Nurses.Where(d => d.N_FullName != null && d.N_FullName.Contains(searchText, StringComparison.OrdinalIgnoreCase)));
                        break;
                    case "By ID":
                        filteredDoctors = new ObservableCollection<Nurse>(
                            Nurses.Where(d => d.NurseID.ToString().Contains(searchText)));
                        break;
                        // Add cases for other columns as needed
                }
            }

            showNurse_DataGrid.ItemsSource = filteredDoctors;
        }

        private void FilterColumn_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SearchBar_tbx.Text = "";
        }
    }
}
