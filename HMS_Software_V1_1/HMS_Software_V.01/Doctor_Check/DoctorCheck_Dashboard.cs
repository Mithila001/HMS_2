using HMS_Software_V1._01.Common_UseForms;
using HMS_Software_V1._01.Doctor_Check;
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

namespace HMS_Software_V1._01.Doctor_OPD
{
    public partial class DoctorCheck_Dashboard : Form
    {

        #region Connection string
        SqlConnection connect = new SqlConnection(MyCommonConnecString.ConnectionString);
        #endregion

        #region Constructor with variables

        private int DoctorID;
        private string unitTypeName;
        private int WardNumber;

        public DoctorCheck_Dashboard(int userID = 10, string unit = "OPD", int WardNumber = 2)
        {//need to remove the default values like 10,opd,2
            InitializeComponent();

            this.DoctorID = userID;
            this.unitTypeName = unit;
            this.WardNumber = WardNumber;

            UiCountDetails();
            GetDoctorDetails(); // Get Dashboard Data
        }
        #endregion

        #region Counting Details of dashboard

        int TotalPatient_Count;
        int Today_Clinic_Doctors;
        int Lab_Request_Count;
        int Prescription_request_Count;
        int Inpatient_Request_Count;
        private void UiCountDetails()
        {
            
            #region Date/Time
            DateTime currentDate = DateTime.Today;
            string formattedDate = currentDate.ToString("dd-MM-yyyy");

            DateTime currentTime = DateTime.Now;
            string formattedTime = currentTime.ToString("h.mm tt");

            DOPD_date.Text = formattedDate;
            DOPD_time.Text = formattedTime;
            #endregion

            #region Countings in Dashboard
            try
            {
                using (SqlConnection connect = new SqlConnection(MyCommonConnecString.ConnectionString))
                {
                    connect.Open();

                    string query1 = "SELECT COUNT(*) FROM Patient WHERE CONVERT(date, P_RegisteredDate) = @TodayDate";
                    using (SqlCommand command = new SqlCommand(query1, connect))
                    {
                        command.Parameters.AddWithValue("@TodayDate", DateTime.Today);
                        TotalPatient_Count = (int)command.ExecuteScalar();
                    }

                    string query2 = "SELECT COUNT(*) FROM ClinicEvents WHERE CONVERT(date, CE_Date) = @TodayDate";
                    using (SqlCommand command = new SqlCommand(query2, connect))
                    {
                        command.Parameters.AddWithValue("@TodayDate", DateTime.Today);
                        Today_Clinic_Doctors = (int)command.ExecuteScalar();// this doesnt give clinic doctor count instead available clinic events
                    }

                    string query3 = "SELECT COUNT(*) FROM Lab_Request WHERE Doctor_ID = @Visited_Doctor_ID";
                    using (SqlCommand command = new SqlCommand(query3, connect))
                    {
                        command.Parameters.AddWithValue("@Visited_Doctor_ID", DoctorID);
                        Lab_Request_Count = (int)command.ExecuteScalar();
                    }

                    string query4 = "SELECT COUNT(*) FROM PatientMedical_Event WHERE ISNULL(PrescriptionRequest_ID, '') = '' AND Doctor_ID = @Visited_Doctor_ID";
                    using (SqlCommand command = new SqlCommand(query4, connect))
                    {
                        command.Parameters.AddWithValue("@Visited_Doctor_ID", DoctorID);
                        Prescription_request_Count = (int)command.ExecuteScalar();//changed it so only get the count of the prescription request with loged in doctor
                    }

                    string query5 = "SELECT COUNT(*) FROM Patient_Admit WHERE Is_Admitted = @Is_Admitted";
                    using (SqlCommand command = new SqlCommand(query5, connect))
                    {
                        command.Parameters.AddWithValue("@Is_Admitted", 0);
                        Inpatient_Request_Count = (int)command.ExecuteScalar();
                    }
                }

                DashB_TotalPatientsToday.Text = TotalPatient_Count.ToString();
                DashB_ClinicDoctorsToday.Text = Today_Clinic_Doctors.ToString();
                DashB_LabRequestCount.Text = Lab_Request_Count.ToString();
                DashB_PrescriptionRequestCount.Text = Prescription_request_Count.ToString();
                DashB_InpatientRequestCount.Text = Inpatient_Request_Count.ToString();
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
            #endregion
        }
        #endregion

        #region Get Doctor Details
        private void GetDoctorDetails()
        {
            try
            {
                connect.Open();

                string query = "SELECT D_NameWithInitials, D_Position, Doctor_ID FROM Doctor WHERE Doctor_ID = @UserID";

                SqlCommand sqlCommand = new SqlCommand(query, connect);
                sqlCommand.Parameters.AddWithValue("@UserID", DoctorID);
                SqlDataReader reader = sqlCommand.ExecuteReader();


                if (reader.Read())
                {
                    DCD_doctorName_lbl.Text = reader.GetString(0);
                    DCD_doctor_position_lbl.Text = reader.GetString(1);
                }
                else
                {
                    //when no matching record is found
                    MessageBox.Show("Doctor not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connect.Close();
            }


        }
        #endregion

        #region PatientCheck_btn
        private void DCD_confrim_btn_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(DCD_enterPatientID_tbx.Text))
            {
                try
                {
                    connect.Open();

                    string query = "SELECT P_RegistrationID FROM Patient WHERE P_RegistrationID = @PatientID";

                    using (SqlCommand sqlCommand = new SqlCommand(query, connect))
                    {
                        sqlCommand.Parameters.AddWithValue("@PatientID", "P" + DCD_enterPatientID_tbx.Text);

                        using (SqlDataReader reader = sqlCommand.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string patientID_str = reader.GetString(0);

                                string inputIDstr = "P" + DCD_enterPatientID_tbx.Text;

                                string doctorPosition = DCD_doctor_position_lbl.Text;
                                string doctorName = DCD_doctorName_lbl.Text;

                                if (patientID_str == inputIDstr)
                                {
                                    // Go to a form
                                    DoctorCheck_PatientCheck doctorCheck_PatientCheck = new DoctorCheck_PatientCheck(patientID_str, DoctorID, doctorPosition, doctorName, unitTypeName, WardNumber);
                                    doctorCheck_PatientCheck.Show();
                                    this.Hide();
                                }
                                else
                                {
                                    MessageBox.Show("Invalid Patient Number", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Database connection error", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
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
                MessageBox.Show("Add a Patient number", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        #endregion

        #region When Dashboard Closed
        private void DoctorCheck_Dashboard_FormClosed(object sender, FormClosedEventArgs e)
        {
            UserLogin userLogin = new UserLogin();
            userLogin.Show();
            this.Hide();
        }
        #endregion

        #region Reset_btn
        private void DCD_reset_btn_Click(object sender, EventArgs e)
        {
            DCD_enterPatientID_tbx.Text = "";
        } 
        #endregion
    }
}
