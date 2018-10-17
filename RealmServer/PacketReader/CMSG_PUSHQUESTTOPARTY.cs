using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_PUSHQUESTTOPARTY : Common.Network.PacketReader
    {
        public int QuestId;

        public CMSG_PUSHQUESTTOPARTY(byte[] data) : base(data)
        {
            QuestId = ReadInt32();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_PUSHQUESTTOPARTY] QuestId: {QuestId}");
#endif
        }
    }
}