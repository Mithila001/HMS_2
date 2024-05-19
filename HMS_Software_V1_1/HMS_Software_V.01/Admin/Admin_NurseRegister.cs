using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HMS_Software_V1._01.Admin
{
    public partial class Admin_NurseRegister : Form
    {
        SqlConnection connect = new SqlConnection(MyCommonConnecString.ConnectionString);//Call connection string from a class
        public Admin_NurseRegister()
        {
            InitializeComponent();
            this.FormClosed += (s, e) => new Admin_Dashboard().Show();


            string formattedDate = DateTime.Today.ToString("dd-MM-yyyy");
            string formattedTime = DateTime.Now.ToString("h.mm tt");
            A_DR_time.Text = formattedTime;
            A_DR_date.Text = formattedDate;
        }

        public string MyValidateTextBox(string value) // Method to validate textbox value and return the validated value
        {

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("This field is required.", nameof(value));
            }
            else
            {

                return value;
            }
        }

        private void A_N_Save_btn_Click(object sender, EventArgs e)
        {
            if (A_N_fullName_tbx.Text == ""
                || A_N_NameWithInitials_tbx.Text == ""
                || A_N_age_tbx.Text == ""
                || A_N_gender_tbx.Text == ""
                || A_N_bloodGroup_tbx.Text == ""
                || A_N_Nic_tbx.Text == ""
                || A_N_Natinality_tbx.Text == ""
                || A_N_LicenceNumber_tbx.Text == ""
                || A_N_Specialty_tbx.Text == ""
                || A_N_Email_tbx.Text == ""
                || A_N_contactNo_tbx.Text == ""
                || A_N_position_tbx.Text == ""
                || A_N_experiecedYears_tbx.Text == ""
                || A_N_nursingSchool_tbx.Text == ""
                || A_N_graduatedYear_tbx.Text == ""
                || A_N_degree_tbx.Text == ""
                || A_N_certificate_tbx.Text == ""
                || A_N_address_tbx.Text == ""
                /*|| D_Register_DTimePicker.Value != D_Register_DTimePicker.MinDate*/)  
            {
                MessageBox.Show("Please fill all  fields"
                  , "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (connect.State == ConnectionState.Closed)
                {
                    try
                    {
                        connect.Open();
                        DateTime today = DateTime.Today;

                        string quarryNurse = "INSERT INTO Nurse " +
                            "(N_FullName, N_NameWithInitials, N_Age, N_Gender, N_BloodGroup," +
                            "N_NIC, N_Nationality, N_LicenseNo, N_Specializations, N_Email, N_ContactNO," +
                            "N_Position, N_ExperiaceYears, N_NursingSchool_Name, N_Graduated_Year," +
                            "N_Degree, N_Certificates, N_Address, N_DateOfBirth, N_Registered_Date) " +

                            "VALUES (@fullName, @nameWithInitials, @age, @gender, @bloodGroup," +
                            "@nic, @nationality, @licenseNo, @specializations, @email, @contactNo," +
                            "@position, @experienceYears, @nursingSchoolName, @graduatedYear," +
                            "@degree, @certificates, @address, @dateOfBirth, @registeredDate)";

                        int generatedNurseID;
                        using (SqlCommand cmd = new SqlCommand(quarryNurse + "; SELECT SCOPE_IDENTITY();", connect))
                        {
                            cmd.Parameters.AddWithValue("@fullName", A_N_fullName_tbx.Text);
                            cmd.Parameters.AddWithValue("@nameWithInitials", A_N_NameWithInitials_tbx.Text);
                            cmd.Parameters.AddWithValue("@age", A_N_age_tbx.Text);
                            cmd.Parameters.AddWithValue("@gender", A_N_gender_tbx.Text);
                            cmd.Parameters.AddWithValue("@bloodGroup", A_N_bloodGroup_tbx.Text);
                            cmd.Parameters.AddWithValue("@nic", A_N_Nic_tbx.Text);
                            cmd.Parameters.AddWithValue("@nationality", A_N_Natinality_tbx.Text);
                            cmd.Parameters.AddWithValue("@licenseNo", A_N_LicenceNumber_tbx.Text);
                            cmd.Parameters.AddWithValue("@specializations", A_N_Specialty_tbx.Text);
                            cmd.Parameters.AddWithValue("@email", A_N_Email_tbx.Text);
                            cmd.Parameters.AddWithValue("@contactNo", A_N_contactNo_tbx.Text);
                            cmd.Parameters.AddWithValue("@position", A_N_position_tbx.Text);
                            cmd.Parameters.AddWithValue("@experienceYears", A_N_experiecedYears_tbx.Text);
                            cmd.Parameters.AddWithValue("@nursingSchoolName", A_N_nursingSchool_tbx.Text);
                            cmd.Parameters.AddWithValue("@graduatedYear", A_N_graduatedYear_tbx.Text);
                            cmd.Parameters.AddWithValue("@degree", A_N_degree_tbx.Text);
                            cmd.Parameters.AddWithValue("@certificates", A_N_certificate_tbx.Text);
                            cmd.Parameters.AddWithValue("@address", A_N_address_tbx.Text);
                            cmd.Parameters.AddWithValue("@dateOfBirth", D_Register_DTimePicker.Value);
                            cmd.Parameters.AddWithValue("@registeredDate", today);

                            generatedNurseID = Convert.ToInt32(cmd.ExecuteScalar());

                            // get Doctor ID 
                            MessageBox.Show("Nurse added successfully with ID: " + generatedNurseID, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            MyAddUserLoginDetails(generatedNurseID);

                        }

                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message
                                , "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        Console.WriteLine(ex);

                    }
                    finally
                    {
                        connect.Close();
                    }
                }
            }
        }

        private void MyAddUserLoginDetails(int generatedNurseID)
        {
            //Create User name And Password =================================================================================

            string nurse_Name = A_N_NameWithInitials_tbx.Text;
            string nurse_Age = A_N_age_tbx.Text.Trim();

            // Get the first three letters of each string
            string abbreviatedName = nurse_Name.Substring(0, Math.Min(nurse_Name.Length, 3));
            abbreviatedName = abbreviatedName.Trim();

            string userName = "D" + abbreviatedName + nurse_Age;
            string password = userName.Substring(0, Math.Min(userName.Length, 3));


            try
            {
                using (SqlConnection connect = new SqlConnection(MyCommonConnecString.ConnectionString))
                {
                    connect.Open();

                    string insertQuery = "INSERT INTO UserLogin (UserPosition, UserName, UserPassword, UserID) VALUES (@UserPosition, @UserName, @UserPassword, @UserID)";

                    // Create a new instance of SqlCommand
                    using (SqlCommand command = new SqlCommand(insertQuery, connect))
                    {
                        // Add parameters to the command if necessary
                        command.Parameters.AddWithValue("@UserPosition", "Nurse");
                        command.Parameters.AddWithValue("@UserName", userName);
                        command.Parameters.AddWithValue("@UserPassword", password);
                        command.Parameters.AddWithValue("@UserID", generatedNurseID);

                        // Execute the command
                        int rowsAffected = command.ExecuteNonQuery();

                        // Check if any rows were affected by the INSERT operation
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("INSERT INTO UserLogin Data inserted successfully.");
                        }
                        else
                        {
                            Console.WriteLine("INSERT INTO UserLogin No data inserted.");
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error2: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

                Console.WriteLine(ex);

            }
        }
    }
}
