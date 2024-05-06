using HMS_Software_V1._01.Common_UseForms.UserControls;
using HMS_Software_V1._01.Doctor_OPD.UserControls;
using HMS_Software_V1._01.Reception;
using HMS_Software_V1._01.Reception.UserControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static HMS_Software_V1._01.Doctor_OPD.DoctorCheck_PatientCheck;

namespace HMS_Software_V1._01.Doctor_OPD
{
    public partial class DoctorCheck_AddClinic : Form
    {
        public Form DoctorCkeckFromReferece { get; set; } // To get Patient Check from referecein for proper form close




        SqlConnection connect = new SqlConnection(MyCommonConnecString.ConnectionString);

        private MyDataStoringClass dataImporter;
        public DoctorCheck_AddClinic(MyDataStoringClass dataImporter)
        {
            InitializeComponent();
            this.dataImporter = dataImporter;
            string FC_patientID_str = dataImporter.PatientRID;
            int FC_userID = dataImporter.DoctorID;
            string FC_doctorPosition = dataImporter.DoctorPosition;
            string FC_doctorName = dataImporter.DoctorName;
            string FC_unittype = dataImporter.EventUnitType;
            /*this.FormClosed += (s, e) => new DoctorCheck_PatientCheck(FC_patientID_str, FC_userID, FC_doctorPosition, FC_doctorName, FC_unittype).Show();*/

            LoadUserData();

            this.SizeChanged += MyRe_Appointments_SizeChanged; //To fix clinicTypeAvailableDates Usercontrol scaling issues from small window to Full screen

            /*DoctorOPD_clincType doctorOPD_ClincType = new DoctorOPD_clincType();
            doctorOPD_ClincType.ClinicClicked += (sender, clinicId) =>
            {
                _clinicId = clinicId;

            };*/

            // In order form to close, DoctorCheck_PatientCheck required values. So we get original values from DoctorCheck_PatientCheck public class 
           ;

        }
        private void MyRe_Appointments_SizeChanged(object sender, EventArgs e)
        {
            foreach (Control control in flowLP_DOPDA_left.Controls)
            {
                if (control is DoctorCheck_clincType doctorOPD_ClincType)
                {
                    doctorOPD_ClincType.Width = flowLP_DOPDA_left.ClientSize.Width - doctorOPD_ClincType.Margin.Horizontal;
                }
            }
        }

        //================================= Show Clinic Types UserControls =================================
        private void LoadUserData()
        {
            try
            {
                connect.Open();
                DateTime today = DateTime.Today;
                
                string query = "SELECT CT_Name, CT_WardNo, ClincType_ID "
                    + "FROM ClincType";

                SqlCommand command = new SqlCommand(query, connect);
                using(SqlDataReader reader1 = command.ExecuteReader())
                {
                    // Loop through the records retrieved from the database
                    while (reader1.Read())
                    {
                        string getclincName = reader1["CT_Name"].ToString();
                        string GclinicID = reader1["ClincType_ID"].ToString();

                        int getclinicID = 0;

                        if (int.TryParse(GclinicID, out getclinicID))  // Convert to int
                        {

                            Console.WriteLine("Investigation ID: " + getclinicID);
                        }

                        DoctorCheck_clincType doctorOPD_ClincType = new DoctorCheck_clincType();

                        doctorOPD_ClincType.DOPDA_ClincType_lbl.Text = reader1["CT_Name"].ToString();



                        // Set the ID as the Tag property
                        /* doctorOPD_ClincType.Tag = reader1["ClincType_ID"];*/

                        doctorOPD_ClincType.DOPDA_add_btn.Click += (sender, clinicID) =>
                        {
                            LoadUserData2(sender, getclinicID, getclincName);  // Call LoadUserData2() method when ClinicClicked event is raised

                            /* flowLP_DOPDA_left.Controls.Remove(doctorOPD_ClincType);*/
                            //Auto remove when add button clicked. Curently off becouse cant risk messing up the flow if we try to add that usercontrol again from LoadUserData2(); method
                        };


                        flowLP_DOPDA_left.SizeChanged += (sender, e) =>
                        {
                            // Adjust the width of the user control to match the width of the parent container
                            doctorOPD_ClincType.Width = flowLP_DOPDA_left.ClientSize.Width - doctorOPD_ClincType.Margin.Horizontal;
                        };

                        flowLP_DOPDA_left.Controls.Add(doctorOPD_ClincType);

                    }
                    reader1.Close();

                }

               
               

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:1 " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine("Error1:" + ex);
            }
            finally
            {
                connect.Close();
            }
        }

        private int ClinicID;

        private void LoadUserData2(object sendersenderObj, int clinicID, string clincName)
        {
            ClinicID = clinicID;


            DoctorCheck_showAddedClinics doctorOPD_ShowAddedClinics = new DoctorCheck_showAddedClinics();
            doctorOPD_ShowAddedClinics.DOPDA_ClincType_lbl.Text = clincName;


            doctorOPD_ShowAddedClinics.DOPDA_delete_btn.Click += (sender, clinicID2 ) => //no idea about this  clinicID2 maybe replace it with e
            {
                flowLP_DOPDA_Right.Controls.Remove(doctorOPD_ShowAddedClinics);

            };

            flowLP_DOPDA_Right.SizeChanged += (sender, e) =>
            {
                doctorOPD_ShowAddedClinics.Width = flowLP_DOPDA_Right.ClientSize.Width - doctorOPD_ShowAddedClinics.Margin.Horizontal;
            };
            doctorOPD_ShowAddedClinics.Width = flowLP_DOPDA_Right.ClientSize.Width - doctorOPD_ShowAddedClinics.Margin.Horizontal; //Intentional

            flowLP_DOPDA_Right.Controls.Add(doctorOPD_ShowAddedClinics);

        }


        // ============================================= Button Clikced Save Data =======================================

        private string storeAllClincTypeIDs;
        // ************************** need to change values 
        private void DOPDA_save_btn_Click(object sender, EventArgs e)
        {
            storeAllClincTypeIDs = "";

            foreach (Control control in flowLP_DOPDA_Right.Controls)
            {
                if (control is DoctorCheck_showAddedClinics doctorCheck_ShowAddedClinics)
                {
                    // Get data from the AddLabRequest user control  
                    string ClinicName = doctorCheck_ShowAddedClinics.DOPDA_ClincType_lbl.Text;

                    int getClinicID = ClinicID;
                    storeAllClincTypeIDs += getClinicID.ToString() + ", ";

                    // Insert data into the database
                    
                }
            }
            InsertDataIntoDatabase();
        }

        // ============================================= Button Clikced Save Data and Send to databse =======================================
        private void InsertDataIntoDatabase()
        {
            try
            {
                Console.WriteLine("All the clinic IDs:" + storeAllClincTypeIDs);

                connect.Open();
                string query = "UPDATE PatientMedical_Event SET PatientAppointment_ID = @appointmentID WHERE PatientMedicalEvent_ID = @pmeID";

                using(SqlCommand cmd = new SqlCommand(query, connect))
                {
                    cmd.Parameters.AddWithValue("@pmeID", dataImporter.PatientMedicalEventID);
                    cmd.Parameters.AddWithValue("@appointmentID", storeAllClincTypeIDs);
             
                    int rowsAffected = cmd.ExecuteNonQuery(); // Execute the UPDATE query

                    if (rowsAffected > 0) // Check if any rows were affected
                    {
                        
                        Console.WriteLine("Appointment ID stored: " + dataImporter.PatientMedicalEventID);
                        MessageBox.Show("Success", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        // No rows updated (record not found)
                        MessageBox.Show("No record found to store Appointment ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Console.WriteLine("No record found.");
                    }
                }

                


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error3: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            finally
            {
                connect.Close();
            }
        }

        private void DoctorCheck_AddClinic_FormClosed(object sender, FormClosedEventArgs e)
        {
            DoctorCkeckFromReferece.Show();
        }
    }
}
