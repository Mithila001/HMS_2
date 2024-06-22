using HMS_Software_V2.General_Purpose;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace HMS_Software_V2.Admin.Admin_UserControls
{
    /// <summary>
    /// Interaction logic for UC_A_Doctors.xaml
    /// </summary>
    public partial class UC_A_Doctors : UserControl
    {
        public UC_A_Doctors()
        {
            InitializeComponent();
            MyGetDoctorData();
        }

        private void MyGetDoctorData()
        {
            using (SqlConnection connection = new Database_Connector().GetConnection())
            {
                try
                {
                    connection.Open();

                    #region Get Total Doctors Count
                    string query2 = "SELECT COUNT(*) FROM Doctor";
                    using (SqlCommand command2 = new SqlCommand(query2, connection))
                    {

                        int count = (int)command2.ExecuteScalar();
                        totalDoctors.Content = count.ToString();
                    }

                    #endregion


                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                finally
                {
                    connection.Close();
                }


            }
        }

        private void DoctorAdd_btn_Click(object sender, RoutedEventArgs e)
        {
            Admin_Doctor_Register admin_Doctor_Register = new Admin_Doctor_Register();
            admin_Doctor_Register.ShowDialog();
        }

        private void DoctorSearch_btn_Click(object sender, RoutedEventArgs e)
        {
            Admin_Nurse_Search admin_Nurse_Search = new Admin_Nurse_Search();
            admin_Nurse_Search.ShowDialog();
        }
    }
}
