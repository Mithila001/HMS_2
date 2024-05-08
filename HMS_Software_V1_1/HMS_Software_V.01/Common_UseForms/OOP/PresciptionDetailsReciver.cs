using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS_Software_V1._01.Common_UseForms.OOP
{
    internal class PresciptionDetailsReciver: ForCommonLabRequests
    {
        public int DoctorID { get; set; }
        public string DoctorName { get; set; }
        public string DoctorPosition { get; set; }
        public string PatientRID { get; set; }
        public string PatientName { get; set; }
        public string PatientAge { get; set; }
        public string PatientGender { get; set; }
        public int PatientMedicalEventID { get; set; }
        public string EventUnitType { get; set; }
        public int WardNumber { get; set; }
    }
}
