using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using ToDoDonelyApp;
using System.IO;
using System.Text.Json;
using System.Globalization;
namespace ToDoDonelyApp

{
    // Taskmanager - 
    public class Taskmanager
    {
        //Method for calling tasklists
        public static void ShowTasks(List<Project> tasklist)
        {
            bool exit = false;
            int consoleWidth = Console.WindowWidth;
         

            string sortTitle = "Unsorted Tasks"; // Default title
            ConsoleColor titleColor = ConsoleColor.Yellow;

            while (!exit)
            {
                Console.Clear();
                MenuInterface.TasklistHeader(sortTitle, titleColor);
                DisplayTasks(tasklist, sortTitle);
                Console.ForegroundColor = ConsoleColor.Gray;
                DisplayMenu(tasklist);
                MenuInterface.PointToInput();

                string? userInput = Console.ReadLine();
                switch (userInput)
                {
                    case "1":
                        sortTitle = "Show Projects by Name";
                        titleColor = ConsoleColor.Yellow;
                        break;
                    case "2":
                        sortTitle = "Show Tasks by Name";
                        titleColor = ConsoleColor.Yellow;
                        break;
                    case "3":
                        sortTitle = "Show Tasks by Date";
                        titleColor = ConsoleColor.Yellow;
                        break;
                    case "4":
                        Console.Clear();
                        Console.SetCursorPosition(0, 0);
                        MenuInterface.MenuHeader();
                        Console.BackgroundColor = ConsoleColor.DarkMagenta;
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("   >> Exited to Main Menu <<".PadRight(consoleWidth));
                        return;
                    default:
                        sortTitle = "Invalid option! Showing unsorted list.";
                        titleColor = ConsoleColor.Red;
                        break;
                }
            }
        }

        //Method for displaying Tasks in a dynamic sorted tasklist, default unsorted
        public static void DisplayTasks(List<Project> tasklist, string sortBy)
        {
            
            int consoleWidth = Console.WindowWidth;
            MenuInterface.TableColor();

            List<Project> sortedtasklist = sortBy switch
            {
                "Show Projects by Name" => tasklist.OrderBy(x => x.ProjectName).ToList(),
                "Show Tasks by Name" => tasklist.OrderBy(x => x.TaskDescription).ToList(),
                "Show Tasks by Date" => tasklist.OrderBy(x => x.ProjectDate).ToList(),
                _ => tasklist // Default: No sorting
            };

            foreach (var task in sortedtasklist)
            {
                ApplyStatusColor(task.ProjectStatus); // If Done mark as Green
                Console.WriteLine(
                    "   #" + task.ProjectIDnumber.ToString().PadRight(5) +
                    task.ProjectName.PadRight(25) +
                    task.TaskDescription.PadRight(30) +
                    task.ProjectDate.ToString("dd MMMM yy", CultureInfo.InvariantCulture).PadRight(18) +
                    task.ProjectDueDate.ToString("dd MMMM yy", CultureInfo.InvariantCulture).PadRight(18) +
                    task.ProjectStatus.PadRight(consoleWidth - 100)
                );
            }
        }

        //Method to apply DYNAMIC ForegroundColor on Rows Depending on Status
        public static void ApplyStatusColor(string status)
        {
            Console.ForegroundColor = status.ToLower() switch
            {
                "done" or "completed" => ConsoleColor.Green,
                "development phase" or "in progress" or "in process" => ConsoleColor.Blue,
                "planning phase" => ConsoleColor.Yellow,
                "on hold" or "not started" => ConsoleColor.DarkYellow,
                "canceled" or "removed" => ConsoleColor.DarkRed,
                "review" or "testing phase" => ConsoleColor.Cyan,
                "deployed" or "live" => ConsoleColor.Magenta,
                _ => ConsoleColor.Gray // Default color
            };
        }

        // Menu after ShowTasks- displaying the tasklist
        public static void DisplayMenu(List<Project> tasklist)
        {
          
            int consoleWidth = Console.WindowWidth;
            MenuInterface.TableColor();
            Console.WriteLine(new string('-', consoleWidth));

            MenuInterface.MenuHeader();
            MenuInterface.TableColor();
            MenuInterface.Spacer();
            MenuInterface.Counter(tasklist);
            MenuInterface.Spacer();
            MenuInterface.Options();

            string[] options =
            {
                "Show Projects by Name",
                "Show Tasks by Name",
                "Show Tasks by Date",
                "Exit to Main Menu"
            };

            for (int i = 0; i < options.Length; i++)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("  >>");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write(" (");
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write(i + 1);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine($") {options[i]}".PadRight(consoleWidth - 7));
            }
        }
      
        //Error invalid Date
        public static void ErrorDate()
        {
            int consoleWidth = Console.WindowWidth;
            MenuInterface.TableColor();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("   >> Invalid date! Returning to Main Menu. <<".PadRight(consoleWidth));
            Console.ResetColor();
        }
    }
}
