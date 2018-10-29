using Common.Globals;
using RealmServer.PacketReader;

namespace RealmServer.PacketServer
{
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
