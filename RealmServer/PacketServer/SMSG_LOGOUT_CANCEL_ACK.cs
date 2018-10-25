using Common.Globals;

namespace RealmServer.PacketServer
{
    /// <summary>
    ///     SMSG_LOGOUT_CANCEL represents a message sent by the server to indicates that the logout operation has been
    ///     cancelled.
    /// </summary>
    internal sealed class SMSG_LOGOUT_CANCEL_ACK : Common.Network.PacketServer
    {
        public SMSG_LOGOUT_CANCEL_ACK() : base(RealmEnums.SMSG_LOGOUT_CANCEL_ACK)
        {
            Write((byte) 0);
        }
    }
}