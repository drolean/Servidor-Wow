using System;
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

            Console.WriteLine((ChatMessageType) Type);

            switch ((ChatMessageType) Type)
            {
                case ChatMessageType.CHAT_MSG_SAY:
                case ChatMessageType.CHAT_MSG_EMOTE:
                case ChatMessageType.CHAT_MSG_YELL:
                    Message = ReadCString();
                    break;
                case ChatMessageType.CHAT_MSG_CHANNEL:
                    Channel = ReadCString();
                    Message = ReadCString();
                    break;
            }

#if DEBUG
            Log.Print(LogType.Debug,
                $"[CMSG_MESSAGECHAT] Type: {(ChatMessageType) Type} Language: {Language} Message: {Message} Channel: {Channel}");
#endif
        }
    }
}