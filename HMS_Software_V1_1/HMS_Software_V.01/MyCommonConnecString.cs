using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS_Software_V1._01
{
    public static class MyCommonConnecString
    {
        public static string ConnectionString
        {
            get
            {
                return @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\Source\Repos\HMS_2\HMS_Software_V1_1\HMS_Software_V.01\DatabaseMithilaHMS.mdf;Integrated Security=True";
            }
        }
    }
}
