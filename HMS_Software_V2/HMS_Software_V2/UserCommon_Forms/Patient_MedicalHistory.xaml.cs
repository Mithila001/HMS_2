using HMS_Software_V2._DataManage_Classes;
using HMS_Software_V2.Doctor_ClincOPD;
using HMS_Software_V2.Doctor_Ward;
using HMS_Software_V2.Doctor_Ward.UserControls_DW;
using HMS_Software_V2.General_Purpose;
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

namespace HMS_Software_V2.UserCommon_Forms
{
    /// <summary>
    /// Interaction logic for Patient_MedicalHistory.xaml
    /// </summary>

    /// <include>
    /// Required ViewPatientHistory.cs instance with data added to it
    /// </include>
    public partial class Patient_MedicalHistory : Window
    {
        bool IsFromOPD = false;
        DCO_PatientCheck? parentFormre_OPD;
        public Patient_MedicalHistory(DCO_PatientCheck dCO_PatientCheck)
        {
            InitializeComponent();
            this.parentFormre_OPD = dCO_PatientCheck;
            IsFromOPD = true;

            MyLoadBasicData();
            MyLoadMedicalHistory();

            TemporyData();
        }

        bool IsFromWard = false;
        DW_ProgressNote? parentForm_Ward;
        public Patient_MedicalHistory(DW_ProgressNote dW_ProgressNote)
        {
            InitializeComponent();
            this.parentForm_Ward = dW_ProgressNote;
            IsFromWard = true;

            MyLoadBasicData();
            MyLoadMedicalHistory();

            TemporyData();
        }

        private void TemporyData()
        {

           

        }

        private void MyLoadBasicData()
        {
            patientName_lbl.Content = SharedData.viewPatientHistory.PatientName;
            patientRID_lbl.Content = SharedData.viewPatientHistory.PatientRID;

            totalOpdVisits_lbl.Content = TotalOPD_Visits;
            totalWardVisits_lbl.Content = TotalWard_Visits;
        }

        int TotalOPD_Visits = 0;
        int TotalWard_Visits = 0;
        private void MyLoadMedicalHistory()
        {
            using (SqlConnection connection = new Database_Connector().GetConnection())
            {
                string query1 = @"
                        SELECT 
                            PME.*, 
                            P.P_NameWithIinitials, 
                            P.P_Age, 
                            P.P_Gender, 
                            D.D_NameWithInitials 
                        FROM 
                            PatientMedical_Event PME
                        INNER JOIN 
                            Patient P ON PME.Patient_ID = P.Patient_ID
                        INNER JOIN 
                            Doctor D ON PME.PME_Doctor_ID = D.Doctor_ID
                        WHERE 
                            PME.Patient_ID = @Patient_ID";


                SqlCommand cmd = new SqlCommand(query1, connection);
                //cmd.Parameters.AddWithValue("@Patient_ID", SharedData.Ward_Doctor.PatientID);
                cmd.Parameters.AddWithValue("@Patient_ID", SharedData.viewPatientHistory.PatientID);

                try
                {
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {



                        int medicalEventID = Convert.ToInt32(reader["PatientMedicalEvent_ID"]);
                        int patientID = Convert.ToInt32(reader["Patient_ID"]);
                        int visitedDoctorID = Convert.ToInt32(reader["PME_Doctor_ID"]);
                        int visitedNurseID = Convert.ToInt32(reader["PME_Nurse_ID"]);


                        DateTime patientDate = Convert.ToDateTime(reader["PME_Date"]);
                        TimeSpan visitedTimeSpan = (TimeSpan)reader["PME_Time"];
                        string visitedTime = visitedTimeSpan.ToString(@"hh\:mm\:ss");


                        int day = patientDate.Day;
                        string daySuffix = day switch
                        {
                            1 or 21 or 31 => "st",
                            2 or 22 => "nd",
                            3 or 23 => "rd",
                            _ => "th"
                        };
                        string visitedDate = $"{patientDate:MMMM} {day}{daySuffix}, {patientDate:yyyy}";

                        //string visitedTime = visitedTime_get.ToString("hh:mm: tt");

                        string location = reader["PME_Location"].ToString() ?? "Error";

                        if(location == "OPD" || location == "Clinic")
                        {
                            TotalOPD_Visits++;
                        }
                        else if(location == "Ward")
                        {
                            TotalWard_Visits++;
                        }
                        else
                        {
                            //MessageBox.Show("Mismatched Location: " + location, "Error", MessageBoxButton.OK, MessageBoxImage.Error);   
                        }




                        bool isLabRequest = Convert.ToBoolean(reader["PME_Is_LabRequest"]);
                        bool isPrescriptionRequest = Convert.ToBoolean(reader["PME_Is_PrescriptionRequest"]);
                        string p_examinationNotes = reader["PME_PatientExaminationNote"].ToString() ?? "Error";
                        string p_medicalCondition = reader["PME_PatietnMedicalCondition"].ToString() ?? "Error";
                        bool p_isInpatient = Convert.ToBoolean(reader["PME_Is_InPatient"]);
                        string p_monitorRequest = reader["PME_MonitorRequest"].ToString() ?? "";
                        string p_monitor_Results = reader["PME_MonitorRequest_Results"].ToString() ?? "";

                        string patientName = reader["P_NameWithIinitials"].ToString() ?? "Error";
                        string patientAge = reader["P_Age"].ToString() ?? "Error";
                        string patientGender = reader["P_Gender"].ToString() ?? "Error";

                        string doctorName = reader["D_NameWithInitials"].ToString() ?? "Error";



                        UC_UFC_PatientMedicalHistory uC_UFC_PatientMedicalHistory = new UC_UFC_PatientMedicalHistory(medicalEventID);
                  
                        uC_UFC_PatientMedicalHistory.medicalEventDate_lbl.Content = visitedDate;
                        uC_UFC_PatientMedicalHistory.visitedTime_lbl.Content = visitedTime;

                        uC_UFC_PatientMedicalHistory.location_lbl.Content = location;
                        uC_UFC_PatientMedicalHistory.patientName_lbl.Content = patientName;
                        uC_UFC_PatientMedicalHistory.patientAge_lbl.Content = patientAge;
                        uC_UFC_PatientMedicalHistory.patientGender_lbl.Content = patientGender;
                        uC_UFC_PatientMedicalHistory.doctorName_lbl.Content = doctorName;


                        // Adjust the width of the user control to match the width of the parent container
                        DisplayMedicalHistory_WrapP.SizeChanged += (sender, e) =>
                        {
                            uC_UFC_PatientMedicalHistory.Width = DisplayMedicalHistory_WrapP.ActualWidth - uC_UFC_PatientMedicalHistory.Margin.Left - uC_UFC_PatientMedicalHistory.Margin.Right;

                        };

                        DisplayMedicalHistory_WrapP.Children.Add(uC_UFC_PatientMedicalHistory);

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

                    totalOpdVisits_lbl.Content = TotalOPD_Visits;
                    totalWardVisits_lbl.Content = TotalWard_Visits;
                }
            }
        }

        private void Patient_MedicalHistory1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsFromOPD)
            {
                parentFormre_OPD?.Show();
            }
            else if (IsFromWard)
            {
                parentForm_Ward?.Show();
            }
            
            
        }
    }
}
