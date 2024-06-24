using HMS_Software_V2._DataManage_Classes;
using HMS_Software_V2.General_Purpose;
using HMS_Software_V2.Reception.R_UserControls;
using HMS_Software_V2.UserLogin_Page;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using static System.Net.Mime.MediaTypeNames;

namespace HMS_Software_V2.Reception
{
    /// <summary>
    /// Interaction logic for Reception_Dashboard.xaml
    /// </summary>
    public partial class Reception_Dashboard : Window
    {
        public Reception_Dashboard()
        {
            InitializeComponent();

            MyLoadBasicData();

            LoadClinicData();

        }

        private void MyLoadBasicData()
        {
            #region Get and Assign Date Time
            int day = DateTime.Now.Day;
            string daySuffix = day switch
            {
                1 or 21 or 31 => "st",
                2 or 22 => "nd",
                3 or 23 => "rd",
                _ => "th"
            };

            todayDate.Content = $"{day}{daySuffix} {DateTime.Now:MMMM yyyy}";

            todayTime.Content = DateTime.Now.ToString("hh:mm: tt");
            #endregion

            receptionName_lbl.Content = SharedData.receptionData.ReceptionName;
        }

        private void LoadClinicData()
        {
            using (SqlConnection connection = new Database_Connector().GetConnection())
            {
                string query1 = "SELECT CE.CE_HallNumber, CE.CE_StartTime, CE.CE_EndTime, CE.CE_Date, CE.CE_TotalSlots, CE.CE_TakenSlots, CT.CT_Name " +
               "FROM ClinicEvents CE " +
               "INNER JOIN ClinicType CT ON CE.CE_ClinicType_ID = CT.ClinicType_ID";

                SqlCommand cmd = new SqlCommand(query1, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        UC_RD_ClinicInfo uC_RD_ClinicInfo = new UC_RD_ClinicInfo();
                        uC_RD_ClinicInfo.clincName.Content = reader["CT_Name"].ToString();
                        uC_RD_ClinicInfo.hallNumber.Content = reader["CE_HallNumber"].ToString();

                        TimeSpan startTimeValue = (TimeSpan)reader["CE_StartTime"];
                        string startTime = new DateTime(startTimeValue.Ticks).ToString("hh:mm tt");

                        TimeSpan endTimeValue = (TimeSpan)reader["CE_EndTime"];
                        string endTime = new DateTime(endTimeValue.Ticks).ToString("hh:mm tt");

                        string clinicEventTime = $"{startTime} - {endTime}";
                        uC_RD_ClinicInfo.timePeriod_lbl.Content = clinicEventTime;


                        DateTime clinicEventDate = (DateTime)reader["CE_Date"];
                        DateTime today = DateTime.Today;
                        TimeSpan currentTime = DateTime.Now.TimeOfDay;

                        if (currentTime < endTimeValue)
                        {
                            uC_RD_ClinicInfo.availabilityBorder_border.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF66FF86"));
                            uC_RD_ClinicInfo.clinic_Availability.Content = "Available";
                        }
                        else
                        {
                            uC_RD_ClinicInfo.availabilityBorder_border.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF7979"));
                            uC_RD_ClinicInfo.clinic_Availability.Content = "Not Available";
                        }



                        if(clinicEventDate == today)
                        {
                            // Adjust the width of the user control to match the width of the parent container
                            clinicAvailability_WrapP.SizeChanged += (sender, e) =>
                            {
                                uC_RD_ClinicInfo.Width = clinicAvailability_WrapP.ActualWidth - uC_RD_ClinicInfo.Margin.Left - uC_RD_ClinicInfo.Margin.Right;
                                Debug.WriteLine("\n Width:" + uC_RD_ClinicInfo.Width);
                                Debug.WriteLine("\n clinicAvailability_WrapP.ActualWidth:" + clinicAvailability_WrapP.ActualWidth);
                            };
                            uC_RD_ClinicInfo.Width = clinicAvailability_WrapP.ActualWidth - uC_RD_ClinicInfo.Margin.Left - uC_RD_ClinicInfo.Margin.Right;

                            clinicAvailability_WrapP.Children.Add(uC_RD_ClinicInfo);

                        }


                        
                    }
                    reader.Close();


                }

                catch(Exception ex)
                {
                    Debug.WriteLine("\nError1: \n"+ex.Message);
                    MessageBox.Show("Error1: "+ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    connection.Close();
                }
            }


        }

        private void SearchPatient_btn_Click(object sender, RoutedEventArgs e)
        {
            Reception_PatientSearch reception_PatientSearch = new Reception_PatientSearch();
            reception_PatientSearch.Show();

            IsMovingToUserLoggin = false;

            this.Close();
        }

        private void ShowClinics_btn_Click(object sender, RoutedEventArgs e)
        {
            Reception_ViewAssigneClinics reception_ViewAssigne = new Reception_ViewAssigneClinics();
            reception_ViewAssigne.Show();

            IsMovingToUserLoggin = false;

            this.Close();
        }

        private void RegisterPatient_btn_Click(object sender, RoutedEventArgs e)
        {
            Reception_RegisterPatient reception_RegisterPatient = new Reception_RegisterPatient();
            reception_RegisterPatient.Show();

            IsMovingToUserLoggin = false;

            this.Close();
        }

        bool IsMovingToUserLoggin = true;
        private void Reception_Dashboard1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsMovingToUserLoggin)
            {
                UserLogin userLogin = new UserLogin();
                userLogin.Show();

            }
            
            
           
        }
    }
}
