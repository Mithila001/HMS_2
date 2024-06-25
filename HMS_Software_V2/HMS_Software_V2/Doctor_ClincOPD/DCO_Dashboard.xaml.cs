using HMS_Software_V2._DataManage_Classes;
using HMS_Software_V2.General_Purpose;
using HMS_Software_V2.UserLogin_Page;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for DCO_Dashboard.xaml
    /// </summary>
    public partial class DCO_Dashboard : Window
    {

        bool IsClinic = false;
        public DCO_Dashboard()
        {
            InitializeComponent();

            MyDisplayBasicData();

            doctorName_lbl.Content = SharedData.doctorData.doctorName;
           

            if (SharedData.doctorData.doctorLocation == "Clinic")
            {
                MyDoctorClinic(); 
                
                

            }
            else if(SharedData.doctorData.doctorLocation == "OPD")
            {
                MyDoctorOPD();
            }
            else
            {
                MessageBox.Show("Error: Doctor Location is not Valid", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
            }

        }

        private void MyDisplayBasicData()
        {
            using (SqlConnection connection = new Database_Connector().GetConnection())
            {
                try
                {
                    connection.Open();

                    #region Get Total OPD Patients Count
                    string query2 = "SELECT COUNT(*) FROM Patient WHERE P_CurrentStatus = 'Out-Patient'";
                    using (SqlCommand command2 = new SqlCommand(query2, connection))
                    {

                        int count = (int)command2.ExecuteScalar();
                        totalOpdPatients_lbl.Content = count.ToString();
                    }

                    #endregion

                    #region Get Total OPD Doctors Count
                    string query3 = "SELECT COUNT(*) FROM Doctor";
                    using (SqlCommand command2 = new SqlCommand(query3, connection))
                    {

                        int count = (int)command2.ExecuteScalar();
                        totalOpdDoctors_lbl.Content = count.ToString();
                    }

                    #endregion

                    #region Get Total Lab Requests Count
                    string query4 = "SELECT COUNT(*) FROM Patient_LabRequest WHERE Is_Completed = 0";
                    using (SqlCommand command2 = new SqlCommand(query4, connection))
                    {

                        int count = (int)command2.ExecuteScalar();
                        totalLabRequests_lbl.Content = count.ToString();
                    }

                    #endregion

                    #region Get Total Prescription Requests Count
                    string query5 = "SELECT COUNT(*) FROM Patient_PrescriptionRequest WHERE PR__IsCompleted = 0";
                    using (SqlCommand command2 = new SqlCommand(query5, connection))
                    {

                        int count = (int)command2.ExecuteScalar();
                        totalPrescriptionRequests_lbl.Content = count.ToString();
                    }

                    #endregion

                    #region Get Total Admit Request Count
                    string query6 = "SELECT COUNT(*) FROM Doc_PatientAdmit_Request WHERE Requested_Date = @Requested_Date";
                    using (SqlCommand command2 = new SqlCommand(query6, connection))
                    {
                        command2.Parameters.AddWithValue("@Requested_Date", DateTime.Today);

                        int count = (int)command2.ExecuteScalar();
                        totalAdmitRequests_lbl.Content = count.ToString();
                    }

                    #endregion




                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                finally
                {
                    connection.Close();
                }


            }

        }

        
        //bool IsOPD = false;
        private void MyDoctorOPD()
        {
            dipartmentName_lbl.Content = "Outpatient Dipartment (OPD)";
        }

        private void MyDoctorClinic()
        {
            dipartmentName_lbl.Content = "Outpatient Dipartment (Clinic)";
            IsClinic = true;
        }



        bool IsConfirmButtonIsClicked = false; //Flag related to Form exit

        // Button Section ------------------------------------------------------------------------------------
        private void confirm_btn_Click(object sender, RoutedEventArgs e)
        {

            string patientRID = patientRID_tbx.Text.ToString();


            #region Validations
            if (General_Purpose.InputValidations.MyIsNullorempty(patientRID))
            {
                MessageBox.Show("Enter Patient RID", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!General_Purpose.InputValidations.MyIsOnlyNumbers(patientRID))
            {
                MessageBox.Show("Enter only numbers", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            } 
            #endregion


            int PatientID = 0;
            bool IsRID_Valid = false;

            using (SqlConnection connection = new Database_Connector().GetConnection())  //to check if the patient RID is correct or not
            {
                string query1 = "SELECT * FROM Patient WHERE P_RegistrationID = @patientRID ";

                SqlCommand cmd = new SqlCommand(query1, connection);

                cmd.Parameters.AddWithValue("@patientRID", "P"+patientRID_tbx.Text);


                try
                {
                    connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PatientID = Convert.ToInt32(reader["Patient_ID"]);

                            string patientStatus = reader["P_CurrentStatus"].ToString() ?? "";

                            if(patientStatus == "In-Patient")
                            {
                                MessageBox.Show("Patient is an In-Patient", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                                return;
                            }

                            IsRID_Valid = true;
                        }
                    }

                    if (!IsRID_Valid)
                    {
                        // No data was read from the database.
                        Debug.WriteLine("No data found for the given patient registration RID.");
                        MessageBox.Show("Incorrect RID", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;

                    }
                    else
                    {

                        if (IsClinic)
                        {
                            bool isGotAnAppointment = false;
                            int patientAppointmentID = 0;

                            string query2 = "SELECT * FROM Patient_AppointmentRequest WHERE PatientID = @PatientID AND IsVisitedByDoctor = 0 ";

                            SqlCommand cmd2 = new SqlCommand(query2, connection);

                            cmd2.Parameters.AddWithValue("@PatientID", PatientID);

                            using (SqlDataReader reader2 = cmd2.ExecuteReader())
                            {
                                while (reader2.Read())
                                {
                                    isGotAnAppointment = true;

                                    patientAppointmentID = Convert.ToInt32(reader2["PatientAppointmentRequest_ID"]);
                                }
                                reader2.Close();
                            }

                            if (isGotAnAppointment)
                            {
                                string query3 = "UPDATE Patient_AppointmentRequest SET IsVisitedByDoctor = 1 WHERE PatientAppointmentRequest_ID = @PatientAppointmentRequest_ID";

                                SqlCommand cmd3 = new SqlCommand(query3, connection);
                                cmd3.Parameters.AddWithValue("@PatientAppointmentRequest_ID", patientAppointmentID);
                                cmd3.ExecuteNonQuery();

                                

                            }
                            else
                            {
                                MessageBox.Show("Patient has not requested an appointment", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                return;
                            }


                        }


                        HMS_Software_V2._DataManage_Classes.SharedData.medicalEvent = new HMS_Software_V2._DataManage_Classes.MedicalEvnent(); // Get a new copy of the medical event template
                        
                        SharedData.medicalEvent.pationetRID = "P" + patientRID_tbx.Text;



                        IsConfirmButtonIsClicked = true; // So When Form is closed, it will not open the UserLogin Page

                        DCO_PatientCheck dCO_PatientCheck = new DCO_PatientCheck();
                        dCO_PatientCheck.Show();
                        this.Close();
                    }


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

        private void reset_btn_Click(object sender, RoutedEventArgs e)
        {
            patientRID_tbx.Text = "";
        }

        private void DCO_Dashboard1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!IsConfirmButtonIsClicked)
            {
                UserLogin userLogin = new UserLogin();
                userLogin.Show();
            }
            
            
        }

    }
}
