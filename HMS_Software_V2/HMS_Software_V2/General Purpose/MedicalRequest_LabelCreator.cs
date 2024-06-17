using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS_Software_V2.General_Purpose
{
    public class MedicalRequest_LabelCreator
    {

        public string Create_LabRequest_Label(string specimen, int specimentID, string investigation, int investigationID, int medicaleventID)
        {
            StringBuilder result = new StringBuilder();


            #region Specimen
            // Split the input string into words
            string[] words = specimen.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string word in words)
            {
                // Take the first character of each word, make it uppercase, and append it to the result
                if (word.Length > 0)
                {
                    result.Append(char.ToUpper(word[0]));
                }
            }

            #endregion
            string specimentLabel = result.ToString() + " " + specimentID.ToString();
            result.Clear();


            #region Investigation
            // Split the input string into words
            string[] words2 = investigation.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string word in words2)
            {
                // Take the first character of each word, make it uppercase, and append it to the result
                if (word.Length > 0)
                {
                    result.Append(char.ToUpper(word[0]));
                }
            }

            #endregion
            string investigationLabel = result.ToString() + " " + investigationID.ToString();

            string finalLabel = specimentLabel + " " + investigationLabel + " " + medicaleventID.ToString();



            return finalLabel;

        }


        public string Create_Prescription_Label(string medicinName, int medicinID, int medicaleventID)
        {
            StringBuilder result = new StringBuilder();


            #region Prescription Label
            // Split the input string into words
            string[] words = medicinName.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string word in words)
            {
                // Take the first character of each word, make it uppercase, and append it to the result
                if (word.Length > 0)
                {
                    result.Append(char.ToUpper(word[0]));
                }
            }

            #endregion
            string finalLable = result.ToString() + " " + medicinID.ToString() + " " + medicaleventID.ToString();


            return finalLable;

        }

    }
}
