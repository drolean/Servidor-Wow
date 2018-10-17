using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_CLEAR_TRADE_ITEM : Common.Network.PacketReader
    {
        public byte Slot;

        public CMSG_CLEAR_TRADE_ITEM(byte[] data) : base(data)
        {
            Slot = ReadByte();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_CLEAR_TRADE_ITEM] Slot: {Slot}");
#endif
        }
    }
}