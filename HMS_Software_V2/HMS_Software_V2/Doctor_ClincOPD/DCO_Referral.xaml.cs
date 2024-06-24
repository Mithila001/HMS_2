using HMS_Software_V2._DataManage_Classes;
using HMS_Software_V2.General_Purpose;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HMS_Software_V2.Doctor_ClincOPD
{
    /// <summary>
    /// Interaction logic for DCO_Referral.xaml
    /// </summary>
    public partial class DCO_Referral : Window
    {
        DCO_PatientCheck parentForm;
        public DCO_Referral(DCO_PatientCheck dCO_PatientCheck)
        {
            InitializeComponent();
            this.parentForm = dCO_PatientCheck;
        }

        private void Confirm_btn_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(refferalNote_tbx.Text))
            {
                MessageBox.Show("Please enter a referral note");
                return;
            }


            int medicalEventID = 0;

            using (SqlConnection connection = new Database_Connector().GetConnection())
            {
                connection.Open();

                try
                {
                    DateTime dateRaw = new DateTime(SharedData.medicalEvent.Date.Year, SharedData.medicalEvent.Date.Month, SharedData.medicalEvent.Date.Day);
                    string date = dateRaw.ToString("yyyy-MM-dd");
                    TimeSpan timeSpan = SharedData.medicalEvent.Time.ToTimeSpan();
                    string time = timeSpan.ToString(@"hh\:mm");


                    #region Insert Data To Medical Event Table 
                    string query = "INSERT INTO PatientMedical_Event (Patient_ID, PME_Doctor_ID, PME_Nurse_ID, PME_Date, PME_Time, PME_Location, PME_Is_LabRequest," +
                                    " PME_Is_PrescriptionRequest, PME_Is_PatientAppointment, PME_PatientExaminationNote, PME_PatietnMedicalCondition, PME_Is_InPatient) "
                                    + "VALUES (@Patient_ID, @PME_Doctor_ID, @PME_Nurse_ID, @PME_Date, @PME_Time, @PME_Location, @PME_Is_LabRequest, @PME_Is_PrescriptionRequest, @PME_Is_PatientAppointment," +
                                      " @PME_PatientExaminationNote, @PME_PatietnMedicalCondition, @PME_Is_InPatient); SELECT SCOPE_IDENTITY();";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {


                        #region Debug Section
                        Debug.WriteLine("\n ------ Inserting the following values into PatientMedical_Event ------");
                        Debug.WriteLine("Patient_ID: " + SharedData.medicalEvent.PatientID);
                        Debug.WriteLine("PME_Doctor_ID: " + SharedData.medicalEvent.DoctorID);
                        Debug.WriteLine("PME_Nurse_ID: " + 0);
                        Debug.WriteLine("PME_Date: " + date);
                        Debug.WriteLine("PME_Time: " + time);
                        Debug.WriteLine("PME_Location: " + SharedData.medicalEvent.Location);
                        Debug.WriteLine("PME_Is_LabRequest: " + SharedData.medicalEvent.IsLabRequest);
                        Debug.WriteLine("PME_Is_PrescriptionRequest: " + SharedData.medicalEvent.IsPrescriptionRequest);
                        Debug.WriteLine("PME_Is_PatientAppointment: " + SharedData.medicalEvent.IsAppointmentRequest);
                        Debug.WriteLine("PME_PatientExaminationNote: " + SharedData.medicalEvent.PersonExaminationNote);
                        Debug.WriteLine("PME_PatietnMedicalCondition: " + SharedData.medicalEvent.PatientMedicalCondition);
                        Debug.WriteLine("PME_Is_InPatient: " + SharedData.medicalEvent.IsInpationt); 
                        #endregion



                        cmd.Parameters.AddWithValue("@Patient_ID", SharedData.medicalEvent.PatientID);
                        cmd.Parameters.AddWithValue("@PME_Doctor_ID", SharedData.medicalEvent.DoctorID);
                        cmd.Parameters.AddWithValue("@PME_Nurse_ID", 0);


                        cmd.Parameters.AddWithValue("@PME_Date", date);
                        cmd.Parameters.AddWithValue("@PME_Time", time);
                        cmd.Parameters.AddWithValue("@PME_Location", SharedData.medicalEvent.Location);
                        cmd.Parameters.AddWithValue("@PME_Is_LabRequest", SharedData.medicalEvent.IsLabRequest);
                        cmd.Parameters.AddWithValue("@PME_Is_PrescriptionRequest", SharedData.medicalEvent.IsPrescriptionRequest);
                        cmd.Parameters.AddWithValue("@PME_Is_PatientAppointment", SharedData.medicalEvent.IsAppointmentRequest);
                        cmd.Parameters.AddWithValue("@PME_PatientExaminationNote", SharedData.medicalEvent.PersonExaminationNote);
                        cmd.Parameters.AddWithValue("@PME_PatietnMedicalCondition", SharedData.medicalEvent.PatientMedicalCondition);
                        cmd.Parameters.AddWithValue("@PME_Is_InPatient", SharedData.medicalEvent.IsInpationt);

                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            medicalEventID = Convert.ToInt32(result);
                            Debug.WriteLine("\nInserted Medical Event ID: " + medicalEventID);

                        }
                        else
                        {
                            MessageBox.Show("Error: Medical Event ID is not generated", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                    }
                    #endregion



                    #region Insert Data To Doc_PatientAdmit_Request Table 
                    string query2 = "INSERT INTO Doc_PatientAdmit_Request (PatientID, Doctor_ID, P_ReferralNote, Requested_Time, Requested_Date, Is_Urgent, SendFrom_Location) "
                                    + "VALUES (@PatientID, @Doctor_ID, @P_ReferralNote, @Requested_Time, @Requested_Date, @Is_Urgent, @SendFrom_Location);";

                    using (SqlCommand cmd = new SqlCommand(query2, connection))
                    {

                        cmd.Parameters.AddWithValue("@PatientID", SharedData.medicalEvent.PatientID);
                        cmd.Parameters.AddWithValue("@Doctor_ID", SharedData.medicalEvent.DoctorID);
                        cmd.Parameters.AddWithValue("@P_ReferralNote", refferalNote_tbx.Text);
                        cmd.Parameters.AddWithValue("@Requested_Time", time);
                        cmd.Parameters.AddWithValue("@Requested_Date", date);
                        cmd.Parameters.AddWithValue("@Is_Urgent", SharedData.medicalEvent.IsAdmitUrgent);
                        cmd.Parameters.AddWithValue("@SendFrom_Location", SharedData.doctorData.doctorLocation);
                        

                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            
                            Debug.WriteLine("\nInserted Data To Doc_PatientAdmit_Request Table");

                        }
                        else
                        {
                            MessageBox.Show("Error: Medical Event ID is not generated", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                    }
                    #endregion



                }
                catch (Exception ex)
                {
                    Debug.WriteLine("\nError1: \n" + ex.Message);
                    MessageBox.Show("Error1: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    connection.Close();
                }
            }

        }
    }
}
