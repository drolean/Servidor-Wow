using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_DUEL_ACCEPTED : Common.Network.PacketReader
    {
        public ulong ObjectUid;

        public CMSG_DUEL_ACCEPTED(byte[] data) : base(data)
        {
            ObjectUid = ReadUInt64();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_DUEL_ACCEPTED] ObjectUid: {ObjectUid}");
#endif
        }
    }
}
