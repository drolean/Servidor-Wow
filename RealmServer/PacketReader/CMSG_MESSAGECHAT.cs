using System;
using Common.Helpers;
using RealmServer.Enums;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_MESSAGECHAT : Common.Network.PacketReader
    {
        public string Channel;
        public int Language;
        public string Message;
        public int Type;

        public CMSG_MESSAGECHAT(byte[] data) : base(data)
        {
            Type = ReadInt32();
            Language = ReadInt32();

            Console.WriteLine((ChatMessageType) Type);

            switch ((ChatMessageType) Type)
            {
                case ChatMessageType.Say:
                case ChatMessageType.Emote:
                case ChatMessageType.Yell:
                    Message = ReadCString();
                    break;
                case ChatMessageType.Channel:
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