using RealmServer.Enums;

namespace RealmServer.World.Enititys
{
    public class UnitEntity : ObjectEntity
    {
        public UnitEntity(ObjectGuid objectGuid) : base(objectGuid)
        {
        }

        public UnitEntity(int parse) : base(new ObjectGuid((uint) parse, TypeId.TypeidUnit, HighGuid.HighguidUnit))
        {
            Type = 0x9;
            //Scale = 0.42f;
            Entry = (byte) parse; // 30=Forest Spider

            //
            SetUpdateField((int)UnitFields.UNIT_FIELD_DISPLAYID, parse);
            SetUpdateField((int)UnitFields.UNIT_FIELD_NATIVEDISPLAYID, parse);

            SetUpdateField((int)UnitFields.UNIT_NPC_FLAGS, 0);
            SetUpdateField((int)UnitFields.UNIT_DYNAMIC_FLAGS, 0);
            SetUpdateField((int)UnitFields.UNIT_FIELD_FLAGS, 0);

            SetUpdateField((int)UnitFields.UNIT_FIELD_FACTIONTEMPLATE, 25);

            SetUpdateField((int)UnitFields.UNIT_FIELD_HEALTH, 60);
            SetUpdateField((int)UnitFields.UNIT_FIELD_MAXHEALTH, 125);
            SetUpdateField((int)UnitFields.UNIT_FIELD_LEVEL, 1);
        }

        public TypeId TypeId => TypeId.TypeidUnit;
        public override int DataLength => (int) UnitFields.UNIT_END - 0x4;
    }
}