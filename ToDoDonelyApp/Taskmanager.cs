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
                TaskHeader(sortTitle, titleColor);
                DisplayTasks(tasklist, sortTitle);
                DisplayMenu(tasklist);

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
                        Console.WriteLine("   Main Menu".PadRight(consoleWidth));
                        PlainTaskList(tasklist);
                        return; // Skip displaying unsorted tasks
                    default:
                        sortTitle = "Invalid option! Showing unsorted list.";
                        titleColor = ConsoleColor.Red;
                        break;
                }
                TaskHeader(sortTitle, titleColor);
                DisplayTasks(tasklist, sortTitle);
                DisplayMenu(tasklist);
            }
        }

        // Header for Tasklist
        public static void TaskHeader(string title, ConsoleColor titleColor)
        {
            Console.Clear();
            MenuInterface.MenuHeader();
            // Dynamic title based on sorting type
            MenuInterface.TableColor();
            Console.ForegroundColor = titleColor;
            int consoleWidth = Console.WindowWidth;

            Console.WriteLine("   " + title.PadRight(consoleWidth - 3));

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

        //Add a Project/Task to the tasklist
        public static void AddTask(List<Project> tasklist)
        {
            Console.Write("Enter Project Name: ");
            string? projectname = Console.ReadLine();
            if (string.IsNullOrEmpty(projectname))
            {
                projectname = "Unspecified";
            }

            //Adding Task Description
            Console.Write("Enter Task Description: ");
            string? taskdescription = Console.ReadLine();
            if (string.IsNullOrEmpty(taskdescription))
            {
                taskdescription = "Unspecified";
            }


            Console.Write("Enter Project Start Date (yyyy-MM-dd), if today enter (T): ");
            string? projectdateInput = Console.ReadLine()?.Trim();

            DateTime projectdate;

            if (!string.IsNullOrEmpty(projectdateInput) && projectdateInput.Equals("T", StringComparison.OrdinalIgnoreCase))
            {
                projectdate = DateTime.Today; // Assign today's date
            }
            else if (!DateTime.TryParseExact(projectdateInput, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out projectdate))
            {
                Console.Clear();
                MenuInterface.MenuHeader();
                ErrorDate();
                return;
            }

            Console.Write("Enter Project Due Date (yyyy-MM-dd), if today enter (T): ");
            string? projectduedateInput = Console.ReadLine()?.Trim();

            DateTime projectduedate;
            int consoleWidth = Console.WindowWidth;

            if (!string.IsNullOrEmpty(projectduedateInput) && projectduedateInput.Equals("T", StringComparison.OrdinalIgnoreCase))
            {
                projectduedate = DateTime.Today; // Assign today's date
            }
            else if (!DateTime.TryParseExact(projectduedateInput, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out projectduedate))
            {
                Console.Clear();
                MenuInterface.MenuHeader();
                ErrorDate();
                return;
            }

            // Display status options and get user input
            Console.WriteLine("Select Project Status (leave blank if unknown):");
            Console.WriteLine("1. Done");
            Console.WriteLine("2. Development Phase");
            Console.WriteLine("3. Planning Phase");
            Console.WriteLine("4. Not Started");

            string? projectstatus = Console.ReadLine()?.Trim();

            switch (projectstatus)
            {
                case "1":
                    projectstatus = "Done";
                    break;
                case "2":
                    projectstatus = "Development Phase";
                    break;
                case "3":
                    projectstatus = "Planning Phase";
                    break;
                case "4":
                    projectstatus = "Not Started";
                    break;
                default:
                    projectstatus = "Unknown"; // Default if input is invalid or no input
                    break;
            }

            //Adding Task to list
            tasklist.Add(new Project(projectname, taskdescription, projectdate, projectduedate, projectstatus));

            //Confirm that the Task has been added
            Console.Clear();
            MenuInterface.MenuHeader();
            MenuInterface.TableColor();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"  {taskdescription} Task added! Status:{projectstatus}".PadRight(consoleWidth));
            PlainTaskList(tasklist);
            Console.ResetColor();
        }
        //static void TableColor()
        //{
        //    Console.BackgroundColor = ConsoleColor.DarkMagenta;
        //}

        //Add function that counts listed Tasks in the tasklist, count Done Tasks as well
        public static void CountTasks(List<Project> tasklist, out int totalTasks, out int completedTasks)
        {
            totalTasks = tasklist.Count;
            completedTasks = tasklist.Count(task =>
                task.ProjectStatus.Equals("Completed", StringComparison.OrdinalIgnoreCase) ||
                task.ProjectStatus.Equals("Done", StringComparison.OrdinalIgnoreCase));
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

        public static void EditTask(List<Project> tasklist)
        {
            Console.Clear();
            TaskHeader("Edit Task", ConsoleColor.White); // Provide title and color
            DisplayTasks(tasklist, "Current Tasks");
            int consoleWidth = Console.WindowWidth;
            Console.WriteLine(new string('-', consoleWidth));
            MenuInterface.MenuHeader();

            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write("Enter a Project ID to edit: ");
            string? input = Console.ReadLine();

            Project? task = null;
            if (int.TryParse(input, out int taskId))
            {
                task = tasklist.FirstOrDefault(t => t.ProjectIDnumber == taskId);
            }
            if (task == null)
            {
                Console.Clear();
                MenuInterface.MenuHeader();
                MenuInterface.TableColor();
                Console.ForegroundColor = ConsoleColor.Red; // Set text color to Red
                Console.WriteLine("   Task not found.".PadRight(consoleWidth));
                return;
            }

            Console.WriteLine("Select an option:");
            Console.WriteLine("1. Edit Task");
            Console.WriteLine("2. Remove Task");
            Console.WriteLine("3. Mark as Done");

            string? option = Console.ReadLine();
            switch (option)
            {
                case "1":
                    EditTaskDetails(task, tasklist);
                    break;
                case "2":
                    tasklist.Remove(task);
                    Console.Clear();
                    MenuInterface.MenuHeader();
                    MenuInterface.TableColor();
                    Console.ForegroundColor = ConsoleColor.Red; // Set text color to Red
                    Console.WriteLine($"   {task.ProjectName} removed successfully.".PadRight(consoleWidth));
                    PlainTaskList(tasklist);
                    break;
                case "3":
                    task.ProjectStatus = "Done";
                    Console.Clear();
                    MenuInterface.MenuHeader();
                    MenuInterface.TableColor();
                    Console.Write($"   {task.ProjectName} marked as Done.");
                    Console.WriteLine("🟢".PadRight(consoleWidth - 41));
                    Console.ResetColor();
                    PlainTaskList(tasklist);
                    break;
                default:
                    Console.Clear();
                    MenuInterface.MenuHeader();
                    MenuInterface.TableColor();
                    Console.ForegroundColor = ConsoleColor.Red; // Set text color to Red
                    Console.WriteLine("   Invalid option.".PadRight(consoleWidth));
                    PlainTaskList(tasklist);
                    break;
            }
        }

        private static void EditTaskDetails(Project task, List<Project> tasklist)
        {
            int consoleWidth = Console.WindowWidth;
            Console.Write("Enter new Project Name (leave blank to keep current): ");
            string? newTaskName = Console.ReadLine();
            if (!string.IsNullOrEmpty(newTaskName))
            {
                task.ProjectName = newTaskName;
            }

            Console.Write("Enter Task Description (leave blank to keep current): ");
            string? newTaskDescription = Console.ReadLine();
            if (!string.IsNullOrEmpty(newTaskDescription))
            {
                task.TaskDescription = newTaskDescription;
            }

            Console.Write("Enter new Project Start Date (yyyy-MM-dd) (leave blank to keep current): ");
            string? newProjectDateInput = Console.ReadLine();
            switch (newProjectDateInput)
            {
                case string date when !string.IsNullOrEmpty(date) && DateTime.TryParseExact(date, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out DateTime newProjectDate):
                    task.ProjectDate = newProjectDate;
                    break;
                case "":
                    // Keep current date
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid date format. Keeping current date.");
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
            }

            Console.Write("Enter new Project Due Date (yyyy-MM-dd) (leave blank to keep current): ");
            string? newProjectDueDateInput = Console.ReadLine();
            switch (newProjectDueDateInput)
            {
                case string date when !string.IsNullOrEmpty(date) && DateTime.TryParseExact(date, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out DateTime newProjectDueDate):
                    task.ProjectDueDate = newProjectDueDate;
                    break;
                case "":
                    // Keep current date
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid date format. Keeping current date.");
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
            }

            // Display status options and get user input
            Console.WriteLine("Select Project Status (leave blank if unknown):");
            Console.WriteLine("1. Done");
            Console.WriteLine("2. Development Phase");
            Console.WriteLine("3. Planning Phase");
            Console.WriteLine("4. Not Started");

            string? projectstatus = Console.ReadLine()?.Trim();
            switch (projectstatus)
            {
                case "1":
                    projectstatus = "Done";
                    break;
                case "2":
                    projectstatus = "Development Phase";
                    break;
                case "3":
                    projectstatus = "Planning Phase";
                    break;
                case "4":
                    projectstatus = "Not Started";
                    break;
                default:
                    projectstatus = "Unknown"; // Default if input is invalid or no input
                    break;
            }
            task.ProjectStatus = projectstatus;

            Console.Clear();
            MenuInterface.MenuHeader();
            MenuInterface.TableColor();
            Console.ForegroundColor = ConsoleColor.White; // Set text color to white
            Console.WriteLine($"   ID# {task.ProjectIDnumber} updated successfully.".PadRight(consoleWidth));
            Console.ResetColor(); // Reset color to default
            PlainTaskList(tasklist);
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
