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

namespace HMS_Software_V1._01.Lab_Management
{
    public partial class Lab_CompleteTestDetails : Form
    {
        bool labCollapse;
        SqlConnection connect = new SqlConnection(MyCommonConnecString.ConnectionString);
        public Lab_CompleteTestDetails()
        {
            InitializeComponent();
            DisplayData();
        }

        public void DisplayData()
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(MyCommonConnecString.ConnectionString))
                {
                    connect.Open();

                    string query = "SELECT * FROM Finished_Tests;";
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, connect))
                    {
                        DataSet dataset = new DataSet();
                        adapter.Fill(dataset, "Finished_Tests");
                        dataGridView1.DataSource = dataset.Tables["Finished_Tests"];
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            labtimer.Start();
        }

        private void labtimer_Tick(object sender, EventArgs e)
        {

        }

        private void button20_Click(object sender, EventArgs e)
        {
            bool Isopen = false;
            foreach (Form Form1 in Application.OpenForms)
            {
                if (Form1.Name == "Form1")
                {
                    Isopen = true;
                    Form1.BringToFront();
                    break;
                }
            }
            if (Isopen == false)
            {
                Lab_Dashboard form1 = new Lab_Dashboard();
                form1.Show();
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            bool Isopen = false;
            foreach (Form Lab_UpdateTestDetails in Application.OpenForms)
            {
                if (Lab_UpdateTestDetails.Name == "Lab_UpdateTestDetails")
                {
                    Isopen = true;
                    Lab_UpdateTestDetails.BringToFront();
                    break;
                }
            }
            if (Isopen == false)
            {
                Lab_UpdateTestDetails Lab_UpdateTestDetails = new Lab_UpdateTestDetails();
                Lab_UpdateTestDetails.Show();
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            bool Isopen = false;
            foreach (Form Lab_EmployeeStatus in Application.OpenForms)
            {
                if (Lab_EmployeeStatus.Name == "Lab_EmployeeStatus")
                {
                    Isopen = true;
                    Lab_EmployeeStatus.BringToFront();
                    break;
                }
            }
            if (Isopen == false)
            {
                Lab_EmployeeStatus Lab_EmployeeStatus = new Lab_EmployeeStatus();
                Lab_EmployeeStatus.Show();
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            bool Isopen = false;
            foreach (Form Lab_TestToDo in Application.OpenForms)
            {
                if (Lab_TestToDo.Name == "Lab_TestToDo")
                {
                    Isopen = true;
                    Lab_TestToDo.BringToFront();
                    break;
                }
            }
            if (Isopen == false)
            {
                Lab_TestToDo Lab_TestToDo = new Lab_TestToDo();
                Lab_TestToDo.Show();
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            bool Isopen = false;
            foreach (Form Lab_CompleteTestDetails in Application.OpenForms)
            {
                if (Lab_CompleteTestDetails.Name == "Lab_CompleteTestDetails")
                {
                    Isopen = true;
                    Lab_CompleteTestDetails.BringToFront();
                    break;
                }
            }
            if (Isopen == false)
            {
                Lab_CompleteTestDetails Lab_CompleteTestDetails = new Lab_CompleteTestDetails();
                Lab_CompleteTestDetails.Show();
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            bool Isopen = false;
            foreach (Form Lab_Inventory in Application.OpenForms)
            {
                if (Lab_Inventory.Name == "Lab_Inventory")
                {
                    Isopen = true;
                    Lab_Inventory.BringToFront();
                    break;
                }
            }
            if (Isopen == false)
            {
                Lab_Inventory Lab_Inventory = new Lab_Inventory();
                Lab_Inventory.Show();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void labtimer_Tick_1(object sender, EventArgs e)
        {
            if (labCollapse)
            {
                panel8.Height += 10;
                if (panel8.Height == panel8.MaximumSize.Height)
                {
                    labCollapse = false;
                    labtimer.Stop();
                }
            }
            else
            {
                panel8.Height -= 10;
                if (panel8.Height == panel8.MinimumSize.Height)
                {
                    labCollapse = true;
                    labtimer.Stop();
                }
            }
        }

        private void button19_Click_1(object sender, EventArgs e)
        {
            labtimer.Start();
        }

        private void button20_Click_1(object sender, EventArgs e)
        {
            bool Isopen = false;
            foreach (Form Form1 in Application.OpenForms)
            {
                if (Form1.Name == "Form1")
                {
                    Isopen = true;
                    Form1.BringToFront();
                    break;
                }
            }
            if (Isopen == false)
            {
                Lab_Dashboard form1 = new Lab_Dashboard();
                form1.Show();
            }
        }

        private void button18_Click_1(object sender, EventArgs e)
        {
            bool Isopen = false;
            foreach (Form Lab_UpdateTestDetails in Application.OpenForms)
            {
                if (Lab_UpdateTestDetails.Name == "Lab_UpdateTestDetails")
                {
                    Isopen = true;
                    Lab_UpdateTestDetails.BringToFront();
                    break;
                }
            }
            if (Isopen == false)
            {
                Lab_UpdateTestDetails Lab_UpdateTestDetails = new Lab_UpdateTestDetails();
                Lab_UpdateTestDetails.Show();
            }
        }

        private void button17_Click_1(object sender, EventArgs e)
        {
            bool Isopen = false;
            foreach (Form Lab_EmployeeStatus in Application.OpenForms)
            {
                if (Lab_EmployeeStatus.Name == "Lab_EmployeeStatus")
                {
                    Isopen = true;
                    Lab_EmployeeStatus.BringToFront();
                    break;
                }
            }
            if (Isopen == false)
            {
                Lab_EmployeeStatus Lab_EmployeeStatus = new Lab_EmployeeStatus();
                Lab_EmployeeStatus.Show();
            }
        }

        private void button16_Click_1(object sender, EventArgs e)
        {
            bool Isopen = false;
            foreach (Form Lab_TestToDo in Application.OpenForms)
            {
                if (Lab_TestToDo.Name == "Lab_TestToDo")
                {
                    Isopen = true;
                    Lab_TestToDo.BringToFront();
                    break;
                }
            }
            if (Isopen == false)
            {
                Lab_TestToDo Lab_TestToDo = new Lab_TestToDo();
                Lab_TestToDo.Show();
            }
        }

        private void button15_Click_1(object sender, EventArgs e)
        {
            bool Isopen = false;
            foreach (Form Lab_CompleteTestDetails in Application.OpenForms)
            {
                if (Lab_CompleteTestDetails.Name == "Lab_CompleteTestDetails")
                {
                    Isopen = true;
                    Lab_CompleteTestDetails.BringToFront();
                    break;
                }
            }
            if (Isopen == false)
            {
                Lab_CompleteTestDetails Lab_CompleteTestDetails = new Lab_CompleteTestDetails();
                Lab_CompleteTestDetails.Show();
            }
        }

        private void button14_Click_1(object sender, EventArgs e)
        {
            bool Isopen = false;
            foreach (Form Lab_Inventory in Application.OpenForms)
            {
                if (Lab_Inventory.Name == "Lab_Inventory")
                {
                    Isopen = true;
                    Lab_Inventory.BringToFront();
                    break;
                }
            }
            if (Isopen == false)
            {
                Lab_Inventory Lab_Inventory = new Lab_Inventory();
                Lab_Inventory.Show();
            }
        }
    }
}
