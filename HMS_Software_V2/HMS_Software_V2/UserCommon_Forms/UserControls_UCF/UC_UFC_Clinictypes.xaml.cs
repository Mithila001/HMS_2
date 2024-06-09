using HMS_Software_V2.Reception.R_UserControls;
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

namespace HMS_Software_V2.UserCommon_Forms.UserControls_UCF
{
    /// <summary>
    /// Interaction logic for UC_UFC_Clinictypes.xaml
    /// </summary>
    public partial class UC_UFC_Clinictypes : UserControl
    {
        public int? ClinicTypeID { get; set; }
        public string? ClinicType_Name { get; set; }
        private WrapPanel ParentWrapPanel { get; set; }

        public UC_UFC_Clinictypes(WrapPanel parentWrapPanel)
        {
            InitializeComponent();
            this.ParentWrapPanel = parentWrapPanel;
        }

        public event EventHandler<UC_UCF_MyClinicType_CustomEventArgs>? UC_UCF_MyClinicTypeClicked;

        private void AddClinic_btn_Click(object sender, RoutedEventArgs e)
        {
            //For null safety
            int clinicTypeID = ClinicTypeID ?? 0; 
            string clinicName = ClinicType_Name ?? ""; 

            UC_UCF_MyClinicTypeClicked?.Invoke(this, new UC_UCF_MyClinicType_CustomEventArgs(clinicTypeID, clinicName));

            ParentWrapPanel.Children.Remove(this);

        }
    }
    public class UC_UCF_MyClinicType_CustomEventArgs : EventArgs
    {
        public int ClinicTypeID_evnet { get; }
        public string ClinicType_Name_evnet { get; }

        public UC_UCF_MyClinicType_CustomEventArgs(int clinicTypeID, string clinicName)
        {
            ClinicTypeID_evnet = clinicTypeID;
            ClinicType_Name_evnet = clinicName;
        }
    }
}
