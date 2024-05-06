using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HMS_Software_V1._01.Nurse_Ward.UserControls
{
    public partial class NWTP_PatientMedicalEvents : UserControl
    {
        /*public Form NurseWard_TreatePatientFormReference { get; set; }*/
        public NWTP_PatientMedicalEvents()
        {
            InitializeComponent();
        }

        public bool NWTP_IsCheckBoxCkecked {  get; set; }
        public int NWTP_NuresID { get; set; }
        public string NWTP_Patient_RID {  get; set; }
        public int NWTP_Patient_MEID {  get; set; }
        public string NWTP_RequestType { get; set; }
        public int NWTTP_MonitorRequest_ID {  get; set; }
        public string NWTP_PrescriptionRequestIDs { get; set; }

        private void NWTPUC_View_btn_Click(object sender, EventArgs e)
        {
            if(NWTP_RequestType == "Monitor")
            {
                NurseWard_P_Monitor nurseWard_P_Monitor = new NurseWard_P_Monitor(NWTP_Patient_RID, NWTTP_MonitorRequest_ID);
                nurseWard_P_Monitor.Show();
            }
            else if(NWTP_RequestType == "Medication")
            {
                NurseWard_P_Medication nurseWard_P_Medication = new NurseWard_P_Medication(NWTP_PrescriptionRequestIDs, NWTP_Patient_RID);
                nurseWard_P_Medication.Show();
            }
            else
            {
                MessageBox.Show("NWTP_RequestType not matched --> "+ NWTP_RequestType);
            }
            

        }
    }
}
