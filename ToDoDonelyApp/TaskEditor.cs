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
            Console.WriteLine("  >> Add a New Task <<".PadRight(consoleWidth));
            MenuInterface.MenuHeader();
            MenuInterface.Spacer();
            Console.Write("Enter Project Name or press [Enter] to skip: ".PadRight(consoleWidth - 5));
            MenuInterface.Exit();
            MenuInterface.PointToInput();

            string? projectname = Console.ReadLine()?.Trim();
            if (projectname == "4")
            {
                Console.Clear();
                Console.SetCursorPosition(0, 0);
                MenuInterface.MenuHeader();
                Console.BackgroundColor = ConsoleColor.DarkMagenta;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("   >> Exited to Main Menu <<".PadRight(Console.WindowWidth));
                Console.ResetColor();
                return; // Exit the AddTask method
            }

            if (string.IsNullOrEmpty(projectname))
            {
                projectname = "Unspecified";
            }

            Project newProject = new Project(projectname, string.Empty, DateTime.MinValue, DateTime.MinValue, string.Empty);

            Console.Clear();
            MenuInterface.MenuHeader();
            MenuInterface.TableColor();
            Console.WriteLine($"   >> Project {projectname} assigned to Task ID#{newProject.ProjectIDnumber}. <<".PadRight(consoleWidth));
            DisplaySingleTask(newProject);

            MenuInterface.MenuHeader();
            MenuInterface.Spacer();
            Console.Write("Enter Task Description or press [Enter] to skip: ".PadRight(consoleWidth - 5));
            MenuInterface.Exit();
            MenuInterface.PointToInput();

            string? taskdescription = Console.ReadLine()?.Trim();
            if (taskdescription == "4")
            {
                Console.Clear();
                Console.SetCursorPosition(0, 0);
                MenuInterface.MenuHeader();
                Console.BackgroundColor = ConsoleColor.DarkMagenta;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("   >> Exited to Main Menu <<".PadRight(Console.WindowWidth));
                Console.ResetColor();
                return; // Exit the AddTask method
            }
            newProject.TaskDescription = string.IsNullOrEmpty(taskdescription) ? "Unspecified" : taskdescription;

            Console.Clear();
            MenuInterface.MenuHeader();
            MenuInterface.TableColor();
            Console.WriteLine($"   >> Task '{newProject.TaskDescription}' assigned to Task ID#{newProject.ProjectIDnumber}. <<".PadRight(consoleWidth));
            DisplaySingleTask(newProject);

            MenuInterface.MenuHeader();
            MenuInterface.Spacer();
            Console.Write("Enter Start Date (");
            MenuInterface.Date();
            Console.Write(") or press [");
            MenuInterface.Enter();
            Console.WriteLine("] to assign today's date:".PadRight(consoleWidth - 48));
            MenuInterface.Exit();
            MenuInterface.PointToInput();

            string? projectdateInput = Console.ReadLine()?.Trim();
            switch (projectdateInput)
            {
                case null or "":
                    newProject.ProjectDate = DateTime.Today;
                    break;
                case "4":
                    Console.Clear();
                    Console.SetCursorPosition(0, 0);
                    MenuInterface.MenuHeader();
                    Console.BackgroundColor = ConsoleColor.DarkMagenta;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("   >> Exited to Main Menu <<".PadRight(Console.WindowWidth));
                    Console.ResetColor();
                    return; // Exit the AddTask method
                default:
                    if (!DateTime.TryParseExact(projectdateInput, "yy-MM-dd", null, System.Globalization.DateTimeStyles.None, out DateTime projectdate))
                    {
                        Console.Clear();
                        MenuInterface.MenuHeader();
                        ErrorDate();
                        return;
                    }
                    newProject.ProjectDate = projectdate;
                    break;
            }

            Console.Clear();
            MenuInterface.MenuHeader();
            MenuInterface.TableColor();
            Console.WriteLine($"   >> Date {newProject.ProjectDate:yy-MM-dd} assigned to Task ID#{newProject.ProjectIDnumber}. <<".PadRight(consoleWidth));
            DisplaySingleTask(newProject);

            MenuInterface.MenuHeader();
            MenuInterface.Spacer(); MenuInterface.Options();
            
            MenuInterface.Spacer();
            Console.Write("Enter Due Date (");
            MenuInterface.Date(); 
            Console.Write(") or press [");
            MenuInterface.Enter();
            Console.WriteLine("] to assign today's date:".PadRight(consoleWidth - 46));
            
            MenuInterface.Spacer(); Console.Write("[");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("T");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("] for tomorrow's date".PadRight(consoleWidth -7));




            MenuInterface.Spacer(); Console.WriteLine("Or simply enter the number of days the Task is due.".PadRight(consoleWidth -5));
            MenuInterface.Exit();
            MenuInterface.PointToInput();

            string? projectduedateInput = Console.ReadLine()?.Trim();

            switch (projectduedateInput?.ToUpper())
            {
                case null or "":
                    newProject.ProjectDueDate = DateTime.Today;
                    break;
                case "4":
                    Console.Clear();
                    Console.SetCursorPosition(0, 0);
                    MenuInterface.MenuHeader();
                    Console.BackgroundColor = ConsoleColor.DarkMagenta;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("   >> Exited to Main Menu <<".PadRight(Console.WindowWidth));
                    Console.ResetColor();
                    return; // Exit the AddTask method
                case "T":
                    newProject.ProjectDueDate = DateTime.Today.AddDays(1);
                    break;

                default:
                    if (int.TryParse(projectduedateInput, out int daysToAdd))
                    {
                        // If user enters a number, add it as days to today
                        newProject.ProjectDueDate = DateTime.Today.AddDays(daysToAdd);
                    }
                    else if (DateTime.TryParseExact(projectduedateInput, "yy-MM-dd", null,
                             System.Globalization.DateTimeStyles.None, out DateTime projectduedate))
                    {
                        // If user enters a valid date in "yy-MM-dd" format
                        newProject.ProjectDueDate = projectduedate;
                    }
                    else
                    {
                        // Handle invalid input
                        Console.Clear();
                        MenuInterface.MenuHeader();
                        ErrorDate();
                        return;
                    }
                    break;
            }

            Console.Clear();
            MenuInterface.MenuHeader();
            MenuInterface.TableColor();
            Console.WriteLine($"   >> Due Date {newProject.ProjectDueDate:yy-MM-dd} assigned to Task ID#{newProject.ProjectIDnumber}. <<".PadRight(consoleWidth));
            DisplaySingleTask(newProject);
            MenuInterface.AssignEditStatusMenu();

            // Assign/Edit Status to Project/Task
            string? projectstatus = Console.ReadLine()?.Trim();
            if (projectstatus == "4")
            {
                Console.Clear();
                Console.SetCursorPosition(0, 0);
                MenuInterface.MenuHeader();
                Console.BackgroundColor = ConsoleColor.DarkMagenta;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("   >> Exited to Main Menu <<".PadRight(Console.WindowWidth));
                Console.ResetColor();
                return; // Exit the AddTask method
            }
            else if (projectstatus == null)
            {
                projectstatus = "Unknown";
            }
            else if (string.IsNullOrEmpty(projectstatus))
            {
                newProject.ProjectStatus = "Unknown";
            }
            else
                newProject.ProjectStatus = projectstatus switch
                {
                    "1" => "Done",
                    "2" => "Development Phase",
                    "3" => "Planning Phase",
                    _ => projectstatus,
                };
            MenuInterface.Exit();

            //Assign ID only after adding to tasklist
            tasklist.Add(newProject);
            newProject.AssignID();

            Console.Clear();
            MenuInterface.MenuHeader();
            MenuInterface.TableColor();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"   >>  Status {newProject.ProjectStatus} assigned to Task ID#{newProject.ProjectIDnumber}. <<".PadRight(consoleWidth));
            Taskmanager.ApplyStatusColor(newProject.ProjectStatus);
            DisplaySingleTask(newProject);
            Console.ResetColor();
        }

        // Edit a Task/Project in the tasklist
        public static void EditTask(List<Project> tasklist)
        {
            Console.Clear();
            MenuInterface.TasklistHeader("Edit Task", ConsoleColor.Gray); // Provide title and color
            Taskmanager.DisplayTasks(tasklist, "Current Tasks");
            int consoleWidth = Console.WindowWidth;
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(new string('-', consoleWidth));
            MenuInterface.MenuHeader();
            //Select the Task/ProjectID to Edit/Remove/Mark as Done
            MenuInterface.Spacer();
            Console.Write("Enter a Task ID# to edit:".PadRight(consoleWidth - 5));
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
                Console.WriteLine("   >> Task not found. <<".PadRight(consoleWidth));
                return;
            }

            Console.Clear();
            MenuInterface.MenuHeader();
            MenuInterface.TableColor();
            Console.Write($"   >> Task #{task.ProjectIDnumber} selected. <<".PadRight(consoleWidth));
            DisplaySingleTask(task);
            MenuInterface.MenuHeader();
            MenuInterface.TableColor();
            MenuInterface.Spacer();
            MenuInterface.Options();
            //Choose What to Do

            MenuInterface.Colored1();
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(") Edit Task".PadRight(consoleWidth - 7));


            MenuInterface.Colored2();
            Console.WriteLine(") Remove Task".PadRight(consoleWidth - 7));

            MenuInterface.Colored3();
            Console.WriteLine(") Mark as Done".PadRight(consoleWidth - 7));

            MenuInterface.Exit();
            MenuInterface.PointToInput();

            string? option = Console.ReadLine();
            switch (option)
            {
                case "1":
                    EditTaskDetails(task, tasklist);
                    break;
                case "2":
                    tasklist.Remove(task);
                    task.ProjectStatus = "Removed";
                    Console.Clear();
                    MenuInterface.MenuHeader();
                    MenuInterface.TableColor();
                    Console.Write($"   >> Task #{task.ProjectIDnumber} || {task.TaskDescription} | Removed. <<".PadRight(consoleWidth));
                    Taskmanager.ApplyStatusColor(task.ProjectStatus);
                    DisplaySingleTask(task);
                    break;
                case "3":
                    task.ProjectStatus = "Done";
                    Console.Clear();
                    MenuInterface.MenuHeader();
                    MenuInterface.TableColor();
                    Console.Write($"   >> Task #{task.ProjectIDnumber} || {task.TaskDescription} | Done. <<".PadRight(consoleWidth));
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Taskmanager.ApplyStatusColor(task.ProjectStatus);
                    DisplaySingleTask(task);
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
            Console.Write($"   >> Task Editor - Edit Project Name <<".PadRight(consoleWidth));
            DisplaySingleTask(task);
            MenuInterface.MenuHeader();
            MenuInterface.Spacer();
            Console.Write("Assign a new Project Name or press [Enter] to skip:".PadRight(consoleWidth -5));
            MenuInterface.Exit();
            MenuInterface.PointToInput();



            string? newTaskName = Console.ReadLine();
            switch (newTaskName)
            {
                case "4":
                    Console.Clear();
                    return;
                case "":
                    // Skip and keep current name
                    break;
                default:
                    if (!string.IsNullOrEmpty(newTaskName))
                    {
                        task.ProjectName = newTaskName;
                    }
                    break;
            }

            Console.Clear();
            MenuInterface.MenuHeader();
            MenuInterface.TableColor();
            Console.Write($"   >> Task Editor - Edit Task Description <<".PadRight(consoleWidth));
            DisplaySingleTask(task);
            MenuInterface.MenuHeader();
            MenuInterface.Spacer();
            Console.Write("Edit the Task Description or Press [Enter] to skip:".PadRight(consoleWidth -5));
            MenuInterface.Exit();
            MenuInterface.PointToInput();

            string? newTaskDescription = Console.ReadLine();
            switch (newTaskDescription)
            {
                case "4":
                    Console.Clear();
                    return;
                case "":
                    // Skip and keep current name
                    break;
                default:
                    if (!string.IsNullOrEmpty(newTaskDescription))
                    {
                        task.TaskDescription = newTaskDescription;
                    }
                    break;
            }

            Console.Clear();
            MenuInterface.MenuHeader();
            MenuInterface.TableColor();
            Console.Write($"   >> Task Editor - Edit Date Added <<".PadRight(consoleWidth));
            DisplaySingleTask(task);
            MenuInterface.MenuHeader();
            MenuInterface.Spacer();
            Console.Write("Edit the Date Added (");
            MenuInterface.Date();
            Console.Write(") or press [");
            MenuInterface.Enter();
            Console.WriteLine("] to skip:".PadRight(consoleWidth - 51));

            MenuInterface.Exit();
            MenuInterface.PointToInput();


            string? newProjectDateInput = Console.ReadLine();
            switch (newProjectDateInput)
            {
                case string date when !string.IsNullOrEmpty(date) && DateTime.TryParseExact(date, "yy-MM-dd", null, System.Globalization.DateTimeStyles.None, out DateTime newProjectDate):
                    task.ProjectDate = newProjectDate;
                    break;
                case "":
                    // Keep current date
                    break;
                case "4":
                    Console.Clear();
                    return;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid date format. Keeping current date.");
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
            }

            Console.Clear();
            MenuInterface.MenuHeader();
            MenuInterface.TableColor();
            Console.Write($"   >> Task Editor - Edit Due Date <<".PadRight(consoleWidth));
            DisplaySingleTask(task);
            MenuInterface.MenuHeader();
            MenuInterface.Spacer(); MenuInterface.Options();
            MenuInterface.Spacer(); Console.Write("Enter Due Date (");
            MenuInterface.Date();
            Console.Write(") or press [");
            MenuInterface.Enter();
            Console.WriteLine("] to keep current Due Date".PadRight(consoleWidth - 46));
            MenuInterface.Spacer(); 
            Console.Write("[");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("T");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("] for tomorrow's date".PadRight(consoleWidth - 7));
            MenuInterface.Spacer(); Console.WriteLine("Or simply enter the number of days the Task is due".PadRight(consoleWidth - 5));
            MenuInterface.Exit();
            MenuInterface.PointToInput();

            string? projectduedateInput = Console.ReadLine()?.Trim();

            switch (projectduedateInput?.ToUpper())
            {
                case "4":
                    Console.Clear();
                    return;
                case null or "":
                    break;
                case "T":
                    task.ProjectDueDate = DateTime.Today.AddDays(1);
                    break;
                default:
                    if (int.TryParse(projectduedateInput, out int daysToAdd))
                    {
                        task.ProjectDueDate = DateTime.Today.AddDays(daysToAdd);
                    }
                    else if (!DateTime.TryParseExact(projectduedateInput, "yy-MM-dd", null, System.Globalization.DateTimeStyles.None, out DateTime projectduedate))
                    {
                        Console.Clear();
                        MenuInterface.MenuHeader();
                        ErrorDate();
                        return;
                    }

                    break;
            }

            Console.Clear();
            MenuInterface.MenuHeader();
            MenuInterface.TableColor();
            Console.Write($"   >> Task Editor - Assign new Status <<".PadRight(consoleWidth));
            DisplaySingleTask(task);
            MenuInterface.AssignEditStatusMenu();

            // Get User Input

            string? projectstatus = Console.ReadLine()?.Trim();
            if (projectstatus == "4")
            {
                Console.Clear();
                return;
            }
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
                    _ => projectstatus,
                };
            task.ProjectStatus = projectstatus;

            Console.Clear();
            MenuInterface.MenuHeader();
            MenuInterface.TableColor();
            Taskmanager.ApplyStatusColor(task.ProjectStatus);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine($"  >> Task #{task.ProjectIDnumber} updated successfully. <<".PadRight(consoleWidth));
            Console.ResetColor(); // Reset color to default
            DisplaySingleTask(task);
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
