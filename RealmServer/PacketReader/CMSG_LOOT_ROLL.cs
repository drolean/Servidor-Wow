using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_LOOT_ROLL : Common.Network.PacketReader
    {
        public ulong LootedUid;
        public byte RollType;
        public int Slot;

        public CMSG_LOOT_ROLL(byte[] data) : base(data)
        {
            LootedUid = ReadUInt64();
            Slot = ReadInt32();
            RollType = ReadByte();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_LOOT_ROLL] LootedUid: {LootedUid} Slot: {Slot} RollType: {RollType}");
#endif
        }
    }
}