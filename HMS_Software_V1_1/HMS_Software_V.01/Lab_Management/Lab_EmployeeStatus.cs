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
            foreach (Form Form2 in Application.OpenForms)
            {
                if (Form2.Name == "Form2")
                {
                    Isopen = true;
                    Form2.BringToFront();
                    break;
                }
            }
            if (Isopen == false)
            {
                Lab_EmployeeStatus_Edit form2 = new Lab_EmployeeStatus_Edit();
                form2.Show();
            }

        }

        private void button16_Click(object sender, EventArgs e)
        {
            bool Isopen = false;
            foreach (Form Form6 in Application.OpenForms)
            {
                if (Form6.Name == "Form6")
                {
                    Isopen = true;
                    Form6.BringToFront();
                    break;
                }
            }
            if (Isopen == false)
            {
                Lab_TestToDo form6 = new Lab_TestToDo();
                form6.Show();
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
            foreach (Form Form8 in Application.OpenForms)
            {
                if (Form8.Name == "Form8")
                {
                    Isopen = true;
                    Form8.BringToFront();
                    break;
                }
            }
            if (Isopen == false)
            {
                Lab_UpdateTestDetails form8 = new Lab_UpdateTestDetails();
                form8.Show();
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            bool Isopen = false;
            foreach (Form Form5 in Application.OpenForms)
            {
                if (Form5.Name == "Form5")
                {
                    Isopen = true;
                    Form5.BringToFront();
                    break;
                }
            }
            if (Isopen == false)
            {
                Lab_EmployeeStatus form5 = new Lab_EmployeeStatus();
                form5.Show();
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            bool Isopen = false;
            foreach (Form Form3 in Application.OpenForms)
            {
                if (Form3.Name == "Form3")
                {
                    Isopen = true;
                    Form3.BringToFront();
                    break;
                }
            }
            if (Isopen == false)
            {
                Lab_Inventory form3 = new Lab_Inventory();
                form3.Show();
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            bool Isopen = false;
            foreach (Form Form9 in Application.OpenForms)
            {
                if (Form9.Name == "Form9")
                {
                    Isopen = true;
                    Form9.BringToFront();
                    break;
                }
            }
            if (Isopen == false)
            {
                Lab_CompleteTestDetails form9 = new Lab_CompleteTestDetails();
                form9.Show();
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            bool Isopen = false;
            foreach (Form Form2 in Application.OpenForms)
            {
                if (Form2.Name == "Form2")
                {
                    Isopen = true;
                    Form2.BringToFront();
                    break;
                }
            }
            if (Isopen == false)
            {
                Lab_EmployeeStatus_Edit form2 = new Lab_EmployeeStatus_Edit();
                form2.Show();
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
            foreach (Form Form8 in Application.OpenForms)
            {
                if (Form8.Name == "Form8")
                {
                    Isopen = true;
                    Form8.BringToFront();
                    break;
                }
            }
            if (Isopen == false)
            {
                Lab_UpdateTestDetails form8 = new Lab_UpdateTestDetails();
                form8.Show();
            }
        }

        private void button17_Click_1(object sender, EventArgs e)
        {
            bool Isopen = false;
            foreach (Form Form5 in Application.OpenForms)
            {
                if (Form5.Name == "Form5")
                {
                    Isopen = true;
                    Form5.BringToFront();
                    break;
                }
            }
            if (Isopen == false)
            {
                Lab_EmployeeStatus form5 = new Lab_EmployeeStatus();
                form5.Show();
            }
        }

        private void button16_Click_1(object sender, EventArgs e)
        {
            bool Isopen = false;
            foreach (Form Form6 in Application.OpenForms)
            {
                if (Form6.Name == "Form6")
                {
                    Isopen = true;
                    Form6.BringToFront();
                    break;
                }
            }
            if (Isopen == false)
            {
                Lab_TestToDo form6 = new Lab_TestToDo();
                form6.Show();
            }
        }

        private void button15_Click_1(object sender, EventArgs e)
        {
            bool Isopen = false;
            foreach (Form Form9 in Application.OpenForms)
            {
                if (Form9.Name == "Form9")
                {
                    Isopen = true;
                    Form9.BringToFront();
                    break;
                }
            }
            if (Isopen == false)
            {
                Lab_CompleteTestDetails form9 = new Lab_CompleteTestDetails();
                form9.Show();
            }
        }

        private void button14_Click_1(object sender, EventArgs e)
        {
            bool Isopen = false;
            foreach (Form Form3 in Application.OpenForms)
            {
                if (Form3.Name == "Form3")
                {
                    Isopen = true;
                    Form3.BringToFront();
                    break;
                }
            }
            if (Isopen == false)
            {
                Lab_Inventory form3 = new Lab_Inventory();
                form3.Show();
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
