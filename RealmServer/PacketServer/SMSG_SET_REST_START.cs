using Common.Globals;

namespace RealmServer.PacketServer
{
    public sealed class SMSG_SET_REST_START : Common.Network.PacketServer
    {
        public SMSG_SET_REST_START(uint result) : base(RealmEnums.SMSG_SET_REST_START)
        {
            Write(result);
        }
    }
}