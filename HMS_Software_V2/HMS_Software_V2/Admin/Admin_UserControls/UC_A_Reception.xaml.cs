using HMS_Software_V2.General_Purpose;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
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
    /// Interaction logic for UC_A_Reception.xaml
    /// </summary>
    public partial class UC_A_Reception : UserControl
    {
        public UC_A_Reception()
        {
            InitializeComponent();
            MyGetReceptionData();
        }

        private void MyGetReceptionData()
        {
            using (SQLiteConnection connection = new Database_Connector().GetConnection())
            {
                try
                {
                    connection.Open();

                    #region Get Total Reception Count
                    string query2 = "SELECT COUNT(*) FROM Reception";
                    using (SQLiteCommand command2 = new SQLiteCommand(query2, connection))
                    {

                        int count = Convert.ToInt32(command2.ExecuteScalar());
                        totalReceptions.Content = count.ToString();
                    }

                    #endregion




                }
                catch (SQLiteException ex)
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

        private void ReceptionAdd_btn_Click(object sender, RoutedEventArgs e)
        {
            Admin_Reception_Register admin_Reception_Register = new Admin_Reception_Register();
            admin_Reception_Register.ShowDialog();
        }

        private void Receptionseach_btn_Click(object sender, RoutedEventArgs e)
        {
            Admin_Reception_Search admin_Reception_Search = new Admin_Reception_Search();
            admin_Reception_Search.ShowDialog();
        }
    }
}
