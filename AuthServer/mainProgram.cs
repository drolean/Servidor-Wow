using System;
using System.Net;
using System.Reflection;
using Common.Database;
using Common.Globals;
using Common.Helpers;

namespace AuthServer
{
    internal class MainProgram
    {
        public static AuthServerDatabase Database { get; set; }

        private static void Main()
        {
            var time = Time.GetMsTime();
            var authPoint = new IPEndPoint(IPAddress.Any, 3724);

            bool quitNow = false;
            Console.Title = $"{Assembly.GetExecutingAssembly().GetName().Name} v{Assembly.GetExecutingAssembly().GetName().Version}";

            Log.Print(LogType.AuthServer, $"Version {Assembly.GetExecutingAssembly().GetName().Version}");
            Log.Print(LogType.AuthServer, $"Running on .NET Framework Version {Environment.Version}");

            // ReSharper disable once UnusedVariable
            var authServerClass = new AuthServerClass(authPoint);

            Database = new AuthServerDatabase();

            //
            AuthServerRouter.AddHandler<AuthLogonChallenge>(AuthCMD.CMD_AUTH_LOGON_CHALLENGE, AuthServerHandler.OnAuthLogonChallenge);
            AuthServerRouter.AddHandler<AuthLogonProof>(AuthCMD.CMD_AUTH_LOGON_PROOF, AuthServerHandler.OnAuthLogonProof);
            AuthServerRouter.AddHandler(AuthCMD.CMD_AUTH_REALMLIST, AuthServerHandler.OnAuthRealmList);

            Log.Print(LogType.AuthServer,
                $"Successfully started in {Time.GetMsTimeDiff(time, Time.GetMsTime()) / 1000}s");

            // Commands
            while (!quitNow)
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
                        quitNow = true;
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

        private static void PrintHelp()
        {
            Console.Clear();
            Console.WriteLine("AuthServer help\n");
            Console.WriteLine("Commands: \n");
            Console.WriteLine("\t/db\t\t\tRecreate database.");
            Console.WriteLine("\t/gc\t\t\tShow garbage collection.");
            Console.WriteLine("\t/q\t\t\tExit application.");
            Console.WriteLine("\t/help\t\t\tShow this help.");
        }
    }
}
