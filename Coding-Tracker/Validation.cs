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

        public bool ValidateUserInput(string userInput)
        {
            if (userInput == "Progress" || userInput == "Exit")
                return true;

            return DateTime.TryParse(userInput, out var dateTime);
        }

        public bool CheckForExistingEntry(DateTime dateFromInput, List<CodingSession> codingSession)
        {
            int i = 0;

            if (codingSession == null)
                return false;


            foreach (CodingSession session in codingSession)
            {
                if (dateFromInput < Convert.ToDateTime(session.FinalTime) && dateFromInput > Convert.ToDateTime(session.StartTime))
                {
                    startError = session.StartTime;
                    finalError = session.FinalTime;
                    return true;
                }
                
                i++;
            }

            return false;
        }

        public bool CheckForExistingEntry(DateTime startTimeFromInput, DateTime finalTimeFromInput, List<CodingSession> codingSession)
        {
            int i = 0;

            if (codingSession == null)
                return false;


            foreach (CodingSession session in codingSession)
            {
                if (finalTimeFromInput > Convert.ToDateTime(session.FinalTime) && startTimeFromInput < Convert.ToDateTime(session.StartTime))
                {
                    startError = session.StartTime;
                    finalError = session.FinalTime;
                    return true;
                }
            }

            return false;
        }

        public string GetAndValidateInfoFromUser()
        {
            string chosenOption;
            do
            {
                Console.WriteLine("Please type a valid date, \"Progress\" to see your current progress or \"Exit\" to quit the program");
                chosenOption = Console.ReadLine();
            } while (!ValidateUserInput(chosenOption));

            return chosenOption;
        }


    }
}
