using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

using System.Data.SqlClient;
using System.IO;

namespace HMS_Software_V1._01.Admin
{
    public partial class Admin_DoctorRegister : Form
    {
        SqlConnection connect = new SqlConnection(MyCommonConnecString.ConnectionString);//Call connection string from a class

        string AdminName;
        public Admin_DoctorRegister(string AdminName)
        {
            InitializeComponent();
            this.AdminName = AdminName;
            this.FormClosed += (s, e) => new Admin_Dashboard().Show();


            A_DR_adminName_lbl.Text = AdminName;


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

        private void A_D_register_btn_Click(object sender, EventArgs e)
        {
            if(A_D_fullName_tbx.Text == ""
                || A_D_NameWithInitials_tbx.Text == ""
                || A_D_age_tbx.Text == ""
                || A_D_gender_tbx.Text == ""
                || A_D_bloodGroup_tbx.Text == ""
                || A_D_Nic_tbx.Text == ""
                || A_D_Natinality_tbx.Text == ""
                || A_D_LicenceNumber_tbx.Text == ""
                || A_D_Specialty_tbx.Text == ""
                || A_D_Email_tbx.Text == ""
                || A_D_nextOfKin_tbx.Text == ""
                || A_D_contactNo_tbx.Text == ""
                || A_D_position_tbx.Text == ""
                || A_D_experiecedYears_tbx.Text == ""
                || A_D_medicalSchool_tbx.Text == ""
                || A_D_graduatedYear_tbx.Text == ""
                || A_D_degree_tbx.Text == ""
                || A_D_certificate_tbx.Text == ""
                || A_D_address_tbx.Text == "" )
            {
                MessageBox.Show("Please fill all  fields"
                    , "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if(connect.State == ConnectionState.Closed)
                {
                    try
                    {
                        connect.Open();
                        DateTime today = DateTime.Today;
                        string quarryDoctor = "INSERT INTO Doctor " +
                            "(D_FullName, D_NameWithInitials, D_Age, D_Gender, D_BloodGroup," +
                            "D_NIC, D_Nationality, D_LicenceNumber, D_Specialty, D_Email, D_NextOfKin," +
                            "D_ContactNo, D_Position, D_Experience, D_MedicalSchool_Name, D_Graduated_Year," +
                            "D_Degree, D_Certificate, D_Address, D_RegisteredDate, D_IsAdmissionOfficer) " +

                            "VALUES (@fullName, @nameWithInitials, @age, @gender, @bloodGroup," +
                            "@nic, @nationality, @licenceNumber, @specialty, @email, @nextOfKin," +
                            "@contactNo, @position, @experience, @medicalSchoolName, @graduatedYear," +
                            "@degree, @certificate, @address, @registeredDate, @D_IsAdmissionOfficer)";

                        /*string imageFilePath = Path.Combine(@"E:\Programming\Github\HMS_Software_V1.01\HMS_Software_V1.01\Assests\Doctor_Images\"
                                    + A_D_NameWithInitials_tbx.Text.Trim() + ".jpg");

                        File.Copy(A_D_pictureBox.ImageLocation, imageFilePath, true);*/

                        int generatedDoctorID;
                        using (SqlCommand cmd = new SqlCommand(quarryDoctor + "; SELECT SCOPE_IDENTITY();", connect))
                        {
                            cmd.Parameters.AddWithValue("@fullName", A_D_fullName_tbx.Text);
                            cmd.Parameters.AddWithValue("@nameWithInitials", A_D_NameWithInitials_tbx.Text);
                            cmd.Parameters.AddWithValue("@age", A_D_age_tbx.Text);
                            cmd.Parameters.AddWithValue("@gender", A_D_gender_tbx.Text);
                            cmd.Parameters.AddWithValue("@bloodGroup", A_D_bloodGroup_tbx.Text);
                            cmd.Parameters.AddWithValue("@nic", A_D_Nic_tbx.Text);
                            cmd.Parameters.AddWithValue("@nationality", A_D_Natinality_tbx.Text);
                            cmd.Parameters.AddWithValue("@licenceNumber", A_D_LicenceNumber_tbx.Text);
                            cmd.Parameters.AddWithValue("@specialty", A_D_Specialty_tbx.Text);
                            cmd.Parameters.AddWithValue("@email", A_D_Email_tbx.Text);
                            cmd.Parameters.AddWithValue("@nextOfKin", A_D_nextOfKin_tbx.Text);
                            cmd.Parameters.AddWithValue("@contactNo", A_D_contactNo_tbx.Text);
                            cmd.Parameters.AddWithValue("@position", A_D_position_tbx.Text);
                            cmd.Parameters.AddWithValue("@experience", A_D_experiecedYears_tbx.Text);
                            cmd.Parameters.AddWithValue("@medicalSchoolName", A_D_medicalSchool_tbx.Text);
                            cmd.Parameters.AddWithValue("@graduatedYear", A_D_graduatedYear_tbx.Text);
                            cmd.Parameters.AddWithValue("@degree", A_D_degree_tbx.Text);
                            cmd.Parameters.AddWithValue("@certificate", A_D_certificate_tbx.Text);
                            cmd.Parameters.AddWithValue("@address", A_D_address_tbx.Text);
                            /*cmd.Parameters.AddWithValue("@imageFilePath", imageFilePath);*/
                            cmd.Parameters.AddWithValue("@registeredDate", today);
                            cmd.Parameters.AddWithValue("@D_IsAdmissionOfficer", isAdmissionOfficer_checkBox.Checked);


                            generatedDoctorID = Convert.ToInt32(cmd.ExecuteScalar());

                            // get Doctor ID 
                            MessageBox.Show("Doctor added successfully with ID: " + generatedDoctorID, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            

                            MessageBox.Show("Added successfully!"
                                       , "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            MyAddUserLoginDetails(generatedDoctorID);

                        }


                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show("Error1: " + ex.Message
                   ,                "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        Console.WriteLine(ex);
                    }
                    finally
                    {
                        connect.Close();
                    }
                }
            }




        }

        private void MyAddUserLoginDetails(int generatedDoctorID)
        {
            //Create User name And Password =================================================================================

            string doctor_Name = A_D_NameWithInitials_tbx.Text;
            string doctro_Age = A_D_age_tbx.Text.Trim();

            // Get the first three letters of each string
            string abbreviatedName = doctor_Name.Substring(0, Math.Min(doctor_Name.Length, 3));
            abbreviatedName = abbreviatedName.Trim();

            string userName = "D" + abbreviatedName + doctro_Age;
            string password = userName.Substring(0, Math.Min(userName.Length, 3));

            string userPosition;
            //Ckeck if an Admission Officer
            if (isAdmissionOfficer_checkBox.Checked)
            {
                userPosition = "Admission Officer";
            }
            else
            {
                userPosition = "Doctor";
            }


            try
            {
                using(SqlConnection connect = new SqlConnection(MyCommonConnecString.ConnectionString))
                {
                    connect.Open();

                    string insertQuery = "INSERT INTO UserLogin (UserPosition, UserName, UserPassword, UserID) VALUES (@UserPosition, @UserName, @UserPassword, @UserID)";

                    // Create a new instance of SqlCommand
                    using (SqlCommand command = new SqlCommand(insertQuery, connect))
                    {
                        // Add parameters to the command if necessary
                        command.Parameters.AddWithValue("@UserPosition", userPosition);
                        command.Parameters.AddWithValue("@UserName", userName);
                        command.Parameters.AddWithValue("@UserPassword", password);
                        command.Parameters.AddWithValue("@UserID", generatedDoctorID);

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
