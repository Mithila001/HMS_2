using HMS_Software_V2.General_Purpose;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HMS_Software_V2._DataManage_Classes
{
    // Put stuff that you want to be defualt values when a new Doctor Login.
    // This is just a template. 
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    public class MedicalEvnent
    {
        public int MedicalEventID { get; set; }
        public int PatientID { get; set; }
        public int DoctorID { get; set; }
        public int NurseID { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }
        public string Location { get; set; }
        public bool IsLabRequest { get; set; }
        public bool IsPrescriptionRequest { get; set; }
        public bool IsAppointmentRequest { get; set; }
        public string PersonExaminationNote { get; set; }
        public string PatientMedicalCondition { get; set; }
        public bool IsInpationt { get; set; }

        public string PatientName { get; set; }
        public string PatientAge { get; set; }
        public string PatientGender { get; set; }



        #region LabData Managemet List
        private List<(int, string)> labInvestigations = new List<(int, string)>();
        public List<(int, string)> Raw_LabInvestigations
        {
            get { return labInvestigations; }
        }

        private List<(int, string)> labSpeciment = new List<(int, string)>();
        public List<(int, string)> Raw_LabSpeciment
        {
            get { return labSpeciment; }
        }
        
        


        #endregion


        public MedicalEvnent()
        {
            MedicalEventID = 0;
            PatientID = 0;
            DoctorID = 0;
            NurseID = 0;
            Date = new DateOnly();
            Time = new TimeOnly();
            Location = string.Empty;
            IsLabRequest = false;
            IsPrescriptionRequest = false;
            IsAppointmentRequest = false;
            PersonExaminationNote = string.Empty;
            PatientMedicalCondition = string.Empty;
            IsInpationt = false;

            PatientName = string.Empty;
            PatientAge = string.Empty;
            PatientGender = string.Empty;

            GetLabInfoFromDatabase();
        }

        private void GetLabInfoFromDatabase()
        {

            using (SqlConnection connection = new Database_Connector().GetConnection())
            {
                string query1 = "SELECT * FROM Lab_Investigation";
                string query2 = "SELECT * FROM Lab_Specimen";

                try
                {
                    connection.Open();


                    SqlCommand cmd1 = new SqlCommand(query1, connection);
                    SqlDataReader reader1 = cmd1.ExecuteReader();
                    while (reader1.Read())
                    {
                        int id = (int)reader1["Lab_Investigation_ID"];
                        string name = (string)reader1["Lab_Investigation_Name"];

                        Raw_LabInvestigations.Add((id, name));
                    }
                    reader1.Close();

                    SqlCommand cmd2 = new SqlCommand(query2, connection);
                    SqlDataReader reader2 = cmd2.ExecuteReader();
                    while (reader2.Read())
                    {
                        int id = (int)reader2["Lab_Specimen_ID"];
                        string name = (string)reader2["Lab_Specimen_Name"];

                        Raw_LabSpeciment.Add((id, name));
                    }
                    reader2.Close();
                }

                catch (Exception ex)
                {
                    Debug.WriteLine("\nError1 (from a Medical Event Class): \n" + ex.Message);
                    MessageBox.Show("Error1_MEclass: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }

        }
    }
}
