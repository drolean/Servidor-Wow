using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_DESTROYITEM : Common.Network.PacketReader
    {
        public byte Count;
        public byte SrcBag;
        public byte SrcSlot;

        public CMSG_DESTROYITEM(byte[] data) : base(data)
        {
            SrcBag = ReadByte();
            SrcSlot = ReadByte();
            Count = ReadByte();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_DESTROYITEM] SrcBag: {SrcBag} SrcSlot: {SrcSlot} Count: {Count}");
#endif
        }
    }
}
