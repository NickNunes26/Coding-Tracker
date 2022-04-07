// See https://aka.ms/new-console-template for more information
using ConsoleTableExt;
using Microsoft.Data.Sqlite;

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
            @"CREATE TABLE IF NOT EXISTS Coding-Tracker (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            StartTime TEXT
            EndTime TEXT
            Duration REAL
            )";

        connection.Open();

        tableCmd.ExecuteNonQuery();

        connection.Close();

        

    }


}