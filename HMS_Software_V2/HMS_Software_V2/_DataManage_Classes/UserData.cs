using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS_Software_V2._DataManage_Classes
{
    public class UserData
    {
        public int UserID { get; set; }
        public string UserName { get; set; }


        public UserData()
        {
            UserID = 0;
            UserName = string.Empty;
        }

    }

}
