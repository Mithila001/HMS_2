using HMS_Software_V2._DataManage_Classes;
using HMS_Software_V2.General_Purpose;
using HMS_Software_V2.Nurse_Ward.NuresWard_UserControls;
using HMS_Software_V2.UserCommon_Forms;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace HMS_Software_V2.Nurse_Ward
{
    /// <summary>
    /// Interaction logic for NW_PatientTreat.xaml
    /// </summary>
    public partial class NW_PatientTreat : Window
    {
        public NW_PatientTreat()
        {
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
        private void MyLoadRequestedTreatments()
        {
            using (SqlConnection connection = new Database_Connector().GetConnection())
            {

                try
                {
                    connection.Open();

                    #region SELECT Available Reqeust From PatientMedical_Event table
                    string query1 = "SELECT PME_Is_LabRequest, PME_Is_PrescriptionRequest, PME_MonitorRequest FROM PatientMedical_Event WHERE PatientMedicalEvent_ID = @PatientMedicalEvent_ID";
                    using (SqlCommand command = new SqlCommand(query1, connection))
                    {
                       
                        command.Parameters.AddWithValue("@PatientMedicalEvent_ID", SharedData.Ward_NursePatient.PatientMedicalEventID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {

                                int isLabRequestColumnIndex = reader.GetOrdinal("PME_Is_LabRequest");
                                IsLabRequest = !reader.IsDBNull(isLabRequestColumnIndex) && reader.GetBoolean(isLabRequestColumnIndex);

                                int isPrescriptionRequest = reader.GetOrdinal("PME_Is_PrescriptionRequest");
                                IsPrescription = !reader.IsDBNull(isPrescriptionRequest) && reader.GetBoolean(isPrescriptionRequest);

                                int monitorRequestColumnIndex = reader.GetOrdinal("PME_MonitorRequest");
                                MonitorRequest = reader.IsDBNull(monitorRequestColumnIndex) ? string.Empty : reader.GetString(monitorRequestColumnIndex);

                                MyFilterMeicalEvenrRequests();
                                Debug.WriteLine("\nRound => SELECT Available Reqeust From PatientMedical_Event table");

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

                catch (Exception ex)
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

            using (SqlConnection connection = new Database_Connector().GetConnection())
            {

                try
                {
                    connection.Open();

                    // Getting Lab Requests
                    if (IsLabRequest)
                    {
                        #region SELECT Lab Reqests

                        int dataReadCounter = 0;

                        string query2 = "SELECT * FROM Patient_LabRequest WHERE PatientMedicalEvent_ID = @PatientMedicalEvent_ID";
                        using (SqlCommand command = new SqlCommand(query2, connection))
                        {
                            command.Parameters.AddWithValue("@PatientMedicalEvent_ID", SharedData.Ward_NursePatient.PatientMedicalEventID);

                            using (SqlDataReader reader = command.ExecuteReader())
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

                catch (Exception ex)
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

        private void save_btn_Click(object sender, RoutedEventArgs e)
        {
            MyAssigneLabRequests();
            Debug.WriteLine("\n---------- save_btn_Click ----------\n");

        }

        private void MyAssigneLabRequests()
        {
            bool foundUserControls = false;

            Debug.WriteLine("\n\n---------- MyAssigneLabRequests ----------\n");

            #region Get and Store required Lab Requesrt User Control's data
            List<(bool IsSelected, int PublicIntValue)> labData = new List<(bool, int)>();

            foreach (var control in ShowDoctorRequests_WrapP.Children)
            {
                Debug.WriteLine("\nforeach (var control in ShowDoctorRequests_WrapP.Children)");

                if (control is UC_NW_P_LabRequest uC_NW_P_LabRequest)
                {
                    ComboBox? comboBox = uC_NW_P_LabRequest.FindName("Completed_comboBox") as ComboBox;
                    bool isSelected = comboBox != null && comboBox.SelectedItem != null;
                    Debug.WriteLine("IsSelected: " + isSelected);

                    int labrequestID = uC_NW_P_LabRequest.LabRequestID;
                    Debug.WriteLine("labrequestID: " + labrequestID);

                    labData.Add((isSelected, labrequestID));

                    foundUserControls = true;
                }
            }
            #endregion

            Debug.WriteLine("foundUserControls: " + foundUserControls);
            //#region UPDATE DB Table
            //if (foundUserControls)
            //{
            //    using (SqlConnection connection = new Database_Connector().GetConnection())
            //    {
            //        connection.Open();

            //        try
            //        {
            //            connection.Open();

            //            #region UPDATE Patient_LabRequest
            //            string query = "UPDATE Patient_LabRequest SET Is_Completed = @IsCompleted WHERE PatientLabRequests_ID = @PatientLabRequests_ID";

            //            foreach (var (IsSelected, LabRequestID) in labData)
            //            {
            //                // Assuming "IsCompleted" is the column you want to update
            //                // and "PatientLabRequests_ID" is the primary key column of "Patient_LabRequest" table

            //                using (SqlCommand command = new SqlCommand(query, connection))
            //                {
            //                    command.Parameters.AddWithValue("@IsCompleted", IsSelected);
            //                    command.Parameters.AddWithValue("@PatientLabRequests_ID", LabRequestID);


            //                    command.ExecuteNonQuery();

            //                    Debug.WriteLine($"Patient_LabRequest Updated => IsCompleted:{IsSelected}, PatientLabRequests_ID:{LabRequestID} ");

            //                }
            //            }
            //            #endregion


            //        }
            //        catch (Exception ex)
            //        {
            //            Debug.WriteLine("\nError5: \n" + ex.Message);
            //            MessageBox.Show("Error5: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            //            return;
            //        }
            //        finally
            //        {
            //            connection.Close();
            //        }

            //    }

            //}
            //else
            //{
            //    Debug.WriteLine("\nNo Lab Request user controls found !!!!!!!!");
            //    return;
            //} 
            //#endregion


        }

        private void MyAssigPrescriptionRequests()
        {
            Debug.WriteLine("\n\n---------- MyAssigPrescriptionRequests ----------\n");

            bool foundUserControls = false;

            Debug.WriteLine("\n\n---------- MyAssigneLabRequests ----------\n");

            #region Get and Store required Lab Requesrt User Control's data

            foreach (var control in ShowDoctorRequests_WrapP.Children)
            {
                Debug.WriteLine("\nforeach (var control in ShowDoctorRequests_WrapP.Children)");

                if (control is UC_NW_P_PrescrioptionsView uC_NW_P_PrescrioptionsView)
                {
                    ComboBox? comboBox = uC_NW_P_PrescrioptionsView.FindName("Completed_comboBox") as ComboBox;
                    bool isSelected = comboBox != null && comboBox.SelectedItem != null;
                    Debug.WriteLine("IsSelected: " + isSelected);

                    int medicalEvnetID = uC_NW_P_PrescrioptionsView.PatientMedicalEvnetID;
                    Debug.WriteLine("PatientMedicalEvnetID: " + medicalEvnetID);

                    foundUserControls = true;
                }
            }
            #endregion


            if (foundUserControls)
            {

            }
        }


    }
    
}
