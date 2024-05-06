using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HMS_Software_V1._01.Admin.Admin_UserConotrols
{
    public partial class Admin_home : UserControl
    {
        public string AdminName;
        public Admin_home()
        {
            InitializeComponent();

            MyLoadDashboardData();
        }

        public void MySendDataToUserControl(string adminName, string date, string time)
        {
            A_H_adminName.Text = adminName;
            A_H_date.Text = date;
            A_H_time.Text = time;
            Console.WriteLine("Admin Name recived to userControl:"+adminName);
        }

        int TotalDoctors;
        int TotalNurses;
        int TotalPatients;
        int TotalReceptions;
        int TotalWards;
        int TotalClinics;

        private void MyLoadDashboardData()
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(MyCommonConnecString.ConnectionString))
                {
                    connect.Open();

                    // Total Doctors
                    string query1 = "SELECT COUNT(*) FROM Doctor";

                    using (SqlCommand command = new SqlCommand(query1, connect))
                    {
                        TotalDoctors = (int)command.ExecuteScalar();
                        Console.WriteLine("Number of matching records: " + TotalDoctors);
                    }

                    // Total Nurses
                    string query2 = "SELECT COUNT(*) FROM Nurse";

                    using (SqlCommand command = new SqlCommand(query2, connect))
                    {
                        TotalNurses = (int)command.ExecuteScalar();

                    }

                    // Total Patient
                    string query3 = "SELECT COUNT(*) FROM Patient";

                    using (SqlCommand command = new SqlCommand(query3, connect))
                    {
                        TotalPatients = (int)command.ExecuteScalar();

                    }

                    // Total Receptions
                    string query4 = "SELECT COUNT(*) FROM Reception";

                    using (SqlCommand command = new SqlCommand(query4, connect))
                    {
                        TotalReceptions = (int)command.ExecuteScalar();

                    }

                    // Total Wards
                    string query5 = "SELECT COUNT(*) FROM WardTypes";

                    using (SqlCommand command = new SqlCommand(query5, connect))
                    {
                        TotalWards = (int)command.ExecuteScalar();

                    }

                    // Total Clinics
                    string query6 = "SELECT COUNT(*) FROM ClincType";

                    using (SqlCommand command = new SqlCommand(query6, connect))
                    {
                        TotalClinics = (int)command.ExecuteScalar();

                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error1: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            AdminHome_TDoc_lbl.Text = TotalDoctors.ToString();
            AdminHome_TClinics_lbl.Text = TotalClinics.ToString();
            AdminHome_TNurse_lbl.Text = TotalNurses.ToString();
            AdminHome_TPatient_lbl.Text = TotalPatients.ToString();
            AdminHome_TReception_lbl.Text = TotalReceptions.ToString();
            AdminHome_TWard_lbl.Text = TotalWards.ToString();


        }
    }
}
