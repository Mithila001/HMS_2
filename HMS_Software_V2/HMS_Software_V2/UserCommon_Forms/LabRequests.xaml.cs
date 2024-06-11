using HMS_Software_V2._DataManage_Classes;
using HMS_Software_V2.Doctor_ClincOPD;
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


        private DCO_PatientCheck _parentForm;
        public LabRequests(DCO_PatientCheck parentForm)
        {
            InitializeComponent();

            _parentForm = parentForm;



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

            SharedData.medicalEvent.IsLabRequestUrgent = IsUrgent_btn.IsChecked ?? false;

            List<(int, string)> labRequestDetails = new List<(int, string)>();
            List<(int, string)> specimenDetails = new List<(int, string)>();

            if (AddLabRequest_WrapP.Children.OfType<UC_UCF_LabRequest>().Count() == 1)
            {
                var singleChild = AddLabRequest_WrapP.Children.OfType<UC_UCF_LabRequest>().First();
                if ((string.IsNullOrEmpty(singleChild.investigationTypeSearch_tbx.Text)) && (string.IsNullOrEmpty(singleChild.specimentSearch_tbx.Text))) // Check the textboxes are empty or not
                {
                    MessageBox.Show("No Requests", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            foreach (var child in AddLabRequest_WrapP.Children)
            {
                if (child is UC_UCF_LabRequest labRequest)
                {
                    string investigationType = labRequest.InvestgationType_selected ?? string.Empty;
                    labRequestDetails.Add((Convert.ToInt32(labRequest.InvestgationID_selected), investigationType)); //add to list

                    Debug.WriteLine("\nMainForm => Investigation Type: " + investigationType);
                    Debug.WriteLine("MainForm => Investigation ID: " + Convert.ToInt32(labRequest.InvestgationID_selected));


                    string specimenType = labRequest.SpecimenType_selected ?? string.Empty;
                    specimenDetails.Add((Convert.ToInt32(labRequest.SpecimenID_selected), specimenType));

                    Debug.WriteLine("\nMainForm => Specimen Type: " + specimenType);
                    Debug.WriteLine("MainForm => Specimen ID: " + Convert.ToInt32(labRequest.SpecimenID_selected));
                }
            }

            SharedData.medicalEvent.Raw_LabInvestigations.AddRange(labRequestDetails); // Add the list to Class List
            SharedData.medicalEvent.Raw_LabSpeciment.AddRange(specimenDetails); // Add the list to Class List

            SharedData.medicalEvent.IsLabRequest = true;

            #region Debug Outputs
            Debug.WriteLine("\n\n --- List ---"); //!!! Debugging
            foreach (var item in labRequestDetails)
            {
                Debug.WriteLine($"Investigations = 1.  ID: {item.Item1}, Type: {item.Item2}");
            }

            Debug.WriteLine("\n\n --- List From Class ---"); //!!! Debugging
            foreach (var item in SharedData.medicalEvent.Raw_LabInvestigations)
            {
                Debug.WriteLine($"Investigations = 2.  ID: {item.Item1}, Type: {item.Item2}");
            }

            Debug.WriteLine("\n\n --- List ---"); //!!! Debugging
            foreach (var item in specimenDetails)
            {
                Debug.WriteLine($"Specimen = 1.  ID: {item.Item1}, Type: {item.Item2}");
            }

            Debug.WriteLine("\n\n --- List From Class ---"); //!!! Debugging
            foreach (var item in SharedData.medicalEvent.Raw_LabSpeciment)
            {
                Debug.WriteLine($"Specimen = 2.  ID: {item.Item1}, Type: {item.Item2}");
            }
            #endregion


            this.Close();

        }

        private void LabRequests1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _parentForm.Show();
        }
    }
}
