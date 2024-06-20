using HMS_Software_V2.General_Purpose;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HMS_Software_V2.Admin.Admin_UserControls
{
    /// <summary>
    /// Interaction logic for UC_A_Appointments.xaml
    /// </summary>
    public partial class UC_A_Appointments : UserControl
    {
        public UC_A_Appointments()
        {
            InitializeComponent();
            MyGetAppointmentData();
        }

        private void MyGetAppointmentData()
        {
            using (SqlConnection connection = new Database_Connector().GetConnection())
            {
                try
                {
                    connection.Open();

                    #region Get Total Todays Appointment Count
                    string query2 = "SELECT COUNT(*) FROM ClinicEvents WHERE CE_Date = @CE_Date";
                    using (SqlCommand command2 = new SqlCommand(query2, connection))
                    {
                        // Add the parameter and set its value to today's date
                        command2.Parameters.AddWithValue("@CE_Date", DateTime.Today);

                        int count = (int)command2.ExecuteScalar();
                        todaysAppointments_lbl.Content = count.ToString();
                    }

                    #endregion
 

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                finally
                {
                    connection.Close();
                }


            }
        }
    }
}
