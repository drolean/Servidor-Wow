using Common.Globals;

namespace RealmServer.Game.Entitys
{
    public class ObjectEntity : EntityBase
    {
        public ObjectGuid ObjectGuid { get; set; }

        public ulong Guid
        {
            get { return (ulong) UpdateData[EObjectFields.OBJECT_FIELD_GUID]; }
            set { SetUpdateField((int) EObjectFields.OBJECT_FIELD_GUID, value); }
        }

        public ObjectEntity(ObjectGuid objectGuid)
        {
            ObjectGuid = objectGuid;
            Guid       = ObjectGuid.RawGuid;
        }
    }

    public class ObjectGuid
    {
        public TypeId TypeId { get; private set; }
        public HighGuid HighGuid { get; private set; }
        public ulong RawGuid { get; }

        public ObjectGuid(ulong guid)
        {
            RawGuid = guid;
        }

        public ObjectGuid(uint index, TypeId type, HighGuid high)
        {
            TypeId = type;
            HighGuid = high;
            RawGuid = index | ((ulong) type << 24) | ((ulong) high << 48);
        }
    }

    public enum HighGuid
    {
        HighguidItem = 0x4700,
        HighguidContainer = 0x4700,
        HighguidPlayer = 0x0000,
        HighguidGameobject = 0xF110,
        HighguidTransport = 0xF120,
        HighguidUnit = 0xF130,
        HighguidPet = 0xF140,
        HighguidVehicle = 0xF150,
        HighguidDynamicobject = 0xF100,
        HighguidCorpse = 0xF500,
        HighguidMoTransport = 0x1FC0
    }
}