using System;
using System.Net;
using System.Reflection;
using Common.Globals;
using Common.Helpers;
using Common.Database;

namespace AuthServer
{
    class MainProgram
    {
        public static AuthServerDatabase Database { get; set; }

        static void Main()
        {
            var time = Time.getMSTime();
            var authPoint = new IPEndPoint(IPAddress.Any, 3724);

            bool quitNow = false;
            Console.Title = $"{Assembly.GetExecutingAssembly().GetName().Name} v{Assembly.GetExecutingAssembly().GetName().Version}";

            Log.Print(LogType.AuthServer, $"Version {Assembly.GetExecutingAssembly().GetName().Version}");
            Log.Print(LogType.AuthServer, $"Running on .NET Framework Version {Environment.Version}");

            var authServerClass = new AuthServerClass(authPoint);

            Database = new AuthServerDatabase();

            // recreate database***
            new DatabaseManager();

            //
            AuthServerRouter.AddHandler<AuthLogonChallenge>(AuthCMD.CMD_AUTH_LOGON_CHALLENGE, AuthServerHandler.OnAuthLogonChallenge);
            AuthServerRouter.AddHandler<AuthLogonProof>(AuthCMD.CMD_AUTH_LOGON_PROOF, AuthServerHandler.OnAuthLogonProof);
            AuthServerRouter.AddHandler(AuthCMD.CMD_AUTH_REALMLIST, AuthServerHandler.OnAuthRealmList);


            Log.Print(LogType.AuthServer,
                $"Successfully started in {Time.getMSTimeDiff(time, Time.getMSTime()) / 1000}s");

            // Commands
            while (!quitNow)
            {
                var command = Console.ReadLine();
                switch (command)
                {
                    case "/gc":
                        GC.Collect();
                        Log.Print(LogType.Console,
                            $"Total Memory: {Convert.ToSingle(GC.GetTotalMemory(false) / 1024 / 1024)}MB");
                        break;

                    case "/q":
                        quitNow = true;
                        break;

                    default:
                        Log.Print(LogType.Console, $"Unknown Command: {command}");
                        break;
                }
            }
        }
    }
}
