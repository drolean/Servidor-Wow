using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_QUESTGIVER_QUERY_QUEST : Common.Network.PacketReader
    {
        public int QuestId;
        public ulong Uid;

        public CMSG_QUESTGIVER_QUERY_QUEST(byte[] data) : base(data)
        {
            Uid = ReadUInt64();
            QuestId = ReadInt32();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_QUESTGIVER_QUERY_QUEST] Uid: {Uid} QuestId: {QuestId}");
#endif
        }
    }
}