using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_BATTLEMASTER_JOIN : Common.Network.PacketReader
    {
        public uint InstaceId;
        public byte JoinAsGroup;
        public uint MapId;
        public ulong Uid;

        public CMSG_BATTLEMASTER_JOIN(byte[] data) : base(data)
        {
            Uid = ReadUInt64();
            InstaceId = ReadUInt32();
            MapId = ReadUInt32();
            JoinAsGroup = ReadByte();

#if DEBUG
            Log.Print(LogType.Debug,
                $"[CMSG_BATTLEMASTER_JOIN] Uid: {Uid} InstaceId: {InstaceId} MapId: {MapId} JoinAsGroup: {JoinAsGroup}");
#endif
        }
    }
}