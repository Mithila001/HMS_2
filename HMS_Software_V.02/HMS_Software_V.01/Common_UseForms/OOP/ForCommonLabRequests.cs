using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS_Software_V_2.Common_UseForms.OOP
{
    public interface ForCommonLabRequests
    {


        int DoctorID { get; set; }
        string DoctorName { get; set; }
        string DoctorPosition { get; set; }
        string PatientRID { get; set; }
        string PatientName { get; set; }
        string PatientAge { get; set; }
        string PatientGender { get; set; }
        string PatientMedicalEventID { get; set; }
        string EventUnitType { get; set; }
        int WardNumber { get; set; }
    }
}
