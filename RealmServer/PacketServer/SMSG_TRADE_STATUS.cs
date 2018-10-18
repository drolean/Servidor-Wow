using Common.Globals;

namespace RealmServer.PacketServer
{
    internal sealed class SMSG_TRADE_STATUS : Common.Network.PacketServer
    {
        public SMSG_TRADE_STATUS(object status) : base(RealmEnums.SMSG_TRADE_STATUS)
        {
            Write((uint)status);
        }
    }
}