using Common.Database.Tables;
using Common.Globals;

namespace RealmServer.PacketServer
{
    internal sealed class SMSG_INITIALIZE_FACTIONS : Common.Network.PacketServer
    {
        public SMSG_INITIALIZE_FACTIONS(Characters character) : base(RealmEnums.SMSG_INITIALIZE_FACTIONS)
        {
            Write(character.SubFactions.Count);

            foreach (var fact in character.SubFactions)
            {
                Write((byte)fact.Flags); // Flag
                Write(fact.Standing); // Value
            }
        }
    }
}