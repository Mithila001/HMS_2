using HMS_Software_V2.General_Purpose;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
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
    /// Interaction logic for UC_A_Home.xaml
    /// </summary>
    public partial class UC_A_Home : UserControl
    {
        public UC_A_Home()
        {
            InitializeComponent();
            MyLoadData();
        }

        private void MyLoadData()
        {
            using (SQLiteConnection connection = new Database_Connector().GetConnection())
            {
                try
                {
                    connection.Open();

                    #region Get Total Doctors Count
                    string query2 = "SELECT COUNT(*) FROM Doctor";
                    using (SQLiteCommand command2 = new SQLiteCommand(query2, connection))
                    {

                        int count = Convert.ToInt32(command2.ExecuteScalar());
                        totalDoctors_lbl.Content = count.ToString();
                    }

                    #endregion

                    #region Get Total Nurses Count
                    string query3 = "SELECT COUNT(*) FROM Nurse";
                    using (SQLiteCommand command2 = new SQLiteCommand(query3, connection))
                    {

                        int count = Convert.ToInt32(command2.ExecuteScalar());
                        totalNurses_lbl.Content = count.ToString();
                    }

                    #endregion

                    #region Get Total In Patient Count
                    string query4 = "SELECT COUNT(*) FROM Admitted_Patients";
                    using (SQLiteCommand command2 = new SQLiteCommand(query4, connection))
                    {

                        int count = Convert.ToInt32(command2.ExecuteScalar());
                        totalPatients_lbl.Content = count.ToString();
                    }

                    #endregion

                    #region Get Total Reception Count
                    string query5 = "SELECT COUNT(*) FROM Reception";
                    using (SQLiteCommand command2 = new SQLiteCommand(query5, connection))
                    {

                        int count = Convert.ToInt32(command2.ExecuteScalar());
                        totalReceptions_lbl.Content = count.ToString();
                    }

                    #endregion

                    #region Get Total Todays Appointment Count
                    string query6 = "SELECT COUNT(*) FROM ClinicEvents WHERE CE_Date = @CE_Date";
                    using (SQLiteCommand command2 = new SQLiteCommand(query6, connection))
                    {
                        // Add the parameter and set its value to today's date
                        command2.Parameters.AddWithValue("@CE_Date", DateTime.Today);

                        int count = Convert.ToInt32(command2.ExecuteScalar());
                        totalTodaysClinics_lbl.Content = count.ToString();
                    }

                    #endregion

                    #region Get Total Ward Count
                    string query7 = "SELECT COUNT(*) FROM WardTypes";
                    using (SQLiteCommand command2 = new SQLiteCommand(query7, connection))
                    {

                        int count =Convert.ToInt32(command2.ExecuteScalar());
                        totalWards_lbl.Content = count.ToString();
                    }

                    #endregion



                }
                catch (SQLiteException ex)
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
