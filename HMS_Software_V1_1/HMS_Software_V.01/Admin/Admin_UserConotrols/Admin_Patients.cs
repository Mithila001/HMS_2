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

namespace HMS_Software_V1._01.Admin.Admin_UserConotrols
{
    public partial class Admin_Patients : UserControl
    {
        public Admin_Patients()
        {
            InitializeComponent();

            SearchPatient_btn.Visible = false;


            MyLoadDashboardData();
        }


        string AdminName;
        public void SetAdminName(string adminName) // Recevied from the Admin Dahsboard
        {
            AdminName = adminName;
        }


        //Reciving Data from the form
        public void MySendDataToUserControl(string adminName, string date, string time)
        {
            Ad_patient_Date.Text = date;
            Ad_patient_time.Text = time;
        }

        private void SearchPatient_btn_Click(object sender, EventArgs e)
        {
            Admin_PatientSearch admin_PatientSearch = new Admin_PatientSearch();
            admin_PatientSearch.Show();

            Form parentForm = this.FindForm();
            parentForm.Hide();
        }

        int TotalInpatietns;
        int TotalPatients;
        int TotalClinicPatients;

        private void MyLoadDashboardData()
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(MyCommonConnecString.ConnectionString))
                {
                    connect.Open();

                    // Total Doctors
                    string query1 = "SELECT COUNT(*) FROM Patient";

                    using (SqlCommand command = new SqlCommand(query1, connect))
                    {
                        TotalPatients = (int)command.ExecuteScalar();

                    }

                    // Total Clinic and OPD Doctors
                    string query2 = "SELECT COUNT(DISTINCT PatientAppointment_ID) FROM Patient_Appointment";

                    using (SqlCommand command = new SqlCommand(query2, connect))
                    {
                        TotalClinicPatients = (int)command.ExecuteScalar();

                    }

                    // Total Clinic and OPD Doctors
                    string query3 = "SELECT COUNT(*) FROM Admitted_Patients";

                    using (SqlCommand command = new SqlCommand(query3, connect))
                    {
                        TotalInpatietns = (int)command.ExecuteScalar();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error1: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            Total_Inpatients.Text = TotalInpatietns.ToString();
            Total_Patients.Text = TotalPatients.ToString();
            Tot_ClinicPatients.Text = TotalClinicPatients.ToString();


        }
    }
}
