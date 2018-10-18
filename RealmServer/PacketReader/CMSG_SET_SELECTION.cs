using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_SET_SELECTION : Common.Network.PacketReader
    {
        public ulong Uid;

        public CMSG_SET_SELECTION(byte[] data) : base(data)
        {
            Uid = ReadUInt64();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_SET_SELECTION] Uid: {Uid}");
#endif
        }
    }
}