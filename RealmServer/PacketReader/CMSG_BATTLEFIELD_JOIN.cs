using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_BATTLEFIELD_JOIN : Common.Network.PacketReader
    {
        public uint MapId;

        public CMSG_BATTLEFIELD_JOIN(byte[] data) : base(data)
        {
            MapId = ReadUInt32();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_BATTLEFIELD_JOIN] MapId: {MapId}");
#endif
        }
    }
}
