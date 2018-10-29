using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_NPC_TEXT_QUERY : Common.Network.PacketReader
    {
        public long TextId;
        public ulong Uid;

        public CMSG_NPC_TEXT_QUERY(byte[] data) : base(data)
        {
            TextId = ReadInt32();
            Uid = ReadUInt64();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_NPC_TEXT_QUERY] TextId: {TextId} Uid: {Uid}");
#endif
        }
    }
}
