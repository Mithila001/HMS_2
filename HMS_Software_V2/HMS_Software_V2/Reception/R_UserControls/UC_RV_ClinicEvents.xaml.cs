using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HMS_Software_V2.Reception.R_UserControls
{
    /// <summary>
    /// Interaction logic for UC_RV_ClinicEvents.xaml
    /// </summary>
    public partial class UC_RV_ClinicEvents : UserControl
    {
        // Define a delegate for the event
        public delegate void MyAssignClinicEventHandler(object sender, EventArgs e, int clinicEventID, int clinicTypeID);

        // Define the event using the delegate
        public event MyAssignClinicEventHandler? AssignClinicClicked;

        public UC_RV_ClinicEvents()
        {
            InitializeComponent();
        }

        

        public int ClinicEventID { get; set;  }
        public int ClinicTypeID { get; set; }
        private void AssignClinic_btn_Click(object sender, RoutedEventArgs e)
        {
            // Raise the event
            AssignClinicClicked?.Invoke(sender, e, ClinicEventID, ClinicTypeID);
        }
    }
}
