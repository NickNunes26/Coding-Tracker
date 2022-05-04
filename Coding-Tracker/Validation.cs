using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using Microsoft.Data.Sqlite;

namespace Coding_Tracker
{
    static class Validations
    {
        public static bool ValidateUserInput(string userInput)
        {
            if (userInput == "Progress" || userInput == "Exit")
                return true;

            return DateTime.TryParse(userInput, out var dateTime);
        }

        public static bool CheckForExistingDate(string dateFromInput, SqliteConnection sqlConnection)
        {

            var command = sqlConnection.CreateCommand();

            command.CommandText = "SELECT count(*) FROM Coding-Tracker WHERE StartTime='" + dateFromInput + "'";

            return true;
        }



    }
}
