using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HMS_Software_V1._01.Admin.Admin_UserConotrols
{
    public partial class Admin_Appointment : UserControl
    {
        public Admin_Appointment()
        {
            InitializeComponent();
        }

        //Reciving Data from the form
        public void MySendDataToUserControl(string adminName, string date, string time)
        {
            Ad_appointmetn_Date.Text = date;
            Ad_appointmetn_time.Text = time;
        }
    }
}
