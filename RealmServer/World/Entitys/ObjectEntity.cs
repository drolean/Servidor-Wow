﻿using RealmServer.Enums;

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

        public override int DataLength => (int) ObjectFields.End;

        public ulong Guid
        {
            get => (ulong) UpdateData[ObjectFields.Guid];
            set => SetUpdateField((int) ObjectFields.Guid, value);
        }

        public byte Type
        {
            get => (byte) UpdateData[(int) ObjectFields.Type];
            set => SetUpdateField((int) ObjectFields.Type, value);
        }

        public int Entry
        {
            get => (int) UpdateData[(int) ObjectFields.Entry];
            set => SetUpdateField((int) ObjectFields.Entry, value);
        }

        public float Scale
        {
            get => (float) UpdateData[(int) ObjectFields.ScaleX];
            set => SetUpdateField((int) ObjectFields.ScaleX, value);
        }
    }
}
