using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Coding_Tracker.Validations;
using Microsoft.Data.Sqlite;

namespace Coding_Tracker
{
    internal class CodingController
    {
        private readonly SqliteConnection sqliteConnection;


        public CodingController(SqliteConnection sqlite)
        {
            sqliteConnection = sqlite;
        }

        void AddHoursToDB()
        {

        }

        void RemoveHoursFromDB()
        {

        }

        public void UpdateHoursFromDB(DateTime newTimeFromUser, string dateToBeReplaced, bool isTheStartDate)
        {
            string query;

            if (isTheStartDate)
            {
                query = @"UPDATE Coding_Tracker SET StartTime = " + Convert.ToString(newTimeFromUser) + " WHERE StartTime = '" + dateToBeReplaced + "'";

            }
            else
            {
                query = @"UPDATE Coding_Tracker SET Quantity = " + Convert.ToString(newTimeFromUser) + " WHERE Date = '" + dateToBeReplaced + "'";
            }
            

            SqliteCommand command = sqliteConnection.CreateCommand();

            command.CommandText = query;
            command.ExecuteNonQuery();
        }

        public void CreateTable()
        {

        }



    }
}
