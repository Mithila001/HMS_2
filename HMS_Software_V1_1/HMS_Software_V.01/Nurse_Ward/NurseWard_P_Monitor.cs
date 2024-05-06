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

namespace HMS_Software_V1._01.Nurse_Ward
{
    public partial class NurseWard_P_Monitor : Form
    {
        private string Patient_RID;
        private int P_MonitorRequest_ID;
        public NurseWard_P_Monitor(string NWTP_Patient_RID, int NWTTP_MonitorRequest_ID)
        {
            this.Patient_RID = NWTP_Patient_RID;
            this.P_MonitorRequest_ID = NWTTP_MonitorRequest_ID;

            InitializeComponent();

           

            MyLoadData();
        }


        string MonitroInfor;
        private void MyLoadData()
        {
            NWPM_P_RID.Text = Patient_RID;


            try
            {
                using (SqlConnection connect = new SqlConnection(MyCommonConnecString.ConnectionString))
                {
                    connect.Open();
                    Console.WriteLine("Monitor Request ID: "+ P_MonitorRequest_ID);
                    // Load Monitor request Details ------------------------------------------------------------------------
                    string query = "SELECT MR_Info" +
                                " FROM Monitor_Request WHERE MonitorRequest_ID = @MonitorRequest_ID";
                    using (SqlCommand command = new SqlCommand(query, connect))
                    {
                        Console.WriteLine("MonitorRequest_ID: " + P_MonitorRequest_ID);
                        command.Parameters.AddWithValue("@MonitorRequest_ID", P_MonitorRequest_ID);

                        try
                        {
                            SqlDataReader reader = command.ExecuteReader();

                            // Check if any rows were returned
                            if (reader.Read())
                            {
                                MonitroInfor = reader["MR_Info"].ToString();
                                NWPM_ShowDetails.Text = MonitroInfor;

                                Console.WriteLine("MonitroInfor Found:" + MonitroInfor);
                            }
                            else
                            {
                                MessageBox.Show("No matching N_NameWithInitials record found.");
                            }
                            reader.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error:1 " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Console.WriteLine("Error1:" + ex);
                        }
                    }
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show("Error:2 " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine("Error2:" + ex);
            }
        }

        private void NWPM_Save_btn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(NWPM_richTextBox1.Text))
            {
                using (SqlConnection connect = new SqlConnection(MyCommonConnecString.ConnectionString))
                {
                    connect.Open();

                    // Update Monitor reuslts ------------------------------------------------------------------------
                    string query = "UPDATE Monitor_Request " +
                               "SET MR_Results = @MR_Results " +
                               "WHERE MonitorRequest_ID = @MonitorRequest_ID";
                    using (SqlCommand command = new SqlCommand(query, connect))
                    {
                        command.Parameters.AddWithValue("@MR_Results", NWPM_richTextBox1.Text); // Specify the new results to update
                        command.Parameters.AddWithValue("@MonitorRequest_ID", P_MonitorRequest_ID); // Specify the monitor request ID

                        try
                        {
                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Success! " , "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                Console.WriteLine("Record updated successfully.");
                            }
                            else
                            {
                                Console.WriteLine("No matching record found.");
                                MessageBox.Show("No matching record found" , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error:3 " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Console.WriteLine("Error3:" + ex);
                        }
                    }
                }

            }
            else
            {
                DialogResult result = MessageBox.Show("Monitor Details are Empty! Want To Continue?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    this.Close();

                }
                else
                {
                    return;
                }
            }
        }
    }
}
