using System;
using System.Globalization;
using System.Net;
using System.Reflection;
using System.Threading;
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
                $"{Assembly.GetExecutingAssembly().GetName().Name} v{Assembly.GetExecutingAssembly().GetName().Version}";

            Log.Print(LogType.AuthServer, $"Version {Assembly.GetExecutingAssembly().GetName().Version}");
            Log.Print(LogType.AuthServer, $"Running on .NET Framework Version {Environment.Version}");
            //
            Initalizing();

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
                        new DatabaseManager();
                        Log.Print(LogType.Console, "Database recreated.");
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
            AuthServerRouter.AddHandler<AuthLogonChallenge>(AuthCMD.CMD_AUTH_LOGON_CHALLENGE,
                AuthServerHandler.OnAuthLogonChallenge);
            AuthServerRouter.AddHandler<AuthLogonProof>(AuthCMD.CMD_AUTH_LOGON_PROOF,
                AuthServerHandler.OnAuthLogonProof);
            AuthServerRouter.AddHandler(AuthCMD.CMD_AUTH_REALMLIST, AuthServerHandler.OnAuthRealmList);
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