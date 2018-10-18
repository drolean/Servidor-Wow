using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_QUESTGIVER_STATUS_QUERY : Common.Network.PacketReader
    {
        public ulong QuestUid;

        public CMSG_QUESTGIVER_STATUS_QUERY(byte[] data) : base(data)
        {
            QuestUid = ReadUInt64();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_QUESTGIVER_STATUS_QUERY] QuestUid: {QuestUid}");
#endif
        }
    }
}