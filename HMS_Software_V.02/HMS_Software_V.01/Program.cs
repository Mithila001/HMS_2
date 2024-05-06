using HMS_Software_V_2.Admition_Officer;
using HMS_Software_V_2.Common_UseForms;
using HMS_Software_V_2.Doctor_OPD;
using HMS_Software_V_2.Doctor_Ward;
using HMS_Software_V_2.Nurse_Ward;
using HMS_Software_V_2.Reception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HMS_Software_V_2.Admin
{

    //HMS_Software_V._01.Admin
    //HMS_Software_V_2
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new UserLogin()
            {

            });


           
            

            /* // Dummy values
             string patientID_str = "P00005";
             int userID = 5;
             string doctorPosition = "Pediatrician";
             string doctorName = "Mice";
             string unittype = "Clinic";

             Application.Run(new DoctorCheck_PatientCheck(patientID_str, userID, doctorPosition, doctorName, unittype));*/
        }
    }
}
