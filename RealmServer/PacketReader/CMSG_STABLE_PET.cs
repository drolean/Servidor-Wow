using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_STABLE_PET : Common.Network.PacketReader
    {
        public ulong StableId;

        public CMSG_STABLE_PET(byte[] data) : base(data)
        {
            StableId = ReadUInt64();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_STABLE_PET] StableId: {StableId}");
#endif
        }
    }
}