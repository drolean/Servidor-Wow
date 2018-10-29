using Common.Globals;

namespace RealmServer.PacketServer
{
    public sealed class SMSG_SET_FACTION_STANDING : Common.Network.PacketServer
    {
        public SMSG_SET_FACTION_STANDING(int faction, byte enabled, int standing) : base(RealmEnums
            .SMSG_SET_FACTION_STANDING)
        {
            Write((uint) enabled); // flag.database
            Write((uint) faction); // id.database
            Write((uint) standing); // standing.database
        }
    }
}
