using RealmServer.PacketReader;
using RealmServer.PacketServer;

namespace RealmServer.Handlers
{
    internal class OnMoveTimeSkipped
    {
        public static void Handler(RealmServerSession session, CMSG_MOVE_TIME_SKIPPED handler)
        {
            session.SendPacket(new MSG_MOVE_TIME_SKIPPED(handler));
        }
    }
}