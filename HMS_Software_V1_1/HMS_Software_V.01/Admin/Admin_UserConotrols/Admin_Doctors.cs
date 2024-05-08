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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace HMS_Software_V1._01.Admin.Admin_UserConotrols
{
    public partial class Admin_Doctors : UserControl
    {
        public Admin_Doctors()
        {
            InitializeComponent();

            MyLoadDashboardData();
        }

        public void MySendDataToUserControl(string adminName, string date, string time)
        {
           
            Ad_Doc_Date.Text = date;
            Ad_Doc_time.Text = time;
         
        }

        string AdminName;
        public void SetAdminName(string adminName) // Recevied from the Admin Dahsboard
        {
            AdminName = adminName;
        }




        private void searchDoctor_btn_Click(object sender, EventArgs e)
        {
            Admin_DoctorSearch admin_DoctorSearch = new Admin_DoctorSearch(AdminName);  
            admin_DoctorSearch.Show();

            Form parentForm = this.FindForm();
            parentForm.Hide();


        }

        private void registerDoctor_btn_Click(object sender, EventArgs e)
        {
            Admin_DoctorRegister admin_DoctorRegister   = new Admin_DoctorRegister(AdminName);
            admin_DoctorRegister.Show();

            Form parentForm = this.FindForm();  
            parentForm.Hide();
            
        }

        int TotalDoctors;
        int TotalOPDDoctors;
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
                        
                    }

                    // Total Clinic and OPD Doctors
                    string query2 = "SELECT COUNT(DISTINCT Doctor_ID) FROM ClinicEvents";

                    using (SqlCommand command = new SqlCommand(query2, connect))
                    {
                        TotalOPDDoctors = (int)command.ExecuteScalar();
                        
                    }
                }
                    
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error1: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            Ad_Doc_TotalDoc_lbl.Text = TotalDoctors.ToString();
            Ad_Doc_TotClincDoc_lbl.Text = TotalOPDDoctors.ToString();
            Ad_Doc_TotOPDDoc_lbl.Text = TotalOPDDoctors.ToString();


        }
    }
}
