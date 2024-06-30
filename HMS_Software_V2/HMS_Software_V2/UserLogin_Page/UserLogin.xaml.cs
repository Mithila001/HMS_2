using HMS_Software_V2._DataManage_Classes;
using HMS_Software_V2.Doctor_ClincOPD;
using System;
using System.Collections.Generic;
using System.Globalization;
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

using Newtonsoft.Json;
using HMS_Software_V2.Doctor_Ward;
using HMS_Software_V2.Nurse_Ward;
using HMS_Software_V2.Admin;
using HMS_Software_V2.Reception;
using HMS_Software_V2.AdmissionOfficer;
using HMS_Software_V2.General_Purpose;
using System.Data.SQLite;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Diagnostics;
using System.ComponentModel.Design;

namespace HMS_Software_V2.UserLogin_Page
{
    /// <summary>
    /// Interaction logic for UserLogin.xaml
    /// </summary>
    public partial class UserLogin : Window
    {

        public UserLogin()
        {
            InitializeComponent();


        }


        

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

      

        string SelectedPosition = "";
        private void userPosition_combobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ComboBox comboBox && comboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                string? selectedContent = selectedItem.Content.ToString() ?? "";
                SelectedPosition = selectedContent;

                switch (selectedContent)
                {
                    case "Doctor":
                        wardNumber_lbl.Visibility = Visibility.Visible;
                        wardNumber_tbx.Visibility = Visibility.Visible;
                        unitTye_combobox.Visibility = Visibility.Visible;
                        unitType_lbl.Visibility = Visibility.Visible;
                        break;
                    case "Nurse":
                        unitTye_combobox.Visibility = Visibility.Collapsed;
                        unitType_lbl.Visibility = Visibility.Collapsed;

                        wardNumber_lbl.Visibility = Visibility.Visible;
                        wardNumber_tbx.Visibility = Visibility.Visible;
                        break;
                    case "Receptionist":

                        wardNumber_lbl.Visibility = Visibility.Collapsed;
                        wardNumber_tbx.Visibility = Visibility.Collapsed;
                        unitTye_combobox.Visibility = Visibility.Collapsed;
                        unitType_lbl.Visibility = Visibility.Collapsed;

                        break;
                    case "Admission Officer":

                        wardNumber_lbl.Visibility = Visibility.Collapsed;
                        wardNumber_tbx.Visibility = Visibility.Collapsed;
                        unitTye_combobox.Visibility = Visibility.Collapsed;
                        unitType_lbl.Visibility = Visibility.Collapsed;

                        break;
                    case "Admin":

                        wardNumber_lbl.Visibility = Visibility.Collapsed;
                        wardNumber_tbx.Visibility = Visibility.Collapsed;
                        unitTye_combobox.Visibility = Visibility.Collapsed;
                        unitType_lbl.Visibility = Visibility.Collapsed;


                        break;
                    default:
                        break;
                }
            }
        }


        string? SelectedUnittype;
        private void logIn_btn_Click(object sender, RoutedEventArgs e)
        {
            //Check if texboxes are empty

            if (InputValidations.MyIsNullorempty(userName_tbx.Text))
            {
                MessageBox.Show("Error: User Name is Empty", "Error", MessageBoxButton.OK, MessageBoxImage.Error); return;
            }
            if (InputValidations.MyIsNullorempty(userPassword_tbx.Text))
            {
                MessageBox.Show("Error: Password is Empty", "Error", MessageBoxButton.OK, MessageBoxImage.Error); return;
            }
           



            int resivedUserID = 0;
            string resivedWardName = "";

            switch (SelectedPosition)
            {
                case "Doctor":

                    #region Chech if a unit type is selected
                    if (unitTye_combobox.SelectedItem is ComboBoxItem selectedItem)
                    {
                        SelectedUnittype = selectedItem.Content.ToString();


                        switch (SelectedUnittype)
                        {
                            case "OPD":
                                
                                break;

                            case "Clinic":

                                break;

                            case "Ward":

                                break;

                            default:

                                MessageBox.Show("Select a Unit Type", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Error:Select a Unit Type", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    #endregion

                    resivedWardName = MyGetWardDetails(); //Check if the ward is valid
                    resivedUserID = MyIsValideUser(); //Check if the user is valid

                    if (resivedUserID != 0 && resivedWardName != "")
                    {
                        MyDoctorLogin(resivedUserID, resivedWardName);
                    }

                    break;

                case "Nurse":

                    resivedWardName = MyGetWardDetails();
                    resivedUserID = MyIsValideUser();
                    if (resivedUserID != 0 && resivedWardName != "")
                    {
                        MyNurseLogin(resivedUserID, resivedWardName);
                    }

                    break;

                case "Receptionist":

                    resivedUserID = MyIsValideUser();
                    if (resivedUserID != 0)
                    {
                        MyReceptionLogin(resivedUserID);
                    }

                    break;

                case "Admission Officer":

                    resivedUserID = MyIsValideUser();
                    if (resivedUserID != 0)
                    {
                        MyAdmissionOfficerLogin(resivedUserID);
                    }
                    break;

                case "Admin":

                    resivedUserID = MyIsValideUser();
                    if (resivedUserID != 0)
                    {
                        MyAdmin(resivedUserID);
                    }
                    break;

                default:
                    MessageBox.Show("Select a Position", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
            }



            

        }

        private string MyGetWardDetails()
        {

            #region Ward Input Validation
            if (InputValidations.MyIsNullorempty(wardNumber_tbx.Text))
            {
                MessageBox.Show("Error: Add the Ward Number", "Error", MessageBoxButton.OK, MessageBoxImage.Error); return "";
            }
            if (!InputValidations.MyIsOnlyNumbers(wardNumber_tbx.Text))
            {
                MessageBox.Show("Error: Add Only numbers to Ward", "Error", MessageBoxButton.OK, MessageBoxImage.Error); return "";
            } 
            #endregion

            using (SQLiteConnection connection = new Database_Connector().GetConnection())
            {
                try
                {
                    connection.Open();


                    string query = "SELECT WardName FROM WardTypes WHERE WardNumber = @WardNumber";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@WardNumber", wardNumber_tbx.Text);



                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read()) // Since we're only expecting one record, we use if instead of while
                            {
                                string warName = reader["WardName"].ToString() ?? "Error";

                                return warName;

                            }
                            else
                            {
                                MessageBox.Show("Error: Ward Details Not Found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                return "";

                            }
                        }
                    }

                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show("Error1: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return "";
                }
                finally
                {
                    connection.Close();
                }
            }

        }
        private int MyIsValideUser()
        {
            int userID = 0;
            string userPosition = "";

            using (SQLiteConnection connection = new Database_Connector().GetConnection())
            {
                try
                {
                    connection.Open();


                    string query = "SELECT UserID, UserPosition FROM UserLogin WHERE UserName = @UserName AND UserPassword = @UserPassword";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@UserName", userName_tbx.Text);
                        cmd.Parameters.AddWithValue("@UserPassword", userPassword_tbx.Text);


                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read()) // Since we're only expecting one record, we use if instead of while
                            {
                                userID = Convert.ToInt32(reader["UserID"]);
                                userPosition = reader["UserPosition"].ToString() ?? "Error";


                                if(userPosition == SelectedPosition)
                                {
                                    return userID;
                                }
                                else
                                {
                                    MessageBox.Show("Error: User Positions doesn't matched", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                    return 0;
                                }

                             
                              

                            }
                            else
                            {
                                MessageBox.Show("Error: Invalid User Name or Password", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                return 0;

                            }
                        }
                    }


                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show("Error2: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return 0;
            
                }
                finally
                {
                    connection.Close();
                }
            }


        }


        private void MyDoctorLogin(int userId, string wardName)
        {

            using (SQLiteConnection connection = new Database_Connector().GetConnection())
            {
                try
                {
                    connection.Open();


                    string query = "SELECT Doctor_ID, D_NameWithInitials, D_Specialty, D_RegistrationID FROM Doctor WHERE Doctor_ID = @DoctorID";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@DoctorID", userId);
                       


                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read()) // Since we're only expecting one record, we use if instead of while
                            {
                                int doctorID = Convert.ToInt32(reader["Doctor_ID"]);
                                string doctorName = reader["D_NameWithInitials"].ToString() ?? "Error";
                                string doctorSpecialty = reader["D_Specialty"].ToString() ?? "Error";
                                string doctorRID = reader["D_RegistrationID"].ToString() ?? "Error";



                                switch (SelectedUnittype)
                                {
                                    case "OPD":

                                        HMS_Software_V2._DataManage_Classes.SharedData.doctorData = new HMS_Software_V2._DataManage_Classes.DoctorData(); // Get a new copy of the template
                                        
                                        SharedData.doctorData.doctorID = doctorID;
                                        SharedData.doctorData.doctorName = doctorName;
                                        SharedData.doctorData.doctorSpecialization = doctorSpecialty;
                                        SharedData.doctorData.doctorLocation = "OPD";

                                        SharedData.doctorData.wardID = int.Parse(wardNumber_tbx.Text);
                                        SharedData.doctorData.wardName = wardName;

                                        DCO_Dashboard dCO_Dashboard1 = new DCO_Dashboard();
                                        dCO_Dashboard1.Show();
                                        this.Close();

                                        

                                        break;

                                    case "Clinic":

                                        HMS_Software_V2._DataManage_Classes.SharedData.doctorData = new HMS_Software_V2._DataManage_Classes.DoctorData(); // Get a new copy of the template

                                        SharedData.doctorData.doctorID = doctorID;
                                        SharedData.doctorData.doctorName = doctorName;
                                        SharedData.doctorData.doctorSpecialization = doctorSpecialty;
                                        SharedData.doctorData.doctorLocation = "Clinic";

                                        SharedData.doctorData.wardID = int.Parse(wardNumber_tbx.Text);
                                        SharedData.doctorData.wardName = wardName;

                                        DCO_Dashboard dCO_Dashboard2 = new DCO_Dashboard();
                                        dCO_Dashboard2.Show();
                                        this.Close();

                                        break;

                                    case "Ward":

                                        HMS_Software_V2._DataManage_Classes.SharedData.Ward_Doctor = new HMS_Software_V2._DataManage_Classes.Ward_Doctor();

                                        SharedData.Ward_Doctor.DoctorID = doctorID;
                                        SharedData.Ward_Doctor.DoctorRID = doctorRID;
                                        SharedData.Ward_Doctor.DoctorName = doctorName;
                                        SharedData.Ward_Doctor.DoctorSpeciality = doctorSpecialty;

                                        SharedData.Ward_Doctor.WardID = int.Parse(wardNumber_tbx.Text);
                                        SharedData.Ward_Doctor.WardName = wardName;

                                        DW_MainPage dW_MainPage = new DW_MainPage();
                                        dW_MainPage.Show();
                                        this.Close();

                                        break;

                                    default:

                                        MessageBox.Show("Select a Unit Type", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                        return;
                                }



                            }
                            else
                            {
                                MessageBox.Show("Error: Doctor Data not found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                return;
                                
                            }
                        }
                    }

                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show("Error3: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private void MyNurseLogin(int userId, string wardName)
        {
            using (SQLiteConnection connection = new Database_Connector().GetConnection())
            {
                try
                {
                    connection.Open();


                    string query = "SELECT Nurce_ID, N_NameWithInitials, N_LicenseNo FROM Nurse WHERE Nurce_ID = @Nurce_ID";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@Nurce_ID", userId);



                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read()) // Since we're only expecting one record, we use if instead of while
                            {
                                int nurseID = Convert.ToInt32(reader["Nurce_ID"]);
                                string nurseName = reader["N_NameWithInitials"].ToString() ?? "Error";
                                string nurseLicenseNo = reader["N_LicenseNo"].ToString() ?? "Error";


                                HMS_Software_V2._DataManage_Classes.SharedData.Ward_Nurse = new HMS_Software_V2._DataManage_Classes.Ward_Nurse(); // Get a new copy of the template
                                SharedData.Ward_Nurse.NurseID = nurseID;
                                SharedData.Ward_Nurse.NurseName = nurseName;
                                SharedData.Ward_Nurse.NureseLicenceNumber = nurseLicenseNo;

                                SharedData.Ward_Nurse.WardName = wardName;
                                SharedData.Ward_Nurse.WardNumber = int.Parse(wardNumber_tbx.Text);

                                NW_Dashboard nW_Dashboard = new NW_Dashboard();
                                nW_Dashboard.Show();
                                this.Close();

                            }
                            else
                            {
                                MessageBox.Show("Error: Nurse Data not found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                return;

                            }
                        }
                    }

                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show("Error4: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    connection.Close();
                }
            }

        }

        private void MyReceptionLogin(int userId)
        {
            using (SQLiteConnection connection = new Database_Connector().GetConnection())
            {
                try
                {
                    connection.Open();


                    string query = "SELECT Reception_ID, R_NameWithInitials FROM Reception WHERE Reception_ID = @Reception_ID";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@Reception_ID", userId);



                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read()) // Since we're only expecting one record, we use if instead of while
                            {
                                int receptionID = Convert.ToInt32(reader["Reception_ID"]);
                                string receptionName = reader["R_NameWithInitials"].ToString() ?? "Error";

                                HMS_Software_V2._DataManage_Classes.SharedData.receptionData = new HMS_Software_V2._DataManage_Classes.ReceptionData(); // Get a new copy of the template

                                SharedData.receptionData.ReceptionID = receptionID;
                                SharedData.receptionData.ReceptionName = receptionName;

                                Reception_Dashboard reception_Dashboard = new Reception_Dashboard();
                                reception_Dashboard.Show();
                                this.Close();


                            }
                            else
                            {
                                MessageBox.Show("Error: Reception Data not found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                return;

                            }
                        }
                    }

                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show("Error5: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    connection.Close();
                }
            }

        }

        private void MyAdmissionOfficerLogin(int userId)
        {
            using (SQLiteConnection connection = new Database_Connector().GetConnection())
            {
                try
                {
                    connection.Open();


                    string query = "SELECT Doctor_ID, D_NameWithInitials FROM Doctor WHERE Doctor_ID = @Doctor_ID";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@Doctor_ID", userId);



                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read()) // Since we're only expecting one record, we use if instead of while
                            {
                                int admissionOfficerId = Convert.ToInt32(reader["Doctor_ID"]);
                                string admissionOfficerName = reader["D_NameWithInitials"].ToString() ?? "Error";

                                HMS_Software_V2._DataManage_Classes.SharedData.admissioOfficer = new HMS_Software_V2._DataManage_Classes.AdmissioOfficer(); // Get a new copy of the template

                                SharedData.admissioOfficer.AdmissionOfficerID = admissionOfficerId;
                                SharedData.admissioOfficer.AdmissionOfficerName = admissionOfficerName;

                                Debug.WriteLine("Admission Officer ID: " + SharedData.admissioOfficer.AdmissionOfficerID);
                                Debug.WriteLine("Admission Officer Name: " + SharedData.admissioOfficer.AdmissionOfficerName);

                                AO_Dashboard aO_Dashboard = new AO_Dashboard();
                                aO_Dashboard.Show();
                                this.Close();


                            }
                            else
                            {
                                MessageBox.Show("Error: Reception Data not found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                return;

                            }
                        }
                    }

                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show("Error6: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private void MyAdmin(int userId)
        {
            using (SQLiteConnection connection = new Database_Connector().GetConnection())
            {
                try
                {
                    connection.Open();


                    string query = "SELECT Admin_ID, AdminName FROM Admin WHERE Admin_ID = @Admin_ID";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@Admin_ID", userId);



                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read()) // Since we're only expecting one record, we use if instead of while
                            {
                                int adminID = Convert.ToInt32(reader["Admin_ID"]);
                                string adminName = reader["AdminName"].ToString() ?? "Error";

                                HMS_Software_V2._DataManage_Classes.SharedData.adminData = new HMS_Software_V2._DataManage_Classes.AdminData(); // Get a new copy of the template

                                SharedData.adminData.AdminID = adminID;
                                SharedData.adminData.AdminName = adminName;

                                Admin_Dashboard admin_Dashboard = new Admin_Dashboard();
                                admin_Dashboard.Show();
                                this.Close();


                            }
                            else
                            {
                                MessageBox.Show("Error: Admin Data not found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                return;

                            }
                        }
                    }

                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show("Error7: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    connection.Close();
                }
            }

        }

        private void fromClose_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
