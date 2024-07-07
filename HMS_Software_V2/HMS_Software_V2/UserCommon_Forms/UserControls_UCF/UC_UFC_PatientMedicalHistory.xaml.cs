using HMS_Software_V2._DataManage_Classes;
using HMS_Software_V2.General_Purpose;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HMS_Software_V2.UserCommon_Forms.UserControls_UCF
{
    /// <summary>
    /// Interaction logic for UC_UFC_PatientMedicalHistory.xaml
    /// </summary>
    public partial class UC_UFC_PatientMedicalHistory : UserControl
    {
        int MedicalEvnetID;
        public UC_UFC_PatientMedicalHistory(int medicalEventID)
        {
            InitializeComponent();

            MedicalEvnetID = medicalEventID;
            SharedData.viewPatientHistory.PatientMedicalEventID = MedicalEvnetID;

            MyCheckRequestTypeAvailability();

        }

        private void MyCheckRequestTypeAvailability()
        {

            using (SQLiteConnection connection = new Database_Connector().GetConnection())
            {
                bool isLabRequestAvailable = false;
                bool isPrescriptionRequestAvailable = false;
                bool isMonitorRequestAvailable = false;

                try
                {
                    connection.Open();


                    #region Check Lab Request type availability

                    string query1 = @"
                    SELECT 
                        PatientLabRequests_ID,
                        Lab_Specimen_ID,
                        Lab_Specimen_Name, 
                        Lab_Investigation_ID, 
                        Lab_Investigation_Name,
                        Is_Completed
                    FROM 
                        Patient_LabRequest
                    WHERE 
                        PatientMedicalEvent_ID = @PatientMedicalEvent_ID";


                    SQLiteCommand cmd = new SQLiteCommand(query1, connection);

                    cmd.Parameters.AddWithValue("@PatientMedicalEvent_ID", SharedData.viewPatientHistory.PatientMedicalEventID);

                    try
                    {
                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {

                                while (reader.Read())
                                {
                                    //If there are any data in the table

                                    isLabRequestAvailable = true;
                                    

                                }
                            }
                           
                        }

                    }

                    catch (SQLiteException ex)
                    {
                        Debug.WriteLine("\nError1: \n" + ex.Message);
                        MessageBox.Show("Error1: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }


                    #endregion


                    #region Check Prescription Request Type Availabliry
                    try
                    {

                        string query = "SELECT * FROM Patient_PrescriptionRequest WHERE PatientMedicalEvent_ID = @PatientMedicalEvent_ID";

                        SQLiteCommand command = new SQLiteCommand(query, connection);

                        command.Parameters.AddWithValue("@PatientMedicalEvent_ID", SharedData.viewPatientHistory.PatientMedicalEventID);

                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {

                                while (reader.Read())
                                {
                                    //If there are any data in the table
                                    isLabRequestAvailable = true;


                                }
                            }
                           
                        }


                    }
                    catch (SQLiteException ex)
                    {
                        Debug.WriteLine("\nError9: \n" + ex.Message);
                        MessageBox.Show("Error9: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    #endregion


                    #region Check Monitor Request Type Availabliry
                    string query3 = "SELECT PME_MonitorRequest_Results, PME_MonitorRequest FROM PatientMedical_Event WHERE PatientMedicalEvent_ID = @PatientMedicalEvent_ID";
                    using (SQLiteCommand command = new SQLiteCommand(query3, connection))
                    {

                        command.Parameters.AddWithValue("@PatientMedicalEvent_ID", SharedData.viewPatientHistory.PatientMedicalEventID);

                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {

                                while (reader.Read())
                                {
                                    string PME_MonitorRequest = reader["PME_MonitorRequest"].ToString() ?? "";
                                    //If there are any data in the table

                                    if (string.IsNullOrEmpty(PME_MonitorRequest) == false || PME_MonitorRequest != "")
                                    {
                                        isMonitorRequestAvailable = true;
                                        //Debug.WriteLine("################# Progress Note Request Available #################");
                                    }
                                    else
                                    {
                                       // Debug.WriteLine("################# Progress Note Request NOT Available #################");
                                    }
                                    

                                }
                            }
                           
                        }
                    }
                    #endregion


                    if(isLabRequestAvailable == false)
                    {
                        View_LabRequest_btn.IsEnabled = false;
                        View_LabRequest_btn.Background = new SolidColorBrush(Colors.Gray);
                    }
                    if(isPrescriptionRequestAvailable == false)
                    {
                        View_Prescription_btn.IsEnabled = false;
                        View_Prescription_btn.Background = new SolidColorBrush(Colors.Gray);
                    }
                    if(isMonitorRequestAvailable == false)
                    {
                        View_MonitorResults_btn.IsEnabled = false;
                        View_MonitorResults_btn.Background = new SolidColorBrush(Colors.Gray);
                    }






                }
                catch (SQLiteException ex)
                {
                    Debug.WriteLine("\nError: \n" + ex.Message);
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    connection.Close();
                }
            }


        }

        private void View_LabRequest_btn_Click(object sender, RoutedEventArgs e)
        {
            SharedData.viewPatientHistory.PatientMedicalEventID = MedicalEvnetID;

            PMH_ViewLabResults pMH_ViewLabResults = new PMH_ViewLabResults();
            pMH_ViewLabResults.ShowDialog();
        }

        private void View_ProgressNote_btn_Click(object sender, RoutedEventArgs e)
        {
            SharedData.viewPatientHistory.PatientMedicalEventID = MedicalEvnetID;

            SharedData.viewPatientHistory.MedicalEvnentDate = medicalEventDate_lbl.Content.ToString() ?? "Error";
            SharedData.viewPatientHistory.MedicalEvnentTime = visitedTime_lbl.Content.ToString() ?? "Error";

            PMH_ViewProgressReporsts pMH_ViewProgressReporsts = new PMH_ViewProgressReporsts();
            pMH_ViewProgressReporsts.ShowDialog();
        }

        private void View_Prescription_btn_Click(object sender, RoutedEventArgs e)
        {
            SharedData.viewPatientHistory.PatientMedicalEventID = MedicalEvnetID;

            PMH_ViewPrescriptionrequest pMH_ViewPrescriptionrequest = new PMH_ViewPrescriptionrequest();
            pMH_ViewPrescriptionrequest.ShowDialog();
        }

        private void View_MonitorResults_btn_Click(object sender, RoutedEventArgs e)
        {
            SharedData.viewPatientHistory.PatientMedicalEventID = MedicalEvnetID;

            PMH_ViewMonitorReuslts pMH_ViewMonitorReuslts = new PMH_ViewMonitorReuslts();
            pMH_ViewMonitorReuslts.ShowDialog();
        }
    }
}
