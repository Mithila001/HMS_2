using HMS_Software_V1._01;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HMS_Software_V._01.Doctor_Check
{
    public partial class DoctorCheck_ViewHistory : Form
    {
        #region Connection string
        SqlConnection connect = new SqlConnection(MyCommonConnecString.ConnectionString);
        #endregion

        string PatientID;        
        string MedicalRecord;
        public DoctorCheck_ViewHistory(string patientID)
        {
            InitializeComponent();
            
            try
            {
                
                    this.PatientID = patientID;
                    Console.WriteLine("Patient ID: " + PatientID);
                    connect.Open();

                    string query = "SELECT PatientExaminatioNote FROM PatientMedical_Event WHERE PatientRegistration_ID = @patientRID";

                    using (SqlCommand command = new SqlCommand(query, connect))
                    {
                        command.Parameters.AddWithValue("@patientRID", PatientID);

                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {                            
                            while (reader.Read())
                            {
                                
                                MedicalRecord = reader.GetString(0);


                                richTextBox1.AppendText(MedicalRecord + "\n\n");
                            }
                        }
                        else
                        {
                            MessageBox.Show("No Medical Event Found", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                                   

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connect.Close();
            }

            
        }
    }
}
