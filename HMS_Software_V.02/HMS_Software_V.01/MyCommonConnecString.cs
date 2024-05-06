using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS_Software_V_2
{
    public static class MyCommonConnecString
    {
        public static string ConnectionString
        {
            get
            {
                return @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\Programming\Github\HMS_Mithila\HMS_Software_V.02\HMS_Software_V.01\DatabaseMithilaHMS.mdf;Integrated Security=True";
            }
        }
    }
}
