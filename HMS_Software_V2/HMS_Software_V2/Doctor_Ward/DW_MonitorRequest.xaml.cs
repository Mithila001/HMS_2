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

namespace HMS_Software_V2.Doctor_Ward
{
    /// <summary>
    /// Interaction logic for DW_MonitorRequest.xaml
    /// </summary>
    public partial class DW_MonitorRequest : Window
    {
        DW_ProgressNote parentWard_FormReferce;
        public DW_MonitorRequest(DW_ProgressNote dW_ProgressNote)
        {
            InitializeComponent();
            parentWard_FormReferce = dW_ProgressNote;
        }


        private void Confirm_btn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(monitorRequest_tbx.Text))
            {
                MessageBox.Show("Please Enter the Monitor Request", "Monitor Request", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            SharedData.medicalEvent.MonitorRequest = monitorRequest_tbx.Text;
            this.Close();
        }
        private void DW_MonitorRequest1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            parentWard_FormReferce.Show();
        }
    }
}
