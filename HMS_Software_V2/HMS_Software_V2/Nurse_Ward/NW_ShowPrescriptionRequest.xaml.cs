using HMS_Software_V2._DataManage_Classes;
using HMS_Software_V2.General_Purpose;
using HMS_Software_V2.Nurse_Ward.NuresWard_UserControls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
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

namespace HMS_Software_V2.Nurse_Ward
{
    /// <summary>
    /// Interaction logic for NW_ShowPrescriptionRequest.xaml
    /// </summary>
    public partial class NW_ShowPrescriptionRequest : Window
    {
        NW_PatientTreat NureseTreateFormReferece;
        public NW_ShowPrescriptionRequest(NW_PatientTreat parentFormReferece)
        {
            InitializeComponent();
            Debug.WriteLine("\n-----------------NW_ShowPrescriptionRequest Triggerd---------------\n");
            NureseTreateFormReferece = parentFormReferece;

            todayTime_lbl.Content = DateTime.Now.ToString("HH:mm:ss");

            MyGetTableData();
        }

        private void MyGetTableData()
        {
            Debug.WriteLine("---MyGetTableData Triggerd---");
            using (SqlConnection connection = new Database_Connector().GetConnection())
            {

                DataTable dataTable = new DataTable();
                // Define columns.
                dataTable.Columns.Add("Medicin", typeof(string));
                dataTable.Columns.Add("Route", typeof(string));
                dataTable.Columns.Add("Dosage", typeof(string));
                dataTable.Columns.Add("Frequency", typeof(string));
                dataTable.Columns.Add("Duration", typeof(string));
                dataTable.Columns.Add("LabelNo", typeof(string));

                try
                {
                    connection.Open();

                    string query = "SELECT * FROM Patient_PrescriptionRequest WHERE PatientMedicalEvent_ID = @PatientMedicalEvent_ID";

                    SqlCommand command = new SqlCommand(query, connection);

                    command.Parameters.AddWithValue("@PatientMedicalEvent_ID", SharedData.Ward_NursePatient.PatientMedicalEventID);
                    Debug.WriteLine("---PatientMedicalEventID: " + SharedData.Ward_NursePatient.PatientMedicalEventID);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string route = reader["PR_Route"].ToString() ?? "No Data";
                            string medicin = reader["PR_Medicin"].ToString() ?? "No Data";
                            string dosage = reader["PR_Dosage"].ToString() ?? "No Data";
                            string frequency = reader["PR_Frequency"].ToString() ?? "No Data";
                            string duration = reader["PR_Duration"].ToString() ?? "No Data";
                            string label = reader["LabelName"].ToString() ?? "No Data";
                            bool isRowCompleted = reader["Is_TableRowCompleted"] != DBNull.Value && Convert.ToBoolean(reader["Is_TableRowCompleted"]);

                            Debug.WriteLine($"Route: {route}");
                            Debug.WriteLine($"Medicin: {medicin}");
                            Debug.WriteLine($"Dosage: {dosage}");
                            Debug.WriteLine($"Frequency: {frequency}");
                            Debug.WriteLine($"Duration: {duration}");
                            Debug.WriteLine($"Label: {label}");
                            Debug.WriteLine($"Is Row Completed: {isRowCompleted}");



                            MyDisplayTable(dataTable,route, medicin, dosage, frequency, duration, label, isRowCompleted);

                        }

                    }


                }
                catch (Exception ex)
                {
                    Debug.WriteLine("\nError9: \n" + ex.Message);
                    MessageBox.Show("Error9: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private void MyDisplayTable(DataTable dataTable,string route, string medicin, string dosage, string frequency, string duration, string label, bool isRowCompleted)
        {

            string m_dosage = dosage.Replace(",", " ");
            string m_duration = duration.Replace(",", " ");

            
            dataTable.Rows.Add(medicin,route,m_dosage,frequency,m_duration,label);
           
            showPrescriptionReq_DataGrid.ItemsSource = dataTable.DefaultView;

        }

        private void exit_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void NW_ShowPrescriptionRequest1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            NureseTreateFormReferece.Show();
        }
    }
}
