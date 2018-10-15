namespace RealmServer.Enums
{
    public enum ObjectFields
    {
        OBJECT_FIELD_GUID = 0x0, // 0x000 - Size: 2 - Type: GUID  - Flags: PUBLIC
        OBJECT_FIELD_TYPE = 0x2, // 0x002 - Size: 1 - Type: INT   - Flags: PUBLIC
        OBJECT_FIELD_ENTRY = 0x3, // 0x003 - Size: 1 - Type: INT   - Flags: PUBLIC
        OBJECT_FIELD_SCALE_X = 0x4, // 0x004 - Size: 1 - Type: FLOAT - Flags: PUBLIC
        OBJECT_FIELD_PADDING = 0x5, // 0x005 - Size: 1 - Type: INT   - Flags: NONE
        OBJECT_END = 0x6
    }
}