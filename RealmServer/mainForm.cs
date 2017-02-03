using System;
using System.Net;
using System.Reflection;
using System.Windows.Forms;
using Common.Globals;
using Common.Helpers;
using RealmServer.Handlers;
using Common.Database.Dbc;

namespace RealmServer
{
    public partial class MainForm : Form
    {
        public static RealmServerDatabase Database { get; set; }

        public MainForm()
        {
            var time = Time.getMSTime();
            var realmPoint = new IPEndPoint(IPAddress.Any, 1001);

            InitializeComponent();
            Win32.AllocConsole();
            Text = $@"{Assembly.GetExecutingAssembly().GetName().Name} v{Assembly.GetExecutingAssembly().GetName().Version}";

            // Add columns
            listView1.Columns.Add("Id", -2, HorizontalAlignment.Left);
            listView1.Columns.Add("Name", 100, HorizontalAlignment.Left);
            listView1.Columns.Add("Char", 120, HorizontalAlignment.Left);
            listView1.Columns.Add("Location", -2, HorizontalAlignment.Left);

            Log.Print(LogType.RealmServer, $"World of Warcraft (Realm Server/World Server)");
            Log.Print(LogType.RealmServer, $"Supported WoW Client 1.2.1");
            Log.Print(LogType.RealmServer, $"Version {Assembly.GetExecutingAssembly().GetName().Version}");
            Log.Print(LogType.RealmServer, $"Running on .NET Framework Version {Environment.Version}");

            var realmServerClass = new RealmServerClass(realmPoint);

            Database = new RealmServerDatabase();
            DatabaseManager();

            RealmServerRouter.AddHandler<CmsgAuthSession>(RealmCMD.CMSG_AUTH_SESSION, RealmServerHandler.OnAuthSession);
            RealmServerRouter.AddHandler<CmsgPing>(RealmCMD.CMSG_PING, RealmServerHandler.OnPingPacket);

            RealmServerRouter.AddHandler(RealmCMD.CMSG_CHAR_ENUM, CharacterHandler.OnCharEnum);
            RealmServerRouter.AddHandler<CmsgCharCreate>(RealmCMD.CMSG_CHAR_CREATE, CharacterHandler.OnCharCreate);

            Log.Print(LogType.RealmServer, $"Successfully started in {Time.getMSTimeDiff(time, Time.getMSTime()) / 1000}s");
        }

        public readonly AreaTableReader AreaTableReader = new AreaTableReader();
        public readonly CharStartOutfitReader CharacterOutfitReader = new CharStartOutfitReader();
        public readonly FactionReader FactionReader = new FactionReader();

        public async void DatabaseManager()
        {
            Log.Print(LogType.RealmServer, $"Loading DBCs...");
            await CharacterOutfitReader.Load("CharStartOutfit.dbc");
            await AreaTableReader.Load("AreaTable.dbc");
            await FactionReader.Load("Faction.dbc");

            for (int i = 0; i < 69; i++)
            {
                var faction = FactionReader.GetFaction(i);
                if (faction != null)
                {
                    for (int ai = 0; ai < 3; ai++)
                    {
                        if (HaveFlag(faction.Flags[ai], 1 - 1))
                        {
                            Console.WriteLine($@"- Flag [ {faction.ReputationFlags[ai].ToString().PadRight(5, ' ')}] Stat [ {(faction.ReputationStats[ai]).ToString().PadRight(6, ' ')}] - FactionID: {faction.FactionId.ToString().PadRight(5, '.')} = {faction.FactionName}");
                        }
                    }
                }
            }
        }

        public bool HaveFlag(int value, byte flagPos)
        {
            value = value >> flagPos;
            value = value % 2;

            if (value == 1) return true;

            return false;
        }
    }
}
