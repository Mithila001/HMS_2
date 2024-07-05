using HMS_Software_V2._DataManage_Classes;
using HMS_Software_V2.General_Purpose;
using HMS_Software_V2.General_Purpose.General_UserControls;
using HMS_Software_V2.UserLogin_Page;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
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


            #region Check If this is CLinic or OPD type
            if (SharedData.doctorData.doctorLocation == "Clinic")
            {
                MyDoctorClinic();

                SharedData.doctorData.doctorLocation = "Clinic";



            }
            else if (SharedData.doctorData.doctorLocation == "OPD")
            {
                MyDoctorOPD();
                SharedData.doctorData.doctorLocation = "OPD";
            }
            else
            {
                MessageBox.Show("Error: Doctor Location is not Valid", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
            } 
            #endregion

        }
        private void MyDisplayBasicData()
        {

            #region Get and Assign Date Time
            int day = DateTime.Now.Day;
            string daySuffix = day switch
            {
                1 or 21 or 31 => "st",
                2 or 22 => "nd",
                3 or 23 => "rd",
                _ => "th"
            };

            todayDate_lbl.Content = $"{day}{daySuffix} {DateTime.Now:MMMM yyyy}";

            today_Time_lbl.Content = DateTime.Now.ToString("hh:mm tt");

            todayDateName_lbl.Content = DateTime.Now.DayOfWeek.ToString();
            #endregion



            using (SQLiteConnection connection = new Database_Connector().GetConnection())
            {
                try
                {
                    connection.Open();

                    #region Get Total OPD Patients Count
                    string query2 = "SELECT COUNT(*) FROM Patient WHERE P_CurrentStatus = 'Out-Patient'";
                    using (SQLiteCommand command2 = new SQLiteCommand(query2, connection))
                    {

                        int count = Convert.ToInt32(command2.ExecuteScalar());
                        totalOpdPatients_lbl.Content = count.ToString();
                    }

                    #endregion

                    #region Get Total OPD Doctors Count
                    string query3 = "SELECT COUNT(*) FROM Doctor";
                    using (SQLiteCommand command2 = new SQLiteCommand(query3, connection))
                    {

                        int count = Convert.ToInt32(command2.ExecuteScalar());
                        totalOpdDoctors_lbl.Content = count.ToString();
                    }

                    #endregion

                    #region Get Total Lab Requests Count
                    string query4 = "SELECT COUNT(*) FROM Patient_LabRequest WHERE Is_Completed = 0";
                    using (SQLiteCommand command2 = new SQLiteCommand(query4, connection))
                    {

                        int count = Convert.ToInt32(command2.ExecuteScalar());
                        totalLabRequests_lbl.Content = count.ToString();
                    }

                    #endregion

                    #region Get Total Prescription Requests Count
                    string query5 = "SELECT COUNT(*) FROM Patient_PrescriptionRequest WHERE PR__IsCompleted = 0";
                    using (SQLiteCommand command2 = new SQLiteCommand(query5, connection))
                    {

                        int count = Convert.ToInt32(command2.ExecuteScalar());
                        totalPrescriptionRequests_lbl.Content = count.ToString();
                    }

                    #endregion

                    #region Get Total Admit Request Count
                    string query6 = "SELECT COUNT(*) FROM Doc_PatientAdmit_Request WHERE Requested_Date = @Requested_Date";
                    using (SQLiteCommand command2 = new SQLiteCommand(query6, connection))
                    {
                        string formattedDate = DateTime.Today.ToString("yyyy-MM-dd");
                        command2.Parameters.AddWithValue("@Requested_Date", formattedDate);

                        int count = Convert.ToInt32(command2.ExecuteScalar());
                        totalAdmitRequests_lbl.Content = count.ToString();
                    }

                    #endregion




                }
                catch (SQLiteException ex)
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

            MyOPDProgressBar();
        }

        private void MyDoctorClinic()
        {
            dipartmentName_lbl.Content = "Outpatient Dipartment (Clinic)";
            IsClinic = true;
            MyClincProgressBar();
        }



        private void MyOPDProgressBar()
        {
            int opdCount = -1;
            int totalCount = -1;

            using (SQLiteConnection connection = new Database_Connector().GetConnection())
            {
                try
                {
                    connection.Open();
                    string query = "SELECT COUNT(*) FROM PatientMedical_Event WHERE PME_Date = @TodayDate AND PME_Location = 'OPD'";
                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        // Format today's date as a string that matches the date format in your database
                        string todayDate = DateTime.Today.ToString("yyyy-MM-dd");
                        command.Parameters.AddWithValue("@TodayDate", todayDate);

                        opdCount = Convert.ToInt32(command.ExecuteScalar());
                    }

                    string query2 = "SELECT COUNT(*) FROM PatientMedical_Event WHERE PME_Location = 'OPD' ";
                    using (SQLiteCommand command = new SQLiteCommand(query2, connection))
                    {

                        totalCount = Convert.ToInt32(command.ExecuteScalar());
                    }
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                finally
                {
                    connection.Close();
                }
            }

            if(opdCount != -1 && totalCount != -1)
            {
                Debug.WriteLine($"\n\n\nOPD Count: {opdCount} \nTotal Count: {totalCount}");


                double percentage = (opdCount / (double)totalCount) * 100; // Ensure division is not integer division
                int roundedPercentage = (int)Math.Round(percentage);


                progressBar_UC.UpdateProgressBar(roundedPercentage);
                progressBar_UC.presentageCount_lbl.Content = $"{opdCount}/{totalCount}";
                progressBar_UC.presentage_lbl.Content = $"{roundedPercentage}%";

                Debug.WriteLine($"RoundedPercentage: {roundedPercentage}");
                Debug.WriteLine($"Percentage: {percentage}");
                
            }
            else
            {
                MessageBox.Show("Error: OPD Progress Bar Data is not Valid", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }


            
        }

        private void MyClincProgressBar()
        {
            int clinicTotal = -1;
            int clinicVisited = -1;

            using (SQLiteConnection connection = new Database_Connector().GetConnection())
            {
                try
                {
                    connection.Open();
                    string query = "SELECT COUNT(*) FROM Patient_AppointmentRequest WHERE IsBooked = 1";
                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        // Format today's date as a string that matches the date format in your database
                        string todayDate = DateTime.Today.ToString("yyyy-MM-dd");
                        command.Parameters.AddWithValue("@TodayDate", todayDate);

                        clinicTotal = Convert.ToInt32(command.ExecuteScalar());
                    }

                    string query2 = "SELECT COUNT(*) FROM Patient_AppointmentRequest WHERE IsBooked = 1 AND IsVisitedByDoctor = 1  ";
                    using (SQLiteCommand command = new SQLiteCommand(query2, connection))
                    {

                        clinicVisited = Convert.ToInt32(command.ExecuteScalar());
                    }
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                finally
                {
                    connection.Close();
                }
            }

            if (clinicTotal != -1 && clinicVisited != -1)
            {
                //Debug.WriteLine($"\n\n\nClinci Count: {opdCount} \nTotal Count: {totalCount}");


                double percentage = ( clinicVisited / (double)clinicTotal) * 100; // Ensure division is not integer division
                int roundedPercentage = (int)Math.Round(percentage);


                progressBar_UC.UpdateProgressBar(roundedPercentage);
                progressBar_UC.presentageCount_lbl.Content = $"{clinicVisited}/{clinicTotal}";
                progressBar_UC.presentage_lbl.Content = $"{roundedPercentage}%";


            }
            else
            {
                MessageBox.Show("Error: Clinic Progress Bar Data is not Valid", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }



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

            using (SQLiteConnection connection = new Database_Connector().GetConnection())  //to check if the patient RID is correct or not
            {
                string query1 = "SELECT * FROM Patient WHERE P_RegistrationID = @patientRID ";

                SQLiteCommand cmd = new SQLiteCommand(query1, connection);

                cmd.Parameters.AddWithValue("@patientRID", "P"+patientRID_tbx.Text);


                try
                {
                    connection.Open();

                    using (SQLiteDataReader reader = cmd.ExecuteReader())
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

                            string query2 = "SELECT * FROM Patient_AppointmentRequest WHERE PatientID = @PatientID AND IsVisitedByDoctor = 0 And IsBooked = 1 ";

                            SQLiteCommand cmd2 = new SQLiteCommand(query2, connection);

                            cmd2.Parameters.AddWithValue("@PatientID", PatientID);

                            using (SQLiteDataReader reader2 = cmd2.ExecuteReader())
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

                                SQLiteCommand cmd3 = new SQLiteCommand(query3, connection);
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

                catch (SQLiteException ex)
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
