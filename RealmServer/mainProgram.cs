using System;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Reflection;
using System.Threading;
using Common.Database.Dbc;
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

        public static readonly AreaTableReader AreaTableReader = new AreaTableReader();
        public static readonly CharStartOutfitReader CharacterOutfitReader = new CharStartOutfitReader();
        public static readonly ChrRacesReader ChrRacesReader = new ChrRacesReader();
        public static readonly EmotesTextReader EmotesTextReader = new EmotesTextReader();
        public static readonly FactionReader FactionReader = new FactionReader();
        public static readonly MapReader MapReader = new MapReader();
        public static RealmServerClass RealmServerClass { get; set; }
        public static RealmServerDatabase RealmServerDatabase { get; set; }

        private static void Main()
        {
            // Set Culture
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;

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
            DbcInit();
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

                    case "/reload":
                    case "reload":
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
                        Log.Print(LogType.Console, "Halting process ".PadRight(40, '.'));
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
            RealmServerRouter.AddHandler(RealmEnums.CMSG_CHAR_ENUM, OnCharEnum.Handler);
            RealmServerRouter.AddHandler<CMSG_CHAR_CREATE>(RealmEnums.CMSG_CHAR_CREATE, OnCharCreate.Handler);
            RealmServerRouter.AddHandler<CMSG_CHAR_DELETE>(RealmEnums.CMSG_CHAR_DELETE, OnCharDelete.Handler);
            RealmServerRouter.AddHandler<CMSG_PLAYER_LOGIN>(RealmEnums.CMSG_PLAYER_LOGIN, OnPlayerLogin.Handler);
        }

        private static async void DbcInit()
        {
            Log.Print(LogType.RealmServer, "Loading DBCs ".PadRight(40, '.') + " [OK] ");
            await AreaTableReader.Load("AreaTable.dbc");
            await CharacterOutfitReader.Load("CharStartOutfit.dbc");
            await ChrRacesReader.Load("ChrRaces.dbc");
            await EmotesTextReader.Load("EmotesText.dbc");
            await FactionReader.Load("Faction.dbc");
            await MapReader.Load("Map.dbc");
        }

        private static void ConfigFile(bool reload = false)
        {
            Log.Print(LogType.RealmServer,
                reload
                    ? "Reloading settings ".PadRight(40, '.') + " [OK]"
                    : "Loading settings ".PadRight(40, '.') + " [OK] ");

            try
            {
                Config.Load();
            }
            catch (Exception)
            {
                Log.Print(LogType.RealmServer, "Failed to load config from file. Loading default config.");
                Config.Default();
                Log.Print(LogType.RealmServer, "Saving config ".PadRight(40, '.') + " [OK] ");
                Config.Instance.Save();
            }
        }

        private static void PrintHelp()
        {
            Console.Clear();
            Console.WriteLine(@"RealmServer help
Commands:
  /config   Reload configuration file.
  /g 'msg'  Send Global message to players.
  /reload   Reload XML.
  /gc       Show garbage collection.
  /up       Show uptime.
  /q 900    Shutdown server in 900sec = 15min. *Debug exit now!
  /help     Show this help.");
        }
    }
}