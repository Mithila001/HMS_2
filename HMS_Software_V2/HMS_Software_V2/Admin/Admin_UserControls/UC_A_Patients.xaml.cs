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
    /// Interaction logic for UC_A_Patients.xaml
    /// </summary>
    public partial class UC_A_Patients : UserControl
    {
        public UC_A_Patients()
        {
            InitializeComponent();
            MyGetPatientData();
        }


        private void MyGetPatientData()
        {
            using (SQLiteConnection connection = new Database_Connector().GetConnection())
            {
                try
                {
                    connection.Open();

                    #region Get Total OPD Patient Count
                    string query2 = "SELECT COUNT(*) FROM Patient WHERE P_CurrentStatus = 'Out-Patient' OR P_CurrentStatus = 'New Registered' ";
                    using (SQLiteCommand command2 = new SQLiteCommand(query2, connection))
                    {

                        int count = Convert.ToInt32(command2.ExecuteScalar());
                        totalOpdPatients.Content = count.ToString();
                    }

                    #endregion


                    #region Get Total In Patient Count
                    string query3 = "SELECT COUNT(*) FROM Admitted_Patients";
                    using (SQLiteCommand command2 = new SQLiteCommand(query3, connection))
                    {

                        int count = Convert.ToInt32(command2.ExecuteScalar());
                        totalInpatients.Content = count.ToString();
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

        private void PatientSearch_btn_Click(object sender, RoutedEventArgs e)
        {
            Admin_Patient_Search admin_Patient_Search = new Admin_Patient_Search();
            admin_Patient_Search.ShowDialog();
        }
    }
}
