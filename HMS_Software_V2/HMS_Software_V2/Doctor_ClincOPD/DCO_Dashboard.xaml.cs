using HMS_Software_V2._DataManage_Classes;
using HMS_Software_V2.UserLogin_Page;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace HMS_Software_V2.Doctor_ClincOPD
{
    /// <summary>
    /// Interaction logic for DCO_Dashboard.xaml
    /// </summary>
    public partial class DCO_Dashboard : Window
    {
        public DCO_Dashboard()
        {
            InitializeComponent();

            doctorName_lbl.Content = SharedData.doctorData.doctorName;
            SharedData.doctorData.doctorID = 1;
            SharedData.doctorData.doctorSpecialization = "General Physician";



        }

        bool IsConfirmButtonIsClicked = false; //Flag

        // Button Section ------------------------------------------------------------------------------------
        private void confirm_btn_Click(object sender, RoutedEventArgs e)
        {
            //string patientRID = patientRID_tbx.Text.ToString();

            //if (General_Purpose.InputValidations.MyIsNullorempty(patientRID))
            //{
            //    MessageBox.Show("Enter Patient RID", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            //}

            //if(!General_Purpose.InputValidations.MyIsOnlyNumbers(patientRID))
            //{
            //    MessageBox.Show("Enter only numbers", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            //}


            IsConfirmButtonIsClicked = true;

            DCO_PatientCheck dCO_PatientCheck = new DCO_PatientCheck();
            dCO_PatientCheck.Show();
            this.Close();

        }

        private void reset_btn_Click(object sender, RoutedEventArgs e)
        {
            patientRID_tbx.Text = "";
        }

        private void DCO_Dashboard1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!IsConfirmButtonIsClicked)
            {
                UserLogin userLogin = new UserLogin();
                userLogin.Show();
            }
            
            
        }

    }
}
