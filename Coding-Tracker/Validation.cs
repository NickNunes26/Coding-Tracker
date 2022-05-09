using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using Microsoft.Data.Sqlite;

namespace Coding_Tracker
{
    internal class Validations
    {

        public string startError;
        public string finalError;

        public static bool ValidateUserInput(string userInput)
        {
            if (userInput == "Progress" || userInput == "Exit")
                return true;

            return DateTime.TryParse(userInput, out var dateTime);
        }

        public bool CheckForExistingEntry(DateTime dateFromInput, List<DateTime> startTime, List<DateTime> finalTime)
        {
            int i = 0;



            foreach (DateTime final in finalTime)
            {
                if (dateFromInput < final && dateFromInput > startTime[i])
                {
                    startError = Convert.ToString(startTime[i]);
                    finalError = Convert.ToString(final);
                    return true;
                }
                
                i++;
            }

            return false;
        }

        public bool CheckForExistingEntry(DateTime startTimeFromInput, DateTime finalTimeFromInput, List<DateTime> startTime, List<DateTime> finalTime)
        {
            int i = 0;


            foreach(DateTime final in finalTime)
            {
                if (finalTimeFromInput > final && startTimeFromInput < startTime[i])
                {
                    startError = Convert.ToString(startTime[i]);
                    finalError = Convert.ToString(final);
                    return true;
                }
            }

            return false;
        }


    }
}
