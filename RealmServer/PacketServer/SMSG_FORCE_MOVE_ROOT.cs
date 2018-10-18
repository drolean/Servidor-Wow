using Common.Globals;
using Common.Helpers;

namespace RealmServer.PacketServer
{
    internal sealed class SMSG_FORCE_MOVE_ROOT : Common.Network.PacketServer
    {
        public SMSG_FORCE_MOVE_ROOT(ulong characterId) : base(RealmEnums.SMSG_FORCE_MOVE_ROOT)
        {
            this.WritePackedUInt64(characterId);
            Write((uint)0);
        }
    }
}