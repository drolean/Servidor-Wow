using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_BINDER_ACTIVATE : Common.Network.PacketReader
    {
        public ulong BinderUid;

        public CMSG_BINDER_ACTIVATE(byte[] data) : base(data)
        {
            BinderUid = ReadUInt64();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_BINDER_ACTIVATE] BinderUid: {BinderUid}");
#endif
        }
    }
}
