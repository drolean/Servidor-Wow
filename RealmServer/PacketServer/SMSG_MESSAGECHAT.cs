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
                case ChatMessageType.Channel:
                    Write(Encoding.UTF8.GetBytes(channelName + '\0')); // string => Channel
                    Write((uint) 0); //32
                    Write(id); //64 => SenderId
                    break;
                case ChatMessageType.Yell:
                case ChatMessageType.Say:
                case ChatMessageType.Party:
                    Write(id); // SenderId
                    Write(id); // SenderId
                    break;
                case ChatMessageType.System:
                case ChatMessageType.Emote:
                case ChatMessageType.Ignored:
                case ChatMessageType.Skill:
                case ChatMessageType.Officer:
                case ChatMessageType.Raid:
                case ChatMessageType.WhisperInform:
                case ChatMessageType.Guild:
                case ChatMessageType.Whisper:
                case ChatMessageType.Afk:
                case ChatMessageType.Dnd:
                case ChatMessageType.RaidLeader:
                case ChatMessageType.RaidWarning:
                    Write(id); // SenderId
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

            writer.Write((byte) ChatMessageType.System);
            writer.Write((uint) ChatMessageLanguage.Universal);
            writer.Write(id);
            writer.Write((uint) message.Length + 1);
            writer.Write(Encoding.UTF8.GetBytes(message + '\0'));
            writer.Write((byte) 0);

            return new SMSG_MESSAGECHAT(new List<byte[]> {((MemoryStream) writer.BaseStream).ToArray()});
        }
    }
}
