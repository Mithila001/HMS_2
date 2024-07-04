using HMS_Software_V2.Admin;
using HMS_Software_V2.Admin.Admin_UserControls;
using HMS_Software_V2.Doctor_Ward;
using HMS_Software_V2.General_Purpose;
using HMS_Software_V2.Nurse_Ward;
using HMS_Software_V2.UserCommon_Forms;
using HMS_Software_V2.UserLogin_Page;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Windows;

namespace HMS_Software_V2
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Debug.WriteLine("\nWard_MedicalEventManager Triggerd\n");
            Ward_MedicalEventManager ward_MedicalEventManager = new Ward_MedicalEventManager();
            ward_MedicalEventManager.MyStart();
            Debug.WriteLine("\n=========================================================\n\nApplication_Startup\n\n");
            Debug.WriteLine("Ward_MedicalEventManager Triggerd\n");

            // --------------------- Nurse Ward ---------------------

            //NW_Dashboard nW_Dashboard = new NW_Dashboard();
            //nW_Dashboard.Show();

            //DW_MainPage dW_MainPage = new DW_MainPage();
            //dW_MainPage.Show();

            //NW_ShowPrescriptionRequest nW_ShowPrescriptionRequest = new NW_ShowPrescriptionRequest();
            //nW_ShowPrescriptionRequest.Show();

            //Test test = new Test();
            //test.Show();

            UserLogin userLogin = new UserLogin();
            userLogin.Show();

            //MainWindow mainWindow = new MainWindow();
            //mainWindow.Show();





        }
    }

}
