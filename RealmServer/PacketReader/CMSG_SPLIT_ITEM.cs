using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_SPLIT_ITEM : Common.Network.PacketReader
    {
        public byte Count;
        public byte DstBag;
        public byte DstSlot;
        public byte SrcBag;
        public byte SrcSlot;

        public CMSG_SPLIT_ITEM(byte[] data) : base(data)
        {
            SrcBag = ReadByte();
            SrcSlot = ReadByte();
            DstBag = ReadByte();
            DstSlot = ReadByte();
            Count = ReadByte();

#if DEBUG
            Log.Print(LogType.Debug,
                $"[CMSG_SPLIT_ITEM] SrcBag: {SrcBag} SrcSlot: {SrcSlot} DstBag: {DstBag} DstSlot: {DstSlot} Count: {Count}");
#endif
        }
    }
}
