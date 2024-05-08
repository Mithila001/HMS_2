using HMS_Software_V1._01.Doctor_OPD;
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

namespace HMS_Software_V1._01.Doctor_Check
{
    public partial class Admit_ReferralNote : Form
    {
        SqlConnection connect = new SqlConnection(MyCommonConnecString.ConnectionString);

       /* private string MedicalEventID;
        private string DoctorPosition;
        private int DoctorID;
        private string UnitType;
        private string ExaminationNotes;
        private bool IsUrgent;*/

        private MyDataStoringClass dataImporter;
        public Admit_ReferralNote(MyDataStoringClass dataImporter)
        {
            InitializeComponent();
            this.dataImporter = dataImporter; // Put it befor MyLoadBasicDetails()  --ChatGPT


            DCOMPA_urgent_lbl.Visible = false; // Hide the urgetn Label

            if (dataImporter.Isurgetn)
            {
                DCOMPA_urgent_lbl.Visible = true;
            }
        }

        private void DCOMPA_confrim_btn_Click(object sender, EventArgs e)
        {
            if (DCOMPA_richTextBox.Text == "")
            {
                MessageBox.Show("Please enter the Referral Note", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    using (SqlConnection connect = new SqlConnection(MyCommonConnecString.ConnectionString))
                    {
                        connect.Open();

                        DateTime currentDate = DateTime.Today;
                        DateTime currentTime = DateTime.Now;



                        string query = "INSERT INTO Patient_Admit (P_RegistrationID, P_NameWithInitials, P_Age, P_Gender, P_ReferralNote," +
                            " Doctor_ID, Requested_Time, Requested_Date, Is_Urgent, Is_Admitted, Unit_Type)"
                            + " VALUES (@prID, @pName, @pAge, @pGender, @pReferralNote, @dID, @time, @date, @isUrgent, @isAdmitted, @unitType)";

                        using (SqlCommand cmd = new SqlCommand(query, connect))
                        {
                            cmd.Parameters.AddWithValue("@prID", dataImporter.PatientRID);
                            cmd.Parameters.AddWithValue("@pName", dataImporter.PatientName);
                            cmd.Parameters.AddWithValue("@pAge", dataImporter.PatientAge);
                            cmd.Parameters.AddWithValue("@pGender", dataImporter.PatientGender);
                            cmd.Parameters.AddWithValue("@pReferralNote", DCOMPA_richTextBox.Text);
                            cmd.Parameters.AddWithValue("@dID", dataImporter.DoctorID);
                            cmd.Parameters.AddWithValue("@time", currentTime);
                            cmd.Parameters.AddWithValue("@date", currentDate);
                            cmd.Parameters.AddWithValue("@isUrgent", dataImporter.Isurgetn);
                            cmd.Parameters.AddWithValue("@isAdmitted", false);
                            cmd.Parameters.AddWithValue("@unitType", dataImporter.EventUnitType);


                            int rowsAffected = cmd.ExecuteNonQuery();

                            // Check if the insertion was successful
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Success!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                Console.WriteLine("Data inserted successfully.");


                                // Moving to doctor Dashboard Form
                                DoctorCheck_Dashboard doctorCheck_Dashboard = new DoctorCheck_Dashboard(dataImporter.DoctorID, dataImporter.EventUnitType, dataImporter.SendWardNumber);
                                doctorCheck_Dashboard.Show();
                                this.Close();

                            }
                            else
                            {
                                MessageBox.Show("failed!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Console.WriteLine("No rows were affected. Insertion failed.");
                            }
                        }
                    }



                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connect.Close();
                }
            }
            
        }
    }
}
