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

Console.OutputEncoding = Encoding.UTF8; // Enabling Unicode output to be able to display circles in the MenuInterface

const string filePath = "tasks.json";
var tasklist = Files.LoadTasks(filePath);

bool exit = false;
bool startup = true; // Flag to track if the program starts


while (!exit)
{
    MenuInterface.ShowMainMenu(startup, tasklist);
    startup = false;
    int consoleWidth = Console.WindowWidth;


    string? userInput = Console.ReadLine()?.ToUpper();
    switch (userInput)
    {
        case "1":
            Taskmanager.ShowTasks(tasklist);
            break;
        case "2": 
            TaskEditor.AddTask(tasklist);
            break;
        case "3": 
            TaskEditor.EditTask(tasklist);
            break;
        case "4": 
            Console.Clear();
            Files.SaveTasks(tasklist, filePath);
            MenuInterface.MenuHeader();
            MenuInterface.TableColor();
            MenuInterface.Goodbye();
            MenuInterface.MenuHeader();
            exit = true;
            break;
        default:
            Console.Clear();
            MenuInterface.MenuHeader();
            Console.ForegroundColor = ConsoleColor.Red;
            MenuInterface.Spacer();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Invalid option! Please enter a number between 1 and 4.");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(" <<".PadRight(consoleWidth -59));
            MenuInterface.TableColor();
            break;
    }
}

