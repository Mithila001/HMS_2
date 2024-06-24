using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS_Software_V2._DataManage_Classes
{
    public class ReceptionData
    {
        public int ReceptionID { get; set; }
        public string ReceptionName { get; set; }
       

        public ReceptionData()
        {
            ReceptionID = 0;
            ReceptionName = "";
        }
    }
}
