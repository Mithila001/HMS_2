using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS_Software_V2._DataManage_Classes
{

    // Put stuff that you want to be defualt values when a new Doctor Login.
    // This is just a template. 
//--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    public class DoctorData
    {
        public int doctorID { get; set; }
        public string doctorName { get; set; }
        public string doctorSpecialization { get; set; }
        public string doctorRID { get; set; }

        public string doctorLocation { get; set; }

        

        //Constructor
        public DoctorData()
        {
            doctorID = 0;
            doctorName = "";
            doctorSpecialization = string.Empty;
            doctorRID = string.Empty;
            doctorLocation = "";
            
        }
    }

}
