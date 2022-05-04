using ConsoleTableExt;
using Microsoft.Data.Sqlite;
using Coding_Tracker;

/*
 EXAMPLE OF HOW THE TABLE THING SHOULD WORK. TO DELETE LATER.
var tableData = new List<List<object>>
{
    new List<object>{ "Sakura Yamamoto", "Support Engineer", "London", 46},
    new List<object>{ "Serge Baldwin", "Data Coordinator", "San Francisco", 28, "something else" },
    new List<object>{ "Shad Decker", "Regional Director", "Edinburgh"},
};

ConsoleTableBuilder
    .From(tableData)
    .ExportAndWriteLine();
*/

string connectionString = @"Data Source = Coding-Tracker.db";



using (var connection = new SqliteConnection(connectionString))
{

    using (var tableCmd = connection.CreateCommand())
    {

        tableCmd.CommandText =
            @"CREATE TABLE IF NOT EXISTS Coding_Tracker (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            StartTime TEXT,
            FinalTime TEXT,
            Duration REAL
            )";

        connection.Open();

        tableCmd.ExecuteNonQuery();

        connection.Close();

        var userInputs = new UserInputs(connection);

        while (userInputs.ExitProgram == false)
        {
            userInputs.MainMenu();
        }



    }


}