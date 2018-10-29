using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_REQUEST_ACCOUNT_DATA : Common.Network.PacketReader
    {
        public uint Type;

        public CMSG_REQUEST_ACCOUNT_DATA(byte[] data) : base(data)
        {
            Type = ReadUInt32();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_REQUEST_ACCOUNT_DATA] Type: {Type}");
#endif
        }
    }
}
