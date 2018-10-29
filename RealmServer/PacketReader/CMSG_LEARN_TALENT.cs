using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_LEARN_TALENT : Common.Network.PacketReader
    {
        public int Rank;
        public uint TalentId;

        public CMSG_LEARN_TALENT(byte[] data) : base(data)
        {
            TalentId = ReadUInt32();
            Rank = ReadInt32();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_LEARN_TALENT] TalentId: {TalentId} Rank: {Rank}");
#endif
        }
    }
}
