using HMS_Software_V2._DataManage_Classes;
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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HMS_Software_V2.UserCommon_Forms
{
    /// <summary>
    /// Interaction logic for PMH_ViewMonitorReuslts.xaml
    /// </summary>
    public partial class PMH_ViewMonitorReuslts : Window
    {
        public PMH_ViewMonitorReuslts()
        {
            InitializeComponent();

            MyGetPreviousAddedData();
        }

        string? AddedMonitorDetails;
        string? MonitorRequest;
        private void MyGetPreviousAddedData()
        {
            using (SQLiteConnection connection = new Database_Connector().GetConnection())
            {

                try
                {
                    connection.Open();

                    #region SELECT Monitro Request And Alrady Addeded Monitor info From PatientMedical_Event table
                    string query1 = "SELECT PME_MonitorRequest_Results, PME_MonitorRequest FROM PatientMedical_Event WHERE PatientMedicalEvent_ID = @PatientMedicalEvent_ID";
                    using (SQLiteCommand command = new SQLiteCommand(query1, connection))
                    {

                        command.Parameters.AddWithValue("@PatientMedicalEvent_ID", SharedData.viewPatientHistory.PatientMedicalEventID);

                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {

                                int isMonitorRequestResult_CI = reader.GetOrdinal("PME_MonitorRequest_Results");
                                AddedMonitorDetails = !reader.IsDBNull(isMonitorRequestResult_CI) ? reader.GetString(isMonitorRequestResult_CI) : string.Empty;

                                int isMonitorRequest_CI = reader.GetOrdinal("PME_MonitorRequest");
                                MonitorRequest = !reader.IsDBNull(isMonitorRequest_CI) ? reader.GetString(isMonitorRequest_CI) : string.Empty;

                                monitorInfoInput_TextBox.Text = AddedMonitorDetails;
                                monitoInforOutput_TextBlock.Text = MonitorRequest;

                            }
                            else
                            {
                                // Handle the case where no records are found
                                MessageBox.Show("No records found.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }
                    }
                    #endregion


                }

                catch (SQLiteException ex)
                {
                    Debug.WriteLine("\nError1: \n" + ex.Message);
                    MessageBox.Show("Error4: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}
