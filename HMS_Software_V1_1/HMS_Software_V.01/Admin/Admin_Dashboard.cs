using HMS_Software_V1._01.Admin.Admin_UserConotrols;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static HMS_Software_V1._01.Doctor_OPD.DoctorCheck_PatientCheck;

namespace HMS_Software_V1._01.Admin
{
    public partial class Admin_Dashboard : Form
    {
        SqlConnection connect = new SqlConnection(MyCommonConnecString.ConnectionString);

        private int userID;
        public Admin_Dashboard(int userID = 3)
        {
            InitializeComponent();
            this.userID = userID;
            MyLoadDataToUserControls();

            


        }


        public void MySendDataToUserControls()
        {
            // Your existing code to obtain AdminName
            admin_Doctors1.SetAdminName(AdminName);
            Console.WriteLine("AdminName sent to admin_Doctors1: " + AdminName);

            admin_Nurses1.SetAdminName(AdminName);
            Console.WriteLine("AdminName sent to admin_Nurses1: " + AdminName);

            admin_Patients1.SetAdminName(AdminName);
            Console.WriteLine("AdminName sent to admin_Patients1: " + AdminName);

            admin_Reception1.SetAdminName(AdminName);
            Console.WriteLine("AdminName sent to admin_Reception1: " + AdminName);

            admin_Appointment1.SetAdminName(AdminName);
            Console.WriteLine("AdminName sent to admin_Appointment1: " + AdminName);
        }




        private string AdminName;
        private string TodayDate;
        private string TodayTime;
        public void MyLoadDataToUserControls()
        {
            try
            {
                connect.Open();

                string query = "SELECT A_NameWithInitials FROM Admin WHERE Admin_ID = @adminID";

                using(SqlCommand sqlCommand2 = new SqlCommand(query, connect))
                {
                    sqlCommand2.Parameters.AddWithValue("@adminID", userID);
                    SqlDataReader reader2 = sqlCommand2.ExecuteReader();
                    if (reader2.Read())
                    {
                        AdminName = reader2.GetString(0);

                        Console.WriteLine("Admin Name:" + AdminName);

                        MySendDataToUserControls();

                        
                        /*MyDataStoringClass transport = new MyDataStoringClass();
                        transport.PatientName = reader2.GetString(0);
                        transport.PatientAge = reader2.GetString(1);
                        transport.PatientGender = reader2.GetString(2);*/


                    }
                    else
                    {
                        // Handle case when no matching record is found
                        MessageBox.Show("Patien Registration ID not match!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }

            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

                Console.WriteLine("AdminDashboard: " + ex);
            }
            finally
            {
                connect.Close();
            }


            /*Admin_home admin_Home = new Admin_home();
            Controls.Add(admin_Home);
            admin_Home.MySetFromDataFromAdminForm(AdminName);*/


            DateTime currentDate = DateTime.Today;
            DateTime currentTime = DateTime.Now;

            TodayDate = currentDate.ToShortDateString();
            TodayTime = currentTime.ToShortTimeString();

            admin_home1.MySendDataToUserControl(AdminName, TodayDate, TodayTime);
            admin_Doctors1.MySendDataToUserControl(AdminName, TodayDate, TodayTime);
            admin_Nurses1.MySendDataToUserControl(AdminName, TodayDate, TodayTime);
            admin_Patients1.MySendDataToUserControl(AdminName, TodayDate, TodayTime);
            admin_Reception1.MySendDataToUserControl(AdminName, TodayDate, TodayTime);
            admin_Appointment1.MySendDataToUserControl(AdminName, TodayDate, TodayTime);




        }

        private void admin_doctors_btn_Click(object sender, EventArgs e)
        {
            admin_Doctors1.Visible = true;
            admin_home1.Visible = false;
            admin_Nurses1.Visible = false;
            admin_Patients1.Visible = false;
            admin_Reception1.Visible = false;
            admin_Appointment1.Visible = false;
        }

        private void admin_home_btn_Click(object sender, EventArgs e)
        {
            admin_Doctors1.Visible = false;
            admin_home1.Visible = true;
            admin_Nurses1.Visible = false;
            admin_Patients1.Visible = false;
            admin_Reception1.Visible = false;
            admin_Appointment1.Visible = false;
        }

        private void admin_nurse_btn_Click(object sender, EventArgs e)
        {
            admin_Doctors1.Visible = false;
            admin_home1.Visible = false;
            admin_Nurses1.Visible = true;
            admin_Patients1.Visible = false;
            admin_Reception1.Visible = false;
            admin_Appointment1.Visible = false;
        }

        private void admin_patiets_btn_Click(object sender, EventArgs e)
        {
            admin_Doctors1.Visible = false;
            admin_home1.Visible = false;
            admin_Nurses1.Visible = false;
            admin_Patients1.Visible = true;
            admin_Reception1.Visible = false;
            admin_Appointment1.Visible = false;
        }

        private void admin_Reception_btn_Click(object sender, EventArgs e)
        {

            admin_Doctors1.Visible = false;
            admin_home1.Visible = false;
            admin_Nurses1.Visible = false;
            admin_Patients1.Visible = false;
            admin_Reception1.Visible = true;
            admin_Appointment1.Visible = false;
        }

        private void admin_Appointment_btn_Click(object sender, EventArgs e)
        {
            admin_Doctors1.Visible = false;
            admin_home1.Visible = false;
            admin_Nurses1.Visible = false;
            admin_Patients1.Visible = false;
            admin_Reception1.Visible = false;
            admin_Appointment1.Visible = true;

        }
    }
}
