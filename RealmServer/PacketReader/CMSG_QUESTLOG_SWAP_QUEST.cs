using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_QUESTLOG_SWAP_QUEST : Common.Network.PacketReader
    {
        public byte Slot1;
        public byte Slot2;

        public CMSG_QUESTLOG_SWAP_QUEST(byte[] data) : base(data)
        {
            Slot1 = ReadByte();
            Slot2 = ReadByte();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_QUESTLOG_SWAP_QUEST] Slot1: {Slot1} Slot2: {Slot2}");
#endif
        }
    }
}