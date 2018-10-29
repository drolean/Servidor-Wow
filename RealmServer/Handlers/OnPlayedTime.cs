using RealmServer.PacketServer;

namespace RealmServer.Handlers
{
    public class OnPlayedTime
    {
        public static void Handler(RealmServerSession session, byte[] data)
        {
            session.SendPacket(new SMSG_PLAYED_TIME(session.Character));
        }
    }
}
