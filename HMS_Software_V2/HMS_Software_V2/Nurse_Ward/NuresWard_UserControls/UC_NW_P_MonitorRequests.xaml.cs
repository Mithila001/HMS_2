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

namespace HMS_Software_V2.Nurse_Ward.NuresWard_UserControls
{
    /// <summary>
    /// Interaction logic for UC_NW_P_MonitorRequests.xaml
    /// </summary>
    public partial class UC_NW_P_MonitorRequests : UserControl
    {
        NW_PatientTreat parentFormReferece;
        public UC_NW_P_MonitorRequests(NW_PatientTreat nW_PatientTreat)
        {
            InitializeComponent();
            parentFormReferece = nW_PatientTreat;
        }

        public bool IsCompleted { get; set; }

        private void ViewInfo_btn_Click(object sender, RoutedEventArgs e)
        {
            NW_MonitorPatient nW_MonitorPatient = new NW_MonitorPatient(parentFormReferece);
            nW_MonitorPatient.Show();
            parentFormReferece.Hide();
        }
    }
}
