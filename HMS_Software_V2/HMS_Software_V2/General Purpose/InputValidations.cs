using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS_Software_V2.General_Purpose
{
    public class InputValidations
    {
        public static bool MyIsNullorempty(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool MyIsOnlyNumbers(string value)
        {
            if (value.All(char.IsDigit))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
