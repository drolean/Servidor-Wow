using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_FAR_SIGHT : Common.Network.PacketReader
    {
        public byte Op;

        public CMSG_FAR_SIGHT(byte[] data) : base(data)
        {
            Op = ReadByte();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_FAR_SIGHT] Op: {Op}");
#endif
        }
    }
}