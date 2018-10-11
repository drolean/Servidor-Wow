using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Common.Globals;
using Common.Network;

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
        Implement
    }

    public enum LogPacket
    {
        Sending,
        Receive
    }

    public class Log
    {
        public static readonly Dictionary<LogType, ConsoleColor> TypeColour = new Dictionary<LogType, ConsoleColor>
        {
            {LogType.Debug, ConsoleColor.DarkMagenta},
            {LogType.AuthServer, ConsoleColor.Green},
            {LogType.RealmServer, ConsoleColor.Green},
            {LogType.Console, ConsoleColor.Magenta},
            {LogType.Error, ConsoleColor.DarkRed},
            {LogType.Loading, ConsoleColor.Cyan},
            {LogType.Implement, ConsoleColor.Blue}
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

        public static void Print(object obj)
        {
            Console.WriteLine($"{DateTime.Now:hh:mm:ss.fff} [FRAMEWORK] {obj}");
            //
            WriteLog($"{DateTime.Now:hh:mm:ss.fff} [FRAMEWORK] {obj}");
        }

        public static void WriteLog(string strLog)
        {
            var logFilePath = $"logs/log-{DateTime.Now:yyyy-M-d}.txt";
            var logFileInfo = new FileInfo(logFilePath);
            var logDirInfo = new DirectoryInfo(logFileInfo.DirectoryName ?? throw new InvalidOperationException());

            if (!logDirInfo.Exists)
                logDirInfo.Create();

            using (var fileStream = new FileStream(logFilePath, FileMode.Append))
            {
                using (var log = new StreamWriter(fileStream))
                {
                    log.WriteLine(strLog);
                }
            }
        }

        public static void LogPacket(LogPacket type, PacketServer packet)
        {
            try
            {
                var logFilePath = $"logs/{type}_log-{DateTime.Now:yyyy-M-d}.txt";
                var logFileInfo = new FileInfo(logFilePath);
                var logDirInfo = new DirectoryInfo(logFileInfo.DirectoryName ?? throw new InvalidOperationException());

                if (!logDirInfo.Exists)
                    logDirInfo.Create();

                using (var fileStream = new FileStream(logFilePath, FileMode.Append))
                {
                    using (var log = new StreamWriter(fileStream))
                    {
                        log.WriteLine(
                            $"[{DateTime.Now:yyyy-M-d H:mm:ss}] OPCode: {(RealmEnums) packet.Opcode} " + // (RealmCMD)
                            $"[Length: {packet.Packet.Length}]");
                        log.Write(Utils.ByteArrayToHex(packet.Packet));
                        log.WriteLine();
                        log.WriteLine();
                    }
                }
            }
            catch (Exception e)
            {
                var trace = new StackTrace(e, true);
                Print(LogType.Error,
                    $"{e.Message}: {e.Source}\n{trace.GetFrame(trace.FrameCount - 1).GetFileName()}:{trace.GetFrame(trace.FrameCount - 1).GetFileLineNumber()}");
            }
        }
    }
}