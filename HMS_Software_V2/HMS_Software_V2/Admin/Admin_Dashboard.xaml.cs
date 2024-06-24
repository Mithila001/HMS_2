using HMS_Software_V2._DataManage_Classes;
using HMS_Software_V2.UserLogin_Page;
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

namespace HMS_Software_V2.Admin
{
    /// <summary>
    /// Interaction logic for Admin_Dashboard.xaml
    /// </summary>
    public partial class Admin_Dashboard : Window
    {
        public Admin_Dashboard()
        {
            InitializeComponent();

            TemporyData();

            adminName_lbl.Content = SharedData.adminData.AdminName;

            #region Get and Assign Date Time
            int day = DateTime.Now.Day;
            string daySuffix = day switch
            {
                1 or 21 or 31 => "st",
                2 or 22 => "nd",
                3 or 23 => "rd",
                _ => "th"
            };

            todatDate_lbl.Content = $"{day}{daySuffix} {DateTime.Now:MMMM yyyy}";

            todayTime_lbl.Content = DateTime.Now.ToString("hh:mm: tt"); 
            #endregion





            selectedPage_lbl.Content = "Home"; 
      
            UserControl_Doctors.Visibility = Visibility.Collapsed;
            UserControl_Nurse.Visibility = Visibility.Collapsed;
            UserControl_Patients.Visibility = Visibility.Collapsed;
            UserControl_Reception.Visibility = Visibility.Collapsed;
            UserControl_Appointments.Visibility = Visibility.Collapsed;
        }

        private void TemporyData()
        {
           
        }

        private void Home_btn_Click(object sender, RoutedEventArgs e)
        {
            if (UserControl_Home.Visibility == Visibility.Visible)
            {
                return;   
            }
            else
            {
                selectedPage_lbl.Content = "Home";
                UserControl_Home.Visibility = Visibility.Visible;

                UserControl_Doctors.Visibility = Visibility.Collapsed;
                UserControl_Nurse.Visibility = Visibility.Collapsed;
                UserControl_Patients.Visibility = Visibility.Collapsed;
                UserControl_Reception.Visibility = Visibility.Collapsed;
                UserControl_Appointments.Visibility = Visibility.Collapsed;
            }
        }

        private void Doctor_btn_Click(object sender, RoutedEventArgs e)
        {
            if(UserControl_Doctors.Visibility == Visibility.Visible)
            {
                return;
            }
            else
            {
                selectedPage_lbl.Content = "Doctor";
                UserControl_Doctors.Visibility = Visibility.Visible;

                UserControl_Home.Visibility = Visibility.Collapsed;
                UserControl_Nurse.Visibility = Visibility.Collapsed;
                UserControl_Patients.Visibility = Visibility.Collapsed;
                UserControl_Reception.Visibility = Visibility.Collapsed;
                UserControl_Appointments.Visibility = Visibility.Collapsed;
            }
        }

        private void Nurce_btn_Click(object sender, RoutedEventArgs e)
        {
            if(UserControl_Nurse.Visibility == Visibility.Visible)
            {
                return;
            }
            else
            {
                selectedPage_lbl.Content = "Nures";
                UserControl_Nurse.Visibility = Visibility.Visible;

                UserControl_Home.Visibility = Visibility.Collapsed;
                UserControl_Doctors.Visibility = Visibility.Collapsed;
                UserControl_Patients.Visibility = Visibility.Collapsed;
                UserControl_Reception.Visibility = Visibility.Collapsed;
                UserControl_Appointments.Visibility = Visibility.Collapsed;
            }
        }

        private void Patient_btn_Click(object sender, RoutedEventArgs e)
        {
            if(UserControl_Patients.Visibility == Visibility.Visible)
            {
                return;
            }
            else
            {
                selectedPage_lbl.Content = "Patient";

                UserControl_Patients.Visibility = Visibility.Visible;

                UserControl_Home.Visibility = Visibility.Collapsed;
                UserControl_Doctors.Visibility = Visibility.Collapsed;
                UserControl_Nurse.Visibility = Visibility.Collapsed;
                UserControl_Reception.Visibility = Visibility.Collapsed;
                UserControl_Appointments.Visibility = Visibility.Collapsed;
            }
        }

        private void Reception_btn_Click(object sender, RoutedEventArgs e)
        {
            if(UserControl_Reception.Visibility == Visibility.Visible)
            {
                return;
            }
            else
            {
                selectedPage_lbl.Content = "Reception";

                UserControl_Reception.Visibility = Visibility.Visible;

                UserControl_Home.Visibility = Visibility.Collapsed;
                UserControl_Doctors.Visibility = Visibility.Collapsed;
                UserControl_Nurse.Visibility = Visibility.Collapsed;
                UserControl_Patients.Visibility = Visibility.Collapsed;
                UserControl_Appointments.Visibility = Visibility.Collapsed;
            }
        }

        private void Appointment_btn_Click(object sender, RoutedEventArgs e)
        {
            if(UserControl_Appointments.Visibility == Visibility.Visible)
            {
                return; 
            }
            else
            {
                selectedPage_lbl.Content = "Appointments";

                UserControl_Appointments.Visibility = Visibility.Visible;

                UserControl_Home.Visibility = Visibility.Collapsed;
                UserControl_Doctors.Visibility = Visibility.Collapsed;
                UserControl_Nurse.Visibility = Visibility.Collapsed;
                UserControl_Patients.Visibility = Visibility.Collapsed;
                UserControl_Reception.Visibility = Visibility.Collapsed;
            }
        }

        private void Admin_UserControls_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            UserLogin userLogin = new UserLogin();
            userLogin.Show();
        }
    }
}
