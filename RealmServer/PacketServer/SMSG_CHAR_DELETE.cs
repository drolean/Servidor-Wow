using Common.Globals;

namespace RealmServer.PacketServer
{
    public sealed class SMSG_CHAR_DELETE : Common.Network.PacketServer
    {
        public SMSG_CHAR_DELETE(LoginErrorCode code) : base(RealmEnums.SMSG_CHAR_DELETE)
        {
            Write((byte) code);
        }
    }
}