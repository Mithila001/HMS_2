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
    public partial class Admin_Nurses : UserControl
    {
        public Admin_Nurses()
        {
            InitializeComponent();

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
            Ad_Nurse_Date.Text = date;
            Ad_Nurse_Time.Text = time;
        }

        private void RegisterNurse_btn_Click(object sender, EventArgs e)
        {
            Admin_NurseRegister admin_NurseRegister = new Admin_NurseRegister();
            admin_NurseRegister.Show();

            Form parentForm = this.FindForm();
            parentForm.Hide();
        }

        private void SearchNurse_btn_Click(object sender, EventArgs e)
        {
            Admin_NurseSearch admin_NurseSearch = new Admin_NurseSearch();  
            admin_NurseSearch.Show();

            Form parentForm = this.FindForm();
            parentForm.Hide();
        }

        int TotalNurses;
        int TotalSisters;
        
        private void MyLoadDashboardData()
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(MyCommonConnecString.ConnectionString))
                {
                    connect.Open();

                    // Total Doctors
                    string query1 = "SELECT COUNT(*) FROM Nurse";

                    using (SqlCommand command = new SqlCommand(query1, connect))
                    {
                        TotalNurses = (int)command.ExecuteScalar();

                    }

                    // Total Clinic and OPD Doctors
                    string query2 = "SELECT COUNT(*) FROM Nurse WHERE N_Position = @N_Position";

                    using (SqlCommand command = new SqlCommand(query2, connect))
                    {
                        command.Parameters.AddWithValue("@N_Position", "sister");

                        TotalSisters = (int)command.ExecuteScalar();

                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error1: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            Ad_Nurse_TotNurse_lbl.Text = TotalNurses.ToString();
            Ad_Nurse_TotSisters_lbl.Text = TotalSisters.ToString();
           

        }
    }
}
