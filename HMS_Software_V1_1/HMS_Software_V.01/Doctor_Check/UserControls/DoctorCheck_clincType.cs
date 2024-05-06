using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HMS_Software_V1._01.Doctor_OPD.UserControls
{
    public partial class DoctorCheck_clincType : UserControl
    {
        public DoctorCheck_clincType()
        {
            InitializeComponent();
            /*AttachClickEventHandlers(this);*/
        }

        /*private void AttachClickEventHandlers(Control parentControl)
        {
            foreach (Control control in parentControl.Controls)
            {
                // Attach event handler for the current control
                control.Click += Control_Click;
                *//*control.MouseEnter += materialCard1_MouseEnter;
                control.MouseLeave += materialCard1_MouseLeave;*//*

               
                if (control.HasChildren)  // If the current control has child controls, attach event handlers to them
                {
                    AttachClickEventHandlers(control);
                }
            }
        }

        public event EventHandler<int> ClinicClicked;
        private void Control_Click(object sender, EventArgs e)
        {
            *//*int clinicID = (int)this.Tag;

            ClinicClicked?.Invoke(this, clinicID);
*//*
            
            OnClick(e); // Trigger the UserControl click event
        }*/
    }
}
