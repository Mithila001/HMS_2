using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS_Software_V2._DataManage_Classes
{
    public class Ward_Doctor
    {
        public int DoctorID { get; set; }
        public string DoctorName { get; set; }
        public string DoctorTitle { get; set; }
        public string DoctorRID {  get; set; }
        public int WardID { get; set; }
        public string WardName { get; set; }
        public int RoundNo { get; set; }

        public Ward_Doctor()
        {
            DoctorID = 0;
            DoctorName = "";
            DoctorTitle = "";
            DoctorRID = "";
            WardID = 0;
            WardName = "";
            RoundNo = 0;
        }
    }
}
