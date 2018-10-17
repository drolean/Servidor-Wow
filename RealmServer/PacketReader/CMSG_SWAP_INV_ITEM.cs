using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_SWAP_INV_ITEM : Common.Network.PacketReader
    {
        public byte SrcSlot;
        public byte DstSlot;

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