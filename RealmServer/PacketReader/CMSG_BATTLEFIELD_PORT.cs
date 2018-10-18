using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_BATTLEFIELD_PORT : Common.Network.PacketReader
    {
        public byte Action;
        public uint MapId;

        public CMSG_BATTLEFIELD_PORT(byte[] data) : base(data)
        {
            Action = ReadByte();
            MapId = ReadUInt32();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_BATTLEFIELD_PORT] Action: {Action} MapId: {MapId}");
#endif
        }
    }
}