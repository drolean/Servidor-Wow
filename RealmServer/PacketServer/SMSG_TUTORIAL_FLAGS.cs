using Common.Database.Tables;
using Common.Globals;

namespace RealmServer.PacketServer
{
    public sealed class SMSG_TUTORIAL_FLAGS : Common.Network.PacketServer
    {
        public SMSG_TUTORIAL_FLAGS(Characters session) : base(RealmEnums.SMSG_TUTORIAL_FLAGS)
        {
            Write(session.TutorialFlags);
        }
    }
}
