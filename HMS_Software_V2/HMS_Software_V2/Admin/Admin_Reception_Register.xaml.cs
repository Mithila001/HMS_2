using HMS_Software_V2._DataManage_Classes;
using HMS_Software_V2.General_Purpose;
using System;
using System.Collections.Generic;
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

namespace HMS_Software_V2.Admin
{
    /// <summary>
    /// Interaction logic for Admin_Reception_Register.xaml
    /// </summary>
    public partial class Admin_Reception_Register : Window
    {
        public Admin_Reception_Register()
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
        }

        private void Register_btn_Click(object sender, RoutedEventArgs e)
        {
            MyGetUserInputData();
        }

        #region Variables

        string? FullName;
        string? NameWithInitials;
        string? Age;
        string? Gender;
        string? BloodGroup;
        string? Nic;
        string? Nationality;
        string? Specialty;
        string? Email;
        string? ContactNo;
        string? Experience;
        string? Certificates;
        string? Address;
        DateTime? DateOfBirth;

        #endregion

        private void MyGetUserInputData()
        {
            FullName = reception_FullName.Text;
            NameWithInitials = reception_NameWithInitials.Text;
            DateOfBirth = reception_DateOfBirth.SelectedDate;
            Age = reception_Age.Text;
            Gender = reception_Gender.SelectedItem is ComboBoxItem selectedItem ? selectedItem.Content.ToString() : string.Empty;
            Nic = reception_NIC.Text;
            ContactNo = reception_ContactNo.Text;
            Address = reception_Address.Text;
            Nationality = reception_Nationality.Text;
            BloodGroup = reception_BloodGroup.Text;
            Certificates = reception_Certificates.Text;
            Experience = reception_Experience.Text;
            Email = reception_Email.Text;
            Specialty = reception_Specialty.Text;



            #region Input Validations

            if (reception_Gender.SelectedItem == null)
            {
                MessageBox.Show("Please select a gender.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (reception_DateOfBirth.SelectedDate == null)
            {
                MessageBox.Show("Please select a date of birth.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            if (InputValidations.MyIsNullorempty(FullName))
            {
                Console.WriteLine("Full Name is required.");
                MessageBox.Show("Full Name is required", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (InputValidations.MyIsNullorempty(NameWithInitials))
            {
                Console.WriteLine("Name with initials is required.");
                MessageBox.Show("Name with initials is required", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (InputValidations.MyIsNullorempty(Age) || !InputValidations.MyIsOnlyNumbers(Age))
            {
                Console.WriteLine("Age is required and must be a number.");
                MessageBox.Show("Age is required and must be a number", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (InputValidations.MyIsNullorempty(ContactNo) || !InputValidations.MyIsOnlyNumbers(ContactNo))
            {
                Console.WriteLine("Contact Number is required and must be a number.");
                MessageBox.Show("Contact Number is required and must be a number", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (InputValidations.MyIsNullorempty(Address))
            {
                Console.WriteLine("Address is required.");
                MessageBox.Show("Address is required", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (InputValidations.MyIsNullorempty(Nic))
            {
                Console.WriteLine("NIC is required.");
                MessageBox.Show("NIC is required", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (InputValidations.MyIsNullorempty(ContactNo))
            {
                Console.WriteLine("Contact Number is required.");
                MessageBox.Show("Contact Number is required", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (InputValidations.MyIsNullorempty(Address))
            {
                Console.WriteLine("Address is required.");
                MessageBox.Show("Address is required", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (InputValidations.MyIsNullorempty(Nationality))
            {
                Console.WriteLine("Nationality is required.");
                MessageBox.Show("Nationality is required", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (InputValidations.MyIsNullorempty(BloodGroup))
            {
                Console.WriteLine("BloodGroup is required.");
                MessageBox.Show("BloodGroup is required", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            if (InputValidations.MyIsNullorempty(Certificates))
            {
                Console.WriteLine("Certificates is required.");
                MessageBox.Show("Certificates is required", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (InputValidations.MyIsNullorempty(Experience))
            {
                Console.WriteLine("Experience is required.");
                MessageBox.Show("Experience is required", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (InputValidations.MyIsNullorempty(Email))
            {
                Console.WriteLine("Email is required.");
                MessageBox.Show("Email is required", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (InputValidations.MyIsNullorempty(Specialty))
            {
                Console.WriteLine("Specialty is required.");
                MessageBox.Show("Specialty is required", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            #endregion

            MyInsertDataToTheTable();


        }


        int ReceptionID;
        private void MyInsertDataToTheTable()
        {
            using (SqlConnection connection = new Database_Connector().GetConnection())
            {
                connection.Open();

                try
                {
                    string query = @"
                                    INSERT INTO [dbo].[Reception] (
                                        [R_FullName], 
                                        [R_NameWithInitials], 
                                        [R_Age], 
                                        [R_Gender], 
                                        [R_BloodGroup], 
                                        [R_NIC], 
                                        [R_Nationality], 
                                        [R_Specialty], 
                                        [R_Email], 
                                        [R_ContactNo], 
                                        [R_ExperiencedYears], 
                                        [R_Certificates], 
                                        [R_Address], 
                                        [R_DateOfBirth], 
                                        [R_RegisteredTime], 
                                        [R_RegisteredDate]
                                    ) 
                                    VALUES (
                                        @R_FullName, 
                                        @R_NameWithInitials, 
                                        @R_Age, 
                                        @R_Gender, 
                                        @R_BloodGroup, 
                                        @R_NIC, 
                                        @R_Nationality, 
                                        @R_Specialty, 
                                        @R_Email, 
                                        @R_ContactNo, 
                                        @R_ExperiencedYears, 
                                        @R_Certificates, 
                                        @R_Address, 
                                        @R_DateOfBirth, 
                                        @R_RegisteredTime, 
                                        @R_RegisteredDate
                                    ); 
                                    SELECT SCOPE_IDENTITY();";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        // Replace these placeholders with actual values from your application
                        cmd.Parameters.AddWithValue("@R_FullName", FullName);
                        cmd.Parameters.AddWithValue("@R_NameWithInitials", NameWithInitials);
                        cmd.Parameters.AddWithValue("@R_Age", Age);
                        cmd.Parameters.AddWithValue("@R_Gender", Gender);
                        cmd.Parameters.AddWithValue("@R_BloodGroup", BloodGroup);
                        cmd.Parameters.AddWithValue("@R_NIC", Nic);
                        cmd.Parameters.AddWithValue("@R_Nationality", Nationality);
                        cmd.Parameters.AddWithValue("@R_Specialty", Specialty);
                        cmd.Parameters.AddWithValue("@R_Email", Email);
                        cmd.Parameters.AddWithValue("@R_ContactNo", ContactNo);
                        cmd.Parameters.AddWithValue("@R_ExperiencedYears",Experience);
                        cmd.Parameters.AddWithValue("@R_Certificates", Certificates);
                        cmd.Parameters.AddWithValue("@R_Address", Address);
                        cmd.Parameters.AddWithValue("@R_DateOfBirth", DateOfBirth); 
                        cmd.Parameters.AddWithValue("@R_RegisteredTime", DateTime.Now.ToString("HH:mm:ss"));
                        cmd.Parameters.AddWithValue("@R_RegisteredDate", DateTime.Today.ToString("yyyy-MM-dd"));

                        ReceptionID = Convert.ToInt32(cmd.ExecuteScalar());
                        Debug.WriteLine("Inserted ID: " + ReceptionID);

                        MyCreateUserLoginDetails(ReceptionID);
                    }

                }
                catch (Exception ex)
                {
                    Debug.WriteLine("\nError: \n" + ex.Message);
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private void MyCreateUserLoginDetails(int ID)
        {
            // Remove all whitespaces 
            string nameWithoutSpaces = NameWithInitials?.Replace(" ", string.Empty) ?? "Error";

            // Generate a random 2-digit number
            Random random = new Random();
            int randomNumber = random.Next(10, 100);

            string userPassword = $"{nameWithoutSpaces}{randomNumber}{ReceptionID}D";

            string userName = $"R_{nameWithoutSpaces}";

            MyAddUserLoginToTable(userName, userPassword);

        }

        private void MyAddUserLoginToTable(string userName, string password)
        {
            using (SqlConnection connection = new Database_Connector().GetConnection())
            {
                connection.Open();

                try
                {
                    string query = @"INSERT INTO UserLogin (UserID, UserPosition, UserName, UserPassword)
                                    VALUES
                                    (@UserID, @UserPosition, @UserName, @UserPassword);";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        // Assuming you have variables for each of these parameters
                        cmd.Parameters.AddWithValue("@UserID", ReceptionID);

                        cmd.Parameters.AddWithValue("@UserPosition", "Reception");

                        cmd.Parameters.AddWithValue("@UserName", userName);
                        cmd.Parameters.AddWithValue("@UserPassword", password);

                        cmd.ExecuteNonQuery();

                        outputUserName_lbl.Content = userName;
                        outputUserPassword_lbl.Content = password;

                        Register_btn.IsEnabled = false;


                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("\nError: \n" + ex.Message);
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                finally
                {
                    connection.Close();
                }
            }

        }
    }
}
