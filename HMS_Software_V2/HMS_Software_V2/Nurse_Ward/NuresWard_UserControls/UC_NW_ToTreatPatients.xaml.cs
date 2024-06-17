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

namespace HMS_Software_V2.Nurse_Ward.NuresWard_UserControls
{
    /// <summary>
    /// Interaction logic for UC_NW_ToTreatPatients.xaml
    /// </summary>
    public partial class UC_NW_ToTreatPatients : UserControl
    {
        NW_Dashboard parentForm;
        public UC_NW_ToTreatPatients(NW_Dashboard nW_Dashboard)
        {
            InitializeComponent();
            parentForm = nW_Dashboard;
        }

        public int PatientID { get; set; }
        public string? PatientName { get; set; }
        public string? PatientCondition { get; set; }
        public string? PatientGender { get; set; }
        public string? PatientAge { get; set; }
        public string? PatientTreatmentStatus { get; set; }

        public int PatientMedicalEventID {  get; set; }

        private void UC_NW_ToTreatPatients1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if(PatientTreatmentStatus == "Completed")
            {
                return;
            }
            else
            {
                HMS_Software_V2._DataManage_Classes.SharedData.Ward_NursePatient = new HMS_Software_V2._DataManage_Classes.Ward_NursePatient(); // Get a new copy of the template
                
                SharedData.Ward_NursePatient.PatientID = PatientID;
                SharedData.Ward_NursePatient.PatientName = PatientName ?? "Error";
                SharedData.Ward_NursePatient.PatientGender = PatientGender ?? "Error";
                SharedData.Ward_NursePatient.PatientAge = PatientAge ?? "Error";
                SharedData.Ward_NursePatient.PatientCondition = PatientCondition ?? "Error";
                SharedData.Ward_NursePatient.PatientMedicalEventID = PatientMedicalEventID;

                NW_PatientTreat nW_PatientTreat = new NW_PatientTreat();
                nW_PatientTreat.Show();
                parentForm.Close();



            }

        }
    }
}
