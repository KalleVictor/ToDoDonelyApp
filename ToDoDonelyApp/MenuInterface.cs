﻿using System;
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
    }
}
