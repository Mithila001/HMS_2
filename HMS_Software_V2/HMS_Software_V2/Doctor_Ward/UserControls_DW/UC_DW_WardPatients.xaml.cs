using HMS_Software_V2._DataManage_Classes;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HMS_Software_V2.Doctor_Ward.UserControls_DW
{
    /// <summary>
    /// Interaction logic for UC_DW_WardPatients.xaml
    /// </summary>
    public partial class UC_DW_WardPatients : UserControl
    {
        private DW_MainPage ParentPageReferece;
        public UC_DW_WardPatients(DW_MainPage dW_MainPage)
        {
            InitializeComponent();
            ParentPageReferece = dW_MainPage;
        }

        public int PatientID { get; set; }
        public string? PatientConditon { get; set; }
        public int PatientVisitRound { get; set; }
        public string? TreatmentStatus { get; set; }
        public int TotalVisitRouds { get; set; }
        public string? PatientName { get; set; }
        public string? PatientGender { get; set; }
        public string? PatientAge { get; set; }
        public string? PatientRID {  get; set; }
        public bool IsVisitedByTheDoctor { get; set; }


        private void UC_DW_WardPatients1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            if (!IsVisitedByTheDoctor)
            {
                HMS_Software_V2._DataManage_Classes.SharedData.medicalEvent = new HMS_Software_V2._DataManage_Classes.MedicalEvnent(); // Get a new copy of the template

                SharedData.medicalEvent.PatientID = PatientID;
                SharedData.medicalEvent.pationetRID = PatientRID ?? "Error";
                SharedData.medicalEvent.DoctorID = SharedData.Ward_Doctor.DoctorID;
                SharedData.medicalEvent.Date = DateOnly.FromDateTime(DateTime.Now);
                SharedData.medicalEvent.Time = TimeOnly.FromDateTime(DateTime.Now);
                SharedData.medicalEvent.Location = "Ward";
                SharedData.medicalEvent.PatientMedicalCondition = PatientConditon ?? "Error";
                SharedData.medicalEvent.IsInpationt = true;

                SharedData.medicalEvent.PatientName = PatientName ?? "Error";
                SharedData.medicalEvent.PatientAge = PatientAge ?? "Error";
                SharedData.medicalEvent.PatientGender = PatientGender ?? "Error";

                SharedData.medicalEvent.PatientVisitCount = TotalVisitRouds;


                ParentPageReferece.IsGoingToUserLoginPage = false;

                DW_ProgressNote dw_ProgressNote = new DW_ProgressNote();
                dw_ProgressNote.Show();
                ParentPageReferece.Close();



                MyDebugPrintProperties();



            }
            else
            {
                return;
            }



            
        }


        private void MyDebugPrintProperties()
        {
            Debug.WriteLine($"\n\n-----------------------------------\n");
            Debug.WriteLine($"PatientID: {PatientID}");
            Debug.WriteLine($"PatientCondition: {PatientConditon}");
            Debug.WriteLine($"PatientVisitRound: {PatientVisitRound}");
            Debug.WriteLine($"TreatmentStatus: {TreatmentStatus}");
            Debug.WriteLine($"TotalVisitRounds: {TotalVisitRouds}");
            Debug.WriteLine($"PatientName: {PatientName}");
            Debug.WriteLine($"PatientGender: {PatientGender}");
            Debug.WriteLine($"PatientAge: {PatientAge}");
            Debug.WriteLine($"PatientRID: {PatientRID}\n");
        }
    }
}
