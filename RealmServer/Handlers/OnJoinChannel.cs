using RealmServer.Enums;
using RealmServer.PacketReader;
using RealmServer.PacketServer;

namespace RealmServer.Handlers
{
    public class OnJoinChannel
    {
        public static void Handler(RealmServerSession session, CMSG_JOIN_CHANNEL handler)
        {
            session.SendPacket(new SMSG_CHANNEL_NOTIFY(ChatChannelNotify.YouJoinedNotice, session.Character.Uid,
                handler.Channel));
        }
    }
}