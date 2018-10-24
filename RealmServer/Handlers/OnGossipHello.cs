using RealmServer.PacketReader;
using RealmServer.PacketServer;

namespace RealmServer.Handlers
{
    public class OnGossipHello
    {
        public static void Handler(RealmServerSession session, CMSG_GOSSIP_HELLO handler)
        {
            session.SendPacket(new SMSG_NPC_WONT_TALK(handler));
        }
    }
}