using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleTableExt;
using System.Data;
using Microsoft.Data.Sqlite;

namespace Coding_Tracker
{
    internal class UserInputs
    {

        public bool ExitProgram { get; set; }
        public SqliteConnection sqliteConnection { get; set; }
        private CodingSession codingSession = new CodingSession();
        private Validations validations = new Validations();

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
            } while (!validations.ValidateUserInput(chosenOption));

            switch (chosenOption)
            {
                case "Progress":
                    break;
                case "Exit":
                    ExitProgram = true;
                    return;
            }

            DateTime dateFromMenu = Convert.ToDateTime(chosenOption);

            int i = 0;

            if (validations.CheckForExistingEntry(dateFromMenu, codingSession.ListOfStartTimes, codingSession.ListOfFinalTimes))
            {
                ErrorMenu();
            }

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
                codingSession.ListOfStartTimes.Add(Convert.ToDateTime(row["StartTime"]));
                codingSession.ListOfFinalTimes.Add(Convert.ToDateTime(row["FinalTime"]));

            }

            sqliteConnection.Close();


        }

        private void DirectFlowOfMenu(string chosenOptionFromMainMenu)
        {
            



        }

        private void ErrorMenu()
        {
            Console.WriteLine(string.Format("The date you have entered falls between two other dates: \n {0} & \n {1} \n What would you like to do?", validations.startError, validations.finalError));


            bool validChoice = false;

            do
            {
                ErrorMenuTexts();

                switch (Console.ReadLine())
                {
                    case "1":
                        return;
                    case "2":
                        return;
                    case "3":
                        return;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Please select a valid choice:");
                        break;
                }

            } while (!validChoice);



        }

        static void ErrorMenuTexts()
        {
            Console.WriteLine("1. Set my input date to the final date");
            Console.WriteLine("2. Change my input date");
            Console.WriteLine("3. Update the starting date as the date I just input");
            Console.WriteLine("4. Update the final date as the date I just input");

        }


    }
}
