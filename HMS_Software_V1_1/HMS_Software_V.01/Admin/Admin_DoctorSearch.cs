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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace HMS_Software_V1._01.Admin
{
    public partial class Admin_DoctorSearch : Form
    {
        SqlConnection connect = new SqlConnection(MyCommonConnecString.ConnectionString);//Call connection string from a class
        BindingSource bs = new BindingSource(); //For Search Filter

        string AdminName;
        public Admin_DoctorSearch(string adminName)
        {
            InitializeComponent();
            doctorSearch_combobox.SelectedIndex = 0;
            this.FormClosed += (s, e) => new Admin_Dashboard().Show();

            this.AdminName = adminName;
            A_DS_adminName_lbl.Text = AdminName;

            string formattedDate = DateTime.Today.ToString("dd-MM-yyyy");
            string formattedTime = DateTime.Now.ToString("h.mm tt");
            A_DS_time.Text = formattedTime;
            A_DS_date.Text = formattedDate;
        }

        private void Admin_DoctorSearch_Load(object sender, EventArgs e)
        {
            MyRefershTableData();
        }

        public void MyRefershTableData() // To Refresh Data Table
        {
            MyLoadTablData();  
        }

        private void MyLoadTablData()
        {
            string query = "SELECT * FROM Doctor";
            DataTable dataTable = new DataTable();

            try
            {
                connect.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(query, connect);
                adapter.Fill(dataTable);
                dataGridView1_doctor.DataSource = dataTable;

                // Assign the DataTable to the BindingSource
                bs.DataSource = dataTable;
                dataGridView1_doctor.DataSource = bs;


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

        //=========================== Search Filter ========================================
        private void SearchData(string searchTerm)
        {

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                if (doctorSearch_combobox.Text == "By ID")
                {
                    
                    bs.Filter = $"Convert(Doctor_ID, 'System.String') like '%{searchTerm}%'"; // Becouse ID is an int ??
                }
                else
                {
                   
                    bs.Filter = $"D_NIC like '%{searchTerm}%'";
                }
            }
            else
            {
                // If searchTerm is empty or null, clear the filter
                bs.Filter = string.Empty;
            }
        }

        private void A_D_search_tbx_TextChanged(object sender, EventArgs e)
        {        
            SearchData(A_D_search_tbx.Text);
        }

        private void doctorSearch_combobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            bs.Filter = string.Empty;
            A_D_search_tbx.Text = "";
        }
    }
}
