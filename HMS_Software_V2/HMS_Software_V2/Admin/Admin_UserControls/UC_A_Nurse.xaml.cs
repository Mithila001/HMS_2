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
    /// Interaction logic for UC_A_Nurse.xaml
    /// </summary>
    public partial class UC_A_Nurse : UserControl
    {
        public UC_A_Nurse()
        {
            InitializeComponent();
            MyGetNurseData();
        }

        private void MyGetNurseData()
        {
            using (SqlConnection connection = new Database_Connector().GetConnection())
            {
                try
                {
                    connection.Open();

                    #region Get Total Nurses Count
                    string query2 = "SELECT COUNT(*) FROM Nurse";
                    using (SqlCommand command2 = new SqlCommand(query2, connection))
                    {

                        int count = (int)command2.ExecuteScalar();
                        totalNurses.Content = count.ToString();
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

        private void NurseAdd_btn_Click(object sender, RoutedEventArgs e)
        {
            Admin_Nurse_Register admin_Nurse_Register = new Admin_Nurse_Register();
            admin_Nurse_Register.ShowDialog();
        }

        private void NurseSearch_btn_Click(object sender, RoutedEventArgs e)
        {
            Admin_Nurse_Search admin_Nurse_Search = new Admin_Nurse_Search();
            admin_Nurse_Search.ShowDialog();
        }
    }
}
