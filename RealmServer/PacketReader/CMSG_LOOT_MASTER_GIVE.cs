using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_LOOT_MASTER_GIVE : Common.Network.PacketReader
    {
        public ulong LootUid;
        public ulong PlayerUid;
        public byte SlotId;

        public CMSG_LOOT_MASTER_GIVE(byte[] data) : base(data)
        {
            SlotId = ReadByte();
            LootUid = ReadUInt64();
            PlayerUid = ReadUInt64();

#if DEBUG
            Log.Print(LogType.Debug,
                $"[CMSG_LOOT_MASTER_GIVE] SlotId: {SlotId} LootUid: {LootUid} PlayerUid: {PlayerUid}");
#endif
        }
    }
}