using HMS_Software_V2._DataManage_Classes;
using HMS_Software_V2.UserCommon_Forms;
using System;
using System.Collections.Generic;
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
using System.Xml;

using Newtonsoft.Json;
using System.Diagnostics;
using System.Data.SqlClient;
using HMS_Software_V2.General_Purpose;
using HMS_Software_V2.Reception.R_UserControls;
using System.Collections;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HMS_Software_V2.Doctor_ClincOPD
{
    /// <summary>
    /// Interaction logic for DCO_PatientCheck.xaml
    /// </summary>
    /// 
    public partial class DCO_PatientCheck : Window
    {
      
        public DCO_PatientCheck()
        {
            InitializeComponent();

            Debug.WriteLine("\n\n----- DCO_PatientCheck -----\n");



            doctorName_lbl.Content = SharedData.doctorData.doctorName;
            

            MyGetPatientDetails();
        }


        private void MyGetPatientDetails()
        {
            //Display the current date and time
            TodayDate_lbl.Content = DateTime.Now.ToString("dd/MM/yyyy");
            TodayTime_lbl.Content = DateTime.Now.ToString("hh:mm tt");


            string? patientName;
            string? patientAge;
            string? patientGender;

            using (SqlConnection connection = new Database_Connector().GetConnection())
            {
                string query1 = "SELECT * FROM Patient WHERE P_RegistrationID = @patientRID ";

                SqlCommand cmd = new SqlCommand(query1, connection);
                
                cmd.Parameters.AddWithValue("@patientRID", SharedData.medicalEvent.pationetRID);
               
                

                try
                {
                    connection.Open();

                    using(SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Debug.WriteLine("\n\nRetrive Patient ID: \n" + Convert.ToInt32(reader["Patient_ID"]));

                            SharedData.medicalEvent.PatientID = Convert.ToInt32(reader["Patient_ID"]);
                            patientName = reader["P_NameWithIinitials"].ToString();
                            patientAge = reader["P_Age"].ToString();
                            patientGender = reader["P_Gender"].ToString();

                            SharedData.medicalEvent.PatientName = patientName ?? "Not Found";
                            SharedData.medicalEvent.PatientAge = patientAge ?? "Not Found";
                            SharedData.medicalEvent.PatientGender = patientGender ?? "Not Found";


                            //Display the patient details
                            PatientName_lbl.Content = patientName;
                            PatientAge_lbl.Content = patientAge;
                            PatientGender_lbl.Content = patientGender;

                        }
                    }


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



        bool Flag_IsChecked_Prescription = false;
        bool Flag_IsChecked_Appointment = false;
        bool Flag_IsChecked_LabRequest = false;

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

        private void AddAppointment_btn_Click(object sender, RoutedEventArgs e)
        {
            if (!Flag_IsChecked_Appointment)
            {
                // Change the image source
                AddAppointment_image.Source = new BitmapImage(new Uri("pack://application:,,,/HMS_Software_V2;component/Assest/icons/icons8-correct-100.png", UriKind.Absolute));

                AddAppointment_image.Height = 40; // replace with the new height
                AddAppointment_image.Width = 40; // replace with the new width

                Flag_IsChecked_Appointment = true;
            }

            RequestAppointment requestAppointment = new RequestAppointment(this);
            requestAppointment.Show();
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



        private void DCO_PatientCheck1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DCO_Dashboard dCO_Dashboard = new DCO_Dashboard();
            dCO_Dashboard.Show();

            
        }

        private void Admit_btn_Click(object sender, RoutedEventArgs e)
        {

            #region Check If At least one TextBox is Filled

            string ExaminationNote = "";
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
                MessageBox.Show("No data added to the  at least one note.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            #endregion


            SharedData.medicalEvent.PatientMedicalCondition = "In-Patient";
            SharedData.medicalEvent.IsInpationt = true;

            SharedData.medicalEvent.Date = DateOnly.FromDateTime(DateTime.Now);
            SharedData.medicalEvent.Time = TimeOnly.FromDateTime(DateTime.Now);

            SharedData.medicalEvent.DoctorID = SharedData.doctorData.doctorID;

            Debug.WriteLine("\n SharedData.medicalEvent.DoctorID: " + SharedData.doctorData.doctorID + " \n");

            if (SharedData.doctorData.doctorLocation == "OPD")
            {
                SharedData.medicalEvent.Location = "OPD";
            }
            else if (SharedData.doctorData.doctorLocation == "Clinic")
            {
                SharedData.medicalEvent.Location = "Clinic";
                // Warning! Need to Complete
            }
            else
            {
                SharedData.medicalEvent.Location = "Location Not Found";
            }




            int medicalEventID = 0;

            using (SqlConnection connection = new Database_Connector().GetConnection())
            {
                connection.Open();

                try
                {
                    DateTime dateRaw = new DateTime(SharedData.medicalEvent.Date.Year, SharedData.medicalEvent.Date.Month, SharedData.medicalEvent.Date.Day);
                    string date = dateRaw.ToString("yyyy-MM-dd");
                    TimeSpan timeSpan = SharedData.medicalEvent.Time.ToTimeSpan();
                    string time = timeSpan.ToString(@"hh\:mm");


                    #region Insert Data To Medical Event Table 
                    string query = "INSERT INTO PatientMedical_Event (Patient_ID, PME_Doctor_ID, PME_Nurse_ID, PME_Date, PME_Time, PME_Location, PME_Is_LabRequest," +
                                    " PME_Is_PrescriptionRequest, PME_Is_PatientAppointment, PME_PatientExaminationNote, PME_PatietnMedicalCondition, PME_Is_InPatient) "
                                    + "VALUES (@Patient_ID, @PME_Doctor_ID, @PME_Nurse_ID, @PME_Date, @PME_Time, @PME_Location, @PME_Is_LabRequest, @PME_Is_PrescriptionRequest, @PME_Is_PatientAppointment," +
                                      " @PME_PatientExaminationNote, @PME_PatietnMedicalCondition, @PME_Is_InPatient); SELECT SCOPE_IDENTITY();";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {


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

                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            medicalEventID = Convert.ToInt32(result);
                            Debug.WriteLine("\nInserted Medical Event ID: " + medicalEventID);

                        }
                        else
                        {
                            MessageBox.Show("Error: Medical Event ID is not generated", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                    }
                    #endregion



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


        private string GetRichTextBoxText(RichTextBox richTextBox)
        {
            TextRange textRange = new TextRange(
                richTextBox.Document.ContentStart,
                richTextBox.Document.ContentEnd
            );

            return textRange.Text;
        }// Get the text from the rich text box
        private void Confirm_btn_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("\n Confirm_btn_Click \n");



            #region Check If At least one TextBox is Filled


            bool IsAtLeastOneNoteEmpty = false;
            string ExaminationNote = "";

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
                MessageBox.Show("No data added to the  at least one note.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            #endregion


            SharedData.medicalEvent.PersonExaminationNote = ExaminationNote;
            SharedData.medicalEvent.PatientMedicalCondition = "Out-Patient";
            SharedData.medicalEvent.IsInpationt = false;

            SharedData.medicalEvent.Date = DateOnly.FromDateTime(DateTime.Now);
            SharedData.medicalEvent.Time = TimeOnly.FromDateTime(DateTime.Now);

            SharedData.medicalEvent.DoctorID = SharedData.doctorData.doctorID;

            Debug.WriteLine("\n SharedData.medicalEvent.DoctorID: " + SharedData.doctorData.doctorID + " \n");

            if (SharedData.doctorData.doctorLocation == "OPD")
            {
                SharedData.medicalEvent.Location = "OPD";
            }
            else
            {
                SharedData.medicalEvent.Location = "OPD Tempory";
                // Warning! Need to Complete
            }

            MyCreatePatientMedicalEvent();

        }

        private void MyCreatePatientMedicalEvent()
        {
            int medicalEventID = 0;

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
                                    " PME_Is_PrescriptionRequest, PME_Is_PatientAppointment, PME_PatientExaminationNote, PME_PatietnMedicalCondition, PME_Is_InPatient) "
                                    + "VALUES (@Patient_ID, @PME_Doctor_ID, @PME_Nurse_ID, @PME_Date, @PME_Time, @PME_Location, @PME_Is_LabRequest, @PME_Is_PrescriptionRequest, @PME_Is_PatientAppointment," +
                                      " @PME_PatientExaminationNote, @PME_PatietnMedicalCondition, @PME_Is_InPatient); SELECT SCOPE_IDENTITY();";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        

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

                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            medicalEventID = Convert.ToInt32(result);
                            Debug.WriteLine("\nInserted Medical Event ID: " + medicalEventID);
                            
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
                            cmd.Parameters.AddWithValue("@PatientMedicalEvent_ID", medicalEventID);
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


                            MedicalRequest_LabelCreator medicalRequest_LabelCreator = new MedicalRequest_LabelCreator();
                            string label = medicalRequest_LabelCreator.Create_Prescription_Label(medicin.Item2, medicin.Item1, medicalEventID);

                            cmd.Parameters.AddWithValue("@LabelName", label ?? "Error");

                            cmd.ExecuteNonQuery();


                            Debug.WriteLine("\n ------ Inserting the following values into PatientPrescriptionRequest ------");
                            Debug.WriteLine("PatientMedicalEvent_ID: " + medicalEventID);
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

                            Debug.WriteLine("\nLabelName: " + label);
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
                            cmd.Parameters.AddWithValue("@PatientMedicalEvent_ID", medicalEventID);
                            cmd.Parameters.AddWithValue("@Lab_Specimen_ID", investigationList.Item1);
                            cmd.Parameters.AddWithValue("@Lab_Specimen_Name", investigationList.Item2);
                            cmd.Parameters.AddWithValue("@Lab_Investigation_ID", specimenList.Item1);
                            cmd.Parameters.AddWithValue("@Lab_Investigation_Name", specimenList.Item2);
                            cmd.Parameters.AddWithValue("@IsUrgent", SharedData.medicalEvent.IsLabRequestUrgent);

                            MedicalRequest_LabelCreator medicalRequest_LabelCreator = new MedicalRequest_LabelCreator();
                            string label = medicalRequest_LabelCreator.Create_LabRequest_Label(investigationList.Item2, investigationList.Item1, specimenList.Item2, specimenList.Item1, medicalEventID);

                            cmd.Parameters.AddWithValue("@LabelNumber", label ?? "Error");



                            cmd.ExecuteNonQuery();

                            Debug.WriteLine("\n ------ Inserting the following values Lab Request ------");
                            Debug.WriteLine("PatientMedicalEvent_ID: " + medicalEventID);
                            Debug.WriteLine("Lab_Specimen_ID: " + investigationList.Item1);
                            Debug.WriteLine("Lab_Specimen_Name: " + investigationList.Item2);
                            Debug.WriteLine("Lab_Investigation_ID: " + specimenList.Item1);
                            Debug.WriteLine("Lab_Investigation_Name: " + specimenList.Item2);
                            Debug.WriteLine("IsUrgent: " + SharedData.medicalEvent.IsLabRequestUrgent);
                            Debug.WriteLine("\nLabelName: " + label);
                        }
                    }

                    #endregion



                    #region Add Appointment Requests to the Patient_AppointmentRequest table
                    string query4 = "INSERT INTO [dbo].[Patient_AppointmentRequest] " +
                            "([PatientMedicalEvent_ID], [ClinicType_ID], [PatientID]) " +
                            "VALUES " +
                            "(@PatientMedicalEvent_ID, @ClinicType_ID, @PatientID)";

                    foreach (var appointment in SharedData.medicalEvent.Raw_AppointmentsRequests)
                    {
                        if (appointment.Item1 == 0) //If liest item is empty or invalid(0) 
                        {
                            continue;
                        }
                        //medicinReqeustList => Medicin ID, Medicin Type, Dosage, Frequency, Duration, Route
                        using (SqlCommand cmd = new SqlCommand(query4, connection))
                        {
                            cmd.Parameters.AddWithValue("@PatientMedicalEvent_ID", medicalEventID);
                            cmd.Parameters.AddWithValue("@ClinicType_ID", appointment.Item1);
                            cmd.Parameters.AddWithValue("@PatientID", SharedData.medicalEvent.PatientID);

                            cmd.ExecuteNonQuery();


                            Debug.WriteLine("\n ------ Inserting the following values into Patient Appointment Request ------");
                            Debug.WriteLine("PatientMedicalEvent_ID: " + medicalEventID);
                            Debug.WriteLine("ClinicType_ID: " + appointment.Item1);

                            
                        }
                    }

                    #endregion

                    this.Close();

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

        private void ViewPatientHistory_btn_Click(object sender, RoutedEventArgs e)
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
