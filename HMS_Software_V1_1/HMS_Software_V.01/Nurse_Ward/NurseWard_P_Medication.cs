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
    public partial class NurseWard_P_Medication : Form
    {
        string PrescriptionRequetIDs;
        string PatientRID;
        public NurseWard_P_Medication(string NWTP_PrescriptionRequestIDs, string NWTP_Patient_RID)
        {
            InitializeComponent();

            this.PrescriptionRequetIDs = NWTP_PrescriptionRequestIDs;
            this.PatientRID = NWTP_Patient_RID;

            MyLoadData();
        }



        private List<int> PrescriptionRequestID_Lists;
        private void MyLoadData()
        {

            NWPM_DoctorName.Text = PatientRID;

            // Assigne string IDs to int list
            PrescriptionRequestID_Lists = MyConvertStringRequestIDtoInt(PrescriptionRequetIDs);


            DataTable combinedDataTable = new DataTable();

            using (SqlConnection connect = new SqlConnection(MyCommonConnecString.ConnectionString))
            {
                connect.Open();

                // Iterate over each PrescriptionRequest_ID
                foreach (int prescriptionRequestID in PrescriptionRequestID_Lists)
                {
                    // Load Monitor request Details
                    string query = "SELECT Patient_RID, PR_Route, PR_Medicin, PR_Dosage, PR_Frequency, PR_Duration, PR_MedicinID " +
                                   "FROM Prescription_Request " +
                                   "WHERE PrescriptionRequest_ID = @PrescriptionRequest_ID";

                    using (SqlCommand command = new SqlCommand(query, connect))
                    {
                        // Add parameter for PrescriptionRequest_ID
                        command.Parameters.AddWithValue("@PrescriptionRequest_ID", prescriptionRequestID);

                        // Create a DataTable to hold the results for the current ID
                        DataTable dataTable = new DataTable();

                        // Fill the DataTable with the results of the query
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dataTable);
                        }

                        // Merge the current DataTable into the combined DataTable
                        combinedDataTable.Merge(dataTable);
                    }
                }

                // Bind the combined DataTable to the DataGridView
                dataGridView1_medication.DataSource = combinedDataTable;
            }
        }

        private static List<int> MyConvertStringRequestIDtoInt(string value) // Dividi Table Cells that contain mutiple ID in string fomat
        {
            /*Console.WriteLine($"Input string: {value}");*/

            // Split the input string by comma delimiter
            string[] parts = value.Split(',');

            List<int> intList = new List<int>();

            foreach (string part in parts)
            {
                // Skip empty or whitespace-only parts
                if (string.IsNullOrWhiteSpace(part))
                {
                    /*Console.WriteLine($"Skipping empty or whitespace-only part.");*/
                    continue;
                }

                // Attempt to parse the part as an integer
                if (int.TryParse(part.Trim(), out int num))
                {
                    /*Console.WriteLine($"Processing part: {part}. Parsed integer: {num}");*/
                    intList.Add(num);
                }
                else
                {
                    Console.WriteLine($"Skipping non-numeric value: '{part}'");
                }
            }

            /*Console.WriteLine("Conversion completed.");*/
            return intList;
        }

    }
}
