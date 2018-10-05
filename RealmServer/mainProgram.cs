using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Reflection;
using System.Threading;
using Common.Database;
using Common.Database.Dbc;
using Common.Globals;
using Common.Helpers;
using Common.Network;
using RealmServer.Game.Entitys;
using RealmServer.Game.Managers;
using RealmServer.Handlers;
using RealmServer.Scripting;

namespace RealmServer
{
    internal class MainProgram
    {
        // Basic Config
        // TODO: change this to XML file
        public static int DistanceConfig = 75;
        public static int LimitCharacterPerRealm = 10;
        public static int ReclaimCorpseTime = 30;
        public static int SpeedRunMultiplier = 1;
        public static string FirstMotd = "Welcome to World of Warcraft.";
        public static string SecondMotd = $"Server uptime: {DateTime.UtcNow - Process.GetCurrentProcess().StartTime.ToUniversalTime()}";

        // ignore this
        public static int IsJitI = 1;

        public static RealmServerDatabase Database { get; set; }
        public static RealmServerClass RealmServerClass { get; set; }

        private static void Main()
        {
            // Set Culture
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-us");
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-us");

            Console.SetWindowSize(
                Math.Min(110, Console.LargestWindowWidth),
                Math.Min(20, Console.LargestWindowHeight));

            var time = Time.GetMsTime();
            var realmPoint = new IPEndPoint(IPAddress.Any, 1001);

            Console.Title = $@"{Assembly.GetExecutingAssembly().GetName().Name} v{Assembly.GetExecutingAssembly().GetName().Version}";

            Log.Print(LogType.RealmServer, $"Version {Assembly.GetExecutingAssembly().GetName().Version}");
            Log.Print(LogType.RealmServer, $"Running on .NET Framework Version {Environment.Version}");

            // Boot
            XmlReader.Boot();
            ScriptManager.Boot();
            PlayerManager.Boot();
            AiBrain.Boot();

            // Database
            Database = new RealmServerDatabase();
            DatabaseManager();

            // Initali
            IntializePacketHandlers();

            // Socket Class
            RealmServerClass = new RealmServerClass(realmPoint);

            // TODO: Set Realm to ONLINE

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
                        XmlReader.Boot();
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

        private static void IntializePacketHandlers()
        {
            // DONE HANDLERS
            RealmServerRouter.AddHandler<CmsgPing>(RealmCMD.CMSG_PING, RealmServerHandler.OnPingPacket);
            RealmServerRouter.AddHandler(RealmCMD.CMSG_CHAR_ENUM, CharacterHandler.OnCharEnum);





            // TODO: review this
            RealmServerRouter.AddHandler<CmsgAuthSession>(RealmCMD.CMSG_AUTH_SESSION, RealmServerHandler.OnAuthSession);
            


            // Player Handlers


            // Character Handlers
            
            RealmServerRouter.AddHandler<CmsgCharCreate>(RealmCMD.CMSG_CHAR_CREATE, CharacterHandler.OnCharCreate); // TODO: review
            RealmServerRouter.AddHandler<CmsgCharDelete>(RealmCMD.CMSG_CHAR_DELETE, CharacterHandler.OnCharDelete); // TODO: review
            RealmServerRouter.AddHandler<CmsgPlayerLogin>(RealmCMD.CMSG_PLAYER_LOGIN, CharacterHandler.OnPlayerLogin);
            RealmServerRouter.AddHandler<CmsgUpdateAccountData>(RealmCMD.CMSG_UPDATE_ACCOUNT_DATA, CharacterHandler.OnUpdateAccountData);

            // Misc Handlers
            RealmServerRouter.AddHandler<PacketReader>(RealmCMD.CMSG_NAME_QUERY, MiscHandler.OnNameQuery);
            RealmServerRouter.AddHandler<PacketReader>(RealmCMD.CMSG_COMPLETE_CINEMATIC, MiscHandler.OnCompleteCinematic);
            RealmServerRouter.AddHandler<PacketReader>(RealmCMD.CMSG_SET_ACTIVE_MOVER, MiscHandler.OnSetActiveMover);
            RealmServerRouter.AddHandler<PacketReader>(RealmCMD.CMSG_TUTORIAL_FLAG, MiscHandler.OnTutorialFlag);
            RealmServerRouter.AddHandler(RealmCMD.CMSG_QUERY_TIME, MiscHandler.OnQueryTime);
            RealmServerRouter.AddHandler<PacketReader>(RealmCMD.CMSG_TUTORIAL_CLEAR, MiscHandler.OnTutorialClear);
            RealmServerRouter.AddHandler<PacketReader>(RealmCMD.CMSG_TUTORIAL_RESET, MiscHandler.OnTutorialReset);
            RealmServerRouter.AddHandler<PacketReader>(RealmCMD.CMSG_BATTLEFIELD_STATUS, MiscHandler.OnBattlefieldStatus); // TODO
            RealmServerRouter.AddHandler<PacketReader>(RealmCMD.CMSG_MEETINGSTONE_INFO, MiscHandler.OnMeetingstoneInfo); // TODO

            // Chat Handler
            RealmServerRouter.AddHandler<PacketReader>(RealmCMD.CMSG_JOIN_CHANNEL, ChatHandler.OnJoinChannel);

            // Character Movement Handler
            RealmServerRouter.AddHandler<PacketReader>(RealmCMD.CMSG_ZONEUPDATE, MovementHandler.OnZoneUpdate);

            // Group Handler
            RealmServerRouter.AddHandler<PacketReader>(RealmCMD.CMSG_REQUEST_RAID_INFO, GroupHandler.OnRequestRaidInfo); // TODO

            // GM Handler
            RealmServerRouter.AddHandler<PacketReader>(RealmCMD.CMSG_GMTICKET_GETTICKET, GmHandler.OnGmTicketGetTicket); // TODO

            // Mail Handler
            RealmServerRouter.AddHandler<PacketReader>(RealmCMD.MSG_QUERY_NEXT_MAIL_TIME, MailHandler.OnQueryNextMailTime); // TODO

            // Other Handler
            RealmServerRouter.AddHandler<PacketReader>(RealmCMD.CMSG_NEXT_CINEMATIC_CAMERA, OnNull); // TODO

            /*
            // Character Handlers
            RealmServerRouter.AddHandler(RealmCMD.CMSG_CHAR_ENUM, CharacterHandler.OnCharEnum);                             // DONE
            RealmServerRouter.AddHandler<CmsgCharRename>(RealmCMD.CMSG_CHAR_RENAME, CharacterHandler.OnCharRename);         // DONE
            RealmServerRouter.AddHandler(RealmCMD.CMSG_LOGOUT_REQUEST, CharacterHandler.OnLogoutRequest);                   // PARTIAL
            RealmServerRouter.AddHandler<PacketReader>(RealmCMD.CMSG_LOGOUT_CANCEL, CharacterHandler.OnLogoutCancel);       // DONE
            RealmServerRouter.AddHandler<PacketReader>(RealmCMD.CMSG_STANDSTATECHANGE, CharacterHandler.OnStandStateChange);// PARTIAL

            // Miscs Handlers
            
            
            RealmServerRouter.AddHandler<PacketReader>(RealmCMD.CMSG_TEXT_EMOTE, MiscHandler.OnTextEmote);
            RealmServerRouter.AddHandler<PacketReader>(RealmCMD.CMSG_SET_FACTION_ATWAR, MiscHandler.OnSetFactionAtwar);         // DONE
            RealmServerRouter.AddHandler<PacketReader>(RealmCMD.CMSG_SET_FACTION_INACTIVE, MiscHandler.OnSetFactionInactive);   // DONE
            RealmServerRouter.AddHandler<PacketReader>(RealmCMD.CMSG_SET_WATCHED_FACTION, MiscHandler.OnSetWatchedFaction);     // DONE

            

            // GM Handler
            RealmServerRouter.AddHandler<PacketReader>(RealmCMD.CMSG_GMTICKET_GETTICKET, GmHandler.OnGmTicketGetTicket);
            RealmServerRouter.AddHandler<PacketReader>(RealmCMD.CMSG_GMTICKET_SYSTEMSTATUS, GmHandler.OnGmTicketSystemStatus);
            RealmServerRouter.AddHandler<PacketReader>(RealmCMD.CMSG_GMTICKET_CREATE, GmHandler.OnGmTicketCreate);

            

            // Character Movement Handler
            MovementOpcodes.ForEach(code => RealmServerRouter.AddHandler(code, MovementHandler.GenerateResponse(code)));
            RealmServerRouter.AddHandler<CmsgMoveTimeSkipped>(RealmCMD.CMSG_MOVE_TIME_SKIPPED, MovementHandler.OnMoveTimeSkipped);
            RealmServerRouter.AddHandler<MsgMoveInfo>(RealmCMD.MSG_MOVE_FALL_LAND, MovementHandler.OnMoveFallLand);
            RealmServerRouter.AddHandler<PacketReader>(RealmCMD.CMSG_ZONEUPDATE, MovementHandler.OnZoneUpdate);
            RealmServerRouter.AddHandler<PacketReader>(RealmCMD.CMSG_AREATRIGGER, MovementHandler.OnAreaTrigger);

            RealmServerRouter.AddHandler<PacketReader>(RealmCMD.CMSG_LEAVE_CHANNEL, ChatHandler.OnLeaveChannel);
            RealmServerRouter.AddHandler<PacketReader>(RealmCMD.CMSG_CHANNEL_LIST, ChatHandler.OnChannelList);
            RealmServerRouter.AddHandler<PacketReader>(RealmCMD.CMSG_CHANNEL_PASSWORD, ChatHandler.OnChannelPassword);
            RealmServerRouter.AddHandler<PacketReader>(RealmCMD.CMSG_CHANNEL_SET_OWNER, ChatHandler.OnChannelSetOwner);
            RealmServerRouter.AddHandler<PacketReader>(RealmCMD.CMSG_CHANNEL_OWNER, ChatHandler.OnChannelOwner);
            RealmServerRouter.AddHandler<PacketReader>(RealmCMD.CMSG_CHANNEL_MODERATOR, ChatHandler.OnChannelModerator);
            RealmServerRouter.AddHandler<PacketReader>(RealmCMD.CMSG_CHANNEL_UNMODERATOR, ChatHandler.OnChannelUnmoderator);
            RealmServerRouter.AddHandler<PacketReader>(RealmCMD.CMSG_CHANNEL_MUTE, ChatHandler.OnChannelMute);
            RealmServerRouter.AddHandler<PacketReader>(RealmCMD.CMSG_CHANNEL_UNMUTE, ChatHandler.OnChannelUnmute);
            RealmServerRouter.AddHandler<PacketReader>(RealmCMD.CMSG_CHANNEL_KICK, ChatHandler.OnChannelKick);
            RealmServerRouter.AddHandler<PacketReader>(RealmCMD.CMSG_CHANNEL_INVITE, ChatHandler.OnChannelInvite);
            RealmServerRouter.AddHandler<PacketReader>(RealmCMD.CMSG_CHANNEL_BAN, ChatHandler.OnChannelBan);
            RealmServerRouter.AddHandler<PacketReader>(RealmCMD.CMSG_CHANNEL_UNBAN, ChatHandler.OnChannelUnban);
            RealmServerRouter.AddHandler<PacketReader>(RealmCMD.CMSG_CHANNEL_ANNOUNCEMENTS, ChatHandler.OnChannelAnnouncements);
            RealmServerRouter.AddHandler<PacketReader>(RealmCMD.CMSG_CHANNEL_MODERATE, ChatHandler.OnChannelModerate);
            RealmServerRouter.AddHandler<PacketReader>(RealmCMD.CMSG_MESSAGECHAT, ChatHandler.OnMessageChat);

            // Trade Handler
            RealmServerRouter.AddHandler<PacketReader>(RealmCMD.CMSG_CANCEL_TRADE, TradeHandler.OnCancelTrade);

            // Combat Handler
            RealmServerRouter.AddHandler<PacketReader>(RealmCMD.CMSG_SETSHEATHED, CombatHandler.OnSetsHeathed);
            RealmServerRouter.AddHandler<PacketReader>(RealmCMD.CMSG_SET_SELECTION, CombatHandler.OnSetSelection);
            RealmServerRouter.AddHandler<PacketReader>(RealmCMD.CMSG_ATTACKSWING, CombatHandler.OnAttackSwing);
            RealmServerRouter.AddHandler<PacketReader>(RealmCMD.CMSG_ATTACKSTOP, CombatHandler.OnAttackStop);

            // Item Handler
            RealmServerRouter.AddHandler<PacketReader>(RealmCMD.CMSG_ITEM_QUERY_SINGLE, ItemHandler.OnItemQuerySingle);
            RealmServerRouter.AddHandler<PacketReader>(RealmCMD.CMSG_SWAP_INV_ITEM, ItemHandler.OnSwapInvItem);
            RealmServerRouter.AddHandler<PacketReader>(RealmCMD.CMSG_DESTROYITEM, ItemHandler.OnDestroyItem);
            RealmServerRouter.AddHandler<PacketReader>(RealmCMD.CMSG_USE_ITEM, ItemHandler.OnUseItem);
            RealmServerRouter.AddHandler<PacketReader>(RealmCMD.CMSG_AUTOEQUIP_ITEM, ItemHandler.OnAutoEquipItem);
            RealmServerRouter.AddHandler<PacketReader>(RealmCMD.CMSG_SPLIT_ITEM, ItemHandler.OnSplitItem);

            // Social Handler
            RealmServerRouter.AddHandler<PacketReader>(RealmCMD.CMSG_FRIEND_LIST, SocialHandler.OnFriendList);
            RealmServerRouter.AddHandler<PacketReader>(RealmCMD.CMSG_ADD_FRIEND, SocialHandler.OnAddFriend);
            RealmServerRouter.AddHandler<PacketReader>(RealmCMD.CMSG_ADD_IGNORE, SocialHandler.OnAddIgnore);
            RealmServerRouter.AddHandler<PacketReader>(RealmCMD.CMSG_DEL_FRIEND, SocialHandler.OnDelFriend);
            RealmServerRouter.AddHandler<PacketReader>(RealmCMD.CMSG_DEL_IGNORE, SocialHandler.OnDelIgnore);
            RealmServerRouter.AddHandler<PacketReader>(RealmCMD.CMSG_WHO, SocialHandler.OnWho);

            // Spell Handler
            RealmServerRouter.AddHandler<PacketReader>(RealmCMD.CMSG_CAST_SPELL, SpellHandler.OnCastSpell);

            
            RealmServerRouter.AddHandler<PacketReader>(RealmCMD.CMSG_FORCE_MOVE_ROOT_ACK, OnNull);   // DONE
            RealmServerRouter.AddHandler<PacketReader>(RealmCMD.CMSG_FORCE_MOVE_UNROOT_ACK, OnNull); // DONE
            */
        }

        private static void OnNull(RealmServerSession session, PacketReader handler)
        {
            Log.Print(LogType.Debug, "Null Code");
        }

        private static readonly List<RealmCMD> MovementOpcodes = new List<RealmCMD>()
        {
            RealmCMD.MSG_MOVE_HEARTBEAT,
            RealmCMD.MSG_MOVE_START_FORWARD,
            RealmCMD.MSG_MOVE_START_BACKWARD,
            RealmCMD.MSG_MOVE_STOP,
            RealmCMD.MSG_MOVE_START_STRAFE_LEFT,
            RealmCMD.MSG_MOVE_START_STRAFE_RIGHT,
            RealmCMD.MSG_MOVE_STOP_STRAFE,
            RealmCMD.MSG_MOVE_JUMP,
            RealmCMD.MSG_MOVE_START_TURN_LEFT,
            RealmCMD.MSG_MOVE_START_TURN_RIGHT,
            RealmCMD.MSG_MOVE_STOP_TURN,
            RealmCMD.MSG_MOVE_START_PITCH_UP,
            RealmCMD.MSG_MOVE_START_PITCH_DOWN,
            RealmCMD.MSG_MOVE_STOP_PITCH,
            RealmCMD.MSG_MOVE_SET_RUN_MODE,
            RealmCMD.MSG_MOVE_SET_WALK_MODE,
            RealmCMD.MSG_MOVE_SET_FACING,
            RealmCMD.MSG_MOVE_SET_PITCH
        };

        public static readonly AreaTableReader AreaTableReader = new AreaTableReader();
        public static readonly CharStartOutfitReader CharacterOutfitReader = new CharStartOutfitReader();
        public static readonly ChrRacesReader ChrRacesReader = new ChrRacesReader();
        public static readonly EmotesTextReader EmotesTextReader = new EmotesTextReader();
        public static readonly FactionReader FactionReader = new FactionReader();
        public static readonly MapReader MapReader = new MapReader();

        private static async void DatabaseManager()
        {
            Log.Print(LogType.Loading, "Loading DBCs ......................... [OK]");
            await AreaTableReader.Load("AreaTable.dbc");
            await CharacterOutfitReader.Load("CharStartOutfit.dbc");
            await ChrRacesReader.Load("ChrRaces.dbc");
            await EmotesTextReader.Load("EmotesText.dbc");
            await FactionReader.Load("Faction.dbc");
            await MapReader.Load("Map.dbc");
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
