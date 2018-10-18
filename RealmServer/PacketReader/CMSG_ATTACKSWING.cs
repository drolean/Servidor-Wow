using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_ATTACKSWING : Common.Network.PacketReader
    {
        public ulong Uid;

        public CMSG_ATTACKSWING(byte[] data) : base(data)
        {
            Uid = ReadUInt64();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_ATTACKSWING] Uid: {Uid}");
#endif
        }
    }
}