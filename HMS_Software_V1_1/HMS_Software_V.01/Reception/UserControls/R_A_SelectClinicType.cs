using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HMS_Software_V1._01.Reception.UserControls
{
    public partial class R_A_SelectClinicType : UserControl
    {
        public R_A_SelectClinicType()
        {
            InitializeComponent();
            AttachClickEventHandlers(this);
            
        }
        public int RASCT_ClincID { get; set; }

        private void AttachClickEventHandlers(Control parentControl)
        {
            foreach (Control control in parentControl.Controls)
            {
                // Attach event handler for the current control
                control.Click += R_A_SelectClinicType_Click;
                control.MouseEnter += materialCard1_MouseEnter;
                control.MouseLeave += materialCard1_MouseLeave;

                // If the current control has child controls, attach event handlers to them
                if (control.HasChildren)
                {
                    AttachClickEventHandlers(control);
                }
            }
        }


        public event EventHandler<int> ClinicClicked;

        private void materialCard1_MouseEnter(object sender, EventArgs e)
        {
            panel1.BackColor = Color.FromArgb(223, 223, 223); // Change background color
           
        }

        private void materialCard1_MouseLeave(object sender, EventArgs e)
        {
            panel1.BackColor = Color.FromArgb(255, 255, 255); // Restore background color
            
        }


        





        public event EventHandler UserControlClicked;
        private void R_A_SelectClinicType_Click(object sender, EventArgs e)
        {
            // Get the parent form
            /*Reception_Appointment parentForm = this.Parent as Reception_Appointment;

            // Check if the parent form is not null
            if (parentForm != null)
            {
                // Call the method
                parentForm.MyRemoveRightFLowLPControls();
            }*/
            Console.WriteLine("ClinicID from User Control: " + RASCT_ClincID);
            UserControlClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}
