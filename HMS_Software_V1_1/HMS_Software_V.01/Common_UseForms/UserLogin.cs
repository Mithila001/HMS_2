using HMS_Software_V1._01.Admin;
using HMS_Software_V1._01.Admition_Officer;
using HMS_Software_V1._01.Doctor_OPD;
using HMS_Software_V1._01.Doctor_Ward;
using HMS_Software_V1._01.Lab_Management;
using HMS_Software_V1._01.Nurse_Ward;
using HMS_Software_V1._01.Reception;
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

namespace HMS_Software_V1._01.Common_UseForms
{
    public partial class UserLogin : Form
    {
        SqlConnection connect = new SqlConnection(MyCommonConnecString.ConnectionString);
        public UserLogin()
        {
            InitializeComponent();

            comboB_selcePosition.SelectedIndex = 0; //Default ComboBox Selection --> Doctor
            selectedPosition = "Doctor"; //Doctor Selected assigned to variable
        }

        private string unit; //Get the unit name to seed Doctor forms and nurse forms
        private int WardNumber;
        private void userLogin_btn_Click(object sender, EventArgs e)
        {

            // Combo Box check -------------------------------------------------------------------------------------------------------------------

            if (comboB_selcePosition.Text == "Doctor")
            {
                if (comboB_selceUnit.SelectedItem == null)
                {
                    MessageBox.Show("Add a Unit", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (string.IsNullOrEmpty(wardNumber_tbx.Text))
                {
                    MessageBox.Show("Add a Ward Number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (!int.TryParse(wardNumber_tbx.Text, out _))
                {
                    MessageBox.Show("Please enter a valid ward number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    // If All conditions are met
                    WardNumber = int.Parse(wardNumber_tbx.Text);
                    unit = comboB_selceUnit.Text;

                }

            }
            else if (comboB_selcePosition.Text == "Nurse")
            {
                if (string.IsNullOrEmpty(wardNumber_tbx.Text))
                {
                    MessageBox.Show("Add a Ward Number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (!int.TryParse(wardNumber_tbx.Text, out _))
                {
                    MessageBox.Show("Please enter a valid ward number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    //If All conditions are met
                    WardNumber = int.Parse(wardNumber_tbx.Text);
                    unit = comboB_selceUnit.Text;

                }

            }
            else
            {
                //Empty
            }


            // Tex box Check -------------------------------------------------------------------------------------------------------------------


            if (!string.IsNullOrWhiteSpace(useName_tbx.Text) && !string.IsNullOrEmpty(userPassword_tbx.Text))
            {
                try
                {
                    connect.Open();

                    // Getting Login User Details
                    string query = "SELECT UserID, UserPosition, UserName, UserPassword FROM UserLogin";

                    using (SqlCommand sqlCommand = new SqlCommand(query, connect))
                    {
                        SqlDataReader reader = sqlCommand.ExecuteReader();

                        bool loginSuccessful = false; // Flag to track successful login
                        int userID = -1;

                        while (reader.Read())
                        {
                            // Retrieve values from the current record
                            userID = reader.GetInt32(0);
                            string userPosition = reader.GetString(1);
                            string userName = reader.GetString(2);
                            string userPassword = reader.GetString(3);


                            if (userName == useName_tbx.Text && userPassword == userPassword_tbx.Text && comboB_selcePosition.Text == userPosition)
                            {
                                loginSuccessful = true; // Set flag to true if login is successful
                                break; // Exit the loop since login is successful
                            }

                        }

                        // Check if login was successful ---------------------------------------------------------------------------------------
                        if (loginSuccessful)
                        {
                            MessageBox.Show("Login Successful", "Infromation Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            if (comboB_selcePosition.Text == "Admin")
                            {
                                Admin_Dashboard admin_Dashboard = new Admin_Dashboard(userID);
                                admin_Dashboard.Show();

                            }
                            else if (comboB_selcePosition.Text == "Doctor")
                            {
                                if (unit == "Clinic")
                                {
                                    DoctorCheck_Dashboard doctorOPD = new DoctorCheck_Dashboard(userID, unit, WardNumber);
                                    doctorOPD.Show();
                                    this.Hide();


                                }
                                else if(unit == "Ward")
                                {
                                    DoctorWard_Dashboard doctorWard_DASH = new DoctorWard_Dashboard(userID, WardNumber);
                                    doctorWard_DASH.Show();
                                    this.Hide();
                                   

                                }
                                else if(unit == "OPD")
                                {
                                    DoctorCheck_Dashboard doctorOPD = new DoctorCheck_Dashboard(userID, unit, WardNumber);
                                    doctorOPD.Show();
                                    this.Hide();
                                }
                                else
                                {
                                    MessageBox.Show("Unit Not Found", "error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                

                            }
                            else if (comboB_selcePosition.Text == "Nurse")
                            {
                                NurseWard_Dashboard nurseWard_Dashboard = new NurseWard_Dashboard(userID, WardNumber);
                                nurseWard_Dashboard.Show();
                                this.Hide();
                            }
                            else if(comboB_selcePosition.Text == "Reception")
                            {
                                Reception_Dashboard reception_Dashboard = new Reception_Dashboard(userID);
                                reception_Dashboard.Show(); 
                                this.Hide();

                            }
                            else if(comboB_selcePosition.Text == "Admission Officer")
                            {
                                AdmissionOfficer_Dashboard admissionOfficer_Dashboard = new AdmissionOfficer_Dashboard(userID);
                                admissionOfficer_Dashboard.Show();
                                this.Hide();
                            }
                            else // To Lab_Managment
                            {
                                Lab_Dashboard lab_Dashboard = new Lab_Dashboard();
                                lab_Dashboard.Show();
                                this.Hide();

                            }

                        }
                        else
                        {
                            MessageBox.Show("Invalid Username or Password", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    Console.WriteLine(ex);
                }
                finally
                {
                    connect.Close();
                }
            }
            else
            {
                MessageBox.Show("Error", "Error Messsage", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        private string selectedPosition;

        // To Hide/Unhide Texbox according to the Position
        private void comboB_selcePosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboB_selcePosition.Text == "Doctor")
            {
                comboB_selceUnit.Visible = true;
                unit_lbl.Visible = true;
                wardNumber_tbx.Visible = true;
                warNumber_lbl.Visible = true;
                selectedPosition = "Doctor";

            }
            else if (comboB_selcePosition.Text == "Nurse")
            {
                comboB_selceUnit.Visible = false;
                unit_lbl.Visible = false;
                wardNumber_tbx.Visible = true;
                warNumber_lbl.Visible = true;
                selectedPosition = "Nurse";

            }
            else if (comboB_selcePosition.Text == "Admin")
            {
                comboB_selceUnit.Visible = false;
                unit_lbl.Visible = false;
                wardNumber_tbx.Visible = false;
                warNumber_lbl.Visible = false;
                selectedPosition = "Admin";

            }
            else if(comboB_selcePosition.Text == "Reception")
            {
                comboB_selceUnit.Visible = false;
                unit_lbl.Visible = false;
                wardNumber_tbx.Visible = false;
                warNumber_lbl.Visible = false;
                selectedPosition = "Reception";

            }
            else if(comboB_selcePosition.Text == "LabEmployee")
            {
                comboB_selceUnit.Visible = false;
                unit_lbl.Visible = false;
                wardNumber_tbx.Visible = false;
                warNumber_lbl.Visible = false;
                selectedPosition = "LabEmployee";

            }
            else
            {
                comboB_selceUnit.Visible = false;
                unit_lbl.Visible = false;
                wardNumber_tbx.Visible = false;
                warNumber_lbl.Visible = false;
                selectedPosition = "Admission Officer";

            }

            Console.WriteLine("Position ID : "+ selectedPosition);
        }
    }
}
