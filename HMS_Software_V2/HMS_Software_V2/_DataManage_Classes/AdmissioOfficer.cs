using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS_Software_V2._DataManage_Classes
{
    public class AdmissioOfficer
    {
        public int PatientID { get; set; }
        public int Doctor_ID { get; set; }
        public string P_ReferralNote { get; set; }
        public bool Is_Urgent { get; set; }
        public string SendFrom_Location { get; set; }
        public string P_NameWithIinitials { get; set; }
        public string P_Age { get; set; }
        public string P_Gender { get; set; }
        public string P_RegistrationID { get; set; }
        public string D_NameWithInitials { get; set; }
        public string D_Specialty { get; set; }


        //Constructor
        public AdmissioOfficer()
        {
            PatientID = 0;
            Doctor_ID = 0;
            P_ReferralNote = string.Empty;
            Is_Urgent = false;
            SendFrom_Location = string.Empty;
            P_NameWithIinitials = string.Empty;
            P_Age = string.Empty;
            P_Gender = string.Empty;
            P_RegistrationID = string.Empty;
            D_NameWithInitials = string.Empty;
            D_Specialty = string.Empty;
        }
    }
}
