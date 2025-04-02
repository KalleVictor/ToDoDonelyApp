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
            string sortTitle = "Unsorted Tasks"; // Default title
            ConsoleColor titleColor = ConsoleColor.White; // Default title color

            while (!exit)
            {
                Console.Clear();
                MenuInterface.TasklistHeader(sortTitle, titleColor);
                DisplayTasks(tasklist, sortTitle);
                DisplayMenu(tasklist);
                MenuInterface.PointToInput();

                string? userInput2 = Console.ReadLine();
                switch (userInput2)


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
                        sortTitle = "Show Tasks by Due Date";
                        titleColor = ConsoleColor.White;
                        break;
                    case "5":
                        Console.Clear();
                        MenuInterface.MenuHeader();
                        int consoleWidth = Console.WindowWidth;
                        Console.BackgroundColor = ConsoleColor.DarkMagenta;
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.WriteLine("   Exited to Main Menu".PadRight(consoleWidth));
                        return; // Skip displaying unsorted tasks
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
                "Show Tasks by Due Date" => tasklist.OrderBy(x => x.ProjectDueDate).ToList(),
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

        //Add function that counts listed Tasks in the tasklist, count Done Tasks as well
        public static void CountTasks(List<Project> tasklist, out int totalTasks, out int completedTasks)
        {
            totalTasks = tasklist.Count;
            completedTasks = tasklist.Count(task =>
                task.ProjectStatus.Equals("Completed", StringComparison.OrdinalIgnoreCase) ||
                task.ProjectStatus.Equals("Done", StringComparison.OrdinalIgnoreCase));
        }

        // Menu in Show Tasks
        public static void DisplayMenu(List<Project> tasklist)
        {
          
            int consoleWidth = Console.WindowWidth;
            MenuInterface.TableColor();
            Console.WriteLine(new string('-', consoleWidth));

            MenuInterface.MenuHeader();
                        MenuInterface.TableColor();

            //Count Tasks in Show Menu
            CountTasks(tasklist, out int totalTasks, out int completedTasks);
            int doneTasks = tasklist.Count(task => task.ProjectStatus.Equals("Done", StringComparison.OrdinalIgnoreCase));
            int pendingTasks = totalTasks - doneTasks;

            MenuInterface.Spacer();
            Console.WriteLine($"You have {pendingTasks} tasks to do and {completedTasks} tasks are done!".PadRight(consoleWidth - 5));
            
            MenuInterface.Spacer();
            Console.WriteLine("Pick an option".PadRight(consoleWidth - 5));

            string[] options =
            {
                "Show Projects by Name",
                "Show Tasks by Name",
                "Show Tasks by Date",
                "Show Tasks by Due Date",
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
            Console.WriteLine("   Invalid date! Please enter the date in yyyy-MM-dd format or 'T' for Today.".PadRight(consoleWidth));
            Console.ResetColor();
        }

        public static void PlainTaskList(List<Project> tasklist)
        {
            int consoleWidth = Console.WindowWidth;
            MenuInterface.TableColor();
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(" ".PadRight(consoleWidth));
            Console.WriteLine
            ("   " + "ID".PadRight(6) +
                "Project Name".PadRight(25) +
                "Task Description".PadRight(30) +
                "Date Added".PadRight(18) +
                "Due Date".PadRight(18) +
                "Status".PadRight(consoleWidth - 100)
            );
            Console.WriteLine(new string('-', consoleWidth));
            foreach (var task in tasklist)
            {
                ApplyStatusColor(task.ProjectStatus);
                Console.WriteLine(
                    "   #" + task.ProjectIDnumber.ToString().PadRight(5) +
                    task.ProjectName.PadRight(25) +
                    task.TaskDescription.PadRight(30) +
                    task.ProjectDate.ToString("yyyy-MM-dd").PadRight(18) +
                    task.ProjectDueDate.ToString("yyyy-MM-dd").PadRight(18) +
                    task.ProjectStatus.PadRight(consoleWidth - 100)
                );
            }
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(new string('-', consoleWidth));
            Console.BackgroundColor = ConsoleColor.Black;
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
