using Common.Helpers;
using RealmServer.Enums;
using RealmServer.Helpers;
using RealmServer.PacketReader;
using RealmServer.PacketServer;

namespace RealmServer.Handlers
{
    public class OnMessageChat
    {
        public static void Handler(RealmServerSession session, CMSG_MESSAGECHAT handler)
        {
            var msgType = (ChatMessageType) handler.Type;
            var msgLanguage = (ChatMessageLanguage) handler.ReadUInt32();


            new CommandTest(session, handler.Message);

            switch (msgType)
            {
                case ChatMessageType.Say:
                case ChatMessageType.Yell:
                case ChatMessageType.Emote:
                    session.SendPacket(new SMSG_MESSAGECHAT(msgType, msgLanguage, session.Character.Uid,
                        handler.Message));
                    break;
                case ChatMessageType.Channel:
                    session.SendPacket(new SMSG_MESSAGECHAT(msgType, msgLanguage, session.Character.Uid,
                        handler.Message, handler.Channel));
                    break;
                default:
                    Log.Print(LogType.Debug, $"Not Implemented {msgType}");
                    break;
            }
        }
    }
}