using HMS_Software_V_2.Admition_Officer;
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

namespace HMS_Software_V_2.Admission_Officer
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
                connect.Open();

                string query = "INSERT INTO PatientMedical_Event (PatientRegistration_ID, Doctor_ID, PMRE_Location, PMRE_Date, PMRE_Time, PatientExaminatioNote)"
                    + "VALUES (@patietnRegistrationId, @doctorId, @location, @date, @time, @patientExaminatioNote)";

                using (SqlCommand command = new SqlCommand(query, connect))
                {
                    command.Parameters.AddWithValue("@patietnRegistrationId", PatientRID);
                    command.Parameters.AddWithValue("@doctorId", AdmissionOfficerID);
                    command.Parameters.AddWithValue("@location", "Admission Office"); // Warnig: this need to change
                    command.Parameters.AddWithValue("@date", currentDate);
                    command.Parameters.AddWithValue("@time", timeString);
                    command.Parameters.AddWithValue("@patientExaminatioNote", typeExaminationNote_tbx.Text);
                    // Need to add PatientExaminatioNote

                    /*//Retriving Patient RID
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        string getIdQuery = "SELECT P_RegistrationID FROM PatientMedical_Event WHERE" +
                            " PMRE_Date = @date AND PMRE_Time = @time AND Doctor_ID = @doctorId";

                        using (SqlCommand getIdCommand = new SqlCommand(getIdQuery, connect))
                        {
                            getIdCommand.Parameters.AddWithValue("@date", currentDate);
                            getIdCommand.Parameters.AddWithValue("@time", timeString);
                            getIdCommand.Parameters.AddWithValue("@doctorId", AdmissionOfficerID);

                            // Executing the query
                            object result = getIdCommand.ExecuteScalar();
                            if (result != null)
                            {
                                Patient_RID = result.ToString();
                                Console.WriteLine($"PatientMedical_Event Record with ID {Patient_RID} found successfully.");
                            }
                            else
                            {
                                Console.WriteLine("PatientMedical_Event Record not found for the given criteria.");
                            }
                        }

                        Console.WriteLine($"PatientMedical_Event Record with ID {Patient_RID} inserted successfully.");
                        Console.WriteLine("PatientMedical_Event record created.");


                    }
                    else
                    {
                        Console.WriteLine("Failed to insert PatientMedical_Event record.");

                    }*/
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
                Console.WriteLine("Patient Medical Event Record created successfully.");
            }
        }

        private void AODA_switch_Ward_CheckedChanged(object sender, EventArgs e)
        {
            if (AODA_switch_Ward.Checked)
            {
                AODA_switch_Ward.Checked = false;
                AODA_switch_Ward.Checked = false;
            }

        }

        private void AODA_switch_ETU_CheckedChanged(object sender, EventArgs e)
        {
            if (AODA_switch_Ward.Checked)
            {
                AODA_switch_Ward.Checked = false;
                AODA_switch_Ward.Checked = false;
            }

        }

        private void AODA_switch_PCU_CheckedChanged(object sender, EventArgs e)
        {
            if (AODA_switch_Ward.Checked)
            {
                AODA_switch_Ward.Checked = false;
                AODA_switch_Ward.Checked = false;
            }

        }


        private bool isAdmitted;
        private string patientStatus;
        private bool IsPatientRIDfound = false;
        private void AODA_admit_btn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(typeExaminationNote_tbx.Text))
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
                    connect.Open();


                    //Check Patient RID
                    string query1 = "SELECT P_RegistrationID FROM Patient WHERE P_RegistrationID = @patientRID";
                    using (SqlCommand command = new SqlCommand(query1, connect))
                    {
                        command.Parameters.AddWithValue("@patientRID", "P"+AOVR_ward_tbx.Text);

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
                            MessageBox.Show("Error: " + ex.Message);
                        }//Check if the RID is found
                    }


                    DateTime currentDate = DateTime.Today;
                    DateTime currentTime = DateTime.Now;

                    if(IsPatientRIDfound)
                    {
                        // Creating a Patient_Admit Record
                        string query = "INSERT INTO Patient_Admit (P_RegistrationID, P_NameWithInitials, P_Age, P_Gender, P_ReferralNote," +
                            " Doctor_ID, Requested_Time, Requested_Date, Is_Urgent, Is_Admitted, Unit_Type,Sended_To)"
                            + " VALUES (@prID, @pName, @pAge, @pGender, @pReferralNote, @dID, @time, @date, @isUrgent, @isAdmitted, @unitType, @sendedTo)";

                        using (SqlCommand cmd = new SqlCommand(query, connect))
                        {
                            cmd.Parameters.AddWithValue("@prID", PatientRID);
                            cmd.Parameters.AddWithValue("@pName", PatientName);
                            cmd.Parameters.AddWithValue("@pAge", PatientAge);
                            cmd.Parameters.AddWithValue("@pGender", PatientGender);
                            cmd.Parameters.AddWithValue("@pReferralNote", "-");
                            cmd.Parameters.AddWithValue("@dID", AdmissionOfficerID);
                            cmd.Parameters.AddWithValue("@time", currentTime);
                            cmd.Parameters.AddWithValue("@date", currentDate);
                            cmd.Parameters.AddWithValue("@isUrgent", 0);
                            cmd.Parameters.AddWithValue("@isAdmitted", true);
                            cmd.Parameters.AddWithValue("@unitType", "Admission Office");
                            cmd.Parameters.AddWithValue("@sendedTo", patientStatus);

                            int rowsAffected = cmd.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                Console.WriteLine("Patient Admit Record created successfully.");
                                MyCreateMedicalEvnet();
                            }
                            else
                            {
                                Console.WriteLine("Failed to create Patient Admit Record.");
                                MessageBox.Show("Failed to create Patient Admit Record", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }

                    }
                    else
                    {
                        MessageBox.Show("Patietn RID is not matched", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
    }
}
