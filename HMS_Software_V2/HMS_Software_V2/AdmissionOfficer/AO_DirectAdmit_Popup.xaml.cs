using HMS_Software_V2._DataManage_Classes;
using HMS_Software_V2.AdmissionOfficer.UserControls_AO;
using HMS_Software_V2.General_Purpose;
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
using System.Windows.Media.Converters;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HMS_Software_V2.AdmissionOfficer
{
    /// <summary>
    /// Interaction logic for AO_DirectAdmit_Popup.xaml
    /// </summary>
    /// 
    public partial class AO_DirectAdmit_Popup : Window
    {
        private AO_Dashboard dashboardWindow; // Reference to the AO_Dashboard window
        public AO_DirectAdmit_Popup(AO_Dashboard dashboard)
        {
            InitializeComponent();
            dashboardWindow = dashboard;
        }

        bool IsGoingToAdmit_Flag = false;
        
        private void Confirm_btn_Click(object sender, RoutedEventArgs e)
        {
            if(patientRID_tbx.Text == "")
            {
                MessageBox.Show("Please enter the Patient Registration ID.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            int patientId;
            using (SQLiteConnection connection = new Database_Connector().GetConnection())
            {
                string query1 = "SELECT Patient_ID FROM Patient WHERE P_RegistrationID = @P_RegistrationID AND P_CurrentStatus = 'Out-Patient'";

                SQLiteCommand cmd = new SQLiteCommand(query1, connection);

                try
                {
                    connection.Open();
                    cmd.Parameters.AddWithValue("@P_RegistrationID", "P"+patientRID_tbx.Text);

                    SQLiteDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        patientId = Convert.ToInt32(reader["Patient_ID"]);

                        IsGoingToAdmit_Flag = true;


                        MyMoveToPatientAdmitWindow(patientId);
                     
                    }
                    else
                    {
                        MessageBox.Show("No patient found or patient is already an inpatient.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    
                    }

                }
                catch (SQLiteException ex)
                {
                    Debug.WriteLine("\nError1: \n" + ex.Message);
                    MessageBox.Show("Error1: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    // Close the connection
                    connection.Close();
                }
            }
        }

        private void MyMoveToPatientAdmitWindow(int patientId)
        {

            HMS_Software_V2._DataManage_Classes.SharedData.admissioOfficer = new HMS_Software_V2._DataManage_Classes.AdmissioOfficer(); // Get a new copy of the template

            SharedData.admissioOfficer.PatientID = patientId;

            this.Close();
        }

        private void AO_DirectAdmit_Popup1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
  
            if (IsGoingToAdmit_Flag)
            {
                AO_DirectAdmit aO_DirectAdmit = new AO_DirectAdmit();
                aO_DirectAdmit.Show();

                dashboardWindow.IsFormClosing = false;
                dashboardWindow.Close();

            } // Open the dashboard window
            else
            {
                
            }// Open the Admit Patient window
        }
    }
}
