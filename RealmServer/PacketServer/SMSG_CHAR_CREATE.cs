using Common.Globals;

namespace RealmServer.PacketServer
{
    public sealed class SMSG_CHAR_CREATE : Common.Network.PacketServer
    {
        public SMSG_CHAR_CREATE(LoginErrorCode code) : base(RealmEnums.SMSG_CHAR_CREATE)
        {
            Write((byte)code);
        }
    }
}