using Common.Database.Tables;
using Common.Globals;
using Common.Helpers;

namespace RealmServer.PacketServer
{
    internal sealed class SMSG_CREATURE_QUERY_RESPONSE : Common.Network.PacketServer
    {
        public SMSG_CREATURE_QUERY_RESPONSE(Creatures creature) : base(RealmEnums.SMSG_CREATURE_QUERY_RESPONSE)
        {
            Write(creature.Entry);

            WriteCString(creature.Name);
            Write((byte) 0);
            Write((byte) 0);
            Write((byte) 0);

            WriteCString(creature.Subname);

            Write((uint) creature.SubFlags.Type);
            Write((uint) creature.Type);
            Write((uint) creature.Family);
            Write((uint) creature.Rank);
            Write((uint) 0);

            Write((uint) 0); // PetSpellDataId
            Write((uint) creature.SubModels.RandomElement().Model);
            Write((byte) creature.Civilian);
            Write((byte) creature.RacialLeader);
        }
    }
}