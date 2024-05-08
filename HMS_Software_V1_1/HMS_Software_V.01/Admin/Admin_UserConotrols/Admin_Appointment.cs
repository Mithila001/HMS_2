using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HMS_Software_V1._01.Admin.Admin_UserConotrols
{
    public partial class Admin_Appointment : UserControl
    {
        public Admin_Appointment()
        {
            InitializeComponent();


           
        }

        string AdminName;
        public void SetAdminName(string adminName) // Recevied from the Admin Dahsboard
        {
            AdminName = adminName;
        }



        //Reciving Data from the form
        public void MySendDataToUserControl(string adminName, string date, string time)
        {
            Ad_appointmetn_Date.Text = date;
            Ad_appointmetn_time.Text = time;
        }


        private bool Found_Clinic_Events = false;

        DateTime startTime;
        DateTime endTime;
        DateTime eventDate;
        int DoctorID;
        int HallNumber;

        int ClinicID;
        private void AP_Save_btn_Click(object sender, EventArgs e)
        {
            if (AP_duration.Text == ""
                || AP_DoctorID.Text == ""
                || AP_WardNo.Text == ""
                || AP_TotalSlots.Text == ""
                || AP_HallNumber.Text == ""
                || AP_Clinci_ID.Text == ""
                || AP_dat_dtPicker.Format == DateTimePickerFormat.Short && !AP_dat_dtPicker.Checked && AP_dat_dtPicker.Value != DateTime.MinValue
                || AP_Time_dtPicker.Format == DateTimePickerFormat.Time && !AP_Time_dtPicker.Checked && AP_Time_dtPicker.Value != DateTime.MinValue

                /*|| D_Register_DTimePicker.Value != D_Register_DTimePicker.MinDate*/)
            {
                MessageBox.Show("Please fill all  fields"
                 , "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    using (SqlConnection connect = new SqlConnection(MyCommonConnecString.ConnectionString))
                    {
                        connect.Open();


                        //Get Clinic Type Details string query = "SELECT Doctor_ID, CE_HallNumber, CE_StartTime, CE_EndTime, CE_Date FROM ClinicEvents WHERE CE_Date >= GETDATE()";

                        string query4 = "SELECT ClincType_ID FROM ClincType WHERE ClincType_ID = @ClincID";
                        using (SqlCommand command = new SqlCommand(query4, connect))
                        {
                            command.Parameters.AddWithValue("@ClincID", Convert.ToInt32(AP_Clinci_ID.Text));

                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                        ClinicID = reader.GetInt32(0);
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Clinic ID not Found", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;

                                }
                            }
                        }



                        // Get Curretn Clinic Event Details ------------------------------------------------------------------ 

                        string query = "SELECT Doctor_ID, CE_HallNumber, CE_StartTime, CE_EndTime, CE_Date FROM ClinicEvents WHERE CE_Date >= GETDATE()";
                        using (SqlCommand command = new SqlCommand(query, connect))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                        DoctorID = reader.GetInt32(0);
                                        HallNumber = reader.GetInt32(1);
                                        startTime = reader.GetDateTime(2);
                                        endTime = reader.GetDateTime(3);
                                        eventDate = reader.GetDateTime(4);

                                        Console.WriteLine($"Doctor ID: {DoctorID}, Hall Number: {HallNumber}, Start Time: {startTime}, End Time: {endTime}, Event Date: {eventDate}");
                                        Found_Clinic_Events = true;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("No data found.");

                                    string query3 = "INSERT INTO ClinicEvents (Clinic_ID, Doctor_ID, CE_HallNumber, CE_StartTime, CE_EndTime, CE_Date, CE_TotalSlots) " +
                                        "VALUES (@Clinic_ID, @Doctor_ID, @CE_HallNumber, @CE_StartTime, @CE_EndTime, @CE_Date, @CE_TotalSlots)";

                                   
                                    using (SqlCommand command3 = new SqlCommand(query3, connect))
                                    {
               
                                        /*command3.Parameters.AddWithValue("@Clinic_ID", value1);
                                        command3.Parameters.AddWithValue("@Doctor_ID", value2);
                                        command3.Parameters.AddWithValue("@CE_HallNumber", value3);
                                        command3.Parameters.AddWithValue("@CE_StartTime", value3);
                                        command3.Parameters.AddWithValue("@CE_EndTime", value3);
                                        command3.Parameters.AddWithValue("@CE_Date", value3);
                                        command3.Parameters.AddWithValue("@CE_TotalSlots", value3);*/


                                        int rowsAffected = command.ExecuteNonQuery();
                                        if (rowsAffected > 0)
                                        {
                                   
                                            Console.WriteLine("Data inserted successfully!");
                                        }
                                        else
                                        {
              
                                            Console.WriteLine("Failed to insert data!");
                                        }
                                    }
                                }
                            }
                        }

                        if (!Found_Clinic_Events)
                        {
                            




                        }



                    }

                }
                catch(Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message
                               , "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    Console.WriteLine(ex);

                }
            }
        }

        private void MyIsClinicEventAvailable()
        {
           /* DateTime startTime;
            DateTime endTime;
            DateTime eventDate;
            int DoctorID;
            int HallNumber;*/


            int AA_DoctorID = Convert.ToInt32(AP_DoctorID.Text);
            int AA_WardNo = Convert.ToInt32(AP_WardNo.Text);
            int AA_TotalSlots = Convert.ToInt32(AP_TotalSlots.Text);
            int AA_HallNumber = Convert.ToInt32(AP_HallNumber.Text);
            int AA_Duration = Convert.ToInt32(AP_HallNumber.Text);

            DateTime AA_startTime = AP_Time_dtPicker.Value;
            DateTime AA_eventDate = AP_dat_dtPicker.Value;

            DateTime AA_endTim = AA_startTime.AddMinutes(AA_Duration);


            if(eventDate <= AA_eventDate)
            {
                if(AA_endTim <= startTime)
                {


                }
                else
                {
                    TimeSpan timeDifference = startTime - AA_endTim;
                    MessageBox.Show("Time Overlap by:"+ timeDifference.ToString(@"hh\:mm\:ss"), "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }

        }
    }
}
