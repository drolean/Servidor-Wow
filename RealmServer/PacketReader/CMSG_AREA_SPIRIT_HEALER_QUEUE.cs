using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_AREA_SPIRIT_HEALER_QUEUE : Common.Network.PacketReader
    {
        public ulong Uid;

        public CMSG_AREA_SPIRIT_HEALER_QUEUE(byte[] data) : base(data)
        {
            Uid = ReadUInt64();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_AREA_SPIRIT_HEALER_QUEUE] Uid: {Uid}");
#endif
        }
    }
}