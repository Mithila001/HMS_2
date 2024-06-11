using HMS_Software_V2._DataManage_Classes;
using HMS_Software_V2.Doctor_ClincOPD;
using HMS_Software_V2.General_Purpose;
using HMS_Software_V2.Reception.R_UserControls;
using HMS_Software_V2.UserCommon_Forms.UserControls_UCF;
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
using static HMS_Software_V2.UserCommon_Forms.UserControls_UCF.UC_UFC_Clinictypes;

namespace HMS_Software_V2.UserCommon_Forms
{
    /// <summary>
    /// Interaction logic for RequestAppointment.xaml
    /// </summary>
    public partial class RequestAppointment : Window
    {
        private DCO_PatientCheck _parentForm;
        public RequestAppointment(DCO_PatientCheck parentForm)
        {
            InitializeComponent();

            LoadClinicType();

            _parentForm = parentForm;
        }

        private void LoadClinicType()
        {
            using (SqlConnection connection = new Database_Connector().GetConnection())
            {
                string query1 = "SELECT * FROM ClinicType";

                SqlCommand cmd = new SqlCommand(query1, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        UC_UFC_Clinictypes uC_UFC_Clinictypes = new UC_UFC_Clinictypes(D_Clinictypes_WrapP);

                        string clinicTypeName = reader["CT_Name"].ToString() ?? "Error";

                        uC_UFC_Clinictypes.ClinicTypeName.Content = clinicTypeName;
                        uC_UFC_Clinictypes.ClinicType_Name = clinicTypeName;
                        uC_UFC_Clinictypes.ClinicTypeID = Convert.ToInt32(reader["ClinicType_ID"]);

                        // Subscribe to the custom event
                        uC_UFC_Clinictypes.UC_UCF_MyClinicTypeClicked += My_UC_UFC_ClinicType_AddButtonClicked;


                        // Adjust the width of the user control to match the width of the parent container
                        D_Clinictypes_WrapP.SizeChanged += (sender, e) =>
                        {
                            uC_UFC_Clinictypes.Width = D_Clinictypes_WrapP.ActualWidth - uC_UFC_Clinictypes.Margin.Left - uC_UFC_Clinictypes.Margin.Right;

                        };
                        uC_UFC_Clinictypes.Width = D_Clinictypes_WrapP.ActualWidth - uC_UFC_Clinictypes.Margin.Left - uC_UFC_Clinictypes.Margin.Right;

                        D_Clinictypes_WrapP.Children.Add(uC_UFC_Clinictypes);
                    }
                    reader.Close();


                }

                catch (Exception ex)
                {
                    Debug.WriteLine("\nError1: \n" + ex.Message);
                    MessageBox.Show("Error1: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
        }

        private void My_UC_UFC_ClinicType_AddButtonClicked(object? sender, UC_UCF_MyClinicType_CustomEventArgs e)
        {
            HandleClinicTypeClicked(e.ClinicTypeID_evnet, e.ClinicType_Name_evnet);
        }

        private void HandleClinicTypeClicked(int clinicTypeID, string clinicName)
        {
            UC_UCF_ToAssigedClinic uC_UCF_ToAssigedClinic = new UC_UCF_ToAssigedClinic(D_RequestedClinics_WrapP);

            uC_UCF_ToAssigedClinic.ToAssigne_ClinicTypeName.Content = clinicName;

            uC_UCF_ToAssigedClinic.UC_UCF_TA_AddClinicClicked += My_UC_UCF_ToAssigeClinic_RemoveButtonClicked;

            uC_UCF_ToAssigedClinic.UC_UCF_TAC_ClinicName = clinicName;
            uC_UCF_ToAssigedClinic.UC_UCF_TAC_ClinicID = clinicTypeID;

            D_RequestedClinics_WrapP.SizeChanged += (sender, e) =>
            {
                uC_UCF_ToAssigedClinic.Width = D_RequestedClinics_WrapP.ActualWidth - uC_UCF_ToAssigedClinic.Margin.Left - uC_UCF_ToAssigedClinic.Margin.Right;

            };
            uC_UCF_ToAssigedClinic.Width = D_RequestedClinics_WrapP.ActualWidth - uC_UCF_ToAssigedClinic.Margin.Left - uC_UCF_ToAssigedClinic.Margin.Right;

            D_RequestedClinics_WrapP.Children.Add(uC_UCF_ToAssigedClinic);
        }


        private void My_UC_UCF_ToAssigeClinic_RemoveButtonClicked(object? sender, UC_UCF_ToAssigeClinic_CustomEventArgs e)
        {
            MyAddClinicTypes_Again(e.UC_UCF_TA_ClinicID, e.UC_UCF_TA_ClinicName);
        }

        private void MyAddClinicTypes_Again(int clinicID, string clinicTypeName)
        {
            UC_UFC_Clinictypes uC_UFC_Clinictypes = new UC_UFC_Clinictypes(D_Clinictypes_WrapP);

            uC_UFC_Clinictypes.ClinicTypeName.Content = clinicTypeName;
            uC_UFC_Clinictypes.ClinicType_Name = clinicTypeName;
            uC_UFC_Clinictypes.ClinicTypeID = clinicID;

            // Subscribe to the custom event
            uC_UFC_Clinictypes.UC_UCF_MyClinicTypeClicked += My_UC_UFC_ClinicType_AddButtonClicked;


            // Adjust the width of the user control to match the width of the parent container
            D_Clinictypes_WrapP.SizeChanged += (sender, e) =>
            {
                uC_UFC_Clinictypes.Width = D_Clinictypes_WrapP.ActualWidth - uC_UFC_Clinictypes.Margin.Left - uC_UFC_Clinictypes.Margin.Right;

            };
            uC_UFC_Clinictypes.Width = D_Clinictypes_WrapP.ActualWidth - uC_UFC_Clinictypes.Margin.Left - uC_UFC_Clinictypes.Margin.Right;

            D_Clinictypes_WrapP.Children.Add(uC_UFC_Clinictypes);
        }

        private void Confirm_btn_Click(object sender, RoutedEventArgs e)
        {
            List<(int, string)> appointmentReqeustList = new List<(int, string)>();

            if (D_RequestedClinics_WrapP.Children.OfType<UC_UCF_ToAssigedClinic>().Count() == 0)
            {
                MessageBox.Show("No Requests", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }



            foreach (var child in D_RequestedClinics_WrapP.Children)
            {
                if (child is UC_UCF_ToAssigedClinic uC_UCF_ToAssigedClinic)
                {
                    string appointmentType = uC_UCF_ToAssigedClinic.UC_UCF_TAC_ClinicName ?? string.Empty;
                    appointmentReqeustList.Add((Convert.ToInt32(uC_UCF_ToAssigedClinic.UC_UCF_TAC_ClinicID), appointmentType)); //add to list

                    Debug.WriteLine("\nMainForm => Appointment Type: " + appointmentType);
                    Debug.WriteLine("MainForm => Appointment ID: " + Convert.ToInt32(uC_UCF_ToAssigedClinic.UC_UCF_TAC_ClinicID));

                }
            }

            SharedData.medicalEvent.Raw_AppointmentsRequests.AddRange(appointmentReqeustList); // Add the list to Class List
            SharedData.medicalEvent.IsAppointmentRequest = true; 

            #region Debug Outputs

            Debug.WriteLine("\n\n --- List ---"); //!!! Debugging
            foreach (var item in appointmentReqeustList)
            {
                Debug.WriteLine($"Appointment  ID: {item.Item1}, Type: {item.Item2}");
            }

            Debug.WriteLine("\n\n --- List From Class ---"); //!!! Debugging
            foreach (var item in SharedData.medicalEvent.Raw_AppointmentsRequests)
            {
                Debug.WriteLine($"Appointment  ID: {item.Item1}, Type: {item.Item2}");
            }

            #endregion

        }

        private void RequestAppointment1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _parentForm.Show();
        }
    }
}
