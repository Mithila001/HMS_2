using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS_Software_V2._DataManage_Classes
{
    public class DoctorData
    {
        public int doctorID { get; set; }
        public string doctorName { get; set; }
        public string doctorSpecialization { get; set; }
        public string doctorRID { get; set; }

        public DoctorData()
        {
            doctorID = 0;
            doctorName = "Dr. Joe";
            doctorSpecialization = string.Empty;
            doctorRID = string.Empty;
        }
    }

    public static class SharedData
    {
        public static DoctorData doctorData = new DoctorData();
    }
}
