using Common.Globals;

namespace RealmServer.PacketServer
{
    /// <summary>
    /// SMSG_LOGOUT_RESPONSE represents a message sent by the server to indicates that the player logout is accepted or refused.
    /// </summary>
    internal sealed class SMSG_LOGOUT_RESPONSE : Common.Network.PacketServer
    {
        public SMSG_LOGOUT_RESPONSE(LogoutResponseCode result) : base(RealmEnums.SMSG_LOGOUT_RESPONSE)
        {
            Write((byte) result);
            Write(0);
        }
    }
}