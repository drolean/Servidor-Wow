using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_LOOT_METHOD : Common.Network.PacketReader
    {
        public ulong Master;
        public int Method;
        public int Threshold;

        public CMSG_LOOT_METHOD(byte[] data) : base(data)
        {
            Method = ReadInt32();
            Master = ReadUInt64();
            Threshold = ReadInt32();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_LOOT_METHOD] Method: {Method} Master: {Master} Threshold: {Threshold}");
#endif
        }
    }
}
