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

namespace HMS_Software_V2.AdmissionOfficer.UserControls_AO
{
    /// <summary>
    /// Interaction logic for UC_AO_AdmitRequest.xaml
    /// </summary>
    public partial class UC_AO_AdmitRequest : UserControl
    {
        private AO_Dashboard dashboardWindow;
        public UC_AO_AdmitRequest(AO_Dashboard dashboard)
        {
            InitializeComponent();
            dashboardWindow = dashboard;
        }

        public int PatientID { get; set; }
        public int Doctor_ID { get; set; }
        public string? P_ReferralNote { get; set; }
        public bool Is_Urgent { get; set; }
        public string? SendFrom_Location { get; set; }
        public string? P_NameWithIinitials { get; set; }
        public string? P_Age { get; set; }
        public string? P_Gender { get; set; }
        public string? P_RegistrationID { get; set; }
        public string? D_NameWithInitials { get; set; }
        public string? D_Specialty { get; set; }

        public int PatientAdmitRequestID { get; set; }

        
        private void UC_AO_AdmitRequest1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            HMS_Software_V2._DataManage_Classes.SharedData.admissioOfficer = new HMS_Software_V2._DataManage_Classes.AdmissioOfficer(); // Get a new copy of the template

            MyAssigneDataToClass();// Assign the data to the class

            MyDebugPrintProperties();

            //HMS_Software_V2._DataManage_Classes.SharedData.admissioOfficer = null;

            AO_AdmitPatient aO_AdmitPatient = new AO_AdmitPatient();
            aO_AdmitPatient.Show();

            dashboardWindow.Close();



        }



        private void MyDebugPrintProperties()
        {
            #region Debug Outputs
            Debug.WriteLine($"\n\n---------------------- Patient Admit Request Use Control ----------------------\n");
            Debug.WriteLine($"PatientID: {PatientID}");
            Debug.WriteLine($"Doctor_ID: {Doctor_ID}");
            Debug.WriteLine($"P_ReferralNote: {P_ReferralNote}");
            Debug.WriteLine($"Is_Urgent: {Is_Urgent}");
            Debug.WriteLine($"SendFrom_Location: {SendFrom_Location}");
            Debug.WriteLine($"P_NameWithIinitials: {P_NameWithIinitials}");
            Debug.WriteLine($"P_Age: {P_Age}");
            Debug.WriteLine($"P_Gender: {P_Gender}");
            Debug.WriteLine($"P_RegistrationID: {P_RegistrationID}");
            Debug.WriteLine($"D_NameWithInitials: {D_NameWithInitials}");
            Debug.WriteLine($"D_Specialty: {D_Specialty}");
            #endregion
        }

        private void MyAssigneDataToClass()
        {
            SharedData.admissioOfficer.PatientID = PatientID;
            SharedData.admissioOfficer.Doctor_ID = Doctor_ID;
            SharedData.admissioOfficer.P_ReferralNote = P_ReferralNote ?? "Error";
            SharedData.admissioOfficer.Is_Urgent = Is_Urgent;
            SharedData.admissioOfficer.SendFrom_Location = SendFrom_Location ?? "Error";
            SharedData.admissioOfficer.P_NameWithIinitials = P_NameWithIinitials ?? "Error";
            SharedData.admissioOfficer.P_Age = P_Age ?? "Error";
            SharedData.admissioOfficer.P_Gender = P_Gender ?? "Error";
            SharedData.admissioOfficer.P_RegistrationID = P_RegistrationID ?? "Error";
            SharedData.admissioOfficer.D_NameWithInitials = D_NameWithInitials ?? "Error";
            SharedData.admissioOfficer.D_Specialty = D_Specialty ?? "Error";
            SharedData.admissioOfficer.PatientAdmitRequestID = PatientAdmitRequestID;
        }
    }
}
