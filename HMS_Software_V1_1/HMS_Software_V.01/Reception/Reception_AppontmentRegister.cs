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

namespace HMS_Software_V1._01.Reception
{
    public partial class Reception_AppontmentRegister : Form
    {
        SqlConnection connect = new SqlConnection(MyCommonConnecString.ConnectionString);

        /*public int ClinicEventClinicID { get; set; }*/

        int ClinicID;
        int ClinicEventID;
        int UserID;
        public Reception_AppontmentRegister(int ClinicID, int ClinicEvnetID, int UserID)
        {
            InitializeComponent();
            /*this.FormClosed += (s, e) => new Reception_Dashboard().Show();*/
            this.ClinicID = ClinicID;
            this.ClinicEventID = ClinicEvnetID;
            this.UserID = UserID;
        }

        private void RPR_assign_btn_Click(object sender, EventArgs e)
        {
            string registrationNo = "P"+RPR_RegistrationNo_tbx.Text.Trim();

            try
            {
                connect.Open();

                string query1 = "SELECT Patient_ID FROM Patient WHERE P_RegistrationID = @RegistrationID";
                

                int patientID;

                using (SqlCommand cmd = new SqlCommand(query1, connect))
                {
                    cmd.Parameters.AddWithValue("@RegistrationID", registrationNo);
                    object result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        patientID = Convert.ToInt32(result);

                        string query2 = "INSERT INTO Patient_Appointment "
                                          + "(Patient_ID, ClinicEvent_ID) "
                                            + "VALUES (@patientID, @clinicEventID)";

                        using (SqlCommand cmd2 = new SqlCommand(query2, connect))
                        {
                            cmd2.Parameters.AddWithValue("@patientID", patientID);
                            cmd2.Parameters.AddWithValue("@clinicEventID", ClinicEventID);

                            cmd2.ExecuteNonQuery();

                            // Remove a slot from ClinicEvents
                            string query3 = "UPDATE ClinicEvents SET CE_TakenSlots = CE_TakenSlots + 1 " +
                                "WHERE ClinicEvent_ID = @ClinicEvent_ID;";
                            using (SqlCommand cmd3 = new SqlCommand(query3, connect))
                            {
                                cmd3.Parameters.AddWithValue("@ClinicEvent_ID", ClinicEventID);
                                cmd3.ExecuteNonQuery();
                            }


                            MessageBox.Show("Added successfully!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                            /*DialogResult dialogResult = 

                            if (dialogResult == DialogResult.Cancel || dialogResult == DialogResult.None)
                            {
                                 // Close the form when "OK" button is clicked
                            }*/
                        }
                        // Now you have the Patient_ID in the patientID variable
                    }
                    else
                    {
                        MessageBox.Show("Patient ID not found", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message
                   , "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

                Console.WriteLine(ex);
            }
            finally
            {
                connect.Close();
            }

           
        }
    }
}
