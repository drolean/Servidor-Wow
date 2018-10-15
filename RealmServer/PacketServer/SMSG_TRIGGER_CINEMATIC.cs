using Common.Globals;

namespace RealmServer.PacketServer
{
    public sealed class SMSG_TRIGGER_CINEMATIC : Common.Network.PacketServer
    {
        public SMSG_TRIGGER_CINEMATIC(int cinematic) : base(RealmEnums.SMSG_TRIGGER_CINEMATIC)
        {
            Write(cinematic);
        }
    }
}