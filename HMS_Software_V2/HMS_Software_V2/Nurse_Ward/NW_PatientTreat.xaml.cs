using HMS_Software_V2._DataManage_Classes;
using HMS_Software_V2.General_Purpose;
using HMS_Software_V2.Nurse_Ward.NuresWard_UserControls;
using HMS_Software_V2.UserCommon_Forms;
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
using System.Windows.Shapes;

namespace HMS_Software_V2.Nurse_Ward
{
    /// <summary>
    /// Interaction logic for NW_PatientTreat.xaml
    /// </summary>
    public partial class NW_PatientTreat : Window
    {
        public NW_PatientTreat()
        {
            Debug.WriteLine("\n\n\n =================================================== NW_PatientTreat ===================================================\n\n ");

            InitializeComponent();

            MyLoadBasicDetails();

            MyLoadRequestedTreatments();
        }

        private void MyLoadBasicDetails()
        {
           
            patientName_lbl.Content = SharedData.Ward_NursePatient.PatientName;
            patientRID_lbl.Content = SharedData.Ward_NursePatient.PatientID;
            patientAge_lbl.Content = SharedData.Ward_NursePatient.PatientAge;
            patientGender_lbl.Content = SharedData.Ward_NursePatient.PatientGender;
            patientMedicalCondition_lbl.Content = SharedData.Ward_NursePatient.PatientCondition;

            nurseName_lbl.Content = SharedData.Ward_Nurse.NurseName;
            nurseLicenceNo_lbl.Content = SharedData.Ward_Nurse.NureseLicenceNumber;

            wardName_lbl.Content = SharedData.Ward_Nurse.WardName;
        }

        bool IsLabRequest = false;
        bool IsPrescription = false;
        string MonitorRequest = "";

        bool IsPrescriptionRequestCompleted = false;
        bool IsMonitorRequestCompleted = false;
        private void MyLoadRequestedTreatments()
        {
            Debug.WriteLine("\n Method => MyLoadRequestedTreatments() ");
            using (SQLiteConnection connection = new Database_Connector().GetConnection())
            {

                try
                {
                    connection.Open();
                    Debug.WriteLine("\n SELECT Available Reqeust From [PatientMedical_Event] table ");
                    #region SELECT Available Reqeust From PatientMedical_Event table
                    string query1 = "SELECT PME_Is_LabRequest, PME_Is_PrescriptionRequest, PME_MonitorRequest, PME_Is_PrescriptionR_Completed, PME_IsMonitorRequestComplet FROM PatientMedical_Event WHERE PatientMedicalEvent_ID = @PatientMedicalEvent_ID";
                    using (SQLiteCommand command = new SQLiteCommand(query1, connection))
                    {
                       
                        command.Parameters.AddWithValue("@PatientMedicalEvent_ID", SharedData.Ward_NursePatient.PatientMedicalEventID);

                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Debug.WriteLine("Found Record ");
                                int isLabRequestColumnIndex = reader.GetOrdinal("PME_Is_LabRequest");
                                IsLabRequest = !reader.IsDBNull(isLabRequestColumnIndex) && reader.GetBoolean(isLabRequestColumnIndex);

                                int isPrescriptionRequest = reader.GetOrdinal("PME_Is_PrescriptionRequest");
                                IsPrescription = !reader.IsDBNull(isPrescriptionRequest) && reader.GetBoolean(isPrescriptionRequest);

                                int monitorRequestColumnIndex = reader.GetOrdinal("PME_MonitorRequest");
                                MonitorRequest = reader.IsDBNull(monitorRequestColumnIndex) ? string.Empty : reader.GetString(monitorRequestColumnIndex);

                                int isPrescriptionRequestCompleted = reader.GetOrdinal("PME_Is_PrescriptionR_Completed");
                                IsPrescriptionRequestCompleted = !reader.IsDBNull(isPrescriptionRequestCompleted) && reader.GetBoolean(isPrescriptionRequestCompleted);

                                int isMonitorRequestCompleted = reader.GetOrdinal("PME_IsMonitorRequestComplet");
                                IsMonitorRequestCompleted = !reader.IsDBNull(isMonitorRequestCompleted) && reader.GetBoolean(isMonitorRequestCompleted);


                                Debug.WriteLine("\n ------------------------------------------------------");

                                Debug.WriteLine("IsLabReques: "+ IsLabRequest);
                                Debug.WriteLine("IsPrescription: " + IsPrescription);
                                Debug.WriteLine("MonitorRequest: " + MonitorRequest);
                                Debug.WriteLine("IsPrescriptionRequestCompleted: " + IsPrescriptionRequestCompleted);
                                Debug.WriteLine("IsMonitorRequestCompleted: " + IsMonitorRequestCompleted);
                                Debug.WriteLine("\n ------------------------------------------------------");


                                Debug.WriteLine("\n Method =>  MyFilterMeicalEvenrRequests() ");
                                MyFilterMeicalEvenrRequests();
                                

                               

                            }
                            else
                            {
                                // Handle the case where no records are found
                                MessageBox.Show("No records found.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }
                    }
                    #endregion


                }

                catch (SQLiteException ex)
                {
                    Debug.WriteLine("\nError1: \n" + ex.Message);
                    MessageBox.Show("Error4: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private async void MyFilterMeicalEvenrRequests()
        {

            using (SQLiteConnection connection = new Database_Connector().GetConnection())
            {

                try
                {
                    connection.Open();

                    // Getting Lab Requests
                    if (IsLabRequest)
                    {
                        Debug.WriteLine("\n Getting Lab Request from DB ");

                        #region SELECT Lab Reqests

                        int dataReadCounter = 0;

                        string query2 = "SELECT * FROM Patient_LabRequest WHERE PatientMedicalEvent_ID = @PatientMedicalEvent_ID";
                        using (SQLiteCommand command = new SQLiteCommand(query2, connection))
                        {
                            command.Parameters.AddWithValue("@PatientMedicalEvent_ID", SharedData.Ward_NursePatient.PatientMedicalEventID);

                            using (SQLiteDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    int _isLabRequestIDColumnIndex = reader.GetOrdinal("PatientLabRequests_ID");
                                    int labRequestID = reader.IsDBNull(_isLabRequestIDColumnIndex) ? 0 : reader.GetInt32(_isLabRequestIDColumnIndex);
                                    Debug.WriteLine("----IsLabRequest: " + IsLabRequest);

                                    int _isLabRequestColumnIndex = reader.GetOrdinal("Lab_Investigation_Name");
                                    string labRequestName = reader.IsDBNull(_isLabRequestColumnIndex) ? string.Empty : reader.GetString(_isLabRequestColumnIndex);

                                    int _isSpecimentCI = reader.GetOrdinal("Lab_Specimen_Name");
                                    string specimentName = reader.IsDBNull(_isSpecimentCI) ? string.Empty : reader.GetString(_isSpecimentCI);

                                    int _isLabelCI = reader.GetOrdinal("LabelNumber");
                                    string labelNumber = reader.IsDBNull(_isLabelCI) ? string.Empty : reader.GetString(_isLabelCI);

                                    int _isCompletedCI = reader.GetOrdinal("Is_Completed"); 
                                    bool isRequestCompleted = !reader.IsDBNull(_isCompletedCI) && reader.GetBoolean(_isCompletedCI);

                                    UC_NW_P_LabRequest uc_NW_P_LabRequest = new UC_NW_P_LabRequest();
                                    uc_NW_P_LabRequest.investigationName_lbl.Content = $"{labRequestName} ({specimentName})";
                                    uc_NW_P_LabRequest.lableNumber_lbl.Content = labelNumber;
                                    uc_NW_P_LabRequest.LabRequestID = labRequestID;

                                    #region Check UnCheck checkbox
                                    if (isRequestCompleted)
                                    {
                                        uc_NW_P_LabRequest.Completed_checkbox.IsChecked = true;
                                    }
                                    else
                                    {
                                        uc_NW_P_LabRequest.Completed_checkbox.IsChecked = false;
                                    } 
                                    #endregion


                                    // Add the user control to the parent container
                                    ShowDoctorRequests_WrapP.Children.Add(uc_NW_P_LabRequest);

                                    await Task.Delay(0005); // 5ms delay

                                    ShowDoctorRequests_WrapP.SizeChanged += (sender, e) =>
                                    {
                                        uc_NW_P_LabRequest.Width = ShowDoctorRequests_WrapP.ActualWidth - uc_NW_P_LabRequest.Margin.Left - uc_NW_P_LabRequest.Margin.Right;
                                    };
                                    uc_NW_P_LabRequest.Width = ShowDoctorRequests_WrapP.ActualWidth - uc_NW_P_LabRequest.Margin.Left - uc_NW_P_LabRequest.Margin.Right;

                                    dataReadCounter++;
                                }

                                if (dataReadCounter == 0)
                                {
                                    MessageBox.Show("No records found.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                                }
                               
                                    
                                
                            }
                        }
                        #endregion

                    }

                    if (IsPrescription)
                    {

                        #region add a Prescription Requests user control


                        UC_NW_P_PrescrioptionsView uc_NW_P_PrescrioptionsView = new UC_NW_P_PrescrioptionsView(this);

                        uc_NW_P_PrescrioptionsView.PatientMedicalEvnetID =  SharedData.Ward_NursePatient.PatientMedicalEventID;


                        if (IsPrescriptionRequestCompleted)
                        {
                            uc_NW_P_PrescrioptionsView.Completed_CheckBox.IsChecked = true;
                        }
                        else
                        {
                            uc_NW_P_PrescrioptionsView.Completed_CheckBox.IsChecked = false;
                        }




                        // Add the user control to the parent container
                        ShowDoctorRequests_WrapP.Children.Add(uc_NW_P_PrescrioptionsView);

                        await Task.Delay(0005); // 1000ms = 1s

                        ShowDoctorRequests_WrapP.SizeChanged += (sender, e) =>
                        {
                            uc_NW_P_PrescrioptionsView.Width = ShowDoctorRequests_WrapP.ActualWidth - uc_NW_P_PrescrioptionsView.Margin.Left - uc_NW_P_PrescrioptionsView.Margin.Right;

                        };
                        uc_NW_P_PrescrioptionsView.Width = ShowDoctorRequests_WrapP.ActualWidth - uc_NW_P_PrescrioptionsView.Margin.Left - uc_NW_P_PrescrioptionsView.Margin.Right;
                        #endregion

                    }

                    if (!string.IsNullOrEmpty(MonitorRequest))
                    {

                        #region add a Monitor Requests user control


                        UC_NW_P_MonitorRequests uc_NW_P_MonitorRequests = new UC_NW_P_MonitorRequests(this);
                        uc_NW_P_MonitorRequests.PatientMedicalaEventID = SharedData.Ward_NursePatient.PatientMedicalEventID;


                        if (IsMonitorRequestCompleted)
                        {
                            uc_NW_P_MonitorRequests.Completed_CheckBox.IsChecked = true;
                        }
                        else
                        {
                            uc_NW_P_MonitorRequests.Completed_CheckBox.IsChecked = false;
                        }

                        // Add the user control to the parent container
                        ShowDoctorRequests_WrapP.Children.Add(uc_NW_P_MonitorRequests);

                        await Task.Delay(0005); // 1000ms = 1s

                        ShowDoctorRequests_WrapP.SizeChanged += (sender, e) =>
                        {
                            uc_NW_P_MonitorRequests.Width = ShowDoctorRequests_WrapP.ActualWidth - uc_NW_P_MonitorRequests.Margin.Left - uc_NW_P_MonitorRequests.Margin.Right;

                        };
                        uc_NW_P_MonitorRequests.Width = ShowDoctorRequests_WrapP.ActualWidth - uc_NW_P_MonitorRequests.Margin.Left - uc_NW_P_MonitorRequests.Margin.Right;
                        #endregion

                    }


                }

                catch (SQLiteException ex)
                {
                    Debug.WriteLine("\nError1: \n" + ex.Message);
                    MessageBox.Show("Error4: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    connection.Close();
                }
            }


            

        }

        private async void save_btn_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("\n---------- save_btn_Click ----------\n");
            MyAssigneLabRequests();
            MyAssigPrescriptionRequests();
            MyAssigneMonitorReqeust();

            await Task.Delay(0001); // 1000ms = 1s
            this.Close();

        }

        private void MyAssigneLabRequests()
        {
            bool foundUserControls = false;

            Debug.WriteLine("\n\n---------- MyAssigneLabRequests ----------\n");

            #region Get and Store required Lab Requesrt User Control's data


            List<(bool IsSelected, int labRwquestID)> labData = new List<(bool, int)>();

            foreach (var control in ShowDoctorRequests_WrapP.Children)
            {
                Debug.WriteLine("\nforeach (var control in ShowDoctorRequests_WrapP.Children)");

                if (control is UC_NW_P_LabRequest uC_NW_P_LabRequest)
                {
                    CheckBox? checkBox = uC_NW_P_LabRequest.FindName("Completed_checkbox") as CheckBox;
                    bool isSelected = checkBox != null && checkBox.IsChecked == true;
                    Debug.WriteLine("Get => IsSelected: " + isSelected);

                    int labrequestID = uC_NW_P_LabRequest.LabRequestID;
                    Debug.WriteLine("labrequestID: " + labrequestID);

                    labData.Add((isSelected, labrequestID));

                    foundUserControls = true;
                }
            }


            #endregion

            Debug.WriteLine("foundUserControls: " + foundUserControls);
            #region UPDATE DB Table
            if (foundUserControls)
            {
                using (SQLiteConnection connection = new Database_Connector().GetConnection())
                {
                    connection.Open();

                    try
                    {
                        

                        #region UPDATE Patient_LabRequest
                        string query = "UPDATE Patient_LabRequest SET Is_Completed = @IsCompleted WHERE PatientLabRequests_ID = @PatientLabRequests_ID";

                        foreach (var (IsSelected, labRwquestID) in labData)
                        {
                            // Assuming "IsCompleted" is the column you want to update
                            // and "PatientLabRequests_ID" is the primary key column of "Patient_LabRequest" table

                            using (SQLiteCommand command = new SQLiteCommand(query, connection))
                            {
                                command.Parameters.AddWithValue("@IsCompleted", IsSelected);
                                command.Parameters.AddWithValue("@PatientLabRequests_ID", labRwquestID);


                                command.ExecuteNonQuery();

                                Debug.WriteLine($"Patient_LabRequest Updated => IsCompleted:{IsSelected}, PatientLabRequests_ID:{labRwquestID} ");

                            }
                        }
                        #endregion


                        #region UPDATE Admitted_Patients_VisitEvent
                        string query2 = "UPDATE Admitted_Patients_VisitEvent SET N_TreatmentStatus = @N_TreatmentStatus WHERE Patient_ID = @Patient_ID";

                        using (SQLiteCommand command = new SQLiteCommand(query2, connection))
                        {
                            command.Parameters.AddWithValue("@N_TreatmentStatus", "In Progress");
                            command.Parameters.AddWithValue("@Patient_ID", SharedData.Ward_NursePatient.PatientID);


                            command.ExecuteNonQuery();

                            Debug.WriteLine($"Admitted_Patients_VisitEvent Updated => N_TreatmentStatus - In Progress, Patient_ID:{SharedData.Ward_NursePatient.PatientID} ");

                        }
                        
                        #endregion


                    }
                    catch (SQLiteException ex)
                    {
                        Debug.WriteLine("\nError5: \n" + ex.Message);
                        MessageBox.Show("Error5: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    finally
                    {
                        connection.Close();
                    }

                }

            }
            else
            {
                Debug.WriteLine("\nNo Lab Request user controls found !!!!!!!!");
                return;
            }
            #endregion


        }

        private void MyAssigPrescriptionRequests()
        {
            Debug.WriteLine("\n\n---------- MyAssigPrescriptionRequests ----------\n");

            bool isSelected = false;
            int medicalEvnetID = 0;

            bool foundUserControls = false;

            #region Get and Store Prescription Requesrt User Control's data

            foreach (var control in ShowDoctorRequests_WrapP.Children)
            {
                Debug.WriteLine("\nforeach (var control in ShowDoctorRequests_WrapP.Children)");

                if (control is UC_NW_P_PrescrioptionsView uC_NW_P_PrescrioptionsView)
                {
                    CheckBox? checkBox = uC_NW_P_PrescrioptionsView.FindName("Completed_CheckBox") as CheckBox;

                    // Check if the CheckBox is checked
                    isSelected = checkBox != null && checkBox.IsChecked == true;
                    Debug.WriteLine("Prescription => IsSelected: " + isSelected);

                    medicalEvnetID = uC_NW_P_PrescrioptionsView.PatientMedicalEvnetID;
                    Debug.WriteLine("Prescription => PatientMedicalEvnetID: " + medicalEvnetID);

                    foundUserControls = true;
                    break;
                }
            }
            #endregion
            

            if (foundUserControls)
            {
                if (medicalEvnetID == 0)
                {
                    MessageBox.Show("Failde to get Medical Evnet ID. Medical Evetn ID:" + medicalEvnetID, "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                using (SQLiteConnection connection = new Database_Connector().GetConnection())
                {
                    connection.Open();

                    try
                    {

                        #region UPDATE Patient_PrescriptionRequest Table
                        string query = "UPDATE Patient_PrescriptionRequest SET PR__IsCompleted = @PR__IsCompleted WHERE PatientMedicalEvent_ID = @PatientMedicalEvent_ID";

                        using (SQLiteCommand command = new SQLiteCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@PR__IsCompleted", isSelected);
                            command.Parameters.AddWithValue("@PatientMedicalEvent_ID", medicalEvnetID);
                            command.ExecuteNonQuery();
                            Debug.WriteLine("\nUPDATE Patient_PrescriptionRequest Table,  MedicalEventID: " + medicalEvnetID);
                        }
                        #endregion

                        #region UPDATE PatientMedical_Event Table
                        string query2 = "UPDATE PatientMedical_Event SET PME_Is_PrescriptionR_Completed = @PME_Is_PrescriptionR_Completed WHERE PatientMedicalEvent_ID = @PatientMedicalEvent_ID";

                        using (SQLiteCommand command = new SQLiteCommand(query2, connection))
                        {
                            command.Parameters.AddWithValue("@PME_Is_PrescriptionR_Completed", isSelected);
                            command.Parameters.AddWithValue("@PatientMedicalEvent_ID", medicalEvnetID);
                            command.ExecuteNonQuery();
                            Debug.WriteLine("\nUPDATE Patient_PrescriptionRequest Table,  MedicalEventID: " + medicalEvnetID);
                        }
                        #endregion

                        #region UPDATE Admitted_Patients_VisitEvent
                        string query3 = "UPDATE Admitted_Patients_VisitEvent SET N_TreatmentStatus = @N_TreatmentStatus WHERE Patient_ID = @Patient_ID";

                        using (SQLiteCommand command = new SQLiteCommand(query3, connection))
                        {
                            command.Parameters.AddWithValue("@N_TreatmentStatus", "In Progress");
                            command.Parameters.AddWithValue("@Patient_ID", SharedData.Ward_NursePatient.PatientID);


                            command.ExecuteNonQuery();

                            Debug.WriteLine($"Admitted_Patients_VisitEvent Updated => N_TreatmentStatus - In Progress, Patient_ID:{SharedData.Ward_NursePatient.PatientID} ");

                        }

                        #endregion

                    }
                    catch (SQLiteException ex)
                    {
                        MessageBox.Show("Error6: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        Debug.WriteLine("\nError5: \n" + ex.Message);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }

            }//Update Patient_PrescriptionRequest Table
            
        }

        private void MyAssigneMonitorReqeust()
        {

            bool isSelected = false;
            int medicalEvnetID = 0;
            bool foundUserControls = false;

            #region Get and Store required Monitor Request User Control's data

            foreach (var control in ShowDoctorRequests_WrapP.Children)
            {
                Debug.WriteLine("\nforeach (var control in ShowDoctorRequests_WrapP.Children)");

                if (control is UC_NW_P_MonitorRequests uC_NW_P_MonitorRequests)
                {
                    CheckBox? checkBox = uC_NW_P_MonitorRequests.FindName("Completed_CheckBox") as CheckBox;

                    // Check if the CheckBox is checked
                    isSelected = checkBox != null && checkBox.IsChecked == true;
                    Debug.WriteLine("Monitor => IsSelected: " + isSelected);

                    medicalEvnetID = uC_NW_P_MonitorRequests.PatientMedicalaEventID;
                    Debug.WriteLine("Monitor => PatientMedicalaEventID: " + medicalEvnetID);

                    foundUserControls = true;
                    break;
                }
            }
            #endregion



            if (foundUserControls)
            {
                if (medicalEvnetID == 0)
                {
                    MessageBox.Show("Failde to get Medical Evnet ID. Medical Evetn ID:" + medicalEvnetID, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                using (SQLiteConnection connection = new Database_Connector().GetConnection())
                {
                    connection.Open();

                    try
                    {

                        #region UPDATE PatientMedical_Event Table
                        string query = "UPDATE PatientMedical_Event SET PME_IsMonitorRequestComplet = @PME_IsMonitorRequestComplet WHERE PatientMedicalEvent_ID = @PatientMedicalEvent_ID";

                        using (SQLiteCommand command = new SQLiteCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@PME_IsMonitorRequestComplet", isSelected);
                            command.Parameters.AddWithValue("@PatientMedicalEvent_ID", medicalEvnetID);
                            command.ExecuteNonQuery();
                            Debug.WriteLine("\nUPDATE PatientMedical_Event Table,  MedicalEventID: " + medicalEvnetID);
                            Debug.WriteLine("\nUPDATE PatientMedical_Event Table,  PME_IsMonitorRequestComplet: " + isSelected);
                        }
                        #endregion

                        #region UPDATE Admitted_Patients_VisitEvent
                        string query2 = "UPDATE Admitted_Patients_VisitEvent SET N_TreatmentStatus = @N_TreatmentStatus WHERE Patient_ID = @Patient_ID";

                        using (SQLiteCommand command = new SQLiteCommand(query2, connection))
                        {
                            command.Parameters.AddWithValue("@N_TreatmentStatus", "In Progress");
                            command.Parameters.AddWithValue("@Patient_ID", SharedData.Ward_NursePatient.PatientID);


                            command.ExecuteNonQuery();

                            Debug.WriteLine($"Admitted_Patients_VisitEvent Updated => N_TreatmentStatus - In Progress, Patient_ID:{SharedData.Ward_NursePatient.PatientID} ");

                        }

                        #endregion

                    }
                    catch (SQLiteException ex)
                    {
                        MessageBox.Show("Error6: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        Debug.WriteLine("\nError5: \n" + ex.Message);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }

            }//Update Medical Event Table Table




        }

        private void NW_PatientTreat1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            NW_Dashboard nW_Dashboard = new NW_Dashboard();
            nW_Dashboard.Show();
        }

        private void complete_btn_Click(object sender, RoutedEventArgs e)
        {
            bool isLabRequestCompleted = true;
            bool isPrescriptionRequestCompleted = true;
            bool isMonitorRequestCompleted = true;

            #region Scan throug LabRequest User Controls
            foreach (var control in ShowDoctorRequests_WrapP.Children)
            {
                Debug.WriteLine("\nScan throug LabRequest User Controls)");

                if (control is UC_NW_P_LabRequest uC_NW_P_LabRequest)
                {
                    CheckBox? checkBox = uC_NW_P_LabRequest.FindName("Completed_checkbox") as CheckBox;
                    bool isSelected = checkBox != null && checkBox.IsChecked == true;
                    Debug.WriteLine("LabReqeust Check => IsSelected: " + isSelected);

                    if (!isSelected)
                    {
                        isLabRequestCompleted = false;
                        break;
                    }

                }
            } 
            #endregion


            #region Scan through Prescription Request User Controls

            foreach (var control in ShowDoctorRequests_WrapP.Children)
            {
                Debug.WriteLine("\nScan through Prescription Request User Controls)");

                if (control is UC_NW_P_PrescrioptionsView uC_NW_P_PrescrioptionsView)
                {
                    CheckBox? checkBox = uC_NW_P_PrescrioptionsView.FindName("Completed_CheckBox") as CheckBox;

                    // Check if the CheckBox is checked
                    bool isSelected = checkBox != null && checkBox.IsChecked == true;
                    Debug.WriteLine("Prescription => IsSelected: " + isSelected);

                    if(!isSelected)
                    {
                        isPrescriptionRequestCompleted = false;
                        break;
                    }
                }
            }
            #endregion


            #region Scan through Monitor Request User Control's data

            foreach (var control in ShowDoctorRequests_WrapP.Children)
            {
                Debug.WriteLine("\nScan through Monitor Request User Control's data");

                if (control is UC_NW_P_MonitorRequests uC_NW_P_MonitorRequests)
                {
                    CheckBox? checkBox = uC_NW_P_MonitorRequests.FindName("Completed_CheckBox") as CheckBox;

                    // Check if the CheckBox is checked
                    bool isSelected = checkBox != null && checkBox.IsChecked == true;
                    Debug.WriteLine("Monitor => IsSelected: " + isSelected);

                    if (!isSelected)
                    {
                        isMonitorRequestCompleted = false;
                        break;
                    }
  
                }
            }
            #endregion



            if (isLabRequestCompleted && isPrescriptionRequestCompleted && isMonitorRequestCompleted)
            {
                #region UPDATE Admitted_Patients_VisitEvent
                using (SQLiteConnection connection = new Database_Connector().GetConnection())
                {
                    connection.Open();

                    try
                    {
                        string query2 = "UPDATE Admitted_Patients_VisitEvent SET N_TreatmentStatus = @N_TreatmentStatus, Is_VisitedByNurse = @Is_VisitedByNurse  WHERE Patient_ID = @Patient_ID AND Is_RoundTimeOut = 0 ";

                        using (SQLiteCommand command = new SQLiteCommand(query2, connection))
                        {
                            command.Parameters.AddWithValue("@N_TreatmentStatus", "Completed");
                            command.Parameters.AddWithValue("@Is_VisitedByNurse", 1);
                            command.Parameters.AddWithValue("@Patient_ID", SharedData.Ward_NursePatient.PatientID);
                            command.ExecuteNonQuery();
                            this.Close();

                        }
                    }
                    catch (SQLiteException ex)
                    {
                        MessageBox.Show("Error8" + ex, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    finally
                    {
                        connection.Close();
                    }
                } 
                #endregion
            }
            else
            {
                var result = MessageBox.Show("All The Tasks Are Not Completed! Want to Continue?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                #region If Yes -> Update Admitted_Patients_VisitEvent
                if (result == MessageBoxResult.Yes)
                {
                    using (SQLiteConnection connection = new Database_Connector().GetConnection())
                    {
                        connection.Open();

                        try
                        {
                            string query2 = "UPDATE Admitted_Patients_VisitEvent SET N_TreatmentStatus = @N_TreatmentStatus, Is_VisitedByNurse = @Is_VisitedByNurse  WHERE Patient_ID = @Patient_ID AND Is_RoundTimeOut = 0 ";

                            using (SQLiteCommand command = new SQLiteCommand(query2, connection))
                            {
                                command.Parameters.AddWithValue("@N_TreatmentStatus", "Completed");
                                command.Parameters.AddWithValue("@Is_VisitedByNurse", 1);
                                command.Parameters.AddWithValue("@Patient_ID", SharedData.Ward_NursePatient.PatientID);
                                command.ExecuteNonQuery();

                                Debug.WriteLine("\n If Yes -> Update Admitted_Patients_VisitEvent ");
                            }
                        }
                        catch (SQLiteException ex)
                        {
                            MessageBox.Show("Error8" + ex, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        finally
                        {
                            connection.Close();
                        }
                    }


                    this.Close();
                }
                else
                {
                    return;
                } 
                #endregion

            }


        }
    }
    
}
