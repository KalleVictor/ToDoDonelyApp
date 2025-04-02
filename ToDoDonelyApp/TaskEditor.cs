using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoDonelyApp
{
    internal class TaskEditor
    {
        // Displaying the Task that is either being Edited or Added. 
        private static void DisplaySingleTaskinEditor(Project task)
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
            Taskmanager.ApplyStatusColor(task.ProjectStatus);

            Console.WriteLine(
                "   #" + task.ProjectIDnumber.ToString().PadRight(5) +
        (string.IsNullOrEmpty(task.ProjectName) ? "????" : task.ProjectName).PadRight(25) +
        (string.IsNullOrEmpty(task.TaskDescription) ? "????" : task.TaskDescription).PadRight(30) +
        (task.ProjectDate == default ? "????" : task.ProjectDate.ToString("yyyy-MM-dd")).PadRight(18) +
        (task.ProjectDueDate == default ? "????" : task.ProjectDueDate.ToString("yyyy-MM-dd")).PadRight(18) +
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
            Console.Write("Enter Project Name or press [Enter] to skip: ".PadRight(consoleWidth -5));
            MenuInterface.PointToInput();

            string? projectname = Console.ReadLine();
            if (string.IsNullOrEmpty(projectname))
            {
                projectname = "Unspecified";
            }

            //Adding Task Description
            Console.Clear();
            MenuInterface.MenuHeader();
            MenuInterface.TableColor();
            Console.ForegroundColor = ConsoleColor.Green;
            //Text in Console
            Console.WriteLine($"   >> Project name set to {projectname} <<".PadRight(consoleWidth));

            // Temporary task displayed for the user's convenience 
            var tempTask = new Project(projectname, string.Empty, DateTime.MinValue, DateTime.MinValue, string.Empty);
            DisplaySingleTaskinEditor(tempTask);

            MenuInterface.MenuHeader();
            MenuInterface.Spacer();
            Console.Write("Enter Task Description or press [Enter] to skip: ".PadRight(consoleWidth -5));
            MenuInterface.PointToInput();

            string? taskdescription = Console.ReadLine();
            if (string.IsNullOrEmpty(taskdescription))
            {
                taskdescription = "Unspecified";
            }

            // Assign a Date
            Console.Clear();
            MenuInterface.MenuHeader();
            MenuInterface.TableColor();
            Console.ForegroundColor = ConsoleColor.Green;
            //Text in Console
            Console.WriteLine($"   >> Task '{taskdescription}' assigned to {projectname}. <<".PadRight(consoleWidth));

            // Temporary task displayed for the user's convenience 
            var tempTask2 = new Project(projectname, taskdescription, DateTime.MinValue, DateTime.MinValue, string.Empty);
            DisplaySingleTaskinEditor(tempTask2);

            MenuInterface.MenuHeader();
            MenuInterface.Spacer();
            Console.Write("Enter Project Start Date (yyyy-MM-dd) or press [T] to assign today's date:".PadRight(consoleWidth -5));
            MenuInterface.PointToInput();

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
                Taskmanager.ErrorDate();
                return;
            }

            // Assign a Due Date
            Console.Clear();
            MenuInterface.MenuHeader();
            MenuInterface.TableColor();
            Console.ForegroundColor = ConsoleColor.Green;
            //Text in Console
            Console.WriteLine($"   >> Date {projectdate.ToString("yyyy-MM-dd")} assigned to {projectname}. <<".PadRight(consoleWidth));

            // Temporary task displayed for the user's convenience 
            var tempTask3 = new Project(projectname, taskdescription, projectdate, DateTime.MinValue, string.Empty);
            DisplaySingleTaskinEditor(tempTask3);

            MenuInterface.MenuHeader();
            MenuInterface.Spacer();
            Console.Write("Enter Project Due Date (yyyy-MM-dd) or press [T] to assign today's date:".PadRight(consoleWidth -5));
            MenuInterface.PointToInput();

            string? projectduedateInput = Console.ReadLine()?.Trim();

            DateTime projectduedate;

            if (!string.IsNullOrEmpty(projectduedateInput) && projectduedateInput.Equals("T", StringComparison.OrdinalIgnoreCase))
            {
                projectduedate = DateTime.Today; // Assign today's date
            }
            else if (!DateTime.TryParseExact(projectduedateInput, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out projectduedate))
            {
                Console.Clear();
                MenuInterface.MenuHeader();
                Taskmanager.ErrorDate();
                return;
            }

            // Assign Status to Task
            Console.Clear();
            MenuInterface.MenuHeader();
            MenuInterface.TableColor();
            Console.ForegroundColor = ConsoleColor.Green;
            //Text in Console
            Console.WriteLine($"   >> Due Date {projectduedate.ToString("yyyy-MM-dd")} assigned to {projectname}. <<".PadRight(consoleWidth));
            
            // Temporary task displayed for the user's convenience 
            var tempTask4 = new Project(projectname, taskdescription, projectdate, projectduedate, string.Empty);
            DisplaySingleTaskinEditor(tempTask4);

            MenuInterface.MenuHeader();
            MenuInterface.Spacer();
            Console.WriteLine("Assign Status or press [Enter] to skip:".PadRight(consoleWidth - 5));
            MenuInterface.Spacer();
            Console.WriteLine("1. Done".PadRight(consoleWidth - 5));
            MenuInterface.Spacer();
            Console.WriteLine("2. Development Phase".PadRight(consoleWidth - 5));
            MenuInterface.Spacer();
            Console.WriteLine("3. Planning Phase".PadRight(consoleWidth - 5));
            MenuInterface.Spacer();
            Console.WriteLine("4. Not Started".PadRight(consoleWidth -5));
            MenuInterface.PointToInput();

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

            //Adding Task to tasklist
            tasklist.Add(new Project(projectname, taskdescription, projectdate, projectduedate, projectstatus));

            //Confirm that the Task has been added
            Console.Clear();
            MenuInterface.MenuHeader();
            MenuInterface.TableColor();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"   >> {taskdescription} Task added! Status: {projectstatus} <<".PadRight(consoleWidth));
            Taskmanager.ApplyStatusColor(projectstatus);
            DisplaySingleTaskinEditor(new Project(projectname, taskdescription, projectdate, projectduedate, projectstatus));
            Console.ResetColor();
        }

        // Edit a Task/Project in the tasklist
        public static void EditTask(List<Project> tasklist)
        {
            Console.Clear();
            MenuInterface.TasklistHeader(">> Edit Task <<", ConsoleColor.White); // Provide title and color
            Taskmanager.DisplayTasks(tasklist, "Current Tasks");
            int consoleWidth = Console.WindowWidth;
            Console.WriteLine(new string('-', consoleWidth));
            MenuInterface.MenuHeader();

            Console.BackgroundColor = ConsoleColor.Black;


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
                Console.WriteLine("   Project not found.".PadRight(consoleWidth));
                return;
            }

            Console.Clear();
            MenuInterface.MenuHeader();
            MenuInterface.TableColor();
            Console.Write($"   >> Project: {task.ProjectName} selected. <<".PadRight(consoleWidth));
            DisplaySingleTaskinEditor(task);
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
                    DisplaySingleTaskinEditor(task);
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
                    DisplaySingleTaskinEditor(task);
                    break;
                default:
                    Console.Clear();
                    MenuInterface.MenuHeader();
                    MenuInterface.TableColor();
                    Console.ForegroundColor = ConsoleColor.Red; // Set text color to Red
                    Console.WriteLine($"  >> Invalid option <<".PadRight(consoleWidth));
                    DisplaySingleTaskinEditor(task);
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
            DisplaySingleTaskinEditor(task);
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
            DisplaySingleTaskinEditor(task);
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
            DisplaySingleTaskinEditor(task);
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
            DisplaySingleTaskinEditor(task);
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
            DisplaySingleTaskinEditor(task);
            MenuInterface.MenuHeader();
            MenuInterface.Spacer();
            Console.WriteLine("Select Status or (leave blank if Unknown):".PadRight(consoleWidth - 5));
            MenuInterface.Spacer();
            Console.WriteLine("1. Done".PadRight(consoleWidth - 5));
            MenuInterface.Spacer();
            Console.WriteLine("2. Development Phase".PadRight(consoleWidth - 5));
            MenuInterface.Spacer();
            Console.WriteLine("3. Planning Phase".PadRight(consoleWidth - 5));
            MenuInterface.Spacer();
            Console.WriteLine("4. Not Started".PadRight(consoleWidth - 5));
            MenuInterface.PointToInput();

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
            Console.ForegroundColor = ConsoleColor.Green; // Set text color to white
            Taskmanager.ApplyStatusColor(task.ProjectStatus);
            Console.WriteLine($"  >> ID#{task.ProjectIDnumber} - {task.ProjectName} updated successfully. <<".PadRight(consoleWidth));
            Console.ResetColor(); // Reset color to default
            DisplaySingleTaskinEditor(task);
        }

    }
}
