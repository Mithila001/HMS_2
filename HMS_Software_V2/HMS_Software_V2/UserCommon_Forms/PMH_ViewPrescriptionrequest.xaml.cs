using HMS_Software_V2._DataManage_Classes;
using HMS_Software_V2.General_Purpose;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data;
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
    /// Interaction logic for PMH_ViewPrescriptionrequest.xaml
    /// </summary>
    public partial class PMH_ViewPrescriptionrequest : Window
    {
        public PMH_ViewPrescriptionrequest()
        {
            InitializeComponent();

            MyGetTableData();
        }

        private void MyGetTableData()
        {
            Debug.WriteLine("---MyGetTableData Triggerd---");
            using (SQLiteConnection connection = new Database_Connector().GetConnection())
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

                    SQLiteCommand command = new SQLiteCommand(query, connection);

                    command.Parameters.AddWithValue("@PatientMedicalEvent_ID", SharedData.viewPatientHistory.PatientMedicalEventID);
                   
                    using (SQLiteDataReader reader = command.ExecuteReader())
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



                            MyDisplayTable(dataTable, route, medicin, dosage, frequency, duration, label, isRowCompleted);

                        }

                    }


                }
                catch (SQLiteException ex)
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

        private void MyDisplayTable(DataTable dataTable, string route, string medicin, string dosage, string frequency, string duration, string label, bool isRowCompleted)
        {

            string m_dosage = dosage.Replace(",", " ");
            string m_duration = duration.Replace(",", " ");


            dataTable.Rows.Add(medicin, route, m_dosage, frequency, m_duration, label);

            showPrescriptionReq_DataGrid.ItemsSource = dataTable.DefaultView;

        }
    }
}
