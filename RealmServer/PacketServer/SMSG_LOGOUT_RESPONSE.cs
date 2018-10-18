using Common.Globals;

namespace RealmServer.PacketServer
{
    internal sealed class SMSG_LOGOUT_RESPONSE : Common.Network.PacketServer
    {
        public SMSG_LOGOUT_RESPONSE(LogoutResponseCode result) : base(RealmEnums.SMSG_LOGOUT_RESPONSE)
        {
            Write((byte) result);
            Write(0);
        }
    }
}