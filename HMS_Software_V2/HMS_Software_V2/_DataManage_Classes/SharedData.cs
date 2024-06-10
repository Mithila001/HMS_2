﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS_Software_V2._DataManage_Classes
{
    // When a new user Login, We create a copy of the Defualt template and assigne it to one of the static class.
    // This is the class that we will be used to store,edit, and retrive the data for the program.

//--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


    public static class SharedData
    {
        // For Doctors
        public static DoctorData doctorData = new DoctorData();

        // For a Medical Event
        public static MedicalEvnent medicalEvent = new MedicalEvnent();
    }
}