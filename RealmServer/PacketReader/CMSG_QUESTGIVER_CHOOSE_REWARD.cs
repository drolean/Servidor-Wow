using System;
using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_QUESTGIVER_CHOOSE_REWARD : Common.Network.PacketReader
    {
        public UInt64 Uid;
        public int QuestId;
        public int Reward;

        public CMSG_QUESTGIVER_CHOOSE_REWARD(byte[] data) : base(data)
        {
            Uid = ReadUInt64();
            QuestId = ReadInt32();
            Reward = ReadInt32();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_QUESTGIVER_CHOOSE_REWARD] Uid: {Uid} QuestId: {QuestId} Reward: {Reward}");
#endif
        }
    }
}