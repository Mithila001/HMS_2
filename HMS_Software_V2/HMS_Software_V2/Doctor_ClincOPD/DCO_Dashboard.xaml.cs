using HMS_Software_V2._DataManage_Classes;
using HMS_Software_V2.General_Purpose;
using HMS_Software_V2.UserLogin_Page;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace HMS_Software_V2.Doctor_ClincOPD
{
    /// <summary>
    /// Interaction logic for DCO_Dashboard.xaml
    /// </summary>
    public partial class DCO_Dashboard : Window
    {
        

        public DCO_Dashboard()
        {
            InitializeComponent();

            doctorName_lbl.Content = SharedData.doctorData.doctorName;
            SharedData.doctorData.doctorID = 1;
            SharedData.doctorData.doctorSpecialization = "General Physician";

            if(SharedData.doctorData.doctorLocation == "Clinic")
            {
                MyDoctorClinic();   

            }
            else if(SharedData.doctorData.doctorLocation == "OPD")
            {
                //MyDoctorOPD();
            }
            else
            {
                MessageBox.Show("Error: Doctor Location is not Valid", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
            }

        }

        //bool IsClinic = false;
        //bool IsOPD = false;
        //private void MyDoctorOPD()
        //{
        //    IsOPD = true;
        //}

        private void MyDoctorClinic()
        {
            dipartmentName_lbl.Content = "Outpatient Dipartment (Clinic)";
            //IsClinic = true;
        }



        bool IsConfirmButtonIsClicked = false; //Flag related to Form exit

        // Button Section ------------------------------------------------------------------------------------
        private void confirm_btn_Click(object sender, RoutedEventArgs e)
        {

            string patientRID = patientRID_tbx.Text.ToString();


            #region Validations
            if (General_Purpose.InputValidations.MyIsNullorempty(patientRID))
            {
                MessageBox.Show("Enter Patient RID", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!General_Purpose.InputValidations.MyIsOnlyNumbers(patientRID))
            {
                MessageBox.Show("Enter only numbers", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            } 
            #endregion



            bool IsRID_Valid = false;

            using (SqlConnection connection = new Database_Connector().GetConnection())  //to check if the patient RID is correct or not
            {
                string query1 = "SELECT * FROM Patient WHERE P_RegistrationID = @patientRID ";

                SqlCommand cmd = new SqlCommand(query1, connection);

                cmd.Parameters.AddWithValue("@patientRID", "P"+patientRID_tbx.Text);


                try
                {
                    connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            IsRID_Valid = true;
                        }
                    }

                    if (!IsRID_Valid)
                    {
                        // No data was read from the database.
                        Debug.WriteLine("No data found for the given patient registration RID.");
                        MessageBox.Show("Incorrect RID", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;

                    }
                    else
                    {

                        //if (IsClinic)
                        //{
                        //    string query2 = "SELECT * FROM Patient_AppointmentRequest WHERE P_RegistrationID = @patientRID ";

                        //    SqlCommand cmd2 = new SqlCommand(query2, connection);

                        //    cmd2.Parameters.AddWithValue("@patientRID", "P" + patientRID_tbx.Text);

                        //    using (SqlDataReader reader2 = cmd2.ExecuteReader())
                        //    {
                        //        while (reader2.Read())
                        //        {
                        //            IsRID_Valid = true;
                        //        }
                        //        reader2.Close();
                        //    }


                        //}


                        HMS_Software_V2._DataManage_Classes.SharedData.medicalEvent = new HMS_Software_V2._DataManage_Classes.MedicalEvnent(); // Get a new copy of the medical event template
                        
                        SharedData.medicalEvent.pationetRID = "P" + patientRID_tbx.Text;



                        IsConfirmButtonIsClicked = true; // So When Form is closed, it will not open the UserLogin Page

                        DCO_PatientCheck dCO_PatientCheck = new DCO_PatientCheck();
                        dCO_PatientCheck.Show();
                        this.Close();
                    }


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

        private void reset_btn_Click(object sender, RoutedEventArgs e)
        {
            patientRID_tbx.Text = "";
        }

        private void DCO_Dashboard1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!IsConfirmButtonIsClicked)
            {
                UserLogin userLogin = new UserLogin();
                userLogin.Show();
            }
            
            
        }

    }
}
