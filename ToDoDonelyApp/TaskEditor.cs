using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ToDoDonelyApp
{
    internal class TaskEditor
    {
        // Displaying the Task that is either being Edited or Added. 
        private static void DisplaySingleTask(Project task)
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
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(new string('-', consoleWidth));
            Taskmanager.ApplyStatusColor(task.ProjectStatus);

            Console.WriteLine(
            "   #" + task.ProjectIDnumber.ToString().PadRight(5) +
        (string.IsNullOrEmpty(task.ProjectName) ? "????" : task.ProjectName).PadRight(25) +
        (string.IsNullOrEmpty(task.TaskDescription) ? "????" : task.TaskDescription).PadRight(30) +
        (task.ProjectDate == default ? "????" : task.ProjectDate.ToString("dd MMMM yy", CultureInfo.InvariantCulture)).PadRight(18) +
        (task.ProjectDueDate == default ? "????" : task.ProjectDueDate.ToString("dd MMMM yy", CultureInfo.InvariantCulture)).PadRight(18) +
        (string.IsNullOrEmpty(task.ProjectStatus) ? "????" : task.ProjectStatus).PadRight(consoleWidth - 100)
            );
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(new string('-', consoleWidth));
            Console.BackgroundColor = ConsoleColor.Black;
        }



        //Add a Project/Task to the tasklist        
        public static void AddTask(List<Project> tasklist)
        {
            int consoleWidth = Console.WindowWidth;
            Console.Clear();
            MenuInterface.MenuHeader();
            MenuInterface.TableColor();
            Console.WriteLine("  >> Add a New Project & Task <<".PadRight(consoleWidth));
            MenuInterface.MenuHeader();
            MenuInterface.Spacer();
            Console.Write("Enter Project Name or press [Enter] to skip: ".PadRight(consoleWidth - 5));
            MenuInterface.PointToInput();

            string? projectname = Console.ReadLine();
            if (string.IsNullOrEmpty(projectname))
            {
                projectname = "Unspecified";
            }

            // ** Changed: Create the project object once, without an ID yet **
            Project newProject = new Project(projectname, string.Empty, DateTime.MinValue, DateTime.MinValue, string.Empty);

            Console.Clear();
            MenuInterface.MenuHeader();
            MenuInterface.TableColor();
            Console.WriteLine($"   >> Project name set to {projectname} <<".PadRight(consoleWidth));
            DisplaySingleTask(newProject);

            MenuInterface.MenuHeader();
            MenuInterface.Spacer();
            Console.Write("Enter Task Description or press [Enter] to skip: ".PadRight(consoleWidth - 5));
            MenuInterface.PointToInput();

            string? taskdescription = Console.ReadLine();
            newProject.TaskDescription = string.IsNullOrEmpty(taskdescription) ? "Unspecified" : taskdescription;

            Console.Clear();
            MenuInterface.MenuHeader();
            MenuInterface.TableColor();
            Console.WriteLine($"   >> Task '{newProject.TaskDescription}' assigned to {newProject.ProjectName}. <<".PadRight(consoleWidth));
            DisplaySingleTask(newProject);

            MenuInterface.MenuHeader();
            MenuInterface.Spacer();
            Console.Write("Enter Project Start Date (yyyy-MM-dd) or press [Enter] to assign today's date:".PadRight(consoleWidth - 5));
            MenuInterface.PointToInput();

            string? projectdateInput = Console.ReadLine()?.Trim();

            if (string.IsNullOrEmpty(projectdateInput))
            {
                newProject.ProjectDate = DateTime.Today;
            }
            else if (!DateTime.TryParseExact(projectdateInput, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out DateTime projectdate))
            {
                Console.Clear();
                MenuInterface.MenuHeader();
                Taskmanager.ErrorDate();
                return;
            }
            else
            {
                newProject.ProjectDate = projectdate;
            }

            Console.Clear();
            MenuInterface.MenuHeader();
            MenuInterface.TableColor();
            Console.WriteLine($"   >> Date {newProject.ProjectDate:yyyy-MM-dd} assigned to {newProject.ProjectName}. <<".PadRight(consoleWidth));
            DisplaySingleTask(newProject);

            MenuInterface.MenuHeader();
            MenuInterface.Spacer();
            Console.Write("Enter Project Due Date (yyyy-MM-dd) or press [Enter] to assign today's date, [T] for tomorrow's date:".PadRight(consoleWidth - 5));
            MenuInterface.PointToInput();

            string? projectduedateInput = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(projectduedateInput))
            {
                newProject.ProjectDueDate = DateTime.Today;
            }
            // Assign Tomorrow's Date
            else if (projectduedateInput.Equals("T", StringComparison.OrdinalIgnoreCase)) 
            {
                newProject.ProjectDate = DateTime.Today.AddDays(1); 
            }
            else if (!DateTime.TryParseExact(projectduedateInput, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out DateTime projectduedate))
            {
                Console.Clear();
                MenuInterface.MenuHeader();
                Taskmanager.ErrorDate();
                return;
            }
            else
            {
                newProject.ProjectDueDate = projectduedate;
            }

            Console.Clear();
            MenuInterface.MenuHeader();
            MenuInterface.TableColor();
            Console.WriteLine($"   >> Due Date {newProject.ProjectDueDate:yyyy-MM-dd} assigned to {newProject.ProjectName}. <<".PadRight(consoleWidth));
            DisplaySingleTask(newProject);
            MenuInterface.AssignEditStatusMenu();

            // Assign/Edit Status to Project/Task
            string? projectstatus = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(projectstatus))
            {
                newProject.ProjectStatus = "Unknown";
            }
            else
                newProject.ProjectStatus = projectstatus switch
                {
                    "1" => "Done",
                    "2" => "Development Phase",
                    "3" => "Planning Phase",
                    "4" => "Canceled",
                    _ => projectstatus,
                };

            //Assign ID only after adding to tasklist
            tasklist.Add(newProject);
            newProject.AssignID();

            Console.Clear();
            MenuInterface.MenuHeader();
            MenuInterface.TableColor();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"   >> {newProject.TaskDescription} Task added! Status: {newProject.ProjectStatus} <<".PadRight(consoleWidth));
            Taskmanager.ApplyStatusColor(newProject.ProjectStatus);
            DisplaySingleTask(newProject);
            Console.ResetColor();
        }

        // Edit a Task/Project in the tasklist
        public static void EditTask(List<Project> tasklist)
        {
            Console.Clear();
            MenuInterface.TasklistHeader("Edit Task", ConsoleColor.Green); // Provide title and color
            Taskmanager.DisplayTasks(tasklist, "Current Tasks");
            int consoleWidth = Console.WindowWidth;
            Console.WriteLine(new string('-', consoleWidth));
            MenuInterface.MenuHeader();
            //Select the ProjectID to Edit/Remove/Mark as Done
            MenuInterface.Spacer();
            Console.Write("Enter a Project ID to edit:".PadRight(consoleWidth - 5));
            MenuInterface.PointToInput();

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
                Console.WriteLine("   >> Project not found. <<".PadRight(consoleWidth));
                return;
            }

            Console.Clear();
            MenuInterface.MenuHeader();
            MenuInterface.TableColor();
            Console.Write($"   >> Project: {task.ProjectName} selected. <<".PadRight(consoleWidth));
            DisplaySingleTask(task);
            MenuInterface.MenuHeader();

            //Choose What to Do
            MenuInterface.Spacer();
            Console.WriteLine("Select an option:".PadRight(consoleWidth - 5));
            MenuInterface.Spacer();
            Console.WriteLine("1. Edit Task".PadRight(consoleWidth -5));
            MenuInterface.Spacer();
            Console.WriteLine("2. Remove Task".PadRight(consoleWidth -5));
            MenuInterface.Spacer();
            Console.WriteLine("3. Mark as Done".PadRight(consoleWidth -5));
            MenuInterface.PointToInput();

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
                    Console.Write($"   >> Project: {task.ProjectName} || Task: {task.TaskDescription} || Removed. <<".PadRight(consoleWidth));
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Taskmanager.ApplyStatusColor(task.ProjectStatus);
                    DisplaySingleTask(task);
                    break;
                case "3":
                    task.ProjectStatus = "Done";
                    Console.Clear();
                    MenuInterface.MenuHeader();
                    MenuInterface.TableColor();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($"   >> Project: {task.ProjectName} || Task: {task.TaskDescription} || has been marked as Done. <<".PadRight(consoleWidth));
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Taskmanager.ApplyStatusColor(task.ProjectStatus);
                    DisplaySingleTask(task);
                    break;
                default:
                    Console.Clear();
                    MenuInterface.MenuHeader();
                    MenuInterface.TableColor();
                    Console.ForegroundColor = ConsoleColor.Red; // Set text color to Red
                    Console.WriteLine($"  >> Invalid option <<".PadRight(consoleWidth));
                    DisplaySingleTask(task);
                    break;
            }
        }

        // Edit a Task/Project in the tasklist

        private static void EditTaskDetails(Project task, List<Project> tasklist)
        {
            int consoleWidth = Console.WindowWidth;
            Console.Clear();
            MenuInterface.MenuHeader();
            MenuInterface.TableColor();
            Console.Write($"   >> Task Editor <<".PadRight(consoleWidth));
            DisplaySingleTask(task);
            MenuInterface.MenuHeader();
            MenuInterface.Spacer();
            Console.Write("Assign a new Project Name or press [Enter] to skip:".PadRight(consoleWidth -5));
            MenuInterface.PointToInput();

            string? newTaskName = Console.ReadLine();
            if (!string.IsNullOrEmpty(newTaskName))
            {
                task.ProjectName = newTaskName;
            }

            Console.Clear();
            MenuInterface.MenuHeader();
            MenuInterface.TableColor();
            Console.Write($"   >> Task Editor <<".PadRight(consoleWidth));
            DisplaySingleTask(task);
            MenuInterface.MenuHeader();
            MenuInterface.Spacer();
            Console.Write("Edit the Task Description or Press [Enter] to skip:".PadRight(consoleWidth -5));
            MenuInterface.PointToInput();

            string? newTaskDescription = Console.ReadLine();
            if (!string.IsNullOrEmpty(newTaskDescription))
            {
                task.TaskDescription = newTaskDescription;
            }

            Console.Clear();
            MenuInterface.MenuHeader();
            MenuInterface.TableColor();
            Console.Write($"   >> Task Editor <<".PadRight(consoleWidth));
            DisplaySingleTask(task);
            MenuInterface.MenuHeader();
            MenuInterface.Spacer();
            Console.Write("Edit the start Date (yyyy-MM-dd) or press [Enter] to skip:".PadRight(consoleWidth - 5));
            MenuInterface.PointToInput();
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

            Console.Clear();
            MenuInterface.MenuHeader();
            MenuInterface.TableColor();
            Console.Write($"   >> Task Editor <<".PadRight(consoleWidth));
            DisplaySingleTask(task);
            MenuInterface.MenuHeader();
            MenuInterface.Spacer();
            Console.Write("Edit Due Date (yyyy-MM-dd) or press [Enter] to skip:".PadRight(consoleWidth - 5));
            MenuInterface.PointToInput();

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

            Console.Clear();
            MenuInterface.MenuHeader();
            MenuInterface.TableColor();
            Console.Write($"   >> Task Editor <<".PadRight(consoleWidth));
            DisplaySingleTask(task);
            MenuInterface.AssignEditStatusMenu();

            // Get User Input

            string? projectstatus = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(projectstatus))
            {
                projectstatus = "Unknown";
            }
            else
                projectstatus = projectstatus switch
                {
                    "1" => "Done",
                    "2" => "Development Phase",
                    "3" => "Planning Phase",
                    "4" => "Canceled",
                    _ => projectstatus,
                };
            task.ProjectStatus = projectstatus;

            Console.Clear();
            MenuInterface.MenuHeader();
            MenuInterface.TableColor();
            Console.ForegroundColor = ConsoleColor.Green; // Set text color to white
            Taskmanager.ApplyStatusColor(task.ProjectStatus);
            Console.WriteLine($"  >> #{task.ProjectIDnumber} - {task.ProjectName} | {task.TaskDescription} updated successfully. <<".PadRight(consoleWidth));
            Console.ResetColor(); // Reset color to default
            DisplaySingleTask(task);
        }
    }
}
