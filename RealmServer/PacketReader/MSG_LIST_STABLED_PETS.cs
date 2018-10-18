using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class MSG_LIST_STABLED_PETS : Common.Network.PacketReader
    {
        public ulong StableId;

        public MSG_LIST_STABLED_PETS(byte[] data) : base(data)
        {
            StableId = ReadUInt64();

#if DEBUG
            Log.Print(LogType.Debug, $"[MSG_LIST_STABLED_PETS] StableId: {StableId}");
#endif
        }
    }
}