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

namespace HMS_Software_V_2.Admin
{
    public partial class Admin_NurseRegister : Form
    {
        SqlConnection connect = new SqlConnection(MyCommonConnecString.ConnectionString);//Call connection string from a class
        public Admin_NurseRegister()
        {
            InitializeComponent();
            this.FormClosed += (s, e) => new Admin_Dashboard().Show();
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

                        using (SqlCommand cmd = new SqlCommand(quarryNurse, connect))
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

                            cmd.ExecuteNonQuery();

                            MessageBox.Show("Added successfully!"
                                       , "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
    }
}
