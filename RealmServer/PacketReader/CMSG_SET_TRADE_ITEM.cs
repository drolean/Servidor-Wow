using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_SET_TRADE_ITEM : Common.Network.PacketReader
    {
        public byte MyBag;
        public byte MySlot;
        public byte Slot;

        public CMSG_SET_TRADE_ITEM(byte[] data) : base(data)
        {
            Slot = ReadByte();
            MyBag = ReadByte();
            MySlot = ReadByte();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_SET_TRADE_ITEM] Slot: {Slot} MyBag: {MyBag} MySlot: {MySlot}");
#endif
        }
    }
}