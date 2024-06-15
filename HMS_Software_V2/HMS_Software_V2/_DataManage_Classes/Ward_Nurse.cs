using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS_Software_V2._DataManage_Classes
{
    public class Ward_Nurse
    {
        public int NurseID { get; set; }
        public string NurseName { get; set; }
        public string NureseLicenceNumber { get; set; }
        public string WardName { get; set; }
        public int WardNumber {  get; set; }


        public Ward_Nurse()
        {
            NurseID = 0;
            NurseName = "";
            NureseLicenceNumber = "";
            WardName = "";
            WardNumber = 0;
        }
    }
}
