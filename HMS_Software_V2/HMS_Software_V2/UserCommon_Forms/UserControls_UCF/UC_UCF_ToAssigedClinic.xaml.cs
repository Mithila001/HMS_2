using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
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
    /// Interaction logic for UC_UCF_ToAssigedClinic.xaml
    /// </summary>
    public partial class UC_UCF_ToAssigedClinic : UserControl
    {
        public string? UC_UCF_TAC_ClinicName { get; set; }
        public int? UC_UCF_TAC_ClinicID { get; set; }
        private WrapPanel ParentWrapPanel { get; set; }

        public event EventHandler<UC_UCF_ToAssigeClinic_CustomEventArgs>? UC_UCF_TA_AddClinicClicked;
        public UC_UCF_ToAssigedClinic(WrapPanel D_RequestedClinics_WrapP)
        {
            InitializeComponent();
            this.ParentWrapPanel = D_RequestedClinics_WrapP;

            


        }

        private void ToAssigne_AddClinic_btn_Click(object sender, RoutedEventArgs e)
        {
            int clinicID = UC_UCF_TAC_ClinicID ?? 0;
            string clinicName = UC_UCF_TAC_ClinicName ?? "";

            ParentWrapPanel.Children.Remove(this);
            UC_UCF_TA_AddClinicClicked?.Invoke(this, new UC_UCF_ToAssigeClinic_CustomEventArgs(clinicID, clinicName));
        }
    }

    public class UC_UCF_ToAssigeClinic_CustomEventArgs : EventArgs
    {
        public int UC_UCF_TA_ClinicID { get; }
        public string UC_UCF_TA_ClinicName { get; }
        public UC_UCF_ToAssigeClinic_CustomEventArgs(int uC_UCF_TA_ClinicID, string uC_UCF_TA_ClinicName)
        {
            UC_UCF_TA_ClinicID = uC_UCF_TA_ClinicID;
            UC_UCF_TA_ClinicName = uC_UCF_TA_ClinicName;
        }
    }
}
