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

        public UnitEntity(int vai, int parse) : base(new ObjectGuid((uint) vai, TypeId.TypeidUnit,
            HighGuid.HighguidUnit))
        {
            var creature = DatabaseModel.CreaturesCollection.Find(x => x.Entry == parse).First();

            Type = (byte) (ObjectType.TYPE_OBJECT + (int) ObjectType.TYPE_UNIT);
            Scale = 1f;
            Entry = creature.Entry; // id of Database

            //
            SetUpdateField((int) UnitFields.UNIT_FIELD_DISPLAYID, creature.Modelid1);
            SetUpdateField((int) UnitFields.UNIT_FIELD_NATIVEDISPLAYID, creature.Modelid1);

            SetUpdateField((int) UnitFields.UNIT_NPC_FLAGS, creature.NpcFlag);
            SetUpdateField((int) UnitFields.UNIT_DYNAMIC_FLAGS, creature.DynamicFlags);
            SetUpdateField((int) UnitFields.UNIT_FIELD_FLAGS, creature.UnitFlags);

            SetUpdateField((int) UnitFields.UNIT_FIELD_FACTIONTEMPLATE, creature.FactionH);

            SetUpdateField((int) UnitFields.UNIT_FIELD_HEALTH, creature.MinHealth);
            SetUpdateField((int) UnitFields.UNIT_FIELD_MAXHEALTH, creature.MaxHealth);
            SetUpdateField((int) UnitFields.UNIT_FIELD_LEVEL, creature.MaxLevel);

            Console.WriteLine(MainProgram.Vai);
            MainProgram.Vai++;
        }

        public TypeId TypeId => TypeId.TypeidUnit;
        public override int DataLength => (int) UnitFields.UNIT_END - 0x4;
    }
}