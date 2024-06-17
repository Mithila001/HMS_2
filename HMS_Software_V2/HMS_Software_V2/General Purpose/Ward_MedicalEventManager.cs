using HMS_Software_V2._DataManage_Classes;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HMS_Software_V2.General_Purpose
{
    internal class Ward_MedicalEventManager
    {

        #region Time Zones

        TimeSpan Round1_Time = new TimeSpan(00, 00, 0); // 00:00:00 (Midnight)
        TimeSpan Round2_Time = new TimeSpan(09, 00, 0); // 09:00:00 (9 AM)
        TimeSpan Round3_Time = new TimeSpan(23, 59, 0); // 23:59:00 (11:59 PM)

        TimeSpan CurrentTime = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second); //09:52:23 in 24h format 
        #endregion


        #region Variabals
        DateTime LastRecordedDate;
        bool Round1;
        bool Round2;
        bool Round3;
        int RoundRecordID;

        string? SelectedRound;
        int SlectedRoundNumber;
        #endregion

        public void MyStart()
        {
            Debug.WriteLine("\n\n<< --------------Ward_MedicalEventManager ----------------- >>\n");
            MyMedicalEventManger_Start();
        }
        private void MyMedicalEventManger_Start()
        {

            using (SqlConnection connection = new Database_Connector().GetConnection())
            {

                try
                {
                    connection.Open();


                    #region Getting Admit_MedicalRoundManager Table Data

                    string query1 = "SELECT TOP 1 LastRecorded_Date, Round_1, Round_2, Round_3, AdmitMedicalRoundManager_ID" +
                        " FROM Admit_MedicalRoundManager" +
                        " ORDER BY LastRecorded_Date DESC";

                    SqlCommand cmd = new SqlCommand(query1, connection);

                    using(SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            LastRecordedDate = Convert.ToDateTime(reader["LastRecorded_Date"]);
                            Round1 = Convert.ToBoolean(reader["Round_1"]);
                            Round2 = Convert.ToBoolean(reader["Round_2"]);
                            Round3 = Convert.ToBoolean(reader["Round_3"]);
                            RoundRecordID = Convert.ToInt32(reader["AdmitMedicalRoundManager_ID"]);

                            Debug.WriteLine("Getting Admit_MedicalRoundManager Table Data ");


                        }
                        else
                        {
                            MessageBox.Show("WMM Class Error1: ", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }

                    }
                   
                    #endregion


                    if (LastRecordedDate.Date == DateTime.Today)
                    {
                        // Check witch time zone is now
                        if (Round1_Time > CurrentTime)
                        {
                            Debug.WriteLine("In Round 1");
                            SelectedRound = "Round_1";
                            SlectedRoundNumber = 1;

                            if (!Round1) // If not Checked in that timzone
                            {
                                Debug.WriteLine("ADD new In Round 1 State");
                                MyCreateAdmitEvent_Step_1(); //Update the record
                            }
                            else
                            {
                                Debug.WriteLine("Already Checked in Round 1");
                                  
                            }
                        }
                        else if (Round2_Time > CurrentTime)
                        {
                            Debug.WriteLine("In Round 2");
                            SelectedRound = "Round_2";
                            SlectedRoundNumber = 2;

                            if (!Round2)
                            {
                                Debug.WriteLine("ADD new Round 2 State");
                                MyCreateAdmitEvent_Step_1();
                            }
                            else
                            {
                                Debug.WriteLine("Already Checked in Round 2");
                                
                            }
                        }
                        else
                        {
                            SelectedRound = "Round_3";
                            SlectedRoundNumber = 3;
                            Debug.WriteLine("In Round 3");

                            if (!Round3)
                            {
                                Debug.WriteLine("ADD new In Round 3 State");
                                MyCreateAdmitEvent_Step_1();
                            }
                            else
                            {
                                Debug.WriteLine("Already Checked in Round 3");
                                

                            }
                        }
                    } // If alrady created a record today
                    else
                    {
                        #region INSERT INTO Admit_MedicalRoundManager Table
                        string query3 = "INSERT INTO Admit_MedicalRoundManager (LastRecorded_Date) VALUES (@LastRecorded_Date)";

                        using (SqlCommand command1 = new SqlCommand(query3, connection))
                        {
                            command1.Parameters.AddWithValue("@LastRecorded_Date", DateTime.Today.ToString("yyyy-MM-dd"));

                            command1.ExecuteNonQuery();
                        }
                        #endregion

                        Debug.WriteLine("INSERT INTO Admit_MedicalRoundManager Table");

                        MyMedicalEventManger_Start();
                    }


                }
                catch (Exception ex)
                {
                    Debug.WriteLine("\n MyMedicalEventManger_Start: \n" + ex.Message);
                    MessageBox.Show("WMM Class Error2: : " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                finally
                {
                    connection.Close();
                }
                
            }

        }

        private void MyCreateAdmitEvent_Step_1()
        {
            Debug.WriteLine("<< MyCreateAdmitEvent_Step_1 >>");
            using (SqlConnection connection = new Database_Connector().GetConnection())
            {
                connection.Open();
                try
                {

                    #region UPDATE Old WardMedicalEvent Status in Admitted_Patients_VisitEvent Table
                    string query5 = $"UPDATE Admitted_Patients_VisitEvent SET Is_RoundTimeOut = 1";

                    using (SqlCommand command1 = new SqlCommand(query5, connection))
                    {
                        // No need to add SelectedRound as a parameter since it's part of the query
                        Debug.WriteLine("\nUPDATE Old WardMedicalEvent Status in Admitted_Patients_VisitEvent Table");

                        command1.ExecuteNonQuery();
                    }
                    #endregion

                    #region SELECT All Admitted Patients
                    string query1 = "SELECT AdmittedPatient_ID, Patient_ID, AP_Condition, AP_VisiteTotalRounds, AP_Ward FROM Admitted_Patients";

                    using (SqlCommand command = new SqlCommand(query1, connection))
                    {
                        using (SqlDataReader reader2 = command.ExecuteReader())
                        {
                            while (reader2.Read())
                            {
                                int admittedPatientId = Convert.ToInt32(reader2["AdmittedPatient_ID"]);
                                int patientId = Convert.ToInt32(reader2["Patient_ID"]);
                                string condition = reader2["AP_Condition"].ToString()?? "Error";
                                int totalRounds = Convert.ToInt32(reader2["AP_VisiteTotalRounds"]);
                                int ward = Convert.ToInt32(reader2["AP_Ward"]);

                                Debug.WriteLine("SELECT All Admitted Patients");

                                MyCreateAdmitEvent_Step_2(admittedPatientId, patientId, condition, totalRounds, ward);


                            }
                        }
                    }


                    #endregion

                }
                catch (Exception ex)
                {
                    Debug.WriteLine("\n MyCreateAdmitEvent_Step_1: \n" + ex.Message);
                    MessageBox.Show("WMM Class Error3:  " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private void MyCreateAdmitEvent_Step_2(int admittedPatientId, int patientId, string condition, int totalRounds, int ward)
        {
            Debug.WriteLine("<< MyCreateAdmitEvent_Step_2 >>");
            using (SqlConnection connection = new Database_Connector().GetConnection())
            {
                connection.Open();
                try
                {
                    

                 

                    #region INSERT INTO Admitted_Patients_VisitEvent Table
                    string query1 = "INSERT INTO Admitted_Patients_VisitEvent (Patient_ID, P_Condition, Visited_Doctor_ID, Visited_Nurse_ID,"+
                        " Visite_Round,P_MedicalEventID, Is_VisistedByDoctor, Is_VisitedByNurse, P_WardNo, N_TreatmentStatus, Total_VisitRounds, VisitPerDay_ID)" +

                    " VALUES (@Patient_ID, @P_Condition, @Visited_Doctor_ID, @Visited_Nurse_ID, @Visite_Round,"+
                    " @P_MedicalEventID, @Is_VisistedByDoctor, @Is_VisitedByNurse, @P_WardNo, @N_TreatmentStatus, @Total_VisitRounds, @VisitPerDay_ID)";

                    using (SqlCommand command1 = new SqlCommand(query1, connection))
                    {
                        command1.Parameters.AddWithValue("@Patient_ID", patientId);
                        command1.Parameters.AddWithValue("@P_Condition", condition);
                        command1.Parameters.AddWithValue("@Visited_Doctor_ID", 0);
                        command1.Parameters.AddWithValue("@Visited_Nurse_ID", 0);
                        command1.Parameters.AddWithValue("@Visite_Round", SlectedRoundNumber);
                        command1.Parameters.AddWithValue("@P_MedicalEventID", 0);
                        command1.Parameters.AddWithValue("@Is_VisistedByDoctor", false);
                        command1.Parameters.AddWithValue("@Is_VisitedByNurse", false);
                        command1.Parameters.AddWithValue("@P_WardNo", ward);
                        command1.Parameters.AddWithValue("@N_TreatmentStatus", "Waiting");
                        command1.Parameters.AddWithValue("@Total_VisitRounds", totalRounds);
                        command1.Parameters.AddWithValue("@VisitPerDay_ID", RoundRecordID);

                        command1.ExecuteNonQuery();
                    }
                    #endregion

                    MyUpdate_MedicalRoundManager();

                    Debug.WriteLine("INSERT INTO Admitted_Patients_VisitEvent Table");

                }
                catch (Exception ex)
                {
                    Debug.WriteLine("\nMyCreateAdmitEvent_Step_2: \n" + ex.Message);
                    MessageBox.Show("WMM Class Error4: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                finally
                {
                    connection.Close();
                }
            }

        }

        private void MyUpdate_MedicalRoundManager()
        {
            using (SqlConnection connection = new Database_Connector().GetConnection())
            {
                connection.Open();
                try
                {
                    #region UPDATE Admit_MedicalRoundManager Table
                    string query1 = $"UPDATE Admit_MedicalRoundManager SET {SelectedRound} = 1 WHERE AdmitMedicalRoundManager_ID = @AdmitMedicalRoundManager_ID";

                    using (SqlCommand command1 = new SqlCommand(query1, connection))
                    {
                        // No need to add SelectedRound as a parameter since it's part of the query
                        Debug.WriteLine("\nRoundRecordID: \n" + RoundRecordID);
                        Debug.WriteLine("\nSelectedRound: \n" + SelectedRound);
                        command1.Parameters.AddWithValue("@AdmitMedicalRoundManager_ID", RoundRecordID);

                        command1.ExecuteNonQuery();
                    }
                    #endregion

                    Debug.WriteLine("UPDATE Admit_MedicalRoundManager Table");

                }
                catch (Exception ex)
                {
                    Debug.WriteLine("\nWard_MedicalEventManager: \n" + ex.Message);
                    MessageBox.Show("WMM Class Error5: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
