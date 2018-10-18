using Common.Globals;

namespace RealmServer.PacketServer
{
    internal sealed class SMSG_LOGOUT_CANCEL_ACK : Common.Network.PacketServer
    {
        public SMSG_LOGOUT_CANCEL_ACK() : base(RealmEnums.SMSG_LOGOUT_CANCEL_ACK)
        {
            Write((byte) 0);
        }
    }
}