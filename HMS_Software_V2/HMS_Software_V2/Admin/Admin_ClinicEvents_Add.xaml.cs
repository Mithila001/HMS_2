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

                string todayDateFormatted = DateTime.Today.ToString("yyyy-MM-dd");
                cmd.Parameters.AddWithValue("@TodayDate", todayDateFormatted);

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
                            ClinicEvnetDate = DateTime.Parse(reader["CE_Date"].ToString() ?? "Error").ToString("yyyy-MM-dd"),
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

        private void Register_btn_Click(object sender, RoutedEventArgs e)
        {
            #region Input Validations

            if (InputValidations.MyIsNullorempty(clinicAdd_ClinicTypeID.Text))
            {
                MessageBox.Show("Add a Clinic Type ID", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!InputValidations.MyIsOnlyNumbers(clinicAdd_ClinicTypeID.Text))
            {
                MessageBox.Show("Invalid Clinic Type ID", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (InputValidations.MyIsNullorempty(clinicAdd_DoctorID.Text))
            {
                MessageBox.Show("Add a Doctor ID", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!InputValidations.MyIsOnlyNumbers(clinicAdd_DoctorID.Text))
            {
                MessageBox.Show("Invalid Doctor ID", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!clinicAdd_date.SelectedDate.HasValue)
            {
                MessageBox.Show("Please select a date of birth.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (InputValidations.MyIsNullorempty(clinicAdd_HallNumber.Text))
            {
                MessageBox.Show("Add a Hall Number", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (hoursComboBox.Text == "" || minutesComboBox.Text == "" || hoursComboBox1.Text == "" || minutesComboBox1.Text == "")
            {
                MessageBox.Show("Please add a time duration", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (InputValidations.MyIsNullorempty(clinicAdd_totalSlots.Text))
            {
                MessageBox.Show("Add the Total Slots Amount", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!InputValidations.MyIsOnlyNumbers(clinicAdd_totalSlots.Text))
            {
                MessageBox.Show("Invalid Total Slots Number", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (Convert.ToInt32(clinicAdd_totalSlots.Text) < 1 && Convert.ToInt32(clinicAdd_totalSlots.Text) > 100)
            {
                MessageBox.Show("Total Slots must be between 1 - 100", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }




            #endregion

            int clinicType = Convert.ToInt32(clinicAdd_ClinicTypeID.Text);
            int doctorID = Convert.ToInt32(clinicAdd_DoctorID.Text);
            string clinicDate = clinicAdd_date.SelectedDate.Value.ToString("yyyy-MM-dd");
            string hallNumber = clinicAdd_HallNumber.Text;
            string startTime = $"{hoursComboBox.Text}:{minutesComboBox.Text}";
            string endTime = $"{hoursComboBox1.Text}:{minutesComboBox1.Text}";
            int totalSlots = Convert.ToInt32(clinicAdd_totalSlots.Text);



            using (SQLiteConnection connection = new Database_Connector().GetConnection())
            {
                string insertQuery = @"
                    INSERT INTO ClinicEvents (CE_ClinicType_ID, Doctor_ID, CE_HallNumber, CE_StartTime, CE_EndTime, CE_Date, CE_TotalSlots, CE_TakenSlots)
                    VALUES (@ClinicType, @DoctorID, @HallNumber, @StartTime, @EndTime, @EventDate, @TotalSlots, 0);";

                SQLiteCommand cmd = new SQLiteCommand(insertQuery, connection);

                // Add parameters to prevent SQL injection
                cmd.Parameters.AddWithValue("@ClinicType", clinicType);
                cmd.Parameters.AddWithValue("@DoctorID", doctorID);
                cmd.Parameters.AddWithValue("@HallNumber", hallNumber);
                cmd.Parameters.AddWithValue("@StartTime", startTime);
                cmd.Parameters.AddWithValue("@EndTime", endTime);
                cmd.Parameters.AddWithValue("@EventDate", clinicDate);
                cmd.Parameters.AddWithValue("@TotalSlots", totalSlots);

                try
                {
                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Clinic event successfully registered.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                        showClinicEvnets_ListView.ItemsSource = null;

                        #region Clean Feilds
                        clinicAdd_ClinicTypeID.Text = "";
                        clinicAdd_DoctorID.Text = "";
                        clinicAdd_HallNumber.Text = "";
                        clinicAdd_totalSlots.Text = ""; 
                        #endregion


                        MyDisplayClincEvnets();

                    }
                    else
                    {
                        MessageBox.Show("Failed to register the clinic event.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
                catch (SQLiteException ex)
                {
                    Debug.WriteLine("\nError: \n" + ex.Message);
                    MessageBox.Show("Database error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    connection.Close();
                }
            }





        }
    }

   
}
