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

        private List<string> data = new List<string>
        {
            "Apple", "Banana", "Cherry", "Date", "Elderberry", "Fig", "Grape", "Honeydew","Ann"
        };

        private void SearchTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            string query = SearchTextBox.Text.ToLower();
            if (string.IsNullOrEmpty(query))
            {
                SearchResultsListBox.ItemsSource = null;
                SearchResultsPopup.IsOpen = false;
                return;
            }

            var filteredData = data.Where(item => item.ToLower().Contains(query)).Take(10).ToList();
            SearchResultsListBox.ItemsSource = filteredData;
            SearchResultsPopup.IsOpen = filteredData.Any();


            // Assuming each item in the ListBox has a height of 20
            SearchResultsListBox.Height = filteredData.Count * 20;
        }

        


        private void login_btn_Click(object sender, RoutedEventArgs e)
        {

            HMS_Software_V2._DataManage_Classes.SharedData.doctorData = new HMS_Software_V2._DataManage_Classes.DoctorData();

            SharedData.doctorData.doctorName = "Sam J";
            SharedData.doctorData.doctorID = 1;
            SharedData.doctorData.doctorSpecialization = "General Physician";

            DCO_Dashboard dCO_Dashboard = new DCO_Dashboard();
            dCO_Dashboard.Show();
            this.Close();
        }

        private void login_btn2_Click(object sender, RoutedEventArgs e)
        {
            HMS_Software_V2._DataManage_Classes.SharedData.doctorData = new HMS_Software_V2._DataManage_Classes.DoctorData();

            SharedData.doctorData.doctorName = "Cardi V";

            DCO_Dashboard dCO_Dashboard = new DCO_Dashboard();
            dCO_Dashboard.Show();
            this.Close();
        }
    }
}
