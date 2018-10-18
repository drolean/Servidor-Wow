using Common.Globals;
using Common.Helpers;

namespace RealmServer.PacketServer
{
    internal sealed class SMSG_FORCE_MOVE_UNROOT : Common.Network.PacketServer
    {
        public SMSG_FORCE_MOVE_UNROOT(ulong characterUid, uint result) : base(RealmEnums.SMSG_FORCE_MOVE_UNROOT)
        {
            this.WritePackedUInt64(characterUid);
            Write(result);
        }
    }
}