using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


// To  UpdateLastExecutionDate() to be properlt funtion,ExecutionLog shouldn't be null


namespace HMS_Software_V1._01
{
    internal class MyTableData_Automation
    {
        /*SqlConnection connect = new SqlConnection(MyCommonConnecString.ConnectionString);*/


        public void MyGetAdmittedPatientRecord()
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(MyCommonConnecString.ConnectionString))
                {
                    connect.Open();
                    if (!HasCodeExecutedToday())
                    {
                        Console.WriteLine("HasCodeExecutedToday Executed");

                        string query = "SELECT * FROM Admitted_Patients";
                        using (SqlCommand command = new SqlCommand(query, connect))
                        {
                            
                            try
                            {
                                SqlDataReader reader = command.ExecuteReader();

                                while (reader.Read())
                                {
                                    // Retrieve data from Admitted_Patients table
                                    string p_RID = reader["P_RID"].ToString();
                                    string p_NameWithInitials = reader["P_NameWithInitials"].ToString();
                                    string p_Age = reader["P_Age"].ToString();
                                    string p_Gender = reader["P_Gender"].ToString();
                                    string p_Admit_To = reader["P_Admit_To"].ToString();
                                    string p_Condition = reader["P_Condition"].ToString();
                                    int p_Visit_TotalRound = Convert.ToInt32(reader["P_Visite_TotalRounds"]);
                                    string p_Ward = reader["P_Ward"].ToString();

                                    // Create a new visit event record in Admitted_Patients_VisitEvent table
                                    MyCreateDailyAdmitVisitEvent(p_RID, p_NameWithInitials, p_Age, p_Gender, p_Admit_To, p_Condition, p_Ward, p_Visit_TotalRound);
                                }

                                reader.Close();
                            }

                            catch (Exception ex)
                            {
                                // Handle exceptions
                                Console.WriteLine("Error11: " + ex.Message);
                            }
                        }
                        UpdateLastExecutionDate();
                    }

                }
                    // Check if the code has already run today         

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error22: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        //If Execution not happend today
        private bool HasCodeExecutedToday()
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(MyCommonConnecString.ConnectionString))
                {
                    connect.Open();
                    string query = "SELECT LastExecutionDate FROM ExecutionLog";
                    using (SqlCommand command = new SqlCommand(query, connect))
                    {
                        object result = command.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            DateTime lastExecutionDate = Convert.ToDateTime(result);
                            return lastExecutionDate.Date == DateTime.Today;

                        }
                    }

                }

                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error checking last execution date: " + ex.Message);
            }
            

            return false;
        }

        //Update Last Execution Value
        private void UpdateLastExecutionDate()
        {
            Console.WriteLine("UpdateLastExecutionDate Executed");
            try
            {
                
                using (SqlConnection connect = new SqlConnection(MyCommonConnecString.ConnectionString))
                {
                    connect.Open();
                    string query = "UPDATE ExecutionLog SET LastExecutionDate = @LastExecutionDate";
                    using (SqlCommand command = new SqlCommand(query, connect))
                    {
                        command.Parameters.AddWithValue("@LastExecutionDate", DateTime.Today);
                        command.ExecuteNonQuery();
                    }

                }

                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error33 " + ex.Message);
            }
            
        }

        private void MyCreateDailyAdmitVisitEvent(string p_RID, string p_NameWithInitials, string p_Age, string p_Gender, string p_Admit_To, string p_Condition, string p_Ward, int p_Visit_TotalRound)
        {
            try
            {
                DateTime today = DateTime.Today;

                using (SqlConnection connect = new SqlConnection(MyCommonConnecString.ConnectionString))
                {
                    connect.Open();
                    string insertQuery = "INSERT INTO Admitted_Patients_VisitEvent (P_RID, P_NameWithInitials, P_Age," +
                    " P_Gender, P_Admit_To, P_Condition, P_Ward, Visite_Round, Visite_Date, Is_VisitedByDoctor) " +
                    "VALUES (@P_RID, @P_NameWithInitials, @P_Age, @P_Gender, @P_Admit_To, @P_Condition, @P_Ward, @Visite_Round,@visteDate, @Is_VisitedByDoctor)";
                    using (SqlCommand command = new SqlCommand(insertQuery, connect))
                    {
                        command.Parameters.AddWithValue("@P_RID", p_RID);
                        command.Parameters.AddWithValue("@P_NameWithInitials", p_NameWithInitials);
                        command.Parameters.AddWithValue("@P_Age", p_Age);
                        command.Parameters.AddWithValue("@P_Gender", p_Gender);
                        command.Parameters.AddWithValue("@P_Admit_To", p_Admit_To);
                        command.Parameters.AddWithValue("@P_Condition", p_Condition);
                        command.Parameters.AddWithValue("@P_Ward", p_Ward);
                        command.Parameters.AddWithValue("@Visite_Round", p_Visit_TotalRound + 1);
                        command.Parameters.AddWithValue("@visteDate", today);
                        command.Parameters.AddWithValue("@Is_VisitedByDoctor", 0);


                        command.ExecuteNonQuery();
                    }

                }


                    


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error44: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        

        }
    }
}
