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
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HMS_Software_V2.AdmissionOfficer
{
    /// <summary>
    /// Interaction logic for AO_AdmitPatient.xaml
    /// </summary>
    public partial class AO_AdmitPatient : Window
    {
        public AO_AdmitPatient()
        {
            InitializeComponent();

            MyLoadBasicDetails();
        }

        private void MyLoadBasicDetails()
        {
            patientName_lbl.Content = SharedData.admissioOfficer.P_NameWithIinitials;
            patientAge_lbl.Content = SharedData.admissioOfficer.P_Age;
            patientGender_lbl.Content = SharedData.admissioOfficer.P_Gender;
            patientRID_lbl.Content = SharedData.admissioOfficer.P_RegistrationID;

            urgentStatus_lbl.Content = SharedData.admissioOfficer.Is_Urgent ? "Urgent" : "Not Urgent";
            doctorReqeustComeFrom_lbl.Content = SharedData.admissioOfficer.SendFrom_Location;

            AdmissionOfficerName_lbl.Content = SharedData.userData.UserName;

            referalNote_tbx.Text = SharedData.admissioOfficer.P_ReferralNote;

            todayDate.Content = DateTime.Now.ToString("dd MMMM yyyy");
            todayTime.Content = DateTime.Now.ToString("hh:mm tt");


            using (SqlConnection connection = new Database_Connector().GetConnection())
            {
                #region Getting Admission Officer ID
                int admissionOfficeID = SharedData.userData.UserID;
                if (admissionOfficeID == 0)
                {
                    MessageBox.Show("Error: User ID is not set.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                #endregion

                #region Search Admit Requested Doctor Details FROM DB
                string query1 = "SELECT D_NameWithInitials, D_Specialty, D_RegistrationID" +
                            " FROM Doctor WHERE Doctor_ID = @Doctor_ID";

                SqlCommand cmd = new SqlCommand(query1, connection);

                try
                {
                    connection.Open();

                    cmd.Parameters.AddWithValue("@Doctor_ID", SharedData.admissioOfficer.Doctor_ID);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {

                        string doctorName = reader["D_NameWithInitials"].ToString() ?? "";
                        string doctorSpecialty = reader["D_Specialty"].ToString() ?? "";
                        string doctorRID = reader["D_RegistrationID"].ToString() ?? "";

                        doctorName_lbl.Content = doctorName;
                        doctorSpecialty_lbl.Content = doctorSpecialty;
                        doctorRID_lbl.Content = doctorRID;

                    }
                    else
                    {
                        MessageBox.Show("No patient found or patient is already an inpatient.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("\nError1: \n" + ex.Message);
                    MessageBox.Show("Error1: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    // Close the connection
                    connection.Close();
                } 
                #endregion
            }

        }

        #region Control Checkbox ON OFF
        private void WardNo_Select_CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            WardNo_Select_CheckBox.IsChecked = true;
            ETU_Selecte_CheckBox.IsChecked = false;
            IsPCU_Select_CheckVox.IsChecked = false;

            WardNo_tbx.IsEnabled = true;
        }

        private void ETU_Selecte_CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            WardNo_Select_CheckBox.IsChecked = false;
            ETU_Selecte_CheckBox.IsChecked = true;
            IsPCU_Select_CheckVox.IsChecked = false;

            WardNo_tbx.IsEnabled = false;
        }

        private void IsPCU_Select_CheckVox_Checked(object sender, RoutedEventArgs e)
        {
            WardNo_Select_CheckBox.IsChecked = false;
            ETU_Selecte_CheckBox.IsChecked = false;
            IsPCU_Select_CheckVox.IsChecked = true;

            WardNo_tbx.IsEnabled = false;
        }
        #endregion

        int InputWardNo = 0;
        private void Admit_btn_Click(object sender, RoutedEventArgs e)
        {
            
            #region Check/Validate For Admit Location Selections
            if (WardNo_Select_CheckBox.IsChecked == true)
            {
                // Check if the WardNo_tbx contains only numbers
                bool isNumeric = int.TryParse(WardNo_tbx.Text, out int wardNumber);
                if (!isNumeric)
                {
                    MessageBox.Show("Only add the Ward Number", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                InputWardNo = wardNumber;
            }
            else if (ETU_Selecte_CheckBox.IsChecked == true)
            {
                InputWardNo = 21;
            }
            else if (IsPCU_Select_CheckVox.IsChecked == true)
            {
                InputWardNo = 22;
            }
            else
            {
                MessageBox.Show("Please select a ward.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            } 
            #endregion

            MyAddPatientAdmiteDataToDatabase();
        }

        private void MyAddPatientAdmiteDataToDatabase()
        {
            using (SqlConnection connection = new Database_Connector().GetConnection())
            {
                
                try
                {
                    connection.Open();

                    #region INSERT to Admitted_Patient Table

                    string query1 = "INSERT INTO Admitted_Patients" +
                            " (Patient_ID, AP_Condition, AP_VisiteTotalRounds, AP_AdmittedDate, AP_AdmittedTime, AP_Ward)" +
                            " VALUES(@Patient_ID, @AP_Condition, @AP_VisiteTotalRounds, @AP_AdmittedDate, @AP_AdmittedTime, @AP_Ward)";

                    using (SqlCommand command = new SqlCommand(query1, connection))
                    {
                       
                        command.Parameters.AddWithValue("@Patient_ID", SharedData.admissioOfficer.PatientID); 
                        command.Parameters.AddWithValue("@AP_Condition", "Just Admitted"); 
                        command.Parameters.AddWithValue("@AP_VisiteTotalRounds", 0); 
                        command.Parameters.AddWithValue("@AP_AdmittedDate", DateTime.Now.ToString("dd MMMM yyyy"));
                        command.Parameters.AddWithValue("@AP_AdmittedTime", DateTime.Now.ToString("hh:mm tt")); 
                        command.Parameters.AddWithValue("@AP_Ward", InputWardNo); 

                        command.ExecuteNonQuery();
                    }

                    #endregion

                    #region UPDATE Doc_PatientAdmit_Request Table
                    string query2 = "UPDATE Doc_PatientAdmit_Request SET Is_AdmittedToWard = 1 WHERE DocPatientAdmitRequest_ID = @DocPatientAdmitRequest_ID";

                    using (SqlCommand command2 = new SqlCommand(query2, connection))
                    {
                        command2.Parameters.AddWithValue("@DocPatientAdmitRequest_ID", SharedData.admissioOfficer.PatientAdmitRequestID);

                        command2.ExecuteNonQuery();
                    }
                    #endregion

                    #region UPDATE Patient Table
                    string query3 = "UPDATE Patient SET P_CurrentStatus = 'In-Patient' WHERE Patient_ID = @Patient_ID";

                    using (SqlCommand command3 = new SqlCommand(query3, connection))
                    {
                        command3.Parameters.AddWithValue("@Patient_ID", SharedData.admissioOfficer.PatientID);

                        command3.ExecuteNonQuery();
                    } 
                    #endregion

                }
                catch (Exception ex)
                {
                    Debug.WriteLine("\nError1: \n" + ex.Message);
                    MessageBox.Show("Error1: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                finally
                {
                    connection.Close();
                }
            }
            this.Close();

        }

        private void AO_AdmitPatient1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            AO_Dashboard ao_Dashboard = new AO_Dashboard();
            ao_Dashboard.Show();
        }
    }
}
