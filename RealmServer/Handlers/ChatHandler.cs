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
                    Write((uint) 0);
                    Write(id);
                    break;
                case ChatMessageType.CHAT_MSG_YELL:
                case ChatMessageType.CHAT_MSG_SAY:
                case ChatMessageType.CHAT_MSG_PARTY:
                    Write(id);
                    Write(id);
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
                    Write(id);
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

            Log.Print(LogType.Debug, $"[{session.ConnectionRemoteIp}] Channel leave [{session.Character.name} ({channelName})]");

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
                //string channel = handler.ReadCString();
            }

            if (msgType == ChatMessageType.CHAT_MSG_WHISPER)
            {
                //var toUser = handler.ReadCString();
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
                    Console.WriteLine($@"veio aqui algo [{msgType}]");
                    break;
            }
        }
    }
}
