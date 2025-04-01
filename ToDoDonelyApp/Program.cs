using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Xml.Linq;
using ToDoDonelyApp;

Console.OutputEncoding = Encoding.UTF8; // Enabling Unicode output to be able to display circles in the MenuInterface.


//// Generic list with Projects added for debugging and displaying
//var tasklist = new List<Project>
//{
//   new ("Project ToDoDonely App", "Devel App", new DateTime(2025, 3, 31), new DateTime(2025, 4, 4), "Development Phase"),
//   new ("Mini Project 1", "Do not remember!", new DateTime(2025, 3, 10), new DateTime(2025, 3, 14), "Done"),
//   new ("Mini Project 2", "Product-related Task", new DateTime(2025, 3, 18), new DateTime(2025, 3, 21), "Done"),
//   new ("Mini Project 3", "Assetmanagement Task", new DateTime(2025, 3, 22), new DateTime(2025, 3, 28), "Done"),
//   new ("Project ToDoDonely App", "Count Tasks Feature", new DateTime(2025, 04, 01), new DateTime(2025, 04, 01), "Done")
//};

//// Added items to the list for practice
//var extratasks = new List<Project>
//{
//        new Project ("Project ToDoDonely App", "Add Tasks Feature", new DateTime(2025, 4, 1), new DateTime(2025, 04, 01), "Done"),
//};
//tasklist.AddRange(extratasks);

//// Added additional items to the list
//tasklist.Add(new Project("Project ToDoDonely App","Save & Exit Feature", new DateTime(2025, 04, 02), new DateTime(2025, 04, 04), "Planning Phase"));

const string filePath = "tasks.json";
// Load tasklist from file
var tasklist = Taskmanager.LoadTasks(filePath);

bool exit = false;
bool firstRun = true; // Flag to track first execution


while (!exit)
{
    firstRun = false; // Set it to false after first execution
    ShowMainMenu(firstRun, tasklist); // Pass the flag to the function
    int consoleWidth = Console.WindowWidth;
    string? userInput = Console.ReadLine()?.ToUpper();
    switch (userInput)
    {
        case "1": // Taskmanager - Show Tasks  
            Taskmanager.ShowTasks(tasklist);
            break;
        case "2": // Add Tasks
            Taskmanager.AddTask(tasklist);
            break;
        case "3": // Edit Tasks (update, mark as done, remove)
            Taskmanager.EditTask(tasklist);
            break;
        case "4": // Save and Quit
            // Temporary Code - Will be Added in the SaveTasks later
            Console.Clear();
            MenuInterface.MenuHeader();
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("   " + "Thank you for using the ToDoDonely App! Have a Nice Day!".PadRight(consoleWidth -3));
            MenuInterface.MenuHeader();
            Taskmanager.SaveTasks(tasklist, filePath);
            //
            exit = true; // Exit the loop
            break;
        default:
            Console.Clear();
            MenuInterface.MenuHeader();
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("   Invalid option, Please enter a number between 1 and 4.".PadRight(consoleWidth));
            Taskmanager.PlainTaskList(tasklist);
            break;
    }


    //ShowMainMenu function 

    static void ShowMainMenu(bool isFirstRun, List<Project> tasklist)
    {
        int consoleWidth = Console.WindowWidth;
        MenuInterface.MenuHeader();
        MenuInterface.TableColor();

        //This part only runs once at start, Greeting Message!
        if (isFirstRun) // Makes sure it runs only on the first execution
        {
            MenuInterface.Spacer();
            Console.WriteLine("Welcome to ToDoDonely".PadRight(consoleWidth - 5));
        }
        else
        {   
            MenuInterface.Spacer();
            Console.WriteLine("Main Menu".PadRight(consoleWidth - 5));
        }
            // This below runs in the loop all the time.
            Taskmanager.CountTasks(tasklist, out int totalTasks, out int completedTasks);
        int doneTasks = tasklist.Count(task => task.ProjectStatus.Equals("Done", StringComparison.OrdinalIgnoreCase));
        int pendingTasks = totalTasks - doneTasks;
        //MenuInterface.MenuHeader();
        MenuInterface.TableColor();

        MenuInterface.Spacer();
        Console.WriteLine($"You have {pendingTasks} tasks to do and {completedTasks} tasks are done!".PadRight(consoleWidth - 5));
        
        MenuInterface.Spacer();
        Console.WriteLine("Pick an option".PadRight(consoleWidth - 5));

        string[] options =
        {
            "Show Task List (by date or project)",
            "Add New Task",
            "Edit Task (update, mark as done, remove)",
            "Save and Quit"
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
        Console.BackgroundColor = ConsoleColor.Black;
        Console.ResetColor();
    }
}

