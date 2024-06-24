using HMS_Software_V2._DataManage_Classes;
using HMS_Software_V2.General_Purpose;
using HMS_Software_V2.UserCommon_Forms;
using Newtonsoft.Json;
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

namespace HMS_Software_V2.Doctor_Ward
{
    /// <summary>
    /// Interaction logic for DW_ProgressNote.xaml
    /// </summary>
    public partial class DW_ProgressNote : Window
    {
        public DW_ProgressNote()
        {
            InitializeComponent();
            MyLoadBasicData();
        }

        private void MyLoadBasicData()
        {
            doctor_Name_lbl.Content = SharedData.Ward_Doctor.DoctorName;
            doctor_RID_lbl.Content = SharedData.Ward_Doctor.DoctorRID;
            doctor_Specialty_lbl.Content = SharedData.Ward_Doctor.DoctorSpeciality;

            wardName_lbl.Content = SharedData.Ward_Doctor.WardName;
            TodayDate_lbl.Content = GetFormattedDateWithSuffix(DateTime.Now);
            TodayTime_lbl.Content = DateTime.Now.ToString("hh:mm tt");

            patient_Name_lbl.Content = SharedData.medicalEvent.PatientName;
            patient_Condition_lbl.Content = SharedData.medicalEvent.PatientMedicalCondition;
            patient_RID_lbl.Content = SharedData.medicalEvent.pationetRID;
            patient_VisitCount_lbl.Content = SharedData.medicalEvent.PatientVisitCount.ToString();
        }

        #region Get the Date in Custom Styled Format
        private string GetFormattedDateWithSuffix(DateTime date)
        {
            string daySuffix = GetDaySuffix(date.Day);
            return date.ToString($"dd'{daySuffix}' MMMM yyyy");
        }

        private string GetDaySuffix(int day)
        {
            if (day >= 11 && day <= 13)
            {
                return "th";
            }
            switch (day % 10)
            {
                case 1: return "st";
                case 2: return "nd";
                case 3: return "rd";
                default: return "th";
            }
        }
        #endregion


        bool Flag_IsChecked_Prescription = false;
        bool Flag_IsChecked_LabRequest = false;
        bool Flag_IsChecked_Monitor = false;

        private void AddPrescription_btn_Click(object sender, RoutedEventArgs e)
        {
            if (!Flag_IsChecked_Prescription)
            {
                // Change the image source
                AddPrescription_image.Source = new BitmapImage(new Uri("pack://application:,,,/HMS_Software_V2;component/Assest/icons/icons8-correct-100.png", UriKind.Absolute));

                AddPrescription_image.Height = 40; // replace with the new height
                AddPrescription_image.Width = 40; // replace with the new width

                Flag_IsChecked_Prescription = true;
            }

            PrescriptionRequest prescriptionRequest = new PrescriptionRequest(this);
            prescriptionRequest.Show();
            this.Hide();

        }

        private void Add_LabRequest_btn_Click(object sender, RoutedEventArgs e)
        {
            if (!Flag_IsChecked_LabRequest)
            {
                // Change the image source
                Add_LabRequest_image.Source = new BitmapImage(new Uri("pack://application:,,,/HMS_Software_V2;component/Assest/icons/icons8-correct-100.png", UriKind.Absolute));

                Add_LabRequest_image.Height = 40; // replace with the new height
                Add_LabRequest_image.Width = 40; // replace with the new width

                Flag_IsChecked_LabRequest = true;
            }

            LabRequests labRequests = new LabRequests(this);
            labRequests.Show();
            this.Hide();

        }

        private void MonitorRequest_btn_Click(object sender, RoutedEventArgs e)
        {
            if (!Flag_IsChecked_Monitor)
            {
                // Change the image source
                MonitorRequest_image.Source = new BitmapImage(new Uri("pack://application:,,,/HMS_Software_V2;component/Assest/icons/icons8-correct-100.png", UriKind.Absolute));

                MonitorRequest_image.Height = 40; // replace with the new height
                MonitorRequest_image.Width = 40; // replace with the new width

                Flag_IsChecked_Monitor = true;
            }

            DW_MonitorRequest dW_MonitorRequest = new DW_MonitorRequest(this);
            dW_MonitorRequest.Show();
            this.Hide();

        }

        private void DW_ProgressNote1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DW_MainPage dW_MainPage = new DW_MainPage();
            dW_MainPage.Show();
        }

        string? ExaminationNote;
        private void Confirm_btn_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Confirm_btn_Click => Method");
            #region Check Medical Conditon is Added
            if (string.IsNullOrEmpty(patientMedicalCondition_tbx.Text))
            {
                MessageBox.Show("Please fill the Patient Medical Condition.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            SharedData.medicalEvent.PatientMedicalCondition = patientMedicalCondition_tbx.Text;
            #endregion

            #region Check If At least one TextBox is Filled


            bool IsAtLeastOneNoteEmpty = false;

            var textBoxes = new List<TextBox>
            {
                historyOfPComplaints_tbx,
                pastMedicalHistory_tbx,
                pastSurgicalHistory_tbx,
                familyHistory_tbx,
                examinationNotes_tbx,
                medication_tbx,
                alergies_tbx,
                socialHistory_tbx
            };

            // Iterate through each textbox and check for content
            foreach (var textBox in textBoxes)
            {
                if (string.IsNullOrEmpty(textBox.Text))
                {
                    textBox.Text = "No Data";
                }
                else
                {
                    IsAtLeastOneNoteEmpty = true;
                }
            }
            #endregion

            #region Assign Examination Notes To a single variable
            if (IsAtLeastOneNoteEmpty)
            {
                Dictionary<string, string> textBoxContents = new Dictionary<string, string>
                {
                    { "History Of Presenting Complaints", historyOfPComplaints_tbx.Text },
                    { "PastMedical History", pastMedicalHistory_tbx.Text },
                    { "PastSurgical History", pastSurgicalHistory_tbx.Text },
                    { "Familty History", familyHistory_tbx.Text },
                    { "Examination Notes", examinationNotes_tbx.Text },
                    { "Medications", medication_tbx.Text },
                    { "Allergies", alergies_tbx.Text },
                    { "Socical History", socialHistory_tbx.Text }
                };

                ExaminationNote = JsonConvert.SerializeObject(textBoxContents, Newtonsoft.Json.Formatting.Indented);
                SharedData.medicalEvent.PersonExaminationNote = ExaminationNote;

            }
            else
            {
                MessageBox.Show("Please fill at least one note.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            #endregion

            MyCreatePatientMedicalEvent();
            MyUpdateingOtherTable();
        }



        int MedicalEventID = 0;
        private void MyCreatePatientMedicalEvent()
        {
            Debug.WriteLine("MyCreatePatientMedicalEvent => Method");


            DateTime dateRaw = new DateTime(SharedData.medicalEvent.Date.Year, SharedData.medicalEvent.Date.Month, SharedData.medicalEvent.Date.Day);
            string date = dateRaw.ToString("yyyy-MM-dd");
            TimeSpan timeSpan = SharedData.medicalEvent.Time.ToTimeSpan();
            string time = timeSpan.ToString(@"hh\:mm");

            Debug.WriteLine("\n MyCreatePatientMedicalEvent(); \n");

            using (SqlConnection connection = new Database_Connector().GetConnection())
            {
                connection.Open();

                try
                {
                    #region Insert Data To Medical Event Table 
                    string query = "INSERT INTO PatientMedical_Event (Patient_ID, PME_Doctor_ID, PME_Nurse_ID, PME_Date, PME_Time, PME_Location, PME_Is_LabRequest," +
                                    " PME_Is_PrescriptionRequest, PME_Is_PatientAppointment, PME_PatientExaminationNote, PME_PatietnMedicalCondition, PME_Is_InPatient, PME_MonitorRequest) "
                                    + "VALUES (@Patient_ID, @PME_Doctor_ID, @PME_Nurse_ID, @PME_Date, @PME_Time, @PME_Location, @PME_Is_LabRequest, @PME_Is_PrescriptionRequest, @PME_Is_PatientAppointment," +
                                      " @PME_PatientExaminationNote, @PME_PatietnMedicalCondition, @PME_Is_InPatient, @PME_MonitorRequest); SELECT SCOPE_IDENTITY();";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {


                        #region Debug Outputs
                        Debug.WriteLine("\n ------ Inserting the following values into PatientMedical_Event ------");
                        Debug.WriteLine("Patient_ID: " + SharedData.medicalEvent.PatientID);
                        Debug.WriteLine("PME_Doctor_ID: " + SharedData.medicalEvent.DoctorID);
                        Debug.WriteLine("PME_Nurse_ID: " + 0);
                        Debug.WriteLine("PME_Date: " + date);
                        Debug.WriteLine("PME_Time: " + time);
                        Debug.WriteLine("PME_Location: " + SharedData.medicalEvent.Location);
                        Debug.WriteLine("PME_Is_LabRequest: " + SharedData.medicalEvent.IsLabRequest);
                        Debug.WriteLine("PME_Is_PrescriptionRequest: " + SharedData.medicalEvent.IsPrescriptionRequest);
                        Debug.WriteLine("PME_Is_PatientAppointment: " + SharedData.medicalEvent.IsAppointmentRequest);
                        Debug.WriteLine("PME_PatientExaminationNote: " + SharedData.medicalEvent.PersonExaminationNote);
                        Debug.WriteLine("PME_PatietnMedicalCondition: " + SharedData.medicalEvent.PatientMedicalCondition);
                        Debug.WriteLine("PME_Is_InPatient: " + SharedData.medicalEvent.IsInpationt); 
                        #endregion




                        cmd.Parameters.AddWithValue("@Patient_ID", SharedData.medicalEvent.PatientID);
                        cmd.Parameters.AddWithValue("@PME_Doctor_ID", SharedData.medicalEvent.DoctorID);
                        cmd.Parameters.AddWithValue("@PME_Nurse_ID", 0);



                        cmd.Parameters.AddWithValue("@PME_Date", date);
                        cmd.Parameters.AddWithValue("@PME_Time", time);
                        cmd.Parameters.AddWithValue("@PME_Location", SharedData.medicalEvent.Location);
                        cmd.Parameters.AddWithValue("@PME_Is_LabRequest", SharedData.medicalEvent.IsLabRequest);
                        cmd.Parameters.AddWithValue("@PME_Is_PrescriptionRequest", SharedData.medicalEvent.IsPrescriptionRequest);
                        cmd.Parameters.AddWithValue("@PME_Is_PatientAppointment", SharedData.medicalEvent.IsAppointmentRequest);
                        cmd.Parameters.AddWithValue("@PME_PatientExaminationNote", SharedData.medicalEvent.PersonExaminationNote);
                        cmd.Parameters.AddWithValue("@PME_PatietnMedicalCondition", SharedData.medicalEvent.PatientMedicalCondition);
                        cmd.Parameters.AddWithValue("@PME_Is_InPatient", SharedData.medicalEvent.IsInpationt);
                        cmd.Parameters.AddWithValue("@PME_MonitorRequest", SharedData.medicalEvent.MonitorRequest);

                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            MedicalEventID = Convert.ToInt32(result);
                            Debug.WriteLine("\nInserted Medical Event ID: " + MedicalEventID);

                        }
                        else
                        {
                            MessageBox.Show("Error: Medical Event ID is not generated", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                    }
                    #endregion



                    #region Add Prescription Requests to the PrescriptionRequest table
                    string query2 = "INSERT INTO Patient_PrescriptionRequest (PatientMedicalEvent_ID, Patient_ID, Doctor_ID, PR_Route, PR_Medicin, PR_Medicin_ID," +
                                    " PR_Dosage, PR_Frequency, PR_Duration, PR_Time, PR_Date, PR__IsCompleted, LabelName) "

                                    + "VALUES (@PatientMedicalEvent_ID, @Patient_ID, @Doctor_ID, @PR_Route, @PR_Medicin, @PR_Medicin_ID, @PR_Dosage, @PR_Frequency, @PR_Duration," +
                                    " @PR_Time, @PR_Date, @PR__IsCompleted, @LabelName);";


                    foreach (var medicin in SharedData.medicalEvent.Raw_Medicin)
                    {
                        if (medicin.Item1 == 0) //If liest item is empty or invalid(0) 
                        {
                            continue;
                        }
                        //medicinReqeustList => Medicin ID, Medicin Type, Dosage, Frequency, Duration, Route
                        using (SqlCommand cmd = new SqlCommand(query2, connection))
                        {
                            cmd.Parameters.AddWithValue("@PatientMedicalEvent_ID", MedicalEventID);
                            cmd.Parameters.AddWithValue("@Patient_ID", SharedData.medicalEvent.PatientID);
                            cmd.Parameters.AddWithValue("@Doctor_ID", SharedData.medicalEvent.DoctorID);
                            cmd.Parameters.AddWithValue("@PR_Route", medicin.Item6); // Replace with actual property name
                            cmd.Parameters.AddWithValue("@PR_Medicin", medicin.Item2); // Replace with actual property name
                            cmd.Parameters.AddWithValue("@PR_Medicin_ID", medicin.Item1); // Replace with actual property name
                            cmd.Parameters.AddWithValue("@PR_Dosage", medicin.Item3); // Replace with actual property name
                            cmd.Parameters.AddWithValue("@PR_Frequency", medicin.Item4); // Replace with actual property name
                            cmd.Parameters.AddWithValue("@PR_Duration", medicin.Item5); // Replace with actual property name
                            cmd.Parameters.AddWithValue("@PR_Time", time);
                            cmd.Parameters.AddWithValue("@PR_Date", date);
                            cmd.Parameters.AddWithValue("@PR__IsCompleted", false);

                            string labelName = new MedicalRequest_LabelCreator().Create_Prescription_Label(medicin.Item2, medicin.Item1, MedicalEventID);
                            cmd.Parameters.AddWithValue("@LabelName", labelName);

                            cmd.ExecuteNonQuery();


                            Debug.WriteLine("\n ------ Inserting the following values into PatientPrescriptionRequest ------");
                            Debug.WriteLine("PatientMedicalEvent_ID: " + MedicalEventID);
                            Debug.WriteLine("Patient_ID: " + SharedData.medicalEvent.PatientID);
                            Debug.WriteLine("Doctor_ID: " + SharedData.medicalEvent.DoctorID);
                            Debug.WriteLine("PR_Route: " + medicin.Item6);
                            Debug.WriteLine("PR_Medicin: " + medicin.Item2);
                            Debug.WriteLine("PR_Medicin_ID: " + medicin.Item1);
                            Debug.WriteLine("PR_Dosage: " + medicin.Item3);
                            Debug.WriteLine("PR_Frequency: " + medicin.Item4);
                            Debug.WriteLine("PR_Duration: " + medicin.Item5);
                            Debug.WriteLine("PR_Time: " + time);
                            Debug.WriteLine("PR_Date: " + date);
                            Debug.WriteLine("PR__IsCompleted: " + false);
                            Debug.WriteLine("\nLabelName: " + labelName);
                        }
                    }
                    #endregion



                    #region Add Lab Requests to the LabRequest table
                    string query3 = "INSERT INTO [dbo].[Patient_LabRequest] " +
                            "([PatientMedicalEvent_ID], [Lab_Specimen_ID], [Lab_Specimen_Name], [Lab_Investigation_ID], [Lab_Investigation_Name], [IsUrgent], [LabelNumber]) " +
                            "VALUES " +
                            "(@PatientMedicalEvent_ID, @Lab_Specimen_ID, @Lab_Specimen_Name, @Lab_Investigation_ID, @Lab_Investigation_Name, @IsUrgent, @LabelNumber)";

                    for (int i = 0; i < SharedData.medicalEvent.Raw_LabInvestigations.Count; i++)
                    {
                        var investigationList = SharedData.medicalEvent.Raw_LabInvestigations[i];
                        var specimenList = SharedData.medicalEvent.Raw_LabSpeciment[i];

                        if (investigationList.Item1 == 0 && specimenList.Item1 == 0) //If liest items is empty or invalid(0) 
                        {
                            continue;
                        }

                        using (SqlCommand cmd = new SqlCommand(query3, connection))
                        {
                            cmd.Parameters.AddWithValue("@PatientMedicalEvent_ID", MedicalEventID);
                            cmd.Parameters.AddWithValue("@Lab_Specimen_ID", specimenList.Item1);
                            cmd.Parameters.AddWithValue("@Lab_Specimen_Name", specimenList.Item2);
                            cmd.Parameters.AddWithValue("@Lab_Investigation_ID", investigationList.Item1);
                            cmd.Parameters.AddWithValue("@Lab_Investigation_Name", investigationList.Item2);
                            cmd.Parameters.AddWithValue("@IsUrgent", SharedData.medicalEvent.IsLabRequestUrgent);
                            string labelName = new MedicalRequest_LabelCreator().Create_LabRequest_Label(investigationList.Item2, investigationList.Item1, specimenList.Item2, specimenList.Item1, MedicalEventID);
                            cmd.Parameters.AddWithValue("@LabelNumber", labelName);

                            cmd.ExecuteNonQuery();

                            #region Debug Outputs
                            Debug.WriteLine("\n ------ Inserting the following values Lab Request ------");
                            Debug.WriteLine("PatientMedicalEvent_ID: " + MedicalEventID);
                            Debug.WriteLine("Lab_Specimen_ID: " + investigationList.Item1);
                            Debug.WriteLine("Lab_Specimen_Name: " + investigationList.Item2);
                            Debug.WriteLine("Lab_Investigation_ID: " + specimenList.Item1);
                            Debug.WriteLine("Lab_Investigation_Name: " + specimenList.Item2);
                            Debug.WriteLine("IsUrgent: " + SharedData.medicalEvent.IsLabRequestUrgent); 
                            Debug.WriteLine("\nLabelNumber: " + labelName);
                            #endregion
                        }
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
        }

        private void MyUpdateingOtherTable()
        {
            Debug.WriteLine("MyUpdateingOtherTable => Method");
            using (SqlConnection connection = new Database_Connector().GetConnection())
            {
                try
                {
                    connection.Open();

                    #region UPDATE Admitted_Patients_VisitEvent
                    string query1 = "UPDATE Admitted_Patients_VisitEvent SET" +
                                    " Visited_Doctor_ID = @Visited_Doctor_ID, Visite_Date= @Visite_Date, Visite_Time = @Visite_Time," +
                                    " P_MedicalEventID = @P_MedicalEventID, Is_VisistedByDoctor = 1, P_Condition = @P_Condition" +
                                    " WHERE Patient_ID = @Patient_ID AND VisitPerDay_ID = @MedicalRoundManagerID";

                    using (SqlCommand cmd = new SqlCommand(query1, connection))
                    {

                        cmd.Parameters.AddWithValue("@Visited_Doctor_ID", SharedData.Ward_Doctor.DoctorID);
                        cmd.Parameters.AddWithValue("@Visite_Date", DateTime.Now.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@Visite_Time", DateTime.Now.ToString("HH:mm"));
                        cmd.Parameters.AddWithValue("@P_MedicalEventID", MedicalEventID);
                        cmd.Parameters.AddWithValue("@Patient_ID", SharedData.medicalEvent.PatientID);
                        cmd.Parameters.AddWithValue("@MedicalRoundManagerID", SharedData.Ward_Doctor.RoundManagerID);
                        cmd.Parameters.AddWithValue("@P_Condition", SharedData.medicalEvent.PatientMedicalCondition);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Debug.WriteLine("Update successful");
                        }
                        else
                        {
                            Debug.WriteLine("Update failed: No rows affected.");
                            MessageBox.Show("Error: Update failed: No rows affected.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                    }
                    #endregion


                    #region UPDATE Admitted_Patients
                    string query2 = "UPDATE Admitted_Patients SET" +
                            " AP_Condition = @AP_Condition, AP_VisiteTotalRounds = AP_VisiteTotalRounds + 1" +
                            " WHERE Patient_ID = @Patient_ID";

                    using (SqlCommand cmd = new SqlCommand(query2, connection))
                    {
                        // Assuming SharedData.medicalEvent.PatientMedicalCondition holds the updated condition
                        cmd.Parameters.AddWithValue("@AP_Condition", SharedData.medicalEvent.PatientMedicalCondition);
                        cmd.Parameters.AddWithValue("@Patient_ID", SharedData.medicalEvent.PatientID);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Debug.WriteLine("Admitted_Patients update successful");
                        }
                        else
                        {
                            Debug.WriteLine("Admitted_Patients update failed: No rows affected.");
                            MessageBox.Show("Error: Admitted_Patients update failed: No rows affected.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    #endregion


                }
                catch (Exception ex)
                {
                    Debug.WriteLine("\nError2: \n" + ex.Message);
                    MessageBox.Show("Error2: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                finally
                {
                    connection.Close();
                }
            }

            this.Close();
        }

        private void ViewPatientMedicalHistory_btn_Click(object sender, RoutedEventArgs e)
        {
            HMS_Software_V2._DataManage_Classes.SharedData.viewPatientHistory = new HMS_Software_V2._DataManage_Classes.ViewPatientHistory(); // Get a new copy of the template

            SharedData.viewPatientHistory.PatientID = SharedData.medicalEvent.PatientID;
            SharedData.viewPatientHistory.PatientName = SharedData.medicalEvent.PatientName;
            SharedData.viewPatientHistory.PatientRID = SharedData.medicalEvent.pationetRID;


            Patient_MedicalHistory patient_MedicalHistory = new Patient_MedicalHistory(this);
            patient_MedicalHistory.Show();
            this.Hide();
        }
    }
}
