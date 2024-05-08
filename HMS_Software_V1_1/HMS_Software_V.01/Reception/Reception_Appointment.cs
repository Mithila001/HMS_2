using HMS_Software_V1._01.Reception.UserControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HMS_Software_V1._01.Reception
{
    public partial class Reception_Appointment : Form
    {
        SqlConnection connect = new SqlConnection(MyCommonConnecString.ConnectionString);

        int UserID;
        public Reception_Appointment(int userID)
        {
            InitializeComponent();
            LoadUserData();
            this.UserID = userID;
            /*this.FormClosed += (s, e) => new Reception_Dashboard().Show();*/


            this.SizeChanged += MyRe_Appointments_SizeChanged; //To fix clinicTypeAvailableDates Usercontrol scaling issues from small window to Full screen


        }

        private void MyRe_Appointments_SizeChanged(object sender, EventArgs e)
        {
            foreach (Control control in flowLayoutPanel_R_A_left.Controls)
            {
                if (control is R_A_SelectClinicType r_A_SelectClinicType)
                {
                    r_A_SelectClinicType.Width = flowLayoutPanel_R_A_left.ClientSize.Width - r_A_SelectClinicType.Margin.Horizontal;
                }
            }
        }

        int RASCT_clincID;
        private void UserControl_UserControlClicked(object sender, EventArgs e) //##################
        {
            flowLayoutPflowLayoutPanel_R_A_right.Controls.Clear();

            if (sender is R_A_SelectClinicType selectedClinic)
            {
                RASCT_clincID = selectedClinic.RASCT_ClincID;
                LoadUserData2(RASCT_clincID);
            }
        }


        int ClinicID;
        private void LoadUserData()
        {
            try
            {
                connect.Open();
                DateTime today = DateTime.Today;
                //================================= Show Clinic Types UserControls =================================
                string query = "SELECT CT_Name, CT_WardNo, ClincType_ID "
                    + "FROM ClincType";

                SqlCommand command = new SqlCommand(query, connect);
                SqlDataReader reader1 = command.ExecuteReader();

                // Loop through the records retrieved from the database
                while (reader1.Read())
                {

                    R_A_SelectClinicType r_A_SelectClinicType = new R_A_SelectClinicType();

                    r_A_SelectClinicType.UserControlClicked += UserControl_UserControlClicked;  //##################

                    r_A_SelectClinicType.RASCT_ClincType_lbl.Text = reader1["CT_Name"].ToString();
                    r_A_SelectClinicType.RASCT_ClincID = Convert.ToInt32(reader1["ClincType_ID"]);

                    ClinicID = Convert.ToInt32(reader1["ClincType_ID"]);

                    Console.WriteLine("ClinicID from Form: " + ClinicID);
                    
                    




                    flowLayoutPanel_R_A_left.SizeChanged += (sender, e) =>
                    {
                        // Adjust the width of the user control to match the width of the parent container
                        r_A_SelectClinicType.Width = flowLayoutPanel_R_A_left.ClientSize.Width - r_A_SelectClinicType.Margin.Horizontal;
                    };

                    flowLayoutPanel_R_A_left.Controls.Add(r_A_SelectClinicType);

                }
                reader1.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error11: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine("11 " + ex);
            }
            finally
            {
                connect.Close();
            }
        }


       
        int DoctorID;
        string HallNumber;
        TimeSpan StartTime;
        TimeSpan Endtime;
        DateTime Date;
        int TotalSlots;
        int TakenSlots;
        int ClinicEventID;

        bool IsClinicEventsFound =false;

        string DoctorName;
        string CliniName;
        

        private void LoadUserData2(int RASCT_clincID)
        {
            Console.WriteLine("LoadUserData2, ClincID = "+ RASCT_clincID);
            try
            {
                using(SqlConnection connect = new SqlConnection(MyCommonConnecString.ConnectionString))
                {
                    connect.Open();
                    DateTime today1 = DateTime.Today;


                    //Clinic Event Details
                    string query1 = "SELECT ClinicEvent_ID, Clinic_ID, Doctor_ID, CE_HallNumber, CE_StartTime, CE_EndTime, CE_Date, CE_TotalSlots, CE_TakenSlots" +
                        " FROM ClinicEvents WHERE CE_Date = @today AND Clinic_ID = @Clinic_ID";

                    using (SqlCommand command2 = new SqlCommand(query1, connect))
                    {
                        command2.Parameters.AddWithValue("@today", today1);
                        
                        command2.Parameters.AddWithValue("@Clinic_ID", RASCT_clincID);

                        using (SqlDataReader reader = command2.ExecuteReader())
                        {
                            if (reader.Read())
                            {

                                DoctorID = Convert.ToInt32(reader["Doctor_ID"]);
                                HallNumber = reader["CE_HallNumber"].ToString();
                                StartTime = TimeSpan.Parse(reader["CE_StartTime"].ToString());
                                Endtime = TimeSpan.Parse(reader["CE_EndTime"].ToString());
                                Date = Convert.ToDateTime(reader["CE_Date"]);
                                TotalSlots = Convert.ToInt32(reader["CE_TotalSlots"]);
                                TakenSlots = Convert.ToInt32(reader["CE_TakenSlots"]);
                                ClinicEventID  = Convert.ToInt32(reader["ClinicEvent_ID"]);


                                //  Get Doctor Detials --------------------------------------------------------------------
                                using (SqlConnection connect2 = new SqlConnection(MyCommonConnecString.ConnectionString))
                                {
                                    connect2.Open();
                                    string query2 = "SELECT D_NameWithInitials FROM Doctor WHERE Doctor_ID = @Doctor_ID";
                                    using (SqlCommand command3 = new SqlCommand(query2, connect2))
                                    {
                                        command3.Parameters.AddWithValue("@Doctor_ID", DoctorID);

                                        using (SqlDataReader reader3 = command3.ExecuteReader())
                                        {
                                            if (reader3.Read())
                                            {
                                                DoctorName = reader3["D_NameWithInitials"].ToString();

                                            }
                                            else
                                            {
                                                Console.WriteLine("Doctor Name Not Found");
                                            }

                                        }
                                    }

                                }


                                //Get Clinic Details ----------------------------------------------------------------------
                                using (SqlConnection connect3 = new SqlConnection(MyCommonConnecString.ConnectionString))
                                {
                                    connect3.Open();
                                    string query3 = "SELECT CT_Name FROM ClincType WHERE ClincType_ID = @ClincType_ID";
                                    using (SqlCommand command4 = new SqlCommand(query3, connect3))
                                    {
                                        Console.WriteLine("Data Reads3");
                                        command4.Parameters.AddWithValue("@ClincType_ID", RASCT_clincID);

                                        using (SqlDataReader reader4 = command4.ExecuteReader())
                                        {
                                            if (reader4.Read())
                                            {
                                                CliniName = reader4["CT_Name"].ToString();

                                            }
                                            else
                                            {
                                                // No Clinc Name Found Details found
                                            }

                                        }
                                    }

                                }



                                Recep_D_ClinicEvents recep_D_ClinicEvents = new Recep_D_ClinicEvents();

                                int ClinicEventClinicTypeID = RASCT_clincID;
                                Console.WriteLine(" ((---4---)) Retrived the ID From Table:", ClinicEventClinicTypeID);


                                Console.WriteLine(" ((---5---)) Executed");
                                recep_D_ClinicEvents.RPA_doctorName_lbl.Text = DoctorName;
                                recep_D_ClinicEvents.RPA_clincType_lbl.Text = CliniName;
                                recep_D_ClinicEvents.RPA_hallNumber_lbl.Text = "Hall " + HallNumber;
                                recep_D_ClinicEvents.RPA_time_lbl.Text = StartTime.ToString() + " -  " + Endtime.ToString();
                                recep_D_ClinicEvents.RPA_date_lbl.Text = Date.ToString();
                                /*recep_D_ClinicEvents.RPA_wardNo_lbl.Text = reader1["CT_WardNo"].ToString();*/
                                recep_D_ClinicEvents.RPA_totalSlots_lbl.Text = TotalSlots.ToString();
                                recep_D_ClinicEvents.RPA_availableSlots_lbl.Text = (TotalSlots - TakenSlots).ToString();


                                recep_D_ClinicEvents.ClinicEvnetID = ClinicEventID;
                                recep_D_ClinicEvents.ClinicID = RASCT_clincID;
                                recep_D_ClinicEvents.UserID = UserID;

                                flowLayoutPflowLayoutPanel_R_A_right.Controls.Add(recep_D_ClinicEvents);


                               /* #region Send data throug Button
                                Button assignButton = recep_D_ClinicEvents.RPA_assign_btn;
                                assignButton.Tag = ClinicEventClinicTypeID;

                                // Attach an event handler to the button click event
                                assignButton.Click += (sender, e) =>
                                {
                                    // Retrieve the ClinicEventClinicTypeID from the button tag
                                    int clickedClinicEventClinicID = (int)((Button)sender).Tag;

                                    Reception_AppontmentRegister reception_AppontmentRegister = new Reception_AppontmentRegister();
                                    reception_AppontmentRegister.ClinicEventClinicID = clickedClinicEventClinicID;
                                    reception_AppontmentRegister.Show();

                                    // Do something with the clickedClinicEventClinicID
                                    Console.WriteLine("Clicked Clinic Event Clinic ID: " + clickedClinicEventClinicID);
                                }; 
                                #endregion*/






                                // Adjust the width of the user control when added to the panel
                                recep_D_ClinicEvents.Width = flowLayoutPflowLayoutPanel_R_A_right.ClientSize.Width - recep_D_ClinicEvents.Margin.Horizontal;

                                flowLayoutPflowLayoutPanel_R_A_right.SizeChanged += (sender, e) =>
                                {
                                    // Adjust the width of the user control to match the width of the parent container
                                    recep_D_ClinicEvents.Width = flowLayoutPflowLayoutPanel_R_A_right.ClientSize.Width - recep_D_ClinicEvents.Margin.Horizontal;
                                };


                            }
                            else
                            {
                                // No Clinic Event found
                            }
                            reader.Close();

                        }
                    }

                }
                /*connect.Open();*//*
                DateTime today = DateTime.Today;
                //================================= Show Clinic Types UserControls =================================
                string query = "SELECT ce.[CE_Date], ce.CE_StartTime, ce.CE_EndTime, d.D_NameWithInitials, ct.CT_Name, ce.CE_HallNumber, ct.CT_WardNo, ce.CE_TotalSlots, ce.CE_TakenSlots, ce.ClinicEvent_ID AS ClincEventClinicID"
                    + " FROM ClinicEvents ce"
                    + " INNER JOIN Doctor d ON ce.Doctor_ID = d.Doctor_ID"
                    + " INNER JOIN ClincType ct ON ce.Clinic_ID = ct.ClincType_ID;";

                SqlCommand command = new SqlCommand(query, connect);

                SqlDataReader reader1 = command.ExecuteReader();*/



                // Loop through the records retrieved from the database
               /* while (reader1.Read())
                {
                    int ClinicTypeID = clinicID;
                    Console.WriteLine(" ((---4---)) Retrived the clinic ID: ", ClinicTypeID);

                    Recep_D_ClinicEvents recep_D_ClinicEvents = new Recep_D_ClinicEvents();

                    int ClinicEventClinicTypeID = Convert.ToInt32(reader1["ClincEventClinicID"]);
                    Console.WriteLine(" ((---4---)) Retrived the ID From Table:", ClinicEventClinicTypeID);

                    if (ClinicEventClinicTypeID == ClinicTypeID)
                    {



                        Console.WriteLine(" ((---5---)) Executed");
                        recep_D_ClinicEvents.RPA_doctorName_lbl.Text = reader1["D_NameWithInitials"].ToString();
                        recep_D_ClinicEvents.RPA_clincType_lbl.Text = reader1["CT_Name"].ToString();
                        recep_D_ClinicEvents.RPA_hallNumber_lbl.Text = "Hall " + reader1["CE_HallNumber"].ToString();
                        recep_D_ClinicEvents.RPA_time_lbl.Text = reader1["ClinicName"].ToString();
                        recep_D_ClinicEvents.RPA_date_lbl.Text = reader1["CE_Date"].ToString();
                        recep_D_ClinicEvents.RPA_wardNo_lbl.Text = reader1["CT_WardNo"].ToString();
                        recep_D_ClinicEvents.RPA_totalSlots_lbl.Text = reader1["CE_TotalSlots"].ToString();
                        recep_D_ClinicEvents.RPA_availableSlots_lbl.Text = reader1["CT_WardNo"].ToString();

                        flowLayoutPflowLayoutPanel_R_A_right.Controls.Add(recep_D_ClinicEvents);


                        Button assignButton = recep_D_ClinicEvents.RPA_assign_btn;
                        assignButton.Tag = ClinicEventClinicTypeID;

                        // Attach an event handler to the button click event
                        assignButton.Click += (sender, e) =>
                        {
                            // Retrieve the ClinicEventClinicTypeID from the button tag
                            int clickedClinicEventClinicID = (int)((Button)sender).Tag;

                            Reception_AppontmentRegister reception_AppontmentRegister = new Reception_AppontmentRegister();
                            reception_AppontmentRegister.ClinicEventClinicID = clickedClinicEventClinicID;
                            reception_AppontmentRegister.Show();

                            // Do something with the clickedClinicEventClinicID
                            Console.WriteLine("Clicked Clinic Event Clinic ID: " + clickedClinicEventClinicID);
                        };






                        // Adjust the width of the user control when added to the panel
                        recep_D_ClinicEvents.Width = flowLayoutPflowLayoutPanel_R_A_right.ClientSize.Width - recep_D_ClinicEvents.Margin.Horizontal;

                        flowLayoutPflowLayoutPanel_R_A_right.SizeChanged += (sender, e) =>
                        {
                            // Adjust the width of the user control to match the width of the parent container
                            recep_D_ClinicEvents.Width = flowLayoutPflowLayoutPanel_R_A_right.ClientSize.Width - recep_D_ClinicEvents.Margin.Horizontal;
                        };

                    }

                }
                reader1.Close();*/

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine("222222222222222222222222222222222  "+ex);
            }
            finally
            {
                connect.Close();
            }
        }

        private void Reception_Appointment_FormClosed(object sender, FormClosedEventArgs e)
        {
            
            Reception_Dashboard reception_Dashboard = new Reception_Dashboard(UserID);
            reception_Dashboard.Show();
            this.Hide();
        }
    }
}
