using HMS_Software_V2._DataManage_Classes;
using Newtonsoft.Json;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HMS_Software_V2.UserCommon_Forms.UserControls_UCF
{
    /// <summary>
    /// Interaction logic for UC_UCF_PMH_ShowProgressNotes.xaml
    /// </summary>
    public partial class UC_UCF_PMH_ShowProgressNotes : UserControl
    {
        string? ProgressNote;
        public UC_UCF_PMH_ShowProgressNotes(string examinationNotes)
        {
            InitializeComponent();

            this.ProgressNote = examinationNotes;

            // Deserialize the JSON string to an object
            PatientHistory? patientHistory = JsonConvert.DeserializeObject<PatientHistory>(ProgressNote);

            MyGetBasicData(patientHistory);








        }

        private void MyGetBasicData(PatientHistory? patientHistory)
        {

            // Assuming you want to display the 'History Of Presenting Complaints' in the TextBox
            historyOfPresentComplaints_tbx.Text = patientHistory?.HistoryOfPresentingComplaints;
            pastMedivalHistory_tbx.Text = patientHistory?.PastMedicalHistory;
            pastSurgicalHistory_tbx.Text = patientHistory?.PastSurgicalHistory;
            familtyHistoy_tbx.Text = patientHistory?.FamilyHistory;
            examinationNotes_tbxl.Text = patientHistory?.ExaminationNotes;
            medications_tbx.Text = patientHistory?.Medications;
            allergies_tbx.Text = patientHistory?.Allergies;
            socialHistoy_tbx.Text = patientHistory?.SocialHistory;


            recordedDate_lbl.Content = SharedData.viewPatientHistory.MedicalEvnentDate;
            recordedTime_lbl.Content = SharedData.viewPatientHistory.MedicalEvnentTime;

        }


        private class PatientHistory
        {
            [JsonProperty("History Of Presenting Complaints")]
            internal string? HistoryOfPresentingComplaints { get; set; }

            [JsonProperty("PastMedical History")]
            internal string? PastMedicalHistory { get; set; }

            [JsonProperty("PastSurgical History")]
            internal string? PastSurgicalHistory { get; set; }

            [JsonProperty("Familty History")]
            internal string? FamilyHistory { get; set; }

            [JsonProperty("Examination Notes")]
            internal string? ExaminationNotes { get; set; }

            [JsonProperty("Medications")]
            internal string? Medications { get; set; }

            [JsonProperty("Allergies")]
            internal string? Allergies { get; set; }

            [JsonProperty("Socical History")]
            internal string? SocialHistory { get; set; }
        }

        
    }
}
