using System;
using System.Text;
using Common.Globals;
using Common.Helpers;
using Common.Network;

namespace RealmServer.Handlers
{

    #region SMSG_CHANNEL_NOTIFY
    internal sealed class SmsgChannelNotify : PacketServer
    {
        public SmsgChannelNotify(ChatChannelNotify type, ulong guid, string channelName) : base(RealmCMD.SMSG_CHANNEL_NOTIFY)
        {
            Write((byte) type);
            Write(Encoding.UTF8.GetBytes(channelName + '\0'));
            Write(guid);
        }
    }
    #endregion

    #region SMSG_MESSAGECHAT
    internal sealed class SmsgMessagechat : PacketServer
    {
        public SmsgMessagechat(ChatMessageType type, ChatMessageLanguage language, ulong id, string message, string channelName = null) : base(RealmCMD.SMSG_MESSAGECHAT)
        {
            Write((byte)type);
            Write((uint)language);

            switch (type)
            {
                case ChatMessageType.CHAT_MSG_CHANNEL:
                    Write(Encoding.UTF8.GetBytes(channelName + '\0'));
                    Write((uint)0);
                    Write((ulong)id);
                    break;
                case ChatMessageType.CHAT_MSG_YELL:
                case ChatMessageType.CHAT_MSG_SAY:
                case ChatMessageType.CHAT_MSG_PARTY:
                    Write((ulong)id);
                    Write((ulong)id);
                    break;
                case ChatMessageType.CHAT_MSG_SYSTEM:
                case ChatMessageType.CHAT_MSG_EMOTE:
                case ChatMessageType.CHAT_MSG_IGNORED:
                case ChatMessageType.CHAT_MSG_SKILL:
                case ChatMessageType.CHAT_MSG_OFFICER:
                case ChatMessageType.CHAT_MSG_RAID:
                case ChatMessageType.CHAT_MSG_WHISPER_INFORM:
                case ChatMessageType.CHAT_MSG_GUILD:
                case ChatMessageType.CHAT_MSG_WHISPER:
                case ChatMessageType.CHAT_MSG_AFK:
                case ChatMessageType.CHAT_MSG_DND:
                case ChatMessageType.CHAT_MSG_RAID_LEADER:
                case ChatMessageType.CHAT_MSG_RAID_WARNING:
                    Write((ulong)id);
                    break;
                default:
                    Log.Print(LogType.Debug, $"Unknown chat message type - {type}!");
                    break;
            }

            Write((uint) message.Length + 1);
            Write(Encoding.UTF8.GetBytes(message + '\0'));
            Write((byte) 0); // Flag????
        }
    }
    #endregion

    internal class ChatHandler
    {
        /**
         * Aqui precisa fazer a verificação do Join e do Left
         * 
         ************ OnJoin
         * Check if Already joined
         * Check if banned
         * Check for password
         * Joined channel
         * You Joined channel
         * If new channel, set owner
         * Update flags
         * 
         ************ OnLeft
         * Check if not on this channel
         * You Left channel
         * Left channel
         * Set new owner
         * If free and not global - clear channel
         * 
         ************ OnKick
         * Check if owner
         * Check if moderator
         * You Left channel
         * [%s] Player %s kicked by %s.
         * 
         ************ OnBan 
         * Check if owner
         * Check if moderator
         * [%s] Player %s banned by %s.
         * You Left channel
         * 
         ************ OnUnBan 
         * [%s] Player %s unbanned by %s.
         * 
         ************ OnList
         * 
         ************ OnInvite
         * 
         ************ OnSetOwner
         */

        internal static void OnJoinChannel(RealmServerSession session, PacketReader handler)
        {
            string channelName = handler.ReadCString();
            string password = handler.ReadCString();

            Log.Print(LogType.Debug, $"[{session.ConnectionRemoteIp}] Channel Enter [{session.Character.name} ({channelName}) '{password}']");

            // Adicionar a base de dados
            session.SendPacket(new SmsgChannelNotify(ChatChannelNotify.CHAT_YOU_JOINED_NOTICE, (ulong)session.Character.Id, channelName));
        }

        internal static void OnLeaveChannel(RealmServerSession session, PacketReader handler)
        {
            string channelName = handler.ReadCString();

            // Adicionar a base de dados
            session.SendPacket(new SmsgChannelNotify(ChatChannelNotify.CHAT_YOU_LEFT_NOTICE, (ulong)session.Character.Id, channelName));
        }

        internal static void OnChannelList(RealmServerSession session, PacketReader handler)
        {
            throw new NotImplementedException();
        }

        internal static void OnChannelPassword(RealmServerSession session, PacketReader handler)
        {
            throw new NotImplementedException();
        }

        internal static void OnChannelOwner(RealmServerSession session, PacketReader handler)
        {
            throw new NotImplementedException();
        }

        internal static void OnChannelSetOwner(RealmServerSession session, PacketReader handler)
        {
            throw new NotImplementedException();
        }

        internal static void OnChannelModerator(RealmServerSession session, PacketReader handler)
        {
            throw new NotImplementedException();
        }

        internal static void OnChannelMute(RealmServerSession session, PacketReader handler)
        {
            throw new NotImplementedException();
        }

        internal static void OnChannelUnmoderator(RealmServerSession session, PacketReader handler)
        {
            throw new NotImplementedException();
        }

        internal static void OnChannelUnmute(RealmServerSession session, PacketReader handler)
        {
            throw new NotImplementedException();
        }

        internal static void OnChannelKick(RealmServerSession session, PacketReader handler)
        {
            throw new NotImplementedException();
        }

        internal static void OnChannelInvite(RealmServerSession session, PacketReader handler)
        {
            throw new NotImplementedException();
        }

        internal static void OnChannelBan(RealmServerSession session, PacketReader handler)
        {
            throw new NotImplementedException();
        }

        internal static void OnChannelUnban(RealmServerSession session, PacketReader handler)
        {
            throw new NotImplementedException();
        }

        internal static void OnChannelModerate(RealmServerSession session, PacketReader handler)
        {
            throw new NotImplementedException();
        }

        internal static void OnChannelAnnouncements(RealmServerSession session, PacketReader handler)
        {
            throw new NotImplementedException();
        }

        internal static void OnMessageChat(RealmServerSession session, PacketReader handler)
        {
            ChatMessageType msgType = (ChatMessageType) handler.ReadUInt32();
            ChatMessageLanguage msgLanguage = (ChatMessageLanguage) handler.ReadUInt32();

            if (msgType ==  ChatMessageType.CHAT_MSG_CHANNEL)
            {
                var channel = handler.ReadCString();
            }

            if (msgType == ChatMessageType.CHAT_MSG_WHISPER)
            {
                var toUser = handler.ReadCString();
            }

            string message = handler.ReadCString();
           
            switch ((ChatMsgs)msgType)
            {
                case ChatMsgs.CHAT_MSG_SAY:
                case ChatMsgs.CHAT_MSG_YELL:
                case ChatMsgs.CHAT_MSG_EMOTE:
                    session.SendPacket(new SmsgMessagechat(msgType, msgLanguage, (ulong)session.Character.Id, message));
                    break;
                default:
                    Console.WriteLine($"veio aqui algo [{msgType}]");
                    break;
            }
        }
    }

    public enum ChatMsgs
    {
        CHAT_MSG_SAY = 0x00,
        CHAT_MSG_PARTY = 0x01,
        CHAT_MSG_GUILD = 0x02,
        CHAT_MSG_OFFICER = 0x03,
        CHAT_MSG_YELL = 0x04,
        CHAT_MSG_WHISPER = 0x05,
        CHAT_MSG_WHISPER_INFORM = 0x06,
        CHAT_MSG_EMOTE = 0x07,
        CHAT_MSG_TEXT_EMOTE = 0x08,
        CHAT_MSG_SYSTEM = 0x09,
        CHAT_MSG_MONSTER_SAY = 0x0A,
        CHAT_MSG_MONSTER_YELL = 0x0B,
        CHAT_MSG_MONSTER_EMOTE = 0x0C,
        CHAT_MSG_CHANNEL = 0x0D,
        CHAT_MSG_CHANNEL_JOIN = 0x0E,
        CHAT_MSG_CHANNEL_LEAVE = 0xF,
        CHAT_MSG_CHANNEL_LIST = 0x10,
        CHAT_MSG_CHANNEL_NOTICE = 0x11,
        CHAT_MSG_CHANNEL_NOTICE_USER = 0x12,
        CHAT_MSG_AFK = 0x13,
        CHAT_MSG_DND = 0x14,
        CHAT_MSG_IGNORED = 0x16,
        CHAT_MSG_SKILL = 0x17,
        CHAT_MSG_LOOT = 0x18,
    }

    public enum ChatMessageType : byte
    {
        CHAT_MSG_SAY = 0x00,
        CHAT_MSG_PARTY = 0x01,
        CHAT_MSG_RAID = 0x02,
        CHAT_MSG_GUILD = 0x03,
        CHAT_MSG_OFFICER = 0x04,
        CHAT_MSG_YELL = 0x05,
        CHAT_MSG_WHISPER = 0x06,
        CHAT_MSG_WHISPER_INFORM = 0x07,
        CHAT_MSG_EMOTE = 0x08,
        CHAT_MSG_TEXT_EMOTE = 0x09,
        CHAT_MSG_SYSTEM = 0x0A,
        CHAT_MSG_MONSTER_SAY = 0x0B,
        CHAT_MSG_MONSTER_YELL = 0x0C,
        CHAT_MSG_MONSTER_EMOTE = 0x0D,
        CHAT_MSG_CHANNEL = 0x0E,
        CHAT_MSG_CHANNEL_JOIN = 0x0F,
        CHAT_MSG_CHANNEL_LEAVE = 0x10,
        CHAT_MSG_CHANNEL_LIST = 0x11,
        CHAT_MSG_CHANNEL_NOTICE = 0x12,
        CHAT_MSG_CHANNEL_NOTICE_USER = 0x13,
        CHAT_MSG_AFK = 0x14,
        CHAT_MSG_DND = 0x15,
        CHAT_MSG_IGNORED = 0x16,
        CHAT_MSG_SKILL = 0x17,
        CHAT_MSG_LOOT = 0x18,
        CHAT_MSG_BG_SYSTEM_NEUTRAL = 0x52,
        CHAT_MSG_BG_SYSTEM_ALLIANCE = 0x53,
        CHAT_MSG_BG_SYSTEM_HORDE = 0x54,
        CHAT_MSG_RAID_LEADER = 0x57,
        CHAT_MSG_RAID_WARNING = 0x58,
        CHAT_MSG_BATTLEGROUND = 0x5C,
        CHAT_MSG_BATTLEGROUND_LEADER = 0x5D,
        CHAT_MSG_REPLY = 0x09,
        CHAT_MSG_MONSTER_PARTY = 0x30, // 0x0D, just selected some free random value for avoid duplicates with really existed values
        CHAT_MSG_MONSTER_WHISPER = 0x31, // 0x0F, just selected some free random value for avoid duplicates with really existed values
        // CHAT_MSG_MONEY                  = 0x1C,
        // CHAT_MSG_OPENING                = 0x1D,
        // CHAT_MSG_TRADESKILLS            = 0x1E,
        // CHAT_MSG_PET_INFO               = 0x1F,
        // CHAT_MSG_COMBAT_MISC_INFO       = 0x20,
        // CHAT_MSG_COMBAT_XP_GAIN         = 0x21,
        // CHAT_MSG_COMBAT_HONOR_GAIN      = 0x22,
        // CHAT_MSG_COMBAT_FACTION_CHANGE  = 0x23,
        CHAT_MSG_RAID_BOSS_WHISPER = 0x29,
        CHAT_MSG_RAID_BOSS_EMOTE = 0x2A
        // CHAT_MSG_FILTERED               = 0x2B,
        // CHAT_MSG_RESTRICTED             = 0x2E,
    }

    public enum ChatMessageLanguage : byte
    {
        LANG_UNIVERSAL = 0,
        LANG_ORCISH = 1,
        LANG_DARNASSIAN = 2,
        LANG_TAURAHE = 3,
        LANG_DWARVISH = 6,
        LANG_COMMON = 7,
        LANG_DEMONIC = 8,
        LANG_TITAN = 9,
        LANG_THALASSIAN = 10,
        LANG_DRACONIC = 11,
        LANG_KALIMAG = 12,
        LANG_GNOMISH = 13,
        LANG_TROLL = 14,
        LANG_GUTTERSPEAK = 33
    }
}
