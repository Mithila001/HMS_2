using HMS_Software_V._01.Doctor_Check;
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
        public Form DoctorPatientCheckFromReferece { get; set; }

        #region Connection string
        SqlConnection connect = new SqlConnection(MyCommonConnecString.ConnectionString);
        #endregion

        #region Constructor With Variables

        private int UserID;
        private string PatientRID;
        private string DoctorPosition;
        private string DoctorName;
        private string UnitType;
        private int WardNumber;
        bool startEvent = false;
        int i = 0;

        public DoctorCheck_PatientCheck(string patientID_str, int userID, string doctorPosition, string doctorName, string unittype, int WardNumber)
        {
            InitializeComponent();
            this.UnitType = unittype;
            this.PatientRID = patientID_str;
            this.UserID = userID;
            this.DoctorPosition = doctorPosition;
            this.DoctorName = doctorName;
            this.WardNumber = WardNumber;

            DOPDPC_doctorName.Text = doctorName;
            DOPDPC_docPosition.Text = doctorPosition;

            DOPDPC_viewPatientProfile.Visible = true;
            label1.Visible = true;

            TopPanelDetails();

        }
        #endregion

        #region Data Transporter Class
        public class MyDataStoringClass
        {
            public int DoctorID { get; set; }
            public string DoctorName { get; set; }
            public string DoctorPosition { get; set; }
            public string PatientRID { get; set; }
            public string PatientName { get; set; }
            public string PatientAge { get; set; }
            public string PatientGender { get; set; }
            public int PatientMedicalEventID { get; set; }
            public string EventUnitType { get; set; }
            public bool Isurgetn { get; set; }
            public int SendWardNumber { get; set; }

        }
        #endregion

        #region MedicalEvent

        private int PatientMedicalEventID2;

        private void MyStartPatientMedicalEvent()

        {

            // Adding date and time
            DateTime currentDate = DateTime.Today;
            string formattedDate = currentDate.ToString("dd MM yyyy");

            DateTime currentTime = DateTime.Now;
            string timeString = currentTime.ToString("hh:mm tt");


            try
            {
                connect.Open();

                string query = "INSERT INTO PatientMedical_Event (PatientRegistration_ID, Doctor_ID, PMRE_Location, PMRE_Date, PMRE_Time)"
                    + "VALUES (@patietnRegistrationId, @doctorId, @location, @date, @time)";

                using (SqlCommand command = new SqlCommand(query, connect))
                {
                    command.Parameters.AddWithValue("@patietnRegistrationId", PatientRID);
                    command.Parameters.AddWithValue("@doctorId", UserID);
                    command.Parameters.AddWithValue("@location", UnitType); // Warnig: this need to change
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
                            getIdCommand.Parameters.AddWithValue("@doctorId", UserID);

                            // Executing the query
                            object result = getIdCommand.ExecuteScalar();
                            if (result != null)
                            {
                                PatientMedicalEventID2 = Convert.ToInt32(result);

                                Console.WriteLine($"PatientMedical_Event Record with ID {PatientMedicalEventID2} found successfully.");
                            }
                            else
                            {
                                Console.WriteLine("PatientMedical_Event Record not found for the given criteria.");
                            }
                        }

                        Console.WriteLine($"PatientMedical_Event Record with ID {PatientMedicalEventID2} inserted successfully.");


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

                Console.WriteLine("Error:  " + ex);
            }
            finally
            {
                connect.Close();
            }


        }
        #endregion

        #region Top Panel Details
        private void TopPanelDetails()
        {
            #region Date/Time

            DateTime currentDate = DateTime.Today;
            string Date = currentDate.ToString("dd-MM-yyyy");

            DateTime currentTime = DateTime.Now;
            string Time = currentTime.ToString("hh:mm tt");

            DOPDPC_date.Text = Date;
            DOPDPC_time.Text = Time;
            #endregion

            try
            {
                connect.Open();

                string query2 = "SELECT P_NameWithIinitials, P_Age, P_Gender FROM Patient WHERE P_RegistrationID = @patientRID";
                SqlCommand sqlCommand2 = new SqlCommand(query2, connect);
                sqlCommand2.Parameters.AddWithValue("@patientRID", PatientRID);
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
                    //when no matching record is found
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
        #endregion

        #region Start MedicalEvent
        private void Button_Click(object sender, EventArgs e)
        {
            startEvent = true;


            while (i < 1)
            {
                if (sender == DOPDPC_addLabRequest)
                {
                    MyStartPatientMedicalEvent();
                    i++;
                    Console.WriteLine(startEvent);
                }
                else if (sender == DOPDPC_addPrescription)
                {
                    MyStartPatientMedicalEvent();
                    i++;
                    Console.WriteLine(startEvent);
                }
                else if (sender == DOPDPC_addAppointment)
                {
                    MyStartPatientMedicalEvent();
                    i++;
                    Console.WriteLine(startEvent);
                }
                else if (sender == DOPDPC_confirmRequests)
                {
                    MyStartPatientMedicalEvent();
                    i++;
                    Console.WriteLine(startEvent);
                }
                else if (sender == DOPDPC_admit)
                {
                    MyStartPatientMedicalEvent();
                    i++;
                    Console.WriteLine(startEvent);
                }
            }
            Console.WriteLine("Went throug Button_Click. i = "+i);
        }
        #endregion

        #region AddLabRequest
        private void DOPDPC_addLabRequest_Click(object sender, EventArgs e)
        {


            Common_UseForms.OOP.Doctor_Check DC_labRequestData = new Common_UseForms.OOP.Doctor_Check();
            DC_labRequestData.DoctorID = UserID;
            DC_labRequestData.DoctorName = DoctorName;
            DC_labRequestData.DoctorPosition = DoctorPosition;
            DC_labRequestData.PatientRID = PatientRID;
            DC_labRequestData.PatientName = DOPDPC_patietName_lbl.Text;
            DC_labRequestData.PatientAge = DOPDPC_patietage_lbl.Text;
            DC_labRequestData.PatientGender = DOPDPC_patietGender_lbl.Text;
            DC_labRequestData.PatientMedicalEventID = PatientMedicalEventID2;
            DC_labRequestData.EventUnitType = UnitType;
            DC_labRequestData.WardNumber = WardNumber;



            Common_MakeLabRequest common_MakeLabRequest = new Common_MakeLabRequest(DC_labRequestData);
            common_MakeLabRequest.DoctorPatientCheckFromReferece = this; //crete a referece for this form
            common_MakeLabRequest.Show();
            this.Hide();
        }
        #endregion

        #region AddPrescription
        private void DOPDPC_addPrescription_Click(object sender, EventArgs e)
        {


            Common_UseForms.OOP.Doctor_Check DC_addPrescription = new Common_UseForms.OOP.Doctor_Check();

            DC_addPrescription.DoctorID = UserID;
            DC_addPrescription.DoctorName = DoctorName;
            DC_addPrescription.DoctorPosition = DoctorPosition;
            DC_addPrescription.PatientRID = PatientRID;
            DC_addPrescription.PatientMedicalEventID = PatientMedicalEventID2;
            DC_addPrescription.PatientName = DOPDPC_patietName_lbl.Text;
            DC_addPrescription.PatientAge = DOPDPC_patietage_lbl.Text;
            DC_addPrescription.PatientGender = DOPDPC_patietGender_lbl.Text; ;
            DC_addPrescription.EventUnitType = UnitType;
            DC_addPrescription.WardNumber = WardNumber;

            Console.WriteLine("111111111 PatientMedicalEventID: " + PatientMedicalEventID2);
            Common_MakePrescription common_MakePrescription = new Common_MakePrescription(DC_addPrescription);
            common_MakePrescription.DoctorPatientCheckFromReferece = this; //crete a referece for this form
            common_MakePrescription.Show();
            this.Hide();
        }
        #endregion

        #region AddClinic Form Not Corrected yet
        private void DOPDPC_addAppointment_Click(object sender, EventArgs e)
        {

            MyDataStoringClass dataTranspoter = new MyDataStoringClass();

            dataTranspoter.DoctorID = UserID;
            dataTranspoter.DoctorName = DoctorName;
            dataTranspoter.DoctorPosition = DoctorPosition;
            dataTranspoter.PatientRID = PatientRID;
            dataTranspoter.PatientMedicalEventID = PatientMedicalEventID2;
            dataTranspoter.PatientName = DOPDPC_patietName_lbl.Text;
            dataTranspoter.PatientAge = DOPDPC_patietage_lbl.Text;
            dataTranspoter.PatientGender = DOPDPC_patietGender_lbl.Text;
            dataTranspoter.EventUnitType = UnitType;


            DoctorCheck_AddClinic doctorCheck_AddClinic = new DoctorCheck_AddClinic(dataTranspoter);
            doctorCheck_AddClinic.DoctorCkeckFromReferece = this; //crete a referece for this form
            doctorCheck_AddClinic.Show();
            this.Hide();

            Console.WriteLine($"PatientMedicalEventID from PatientCkeck from: {dataTranspoter.PatientMedicalEventID}");

        }
        #endregion

        #region ConfirmRquest_btn
        private void DOPDPC_confirmRequests_Click(object sender, EventArgs e)
        {

            if (P_MedicalRecors_richTbx.Text != "")
            {
                try
                {
                    connect.Open();
                    // Using PatientMedicalEventID to find the lab record that now created and get the current LabRequest_ID
                    string query2 = "UPDATE PatientMedical_Event SET PatientExaminatioNote = @examinationNotes WHERE PatientMedicalEvent_ID = @pmeID";
                    using (SqlCommand updateCommand = new SqlCommand(query2, connect))
                    {
                        updateCommand.Parameters.AddWithValue("@examinationNotes", P_MedicalRecors_richTbx.Text);
                        updateCommand.Parameters.AddWithValue("@pmeID", PatientMedicalEventID2);

                        int rowsAffected = updateCommand.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("PatientExaminatioNote updated successfully.");
                            MessageBox.Show("PatientExaminatioNote updated successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);


                            // Moving to the Doctor Dashboard

                            MyDataStoringClass dataTranspoter = new MyDataStoringClass();
                            dataTranspoter.DoctorID = UserID;
                            dataTranspoter.EventUnitType = UnitType;
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
            else
            {
                MessageBox.Show("PatientExaminatioNote is Empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        #endregion

        #region Admit_btn

        private void DOPDPC_admit_Click(object sender, EventArgs e)
        {
            if (P_MedicalRecors_richTbx.Text != "")
            {
                MyDataStoringClass dataTranspoter = new MyDataStoringClass();

                dataTranspoter.Isurgetn = urgent_checkBox.Checked;
                dataTranspoter.DoctorID = UserID;
                dataTranspoter.DoctorName = DoctorName;
                dataTranspoter.DoctorPosition = DoctorPosition;
                dataTranspoter.PatientRID = PatientRID;
                dataTranspoter.PatientMedicalEventID = PatientMedicalEventID2;
                dataTranspoter.PatientName = DOPDPC_patietName_lbl.Text;
                dataTranspoter.PatientAge = DOPDPC_patietage_lbl.Text;
                dataTranspoter.PatientGender = DOPDPC_patietGender_lbl.Text; ;
                dataTranspoter.EventUnitType = UnitType;


                Admit_ReferralNote admit_ReferralNote = new Admit_ReferralNote(dataTranspoter);
                admit_ReferralNote.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("PatientExaminatioNote is Empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            
        }
        #endregion

        private void DOPDPC_viewPatientProfile_Click_1(object sender, EventArgs e)
        {
            if (startEvent == false)
            {
                DoctorCheck_ViewHistory doctorCheck_PatientHistory = new DoctorCheck_ViewHistory(PatientRID);
                doctorCheck_PatientHistory.ShowDialog();
            }

        }
    }
}
