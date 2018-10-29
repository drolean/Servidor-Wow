using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_AUTOEQUIP_ITEM_SLOT : Common.Network.PacketReader
    {
        public byte DestSlot;
        public ulong ObjectUid;

        public CMSG_AUTOEQUIP_ITEM_SLOT(byte[] data) : base(data)
        {
            ObjectUid = ReadUInt64();
            DestSlot = ReadByte();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_AUTOEQUIP_ITEM_SLOT] ObjectUid: {ObjectUid} DestSlot: {DestSlot}");
#endif
        }
    }
}
