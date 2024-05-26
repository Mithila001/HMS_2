using HMS_Software_V1._01.Common_UseForms.UserControls;
using HMS_Software_V1._01.Nurse_Ward.UserControls;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Newtonsoft.Json;
using System.IO;


namespace HMS_Software_V1._01.Nurse_Ward
{
    public partial class NurseWard_TreatePatient : Form
    {
        
        

        public Form DashboardFormReference { get; set; } //Getting Dashboard From refferece from the user control


        string PatientName;
        string PatientRID;
        string PatientAge;
        string PatientGender;
        string PatientCondition;
        int PatientMEID;
        int NurseID;
        string PatientWard;
        int WardNumber;

        public NurseWard_TreatePatient(string NSAPUC_P_Name, string NSAPUC_P_RID, string NSAPUC_P_Age, string NSAPUC_P_Gender,
                string NSAPUC_P_Condition, int NSAPUC_P_MedicalEventID, int NSAPUC_NureseID, string NSAPUC_P_Ward, int NSAPUC_P_WardNumber)
        {
            InitializeComponent();

            this.PatientName = NSAPUC_P_Name;
            this.PatientRID = NSAPUC_P_RID;
            this.PatientAge = NSAPUC_P_Age;
            this.PatientGender = NSAPUC_P_Gender;
            this.PatientCondition = NSAPUC_P_Condition;
            this.PatientMEID = NSAPUC_P_MedicalEventID;
            this.NurseID = NSAPUC_NureseID;
            this.PatientWard = NSAPUC_P_Ward;
            this.WardNumber = NSAPUC_P_WardNumber;

            Console.WriteLine("\n\n-------------------- NurseWard_TreatePatient ---------------------");
            Console.WriteLine("Patient Name: " + PatientName);
            Console.WriteLine("Patient RID: " + PatientRID);
            Console.WriteLine("Patient Age: " + PatientAge);
            Console.WriteLine("Patient Gender: " + PatientGender);
            Console.WriteLine("Patient Condition: " + PatientCondition);
            Console.WriteLine("Patient Medical Event ID: " + PatientMEID);
            Console.WriteLine("Nurse ID: " + NurseID);
            Console.WriteLine("Patient Ward: " + PatientWard);
            Console.WriteLine("Ward Number: " + WardNumber);
            Console.WriteLine("----------------------------------------------------------------------- ");


            NWTP_Ward.Text = "Number: " +WardNumber.ToString();


            LoadBasicData();

            MyReadJSON();

            MyDataLoeadingController();
            /*LoadData();*/

            /*MyReadJSON();
            MyAssignedToJSON();*/
        }

        private bool isMatchFoundFromJSON = false;
        private void MyReadJSON()
        {
            string json = File.ReadAllText(@"E:\Programming\Github\HMS_Mithila\HMS_Software_V1_1\HMS_Software_V.01\Nurse_Ward\JSON file\NWPC_userControlDetails.json");

            List<UserControlDetails> userControlDetailsList = JsonConvert.DeserializeObject<List<UserControlDetails>>(json);
            Console.WriteLine("Step 1");
            if (userControlDetailsList != null && userControlDetailsList.Any())
            {
                foreach (var userDetails in userControlDetailsList)
                {

                    Console.WriteLine("Step 2");
                    // Check if the NurseID matches the desired NurseID
                    if (userDetails.JSON_NurseID == NurseID && userDetails.JSON_PatientID == PatientRID)
                    {
                       
                        isMatchFoundFromJSON = true;


                        Console.WriteLine("Step 3");
                        // Assign values to the user controls
                        NWTP_PatientMedicalEvents nWTP_PatientMedicalEvents2 = new NWTP_PatientMedicalEvents();

                        nWTP_PatientMedicalEvents2.NWTPUC_checkBox.Checked = userDetails.JSON_IsCompleted;
                        nWTP_PatientMedicalEvents2.NWTPUC_RequestType.Text = userDetails.JSON_RequestType;
                        nWTP_PatientMedicalEvents2.NWTPUC_RequestDetaills.Text = userDetails.JSON_RequestDetails;

                        if(nWTP_PatientMedicalEvents2.NWTPUC_RequestType.Text == "Lab Request")
                        {
                            nWTP_PatientMedicalEvents2.NWTPUC_View_btn.Visible = false;
                        }

                      
                        P_MedicalEvents_FlowLP.Controls.Add(nWTP_PatientMedicalEvents2);

 

                        // Find the corresponding user control by iterating through controls
                        foreach (Control control in P_MedicalEvents_FlowLP.Controls)
                        {
                            if (control is NWTP_PatientMedicalEvents nWTP_PatientMedicalEvents)
                            {
                                Console.WriteLine("Found NWTP_PatientMedicalEvents control.");

                                // Check if the NurseID matches the desired NurseID
                                if (nWTP_PatientMedicalEvents.NWTP_NuresID == userDetails.JSON_NurseID)
                                {


                                    break; // Exit the loop after assigning values to the matched control
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                isMatchFoundFromJSON = false;
            }


            
        }

        private void MyDataLoeadingController()
        {
            // Check if at least one match is found
            if (isMatchFoundFromJSON)
            {
                Console.WriteLine("At least one match is found.");
                
            }
            else
            {
                Console.WriteLine("No match is found.");
                LoadData();
            }
        }
       
        private void MyAssignedToJSON()
        {
            // Define a list to hold the details of multiple user controls
            List<UserControlDetails> NWPC_userControlDetails = new List<UserControlDetails>();

           
            foreach (Control control in P_MedicalEvents_FlowLP.Controls)
            {
                if (control is NWTP_PatientMedicalEvents nWTP_PatientMedicalEvents)
                {
                    // Extract the details from the user control
                    string UC_JSON__RequestType = nWTP_PatientMedicalEvents.NWTPUC_RequestType.Text;
                    bool UC_JSON_IsCompleted = nWTP_PatientMedicalEvents.NWTPUC_checkBox.Checked;
                    string UC_JSON_RequestDetails = nWTP_PatientMedicalEvents.NWTPUC_RequestDetaills.Text;

                    if (nWTP_PatientMedicalEvents.NWTPUC_RequestType.Text == "Lab Request")
                    {
                        nWTP_PatientMedicalEvents.NWTPUC_View_btn.Visible = false;
                    }

                    // Create an instance of UserControlDetails and add it to the list
                    var userDetails = new UserControlDetails
                    {
                        JSON_PatientID = PatientRID,
                        JSON_NurseID = NurseID,
                        JSON_RequestType = UC_JSON__RequestType,
                        JSON_IsCompleted = UC_JSON_IsCompleted,
                        JSON_RequestDetails = UC_JSON_RequestDetails,
                        JSON_PrescriptionID = PrescriptionRequestIDs,
                        JSON_LabRequesID_List = LabRequestID_Lists
                    };
                    NWPC_userControlDetails.Add(userDetails);
                    MessageBox.Show("Saved", "Infromation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            // Serialize the list to JSON format
            string json = JsonConvert.SerializeObject(NWPC_userControlDetails);

            // Write the JSON data to a file
            File.WriteAllText(@"E:\Programming\Github\HMS_Mithila\HMS_Software_V1_1\HMS_Software_V.01\Nurse_Ward\JSON file\NWPC_userControlDetails.json", json);

        }



        // Define a class to hold the details of each user control
        public class UserControlDetails //For JSON 
        {
            public string JSON_RequestType { get; set; }
            public int  JSON_NurseID { get; set; }
            public bool JSON_IsCompleted { get; set; }
            public string JSON_PatientID { get; set; }
            public string JSON_RequestDetails {  get; set; }

            public string JSON_PrescriptionID {  get; set; }
            public List<int> JSON_LabRequesID_List { get; set; }
        }



        string LabRequestIDs;
        string PrescriptionRequestIDs;
        int PatientMonitorRequestID;
        
        private void LoadBasicData()
        {
            NWTP_P_Name.Text = PatientName;
            NWTP_P_Age.Text = PatientAge;
            NWTP_P_Gender.Text = PatientGender;
            NWTP_P_Cause.Text = PatientCondition;
            NWTP_P_RID.Text = PatientRID;

            DateTime time1 = DateTime.Now;
            DateTime today1 = DateTime.Today;

            NWTP_date.Text = today1.ToString("yyyy-MM-dd");
            NWTP_time.Text = time1.ToString("hh:mm:ss tt");

            NWTP_N_ID.Text = NurseID.ToString();

        }


        private List<int> LabRequestID_Lists;
        private void LoadData()
        {
            try
            {

                using (SqlConnection connect = new SqlConnection(MyCommonConnecString.ConnectionString))
                {
                    connect.Open();

                    Console.WriteLine("Step 4");
                    //Load Today Admitted Patient Medical Events  ------------------------------------------------------------------------
                    string query1 = "SELECT LabRequest_ID, PrescriptionRequest_ID, PatinetMonitortRequest FROM" +
                       " PatientMedical_Event" +
                       " WHERE PatientMedicalEvent_ID = @PatientMedicalEvent_ID";


                    DateTime today = DateTime.Today;

                    using (SqlCommand cmd = new SqlCommand(query1, connect))
                    {
                        cmd.Parameters.AddWithValue("@PatientMedicalEvent_ID", PatientMEID);
                        Console.WriteLine("Giving @PatientMedicalEvent_ID: " + PatientMEID);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            Console.WriteLine("Step 5");
                            while (reader.Read())
                            {
                                Console.WriteLine("Step 6");
                                /* Console.WriteLine("Admitted Patient Medical Events Recived. ");*/

                                try
                                {
                                    LabRequestIDs = reader["LabRequest_ID"] != DBNull.Value ? reader["LabRequest_ID"].ToString() : "-";
                                    PrescriptionRequestIDs = reader["PrescriptionRequest_ID"] != DBNull.Value ? reader["PrescriptionRequest_ID"].ToString() : "-";

                                    Console.WriteLine("Got LabRequest_ID: "+ LabRequestIDs);
                                    Console.WriteLine("Got PrescriptionRequestIDs: " + PrescriptionRequestIDs);

                                    // Becouse if ID is empt, there going to be an error
                                    PatientMonitorRequestID = !reader.IsDBNull(reader.GetOrdinal("PatinetMonitortRequest")) ? Convert.ToInt32(reader["PatinetMonitortRequest"]) : 0;
                                    Console.WriteLine("Got PatinetMonitortRequest: " + PatientMonitorRequestID);

                                }
                                catch (FormatException)
                                {
                                    MessageBox.Show("PatientMedicalEvent_ID is not matched", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }


                                NWTP_PatientMedicalEvents nWTP_PatientMedicalEvents = new NWTP_PatientMedicalEvents();

                                //Since there are calling a single Patient Medical Event Record, there will be no Loops.

                                nWTP_PatientMedicalEvents.NWTP_NuresID = NurseID;
                                nWTP_PatientMedicalEvents.NWTP_Patient_RID = PatientRID;
                                nWTP_PatientMedicalEvents.NWTP_Patient_MEID = PatientMEID;
                                nWTP_PatientMedicalEvents.NWTTP_MonitorRequest_ID = PatientMonitorRequestID;
                                nWTP_PatientMedicalEvents.NWTP_PrescriptionRequestIDs = PrescriptionRequestIDs;



                                // Referecne 
                                /*nWTP_PatientMedicalEvents.NurseWard_TreatePatientFormReference = this;*/


                                //--------------------------------------------------------------------------------------------------------------------------------
                                //Check If there are Prescription request table is available
                                if (!string.IsNullOrEmpty(PrescriptionRequestIDs) && PrescriptionRequestIDs == "-")
                                {
                                    /*Console.WriteLine("Prescription Request IDs is available");*/

                                    nWTP_PatientMedicalEvents.NWTPUC_RequestType.Text = "Medication";
                                    nWTP_PatientMedicalEvents.NWTPUC_RequestDetaills.Text = "IDs: "+PrescriptionRequestIDs;

                                    nWTP_PatientMedicalEvents.NWTP_RequestType = "Medication";

                                    P_MedicalEvents_FlowLP.Controls.Add(nWTP_PatientMedicalEvents);

                                }

                                // Check if the Lab Request available
                                if (!string.IsNullOrEmpty(LabRequestIDs))
                                {
                               

                                    // Assigne string IDs to int list
                                    LabRequestID_Lists = MyConvertStringRequestIDtoInt(LabRequestIDs);

                                  

                                    // Generate UserControl for that list
                                    MyGenerateLabTaks(LabRequestID_Lists);
                                }

                                if(PatientMonitorRequestID != 0)
                                {
                                    nWTP_PatientMedicalEvents.NWTPUC_RequestType.Text = "Monitor";
                                    nWTP_PatientMedicalEvents.NWTPUC_RequestDetaills.Text = "IDs : "+ PatientMonitorRequestID;
                                    P_MedicalEvents_FlowLP.Controls.Add(nWTP_PatientMedicalEvents);

                                    nWTP_PatientMedicalEvents.NWTP_RequestType = "Monitor";
                                }


                                P_MedicalEvents_FlowLP.SizeChanged += (sender, e) =>
                                {
                                    // Adjust the width of the user control to match the width of the parent container
                                    nWTP_PatientMedicalEvents.Width = P_MedicalEvents_FlowLP.ClientSize.Width - nWTP_PatientMedicalEvents.Margin.Horizontal;
                                };

                            }
                            reader.Close();
                        }
                    }

                }

                MyAssignedToJSON(); // To store current Generated data to JSON file

            }


            catch (Exception ex)
            {
                MessageBox.Show("Error:1 " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine("Error1:" + ex);

            }
        }

        // Method to convert a string to a list of integers
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

        
        static bool MyIfTaskIsCompleted(string input) //To Check tasks are completed
        {
          
            string[] parts = input.Split(',');
           
            // Check if the last part is "off"
            string lastPart = parts[parts.Length - 1].Trim(); // Trim to remove any leading/trailing whitespace

            if (lastPart.Equals("yes", StringComparison.OrdinalIgnoreCase))
            {
                // "yes" is present at the end 
                return true;
            }
            else
            {
                // "off" is not present at the end
                return false;
            }
        }


        // Generate Lab Task User Controls for each Lab Request
        private void MyGenerateLabTaks(List<int> LabRequestID_Lists)
        {
            NWTP_PatientMedicalEvents nWTP_PatientMedicalEvents = new NWTP_PatientMedicalEvents();

            try
            {
                using (SqlConnection connect = new SqlConnection(MyCommonConnecString.ConnectionString))
                {
                    connect.Open();

                    //Load the Lab Request Table  ------------------------------------------------------------------------
                    string query1 = "SELECT LR_InvestigationName, LR_SpecimenName, LR_LabelNumber FROM" +
                       " Lab_Request" +
                       " WHERE LabRequest_ID = @labRequest_ID";

                    foreach (int id in LabRequestID_Lists) // For each Lab Request IDs, generate a user control
                    {
                        /*Console.WriteLine("MyGenerateLabTaks --> LabID: " + id);*/

                        using (SqlCommand cmd = new SqlCommand(query1, connect))
                        {
                            cmd.Parameters.AddWithValue("@labRequest_ID", id);

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                try
                                {
                                    if (reader.Read()) // Check if there is data available
                                    {
                                        /*Console.WriteLine("MyGenerateLabTaks --> Lab Record Found for LabID: " + id);*/

                                        string investigationName = reader["LR_InvestigationName"].ToString();
                                        string specimentName = reader["LR_SpecimenName"].ToString();
                                        string labelNumber = reader["LR_LabelNumber"].ToString();

                                        /*Console.WriteLine($"Investigation Name: {investigationName}");
                                        Console.WriteLine($"Specimen Name: {specimentName}");
                                        Console.WriteLine($"Label Number: {labelNumber}");*/


                                        // Create a string to store investigationName, specimentName, and labelNumber
                                        string DisplayLabInfo = "Investigation: " + investigationName + "\n" +
                                            "Specimen: " + specimentName + "\n" + labelNumber;

                                        /*Console.WriteLine("Create a string to store investigationName, specimentName, and labelNumber: " + DisplayLabInfo);*/

                                        // Add values to the user control
                                        nWTP_PatientMedicalEvents.NWTPUC_RequestType.Text = "Lab Request";
                                        nWTP_PatientMedicalEvents.NWTPUC_RequestDetaills.Text = DisplayLabInfo;

                                        nWTP_PatientMedicalEvents.NWTP_RequestType = "Lab Request";

                                        //---------------------------------------------------------------------------------------------------------------------------------------

                                        // Change lable Coordinate
                                        int changeY = 8;

                                        // Get the current location of the label
                                        Point currentLocation =  nWTP_PatientMedicalEvents.NWTPUC_RequestDetaills.Location;
                                        int newY = currentLocation.Y - changeY;

                                        // Set the new location with both X and Y coordinates
                                        nWTP_PatientMedicalEvents.NWTPUC_RequestDetaills.Location = new Point(currentLocation.X, newY);

                                        //---------------------------------------------------------------------------------------------------------------------------------------

                                        //Check if the task is alrady completed.
                                        bool IsComplteted = MyIfTaskIsCompleted(LabRequestIDs);
                                        /*Console.WriteLine("Is last task completed? " + IsComplteted);*/

                                        if (IsComplteted)
                                        {
                                            nWTP_PatientMedicalEvents.NWTPUC_checkBox.Checked = true;
                                            P_MedicalEvents_FlowLP.Controls.Add(nWTP_PatientMedicalEvents); // Generate A Single User Control
                                        }
                                        else
                                        {
                                            nWTP_PatientMedicalEvents.NWTPUC_checkBox.Checked = false;
                                            P_MedicalEvents_FlowLP.Controls.Add(nWTP_PatientMedicalEvents); // Generate A Single User Control
                                        }


                                        P_MedicalEvents_FlowLP.SizeChanged += (sender, e) =>
                                        {
                                            // Adjust the width of the user control to match the width of the parent container
                                            nWTP_PatientMedicalEvents.Width = P_MedicalEvents_FlowLP.ClientSize.Width - nWTP_PatientMedicalEvents.Margin.Horizontal;
                                        };
                                    }
                                    else
                                    {
                                        Console.WriteLine("MyGenerateLabTaks --> No Lab Record Found for LabID: " + id);
                                    }

                                }
                                catch (FormatException)
                                {
                                    MessageBox.Show("PatientMedicalEvent_ID is not matched", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }

                            }

                        }

                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:2 " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine("Error2:" + ex);
            }
        }

        private void NurseWard_TreatePatient_FormClosed(object sender, FormClosedEventArgs e)
        {
            /*if (DashboardFormReference != null && !DashboardFormReference.IsDisposed)
            {
                DashboardFormReference.Show();
            }
            else
            {
                // Handle the situation where the reference form is closed or disposed
                Console.WriteLine("Dashboard form is closed or disposed.");
            }*/

            NurseWard_Dashboard nurseWard_Dashboard = new NurseWard_Dashboard(NurseID, WardNumber);
            nurseWard_Dashboard.Show();
            this.Hide();
        }


        private List<bool> Lab_boolList = new List<bool>();
        private List<bool> General_boolList = new List<bool>();
        private bool IsMedicationRequestComplete;

        private void NWTP_Save_btn_Click(object sender, EventArgs e)
        {
            Console.WriteLine("--------------------------------------------------- Save Click Event Started ---------------------------------------------------");

            MyAssignedToJSON();

            /*foreach (Control control in P_MedicalEvents_FlowLP.Controls)
            {
                if (control is NWTP_PatientMedicalEvents nWTP_PatientMedicalEvents)
                {

                    *//*string NWTP_RequestDetails = nWTP_PatientMedicalEvents.NWTPUC_RequestDetaills.Text;*//*
                    string NWTP_RequestType = nWTP_PatientMedicalEvents.NWTPUC_RequestType.Text;

                    bool NWTP_IsCompleted = nWTP_PatientMedicalEvents.NWTPUC_checkBox.Checked;
                    *//*nWTP_PatientMedicalEvents.NWTP_IsCheckBoxCkecked = NWTP_IsCompleted;*//*

                    Console.WriteLine("NWTP_RequestType: "+ NWTP_RequestType);
                    Console.WriteLine("NWTP_IsCompleted: "+ NWTP_IsCompleted);

                    General_boolList.Add(NWTP_IsCompleted); // To deside what should we display in Nures dashboard Patient status --> Pending,Completed or None



                    if (NWTP_RequestType == "Lab Request")
                    {
                        Lab_boolList.Add(NWTP_IsCompleted);
                        Console.WriteLine("This is a Lab Request Type");

                    }
                    else if(NWTP_RequestType == "Medication")
                    {
                        Console.WriteLine("This is a Medication Request Type");
                        if (NWTP_IsCompleted)
                        {
                            IsMedicationRequestComplete = true;
                        }

                    }

                }

            }
            MyUpdateDataTables();*/
        }


        string LabRequestCompleteStatus;
        private void MyUpdateDataTables()
        {
            // The following code only check Lab Requests

            // If the list is empty, return false
            if (Lab_boolList.Count == 0)
            {
                Console.WriteLine("Error !! --> Lab result Bool is empty!");   
            }
                

            // Use LINQ's All method to check if all values are true
            bool result1 = Lab_boolList.All(value => value);
            // Use LINQ's Any method to check if any value is true
            bool result2 = Lab_boolList.Any(value => value);


            if (result1)
            {
                //All the Lab Request are Completed
                LabRequestCompleteStatus = "Completed";

            }
            else if(result2)
            {
                //All are not Completed
                LabRequestCompleteStatus = "Pending";
            }
            else
            {
                //Non is completed
                LabRequestCompleteStatus = "";
            }



            try
            {
                using(SqlConnection connect = new SqlConnection(MyCommonConnecString.ConnectionString))
                {
                    connect.Open();


                    string query = "UPDATE Admitted_Patients_VisitEvent " +
                                               "SET N_TreatmentStatus = @N_TreatmentStatus " +
                                               "WHERE P_RID = @P_RID";

                    using (SqlCommand cmd = new SqlCommand(query, connect))
                    {
                        Console.WriteLine("N_TreatmentStatus : " + LabRequestCompleteStatus);
                        Console.WriteLine("P_RID : " + PatientRID);

                        cmd.Parameters.AddWithValue("@P_RID", PatientRID);
                        cmd.Parameters.AddWithValue("@N_TreatmentStatus", LabRequestCompleteStatus);


                        cmd.ExecuteNonQuery();
                    }

                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:3 " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine("Error3:" + ex);
            }

        }

        bool Requests_IsCompleted =false;
        private List<bool> IsAllRequestsCompleted = new List<bool>();


        private void NWTP_Completed_btn_Click(object sender, EventArgs e)
        {
            foreach (Control control in P_MedicalEvents_FlowLP.Controls)
            {
                if (control is NWTP_PatientMedicalEvents nWTP_PatientMedicalEvents)
                {

                    /*string NWTP_RequestDetails = nWTP_PatientMedicalEvents.NWTPUC_RequestDetaills.Text;*/
                    string NWTP_RequestType = nWTP_PatientMedicalEvents.NWTPUC_RequestType.Text;
                    Requests_IsCompleted = nWTP_PatientMedicalEvents.NWTPUC_checkBox.Checked;

                    IsAllRequestsCompleted.Add(Requests_IsCompleted); // To deside what should we display in Nures dashboard Patient status --> Pending,Completed or None


                }

            }

            // Use LINQ's All method to check if all values are true
            bool isAllCompleted = IsAllRequestsCompleted.All(value => value);

            if (!isAllCompleted) // If all the Requests are Not Completed
            {
                DialogResult result = MessageBox.Show("All the reqest are not completed! Want To Completed?", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        using (SqlConnection connect = new SqlConnection(MyCommonConnecString.ConnectionString))
                        {
                            connect.Open();


                            string query = "UPDATE Admitted_Patients_VisitEvent " +
                                       "SET N_TreatmentStatus = @N_TreatmentStatus, Is_VisitedByNurse = @Is_VisitedByNurse " +
                                       "WHERE P_RID = @P_RID";


                            using (SqlCommand cmd = new SqlCommand(query, connect))
                            {
                                Console.WriteLine("Inserting to Admitted_Patients_VisitEvent table, LabRequestCompleteStatus : " + LabRequestCompleteStatus);

                                cmd.Parameters.AddWithValue("@P_RID", PatientRID);
                                cmd.Parameters.AddWithValue("@Is_VisitedByNurse", 1);
                                cmd.Parameters.AddWithValue("@N_TreatmentStatus", "Completed");


                                cmd.ExecuteNonQuery();
                            }

                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error:3 " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Console.WriteLine("Error3:" + ex);
                    }
                }
                else if (result == DialogResult.No)
                {
                    return;
                }

            }
            else // If all the Requests are Completed
            {

                try
                {
                    using (SqlConnection connect = new SqlConnection(MyCommonConnecString.ConnectionString))
                    {
                        connect.Open();


                        string query = "UPDATE Admitted_Patients_VisitEvent " +
                                            "SET N_TreatmentStatus = @N_TreatmentStatus, Is_VisitedByNurse = @Is_VisitedByNurse " +
                                            "WHERE P_RID = @P_RID";


                        using (SqlCommand cmd = new SqlCommand(query, connect))
                        {
                            Console.WriteLine(" P_RID::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::: " + PatientRID);
             
                            cmd.Parameters.AddWithValue("@P_RID", PatientRID);
                            cmd.Parameters.AddWithValue("@Is_VisitedByNurse", 1);
                            cmd.Parameters.AddWithValue("@N_TreatmentStatus", "Completed");


                            cmd.ExecuteNonQuery();
                            MessageBox.Show(" Success!  ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);



                            //Remove JSON Data 

                            // Read the JSON data from the file
                            string filePath = @"E:\Programming\Github\HMS_Mithila\HMS_Software_V1_1\HMS_Software_V.01\Nurse_Ward\JSON file\NWPC_userControlDetails.json";
                            string json = File.ReadAllText(filePath);

                            // Deserialize JSON data into a list of UserControlDetails
                            List<UserControlDetails> userControlDetailsList = JsonConvert.DeserializeObject<List<UserControlDetails>>(json);

                            // Remove items that meet the specified conditions
                            userControlDetailsList.RemoveAll(item => item.JSON_PatientID == PatientRID && item.JSON_NurseID == NurseID);

                            // Serialize the updated list to JSON format
                            string updatedJson = JsonConvert.SerializeObject(userControlDetailsList);

                            // Write the updated JSON data back to the file
                            File.WriteAllText(filePath, updatedJson);


                        }

                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error:4 " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Console.WriteLine("Error4:" + ex);
                }

            }


        }
    }
}
