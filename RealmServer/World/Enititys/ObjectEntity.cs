using RealmServer.Enums;

namespace RealmServer.World.Enititys
{
    public class ObjectEntity : BaseEntity
    {
        public ObjectEntity(ObjectGuid objectGuid)
        {
            ObjectGuid = objectGuid;
            Guid = ObjectGuid.RawGuid;
        }

        public ObjectGuid ObjectGuid { get; set; }

        public override int DataLength => (int) ObjectFields.OBJECT_END;

        public ulong Guid
        {
            get => (ulong) UpdateData[ObjectFields.OBJECT_FIELD_GUID];
            set => SetUpdateField((int) ObjectFields.OBJECT_FIELD_GUID, value);
        }

        public byte Type
        {
            get => (byte) UpdateData[(int) ObjectFields.OBJECT_FIELD_TYPE];
            set => SetUpdateField((int) ObjectFields.OBJECT_FIELD_TYPE, value);
        }

        public byte Entry
        {
            get => (byte) UpdateData[(int) ObjectFields.OBJECT_FIELD_ENTRY];
            set => SetUpdateField((int) ObjectFields.OBJECT_FIELD_ENTRY, value);
        }

        public float Scale
        {
            get => (float) UpdateData[(int) ObjectFields.OBJECT_FIELD_SCALE_X];
            set => SetUpdateField((int) ObjectFields.OBJECT_FIELD_SCALE_X, value);
        }
    }
}