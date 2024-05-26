using HMS_Software_V1._01.Admition_Officer;
using System;
using System.Collections;
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

namespace HMS_Software_V1._01.Admission_Officer
{
   
    public partial class AdmissionOfficer_DirectAdmit : Form
    {
        SqlConnection connect = new SqlConnection(MyCommonConnecString.ConnectionString);

        string PatientName;
        string PatientAge;
        string PatientGender;
        string PatientRID;
        string AO_Name;
        int AdmissionOfficerID;

        public AdmissionOfficer_DirectAdmit(string patientName, string patientAge, string patientGender, string AO_Name, string patientRID, int AdmissionOfficerID)
        {
            InitializeComponent();
            this.PatientName = patientName;
            this.PatientAge = patientAge;
            this.PatientGender = patientGender;
            this.AO_Name = AO_Name;
            this.PatientRID = patientRID;
            this.AdmissionOfficerID = AdmissionOfficerID;

            MyLoadBasicData();
            
        }

        private void MyLoadBasicData()
        {
            AODA_AO_Name.Text = AO_Name;
            AODA_patient_Name.Text = PatientName;
            AODA_patient_Gender.Text = PatientGender;
            AODA_patient_Age.Text = PatientAge;

            AOVR_date.Text = DateTime.Now.ToString("d MMMM yyyy");
            AOVR_time.Text = DateTime.Now.ToString("hh:mm tt");
        }

        private void MyCreateMedicalEvnet()
        {
            // Adding date and time
            DateTime currentDate = DateTime.Today;
            string formattedDate = currentDate.ToString("d MMMM yyyy");

            DateTime currentTime = DateTime.Now;
            string timeString = currentTime.ToString("hh:mm tt");

           

            try
            {
                using(SqlConnection connect = new SqlConnection(MyCommonConnecString.ConnectionString))
                {
                    connect.Open();

                    string query = "INSERT INTO PatientMedical_Event (PatientRegistration_ID, Doctor_ID, PMRE_Location, PMRE_Date, PMRE_Time, PatientExaminatioNote)"
                        + "VALUES (@patietnRegistrationId, @doctorId, @location, @date, @time, @patientExaminatioNote)";

                    using (SqlCommand command = new SqlCommand(query, connect))
                    {
                        command.Parameters.AddWithValue("@patietnRegistrationId", PatientRID);
                        command.Parameters.AddWithValue("@doctorId", AdmissionOfficerID);
                        command.Parameters.AddWithValue("@location", "Admission Office");
                        command.Parameters.AddWithValue("@date", currentDate);
                        command.Parameters.AddWithValue("@time", timeString);
                        command.Parameters.AddWithValue("@patientExaminatioNote", typeExaminationNote_tbx.Text);
                        // Need to add PatientExaminatioNote

                        //Retriving Patient RID
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Created a PatientMedical_Event");

                            AdmissionOfficer_DirectAdmit_FormClosed(this, null);

                        }
                        else
                        {
                            Console.WriteLine("Failed to insert PatientMedical_Event record.");

                        }
                    }

                }


                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

                Console.WriteLine("Error:  " + ex);
            }
            finally
            {
                connect.Close();
            }
        }

        private void AODA_switch_Ward_CheckedChanged(object sender, EventArgs e)
        {
            if (AODA_switch_Ward.Checked)
            {
                AODA_switch_ETU.Checked = false;
                AODA_switch_PCU.Checked = false;
                AODA_switch_Ward.Checked = true;
            }

        }

        private void AODA_switch_ETU_CheckedChanged(object sender, EventArgs e)
        {
            if (AODA_switch_ETU.Checked)
            {
                AODA_switch_Ward.Checked = false;
                AODA_switch_PCU.Checked = false;
                AODA_switch_ETU.Checked = true;
            }

        }

        private void AODA_switch_PCU_CheckedChanged(object sender, EventArgs e)
        {
            if (AODA_switch_PCU.Checked)
            {
                AODA_switch_Ward.Checked = false;
                AODA_switch_ETU.Checked = false;
                AODA_switch_PCU.Checked = true;
            }

        }

        private void MyInsertDataToAdmittedPatient()
        {
            try
            {


                string query6 = "INSERT INTO Admitted_Patients (P_RID, P_NameWithInitials, P_Age, P_Gender, P_Admit_To, P_Condition, P_Visite_TotalRounds, P_Admitted_Date, P_Admitted_Time, P_Ward) " +
                 "VALUES (@P_RID, @P_NameWithInitials, @P_Age, @P_Gender, @P_Admit_To, @P_Condition, @P_Visite_TotalRounds, @P_Admitted_Date, @P_Admitted_Time, @P_Ward)";

                using (SqlConnection connect = new SqlConnection(MyCommonConnecString.ConnectionString))
                {
                    connect.Open();
                    using (SqlCommand insertCommand2 = new SqlCommand(query6, connect))
                    {
                        // Adding date and time
                        DateTime currentDate = DateTime.Today;
                        string formattedDate = currentDate.ToString("d MMMM yyyy");

                        DateTime currentTime = DateTime.Now;
                        string timeString = currentTime.ToString("hh:mm tt");


                        insertCommand2.Parameters.AddWithValue("@P_RID", PatientRID);
                        insertCommand2.Parameters.AddWithValue("@P_NameWithInitials", PatientName);
                        insertCommand2.Parameters.AddWithValue("@P_Age", PatientAge);
                        insertCommand2.Parameters.AddWithValue("@P_Gender", PatientGender);
                        insertCommand2.Parameters.AddWithValue("@P_Admit_To", "Ward");
                        insertCommand2.Parameters.AddWithValue("@P_Condition", "Just Admitted");
                        insertCommand2.Parameters.AddWithValue("@P_Visite_TotalRounds", 0);
                        insertCommand2.Parameters.AddWithValue("@P_Admitted_Date", formattedDate);
                        insertCommand2.Parameters.AddWithValue("@P_Admitted_Time", currentTime);
                        insertCommand2.Parameters.AddWithValue("@P_Ward", AOVR_ward_tbx.Text);

                        int rowsAffected = insertCommand2.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Doctor record inserted successfully.");
                        }
                        else
                        {
                            Console.WriteLine("Failed to insert Doctor record.");
                            MessageBox.Show("Failed to insert Doctor record", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        } // No need




        string WardNumber;
        private bool isAdmitted = false;
        private string patientStatus;
        private bool IsPatientRIDfound = false;
        private void AODA_admit_btn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(typeExaminationNote_tbx.Text))
            {

                if (AODA_switch_ETU.Checked)
                {

                    isAdmitted = true;
                    patientStatus = "ETU";
                }
                else if (AODA_switch_Ward.Checked && !string.IsNullOrEmpty(AOVR_ward_tbx.Text))
                {

                    isAdmitted = true;
                    patientStatus = "Ward (" + AOVR_ward_tbx.Text + ") ETU";

                    string input = AOVR_ward_tbx.Text;
                    int number;
                    if (int.TryParse(input, out number))
                    {
                        WardNumber = number.ToString();

                    }
                    else
                    {
                        MessageBox.Show("Invalide Ward Number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
               
                    }


                }
                else if (AODA_switch_PCU.Checked)
                {

                    isAdmitted = true;
                    patientStatus = "PCU";
                }
                else
                {
                    Console.WriteLine("Failed to get in.");
                    MessageBox.Show("Select a Unite", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


                Console.WriteLine("Data adding process started");
               

                try
                {

                    using (SqlConnection connect = new SqlConnection(MyCommonConnecString.ConnectionString))
                    {
                        connect.Open();

                        /*Console.WriteLine($"PatientRID   : {PatientRID}");
                   //Check Patient RID
                   string query1 = "SELECT P_RegistrationID FROM Patient WHERE P_RegistrationID = @patientRID";
                   using (SqlCommand command = new SqlCommand(query1, connect))
                   {
                       command.Parameters.AddWithValue("@patientRID", PatientRID);

                       try 
                       {                
                           // Execute the query and get the result
                           object result = command.ExecuteScalar();

                           if (result != null)
                           {
                               IsPatientRIDfound = true;
                           }
                           else
                           {
                               MessageBox.Show("No matching record found.");
                           }
                       }
                       catch (Exception ex)
                       {
                           Console.WriteLine($"Error: {ex.Message}");
                           MessageBox.Show("An error occurred while checking PatientRID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                       }//Check if the RID is found
                   }*/


                        DateTime currentDate = DateTime.Today;
                        DateTime currentTime = DateTime.Now;

                        #region Not Using
                        /*if(IsPatientRIDfound)
                                    {
                                        // Creating a Patient_Admit Record
                                        *//*string query = "INSERT INTO Patient_Admit (P_RegistrationID, P_NameWithInitials, P_Age, P_Gender, P_ReferralNote," +
                                            " Doctor_ID, Requested_Time, Requested_Date, Is_Urgent, Is_Admitted, Unit_Type,Sended_To)"
                                            + " VALUES (@prID, @pName, @pAge, @pGender, @pReferralNote, @dID, @time, @date, @isUrgent, @isAdmitted, @unitType, @sendedTo)";*//*
                                        string query = "INSERT INTO Admitted_Patients (P_RID, P_NameWithInitials, P_Age, P_Gender, P_Admit_To," +
                                           " P_Condition, P_Visite_TotalRounds, P_Admitted_Date, P_Admitted_Time, P_Ward)"
                                           + " VALUES (@P_RID, @P_NameWithInitials, @P_Age, @P_Gender, @P_Admit_To, @P_Condition, @P_Visite_TotalRounds, @P_Admitted_Date, @P_Admitted_Time, @P_Ward)";

                                        using (SqlCommand cmd = new SqlCommand(query, connect))
                                        {
                                            cmd.Parameters.AddWithValue("@P_RID", PatientRID);
                                            cmd.Parameters.AddWithValue("@P_NameWithInitials", PatientName);
                                            cmd.Parameters.AddWithValue("@P_Age", PatientAge);
                                            cmd.Parameters.AddWithValue("@P_Gender", PatientGender);
                                            cmd.Parameters.AddWithValue("@P_Admit_To", "Ward");
                                            cmd.Parameters.AddWithValue("@P_Condition", "Just Admitted");
                                            cmd.Parameters.AddWithValue("@P_Visite_TotalRounds", 0);
                                            cmd.Parameters.AddWithValue("@P_Admitted_Date", currentDate);
                                            cmd.Parameters.AddWithValue("@P_Admitted_Time", currentTime);
                                            cmd.Parameters.AddWithValue("@P_Ward", WardNumber);


                                            int rowsAffected = cmd.ExecuteNonQuery();
                                            if (rowsAffected > 0)
                                            {
                                                Console.WriteLine("Admitted_Patients Record created successfully.");
                                                MyCreateMedicalEvnet();
                                            }
                                            else
                                            {
                                                Console.WriteLine("Failed to create Admitted_Patients Record.");
                                                MessageBox.Show("Failed to create Admitted_Patients Record", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            }
                                        }

                                    }
                                    else
                                    {
                                        MessageBox.Show("Patietn RID is not matched", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }*/
                        #endregion

                        // Creating a Patient_Admit Record
                        /*string query = "INSERT INTO Patient_Admit (P_RegistrationID, P_NameWithInitials, P_Age, P_Gender, P_ReferralNote," +
                            " Doctor_ID, Requested_Time, Requested_Date, Is_Urgent, Is_Admitted, Unit_Type,Sended_To)"
                            + " VALUES (@prID, @pName, @pAge, @pGender, @pReferralNote, @dID, @time, @date, @isUrgent, @isAdmitted, @unitType, @sendedTo)";*/
                        string query = "INSERT INTO Admitted_Patients (P_RID, P_NameWithInitials, P_Age, P_Gender, P_Admit_To," +
                           " P_Condition, P_Visite_TotalRounds, P_Admitted_Date, P_Admitted_Time, P_Ward)"
                           + " VALUES (@P_RID, @P_NameWithInitials, @P_Age, @P_Gender, @P_Admit_To, @P_Condition, @P_Visite_TotalRounds, @P_Admitted_Date, @P_Admitted_Time, @P_Ward)";

                        using (SqlCommand cmd = new SqlCommand(query, connect))
                        {

                            cmd.Parameters.AddWithValue("@P_RID", PatientRID);
                            cmd.Parameters.AddWithValue("@P_NameWithInitials", PatientName);
                            cmd.Parameters.AddWithValue("@P_Age", PatientAge);
                            cmd.Parameters.AddWithValue("@P_Gender", PatientGender);
                            cmd.Parameters.AddWithValue("@P_Admit_To", "Ward");
                            cmd.Parameters.AddWithValue("@P_Condition", "Just Admitted");
                            cmd.Parameters.AddWithValue("@P_Visite_TotalRounds", 0);
                            cmd.Parameters.AddWithValue("@P_Admitted_Date", currentDate);
                            cmd.Parameters.AddWithValue("@P_Admitted_Time", currentTime);

                            if(patientStatus == "ETU")
                            {
                                cmd.Parameters.AddWithValue("@P_Ward", "ETU");
                            }
                            else if(patientStatus == "PCU")
                            {
                                cmd.Parameters.AddWithValue("@P_Ward", "PCU");
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@P_Ward", WardNumber);
                            }
                            


                            int rowsAffected = cmd.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                Console.WriteLine("Admitted_Patients Record created successfully.");
                                MyCreateMedicalEvnet();
                            }
                            else
                            {
                                Console.WriteLine("Failed to create Admitted_Patients Record.");
                                MessageBox.Show("Failed to create Admitted_Patients Record", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }

                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connect.Close();
                }
            }
            else
            {
                MessageBox.Show("Fill the note", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AdmissionOfficer_DirectAdmit_FormClosed(object sender, FormClosedEventArgs e)
        {
            AdmissionOfficer_Dashboard admissionOfficer_Dashboard = new AdmissionOfficer_Dashboard(AdmissionOfficerID);
            admissionOfficer_Dashboard.Show();
            this.Hide();
        }
    }
}
