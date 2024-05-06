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

namespace HMS_Software_V1._01.Doctor_Ward.UserControls
{
    public partial class ShowWaitionInPatients : UserControl
    {
        public ShowWaitionInPatients()
        {
            InitializeComponent();
            MyAttachClickEventHandlers(this);
        }


        public string SWP_PatientName { get; set; }
        public string SWP_PatientRID { get; set; }
        public string SWP_PatientCondition { get; set; }
        public int SWP_PatietnVisitCount { get; set; }
        public string SWP_PatientAge {  get; set; }
        public string SWP_PatientGender {  get; set; }

        public string SWP_D_Name { get; set; }
        public string SWP_D_Title { get; set; }
        public string SWP_D_RID { get; set; }

        public int SWP_D_ID {  get; set; }

        public string SWP_WardName { get; set; }


        private void MyAttachClickEventHandlers(Control parentControl)
        {
            foreach (Control control in parentControl.Controls)
            {
                // Attach event handler for the current control
                control.Click += ShowWaitionInPatients_Click;
                control.MouseEnter += panel1_MouseEnter;
                control.MouseLeave += panel1_MouseLeave;


                // If the current control has child controls, attach event handlers to them
                if (control.HasChildren)
                {
                    MyAttachClickEventHandlers(control);
                }
            }
        }

        private void ShowWaitionInPatients_Click(object sender, EventArgs e)
        {
            //Moving to Another form Form
            DoctorWard_ProgressNote doctorWard_ProgressNote = new DoctorWard_ProgressNote(
                SWP_PatientName,
                SWP_PatientRID,
                SWP_PatientCondition,
                SWP_PatietnVisitCount,
                SWP_D_Name,
                SWP_D_Title,
                SWP_D_RID,
                SWP_WardName,
                SWP_D_ID,
                SWP_PatientAge,
                SWP_PatientGender);
            doctorWard_ProgressNote.Show();

            //Closing UserControl's Parent Form
            DoctorWard_Dashboard parentForm = this.ParentForm as DoctorWard_Dashboard;
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
