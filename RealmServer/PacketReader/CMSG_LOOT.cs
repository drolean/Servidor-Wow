using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_LOOT : Common.Network.PacketReader
    {
        public ulong Uid;

        public CMSG_LOOT(byte[] data) : base(data)
        {
            Uid = ReadUInt64();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_LOOT] Uid: {Uid}");
#endif
        }
    }
}
