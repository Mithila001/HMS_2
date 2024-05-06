using HMS_Software_V1._01.Admin;
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

namespace HMS_Software_V1._01.Reception
{
    public partial class Reception_PatientSearch : Form
    {
        SqlConnection connect = new SqlConnection(MyCommonConnecString.ConnectionString);//Call connection string from a class
        BindingSource bs = new BindingSource(); //For Search Filter

        int UserID;
        public Reception_PatientSearch(int userID)
        {
            InitializeComponent();
            R_PaSearch_combobox.SelectedIndex = 0;
            this.UserID = userID;

            /*this.FormClosed += (s, e) => new Reception_Dashboard().Show();*/

        }

        private void Reception_PatientSearch_Load(object sender, EventArgs e)
        {
            MyRefershTableData();
        }

        public void MyRefershTableData() // To Refresh Data Table
        {
            MyLoadTablData();
        }

        private void MyLoadTablData()
        {
            string query = "SELECT P_RegistrationID, P_NameWithIinitials, P_Age, P_Gender, P_NIC, P_G_ContactNo, P_G_Name, P_Status   FROM Patient";
            DataTable dataTable = new DataTable();

            try
            {
                connect.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(query, connect);
                adapter.Fill(dataTable);
                dataGridView_R_PaSearch.DataSource = dataTable;

                // Assign the DataTable to the BindingSource
                bs.DataSource = dataTable;
                dataGridView_R_PaSearch.DataSource = bs;

                dataGridView_R_PaSearch.Columns["P_RegistrationID"].HeaderText = "Registration ID";
                dataGridView_R_PaSearch.Columns["P_NameWithIinitials"].HeaderText = "Name";
                dataGridView_R_PaSearch.Columns["P_Age"].HeaderText = "Age";
                dataGridView_R_PaSearch.Columns["P_Gender"].HeaderText = "Gender";
                dataGridView_R_PaSearch.Columns["P_NIC"].HeaderText = "NIC";
                dataGridView_R_PaSearch.Columns["P_G_ContactNo"].HeaderText = "Guardien No";
                dataGridView_R_PaSearch.Columns["P_G_Name"].HeaderText = "Guardien Name";
                dataGridView_R_PaSearch.Columns["P_Status"].HeaderText = "Status";


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine(ex);
            }
            finally
            {
                connect.Close();
            }
        }

        private void Reception_PatientSearch_FormClosed(object sender, FormClosedEventArgs e)
        {
            Reception_Dashboard reception_Dashboard = new Reception_Dashboard(UserID);
            reception_Dashboard.Show();
            this.Hide();
        }

        private void R_PaSearch_tbx_TextChanged(object sender, EventArgs e)
        {
            SearchData(R_PaSearch_tbx.Text);
        }



        private void SearchData(string searchTerm)
        {

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                if (R_PaSearch_tbx.Text == "By ID")
                {

                    bs.Filter = $"Convert(Patient_ID, 'System.String') like '%{searchTerm}%'"; // Becouse ID is an int ??
                }
                else
                {

                    bs.Filter = $"P_NIC like '%{searchTerm}%'";
                }
            }
            else
            {
                // If searchTerm is empty or null, clear the filter
                bs.Filter = string.Empty;
            }
        }
    }
}
