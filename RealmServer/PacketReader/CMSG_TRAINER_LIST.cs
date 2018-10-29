using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_TRAINER_LIST : Common.Network.PacketReader
    {
        public ulong VendorUid;

        public CMSG_TRAINER_LIST(byte[] data) : base(data)
        {
            VendorUid = ReadUInt64();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_TRAINER_LIST] VendorUid: {VendorUid}");
#endif
        }
    }
}
