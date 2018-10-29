using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_PETITION_SHOWLIST : Common.Network.PacketReader
    {
        public ulong VendorUid;

        public CMSG_PETITION_SHOWLIST(byte[] data) : base(data)
        {
            VendorUid = ReadUInt64();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_PETITION_SHOWLIST] VendorUid: {VendorUid}");
#endif
        }
    }
}
