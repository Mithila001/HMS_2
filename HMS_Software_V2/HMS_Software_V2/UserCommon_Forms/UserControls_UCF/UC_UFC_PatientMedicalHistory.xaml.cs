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

namespace HMS_Software_V2.UserCommon_Forms.UserControls_UCF
{
    /// <summary>
    /// Interaction logic for UC_UFC_PatientMedicalHistory.xaml
    /// </summary>
    public partial class UC_UFC_PatientMedicalHistory : UserControl
    {
        int MedicalEvnetID;
        public UC_UFC_PatientMedicalHistory(int medicalEventID)
        {
            InitializeComponent();

            MedicalEvnetID = medicalEventID;
 

        }

        private void View_LabRequest_btn_Click(object sender, RoutedEventArgs e)
        {
            SharedData.viewPatientHistory.PatientMedicalEventID = MedicalEvnetID;

            PMH_ViewLabResults pMH_ViewLabResults = new PMH_ViewLabResults();
            pMH_ViewLabResults.ShowDialog();
        }

        private void View_ProgressNote_btn_Click(object sender, RoutedEventArgs e)
        {
            SharedData.viewPatientHistory.PatientMedicalEventID = MedicalEvnetID;

            SharedData.viewPatientHistory.MedicalEvnentDate = medicalEventDate_lbl.Content.ToString() ?? "Error";
            SharedData.viewPatientHistory.MedicalEvnentTime = visitedTime_lbl.Content.ToString() ?? "Error";

            PMH_ViewProgressReporsts pMH_ViewProgressReporsts = new PMH_ViewProgressReporsts();
            pMH_ViewProgressReporsts.ShowDialog();
        }

        private void View_Prescription_btn_Click(object sender, RoutedEventArgs e)
        {
            SharedData.viewPatientHistory.PatientMedicalEventID = MedicalEvnetID;

            PMH_ViewPrescriptionrequest pMH_ViewPrescriptionrequest = new PMH_ViewPrescriptionrequest();
            pMH_ViewPrescriptionrequest.ShowDialog();
        }

        private void View_MonitorResults_btn_Click(object sender, RoutedEventArgs e)
        {
            SharedData.viewPatientHistory.PatientMedicalEventID = MedicalEvnetID;

            PMH_ViewMonitorReuslts pMH_ViewMonitorReuslts = new PMH_ViewMonitorReuslts();
            pMH_ViewMonitorReuslts.ShowDialog();
        }
    }
}
