using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HMS_Software_V1._01.Admin
{
    public partial class Admin_ReceptionRegistration : Form
    {
        SqlConnection connect = new SqlConnection(MyCommonConnecString.ConnectionString);

        public Admin_ReceptionRegistration()
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

        private void A_R_save_btn_Click(object sender, EventArgs e)
        {
            if (A_R_fullName_tbx.Text == ""
                || A_R_NameWithInitials_tbx.Text == ""
                || A_R_age_tbx.Text == ""
                || A_R_gender_tbx.Text == ""
                || A_R_bloodGroup_tbx.Text == ""
                || A_R_Nic_tbx.Text == ""
                || A_R_Natinality_tbx.Text == ""

                || A_R_Specialty_tbx.Text == ""
                || A_R_Email_tbx.Text == ""
                || A_R_contactNo_tbx.Text == ""

                || A_R_experiecedYears_tbx.Text == ""


                || A_R_certificate_tbx.Text == ""
                || A_R_address_tbx.Text == ""
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
                        DateTime now = DateTime.Now;

                        string quarryNurse = "INSERT INTO Reception " +
                            "(R_FullName, R_NameWithIinitials, R_Age, R_Gender, R_BloodGroup," +
                            "R_NIC, R_Nationality, R_Specialty, R_Email, R_ContactNo, " +
                            "R_ExperiencedYears," +
                            " R_Certificates, R_Address, R_DateOfBirth, R_RegisteredTime, R_RegisteredDate) " +

                            "VALUES (@R_FullName, @R_NameWithIinitials, @R_Age, @R_Gender, @R_BloodGroup," +
                            "@R_NIC, @R_Nationality, @R_Specialty, @R_Email, @R_ContactNo," +
                            "@R_ExperiencedYears," +
                            "@R_Certificates, @R_Address, @R_DateOfBirth, @R_RegisteredTime, @R_RegisteredDate)";

                        int generatedReceptionID;
                        using (SqlCommand cmd = new SqlCommand(quarryNurse + "; SELECT SCOPE_IDENTITY();", connect))
                        {
                            cmd.Parameters.AddWithValue("@R_FullName", A_R_fullName_tbx.Text);
                            cmd.Parameters.AddWithValue("@R_NameWithIinitials", A_R_NameWithInitials_tbx.Text);
                            cmd.Parameters.AddWithValue("@R_Age", A_R_age_tbx.Text);
                            cmd.Parameters.AddWithValue("@R_Gender", A_R_gender_tbx.Text);
                            cmd.Parameters.AddWithValue("@R_BloodGroup", A_R_bloodGroup_tbx.Text);
                            cmd.Parameters.AddWithValue("@R_NIC", A_R_Nic_tbx.Text);
                            cmd.Parameters.AddWithValue("@R_Nationality", A_R_Natinality_tbx.Text);
                            cmd.Parameters.AddWithValue("@R_Specialty", A_R_Specialty_tbx.Text);
                            cmd.Parameters.AddWithValue("@R_Email", A_R_Email_tbx.Text);
                            cmd.Parameters.AddWithValue("@R_ContactNo", A_R_contactNo_tbx.Text);
                            cmd.Parameters.AddWithValue("@R_ExperiencedYears", A_R_experiecedYears_tbx.Text);
                            cmd.Parameters.AddWithValue("@R_Certificates", A_R_certificate_tbx.Text);
                            cmd.Parameters.AddWithValue("@R_Address", A_R_address_tbx.Text);
                            cmd.Parameters.AddWithValue("@R_DateOfBirth", D_Register_DTimePicker.Value);
                            cmd.Parameters.AddWithValue("@R_RegisteredTime", now.TimeOfDay.ToString(@"hh\:mm\:ss"));
                            cmd.Parameters.AddWithValue("@R_RegisteredDate", now.Date);

                            generatedReceptionID = Convert.ToInt32(cmd.ExecuteScalar());

                            // get Doctor ID 
                            MessageBox.Show("Reception added successfully with ID: " + generatedReceptionID, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            MyAddUserLoginDetails(generatedReceptionID);

                        }

                    }
                    catch (Exception ex)
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


        private void MyAddUserLoginDetails(int id)
        {
            //Create User name And Password =================================================================================

            string reception_Name = A_R_NameWithInitials_tbx.Text;
            string reception_Age = A_R_age_tbx.Text.Trim();

            // Get the first three letters of each string
            string abbreviatedName = reception_Name.Substring(0, Math.Min(reception_Name.Length, 3));
            abbreviatedName = abbreviatedName.Trim();

            string userName = "D" + abbreviatedName + reception_Age;
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
                        command.Parameters.AddWithValue("@UserPosition", "Reception");
                        command.Parameters.AddWithValue("@UserName", userName);
                        command.Parameters.AddWithValue("@UserPassword", password);
                        command.Parameters.AddWithValue("@UserID", id);

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
