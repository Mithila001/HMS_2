using HMS_Software_V1._01.Admission_Officer;
using HMS_Software_V1._01.Admission_Officer.UserControls;
using HMS_Software_V1._01.Common_UseForms;
using HMS_Software_V1._01.Doctor_OPD.UserControls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace HMS_Software_V1._01.Admition_Officer
{
    public partial class AdmissionOfficer_Dashboard : Form
    {
        SqlConnection connect = new SqlConnection(MyCommonConnecString.ConnectionString);

        private int AdmissionOfficerID;
        public AdmissionOfficer_Dashboard(int userID = 12)
        {
            InitializeComponent();
            this.AdmissionOfficerID = userID;

            MyLoadDashboadDetails();

            LoadBasicData();


            LoadAdmitRequestData();

        }


        int TotalAdmitted_Patients_Count;
        int Total_Patients_Today;
        int Admit_RequestCount;
        int Admitted_TodayCount;
       
        private void MyLoadDashboadDetails()
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(MyCommonConnecString.ConnectionString))
                {
                    connect.Open();

                    // Find Total Admitted Patient Count
                    string query1 = "SELECT COUNT(*) FROM Admitted_Patients";

                   
                    using (SqlCommand command = new SqlCommand(query1, connect))
                    {
                        command.Parameters.AddWithValue("@TodayDate", DateTime.Today);

                        TotalAdmitted_Patients_Count = (int)command.ExecuteScalar();


                    }


                    // Select today Registered patieetns
                    string query2 = "SELECT COUNT(*) FROM Admitted_Patients WHERE CONVERT(date, P_Admitted_Date) = @TodayDate";

                    // Create a command with the SQL query and the connection
                    using (SqlCommand command = new SqlCommand(query2, connect))
                    {
                        command.Parameters.AddWithValue("@TodayDate", DateTime.Today);

                        Total_Patients_Today = (int)command.ExecuteScalar();


                    }

                    // Select today Registered patieetns
                    string query3 = "SELECT COUNT(*) FROM Patient_Admit WHERE Is_Admitted = @Is_Admitted";

                    // Create a command with the SQL query and the connection
                    using (SqlCommand command = new SqlCommand(query3, connect))
                    {
                        command.Parameters.AddWithValue("@Is_Admitted", 0);

                        Admit_RequestCount = (int)command.ExecuteScalar();


                    }

                    // Select today Registered patieetns
                    string query4 = "SELECT COUNT(*) FROM Patient_Admit WHERE Is_Admitted = @Is_Admitted AND CONVERT(date, Requested_Date) = @TodayDate";

                    // Create a command with the SQL query and the connection
                    using (SqlCommand command = new SqlCommand(query4, connect))
                    {
                        command.Parameters.AddWithValue("@TodayDate", DateTime.Today);
                        command.Parameters.AddWithValue("@Is_Admitted", 1);

                        Admitted_TodayCount = (int)command.ExecuteScalar();


                    }


                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }


            TotalAdmitted_Patients.Text = TotalAdmitted_Patients_Count.ToString();
            TotalToday_Patients.Text = Total_Patients_Today.ToString();
            TotalAdmitRequests.Text = Admit_RequestCount.ToString();
            Total_Admitted_Today.Text = Admitted_TodayCount.ToString();

        }


        private string AO_Name;
        private string AO_Position;
        private string AO_Specialty;
        private string AO_RegistrationID;


        private void LoadBasicData()
        {
            try
            {
                connect.Open();

                // Load Admission Officer (Doctor) Name
                string query = "SELECT D_NameWithInitials, D_Position, D_Specialty, D_RegistrationID" +
                    " FROM Doctor WHERE Doctor_ID = @aoID";
                using (SqlCommand command = new SqlCommand(query, connect))
                {
                    command.Parameters.AddWithValue("@aoID", AdmissionOfficerID);
                    Console.WriteLine("AdmissionOfficerID from dashboard: " + AdmissionOfficerID);

                    try
                    {
                     
                        SqlDataReader reader = command.ExecuteReader();

                        // Check if any rows were returned
                        if (reader.Read())
                        {
                            AO_Name = reader["D_NameWithInitials"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("No matching Doctor record found.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error:1 " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Console.WriteLine("Error1:" + ex);
                    }
                }

                AOD_name.Text = AO_Name;
                TodayTime_lbl.Text = DateTime.Now.ToString("hh:mm tt");

                AOD_date.Text = DateTime.Now.ToString("d MMMM yyyy");
                AOD_time.Text = DateTime.Now.ToString("hh:mm tt");


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:1 " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine("Error1:" + ex);
            }
            finally
            {
                connect.Close();
            }
        }
   

        private void LoadAdmitRequestData()
        {
            try
            {
                connect.Open();
                DateTime today = DateTime.Today;

                string query = "SELECT P_RegistrationID, P_NameWithInitials, P_Age, P_Gender, P_ReferralNote, Doctor_ID, " +
                "Is_Urgent, Is_Admitted, Unit_Type, PatientAdmit_ID " +
                "FROM Patient_Admit";

                SqlCommand command = new SqlCommand(query, connect);
                using (SqlDataReader reader1 = command.ExecuteReader())
                {
                    // Loop through the records retrieved from the database
                    while (reader1.Read())
                    {
                        Console.WriteLine("Found UserData");
                        string p_RegistrationID = reader1["P_RegistrationID"].ToString();
                        string p_NameWithInitials = reader1["P_NameWithInitials"].ToString();
                        string p_age = reader1["P_Age"].ToString();
                        string p_Gender = reader1["P_Gender"].ToString();
                        string p_ReferralNote = reader1["P_ReferralNote"].ToString();
                        int p_DoctorID = Convert.ToInt32(reader1["Doctor_ID"]);
                        bool p_IsUrgent = (bool)reader1["Is_Urgent"];
                        bool p_IsAdmitted = (bool)reader1["Is_Admitted"];
                        string p_UnitType = reader1["Unit_Type"].ToString();
                        int p_admitID = (int)reader1["PatientAdmit_ID"];

                        /*int getclinicID = 0;

                        if (int.TryParse(GclinicID, out getclinicID))  // Convert to int
                        {

                            Console.WriteLine("Investigation ID: " + getclinicID);
                        }*/

                        /*DoctorCheck_clincType doctorOPD_ClincType = new DoctorCheck_clincType();

                        doctorOPD_ClincType.DOPDA_ClincType_lbl.Text = reader1["CT_Name"].ToString();*/

                        AdmitRequest admitRequest = new AdmitRequest(); 

                        admitRequest.AR_patienName.Text = p_NameWithInitials;
                        admitRequest.AR_patienRID.Text = p_RegistrationID;
                        admitRequest.AR_patientGender.Text = p_Gender;
                        admitRequest.AR_patientAge.Text = p_age;
                        admitRequest.AR_unitType.Text = p_UnitType;

                        admitRequest.AdmitRequestID = p_admitID;

                        admitRequest.AO_Name = AO_Name;
                        admitRequest.AO_Position = AO_Position;
                        admitRequest.AO_RegistrationID = AO_RegistrationID;
                        admitRequest.AO_Specialty = AO_Specialty;
                        admitRequest.AO_ID = AdmissionOfficerID;



                        if (p_IsUrgent)
                        {
                            admitRequest.AR_patientPanel_colorChanger.BackColor = Color.FromArgb(255, 128, 128);
                        }

                        admitRequests_FlowLP.SizeChanged += (sender, e) =>
                        {
                            // Adjust the width of the user control to match the width of the parent container
                            admitRequest.Width = admitRequests_FlowLP.ClientSize.Width - admitRequest.Margin.Horizontal;
                        };

                        if (!p_IsAdmitted)
                        {
                            admitRequests_FlowLP.Controls.Add(admitRequest);
                        }



                    }
                    reader1.Close();

                }




            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:1 " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine("Error1:" + ex);
            }
            finally
            {
                connect.Close();
            }
        }

        string patientRID;
        string patientName;
        string patientAge;
        string patientGender;
        bool dataReceived = false;

        private void AOD_directAdmit_btn_Click(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(AOVR_ward_tbx.Text))
            {
                try
                {
                    connect.Open();
                    //Getting Admit Request Details from the Database
                    string query1 = "SELECT P_NameWithIinitials, P_Age, P_Gender FROM" +
                        " Patient WHERE P_RegistrationID = @patientRID";

                    using (SqlCommand cmd = new SqlCommand(query1, connect))
                    {
                        cmd.Parameters.AddWithValue("@patientRID", "P"+AOVR_ward_tbx.Text);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                try
                                {
                                    patientRID = AOVR_ward_tbx.Text;
                                    patientName = reader["P_NameWithIinitials"].ToString();
                                    patientAge = reader["P_Age"].ToString();
                                    patientGender = reader["P_Gender"].ToString();
                                    dataReceived = true;
                                }
                                catch (FormatException)
                                {
                                    MessageBox.Show("Patient RID is not matched", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                    }

                    if (dataReceived)
                    {
                        MessageBox.Show("Success", "information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        AdmissionOfficer_DirectAdmit admissionOfficer_DirectAdmit = new AdmissionOfficer_DirectAdmit(patientName, patientAge, patientGender, AO_Name, patientRID, AdmissionOfficerID);
                        admissionOfficer_DirectAdmit.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Patient RID is not Found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Console.WriteLine("Error:" + ex);

                }
                finally
                {
                    connect.Close();
                }
            }
            else
            {
                MessageBox.Show("Add Patient RID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void AdmissionOfficer_Dashboard_FormClosed(object sender, FormClosedEventArgs e)
        {
            UserLogin userLogin = new UserLogin();
            userLogin.Show();
            this.Hide();
        }
    }
}
