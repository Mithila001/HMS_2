using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS_Software_V2._DataManage_Classes
{
    public class ViewPatientHistory
    {
        public int PatientID { get; set; }
        public int PatientMedicalEventID { get; set; }

        public string PatientName { get; set; }
        public string PatientRID { get; set; }

        public string MedicalEvnentDate { get; set; }
        public string MedicalEvnentTime { get; set; }


        public ViewPatientHistory()
        {
            PatientID = 0;
            PatientMedicalEventID = 0;
            MedicalEvnentDate = "";
            MedicalEvnentTime = "";
            PatientName = "";
            PatientRID = "";
        }
    }

    
}
