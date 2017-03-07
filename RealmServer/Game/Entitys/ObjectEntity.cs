namespace RealmServer.Game.Entitys
{
    public class ObjectEntity : BaseEntity
    {
        public ObjectGuid ObjectGuid { get; set; }

        public override int DataLength => (int) ObjectFields.OBJECT_END;

        public ulong Guid
        {
            get { return (ulong) UpdateData[ObjectFields.OBJECT_FIELD_GUID]; }
            set { SetUpdateField((int) ObjectFields.OBJECT_FIELD_GUID, value); }
        }

        public byte Type
        {
            get { return (byte) UpdateData[(int) ObjectFields.OBJECT_FIELD_TYPE]; }
            set { SetUpdateField((int) ObjectFields.OBJECT_FIELD_TYPE, value); }
        }

        public byte Entry
        {
            get { return (byte) UpdateData[(int) ObjectFields.OBJECT_FIELD_ENTRY]; }
            set { SetUpdateField((int) ObjectFields.OBJECT_FIELD_ENTRY, value); }
        }

        public float Scale
        {
            get { return (float) UpdateData[(int) ObjectFields.OBJECT_FIELD_SCALE_X]; }
            set { SetUpdateField((int) ObjectFields.OBJECT_FIELD_SCALE_X, value); }
        }

        public ObjectEntity(ObjectGuid objectGuid)
        {
            ObjectGuid = objectGuid;
            Guid = ObjectGuid.RawGuid;
        }
    }
}