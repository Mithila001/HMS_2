using HMS_Software_V2._DataManage_Classes;
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
using System.Windows.Shapes;

namespace HMS_Software_V2.Doctor_ClincOPD
{
    /// <summary>
    /// Interaction logic for DCO_PatientCheck.xaml
    /// </summary>
    /// 
    public partial class DCO_PatientCheck : Window
    {
      
        public DCO_PatientCheck()
        {
            InitializeComponent();

            doctorName_lbl.Content = SharedData.doctorData.doctorName;
            SharedData.doctorData.doctorName = "Dr. Wakum";

        }
        private void DCO_PatientCheck1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DCO_Dashboard dCO_Dashboard = new DCO_Dashboard();
            dCO_Dashboard.Show();
        }
    }
}
