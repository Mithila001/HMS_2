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
            HMS_Software_V2._DataManage_Classes.SharedData.Ward_Doctor = new HMS_Software_V2._DataManage_Classes.Ward_Doctor(); // Get a new copy of the template
            SharedData.Ward_Doctor.DoctorName = "Dr. John Doe";
            SharedData.Ward_Doctor.DoctorID = 3;
            SharedData.Ward_Doctor.DoctorSpeciality = "General Physician";
            SharedData.Ward_Doctor.WardID = 6;
            SharedData.Ward_Doctor.WardName = "Maternity";


            DW_MainPage dW_MainPage = new DW_MainPage();
            dW_MainPage.Show();
            this.Close();
        }

        private void NureceWard_btn_Click(object sender, RoutedEventArgs e)
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
    }
}
