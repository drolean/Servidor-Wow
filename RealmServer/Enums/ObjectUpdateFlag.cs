using System;

namespace RealmServer.Enums
{
    [Flags]
    public enum ObjectUpdateFlag : byte
    {
        UpdateflagNone = 0x0000,
        UpdateflagSelf = 0x0001,
        UpdateflagTransport = 0x0002,
        UpdateflagFullguid = 0x0004,
        UpdateflagHighguid = 0x0008,
        UpdateflagAll = 0x0010,
        UpdateflagLiving = 0x0020,
        UpdateflagHasPosition = 0x0040
    }
}