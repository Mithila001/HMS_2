using HMS_Software_V2._DataManage_Classes;
using HMS_Software_V2.Doctor_ClincOPD;
using System;
using System.Collections.Generic;
using System.Globalization;
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

using Newtonsoft.Json;
using HMS_Software_V2.Doctor_Ward;
using HMS_Software_V2.Nurse_Ward;
using HMS_Software_V2.Admin;
using HMS_Software_V2.Reception;
using HMS_Software_V2.AdmissionOfficer;

namespace HMS_Software_V2.UserLogin_Page
{
    /// <summary>
    /// Interaction logic for UserLogin.xaml
    /// </summary>
    public partial class UserLogin : Window
    {

        public UserLogin()
        {
            InitializeComponent();




        }

        private void DoctorWard_btn_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void NureceWard_btn_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Admin_Click(object sender, RoutedEventArgs e)
        {

            HMS_Software_V2._DataManage_Classes.SharedData.adminData = new HMS_Software_V2._DataManage_Classes.AdminData(); // Get a new copy of the template

            // We currently dont have an admin table in the database so we will use a tempory data
            SharedData.adminData.AdminID = 2;
            SharedData.adminData.AdminName = "V J Horathana";

            Admin_Dashboard admin_Dashboard = new Admin_Dashboard();
            admin_Dashboard.Show();
            this.Close();

        }

        private void DoctorOPD_Click(object sender, RoutedEventArgs e)
        {
            HMS_Software_V2._DataManage_Classes.SharedData.doctorData = new HMS_Software_V2._DataManage_Classes.DoctorData(); // Get a new copy of the template

            SharedData.doctorData.doctorID = 1;
            SharedData.doctorData.doctorName = "Dr. John Doe";
            SharedData.doctorData.doctorSpecialization = "General Physician";
            SharedData.doctorData.doctorLocation = "OPD";

            DCO_Dashboard dCO_Dashboard = new DCO_Dashboard();
            dCO_Dashboard.Show();
            this.Close();

        }

        private void DoctorCinic_Click(object sender, RoutedEventArgs e)
        {
            HMS_Software_V2._DataManage_Classes.SharedData.doctorData = new HMS_Software_V2._DataManage_Classes.DoctorData(); // Get a new copy of the template

            SharedData.doctorData.doctorID = 1;
            SharedData.doctorData.doctorName = "Dr. John Doe";
            SharedData.doctorData.doctorSpecialization = "General Physician";
            SharedData.doctorData.doctorLocation = "Clinic";
            SharedData.Ward_Doctor.WardID = 6;
            SharedData.Ward_Doctor.WardName = "Maternity";

            DCO_Dashboard dCO_Dashboard = new DCO_Dashboard();
            dCO_Dashboard.Show();
            this.Close();

        }

        private void DoctorWard_Click(object sender, RoutedEventArgs e)
        {
            HMS_Software_V2._DataManage_Classes.SharedData.Ward_Doctor = new HMS_Software_V2._DataManage_Classes.Ward_Doctor(); // Get a new copy of the template
            SharedData.Ward_Doctor.DoctorName = "Dr. John Doe";
            SharedData.Ward_Doctor.DoctorRID = "D00023";
            SharedData.Ward_Doctor.DoctorID = 3;
            SharedData.Ward_Doctor.DoctorSpeciality = "General Physician";
            SharedData.Ward_Doctor.WardID = 6;
            SharedData.Ward_Doctor.WardName = "Maternity";

            DW_MainPage dW_MainPage = new DW_MainPage();
            dW_MainPage.Show();
            this.Close();

        }

        private void Nurse_Click(object sender, RoutedEventArgs e)
        {
            HMS_Software_V2._DataManage_Classes.SharedData.Ward_Nurse = new HMS_Software_V2._DataManage_Classes.Ward_Nurse(); // Get a new copy of the template
            SharedData.Ward_Nurse.NurseID = 4;
            SharedData.Ward_Nurse.NurseName = "J C Kalubovial";
            SharedData.Ward_Nurse.WardName = "General Ward";
            SharedData.Ward_Nurse.NureseLicenceNumber = "Nurse-0001";
            SharedData.Ward_Nurse.WardNumber = 1;

            NW_Dashboard nW_Dashboard = new NW_Dashboard();
            nW_Dashboard.Show();
            this.Close();

        }

        private void Reception_Click(object sender, RoutedEventArgs e)
        {
           HMS_Software_V2._DataManage_Classes.SharedData.receptionData = new HMS_Software_V2._DataManage_Classes.ReceptionData(); // Get a new copy of the template

            SharedData.receptionData.ReceptionID = 5;
            SharedData.receptionData.ReceptionName = "J C Kalubovial";

            Reception_Dashboard reception_Dashboard = new Reception_Dashboard();
            reception_Dashboard.Show();
            this.Close();
        }

        private void AdmissionOfficer_Click(object sender, RoutedEventArgs e)
        {
            AO_Dashboard aO_Dashboard = new AO_Dashboard();
            aO_Dashboard.Show();
            this.Close();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
