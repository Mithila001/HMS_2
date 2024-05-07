using HMS_Software_V1._01.Common_UseForms.UserControls;
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

using Newtonsoft.Json;
using HMS_Software_V1._01.Doctor_OPD; //To convert data to json fomat
using HMS_Software_V1._01.Common_UseForms.OOP;



namespace HMS_Software_V1._01.Common_UseForms
{
    public partial class Common_MakeLabRequest : Form
    {
        public Form DoctorPatientCheckFromReferece { get; set; } // Referecne from the Outpations

        public Form DoctorPatientCheckWardFromReferece { get; set; } // Reference From the Ward

        SqlConnection connect = new SqlConnection(MyCommonConnecString.ConnectionString);

        string FC_patientID_str;
        int FC_userID;
        string FC_doctorPosition;
        string FC_doctorName;
        string FC_unittype;



        private ForCommonLabRequests doctorDataSendToLabRequest;
        public Common_MakeLabRequest(ForCommonLabRequests doctorDataSendToLabRequest)
        {
            this.doctorDataSendToLabRequest = doctorDataSendToLabRequest;
            /*this.dataImporter = dataImporter; // Put it befor MyLoadBasicDetails()  --ChatGPT
            this.dataImporter2 = dataImporter2;*/
            InitializeComponent();
            MyLoadBasicDetails();



            // In order form to close, DoctorCheck_PatientCheck required values. So we get original values from DoctorCheck_PatientCheck public class 
            FC_patientID_str = doctorDataSendToLabRequest.PatientRID;
            FC_userID = doctorDataSendToLabRequest.DoctorID;
            FC_doctorPosition = doctorDataSendToLabRequest.DoctorPosition;
            FC_doctorName = doctorDataSendToLabRequest.DoctorName;
            FC_unittype = doctorDataSendToLabRequest.EventUnitType;
            /*this.FormClosed += (s, e) => new DoctorCheck_PatientCheck(FC_patientID_str, FC_userID, FC_doctorPosition, FC_doctorName, FC_unittype).Show();*/
            /*this.FormClosed += (s, e) => this.Show()*/
        }

        /*private void MyloadResult()
        {
            try
            {
                connect.Open();

                string query = "SELECT LabInvestigationType_ID, LIT_Name FROM Lab_InvestigationType";

                SqlCommand sqlCommand = connect.CreateCommand();
                sqlCommand.CommandText = query;

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = sqlCommand;

                DataTable dt = new DataTable();
                adapter.Fill(dt);
                labInvestigationSearch_dataGrV.DataSource = dt;

                Console.WriteLine("Lab Investigation Types:");
                foreach (DataRow row in dt.Rows)
                {
                    Console.WriteLine($"ID: {row["LabInvestigationType_ID"]}, Name: {row["LIT_Name"]}");
                }



            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

                Console.WriteLine(ex);

            }
            finally
            {
                connect.Close();
            }
        }*/

        private void MyLoadBasicDetails() //Load basic UI info
        {
            CMLR_doctorName.Text = doctorDataSendToLabRequest.DoctorName;
            CMLR_DocPosition.Text = doctorDataSendToLabRequest.DoctorPosition;
            CMLR_DocID.Text = doctorDataSendToLabRequest.DoctorID.ToString();
            CMLR_Pati_Name.Text = doctorDataSendToLabRequest.PatientName;
            CMLR_Pati_Age.Text = doctorDataSendToLabRequest.PatientAge;
            CMLR_Pati_Gender.Text = doctorDataSendToLabRequest.PatientGender;

            // Adding date and time
            DateTime currentDate = DateTime.Today;
            string formattedDate = currentDate.ToString("d MMMM yyyy");

            DateTime currentTime = DateTime.Now;
            string timeString = currentTime.ToString("hh:mm tt");

            CMLR_date.Text = formattedDate;
            CMLR_time.Text = timeString;
        }


        // ============================================= Lab Request Type Search =======================================================
        private int investigationID = 0;
        private void labInvestigationSearch_dataGrV_CellClick(object sender, DataGridViewCellEventArgs e) //Search bar click result
        {
            
            DataGridViewRow row = this.labInvestigationSearch_dataGrV.Rows[e.RowIndex];
            string investigationIDstring  = row.Cells["Investigation_ID"].Value.ToString();
            LabInvestigations_tbx.Text = row.Cells["InvestigationName"].Value.ToString();
            labInvestigationSearch_dataGrV.Height = 0;

            /*int investigationID = 0;*/ //Convert investigationId to int

            if (int.TryParse(investigationIDstring, out investigationID))
            {
                // Parsing successful, investigationID now holds the integer value
                Console.WriteLine("Investigation ID:::::: " + investigationID);
            }
            else
            {
                // Parsing failed, handle the error (e.g., display an error message)
                Console.WriteLine("Failed to parse Investigation ID  InvestigationName");
            }



        }

        private void labInvestigationSearch_tbx_TextChanged_1(object sender, EventArgs e) //Search bar Show result
        {
            try
            {
                connect.Open();

                if (LabInvestigations_tbx.TextLength >= 1)
                {
                    string query = "SELECT LabInvestigationType_ID, LIT_Name FROM Lab_InvestigationType ";
                    query += "WHERE LabInvestigationType_ID LIKE @LabInvestigationType_ID OR LIT_Name LIKE @LIT_Name";

                    SqlCommand sqlCommand = connect.CreateCommand();
                    sqlCommand.CommandText = query;

                    // Warnig - This section need user error handling, currenly user can add empty vlaues to to request table
                    sqlCommand.CommandText = query;
                    sqlCommand.Parameters.AddWithValue("@LabInvestigationType_ID", LabInvestigations_tbx.Text + "%");
                    sqlCommand.Parameters.AddWithValue("@LIT_Name", LabInvestigations_tbx.Text + "%");

                    using (SqlDataAdapter adapter = new SqlDataAdapter())
                    {
                        adapter.SelectCommand = sqlCommand;

                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        if (dt != null && dt.Rows.Count > 0)
                        {
                            labInvestigationSearch_dataGrV.DataSource = dt;
                            labInvestigationSearch_dataGrV.Height = labInvestigationSearch_dataGrV.Rows.Count * 30;

                        }
                        else
                        {
                            labInvestigationSearch_dataGrV.Height = 0;
                        }

                    }
                    

                }
                else if (LabInvestigations_tbx.TextLength <= 0)
                {
                    labInvestigationSearch_dataGrV.Height = 0;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

                Console.WriteLine(ex);

            }
            finally
            {
                connect.Close();
            }

        }


        // ============================================= Specimen Search ===============================================================
        private int specimenID = 0;
        private void specimenSearch_dataGrV_CellClick(object sender, DataGridViewCellEventArgs e) //Search bar click result
        {
            DataGridViewRow row = this.specimenSearch_dataGrV.Rows[e.RowIndex];
            string speciementIDstring = row.Cells["labSpecimentTypeID"].Value.ToString();
            Speciment_tbx.Text = row.Cells["specimenName"].Value.ToString();
            specimenSearch_dataGrV.Height = 0;

            

            if (int.TryParse(speciementIDstring, out specimenID))
            {
                // Parsing successful, investigationID now holds the integer value
                Console.WriteLine("Investigation ID: " + specimenID);
            }
            else
            {
                // Parsing failed, handle the error (e.g., display an error message)
                Console.WriteLine("Failed to parse Investigation ID  InvestigationName");
            }

        }

        private void Speciment_tbx_TextChanged(object sender, EventArgs e) //Search bar Show result
        {
            try
            {
                connect.Open();

                if (Speciment_tbx.TextLength >= 1)
                {
                    string query = "SELECT Lab_SpeciementType_ID, LST_Name FROM Lab_SpeciementType ";
                    query += "WHERE Lab_SpeciementType_ID LIKE @LabSpeciementTypeID OR LST_Name LIKE @LSTName";

                    SqlCommand sqlCommand = connect.CreateCommand();
                    sqlCommand.CommandText = query;

                    sqlCommand.CommandText = query;
                    sqlCommand.Parameters.AddWithValue("@LabSpeciementTypeID", Speciment_tbx.Text + "%");
                    sqlCommand.Parameters.AddWithValue("@LSTName", Speciment_tbx.Text + "%");

                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = sqlCommand;

                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        specimenSearch_dataGrV.DataSource = dt;
                        specimenSearch_dataGrV.Height = specimenSearch_dataGrV.Rows.Count * 30;

                    }
                    else
                    {
                        specimenSearch_dataGrV.Height = 0;
                    }

                }
                else if (Speciment_tbx.TextLength <= 0)
                {
                    specimenSearch_dataGrV.Height = 0;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

                Console.WriteLine(ex);

            }
            finally
            {
                connect.Close();
            }

        }


        // ============================================= Add Button Clicked ================================================================
        private void CMLR_add_btn_Click(object sender, EventArgs e)
        {
            int getSpecimenID = specimenID;
            int getinvestigationID = investigationID;
            string labInvestigations = LabInvestigations_tbx.Text;
            string specimenName = Speciment_tbx.Text;

            if (!string.IsNullOrEmpty(labInvestigations) && !string.IsNullOrEmpty(specimenName))
            {
                MyLoadUserData(getSpecimenID, getinvestigationID, labInvestigations, specimenName);
                

                Speciment_tbx.Text = "";
                LabInvestigations_tbx.Text = "";
            }
            else
            {
                // LabInvestigations_tbx.Text does not exist or is empty
                MessageBox.Show("Fill blanks");
            }

        }


        // ============================================= Button Clikced --> FlowLayoutPanel =================================================
        private void MyLoadUserData(int getSpecimenID, int getinvestigationID, string labInvestigations, string specimenName)
        {
            // Generate a number for the LabRequest
            DateTime now = DateTime.Now;
            string generatedSpceimentNumber = specimenName.Substring(0, Math.Min(specimenName.Length, 3)).ToUpper() + " " + now.Month + " " + now.Day + " " + now.ToString("HHmm");

            AddLabRequest addLabRequest = new AddLabRequest();
            addLabRequest.investigationType_lbl.Text = labInvestigations.ToString();
            addLabRequest.specimenName_lbl.Text = specimenName.ToString();
            addLabRequest.requestNumber_lbl.Text = generatedSpceimentNumber.ToString();

            //Store and send data to the UserControl
            addLabRequest.LabInvestigationsID = getinvestigationID;
            addLabRequest.SpecimenNameID = getSpecimenID;

            flowLayoutPanel_CMLR_selected.SizeChanged += (sender, e) =>
            {
                addLabRequest.Width = flowLayoutPanel_CMLR_selected.ClientSize.Width - addLabRequest.Margin.Horizontal;
            };
            addLabRequest.Width = flowLayoutPanel_CMLR_selected.ClientSize.Width - addLabRequest.Margin.Horizontal; //Intentional

            flowLayoutPanel_CMLR_selected.Controls.Add(addLabRequest);

            
        }


        // ============================================= Button Clikced Save Data ======================================================

        /*public class LabRequestData // To store the all the labRequest that collected and list below
        {
            public int InvestigationID { get; set; }
            public int SpecimenID { get; set; }
            public string GeneratedNumber { get; set; }
        }*/
        
        private void CMLR_save_btn_Click(object sender, EventArgs e)
        {
            /*List<LabRequestData> dataList = new List<LabRequestData>();*/

            foreach (Control control in flowLayoutPanel_CMLR_selected.Controls)
            {
                if (control is AddLabRequest addLabRequest)
                {                  

                    string investigationName = addLabRequest.investigationType_lbl.Text;
                    string scpecimenName = addLabRequest.specimenName_lbl.Text;
                    string generatedNumber = addLabRequest.requestNumber_lbl.Text;

                    int getInvstigationID = addLabRequest.LabInvestigationsID;
                    int getSpecimenID = addLabRequest.SpecimenNameID;
                    
                    // Create an instance of LabRequestData and add it to the list
                   /* dataList.Add(new LabRequestData
                    {
                        InvestigationID = getInvstigationID,
                        SpecimenID = getSpecimenID,
                        GeneratedNumber = generatedNumber
                    });*/

                    MyInsertLabDataIntoDatabase(getInvstigationID, getSpecimenID, generatedNumber, investigationName, scpecimenName);
                }

            }
            // Serialize the list of LabRequestData objects into a JSON string
            /*string labRequestJsonData = JsonConvert.SerializeObject(dataList);*/
            // Insert data into the database
            /*MyInsertDataIntoDatabase(dataList);*/
            MySuccessfulyDataInsertMessage();

        }


        // ============================================= Button Clikced -> Save Data and Send to databse =======================================
        private int getLabID;
        
        private string labRequestIDlistStrig; // To Store all the lab request IDs in thie event
        private void MyInsertLabDataIntoDatabase(int getInvstigationID, int getSpecimenID, string generatedNumber, string investigationName, string scpecimenName)
        {
            /*Console.WriteLine("JSon string ============="+ labRequestJsonData);*/
            try
            {
                
                connect.Open();

                // Create a Lab Request record for  Lab_Request1 table ------------------------
                string query1 = "INSERT INTO Lab_Request1 (LR_InvestigationID, LR_SpecimenID, PatientMedicalEvent_ID, LR_InvestigationName," +
                    " LR_SpecimenName, LR_LabelNumber, Patient_ID, Doctor_ID)" +

                    " VALUES (@investigationID,@specimenID, @patientMedicalEventID, @investigationName," +
                    " @specimenName, @lableNumber, @patientID, @doctorID)";
                using (SqlCommand cmd = new SqlCommand(query1, connect))
                {
                    cmd.Parameters.AddWithValue("@investigationID", getInvstigationID);
                    cmd.Parameters.AddWithValue("@specimenID", getSpecimenID);
                    cmd.Parameters.AddWithValue("@patientMedicalEventID", doctorDataSendToLabRequest.PatientMedicalEventID);
                    cmd.Parameters.AddWithValue("@investigationName", investigationName);
                    cmd.Parameters.AddWithValue("@specimenName", scpecimenName);
                    cmd.Parameters.AddWithValue("@lableNumber", generatedNumber);
                    cmd.Parameters.AddWithValue("@patientID", doctorDataSendToLabRequest.PatientRID);
                    cmd.Parameters.AddWithValue("@doctorID", doctorDataSendToLabRequest.DoctorID);

                    cmd.ExecuteNonQuery();
                }




                // Create a Lab Request record in Lab_Request table
                string query = "INSERT INTO Lab_Request (LR_InvestigationID, LR_SpecimenID, PatientMedicalEvent_ID, LR_InvestigationName,"+
                    " LR_SpecimenName, LR_LabelNumber, Patient_ID, Doctor_ID)" +

                    " VALUES (@investigationID,@specimenID, @patientMedicalEventID, @investigationName,"+
                    " @specimenName, @lableNumber, @patientID, @doctorID)";
                using (SqlCommand cmd = new SqlCommand(query, connect))
                {
                    cmd.Parameters.AddWithValue("@investigationID", getInvstigationID);
                    cmd.Parameters.AddWithValue("@specimenID", getSpecimenID);
                    cmd.Parameters.AddWithValue("@patientMedicalEventID", doctorDataSendToLabRequest.PatientMedicalEventID);
                    cmd.Parameters.AddWithValue("@investigationName", investigationName);
                    cmd.Parameters.AddWithValue("@specimenName", scpecimenName);
                    cmd.Parameters.AddWithValue("@lableNumber", generatedNumber);
                    cmd.Parameters.AddWithValue("@patientID", doctorDataSendToLabRequest.PatientRID);
                    cmd.Parameters.AddWithValue("@doctorID", doctorDataSendToLabRequest.DoctorID);

                    cmd.ExecuteNonQuery();
                }

                // Using PatientMedicalEventID to find the lab record that now created and get the current LabRequest_ID
                string query2 = "SELECT LabRequest_ID FROM Lab_Request WHERE PatientMedicalEvent_ID = @pmeID";

                labRequestIDlistStrig = "";

                using (SqlCommand getIdCommand = new SqlCommand(query2, connect))
                {
                    getIdCommand.Parameters.AddWithValue("@pmeID", doctorDataSendToLabRequest.PatientMedicalEventID);

                    using (SqlDataReader reader = getIdCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            try
                            {
                                getLabID = Convert.ToInt32(reader["LabRequest_ID"]);
                                Console.WriteLine($"Lab ID: {labRequestIDlistStrig}");
                                labRequestIDlistStrig += getLabID.ToString() + ", "; // Store or process labRequestID as need
                                
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("Failed to parse Lab ID.");
                            }
                        }
                    }
                }

                if(getLabID != 0)
                {
                    
                    // Assigne LabRequest_ID to PatientMedical_Event table record 
                    string updateQuery = "UPDATE PatientMedical_Event SET LabRequest_ID = @labRequestID WHERE PatientMedicalEvent_ID = @pmeID";
                    using (SqlCommand updateCommand = new SqlCommand(updateQuery, connect))
                    {
                        Console.WriteLine("---------> LAB Patietn Medical Event ID:"+ doctorDataSendToLabRequest.PatientMedicalEventID);
                       

                        updateCommand.Parameters.AddWithValue("@labRequestID", labRequestIDlistStrig);
                        updateCommand.Parameters.AddWithValue("@pmeID", doctorDataSendToLabRequest.PatientMedicalEventID);

                        int rowsAffected = updateCommand.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("LabRequest_ID updated successfully.");
                            /*MessageBox.Show("Success", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);*/
                        }
                        else
                        {
                            Console.WriteLine("Failed to update LabRequest_ID.");
                            MessageBox.Show("Failed to update LabRequest_ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                }
                else
                {
                    Console.WriteLine("Error: Didn't Assigne LabRequest_ID to PatientMedical_Event table record --> getLabID  = 0 ");
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

        // After saving the request, show the message if the Labrequest ID insert successful.
        private void MySuccessfulyDataInsertMessage()
        {
            if (!string.IsNullOrEmpty(labRequestIDlistStrig))
            {
                MessageBox.Show("Successfully Added " + labRequestIDlistStrig.Substring(0, labRequestIDlistStrig.Length - 2)
                    + " Request IDs", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Lab Request list is empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Common_MakeLabRequest_FormClosed(object sender, FormClosedEventArgs e)
        {
            

            Console.WriteLine("Common_MakePrescription_FormClosed: FC_unittype =  " + FC_unittype);

            if (FC_unittype == "OPD" || FC_unittype == "Clinic")
            {
                // Show Patient Check Form
                DoctorPatientCheckFromReferece.Show();
            }
            else
            {
                DoctorPatientCheckWardFromReferece.Show();
            }

            // Currently this Closing method is properly working only if there are 2 forms accessing this form.
        }
    }
}
