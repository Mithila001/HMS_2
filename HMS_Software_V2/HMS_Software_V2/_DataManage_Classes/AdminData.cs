using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS_Software_V2._DataManage_Classes
{
    public class AdminData
    {
        public int AdminID { get; set; }
        public string AdminName { get; set; }
        public string SelectedUserControl { get; set; }

        public AdminData()
        {
            AdminID = 0;
            AdminName = "";
            SelectedUserControl = "Home";
        }

    }
}
