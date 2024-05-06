using HMS_Software_V1._01.Common_UseForms;
using HMS_Software_V1._01.Common_UseForms.OOP;
using HMS_Software_V1._01.Doctor_Check;
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


namespace HMS_Software_V1._01.Doctor_OPD
{
    public partial class DoctorCheck_PatientCheck : Form
    {
        public Form DoctorPatientCheckFromReferece {  get; set; }



        SqlConnection connect = new SqlConnection(MyCommonConnecString.ConnectionString);

        private int userID;
        private string patientRID;
        private string doctorPosition;
        private string doctorName;
        private string unitType;
        private int WardNumber;



        



        public DoctorCheck_PatientCheck(string patientID_str, int userID, string doctorPosition, string doctorName, string unittype, int WardNumber)
        {
            InitializeComponent();
            this.unitType = unittype;
            this.patientRID = patientID_str;
            this.userID = userID;
            this.doctorPosition = doctorPosition;
            this.doctorName = doctorName;
            this.WardNumber = WardNumber;

            DOPDPC_doctorName.Text = doctorName;
            DOPDPC_docPosition.Text = doctorPosition;
            DOPDPC_docID.Text = userID.ToString();

            DOPDPC_viewPatientProfiel.Visible = false;
            label1.Visible = false;

            MyGetPatientDetails();
            MyStartPatientMedicalEvent();
         
        }


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
            public bool Isurgetn {  get; set; }
            public int SendWardNumber { get; set; }

        }
        private string PatientMedicalEventID; //Storing PatientMedicalEventID 


        private void MyStartPatientMedicalEvent() // Create PatientMedical_Event record

        {

            // Adding date and time
            DateTime currentDate = DateTime.Today;
            string formattedDate = currentDate.ToString("d MMMM yyyy");

            DateTime currentTime = DateTime.Now;
            string timeString = currentTime.ToString("hh:mm tt");

            DOPDPC_date.Text = formattedDate;
            DOPDPC_time.Text = timeString;

            try
            {
                connect.Open();

                string query = "INSERT INTO PatientMedical_Event (PatientRegistration_ID, Doctor_ID, PMRE_Location, PMRE_Date, PMRE_Time)"
                    + "VALUES (@patietnRegistrationId, @doctorId, @location, @date, @time)";

                using (SqlCommand command = new SqlCommand(query, connect))
                {
                    command.Parameters.AddWithValue("@patietnRegistrationId", patientRID);
                    command.Parameters.AddWithValue("@doctorId", userID);
                    command.Parameters.AddWithValue("@location", unitType); // Warnig: this need to change
                    command.Parameters.AddWithValue("@date", currentDate);
                    command.Parameters.AddWithValue("@time", timeString);
                    // Need to add PatientExaminatioNote


                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        string getIdQuery = "SELECT PatientMedicalEvent_ID FROM PatientMedical_Event WHERE" +
                            " PMRE_Date = @date AND PMRE_Time = @time AND Doctor_ID = @doctorId";

                        using (SqlCommand getIdCommand = new SqlCommand(getIdQuery, connect))
                        {
                            getIdCommand.Parameters.AddWithValue("@date", currentDate);
                            getIdCommand.Parameters.AddWithValue("@time", timeString);
                            getIdCommand.Parameters.AddWithValue("@doctorId", userID);

                            // Executing the query
                            object result = getIdCommand.ExecuteScalar();
                            if (result != null)
                            {
                                PatientMedicalEventID = result.ToString();
                                Console.WriteLine($"PatientMedical_Event Record with ID {PatientMedicalEventID} found successfully.");
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
                        Console.WriteLine("Failed to insert PatientMedical_Event record.");
                        
                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

                Console.WriteLine("Error:  "+ex);
            }
            finally
            {
                connect.Close();
            }


        }

        private void MyGetPatientDetails() // Get Patient Details
        {
            DateTime currentDate = DateTime.Today;
            DateTime currentTime = DateTime.Now;

            DOPDPC_date.Text = currentDate.ToShortDateString();
            DOPDPC_time.Text = currentTime.ToShortTimeString();

            try
            {
                connect.Open();

                string query2 = "SELECT P_NameWithIinitials, P_Age, P_Gender FROM Patient WHERE P_RegistrationID = @patientRID";
                SqlCommand sqlCommand2 = new SqlCommand(query2, connect);
                sqlCommand2.Parameters.AddWithValue("@patientRID", patientRID);
                SqlDataReader reader2 = sqlCommand2.ExecuteReader();
                if (reader2.Read())
                {
                    DOPDPC_patietName_lbl.Text = reader2.GetString(0);
                    DOPDPC_patietage_lbl.Text = reader2.GetString(1);
                    DOPDPC_patietGender_lbl.Text = reader2.GetString(2);

                    MyDataStoringClass transport = new MyDataStoringClass();
                    transport.PatientName = reader2.GetString(0);
                    transport.PatientAge = reader2.GetString(1);
                    transport.PatientGender = reader2.GetString(2);

                    


                }
                else
                {
                    // Handle case when no matching record is found
                    MessageBox.Show("Patien Registration ID not match!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

                Console.WriteLine("DCPC;; " + ex);
            }
            finally
            {
                connect.Close();
            }

        }


        private ForCommonLabRequests doctorDataSendToLabRequest;

        // Move To Lab Request form
        private void DOPDPC_addLabRequest_Click(object sender, EventArgs e)
        {
            /*MyDataStoringClass dataTranspoter = new MyDataStoringClass();
            MyDataStoringClass dataTranspoter2 = new MyDataStoringClass();

            dataTranspoter.DoctorID = userID;
            dataTranspoter.DoctorName = doctorName;
            dataTranspoter.DoctorPosition = doctorPosition;
            dataTranspoter.PatientRID = patientRID;
        
            dataTranspoter.PatientName = DOPDPC_patietName_lbl.Text;
            dataTranspoter.PatientAge = DOPDPC_patietage_lbl.Text;
            dataTranspoter.PatientGender = DOPDPC_patietGender_lbl.Text;
            dataTranspoter.PatientMedicalEventID = PatientMedicalEventID;
            dataTranspoter.EventUnitType = unitType;


            Console.WriteLine($"DoctorID: {dataTranspoter.DoctorID}");
            Console.WriteLine($"DoctorName: {dataTranspoter.DoctorName}");
            Console.WriteLine($"DoctorPosition: {dataTranspoter.DoctorPosition}");
            Console.WriteLine($"PatientRID: {dataTranspoter.PatientRID}");
            Console.WriteLine($"PatientName: {dataTranspoter.PatientName}");
            Console.WriteLine($"PatientAge: {dataTranspoter.PatientAge}");
            Console.WriteLine($"PatientGender: {dataTranspoter.PatientGender}");
            Console.WriteLine($"PatientMedicalEventID: {dataTranspoter.PatientMedicalEventID}");*/


            Common_UseForms.OOP.Doctor_Check DC_labRequestData = new Common_UseForms.OOP.Doctor_Check();
            DC_labRequestData.DoctorID = userID;
            DC_labRequestData.DoctorName = doctorName;
            DC_labRequestData.DoctorPosition = doctorPosition;
            DC_labRequestData.PatientRID = patientRID;
            DC_labRequestData.PatientName = DOPDPC_patietName_lbl.Text;
            DC_labRequestData.PatientAge = DOPDPC_patietage_lbl.Text;
            DC_labRequestData.PatientGender = DOPDPC_patietGender_lbl.Text;
            DC_labRequestData.PatientMedicalEventID = PatientMedicalEventID;
            DC_labRequestData.EventUnitType = unitType;
            DC_labRequestData.WardNumber = WardNumber;



            Common_MakeLabRequest common_MakeLabRequest = new Common_MakeLabRequest(DC_labRequestData);
            common_MakeLabRequest.DoctorPatientCheckFromReferece = this; //crete a referece for this form
            common_MakeLabRequest.Show();
            this.Hide();
        }

        // Move To Lab Prescription form
        private void DOPDPC_addPrescription_Click(object sender, EventArgs e)
        {
            /*MyDataStoringClass dataTranspoter = new MyDataStoringClass();*/

            /*dataTranspoter.DoctorID = userID;
            dataTranspoter.DoctorName = doctorName;
            dataTranspoter.DoctorPosition = doctorPosition;
            dataTranspoter.PatientRID = patientRID;
            dataTranspoter.PatientMedicalEventID = PatientMedicalEventID;
            dataTranspoter.PatientName = DOPDPC_patietName_lbl.Text;
            dataTranspoter.PatientAge = DOPDPC_patietage_lbl.Text;
            dataTranspoter.PatientGender = DOPDPC_patietGender_lbl.Text;;
            dataTranspoter.EventUnitType = unitType;*/

            Common_UseForms.OOP.Doctor_Check DC_addPrescription = new Common_UseForms.OOP.Doctor_Check();

            DC_addPrescription.DoctorID = userID;
            DC_addPrescription.DoctorName = doctorName;
            DC_addPrescription.DoctorPosition = doctorPosition;
            DC_addPrescription.PatientRID = patientRID;
            DC_addPrescription.PatientMedicalEventID = PatientMedicalEventID;
            DC_addPrescription.PatientName = DOPDPC_patietName_lbl.Text;
            DC_addPrescription.PatientAge = DOPDPC_patietage_lbl.Text;
            DC_addPrescription.PatientGender = DOPDPC_patietGender_lbl.Text; ;
            DC_addPrescription.EventUnitType = unitType;
            DC_addPrescription.WardNumber = WardNumber;


            Common_MakePrescription common_MakePrescription = new Common_MakePrescription(DC_addPrescription);
            common_MakePrescription.DoctorPatientCheckFromReferece = this; //crete a referece for this form
            common_MakePrescription.Show();
            this.Hide();

           /* Console.WriteLine($"PatientMedicalEventID from PatientCkeck from: {dataTranspoter.PatientMedicalEventID}");*/
        }


        // Move To Lab Appointment form
        private void DOPDPC_addAppointment_Click(object sender, EventArgs e)
        {
            MyDataStoringClass dataTranspoter = new MyDataStoringClass();

            dataTranspoter.DoctorID = userID;
            dataTranspoter.DoctorName = doctorName;
            dataTranspoter.DoctorPosition = doctorPosition;
            dataTranspoter.PatientRID = patientRID;
            dataTranspoter.PatientMedicalEventID = PatientMedicalEventID;
            dataTranspoter.PatientName = DOPDPC_patietName_lbl.Text;
            dataTranspoter.PatientAge = DOPDPC_patietage_lbl.Text;
            dataTranspoter.PatientGender = DOPDPC_patietGender_lbl.Text;
            dataTranspoter.EventUnitType = unitType;
            

            DoctorCheck_AddClinic doctorCheck_AddClinic = new DoctorCheck_AddClinic(dataTranspoter);
            doctorCheck_AddClinic.DoctorCkeckFromReferece = this; //crete a referece for this form
            doctorCheck_AddClinic.Show();
            this.Hide();

            Console.WriteLine($"PatientMedicalEventID from PatientCkeck from: {dataTranspoter.PatientMedicalEventID}");

        }

        private void DOPDPC_confirmRequests_Click(object sender, EventArgs e)
        {
            try
            {
                connect.Open();
                // Using PatientMedicalEventID to find the lab record that now created and get the current LabRequest_ID
                string query2 = "UPDATE PatientMedical_Event SET PatientExaminatioNote = @examinationNotes WHERE PatientMedicalEvent_ID = @pmeID";
                using (SqlCommand updateCommand = new SqlCommand(query2, connect))
                {
                    updateCommand.Parameters.AddWithValue("@examinationNotes", P_MedicalRecors_richTbx.Text);
                    updateCommand.Parameters.AddWithValue("@pmeID", PatientMedicalEventID);

                    int rowsAffected = updateCommand.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("PatientExaminatioNote updated successfully.");
                        MessageBox.Show("PatientExaminatioNote updated successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);


                        // Moving to the Doctor Dashboard

                        MyDataStoringClass dataTranspoter = new MyDataStoringClass();
                        dataTranspoter.DoctorID = userID;
                        dataTranspoter.EventUnitType = unitType;
                        dataTranspoter.SendWardNumber = WardNumber;

                        DoctorCheck_Dashboard doctorCheck_Dashboard = new DoctorCheck_Dashboard(dataTranspoter.DoctorID, dataTranspoter.EventUnitType, dataTranspoter.SendWardNumber);
                        doctorCheck_Dashboard.Show();

                        this.Close();
                    }
                    else
                    {
                        Console.WriteLine("Failed to update PatientExaminatioNote.");
                        MessageBox.Show("Failed to update PatientExaminatioNote", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void DOPDPC_admit_Click(object sender, EventArgs e)
        {


            MyDataStoringClass dataTranspoter = new MyDataStoringClass();

            dataTranspoter.Isurgetn = urgent_checkBox.Checked;
            dataTranspoter.DoctorID = userID;
            dataTranspoter.DoctorName = doctorName;
            dataTranspoter.DoctorPosition = doctorPosition;
            dataTranspoter.PatientRID = patientRID;
            dataTranspoter.PatientMedicalEventID = PatientMedicalEventID;
            dataTranspoter.PatientName = DOPDPC_patietName_lbl.Text;
            dataTranspoter.PatientAge = DOPDPC_patietage_lbl.Text;
            dataTranspoter.PatientGender = DOPDPC_patietGender_lbl.Text; ;
            dataTranspoter.EventUnitType = unitType;


            Admit_ReferralNote admit_ReferralNote = new Admit_ReferralNote(dataTranspoter);
           
            admit_ReferralNote.ShowDialog();
            this.Close();
        }
    }
}
