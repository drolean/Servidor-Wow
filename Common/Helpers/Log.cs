using System;
using System.Collections.Generic;

namespace Framework.Helpers
{
    public enum LogType
    {
        Debug,
        AuthServer,
        RealmServer,
        Console,
        Error,
    }

    public class Log
    {
        public static readonly Dictionary<LogType, ConsoleColor> TypeColour = new Dictionary<LogType, ConsoleColor>()
        {
            { LogType.Debug,       ConsoleColor.DarkMagenta },
            { LogType.AuthServer,  ConsoleColor.Green },
            { LogType.RealmServer, ConsoleColor.Green },
            { LogType.Console,     ConsoleColor.Magenta },
            { LogType.Error,       ConsoleColor.DarkRed },
        };

        public static void Print(LogType type, object obj)
        {
            Console.ForegroundColor = TypeColour[type];
            Console.Write($"{DateTime.Now:hh:mm:ss.fff} [{type}] ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(obj.ToString());
        }

        public static void Print(string subject, object obj, ConsoleColor colour)
        {
            Console.Write($"{DateTime.Now:hh:mm:ss.fff} [{subject}] ");
            Console.ForegroundColor = colour;
            Console.WriteLine(obj.ToString());
        }

        public static void Print(object obj)
        {
            Console.WriteLine($"{DateTime.Now:hh:mm:ss.fff} [FRAMEWORK] {obj}");
        }
    }
}
