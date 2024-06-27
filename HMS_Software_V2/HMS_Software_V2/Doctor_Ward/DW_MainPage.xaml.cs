using HMS_Software_V2._DataManage_Classes;
using HMS_Software_V2.AdmissionOfficer.UserControls_AO;
using HMS_Software_V2.Doctor_Ward.UserControls_DW;
using HMS_Software_V2.General_Purpose;
using HMS_Software_V2.UserLogin_Page;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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

namespace HMS_Software_V2.Doctor_Ward
{
    /// <summary>
    /// Interaction logic for DW_MainPage.xaml
    /// </summary>
    public partial class DW_MainPage : Window
    {
        public DW_MainPage()
        {
            InitializeComponent();

            // Check/Create Ward Medical Visit Event for the Patients
            Ward_MedicalEventManager ward_MedicalEventManager = new Ward_MedicalEventManager();
            ward_MedicalEventManager.MyStart();


            Debug.WriteLine("\n\n============= DW_MainPage =============\n\n");

            //Temporory();

            MyLoad_BasicDetails();

            MyLoad_TopBarDetails();

            MyLoad_Patients();


            


        }

        private void Temporory()
        {
            
        }

        private void MyLoad_BasicDetails()
        {
            TodayDate_lbl.Content = DateTime.Now.ToString("yyyy/MM/dd");
            TodayTime_lbl.Content = DateTime.Now.ToString("hh:mm tt");

            doctorName_lbl.Content = SharedData.Ward_Doctor.DoctorName;
            doctorSpecialty_lbl.Content = SharedData.Ward_Doctor.DoctorSpeciality;

            wardName_lbl.Content = $"{SharedData.Ward_Doctor.WardName}({SharedData.Ward_Doctor.WardID})";



        }

        int AdmitRoundManagerID = 0;
        private void MyLoad_TopBarDetails()
        {


            using (SqlConnection connection = new Database_Connector().GetConnection())
            {

                try
                {
                    connection.Open();


                    #region Find Witch Ward Round
                    string query1 = "SELECT TOP 1 AdmitMedicalRoundManager_ID, Round_1, Round_2, Round_3 FROM Admit_MedicalRoundManager ORDER BY AdmitMedicalRoundManager_ID DESC";

                    using (SqlCommand command1 = new SqlCommand(query1, connection))
                    {
                        using (SqlDataReader reader = command1.ExecuteReader())
                        {
                            if (reader.Read()) // Since we're only expecting one record, we use if instead of while
                            {
                                AdmitRoundManagerID = (int)reader["AdmitMedicalRoundManager_ID"];
                                SharedData.Ward_Doctor.RoundManagerID = AdmitRoundManagerID;

                                bool round1 = (bool)reader["Round_1"];
                                bool round2 = (bool)reader["Round_2"];
                                bool round3 = (bool)reader["Round_3"];

                                if (round3)
                                {
                                    wardRoundCount_lbl.Content = "3";
                                }
                                else if (round2)
                                {
                                    wardRoundCount_lbl.Content = "3";
                                }
                                else if (round1)
                                {
                                    wardRoundCount_lbl.Content = "2";
                                }
                                else
                                {
                                    wardRoundCount_lbl.Content = "1";
                                }

                            }
                        }
                    }
                    #endregion

                    Debug.WriteLine("AdmitRoundManagerID: " + AdmitRoundManagerID);

                    #region Get Pending Total Count
                    string query2 = "SELECT COUNT(*) FROM Admitted_Patients_VisitEvent WHERE VisitPerDay_ID = @VisitPerDay_ID AND Is_VisistedByDoctor = 0";
                    using (SqlCommand command2 = new SqlCommand(query2, connection))
                    {
                        if (AdmitRoundManagerID != 0)
                        {
                            command2.Parameters.AddWithValue("@VisitPerDay_ID", AdmitRoundManagerID);

                            int count = (int)command2.ExecuteScalar();
                            totalPending.Content = count.ToString();
                        }
                        else
                        {
                            MessageBox.Show("AdmitRoundManagerID is 0", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }

                    #endregion


                    #region Get Total Completed
                    string query3 = "SELECT COUNT(*) FROM Admitted_Patients_VisitEvent WHERE VisitPerDay_ID = @VisitPerDay_ID AND Is_VisistedByDoctor = 1";
                    using (SqlCommand command2 = new SqlCommand(query3, connection))
                    {
                        if (AdmitRoundManagerID != 0)
                        {
                            command2.Parameters.AddWithValue("@VisitPerDay_ID", AdmitRoundManagerID);

                            int count = (int)command2.ExecuteScalar();
                            totalCompleted.Content = count.ToString();
                        }
                        else
                        {
                            MessageBox.Show("AdmitRoundManagerID is 0", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }

                    #endregion


                    #region Get Doctor Total Completed
                    string query4 = "SELECT COUNT(*) FROM Admitted_Patients_VisitEvent WHERE VisitPerDay_ID = @VisitPerDay_ID AND Visited_Doctor_ID = @Visited_Doctor_ID AND Is_VisistedByDoctor = 1";
                    using (SqlCommand command2 = new SqlCommand(query4, connection))
                    {
                        if (AdmitRoundManagerID != 0)
                        {
                            command2.Parameters.AddWithValue("@Visited_Doctor_ID", SharedData.Ward_Doctor.DoctorID);
                            command2.Parameters.AddWithValue("@VisitPerDay_ID", AdmitRoundManagerID);

                            int count = (int)command2.ExecuteScalar();
                            doctorCompleted_lbl.Content = count.ToString();
                        }
                        else
                        {
                            MessageBox.Show("AdmitRoundManagerID is 0", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }

                    #endregion





                }
                catch (Exception ex)
                {
                    Debug.WriteLine("\n MyMedicalEventManger_Start: \n" + ex.Message);
                    MessageBox.Show("MainPage Error1: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                finally
                {
                    connection.Close();
                }

            }
        }

        private void MyLoad_Patients()
        {
            using (SqlConnection connection = new Database_Connector().GetConnection())
            {
                string query1 = "SELECT APV.Patient_ID, APV.P_Condition, APV.Visite_Round, APV.N_TreatmentStatus, APV.Total_VisitRounds, APV.Is_VisistedByDoctor, APV.Is_RoundTimeOut, " +
                "P.P_NameWithIinitials, P.P_Age, P.P_Gender, P.P_RegistrationID " +
                "FROM Admitted_Patients_VisitEvent APV " +
                "INNER JOIN Patient P ON P.Patient_ID = APV.Patient_ID " +
                "WHERE APV.VisitPerDay_ID = @AdmitRoundManagerID AND APV.Is_RoundTimeOut = 0 " +
                "ORDER BY APV.Is_VisistedByDoctor ASC";

                SqlCommand cmd = new SqlCommand(query1, connection);
                cmd.Parameters.AddWithValue("@AdmitRoundManagerID", AdmitRoundManagerID);

                try
                {
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        UC_DW_WardPatients uC_DW_WardPatients = new UC_DW_WardPatients(this);


                        string patientName = reader["P_NameWithIinitials"].ToString() ?? "Error";
                        string patientRID = reader["P_RegistrationID"].ToString() ?? "Error";
                        string patientGender = reader["P_Gender"].ToString() ?? "Error";
                        string patientCondition = reader["P_Condition"].ToString() ?? "Error";

                        uC_DW_WardPatients.patientName_lbl.Content = patientName;
                        uC_DW_WardPatients.patientRID_lbl.Content = patientRID;
                        uC_DW_WardPatients.patientAge_lbl.Content = patientGender;
                        uC_DW_WardPatients.patientMedicalCondition_lbl.Content = patientCondition;

                        uC_DW_WardPatients.PatientID = Convert.ToInt32(reader["Patient_ID"]);
                        uC_DW_WardPatients.PatientConditon = reader["P_Condition"].ToString() ?? "Error";
                        uC_DW_WardPatients.PatientVisitRound = Convert.ToInt32(reader["Visite_Round"]);
                        uC_DW_WardPatients.TreatmentStatus = reader["N_TreatmentStatus"].ToString() ?? "Error";
                        uC_DW_WardPatients.TotalVisitRouds = Convert.ToInt32(reader["Total_VisitRounds"]);
                        uC_DW_WardPatients.PatientName = reader["P_NameWithIinitials"].ToString() ?? "Error";
                        uC_DW_WardPatients.PatientGender = reader["P_Gender"].ToString() ?? "Error";
                        uC_DW_WardPatients.PatientAge = reader["P_Age"].ToString() ?? "Error";
                        uC_DW_WardPatients.PatientRID = reader["P_RegistrationID"].ToString() ?? "Error";
                        
                        bool isVisitedByDoctor = Convert.ToBoolean(reader["Is_VisistedByDoctor"]);

                        Debug.WriteLine($"---------------------------------------------------------\n\nisVisitedByDoctor: {isVisitedByDoctor}\n\n");

                        uC_DW_WardPatients.IsVisitedByTheDoctor = isVisitedByDoctor;

                        if (isVisitedByDoctor)
                        {
                            uC_DW_WardPatients.colorIndicator_Retangle.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#46ff46"));
                            Debug.WriteLine($"Green");
                        }
                        else
                        {
                            uC_DW_WardPatients.colorIndicator_Retangle.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#fd3535"));
                            Debug.WriteLine($"Red");
                        }
                        


                        #region Debug Outputs
                        Debug.WriteLine($"\n\n---------------------- Admitted Patient Visit ----------------------\n");

                        Debug.WriteLine($"Patient Name: {patientName}");
                        Debug.WriteLine($"Patient RID: {patientRID}");
                        Debug.WriteLine($"Patient Gender: {patientGender}");
                        Debug.WriteLine($"Patient Medical Condition: {patientCondition}");

                        Debug.WriteLine($"Patient ID: {Convert.ToInt32(reader["Patient_ID"])}");
                        Debug.WriteLine($"Patient Condition: {reader["P_Condition"].ToString() ?? "Error"}");
                        Debug.WriteLine($"Patient Visit Round: {Convert.ToInt32(reader["Visite_Round"])}");
                        Debug.WriteLine($"Treatment Status: {reader["N_TreatmentStatus"].ToString() ?? "Error"}");
                        Debug.WriteLine($"Total Visit Rounds: {Convert.ToInt32(reader["Total_VisitRounds"])}");
                        Debug.WriteLine($"Patient Name: {reader["P_NameWithIinitials"].ToString() ?? "Error"}");
                        Debug.WriteLine($"Patient Gender: {reader["P_Gender"].ToString() ?? "Error"}");
                        Debug.WriteLine($"Patient Age: {reader["P_Age"].ToString() ?? "Error"}");
                        Debug.WriteLine($"Patient Registration ID: {reader["P_RegistrationID"].ToString() ?? "Error"}");

                        #endregion



                        // Adjust the width of the user control to match the width of the parent container
                        DisplayToCheckPatients_WrapP.SizeChanged += (sender, e) =>
                        {
                            uC_DW_WardPatients.Width = DisplayToCheckPatients_WrapP.ActualWidth - uC_DW_WardPatients.Margin.Left - uC_DW_WardPatients.Margin.Right;

                        };
                        

                        DisplayToCheckPatients_WrapP.Children.Add(uC_DW_WardPatients);
                        //Debug.WriteLine($"\n\nuC_DW_WardPatients.Width: {uC_DW_WardPatients.ActualWidth}");
                        //Debug.WriteLine($"ShowWardPatient_WrapP.ActualWidth: {DisplayToCheckPatients_WrapP.ActualWidth}");
                        //Debug.WriteLine($"uC_DW_WardPatients.Margin.Left: {uC_DW_WardPatients.Margin.Left}");
                        //Debug.WriteLine($"uC_DW_WardPatients.Margin.Right: {uC_DW_WardPatients.Margin.Right}");
                        //Debug.WriteLine($"Final Width: {DisplayToCheckPatients_WrapP.ActualWidth - uC_DW_WardPatients.Margin.Left - uC_DW_WardPatients.Margin.Right}");

                        
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

        public bool IsGoingToUserLoginPage = true;
        private void DW_MainPage1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsGoingToUserLoginPage)
            {
                UserLogin userLogin = new UserLogin();
                userLogin.Show();
            }
            
            
        }
    }
}
