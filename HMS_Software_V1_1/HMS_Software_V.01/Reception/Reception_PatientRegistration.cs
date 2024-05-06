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

namespace HMS_Software_V1._01.Reception
{
    public partial class Reception_PatientRegistration : Form
    {
        SqlConnection connect = new SqlConnection(MyCommonConnecString.ConnectionString);

        private int userID;
        public Reception_PatientRegistration(int userID)
        {
            InitializeComponent();

            this.FormClosed += (s, e) => new Reception_Dashboard(userID).Show();
            this.userID = userID;
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

        private void R_P_Register_btn_Click(object sender, EventArgs e)
        {
            if (R_P_Reg_fullName_tbx.Text == ""
                || R_P_Reg_NameWithInitials_tbx.Text ==""
                || R_P_Reg_age_tbx.Text ==""
                || R_P_Reg_gender_tbx.Text == ""
                || R_P_Reg_Nic_tbx.Text == ""
                || R_P_Reg_contactNo_tbx.Text == ""
                || R_P_Reg_GuardenName_tbx.Text == ""
                || R_P_Reg_GuardenCoNo_tbx.Text == ""
                || R_P_Reg_address_tbx.Text == ""
                )
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

                        string quarryPatient = "INSERT INTO Patient " +
                            "(P_FullName, P_NameWithIinitials, P_Age, P_Gender," +
                            "P_NIC, P_ContactNo, P_Address, P_DathOfBirth, P_G_Name, P_G_ContactNo," +
                            "P_RegisteredTime, P_RegisteredDate, P_Status) " +

                            "VALUES (@fullName, @nameWithInitials, @age, @gender," +
                            "@nic, @contactNo, @address, @dateOfBirth, @gName, @gContactNo," +
                            "@registeredTime, @registeredDate, @status)";

                        using (SqlCommand cmd = new SqlCommand(quarryPatient, connect))
                        {
                            cmd.Parameters.AddWithValue("@fullName", R_P_Reg_fullName_tbx.Text);
                            cmd.Parameters.AddWithValue("@nameWithInitials", R_P_Reg_NameWithInitials_tbx.Text);
                            cmd.Parameters.AddWithValue("@age", R_P_Reg_age_tbx.Text);
                            cmd.Parameters.AddWithValue("@gender", R_P_Reg_gender_tbx.Text);
                            cmd.Parameters.AddWithValue("@nic", R_P_Reg_Nic_tbx.Text);
                            cmd.Parameters.AddWithValue("@contactNo", R_P_Reg_contactNo_tbx.Text);
                            cmd.Parameters.AddWithValue("@address", R_P_Reg_address_tbx.Text);
                            cmd.Parameters.AddWithValue("@dateOfBirth", R_P_Reg_DTimePicker.Value);
                            cmd.Parameters.AddWithValue("@gName", R_P_Reg_GuardenName_tbx.Text);
                            cmd.Parameters.AddWithValue("@gContactNo", R_P_Reg_GuardenCoNo_tbx.Text);
                            cmd.Parameters.AddWithValue("@registeredTime", DateTime.Now.TimeOfDay);
                            cmd.Parameters.AddWithValue("@registeredDate", today);
                            cmd.Parameters.AddWithValue("@status", "OPD");

                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Added successfully!"
                                       , "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            Reception_Dashboard reception_Dashboard = new Reception_Dashboard(userID);
                            reception_Dashboard.Show();
                            this.Hide();
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
