using HMS_Software_V2._DataManage_Classes;
using HMS_Software_V2.General_Purpose;
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

namespace HMS_Software_V2.Nurse_Ward.NuresWard_UserControls
{
    /// <summary>
    /// Interaction logic for NW_MonitorPatient.xaml
    /// </summary>
    public partial class NW_MonitorPatient : Window
    {
        NW_PatientTreat nureseTreatementFormReferece;
        public NW_MonitorPatient(NW_PatientTreat nW_PatientTreat)
        {
            InitializeComponent();

            nureseTreatementFormReferece = nW_PatientTreat;

            MyGetPreviousAddedData();
        }

        string? AddedMonitorDetails;
        string? MonitorRequest;
        private void MyGetPreviousAddedData()
        {
            using (SqlConnection connection = new Database_Connector().GetConnection())
            {

                try
                {
                    connection.Open();

                    #region SELECT Monitro Request And Alrady Addeded Monitor info From PatientMedical_Event table
                    string query1 = "SELECT PME_MonitorRequest_Results, PME_MonitorRequest FROM PatientMedical_Event WHERE PatientMedicalEvent_ID = @PatientMedicalEvent_ID";
                    using (SqlCommand command = new SqlCommand(query1, connection))
                    {

                        command.Parameters.AddWithValue("@PatientMedicalEvent_ID", SharedData.Ward_NursePatient.PatientMedicalEventID);

                        using (SqlDataReader reader = command.ExecuteReader())
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

                catch (Exception ex)
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

        private void Confirm_btn_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(monitorInfoInput_TextBox.Text))
            {
                using (SqlConnection connection = new Database_Connector().GetConnection())
                {
                    try
                    {
                        connection.Open();

                        #region UPDATE PatientMedical_Event
                        string query2 = "UPDATE PatientMedical_Event SET" +
                        " PME_MonitorRequest_Results = @NewValue" +
                        " WHERE PatientMedicalEvent_ID = @PatientMedicalEvent_ID";

                        using (SqlCommand cmd = new SqlCommand(query2, connection))
                        {
                            cmd.Parameters.AddWithValue("@NewValue", monitorInfoInput_TextBox.Text);
                            cmd.Parameters.AddWithValue("@PatientMedicalEvent_ID", SharedData.Ward_NursePatient.PatientMedicalEventID);

                            Debug.WriteLine("Inputed Texts: ...." + "\r\n" + monitorInfoInput_TextBox.Text);

                            int rowsAffected = cmd.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                Debug.WriteLine("PatientMedical_Event update successful");

                                this.Close();
                                

                            }
                            else
                            {
                                Debug.WriteLine("PatientMedical_Event update failed: No rows affected.");
                                MessageBox.Show("Error: PatientMedical_Event update failed: No rows affected.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                        #endregion


                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("\nError2: \n" + ex.Message);
                        MessageBox.Show("Error2: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("Please enter the monitor information.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }

        private void NW_MonitorPatient1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            nureseTreatementFormReferece.Show();

        }
    }
}
