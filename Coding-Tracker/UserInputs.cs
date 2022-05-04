using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleTableExt;
using System.Data;
using Microsoft.Data.Sqlite;
using static Coding_Tracker.Validations;

namespace Coding_Tracker
{
    internal class UserInputs
    {

        public bool ExitProgram { get; set; }
        private readonly SqliteConnection sqliteConnection;
        private List<DateTime> startDateTimeFromDB = new List<DateTime>();
        private List<DateTime> finalDateTimeFromDB = new List<DateTime>();

        public UserInputs(SqliteConnection sqliteConnectionReceived)
        {
            sqliteConnection = sqliteConnectionReceived;
            ExitProgram = false;
        }

        //MainMenu receives input from user and sends the user to required action
        public void MainMenu()
        {

            CreateListsFromDB();

            string chosenOption;

            do
            {
                chosenOption = GetDateOrProgressFromUser();
            } while (!ValidateUserInput(chosenOption));



            DirectFlowOfMenu(chosenOption);



        }

        //Request date or choice from the menu
        private string GetDateOrProgressFromUser()
        {
            Console.WriteLine("Please type a valid date, \"Progress\" to see your current progress or \"Exit\" to quit the program");
            return Console.ReadLine();
        }

        
        //Create initial list of Start and End dates.
        private void CreateListsFromDB()
        {

            const string query = "SELECT StartTime, FinalTime FROM Coding_Tracker ORDER BY StartTime ASC";

            SqliteCommand getDatesCmd = new SqliteCommand(query, sqliteConnection);

            DataTable table = new DataTable();

            sqliteConnection.Open();

            table.Load(getDatesCmd.ExecuteReader());

            for (int i = 0; i < table.Rows.Count; i++)
            {
                DataRow row = table.Rows[i];
                startDateTimeFromDB.Add(Convert.ToDateTime(row["StartTime"]));
                finalDateTimeFromDB.Add(Convert.ToDateTime(row["FinalTime"]));
                Console.WriteLine(row["StartTime"]);
                Console.WriteLine(row["EndTime"]);

            }

            sqliteConnection.Close();


        }

        private void DirectFlowOfMenu(string chosenOptionFromMainMenu)
        {
            
            switch (chosenOptionFromMainMenu)
            {
                case "Progress":
                    break;
                case "Exit":
                    ExitProgram = true;
                    return;
            }

            DateTime dateFromMenu = Convert.ToDateTime(chosenOptionFromMainMenu);

            int i = 0;

            foreach (DateTime date in finalDateTimeFromDB)
            {

                if (dateFromMenu < date && dateFromMenu > startDateTimeFromDB[i])
                {
                    Console.WriteLine("The date you've chosen is between two ex");
                }

                i++;
            }


        }


    }
}
