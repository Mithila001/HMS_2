using HMS_Software_V2._DataManage_Classes;
using HMS_Software_V2.General_Purpose;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Net;
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


namespace HMS_Software_V2.Reception
{
    /// <summary>
    /// Interaction logic for Reception_RegisterPatient.xaml
    /// </summary>
    /// 

    // Need to add NIC validation

    public partial class Reception_RegisterPatient : Window
    {
        public Reception_RegisterPatient()
        {
            InitializeComponent();
            MyLoadBasicData();
        }

        private void MyLoadBasicData()
        {
            #region Get and Assign - Date Time
            int day = DateTime.Now.Day;
            string daySuffix = day switch
            {
                1 or 21 or 31 => "st",
                2 or 22 => "nd",
                3 or 23 => "rd",
                _ => "th"
            };

            todayDate.Content = $"{day}{daySuffix} {DateTime.Now:MMMM yyyy}";

            todayTime.Content = DateTime.Now.ToString("hh:mm: tt");
            #endregion


            receptionName.Content = SharedData.receptionData.ReceptionName;
        }

        string? PatientRID;
        bool isGenerated = false;

        private void Generate_btn_Click(object sender, RoutedEventArgs e)
        {
            if(isGenerated)
            {
                return;
            }


            string p_FullName = patient_FullName.Text;
            string p_NameWithInitials = patient_NameWithInitials.Text;
            DateTime? dateOfBirth = patient_DateOfBirth.SelectedDate;
            string p_Age = patient_Age.Text;
            string p_NIC = patient_NIC.Text;
            string p_ContacntNo = patient_ContactNo.Text;
            string p_Address = patient_Address.Text;
            string g_Name = guardian_NameWithInitials.Text;
            string g_ContactNo = guardian_ContactNo.Text;
            string g_NIC = guardian_NIC.Text;
            string p_Gender = patient_Gender.Text;

            if (string.IsNullOrEmpty(p_FullName) ||
                string.IsNullOrEmpty(p_NameWithInitials) ||
                !dateOfBirth.HasValue ||
                string.IsNullOrEmpty(p_Age) ||
                string.IsNullOrEmpty(p_NIC) ||
                string.IsNullOrEmpty(p_ContacntNo) ||
                string.IsNullOrEmpty(p_Address) ||
                string.IsNullOrEmpty(g_Name) ||
                string.IsNullOrEmpty(g_ContactNo) ||
                string.IsNullOrEmpty(p_Gender) ||
                string.IsNullOrEmpty(g_NIC))
            {
                MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                using (SQLiteConnection connection = new Database_Connector().GetConnection())
                {
                    string query = "INSERT INTO Patient (P_FullName, P_NameWithIinitials, P_Age, P_Gender, P_NIC, P_ContactNo," +
                                    " P_Address, P_DateOfBirth, P_G_Name, P_G_ContactNo, P_G_NIC, P_RegisteredTime, P_RegisteredDate, P_CurrentStatus)" +
                                    " OUTPUT inserted.P_RegistrationID " +
                                    " VALUES (@P_FullName, @P_NameWithIinitials, @P_Age, @P_Gender, @P_NIC, @P_ContactNo," +
                                    " @P_Address, @P_DateOfBirth, @P_G_Name, @P_G_ContactNo, @P_G_NIC, @P_RegisteredTime, @P_RegisteredDate, @P_CurrentStatus)";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@P_FullName", p_FullName);
                        cmd.Parameters.AddWithValue("@P_NameWithIinitials", p_NameWithInitials);
                        cmd.Parameters.AddWithValue("@P_DateOfBirth", dateOfBirth.Value);
                        cmd.Parameters.AddWithValue("@P_Age", p_Age);
                        cmd.Parameters.AddWithValue("@P_Gender", p_Gender);
                        cmd.Parameters.AddWithValue("@P_NIC", p_NIC);
                        cmd.Parameters.AddWithValue("@P_ContactNo", p_ContacntNo);
                        cmd.Parameters.AddWithValue("@P_Address", p_Address);
                        cmd.Parameters.AddWithValue("@P_G_Name", g_Name);
                        cmd.Parameters.AddWithValue("@P_G_ContactNo", g_ContactNo);
                        cmd.Parameters.AddWithValue("@P_G_NIC", g_NIC);
                        cmd.Parameters.AddWithValue("@P_RegisteredTime", DateTime.Now.ToString("HH:mm:ss"));
                        cmd.Parameters.AddWithValue("@P_RegisteredDate", DateTime.Now.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@P_CurrentStatus", "New Registered");

                        connection.Open();
                        string registrationId = (string)cmd.ExecuteScalar();
                        PatientRID = registrationId;

                        if (!string.IsNullOrEmpty(registrationId))
                        {
                            isGenerated = true; // To prvent another Generate [For extra safty]
                            Generate_btn.Background = Brushes.Gray;
                            Generate_btn.IsEnabled = false;

                            var bc = new BrushConverter();

                            patientRID_lbl.Content = registrationId;

                            save_btn.IsEnabled = true;
                            save_btn.ClearValue(Button.BackgroundProperty);


                            print_btn.IsEnabled = true ;
                            print_btn.ClearValue(Button.BackgroundProperty);


                        }
                        else
                        {
                            MessageBox.Show("Error saving patient information.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void save_btn_Click(object sender, RoutedEventArgs e)
        {
            
            this.Close();
        }

        private void Reception_RegisterPatient1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Reception_Dashboard reception_Dashboard = new Reception_Dashboard();
            reception_Dashboard.Show();
           
        }

        private void Reception_RegisterPatient1_Closed(object sender, EventArgs e)
        {
         // Nothing for now  
        }

        private void print_btn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
