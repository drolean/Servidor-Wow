using Common.Globals;

namespace RealmServer.PacketServer
{
    public sealed class SMSG_AUTH_CHALLENGE : Common.Network.PacketServer
    {
        public SMSG_AUTH_CHALLENGE() : base(RealmEnums.SMSG_AUTH_CHALLENGE)
        {
            Write((byte) 1);
            Write((byte) 2);
            Write((byte) 3);
            Write((byte) 4);
        }
    }
}