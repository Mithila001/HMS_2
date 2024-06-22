using HMS_Software_V2._DataManage_Classes;
using HMS_Software_V2.Admin.Admin_UserControls;
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
    /// Interaction logic for Admin_Nurse_Register.xaml
    /// </summary>
    public partial class Admin_Nurse_Register : Window
    {
        public Admin_Nurse_Register()
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
        string? NursingSchoolName;
        string? GraduatedYear;
        string? Degree;
        string? Certificates;
        string? Position;
        string? Experience;
        string? Email;
        string? Specialty;

        #endregion


        private void MyGetUserInputData()
        {
            FullName = nurse_FullName.Text;
            NameWithInitials = nurse_NameWithInitials.Text;
            DateOfBirth = nurse_DateOfBirth.SelectedDate;
            Age = nurse_Age.Text;
            Gender = nurse_Gender.SelectedItem is ComboBoxItem selectedItem ? selectedItem.Content.ToString() : string.Empty;
            Nic = nurse_NIC.Text;
            ContactNo = nurse_ContactNo.Text;
            Address = nurse_Address.Text;
            Nationality = nurse_Nationality.Text;
            BloodGroup = nurse_BloodGroup.Text;
            LicenseNumber = nurse_LincencNumber.Text;
            NursingSchoolName = nurse_MedicalSchoolName.Text;
            GraduatedYear = nurse_GraduatedYear.Text;
            Degree = nurse_Degree.Text;
            Certificates = nurse_Certificates.Text;
            Position = nurse_Position.Text;
            Experience = nurse_Experience.Text;
            Email = nurse_Email.Text;
            Specialty = nurse_Specialty.Text;

            #region Input Validations

            if (nurse_Gender.SelectedItem == null)
            {
                MessageBox.Show("Please select a gender.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (nurse_DateOfBirth.SelectedDate == null)
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

            if (InputValidations.MyIsNullorempty(NursingSchoolName))
            {
                Console.WriteLine("Nursing School Name is required.");
                MessageBox.Show("Nursing School Name is required", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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


        int NurseId;
        private void MyInsertDataToTheTable()
        {
            using (SqlConnection connection = new Database_Connector().GetConnection())
            {
                connection.Open();

                try
                {
                    string query = @"
                        INSERT INTO Nurse (
                            N_FullName, N_NameWithInitials, N_DateOfBirth, N_Age, N_Gender, N_NIC, N_ContactNO, N_Address, N_Nationality,
                            N_BloodGroup, N_LicenseNo, N_NursingSchool_Name, N_Graduated_Year, N_Degree, N_Certificates, N_Position, N_ExperiaceYears,
                            N_Specializations, N_Email, N_Registered_Date)
                            VALUES 
                            (@N_FullName, @N_NameWithInitials, @N_DateOfBirth, @N_Age,@N_Gender, @N_NIC, @N_ContactNO ,@N_Address, @N_Nationality,
                            @N_BloodGroup, @N_LicenseNo, @N_NursingSchool_Name, @N_Graduated_Year, @N_Degree,@N_Certificates, @N_Position, @N_ExperiaceYears,
                            @N_Specializations, @N_Email, @N_Registered_Date); 
                        SELECT SCOPE_IDENTITY();";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        // Assuming you have variables for each of these parameters
                        cmd.Parameters.AddWithValue("@N_FullName", FullName);
                        cmd.Parameters.AddWithValue("@N_NameWithInitials", NameWithInitials);
                        cmd.Parameters.AddWithValue("@N_DateOfBirth", DateOfBirth);
                        cmd.Parameters.AddWithValue("@N_Age", Age);
                        cmd.Parameters.AddWithValue("@N_Gender", Gender);
                        cmd.Parameters.AddWithValue("@N_NIC", Nic);
                        cmd.Parameters.AddWithValue("@N_ContactNO", ContactNo);
                        cmd.Parameters.AddWithValue("@N_Address", Address);
                        cmd.Parameters.AddWithValue("@N_Nationality", Nationality);
                        cmd.Parameters.AddWithValue("@N_BloodGroup", BloodGroup);
                        cmd.Parameters.AddWithValue("@N_LicenseNo", LicenseNumber);
                        cmd.Parameters.AddWithValue("@N_NursingSchool_Name", NursingSchoolName);
                        cmd.Parameters.AddWithValue("@N_Graduated_Year", GraduatedYear);
                        cmd.Parameters.AddWithValue("@N_Degree", Degree);
                        cmd.Parameters.AddWithValue("@N_Certificates", Certificates);
                        cmd.Parameters.AddWithValue("@N_Position", Position);
                        cmd.Parameters.AddWithValue("@N_ExperiaceYears", Experience);
                        cmd.Parameters.AddWithValue("@N_Specializations", Specialty);
                        cmd.Parameters.AddWithValue("@N_Email", Email);
                        cmd.Parameters.AddWithValue("@N_Registered_Date", DateTime.Today.ToString("yyyy-MM-dd"));

                        // Execute the command
                        NurseId = Convert.ToInt32(cmd.ExecuteScalar());
                        Debug.WriteLine("Inserted ID: " + NurseId);

                        MyCreateUserLoginDetails(NurseId);
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

            string userPassword = $"{nameWithoutSpaces}{randomNumber}{NurseId}D";

            string userName = $"N_{nameWithoutSpaces}";

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
                        cmd.Parameters.AddWithValue("@UserID", NurseId);

                        cmd.Parameters.AddWithValue("@UserPosition", "Nurse");
                        
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
