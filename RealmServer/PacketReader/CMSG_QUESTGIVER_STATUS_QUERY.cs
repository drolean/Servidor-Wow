using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_QUESTGIVER_STATUS_QUERY : Common.Network.PacketReader
    {
        public ulong CreatureUid;

        public CMSG_QUESTGIVER_STATUS_QUERY(byte[] data) : base(data)
        {
            CreatureUid = ReadUInt64();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_QUESTGIVER_STATUS_QUERY] CreatureUid: {CreatureUid}");
#endif
        }
    }
}
