using HMS_Software_V1._01.Doctor_Ward;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HMS_Software_V1._01.Nurse_Ward.UserControls
{
    public partial class N_ShowAllPatients : UserControl
    {

        // Property to hold the reference to the form
        public Form DashboardFormReference { get; set; }



        public N_ShowAllPatients()
        {
            InitializeComponent();
            MyAttachClickEventHandlers(this);
        }

        public string NSAPUC_P_Name { get; set; }
        public string NSAPUC_P_RID { get; set; }
        public string NSAPUC_P_Age { get; set; }
        public string NSAPUC_P_Gender { get; set; }
        public string NSAPUC_P_Condition { get; set; }
        public int NSAPUC_P_MedicalEventID { get; set; }
        public int NSAPUC_NureseID { get; set; }
        public string NSAPUC_P_Ward { get; set; }
        public int NSAPUC_P_WardNumber {  get; set; }



        private void MyAttachClickEventHandlers(Control parentControl)
        {
            foreach (Control control in parentControl.Controls)
            {
                // Attach event handler for the current control
                control.Click += N_ShowAllPatients_Click;
                control.MouseEnter += panel2_MouseEnter;
                control.MouseLeave += panel2_MouseLeave;


                // If the current control has child controls, attach event handlers to them
                if (control.HasChildren)
                {
                    MyAttachClickEventHandlers(control);
                }
            }
        }

        private void N_ShowAllPatients_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Clicked the user Control");

            NurseWard_TreatePatient nurseWard_TreatePatient = new NurseWard_TreatePatient(NSAPUC_P_Name, NSAPUC_P_RID, NSAPUC_P_Age, NSAPUC_P_Gender,
                NSAPUC_P_Condition, NSAPUC_P_MedicalEventID, NSAPUC_NureseID, NSAPUC_P_Ward, NSAPUC_P_WardNumber);

            /*Console.WriteLine("Patient Name: " + NSAPUC_P_Name);
            Console.WriteLine("Patient RID: " + NSAPUC_P_RID);
            Console.WriteLine("Patient Age: " + NSAPUC_P_Age);
            Console.WriteLine("Patient Gender: " + NSAPUC_P_Gender);
            Console.WriteLine("Patient Condition: " + NSAPUC_P_Condition);
            Console.WriteLine("Medical Event ID: " + NSAPUC_P_MedicalEventID);
            Console.WriteLine("Nurse ID: " + NSAPUC_NureseID);
            Console.WriteLine("Patient Ward: " + NSAPUC_P_Ward);*/

            // Set the reference to the NurseWard_Dashboard form
            nurseWard_TreatePatient.DashboardFormReference = this.DashboardFormReference;

            nurseWard_TreatePatient.Show();

            //Closing UserControl's Parent Form
            NurseWard_Dashboard parentForm = this.ParentForm as NurseWard_Dashboard;
            if (parentForm != null)
            {
                parentForm.Hide();
            }
        }

        private void panel2_MouseEnter(object sender, EventArgs e)
        {
            panel2.BackColor = Color.FromArgb(227, 227, 255);
            panel3.BackColor = Color.FromArgb(227, 227, 255);
            panel4.BackColor = Color.FromArgb(227, 227, 255);
        }

        private void panel2_MouseLeave(object sender, EventArgs e)
        {
            panel2.BackColor = Color.White;
            panel3.BackColor = Color.White;
            panel4.BackColor = Color.White;
        }
    }

}
