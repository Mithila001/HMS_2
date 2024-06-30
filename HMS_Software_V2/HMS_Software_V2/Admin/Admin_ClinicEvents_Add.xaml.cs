using HMS_Software_V2._DataManage_Classes;
using HMS_Software_V2.General_Purpose;
using HMS_Software_V2.Reception.R_UserControls;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
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

namespace HMS_Software_V2.Admin
{
    /// <summary>
    /// Interaction logic for Admin_ClinicEvents_Add.xaml
    /// </summary>
    public partial class Admin_ClinicEvents_Add : Window
    {
        public Admin_ClinicEvents_Add()
        {
            InitializeComponent();

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


            PopulateTimeValues();

            MyDisplayClincEvnets();
        }

        private void PopulateTimeValues()
        {
            for (int hour = 0; hour < 24; hour++) // 24 hours in a day
            {
                hoursComboBox.Items.Add(hour.ToString("00"));
                hoursComboBox1.Items.Add(hour.ToString("00"));
            }

            for (int minute = 0; minute < 60; minute += 15) // increment by 15 minutes
            {
                minutesComboBox.Items.Add(minute.ToString("00"));
                minutesComboBox1.Items.Add(minute.ToString("00"));
            }
        }

        public class ClinicEvent
        {
            public int ClinicEvnetID { get; set; }
            public string? ClinicType { get; set; }
            public int DoctorID { get; set; }
            public string? HallNumber { get; set; }
            public string? StartTime { get; set; }
            public string? EndTime { get; set; }
            public int TotalSlots { get; set; }
            public int TakenSlots { get; set; }
            public string? ClinicEvnetDate { get; set; }
        }

        private void MyDisplayClincEvnets()
        {
            List<ClinicEvent> clinicEvents = new List<ClinicEvent>();

            using (SQLiteConnection connection = new Database_Connector().GetConnection())
            {
                string query1 = "SELECT * FROM ClinicEvents WHERE CE_Date > @TodayDate";

                SQLiteCommand cmd = new SQLiteCommand(query1, connection);

                cmd.Parameters.AddWithValue("@TodayDate", DateTime.Today);

                try
                {
                    connection.Open();
                    SQLiteDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        clinicEvents.Add(new ClinicEvent
                        {
                            ClinicEvnetID = Convert.ToInt32(reader["CE_ClinicType_ID"]),
                            DoctorID = Convert.ToInt32(reader["Doctor_ID"]),
                            HallNumber = reader["CE_HallNumber"].ToString() ?? "Error",
                            StartTime = reader["CE_StartTime"].ToString() ?? "Error",
                            EndTime = reader["CE_EndTime"].ToString() ?? "Error",
                            ClinicEvnetDate = reader["CE_Date"].ToString() ?? "Error",
                            TotalSlots = Convert.ToInt32(reader["CE_TotalSlots"]),
                            TakenSlots = Convert.ToInt32(reader["CE_TakenSlots"])
                        });

                    }
                    reader.Close();
                }
                catch (SQLiteException ex)
                {
                    Debug.WriteLine("\nError1: \n" + ex.Message);
                    MessageBox.Show("Error1: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    connection.Close();
                }
            }

            showClinicEvnets_ListView.ItemsSource = clinicEvents;
        }
    }

   
}
