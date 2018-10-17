using Common.Helpers;

namespace RealmServer.PacketReader
{
    /// <summary>
    /// CMSG_QUEST_QUERY represents a packet sent by the client when it wants to retrieve quest information.
    /// </summary>
    public sealed class CMSG_QUEST_QUERY : Common.Network.PacketReader
    {
        public int QuestId;

        public CMSG_QUEST_QUERY(byte[] data) : base(data)
        {
            QuestId = ReadInt32();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_QUEST_QUERY] QuestId: {QuestId}");
#endif
        }
    }
}