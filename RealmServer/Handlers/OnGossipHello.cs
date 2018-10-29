using Common.Globals;
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

    public sealed class SMSG_GOSSIP_MESSAGE : Common.Network.PacketServer
    {
        public SMSG_GOSSIP_MESSAGE(CMSG_GOSSIP_HELLO handler) : base(RealmEnums.SMSG_GOSSIP_MESSAGE)
        {
            Write((ulong) handler.Uid);
            Write(34);

            Write(0);

            Write(0);
        }
    }

    public sealed class SMSG_NPC_TEXT_UPDATE : Common.Network.PacketServer
    {
        public SMSG_NPC_TEXT_UPDATE(CMSG_GOSSIP_HELLO handler) : base(RealmEnums.SMSG_NPC_TEXT_UPDATE)
        {
            Write(34);

            //for (var i = 0; i < 7; ++i)
            //{
                Write((float) 0);
                WriteCString("Hi $N, I\'m not yet scripted to talk with you.");
                WriteCString("Hi $N, I\'m not yet scripted to talk with you.");
                Write(0);
                Write(0);
                Write(0);
                Write(0);
                Write(0);
                Write(0);
                Write(0);
            //}
        }
    }
}
