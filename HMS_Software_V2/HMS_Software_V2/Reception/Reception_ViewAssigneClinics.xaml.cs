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
            LoadClinicType();
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
                        uC_RV_ClinicTypes.ClinicType = clinicTypeName;
                        uC_RV_ClinicTypes.Availability = availability;

                        // Subscribe to the custom event
                        uC_RV_ClinicTypes.ClinicTypeClicked += UC_RV_ClinicTypes_ClinicTypeClicked;


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
            // Call the method in the main form with the clinic type and availability details
            HandleClinicTypeClicked(e.ClinicType, e.Availability);
        }

        private void HandleClinicTypeClicked(string clinicType, string availability)
        {
            // Your method implementation here
            MessageBox.Show($"Clinic Type: {clinicType}\nAvailability: {availability}", "Clinic Details", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
