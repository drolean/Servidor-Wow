using Common.Helpers;

namespace RealmServer.PacketReader
{
    /// <summary>
    ///     Swap item within the backpack
    /// </summary>
    public sealed class CMSG_SWAP_INV_ITEM : Common.Network.PacketReader
    {
        public byte DstSlot;
        public byte SrcSlot;

        public CMSG_SWAP_INV_ITEM(byte[] data) : base(data)
        {
            SrcSlot = ReadByte();
            DstSlot = ReadByte();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_SWAP_INV_ITEM] SrcSlot: {SrcSlot} DstSlot: {DstSlot}");
#endif
        }
    }
}