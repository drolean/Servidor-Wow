using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_SET_WATCHED_FACTION : Common.Network.PacketReader
    {
        public int RepId;

        public CMSG_SET_WATCHED_FACTION(byte[] data) : base(data)
        {
            RepId = ReadInt32();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_SET_WATCHED_FACTION] RepId: {RepId}");
#endif
        }
    }
}