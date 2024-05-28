using HMS_Software_V2.General_Purpose;
using HMS_Software_V2.Reception.R_UserControls;
using System;
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

namespace HMS_Software_V2.Reception
{
    /// <summary>
    /// Interaction logic for Reception_ViewAssigneClinics.xaml
    /// </summary>
    public partial class Reception_ViewAssigneClinics : Window
    {
        public Reception_ViewAssigneClinics()
        {
            InitializeComponent();
            loadBasicData();
            LoadClinicType();
        }

        private void loadBasicData()
        {
            receptionName.Content = "A V Temporory";
            string formattedDate = DateTime.Today.ToString("dd MMMM yyyy"); // "20 October 2024"
            string formattedTime = DateTime.Now.ToString("hh:mm tt"); // "02:30 PM"
            todayDate.Content = formattedDate;
            todayTime.Content = formattedTime;
        }

        private void LoadClinicType()
        {
            using (SqlConnection connection = new Database_Connector().GetConnection())
            {
                string query1 = "SELECT * FROM ClinicType";

                SqlCommand cmd = new SqlCommand(query1, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        UC_RV_ClinicTypes uC_RV_ClinicTypes = new UC_RV_ClinicTypes();
                        string clinicTypeName = reader["CT_Name"].ToString() ?? "Error";
                        string availability = "XXXXX"; // Replace with actual availability if available

                        uC_RV_ClinicTypes.ClinicTypeName.Content = clinicTypeName;
                        uC_RV_ClinicTypes.ClinicAvailabilityToday.Content = availability;

                        uC_RV_ClinicTypes.ClinicTypeID = Convert.ToInt32(reader["ClinicType_ID"]);
                        uC_RV_ClinicTypes.Availability = availability;

                        // Subscribe to the custom event
                        uC_RV_ClinicTypes.MyClinicTypeClicked += UC_RV_ClinicTypes_ClinicTypeClicked;


                        // Adjust the width of the user control to match the width of the parent container
                        ViewClinicTypes_WrapP.SizeChanged += (sender, e) =>
                        {
                            uC_RV_ClinicTypes.Width = ViewClinicTypes_WrapP.ActualWidth - uC_RV_ClinicTypes.Margin.Left - uC_RV_ClinicTypes.Margin.Right;
                            //Debug.WriteLine("\n Width:" + uC_RV_ClinicTypes.Width);
                            //Debug.WriteLine("\n clinicAvailability_WrapP.ActualWidth:" + ViewClinicTypes_WrapP.ActualWidth);
                        };
                        uC_RV_ClinicTypes.Width = ViewClinicTypes_WrapP.ActualWidth - uC_RV_ClinicTypes.Margin.Left - uC_RV_ClinicTypes.Margin.Right;

                        ViewClinicTypes_WrapP.Children.Add(uC_RV_ClinicTypes);
                    }
                    reader.Close();


                }

                catch (Exception ex)
                {
                    Debug.WriteLine("\nError1: \n" + ex.Message);
                    MessageBox.Show("Error1: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
        }

        private void UC_RV_ClinicTypes_ClinicTypeClicked(object? sender, MyClinicTypeEventArgs e)
        {
            ClinicEvents_WrapP.Children.Clear();

            // Call the method in the main form with the clinic type and availability details
            HandleClinicTypeClicked(e.ClinicTypeID, e.Availability);
        }

        private void HandleClinicTypeClicked(int clinicTypeID, string availability)
        {
            // Your method implementation here
            //MessageBox.Show($"Clinic Type: {clinicTypeID}\nAvailability: {availability}", "Clinic Details", MessageBoxButton.OK, MessageBoxImage.Information);

            using (SqlConnection connection = new Database_Connector().GetConnection())
            {
                string query1 = "SELECT ce.*, d.D_NameWithInitials, d.D_Specialty, ce.CE_HallNumber, ce.CE_StartTime,"+
                    " ce.CE_EndTime, ce.CE_Date, ce.CE_TotalSlots, ce.CE_TakenSlots, ct.CT_WardNo " +
                    " FROM ClinicEvents ce" +
                    " INNER JOIN ClinicType ct ON ce.CE_ClinicType_ID = ct.ClinicType_ID" +
                    " INNER JOIN Doctor d ON ce.Doctor_ID = d.Doctor_ID" +
                    " WHERE ce.CE_ClinicType_ID = @clinicTypeID;";

                SqlCommand cmd = new SqlCommand(query1, connection);
                cmd.Parameters.AddWithValue("@clinicTypeID", Convert.ToInt32(clinicTypeID));

                try
                {
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Debug.WriteLine("\nD_NameWithInitials: \n" + reader["D_NameWithInitials"].ToString());
                        Debug.WriteLine("\nD_Specialty: \n" + reader["D_Specialty"].ToString());
                        Debug.WriteLine("\nCE_HallNumber: \n" + reader["CE_HallNumber"].ToString());
                        Debug.WriteLine("\nCE_StartTime: \n" + (TimeSpan)reader["CE_StartTime"]);
                        Debug.WriteLine("\nCE_TotalSlots: \n" + Convert.ToInt32(reader["CE_TotalSlots"]));
                        Debug.WriteLine("\nCE_TakenSlots: \n" + Convert.ToInt32(reader["CE_TakenSlots"]));


                        UC_RV_ClinicEvents uC_RV_ClinicEvents = new UC_RV_ClinicEvents();

                        uC_RV_ClinicEvents.doctorName.Content = reader["D_NameWithInitials"].ToString();
                        uC_RV_ClinicEvents.d_Specialty.Content = reader["D_Specialty"].ToString();
                        uC_RV_ClinicEvents.clinicLocation.Content = reader["CE_HallNumber"].ToString();
                        uC_RV_ClinicEvents.clinicWardNo.Content = reader["CT_WardNo"].ToString();

                        TimeSpan ceStartTime = (TimeSpan)reader["CE_StartTime"];
                        TimeSpan ceEndTime = (TimeSpan)reader["CE_EndTime"];
                        string formattedStartTime = DateTime.Today.Add(ceStartTime).ToString("hh:mm tt"); // "01:00 PM"
                        string formattedEndTime = DateTime.Today.Add(ceEndTime).ToString("hh:mm tt"); // "01:00 PM"

                        uC_RV_ClinicEvents.clinicEventTime.Content = $"{formattedStartTime} to {formattedEndTime}";

                        DateTime ceDate = (DateTime)reader["CE_Date"];
                        string formattedDate = ceDate.ToString("dd'\\t\\h' MMMM yyyy"); // "31st May 2024"

                        Debug.WriteLine("\nformattedDate \n" + formattedDate);

                        uC_RV_ClinicEvents.clinicEventDate.Content = formattedDate;

                        uC_RV_ClinicEvents.ClinicEventTotalSlots.Content = reader["CE_TotalSlots"].ToString();

                        int totalSlots = Convert.ToInt32(reader["CE_TotalSlots"]);
                        int takenSlots = Convert.ToInt32(reader["CE_TakenSlots"]);

                        uC_RV_ClinicEvents.ClinicAvailableSlots.Content = (totalSlots - takenSlots).ToString();



                        // Adjust the width of the user control to match the width of the parent container
                        ClinicEvents_WrapP.SizeChanged += (sender, e) =>
                        {
                            uC_RV_ClinicEvents.Width = ClinicEvents_WrapP.ActualWidth - uC_RV_ClinicEvents.Margin.Left - uC_RV_ClinicEvents.Margin.Right;
                            //Debug.WriteLine("\n Width:" + uC_RV_ClinicTypes.Width);
                            //Debug.WriteLine("\n clinicAvailability_WrapP.ActualWidth:" + ViewClinicTypes_WrapP.ActualWidth);
                        };
                        uC_RV_ClinicEvents.Width = ClinicEvents_WrapP.ActualWidth - uC_RV_ClinicEvents.Margin.Left - uC_RV_ClinicEvents.Margin.Right;

                        ClinicEvents_WrapP.Children.Add(uC_RV_ClinicEvents);
                    }
                    reader.Close();


                }

                catch (Exception ex)
                {
                    Debug.WriteLine("\nError1: \n" + ex.Message);
                    MessageBox.Show("Error1: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }

        }
    }
}
