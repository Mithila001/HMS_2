using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS_Software_V2._DataManage_Classes
{
    // Put stuff that you want to be defualt values when a new Doctor Login.
    // This is just a template. 
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    public class MedicalEvnent
    {
        public int MedicalEventID { get; set; }
        public int PatientID { get; set; }
        public int DoctorID { get; set; }
        public int NurseID { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }
        public string Location { get; set; }
        public bool IsLabRequest { get; set; }
        public bool IsPrescriptionRequest { get; set; }
        public bool IsAppointmentRequest { get; set; }
        public string PersonExaminationNote { get; set; }
        public string PatientMedicalCondition { get; set; }
        public bool IsInpationt { get; set; }


        public MedicalEvnent()
        {
            MedicalEventID = 0;
            PatientID = 0;
            DoctorID = 0;
            NurseID = 0;
            Date = new DateOnly();
            Time = new TimeOnly();
            Location = string.Empty;
            IsLabRequest = false;
            IsPrescriptionRequest = false;
            IsAppointmentRequest = false;
            PersonExaminationNote = string.Empty;
            PatientMedicalCondition = string.Empty;
            IsInpationt = false;
        }
    }
}
