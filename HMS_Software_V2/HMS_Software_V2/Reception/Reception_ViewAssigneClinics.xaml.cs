using HMS_Software_V2.General_Purpose;
using HMS_Software_V2.Reception.R_UserControls;
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

namespace HMS_Software_V2.Reception
{
    /// <summary>
    /// Interaction logic for Reception_ViewAssigneClinics.xaml
    /// </summary>
    public partial class Reception_ViewAssigneClinics : Window
    {
        public Reception_ViewAssigneClinics()
        {
            InitializeComponent();
            loadBasicData();
            LoadClinicType();
        }

        private void loadBasicData()
        {
            receptionName.Content = "A V Temporory";
            string formattedDate = DateTime.Today.ToString("dd MMMM yyyy"); // "20 October 2024"
            string formattedTime = DateTime.Now.ToString("hh:mm tt"); // "02:30 PM"
            todayDate.Content = formattedDate;
            todayTime.Content = formattedTime;
        }

        // These variables are used to filter the clinics based on the patient's clinic requests
        bool IsClinicFiltering = false;
        List<int> Requested_ClinicIDs = new List<int>();
        int PatientID = 0;

        private void LoadClinicType()
        {
            using (SqlConnection connection = new Database_Connector().GetConnection())
            {
                string query1 = "SELECT * FROM ClinicType";

                SqlCommand cmd = new SqlCommand(query1, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {

                        UC_RV_ClinicTypes uC_RV_ClinicTypes = new UC_RV_ClinicTypes();
                        string clinicTypeName = reader["CT_Name"].ToString() ?? "Error";
                        string availability = "XXXXX"; // Replace with actual availability if available

                        uC_RV_ClinicTypes.ClinicTypeName.Content = clinicTypeName;
                        uC_RV_ClinicTypes.ClinicAvailabilityToday.Content = availability;

                        int clinicTypeID = Convert.ToInt32(reader["ClinicType_ID"]);
                        uC_RV_ClinicTypes.ClinicTypeID = clinicTypeID;
                        uC_RV_ClinicTypes.Availability = availability;

                        // Subscribe to the custom event
                        uC_RV_ClinicTypes.MyClinicTypeClicked += UC_RV_ClinicTypes_ClinicTypeClicked;

                        // Adjust the width of the user control to match the width of the parent container
                        ViewClinicTypes_WrapP.SizeChanged += (sender, e) =>
                        {
                            uC_RV_ClinicTypes.Width = ViewClinicTypes_WrapP.ActualWidth - uC_RV_ClinicTypes.Margin.Left - uC_RV_ClinicTypes.Margin.Right;
                        };
                        uC_RV_ClinicTypes.Width = ViewClinicTypes_WrapP.ActualWidth - uC_RV_ClinicTypes.Margin.Left - uC_RV_ClinicTypes.Margin.Right;


                        // Check if Clinic Filtering is enabled
                        if (IsClinicFiltering)
                        {
                            if (Requested_ClinicIDs.Contains(clinicTypeID))
                            {
                                ViewClinicTypes_WrapP.Children.Add(uC_RV_ClinicTypes);
                            }
                            else
                            {
                                continue;
                            }
                        }
                        else
                        {
                            // Normal Clinic Type Display
                            ViewClinicTypes_WrapP.Children.Add(uC_RV_ClinicTypes);
                        }
                        
                    }
                    reader.Close();


                }

                catch (Exception ex)
                {
                    Debug.WriteLine("\nError1: \n" + ex.Message);
                    MessageBox.Show("Error1: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
        }//Load Clinc Types

        private void UC_RV_ClinicTypes_ClinicTypeClicked(object? sender, MyClinicTypeEventArgs e)
        {
            ClinicEvents_WrapP.Children.Clear();

            // Call the method in the main form with the clinic type and availability details
            HandleClinicTypeClicked(e.ClinicTypeID, e.Availability);
        }


        bool IsAddeddPatientRID = false; // Mainly use to control Assigne button visibility
       
        private void HandleClinicTypeClicked(int clinicTypeID, string availability)
        {
            // Your method implementation here
            //MessageBox.Show($"Clinic Type: {clinicTypeID}\nAvailability: {availability}", "Clinic Details", MessageBoxButton.OK, MessageBoxImage.Information);

            using (SqlConnection connection = new Database_Connector().GetConnection())
            {
                string query1 = "SELECT ce.*, d.D_NameWithInitials, d.D_Specialty, ce.CE_HallNumber, ce.CE_StartTime,"+
                    " ce.CE_EndTime, ce.CE_Date, ce.CE_TotalSlots, ce.CE_TakenSlots, ce.ClinicEvent_ID ,ct.CT_WardNo " +
                    " FROM ClinicEvents ce" +
                    " INNER JOIN ClinicType ct ON ce.CE_ClinicType_ID = ct.ClinicType_ID" +
                    " INNER JOIN Doctor d ON ce.Doctor_ID = d.Doctor_ID" +
                    " WHERE ce.CE_ClinicType_ID = @clinicTypeID;";

                SqlCommand cmd = new SqlCommand(query1, connection);
                cmd.Parameters.AddWithValue("@clinicTypeID", Convert.ToInt32(clinicTypeID));

                try
                {
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Debug.WriteLine("\nD_NameWithInitials: \n" + reader["D_NameWithInitials"].ToString());
                        Debug.WriteLine("\nD_Specialty: \n" + reader["D_Specialty"].ToString());
                        Debug.WriteLine("\nCE_HallNumber: \n" + reader["CE_HallNumber"].ToString());
                        Debug.WriteLine("\nCE_StartTime: \n" + (TimeSpan)reader["CE_StartTime"]);
                        Debug.WriteLine("\nCE_TotalSlots: \n" + Convert.ToInt32(reader["CE_TotalSlots"]));
                        Debug.WriteLine("\nCE_TakenSlots: \n" + Convert.ToInt32(reader["CE_TakenSlots"]));
                        Debug.WriteLine("\nClinicEvent_ID: \n" + Convert.ToInt32(reader["ClinicEvent_ID"]));


                        UC_RV_ClinicEvents uC_RV_ClinicEvents = new UC_RV_ClinicEvents();

                        if(IsAddeddPatientRID) // Button Activation
                        {
                            uC_RV_ClinicEvents.AssignClinic_btn.ClearValue(Button.BackgroundProperty);
                            uC_RV_ClinicEvents.AssignClinic_btn.IsEnabled = true;

                        }
                        

                        uC_RV_ClinicEvents.AssignClinicClicked += MyUserControlAssignedButtonClicked_method; // Subscribe to the event
                        uC_RV_ClinicEvents.ClinicEventID = Convert.ToInt32(reader["ClinicEvent_ID"]);
                        uC_RV_ClinicEvents.ClinicTypeID = clinicTypeID;

                        uC_RV_ClinicEvents.doctorName.Content = reader["D_NameWithInitials"].ToString();
                        uC_RV_ClinicEvents.d_Specialty.Content = reader["D_Specialty"].ToString();
                        uC_RV_ClinicEvents.clinicLocation.Content = reader["CE_HallNumber"].ToString();
                        uC_RV_ClinicEvents.clinicWardNo.Content = reader["CT_WardNo"].ToString();

                        TimeSpan ceStartTime = (TimeSpan)reader["CE_StartTime"];
                        TimeSpan ceEndTime = (TimeSpan)reader["CE_EndTime"];
                        string formattedStartTime = DateTime.Today.Add(ceStartTime).ToString("hh:mm tt"); // "01:00 PM"
                        string formattedEndTime = DateTime.Today.Add(ceEndTime).ToString("hh:mm tt"); // "01:00 PM"

                        uC_RV_ClinicEvents.clinicEventTime.Content = $"{formattedStartTime} to {formattedEndTime}";

                        DateTime ceDate = (DateTime)reader["CE_Date"];
                        string formattedDate = ceDate.ToString("dd'\\t\\h' MMMM yyyy"); // "31st May 2024"

                        Debug.WriteLine("\nformattedDate \n" + formattedDate);

                        uC_RV_ClinicEvents.clinicEventDate.Content = formattedDate;

                        uC_RV_ClinicEvents.ClinicEventTotalSlots.Content = reader["CE_TotalSlots"].ToString();

                        int totalSlots = Convert.ToInt32(reader["CE_TotalSlots"]);
                        int takenSlots = Convert.ToInt32(reader["CE_TakenSlots"]);

                        uC_RV_ClinicEvents.ClinicAvailableSlots.Content = (totalSlots - takenSlots).ToString();



                        // Adjust the width of the user control to match the width of the parent container
                        ClinicEvents_WrapP.SizeChanged += (sender, e) =>
                        {
                            uC_RV_ClinicEvents.Width = ClinicEvents_WrapP.ActualWidth - uC_RV_ClinicEvents.Margin.Left - uC_RV_ClinicEvents.Margin.Right;
                            //Debug.WriteLine("\n Width:" + uC_RV_ClinicTypes.Width);
                            //Debug.WriteLine("\n clinicAvailability_WrapP.ActualWidth:" + ViewClinicTypes_WrapP.ActualWidth);
                        };
                        uC_RV_ClinicEvents.Width = ClinicEvents_WrapP.ActualWidth - uC_RV_ClinicEvents.Margin.Left - uC_RV_ClinicEvents.Margin.Right;

                        ClinicEvents_WrapP.Children.Add(uC_RV_ClinicEvents);
                    }
                    reader.Close();


                }

                catch (Exception ex)
                {
                    Debug.WriteLine("\nError1: \n" + ex.Message);
                    MessageBox.Show("Error1: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }

        } // Clinic Events Load


        #region Happend when Reception Assigned a Clinic Event To the Patient
        private void MyUserControlAssignedButtonClicked_method(object sender, EventArgs e, int clinicEvntID, int clinicType)
        {
            // Call the desired method
            NyAssigneClinicToPatient(clinicEvntID, clinicType);
        }

        private void NyAssigneClinicToPatient(int clinicEventID, int clinicType)
        {
            int patientAppointmentRequestID = 0;

            using (SqlConnection connection = new Database_Connector().GetConnection())
            {
                connection.Open();
                

                try
                {
                    // Check if there any Clinic request givent to this patient in Patient_AppointmentRequest table 
                    string query3 = "SELECT * FROM Patient_AppointmentRequest WHERE PatientID = @PatientID AND IsVisitedByDoctor = 0 AND IsBooked = 0 AND ClinicType_ID = @ClinicType_ID";
                    using (SqlCommand cmd3 = new SqlCommand(query3, connection))
                    {
                        cmd3.Parameters.AddWithValue("@PatientID", PatientID);
                        cmd3.Parameters.AddWithValue("@ClinicType_ID", clinicType);

                        SqlDataReader reader = cmd3.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                patientAppointmentRequestID = reader.GetInt32(reader.GetOrdinal("PatientAppointmentRequest_ID"));
                            }
                        }
                        else
                        {
                            Debug.WriteLine("Appointment is Already Booked By the Patient.");
                            MessageBox.Show("Appointment is Already Booked", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }

                        reader.Close();
                    }




                    // update the ClinicEvents Table slots count
                    string query1 = "UPDATE ClinicEvents SET CE_TakenSlots = CE_TakenSlots + 1 WHERE ClinicEvent_ID = @clinicEventId";
                    using (SqlCommand cmd = new SqlCommand(query1, connection))
                    {
                        cmd.Parameters.AddWithValue("@clinicEventId", clinicEventID);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected == 0)
                        {
                            Debug.WriteLine("Error! Not Updated ----> Clinc Taken Slots.");
                            MessageBox.Show("Error! Not Updated ----> Clinc Taken Slots.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                    }


                    // Update the Patient_AppointmentRequest Table
                    string query2 = "UPDATE Patient_AppointmentRequest SET IsBooked = 1, ClinicEvent_ID = @ClinicEvent_ID"+
                        "  WHERE PatientAppointmentRequest_ID = @PatientAppointmentRequest_ID";
                    using (SqlCommand cmd2 = new SqlCommand(query2, connection))
                    {
                        cmd2.Parameters.AddWithValue("@PatientAppointmentRequest_ID", patientAppointmentRequestID);
                        cmd2.Parameters.AddWithValue("@ClinicEvent_ID", clinicEventID);

                        int rowsAffected = cmd2.ExecuteNonQuery();

                        if (rowsAffected == 0)
                        {
                            Debug.WriteLine("Error! Not Updated ----> Appointment is Booked By the Patient.");
                            MessageBox.Show("Error! Not Updated ----> Appointment is Booked By the Patient.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }

                    string tempAvailabity = "XXXXX"; // tempory availability

                    ClinicEvents_WrapP.Children.Clear();
                    HandleClinicTypeClicked(clinicType, tempAvailabity); // Update ClinicEvnet User Control again






                }
                catch (Exception ex)
                {
                    Debug.WriteLine("\nError1: \n" + ex.Message);
                    MessageBox.Show("Error1: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
        } 
        #endregion




        private void Reception_ViewAssigneClinics1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Reception_Dashboard reception_Dashboard = new Reception_Dashboard();    
            reception_Dashboard.Show();
        }

        private void PatientSearch_btn_Click(object sender, RoutedEventArgs e)
        {
            ClinicEvents_WrapP.Children.Clear();

            bool isPatientRID_Found = false;

            using (SqlConnection connection = new Database_Connector().GetConnection())
            {
                string query1 = "SELECT P_NameWithIinitials, P_Age, P_Gender, Patient_ID FROM Patient WHERE P_RegistrationID = @P_RegistrationID";
                SqlCommand cmd = new SqlCommand(query1, connection);
                cmd.Parameters.AddWithValue("@P_RegistrationID", "P"+PatientRID_tbx.Text); 

                connection.Open();

                try
                {


                    #region Getting Patient ID 
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            //Display Patient Details
                            patientName_lbl.Content = reader["P_NameWithIinitials"].ToString() ?? "error";
                            patientAge_lbl.Content = reader["P_Age"].ToString() ?? "error";
                            patientGender_lbl.Content = reader["P_Gender"].ToString() ?? "error";


                            PatientID = Convert.ToInt32(reader["Patient_ID"]);

                            isPatientRID_Found = true;

                        }
                    }
                    else
                    {
                        MessageBox.Show("ID not match", "Information", MessageBoxButton.OK, MessageBoxImage.Warning);
                        PatientRID_tbx.Text = "";
                        return;

                    }
                    reader.Close();
                    #endregion


                    #region If Patient RID found, then check for Clinc Requests
                    if (isPatientRID_Found)
                    {
                        string query2 = "SELECT ClinicType_ID FROM Patient_AppointmentRequest WHERE PatientID = @PatientID AND IsBooked = 0";
                        SqlCommand cmd2 = new SqlCommand(query2, connection);
                        cmd2.Parameters.AddWithValue("@PatientID", PatientID);


                        SqlDataReader reader2 = cmd2.ExecuteReader();

                        if (reader2.HasRows)
                        {
                            IsClinicFiltering = true;

                            while (reader2.Read())
                            {
                                
                                Requested_ClinicIDs.Add(Convert.ToInt32(reader2["ClinicType_ID"])); //add to list

                            }

                            ViewClinicTypes_WrapP.Children.Clear();
                            LoadClinicType();

                            IsAddeddPatientRID = true;
                        }
                        else
                        {
                            MessageBox.Show("No Clinic Assigned", "Information", MessageBoxButton.OK, MessageBoxImage.Warning);
                            PatientRID_tbx.Text = "";
                            return;

                        }
                        reader2.Close();

                    } 
                    #endregion




                }
                catch (Exception ex)
                {
                    Debug.WriteLine("\nError1: \n" + ex.Message);
                    MessageBox.Show("Error1: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

        }//Search Patient

        private void PatientRID_Reset_btn_Click(object sender, RoutedEventArgs e)//Reset Patient RID
        {
            IsAddeddPatientRID = false;

            IsClinicFiltering = false;
            Requested_ClinicIDs.Clear();
            PatientID = 0;

            ViewClinicTypes_WrapP.Children.Clear();
            ClinicEvents_WrapP.Children.Clear();
            LoadClinicType();
        }
    }
}
