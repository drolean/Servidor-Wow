using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_INITIATE_TRADE : Common.Network.PacketReader
    {
        public ulong Uid;

        public CMSG_INITIATE_TRADE(byte[] data) : base(data)
        {
            Uid = ReadUInt64();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_INITIATE_TRADE] Uid: {Uid}");
#endif
        }
    }
}
