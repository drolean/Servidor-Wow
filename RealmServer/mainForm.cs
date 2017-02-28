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
using Common.Network;
using RealmServer.Scripting;

namespace RealmServer
{
    public partial class MainForm : Form
    {
        public static RealmServerDatabase Database { get; set; }

        public MainForm()
        {
            var time = Time.GetMsTime();
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

            //
            XmlReader.Boot();

            //
            ScriptManager.Boot();

            RealmServerRouter.AddHandler<CmsgAuthSession>(RealmCMD.CMSG_AUTH_SESSION, RealmServerHandler.OnAuthSession);
            RealmServerRouter.AddHandler<CmsgPing>(RealmCMD.CMSG_PING, RealmServerHandler.OnPingPacket);

            // Character Handlers
            RealmServerRouter.AddHandler(RealmCMD.CMSG_CHAR_ENUM, CharacterHandler.OnCharEnum);
            RealmServerRouter.AddHandler<CmsgCharCreate>(RealmCMD.CMSG_CHAR_CREATE, CharacterHandler.OnCharCreate);
            RealmServerRouter.AddHandler<CmsgCharRename>(RealmCMD.CMSG_CHAR_RENAME, CharacterHandler.OnCharRename);
            RealmServerRouter.AddHandler<CmsgCharDelete>(RealmCMD.CMSG_CHAR_DELETE, CharacterHandler.OnCharDelete);
            RealmServerRouter.AddHandler<CmsgPlayerLogin>(RealmCMD.CMSG_PLAYER_LOGIN, CharacterHandler.OnPlayerLogin);
            RealmServerRouter.AddHandler<CmsgUpdateAccountData>(RealmCMD.CMSG_UPDATE_ACCOUNT_DATA, CharacterHandler.OnUpdateAccountData);
            RealmServerRouter.AddHandler(RealmCMD.CMSG_LOGOUT_REQUEST, CharacterHandler.OnLogoutRequest);
            RealmServerRouter.AddHandler<PacketReader>(RealmCMD.CMSG_LOGOUT_CANCEL, CharacterHandler.OnLogoutCancel);
            RealmServerRouter.AddHandler<PacketReader>(RealmCMD.CMSG_STANDSTATECHANGE, CharacterHandler.OnStandStateChange); 

            // Miscs Handlers
            RealmServerRouter.AddHandler<PacketReader>(RealmCMD.CMSG_NAME_QUERY, MiscHandler.OnNameQuery);
            RealmServerRouter.AddHandler<PacketReader>(RealmCMD.CMSG_SET_ACTIVE_MOVER, MiscHandler.OnSetActiveMover);
            RealmServerRouter.AddHandler(RealmCMD.CMSG_QUERY_TIME, MiscHandler.OnQueryTime);
            RealmServerRouter.AddHandler<PacketReader>(RealmCMD.CMSG_BATTLEFIELD_STATUS, MiscHandler.OnBattlefieldStatus);
            RealmServerRouter.AddHandler<PacketReader>(RealmCMD.CMSG_MEETINGSTONE_INFO, MiscHandler.OnMeetingstoneInfo);
            RealmServerRouter.AddHandler<PacketReader>(RealmCMD.CMSG_TEXT_EMOTE, MiscHandler.OnTextEmote);
            RealmServerRouter.AddHandler<PacketReader>(RealmCMD.CMSG_SET_FACTION_ATWAR, MiscHandler.OnSetFactionAtwar);
            RealmServerRouter.AddHandler<PacketReader>(RealmCMD.CMSG_SET_FACTION_INACTIVE, MiscHandler.OnSetFactionInactive);
            RealmServerRouter.AddHandler<PacketReader>(RealmCMD.CMSG_SET_WATCHED_FACTION, MiscHandler.OnSetWatchedFaction);
            RealmServerRouter.AddHandler<PacketReader>(RealmCMD.CMSG_COMPLETE_CINEMATIC, MiscHandler.OnCompleteCinematic); 

            // Group Handler
            RealmServerRouter.AddHandler<PacketReader>(RealmCMD.CMSG_REQUEST_RAID_INFO, GroupHandler.OnRequestRaidInfo);

            // GM Handler
            RealmServerRouter.AddHandler<PacketReader>(RealmCMD.CMSG_GMTICKET_GETTICKET, GmHandler.OnGmTicketGetTicket);
            RealmServerRouter.AddHandler<PacketReader>(RealmCMD.CMSG_GMTICKET_SYSTEMSTATUS, GmHandler.OnGmTicketSystemStatus);
            RealmServerRouter.AddHandler<PacketReader>(RealmCMD.CMSG_GMTICKET_CREATE, GmHandler.OnGmTicketCreate); 

            // Mail Handler
            RealmServerRouter.AddHandler<PacketReader>(RealmCMD.MSG_QUERY_NEXT_MAIL_TIME, MailHandler.OnQueryNextMailTime);

            // Character Movement Handler
            MovementOpcodes.ForEach(code => RealmServerRouter.AddHandler(code, MovementHandler.GenerateResponse(code)));
            RealmServerRouter.AddHandler<CmsgMoveTimeSkipped>(RealmCMD.CMSG_MOVE_TIME_SKIPPED, MovementHandler.OnMoveTimeSkipped);
            RealmServerRouter.AddHandler<PacketReader>(RealmCMD.MSG_MOVE_FALL_LAND, MovementHandler.OnMoveFallLand);
            RealmServerRouter.AddHandler<PacketReader>(RealmCMD.CMSG_ZONEUPDATE, MovementHandler.OnZoneUpdate);
            RealmServerRouter.AddHandler<PacketReader>(RealmCMD.CMSG_AREATRIGGER, MovementHandler.OnAreaTrigger);

            // Chat Handler
            RealmServerRouter.AddHandler<PacketReader>(RealmCMD.CMSG_JOIN_CHANNEL, ChatHandler.OnJoinChannel);
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

            // Item Handler
            RealmServerRouter.AddHandler<PacketReader>(RealmCMD.CMSG_ITEM_QUERY_SINGLE, ItemHandler.OnItemQuerySingle);

            // Social Handler
            RealmServerRouter.AddHandler<PacketReader>(RealmCMD.CMSG_FRIEND_LIST, SocialHandler.OnFriendList);
            RealmServerRouter.AddHandler<PacketReader>(RealmCMD.CMSG_ADD_FRIEND, SocialHandler.OnAddFriend);
            RealmServerRouter.AddHandler<PacketReader>(RealmCMD.CMSG_ADD_IGNORE, SocialHandler.OnAddIgnore);
            RealmServerRouter.AddHandler<PacketReader>(RealmCMD.CMSG_DEL_FRIEND, SocialHandler.OnDelFriend);
            RealmServerRouter.AddHandler<PacketReader>(RealmCMD.CMSG_DEL_IGNORE, SocialHandler.OnDelIgnore);
            RealmServerRouter.AddHandler<PacketReader>(RealmCMD.CMSG_WHO, SocialHandler.OnWho);

            // Spell Handler
            RealmServerRouter.AddHandler<PacketReader>(RealmCMD.CMSG_CAST_SPELL, SpellHandler.OnCastSpell);

            Log.Print(LogType.RealmServer,
                $"Successfully started in {Time.GetMsTimeDiff(time, Time.GetMsTime()) / 1000}s");
        }

        public sealed override string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
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

        public readonly AreaTableReader AreaTableReader = new AreaTableReader();
        public static readonly CharStartOutfitReader CharacterOutfitReader = new CharStartOutfitReader();
        public static readonly FactionReader FactionReader = new FactionReader();
        public static readonly ChrRacesReader ChrRacesReader = new ChrRacesReader();

        public async void DatabaseManager()
        {
            Log.Print(LogType.RealmServer, $"Loading DBCs...");
            await CharacterOutfitReader.Load("CharStartOutfit.dbc");
            await AreaTableReader.Load("AreaTable.dbc");
            await FactionReader.Load("Faction.dbc");
            await ChrRacesReader.Load("ChrRaces.dbc");
        }
    }
}