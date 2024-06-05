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
    /// Interaction logic for DCO_Dashboard.xaml
    /// </summary>
    public partial class DCO_Dashboard : Window
    {
        public DCO_Dashboard()
        {
            InitializeComponent();

        }



        // Button Section ------------------------------------------------------------------------------------
        private void confirm_btn_Click(object sender, RoutedEventArgs e)
        {
            string patientRID = patientRID_tbx.Text.ToString();

            if (General_Purpose.InputValidations.MyIsNullorempty(patientRID))
            {
                MessageBox.Show("Enter Patient RID", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if(!General_Purpose.InputValidations.MyIsOnlyNumbers(patientRID))
            {
                MessageBox.Show("Enter only numbers", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void reset_btn_Click(object sender, RoutedEventArgs e)
        {
            patientRID_tbx.Text = "";
        }
    }
}
