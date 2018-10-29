namespace RealmServer.Enums
{
    public enum FriendResults : byte
    {
        DB_ERROR = 0x0,
        LIST_FULL = 0x1,
        ONLINE = 0x2,
        OFFLINE = 0x3,
        NOT_FOUND = 0x4,
        REMOVED = 0x5,
        ADDED_ONLINE = 0x6,
        ADDED_OFFLINE = 0x7,
        ALREADY = 0x8,
        SELF = 0x9,
        ENEMY = 0xA,
        IGNORE_FULL = 0xB,
        IGNORE_SELF = 0xC,
        IGNORE_NOT_FOUND = 0xD,
        IGNORE_ALREADY = 0xE,
        IGNORE_ADDED = 0xF,
        IGNORE_REMOVED = 0x10,
        NameAmbiguous = 0x11,
        Error = 0x12
    }
}
