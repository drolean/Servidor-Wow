using Common.Database.Tables;
using Common.Globals;

namespace RealmServer.PacketServer
{
    /// <summary>
    /// </summary>
    public sealed class SMSG_BINDPOINTUPDATE : Common.Network.PacketServer
    {
        /// <summary>
        /// </summary>
        /// <param name="character"></param>
        public SMSG_BINDPOINTUPDATE(Characters character) : base(RealmEnums.SMSG_BINDPOINTUPDATE)
        {
            Write(character.SubMap.MapX);
            Write(character.SubMap.MapY);
            Write(character.SubMap.MapZ);
            Write((uint) character.SubMap.MapId);
            Write((uint) character.SubMap.MapZone);

            /*
             TODO
                // zone update
                data.Initialize(SMSG_PLAYERBOUND, 8+4);
                data << uint64(player->GetGUID());
                data << uint32(area_id);
             */
        }
    }
}