using System;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Threading;
using Common.Helpers;


namespace RealmServer
{
    internal class MainProgram
    {
        private static void Main()
        {
            // Set Culture
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-us");
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-us");

            Console.SetWindowSize(
                Math.Min(110, Console.LargestWindowWidth),
                Math.Min(20, Console.LargestWindowHeight));

            var time = Time.GetMsTime();

            Console.Title = $@"{Assembly.GetExecutingAssembly().GetName().Name} v{Assembly.GetExecutingAssembly().GetName().Version}";

            Log.Print(LogType.RealmServer, $"Version {Assembly.GetExecutingAssembly().GetName().Version}");
            Log.Print(LogType.RealmServer, $"Running on .NET Framework Version {Environment.Version}");



            Log.Print(LogType.RealmServer, $"Running from: {AppDomain.CurrentDomain.BaseDirectory}");
            Log.Print(LogType.RealmServer, $"Successfully started in {Time.GetMsTimeDiff(time, Time.GetMsTime()) / 100}ms");

            // Commands
            while (true)
            {
                var command = Console.ReadLine();
                switch (command)
                {
                    case "/up":
                    case "up":
                        Log.Print(LogType.Console, $"Uptime {DateTime.UtcNow - Process.GetCurrentProcess().StartTime.ToUniversalTime()}");
                        break;
                    case "/db":
                    case "db":
                        Log.Print(LogType.Console, "XML reloaded.");
                        break;

                    case "/gc":
                    case "gc":
                        GC.Collect();
                        Log.Print(LogType.Console,
                            $"Total Memory: {Convert.ToSingle(GC.GetTotalMemory(false) / 1024 / 1024)}MB");
                        break;

                    case "/q":
                    case "q":
                        // TODO: set all players to OFFLine
                        //       send Global message to shutdown server
                        //       set realm to offline
                        Log.Print(LogType.Console, "Halting process...");
                        Thread.Sleep(500);
                        Environment.Exit(-1);
                        return;
                    case "/help":
                    case "help":
                    case "/?":
                    case "?":
                        PrintHelp();
                        Console.WriteLine();
                        break;
                    default:
                        Log.Print(LogType.Debug, $"Unknown Command: {command}");
                        break;
                }
            }
        }

        private static void PrintHelp()
        {
            Console.Clear();
            Console.WriteLine(@"AuthServer help
Commands:
  /c 'msg'  Send Global message to players.
  /db       Reload XML.
  /gc       Show garbage collection.
  /up       Show uptime.
  /q 900    Shutdown server in 900sec = 15min. *Debug exit now!
  /help     Show this help.
");
        }
    }
}
