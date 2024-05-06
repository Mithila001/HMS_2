using HMS_Software_V1._01.Admition_Officer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HMS_Software_V1._01.Admission_Officer.UserControls
{
    public partial class AdmitRequest : UserControl
    {
        public AdmitRequest()
        {
            InitializeComponent();
            MyAttachClickEventHandlers(this);
        }

        private void MyAttachClickEventHandlers(Control parentControl)
        {
            foreach (Control control in parentControl.Controls)
            {
                // Attach event handler for the current control
                control.Click += AdmitRequest_Click;
                control.MouseEnter += panel1_MouseEnter;
                control.MouseLeave += panel1_MouseLeave;
                

                // If the current control has child controls, attach event handlers to them
                if (control.HasChildren)
                {
                    MyAttachClickEventHandlers(control);
                }
            }
        }

        public int AdmitRequestID;

        public string AO_Name {  get; set; }
        public string AO_Position{get; set; }
        public string AO_Specialty {  get; set; }
        public string AO_RegistrationID {  get; set; }
        public int AO_ID { get; set; }
        private void AdmitRequest_Click(object sender, EventArgs e)
        {
            Console.WriteLine("------ Clicked the user control. Sending request ID: "+ AdmitRequestID);
            Console.WriteLine("------ Clicked the user control. Sending Admission Officer ID: " + AO_ID);

            //Moving to View Request Form
            AdmissionOfficer_ViewRequest admissionOfficer_ViewRequest = new AdmissionOfficer_ViewRequest(AdmitRequestID,
                AO_Name,
                AO_Position,
                AO_Specialty,
                AO_RegistrationID,
                AO_ID);

            admissionOfficer_ViewRequest.Show();

            //Closing UserControl's Parent Form
            AdmissionOfficer_Dashboard parentForm = this.ParentForm as AdmissionOfficer_Dashboard;
            if (parentForm != null)
            {
                parentForm.Hide();
            }

        }

        private void panel1_MouseEnter(object sender, EventArgs e)
        {
            panel1.BackColor = Color.FromArgb(227, 227, 255);
            panel2.BackColor = Color.FromArgb(227, 227, 255);
            panel3.BackColor = Color.FromArgb(227, 227, 255);
            panel4.BackColor = Color.FromArgb(227, 227, 255);
        }

        private void panel1_MouseLeave(object sender, EventArgs e)
        {
            panel1.BackColor = Color.White;
            panel2.BackColor = Color.White;
            panel3.BackColor = Color.White;
            panel4.BackColor = Color.White;
        }

         
    }
}
