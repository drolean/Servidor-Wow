using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_QUESTLOG_REMOVE_QUEST : Common.Network.PacketReader
    {
        public int Slot;

        public CMSG_QUESTLOG_REMOVE_QUEST(byte[] data) : base(data)
        {
            Slot = ReadByte();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_QUESTLOG_REMOVE_QUEST] Slot: {Slot}");
#endif
        }
    }
}