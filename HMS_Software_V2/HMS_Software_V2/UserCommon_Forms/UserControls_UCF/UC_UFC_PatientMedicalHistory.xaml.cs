using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace HMS_Software_V2.UserCommon_Forms.UserControls_UCF
{
    /// <summary>
    /// Interaction logic for UC_UFC_PatientMedicalHistory.xaml
    /// </summary>
    public partial class UC_UFC_PatientMedicalHistory : UserControl
    {
        int MedicalEventID;
        public UC_UFC_PatientMedicalHistory(int medicalenvetID)
        {
            InitializeComponent();
            this.MedicalEventID = medicalenvetID;

            

        }

        
    }
}
