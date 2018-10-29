using Common.Globals;
using RealmServer.PacketReader;

namespace RealmServer.PacketServer
{
    internal sealed class SMSG_NPC_WONT_TALK : Common.Network.PacketServer
    {
        public SMSG_NPC_WONT_TALK(CMSG_GOSSIP_HELLO handler) : base(RealmEnums.SMSG_NPC_WONT_TALK)
        {
            Write(handler.Uid);
            Write((byte) 2);
        }
    }
}
