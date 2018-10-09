using System;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Reflection;
using System.Threading;
using Common.Globals;
using Common.Helpers;
using RealmServer.Handlers;
using RealmServer.PacketReader;

namespace RealmServer
{
    internal class MainProgram
    {
        private static bool _keepGoing = true;
        private static readonly uint Time = Common.Helpers.Time.GetMsTime();
        private static readonly IPEndPoint RealmPoint = new IPEndPoint(IPAddress.Any, 1001);
        public static RealmServerClass RealmServerClass { get; set; }
        public static RealmServerDatabase RealmServerDatabase { get; set; }

        private static void Main()
        {
            // Set Culture
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

            Console.SetWindowSize(
                Math.Min(110, Console.LargestWindowWidth),
                Math.Min(20, Console.LargestWindowHeight)
            );
            Console.Title =
                $@"{Assembly.GetExecutingAssembly().GetName().Name} v{
                        Assembly.GetExecutingAssembly().GetName().Version
                    }";

            Log.Print(LogType.RealmServer, $"Version {Assembly.GetExecutingAssembly().GetName().Version}");
            Log.Print(LogType.RealmServer, $"Running on .NET Framework Version {Environment.Version}");

            ConfigFile();
            //
            Initalizing();

            Log.Print(LogType.RealmServer, $"Running from: {AppDomain.CurrentDomain.BaseDirectory}");
            Log.Print(LogType.RealmServer,
                $"Successfully started in {Common.Helpers.Time.GetMsTimeDiff(Time, Common.Helpers.Time.GetMsTime()) / 100}ms");


            // Commands
            while (_keepGoing)
            {
                var command = Console.ReadLine();
                switch (command)
                {
                    case "/config":
                    case "config":
                        ConfigFile(true);
                        break;
                    case "/up":
                    case "up":
                        Log.Print(LogType.Console,
                            $"Uptime {DateTime.UtcNow - Process.GetCurrentProcess().StartTime.ToUniversalTime()}");
                        break;

                    case "/db":
                    case "db":
                        Log.Print(LogType.Console, "XML reloaded.");
                        break;

                    case "/gc":
                    case "gc":
                        Console.Clear();
                        GC.Collect();
                        Log.Print(LogType.Console,
                            $"Total Memory: {Convert.ToSingle(GC.GetTotalMemory(false) / 1024 / 1024)}MB");
                        break;

                    case "/q":
                    case "q":
                        Log.Print(LogType.Console, "Halting process...");
                        Thread.Sleep(500);
                        _keepGoing = false;
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

        private static void Initalizing()
        {
            RealmServerClass = new RealmServerClass(RealmPoint);
            RealmServerDatabase = new RealmServerDatabase();

            // Handlers
            RealmServerRouter.AddHandler<CMSG_AUTH_SESSION>(RealmEnums.CMSG_AUTH_SESSION, OnAuthSession.Handler);
            RealmServerRouter.AddHandler<CMSG_PING>(RealmEnums.CMSG_PING, OnPing.Handler);
            RealmServerRouter.AddHandler<CMSG_CHAR_CREATE>(RealmEnums.CMSG_CHAR_CREATE, OnCharCreate.Handler);
        }

        private static void ConfigFile(bool reload = false)
        {
            Log.Print(LogType.RealmServer, reload ? "Reloading settings..." : "Loading settings...");

            try
            {
                Config.Load();
            }
            catch (Exception)
            {
                Log.Print(LogType.RealmServer, "Failed to load config from file. Loading default config.");
                Config.Default();
                Log.Print(LogType.RealmServer, "Saving config...");
                Config.Instance.Save();
            }
        }

        private static void PrintHelp()
        {
            Console.Clear();
            Console.WriteLine(@"AuthServer help
Commands:
  /config   Reload configuration file.
  /c 'msg'  Send Global message to players.
  /db       Reload XML.
  /gc       Show garbage collection.
  /up       Show uptime.
  /q 900    Shutdown server in 900sec = 15min. *Debug exit now!
  /help     Show this help.");
        }
    }
}