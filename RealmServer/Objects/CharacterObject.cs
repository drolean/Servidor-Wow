using System.Collections;

namespace RealmServer.Objects
{
    public enum EObjectFields
    {
        OBJECT_FIELD_GUID = 0x0,    // 0x000 - Size: 2 - Type: GUID - Flags: PUBLIC
        OBJECT_FIELD_TYPE = 0x2,    // 0x002 - Size: 1 - Type: INT - Flags: PUBLIC
        OBJECT_FIELD_ENTRY = 0x3,   // 0x003 - Size: 1 - Type: INT - Flags: PUBLIC
        OBJECT_FIELD_SCALE_X = 0x4, // 0x004 - Size: 1 - Type: FLOAT - Flags: PUBLIC
        OBJECT_FIELD_PADDING = 0x5, // 0x005 - Size: 1 - Type: INT - Flags: NONE
        OBJECT_END = 0x6,
    }

    public class CharacterObject : BaseUnit
    {
        private BitArray UpdateMask = new BitArray(FIELD_MASK_SIZE_PLAYER, false);
        private Hashtable UpdateData = new Hashtable();

        public void SetUpdateFlag(int pos, float value)
        {
            UpdateMask.Set(pos, true);
            UpdateData(pos) = (value);
        }

        public void FillAllUpdateFlags()
        {
            SetUpdateFlag(EObjectFields.OBJECT_FIELD_GUID, GUID);
            SetUpdateFlag(EObjectFields.OBJECT_FIELD_TYPE, 25);
            SetUpdateFlag(EObjectFields.OBJECT_FIELD_SCALE_X, Size);
        }
    }
}
