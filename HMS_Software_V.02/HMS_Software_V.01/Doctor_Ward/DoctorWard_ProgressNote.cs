using HMS_Software_V_2.Common_UseForms;
using HMS_Software_V_2.Common_UseForms.OOP;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;


namespace HMS_Software_V_2.Doctor_Ward
{
    public partial class DoctorWard_ProgressNote : Form
    {
        public Form DoctorPatientCheckWardFromReferece { get; set; }



        string PatienName;
        string PatientRID;
        string P_Condition;
        int PatientVisitCount;
        string PatientAge;
        string PatientGender;
        string DoctorName;
        string DoctorRID;
        string DoctorTitle;
        int DoctorID;
        string WardName;
        public DoctorWard_ProgressNote(
                string SWP_PatientName,
                string SWP_PatientRID,
                string SWP_PatientCondition,
                int SWP_PatietnVisitCount,
                string SWP_D_Name,
                string SWP_D_Title,
                string SWP_D_RID,
                string SWP_WardName,
                int SWP_D_ID,
                string SWP_PatientAge,
                string SWP_PatientGender)
        {

            InitializeComponent();

            this.PatienName = SWP_PatientName;
            this.PatientRID = SWP_PatientRID;
            this.P_Condition = SWP_PatientCondition;
            this.PatientVisitCount = SWP_PatietnVisitCount;
            this.DoctorName = SWP_D_Name;
            this.DoctorRID = SWP_D_RID;
            this.WardName = SWP_WardName;
            this.DoctorTitle = SWP_D_Title;
            this.DoctorID = SWP_D_ID;
            this.PatientAge = SWP_PatientAge;
            this.PatientGender = SWP_PatientGender;


            MyLoadBasicDetails();
            MyCreateMedicalEvetn();
        }
        private void MyLoadBasicDetails()
        {
            string todayDateString = DateTime.Today.ToString("yyyy-MM-dd");

            string formattedTime = DateTime.Now.ToString("h:mm tt");


            DWPN_Date.Text = todayDateString;
            DWPN_Time.Text = formattedTime;

            DWPN_D_Name.Text = DoctorName;
            DWPN_D_ID.Text = DoctorRID;
            DWPN_D_Title.Text = DoctorTitle;

            DWPN_WardName.Text = WardName;

            DWPN_P_Name.Text = PatienName;
            DWPN_P_Condition.Text = P_Condition;
            DWPN_P_RID.Text = PatientRID;

            PatientVisitCount = PatientVisitCount + 1; // Add 1 to the Count value
            DWPN_P_VisitCount.Text = PatientVisitCount.ToString(); 


        }

        private int WardID;
        private string PatientMID;

        private void MyCreateMedicalEvetn()
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(MyCommonConnecString.ConnectionString))
                {
                    connect.Open();

                    // Find the Ward Number -------------------------------------------------------------------------------------------------
                    string query1 = "SELECT WardNumber FROM WardTypes WHERE WardName = @wardName";

                    using (SqlCommand command = new SqlCommand(query1, connect))
                    {
                        command.Parameters.AddWithValue("@wardName", WardName);
                        Console.WriteLine("Ward Name: " + WardName);
                        try
                        {
                            SqlDataReader reader = command.ExecuteReader();

                            // Check if any rows were returned
                            if (reader.Read())
                            {
                                WardID = Convert.ToInt32(reader["WardNumber"]);

                            }
                            else
                            {
                                MessageBox.Show("No matching Ward record found.");
                            }
                            reader.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error:11 " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Console.WriteLine("Error11:" + ex);
                        }
                    }


                    // Creating a Patient Medical Event -------------------------------------------------------------------------------------------------

                    string query = "INSERT INTO PatientMedical_Event (PatientRegistration_ID, Doctor_ID, PMRE_Location, PMRE_Date, PMRE_Time, PatinetProgrestNote_ID, PatietnMedicalCondition)"
                    + "VALUES (@patietnRegistrationId, @doctorId, @location, @date, @time, @patinetProgrestNote_ID, @patietnMedicalCondition)";

                    DateTime today = DateTime.Today;
                    DateTime currentTime = DateTime.Now;

                    Console.WriteLine("Creating a Medical Event");

                    using (SqlCommand command = new SqlCommand(query, connect))
                    {
                        command.Parameters.AddWithValue("@patietnRegistrationId", PatientRID);
                        command.Parameters.AddWithValue("@doctorId", DoctorID);
                        command.Parameters.AddWithValue("@location", "Ward"); // Warnig: this need to change
                        command.Parameters.AddWithValue("@date", today);
                        command.Parameters.AddWithValue("@time", currentTime);
                        command.Parameters.AddWithValue("@patinetProgrestNote_ID", DWPN_P_ProgressNote_RichTbx.Text);
                        command.Parameters.AddWithValue("@patietnMedicalCondition", DWPN_P_AddCondition_tbx.Text);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            // Query executed successfully
                            Console.WriteLine("Insert into PatientMedical_Event executed successfully.");
                        }
                        else
                        {
                            // No rows affected (possible validation)
                            Console.WriteLine("Failed to insert into PatientMedical_Event.");
                        }
                    }




                    // Getting Patient Medical Event ID  --------------------------------------------------------------------------------------------

                    string query3 = "SELECT PatientMedicalEvent_ID FROM PatientMedical_Event WHERE PatientRegistration_ID = @PatientRegistration_ID";

                    using (SqlCommand command = new SqlCommand(query3, connect))
                    {
                        command.Parameters.AddWithValue("@PatientRegistration_ID", PatientRID);
                        /*Console.WriteLine("DoctorID from dashboard: " + DoctorID);*/
                        try
                        {
                            SqlDataReader reader = command.ExecuteReader();

                            // Check if any rows were returned
                            if (reader.Read())
                            {
                                PatientMID = reader["PatientMedicalEvent_ID"].ToString();
                                MessageBox.Show("Patient Medical Event ID ---------------> " + PatientMID);

                            }
                            else
                            {
                                MessageBox.Show("No matching PatientMedicalEvent_ID record found.");
                            }
                            reader.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error:4 " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Console.WriteLine("Error4:" + ex);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:22 " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine("Error22:" + ex);
            }
        }


        

        private void DWPN_P_Confirm_btn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(DWPN_P_AddCondition_tbx.Text))
            {
                MessageBox.Show("Add Patietn Condition", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (string.IsNullOrEmpty(DWPN_P_ProgressNote_RichTbx.Text))
            {
                MessageBox.Show("Add Patietn ProgressNote", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                // If every Condtion is passed
                bool Admitted_Patients = false;
                bool Admitted_Patients_VisitEvent = false;

                try
                {
                    using (SqlConnection connect = new SqlConnection(MyCommonConnecString.ConnectionString))
                    {
                        connect.Open();


                        // Updating Admitted_Patients Table --------------------------------------------------------------------------------------------
                        
                        string query = "UPDATE Admitted_Patients" +
                            " SET P_Condition = @p_Condition, "
                            + "P_Visite_TotalRounds = @p_Visite_TotalRounds "
                            + "WHERE P_RID = @p_RID;";

                        using (SqlCommand command = new SqlCommand(query, connect))
                        {
                            command.Parameters.AddWithValue("@p_Condition", P_Condition);
                            command.Parameters.AddWithValue("@p_Visite_TotalRounds", PatientVisitCount+1);
                            command.Parameters.AddWithValue("@p_RID", PatientRID);


                            int rowsAffected = command.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                // Query executed successfully 
                                Console.WriteLine("UPDATE Admitted_Patients successfully.");
                                Admitted_Patients = true;
                            }
                            else
                            {
                                // No rows affected (possible validation)
                                Console.WriteLine("Failed to UPDATE Admitted_Patients.");
                            }
                        }



                        // Getting Patient Medical Event ID  --------------------------------------------------------------------------------------------
                        
                        /*string query3 = "SELECT PatientMedicalEvent_ID FROM PatientMedical_Event WHERE PatientRegistration_ID = @PatientRegistration_ID";

                        using (SqlCommand command = new SqlCommand(query3, connect))
                        {
                            command.Parameters.AddWithValue("@PatientRegistration_ID", PatientRID);
                            *//*Console.WriteLine("DoctorID from dashboard: " + DoctorID);*//*
                            try
                            {
                                SqlDataReader reader = command.ExecuteReader();

                                // Check if any rows were returned
                                if (reader.Read())
                                {
                                    PatientMID = reader["PatientMedicalEvent_ID"].ToString();
                                    MessageBox.Show("Patient Medical Event ID ---------------> "+ PatientMID);

                                }
                                else
                                {
                                    MessageBox.Show("No matching PatientMedicalEvent_ID record found.");
                                }
                                reader.Close();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error:4 " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Console.WriteLine("Error4:" + ex);
                            }
                        }*/



                        // Updating Admitted_Patients_VisitEvent Table  --------------------------------------------------------------------------------------------+
                        
                        string query2 = "UPDATE Admitted_Patients_VisitEvent" +
                            " SET Visited_Doctor_ID = @visited_Doctor_ID, "
                            + "Visite_Time = @visite_Time, "
                            + "P_MedicalEventID = @p_MedicalEventID, "
                            + "Is_VisitedByDoctor = @is_VisitedByDoctor, "
                            + "P_Condition = @p_Condition, "
                            + "Visite_Round = @visite_Round "
                            + "WHERE P_RID = @p_RID;";
                        DateTime currentTime = DateTime.Now;

                        using (SqlCommand command = new SqlCommand(query2, connect))
                        {
                            command.Parameters.AddWithValue("@visited_Doctor_ID", DoctorID);
                            command.Parameters.AddWithValue("@visite_Time", currentTime);
                            command.Parameters.AddWithValue("@p_MedicalEventID", PatientMID);
                            command.Parameters.AddWithValue("@is_VisitedByDoctor", 1);
                            command.Parameters.AddWithValue("@p_Condition", P_Condition);
                            command.Parameters.AddWithValue("@visite_Round", PatientVisitCount + 1);
                            command.Parameters.AddWithValue("@p_RID", PatientRID);


                            int rowsAffected = command.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                // Query executed successfully
                                Console.WriteLine("UPDATE Admitted_Patients_VisitEvent successfully.");
                                Admitted_Patients_VisitEvent = true;
                            }
                            else
                            {
                                // No rows affected (possible validation)
                                Console.WriteLine("Failed to UPDATE Admitted_Patients_VisitEvent.");
                            }
                        }

                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Error:3 " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Console.WriteLine("Error3:" + ex);
                }

                if((Admitted_Patients) && (Admitted_Patients_VisitEvent))
                {
                    MessageBox.Show("Success!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    DoctorWard_Dashboard doctorWard_Dashboard = new DoctorWard_Dashboard(DoctorID, WardID);
                    doctorWard_Dashboard.Show();
                    this.Hide();
                }
            }

            
        }

        // -------------------------------------------------------------------------- Reference for this form
        public Form DoctorWard_ProgressNote_Refferece { get; set; }

        // =========================== Data Transporter =============================
        public class MyDataStoringClass
        {
            public int DoctorID { get; set; }
            public string DoctorName { get; set; }
            public string DoctorPosition { get; set; }
            public string PatientRID { get; set; }
            public string PatientName { get; set; }
            public string PatientAge { get; set; }
            public string PatientGender { get; set; }
            public string PatientMedicalEventID { get; set; }
            public string EventUnitType { get; set; }
            public int WardNumber { get; set; }

        }

        private ForCommonLabRequests doctorDataSendToLabRequest;
        private void DWPN_P_LabRequest_btn_Click(object sender, EventArgs e)
        {


            /* dataTranspoter2.DoctorID = DoctorID;
             dataTranspoter2.DoctorName = DoctorName;
             dataTranspoter2.DoctorPosition = DoctorTitle;
             dataTranspoter2.PatientRID = PatientRID;
             dataTranspoter2.PatientName = PatienName;
             dataTranspoter2.PatientAge = PatientAge;
             dataTranspoter2.PatientGender = PatientGender;
             dataTranspoter2.PatientMedicalEventID = PatientMID;
             dataTranspoter2.EventUnitType = "Ward "+ WardName;*/

            Common_UseForms.OOP.Doctor_Ward doctorW_To_LabRequest = new Common_UseForms.OOP.Doctor_Ward();
            doctorW_To_LabRequest.DoctorID = DoctorID;
            doctorW_To_LabRequest.DoctorName = DoctorName;
            doctorW_To_LabRequest.DoctorPosition = DoctorTitle;
            doctorW_To_LabRequest.PatientRID = PatientRID;
            doctorW_To_LabRequest.PatientName = PatienName;
            doctorW_To_LabRequest.PatientAge = PatientAge;
            doctorW_To_LabRequest.PatientGender = PatientGender;
            doctorW_To_LabRequest.PatientMedicalEventID = PatientMID;
            doctorW_To_LabRequest.EventUnitType = "Ward " + WardName;
            doctorW_To_LabRequest.WardNumber = WardID;


            Common_MakeLabRequest common_MakeLabRequest = new Common_MakeLabRequest(doctorW_To_LabRequest);
            common_MakeLabRequest.DoctorPatientCheckWardFromReferece = this;
            common_MakeLabRequest.Show();
            this.Hide();
        }

        private void DWPN_P_Prescription_btn_Click(object sender, EventArgs e)
        {

            Common_UseForms.OOP.Doctor_Ward doctorW_To_Prescription = new Common_UseForms.OOP.Doctor_Ward();
            doctorW_To_Prescription.DoctorID = DoctorID;
            doctorW_To_Prescription.DoctorName = DoctorName;
            doctorW_To_Prescription.DoctorPosition = DoctorTitle;
            doctorW_To_Prescription.PatientRID = PatientRID;
            doctorW_To_Prescription.PatientName = PatienName;
            doctorW_To_Prescription.PatientAge = PatientAge;
            doctorW_To_Prescription.PatientGender = PatientGender;
            doctorW_To_Prescription.PatientMedicalEventID = PatientMID;
            doctorW_To_Prescription.EventUnitType = "Ward " + WardName;
            doctorW_To_Prescription.WardNumber = WardID;


            Console.WriteLine("DoctorID: " + DoctorID);
            Console.WriteLine("DoctorName: " + DoctorName);
            Console.WriteLine("DoctorPosition: " + DoctorTitle);
            Console.WriteLine("PatientRID: " + PatientRID);
            Console.WriteLine("PatientName: " + PatienName);
            Console.WriteLine("PatientAge: " + PatientAge);
            Console.WriteLine("PatientGender: " + PatientGender);
            Console.WriteLine("PatientMedicalEventID: " + PatientMID);
            Console.WriteLine("EventUnitType: Ward " + WardName);
            Console.WriteLine("WardNumber: " + WardID);


            Common_MakePrescription common_MakePrescription = new Common_MakePrescription(doctorW_To_Prescription);
            common_MakePrescription.DoctorPatientCheckWardFromReferece = this;
            common_MakePrescription.Show();
            this.Hide();

        }

        private void DWPN_Monitor_btn_Click(object sender, EventArgs e)
        {
            DoctorWard_Monitor doctorWard_Monitor = new DoctorWard_Monitor(DoctorName,WardName,PatientRID, PatientMID, DoctorID);
            doctorWard_Monitor.DoctorPatientCheckWardFromReferece = this;
            doctorWard_Monitor.Show();
            

        }

        private void DoctorWard_ProgressNote_FormClosed(object sender, FormClosedEventArgs e)
        {
            DoctorWard_Dashboard doctorWard_Dashboard = new DoctorWard_Dashboard(DoctorID, WardID);
            doctorWard_Dashboard.Show();
            this.Hide();
        }

        private void DWPN_DischargeBtn_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(MyCommonConnecString.ConnectionString))
                {
                    connect.Open();

                    string query1 = "INSERT INTO Patient_Discharge (Patient_RID, Doctor_ID) VALUES (@Patient_RID, @Doctor_ID)";
                    using (SqlCommand command = new SqlCommand(query1, connect))
                    {
                        // Add parameters
                        command.Parameters.AddWithValue("@Patient_RID", PatientRID);
                        command.Parameters.AddWithValue("@Doctor_ID", DoctorID);

                        // Execute the query
                        int rowsAffected = command.ExecuteNonQuery();

                        // Check if rows were affected
                        if (rowsAffected > 0)
                        {
                            
                            Console.WriteLine("Record Found");

                            string deleteQuery = "DELETE FROM Admitted_Patients WHERE P_RID = @PatientRID";
                            using (SqlCommand deleteCommand = new SqlCommand(deleteQuery, connect))
                            {
                                // Add parameter for P_RID
                                deleteCommand.Parameters.AddWithValue("@PatientRID", PatientRID);

                                // Execute the delete query
                                int deletedRows = deleteCommand.ExecuteNonQuery();

                                // Check if any rows were deleted
                                if (deletedRows > 0)
                                {
                                    MessageBox.Show("Successfully Discharged", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    Console.WriteLine("Delete row Error");
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Failed to Discharged", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:3 " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine("Error3:" + ex);

            }

        }
    }
}
