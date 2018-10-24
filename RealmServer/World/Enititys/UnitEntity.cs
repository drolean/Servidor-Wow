using Common.Database;
using Common.Database.Tables;
using Common.Helpers;
using MongoDB.Driver;
using RealmServer.Enums;

namespace RealmServer.World.Enititys
{
    public class UnitEntity : ObjectEntity
    {
        public UnitEntity(ObjectGuid objectGuid) : base(objectGuid)
        {
        }

        public UnitEntity(SpawnCreatures creature)
            : base(new ObjectGuid((uint) creature.Uid, TypeId.TypeidUnit, HighGuid.HighguidUnit))
        {
            var npc = DatabaseModel.CreaturesCollection.Find(x => x.Entry == creature.Entry).First();
            var model = npc.SubModels.RandomElement().Model;

            Type = (byte) (ObjectType.TYPE_OBJECT + (int) ObjectType.TYPE_UNIT);
            Scale = 1f; //creature.SubStats.Scale; // 1f
            Entry = creature.Entry;

            //
            SetUpdateField((int) UnitFields.UNIT_FIELD_DISPLAYID, model);
            SetUpdateField((int) UnitFields.UNIT_FIELD_NATIVEDISPLAYID, model);

            SetUpdateField((int) UnitFields.UNIT_NPC_FLAGS, npc.SubFlags.Npc);
            SetUpdateField((int) UnitFields.UNIT_DYNAMIC_FLAGS, npc.SubFlags.Dynamic);
            SetUpdateField((int) UnitFields.UNIT_FIELD_FLAGS, npc.SubFlags.Unit);

            SetUpdateField((int) UnitFields.UNIT_FIELD_FACTIONTEMPLATE, npc.FactionAlliance);

            // Health of NPC Current change to Manager
            SetUpdateField((int) UnitFields.UNIT_FIELD_HEALTH, npc.SubStats.Health);
            SetUpdateField((int) UnitFields.UNIT_FIELD_MAXHEALTH, npc.SubStats.Health);
            SetUpdateField((int) UnitFields.UNIT_FIELD_LEVEL, npc.SubStats.Level);
        }

        public TypeId TypeId => TypeId.TypeidUnit;
        public override int DataLength => (int) UnitFields.UNIT_END - 0x4;
    }
}