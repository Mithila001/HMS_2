using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HMS_Software_V1._01.Admin.Admin_UserConotrols
{
    public partial class Admin_Reception : UserControl
    {
        public Admin_Reception()
        {
            InitializeComponent();

            MyLoadTablData();
        }

        string AdminName;
        public void SetAdminName(string adminName) // Recevied from the Admin Dahsboard
        {
            AdminName = adminName;
        }


        //Reciving Data from the form
        public void MySendDataToUserControl(string adminName, string date, string time)
        {
            Ad_reception_Date.Text = date;
            Ad_Reception_time.Text = time;
        }

        private void RegisterReception_btn_Click(object sender, EventArgs e)
        {
            Admin_ReceptionRegistration admin_ReceptionRegistration = new Admin_ReceptionRegistration();
            admin_ReceptionRegistration.Show();

            Form parentForm = this.FindForm();
            parentForm.Hide();
        }

        private void MyLoadTablData()
        {
            string query = "SELECT * FROM Reception";
            DataTable dataTable = new DataTable();

            try
            {
                using (SqlConnection connect = new SqlConnection(MyCommonConnecString.ConnectionString))
                {
                    connect.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(query, connect);
                    adapter.Fill(dataTable);
                    reception_DataGView.DataSource = dataTable;

                    


                }
                    
                


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine(ex);
            }
        }
    }
}
