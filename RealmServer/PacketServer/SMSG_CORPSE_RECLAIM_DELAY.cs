using Common.Globals;

namespace RealmServer.PacketServer
{
    public sealed class SMSG_CORPSE_RECLAIM_DELAY : Common.Network.PacketServer
    {
        public SMSG_CORPSE_RECLAIM_DELAY() : base(RealmEnums.SMSG_CORPSE_RECLAIM_DELAY)
        {
            Write(2 * 1000);
        }
    }
}
