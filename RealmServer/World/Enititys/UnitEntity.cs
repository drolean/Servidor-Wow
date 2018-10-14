using RealmServer.Enums;

namespace RealmServer.World.Enititys
{
    public class UnitEntity : ObjectEntity
    {
        public UnitEntity(ObjectGuid objectGuid) : base(objectGuid)
        {
        }

        public TypeId TypeId => TypeId.TypeidUnit;
        public override int DataLength => (int) UnitFields.UNIT_END - 0x4;
    }
}