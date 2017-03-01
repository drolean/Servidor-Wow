using Common.Globals;

namespace RealmServer.Game.Entitys
{
    public class UnitEntity : ObjectEntity
    {
        public UnitEntity(ObjectGuid objectGuid) : base(objectGuid)
        {
        }

        public override int DataLength => (int) UnitFields.UNIT_END - 0x4;
    }
}