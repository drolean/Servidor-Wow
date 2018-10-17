using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_INSPECT : Common.Network.PacketReader
    {
        public ulong Uid;

        public CMSG_INSPECT(byte[] data) : base(data)
        {
            Uid = ReadUInt64();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_INSPECT] Uid: {Uid}");
#endif
        }
    }
}