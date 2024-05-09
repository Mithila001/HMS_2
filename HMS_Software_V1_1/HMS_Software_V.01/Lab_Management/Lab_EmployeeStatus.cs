using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HMS_Software_V1._01.Lab_Management
{
    public partial class Lab_EmployeeStatus : Form
    {
        bool labCollapse;
        private Timer timer;
        public Lab_EmployeeStatus()
        {
            InitializeComponent();
            LoadSavedData();
            timer = new Timer();
            timer.Interval = 500;
            timer.Tick += Timer_Tick;
            timer.Start();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            LoadSavedData();


        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            timer.Stop();
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
        public void LoadSavedData()
        {
            label1.Text = Properties.Settings.Default.result;
            label2.Text = Properties.Settings.Default.result2;
            label3.Text = Properties.Settings.Default.result3;
            label5.Text = Properties.Settings.Default.result4;
            label6.Text = Properties.Settings.Default.result5;
            label7.Text = Properties.Settings.Default.result6;
        }

        private void labtimer_Tick(object sender, EventArgs e)
        {

        }

        private void button19_Click(object sender, EventArgs e)
        {
            labtimer.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool Isopen = false;
            foreach (Form Lab_EmployeeStatus_Edit in Application.OpenForms)
            {
                if (Lab_EmployeeStatus_Edit.Name == "Lab_EmployeeStatus_Edit")
                {
                    Isopen = true;
                    Lab_EmployeeStatus_Edit.BringToFront();
                    break;
                }
            }
            if (Isopen == false)
            {
                Lab_EmployeeStatus_Edit Lab_EmployeeStatus_Edit = new Lab_EmployeeStatus_Edit();
                Lab_EmployeeStatus_Edit.Show();
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

        private void button1_Click_1(object sender, EventArgs e)
        {
            bool Isopen = false;
            foreach (Form Lab_EmployeeStatus_Edit in Application.OpenForms)
            {
                if (Lab_EmployeeStatus_Edit.Name == "Lab_EmployeeStatus_Edit")
                {
                    Isopen = true;
                    Lab_EmployeeStatus_Edit.BringToFront();
                    break;
                }
            }
            if (Isopen == false)
            {
                Lab_EmployeeStatus_Edit Lab_EmployeeStatus_Edit = new Lab_EmployeeStatus_Edit();
                Lab_EmployeeStatus_Edit.Show();
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
    }
}
