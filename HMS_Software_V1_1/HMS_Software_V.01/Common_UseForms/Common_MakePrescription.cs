using HMS_Software_V1._01.Common_UseForms.OOP;
using HMS_Software_V1._01.Common_UseForms.UserControls;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace HMS_Software_V1._01.Common_UseForms
{
    public partial class Common_MakePrescription : Form
    {
        public Form DoctorPatientCheckFromReferece { get; set; } // Referecne from the Outpations
        public Form DoctorPatientCheckWardFromReferece { get; set; } // From the Ward 


        SqlConnection connect = new SqlConnection(MyCommonConnecString.ConnectionString);

        /*private MyDataStoringClass dataImporter;*/

        string FC_patientID_str;
        int FC_userID;
        string FC_doctorPosition;
        string FC_doctorName;
        string FC_unittype;

        private ForCommonLabRequests doctorDataAddPrescription; // To get data from Patietn Check Form
        public Common_MakePrescription(ForCommonLabRequests doctorDataAddPrescription)
        {
            this.doctorDataAddPrescription = doctorDataAddPrescription; // To get data from Patietn Check Form

            InitializeComponent();
            MyLoadBasicDetails();
           /* this.dataImporter = dataImporter; // Put it befor MyLoadBasicDetails()  --ChatGPT*/

            // In order form to close, DoctorCheck_PatientCheck required values. So we get original values from DoctorCheck_PatientCheck public class 
            FC_patientID_str = doctorDataAddPrescription.PatientRID;
            FC_userID = doctorDataAddPrescription.DoctorID;
            FC_doctorPosition = doctorDataAddPrescription.DoctorPosition;
            FC_doctorName = doctorDataAddPrescription.DoctorName;
            FC_unittype = doctorDataAddPrescription.EventUnitType;

           /* Console.WriteLine("FC_patientID_str: " + FC_patientID_str);
            Console.WriteLine("FC_userID: " + FC_userID);
            Console.WriteLine("FC_doctorPosition: " + FC_doctorPosition);
            Console.WriteLine("FC_doctorName: " + FC_doctorName);*/
            Console.WriteLine("doctorDataAddPrescription.PatientMedicalEventID: " + doctorDataAddPrescription.PatientMedicalEventID);


            /*this.FormClosed += (s, e) => new DoctorCheck_PatientCheck(FC_patientID_str, FC_userID, FC_doctorPosition, FC_doctorName, FC_unittype).Show();*/
            /* this.FormClosed += (s, e) => this.Show();*/
        }
        

        private void MyLoadBasicDetails() //Load basic UI info
        {
            CMP_dosageAmount_combx.SelectedIndex = 0;
            CMP_durationType_combx.SelectedIndex = 0;


            DOPDPC_doctorName.Text = doctorDataAddPrescription.DoctorName;
            DOPDPC_docPosition.Text = doctorDataAddPrescription.DoctorPosition;
            DOPDPC_docID.Text = doctorDataAddPrescription.DoctorID.ToString();
            DOPDPC_patietName_lbl.Text = doctorDataAddPrescription.PatientName;
            DOPDPC_patietage_lbl.Text = doctorDataAddPrescription.PatientAge;
            DOPDPC_patietGender_lbl.Text = doctorDataAddPrescription.PatientGender;

            // Adding date and time
            DateTime currentDate = DateTime.Today;
            string formattedDate = currentDate.ToString("d MMMM yyyy");

            DateTime currentTime = DateTime.Now;
            string timeString = currentTime.ToString("hh:mm tt");

            DOPDPC_time.Text = formattedDate;
            DOPDPC_date.Text = timeString;
        }

        // ============================================= Medicin Type Search =======================================================
        private int investigationID = 0;
        private void medicinSearch_dataGrV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = this.medicinSearch_dataGrV.Rows[e.RowIndex];
            string investigationIDstring = row.Cells["Medicin_ID"].Value.ToString();
            CMP_medicin_tbx.Text = row.Cells["MedicinName"].Value.ToString();
            medicinSearch_dataGrV.Height = 0;

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

        private void CMP_medicin_tbx_TextChanged(object sender, EventArgs e)
        {
            try
            {
                connect.Open();

                if (CMP_medicin_tbx.TextLength >= 1)
                {
                    string query = "SELECT Medicin_ID, Name FROM Medicin_Storage ";
                    query += "WHERE Medicin_ID LIKE @medicinID OR Name LIKE @name";

                    SqlCommand sqlCommand = connect.CreateCommand();
                    sqlCommand.CommandText = query;

                    // Warnig - This section need user error handling, currenly user can add empty vlaues to to request table
                    sqlCommand.CommandText = query;
                    sqlCommand.Parameters.AddWithValue("@medicinID", CMP_medicin_tbx.Text + "%");
                    sqlCommand.Parameters.AddWithValue("@name", CMP_medicin_tbx.Text + "%");

                    using (SqlDataAdapter adapter = new SqlDataAdapter())
                    {
                        adapter.SelectCommand = sqlCommand;

                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        if (dt != null && dt.Rows.Count > 0)
                        {
                            medicinSearch_dataGrV.DataSource = dt;
                            medicinSearch_dataGrV.Height = medicinSearch_dataGrV.Rows.Count * 30;

                        }
                        else
                        {
                            medicinSearch_dataGrV.Height = 0;
                        }

                    }


                }
                else if (CMP_medicin_tbx.TextLength <= 0)
                {
                    medicinSearch_dataGrV.Height = 0;
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

        private string Route;
        private string Medicin;
        private string Dosage;
        private string Frequency;
        private string Duration;


        private void CMP_add_btn_Click(object sender, EventArgs e)
        {
            int getMedicinID = 0;
            getMedicinID = investigationID;
            string medicinName = CMP_medicin_tbx.Text;

            // Getting rest of the prescription details from the Form
            if((CMP_route_combx.SelectedItem == null)
                || string.IsNullOrEmpty(CMP_dosage_tbx.Text)
                || (CMP_frequesncy_combx.SelectedItem == null)
                || string.IsNullOrEmpty(CMP_duration_tbx.Text)
                || (!string.IsNullOrEmpty(medicinName)
                || getMedicinID != 0))
            {
                Route = CMP_route_combx.Text;
                Medicin = CMP_medicin_tbx.Text;
                Dosage = CMP_dosage_tbx.Text + " " + CMP_dosageAmount_combx.Text;
                Frequency = CMP_frequesncy_combx.Text;
                Duration = CMP_duration_tbx.Text + " " + CMP_durationType_combx.Text;

                MyLoadUserData(Route, Medicin, Dosage, Frequency, Duration, getMedicinID);

                CMP_medicin_tbx.Text = "";
                CMP_dosage_tbx.Text = "";
                CMP_duration_tbx.Text = "";
                CMP_route_combx.SelectedIndex = -1;

            }
            else
            {
                Console.WriteLine("Fill all the blanks");
            }


            
        }

        // ============================================= Button Clikced --> FlowLayoutPanel =================================================
        private void MyLoadUserData(string route, string medicin, string dosage, string frequency, string duration, int medicinID)
        {
            // Generate a number for the LabRequest
            DateTime now = DateTime.Now;
            
            CMP_PrescriptionAdd_userControl cMP_PrescriptionAdd_UserControl = new CMP_PrescriptionAdd_userControl();
            cMP_PrescriptionAdd_UserControl.CMP_UserCntr_medicin_lbl.Text = medicin;
            cMP_PrescriptionAdd_UserControl.CMP_UserCntr_route_lbl.Text = route;
            cMP_PrescriptionAdd_UserControl.CMP_UserCntr_dosage_lbl.Text = dosage;
            cMP_PrescriptionAdd_UserControl.CMP_UserCntr_frequency_lbl.Text = frequency;
            cMP_PrescriptionAdd_UserControl.CMP_UserCntr_duration_lbl.Text = duration;
            cMP_PrescriptionAdd_UserControl.CMP_UserCntr_id_lbl.Text = medicinID.ToString();

            Console.WriteLine("Assigneing to the user control Prescription Request ID int : "+ medicinID);
            Console.WriteLine("Assigneing to the user control Prescription Request ID string : " + medicinID.ToString());


            //Store and send data to the UserControl
            /*addLabRequest.LabInvestigationsID = getinvestigationID;
            addLabRequest.SpecimenNameID = getSpecimenID;*/

            CMP_prescription_FlowLP.SizeChanged += (sender, e) =>
            {
                cMP_PrescriptionAdd_UserControl.Width = CMP_prescription_FlowLP.ClientSize.Width - cMP_PrescriptionAdd_UserControl.Margin.Horizontal;
            };
            cMP_PrescriptionAdd_UserControl.Width = CMP_prescription_FlowLP.ClientSize.Width - cMP_PrescriptionAdd_UserControl.Margin.Horizontal; //Intentional

            CMP_prescription_FlowLP.Controls.Add(cMP_PrescriptionAdd_UserControl);


        }


        // ============================================= Button Clikced Save Data ======================================================

        private void CMP_saveAll_btn_Click(object sender, EventArgs e)
        {
            foreach (Control control in CMP_prescription_FlowLP.Controls)
            {
                if (control is CMP_PrescriptionAdd_userControl cMP_PrescriptionAdd_UserControl)
                {
                    Console.WriteLine("Retriving Prescription Request ID string : " + cMP_PrescriptionAdd_UserControl.CMP_UserCntr_id_lbl.Text);

                    string DB_medicin = cMP_PrescriptionAdd_UserControl.CMP_UserCntr_medicin_lbl.Text;
                    string DB_route = cMP_PrescriptionAdd_UserControl.CMP_UserCntr_route_lbl.Text;
                    string DB_dosage = cMP_PrescriptionAdd_UserControl.CMP_UserCntr_dosage_lbl.Text;
                    string DB_frequency = cMP_PrescriptionAdd_UserControl.CMP_UserCntr_frequency_lbl.Text;
                    string DB_duration = cMP_PrescriptionAdd_UserControl.CMP_UserCntr_duration_lbl.Text;
                    string medicinID = cMP_PrescriptionAdd_UserControl.CMP_UserCntr_id_lbl.Text;
                    int DB_medicinID = int.Parse(medicinID);
                    Console.WriteLine("converted to itn Prescription Request ID : " + DB_medicinID);




                    MyInsertLabDataIntoDatabase(DB_medicin,DB_route,DB_dosage,DB_frequency,DB_duration,DB_medicinID);
                }

            }
            MySuccessfulyDataInsertMessage();

        }


        // ============================================= Button Clikced -> Sending Data to the databse =======================================
        private int getPrescriptionRequestID;
        private string PrescriptionRequestIDlistStrig; // To Store all the lab request IDs in thie event
        private void MyInsertLabDataIntoDatabase(string DB_medicin, string DB_route, string DB_dosage, string DB_frequency, string DB_duration,int DB_medicinID)
        {
            try
            {
                connect.Open();

                try
                {
                    string query = "INSERT INTO Prescription_Request (PR_Route, PR_Medicin, PR_Dosage, PR_Frequency, PR_Duration, PR_MedicinID, PatientMedicalEvent_ID)" +
                    " VALUES (@route, @medicin, @dosage, @frequency, @duration, @medicinID, @patienRID)";

                    using (SqlCommand cmd = new SqlCommand(query, connect))
                    {
                        Console.WriteLine("Inserting Prescription Request ID to the table : " + DB_medicinID);
                        Console.WriteLine("Inserting Patient Medical Event ID to the table : " + doctorDataAddPrescription.PatientMedicalEventID);

                        cmd.Parameters.AddWithValue("@route", DB_route);
                        cmd.Parameters.AddWithValue("@medicin", DB_medicin);
                        cmd.Parameters.AddWithValue("@dosage", DB_dosage);
                        cmd.Parameters.AddWithValue("@frequency", DB_frequency);
                        cmd.Parameters.AddWithValue("@duration", DB_duration);
                        cmd.Parameters.AddWithValue("@medicinID", DB_medicinID);
                        cmd.Parameters.AddWithValue("@patienRID", doctorDataAddPrescription.PatientMedicalEventID);


                        cmd.ExecuteNonQuery();
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Error executing INSERT query: " + ex.Message);
                    
                }

                


                // Using PatientMedicalEventID to find the medicin records that now created and get the current LabRequest_ID
                string query2 = "SELECT PrescriptionRequest_ID FROM Prescription_Request WHERE PatientMedicalEvent_ID = @pmeID";

                PrescriptionRequestIDlistStrig = "";

                using (SqlCommand getIdCommand = new SqlCommand(query2, connect))
                {
                    getIdCommand.Parameters.AddWithValue("@pmeID", doctorDataAddPrescription.PatientMedicalEventID);

                    using (SqlDataReader reader = getIdCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            try
                            {
                                getPrescriptionRequestID = Convert.ToInt32(reader["PrescriptionRequest_ID"]);
                                Console.WriteLine($"2. PrescriptionRequest ID: {PrescriptionRequestIDlistStrig}");
                                PrescriptionRequestIDlistStrig += getPrescriptionRequestID.ToString() + ", "; // Store or process labRequestID as need

                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("Failed to parse PrescriptionRequest ID.");
                            }
                        }
                    }
                }
                Console.WriteLine("Befor if else --> getPrescriptionRequestID:"+ getPrescriptionRequestID);
                if (getPrescriptionRequestID != 0)
                {
                    try
                    {
                        // Assigne LabRequest_ID to PatientMedical_Event table record 
                        string updateQuery = "UPDATE PatientMedical_Event SET PrescriptionRequest_ID = @prescriptionRequestID WHERE PatientMedicalEvent_ID = @pmeID";
                        using (SqlCommand updateCommand = new SqlCommand(updateQuery, connect))
                        {
                            updateCommand.Parameters.AddWithValue("@prescriptionRequestID", PrescriptionRequestIDlistStrig);
                            updateCommand.Parameters.AddWithValue("@pmeID", doctorDataAddPrescription.PatientMedicalEventID);

                            int rowsAffected = updateCommand.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                Console.WriteLine("PrescriptionRequest ID updated successfully.");
                                /*MessageBox.Show("Success", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);*/
                            }
                            else
                            {
                                Console.WriteLine("Failed to update PrescriptionRequest ID.");
                                MessageBox.Show("Failed to update PrescriptionRequest ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine("Error executing Update query: " + ex.Message);
                       
                    }

                }
                else
                {
                    Console.WriteLine("Error: ");
                }

            }
            catch(Exception ex)
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
            if (!string.IsNullOrEmpty(PrescriptionRequestIDlistStrig))
            {
                MessageBox.Show("Successfully Added " + PrescriptionRequestIDlistStrig.Substring(0, PrescriptionRequestIDlistStrig.Length - 2)
                    + " Request IDs", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                
            }
            else
            {
                MessageBox.Show("Lab Request list is empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Common_MakePrescription_FormClosed(object sender, FormClosedEventArgs e)
        {
            Console.WriteLine("Common_MakePrescription_FormClosed: FC_unittype =  " + FC_unittype);

            if(FC_unittype == "OPD"|| FC_unittype == "Clinic")
            {
                // Show Patient Check Form
                DoctorPatientCheckFromReferece.Show();
            }
            else
            {
                DoctorPatientCheckWardFromReferece.Show();
            }
            
        }
    }
}
