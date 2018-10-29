using Common.Helpers;

namespace RealmServer.PacketReader
{
    /// <summary>
    ///     Swap items within bags
    /// </summary>
    public sealed class CMSG_SWAP_ITEM : Common.Network.PacketReader
    {
        public byte DstBag;
        public byte DstSlot;
        public byte SrcBag;
        public byte SrcSlot;

        public CMSG_SWAP_ITEM(byte[] data) : base(data)
        {
            DstBag = ReadByte();
            DstSlot = ReadByte();
            SrcBag = ReadByte();
            SrcSlot = ReadByte();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_SWAP_ITEM] DstBag: {DstBag} DstSlot: {DstSlot} " +
                                     $"SrcBag: {SrcBag} SrcSlot: {SrcSlot}");
#endif
        }
    }
}
