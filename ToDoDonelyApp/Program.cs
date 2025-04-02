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

Console.OutputEncoding = Encoding.UTF8; // Enabling Unicode output to be able to display circles in the MenuInterface.MenuHeader.

const string filePath = "tasks.json";
var tasklist = Taskmanager.LoadTasks(filePath);

bool exit = false;
bool startup = true; // Flag to track if the program starts


while (!exit)
{
    ShowMainMenu(startup, tasklist);
    startup = false;
    int consoleWidth = Console.WindowWidth;


    string? userInput = Console.ReadLine()?.ToUpper();
    switch (userInput)
    {
        case "1": // Taskmanager - Show Tasks  
            Taskmanager.ShowTasks(tasklist);
            break;
        case "2": // Add Tasks
            TaskEditor.AddTask(tasklist);
            break;
        case "3": // Edit Tasks (update, mark as done, remove)
            TaskEditor.EditTask(tasklist);
            break;
        case "4": // Save and Quit
            Console.Clear();
            MenuInterface.MenuHeader();
            MenuInterface.Spacer();
            Console.Write("Thank you for using the ToDoDonely App! Have a Nice Day! <<".PadRight(consoleWidth -5));
            MenuInterface.MenuHeader();
            Taskmanager.SaveTasks(tasklist, filePath);
            //
            exit = true; // Exit the loop
            break;
        default:
            Console.Clear();
            MenuInterface.MenuHeader();
            Console.ForegroundColor = ConsoleColor.Red;
            MenuInterface.Spacer();
            //Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Invalid option! Please enter a number between 1 and 4.");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(" <<".PadRight(consoleWidth -59));
            MenuInterface.TableColor();
            //taskmanager.plaintasklist(tasklist);
            break;
    }


    //ShowMainMenu function 

    static void ShowMainMenu(bool isStartUp, List<Project> tasklist)
    {
        int consoleWidth = Console.WindowWidth;
        MenuInterface.MenuHeader();
        MenuInterface.TableColor();

        //This part only runs once at start, Greeting Message!
        if (isStartUp) // Makes sure it runs only on the first execution
        {
            MenuInterface.Spacer();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Welcome to ToDoDonely!".PadRight(consoleWidth - 5));
        }
        else
        {   
            MenuInterface.Spacer();
            Console.WriteLine("Main Menu".PadRight(consoleWidth - 5));
        }

        MenuInterface.TableColor();
        MenuInterface.Spacer();
        MenuInterface.Counter(tasklist);
        MenuInterface.Spacer();
        MenuInterface.Options();

        string[] options =
        {
            "Show Task List (by Date or Project)",
            "Add New Task",
            "Edit Task (Update, Mark as Done, Remove)",
            "Save & Quit"
        };

        for (int i = 0; i < options.Length; i++)
        {
            MenuInterface.Spacer();
            Console.Write("(");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write(i + 1);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine($") {options[i]}".PadRight(consoleWidth - 7));
        }
        Console.WriteLine(" ".PadRight(consoleWidth));
        MenuInterface.PointToInput();
    }
}

