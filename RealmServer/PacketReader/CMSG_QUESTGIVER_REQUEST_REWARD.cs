using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_QUESTGIVER_REQUEST_REWARD : Common.Network.PacketReader
    {
        public int QuestId;
        public ulong Uid;

        public CMSG_QUESTGIVER_REQUEST_REWARD(byte[] data) : base(data)
        {
            Uid = ReadUInt64();
            QuestId = ReadInt32();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_QUESTGIVER_REQUEST_REWARD] Uid: {Uid} QuestId: {QuestId}");
#endif
        }
    }
}
