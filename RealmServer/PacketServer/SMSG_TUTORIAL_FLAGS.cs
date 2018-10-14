using Common.Globals;

namespace RealmServer.PacketServer
{
    public sealed class SMSG_TUTORIAL_FLAGS : Common.Network.PacketServer
    {
        public SMSG_TUTORIAL_FLAGS() : base(RealmEnums.SMSG_TUTORIAL_FLAGS)
        {
            for (var i = 0; i < 8; i++)
                Write(-1);
        }
    }
}