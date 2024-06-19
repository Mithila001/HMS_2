using HMS_Software_V2._DataManage_Classes;
using HMS_Software_V2.Doctor_ClincOPD;
using HMS_Software_V2.Doctor_Ward.UserControls_DW;
using HMS_Software_V2.General_Purpose;
using HMS_Software_V2.Nurse_Ward.NuresWard_UserControls;
using HMS_Software_V2.Reception.R_UserControls;
using HMS_Software_V2.UserLogin_Page;
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

namespace HMS_Software_V2.Nurse_Ward
{
    /// <summary>
    /// Interaction logic for NW_Dashboard.xaml
    /// </summary>
    public partial class NW_Dashboard : Window
    {
        public NW_Dashboard()
        {
            InitializeComponent();


            //MyTemporary();

            MyDisplayBasicData();

            MyDisplayPatients();

        }

        private void MyTemporary()
        {
            HMS_Software_V2._DataManage_Classes.SharedData.Ward_Nurse = new HMS_Software_V2._DataManage_Classes.Ward_Nurse(); // Get a new copy of the template
            SharedData.Ward_Nurse.NurseID = 4;
            SharedData.Ward_Nurse.NurseName = "J C Kalubovial";
            SharedData.Ward_Nurse.WardName = "General Ward";
            SharedData.Ward_Nurse.NureseLicenceNumber = "Nurse-0001";
            SharedData.Ward_Nurse.WardNumber = 1;
        }
        private void MyDisplayBasicData()
        {
            nurseName_lbl.Content = SharedData.Ward_Nurse.NurseName;
            nurseLicenceNo_lbl.Content = SharedData.Ward_Nurse.NureseLicenceNumber;
            wardName_lbl.Content = SharedData.Ward_Nurse.WardName;
            wardNumber_lbl.Content = "No:"+SharedData.Ward_Nurse.WardNumber+"";

            todayDate_lbl.Content = DateTime.Now.ToString("dd/MM/yyyy");
            todaytime_lbl.Content = DateTime.Now.ToString("hh:mm:ss tt");


            using (SqlConnection connection = new Database_Connector().GetConnection())
            {
                try
                {
                    connection.Open();

                    #region Get Total Pending Patients Count
                    string query2 = "SELECT COUNT(*) FROM Admitted_Patients_VisitEvent WHERE Is_VisistedByDoctor =1 AND Is_RoundTimeOut = 0 AND Is_VisitedByNurse = 0";
                    using (SqlCommand command2 = new SqlCommand(query2, connection))
                    {

                        int count = (int)command2.ExecuteScalar();
                        totalPending_lbl.Content = count.ToString();

                    }
                    #endregion

                    #region Get Total Completed Patients Count
                    string query3 = "SELECT COUNT(*) FROM Admitted_Patients_VisitEvent WHERE Is_VisistedByDoctor =1 AND Is_RoundTimeOut = 0 AND Is_VisitedByNurse = 1";
                    using (SqlCommand command2 = new SqlCommand(query3, connection))
                    {

                        int count = (int)command2.ExecuteScalar();
                        totalCompleted_lbl.Content = count.ToString();

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

        private async void MyDisplayPatients()
        {
            using (SqlConnection connection = new Database_Connector().GetConnection())
            {
                string query1 = "SELECT APV.Patient_ID, APV.P_Condition, APV.N_TreatmentStatus, APV.Is_VisistedByDoctor, APV.P_MedicalEventID," +
                "P.P_NameWithIinitials, P.P_Age, P.P_Gender, P.P_RegistrationID " +
                "FROM Admitted_Patients_VisitEvent APV " +
                "INNER JOIN Patient P ON P.Patient_ID = APV.Patient_ID " +
                "WHERE APV.Is_VisistedByDoctor = 1 AND Is_RoundTimeOut = 0 " +
                "ORDER BY CASE WHEN APV.N_TreatmentStatus = 'Waiting' THEN 0 ELSE 1 END, APV.N_TreatmentStatus";

                SqlCommand cmd = new SqlCommand(query1, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {

                        UC_NW_ToTreatPatients uC_NW_ToTreatPatients = new UC_NW_ToTreatPatients(this);

                        string patientName = reader["P_NameWithIinitials"].ToString() ?? "Error";
                        string patientRID = reader["P_RegistrationID"].ToString() ?? "Error";
                        string patientGender = reader["P_Gender"].ToString() ?? "Error";
                        string patientCondition = reader["P_Condition"].ToString() ?? "Error";
                        string patientAge = reader["P_Age"].ToString() ?? "Error";

                        uC_NW_ToTreatPatients.patientName_lbl.Content = patientName;
                        uC_NW_ToTreatPatients.patientRID_lbl.Content = patientRID;
                        uC_NW_ToTreatPatients.pateintGender_lbl.Content = patientGender;
                        uC_NW_ToTreatPatients.patientMedicalCondition_lbl.Content = patientCondition;


                        uC_NW_ToTreatPatients.PatientID = Convert.ToInt32(reader["Patient_ID"]);
                        uC_NW_ToTreatPatients.PatientName = patientName;
                        uC_NW_ToTreatPatients.PatientCondition = patientCondition;
                        uC_NW_ToTreatPatients.PatientGender = patientGender;
                        uC_NW_ToTreatPatients.PatientAge = patientAge;

                        uC_NW_ToTreatPatients.PatientMedicalEventID = Convert.ToInt32(reader["P_MedicalEventID"]);
                        Debug.WriteLine($"\nP_MedicalEventID : {Convert.ToInt32(reader["P_MedicalEventID"])}");


                        #region Change Color
                        string treatmentStatus = reader["N_TreatmentStatus"].ToString() ?? "Error";
                        Debug.WriteLine($"Patient Statuss: {treatmentStatus}");

                        uC_NW_ToTreatPatients.PatientTreatmentStatus = treatmentStatus;

                        if (treatmentStatus == "Waiting")
                        {
                            // Convert hex color string to SolidColorBrush
                            uC_NW_ToTreatPatients.patientStatusColor_border.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ff6666"));

                        }
                        else if (treatmentStatus == "In Progress")
                        {
                            uC_NW_ToTreatPatients.patientStatusColor_border.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ffcd65"));
                        }
                        else if (treatmentStatus == "Completed")
                        {
                            uC_NW_ToTreatPatients.patientStatusColor_border.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#53d948"));
                        }
                        else
                        {
                            uC_NW_ToTreatPatients.patientStatusColor_border.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("##2a2927"));
                        } 
                        #endregion



                        #region Debug Outputs
                        Debug.WriteLine($"\n\n---------------------- Admitted Patient Visit ----------------------\n");

                        Debug.WriteLine($"Patient Statuss: {treatmentStatus}");
                        Debug.WriteLine($"Patient RID: {patientRID}");
                       
                        Debug.WriteLine($"Patient Medical Condition: {patientCondition}");


                        #endregion





                        // Add the user control to the parent container
                        showToTratePatients_WrapP.Children.Add(uC_NW_ToTreatPatients);

                        await Task.Delay(0005); // 1000ms = 1s

                        showToTratePatients_WrapP.SizeChanged += (sender, e) =>
                        {
                            uC_NW_ToTreatPatients.Width = showToTratePatients_WrapP.ActualWidth - uC_NW_ToTreatPatients.Margin.Left - uC_NW_ToTreatPatients.Margin.Right;
                            //Debug.WriteLine("\n Width:" + uC_NW_ToTreatPatients.Width);
                            //Debug.WriteLine("\n clinicAvailability_WrapP.ActualWidth:" + showToTratePatients_WrapP.ActualWidth);
                        };
                        uC_NW_ToTreatPatients.Width = showToTratePatients_WrapP.ActualWidth - uC_NW_ToTreatPatients.Margin.Left - uC_NW_ToTreatPatients.Margin.Right;

                       
                    }
                    reader.Close();
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
        private async void WaitTimer()
        {
            Debug.WriteLine("\n WaitTimer");
            await Task.Delay(5000);

           
        }

        public bool IsGoingToLoginPage = true;
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsGoingToLoginPage)
            {
                UserLogin userLogin = new UserLogin();
                userLogin.Show();
            }
            
            
        }
    }
}
