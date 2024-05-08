using HMS_Software_V1._01.Common_UseForms;
using HMS_Software_V1._01.Common_UseForms.OOP;
using HMS_Software_V1._01.Doctor_OPD;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;


namespace HMS_Software_V1._01.Doctor_Ward
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

        int i = 0;
        public DoctorWard_ProgressNote(string SWP_PatientName, string SWP_PatientRID, string SWP_PatientCondition, int SWP_PatietnVisitCount, string SWP_D_Name, string SWP_D_Title,
            string SWP_D_RID, string SWP_WardName, int SWP_D_ID, string SWP_PatientAge, string SWP_PatientGender)

        {

            InitializeComponent();

            #region Assign Data
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
            #endregion


            DWPN_P_ViewHistory_btn.Visible = false;
            label20.Visible = false;

           /* if (i < 1)
            {
                MyCreateMedicalEvetn();
            }*/
            MyLoadBasicDetails();
            /*MyCreateMedicalEvetn();*/
        }

        
        bool startEvent = false;

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

            PatientVisitCount = PatientVisitCount + 1; // Including Current Doctor Visit
            DWPN_P_VisitCount.Text = PatientVisitCount.ToString(); 


        }

        private int WardID;
        int PatientMedicalEventID;


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
                    string query = "INSERT INTO PatientMedical_Event (PatientRegistration_ID, Doctor_ID, PMRE_Location, PMRE_Date, PMRE_Time, PatinetProgrestNote, PatietnMedicalCondition)"
                    + "VALUES (@patietnRegistrationId, @doctorId, @location, @date, @time, @PatinetProgrestNote, @patietnMedicalCondition)";

                    string dateString = DateTime.Today.ToString("yyyy-MM-dd");
                    string timeString = DateTime.Now.ToString("HH:mm:ss");



                    Console.WriteLine("Creating a Medical Event");

                    using (SqlCommand command = new SqlCommand(query, connect))
                    {
                        command.Parameters.AddWithValue("@patietnRegistrationId", PatientRID);
                        command.Parameters.AddWithValue("@doctorId", DoctorID);
                        command.Parameters.AddWithValue("@location", "Ward"); // Warnig: this need to change
                        command.Parameters.AddWithValue("@date", dateString);
                        command.Parameters.AddWithValue("@time", timeString);
                        command.Parameters.AddWithValue("@PatinetProgrestNote", DWPN_P_ProgressNote_RichTbx.Text);
                        command.Parameters.AddWithValue("@patietnMedicalCondition", DWPN_P_AddCondition_tbx.Text);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            // Query executed successfully
                            Console.WriteLine("Insert into PatientMedical_Event executed successfully.");



                            // Get the Patietn Medical Event ID ----------------------------------------------------------------------------------------------
                            string getIdQuery = "SELECT PatientMedicalEvent_ID FROM PatientMedical_Event WHERE" +
                            " PMRE_Date = @date AND PMRE_Time = @time AND Doctor_ID = @doctorId";

                            using (SqlCommand getIdCommand = new SqlCommand(getIdQuery, connect))
                            {
                                getIdCommand.Parameters.AddWithValue("@date", dateString);
                                getIdCommand.Parameters.AddWithValue("@time", timeString);
                                getIdCommand.Parameters.AddWithValue("@doctorId", DoctorID);

                                // Executing the query
                                object result = getIdCommand.ExecuteScalar();
                                if (result != null)
                                {
                                    PatientMedicalEventID = Convert.ToInt32(result);
                                    Console.WriteLine("PatientMedicalEventID ::::::::::::: " + PatientMedicalEventID);
                                    Console.WriteLine($"Retrived PatientMedical_Event ID:  {PatientMedicalEventID}");
                                }
                                else
                                {
                                    Console.WriteLine("PatientMedical_Event Record not found for the given criteria.");
                                }
                            }
                            Console.WriteLine($"PatientMedical_Event Record with ID {PatientMedicalEventID} inserted successfully.");
                        }
                        else
                        {
                            // No rows affected (possible validation)
                            Console.WriteLine("Failed to insert into PatientMedical_Event.");
                        }
                    }


                    #region Not Using
                    /*// Getting Patient Medical Event ID  --------------------------------------------------------------------------------------------

                                string query3 = "SELECT PatientMedicalEvent_ID FROM PatientMedical_Event WHERE PatientRegistration_ID = @PatientRegistration_ID";

                                using (SqlCommand command = new SqlCommand(query3, connect))
                                {
                                    command.Parameters.AddWithValue("@PatientRegistration_ID", PatientRID);
                                    // Warning !! this will find multiple Patietn Medical event IDs.
                                    *//*Console.WriteLine("DoctorID from dashboard: " + DoctorID);*//*
                                    try
                                    {
                                        SqlDataReader reader = command.ExecuteReader();

                                        // Check if any rows were returned
                                        if (reader.Read())
                                        {
                                            PatientMID = reader["PatientMedicalEvent_ID"].ToString();
                                           *//* MessageBox.Show("Patient Medical Event ID ---------------> " + PatientMID);*//*

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
                    #endregion
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:22 " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine("Error22:" + ex);
            }
        }

        //----------------------------------------------------------------------------------------------------------------

        private void Button_Click(object sender, EventArgs e)
        {
            startEvent = true;

            // to Execute P_Medical Event only once
            while (i < 1)
            {
                if (sender == DWPN_Monitor_btn)
                {
                    MyCreateMedicalEvetn();
                    i++;
                    Console.WriteLine("Medical Event Created from: Monitor");
                }
                else if (sender == DWPN_P_Prescription_btn)
                {
                    MyCreateMedicalEvetn();
                    i++;
                    Console.WriteLine("Medical Event Created from: Prescription");
                }
                else if (sender == DWPN_P_LabRequest_btn)
                {
                    MyCreateMedicalEvetn();
                    i++;
                    Console.WriteLine("Medical Event Created from: Lab Request");
                }
                else if (sender == DWPN_P_Confirm_btn)
                {
                    MyCreateMedicalEvetn();
                    i++;
                    Console.WriteLine("Medical Event Created from: Confirm Button");
                }

            }
        }

        //----------------------------------------------------------------------------------------------------------------


        private void DWPN_P_Confirm_btn_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(DWPN_P_ProgressNote_RichTbx.Text))
            {
                MessageBox.Show("Progress Note is Empty!! ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (string.IsNullOrEmpty(DWPN_P_AddCondition_tbx.Text))
            {
                MessageBox.Show("Add a Condition!! ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                try
                {
                    using (SqlConnection connect = new SqlConnection(MyCommonConnecString.ConnectionString))
                    {
                        connect.Open();

                        // Using PatientMedicalEventID to find the lab record that now created and get the current LabRequest_ID
                        string query2 = "UPDATE PatientMedical_Event SET PatinetProgrestNote = @PatinetProgrestNote WHERE PatientMedicalEvent_ID = @pmeID";
                        using (SqlCommand updateCommand = new SqlCommand(query2, connect))
                        {
                            updateCommand.Parameters.AddWithValue("@PatinetProgrestNote", DWPN_P_ProgressNote_RichTbx.Text);
                            updateCommand.Parameters.AddWithValue("@pmeID", PatientMedicalEventID);

                            int rowsAffected = updateCommand.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                Console.WriteLine("Patient Examinatio Note updated successfully.");
                                

                                #region Not Using
                                // Moving to the Doctor Ward Dashboard

                                /*  MyDataStoringClass dataTranspoter = new MyDataStoringClass();
                                  dataTranspoter.DoctorID = UserID;
                                  dataTranspoter.EventUnitType = UnitType;
                                  dataTranspoter.SendWardNumber = WardNumber;

                                  DoctorCheck_Dashboard doctorCheck_Dashboard = new DoctorCheck_Dashboard(dataTranspoter.DoctorID, dataTranspoter.EventUnitType, dataTranspoter.SendWardNumber);
                                  doctorCheck_Dashboard.Show();*/

                                /* this.Close();*/
                                #endregion

                                string updateQuery = "UPDATE Admitted_Patients_VisitEvent SET Is_VisitedByDoctor = @Admitted_Patients_VisitEvent, Visite_Time= @Visite_Time,"+
                                    " Visited_Doctor_ID = @Visited_Doctor_ID, P_MedicalEventID = @P_MedicalEventID, P_Condition =@P_Condition " +
                                    "WHERE P_RID = @patientRID";
                                using (SqlCommand command = new SqlCommand(updateQuery, connect))
                                {
                                    string formattedTime = DateTime.Now.ToString("hh:mm tt");

                                    command.Parameters.AddWithValue("@patientRID", PatientRID);
                                    command.Parameters.AddWithValue("@Visited_Doctor_ID", DoctorID);
                                    command.Parameters.AddWithValue("@Visite_Time", formattedTime);
                                    command.Parameters.AddWithValue("@P_MedicalEventID", PatientMedicalEventID);
                                    command.Parameters.AddWithValue("@Admitted_Patients_VisitEvent", 1);
                                    command.Parameters.AddWithValue("@P_Condition", DWPN_P_AddCondition_tbx.Text);
                                    int rowsAffected2 = command.ExecuteNonQuery();

                                    if (rowsAffected2 > 0)
                                    {
                                        Console.WriteLine("Admitted_Patients_VisitEvent updated successfully.");
                                        MessageBox.Show("Success!!.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                        DoctorWard_Dashboard doctorWard_Dashboard = new DoctorWard_Dashboard(DoctorID, WardID);
                                        doctorWard_Dashboard.Show();
                                        this.Hide();
                                    }
                                    else
                                    {
                                        // No matching records found
                                        Console.WriteLine("Admitted_Patients_VisitEvent, No matching records found");
                                        MessageBox.Show("Failed to update Admitted_Patients_VisitEvent", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }

                            }
                            else
                            {
                                Console.WriteLine("Failed to update PatientExaminatioNote.");
                                MessageBox.Show("Failed to update PatientExaminatioNote", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }

           

        }

        // -------------------------------------------------------------------------- Reference for this form
       /* public Form DoctorWard_ProgressNote_Refferece { get; set; }*/

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

        /*private ForCommonLabRequests doctorDataSendToLabRequest;*/
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
            doctorW_To_LabRequest.PatientMedicalEventID = PatientMedicalEventID;
            doctorW_To_LabRequest.EventUnitType = "Ward " + WardName;
            doctorW_To_LabRequest.WardNumber = WardID;
           


            Common_MakeLabRequest common_MakeLabRequest = new Common_MakeLabRequest(doctorW_To_LabRequest);
            common_MakeLabRequest.DoctorPatientCheckWardFromReferece = this;
            common_MakeLabRequest.Show();
            this.Hide();
        }

        private void DWPN_P_Prescription_btn_Click(object sender, EventArgs e)
        {

            /* Common_UseForms.OOP.Doctor_Ward doctorW_To_Prescription = new Common_UseForms.OOP.Doctor_Ward();
             #region Sending Data
             doctorW_To_Prescription.DoctorID = DoctorID;
             doctorW_To_Prescription.DoctorName = DoctorName;
             doctorW_To_Prescription.DoctorPosition = DoctorTitle;
             doctorW_To_Prescription.PatientRID = PatientRID;
             doctorW_To_Prescription.PatientName = PatienName;
             doctorW_To_Prescription.PatientAge = PatientAge;
             doctorW_To_Prescription.PatientGender = PatientGender;
             doctorW_To_Prescription.PatientMedicalEventID = PatientMedicalEventID;
             doctorW_To_Prescription.EventUnitType = "Ward " + WardName;
             doctorW_To_Prescription.WardNumber = WardID; 
             #endregion*/

            Common_UseForms.OOP.Doctor_Ward doctorW_To_Prescription = new Common_UseForms.OOP.Doctor_Ward();
            #region Sending Data
            doctorW_To_Prescription.DoctorID = DoctorID;
            doctorW_To_Prescription.DoctorName = DoctorName;
            doctorW_To_Prescription.DoctorPosition = DoctorTitle;
            doctorW_To_Prescription.PatientRID = PatientRID;
            doctorW_To_Prescription.PatientName = PatienName;
            doctorW_To_Prescription.PatientAge = PatientAge;
            doctorW_To_Prescription.PatientGender = PatientGender;
            doctorW_To_Prescription.PatientMedicalEventID = PatientMedicalEventID;
            doctorW_To_Prescription.EventUnitType = "Ward " + WardName;
            doctorW_To_Prescription.WardNumber = WardID;
            #endregion


            /*Console.WriteLine("DoctorID: " + DoctorID);
            Console.WriteLine("DoctorName: " + DoctorName);
            Console.WriteLine("DoctorPosition: " + DoctorTitle);
            Console.WriteLine("PatientRID: " + PatientRID);
            Console.WriteLine("PatientName: " + PatienName);
            Console.WriteLine("PatientAge: " + PatientAge);
            Console.WriteLine("PatientGender: " + PatientGender);
            Console.WriteLine("PatientMedicalEventID: " + PatientMID);
            Console.WriteLine("EventUnitType: Ward " + WardName);
            Console.WriteLine("WardNumber: " + WardID);*/

            
            Common_MakePrescription common_MakePrescription = new Common_MakePrescription(doctorW_To_Prescription);
            common_MakePrescription.DoctorPatientCheckWardFromReferece = this;
            common_MakePrescription.Show();
            this.Hide();

        }

        private void DWPN_Monitor_btn_Click(object sender, EventArgs e)
        {
            DoctorWard_Monitor doctorWard_Monitor = new DoctorWard_Monitor(DoctorName,WardName,PatientRID, PatientMedicalEventID, DoctorID);
            doctorWard_Monitor.DoctorPatientCheckWardFromReferece = this;
            doctorWard_Monitor.Show();
            

        }

        private void DWPN_DischargeBtn_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(MyCommonConnecString.ConnectionString))
                {
                    connect.Open();

                    string query1 = "INSERT INTO Patient_Discharge (Patient_RID, Doctor_ID, Patient_Name, WardNumber) VALUES (@Patient_RID, @Doctor_ID, @Patient_Name, @WardNumber)";
                    using (SqlCommand command = new SqlCommand(query1, connect))
                    {
                        // Add parameters
                        command.Parameters.AddWithValue("@Patient_RID", PatientRID);
                        command.Parameters.AddWithValue("@Doctor_ID", DoctorID);
                        command.Parameters.AddWithValue("@Patient_Name", PatienName);
                        command.Parameters.AddWithValue("@WardNumber", WardID);

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

        private void DoctorWard_ProgressNote_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to Exit?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
                e.Cancel = true; // Cancel the form closing event
            }
            else
            {
                DoctorWard_Dashboard doctorWard_Dashboard = new DoctorWard_Dashboard(DoctorID, WardID);
                doctorWard_Dashboard.Show();
                this.Hide();
            }
        }
    }
}
