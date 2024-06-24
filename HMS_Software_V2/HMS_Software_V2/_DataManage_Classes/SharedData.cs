using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS_Software_V2._DataManage_Classes
{
    // When a new user Login, We create a copy of the Defualt template and assigne it to one of the static class.
    // This is the class that we will be used to store,edit, and retrive the data for the program.

//--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


    public static class SharedData
    {
        // For Doctors
        public static DoctorData doctorData = new DoctorData();

        // For a Medical Event
        public static MedicalEvnent medicalEvent = new MedicalEvnent();

        public static AdmissioOfficer admissioOfficer = new AdmissioOfficer();

        public static UserData userData = new UserData();

        public static Ward_Doctor Ward_Doctor = new Ward_Doctor();

        public static Ward_Nurse Ward_Nurse = new Ward_Nurse();
        
        public static Ward_NursePatient Ward_NursePatient = new Ward_NursePatient();

        public static AdminData adminData = new AdminData();

        public static ViewPatientHistory viewPatientHistory = new ViewPatientHistory();

        public static ReceptionData receptionData = new ReceptionData();
    }

}
