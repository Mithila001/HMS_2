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

namespace HMS_Software_V2.UserCommon_Forms.UserControls_UCF
{
    /// <summary>
    /// Interaction logic for PMH_ViewProgressReporsts.xaml
    /// </summary>
    public partial class PMH_ViewProgressReporsts : Window
    {
        public PMH_ViewProgressReporsts()
        {
            InitializeComponent();

            MyLoadData();
        }

        private void MyLoadData()
        {
            using (SQLiteConnection connection = new Database_Connector().GetConnection())
            {
                string query1 = @"
                        SELECT 
                            PME.PME_Time,
                            PME.PME_PatientExaminationNote, 
                            D.D_NameWithInitials 
                        FROM 
                            PatientMedical_Event PME
                        INNER JOIN 
                            Doctor D ON PME.PME_Doctor_ID = D.Doctor_ID
                        WHERE 
                            PME.PatientMedicalEvent_ID = @PatientMedicalEvent_ID";


                SQLiteCommand cmd = new SQLiteCommand(query1, connection);

                cmd.Parameters.AddWithValue("@PatientMedicalEvent_ID", SharedData.viewPatientHistory.PatientMedicalEventID);
                Debug.WriteLine("PatientMedicalEvent_ID" + SharedData.viewPatientHistory.PatientMedicalEventID);

                try
                {
                    connection.Open();
                    SQLiteDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Debug.WriteLine("Reading data from the database");

                        string examinationNotes = reader["PME_PatientExaminationNote"].ToString() ?? "Error";
                        string doctorName = reader["D_NameWithInitials"].ToString() ?? "Error";


                        UC_UCF_PMH_ShowProgressNotes uC_UCF_PMH_ShowProgressNotes = new UC_UCF_PMH_ShowProgressNotes(examinationNotes);

                        uC_UCF_PMH_ShowProgressNotes.WroteDoctorName_lbl.Content = doctorName;


                        // Adjust the width of the user control to match the width of the parent container
                        showProgressNotes_WrapP.SizeChanged += (sender, e) =>
                        {
                            uC_UCF_PMH_ShowProgressNotes.Width = showProgressNotes_WrapP.ActualWidth - uC_UCF_PMH_ShowProgressNotes.Margin.Left - uC_UCF_PMH_ShowProgressNotes.Margin.Right;

                        };

                        showProgressNotes_WrapP.Children.Add(uC_UCF_PMH_ShowProgressNotes );

                    }
                    reader.Close();


                }

                catch (SQLiteException ex)
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
