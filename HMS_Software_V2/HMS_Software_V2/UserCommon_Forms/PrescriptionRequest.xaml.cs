using HMS_Software_V2._DataManage_Classes;
using HMS_Software_V2.Doctor_ClincOPD;
using HMS_Software_V2.UserCommon_Forms.UserControls_UCF;
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
using System.Windows.Shapes;

namespace HMS_Software_V2.UserCommon_Forms
{
    /// <summary>
    /// Interaction logic for PrescriptionRequest.xaml
    /// </summary>
    public partial class PrescriptionRequest : Window
    {
        public PrescriptionRequest()
        {
            InitializeComponent();

            Debug.WriteLine("\n\n----- PrescriptionRequest -----\n");

            AddPrescription();
            // _parentForm = null; // You can set _parentForm to null here if you want

        }

        private DCO_PatientCheck _parentForm;
        public PrescriptionRequest(DCO_PatientCheck parentForm)
        {
            InitializeComponent();

            Debug.WriteLine("\n\n----- PrescriptionRequest -----\n");

            AddPrescription();
            _parentForm = parentForm;
        }

        private void MyAddBasicDetails()
        {
            PatientName_lbl.Content = SharedData.medicalEvent.PatientName;
            Age_lbl.Content = SharedData.medicalEvent.PatientAge;
            Gender_lbl.Content = SharedData.medicalEvent.PatientGender;

            TodayDate_lbl.Content = DateTime.Now.ToString("dd/MM/yyyy");
            TodayTime_lbl.Content = DateTime.Now.ToString("hh:mm tt");

        }




        int PrescriptionCount = 0;
        private void AddPrescription()
        {
            PrescriptionCount+= 1;

            Debug.WriteLine("PrescriptionCount => Parent: " + PrescriptionCount);

            UC_UCF_PrescriptionRequest uc_UCF_PrescriptionRequest = new UC_UCF_PrescriptionRequest();   
            uc_UCF_PrescriptionRequest.PerscriptionRequestCount_lbl.Content = PrescriptionCount.ToString();

            uc_UCF_PrescriptionRequest.SetParent(AddPrescription_WrapP);


            // Subscribe to the AddLabRequestClicked event
            uc_UCF_PrescriptionRequest.AddPrescription += () =>
            {
                // This code will be executed when the AddLabRequestClicked event is raised
                AddPrescription();
            };

            AddPrescription_WrapP.SizeChanged += (sender, e) =>
            {
                uc_UCF_PrescriptionRequest.Width = AddPrescription_WrapP.ActualWidth - uc_UCF_PrescriptionRequest.Margin.Left - uc_UCF_PrescriptionRequest.Margin.Right;
            };
            uc_UCF_PrescriptionRequest.Width = AddPrescription_WrapP.ActualWidth - uc_UCF_PrescriptionRequest.Margin.Left - uc_UCF_PrescriptionRequest.Margin.Right;

            AddPrescription_WrapP.Children.Add(uc_UCF_PrescriptionRequest);
        }



        private void PrescriptionRequest1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _parentForm.Show();

        }
    }
}
