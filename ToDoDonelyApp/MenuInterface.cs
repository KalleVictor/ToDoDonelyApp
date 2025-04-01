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
        public static void TableColor()
        {
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
        }

        public static void Spacer()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("  >> ");
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
