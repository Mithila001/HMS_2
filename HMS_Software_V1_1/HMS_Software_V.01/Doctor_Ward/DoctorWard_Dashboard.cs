using HMS_Software_V1._01.Admission_Officer.UserControls;
using HMS_Software_V1._01.Common_UseForms;
using HMS_Software_V1._01.Doctor_Ward.UserControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HMS_Software_V1._01.Doctor_Ward
{
    public partial class DoctorWard_Dashboard : Form
    {
        SqlConnection connect = new SqlConnection(MyCommonConnecString.ConnectionString);

        private MyTableData_Automation automation; //Automation

        private int DoctorID;
        private int WardNumber;


        public DoctorWard_Dashboard(/*int userID, int WardNumber*/ int userID = 12, int WardNumber = 15)
        {
            InitializeComponent();

            this.DoctorID = userID;
            this.WardNumber = WardNumber;


            //Automation to update Admitted_Patients_VisitEvent daily
            automation = new MyTableData_Automation();
            automation.MyGetAdmittedPatientRecord();


            LoadData();
            LoadDashboardData();
        }

        int IsCompletedCount;
        int IsNotCompledCount;
        int IsCompletedByDoctorCount;

        private void LoadDashboardData()
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(MyCommonConnecString.ConnectionString))
                {
                    connect.Open();

                    string query1 = "SELECT COUNT(*) FROM Admitted_Patients_VisitEvent"+
                        " WHERE Is_VisitedByDoctor = @Is_VisitedByDoctor AND CONVERT(date, Visite_Date) = @TodayDate";
                    using (SqlCommand command = new SqlCommand(query1, connect))
                    {
                        command.Parameters.AddWithValue("@Is_VisitedByDoctor", 1);
                        command.Parameters.AddWithValue("@TodayDate", DateTime.Today);
                        IsCompletedCount = (int)command.ExecuteScalar();
                    }

                    string query2 = "SELECT COUNT(*) FROM Admitted_Patients_VisitEvent WHERE"+
                        " Is_VisitedByDoctor = @Is_VisitedByDoctor AND CONVERT(date, Visite_Date) = @TodayDate";
                    using (SqlCommand command = new SqlCommand(query2, connect))
                    {
                        command.Parameters.AddWithValue("@Is_VisitedByDoctor", 0);
                        command.Parameters.AddWithValue("@TodayDate", DateTime.Today);
                        IsNotCompledCount = (int)command.ExecuteScalar();
                    }

                    string query3 = "SELECT COUNT(*) FROM Admitted_Patients_VisitEvent WHERE"+
                        " Is_VisitedByDoctor = @Is_VisitedByDoctor AND Visited_Doctor_ID = @Visited_Doctor_ID AND CONVERT(date, Visite_Date) = @TodayDate";
                    using (SqlCommand command = new SqlCommand(query3, connect))
                    {
                        command.Parameters.AddWithValue("@Is_VisitedByDoctor", 1);
                        command.Parameters.AddWithValue("@Visited_Doctor_ID", DoctorID);
                        command.Parameters.AddWithValue("@TodayDate", DateTime.Today);
                        IsCompletedByDoctorCount = (int)command.ExecuteScalar();
                    }
                }

                DWD_Completed.Text = IsCompletedByDoctorCount.ToString()+" Completed";
                DWD_TotalPending.Text = IsNotCompledCount.ToString();
                DWD_TotalCompleted.Text = IsCompletedCount.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine(ex);
            }
        }





        // Doctor Table
        string DoctorName;
        string DoctorPosition;
        string DoctorSpecialty;
        string Doctor_RID;

        //Admitted_Patients_VisitEvent Table
        string PatientnRID;
        string PatientName;
        string PatientAge;
        string PatientCondition;
        bool IsVisitedByDoctor;
        int PatientVisitCount;
        string PatientGender;

        string WardName;

        private void LoadData()
        {
            DateTime today = DateTime.Today.Date;

            try
            {
                using (SqlConnection connect = new SqlConnection(MyCommonConnecString.ConnectionString))
                {
                   
                    connect.Open();

                    // Load Docotr Details ------------------------------------------------------------------------
                    string query = "SELECT D_NameWithInitials, D_Position, D_Specialty, D_RegistrationID" +
                    " FROM Doctor WHERE Doctor_ID = @doctorID";
                    using (SqlCommand command = new SqlCommand(query, connect))
                    {
                        command.Parameters.AddWithValue("@doctorID", DoctorID);
                        Console.WriteLine("DoctorID from dashboard: " + DoctorID);
                        try
                        {
                            SqlDataReader reader = command.ExecuteReader();

                            // Check if any rows were returned
                            if (reader.Read())
                            {
                                DoctorName = reader["D_NameWithInitials"].ToString();
                                DoctorPosition = reader["D_Position"].ToString();
                                DoctorSpecialty = reader["D_Specialty"].ToString();
                                Doctor_RID = reader["D_RegistrationID"].ToString();

                                DWD_name.Text = DoctorName;
                                DWD_Position.Text = DoctorPosition;
                                
                            }
                            else
                            {
                                MessageBox.Show("No matching record found.");
                            }
                            reader.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error:1 " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Console.WriteLine("Error1:" + ex);
                        }
                    }


                    //Load Ward Details  ------------------------------------------------------------------------
                   /* DWD_WarName.Text = "Temporary Ward"; //Need to change*/
                    string query3 = "SELECT WardName" +
                    " FROM WardTypes WHERE WardNumber = @WardNumber";
                    using (SqlCommand command3 = new SqlCommand(query3, connect))
                    {
                        command3.Parameters.AddWithValue("@WardNumber", WardNumber);
                        /*Console.WriteLine("DoctorID from dashboard: " + DoctorID);*/
                        try
                        {
                            SqlDataReader reader = command3.ExecuteReader();

                            // Check if any rows were returned
                            if (reader.Read())
                            {
                                DWD_WarName.Text = reader["WardName"].ToString();
                            }
                            else
                            {
                                DWD_WarName.Text = "Ward No Not Found";
                            }
                            reader.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error:3 " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Console.WriteLine("Error3:" + ex);
                        }
                    }


                    // Get Dcotor Details
                    string query2 = "SELECT D_NameWithInitials, D_Position, D_Specialty, D_RegistrationID" +
                    " FROM Doctor WHERE Doctor_ID = @doctorID";
                    using (SqlCommand command2 = new SqlCommand(query2, connect))
                    {
                        command2.Parameters.AddWithValue("@doctorID", DoctorID);
                        /*Console.WriteLine("DoctorID from dashboard: " + DoctorID);*/
                        try
                        {
                            SqlDataReader reader = command2.ExecuteReader();

                            // Check if any rows were returned
                            if (reader.Read())
                            {
                                DoctorName = reader["D_NameWithInitials"].ToString();
                                DoctorPosition = reader["D_Position"].ToString();
                                DoctorSpecialty = reader["D_Specialty"].ToString();
                                Doctor_RID = reader["D_RegistrationID"].ToString();

                                DWD_name.Text = DoctorName;
                                DWD_Position.Text = DoctorPosition;

                            }
                            else
                            {
                                MessageBox.Show("No matching record found.");
                            }
                            reader.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error:3 " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Console.WriteLine("Error3:" + ex);
                        }
                    }


                    //Load Today Admitted Patient Events  ------------------------------------------------------------------------
                    string query1 = "SELECT P_RID, P_NameWithInitials, P_Age, P_Gender, Visite_Round, Is_VisitedByDoctor, P_Condition, P_Ward FROM" +
                       " Admitted_Patients_VisitEvent"+
                       " WHERE Visite_Date = @visiteDate";

                    using (SqlCommand cmd = new SqlCommand(query1, connect))
                    {
                        cmd.Parameters.AddWithValue("@visiteDate", today);
                        Console.WriteLine("visiteDate: " + today);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine("Table Reads");
                                try
                                {
                                    PatientnRID = reader["P_RID"].ToString();
                                    PatientName = reader["P_NameWithInitials"].ToString();
                                    PatientAge = reader["P_Age"].ToString();
                                    PatientGender = reader["P_Gender"].ToString();
                                    /*Console.WriteLine("Patient Gender: " + PatientCondition);*/
                                    PatientCondition = reader["P_Condition"].ToString();
                                    IsVisitedByDoctor = (bool)reader["Is_VisitedByDoctor"];
                                    PatientVisitCount = (int)reader["Visite_Round"];
                                    WardName = reader["P_Ward"].ToString();


                                }
                                catch (FormatException)
                                {
                                    MessageBox.Show("Patient RID is not matched", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }

                                ShowWaitionInPatients showWaitionInPatients = new ShowWaitionInPatients();
                                // Adding Values to labels
                                showWaitionInPatients.SWP_Name.Text = PatientName;
                                showWaitionInPatients.SWP_RID.Text = PatientnRID;
                                showWaitionInPatients.SWP_Age.Text = PatientAge;
                                showWaitionInPatients.SWP_Condition.Text = PatientCondition;

                                //Storing Valuse to user control
                                showWaitionInPatients.SWP_PatientName = PatientName;
                                showWaitionInPatients.SWP_PatientRID = PatientnRID;
                                showWaitionInPatients.SWP_PatientCondition = PatientCondition;
                                showWaitionInPatients.SWP_PatietnVisitCount = PatientVisitCount;
                                showWaitionInPatients.SWP_PatientAge = PatientAge;
                                showWaitionInPatients.SWP_PatientGender = PatientGender;

                                showWaitionInPatients.SWP_D_RID = Doctor_RID;
                                showWaitionInPatients.SWP_D_Name = DoctorName;
                                showWaitionInPatients.SWP_D_Title = DoctorPosition;
                                showWaitionInPatients.SWP_D_ID = DoctorID;

                                showWaitionInPatients.SWP_WardName = WardName;



                                if (!IsVisitedByDoctor)
                                {
                                    DWD_DisplayInPatient_FlowLP.Controls.Add(showWaitionInPatients);

                                   
                                }

                                DWD_DisplayInPatient_FlowLP.SizeChanged += (sender, e) =>
                                {
                                    // Adjust the width of the user control to match the width of the parent container
                                    showWaitionInPatients.Width = DWD_DisplayInPatient_FlowLP.ClientSize.Width - showWaitionInPatients.Margin.Horizontal;
                                };

                            }
                            reader.Close();
                        }
                    }

                }

            }
            catch(Exception ex)
            {
                MessageBox.Show("Error:2 " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine("Error2:" + ex);
            }
         
        }

        private void DoctorWard_Dashboard_FormClosed(object sender, FormClosedEventArgs e)
        {
            UserLogin userLogin = new UserLogin();
            userLogin.Show();
            this.Hide();
        }
    }
}
