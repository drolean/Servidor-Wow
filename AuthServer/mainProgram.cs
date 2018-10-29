using System;
using System.Globalization;
using System.Net;
using System.Reflection;
using System.Threading;
using AuthServer.Handlers;
using AuthServer.PacketReader;
using Common.Database;
using Common.Globals;
using Common.Helpers;

namespace AuthServer
{
    internal class MainProgram
    {
        private static bool _keepGoing = true;
        private static readonly uint Time = Common.Helpers.Time.GetMsTime();
        private static readonly IPEndPoint AuthPoint = new IPEndPoint(IPAddress.Any, 3724);
        public static AuthServerDatabase Database { get; set; }
        public static AuthServerClass AuthServerClass { get; set; }
        public static Timer TimerRealm { get; private set; }
        public static int Count { get; set; }

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
                $"{Assembly.GetExecutingAssembly().GetName().Name} v{Assembly.GetExecutingAssembly().GetName().Version}";

            Log.Print(LogType.AuthServer, $"Version {Assembly.GetExecutingAssembly().GetName().Version}");
            Log.Print(LogType.AuthServer, $"Running on .NET Framework Version {Environment.Version}");

            //
            Initalizing();

            // Timer to check realm Status
            //TimerRealm = new Timer(TimerRealmCallback, null, 0, 10000);

            Log.Print(LogType.AuthServer, $"Running from: {AppDomain.CurrentDomain.BaseDirectory}");
            Log.Print(LogType.AuthServer,
                $"Successfully started in {Common.Helpers.Time.GetMsTimeDiff(Time, Common.Helpers.Time.GetMsTime()) / 100}ms");

            // Commands
            while (_keepGoing)
            {
                var command = Console.ReadLine();
                switch (command)
                {
                    case "/db":
                    case "db":
                        // ReSharper disable once ObjectCreationAsStatement
                        new DatabaseModel();
                        Log.Print(LogType.Console, "Database recreated ".PadRight(40, '.') + " [OK] ");
                        break;

                    case "/gc":
                    case "gc":
                        GC.Collect();
                        Log.Print(LogType.Console,
                            $"Total Memory: {Convert.ToSingle(GC.GetTotalMemory(false) / 1024 / 1024)}MB");
                        break;

                    case "/q":
                    case "q":
                        _keepGoing = false;
                        break;
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
            AuthServerClass = new AuthServerClass(AuthPoint);
            Database = new AuthServerDatabase();

            //
            AuthServerRouter.AddHandler<CMD_AUTH_LOGON_CHALLENGE>(AuthCMD.CMD_AUTH_LOGON_CHALLENGE, OnAuthLogonChallenge.Handler);
            AuthServerRouter.AddHandler<CMD_AUTH_LOGON_PROOF>(AuthCMD.CMD_AUTH_LOGON_PROOF, OnAuthLogonProof.Handler);
            AuthServerRouter.AddHandler(AuthCMD.CMD_AUTH_REALMLIST, OnAuthRealmList.Handler);
            //AuthServerRouter.AddHandler<Absbs>(AuthCMD.CLASSIC_LOGON, Handler);
        }
        
        private static void TimerRealmCallback(object o)
        {
            Log.Print(LogType.AuthServer, "Checking Realm Status ".PadRight(40, '.'));
            var realms = Database.GetRealms();
            foreach (var realm in realms) AuthServerHelper.CheckRealmStatus(realm);
            GC.Collect();
        }

        /// <summary>
        ///     Print the console help.
        /// </summary>
        private static void PrintHelp()
        {
            Console.Clear();
            Console.WriteLine(@"AuthServer help
Commands:
  /db    Recreate database.
  /gc    Show garbage collection.
  /q     Exit application.
  /help  Show this help.");
        }
    }
}
