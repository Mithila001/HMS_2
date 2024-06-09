using HMS_Software_V2.UserCommon_Forms.UserControls_UCF;
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

            AddPrescription();

        }

        private void AddPrescription()
        {
            UC_UCF_PrescriptionRequest uc_UCF_PrescriptionRequest = new UC_UCF_PrescriptionRequest();   
            uc_UCF_PrescriptionRequest.SetParent(AddPrescription_WrapP);

            // Subscribe to the AddLabRequestClicked event
            uc_UCF_PrescriptionRequest.AddPrescription += () =>
            {
                // This code will be executed when the AddLabRequestClicked event is raised
                AddPrescription();
            };

            AddPrescription_WrapP.SizeChanged += (sender, e) =>
            {
                uc_UCF_PrescriptionRequest.Width = uc_UCF_PrescriptionRequest.ActualWidth - uc_UCF_PrescriptionRequest.Margin.Left - uc_UCF_PrescriptionRequest.Margin.Right;
            };
            uc_UCF_PrescriptionRequest.Width = AddPrescription_WrapP.ActualWidth - uc_UCF_PrescriptionRequest.Margin.Left - uc_UCF_PrescriptionRequest.Margin.Right;

            AddPrescription_WrapP.Children.Add(uc_UCF_PrescriptionRequest);
        }
    }
}
