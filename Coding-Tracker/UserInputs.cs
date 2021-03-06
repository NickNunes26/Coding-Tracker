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
        private List<CodingSession> codingSession = new List<CodingSession>();
        private Validations validations = new Validations();
        public DateTime initialDateTime;
        public DateTime finalDateTime;
        private bool returnMainMenu;
        private CodingController controller;

        public UserInputs(SqliteConnection sqliteConnectionReceived)
        {
            sqliteConnection = sqliteConnectionReceived;
            ExitProgram = false;
            controller = new CodingController(sqliteConnection);
        }



        //MainMenu receives input from user and sends the user to required action
        public void MainMenu()
        {
            returnMainMenu = false;

            CreateListsFromDB();

            string startDate = GetStartDate();

            if (ExitProgram || returnMainMenu)
                return;

            if (startDate == "Progress")
                controller.CreateTable(codingSession);

            string finalDate = GetFinalDate();

            if (ExitProgram || returnMainMenu)
                return;

            if (finalDate == "Progress")
                controller.CreateTable(codingSession);

            if (finalDateTime < initialDateTime)
            {
                Console.WriteLine("The time you entered as your final is before the time you set as your starting time. Would you like to have them reversed or go back to the Main Menu?");
                Console.WriteLine("Reversed / Main Menu / Exit");
                bool validChoice = false;
                do
                {
                    var read = Console.ReadLine();

                    switch (read)
                    {
                        case "Reversed":
                            controller.AddHoursToDB(finalDateTime, initialDateTime);
                            return;
                        case "Main Menu":
                            return;
                        case "Exit":
                            ExitProgram = true;
                            return;
                        default:
                            Console.WriteLine("Please select a valid choice");
                            break;
                    }

                } while (!validChoice);
            }

            controller.AddHoursToDB(initialDateTime, finalDateTime);


        }

        //Create initial list of Start and End dates.
        private void CreateListsFromDB()
        {

            const string query = "SELECT Id, StartTime, FinalTime, Duration FROM Coding_Tracker ORDER BY StartTime ASC";

            SqliteCommand getDatesCmd = new SqliteCommand(query, sqliteConnection);

            DataTable table = new DataTable();

            sqliteConnection.Open();

            table.Load(getDatesCmd.ExecuteReader());

            for (int i = 0; i < table.Rows.Count; i++)
            {
                DataRow row = table.Rows[i];

                codingSession.Add(
                    new CodingSession
                    {
                        Id = Convert.ToInt32(row["Id"]),
                        StartTime = Convert.ToString(row["StartTime"]),
                        FinalTime = Convert.ToString(row["FinalTime"]),
                        Duration = Convert.ToDouble(row["Duration"])

                    });


            }

            sqliteConnection.Close();


        }

        private string GetStartDate()
        {

            string chosenOption = validations.GetAndValidateInfoFromUser();

            switch (chosenOption)
            {
                case "Exit":
                    ExitProgram = true;
                    return chosenOption;
                case "Progress":
                    return chosenOption;
                case "Start Watch":
                    initialDateTime = DateTime.Now;
                    return Convert.ToString(initialDateTime);
            }

            DateTime dateFromMenu = Convert.ToDateTime(chosenOption);

            //This block checks if the date from the input falls between two existing dates.
            if (validations.CheckForExistingEntry(dateFromMenu, codingSession))
            {
                ErrorMenu(dateFromMenu);
                if (returnMainMenu)
                    return "Main Menu";

            }
            else
            {
                initialDateTime = dateFromMenu;
                return chosenOption;
            }

            return chosenOption;

        }

        private string GetFinalDate()
        {
            Console.WriteLine("Please select a final date and time to log activity");

            string chosenOption = validations.GetAndValidateInfoFromUser();

            switch (chosenOption)
            {
                case "Progress":
                    return chosenOption;
                case "Exit":
                    ExitProgram = true;
                    return chosenOption;
                case "Stop Watch":
                    finalDateTime = DateTime.Now;
                    return Convert.ToString(finalDateTime);
            }

            DateTime dateFromMenu = Convert.ToDateTime(chosenOption);

            //This block checks if the date from the input falls between two existing dates.
            if (validations.CheckForExistingEntry(dateFromMenu, codingSession))
            {
                ErrorMenu(dateFromMenu);
                if (returnMainMenu)
                    return "Main Menu";

            }
            else
            {
                finalDateTime = dateFromMenu;
                return chosenOption;
            }

            return chosenOption;

        }

        //This block receives the error and redirects the user accordingly
        private void ErrorMenu(DateTime dateFromMenu)
        {

            Console.WriteLine(string.Format("The date you have entered falls between two other dates: \n {0} & \n {1} \n What would you like to do?", validations.startError, validations.finalError));


            bool validChoice = false;

            do
            {
                ErrorMenuTexts();

                switch (Console.ReadLine())
                {
                    case "1":
                        initialDateTime = Convert.ToDateTime(validations.finalError);
                        return;
                    case "2":
                        returnMainMenu = true;
                        return;
                    case "3":
                        controller.UpdateHoursFromDB(dateFromMenu, validations.startError, true);
                        returnMainMenu = true;
                        return;
                    case "4":
                        controller.UpdateHoursFromDB(dateFromMenu, validations.finalError, false);
                        returnMainMenu = true;
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
