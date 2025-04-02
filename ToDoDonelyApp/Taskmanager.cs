using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using ToDoDonelyApp;
using System.IO;
using System.Text.Json;
namespace ToDoDonelyApp

{
    public class Taskmanager
    {
        
        public static void ShowTasks(List<Project> tasklist)
        {
            bool exit = false;
            int consoleWidth = Console.WindowWidth;
            string sortTitle = "Unsorted Tasks"; // Default title
            ConsoleColor titleColor = ConsoleColor.White; // Default title color

            while (!exit)
            {
                Console.Clear();
                MenuInterface.TasklistHeader(sortTitle, titleColor);
                DisplayTasks(tasklist, sortTitle);
                DisplayMenu(tasklist);
                MenuInterface.PointToInput();

                string? userInput = Console.ReadLine();
                switch (userInput)


                {
                    case "1":
                        sortTitle = "Show Projects by Name";
                        titleColor = ConsoleColor.White;
                        break;
                    case "2":
                        sortTitle = "Show Tasks by Name";
                        titleColor = ConsoleColor.White;
                        break;
                    case "3":
                        sortTitle = "Show Tasks by Date";
                        titleColor = ConsoleColor.White;
                        break;
                    case "4":
                        Console.Clear();
                        MenuInterface.MenuHeader();
                        Console.BackgroundColor = ConsoleColor.DarkMagenta;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("   >> Exited to Main Menu <<".PadRight(consoleWidth));
                        return;
                    default:
                        sortTitle = "Invalid option! Showing unsorted list.";
                        titleColor = ConsoleColor.Red;
                        break;
                }
            }
        }



        // Display Tasks in a dynamic sorted tasklist, Default unsorted
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
                    task.ProjectDate.ToString("yyyy-MM-dd").PadRight(18) +
                    task.ProjectDueDate.ToString("yyyy-MM-dd").PadRight(18) +
                    task.ProjectStatus.PadRight(consoleWidth - 100)
                );
            }
        }

        //Function to apply color based on project status
        public static void ApplyStatusColor(string status)
        {
            if (status.Equals("Done", StringComparison.OrdinalIgnoreCase))
            {
                Console.ForegroundColor = ConsoleColor.Green;  // Green text for Done status
            }
            else if (status.Equals("Development Phase", StringComparison.OrdinalIgnoreCase))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            else if (status.Equals("Planning Phase", StringComparison.OrdinalIgnoreCase))
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Gray;  // Default color for other statuses
            }
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
                "Exit to main menu"
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

            Console.WriteLine(" ".PadRight(consoleWidth));
            Console.ResetColor();
        }
      
        //Error invalid Date
        public static void ErrorDate()
        {
            int consoleWidth = Console.WindowWidth;
            MenuInterface.TableColor();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("   >> Invalid date! Please enter the date in yyyy-MM-dd format or 'T' for Today. <<".PadRight(consoleWidth));
            Console.ResetColor();
        }

        public static void SaveTasks(List<Project> tasklist, string filePath)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(tasklist, options);
            File.WriteAllText(filePath, jsonString);
        }

        public static List<Project> LoadTasks(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return new List<Project>();
            }
            string jsonString = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<Project>>(jsonString) ?? new List<Project>();
        }
    }
}
