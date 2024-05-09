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

namespace HMS_Software_V1._01.Lab_Management
{
    public partial class Lab_Dashboard : Form
    {
        private Panel panel;
        private Timer timer;
        bool labCollapse;
        
     
        public Lab_Dashboard()
        {
            InitializeComponent();
            label3.Text = $"Employees Working:{Properties.Settings.Default.Count}/6";
            LoadSavedValues();
            DisplayRowCount();
            DisplayRowCount2();

            timer = new Timer();
            timer.Interval = 500;
            timer.Tick += Timer_Tick;
            timer.Start();


            panel = new Panel();
            panel.Width = 1050;
            panel.Height = 195;
            panel.Size = new Size(860, 150);
            panel.AutoScroll = false;
            panel.BorderStyle = BorderStyle.FixedSingle;
            panel.BackColor = Color.FromArgb(192, 255, 192);



            panel.Location = new System.Drawing.Point(255, 475);


            Controls.Add(panel);


            LoadDataFromDatabase();
        }


        private void Timer_Tick(object sender, EventArgs e)
        {
           
            label3.Text = $"Employees Working:{Properties.Settings.Default.Count}";
            DisplayRowCount();
            DisplayRowCount2();
            LoadDataFromDatabase();
            LoadSavedValues();


        }
        private void LoadSavedValues()
        {
            label5.Text = $"Status: {Properties.Settings.Default.Value1}";
            label6.Text = $"Phlembotomist: {Properties.Settings.Default.Value2}";
            label7.Text = $"Test No: {Properties.Settings.Default.Value3}";
            label10.Text = $"Status: {Properties.Settings.Default.Value6}";
            label9.Text = $"Phlembotomist: {Properties.Settings.Default.Value5}";
            label8.Text = $"Test No: {Properties.Settings.Default.Value4}";
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            timer.Stop();
        }




        private void DisplayRowCount()
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(MyCommonConnecString.ConnectionString))
                {
                    connect.Open();
                    string query = "SELECT COUNT(*) FROM Lab_Request1";
                    using (SqlCommand command = new SqlCommand(query, connect))
                    {
                        // No need to open the connection again as it's already opened

                        int rowCount = (int)command.ExecuteScalar();

                        label1.TextAlign = ContentAlignment.MiddleCenter;
                        label1.Padding = new Padding(0, 0, 0, 0);
                        label2.Text = $"Tests to\nDo : {rowCount}";
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error2: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine("Error2 :" + ex);
            }

        }
        private void DisplayRowCount2()
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(MyCommonConnecString.ConnectionString))
                {
                    connect.Open();
                    string query = "SELECT COUNT(*) FROM Finished_Tests";

                    using (SqlCommand command = new SqlCommand(query, connect))
                    {
                        

                        int rowCount = (int)command.ExecuteScalar();

                        label1.TextAlign = ContentAlignment.MiddleCenter;
                        label1.Padding = new Padding(0, 0, 0, 0);
                        label1.Text = $"No of\n Tests : {rowCount}";

                    }
                }

                    
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error3: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine("Error3 :" + ex);
            }
          
        }

        private void LoadDataFromDatabase()
        {
            
            try
            {
                using (SqlConnection connect = new SqlConnection(MyCommonConnecString.ConnectionString))
                {
                    connect.Open();
                    string query = "SELECT TOP 9 inventory, quantity FROM Inventory";
                    using (SqlCommand command = new SqlCommand(query, connect))
                    {


                        SqlDataReader reader = command.ExecuteReader();

                        int labelHeight = 25;
                        int labelWidth = 340;
                        int xMargin = 20;
                        int yMargin = 20;
                        int rowSpacing = 15;

                        int rowIndex = 0;
                        int columnIndex = 0;
                        int totalRows = 0;
                        int rowCount = 0;

                        while (reader.Read() && rowCount < 9)
                        {
                            string inventory = reader["inventory"].ToString();
                            string quantity = reader["quantity"].ToString();

                            Label label = new Label();
                            label.Text = $"{inventory} : {quantity}";
                            label.AutoSize = false;
                            label.Width = labelWidth;
                            label.Height = labelHeight;
                            label.Font = new Font(label.Font.FontFamily, 11);

                            int x = columnIndex * (labelWidth + xMargin) + xMargin;
                            int y = rowIndex * (labelHeight + rowSpacing) + yMargin;
                            label.Location = new System.Drawing.Point(x, y);

                            panel.Controls.Add(label);

                            columnIndex++;
                            if (columnIndex >= 3)
                            {
                                columnIndex = 0;
                                rowIndex++;
                                totalRows++;
                            }
                        }


                        int startIndex = (totalRows - 1) * 3;


                        for (int i = startIndex; i < panel.Controls.Count; i++)
                        {
                            Label currentLabel = panel.Controls[i] as Label;
                            if (currentLabel != null)
                            {
                                if (i >= startIndex + 2)
                                {
                                    currentLabel.Width = 150;
                                }
                            }
                        }

                        reader.Close();
                    }

                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data 4: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void labCollapsed_Tick(object sender, EventArgs e)
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
/*
        private void button1_Click(object sender, EventArgs e)
        {
            labtimer.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bool Isopen = false;
            foreach (Form Lab_Dashboard in Application.OpenForms)
            {
                if (Lab_Dashboard.Name == "Lab_Dashboard")
                {
                    Isopen = true;
                    Lab_Dashboard.BringToFront();
                    break;
                }
            }
            if (Isopen == false)
            {
                Lab_Dashboard Lab_Dashboard = new Lab_Dashboard();
                Lab_Dashboard.Show();
            }

        }

        private void button4_Click(object sender, EventArgs e)
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

        private void button3_Click(object sender, EventArgs e)
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

        private void button7_Click(object sender, EventArgs e)
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

        private void button5_Click(object sender, EventArgs e)
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

        private void button6_Click(object sender, EventArgs e)
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

        private void label1_Click(object sender, EventArgs e)
        {

        }
        */
        private void button19_Click(object sender, EventArgs e)
        {
               labtimer.Start();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            bool Isopen = false;
            foreach (Form Lab_Dashboard in Application.OpenForms)
            {
                if (Lab_Dashboard.Name == "Lab_Dashboard")
                {
                    Isopen = true;
                    Lab_Dashboard.BringToFront();
                    break;
                }
            }
            if (Isopen == false)
            {
                Lab_Dashboard Lab_Dashboard = new Lab_Dashboard();
                Lab_Dashboard.Show();
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

        private void labtimer_Tick(object sender, EventArgs e)
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

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
