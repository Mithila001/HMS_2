using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS_Software_V2._DataManage_Classes
{
    public class Ward_NursePatient
    {
        public int PatientID { get; set; }
        public string PatientName { get; set; }
        public string PatientRID { get; set; }
        public int PatientMedicalEventID { get; set; }
        public string PatientGender { get; set; }
        public string PatientAge { get; set; }
        public string PatientCondition { get; set; }    

        public Ward_NursePatient()
        {
            PatientID = 0;
            PatientName = "";
            PatientRID = "";
            PatientMedicalEventID = 0;
            PatientGender = "";
            PatientAge = "";
            PatientCondition = "";
        }
    }
}
