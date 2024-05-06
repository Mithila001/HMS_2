using HMS_Software_V_2.Reception.UserControls;
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

namespace HMS_Software_V_2.Reception
{
    public partial class Reception_Appointment : Form
    {
        SqlConnection connect = new SqlConnection(MyCommonConnecString.ConnectionString);

        private int _clinicId;
        int UserID;
        public Reception_Appointment(int userID)
        {
            InitializeComponent();
            LoadUserData();
            this.UserID = userID;
            /*this.FormClosed += (s, e) => new Reception_Dashboard().Show();*/


            this.SizeChanged += MyRe_Appointments_SizeChanged; //To fix clinicTypeAvailableDates Usercontrol scaling issues from small window to Full screen

            R_A_SelectClinicType r_A_SelectClinicType = new R_A_SelectClinicType();
            r_A_SelectClinicType.ClinicClicked += (sender, clinicId) =>
            {
                _clinicId = clinicId;

            };
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

                    r_A_SelectClinicType.RASCT_ClincType_lbl.Text = reader1["CT_Name"].ToString();

                    /*int clinicID = Convert.ToInt32(reader1["Clinic_ID"]);*/
                    // Set the ID as the Tag property
                    r_A_SelectClinicType.Tag = reader1["ClincType_ID"];

                    r_A_SelectClinicType.ClinicClicked += (sender, clinicID) =>
                    {
                        flowLayoutPflowLayoutPanel_R_A_right.Controls.Clear();

                        // Call LoadUserData2() method when ClinicClicked event is raised
                        LoadUserData2(sender, clinicID);
                        Console.WriteLine(" ((---5---)) LoadUserData2 Method triggerd ");
                    };


                    Console.WriteLine("((---1---))Tag Assigned: " + r_A_SelectClinicType.Tag);
                    /*
                                        // Check if there are clinics today
                                        if (reader1["Date"] != DBNull.Value)
                                        {
                                            DateTime eventDate = Convert.ToDateTime(reader1["Date"]);
                                            if (eventDate.Date == today)
                                            {
                                                selectClinicType.clinicIsAvailable_lbl.Text = "Available";
                                            }
                                            else
                                            {
                                                selectClinicType.clinicIsAvailable_lbl.Text = "Not Available";
                                            }
                                        }
                                        else
                                        {
                                            // No clinic event for today, set the availability status accordingly
                                            selectClinicType.clinicIsAvailable_lbl.Text = "Not Available";
                                        }*/

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
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine("11111111111111111111111111111  " + ex);
            }
            finally
            {
                connect.Close();
            }
        }

        private void LoadUserData2(object sendersenderObj, int clinicID)
        {
            try
            {
                connect.Open();
                DateTime today = DateTime.Today;
                //================================= Show Clinic Types UserControls =================================
                string query = "SELECT ce.[CE_Date], ce.CE_StartTime, ce.CE_EndTime, d.D_NameWithInitials, ct.CT_Name, ce.CE_HallNumber, ct.CT_WardNo, ce.CE_TotalSlots, ce.CE_TakenSlots, ce.ClinicEvent_ID AS ClincEventClinicID"
                    + " FROM ClinicEvents ce"
                    + " INNER JOIN Doctor d ON ce.Doctor_ID = d.Doctor_ID"
                    + " INNER JOIN ClincType ct ON ce.Clinic_ID = ct.ClincType_ID;";

                SqlCommand command = new SqlCommand(query, connect);

                SqlDataReader reader1 = command.ExecuteReader();



                // Loop through the records retrieved from the database
                while (reader1.Read())
                {
                    int ClinicTypeID = clinicID;
                    /*Console.WriteLine(" ((---4---)) Retrived the clinic ID: ",ClinicTypeID);*/

                    Recep_D_ClinicEvents recep_D_ClinicEvents = new Recep_D_ClinicEvents();

                    int ClinicEventClinicTypeID = Convert.ToInt32(reader1["ClincEventClinicID"]);
                    /*Console.WriteLine(" ((---4---)) Retrived the ID From Table:", ClinicEventClinicTypeID);*/

                    if (ClinicEventClinicTypeID == ClinicTypeID)
                    {



                        /*Console.WriteLine(" ((---5---)) Executed");*/
                        recep_D_ClinicEvents.RPA_doctorName_lbl.Text = reader1["D_NameWithInitials"].ToString();
                        recep_D_ClinicEvents.RPA_clincType_lbl.Text = reader1["CT_Name"].ToString();
                        recep_D_ClinicEvents.RPA_hallNumber_lbl.Text = "Hall "+reader1["CE_HallNumber"].ToString();
                        /*recep_D_ClinicEvents.RPA_time_lbl.Text = reader1["ClinicName"].ToString();*/
                        recep_D_ClinicEvents.RPA_date_lbl.Text = reader1["CE_Date"].ToString();
                        recep_D_ClinicEvents.RPA_wardNo_lbl.Text = reader1["CT_WardNo"].ToString();
                        recep_D_ClinicEvents.RPA_totalSlots_lbl.Text = reader1["CE_TotalSlots"].ToString();
                        /*recep_D_ClinicEvents.RPA_availableSlots_lbl.Text = reader1["CT_WardNo"].ToString();*/

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
                reader1.Close();

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
