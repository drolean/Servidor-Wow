using Common.Globals;

namespace RealmServer.PacketServer
{
    internal sealed class SMSG_LOGOUT_COMPLETE : Common.Network.PacketServer
    {
        public SMSG_LOGOUT_COMPLETE(byte result) : base(RealmEnums.SMSG_LOGOUT_COMPLETE)
        {
            Write(result);
        }
    }
}