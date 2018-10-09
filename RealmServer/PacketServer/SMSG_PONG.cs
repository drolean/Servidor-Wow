using Common.Globals;

namespace RealmServer.PacketServer
{
    public sealed class SMSG_PONG : Common.Network.PacketServer
    {
        public SMSG_PONG(uint ping) : base(RealmEnums.SMSG_PONG)
        {
            Write((ulong) ping);
        }
    }
}