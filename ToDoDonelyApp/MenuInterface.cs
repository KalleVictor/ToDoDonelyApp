using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoDonelyApp
{
    public class MenuInterface
    {
        private static readonly List<Project> tasklist = new();

        // First Line with a Red, Yellow and a Green Circle
        public static void MenuHeader()
        {
            int consoleWidth = Console.WindowWidth;
            TableColor();

            Console.WriteLine(" ".PadRight(consoleWidth));
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("  🔴 ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("🟡 ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("🟢".PadRight(consoleWidth - 8));
            Console.WriteLine(" ".PadRight(consoleWidth));
            Console.ResetColor();
        }

        //ShowMainMenu function 

        public static void ShowMainMenu(bool isStartUp, List<Project> tasklist)
        {
            int consoleWidth = Console.WindowWidth;
            MenuHeader();
            TableColor();

            //This part only runs once at start, Greeting Message!
            if (isStartUp) // Makes sure it runs only on the first execution
            {
                Spacer();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Welcome to ToDoDonely!".PadRight(consoleWidth - 5));
            }
            else
            {
                Spacer(); 
                Console.WriteLine("Main Menu".PadRight(consoleWidth - 5));
            }

            TableColor();
            Spacer();
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

        // Header for Tasklist
        public static void TasklistHeader(string title, ConsoleColor titleColor)
        {
            MenuHeader();
            int consoleWidth = Console.WindowWidth;
            //// Dynamic title based on sorting type
            Console.ForegroundColor = titleColor;
            TableColor();
            Console.WriteLine($"   >> {title} <<".PadRight(consoleWidth));

            //Here comes the tableheader for the tasklist
            TableColor();
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

        //Indicate where the userinput is
        public static void PointToInput()
        {
            int consoleWidth = Console.WindowWidth;
            Console.WriteLine(" ".PadRight(consoleWidth));
            Console.ResetColor();
            Console.Write("==>> ");
        }
        public static void TableColor()
        {
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
        }

        // The "  >>" spacer used in the Project.
        public static void Spacer()
        {
            TableColor();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("  >> ");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        //Localization for the Menus
        public static void Options()
        {
            int consoleWidth = Console.WindowWidth;
            Console.WriteLine("Pick an option".PadRight(consoleWidth - 5));
        }

        public static void Counter(List<Project> tasklist)
        {
            CountTasks(tasklist, out int totalTasks, out int completedTasks);
            int doneTasks = tasklist.Count(task => task.ProjectStatus.Equals("Done", StringComparison.OrdinalIgnoreCase));
            int pendingTasks = totalTasks - doneTasks;
            int consoleWidth = Console.WindowWidth;
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("You have ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(pendingTasks);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(" tasks ToDo and ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(completedTasks);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(" tasks are Done!".PadRight(consoleWidth - 32));
        }

        public static void CountTasks(List<Project> tasklist, out int totalTasks, out int completedTasks)
        {
            totalTasks = tasklist.Count;
            completedTasks = tasklist.Count(task =>
                task.ProjectStatus.Equals("Completed", StringComparison.OrdinalIgnoreCase) ||
                task.ProjectStatus.Equals("Done", StringComparison.OrdinalIgnoreCase));
        }

        //Menu for the Add/Edit Phase in the TaskEditor
        static public void AssignEditStatusMenu()
        {
            int consoleWidth = Console.WindowWidth;
            MenuHeader();
            Spacer();
            Console.WriteLine("Assign a Status or press [Enter] to skip and leave it 'Unknown'".PadRight(consoleWidth - 5));
            Spacer();
            Console.WriteLine("1. Done".PadRight(consoleWidth - 5));
            Spacer();
            Console.WriteLine("2. Development Phase".PadRight(consoleWidth - 5));
            Spacer();
            Console.WriteLine("3. Planning Phase".PadRight(consoleWidth - 5));
            Spacer();
            Console.WriteLine("4. Canceled".PadRight(consoleWidth - 5));
            PointToInput();
        }

        static public void Goodbye()
        {
            int consoleWidth = Console.WindowWidth;
            Console.WriteLine(" ".PadRight(consoleWidth));
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("                      🔴".PadRight(consoleWidth));
            Console.WriteLine(" ".PadRight(consoleWidth));
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("            >> Your Tasks have been saved! Long live the Tasks! <<".PadRight(consoleWidth));
            Console.WriteLine(" ".PadRight(consoleWidth));
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("                                  🟡 ".PadRight(consoleWidth));
            Console.WriteLine(" ".PadRight(consoleWidth));
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("        >> Thank you for using the ToDoDonely App! Have a Nice Day! <<".PadRight(consoleWidth));
            Console.WriteLine(" ".PadRight(consoleWidth));
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("                         🟢  ".PadRight(consoleWidth));
            Console.WriteLine(" ".PadRight(consoleWidth));
        }

        static public void InvalidInput()
        {
            int consoleWidth = Console.WindowWidth;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Invalid option! Please enter a number between 1 and 4.");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(" <<".PadRight(consoleWidth - 59));

        }
    }
}
