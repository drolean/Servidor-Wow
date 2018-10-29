using Common.Globals;

namespace RealmServer.PacketServer
{
    /// <summary>
    ///     SMSG_LOGOUT_COMPLETE represents a message sent by the server to indicates that the player logout is complete.
    /// </summary>
    internal sealed class SMSG_LOGOUT_COMPLETE : Common.Network.PacketServer
    {
        public SMSG_LOGOUT_COMPLETE(byte result) : base(RealmEnums.SMSG_LOGOUT_COMPLETE)
        {
            Write(result);
        }
    }
}
