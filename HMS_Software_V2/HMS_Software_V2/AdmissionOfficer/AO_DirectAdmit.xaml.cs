using HMS_Software_V2._DataManage_Classes;
using HMS_Software_V2.General_Purpose;
using System;
using System.Collections.Generic;
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

using Newtonsoft.Json;

namespace HMS_Software_V2.AdmissionOfficer
{
    /// <summary>
    /// Interaction logic for AO_DirectAdmit.xaml
    /// </summary>
    public partial class AO_DirectAdmit : Window
    {
        public AO_DirectAdmit()
        {
            InitializeComponent();

            MyLoadBasicDetails();
        }

        private void MyLoadBasicDetails()
        {
 
            TodayDate_lbl.Content = DateTime.Now.ToString("dd MMMM yyyy");
            TodayTime_lbl.Content = DateTime.Now.ToString("hh:mm tt");

            doctorName_lbl.Content = SharedData.userData.UserName;

            using (SQLiteConnection connection = new Database_Connector().GetConnection())
            {
                #region Getting Admission Officer ID
                int admissionOfficeID = SharedData.userData.UserID;
                if (admissionOfficeID == 0)
                {
                    MessageBox.Show("Error: User ID is not set.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                #endregion

                #region Search/Assign Patient Details FROM DB
                string query1 = "SELECT P_NameWithIinitials, P_Age, P_Gender" +
                            " FROM Patient WHERE Patient_ID = @Patient_ID";

                SQLiteCommand cmd = new SQLiteCommand(query1, connection);

                try
                {
                    connection.Open();

                    cmd.Parameters.AddWithValue("@Patient_ID", SharedData.admissioOfficer.PatientID);

                    SQLiteDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {

                        string patientName = reader["P_NameWithIinitials"].ToString() ?? "";
                        string patientAge = reader["P_Age"].ToString() ?? "";
                        string patientGender = reader["P_Gender"].ToString() ?? "";

                        PatientName_lbl.Content = patientName;
                        PatientAge_lbl.Content = patientAge;
                        PatientGender_lbl.Content = patientGender;

                    }
                    else
                    {
                        MessageBox.Show("No Patient Found", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                catch (SQLiteException ex)
                {
                    Debug.WriteLine("\nError1: \n" + ex.Message);
                    MessageBox.Show("Error1: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                finally
                {
                    connection.Close();
                }
                #endregion
            }

        }

        bool IsEmergancy = false;
        string EmergancyType = "None";

        int InputWardNo = 0;
        string? ExaminationNote;
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


            }
            else
            {
                MessageBox.Show("Please fill at least one note.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            #endregion

            #region Check if an Emergancy
            if (IsPCU_Select_CheckVox.IsChecked == true)
            {
                IsEmergancy = true;
                EmergancyType = "PCU";
            }
            else if (ETU_Selecte_CheckBox.IsChecked == true)
            {
                IsEmergancy = true;
                EmergancyType = "ETU";
            }
            else
            {
                IsEmergancy = false;
                EmergancyType = "None";
            }
            #endregion


            MyAddPatientAdmiteDataToDatabase();
        }

        private void MyAddPatientAdmiteDataToDatabase()
        {
            using (SQLiteConnection connection = new Database_Connector().GetConnection())
            {

                try
                {
                    connection.Open();

                    #region INSERT to Admitted_Patient Table

                    string query1 = "INSERT INTO Admitted_Patients" +
                            " (Patient_ID, AP_Condition, AP_VisiteTotalRounds, AP_AdmittedDate, AP_AdmittedTime, AP_Ward)" +
                            " VALUES(@Patient_ID, @AP_Condition, @AP_VisiteTotalRounds, @AP_AdmittedDate, @AP_AdmittedTime, @AP_Ward)";

                    using (SQLiteCommand command = new SQLiteCommand(query1, connection))
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

                    #region INSERT to PatientMedical_Event Table
                    string query2 = "INSERT INTO PatientMedical_Event" +
                    " (Patient_ID, PME_Doctor_ID, PME_Nurse_ID, PME_Date, PME_Time, PME_Location, PME_Is_LabRequest,"+
                    " PME_Is_PrescriptionRequest, PME_Is_PatientAppointment, PME_PatientExaminationNote, PME_PatietnMedicalCondition, PME_Is_InPatient)" +
                    " VALUES(@Patient_ID, @PME_Doctor_ID, @PME_Nurse_ID, @PME_Date, @PME_Time, @PME_Location, @PME_Is_LabRequest,"+
                    " @PME_Is_PrescriptionRequest, @PME_Is_PatientAppointment, @PME_PatientExaminationNote, @PME_PatietnMedicalCondition, @PME_Is_InPatient)";

                    using (SQLiteCommand command2 = new SQLiteCommand(query2, connection))
                    {
                        command2.Parameters.AddWithValue("@Patient_ID", SharedData.admissioOfficer.PatientID);
                        command2.Parameters.AddWithValue("@PME_Doctor_ID", SharedData.userData.UserID); 
                        command2.Parameters.AddWithValue("@PME_Nurse_ID", 0); 
                        command2.Parameters.AddWithValue("@PME_Date", DateTime.Now.ToString("yyyy-MM-dd"));
                        command2.Parameters.AddWithValue("@PME_Time", DateTime.Now.ToString("HH:mm:ss")); 
                        command2.Parameters.AddWithValue("@PME_Location", "AdmissionOffice"); 
                        command2.Parameters.AddWithValue("@PME_Is_LabRequest", false); 
                        command2.Parameters.AddWithValue("@PME_Is_PrescriptionRequest", false); 
                        command2.Parameters.AddWithValue("@PME_Is_PatientAppointment", false); 
                        command2.Parameters.AddWithValue("@PME_PatientExaminationNote", ExaminationNote); 
                        command2.Parameters.AddWithValue("@PME_PatietnMedicalCondition", "Just Admitted");
                        command2.Parameters.AddWithValue("@PME_Is_InPatient", true); 

                        command2.ExecuteNonQuery();
                    }
                    #endregion

                    #region UPDATE Patient Table
                    string query3 = "UPDATE Patient SET P_CurrentStatus = 'In-Patient', P_IsEmergency = @P_IsEmergency, P_EmergancyType = @P_EmergancyType, P_EmergancyAssignedTime = @P_EmergancyAssignedTime WHERE Patient_ID = @Patient_ID";

                    using (SQLiteCommand command3 = new SQLiteCommand(query3, connection))
                    {
                        command3.Parameters.AddWithValue("@Patient_ID", SharedData.admissioOfficer.PatientID);

                        command3.Parameters.AddWithValue("@P_IsEmergency", IsEmergancy);
                        command3.Parameters.AddWithValue("@P_EmergancyType", EmergancyType);
                        command3.Parameters.AddWithValue("@P_EmergancyAssignedTime", DateTime.Now.ToString("hh:mm tt"));

                        command3.ExecuteNonQuery();
                    }
                    #endregion

                }
                catch (SQLiteException ex)
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

        private void AD_DirectAdmit1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            AO_Dashboard aO_Dashboard = new AO_Dashboard();
            aO_Dashboard.Show();
        }
    }
}
