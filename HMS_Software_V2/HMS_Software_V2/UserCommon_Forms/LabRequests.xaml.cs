using HMS_Software_V2._DataManage_Classes;
using HMS_Software_V2.Reception.R_UserControls;
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
    /// Interaction logic for LabRequests.xaml
    /// </summary>
    public partial class LabRequests : Window
    {
       
        public LabRequests()
        {
            InitializeComponent();

            Debug.WriteLine("\n\n----- LabRequests -----\n");

            AddNewLabRequest();

            MyAddBasicDetails();
        }


        private void MyAddBasicDetails()
        {
            TodayDate_lbl.Content = DateTime.Now.ToString("dd/MM/yyyy");
            TodayTime_lbl.Content = DateTime.Now.ToString("hh:mm tt");

            PatientName_lbl.Content = SharedData.medicalEvent.PatientName;
            PatientAge_lbl.Content = SharedData.medicalEvent.PatientAge;
            Gender_lbl.Content = SharedData.medicalEvent.PatientGender;

        }

        private void AddNewLabRequest()
        {
            Debug.WriteLine("AddNewLabRequest Started");

            UC_UCF_LabRequest uc_UCF_LabRequest = new UC_UCF_LabRequest();

            
            // Pass the parent control to the user control
            uc_UCF_LabRequest.SetParent(AddLabRequest_WrapP);

            // Subscribe to the AddLabRequestClicked event
            uc_UCF_LabRequest.AddLabRequestClicked += () =>
            {
                // This code will be executed when the AddLabRequestClicked event is raised
                AddNewLabRequest();
            };


            AddLabRequest_WrapP.SizeChanged += (sender, e) =>
            {
                uc_UCF_LabRequest.Width = AddLabRequest_WrapP.ActualWidth - uc_UCF_LabRequest.Margin.Left - uc_UCF_LabRequest.Margin.Right;
            };
            uc_UCF_LabRequest.Width = AddLabRequest_WrapP.ActualWidth - uc_UCF_LabRequest.Margin.Left - uc_UCF_LabRequest.Margin.Right;

            AddLabRequest_WrapP.Children.Add(uc_UCF_LabRequest);
        }

        private void SaveLabRequests_btn_Click(object sender, RoutedEventArgs e)
        {
            List<(string, string)> labRequestDetails = new List<(string, string)>();

            foreach (var child in AddLabRequest_WrapP.Children)
            {
                if (child is UC_UCF_LabRequest labRequest)
                {
                    labRequestDetails.Add((labRequest.investigationTypeSearch_tbx.Text, labRequest.specimentSearch_tbx.Text));
                }
            }
        }
    }
}
