using System.Collections.Generic;
using System.IO;
using System.Text;
using Common.Globals;
using Common.Helpers;
using RealmServer.Enums;

namespace RealmServer.PacketServer
{
    public sealed class SMSG_MESSAGECHAT : Common.Network.PacketServer
    {
        private SMSG_MESSAGECHAT(List<byte[]> blocks) : base(RealmEnums.SMSG_MESSAGECHAT)
        {
            blocks.ForEach(Write);
        }

        public SMSG_MESSAGECHAT(ChatMessageType type, ChatMessageLanguage language, ulong id, string message,
            string channelName = null) : base(RealmEnums.SMSG_MESSAGECHAT)
        {
            Write((byte) type);
            Write((uint) language);

            switch (type)
            {
                case ChatMessageType.CHAT_MSG_CHANNEL:
                    Write(Encoding.UTF8.GetBytes(channelName + '\0')); // string => Channel
                    Write((uint) 0); //32
                    Write(id); //64 => SenderId
                    break;
                case ChatMessageType.CHAT_MSG_YELL:
                case ChatMessageType.CHAT_MSG_SAY:
                case ChatMessageType.CHAT_MSG_PARTY:
                    Write(id); // SenderId
                    Write(id); // SenderId
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
                    Write(id); // SenderId
                    break;
                case ChatMessageType.CHAT_MSG_TEXT_EMOTE:
                    break;
                case ChatMessageType.CHAT_MSG_MONSTER_SAY:
                    break;
                case ChatMessageType.CHAT_MSG_MONSTER_YELL:
                    break;
                case ChatMessageType.CHAT_MSG_MONSTER_EMOTE:
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
                case ChatMessageType.CHAT_MSG_LOOT:
                    break;
                case ChatMessageType.CHAT_MSG_BG_SYSTEM_NEUTRAL:
                    break;
                case ChatMessageType.CHAT_MSG_BG_SYSTEM_ALLIANCE:
                    break;
                case ChatMessageType.CHAT_MSG_BG_SYSTEM_HORDE:
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
                default:
                    Log.Print(LogType.Debug, $"Unknown chat message type - {type}!");
                    break;
            }

            Write((uint) message.Length + 1);
            Write(Encoding.UTF8.GetBytes(message + '\0'));
            // check flag of CHAR [0 = normal] [1 = AFK] [2 = DND] [3 = GM]
            Write((byte) 0); // Flag????
        }

        public static SMSG_MESSAGECHAT SendMotd(ulong id, string message)
        {
            var writer = new BinaryWriter(new MemoryStream());

            writer.Write((byte) ChatMessageType.CHAT_MSG_SYSTEM);
            writer.Write((uint) ChatMessageLanguage.LANG_UNIVERSAL);
            writer.Write(id);
            writer.Write((uint) message.Length + 1);
            writer.Write(Encoding.UTF8.GetBytes(message + '\0'));
            writer.Write((byte) 0);

            return new SMSG_MESSAGECHAT(new List<byte[]> {((MemoryStream) writer.BaseStream).ToArray()});
        }
    }
}