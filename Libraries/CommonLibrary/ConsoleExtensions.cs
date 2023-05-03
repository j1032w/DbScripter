
using System;

namespace CommonLibrary
{
    
    public static class ConsoleExtensions
    {
        
        public static void ConsoleCyan(this string text)
        {
            ConsoleExtensions.ColoredWriteLine(text, ConsoleColor.Cyan);
        }

       
        
        public static void ConsoleWhite(this string text)
        {
            ConsoleExtensions.ColoredWriteLine(text, ConsoleColor.White);
        }
        
        public static void ConsoleGray(this string text)
        {
            ConsoleExtensions.ColoredWriteLine(text, ConsoleColor.Gray);
        }


        public static void ConsoleYellow(this string text)
        {
            ConsoleExtensions.ColoredWriteLine(text, ConsoleColor.Yellow);
        }


        public static void ConsoleRed(this string text)
        {
            ConsoleExtensions.ColoredWriteLine(text, ConsoleColor.Red);
        }


        public static void ConsoleGreen(this string text)
        {
            ConsoleExtensions.ColoredWriteLine(text, ConsoleColor.Green);
        }


        public static void ColoredWriteLine(this string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }
    }
}
