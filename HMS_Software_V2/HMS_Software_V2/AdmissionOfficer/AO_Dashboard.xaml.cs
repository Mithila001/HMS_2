using HMS_Software_V2._DataManage_Classes;
using HMS_Software_V2.AdmissionOfficer.UserControls_AO;
using HMS_Software_V2.General_Purpose;
using HMS_Software_V2.Reception.R_UserControls;
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

namespace HMS_Software_V2.AdmissionOfficer
{
    /// <summary>
    /// Interaction logic for AO_Dashboard.xaml
    /// </summary>
    public partial class AO_Dashboard : Window
    {
        public AO_Dashboard()
        {
            InitializeComponent();


            HMS_Software_V2._DataManage_Classes.SharedData.userData = new HMS_Software_V2._DataManage_Classes.UserData(); // Get a new copy of the template
            SharedData.userData.UserID = 3; //Temporary
            SharedData.userData.UserName = "V C K Ukkupala"; //Temporary

            LoadClinicData();
        }


        private void LoadClinicData()
        {
            using (SqlConnection connection = new Database_Connector().GetConnection())
            {
                string query1 = "SELECT DPR.PatientID, DPR.Doctor_ID, DPR.P_ReferralNote, DPR.Is_Urgent, DPR.SendFrom_Location, DPR.DocPatientAdmitRequest_ID, " +
                "P.P_NameWithIinitials, P.P_Age, P.P_Gender, P.P_RegistrationID, " +
                "D.D_NameWithInitials, D.D_Specialty " +
                "FROM Doc_PatientAdmit_Request DPR " +
                "INNER JOIN Patient P ON P.Patient_ID = DPR.PatientID " +
                "INNER JOIN Doctor D ON D.Doctor_ID = DPR.Doctor_ID " +
                "WHERE Is_AdmittedToWard = 0";

                SqlCommand cmd = new SqlCommand(query1, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        UC_AO_AdmitRequest uC_AO_AdmitRequest = new UC_AO_AdmitRequest(this);
                        uC_AO_AdmitRequest.patientName_lbl.Content = reader["P_NameWithIinitials"].ToString();
                        uC_AO_AdmitRequest.patientRID_lbl.Content = reader["P_RegistrationID"].ToString();
                        uC_AO_AdmitRequest.patientGender_lbl.Content = reader["P_Gender"].ToString();

                        uC_AO_AdmitRequest.PatientID = Convert.ToInt32(reader["PatientID"]);
                        uC_AO_AdmitRequest.Doctor_ID = Convert.ToInt32(reader["Doctor_ID"]);
                        uC_AO_AdmitRequest.P_ReferralNote = reader["P_ReferralNote"].ToString() ?? "Error";
                        uC_AO_AdmitRequest.Is_Urgent = Convert.ToBoolean(reader["Is_Urgent"]);
                        uC_AO_AdmitRequest.SendFrom_Location = reader["SendFrom_Location"].ToString() ?? "Error";
                        uC_AO_AdmitRequest.P_NameWithIinitials = reader["P_NameWithIinitials"].ToString() ?? "Error";
                        uC_AO_AdmitRequest.P_Age = reader["P_Age"].ToString() ?? "Error";
                        uC_AO_AdmitRequest.P_Gender = reader["P_Gender"].ToString() ?? "Error";
                        uC_AO_AdmitRequest.P_RegistrationID = reader["P_RegistrationID"].ToString() ?? "Error";
                        uC_AO_AdmitRequest.D_NameWithInitials = reader["D_NameWithInitials"].ToString() ?? "Error";
                        uC_AO_AdmitRequest.D_Specialty = reader["D_Specialty"].ToString() ?? "Error";
                        uC_AO_AdmitRequest.PatientAdmitRequestID = Convert.ToInt32(reader["DocPatientAdmitRequest_ID"]);


                        //SharedData.admissioOfficer.PatientID = Convert.ToInt32(reader["PatientID"]);
                        //SharedData.admissioOfficer.Doctor_ID = Convert.ToInt32(reader["Doctor_ID"]);
                        //SharedData.admissioOfficer.P_ReferralNote = reader["P_ReferralNote"].ToString() ?? "Error";
                        //SharedData.admissioOfficer.Is_Urgent = Convert.ToBoolean(reader["Is_Urgent"]);
                        //SharedData.admissioOfficer.SendFrom_Location = reader["SendFrom_Location"].ToString() ?? "Error";
                        //SharedData.admissioOfficer.P_NameWithIinitials = reader["P_NameWithIinitials"].ToString() ?? "Error";
                        //SharedData.admissioOfficer.P_Age = reader["P_Age"].ToString() ?? "Error";
                        //SharedData.admissioOfficer.P_Gender = reader["P_Gender"].ToString() ?? "Error";
                        //SharedData.admissioOfficer.P_RegistrationID = reader["P_RegistrationID"].ToString() ?? "Error";
                        //SharedData.admissioOfficer.D_NameWithInitials = reader["D_NameWithInitials"].ToString() ?? "Error";
                        //SharedData.admissioOfficer.D_Specialty = reader["D_Specialty"].ToString() ?? "Error";

                        #region Debug Outputs
                        Debug.WriteLine($"\n\n---------------------- Patient Admit Request ----------------------\n");
                        //Debug.WriteLine($"PatientID: {SharedData.admissioOfficer.PatientID}");
                        //Debug.WriteLine($"Doctor_ID: {SharedData.admissioOfficer.Doctor_ID}");
                        //Debug.WriteLine($"P_ReferralNote: {SharedData.admissioOfficer.P_ReferralNote}");
                        //Debug.WriteLine($"Is_Urgent: {SharedData.admissioOfficer.Is_Urgent}");
                        //Debug.WriteLine($"SendFrom_Location: {SharedData.admissioOfficer.SendFrom_Location}");
                        //Debug.WriteLine($"P_NameWithIinitials: {SharedData.admissioOfficer.P_NameWithIinitials}");
                        //Debug.WriteLine($"P_Age: {SharedData.admissioOfficer.P_Age}");
                        //Debug.WriteLine($"P_Gender: {SharedData.admissioOfficer.P_Gender}");
                        //Debug.WriteLine($"P_RegistrationID: {SharedData.admissioOfficer.P_RegistrationID}");
                        //Debug.WriteLine($"D_NameWithInitials: {SharedData.admissioOfficer.D_NameWithInitials}");
                        //Debug.WriteLine($"D_Specialty: {SharedData.admissioOfficer.D_Specialty}"); 
                        #endregion



                        // Adjust the width of the user control to match the width of the parent container
                        AdmitRequest_WrapP.SizeChanged += (sender, e) =>
                        {
                            uC_AO_AdmitRequest.Width = AdmitRequest_WrapP.ActualWidth - uC_AO_AdmitRequest.Margin.Left - uC_AO_AdmitRequest.Margin.Right;
                           
                        };
                        uC_AO_AdmitRequest.Width = AdmitRequest_WrapP.ActualWidth - uC_AO_AdmitRequest.Margin.Left - uC_AO_AdmitRequest.Margin.Right;

                        AdmitRequest_WrapP.Children.Add(uC_AO_AdmitRequest);
                    }
                    reader.Close();


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

        private void DirectAdmit_btn_Click(object sender, RoutedEventArgs e)
        {
            AO_DirectAdmit_Popup aO_DirectAdmit_Popup = new AO_DirectAdmit_Popup(this);
            aO_DirectAdmit_Popup.ShowDialog();
        }
    }
}
