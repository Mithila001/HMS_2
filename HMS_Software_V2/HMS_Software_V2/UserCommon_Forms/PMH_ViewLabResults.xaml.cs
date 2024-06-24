using HMS_Software_V2._DataManage_Classes;
using HMS_Software_V2.General_Purpose;
using HMS_Software_V2.UserCommon_Forms.UserControls_UCF;
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

namespace HMS_Software_V2.UserCommon_Forms
{
    /// <summary>
    /// Interaction logic for PMH_ViewLabResults.xaml
    /// </summary>
    public partial class PMH_ViewLabResults : Window
    {
        public PMH_ViewLabResults()
        {
            InitializeComponent();

            Debug.WriteLine("\nSharedData.viewPatientHistory.PatientMedicalEventID: " + SharedData.viewPatientHistory.PatientMedicalEventID);   

            MyLoadData();
        }

        private void MyLoadData()
        {
            using (SqlConnection connection = new Database_Connector().GetConnection())
            {
                string query1 = @"
                        SELECT 
                            PatientLabRequests_ID,
                            Lab_Specimen_ID,
                            Lab_Specimen_Name, 
                            Lab_Investigation_ID, 
                            Lab_Investigation_Name,
                            Is_Completed
                        FROM 
                           Patient_LabRequest
                        WHERE 
                            PatientMedicalEvent_ID = @PatientMedicalEvent_ID";


                SqlCommand cmd = new SqlCommand(query1, connection);

                cmd.Parameters.AddWithValue("@PatientMedicalEvent_ID", SharedData.viewPatientHistory.PatientMedicalEventID);

                try
                {
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {


                        int labrequestID = Convert.ToInt32(reader["PatientLabRequests_ID"]);
                        int specimentID = Convert.ToInt32(reader["Lab_Specimen_ID"]);
                        string specimentName = reader["Lab_Specimen_Name"].ToString() ?? "Error";
                        int investigationID = Convert.ToInt32(reader["Lab_Investigation_ID"]);
                        string investigationName = reader["Lab_Investigation_Name"].ToString() ?? "Error";
                        bool isCompleted = Convert.ToBoolean(reader["Is_Completed"]);




                        UC_UCF_PMH_ShowLabResults uC_UCF_PMH_ShowLabResults = new UC_UCF_PMH_ShowLabResults();

                        uC_UCF_PMH_ShowLabResults.specimentType_lbl.Content = specimentName;
                        uC_UCF_PMH_ShowLabResults.investigationName_lbl.Content = investigationName;



                        string labResultRaw = @"
                                            Patient Name: John Doe
                                            Date of Test: June 23, 2024
                                            Blood Test Results:
                                            - Hemoglobin (Hb): 14.5 g/dL
                                            - White Blood Cell Count (WBC): 7,200 cells/μL
                                            - Platelet Count: 250,000 cells/μL
                                            - Glucose: 95 mg/dL
                                            - Cholesterol (Total): 180 mg/dL
                                            - Creatinine: 1.0 mg/dL
                                            Liver Enzymes:
                                            - ALT (Alanine Aminotransferase): 25 U/L
                                            - AST (Aspartate Aminotransferase): 22 U/L
                                            ";

                        string labResultFinal = labResultRaw.Replace("\n", Environment.NewLine);

                        uC_UCF_PMH_ShowLabResults.labResults_tbx.Text = labResultRaw;


                        // Adjust the width of the user control to match the width of the parent container
                        showLabResults_WrapP.SizeChanged += (sender, e) =>
                        {
                            uC_UCF_PMH_ShowLabResults.Width = showLabResults_WrapP.ActualWidth - uC_UCF_PMH_ShowLabResults.Margin.Left - uC_UCF_PMH_ShowLabResults.Margin.Right;

                        };

                        showLabResults_WrapP.Children.Add(uC_UCF_PMH_ShowLabResults);

                    }
                    reader.Close();


                }

                catch (Exception ex)
                {
                    Debug.WriteLine("\nError1: \n" + ex.Message);
                    MessageBox.Show("Error1: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    connection.Close();
                }
            }

        }
    }
}
