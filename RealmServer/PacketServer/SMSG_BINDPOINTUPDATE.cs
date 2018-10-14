using Common.Database.Tables;
using Common.Globals;

namespace RealmServer.PacketServer
{
    public sealed class SMSG_BINDPOINTUPDATE : Common.Network.PacketServer
    {
        public SMSG_BINDPOINTUPDATE(Characters character) : base(RealmEnums.SMSG_BINDPOINTUPDATE)
        {
            Write(character.SubMap.MapX);
            Write(character.SubMap.MapY);
            Write(character.SubMap.MapZ);
            Write((uint) character.SubMap.MapId);
            Write((uint) character.SubMap.MapZone);
        }
    }
}