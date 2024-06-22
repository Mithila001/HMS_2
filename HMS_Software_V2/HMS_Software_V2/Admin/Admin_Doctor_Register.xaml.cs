using HMS_Software_V2._DataManage_Classes;
using HMS_Software_V2.General_Purpose;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HMS_Software_V2.Admin
{
    /// <summary>
    /// Interaction logic for Admin_Doctor_Register.xaml
    /// </summary>
    public partial class Admin_Doctor_Register : Window
    {
        public Admin_Doctor_Register()
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
        DateTime? DateOfBirth;
        string? Age;
        string? Gender;
        string? Nic;
        string? ContactNo;
        string? Address;
        string? Nationality;
        string? BloodGroup;
        string? LicenseNumber;
        string? MedicalSchoolName;
        string? GraduatedYear;
        string? Degree;
        string? Certificates;
        string? Position;
        string? Experience;
        string? Specialty;
        string? Email;
        string? NextOfKin; 
        #endregion

        private void MyGetUserInputData()
        {
            FullName = doctor_FullName.Text;
            NameWithInitials = doctor_NameWithInitials.Text;
            DateOfBirth = doctor_DateOfBirth.SelectedDate;
            Age = doctor_Age.Text;
            Gender = doctor_Gender.SelectedItem is ComboBoxItem selectedItem ? selectedItem.Content.ToString() : string.Empty;
            Nic = doctor_NIC.Text;
            ContactNo = doctor_ContactNo.Text;
            Address = doctor_Address.Text;
            Nationality = doctor_Nationality.Text;
            BloodGroup = doctor_BloodGroup.Text;
            LicenseNumber = doctor_LincencNumber.Text;
            MedicalSchoolName = doctor_MedicalSchoolName.Text;
            GraduatedYear = doctor_GraduatedYear.Text;
            Degree = doctor_Degree.Text;
            Certificates = doctor_Certificates.Text;
            Position = doctor_Position.Text;
            Experience = doctor_Experience.Text;
            Specialty = doctor_Specialty.Text;
            Email = doctor_Email.Text;
            NextOfKin = doctor_NextOfKin.Text;

            #region Input Validations

            if (doctor_Gender.SelectedItem == null)
            {
                MessageBox.Show("Please select a gender.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (doctor_DateOfBirth.SelectedDate == null)
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

            if (InputValidations.MyIsNullorempty(LicenseNumber))
            {
                Console.WriteLine("License Number is required.");
                MessageBox.Show("License Number is required", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (InputValidations.MyIsNullorempty(MedicalSchoolName))
            {
                Console.WriteLine("Medical School Name is required.");
                MessageBox.Show("Medical School Name is required", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (InputValidations.MyIsNullorempty(GraduatedYear) || !InputValidations.MyIsOnlyNumbers(GraduatedYear))
            {
                Console.WriteLine("Graduated Year is required and must be a number.");
                MessageBox.Show("Graduated Year is required and must be a number", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (InputValidations.MyIsNullorempty(Degree))
            {
                Console.WriteLine("Degree is required.");
                MessageBox.Show("Degree is required", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (InputValidations.MyIsNullorempty(Certificates))
            {
                Console.WriteLine("Certificates is required.");
                MessageBox.Show("Certificates is required", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (InputValidations.MyIsNullorempty(Position))
            {
                Console.WriteLine("Position is required.");
                MessageBox.Show("Position is required", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (InputValidations.MyIsNullorempty(Experience))
            {
                Console.WriteLine("Experience is required.");
                MessageBox.Show("Experience is required", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (InputValidations.MyIsNullorempty(Specialty))
            {
                Console.WriteLine("Specialty is required.");
                MessageBox.Show("Specialty is required", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (InputValidations.MyIsNullorempty(Email))
            {
                Console.WriteLine("Email is required.");
                MessageBox.Show("Email is required", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (InputValidations.MyIsNullorempty(NextOfKin))
            {
                Console.WriteLine("Next of Kin is required.");
                MessageBox.Show("Next of Kin is required", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            #endregion

            MyInsertDataToTheTable();
        }


        int DoctorID;
        private void MyInsertDataToTheTable()
        {
            using (SqlConnection connection = new Database_Connector().GetConnection())
            {
                connection.Open();

                try
                {
                    string query = @"
                    INSERT INTO Doctor 
                    (D_FullName, D_NameWithInitials, D_DateOfBirth, D_Age, D_Gender, D_NIC, D_ContactNo, D_Address, D_Nationality,
                    D_BloodGroup, D_LicenceNumber, D_MedicalSchool_Name, D_Graduated_Year, D_Degree, D_Certificate, D_Position, D_Experience,
                    D_Specialty, D_Email, D_NextOfKin, D_RegisteredDate, D_IsAdmissionOfficer) 
                    VALUES 
                    (@D_FullName, @D_NameWithInitials, @D_DateOfBirth, @D_Age, @D_Gender, @D_NIC, @D_ContactNo, @D_Address, @D_Nationality,
                    @D_BloodGroup, @D_LicenceNumber, @D_MedicalSchool_Name, @D_Graduated_Year, @D_Degree, @D_Certificate, @D_Position, @D_Experience,
                    @D_Specialty, @D_Email, @D_NextOfKin, @D_RegisteredDate, @D_IsAdmissionOfficer); 
                    SELECT SCOPE_IDENTITY();";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        // Assuming you have variables for each of these parameters
                        cmd.Parameters.AddWithValue("@D_FullName", FullName);
                        cmd.Parameters.AddWithValue("@D_NameWithInitials", NameWithInitials);
                        cmd.Parameters.AddWithValue("@D_DateOfBirth", DateOfBirth);
                        cmd.Parameters.AddWithValue("@D_Age", Age);
                        cmd.Parameters.AddWithValue("@D_Gender", Gender);
                        cmd.Parameters.AddWithValue("@D_NIC", Nic);
                        cmd.Parameters.AddWithValue("@D_ContactNo", ContactNo);
                        cmd.Parameters.AddWithValue("@D_Address", Address);
                        cmd.Parameters.AddWithValue("@D_Nationality", Nationality);
                        cmd.Parameters.AddWithValue("@D_BloodGroup", BloodGroup);
                        cmd.Parameters.AddWithValue("@D_LicenceNumber", LicenseNumber);
                        cmd.Parameters.AddWithValue("@D_MedicalSchool_Name", MedicalSchoolName);
                        cmd.Parameters.AddWithValue("@D_Graduated_Year", GraduatedYear);
                        cmd.Parameters.AddWithValue("@D_Degree", Degree);
                        cmd.Parameters.AddWithValue("@D_Certificate", Certificates);
                        cmd.Parameters.AddWithValue("@D_Position", Position);
                        cmd.Parameters.AddWithValue("@D_Experience", Experience);
                        cmd.Parameters.AddWithValue("@D_Specialty", Specialty);
                        cmd.Parameters.AddWithValue("@D_Email", Email);
                        cmd.Parameters.AddWithValue("@D_NextOfKin", NextOfKin);
                        cmd.Parameters.AddWithValue("@D_RegisteredDate", DateTime.Today.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@D_IsAdmissionOfficer", IsAdmissionOfficer_ComboBox.IsChecked);

                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            DoctorID = Convert.ToInt32(result);
                            Debug.WriteLine("\nDoctor ID: " + DoctorID);

                            MyCreateUserLoginDetails(DoctorID); // Create user login details
                        }
                        else
                        {
                            MessageBox.Show("Error: Doctor ID is not generated", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
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

            string userPassword = $"{nameWithoutSpaces}{randomNumber}{DoctorID}D";

            string userName = $"D_{nameWithoutSpaces}";

            MyAddUserLoginToTable(userName, userPassword);

        }

        void MyAddUserLoginToTable(string userName, string password)
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
                        cmd.Parameters.AddWithValue("@UserID", DoctorID);

                        if(IsAdmissionOfficer_ComboBox.IsChecked == true)
                        {
                            cmd.Parameters.AddWithValue("@UserPosition", "Doctor/AO");
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@UserPosition", "Doctor");
                        }

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
