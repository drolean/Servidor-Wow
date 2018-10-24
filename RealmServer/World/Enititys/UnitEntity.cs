using System;
using Common.Database;
using MongoDB.Driver;
using RealmServer.Enums;

namespace RealmServer.World.Enititys
{
    public class UnitEntity : ObjectEntity
    {
        public UnitEntity(ObjectGuid objectGuid) : base(objectGuid)
        {
        }

        public UnitEntity(ulong vai, int parse) : base(new ObjectGuid((uint) vai, TypeId.TypeidUnit,
            HighGuid.HighguidUnit))
        {
            var creature = DatabaseModel.CreaturesCollection.Find(x => x.Entry == parse).First();
            var model = creature.SubModels.RandomElement().Model;

            Type = (byte) (ObjectType.TYPE_OBJECT + (int) ObjectType.TYPE_UNIT);
            Scale = 1f; //creature.SubStats.Scale; // 1f
            Entry = creature.Entry;

            //
            SetUpdateField((int) UnitFields.UNIT_FIELD_DISPLAYID, model);
            SetUpdateField((int) UnitFields.UNIT_FIELD_NATIVEDISPLAYID, model);

            SetUpdateField((int) UnitFields.UNIT_NPC_FLAGS, creature.SubFlags.Npc);
            SetUpdateField((int) UnitFields.UNIT_DYNAMIC_FLAGS, creature.SubFlags.Dynamic);
            SetUpdateField((int) UnitFields.UNIT_FIELD_FLAGS, creature.SubFlags.Unit);

            SetUpdateField((int) UnitFields.UNIT_FIELD_FACTIONTEMPLATE, creature.FactionAlliance);

            // Health of NPC Current change to Manager
            SetUpdateField((int) UnitFields.UNIT_FIELD_HEALTH, creature.SubStats.Health);
            SetUpdateField((int) UnitFields.UNIT_FIELD_MAXHEALTH, creature.SubStats.Health);
            SetUpdateField((int) UnitFields.UNIT_FIELD_LEVEL, creature.SubStats.Level);
        }

        public TypeId TypeId => TypeId.TypeidUnit;
        public override int DataLength => (int) UnitFields.UNIT_END - 0x4;
    }
}