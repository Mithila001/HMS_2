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

        private void Confirm_btn_Click(object sender, RoutedEventArgs e)
        {
            //medicinReqeustList => Medicin ID, Medicin Type, Dosage, Frequency, Duration, Route
            List<(int, string, string, string, string, string)> medicinReqeustList = new List<(int, string, string, string, string, string)>();


            if (AddPrescription_WrapP.Children.OfType<UC_UCF_PrescriptionRequest>().Count() == 1)
            {
                var singleChild = AddPrescription_WrapP.Children.OfType<UC_UCF_PrescriptionRequest>().First();
                if ((string.IsNullOrEmpty(singleChild.MedicinSearch_tbx.Text)) && (string.IsNullOrEmpty(singleChild.AddDuration_tbx.Text))) // Check the textboxes are empty or not
                {
                    MessageBox.Show("No Requests", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            foreach (var child in AddPrescription_WrapP.Children)
            {
                if (child is UC_UCF_PrescriptionRequest uC_UCF_PrescriptionRequest)
                {
                    string medicinType = uC_UCF_PrescriptionRequest.MedicinName_Selected ?? string.Empty;
                    medicinReqeustList.Add((
                                 Convert.ToInt32(uC_UCF_PrescriptionRequest.MedcinID_Selected),
                                 medicinType,
                                 uC_UCF_PrescriptionRequest.SelectedDosage ?? string.Empty,
                                 uC_UCF_PrescriptionRequest.SelectedDFrequency ?? string.Empty,
                                 uC_UCF_PrescriptionRequest.SelectedDuration ?? string.Empty,
                                 uC_UCF_PrescriptionRequest.SelectedRoute ?? string.Empty)); //add to list


                    Debug.WriteLine("\nMainForm => Medicin Type: " + medicinType);
                    Debug.WriteLine("MainForm => Medicin ID: " + Convert.ToInt32(uC_UCF_PrescriptionRequest.MedcinID_Selected));

                }
            }

            SharedData.medicalEvent.Raw_Medicin.AddRange(medicinReqeustList); // Add the list to Class List
            SharedData.medicalEvent.IsPrescriptionRequest = true; 

            #region Debug Outputs

            Debug.WriteLine("\n\n --- List ---"); //!!! Debugging
            foreach (var item in medicinReqeustList)
            {
                Debug.WriteLine($"Medicin  ID: {item.Item1}, Type: {item.Item2}");
            }

            Debug.WriteLine("\n\n --- List From Class ---"); //!!! Debugging
            foreach (var item in SharedData.medicalEvent.Raw_Medicin)
            {
                Debug.WriteLine($"Medicin  ID: {item.Item1}, Type: {item.Item2}");
            }

            #endregion
        }
    }
}
