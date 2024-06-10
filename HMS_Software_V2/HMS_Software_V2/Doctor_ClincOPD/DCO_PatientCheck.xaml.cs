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


            HMS_Software_V2._DataManage_Classes.SharedData.medicalEvent = new HMS_Software_V2._DataManage_Classes.MedicalEvnent(); // Get a new copy of the medical event template

            doctorName_lbl.Content = SharedData.doctorData.doctorName;
            SharedData.doctorData.doctorName = "Dr. Wakum";

        }


        private void MyGetPatientDetails()
        {
            TodayDate_lbl.Content = DateTime.Now.ToString("dd/MM/yyyy");
            TodayTime_lbl.Content = DateTime.Now.ToString("hh:mm tt");


            string? patientName;
            string? patientAge;
            string? patientGender;

            using (SqlConnection connection = new Database_Connector().GetConnection())
            {
                string query1 = "SELECT * FROM Patient WHERE P_RegistrationID = @patientRID ";

                SqlCommand cmd = new SqlCommand(query1, connection);

                cmd.Parameters.AddWithValue("@patientRID", SharedData.doctorData.pationetRID);

                try
                {
                    connection.Open();

                    using(SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            SharedData.medicalEvent.PatientID = Convert.ToInt32(reader["Patient_ID"]);
                            patientName = reader["P_NameWithIinitials"].ToString();
                            patientAge = reader["P_Age"].ToString();
                            patientGender = reader["P_Gender"].ToString();

                            SharedData.medicalEvent.PatientName = patientName ?? "Not Found";
                            SharedData.medicalEvent.PatientAge = patientAge ?? "Not Found";
                            SharedData.medicalEvent.PatientGender = patientGender ?? "Not Found";
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

        }



        private void DCO_PatientCheck1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DCO_Dashboard dCO_Dashboard = new DCO_Dashboard();
            dCO_Dashboard.Show();

            
        }

        private void Admit_btn_Click(object sender, RoutedEventArgs e)
        {
            
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



            string HistoryOfPresentingComplaints = GetRichTextBoxText(HistoryOfPresentingComplaints_Rtbx);
            string PastMedicalHistory = GetRichTextBoxText(PastMedicalHistory_Rtbx);
            string PastSurgicalHistory = GetRichTextBoxText(PastSurgicalHistory_Rtbx);
            string FamiltyHistory = GetRichTextBoxText(FamiltyHistory_Rtbx);
            string ExaminationNotes = GetRichTextBoxText(ExaminationNotes_Rtbx);
            string Medications = GetRichTextBoxText(Medications_Rtbx);
            string Allergies = GetRichTextBoxText(Allergies_Rtbx);
            string SocicalHistory = GetRichTextBoxText(SoficalHistory_Rtbx);


            Dictionary<string, string> sections = new Dictionary<string, string>
{
            { "History Of Presenting Complaints", HistoryOfPresentingComplaints },
            { "PastMedical History", PastMedicalHistory },
            { "PastSurgical History", PastSurgicalHistory },
            { "Familty History", FamiltyHistory },
            { "Examination Notes", ExaminationNotes },
            { "Medications", Medications },
            { "Allergies", Allergies },
            { "Socical History", SocicalHistory }
};

            string examinationNote_json = JsonConvert.SerializeObject(sections, Newtonsoft.Json.Formatting.Indented);

            Debug.WriteLine("\n ExaminationNote Json file: "+ examinationNote_json +" \n");

            SharedData.medicalEvent.PersonExaminationNote = examinationNote_json;
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
                // Warning! Need to Complete
            }



            MyCreatePatientMedicalEvent();

        }


        private void MyCreatePatientMedicalEvent()
        {
            Debug.WriteLine("\n MyCreatePatientMedicalEvent(); \n");
        }
    }
}
