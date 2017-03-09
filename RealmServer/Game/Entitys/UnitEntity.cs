using System;
using Common.Database.Xml;
using Common.Globals;

namespace RealmServer.Game.Entitys
{
    public class UnitEntity : ObjectEntity
    {
        public TypeId TypeId => TypeId.TypeidUnit;
        public override int DataLength => (int) UnitFields.UNIT_END - 0x4;

        public static int Ababa;

        public UnitEntity(zoneObjeto zoneObjeto)
            : base(new ObjectGuid((uint) (zoneObjeto.id + Ababa), TypeId.TypeidUnit, HighGuid.HighguidUnit))
        {
            Type  = 0x9;
            Scale = 0.42f;
            Entry = 30; // 30=Forest Spider

            //
            SetUpdateField((int)UnitFields.UNIT_FIELD_DISPLAYID, 382);
            SetUpdateField((int)UnitFields.UNIT_FIELD_NATIVEDISPLAYID, 383);


            SetUpdateField((int) UnitFields.UNIT_NPC_FLAGS, 0);
            SetUpdateField((int) UnitFields.UNIT_DYNAMIC_FLAGS, 0);
            SetUpdateField((int) UnitFields.UNIT_FIELD_FLAGS, 0);

            // flags1=010
            // family=3
            // type=1

            SetUpdateField((int) UnitFields.UNIT_FIELD_FACTIONTEMPLATE, 25);

            SetUpdateField((int) UnitFields.UNIT_FIELD_HEALTH, 60);
            SetUpdateField((int) UnitFields.UNIT_FIELD_MAXHEALTH, 125);
            SetUpdateField((int) UnitFields.UNIT_FIELD_LEVEL, 1);

            

            //SetUpdateField((int) UnitFields.UNIT_FIELD_CREATEDBY, 0);
            Console.WriteLine($@"veio algo aqui => {Ababa}");
            Ababa++;
        }

        public UnitEntity(ObjectGuid objectGuid) : base(objectGuid)
        {

        }
    }
}