using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Windows.Forms;
using Common.Database;
using Common.Globals;
using Common.Helpers;
using RealmServer.Handlers;
using Common.Database.Dbc;
using Shaolinq;

namespace RealmServer
{
    public partial class MainForm : Form
    {
        public static RealmServerDatabase Database { get; set; }

        public static TestCreate TestCreate;

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

            XmlReader.Boot();

            RealmServerRouter.AddHandler<CmsgAuthSession>(RealmCMD.CMSG_AUTH_SESSION, RealmServerHandler.OnAuthSession);
            RealmServerRouter.AddHandler<CmsgPing>(RealmCMD.CMSG_PING, RealmServerHandler.OnPingPacket);

            RealmServerRouter.AddHandler(RealmCMD.CMSG_CHAR_ENUM, CharacterHandler.OnCharEnum);
            RealmServerRouter.AddHandler<CmsgCharCreate>(RealmCMD.CMSG_CHAR_CREATE, CharacterHandler.OnCharCreate);
            // CMSG_CHAR_RENAME

            Log.Print(LogType.RealmServer,
                $"Successfully started in {Time.getMSTimeDiff(time, Time.getMSTime()) / 1000}s");
        }

        public readonly AreaTableReader AreaTableReader = new AreaTableReader();
        public static readonly CharStartOutfitReader CharacterOutfitReader = new CharStartOutfitReader();
        public static readonly FactionReader FactionReader = new FactionReader();

        public async void DatabaseManager()
        {
            Log.Print(LogType.RealmServer, $"Loading DBCs...");
            await CharacterOutfitReader.Load("CharStartOutfit.dbc");
            await AreaTableReader.Load("AreaTable.dbc");
            await FactionReader.Load("Faction.dbc");

            TestCreate = new TestCreate();
            TestCreate.CreateChar();
        }
    }

    public class TestCreate : DatabaseModel<Models>
    {
        public void CreateChar()
        {
            try
            {
                using (var scope = new DataAccessScope())
                {
                    var initRace = XmlReader.GetRace(Races.RACE_HUMAN);

                    // Salva Char
                    var Char = Model.Characters.Create();
                        Char.user = MainForm.Database.GetAccount("doe");
                        Char.name = "Abacate";
                        Char.race = Races.RACE_HUMAN;
                        Char.classe = Classes.CLASS_PALADIN;
                        Char.gender = Genders.GENDER_FEMALE;
                        Char.created_at = DateTime.Now;

                    // Factions 
                    var initiFactions = MainForm.FactionReader.GenerateFactions(Races.RACE_HUMAN);

                    foreach (var valFaction in initiFactions)
                    {
                        string[] fac = valFaction.Split(',');

                        var charFactions = Model.CharactersFactions.Create();
                        charFactions.character = Char;
                        charFactions.faction = Int32.Parse(fac[0]);
                        charFactions.flags = Int32.Parse(fac[1]);
                        charFactions.standing = Int32.Parse(fac[2]);
                        charFactions.created_at = DateTime.Now;
                    }

                    // Set Player Create Items
                    CharStartOutfit startItems = MainForm.CharacterOutfitReader.Get(Classes.CLASS_PALADIN, Races.RACE_HUMAN, Genders.GENDER_FEMALE);

                    if (startItems == null)
                        return;

                    foreach (var VARIABLE in startItems.Items)
                    {
                        Console.WriteLine(VARIABLE);
                    }

                    for (int j = 0; j < 12; ++j)
                    {
                        if (startItems.Items[j] <= 0)
                            continue;

                        var item = XmlReader.GetItem(startItems.Items[j]);

                        if (item == null)
                            continue;

                        var charInventory = Model.CharactersInventorys.Create();
                        charInventory.character = Char;
                        charInventory.item = (ulong)item.id;
                        charInventory.stack = 1;
                        //charInventory.slot = PrefInvSlot(item);
                        charInventory.created_at = DateTime.Now;
                    }

                    scope.Complete();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
