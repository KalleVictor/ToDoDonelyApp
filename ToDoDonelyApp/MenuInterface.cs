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

        // Header for Tasklist
        public static void TasklistHeader(string title, ConsoleColor titleColor)
        {
            Console.Clear();
            MenuInterface.MenuHeader();
            // Dynamic title based on sorting type
            MenuInterface.TableColor();
            Console.ForegroundColor = titleColor;
            int consoleWidth = Console.WindowWidth;
            Console.WriteLine($"   >> {title} <<".PadRight(consoleWidth));
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

        public static void Spacer()
        {
            TableColor();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("  >> ");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

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

    }
}
