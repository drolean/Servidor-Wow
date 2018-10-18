using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_SPIRIT_HEALER_ACTIVATE : Common.Network.PacketReader
    {
        public ulong ShUid;

        public CMSG_SPIRIT_HEALER_ACTIVATE(byte[] data) : base(data)
        {
            ShUid = ReadUInt64();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_SPIRIT_HEALER_ACTIVATE] ShUid: {ShUid}");
#endif
        }
    }
}