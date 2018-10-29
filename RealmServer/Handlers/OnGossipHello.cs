using RealmServer.PacketReader;
using RealmServer.PacketServer;

namespace RealmServer.Handlers
{
    public class OnGossipHello
    {
        public static void Handler(RealmServerSession session, CMSG_GOSSIP_HELLO handler)
        {
            //npcflag = 0 return
            session.SendPacket(new SMSG_NPC_WONT_TALK(handler));

            session.SendPacket(new SMSG_NPC_TEXT_UPDATE(handler));
            session.SendPacket(new SMSG_GOSSIP_MESSAGE(handler));
            // SMSG_NPC_TEXT_UPDATE
            // SMSG_GOSSIP_MESSAGE
        }
    }
}
