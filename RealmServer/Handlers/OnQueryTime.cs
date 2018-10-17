using RealmServer.PacketServer;

namespace RealmServer.Handlers
{
    internal static class OnQueryTime
    {
        public static void Handler(RealmServerSession session, byte[] data)
        {
            session.SendPacket(new SMSG_QUERY_TIME_RESPONSE());
        }
    }
}