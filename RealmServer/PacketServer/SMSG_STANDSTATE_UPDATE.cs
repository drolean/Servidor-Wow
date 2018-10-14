using Common.Globals;

namespace RealmServer.PacketServer
{
    public sealed class SMSG_STANDSTATE_UPDATE : Common.Network.PacketServer
    {
        public SMSG_STANDSTATE_UPDATE(byte state) : base(RealmEnums.SMSG_STANDSTATE_UPDATE)
        {
            Write(state);
        }
    }
}