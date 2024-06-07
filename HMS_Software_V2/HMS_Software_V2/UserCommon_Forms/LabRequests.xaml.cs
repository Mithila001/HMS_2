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

            AddNewLabRequest();
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
    }
}
