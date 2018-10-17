using Common.Helpers;
using RealmServer.Enums;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_MESSAGECHAT : Common.Network.PacketReader
    {
        public int Type;
        public int Language;
        public string Channel;
        public string Message;

        public CMSG_MESSAGECHAT(byte[] data) : base(data)
        {
            Type = ReadInt32();
            Language = ReadInt32();

            switch ((ChatMessageType) Type)
            {
                case ChatMessageType.CHAT_MSG_SAY:
                    break;
                case ChatMessageType.CHAT_MSG_PARTY:
                    Message = ReadCString();
                    break;
                case ChatMessageType.CHAT_MSG_RAID:
                    Message = ReadCString();
                    break;
                case ChatMessageType.CHAT_MSG_GUILD:
                    Message = ReadCString();
                    break;
                case ChatMessageType.CHAT_MSG_OFFICER:
                    Message = ReadCString();
                    break;
                case ChatMessageType.CHAT_MSG_YELL:
                    break;
                case ChatMessageType.CHAT_MSG_WHISPER:
                    Channel = ReadCString();
                    Message = ReadCString();
                    break;
                case ChatMessageType.CHAT_MSG_WHISPER_INFORM:
                    break;
                case ChatMessageType.CHAT_MSG_EMOTE:
                    break;
                case ChatMessageType.CHAT_MSG_TEXT_EMOTE:
                    break;
                case ChatMessageType.CHAT_MSG_SYSTEM:
                    break;
                case ChatMessageType.CHAT_MSG_MONSTER_SAY:
                    break;
                case ChatMessageType.CHAT_MSG_MONSTER_YELL:
                    break;
                case ChatMessageType.CHAT_MSG_MONSTER_EMOTE:
                    break;
                case ChatMessageType.CHAT_MSG_CHANNEL:
                    Channel = ReadCString();
                    Message = ReadCString();
                    break;
                case ChatMessageType.CHAT_MSG_CHANNEL_JOIN:
                    break;
                case ChatMessageType.CHAT_MSG_CHANNEL_LEAVE:
                    break;
                case ChatMessageType.CHAT_MSG_CHANNEL_LIST:
                    break;
                case ChatMessageType.CHAT_MSG_CHANNEL_NOTICE:
                    break;
                case ChatMessageType.CHAT_MSG_CHANNEL_NOTICE_USER:
                    break;
                case ChatMessageType.CHAT_MSG_AFK:
                    Message = ReadCString();
                    break;
                case ChatMessageType.CHAT_MSG_DND:
                    Message = ReadCString();
                    break;
                case ChatMessageType.CHAT_MSG_IGNORED:
                    break;
                case ChatMessageType.CHAT_MSG_SKILL:
                    break;
                case ChatMessageType.CHAT_MSG_LOOT:
                    break;
                case ChatMessageType.CHAT_MSG_BG_SYSTEM_NEUTRAL:
                    break;
                case ChatMessageType.CHAT_MSG_BG_SYSTEM_ALLIANCE:
                    break;
                case ChatMessageType.CHAT_MSG_BG_SYSTEM_HORDE:
                    break;
                case ChatMessageType.CHAT_MSG_RAID_LEADER:
                    break;
                case ChatMessageType.CHAT_MSG_RAID_WARNING:
                    break;
                case ChatMessageType.CHAT_MSG_BATTLEGROUND:
                    break;
                case ChatMessageType.CHAT_MSG_BATTLEGROUND_LEADER:
                    break;
                case ChatMessageType.CHAT_MSG_MONSTER_PARTY:
                    break;
                case ChatMessageType.CHAT_MSG_MONSTER_WHISPER:
                    break;
                case ChatMessageType.CHAT_MSG_RAID_BOSS_WHISPER:
                    break;
                case ChatMessageType.CHAT_MSG_RAID_BOSS_EMOTE:
                    break;
            }

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_MESSAGECHAT] Type: {(ChatMessageType)Type} Language: {Language} Channel: {Channel} Message: {Message}");
#endif
        }
    }
}