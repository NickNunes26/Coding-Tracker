using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Coding_Tracker.Validations;
using Microsoft.Data.Sqlite;
using ConsoleTableExt;

namespace Coding_Tracker
{
    internal class CodingController
    {
        private readonly SqliteConnection sqliteConnection;


        public CodingController(SqliteConnection sqlite)
        {
            sqliteConnection = sqlite;
        }

        public void AddHoursToDB(DateTime start, DateTime final)
        {
            TimeSpan span = final - start;
            
            var query = @"INSERT INTO Coding_Tracker (StartTime, FinalTime, Duration) VALUES (@StartTime, @FinalTime, @Duration)";

            SqliteCommand command = new SqliteCommand(query, sqliteConnection);

            command.Parameters.AddWithValue("@StartTime", Convert.ToString(start));
            command.Parameters.AddWithValue("@FinalTime", Convert.ToString(final));
            command.Parameters.AddWithValue("@Duration", Convert.ToString(Math.Abs(span.TotalHours)));



            sqliteConnection.Open();

            command.ExecuteNonQuery();

            sqliteConnection.Close();


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
                query = @"UPDATE Coding_Tracker SET FinalTime = " + Convert.ToString(newTimeFromUser) + " WHERE FinalTime = '" + dateToBeReplaced + "'";
            }
            

            SqliteCommand command = sqliteConnection.CreateCommand();

            command.CommandText = query;

            sqliteConnection.Open();

            command.ExecuteNonQuery();

            sqliteConnection.Close();
        }

        public void CreateTable(CodingSession codingSession)
        {
            /*
            var tableData = new List<List<object>>
            {
                codingSession.ListOfIDs;

            };


            ConsoleTableBuilder.From(tableData).ExportAndWriteLine();
            */
        }



    }
}
