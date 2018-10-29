using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_SET_WATCHED_FACTION : Common.Network.PacketReader
    {
        public int FactionId;

        public CMSG_SET_WATCHED_FACTION(byte[] data) : base(data)
        {
            FactionId = ReadInt32();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_SET_WATCHED_FACTION] FactionId: {FactionId}");
#endif
        }
    }
}
