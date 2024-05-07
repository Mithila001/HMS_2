using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace HMS_Software_V1._01.Lab_Management
{
    public partial class Lab_DoTest : Form
    {
        SqlConnection connect = new SqlConnection(MyCommonConnecString.ConnectionString);
        public Lab_DoTest()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
        private string GetTestValueFromDatabase()
        {
            string testValue = "";
            try
            {


                using (SqlConnection connect = new SqlConnection(MyCommonConnecString.ConnectionString))
                {
                    connect.Open();

                    int testNo = int.Parse(txtNo.Text);

                    string query = $"SELECT LR_InvestigationName FROM Lab_Request1 WHERE [LabRequest_ID] = {testNo}";
                    SqlCommand command = new SqlCommand(query, connect);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        testValue = reader["LR_InvestigationName"].ToString();
                    }
                    reader.Close();
                }

                
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error1: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine("1:" + ex.Message);
            }
           
            return testValue;




            // Connect to the database and retrieve the value of the test column


        }

        private void DeductQuantityFromInventoryTable()
        {
            try
            {


                // Deduct one from the quantity in another table
                using (SqlConnection connect = new SqlConnection(MyCommonConnecString.ConnectionString))
                {
                    connect.Open();

                    string updateQuery = "UPDATE Inventory SET quantity = quantity - 1 WHERE id = 9";
                    SqlCommand command = new SqlCommand(updateQuery, connect);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error2: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine("3:" + ex.Message);
            }


        }
        private void DeductQuantityFromInventoryTable2(List<int> ids)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(MyCommonConnecString.ConnectionString))
                {
                    connect.Open();

                    string idList = string.Join(",", ids);
                    string updateQuery = $"UPDATE Inventory SET quantity = quantity - 1 WHERE id IN ({idList})";
                    SqlCommand command = new SqlCommand(updateQuery, connect);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error3: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine("3:" + ex.Message);
            }
           

           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(MyCommonConnecString.ConnectionString))
                {
                    connect.Open();

                    DialogResult result = MessageBox.Show("Are you sure you want to add data to finished tests?", "Message", MessageBoxButtons.YesNo);

                    if (result == DialogResult.Yes)
                    {
                        string testValue = GetTestValueFromDatabase();
                        string testvalue1 = GetTestValueFromDatabase();

                        // Check if the test column contains "Urine test"
                        if (testValue == "Urine Test")
                        {
                            // Deduct one from the quantity in another table
                            DeductQuantityFromInventoryTable();

                            // Optionally, display a message to indicate deduction was successful
                            MessageBox.Show("Quantity deducted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else if (testvalue1 == "Complete Blood Count" || testvalue1 == "Blood Chemistry Panel" || testvalue1 == "Blood Sugar Test" || testvalue1 == "Liver Function Test" || testvalue1 == "Renal Function Test" || testvalue1 == "Thyroid Function Test" || testvalue1 == "Lipid Profile" || testvalue1 == "Coagulation Test" || testvalue1 == "Serology Test")
                        {
                            List<int> idsToUpdate = new List<int>() { 1, 2, 5, 4 };
                            DeductQuantityFromInventoryTable2(idsToUpdate);
                            MessageBox.Show("Quantity deducted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("LR_InvestigationName column does not contain those test names.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        try
                        {
                            int testNo = int.Parse(txtNo.Text);
                            Console.WriteLine("Test No: " + testNo);

                            // Fetch data from the source table (Tests) based on the provided test number
                            string selectQuery = $"SELECT LabRequest_ID, PatientMedicalEvent_ID, Patient_ID, Doctor_ID, LR_InvestigationID, LR_SpecimenID, LR_InvestigationName, LR_SpecimenName, LR_LabelNumber FROM Lab_Request1 WHERE LabRequest_ID = @TestNo";
                            SqlCommand selectCommand = new SqlCommand(selectQuery, connect);
                            selectCommand.Parameters.AddWithValue("@TestNo", testNo);

                            SqlDataReader reader = selectCommand.ExecuteReader();

                            if (reader.Read())
                            {
                                // Retrieve data from the source table
                                int No = Convert.ToInt32(reader["LabRequest_ID"]);
                                int medid = Convert.ToInt32(reader["PatientMedicalEvent_ID"]);
                                string patientid = reader["Patient_ID"].ToString();
                                int doctorid = Convert.ToInt32(reader["Doctor_ID"]);
                                int investigationid = Convert.ToInt32(reader["LR_InvestigationID"]);
                                int specimenid = Convert.ToInt32(reader["LR_SpecimenID"]);
                                string investigationname = reader["LR_InvestigationName"].ToString();
                                string specimenname = reader["LR_SpecimenName"].ToString();
                                string labelnumber = reader["LR_LabelNumber"].ToString();

                                reader.Close();


                                /*// Insert the retrieved data into the destination table (Done)
                                string insertQuery = $"INSERT INTO Finished_Tests (LabRequest_ID, PatientMedicalEvent_ID, Patient_ID, Doctor_ID, LR_InvestigationID, LR_SpecimenID, LR_InvestigationName, LR_SpecimenName, LR_LabelNumber) " +
                                                        $"VALUES ({No}, '{medid}', {patientid}, '{doctorid}', '{investigationid}', '{specimenid}', '{investigationname}', '{specimenname}', '{labelnumber}')";

                                
                                
                                SqlCommand insertCommand = new SqlCommand(insertQuery, connect);
                                insertCommand.ExecuteNonQuery();*/

                                string insertQuery = "INSERT INTO Finished_Tests (LabRequest_ID, PatientMedicalEvent_ID, Patient_ID, Doctor_ID, LR_InvestigationID, LR_SpecimenID, LR_InvestigationName, LR_SpecimenName, LR_LabelNumber) " +
                                     "VALUES (@No, @medid, @patientid, @doctorid, @investigationid, @specimenid, @investigationname, @specimenname, @labelnumber)";

                                using (SqlCommand insertCommand = new SqlCommand(insertQuery, connect))
                                {
                                    insertCommand.Parameters.AddWithValue("@No", No);
                                    insertCommand.Parameters.AddWithValue("@medid", medid);
                                    insertCommand.Parameters.AddWithValue("@patientid", patientid);
                                    insertCommand.Parameters.AddWithValue("@doctorid", doctorid);
                                    insertCommand.Parameters.AddWithValue("@investigationid", investigationid);
                                    insertCommand.Parameters.AddWithValue("@specimenid", specimenid);
                                    insertCommand.Parameters.AddWithValue("@investigationname", investigationname);
                                    insertCommand.Parameters.AddWithValue("@specimenname", specimenname);
                                    insertCommand.Parameters.AddWithValue("@labelnumber", labelnumber);

                                    insertCommand.ExecuteNonQuery();
                                }

                                MessageBox.Show("Data added to finished tests successfully");

                               

                                // Delete the added data from the source table (Tests)
                                string deleteQuery = $"DELETE FROM Lab_Request1 WHERE LabRequest_ID = {testNo}";
                                Console.WriteLine("Test No2: " + testNo);

                                SqlCommand deleteCommand = new SqlCommand(deleteQuery, connect);
                                deleteCommand.ExecuteNonQuery();

                                MessageBox.Show("Data deleted from Tests table successfully");
                                
                            }
                            else
                            {
                                MessageBox.Show("No data found for the specified test number");
                            }

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error4: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Console.WriteLine("4:" + ex.Message);
                        }
                    }
                    else
                    {
                       
                        this.Close();
                    }


                }

                    
                

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error5: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine("5:" + ex.Message);
            }

            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(MyCommonConnecString.ConnectionString))
                {
                    connect.Open();

                    int tstNo = int.Parse(txtNo.Text);

                    string query = $"SELECT Patient_ID, Doctor_ID, LR_InvestigationName FROM Lab_Request1 WHERE [LabRequest_ID] = {tstNo}";

                    SqlCommand cmd = new SqlCommand(query, connect);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        string patientid = reader["Patient_ID"].ToString();
                        int doctorid = Convert.ToInt32(reader["Doctor_ID"]);
                        string test = reader["LR_InvestigationName"].ToString();


                        label3.Text = $"Patient Id: {patientid}";
                        label4.Text = $"Doctor Id: {doctorid.ToString()}";
                        label5.Text = $"Test: {test}";

                        Console.WriteLine("patientid+patientid");
                        Console.WriteLine("doctorid" + doctorid);
                        Console.WriteLine("test" + test);
                    }
                    else
                    {
                        MessageBox.Show("No Data found for specific id");
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error6: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine("6:"+ex.Message);
            }
        }
    }
}
