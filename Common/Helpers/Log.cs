using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Common.Helpers
{
    public enum LogType
    {
        Debug,
        AuthServer,
        RealmServer,
        Console,
        Error,
        Loading,
    }

    public class Log
    {
        public static readonly Dictionary<LogType, ConsoleColor> TypeColour = new Dictionary<LogType, ConsoleColor>
        {
            { LogType.Debug,       ConsoleColor.DarkMagenta },
            { LogType.AuthServer,  ConsoleColor.Green },
            { LogType.RealmServer, ConsoleColor.Green },
            { LogType.Console,     ConsoleColor.Magenta },
            { LogType.Error,       ConsoleColor.DarkRed },
            { LogType.Loading,     ConsoleColor.Cyan },
        };

        public static void Print(LogType type, object obj)
        {
            Console.ForegroundColor = TypeColour[type];
            Console.Write($"{DateTime.Now:hh:mm:ss.fff} [{type}] ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(obj.ToString());
            //
            WriteLog($"{DateTime.Now:hh:mm:ss.fff} [{type}] {obj}");
        }

        public static void Print(string subject, object obj, ConsoleColor colour)
        {
            Console.Write($"{DateTime.Now:hh:mm:ss.fff} [{subject}] ");
            Console.ForegroundColor = colour;
            Console.WriteLine(obj.ToString());
            //
            WriteLog($"{DateTime.Now:hh:mm:ss.fff} [{subject}] {obj}");
        }

        public static void Print(object obj)
        {
            Console.WriteLine($"{DateTime.Now:hh:mm:ss.fff} [FRAMEWORK] {obj}");
            //
            WriteLog($"{DateTime.Now:hh:mm:ss.fff} [FRAMEWORK] {obj}");
        }

        public static void WriteLog(string strLog)
        {
            var logFilePath = $"logs/log-{DateTime.Now:yyyy-M-d}.txt";
            FileInfo logFileInfo = new FileInfo(logFilePath);
            DirectoryInfo logDirInfo = new DirectoryInfo(logFileInfo.DirectoryName ?? throw new InvalidOperationException());

            if (!logDirInfo.Exists)
                logDirInfo.Create();

            using (FileStream fileStream = new FileStream(logFilePath, FileMode.Append))
            {
                using (StreamWriter log = new StreamWriter(fileStream))
                {
                    log.WriteLine(strLog);
                }
            }
        }
    }
}
