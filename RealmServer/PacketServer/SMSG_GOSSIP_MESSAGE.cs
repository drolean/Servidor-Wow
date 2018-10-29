using Common.Globals;
using RealmServer.PacketReader;

namespace RealmServer.PacketServer
{
    public sealed class SMSG_GOSSIP_MESSAGE : Common.Network.PacketServer
    {
        public SMSG_GOSSIP_MESSAGE(CMSG_GOSSIP_HELLO handler) : base(RealmEnums.SMSG_GOSSIP_MESSAGE)
        {
            Write(handler.Uid);
            Write(34);

            Write(0);

            Write(0);
        }
    }
}
